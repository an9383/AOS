using HACCP.Models;
using HACCP.Services.Fac;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class NotifyController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(NotifyController));
        private NotifyService notifyService = new NotifyService();


        /// <summary>
        /// 설비 고장 신고 조회
        /// </summary>
        /// <param name="pPeriod"></param>
        /// <param name="pSdate"></param>
        /// <param name="pEdate"></param>
        /// <param name="pStatus"></param>
        /// <param name="pEquip"></param>
        /// <returns></returns>
        [Route("Doc/Notify")]
        [HttpGet]
        public ActionResult Notify(string pPeriod, string pSdate, string pEdate, string pStatus, string pEquip)
        {
            // pPeriod 입력값 체크
            // 신고일자를 기본으로 체크 (신고일자 = 1, 처리일자 = 2)
            if(pPeriod == null || pPeriod == "")
            {
                pPeriod = "1";
            }

            // pSdate 입력값 체크
            // 한달 전 날짜를 기본으로 표시
            // ? 한달 전 구하는 공식 필요
            if(pSdate == null || pSdate == "")
            {
                pSdate = "2020-05-08";
            }

            // pEdate 입력값 체크
            // 오늘 날짜를 기본으로 표시
            // ? 오늘 날짜를 구하는 공식 필요
            if(pEdate == null || pEdate == "")
            {
                pEdate = "2020-06-08";
            }

            // pStatus 입력값 체크
            // ? 상태값에 대한 내용 조사 필요
            if(pStatus == null || pStatus == "")
            {
                pStatus = "1";
            }

            // pEquip 입력값 체크
            // 장비코드 또는 장비명
            // 예.NY-C1-002, 분말충전기
            if (pEquip == null || pEquip == "")
            {
                pEquip = "";
            }

            Notify pNotify = new Notify();
            pNotify.sdate = pSdate;
            pNotify.edate = pEdate;
            pNotify.period = pPeriod;
            pNotify.status = pStatus;
            pNotify.equip = pEquip;

            NotifyService notifyService = new NotifyService();
            List<Notify> notify = notifyService.Select(pNotify);
            ViewBag.Notify = notify;

            return View();
        }
    }
}
