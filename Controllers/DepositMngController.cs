using HACCP.Libs;
using HACCP.Models.DepositMng;
using HACCP.Services.Comm;
using HACCP.Services.DepositMng;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class DepositMngController : Controller
    {

        #region 보관검체 등록 DepositSample

        public ActionResult DepositSample()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable item = codeHelpService.GetCode(CodeHelpType.item, HttpContext.Session["plantCD"].ToString(), "");
            DataTable employee = codeHelpService.GetCode(CodeHelpType.employee, HttpContext.Session["plantCD"].ToString(), "");
            DataTable warehouse = codeHelpService.GetCode(CodeHelpType.qcwarehourse, HttpContext.Session["plantCD"].ToString(), "");

            DataSet programParam = Public_Function.ProgramInitS3("DepositSample");

            //ViewBag.item = Json(Public_Function.DataTableToJSON(item));
            ViewBag.item = item;
            ViewBag.employee = Json(Public_Function.DataTableToJSON(employee));
            ViewBag.warehouse = Json(Public_Function.DataTableToJSON(warehouse));

            ViewBag.programParam = Json(JsonConvert.SerializeObject(programParam));

            return View();
        }

        public JsonResult DepositSampleSelect(DepositSample.SrchParam param)
        {
            DepositSampleService depositSampleService = new DepositSampleService();

            DataTable dt = depositSampleService.DepositSampleSelect(param);

            JsonResult jsonResultData = Json(Public_Function.DataTableToJSON(dt).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            jsonResultData.MaxJsonLength = int.MaxValue;


            return jsonResultData;
        }

        [HttpGet]
        public JsonResult DepositSampleTestNoPopup(string item_cd)
        {
            CodeHelp_TestNoService codeHelp_TestNoService = new CodeHelp_TestNoService();

            DataTable dt = codeHelp_TestNoService.GetCode(item_cd, "", "");

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string DepositSampleTRX(DepositSample depositSample)
        {
            DepositSampleService depositSampleService = new DepositSampleService();

            string res = depositSampleService.DepositSampleTRX(depositSample);

            return res;
        }

        //[HttpGet]
        //public string DepositSampleGetValidDate(string item_cd, string start_date)
        //{
        //    DepositSampleService depositSampleService = new DepositSampleService();

        //    string res = depositSampleService.DepositSampleGetValidDate(item_cd, start_date);

        //    return res;
        //}

        #endregion


        #region 보관검체 사용 요청 DepositSampleUse 

        public ActionResult DepositSampleUse()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable item = codeHelpService.GetCode(CodeHelpType.item, HttpContext.Session["plantCD"].ToString(), "");
            DataTable employee = codeHelpService.GetCode(CodeHelpType.employee, HttpContext.Session["plantCD"].ToString(), "");

            DataSet programParam = Public_Function.ProgramInitS3("DepositSampleUse");

            ViewBag.item = item;
            ViewBag.employee = Json(Public_Function.DataTableToJSON(employee));

            ViewBag.programParam = Json(JsonConvert.SerializeObject(programParam));

            return View();
        }

        public JsonResult DepositSampleUseSelect(DepositSampleUse.SrchParam param)
        {
            DepositSampleUseService depositSampleUseService = new DepositSampleUseService();

            DataTable dt = depositSampleUseService.DepositSampleUseSelect(param);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpGet]
        public JsonResult DepositSampleUseSelectDetail(string depositsample_id, string write_gb)
        {
            DepositSampleUseService depositSampleUseService = new DepositSampleUseService();

            DataTable dt = depositSampleUseService.DepositSampleUseSelectDetail(depositsample_id, write_gb);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string DepositSampleUseTRX(DepositSampleUse depositSampleUse)
        {
            DepositSampleUseService depositSampleUseService = new DepositSampleUseService();

            string res = depositSampleUseService.DepositSampleUseTRX(depositSampleUse);

            return res;
        }

        #endregion


        #region 보관검체 사용 승인 DepositSampleApproveOutput

        public ActionResult DepositSampleApproveOutput()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable item = codeHelpService.GetCode(CodeHelpType.item, HttpContext.Session["plantCD"].ToString(), "");
            DataSet programParam = Public_Function.ProgramInitS3("DepositSampleApproveOutput");

            ViewBag.item = item;
            ViewBag.programParam = Json(JsonConvert.SerializeObject(programParam));

            return View();
        }

        public JsonResult DepositSampleApproveOutputSelect(DepositSampleUse.SrchParam param)
        {
            DepositSampleApproveOutputService depositSampleApproveOutputService = new DepositSampleApproveOutputService();

            DataTable dt = depositSampleApproveOutputService.DepositSampleApproveOutputSelect(param);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpGet]
        public JsonResult DepositSampleApproveOutputSelectSign(string depositsampleuse_id, string sign_set_cd)
        {
            DepositSampleApproveOutputService depositSampleApproveOutputService = new DepositSampleApproveOutputService();

            DataTable dt = depositSampleApproveOutputService.DepositSampleApproveOutputSelectSign(depositsampleuse_id, sign_set_cd);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DepositSampleApproveOutputSelectSignTRX(DepositSampleUse.SignParam signParam) {

            DepositSampleApproveOutputService depositSampleApproveOutputService = new DepositSampleApproveOutputService();

            string res = depositSampleApproveOutputService.DepositSampleApproveOutputSelectSignTRX(signParam);

            return Json( new { message = res } );
        }

        #endregion


        #region 보관검체 반출 DepositSampleApproveOutput2

        public ActionResult DepositSampleApproveOutput2()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable item = codeHelpService.GetCode(CodeHelpType.item, HttpContext.Session["plantCD"].ToString(), "");
            DataTable employee = codeHelpService.GetCode(CodeHelpType.employee, HttpContext.Session["plantCD"].ToString(), "");
            DataSet programParam = Public_Function.ProgramInitS3("DepositSampleApproveOutput2");

            ViewBag.item = item;
            ViewBag.employee = Json(Public_Function.DataTableToJSON(employee));
            ViewBag.programParam = Json(JsonConvert.SerializeObject(programParam));

            return View();
        }

        public JsonResult DepositSampleApproveOutput2Select(DepositSampleUse.SrchParam param)
        {
            DepositSampleApproveOutputService depositSampleApproveOutputService = new DepositSampleApproveOutputService();

            DataTable dt = depositSampleApproveOutputService.DepositSampleApproveOutputSelect(param);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpPost]
        public string DepositSampleApproveOutput2TRX(DepositSampleUse depositSampleUse)
        {
            DepositSampleApproveOutputService depositSampleApproveOutputService = new DepositSampleApproveOutputService();

            string res = depositSampleApproveOutputService.DepositSampleApproveOutput2TRX(depositSampleUse);

            return res;
        }

        #endregion


        #region 보관검체 환입 DepositSampleApproveOutput3

        public ActionResult DepositSampleApproveOutput3()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable item = codeHelpService.GetCode(CodeHelpType.item, HttpContext.Session["plantCD"].ToString(), "");
            DataTable employee = codeHelpService.GetCode(CodeHelpType.employee, HttpContext.Session["plantCD"].ToString(), "");
            DataSet programParam = Public_Function.ProgramInitS3("DepositSampleApproveOutput3");

            ViewBag.item = item;
            ViewBag.employee = Json(Public_Function.DataTableToJSON(employee));
            ViewBag.programParam = Json(JsonConvert.SerializeObject(programParam));

            return View();
        }

        public JsonResult DepositSampleApproveOutput3Select(DepositSampleUse.SrchParam param)
        {
            DepositSampleApproveOutputService depositSampleApproveOutputService = new DepositSampleApproveOutputService();

            DataTable dt = depositSampleApproveOutputService.DepositSampleApproveOutputSelect(param);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpPost]
        public string DepositSampleApproveOutput3TRX(DepositSampleUse depositSampleUse)
        {
            DepositSampleApproveOutputService depositSampleApproveOutputService = new DepositSampleApproveOutputService();

            string res = depositSampleApproveOutputService.DepositSampleApproveOutput3TRX(depositSampleUse);

            return res;
        }

        #endregion


        #region 보관검체 폐기 DepositSampleDisuse 

        public ActionResult DepositSampleDisuse()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable item = codeHelpService.GetCode(CodeHelpType.item, HttpContext.Session["plantCD"].ToString(), "");

            DataSet programParam = Public_Function.ProgramInitS3("DepositSampleDisuse");

            ViewBag.item = item;

            ViewBag.programParam = Json(JsonConvert.SerializeObject(programParam));

            return View();
        }

        public JsonResult DepositSampleDisuseSelect(DepositSampleUse.SrchParam param)
        {
            DepositSampleDisuseService depositSampleDisuseService = new DepositSampleDisuseService();

            DataTable dt = depositSampleDisuseService.DepositSampleDisuseSelect(param);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpPost]
        public ActionResult DepositSampleDisuseTRX(string[] depositsample_id, string write_gb, string gubun)
        {
            DepositSampleDisuseService depositSampleDisuseService = new DepositSampleDisuseService();

            string res = depositSampleDisuseService.DepositSampleDisuseTRX(depositsample_id, write_gb, gubun);

            return Json( new { message = res } );
        }

        #endregion

    }
}