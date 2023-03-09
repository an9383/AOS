using HACCP.Attribute;
using HACCP.Libs;
using HACCP.Models.TraceabilityManagement;
using HACCP.Services.Comm;
using HACCP.Services.TraceabilityManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HACCP.Controllers
{
    public class TraceabilityManagementController : Controller
    {
        StandardMaterialMasterService standardMaterialMasterService = new StandardMaterialMasterService();
        CountryCodeMasterService countryCodeMasterService = new CountryCodeMasterService();
        LotInformationService lotInformationService = new LotInformationService();
        LotTraceabilitySendService lotTraceabilitySendService = new LotTraceabilitySendService();
        DespatchInformationService despatchInformationService = new DespatchInformationService();
        DespatchTraceabilitySendService despatchTraceabilitySendService = new DespatchTraceabilitySendService();

        private SelectBoxService selectBoxService = new SelectBoxService();

        #region 표준코드 관리
        // GET: TraceabilityManagement
        public ActionResult StandardMaterialMaster()
        {
            return View();
        }

        [HttpGet]
        public string StandardMaterialMasterSearch(string standard_cd, string use_ck)
        {

            DataTable dt = standardMaterialMasterService.StandardMaterialMasterSearch(standard_cd, use_ck);
            string result = Public_Function.DataTableToJSON(dt);

            return result;
        }

        [HttpPost]
        public JsonResult StandardMaterialMasterSearchItem(string standard_cd)
        {

            DataTable dt = standardMaterialMasterService.StandardMaterialMasterSearchItem(standard_cd);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }

        [HttpPost]
        public string StandardMaterialMasterSave(string standard_cd, string use_ck)
        {
            string result = standardMaterialMasterService.StandardMaterialMasterSave(standard_cd, use_ck);

            return result;
        }

        [HttpPost]
        [ValidateInput(false)]
        public string InsertExcelData_MaterialTemp([JsonBinder] List<StandardMaterialMaster> standardData)
        {

            string result = standardMaterialMasterService.InsertExcelDataTemp(standardData);

            return result;
           //  paramData;
        }
        #endregion

        #region 국가코드 관리
        public ActionResult CountryCodeMaster()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CountryCodeMasterSearch(string country_cd, string use_ck)
        {

            DataTable dt = countryCodeMasterService.CountryCodeMasterSearch(country_cd, use_ck);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }

        [HttpPost]
        public JsonResult CountryCodeMasterSearchItem(string country_cd)
        {

            DataTable dt = countryCodeMasterService.CountryCodeMasterSearchItem(country_cd);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }

        [HttpPost]
        public string CountryCodeMasterSave(string country_cd, string use_ck)
        {
            string result = countryCodeMasterService.CountryCodeMasterSave(country_cd, use_ck);

            return result;
        }


        [HttpPost]
        [ValidateInput(false)]
        public string InsertExcelData_CountryTemp([JsonBinder] List<CountryMaster> countryData)
        {

            string result = countryCodeMasterService.InsertExcelDataTemp(countryData);

            return result;
            //  paramData;
        }
        #endregion

        #region 제품 로트 정보 준비
        public ActionResult LotInformation()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LotInformationSearch(string s_date, string e_date)
        {

            DataTable dt = lotInformationService.LotInformationSearch(s_date, e_date);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }


        [HttpPost]
        public JsonResult LotInformationDetailSearch(string gms_pdtlot_seq)
        {

            DataTable dt = lotInformationService.LotInformationDetailSearch(gms_pdtlot_seq);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }


        [HttpPost]
        public void LotInformationSave([JsonBinder] List<LotInformation> paramData)
        {

            lotInformationService.LotInformationSave(paramData);

        }


        [HttpPost]
        public void LotInformationMaterialSave([JsonBinder] List<MaterialInformation> materialData)
        {

            lotInformationService.LotInformationMaterialSave(materialData);

        }

        [HttpPost]
        public string LotInformationDelete(string gms_pdtlot_seq)
        {

            string result = lotInformationService.LotInformationDelete(gms_pdtlot_seq);

            return result;
        }

        [HttpPost]
        public void LoadAll(string s_date, string e_date)
        {
            lotInformationService.LoadAll(s_date, e_date);

        }

        [HttpPost]
        public void LoadPart(string date_S, string gms_item_stock_id, string gms_pdtlot_seq)
        {
           lotInformationService.LoadPart(date_S, gms_item_stock_id, gms_pdtlot_seq);
        }

        [HttpPost]
        public string LotInformationMaterialDelete(string gms_pdtlot_seq, string gms_material_seq)
        {
            string result = lotInformationService.LotInformationMaterialDelete(gms_pdtlot_seq, gms_material_seq);
          
            return result;
        }


        [HttpPost]
        public string LotDataCheck(string gms_pdtlot_seq)
        {
            string result = lotInformationService.LotDataCheck(gms_pdtlot_seq);

            return result;
        }

        [HttpPost]
        public void LotPreparePart(string gms_pdtlot_seq, string date_S, string emp_cd)
        {
            lotInformationService.LotPreparePart(gms_pdtlot_seq, date_S, emp_cd);
        }

        [HttpPost]
        public void LotPrepareCancel(string gms_pdtlot_seq)
        {
            lotInformationService.LotPrepareCancel(gms_pdtlot_seq);
        }
        #endregion

        #region 제품 로트 정보 전송
        public ActionResult LotTraceabilitySend()
        {
            return View();
        }


        [HttpPost]
        public JsonResult LotTraceabilitySendSearch(string start_date_S, string end_date_S, string send_status_S)
        {

            DataTable dt = lotTraceabilitySendService.LotTraceabilitySendSearch(start_date_S, end_date_S, send_status_S);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }

        [HttpPost]
        public JsonResult LotTraceabilitySendDetailSearch(string gms_pdtlot_seq)
        {

            DataTable dt = lotTraceabilitySendService.LotTraceabilitySendDetailSearch(gms_pdtlot_seq);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }
        
        [HttpPost]
        public void LotStatusChange(string gms_pdtlot_seq, string send_status)
        {
            lotTraceabilitySendService.LotStatusChange(gms_pdtlot_seq, send_status);
        }

        [HttpPost]
        public void LotSameUpdate(string gms_seq)
        {
            lotTraceabilitySendService.LotSameUpdate(gms_seq);
        }

        [HttpPost]
        public void LotSameRefresh(string gms_seq)
        {
            lotTraceabilitySendService.LotSameRefresh(gms_seq);
        }

        [HttpPost]
        public void LotSendSign(string emp_cd, string gms_pdtlot_seq)
        {
            lotTraceabilitySendService.LotSendSign(emp_cd, gms_pdtlot_seq);
        }

        [HttpPost]
        public string CallPDT_PROC([JsonBinder] List<LotInformation> lotData, [JsonBinder] List<MaterialInformation> materialData, string gubun)
        {
           string result = lotTraceabilitySendService.CallPDT_PROC(lotData, materialData, gubun);
           return result;
        }

        #endregion

        #region 출고 정보 준비
        public ActionResult DespatchInformation()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DespatchInformationSearch(string s_date, string e_date)
        {

            DataTable dt = despatchInformationService.LotInformationSearch(s_date, e_date);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }


        [HttpPost]
        public void DespatchInformationSave([JsonBinder] List<DespatchInformation> paramData)
        {

            despatchInformationService.LotInformationSave(paramData);

        }


        [HttpPost]
        public void DepatchLoadAll(string s_date, string e_date)
        {
            despatchInformationService.DepatchLoadAll(s_date, e_date);

        }

        [HttpPost]
        public void DepatchLoadPart(string date_S, string gms_despatch_order_no, string gms_item_issue_id)
        {
            despatchInformationService.DepatchLoadPart(date_S, gms_despatch_order_no, gms_item_issue_id);
        }

        [HttpPost]
        public string DepatchDataCheck(string gms_seq)
        {
            string result = despatchInformationService.DepatchDataCheck(gms_seq);

            return result;
        }

        [HttpPost]
        public void DespatchPreparePart(string gms_seq, string tgow_dt, string emp_cd)
        {
            despatchInformationService.DespatchPreparePart(gms_seq, tgow_dt, emp_cd);
        }

        [HttpPost]
        public void DespatchPrepareCancel(string gms_seq)
        {
            despatchInformationService.DespatchPrepareCancel(gms_seq);
        }
        #endregion

        #region 출고 정보 전송
        public ActionResult DespatchTraceabilitySend()
        {
            return View();
        }


        [HttpPost]
        public JsonResult DespatchTraceabilitySendSearch(string start_date_S, string end_date_S, string send_status_S)
        {

            DataTable dt = despatchTraceabilitySendService.DespatchTraceabilitySendSearch(start_date_S, end_date_S, send_status_S);
            string result = Public_Function.DataTableToJSON(dt);

            return Json(result);
        }


        [HttpPost]
        public void DespatchStatusChange(string gms_pdtlot_seq, string send_status)
        {
            despatchTraceabilitySendService.DespatchStatusChange(gms_pdtlot_seq, send_status);
        }

        [HttpPost]
        public void DespatchSendSign(string emp_cd, string gms_pdtlot_seq)
        {
            despatchTraceabilitySendService.DespatchSendSign(emp_cd, gms_pdtlot_seq);
        }

        [HttpPost]
        public string CallDespatchPDT_PROC([JsonBinder] List<DespatchInformation> despatchData)
        {
            string result = despatchTraceabilitySendService.CallDespatchPDT_PROC(despatchData);
            return result;
        }
        #endregion
    }
}