using HACCP.Libs;
using HACCP.Services.Comm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class SignController : Controller
    {
        SignService signService = new SignService();

        [HttpPost]
        public JsonResult getSignList(string sign_set_cd, string index_key)
        {
            DataTable result = signService.getSignList(sign_set_cd, index_key);

            return Json(Public_Function.DataTableToJSON(result));
        }

        [HttpPost]
        public JsonResult SaveSign(string sign_emp_cd, string sign_type, string sign_representative_yn, string index_key, string sign_history_id)
        {
            string result = signService.SaveSign(sign_emp_cd, sign_type, sign_representative_yn, index_key, sign_history_id);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult CancelSign(string index_key, string sign_history_id)
        {
            string result = signService.CancelSign(index_key, sign_history_id);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }


        [HttpPost]
        public JsonResult CheckAuthority(string sign_emp_cd, string sign_set_cd, string sign_set_dt_id)
        {
            DataTable dt = signService.CheckAuthority(sign_emp_cd, sign_set_cd, sign_set_dt_id);

            string jsonData = Public_Function.DataTableToJSON(dt);

            return Json(jsonData);
        }

        
        [HttpPost]
        public JsonResult InsertSignList(string sign_set_cd, string index_key)
        {
            string result = signService.InsertSignList(sign_set_cd, index_key);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult DeleteSignList(string sign_set_cd, string index_key)
        {
            string result = signService.DeleteSignList(sign_set_cd, index_key);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

    }
}