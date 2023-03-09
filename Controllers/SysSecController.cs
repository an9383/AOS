
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models;
using HACCP.Services.Comm;
using HACCP.Services.SysSec;
using System;
using System.Data;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class SysSecController : Controller
    {
        private SelectBoxService selectBoxService = new SelectBoxService();

        #region AccessLog Access Log 확인

        private AccessLogService accessLogService = new AccessLogService();

        [CheckSession]
        [HttpGet]
        public ActionResult AccessLog()
        {
            DataTable accesslog1 = accessLogService.selectAccesslogUser(DateTime.Now.AddDays(-(DateTime.Now.Day - 1)).ToShortDateString(), DateTime.Now.ToShortDateString());

            ViewBag.accesslog1 = Json(Public_Function.DataTableToJSON(accesslog1));

            return View();
        }


        [CheckSession]
        [HttpPost]
        public JsonResult SelectAccessLogUser(string emp_cd, string login_time, string logout_time)
        {
            DataTable accesslog = accessLogService.selectAccesslogTime(emp_cd, login_time, logout_time);

            JsonResult jsr = Json(Public_Function.DataTableToJSON(accesslog));

            jsr.MaxJsonLength = int.MaxValue;

            return jsr;
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectAccessLogList(string id)
        {
            DataTable accesslog = accessLogService.selectAccesslogDetail(id);

            return Json(Public_Function.DataTableToJSON(accesslog));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectAccessLog(string login_time, string logout_time)
        {
            DataTable accesslog = accessLogService.selectAccesslogUser(login_time, logout_time);

            return Json(Public_Function.DataTableToJSON(accesslog));
        }

        #endregion



        #region AuditTrail_DLIMS AuditTaril 상세확인

        private AuditTrailService auditTrailService = new AuditTrailService();

        [CheckSession]
        public ActionResult AuditTrail_DLIMS()
        {
            DataTable checkTarget = selectBoxService.GetSelectBox("공통코드", "N", "AT001", "checkTarget");
            DataTable empData = auditTrailService.getEmpData();

            ViewBag.checkTarget = checkTarget;
            ViewBag.empData = Json(Public_Function.DataTableToJSON(empData));

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectAuditTrail(string FromDate, string ToDate, string TableName, string EmpCd)
        {
            DataTable columnData = auditTrailService.getColumnData(TableName);
            DataTable auditTrailData = auditTrailService.getAuditTrailData(FromDate, ToDate, TableName, EmpCd);

            string[] arr = new string[2];

            columnData.Columns["COLUMN_NAME"].ColumnName = "caption";
            columnData.Columns["COLUMN_CODE"].ColumnName = "dataField";
            columnData.Columns.Remove("COLUMN_ID");

            arr[0] = Public_Function.DataTableToJSON(columnData);
            arr[1] = Public_Function.DataTableToJSON(auditTrailData);

            return Json(arr);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectAuditTrailDetail(string TableName, string AuditTrail_ID)
        {

            DataTable columnData = auditTrailService.getColumnData(TableName);
            DataTable auditTrailData = auditTrailService.getAuditTrailDataDetail(TableName, AuditTrail_ID);

            string[] arr = new string[2];

            columnData.Columns["COLUMN_NAME"].ColumnName = "caption";
            columnData.Columns["COLUMN_CODE"].ColumnName = "dataField";
            columnData.Columns.Remove("COLUMN_ID");

            arr[0] = Public_Function.DataTableToJSON(columnData);
            arr[1] = Public_Function.DataTableToJSON(auditTrailData);

            return Json(arr);
        }

        #endregion


    }
}