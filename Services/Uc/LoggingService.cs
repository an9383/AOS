using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Uc;
using log4net;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace HACCP.Services.Uc
{
    public class LoggingService
    {
        private static readonly ILog Log_Action = LogManager.GetLogger("ActionMonitorFile");
        private static readonly ILog Log_Exception = LogManager.GetLogger("ExceptionManagerFile");

        //ExceptionManagerFile

        private BllSpExecute _bllSpExecute;
        /// <summary>
        /// 로깅모델 마스터 : action로그 / Error로그의 공통속성 마스터
        /// </summary>
        private LoggingModel loggingModel;

        /// <summary>
        /// controller명
        /// </summary>
        readonly string ControllerName;
        /// <summary>
        /// Action매서드명
        /// </summary>
        readonly string ActionName;
        /// <summary>
        /// 로그타입:Action / Error
        /// </summary>
        readonly string LogType;


        HttpRequestBase Req;
        HttpResponseBase Res;
        HttpSessionStateBase Ses;
        Exception Exception;

        public LoggingService()
        {
        }

        /// <summary>
        /// 로깅서비스 생성자
        /// </summary>
        /// <param name="logType">로그타입:Action / Error</param>
        public LoggingService(LoggingType logType) : this()
        {
            this.LogType = logType.ToString();

        }

        /// <summary>
        /// 로깅서비스 생성자
        /// </summary>
        /// <param name="logType">로그타입:Action / Error</param>
        /// <param name="routeData">RouteData</param>
        public LoggingService(LoggingType logType, RouteData routeData) : this(logType)
        {
            if (routeData != null)
            {
                this.ControllerName = routeData.Values["controller"].ToString();
                this.ActionName = routeData.Values["action"].ToString();
            }
            else
            {
                this.ControllerName = string.Empty;
                this.ActionName = string.Empty;
            }
            
        }

        /// <summary>
        /// 로깅서비스 생성자
        /// </summary>
        /// <param name="logType">로그타입:Action / Error</param>
        /// <param name="routeData">RouteData</param>
        /// <param name="httpContext">HttpContextBase</param>
        public LoggingService(LoggingType logType, RouteData routeData, HttpContextBase httpContext)
           : this(logType, routeData)
        {          

            this.Req = httpContext.Request;
            this.Res = httpContext.Response;
            this.Ses = httpContext.Session;
        }

        /// <summary>
        /// 로깅서비스 생성자
        /// </summary>
        /// <param name="logType">로그타입:Action / Error</param>
        /// <param name="routeData">RouteData</param>
        /// <param name="httpContext">HttpContextBase</param>
        /// <param name="exception">Exception</param>
        public LoggingService(LoggingType logType, RouteData routeData, HttpContextBase httpContext, Exception exception)
           : this(logType, routeData, httpContext)
        {
            this.Exception = exception;
        }   


        /// <summary>
        ///  로그 기록 - ACtion Log
        /// </summary>
        /// <param name="stepName">단계명:START/END</param>
        /// <param name="elpasedTime">elapsedTime(m/s)</param>
        internal void writeLogAction(string stepName, long elpasedTime)
        {
            // (IloggingModel 구현체 : Action) 설정
            LoggingActionModel loggingImpl = new LoggingActionModel()
            {                
                StepName = stepName,
                ElapsedTime = elpasedTime
            };
            //로그등록 공통
            this.writeLog(loggingImpl);
        }

        /// <summary>
        ///  로그 기록 - Error Log
        /// </summary>
        /// <param name="errLogType">에러로그타입(Action/Global)</param>
        internal void writeLogError(LoggingErrorType errLogType)
        {
            // (IloggingModel 구현체 : Exception) 설정
            LoggingErrorModel loggingImpl = new LoggingErrorModel()
            {                
                ErrLogType = errLogType.ToString(),
                ErrExceptionMesg = this.Exception.Message,
                ErrExceptionStackTrace = this.Exception.StackTrace
            };
            //로그등록 공통
            this.writeLog(loggingImpl);

            // LogginerError DB저장
            this.writeLogErrorDb(loggingImpl);
        }

        /// <summary>
        /// 로그 기록 - Error Log(DB기록)
        /// </summary>
        /// <param name="errorModel">LoggingErrorModel 구현체</param>
        private void writeLogErrorDb(LoggingErrorModel errorModel)
        {
            // error 로그일때만 db에 기록
            if (LoggingType.Error.ToString().Equals(this.LogType))
            {
                String[] sqlParam = GetSqlParam(errorModel);
                if (_bllSpExecute == null)
                {
                    _bllSpExecute = new BllSpExecute();
                }
                string res = _bllSpExecute.SpExecuteString("SP_SYS_LOGGING_ERROR", "I", sqlParam);
            }
        }

        /// <summary>
        /// 로그 기록 - 공통 : Action, Error 로그의 공통부분 구현
        /// </summary>        
        /// <param name="loggingImpl">ILoggingModel 구현체</param>
        private void writeLog(ILoggingModel loggingImpl)
        {
            // 로그 skip 체크
            if (isSkipLog()) return;

            //(공통) LoggingModel 설정
            this.setLoggingModel();

            // 구현체 LoggingModel 설정
            if(loggingImpl is LoggingActionModel)
            {
                ((LoggingActionModel)loggingImpl).PloggingModel = this.loggingModel;
            }
            else if(loggingImpl is LoggingErrorModel)
            {
                ((LoggingErrorModel)loggingImpl).PloggingModel = this.loggingModel;
            }
            else
            {
                //미정의된 객체
                new Exception("ILoggingModel의 구현체가 아닙니다!");
                return ;
            }

            // 파일 logging
            String logString = loggingImpl.makeLogString();
            if (LoggingType.Action.ToString().Equals(this.LogType))
            {
                Log_Action.Info(logString);
            }else if(LoggingType.Error.ToString().Equals(this.LogType))
            {
                Log_Exception.Error(logString);
            }
        }

        /// <summary>
        /// 각구현체의 공통 LoggingModel 설정
        /// </summary>
        private void setLoggingModel()
        {
            // 폼param구성
            StringBuilder formParam = new StringBuilder();
            if (Req.Form.AllKeys.Count() > 0)
            {
                int i = 1;
                foreach (var item in Req.Form.AllKeys)
                {
                    formParam.Append( i +")"+ item + " => " + Req.Form[item] + "\n");
                    ++i;
                }
            }
            else
            {
                formParam.Append("전송된 폼 param 없음\n");
            }

            string queryStringParam = Req.QueryString.ToString();
            if (String.IsNullOrEmpty(queryStringParam))
            {
                queryStringParam = "전송된 querystring param 없음\n";
            }

            string menuName = string.Empty;
            DataTable menuAuthList;
            if (Ses["loginMenuAuthList"] != null) {
                menuAuthList = (DataTable)Ses["loginMenuAuthList"];
                var row = menuAuthList.AsEnumerable()
                                    //.Where(s => s.Field<string>("form_cd").Contains(this.ActionName))
                                    .Where(s => this.ActionName.Contains(s.Field<string>("form_cd")))                                    
                                    .FirstOrDefault();
                if(row !=null)
                {
                    menuName = row.Field<string>("form_nm");
                }
            }
            // 로깅모델 setting
            this.loggingModel = new LoggingModel()
            {
                ServerDomain = Req.ServerVariables["HTTP_HOST"],
                ControllerName = this.ControllerName,
                ActionName = this.ActionName,
                MenuName = menuName,
                ReqPath = Req.Path.ToString(),
                ReqUrl = Req.Url.ToString(),
                ReqClientIp = Req.UserHostAddress.ToString(),
                ReqClientName = Req.UserHostName.ToString(),
                ReqBrowserName = Req.Browser.Browser + "(" + Req.Browser.MajorVersion + ")",
                ReqFormParam = formParam.ToString(),
                ReqQueryString = queryStringParam,
                ResStatusCode = Res.StatusCode.ToString(),
                ResStatus = Res.Status.ToString(),
                ResContentType = Res.ContentType.ToString(),
                SesLoginId = Ses["loginID"]?.ToString()
            };
             
        }

        /// <summary>
        /// sql파라미터구성-에러로그(LoggingErrorModelr구현체를 이용 paramter구성) 파라미터구성
        /// </summary>
        /// <param name="errorModel">파라미터대상 LoggingErrorModelr구현체</param>
        /// <returns></returns>
        private String[] GetSqlParam(LoggingErrorModel errorModel)
        {
            string[] param = new string[] {
                "@ErrLogType:" + errorModel.ErrLogType,
                "@ServerDomain:" + errorModel.PloggingModel.ServerDomain,
                "@ControllerName:" + errorModel.PloggingModel.ControllerName,
                "@ActionName:" + errorModel.PloggingModel.ActionName,
                "@MenuName:" + errorModel.PloggingModel.MenuName,
                "@ReqPath:" + errorModel.PloggingModel.ReqPath,
                "@ReqUrl:" + errorModel.PloggingModel.ReqUrl,
                "@ReqClientIp:" + errorModel.PloggingModel.ReqClientIp,
                "@ReqClientName:" + errorModel.PloggingModel.ReqClientName,
                "@ReqBrowserName:" + errorModel.PloggingModel.ReqBrowserName,
                "@ReqFormParam:" + errorModel.PloggingModel.ReqFormParam,
                "@ReqQueryString:" + errorModel.PloggingModel.ReqQueryString,
                "@ResStatusCode:" + errorModel.PloggingModel.ResStatusCode,
                "@ResStatus:" + errorModel.PloggingModel.ResStatus,
                "@ResContentType:" + errorModel.PloggingModel.ResContentType,
                "@SesLoginId:" + errorModel.PloggingModel.SesLoginId,
                "@ErrExceptionMesg:" + errorModel.ErrExceptionMesg,
                "@ErrExceptionStackTrace:" + errorModel.ErrExceptionStackTrace.Trim(),
            };
            return param;
        }

        /// <summary>
        /// 로그유형(Action Log / Errror Log)별 로그 스킵여부
        /// </summary>        
        /// <returns>로그 스키여부</returns>
        private bool isSkipLog()
        {
            bool isSkip = false;
            if (LoggingType.Action.ToString().Equals(this.LogType))
            {
                // 공통 controller 는 skip한다.
                if ("Comm".Equals(this.ControllerName) || "Uc".Equals(this.ControllerName))
                {
                    isSkip = true;
                }
            } else if (LoggingType.Error.ToString().Equals(this.LogType))
            {
                isSkip = false;
            }
            return isSkip;
        }
    }
}