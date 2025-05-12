using HACCP.Libs;
using HACCP.Models.StabilityTest;
using HACCP.Services.Comm;
using HACCP.Services.StabilityTest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class StabilityTestController : Controller
    {
        public ActionResult StabilityTestReceipt()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable item = codeHelpService.GetCode(CodeHelpType.stability_item, HttpContext.Session["plantCD"].ToString(), "");
            DataTable employee = codeHelpService.GetCode(CodeHelpType.employee, HttpContext.Session["plantCD"].ToString(), "");

            DataSet programParam = Public_Function.ProgramInitS3("StabilityTestReceipt");

            ViewBag.item = Json(Public_Function.DataTableToJSON(item));
            ViewBag.employee = Json(Public_Function.DataTableToJSON(employee));

            ViewBag.programParam = Json(JsonConvert.SerializeObject(programParam));

            return View();
        }

        public JsonResult StabilityTestReceiptSelect(StabilityTestReceipt.SrchParam param)
        {
            StabilityTestReceiptService stabilityTestReceiptService = new StabilityTestReceiptService();

            DataTable dt = stabilityTestReceiptService.StabilityTestReceiptSelect(param);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpPost]
        public string StabilityTestReceiptTRX(StabilityTestReceipt stabilityTestReceipt)
        {
            StabilityTestReceiptService stabilityTestReceiptService = new StabilityTestReceiptService();

            string res = stabilityTestReceiptService.StabilityTestReceipt(stabilityTestReceipt);

            return res;
        }

        [HttpGet]
        public JsonResult StabilityTestReceiptGetDetail(string item_cd)
        {
            StabilityTestReceiptService stabilityTestReceiptService = new StabilityTestReceiptService();

            DataTable dt = stabilityTestReceiptService.StabilityTestReceiptGetValidDate(item_cd);
            DataTable dt2 = stabilityTestReceiptService.StabilityTestReceiptGetLotDate(item_cd);

            string[] jsonArr = new string[2];

            jsonArr[0] = Public_Function.DataTableToJSON(dt);
            jsonArr[1] = Public_Function.DataTableToJSON(dt2);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult StabilityTestReceiptGetKeepingCondition(string stability_test_type)
        {
            StabilityTestReceiptService stabilityTestReceiptService = new StabilityTestReceiptService();

            string res = stabilityTestReceiptService.StabilityTestReceiptGetKeepingCondition(stability_test_type);

            return Json( new { message = res }, JsonRequestBehavior.AllowGet);
        }

    }
}