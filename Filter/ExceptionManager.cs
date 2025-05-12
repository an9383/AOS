using HACCP.Libs;
using HACCP.Services.Uc;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HACCP.Filter
{
    public class ExceptionManager : HandleErrorAttribute, IExceptionFilter
    {
        //private static readonly ILog log = LogManager.GetLogger("ExceptionManagerFile");

        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            Log(filterContext, filterContext.Exception, filterContext.RouteData, filterContext.HttpContext);
        }

        private void Log(ExceptionContext filterContext, Exception exception, RouteData routeData, HttpContextBase httpContext)
        {
            LoggingService service = new LoggingService(LoggingType.Error, routeData, httpContext, exception);
            service.writeLogError(LoggingErrorType.Action);
        }
    }
}