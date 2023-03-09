using HACCP.Filter;
using System.Web;
using System.Web.Mvc;

namespace HACCP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // 세션체크 공통모듈(2020-09-03, 조형진)추가
            filters.Add(new CheckSession());

            // 로깅 - action monitoring filter (2020-11-11)추가
            filters.Add(new ActionMonitor());

            // 로깅 - exception 관리 공통모듈 추가(2020-11-12)
            filters.Add(new ExceptionManager());


            // : 기존 default exception 처리 주석처리
            //filters.Add(new HandleErrorAttribute());
                        

        }
    }
}
