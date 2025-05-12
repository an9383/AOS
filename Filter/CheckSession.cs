using HACCP.Libs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace HACCP.Filter
{
    public class CheckSession : ActionFilterAttribute, IActionFilter {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var ctx = context.HttpContext;

            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
                        
            if ("Comm".Equals(controller) 
                && ("Login".Equals(action) || "LoginRedirect".Equals(action) || "Index".Equals(action)) )
            {
                base.OnActionExecuting(context);
            }
            else
            {
                //로그인 필요한 경우에만 redirect (2020-09-03, 조형진)
                /*
                 * history
                 *  2020-11-09      로그인세션 종료시, LoginRedirect 페이지로 이동, 이후 js의 location으로 전체page 이동
                 */
                //if (ctx.Session["loginID"] == null)
                //{
                //    context.Result = new RedirectToRouteResult(
                //        new RouteValueDictionary(
                //            new { action = "LoginRedirect", controller = "Comm" }
                //            )
                //        );
                //    return;
                //}
                if (ctx.Session["loginID"] == null)
                {
                    context.Result = new HttpStatusCodeResult(401);

                    return;
                }
            } 
        }


    }
}