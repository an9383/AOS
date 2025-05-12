using HACCP.Services.Comm;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using HACCP.Libs;
using HACCP.Filter;
using HACCP.Models.PrdIn;
using HACCP.Services.PrdIn;
using HACCP.Attribute;

namespace HACCP.Controllers
{
    public class PrdInController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PrdInController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private ItemProductionService itemProductionService = new ItemProductionService();
        private PackingResultInq2Service PackingResultInq2Service = new PackingResultInq2Service();
        private ProductTransferService productTransferService = new ProductTransferService();
        private ItemOrderManageService itemOrderManageService = new ItemOrderManageService();

        #region ItemProduction - 완제품 입고 등록

        /// <summary>
        /// 완제품 입고 등록
        /// (첫 페이지 진입) 완제품 입고 등록 조회
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("PrdIn/ItemProduction")]
        [HttpGet]
        public ActionResult ItemProduction(ItemProduction model)
        {
            /* 조회 */
            model.start_date_S = DateTime.Today.AddDays(-30).ToShortDateString(); // (검색) 시작일
            model.end_date_S = DateTime.Today.ToShortDateString(); // (검색) 종료일
            model.prod_return_S = "%"; // (검색) 출하승인상태

            DataTable dt = itemProductionService.Select(model);

            ViewBag.ItemProductionData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ItemProductionAuth = Json(HttpContext.Session["ItemProductionAuth"]);

            /* 드랍 박스 */
            DataTable search_prod_return = selectBoxService.GetSelectBox("공통코드", "S", "GD103", "search_prod_return");
            DataTable form_prod_return = selectBoxService.GetSelectBox("공통코드", "D", "GD100", "search_prod_return");

            /* 팝업 셋팅 */
            DataTable s_item_Popup = itemProductionService.s_item_Popup("CODEHELP"); // 완제품 품목 팝업
            DataTable s2_item_Popup = itemProductionService.s2_item_Popup("CODEHELP"); // 완제품 품목 팝업
            DataTable s_vender_Popup = itemProductionService.s_vender_Popup("CODEHELP"); // 거래처 팝업

            ViewBag.IP_itemCD = Json(Public_Function.DataTableToJSON(s_item_Popup));
            ViewBag.IP2_itemCD = Json(Public_Function.DataTableToJSON(s2_item_Popup));
            ViewBag.IP_venderCD = Json(Public_Function.DataTableToJSON(s_vender_Popup));
            ViewBag.search_prod_return = search_prod_return;
            ViewBag.form_prod_return = form_prod_return;

            return View();
        }

        public JsonResult ItemProduction_DownGrid(ItemProduction model)
        {
            DataTable dt;

            dt = itemProductionService.SelectBox(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        public JsonResult ItemProduction_Select(ItemProduction model)
        {
            DataTable dt;

            if (model.prod_return_S == null || model.prod_return_S == "")
                model.prod_return_S = "%";

            dt = itemProductionService.Select(model);
            return Json(Public_Function.DataTableToJSON(dt));
        }

        public ActionResult Lot_Popup(ItemProduction model)
        {
            model.start_date_S = DateTime.Today.AddDays(-120).ToShortDateString(); // (검색) 시작일
            model.end_date_S = DateTime.Today.ToShortDateString(); // (검색) 종료일

            DataTable Lot_Popup;

            Lot_Popup = itemProductionService.Lot_Popup(model);

            ViewBag.Lot_ItemCd = model.item_cd;

            return View("Lot_Popup", Json(Public_Function.DataTableToJSON(Lot_Popup)));
        }

        public JsonResult Lot_Popup_Search(ItemProduction model)
        {
            DataTable dt;

            dt = itemProductionService.Lot_Popup(model);
            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 입력, 수정, 삭제 기능
        /// </summary>
        [CheckSession]
        [HttpPost]
        public JsonResult ItemProduction_CRUD(ItemProduction model)
        {
            if (!model.gubun.Equals("Delete"))
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Ok = false });
                }
            }

            string res = itemProductionService.CRUD(model);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);

        }

        /// <summary>
        /// 시험의뢰
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult TestReg(ItemProduction model)
        {
            string res = itemProductionService.TestReg(model);

            return Json("{ \"message\": \"" + res + "\" }"); ;
        }

        #endregion

        #region PackingResultInq2 - 완제품 출하승인 조회

        /// <summary>
        /// 완제품 출하승인 조회
        /// (첫 페이지 진입) 완제품 출하승인 조회
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("PrdIn/PackingResultInq2")]
        [HttpGet]
        public ActionResult PackingResultInq2(PackingResultInq2 model)
        {
            /* 조회 */
            model.s_issue_status = "%"; // (검색) 출하승인상태
            model.start_date_S = DateTime.Today.AddDays(-30).ToShortDateString(); // (검색) 시작일
            model.end_date_S = DateTime.Today.ToShortDateString(); // (검색) 종료일

            DataTable dt = PackingResultInq2Service.Select(model);

            ViewBag.PackingResultInq2Data = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.PackingResultInq2Auth = Json(HttpContext.Session["PackingResultInq2Auth"]);

            /* 드랍 박스 */
            DataTable search_issueStatus = selectBoxService.GetSelectBox("공통코드", "S", "SR001", "search_issueStatus");

            /* 팝업 셋팅 */
            DataTable s_item_Popup = PackingResultInq2Service.s_item_Popup("CODEHELP"); // 완제품 품목 팝업

            ViewBag.PRI_itemCD = Json(Public_Function.DataTableToJSON(s_item_Popup));
            ViewBag.search_issueStatus = search_issueStatus;

            return View();
        }

        public JsonResult PackingResultInq2_Select(PackingResultInq2 model)
        {
            DataTable dt;

            if (model.s_issue_status == null || model.s_issue_status == "")
                model.s_issue_status = "%";

            dt = PackingResultInq2Service.Select(model);
            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion

        #region ProductTransfer - 완제품 인수인계

        /// <summary>
        /// 완제품 인수인계
        /// (첫 페이지 진입) 완제품 인수인계 조회
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("PrdIn/ProductTransfer")]
        [HttpGet]
        public ActionResult ProductTransfer(ProductTransfer model)
        {
            /* 조회 */            
            model.start_date_S = DateTime.Today.AddDays(-7).ToShortDateString(); // (검색) 시작일
            model.end_date_S = DateTime.Today.ToShortDateString(); // (검색) 종료일
            model.item_cd_S = "";
            model.s_issue_status = "%"; // (검색) 출하승인상태
            model.s_receipt_status = "%"; // (검색) 입고상태
            model.prod_return_status = "%"; // (검색) 입고구분
            model.search_ck = "0"; // (검색) 날짜 검색조건

            DataTable dt = productTransferService.Select(model);

            ViewBag.ProductTransferData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ProductTransferAuth = Json(HttpContext.Session["ProductTransferAuth"]);

            /* 드랍 박스 */
            DataTable prod_return_status = selectBoxService.GetSelectBox("공통코드", "S", "GD103", "prod_return_status"); // 입고구분
            DataTable s_receipt_status = selectBoxService.GetSelectBox("공통코드", "S", "TF001", "s_receipt_status"); // 입고상태
            DataTable s_issue_status = selectBoxService.GetSelectBox("공통코드", "S", "SR001", "s_issue_status"); // 출하승인상태

            /* 팝업 셋팅 */
            DataTable s_item_Popup = PackingResultInq2Service.s_item_Popup("CODEHELP"); // 완제품 품목 팝업

            ViewBag.PT_itemCD = Json(Public_Function.DataTableToJSON(s_item_Popup));
            ViewBag.search_prod_return_status = prod_return_status;
            ViewBag.search_receipt_status = s_receipt_status;
            ViewBag.search_issue_status = s_issue_status;

            return View();
        }

        public JsonResult ProductTransfer_DownGrid(ProductTransfer model)
        {
            DataTable dt;

            dt = productTransferService.SelectBox(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        public JsonResult ProductTransfer_Select(ProductTransfer model)
        {
            DataTable dt;

            // (검색) 출하승인상태
            if (model.s_issue_status == null || model.s_issue_status == "")
                model.s_issue_status = "%";
            // (검색) 입고상태
            if (model.s_receipt_status == null || model.s_receipt_status == "")
                model.s_receipt_status = "%";
            // (검색) 입고구분
            if (model.prod_return_status == null || model.prod_return_status == "")
                model.prod_return_status = "%";

            dt = productTransferService.Select(model);
            return Json(Public_Function.DataTableToJSON(dt));
        }

        public JsonResult Trans_Stock(ProductTransfer model, string[] selectedKeys)
        {
            string[] result = selectedKeys[0].Split(',');

            string res = productTransferService.Trans_Stock(model, result);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        #endregion

        #region ItemOrderManage - 완제품 주문 관리

        /// <summary>
        /// 완제품 주문관리
        /// (첫 페이지 진입) 완제품 주문관리 조회
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("PrdIn/ItemOrderManage")]
        [HttpGet]
        public ActionResult ItemOrderManage(ItemOrderManage model)
        {
            model.start_date_S = DateTime.Today.AddDays(-30).ToShortDateString(); // (검색) 시작일
            model.end_date_S = DateTime.Today.ToShortDateString(); // (검색) 종료일
            model.item_cd_S = "%"; // (검색) 아이템코드
            model.order_state_S = "%"; // (검색) 주문상태
            model.search_date_S = "0"; // (검색) 0:주문일자/1:요구납기일자

            DataTable s_item_Popup = itemOrderManageService.Searchitem_Popup("CODEHELP"); // 완제품 품목 팝업
            ViewBag.itemCD = Json(Public_Function.DataTableToJSON(s_item_Popup));

            DataTable s_vender_Popup = itemOrderManageService.Vender_Popup("CODEHELP"); // 거래처 팝업
            ViewBag.venderP = Json(Public_Function.DataTableToJSON(s_vender_Popup));

            DataTable s_emp_Popup = itemOrderManageService.Emp_Popup("CODEHELP"); // 영업사원 팝업
            ViewBag.empP = Json(Public_Function.DataTableToJSON(s_emp_Popup));


            DataTable order_status = selectBoxService.GetSelectBox("공통코드", "S", "GD403", "order_status"); // 완제품 주문상태
            ViewBag.search_order_status = order_status;

            DataTable dt = itemOrderManageService.ItemOrderManage_Select(model);
            ViewBag.itemOrderData = Json(Public_Function.DataTableToJSON(dt));

            return View();
        }

        /// <summary>
        /// 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ItemOrderManageSelect(ItemOrderManage model)
        {
            DataTable dt = itemOrderManageService.ItemOrderManage_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 상세조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ItemOrderManageSelectDetail(ItemOrderManage model)
        {
            DataTable dt = itemOrderManageService.ItemOrderManage_SelectDetail(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 입력, 수정, 삭제 기능
        /// </summary>
        [CheckSession]
        [HttpPost]
        public string ItemOrderManage_CRUD(ItemOrderManage model)
        {
            if (!model.gubun.Equals("D") && !model.gubun.Equals("D2"))
            {
                if (!ModelState.IsValid)
                {
                    return "";
                }
            }

            string res = itemOrderManageService.ItemOrderManage_CRUD(model, model.gubun);


            return res;

        }

        /// <summary>
        /// 최근 단가, master단위 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ItemOrderManage_PRICE(ItemOrderManage model)
        {
            List<string> dt = itemOrderManageService.S_PRICE(model);
            return Json(dt.ToArray());
        }

        #endregion
    }
}