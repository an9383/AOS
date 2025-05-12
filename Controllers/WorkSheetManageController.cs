using HACCP.Libs;
using HACCP.Models.WorkSheetManage;
using HACCP.Services.Comm;
using HACCP.Services.WorkSheetManage;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HACCP.Models.Common;
using HACCP.Attribute;

namespace HACCP.Controllers
{
    public class WorkSheetManageController : Controller
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(WorkSheetManageController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        CodeHelpService codeHelpService = new CodeHelpService();
        DayOrderWorkService dayOrderWorkService = new DayOrderWorkService();


        #region 일자별 작업현황
        public ActionResult DayOrderWork(DayOrderWork.SrchParam param)
        {
            DataTable orderStatus = selectBoxService.GetSelectBox("공통코드", "S", "RT006", "orderStatus"); //공정상태 
            DataTable DayOrderWorkList = dayOrderWorkService.DayOrderWorkSelect(param);

            ViewBag.orderStatus = orderStatus;
            ViewBag.srch = param;
            ViewBag.DayOrderWorkList = Json(Public_Function.DataTableToJSON(DayOrderWorkList));
            return View();
        }

        [HttpPost]
        public JsonResult DayOrderWorkSelect(DayOrderWork.SrchParam param)
        {
            DataTable DayOrderWorkList = dayOrderWorkService.DayOrderWorkSelect(param);
            return Json(Public_Function.DataTableToJSON(DayOrderWorkList));
        }

        [HttpPost]
        public JsonResult DayOrderWorkSelectDetail(string order_no, string order_proc_id, string process_cd, string packing_order_no)
        {
            DataTable DetailList = dayOrderWorkService.DayOrderWorkSelectDetail(order_no, order_proc_id, process_cd, packing_order_no);
            return Json(Public_Function.DataTableToJSON(DetailList));
        }

        public JsonResult DayOrderWorkSelectDetailWork(string order_no, string order_proc_id, string order_detailproc_id)
        {
            DataTable DetailWorkList = dayOrderWorkService.DayOrderWorkSelectDetailWork(order_no, order_proc_id, order_detailproc_id);
            return Json(Public_Function.DataTableToJSON(DetailWorkList));
        }

        #region 리포트 관련 

        public JsonResult DayOrderWork_ReportMaster(string order_no, string grouping_cd, string sdate, string edate)
        {

            DataTable ReportMaster = dayOrderWorkService.DayOrderWork_ReportMaster(order_no, grouping_cd, sdate, edate);
            return Json(Public_Function.DataTableToJSON(ReportMaster));
        }


        [HttpPost]
        public void WorkSheetManage_ViewReport(ReportInfo reportInfo)
        {
            this.HttpContext.Session["ReportType"] = reportInfo.RptFileName;
            this.HttpContext.Session[reportInfo.RptFileName + reportInfo.RptSeq] = reportInfo;
        }

        public JsonResult PrintWeighingMasterSP(string order_no)
        {
            DataTable WorkSheetMaster = dayOrderWorkService.WorkSheetMasterSP(order_no);
            //return WorkSheetMaster;
            return Json(Public_Function.DataTableToJSON(WorkSheetMaster));
        }

        public JsonResult PrintPackingOrderMasterSP(string mp_ck, string order_no, string packing_order_no, string sign_set_cd, string sign_set_cd2)
        {
            DataTable WorkSheetMaster = dayOrderWorkService.PrintPackingOrderMasterSP(mp_ck, order_no, packing_order_no, sign_set_cd, sign_set_cd2);
            //return WorkSheetMaster;
            return Json(Public_Function.DataTableToJSON(WorkSheetMaster));
        }

        public JsonResult PrintWorkSheetMasterSP(string mp_ck, string order_no, string packing_order_no)
        {
            DataTable WorkSheetMaster = dayOrderWorkService.PrintWorkSheetMasterSP(mp_ck, order_no, packing_order_no);
            //return WorkSheetMaster;
            return Json(Public_Function.DataTableToJSON(WorkSheetMaster));
        }

        public JsonResult PrintWorkSheetDetailSP(string order_no, string order_proc_id, string order_detailproc_mid)
        {
            DataTable WorkSheetMaster = dayOrderWorkService.PrintWorkSheetDetailSP(order_no, order_proc_id, order_detailproc_mid);
            //return WorkSheetMaster;
            return Json(Public_Function.DataTableToJSON(WorkSheetMaster));
        }

        public JsonResult WorkSheetWeighMasterSP(string order_no)
        {
            DataSet WorkSheetMaster = dayOrderWorkService.WorkSheetWeighMasterSP(order_no);
            return Json(JsonConvert.SerializeObject(WorkSheetMaster));
        }

        public JsonResult DayOrderWork_DetailProcSave([JsonBinder] List<DayOrderWork> paramData)
        {
            string message = dayOrderWorkService.DayOrderWork_DetailProcSave(paramData);
            var resJson = "{ \"message\": \"" + message + "\" }";

            return Json(resJson);
        }
        #endregion

        #endregion

    }
}