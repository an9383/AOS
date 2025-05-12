using CrystalDecisions.ReportSource;
using DevExpress.Web.Internal;
using HACCP.Libs;
using HACCP.Models.Uc;
using HACCP.Services.Uc;
using log4net;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HACCP.Filter
{
    public class ActionMonitor : ActionFilterAttribute
    {
        // [Timer] Start
        private Stopwatch loggingWatch = new Stopwatch();

        // 수행시작
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //// [Timer] Start
            //var loggingWatch = Stopwatch.StartNew();
            //filterContext.HttpContext.Items.Add(loggingTimeKey, 0);
            loggingWatch.Restart();

            // logging
            //Log("OnActionExecuting", filterContext.RouteData, filterContext.HttpContext, 0);
            Log("OnActionExecuting", filterContext.RouteData, filterContext.HttpContext, 0);
            //base.OnActionExecuting(filterContext);
        }

        //수행종료
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // [Timer] End
            //var loggingWatch = (Stopwatch)filterContext.HttpContext.Items[loggingTimeKey];
            //loggingWatch.Stop();
            //filterContext.HttpContext.Items.Remove(loggingTimeKey);
            loggingWatch.Stop();
            long timeSpent = loggingWatch.ElapsedMilliseconds;
            

            // logging
            Log("OnResultExecuted", filterContext.RouteData, filterContext.HttpContext, timeSpent);
            //base.OnResultExecuted(filterContext);
        }

        /*************************************************
         * * 나머지, 이벤트는 현재 사용하지 않는다.
         * **********************************************/
         
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {            
            //base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {         
            //base.OnResultExecuting(filterContext);
        }


        private void Log(string methodName, RouteData routeData, HttpContextBase httpContext, long elpasedTime)
        {
            string executeStep;
            // 단계지정            
            if (methodName == "OnActionExecuting")
            {   
                executeStep = "START";
            }
            else if (methodName == "OnResultExecuted")
            {
                executeStep = "END";
            }
            else
            {
                executeStep = methodName;
            }
            LoggingService service = new LoggingService(LoggingType.Action, routeData, httpContext);
            service.writeLogAction(executeStep, elpasedTime);
        }
    }
}