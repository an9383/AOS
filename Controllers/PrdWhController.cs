using HACCP.Services.PrdWh;
using HACCP.Services.Comm;
using log4net;
using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Web.Mvc;
using HACCP.Models.PrdWh;
using System.Data;
using HACCP.Libs;
using HACCP.Filter;
using HACCP.Attribute;

namespace HACCP.Controllers
{
    public class PrdWhController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RowMatWhController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private ItemStockStatusService itemStockStatusService = new ItemStockStatusService();
        private ItemInOutService itemInOutService = new ItemInOutService();
        private ItemInOutStatusService itemInOutStatusService = new ItemInOutStatusService();   //완제품 팔레트 수불이력
        private ItemUseList2_ItemService itemUseList2_ItemService = new ItemUseList2_ItemService(); //완제품사용내역(수불장)
        private ItemMonthlyStockService itemMonthlyStockService = new ItemMonthlyStockService();    //완제품 월간 수불
        private ItemInManageService itemInManageService = new ItemInManageService(); //완제품 기타 입고 등록
        private ItemOutManageService itemOutManageService = new ItemOutManageService(); //완제품 기타 출고 등록

        #region ItemOutManage - 완제품 재고 현황

        /// <summary>
        /// 원료 재고 현황
        /// 1. (첫 페이지 진입) 원료 품목별 재고량
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("PrdWh/ItemStockStatus")]
        [HttpGet]
        public ActionResult ItemStockStatus(ItemStockStatus model)
        {
            // 1. 완제품 재고량 (첫 진입)
            model.item_cd_S = "";
            model.use_ck = "Y";
            DataTable dt = itemStockStatusService.Select_Tab1(model);

            ViewBag.ItemStockStatusData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ItemStockStatusAuth = Json(HttpContext.Session["ItemStockStatusAuth"]);

            /* 팝업 셋팅 */
            DataTable s_item_Popup = itemStockStatusService.s_item_Popup("CODEHELP"); // 완제품 품목 팝업

            ViewBag.ISS_itemCD = Json(Public_Function.DataTableToJSON(s_item_Popup));

            return View();
        }

        /// <summary>
        /// 완제품 재고 현황
        /// 선택한 Tab 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ItemStockStatus_TabSelect(ItemStockStatus model, string tab_num)
        {
            DataTable dt;

            // 1. 완제품 재고량
            if (tab_num == "1")
            {
                dt = itemStockStatusService.Select_Tab1(model);
                return Json(Public_Function.DataTableToJSON(dt));
            }
            // 2. 제조번호 별 재고량
            else if (tab_num == "2")
            {
                dt = itemStockStatusService.Select_Tab2(model);
                return Json(Public_Function.DataTableToJSON(dt));
            }
            // 3. 팔레트 별 재고량
            else
            {
                dt = itemStockStatusService.Select_Tab3(model);
                return Json(Public_Function.DataTableToJSON(dt));
            }

        }

        public JsonResult Tab3_Grid2(ItemStockStatus model)
        {
            DataTable dt;

            dt = itemStockStatusService.Tab3_Grid2(model);
            return Json(Public_Function.DataTableToJSON(dt));

        }

        public JsonResult Tab3_Grid3(ItemStockStatus model)
        {
            DataTable dt;

            dt = itemStockStatusService.Tab3_Grid3(model);
            return Json(Public_Function.DataTableToJSON(dt));

        }

        #endregion

        #region ItemOutManage - 완제품 재고 현황

        /// <summary>
        /// 완제품 수불 이력
        /// 1. (첫 페이지 진입) 완제품 수불 이력
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("PrdWh/ItemInOut")]
        [HttpGet]
        public ActionResult ItemInOut(ItemInOut model)
        {
            model.item_cd_S = "";
            model.use_ck = "Y";

            /* 왼쪽 메인 그리드 */
            DataTable dt = itemInOutService.Select_S1(model);

            ViewBag.ItemInOutData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ItemInOutAuth = Json(HttpContext.Session["ItemInOutAuth"]);

            /* 팝업 셋팅 */
            DataTable s_item_Popup = itemInOutService.s_item_Popup("CODEHELP"); // 완제품 품목 팝업

            ViewBag.IIO_itemCD = Json(Public_Function.DataTableToJSON(s_item_Popup));

            return View();
        }

        public JsonResult Select_S1(ItemInOut model)
        {
            DataTable dt;

            dt = itemInOutService.Select_S1(model);
            return Json(Public_Function.DataTableToJSON(dt));
        }

        public JsonResult Select_S2(ItemInOut model)
        {
            DataTable dt;

            dt = itemInOutService.Select_S2(model);
            return Json(Public_Function.DataTableToJSON(dt));
        }

        public JsonResult Select_S3(ItemInOut model)
        {
            DataTable dt;

            dt = itemInOutService.Select_S3(model);
            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion

        #region ItemInOutStatus - 완제품 팔레트 수불이력
        [Route("PrdWh/ItemInOutStatus")]
        [HttpGet]
        public ActionResult ItemInOutStatus(ItemInOutStatus iModel)
        {
            iModel.use_ck = "Y";
            iModel.start_date = DateTime.Today.AddMonths(-1).ToShortDateString();
            iModel.end_date = DateTime.Today.ToShortDateString();

            DataTable ItemInOutStatus = itemInOutStatusService.ItemInOutStatus_Select(iModel);

            ViewBag.ItemInOutStatusData = Json(Public_Function.DataTableToJSON(ItemInOutStatus));
            ViewBag.ItemInOutStatusAuth = Json(HttpContext.Session["ItemInOutStatusAuth"]);

            return View();
        }

        [HttpPost]
        public JsonResult ItemInOutStatus_Select(ItemInOutStatus iModel)
        {
            DataTable ItemInOutStatusMain = itemInOutStatusService.ItemInOutStatus_Select(iModel);

            return Json(Public_Function.DataTableToJSON(ItemInOutStatusMain));
        }

        [HttpPost]
        public JsonResult ItemInOutStatus_Select2(string box_barcode_no)
        {
            DataTable ItemInOutStatusDetail = itemInOutStatusService.ItemInOutStatus_Select2(box_barcode_no);

            return Json(Public_Function.DataTableToJSON(ItemInOutStatusDetail));
        }

        #endregion

        #region ItemUseList2_Item - 완제품 사용 내역(수불장)
        [Route("PrdWh/ItemUseList2_Item")]
        [HttpGet]
        public ActionResult ItemUseList2_Item(ItemUseList2_Item iModel, string start_date, string end_date, string item_cd, string item_nm)
        {
            iModel.in_type = "%";
            iModel.out_type = "%";
            DataTable ItemUseList2_Item = itemUseList2_ItemService.ItemUseList2_Item_S(iModel);
            DataTable ItemUseList2_ItemPopup = itemUseList2_ItemService.ItemUseList2_ItemPopup();

            DataTable in_type = selectBoxService.GetSelectBox("공통코드", "S", "WR007", "in_type");
            DataTable out_type = selectBoxService.GetSelectBox("공통코드", "S", "WR008", "out_type");

            if (start_date != null && !start_date.Equals(""))  //시작일자
            {
                ViewBag.start_date = iModel.start_date;
            }
            else
            {
                ViewBag.start_date = DateTime.Today.AddYears(-1).ToShortDateString();
            }
            if (end_date != null && !end_date.Equals(""))     //종료일자
            {
                ViewBag.end_date = iModel.end_date;
            }
            else
            {
                ViewBag.end_date = DateTime.Today.ToShortDateString();
            }

            if (item_cd != null && !item_cd.Equals(""))     //품목코드
            {
                ViewBag.item_cd = item_cd;
            }
            else
            {
                ViewBag.item_cd = "";
            }
            if (item_nm != null && !item_nm.Equals(""))         //품목명
            {
                ViewBag.item_nm = item_nm;
            }
            else
            {
                ViewBag.item_nm = "";
            }

            ViewBag.in_type = in_type;
            ViewBag.out_type = out_type;
            ViewBag.ItemUseList2_ItemData = Json(Public_Function.DataTableToJSON(ItemUseList2_Item));
            ViewBag.ItemUseList2_ItemPopup = Json(Public_Function.DataTableToJSON(ItemUseList2_ItemPopup));
            ViewBag.ItemUseList2_ItemAuth = Json(HttpContext.Session["ItemUseList2_ItemAuth"]);

            return View();
        }

        [HttpPost]
        public JsonResult ItemUseList2_Item_S(ItemUseList2_Item iModel)
        {
            DataTable ItemUseList2_ItemMain = itemUseList2_ItemService.ItemUseList2_Item_S(iModel);

            return Json(Public_Function.DataTableToJSON(ItemUseList2_ItemMain));
        }

        #endregion

        #region ItemMonthlyStock - 완제품 월간 수불
        [Route("PrdWh/ItemMonthlyStock")]
        [HttpGet]
        public ActionResult ItemMonthlyStock(ItemMonthlyStock iModel)
        {
            iModel.use_ck_S = "Y";
            iModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            iModel.end_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).AddMonths(1).AddDays(-1).ToShortDateString();

            DataTable ItemMonthlyStock = itemMonthlyStockService.ItemMonthlyStock_S(iModel);

            ViewBag.ItemMonthlyStockData = Json(Public_Function.DataTableToJSON(ItemMonthlyStock));
            ViewBag.ItemMonthlyStockAuth = Json(HttpContext.Session["ItemMonthlyStockAuth"]);

            return View();
        }

        [HttpPost]
        public JsonResult ItemMonthlyStock_S(ItemMonthlyStock iModel)
        {
            DataTable ItemMonthlyStockMain = itemMonthlyStockService.ItemMonthlyStock_S(iModel);

            return Json(Public_Function.DataTableToJSON(ItemMonthlyStockMain));
        }
        #endregion

        #region ItemInManage - 완제품 기타 입고 등록
        [CheckSession]
        [Route("PrdWh/ItemInManage")]
        [HttpGet]
        public ActionResult ItemInManage(ItemInManage iModel)
        {
            iModel.in_type = "%";
            iModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            iModel.end_date = DateTime.Today.ToShortDateString();

            DataTable ItemInManage = itemInManageService.ItemInManage_Select(iModel);
            DataTable in_type = selectBoxService.GetSelectBox("공통코드", "S", "GD201", "in_type");

            ViewBag.in_type = in_type;
            ViewBag.ItemInManageData = Json(Public_Function.DataTableToJSON(ItemInManage));
            ViewBag.ItemInManageAuth = Json(HttpContext.Session["ItemInManageAuth"]);


            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ItemInManage_Select(ItemInManage iModel)
        {
            DataTable ItemInManageMain = itemInManageService.ItemInManage_Select(iModel);

            return Json(Public_Function.DataTableToJSON(ItemInManageMain));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ItemInManage_Select2(string box_barcode_no)
        {
            DataTable ItemInManageDetail = itemInManageService.ItemInManage_Select2(box_barcode_no);

            return Json(Public_Function.DataTableToJSON(ItemInManageDetail));
        }

        //[CheckSession]
        //[HttpPost]
        //public JsonResult ItemInManageCRUD(ItemInManage iModel, string gubun)
        //{
        //    if (!gubun.Equals("Delete"))
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { Ok = false });
        //        }
        //    }

        //    string res = itemInManageService.ItemInManageCRUD(iModel, gubun);

        //    var resJson = "{ \"messege\": \"" + res + "\" }";

        //    return Json(resJson);
        //}

        [CheckSession]
        [HttpPost]
        public JsonResult ItemInManageCRUD([JsonBinder] List<ItemInManage> mModel, string gubun)
        {
            for (int i = 0; i < mModel.Count; i++)
            {
                if (mModel[i].gubun.Equals("Insert"))
                {
                    mModel[i].gubun = "Insert";
                }
                else if (mModel[i].gubun.Equals("Update"))
                {
                    mModel[i].gubun = "Update";
                }
                else if (mModel[i].gubun.Equals("Delete"))
                {
                    mModel[i].gubun = "Delete";
                }
            }
            string res = itemInManageService.ItemInManageCRUD(mModel, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ItemInManage_ItemPopup(string start_date, string end_date, string search)
        {
            DataTable ItemInManage_ItemPopup = itemInManageService.ItemInManage_ItemPopup("ReceiptItem", start_date, end_date, search);

            string jsonData = Public_Function.DataTableToJSON(ItemInManage_ItemPopup);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);


            return Json(jsonList.ToArray());
        }

        #endregion

        #region ItemOutManage - 완제품 기타 출고 등록
        [Route("PrdWh/ItemOutManage")]
        [CheckSession]
        [HttpGet]
        public ActionResult ItemOutManage(ItemOutManage iModel)
        {
            iModel.out_type = "%";
            iModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            iModel.end_date = DateTime.Today.ToShortDateString();

            DataTable ItemOutManage = itemOutManageService.ItemOutManage_Select(iModel);
            DataTable out_type = selectBoxService.GetSelectBox("공통코드", "S", "GD301", "out_type");
            DataTable repositoryType = selectBoxService.GetSelectBox("공통코드", "D", "GD301", "repositoryType");

            ViewBag.out_type = out_type;
            ViewBag.repositoryType = repositoryType;
            ViewBag.ItemOutManageData = Json(Public_Function.DataTableToJSON(ItemOutManage));
            ViewBag.ItemOutManageAuth = Json(HttpContext.Session["ItemOutManageAuth"]);


            return View();
         }

        [CheckSession]
        [HttpPost]
        public JsonResult ItemOutManage_Select(ItemOutManage iModel)
        {
            DataTable ItemOutManageMain = itemOutManageService.ItemOutManage_Select(iModel);

            return Json(Public_Function.DataTableToJSON(ItemOutManageMain));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ItemOutManage_Select2(string box_barcode_no)
        {
            DataTable ItemOutManageDetail = itemOutManageService.ItemOutManage_Select2(box_barcode_no);

            return Json(Public_Function.DataTableToJSON(ItemOutManageDetail));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ItemOutManageCRUD([JsonBinder] List<ItemOutManage> mModel, string gubun)
        {
            for (int i = 0; i < mModel.Count; i++)
            {
                if (mModel[i].gubun.Equals("Insert"))
                {
                    mModel[i].gubun = "Insert";
                }
                else if (mModel[i].gubun.Equals("Update"))
                {
                    mModel[i].gubun = "Update";
                }
                else if (mModel[i].gubun.Equals("Delete"))
                {
                    mModel[i].gubun = "Delete";
                }
            }
            string res = itemOutManageService.ItemOutManageCRUD(mModel, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        //[CheckSession]
        //[HttpPost]
        //public JsonResult ItemOutManageCRUD(ItemOutManage iModel, string gubun)
        //{
        //    if (!gubun.Equals("Delete"))
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { Ok = false });
        //        }
        //    }

        //    string res = itemOutManageService.ItemOutManageCRUD(iModel, gubun);

        //    var resJson = "{ \"messege\": \"" + res + "\" }";

        //    return Json(resJson);
        //}

        [CheckSession]
        [HttpPost]
        public JsonResult ItemOutManage_ItemPopup(string start_date, string end_date, string search)
        {
             DataTable ItemOutManage_ItemPopup = itemOutManageService.ItemOutManage_ItemPopup("ReceiptItem", start_date, end_date, search);

            string jsonData = Public_Function.DataTableToJSON(ItemOutManage_ItemPopup);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);


            return Json(jsonList.ToArray());
        }
    }
    #endregion

 
}