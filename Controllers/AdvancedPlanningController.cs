using HACCP.Attribute;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models;
using HACCP.Models.AdvancedPlanning;
using HACCP.Services.AdvancedPlanning;
using HACCP.Services.Comm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class AdvancedPlanningController : Controller
    {

        CodeHelpService codeHelpService = new CodeHelpService();
        PlanningOrderManageService planningOrderManageService = new PlanningOrderManageService();
        APS_Workorder_RequestService APS_Workorder_RequestService = new APS_Workorder_RequestService();
        ForcastingOrderManageService ForcastingOrderManageService = new ForcastingOrderManageService();
        SignService signService = new SignService();

        #region 제조의뢰 현황
        // GET: AdvancedPlanning
        public ActionResult PlanningOrderManage()
        {
            DataTable CustPopupData = codeHelpService.GetCode(CodeHelpType.vender, "", "");         //거래처 팝업
            DataTable EmpPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");        //사원 팝업
            DataTable ItemPopupData_M = codeHelpService.GetCode(CodeHelpType.makingitem, "", "");   //제조품목 팝업
            DataTable ItemPopupData_P = codeHelpService.GetCode(CodeHelpType.packingitem, "", "");  //포장품목 팝업
            
            DataTable PlanningOrderManage_SignSetData = signService.GetSignSetNm("7100");                              //서명정보

            ViewBag.CustPopupData = Json(Public_Function.DataTableToJSON(CustPopupData));
            ViewBag.EmpPopupData = Json(Public_Function.DataTableToJSON(EmpPopupData));
            ViewBag.ItemPopupData_M = Json(Public_Function.DataTableToJSON(ItemPopupData_M));
            ViewBag.ItemPopupData_P = Json(Public_Function.DataTableToJSON(ItemPopupData_P));
            ViewBag.PlanningOrderManage_SignSetData = Json(Public_Function.DataTableToJSON(PlanningOrderManage_SignSetData));

            return View();
        }

        [HttpPost]
        public JsonResult PlanningOrderManageSearch(PlanningOrderManage.SrchParam srch)
        {

            DataTable dt = planningOrderManageService.PlanningOrderManageSearch(srch);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }


        [HttpPost]
        public JsonResult PlanningOrderManageSave(PlanningOrderManage planningOrderManage)
        {
            string result = planningOrderManageService.PlanningOrderManageSave(planningOrderManage);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult PlanningOrderManageDelete(string order_request_no)
        {
            string result = planningOrderManageService.PlanningOrderManageDelete(order_request_no);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }


        [HttpPost]
        public JsonResult GetItemPackunit(string item_cd)
        {
            string result = planningOrderManageService.GetItemPackunit(item_cd);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }


        [HttpPost]
        public JsonResult Setting_bom_no(string order_request_c_item_cd, string order_request_h_item_cd)
        {
            string result = planningOrderManageService.Setting_bom_no(order_request_c_item_cd, order_request_h_item_cd);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult Setting_h_item(string item_cd)
        {
            DataTable result = planningOrderManageService.Setting_h_item(item_cd);

            return Json(Public_Function.DataTableToJSON(result));
        }

        [HttpPost]
        public JsonResult Setting_loss_c_qty(PlanningOrderManage planningOrderManage)
        {
            string result = planningOrderManageService.Setting_loss_c_qty(planningOrderManage);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult Setting_loss_h_qty(PlanningOrderManage planningOrderManage)
        {
            string result = planningOrderManageService.Setting_loss_h_qty(planningOrderManage);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }



        [HttpPost]
        public JsonResult StatusBySaveSign(string order_request_no, string sign_set_dt_id)
        {
            string result = planningOrderManageService.StatusBySaveSign(order_request_no, sign_set_dt_id);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }


        [HttpPost]
        public JsonResult StatusByCancelSign(string order_request_no, string sign_history_id)
        {
            string result = planningOrderManageService.StatusByCancelSign(order_request_no, sign_history_id);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }
        #endregion

        #region 원부자재 발주요망서
        public ActionResult APS_Workorder_Request()
        {
            DataTable ItemPopupData_P = codeHelpService.GetCode(CodeHelpType.packingitem, "", "");  //포장품목 팝업

            ViewBag.ItemPopupData_P = Json(Public_Function.DataTableToJSON(ItemPopupData_P));

            return View();
        }

        [HttpPost]
        public JsonResult APS_Workorder_RequestSearch(APS_Workorder_Request.SrchParam srch)
        {

            DataTable dt = APS_Workorder_RequestService.APS_Workorder_RequestServiceSearch(srch);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }

        [HttpPost]
        public JsonResult APS_Workorder_RequestDetailSearch(APS_Workorder_Request request)
        {

            DataTable dt = APS_Workorder_RequestService.APS_Workorder_RequestServiceDetailSearch(request);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }

        public JsonResult APS_Workorder_RequestDetailSave([JsonBinder] List<APS_Workorder_Request> paramData)
        {
            string message = APS_Workorder_RequestService.APS_Workorder_RequestDetailSave(paramData);
            var resJson = "{ \"message\": \"" + message + "\" }";

            return Json(resJson);
        }

         [HttpPost]
        public JsonResult AutoCalc(APS_Workorder_Request request)
        {
            string result = APS_Workorder_RequestService.AutoCalc(request);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult CancelAutoCalc(string order_request_no)
        {
            string result = APS_Workorder_RequestService.CancelAutoCalc(order_request_no);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        
        [HttpPost]
        public JsonResult ReleaseConfirmation(APS_Workorder_Request request)
        {
            string result = APS_Workorder_RequestService.ReleaseConfirmation(request);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }


        [HttpPost]
        public JsonResult InsertPlanProductionItem(string order_request_no)
        {

            string result = APS_Workorder_RequestService.InsertPlanProductionItem(order_request_no);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult InsertPlanProductionItem_M(APS_Workorder_Request request)
        {
            string result = APS_Workorder_RequestService.InsertPlanProductionItem_M(request);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult InsertPlanProductionItem_P(APS_Workorder_Request request)
        {
            string result = APS_Workorder_RequestService.InsertPlanProductionItem_P(request);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult CancelReleaseConfirmation(string order_request_no)
        {

            string result = APS_Workorder_RequestService.CancelReleaseConfirmation(order_request_no);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }


        [HttpPost]
        public void UpdateShortage(APS_Workorder_Request request)
        {
            APS_Workorder_RequestService.UpdateShortage(request);

        }

        #endregion

        #region 제조의뢰 입력(발주량 산출용)
        public ActionResult ForcastingOrderManage()
        {
            DataTable ItemPopupData_M = codeHelpService.GetCode(CodeHelpType.makingitem, "", "");   //제조품목 팝업
            DataTable ItemPopupData_P = codeHelpService.GetCode(CodeHelpType.packingitem, "", "");  //포장품목 팝업

            ViewBag.ItemPopupData_M = Json(Public_Function.DataTableToJSON(ItemPopupData_M));
            ViewBag.ItemPopupData_P = Json(Public_Function.DataTableToJSON(ItemPopupData_P));

            return View();
        }

        [HttpPost]
        public JsonResult ForcastingOrderManageSearch(ForcastingOrderManage.SrchParam srch)
        {
            DataTable dt = ForcastingOrderManageService.ForcastingOrderManageSearch(srch);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }

        [HttpPost]
        public JsonResult ForcastingOrderManageSave(ForcastingOrderManage ForcastingOrderManage)
        {
            string result = ForcastingOrderManageService.ForcastingOrderManageSave(ForcastingOrderManage);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult ForcastingOrderManageDelete(string forcast_order_no)
        {
            string result = ForcastingOrderManageService.ForcastingOrderManageDelete(forcast_order_no);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult ForcastingOrderManage_GetItemUnit(string item_cd)
        {
            string result = ForcastingOrderManageService.GetItemUnit(item_cd);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult ForcastingOrderManage_Setting_loss_h_qty(ForcastingOrderManage ForcastingOrderManage)
        {
            string result = ForcastingOrderManageService.Setting_loss_h_qty(ForcastingOrderManage);

            string jsonRes = "{ \"message\": \"" + result + "\" }";

            return Json(jsonRes);
        }

        [HttpPost]
        public JsonResult ForcastingOrderManage_Setting_h_item(string item_cd)
        {
            DataTable dt = ForcastingOrderManageService.Setting_h_item(item_cd);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
       
        }
        #endregion

    }
}