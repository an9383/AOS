using HACCP.Libs;
using HACCP.Models.Account;
using HACCP.Services.Account;
using HACCP.Services.Comm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class AccountController : Controller
    {

        private SelectBoxService selectBoxService = new SelectBoxService();
        CodeHelpService codeHelpService = new CodeHelpService();
        AccountingDeadLineService accountingDeadLineService = new AccountingDeadLineService();

        public ActionResult AccountingDeadLine()
        {
            //DataTable AccountingDeadLineList = accountingDeadLineService.AccountingDeadLineSelect(srch);


            //ViewBag.AccountingDeadLineList = Json(Public_Function.DataTableToJSON(AccountingDeadLineList));
            return View();
        }


        [HttpPost]
        public JsonResult AccountingDeadLineSearch(AccountingDeadLine.SrchParam srch)
        {

            DataTable accountingDeadLine = accountingDeadLineService.AccountingDeadLineSelect(srch);
            string result = Public_Function.DataTableToJSON(accountingDeadLine);

            return Json(result);
        }

        [HttpPost]
        public JsonResult AccountingDeadLineUpdate(AccountingDeadLine accountingDeadLine)
        {
            string result = accountingDeadLineService.AccountingDeadLineUpdate(accountingDeadLine);

            var resJson = "{ \"message\": \"" + result + "\" }";

            return Json(resJson);
        }

        [HttpPost]
        public JsonResult AccountingDeadLineDetail1(string i_dt, string cust_cd, string no_type, string acct_dt)
        {
            DataTable accountingDeadLineDetail = accountingDeadLineService.AccountingDeadLineDetail1(i_dt, cust_cd, no_type, acct_dt);
            string result = Public_Function.DataTableToJSON(accountingDeadLineDetail);

            return Json(result);
        }


        [HttpPost]
        public JsonResult AccountingDeadLineDetail2(string n_no, string cust_cd, string no_type, string acct_dt)
        {
            DataTable accountingDeadLineDetail = accountingDeadLineService.AccountingDeadLineDetail2(n_no, cust_cd, no_type, acct_dt);
            string result = Public_Function.DataTableToJSON(accountingDeadLineDetail);

            return Json(result);
        }

        #region 발주 묶음번호 자동채번
        [HttpPost]
        public JsonResult AccountingDeadLineGetNo()
        {
            string AccountingDeadLineNo = "";

            AccountingDeadLineNo = accountingDeadLineService.AccountingDeadLineGetNo();

            return Json(AccountingDeadLineNo);
        }

        #endregion
    }
}