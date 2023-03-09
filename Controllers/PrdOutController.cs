using HACCP.Attribute;
using HACCP.Libs;
using HACCP.Models.PrdOut;
using HACCP.Services.Comm;
using HACCP.Services.PrdOut;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class PrdOutController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RowMatWhController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        CodeHelpService codeHelpService = new CodeHelpService();
        private DespatchManageOrder2Service despatchManageOrder2Service = new DespatchManageOrder2Service();
        private DespatchManage2Service despatchManage2Service = new DespatchManage2Service();

        #region DespatchManageOrder2 - 현장 출고지시
        [Route("PrdOut/DespatchManageOrder2")]
        [HttpGet]
        public ActionResult DespatchManageOrder2(DespatchManageOrder2 dModel)
        {
            dModel.s_issue_status = "%";       //(출고)상태
            dModel.search_ck = "0";
            dModel.s_sign_status = "%";
            dModel.s_sdate = DateTime.Today.AddMonths(-1).ToShortDateString();      //시작일자
            dModel.s_edate = DateTime.Today.ToShortDateString();        //끝일자

            DataTable DespatchManageOrder2 = despatchManageOrder2Service.DespatchManageOrder2_S1(dModel);

            //selectBox
            DataTable s_issue_status = selectBoxService.GetSelectBox("공통코드", "S", "GD402", "s_issue_status");
            DataTable issue_ck_cd = selectBoxService.GetSelectBox("공통코드", "N", "GD402", "issue_ck_cd");
            DataTable shipping_status = selectBoxService.GetSelectBox("공통코드", "N", "SR001", "shipping_status");

            //팝업
            DataTable DO2_s_cust_Popup = despatchManageOrder2Service.DO2_s_cust_Popup();        //거래처 팝업
            DataTable DO2_s_item_Popup = despatchManageOrder2Service.DO2_s_item_Popup();        //품목 팝업
            DataTable DO2_item_cd_Popup = despatchManageOrder2Service.DO2_item_cd_Popup();        //출고전표상세 - 제품코드 팝업            
            DataTable DO2_i_cust_Popup = despatchManageOrder2Service.DO2_i_cust_Popup();        //출고전표등록 - 거래처 팝업
            DataTable DO2_i_custIn_Popup = despatchManageOrder2Service.DO2_i_custIn_Popup();        //출고전표등록 - 간납처 팝업


            ViewBag.DespatchManageOrder2Data = Json(Public_Function.DataTableToJSON(DespatchManageOrder2));
            ViewBag.DespatchManageOrder2Auth = Json(HttpContext.Session["DespatchManageOrder2Auth"]);

            ViewBag.s_issue_status = s_issue_status;
            ViewBag.issue_ck_cd = issue_ck_cd;
            ViewBag.shipping_status = shipping_status;

            ViewBag.DO2_s_cust_Popup = Json(Public_Function.DataTableToJSON(DO2_s_cust_Popup));
            ViewBag.DO2_s_item_Popup = Json(Public_Function.DataTableToJSON(DO2_s_item_Popup));
            ViewBag.DO2_item_cd_Popup = Json(Public_Function.DataTableToJSON(DO2_item_cd_Popup));
            ViewBag.DO2_i_cust_Popup = Json(Public_Function.DataTableToJSON(DO2_i_cust_Popup));
            ViewBag.DO2_i_custIn_Popup = Json(Public_Function.DataTableToJSON(DO2_i_custIn_Popup));

            return View();
        }

        [HttpPost]
        public JsonResult DespatchManageOrder2_S1(DespatchManageOrder2 dModel)
        {
            DataTable DespatchManageOrder2Main = despatchManageOrder2Service.DespatchManageOrder2_S1(dModel);

            return Json(Public_Function.DataTableToJSON(DespatchManageOrder2Main));
        }

        [HttpPost]
        public JsonResult DespatchManageOrder2_S2(DespatchManageOrder2 dModel)
        {
            DataTable DespatchManageOrder2DownLeft = despatchManageOrder2Service.DespatchManageOrder2_S2(dModel);

            return Json(Public_Function.DataTableToJSON(DespatchManageOrder2DownLeft));
        }

        [HttpPost]
        public JsonResult DespatchManageOrder2_S3(DespatchManageOrder2 dModel)
        {
            DataTable DespatchManageOrder2_S3 = despatchManageOrder2Service.DespatchManageOrder2_S3(dModel);

            return Json(Public_Function.DataTableToJSON(DespatchManageOrder2_S3));
        }

        [HttpPost]
        public JsonResult DespatchManageOrder2_GetLotNoSum(DespatchManageOrder2 dModel, string check)
        {
            DataTable DespatchManageOrder2DownRight = despatchManageOrder2Service.DespatchManageOrder2_GetLotNoSum(dModel, check);

            return Json(Public_Function.DataTableToJSON(DespatchManageOrder2DownRight));
        }

        [HttpPost]
        public JsonResult DespatchManageOrder2_S_NO(DespatchManageOrder2 dModel)
        {
            DataTable DespatchManageOrder2_S_NO = despatchManageOrder2Service.DespatchManageOrder2_S_NO(dModel);

            return Json(Public_Function.DataTableToJSON(DespatchManageOrder2_S_NO));
        }

        public JsonResult DespatchManageOrder2_TRX(DespatchManageOrder2 dModel, string gubun)
        {
            if (!gubun.Equals("D"))
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Ok = false });
                }
            }

            string res = despatchManageOrder2Service.DespatchManageOrder2_TRX(dModel, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }
        [HttpPost]
        public JsonResult DespatchManageOrder2_batch([JsonBinder]List<DespatchManageOrder2> despatchManageOrder2, string gubun, string issue_detail_date, string despatch_no, string item_issue_id, string Issue_Price)
        {

            for (int i = 0; i < despatchManageOrder2.Count; i++)
            {
                if (gubun.Equals("I2"))
                {
                    despatchManageOrder2[i].gubun = "I2";
                }
                else if (gubun.Equals("U3"))
                {
                    despatchManageOrder2[i].gubun = "U3";
                }
                else if (gubun.Equals("D3"))
                {
                    despatchManageOrder2[i].gubun = "D3";
                }

            }

            string res = despatchManageOrder2Service.DespatchManageOrder2_batch(despatchManageOrder2, gubun, issue_detail_date, despatch_no, item_issue_id, Issue_Price);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }
        #endregion

        #region DespatchManage2 - 출고 전표 등록
        [Route("PrdOut/DespatchManage2")]
        [HttpGet]
        public ActionResult DespatchManage2(DespatchManage2.SrchParam despatchManage2)
        {
            DataTable DespatchManage2 = despatchManage2Service.DespatchManage2_S1(despatchManage2);
            DataTable DM2_CodeHelpCust = codeHelpService.GetCode(CodeHelpType.vender, HttpContext.Session["plantCD"].ToString(), "");
            DataTable DM2_CodeHelpItem = codeHelpService.GetCode(CodeHelpType.packingitem, HttpContext.Session["plantCD"].ToString(), "");

            ViewBag.DespatchManage2 = Json(Public_Function.DataTableToJSON(DespatchManage2));
            ViewBag.DM2_CodeHelpCust = Json(Public_Function.DataTableToJSON(DM2_CodeHelpCust));
            ViewBag.DM2_CodeHelpItem = Json(Public_Function.DataTableToJSON(DM2_CodeHelpItem));

            ViewBag.DespatchManage2Auth = Json(HttpContext.Session["DespatchManage2Auth"]);

            return View();
        }

        [HttpPost]
        public ActionResult DespatchManage2_S1(DespatchManage2.SrchParam despatchManage2)
        {
            DataTable DespatchManage2 = despatchManage2Service.DespatchManage2_S1(despatchManage2);

            return Json(Public_Function.DataTableToJSON(DespatchManage2), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DespatchManage2_S4(string despatch_no, string item_cd)
        {
            DataTable DespatchManage2DownMiddle = despatchManage2Service.DespatchManage2_S4(despatch_no, item_cd);

            return Json(Public_Function.DataTableToJSON(DespatchManage2DownMiddle), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DespatchManage2_S5(string despatch_no, string item_cd, string lot_no)
        {
            DataTable DespatchManage2_S5 = despatchManage2Service.DespatchManage2_S5(despatch_no, item_cd, lot_no);

            return Json(Public_Function.DataTableToJSON(DespatchManage2_S5), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DespatchManage2_GetLotNo2(string item_cd, string check, string despatch_no, string lot_no, string receipt_status)
        {
            DataTable DespatchManage2DownRight = despatchManage2Service.DespatchManage2_GetLotNo2(item_cd, check, despatch_no, lot_no, receipt_status);

            return Json(Public_Function.DataTableToJSON(DespatchManage2DownRight), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DespatchManage2_SelectLotNoCheck(string despatch_order_no)
        {
            DataTable DespatchManage2_SelectLotNoCheck = despatchManage2Service.DespatchManage2_SelectLotNoCheck(despatch_order_no);

            return Json(Public_Function.DataTableToJSON(DespatchManage2_SelectLotNoCheck), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DespatchManage2_DespatchOrderOK(string DESPATCH_ORDER_NO, string ISSUE_DATE, string issue_work_seq)
        {
            DataTable DespatchManage2_DespatchOrderOK = despatchManage2Service.DespatchManage2_DespatchOrderOK(DESPATCH_ORDER_NO, ISSUE_DATE, issue_work_seq);

            return Json(Public_Function.DataTableToJSON(DespatchManage2_DespatchOrderOK), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DespatchManage2_StockQtyCheck(string despatch_order_no)
        {
            DataTable DespatchManage2_StockQtyCheck = despatchManage2Service.DespatchManage2_StockQtyCheck(despatch_order_no);

            return Json(Public_Function.DataTableToJSON(DespatchManage2_StockQtyCheck), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DespatchManage2_DespatchOk(string despatch_no, string issue_work_date)
        {
            DataTable DespatchManage2_DespatchOk = despatchManage2Service.DespatchManage2_DespatchOk(despatch_no, issue_work_date);

            return Json(Public_Function.DataTableToJSON(DespatchManage2_DespatchOk), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DespatchManage2_DespatchOrderSearch(string despatch_no)
        {
            DataTable DespatchManage2_DespatchOrderSearch = despatchManage2Service.DespatchManage2_DespatchOrderSearch(despatch_no);

            return Json(Public_Function.DataTableToJSON(DespatchManage2_DespatchOrderSearch), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DespatchManage2_DespatchOrderEnd2(string despatch_no, string item_cd, string item_issue_id, string box_barcode_no, string issue_qty, string issue_work_date, string keeping_status, string lot_no, string issue_date, string cust_cd)
        {
            DataTable DespatchManage2_DespatchOrderEnd2 = despatchManage2Service.DespatchManage2_DespatchOrderEnd2(despatch_no, item_cd, item_issue_id, box_barcode_no, issue_qty, issue_work_date, keeping_status, lot_no, issue_date, cust_cd);

            return Json(Public_Function.DataTableToJSON(DespatchManage2_DespatchOrderEnd2), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DespatchManage2_batch([JsonBinder] List<DespatchManage2> despatchManage2, string gubun, string despatch_no, string Issue_Price, string box_barcode_no, string car_number, string issue_detail_date, string item_issue_id)
        {
                for (int i = 0; i < despatchManage2.Count; i++)
                {
                    if (gubun.Equals("I2"))
                    {
                        despatchManage2[i].gubun = "I2";
                    }
                    else if (gubun.Equals("U3"))
                    {
                        despatchManage2[i].gubun = "U3";
                    }
                    else if (gubun.Equals("U5"))
                    {
                        despatchManage2[i].gubun = "U5";
                    }
                    else if (gubun.Equals("D3"))
                    {
                        despatchManage2[i].gubun = "D3";
                    }
            }

            string res = despatchManage2Service.DespatchManage2_batch(despatchManage2, gubun, despatch_no, Issue_Price, box_barcode_no, car_number, issue_detail_date, item_issue_id);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }
        
        #endregion
    }
}