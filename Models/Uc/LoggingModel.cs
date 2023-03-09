using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace HACCP.Models.Uc
{
    /// <summary>
    /// 로깅모델
    /// </summary>
    public interface ILoggingModel
    {
        /// <summary>
        /// 로그로 기록할 로그 stirng 생성
        /// </summary>
        string makeLogString();

    }
    /// <summary>
    /// LoggingModel 마스터
    /// </summary>
    public class LoggingModel : ILoggingModel
	{		
		public string ServerDomain { get; set; }
		public string ControllerName { get; set; }
		public string ActionName { get; set; }		
		public string MenuName { get; set; }
		public string ReqPath { get; set; }
		public string ReqUrl { get; set; }
		public string ReqClientIp { get; set; }
		public string ReqClientName { get; set; }
		public string ReqBrowserName { get; set; }
		public string ReqFormParam { get; set; }
		public string ReqQueryString { get; set; }
		public string ResStatusCode { get; set; }
		public string ResStatus { get; set; }
		public string ResContentType { get; set; }
		public string SesLoginId { get; set; }

		StringBuilder ReqInfo;
		StringBuilder ResInfo;
		StringBuilder SesInfo;


		public LoggingModel()
		{
		}

		/// <summary>
		/// 공통 Loging문자열 생성
		/// </summary>
		/// <param name="stepName">STEP명:(START, END, "")=> </param>
		/// <param name="elapsedTime">elapsed(m/s)</param>
		/// <returns></returns>
        public string makeLogStringCommon(string stepName, long elapsedTime)
        {
			string logString = string.Empty;

			ReqInfo = new StringBuilder();
			ResInfo = new StringBuilder();
			SesInfo = new StringBuilder();

			//1. Requst 정보 구성
			ReqInfo.Append("1. Request 정보\n");
			if (stepName == "END")
			{
				ReqInfo.Append("[10] 수행시간(m/s) : " + elapsedTime + "\n");
			}
			else
			{
				ReqInfo.Append("[10] Url :" + this.ReqUrl + "\n");
				ReqInfo.Append("[11] Client IP : " + this.ReqClientIp + "\n");
				ReqInfo.Append("[12] Client Name : " + this.ReqClientName + "\n");
				ReqInfo.Append("[13] 브라우져 : " + this.ReqBrowserName + "\n");
				ReqInfo.Append("[14] form 파라미터 \n");
				ReqInfo.AppendLine("--------------------------------------------------");
				ReqInfo.Append(this.ReqFormParam);
				ReqInfo.AppendLine("--------------------------------------------------");
				ReqInfo.Append("[15] Querystring 파라미터 \n");
				ReqInfo.AppendLine("--------------------------------------------------");
				ReqInfo.Append(this.ReqQueryString);
				ReqInfo.AppendLine("--------------------------------------------------");

			}
			ReqInfo.AppendLine();

			//2. Response 정보 구성
			ResInfo.Append("2. Response 정보\n");
			ResInfo.Append("[20] StatusCode : " + this.ResStatusCode + "\n");
			ResInfo.Append("[21] Status : " + this.ResStatus + "\n");
			ResInfo.Append("[22] ContentType : " + this.ResContentType + "\n");
			ResInfo.AppendLine();

			//3. session 정보 구성
			SesInfo.Append("3. Session 정보\n");
			//sesInfo.Append("[30] session 변수 totoal : " + ses.Count + "\n");
			SesInfo.Append("[30] 사용자ID : " + this.SesLoginId + "\n");

			logString = ReqInfo.ToString() + ResInfo.ToString() + SesInfo.ToString();

			return logString;
		}

        public string makeLogString()
        {
			return "child 에서 구현해라~";
        }
    }

	/// <summary>
	/// LoggingModel > action모델
	/// </summary>
	public class LoggingActionModel : ILoggingModel
	{
		public LoggingModel PloggingModel { get; set; }
		/// <summary>
		/// 스템명 (START / END )
		/// </summary>
		public string StepName { get; set; }
		/// <summary>
		/// execute time (단위 :m/s)
		/// </summary>
		public long ElapsedTime { get; set; }

		StringBuilder FirstInfo;
		
		public string makeLogString()
        {
			string logString = string.Empty;			
			string bodyInfo;
			
			FirstInfo = new StringBuilder();
			
			FirstInfo.AppendLine();
			FirstInfo.Append("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓\n");
			FirstInfo.Append("〓 [풀경로]  : " + PloggingModel.ReqPath + "\n");
			FirstInfo.Append("〓 [매뉴명]  : " + PloggingModel.MenuName+ "\n");
			FirstInfo.Append("〓 [단계]  : " + this.StepName + "\n");
			FirstInfo.Append("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓\n");

			bodyInfo = PloggingModel.makeLogStringCommon(StepName, this.ElapsedTime);

			logString = FirstInfo.ToString() + bodyInfo;
			return logString;
		}      
    }

	/// <summary>
	/// LoggingModel 마스터 - error	
	/// </summary>
	public class LoggingErrorModel : ILoggingModel
	{
		public LoggingModel PloggingModel { get; set; }

		public int ErrLogNo { get; set; }
		/// <summary>
		/// 에러로그타입(Action,Global)
		/// </summary>
		public string ErrLogType { get; set; }
		public string ErrExceptionMesg { get; set; }
		public string ErrExceptionStackTrace { get; set; }
		public DateTime ErrDateTime { get; set; }

		StringBuilder FirstInfo;
		StringBuilder ErrInfo;

		public string makeLogString()
        {
			string logString = string.Empty;
			string bodyInfo;

			FirstInfo = new StringBuilder();
			ErrInfo = new StringBuilder();

			FirstInfo.AppendLine();
			FirstInfo.Append("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓\n");			
			FirstInfo.Append("〓 [에러경로]   : " + PloggingModel.ReqPath + "\n");
			FirstInfo.Append("〓 [매뉴명]  : " + PloggingModel.MenuName + "\n");
			FirstInfo.Append("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓\n");

			bodyInfo = PloggingModel.makeLogStringCommon("", 0);

			ErrInfo.Append("▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲ 오류 정보 ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲>\n");
			ErrInfo.Append("[40] 오류메시지 : " + this.ErrExceptionMesg + "\n");
			ErrInfo.Append("[41] 오류 Stack\n");
			ErrInfo.Append("*********************************************************\n");
			ErrInfo.Append(this.ErrExceptionStackTrace + "\n");
			ErrInfo.Append("*********************************************************\n");
			ErrInfo.AppendLine();

			logString = FirstInfo.ToString() + bodyInfo + ErrInfo.ToString();
			return logString;
		}
    }
}