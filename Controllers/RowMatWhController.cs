using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using HACCP.Attribute;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models.RowMatWh;
using HACCP.Services.Comm;
using HACCP.Services.RowMatWh;
using log4net;

namespace HACCP.Controllers
{
    public class RowMatWhController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RowMatWhController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private StockStatus2Service stockStatus2Service = new StockStatus2Service();
        private MaterialReserveQtyListService materialReserveQtyListService = new MaterialReserveQtyListService();
        private InOutService inOutService = new InOutService();
        private InOutStatusService inOutStatusService = new InOutStatusService();
        private ItemUseList2Service itemUseList2Service = new ItemUseList2Service();
        private MaterialMonthlyStockService materialMonthlyStockService = new MaterialMonthlyStockService();
        private Material_Used_Results2Service material_Used_Results2Service = new Material_Used_Results2Service();
        private MaterialInManageService materialInManageService = new MaterialInManageService();
        private MaterialOutManageService materialOutManageService = new MaterialOutManageService();
        private PickingManageService pickingManageService = new PickingManageService();
        private PickingOrderService pickingOrderService = new PickingOrderService();
        private ValidDate_ListService validDate_ListService = new ValidDate_ListService();

        #region StockStatus2 - 원료/자재 재고 현황

        /// <summary>
        /// 원료 재고 현황
        /// 1. (첫 페이지 진입) 원료 품목별 재고량
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("RowMatWh/StockStatus2_M")]
        [HttpGet]
        public ActionResult StockStatus2_M(StockStatus2 sModel)
        {

            // 1. 원료 품목별 재고량 (첫 진입)
            sModel.check = "Y";
            sModel.StockStatus2_status = "3"; // 원료 3, 자재 2
            DataTable dt = stockStatus2Service.Select_MP1(sModel);

            ViewBag.StockStatus2_M1Data = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.StockStatus2_MAuth = Json(HttpContext.Session["StockStatus2_MAuth"]);
            DataTable obtain_status_S = selectBoxService.GetSelectBox("공통코드", "S", "WR200", "obtain_status");

            /* 팝업 셋팅 */
            DataTable itemCD = stockStatus2Service.ItemCDPopup("CODEHELP"); // 원료 품목 팝업

            ViewBag.obtain_status_S = obtain_status_S;
            ViewBag.SSM_itemCD = Json(Public_Function.DataTableToJSON(itemCD));

            return View();
        }

        /// <summary>
        /// 원료/자재 재고 현황
        /// 선택한 Tab 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult StockStatus2_MP_TabSelect(StockStatus2 sModel, string tab_num)
        {
            DataTable dt;

            // 1. 원료/자재 품목별 재고량
            if (tab_num == "1")
            {
                dt = stockStatus2Service.Select_MP1(sModel);
                return Json(Public_Function.DataTableToJSON(dt));
            }
            // 2. 원료/자재 시험번호별 재고량
            else if (tab_num == "2")
            {
                dt = stockStatus2Service.Select_MP2(sModel);
                return Json(Public_Function.DataTableToJSON(dt));
            }
            // 3. 원료/자재 팩별 재고량
            else
            {
                dt = stockStatus2Service.Select_MP3(sModel);
                return Json(Public_Function.DataTableToJSON(dt));
            }
            
        }

        /// <summary>
        /// 자재 재고 현황
        /// 1. (첫 페이지 진입) 자재 품목별 재고량
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("RowMatWh/StockStatus2_P")]
        [HttpGet]
        public ActionResult StockStatus2_P(StockStatus2 sModel)
        {
            // 1. 자재 품목별 재고량 (첫 진입)
            sModel.check = "Y";               // 사용여부
            sModel.StockStatus2_status = "2"; // 원료 3, 자재 2
            DataTable dt = stockStatus2Service.Select_MP1(sModel);

            ViewBag.StockStatus2_P1Data = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.StockStatus2_PAuth = Json(HttpContext.Session["StockStatus2_PAuth"]);
            DataTable obtain_status_S = selectBoxService.GetSelectBox("공통코드", "S", "WR200", "obtain_status");

            /* 팝업 셋팅 */
            DataTable SSP_ItemCD = stockStatus2Service.ItemPCDPopup("CODEHELP"); // 원료 품목 팝업

            ViewBag.obtain_status_S = obtain_status_S;
            ViewBag.SSP_ItemCD = Json(Public_Function.DataTableToJSON(SSP_ItemCD));

            return View();
        }

        #endregion

        #region InOut - 원료/자재 수불 이력
        [CheckSession]
        [Route("RowMatWh/InOut_P")]
        [HttpGet]
        public ActionResult InOut_P(InOut iModel)
        {
            iModel.s_gubun = "2";   // 원료 3, 자재 2    
            iModel.use_ck = "Y";
            iModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            iModel.end_date = DateTime.Today.ToShortDateString();
            DataTable InOut_P = inOutService.InOutSelect_S(iModel);

            DataTable receipt_status = selectBoxService.GetSelectBox("공통코드", "D", "WR002", "receipt_status");

            ViewBag.InOut_PData = Json(Public_Function.DataTableToJSON(InOut_P));
            ViewBag.InOut_PAuth = Json(HttpContext.Session["InOut_PAuth"]);

            ViewBag.receipt_status = receipt_status;
            return View();
        }

        [CheckSession]
        [Route("RowMatWh/InOut_M")]
        [HttpGet]
        public ActionResult InOut_M(InOut iModel)
        {
            iModel.s_gubun = "3";   // 원료 3, 자재 2    
            iModel.use_ck = "Y";
            iModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            iModel.end_date = DateTime.Today.ToShortDateString();
            DataTable InOut_M = inOutService.InOutSelect_S(iModel);

            DataTable receipt_status = selectBoxService.GetSelectBox("공통코드", "D", "WR002", "receipt_status");

            ViewBag.InOut_MData = Json(Public_Function.DataTableToJSON(InOut_M));
            ViewBag.InOut_MAuth = Json(HttpContext.Session["InOut_MAuth"]);

            ViewBag.receipt_status = receipt_status;
            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult InOutSelect_S(InOut iModel)
        {
            DataTable InOutMain = inOutService.InOutSelect_S(iModel);

            return Json(Public_Function.DataTableToJSON(InOutMain));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult InOutSelect_S2(InOut iModel, string item_cd, string s_test_no)
        {
            DataTable InOutUpDetail = inOutService.InOutSelect_S2(iModel, item_cd, s_test_no);

            return Json(Public_Function.DataTableToJSON(InOutUpDetail));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult InOutSelect_S3(InOut iModel, string test_no, string item_cd)
        {
            DataTable InOutDownDetail = inOutService.InOutSelect_S3(iModel, test_no, item_cd);

            return Json(Public_Function.DataTableToJSON(InOutDownDetail));
        }

        #endregion

        #region InOutStatus - 원료/자재 팩 수불 현황
        [CheckSession]
        [Route("RowMatWh/InOutStatus_M")]
        [HttpGet]
        public ActionResult InOutStatus_M(InOutStatus oModel)
        {
            oModel.s_gubun = "3";   // 원료 3, 자재 2    
            oModel.use_ck = "Y";
            oModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            oModel.end_date = DateTime.Today.ToShortDateString();
            DataTable InOutStatus_M = inOutStatusService.InOutStatusSelect(oModel);

            ViewBag.InOutStatus_MData = Json(Public_Function.DataTableToJSON(InOutStatus_M));
            ViewBag.InOutStatus_MAuth = Json(HttpContext.Session["InOutStatus_MAuth"]);

            return View();
        }

        [CheckSession]
        [Route("RowMatWh/InOutStatus_P")]
        [HttpGet]
        public ActionResult InOutStatus_P(InOutStatus oModel)
        {            
            oModel.s_gubun = "2";   // 원료 3, 자재 2    
            oModel.use_ck = "Y";
            oModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            oModel.end_date = DateTime.Today.ToShortDateString();
            DataTable InOutStatus_P = inOutStatusService.InOutStatusSelect(oModel);

            ViewBag.InOutStatus_PData = Json(Public_Function.DataTableToJSON(InOutStatus_P));
            ViewBag.InOutStatus_PAuth = Json(HttpContext.Session["InOutStatus_PAuth"]);

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult InOutStatusSelect(InOutStatus oModel)
        {
            DataTable InOutStatusMain = inOutStatusService.InOutStatusSelect(oModel);

            return Json(Public_Function.DataTableToJSON(InOutStatusMain));
        }

        public JsonResult InOutStatusSelect_S2(InOutStatus oModel, string receipt_no, string receipt_id, string receipt_pack_seq)
        {
            DataTable InOutStatus_MDetail = inOutStatusService.InOutStatusSelect_S2(oModel, receipt_no, receipt_id, receipt_pack_seq);

            return Json(Public_Function.DataTableToJSON(InOutStatus_MDetail));
        }


        #endregion

        #region ItemUseList2 - 원료/자재/제조 사용 내역
        //원료 사용 내역
        [CheckSession]
        [Route("RowMatWh/ItemUseList2_S")]
        [HttpGet]
        public ActionResult ItemUseList2_S(ItemUseList2 iModel, string start_date, string end_date, string item_cd, string item_nm)
        {
            iModel.s_gubun = "3";   // 원료 3, 자재 2
            iModel.out_type = "%";
            iModel.in_type = "%";

            DataTable ItemUseList2_S = itemUseList2Service.ItemUseList2_Select(iModel);            
            DataTable ItemUseList2Popup_S = itemUseList2Service.ItemUseList2Popup();
            
            if(start_date != null && !start_date.Equals(""))  //시작일자
            {
                ViewBag.start_date = iModel.start_date;
            }
            else
            {
                ViewBag.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            }
            if (end_date != null && !end_date.Equals(""))     //종료일자
            {
                ViewBag.end_date = iModel.end_date;
            }
            else
            {
                ViewBag.end_date = DateTime.Today.ToShortDateString();
            }

            if (item_cd !=null && !item_cd.Equals(""))      //품목코드
            {
                ViewBag.item_cd = item_cd;                
            }
            else
            {
                ViewBag.item_cd = "";
            }
            if (item_nm != null && !item_nm.Equals(""))     //품목명
            {
                ViewBag.item_nm = item_nm;
            }
            else
            {
                ViewBag.item_nm = "";
            }
            
            ViewBag.ItemUseList2_S = Json(Public_Function.DataTableToJSON(ItemUseList2_S));
            ViewBag.ItemUseList2Popup_S = Json(Public_Function.DataTableToJSON(ItemUseList2Popup_S));
            ViewBag.ItemUseList2_SAuth = Json(HttpContext.Session["ItemUseList2_SAuth"]);

            return View();
        }
        //원료(제조) 사용 내역
        [CheckSession]
        [Route("RowMatWh/ItemUseList2_M")]
        [HttpGet]
        public ActionResult ItemUseList2_M(ItemUseList2 iModel, string start_date, string end_date, string item_cd, string item_nm)
        {
            iModel.s_gubun = "3";   // 원료 3, 자재 2
            iModel.out_type = "%";
            iModel.in_type = "%";

            DataTable ItemUseList2_M = itemUseList2Service.ItemUseList2_Select(iModel);            
            DataTable ItemUseList2Popup_M = itemUseList2Service.ItemUseList2Popup();

            if (start_date != null && !start_date.Equals(""))  //시작일자
            {
                ViewBag.start_date = iModel.start_date;
            }
            else
            {
                ViewBag.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            }
            if (end_date != null && !end_date.Equals(""))     //종료일자
            {
                ViewBag.end_date = iModel.end_date;
            }
            else
            {
                ViewBag.end_date = DateTime.Today.ToShortDateString();
            }

            if (item_cd != null && !item_cd.Equals(""))      //품목코드
            {
                ViewBag.item_cd = item_cd;
            }
            else
            {
                ViewBag.item_cd = "";
            }
            if (item_nm != null && !item_nm.Equals(""))     //품목명
            {
                ViewBag.item_nm = item_nm;
            }
            else
            {
                ViewBag.item_nm = "";
            }               

            ViewBag.ItemUseList2_M = Json(Public_Function.DataTableToJSON(ItemUseList2_M));
            ViewBag.ItemUseList2Popup_M = Json(Public_Function.DataTableToJSON(ItemUseList2Popup_M));
            ViewBag.ItemUseList2_MAuth = Json(HttpContext.Session["ItemUseList2_MAuth"]);

            return View();
        }
        //자재 사용 내역
        [CheckSession]
        [Route("RowMatWh/ItemUseList2_P")]
        [HttpGet]
        public ActionResult ItemUseList2_P(ItemUseList2 iModel, string start_date, string end_date, string item_cd, string item_nm)
        {
            iModel.s_gubun = "2";   // 원료 3, 자재 2
            iModel.out_type = "%";
            iModel.in_type = "%";

            DataTable ItemUseList2_P = itemUseList2Service.ItemUseList2_Select(iModel);            
            DataTable ItemUseList2Popup_P = itemUseList2Service.ItemUseList2Popup_P();

            if (start_date != null && !start_date.Equals(""))  //시작일자
            {
                ViewBag.start_date = iModel.start_date;
            }
            else
            {
                ViewBag.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            }
            if (end_date != null && !end_date.Equals(""))     //종료일자
            {
                ViewBag.end_date = iModel.end_date;
            }
            else
            {
                ViewBag.end_date = DateTime.Today.ToShortDateString();
            }

            if (item_cd != null && !item_cd.Equals(""))      //품목코드
            {
                ViewBag.item_cd = item_cd;
            }
            else
            {
                ViewBag.item_cd = "";
            }
            if (item_nm != null && !item_nm.Equals(""))     //품목명
            {
                ViewBag.item_nm = item_nm;
            }
            else
            {
                ViewBag.item_nm = "";
            }
            
            ViewBag.ItemUseList2_P = Json(Public_Function.DataTableToJSON(ItemUseList2_P));
            ViewBag.ItemUseList2Popup_P = Json(Public_Function.DataTableToJSON(ItemUseList2Popup_P));
            ViewBag.ItemUseList2_PAuth = Json(HttpContext.Session["ItemUseList2_PAuth"]);

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ItemUseList2_Select(ItemUseList2 iModel)
        {
            DataTable ItemUseList2Main = itemUseList2Service.ItemUseList2_Select(iModel);

            return Json(Public_Function.DataTableToJSON(ItemUseList2Main));
        }

        #endregion

        #region MaterialMonthlyStock - 원료/자재/제조 월간 수불
        //원료 월간 수불
        [CheckSession]
        [Route("RowMatWh/MaterialMonthlyStock_S")]
        [HttpGet]
        public ActionResult MaterialMonthlyStock_S(MaterialMonthlyStock mModel)
        {
            mModel.item_gb = "3";
            mModel.use_ck_S = "Y";
            mModel.obtain_status = "%";
            mModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            mModel.end_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).AddMonths(1).AddDays(-1).ToShortDateString();

            DataTable MaterialMonthlyStock_S = materialMonthlyStockService.MaterialMonthlyStock_S_Standard(mModel);
            DataTable obtain_status = selectBoxService.GetSelectBox("공통코드", "S", "WR200", "obtain_status");

            ViewBag.obtain_status = obtain_status;
            ViewBag.MaterialMonthlyStock_SData = Json(Public_Function.DataTableToJSON(MaterialMonthlyStock_S));
            ViewBag.MaterialMonthlyStock_SAuth = Json(HttpContext.Session["MaterialMonthlyStock_SAuth"]);            

            return View();
        }

        //원료 월간 수불(제조)
        [CheckSession]
        [Route("RowMatWh/MaterialMonthlyStock_M")]
        [HttpGet]
        public ActionResult MaterialMonthlyStock_M(MaterialMonthlyStock mModel)
        {            
            mModel.item_gb = "3";
            mModel.use_ck_S = "Y";
            mModel.obtain_status = "%";
            mModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            mModel.end_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).AddMonths(1).AddDays(-1).ToShortDateString();

            DataTable MaterialMonthlyStock_M = materialMonthlyStockService.MaterialMonthlyStock_Select(mModel);
            DataTable obtain_status = selectBoxService.GetSelectBox("공통코드", "S", "WR200", "obtain_status");

            ViewBag.obtain_status = obtain_status;
            ViewBag.MaterialMonthlyStock_MData = Json(Public_Function.DataTableToJSON(MaterialMonthlyStock_M));
            ViewBag.MaterialMonthlyStock_MAuth = Json(HttpContext.Session["MaterialMonthlyStock_MAuth"]);

            return View();
        }

        //자재 월간 수불
        [CheckSession]
        [Route("RowMatWh/MaterialMonthlyStock_P")]
        [HttpGet]
        public ActionResult MaterialMonthlyStock_P(MaterialMonthlyStock mModel)
        {
            mModel.item_gb = "2";
            mModel.use_ck_S = "Y";
            mModel.obtain_status = "%";
            mModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            mModel.end_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).AddMonths(1).AddDays(-1).ToShortDateString();

            DataTable MaterialMonthlyStock_P = materialMonthlyStockService.MaterialMonthlyStock_Select(mModel);
            DataTable obtain_status = selectBoxService.GetSelectBox("공통코드", "S", "WR200", "obtain_status");

            ViewBag.obtain_status = obtain_status;
            ViewBag.MaterialMonthlyStock_PData = Json(Public_Function.DataTableToJSON(MaterialMonthlyStock_P));
            ViewBag.MaterialMonthlyStock_PAuth = Json(HttpContext.Session["MaterialMonthlyStock_PAuth"]);

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult MaterialMonthlyStock_S_Standard(MaterialMonthlyStock mModel)
        {
            DataTable MaterialMonthlyStockMain = materialMonthlyStockService.MaterialMonthlyStock_S_Standard(mModel);

            return Json(Public_Function.DataTableToJSON(MaterialMonthlyStockMain));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult MaterialMonthlyStock_Select(MaterialMonthlyStock mModel)
        {
            DataTable MaterialMonthlyStockMain = materialMonthlyStockService.MaterialMonthlyStock_Select(mModel);

            return Json(Public_Function.DataTableToJSON(MaterialMonthlyStockMain));
        }
        #endregion

        #region Material_Used_Results2 - 원료 사용 실적
        [CheckSession]
        [Route("RowMatWh/Material_Used_Results2")]
        [HttpGet]
        public ActionResult Material_Used_Results2(Material_Used_Results2 mModel)
        {
            mModel.S_ORDER_START_DATE = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            mModel.S_ORDER_END_DATE = DateTime.Today.ToShortDateString();
            DataTable Material_Used_Results2 = material_Used_Results2Service.Material_Used_Results2_S(mModel);            

            ViewBag.Material_Used_Results2Data = Json(Public_Function.DataTableToJSON(Material_Used_Results2));
            ViewBag.Material_Used_Results2Auth = Json(HttpContext.Session["Material_Used_Results2Auth"]);
            
            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Material_Used_Results2_S(Material_Used_Results2 mModel)
        {
            DataTable Material_Used_Results2Main = material_Used_Results2Service.Material_Used_Results2_S(mModel);

            return Json(Public_Function.DataTableToJSON(Material_Used_Results2Main));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Material_Used_Results2_S2(Material_Used_Results2 mModel, string order_no, string input_order_id, string process_cd)
        {
            DataTable Material_Used_Results2Detail = material_Used_Results2Service.Material_Used_Results2_S2(mModel, order_no, input_order_id, process_cd);

            return Json(Public_Function.DataTableToJSON(Material_Used_Results2Detail));
        }
        #endregion

        #region MaterialInManage - 원료/자재 기타 입고 관리
        [CheckSession]
        [Route("RowMatWh/MaterialInManage_M")]
        [HttpGet]
        public ActionResult MaterialInManage_M(MaterialInManage mModel)
        {
            mModel.s_gubun = "3";   // 원료 3, 자재 2
            mModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            mModel.end_date = DateTime.Today.ToShortDateString();
            DataTable MaterialInManage_M = materialInManageService.MaterialInManage_Select(mModel);
            DataTable in_type = selectBoxService.GetSelectBox("공통코드", "D", "WR007", "in_type");

            ViewBag.in_type = in_type;
            ViewBag.MaterialInManage_MData = Json(Public_Function.DataTableToJSON(MaterialInManage_M));
            ViewBag.MaterialInManage_MAuth = Json(HttpContext.Session["MaterialInManage_MAuth"]);

            return View();
        }

        [CheckSession]
        [Route("RowMatWh/MaterialInManage_P")]
        [HttpGet]
        public ActionResult MaterialInManage_P(MaterialInManage mModel)
        {
            mModel.s_gubun = "2";   // 원료 3, 자재 2
            mModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            mModel.end_date = DateTime.Today.ToShortDateString();
            DataTable MaterialInManage_P = materialInManageService.MaterialInManage_Select(mModel);
            DataTable in_type = selectBoxService.GetSelectBox("공통코드", "D", "WR007", "in_type");

            ViewBag.in_type = in_type;
            ViewBag.MaterialInManage_PData = Json(Public_Function.DataTableToJSON(MaterialInManage_P));
            ViewBag.MaterialInManage_PAuth = Json(HttpContext.Session["MaterialInManage_PAuth"]);

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult MaterialInManage_Select(MaterialInManage mModel)
        {
            DataTable MaterialInManageMain = materialInManageService.MaterialInManage_Select(mModel);

            return Json(Public_Function.DataTableToJSON(MaterialInManageMain));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult MaterialInManage_Select2(string receipt_no, string receipt_id, string receipt_pack_seq)
        {
            DataTable MaterialInManageDetail = materialInManageService.MaterialInManage_Select2(receipt_no, receipt_id, receipt_pack_seq);

            return Json(Public_Function.DataTableToJSON(MaterialInManageDetail));
        }

        [HttpPost]
        public JsonResult MaterialInManage_Select3(string receipt_no, string receipt_id, string receipt_pack_seq)
        {
            DataTable MaterialInManageUpdateTime = materialInManageService.MaterialInManage_Select3(receipt_no, receipt_id, receipt_pack_seq);

            return Json(Public_Function.DataTableToJSON(MaterialInManageUpdateTime));
        }

        [HttpPost]
        public JsonResult MaterialInManageCRUD([JsonBinder]List<MaterialInManage> mModel, string gubun)
        {
            for(int i = 0; i < mModel.Count; i++)
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
            string res = materialInManageService.MaterialInManageCRUD(mModel, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult MaterialInManage_ItemPopup(string start_date, string end_date, string search, string item_gb)
        {
            DataTable MaterialInManage_ItemPopup = materialInManageService.MaterialInManage_ItemPopup(start_date, end_date, search, item_gb);

            string jsonData = Public_Function.DataTableToJSON(MaterialInManage_ItemPopup);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);

            return Json(jsonList.ToArray());
        }
        #endregion

        #region MaterialOutManage - 원료/자재 기타 출고 관리
        [Route("RowMatWh/MaterialOutManage_M")]
        [HttpGet]
        public ActionResult MaterialOutManage_M(MaterialOutManage mModel)
        {
            mModel.s_gubun = "3";   // 원료 3, 자재 2
            mModel.out_type = "%";
            mModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            mModel.end_date = DateTime.Today.ToShortDateString();
            DataTable MaterialOutManage_M = materialOutManageService.MaterialOutManage_Select(mModel);
            DataTable out_type = selectBoxService.GetSelectBox("공통코드", "S", "WR008", "out_type");
            DataTable repositoryType = selectBoxService.GetSelectBox("공통코드", "D", "WR008", "repositoryType");

            ViewBag.out_type = out_type;
            ViewBag.repositoryType = repositoryType;
            ViewBag.MaterialOutManage_MData = Json(Public_Function.DataTableToJSON(MaterialOutManage_M));
            ViewBag.MaterialOutManage_MAuth = Json(HttpContext.Session["MaterialOutManage_MAuth"]);

            return View();
        }

        [Route("RowMatWh/MaterialOutManage_P")]
        [HttpGet]
        public ActionResult MaterialOutManage_P(MaterialOutManage mModel)
        {
            mModel.s_gubun = "2";   // 원료 3, 자재 2
            mModel.out_type = "%";
            mModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            mModel.end_date = DateTime.Today.ToShortDateString();
            DataTable MaterialOutManage_P = materialOutManageService.MaterialOutManage_Select(mModel);
            DataTable out_type = selectBoxService.GetSelectBox("공통코드", "S", "WR008", "out_type");
            DataTable repositoryType = selectBoxService.GetSelectBox("공통코드", "D", "WR008", "repositoryType");

            ViewBag.out_type = out_type;
            ViewBag.repositoryType = repositoryType;
            ViewBag.MaterialOutManage_PData = Json(Public_Function.DataTableToJSON(MaterialOutManage_P));
            ViewBag.MaterialOutManage_PAuth = Json(HttpContext.Session["MaterialOutManage_PAuth"]);

            return View();
        }

        [HttpPost]
        public JsonResult MaterialOutManage_Select(MaterialOutManage mModel)
        {
            DataTable MaterialOutManageMain = materialOutManageService.MaterialOutManage_Select(mModel);

            return Json(Public_Function.DataTableToJSON(MaterialOutManageMain));
        }

        [HttpPost]
        public JsonResult MaterialOutManage_Select2(string receipt_no, string receipt_id, string receipt_pack_seq)
        {
            DataTable MaterialOutManageDetail = materialOutManageService.MaterialOutManage_Select2(receipt_no, receipt_id, receipt_pack_seq);

            return Json(Public_Function.DataTableToJSON(MaterialOutManageDetail));
        }

        [HttpPost]
        public JsonResult MaterialOutManage_Select3(string receipt_no, string receipt_id, string receipt_pack_seq)
        {
            DataTable MaterialOutManageUpdateTime = materialOutManageService.MaterialOutManage_Select3(receipt_no, receipt_id, receipt_pack_seq);

            return Json(Public_Function.DataTableToJSON(MaterialOutManageUpdateTime));
        }

        [HttpPost]
        public JsonResult MaterialOutManageCRUD([JsonBinder] List<MaterialOutManage> mModel, string gubun)
        {
            for( int i = 0; i < mModel.Count; i++)
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
            string res = materialOutManageService.MaterialOutManageCRUD(mModel, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }        

        [HttpPost]
        public JsonResult MaterialOutManage_ItemPopup(string start_date, string end_date, string search, string item_gb)
        {
            DataTable MaterialOutManage_ItemPopup = materialOutManageService.MaterialOutManage_ItemPopup(start_date, end_date, search, item_gb);

            string jsonData = Public_Function.DataTableToJSON(MaterialOutManage_ItemPopup);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);

            return Json(jsonList.ToArray());
        }
        #endregion

        #region PickingOrder - 원료별 PickingOrder 조회
        [Route("RowMatWh/PickingOrder")]
        [HttpGet]
        public ActionResult PickingOrder(PickingOrder pModel)
        {
            DataTable PickingOrder = pickingOrderService.PickingOrder_Select(pModel);

            ViewBag.PickingOrderData = Json(Public_Function.DataTableToJSON(PickingOrder));
            ViewBag.PickingOrderAuth = Json(HttpContext.Session["PickingOrderAuth"]);

            return View();
        }
        #endregion

        #region PickingManage - Picking 관리
        [Route("RowMatWh/PickingManage")]
        [HttpGet]
        public ActionResult PickingManage(PickingManage pModel)
        {
            pModel.s_gubun = "M";
            pModel.start_date = DateTime.Today.AddDays(1 - DateTime.Today.Day).ToShortDateString();
            pModel.end_date = DateTime.Today.ToShortDateString();
            DataTable PickingManage = pickingManageService.PickingManage_Select(pModel);

            ViewBag.PickingManageData = Json(Public_Function.DataTableToJSON(PickingManage));
            ViewBag.PickingManageAuth = Json(HttpContext.Session["PickingManageAuth"]);

            return View();
        }

        [HttpPost]
        public JsonResult PickingManage_Select(PickingManage pModel)
        {
            DataTable PickingManageMain = pickingManageService.PickingManage_Select(pModel);

            return Json(Public_Function.DataTableToJSON(PickingManageMain));
        }

        [HttpPost]
        public JsonResult PickingManage_Select2(string order_no, string input_order_id, string process_cd, string s_gubun)
        {
            DataTable PickingManageRightUp = pickingManageService.PickingManage_Select2(order_no, input_order_id, process_cd, s_gubun);

            return Json(Public_Function.DataTableToJSON(PickingManageRightUp));
        }

        [HttpPost]
        public JsonResult PickingManage_Select3(string order_no, string input_order_id, string process_cd, string qc_no, string s_gubun)
        {
            DataTable PickingManageRightDown = pickingManageService.PickingManage_Select3(order_no, input_order_id, process_cd, qc_no, s_gubun);

            return Json(Public_Function.DataTableToJSON(PickingManageRightDown));
        }

        [HttpPost]
        public JsonResult PickingManage_TRX([JsonBinder] List<PickingManage> pModel, string gubun)
        {
            for(int i = 0; i < pModel.Count; i++)
            {
                if (pModel[i].gubun.Equals("Update"))
                {
                    pModel[i].gubun = "Update";
                }
            }

            string res = pickingManageService.PickingManage_TRX(pModel, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }
        #endregion

        #region ValidDate_List - 원료 유효기간 현황
        [Route("RowMatWh/ValidDate_List")]
        [HttpGet]
        public ActionResult ValidDate_List(ValidDate_List vModel)
        {
            vModel.s_gubun = "%";
            vModel.Valid_date_S = DateTime.Today.ToShortDateString();
            vModel.Valid_date = DateTime.Today.AddMonths(1).ToShortDateString();
            DataTable ValidDate_List = validDate_ListService.ValidDate_List_S(vModel);

            ViewBag.ValidDate_ListData = Json(Public_Function.DataTableToJSON(ValidDate_List));
            ViewBag.ValidDate_ListAuth = Json(HttpContext.Session["ValidDate_ListAuth"]);

            return View();
        }

        [HttpPost]
        public JsonResult ValidDate_List_S(ValidDate_List vModel)
        {
            DataTable ValidDate_ListMain = validDate_ListService.ValidDate_List_S(vModel);

            return Json(Public_Function.DataTableToJSON(ValidDate_ListMain));
        }
       
        [HttpPost]
        public JsonResult ValidDate_List_SV(ValidDate_List vModel)
        {
            DataTable ValidDate_ListSV = validDate_ListService.ValidDate_List_SV(vModel);

            return Json(Public_Function.DataTableToJSON(ValidDate_ListSV));
        }

        [HttpPost]
        public JsonResult ValidDate_List_V_OUT(string receipt_no, string receipt_id)
        {
            DataTable ValidDate_List_V_OUT = validDate_ListService.ValidDate_List_V_OUT(receipt_no, receipt_id);

            return Json(Public_Function.DataTableToJSON(ValidDate_List_V_OUT));
        }

        [HttpPost]
        public JsonResult ValidDate_List_CK(string receipt_no, string receipt_id)
        {
            DataTable ValidDate_List_CK = validDate_ListService.ValidDate_List_CK(receipt_no, receipt_id);

            return Json(Public_Function.DataTableToJSON(ValidDate_List_CK));
        }

        [HttpPost]
        public JsonResult ValidDate_List_ES(string receipt_no, string receipt_id, string emp_cd, string validation_type, string remain_qty, string item_cd)
        {
            DataTable ValidDate_List_ES = validDate_ListService.ValidDate_List_ES(receipt_no, receipt_id, emp_cd, validation_type, remain_qty, item_cd);

            return Json(Public_Function.DataTableToJSON(ValidDate_List_ES));
        }

        #endregion

        #region MaterialReserveQtyList - 원료/자재 예약량 조회
        [CheckSession]
        [Route("RowMatWh/MaterialReserveQtyList_M")]
        [HttpGet]
        public ActionResult MaterialReserveQtyList_M(MaterialReserveQtyList mModel)
        {
            mModel.option = "3";    //원료 : 3, 자재 : 2
            DataTable MaterialReserveQtyList_M = materialReserveQtyListService.MaterialReserveQtyListSelect_S1(mModel);

            ViewBag.MaterialReserveQtyList_MData = Json(Public_Function.DataTableToJSON(MaterialReserveQtyList_M));
            ViewBag.MaterialReserveQtyList_MAuth = Json(HttpContext.Session["MaterialReserveQtyList_MAuth"]);

            return View();
        }

        public JsonResult MaterialReserveQtyListSelect_S2(MaterialReserveQtyList mModel, string item_cd)
        {
            DataTable MaterialReserveQtyList_MDetail = materialReserveQtyListService.MaterialReserveQtyListSelect_S2(mModel, item_cd);

            return Json(Public_Function.DataTableToJSON(MaterialReserveQtyList_MDetail));
        }

        [CheckSession]
        [Route("RowMatWh/MaterialReserveQtyList_P")]
        [HttpGet]
        public ActionResult MaterialReserveQtyList_P(MaterialReserveQtyList mModel)
        {
            mModel.option = "2";    //원료 : 3, 자재 : 2
            DataTable MaterialReserveQtyList_P = materialReserveQtyListService.MaterialReserveQtyListSelect_S1(mModel);

            ViewBag.MaterialReserveQtyList_PData = Json(Public_Function.DataTableToJSON(MaterialReserveQtyList_P));
            ViewBag.MaterialReserveQtyList_PAuth = Json(HttpContext.Session["MaterialReserveQtyList_PAuth"]);

            return View();
        }

        #endregion
    }
}