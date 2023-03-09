using HACCP.Libs;
using HACCP.Models.Dep;
using HACCP.Services.Comm;
using HACCP.Services.Dep;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class DepController : Controller
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(SysSetController));

        private SelectBoxService selectBoxService = new SelectBoxService();

        DateTime sd = new DateTime();
        DateTime ed = new DateTime();

        #region 일탈등록 

        private DeviationRegService deviationRegService = new DeviationRegService();

        [HttpGet]
        public ActionResult DeviationReg()
        {
            ed = DateTime.Now;
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");

            // 메인 그리드 데이터 조회
            DataTable deviationRegData = deviationRegService.Select(sDate, eDate,"", "1");

            // 셀렉트 박스 데이터 조회
            DataTable depStatus_s = selectBoxService.GetSelectBox("공통코드", "S", "DS005", "depStatus_s"); // 일탈 진행 상태_검색용

            // 팝업 데이터 조회
            DataTable empPopupData = deviationRegService.getEmpPopupData();
            DataTable workRoomPopupData = deviationRegService.getWorkRoomPopupData();
            DataTable itemPopupData = deviationRegService.getItemPopupData();
            DataTable testItemPopupData = deviationRegService.getTestItemPopupData();
            DataTable equipPopupData = deviationRegService.getEquipPopupData();
            DataTable deptPopupData = deviationRegService.getDeptPopupData();



            // 메인 그리드 데이터 ViewBag
            ViewBag.deviationRegData = Json(Public_Function.DataTableToJSON(deviationRegData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            // 셀렉트 박스 데이터 ViewBag
            ViewBag.depStatus_s = depStatus_s;

            // 사원정보 팝업 데이터 ViewBag
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.workRoomPopupData = Json(Public_Function.DataTableToJSON(workRoomPopupData));
            ViewBag.itemPopupData = Json(Public_Function.DataTableToJSON(itemPopupData));
            ViewBag.testItemPopupData = Json(Public_Function.DataTableToJSON(testItemPopupData));
            ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            ViewBag.deptPopupData = Json(Public_Function.DataTableToJSON(deptPopupData));

            ViewBag.deviationRegAuth = Json(HttpContext.Session["deviationRegAuth"]);

            return View();
        }


        #region 기본CRUD
        [HttpPost]
        public JsonResult DeviationRegSearch(string sDate, string eDate, string emp_cd, string emp_nm, string status)
        {
            DataTable deviationRegData = deviationRegService.Select(sDate, eDate, emp_cd, status);

            return ViewBag.deviationRegData = Json(Public_Function.DataTableToJSON(deviationRegData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 일탈 입력
        [HttpPost]
        public ActionResult DeviationRegSave(Deviation data, string _CRUDGubun)
        {
            string message = "";

            if(_CRUDGubun == "I")
            {
                message = deviationRegService.DeviationRegSave(data);
            }else if(_CRUDGubun == "U")
            {
                message = deviationRegService.DeviationRegEdit(data);
            }


            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }


        // 일탈 삭제
        [HttpPost]
        public ActionResult DeviationRegDelete(string deviation_no)
        {
            string message = "";

            message = deviationRegService.DeviationRegDelete(deviation_no);
           

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }


        // 상급자 조회
        [HttpPost]
        public JsonResult CheckConfirmEmp(string emp_cd)
        {
            DataTable confirmEmpData = deviationRegService.CheckConfirmEmp(emp_cd);

            return ViewBag.confirmEmpData = Json(Public_Function.DataTableToJSON(confirmEmpData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }
        #endregion 기본CRUD END


        [HttpPost]
        public JsonResult DeviationRefNoSearch(Deviation data)
        {
            DataTable DeviationRefNoData = deviationRegService.DeviationRefNoSearch(data.deviation_ref_item_cd);

            return ViewBag.DeviationRefNoData = Json(Public_Function.DataTableToJSON(DeviationRefNoData));
        }


        [HttpPost]
        public JsonResult DeviationRefProcessCDSearch(Deviation data)
        {
            DataTable DeviationRefProcessCDData = deviationRegService.DeviationRefProcessCDSearch(data.deviation_ref_item_cd);

            return ViewBag.DeviationRefProcessCDData = Json(Public_Function.DataTableToJSON(DeviationRefProcessCDData));
        }



        #endregion 일탈등록 END


        #region 일탈접수 
        private DeviationReceiptService deviationReceiptService = new DeviationReceiptService();

        [HttpGet]
        public ActionResult DeviationReceipt()
        {
            ed = DateTime.Now;
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");

            // 메인 그리드 데이터 조회
            DataTable deviationReceiptData = deviationReceiptService.Select(sDate, eDate, "1");

            // 셀렉트 박스 데이터 조회
            DataTable depStatus_s = selectBoxService.GetSelectBox("공통코드", "S", "DS005", "depStatus_s"); // 일탈 진행 상태_검색용
            DataTable depClass_d = selectBoxService.GetSelectBox("공통코드", "D", "DS012", "depClass_d"); // 일탈구분_입력용
            DataTable depLevel_d = selectBoxService.GetSelectBox("공통코드", "D", "DS003", "depLevel_d"); // 심각도_입력용
            DataTable requestLavel_d = selectBoxService.GetSelectBox("공통코드", "D", "DS007", "requestLavel_d"); // 심각도_입력용

            // 팝업 데이터 조회
            DataTable empPopupData = deviationRegService.getEmpPopupData();



            // 메인 그리드 데이터 ViewBag
            ViewBag.deviationReceiptData = Json(Public_Function.DataTableToJSON(deviationReceiptData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            // 셀렉트 박스 데이터 ViewBag
            ViewBag.depStatus_s = depStatus_s;
            ViewBag.depClass_d = depClass_d;
            ViewBag.depLevel_d = depLevel_d;
            ViewBag.requestLavel_d = requestLavel_d;


            // 사원정보 팝업 데이터 ViewBag
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            ViewBag.deviationReceiptAuth = Json(HttpContext.Session["deviationReceiptAuth"]);

            return View();
        }


        #region 기본CRUD
        [HttpPost]
        public JsonResult DeviationReceiptSearch(string sDate, string eDate, string status)
        {
            DataTable deviationReceiptData = deviationReceiptService.Select(sDate, eDate, status);

            return ViewBag.deviationReceiptData = Json(Public_Function.DataTableToJSON(deviationReceiptData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }
        #endregion 기본CURD END


        #region 일탈접수 관련
        // 일탈접수 수정
        [HttpPost]
        public ActionResult ReceiptSave(Deviation data)
        {
            string message = "";

            message = deviationReceiptService.ReceiptEdit(data);


            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }


        // 일탈접수 삭제
        [HttpPost]
        public ActionResult ReceiptDelete(string deviation_no)
        {
            string message = "";

            message = deviationReceiptService.ReceiptDelete(deviation_no);


            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }

        #endregion 일탈접수 관련 END


        #region 계획 관련
        [HttpPost]
        public JsonResult DeviationReceiptCapaSearch(string deviation_no)
        {
            DataTable deviationReceiptCapaData = deviationReceiptService.DeviationReceiptCapaSearch(deviation_no);

            return ViewBag.deviationReceiptCapaData = Json(Public_Function.DataTableToJSON(deviationReceiptCapaData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 계획 입력
        [HttpPost]
        public ActionResult CapaSave(string deviation_no, string request_emp_cd, string request_date, string request_contents, string request_level, string despatch_limit, string despatch_charge_emp_cd, string deviation_capa_id,string CRUDGubun)
        {
            string message = "";
            
            if(CRUDGubun == "I")
            {
                message = deviationReceiptService.CapaInsert(deviation_no, request_emp_cd, request_date, request_contents, request_level, despatch_limit, despatch_charge_emp_cd);

            }
            else if(CRUDGubun == "U")
            {
                message = deviationReceiptService.CapaEdit(deviation_capa_id, request_emp_cd, request_date, request_contents, request_level, despatch_limit, despatch_charge_emp_cd);
            }


            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }


        // 계획 삭제
        [HttpPost]
        public ActionResult CapaDelete(string deviation_capa_id)
        {
            string message = "";

            message = deviationReceiptService.CapaDelete(deviation_capa_id);


            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }

        #endregion 계확 관련 END

        #endregion 일탈접수 END


        #region 조사지시
        private InvestigationOrderService investigationOrderService = new InvestigationOrderService();

        [HttpGet]
        public ActionResult InvestigationOrder()
        {
            ed = DateTime.Now.AddMonths(1);
            sd = DateTime.Now.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");

            // 메인 그리드 데이터 조회
            DataTable InvestigationOrderData = investigationOrderService.Select(sDate, eDate, "", "", "");

            // 셀렉트 박스 데이터 조회
            DataTable deviation_type_s = selectBoxService.GetSelectBox("공통코드", "S", "DS001", "deviation_type_s"); // 일탈타입_검색용
            DataTable deviation_level_s = selectBoxService.GetSelectBox("공통코드", "S", "DS003", "deviation_level_s"); // 심각도_검색용
            DataTable deviation_status_s = selectBoxService.GetSelectBox("공통코드", "S", "DS005", "deviation_status_s"); // 진행상태_검색용
            DataTable deviation_level_n = selectBoxService.GetSelectBox("공통코드", "N", "DS003", "deviation_level_n"); // 심각도
            DataTable request_level_d = selectBoxService.GetSelectBox("공통코드", "D", "DS007", "request_level_d"); // 의뢰종류_입력용

            // 팝업 데이터 조회
            DataTable empPopupData = deviationRegService.getEmpPopupData();



            // 메인 그리드 데이터 ViewBag
            ViewBag.InvestigationOrderData = Json(Public_Function.DataTableToJSON(InvestigationOrderData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            // 셀렉트 박스 데이터 ViewBag
            ViewBag.deviation_type_s = deviation_type_s;
            ViewBag.deviation_level_s = deviation_level_s;
            ViewBag.deviation_status_s = deviation_status_s;
            ViewBag.deviation_level_n = deviation_level_n;
            ViewBag.request_level_d = request_level_d;


            // 사원정보 팝업 데이터 ViewBag
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            ViewBag.InvestigationOrderAuth = Json(HttpContext.Session["InvestigationOrderAuth"]);

            return View();
        }


        #region 기본CRUD
        [HttpPost]
        public JsonResult InvestigationOrderSearch(string sDate, string eDate, string type, string level, string status)
        {
            DataTable InvestigationOrderData = investigationOrderService.Select(sDate, eDate, type, level, status);

            return ViewBag.InvestigationOrderData = Json(Public_Function.DataTableToJSON(InvestigationOrderData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        #endregion 기본CURD END


        #region 발생보고서 관련
        [HttpPost]
        public JsonResult InvestigationOrderCapaSearch(string deviation_no)
        {
            DataTable InvestigationOrderCapaData = investigationOrderService.CapaSearch(deviation_no);

            return ViewBag.InvestigationOrderCapaData = Json(Public_Function.DataTableToJSON(InvestigationOrderCapaData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 발생보고서 저장
        [HttpPost]
        public ActionResult InvestigationOrderCapaSave(string deviation_no, string request_emp_cd, string request_date, string request_contents, string request_level, string despatch_limit, string despatch_charge_emp, string deviation_capa_id, string CRUDGubun)
        {
            string message = "";

            if (CRUDGubun == "I")
            {
                message = investigationOrderService.CapaInsert(deviation_no, request_emp_cd, request_date, request_contents, request_level, despatch_limit, despatch_charge_emp);

            }
            else if (CRUDGubun == "U")
            {
                message = investigationOrderService.CapaEdit(deviation_no, request_emp_cd, request_date, request_contents, request_level, despatch_limit, despatch_charge_emp, deviation_capa_id);
            }


            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }


        // 발생보고서 삭제
        [HttpPost]
        public ActionResult InvestigationOrderCapaDelete(string deviation_no, string deviation_capa_id)
        {
            string message = "";

            message = investigationOrderService.CapaDelete(deviation_no, deviation_capa_id);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }

        #endregion 발생보고서 관련 END


        #region 조치계획서 관련
        [HttpPost]
        public JsonResult InvestigationOrderServeySearch(string deviation_no)
        {
            DataTable InvestigationOrderServeyData = investigationOrderService.ServeySearch(deviation_no);

            return ViewBag.InvestigationOrderServeyData = Json(Public_Function.DataTableToJSON(InvestigationOrderServeyData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 조치계획서 저장
        [HttpPost]
        public ActionResult InvestigationOrderServeySave(string deviation_no, string survey_order_emp, string survey_order_date, string survey_charge_emp, string survey_limit, string survey_contents, string deviation_survey_id, string CRUDGubun)
        {
            string message = "";

            if (CRUDGubun == "I")
            {
                message = investigationOrderService.ServeyInsert(deviation_no, survey_order_emp, survey_order_date, survey_charge_emp, survey_limit, survey_contents);

            }
            else if (CRUDGubun == "U")
            {
                message = investigationOrderService.ServeyEdit(deviation_no, survey_order_emp, survey_order_date, survey_charge_emp, survey_limit, deviation_survey_id, survey_contents);
            }


            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }


        // 조치계획서 삭제
        [HttpPost]
        public ActionResult InvestigationOrderServeyDelete(string deviation_no, string deviation_survey_id)
        {
            string message = "";

            message = investigationOrderService.ServeyDelete(deviation_no, deviation_survey_id);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }

        #endregion 계획서 관련 END


        #region 서명 관련
        // 서명 조회
        [HttpPost]
        public JsonResult InvestigationOrderDisplayESInfo(string deviation_no, string sign_set_cd)
        {
            DataTable signInfoData = investigationOrderService.DisplayESInfo(deviation_no, sign_set_cd);

            return ViewBag.signInfoData = Json(Public_Function.DataTableToJSON(signInfoData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 서명 입력 삭제
        [HttpPost]
        public JsonResult InvestigationOrderSign_CRUD(string gubun, string deviation_no, string emp_cd)
        {
            string message = "";

            if (gubun == "ES")
            {
                message = investigationOrderService.InvestigationOrderSignSave(deviation_no, emp_cd);
            }
            else if (gubun == "DEL_ES")
            {
                message = investigationOrderService.InvestigationOrderSignDelete(deviation_no);
            }


            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        #endregion 서명관련 END


        public ActionResult updateLevel(string deviation_no, string deviation_level)
        {
            string message = "";

            message = investigationOrderService.updateLevel(deviation_no, deviation_level);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }

        #endregion


        #region 일탈 조사
        private DeviationInvestigationService deviationInvestigationService = new DeviationInvestigationService();

        [HttpGet]
        public ActionResult DeviationInvestigation()
        {
            ed = DateTime.Now.AddMonths(1);
            sd = DateTime.Now.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");

            // 메인 그리드 데이터 조회
            DataTable DeviationInvestigationData = deviationInvestigationService.Select(sDate, eDate, "", "");

            // 셀렉트 박스 데이터 조회
            //DataTable deviation_type_s = selectBoxService.GetSelectBox("공통코드", "S", "DS001", "deviation_type_s"); // 일탈타입_검색용
            //DataTable deviation_level_s = selectBoxService.GetSelectBox("공통코드", "S", "DS003", "deviation_level_s"); // 심각도_검색용
            //DataTable deviation_status_s = selectBoxService.GetSelectBox("공통코드", "S", "DS005", "deviation_status_s"); // 진행상태_검색용
            //DataTable deviation_level_n = selectBoxService.GetSelectBox("공통코드", "N", "DS003", "deviation_level_n"); // 심각도
            //DataTable request_level_d = selectBoxService.GetSelectBox("공통코드", "D", "DS007", "request_level_d"); // 의뢰종류_입력용
            DataTable dep_servey_status_s = selectBoxService.GetSelectBox("공통코드", "S", "DS010", "dep_servey_status_s"); // 검색용 조사상태
            DataTable servey_result_type_d = selectBoxService.GetSelectBox("공통코드", "D", "DS011", "servey_result_type_d"); // 입력용 원인파악 수준

            // 팝업 데이터 조회
            DataTable empPopupData = deviationRegService.getEmpPopupData();



            // 메인 그리드 데이터 ViewBag
            ViewBag.DeviationInvestigationData = Json(Public_Function.DataTableToJSON(DeviationInvestigationData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            // 셀렉트 박스 데이터 ViewBag
            ViewBag.dep_servey_status_s = dep_servey_status_s;
            ViewBag.servey_result_type_d = servey_result_type_d;
            //ViewBag.deviation_status_s = deviation_status_s;
            //ViewBag.deviation_level_n = deviation_level_n;
            //ViewBag.request_level_d = request_level_d;


            // 사원정보 팝업 데이터 ViewBag
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            ViewBag.DeviationInvestigationAuth = Json(HttpContext.Session["DeviationInvestigationAuth"]);

            return View();
        }


        #region 기본CRUD
        [HttpPost]
        public JsonResult DeviationInvestigationSearch(string sDate, string eDate, string emp_cd, string status)
        {
            DataTable DeviationInvestigationData = deviationInvestigationService.Select(sDate, eDate, emp_cd, status);

            return ViewBag.DeviationInvestigationData = Json(Public_Function.DataTableToJSON(DeviationInvestigationData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult DeviationInvestigationSave(string deviation_no, DeviationSurvey data)
        {
            string message;

            message = deviationInvestigationService.DeviationInvestigationSave(deviation_no, data);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult DeviationInvestigationDelete(string deviation_no, string deviation_survey_id)
        {
            string message;

            message = deviationInvestigationService.DeviationInvestigationDelete(deviation_no, deviation_survey_id);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }

        #endregion 기본CURD END


        #region 첨부파일 관련
        // 첨부파일 조회
        public JsonResult DeviationInvestigationFileSearch(string deviation_no, string deviation_survey_id)
        {
            DataTable fileData = deviationInvestigationService.FileSearch(deviation_no, deviation_survey_id);

            return ViewBag.fileData = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 파일 첨부
        [HttpPost]
        public ActionResult DeviationInvestigationFileAdd(string deviation_no, string deviation_survey_id)
        {
            string message = "";

            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                //var fgubun = name.Substring(name.Length - 1, 1);

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = deviationInvestigationService.FileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, deviation_no, deviation_survey_id);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpGet]
        public ActionResult DeviationInvestigationFileOpen(string doc_file_id)
        {
            DataTable dt = deviationInvestigationService.FileOpen(doc_file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }


        [HttpPost]
        public ActionResult DeviationInvestigationFileDelete(string deviation_no, string deviation_survey_id, string deviation_survey_data_id, string doc_file_id)
        {
            string message;

            message = deviationInvestigationService.FileDelete(deviation_no, deviation_survey_id, deviation_survey_data_id, doc_file_id);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult UpdateFileComment(string doc_file_id, string file_remark)
        {
            string message;

            message = deviationInvestigationService.UpdateFileComment(doc_file_id, file_remark);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }

        #endregion 첨부파일 관련 END


        #region 서명관련
        [HttpPost]
        public JsonResult DeviationInvestigationSignSearch(string deviation_no, string deviation_survey_id, string sign_set_cd)
        {
            DataTable signInfoData = deviationInvestigationService.SignSearch(deviation_no, deviation_survey_id, sign_set_cd);

            return ViewBag.signInfoData = Json(Public_Function.DataTableToJSON(signInfoData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult DeviationOmvestogationSignSave(string deviation_no, string deviation_survey_id, string survey_emp_cd, string deviation_sign_set_seq)
        {
            string message = "";

            message = deviationInvestigationService.SignSave(deviation_no, deviation_survey_id, survey_emp_cd, deviation_sign_set_seq);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        #endregion 서명관련 END


        #endregion


        #region 일탈처리 승인
        private ConfirmDeviationProcessService confirmDeviationProcessService = new ConfirmDeviationProcessService();

        [HttpGet]
        public ActionResult ConfirmDeviationProcess()
        {
            ed = DateTime.Now;
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");

            // 메인 그리드 데이터 조회
            DataTable ConfirmDeviationProcessData = confirmDeviationProcessService.Select(sDate, eDate, "", "", "");

            // 셀렉트 박스 데이터 조회
            DataTable deviation_type_s = selectBoxService.GetSelectBox("공통코드", "S", "DS001", "deviation_type_s"); // 일탈타입_검색용
            DataTable deviation_level_s = selectBoxService.GetSelectBox("공통코드", "S", "DS003", "deviation_level_s"); // 심각도_검색용
            DataTable deviation_status_s = selectBoxService.GetSelectBox("공통코드", "S", "DS005", "deviation_status_s"); // 진행상태_검색용
            DataTable deviation_result_yn_d = selectBoxService.GetSelectBox("공통코드", "D", "DS004", "deviation_result_yn_d"); // 처리결괴_입력용
            //DataTable deviation_level_n = selectBoxService.GetSelectBox("공통코드", "N", "DS003", "deviation_level_n"); // 심각도
            DataTable request_level_d = selectBoxService.GetSelectBox("공통코드", "D", "DS007", "request_level_d"); // 의뢰종류_입력용
            DataTable survey_result_type_d = selectBoxService.GetSelectBox("공통코드", "D", "DS011", "survey_result_type_d"); // 입력용 원인파악 수준

            // 팝업 데이터 조회
            DataTable empPopupData = confirmDeviationProcessService.getEmpPopupData();



            // 메인 그리드 데이터 ViewBag
            ViewBag.ConfirmDeviationProcessData = Json(Public_Function.DataTableToJSON(ConfirmDeviationProcessData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            // 셀렉트 박스 데이터 ViewBag
            ViewBag.deviation_type_s = deviation_type_s;
            ViewBag.deviation_level_s = deviation_level_s;
            ViewBag.deviation_status_s = deviation_status_s;
            ViewBag.deviation_result_yn_d = deviation_result_yn_d;
            ViewBag.request_level_d = request_level_d;
            ViewBag.survey_result_type_d = survey_result_type_d;


            // 사원정보 팝업 데이터 ViewBag
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            ViewBag.ConfirmDeviationProcessAuth = Json(HttpContext.Session["ConfirmDeviationProcessAuth"]);

            return View();
        }


        #region 기본CRUD
        [HttpPost]
        public JsonResult ConfirmDeviationProcessSearch(string sDate, string eDate, string type, string level, string status)
        {
            DataTable ConfirmDeviationProcessData = confirmDeviationProcessService.Select(sDate, eDate, type, level, status);

            return ViewBag.ConfirmDeviationProcessData = Json(Public_Function.DataTableToJSON(ConfirmDeviationProcessData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 일탈내용 조회
        [HttpPost]
        public JsonResult ConfirmDeviationProcessDeviationSearch(string deviation_no)
        {
            DataTable dt = confirmDeviationProcessService.ConfirmDeviationProcessDeviationSearch(deviation_no);

            return ViewBag.dt = Json(Public_Function.DataTableToJSON(dt).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 중간조치 조회
        [HttpPost]
        public JsonResult ConfirmDeviationProcessRequestSearch(string deviation_no)
        {
            DataTable dt = confirmDeviationProcessService.requestSearch(deviation_no);

            return ViewBag.dt = Json(Public_Function.DataTableToJSON(dt).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        
        // 조사결과 조회
        [HttpPost]
        public JsonResult ConfirmDeviationProcessResultSearch(string deviation_no)
        {
            DataTable dt = confirmDeviationProcessService.resultSearch(deviation_no);

            return ViewBag.dt = Json(Public_Function.DataTableToJSON(dt).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 처리결과 수정
        [HttpPost]
        public ActionResult ConfirmDeviationProcessSave(string deviation_no, string deviation_result_yn, string deviation_result, string deviation_end_date, string deviation_inquiry_contents, string deviation_inquiry_cause, string deviation_result_remark)
        {
            string message = "";

            message = confirmDeviationProcessService.ConfirmDeviationProcessSave(deviation_no, deviation_result_yn, deviation_result, deviation_end_date, deviation_inquiry_contents, deviation_inquiry_cause, deviation_result_remark);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        // 처리결과/예방조치 삭제
        [HttpPost]
        public ActionResult ConfirmDeviationProcessDelete(string deviation_no)
        {
            string message = "";

            message = confirmDeviationProcessService.ConfirmDeviationProcessDelete(deviation_no);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }

        #endregion 기본CURD END


        #region 서명관련
        [HttpPost]
        public JsonResult ConfirmDeviationProcessSignSearch(string deviation_no, string sign_set_cd)
        {
            DataTable signData = new DataTable();

            signData = confirmDeviationProcessService.SignSearch(deviation_no, sign_set_cd);

            return ViewBag.signData = Json(Public_Function.DataTableToJSON(signData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult ConfirmDeviationProcessSignSave(string deviation_no, string emp_cd, string type)
        {
            string message = "";

            message = confirmDeviationProcessService.SignSave(deviation_no, emp_cd, type);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        // 중간조치 확인
        [HttpPost]
        public ActionResult RequestCheck(string deviation_no)
        {
            string message = "";

            message = confirmDeviationProcessService.RequestCheck(deviation_no);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        #endregion 서명관련 END


        #region 예방조치 관련
        // 예방조치 조회
        [HttpPost]
        public JsonResult ConfirmDeviationProcessRequest2Search(string deviation_no)
        {
            DataTable ConfirmDeviationProcessRequestData = confirmDeviationProcessService.request2Search(deviation_no);

            return ViewBag.ConfirmDeviationProcessRequestData = Json(Public_Function.DataTableToJSON(ConfirmDeviationProcessRequestData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 예방조치 입력
        [HttpPost]
        public ActionResult ConfirmDeviationProcessResultSave(string deviation_no, string request_emp_cd, string request_date, string request_level, string despatch_limit, string despatch_charge_emp, string request_contents)
        {
            string message = "";

            message = confirmDeviationProcessService.ResultSave(deviation_no, request_emp_cd, request_date, request_level, despatch_limit, despatch_charge_emp, request_contents);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        #endregion 예방조치 관련 END


        #region 첨부파일 관련
        // 첨부파일 조회
        public JsonResult ConfirmDeviationProcessFileSearch(string deviation_no, string deviation_survey_id)
        {
            DataTable fileData = confirmDeviationProcessService.FileSearch(deviation_no);

            return ViewBag.fileData = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }



        [HttpGet]
        public ActionResult ConfirmDeviationProcessFileOpen(string doc_file_id)
        {
            DataTable dt = confirmDeviationProcessService.FileOpen(doc_file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }

        //// 파일 첨부
        //[HttpPost]
        //public ActionResult DeviationInvestigationFileAdd(string deviation_no, string deviation_survey_id)
        //{
        //    string message = "";

        //    try
        //    {
        //        var uploadFile = Request.Files[0];

        //        var fileName = uploadFile.FileName;
        //        //var fgubun = name.Substring(name.Length - 1, 1);

        //        using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
        //        {
        //            message = deviationInvestigationService.FileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, deviation_no, deviation_survey_id);
        //        }
        //    }
        //    catch
        //    {
        //        Response.StatusCode = 400;
        //    }

        //    var resJson = "{ \"message\": \"" + message + "\" }";
        //    return Json(resJson);
        //}


        //[HttpGet]
        //public ActionResult DeviationInvestigationFileOpen(string doc_file_id)
        //{
        //    DataTable dt = deviationInvestigationService.FileOpen(doc_file_id);

        //    var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

        //    string mimeType = GetMimeType(tmp);


        //    return File(
        //        (byte[])dt.Rows[0].ItemArray[0]
        //        , mimeType
        //        , dt.Rows[0].ItemArray[1].ToString()
        //        );
        //}


        //[HttpPost]
        //public ActionResult DeviationInvestigationFileDelete(string deviation_no, string deviation_survey_id, string deviation_survey_data_id, string doc_file_id)
        //{
        //    string message;

        //    message = deviationInvestigationService.FileDelete(deviation_no, deviation_survey_id, deviation_survey_data_id, doc_file_id);

        //    var resJson = "{ \"messege\": \"" + message + "\" }";
        //    return Json(resJson);
        //}
        #endregion

        #endregion 일탈처리승인 END


        #region 시정 및 예방조치
        private Deviation_CapaService deviation_CapaService = new Deviation_CapaService();

        [HttpGet]
        public ActionResult Deviation_Capa()
        {
            ed = DateTime.Now;
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");

            // 메인 그리드 데이터 조회
            DataTable Deviation_CapaData = deviation_CapaService.Select(sDate, eDate, "", "%");

            // 셀렉트 박스 데이터 조회
            DataTable deviation_capa_status_s = selectBoxService.GetSelectBox("공통코드", "S", "DS008", "deviation_capa_status_s"); // 처리상태_검색용
            DataTable deviation_capa_status_d = selectBoxService.GetSelectBox("공통코드", "D", "DS008", "deviation_capa_status_d"); // 처리상태_입력용



            // 팝업 데이터 조회
            DataTable empPopupData = deviation_CapaService.getEmpPopupData();


            // 메인 그리드 데이터 ViewBag
            ViewBag.Deviation_CapaData = Json(Public_Function.DataTableToJSON(Deviation_CapaData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            // 셀렉트 박스 데이터 ViewBag
            ViewBag.deviation_capa_status_s = deviation_capa_status_s;
            ViewBag.deviation_capa_status_d = deviation_capa_status_d;


            // 사원정보 팝업 데이터 ViewBag
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            ViewBag.Deviation_CapaAuth = Json(HttpContext.Session["Deviation_CapaAuth"]);

            return View();
        }


        [HttpPost]
        public JsonResult Deviation_CapaSearch(string sDate, string eDate, string emp_cd, string status)
        {
            DataTable Deviation_CapaData = deviation_CapaService.Select(sDate, eDate, emp_cd, status);

            return ViewBag.Deviation_CapaData = Json(Public_Function.DataTableToJSON(Deviation_CapaData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        // 예방조치 수정
        [HttpPost]
        public ActionResult Deviation_CapaSave(string request_contents, string despatch_date, string despatch_emp_cd, string despatch_contents, string deviation_capa_status, string deviation_capa_id)
        {
            string message = "";

            message = deviation_CapaService.Deviation_CapaSave(request_contents, despatch_date, despatch_emp_cd, despatch_contents, deviation_capa_status, deviation_capa_id);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        // 예방조치 삭제
        [HttpPost]
        public ActionResult Deviation_CapaDelete(string deviation_capa_id)
        {
            string message = "";

            message = deviation_CapaService.Deviation_CapaDelete(deviation_capa_id);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        #region 서명관련
        [HttpPost]
        public JsonResult Deviation_CapaSignSearch(string deviation_capa_id, string sign_set_cd)
        {
            DataTable Deviation_CapaSignData = deviation_CapaService.SignSearch(deviation_capa_id, sign_set_cd);

            return ViewBag.Deviation_CapaSignData = Json(Public_Function.DataTableToJSON(Deviation_CapaSignData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult Deviation_CapaSignSave(string deviation_capa_id, string despatch_emp_cd)
        {
            string message = "";

            message = deviation_CapaService.SignSave(deviation_capa_id, despatch_emp_cd);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }

        #endregion 서명관련 END

        #endregion 시정 및 예방조치 END


        #region 파일 다운로드 확장자 정보
        private IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {

        #region Big freaking list of mime types
        // combination of values from Windows 7 Registry and 
        // from C:\Windows\System32\inetsrv\config\applicationHost.config
        // some added, including .7z and .dat
        {".323", "text/h323"},
        {".3g2", "video/3gpp2"},
        {".3gp", "video/3gpp"},
        {".3gp2", "video/3gpp2"},
        {".3gpp", "video/3gpp"},
        {".7z", "application/x-7z-compressed"},
        {".aa", "audio/audible"},
        {".AAC", "audio/aac"},
        {".aaf", "application/octet-stream"},
        {".aax", "audio/vnd.audible.aax"},
        {".ac3", "audio/ac3"},
        {".aca", "application/octet-stream"},
        {".accda", "application/msaccess.addin"},
        {".accdb", "application/msaccess"},
        {".accdc", "application/msaccess.cab"},
        {".accde", "application/msaccess"},
        {".accdr", "application/msaccess.runtime"},
        {".accdt", "application/msaccess"},
        {".accdw", "application/msaccess.webapplication"},
        {".accft", "application/msaccess.ftemplate"},
        {".acx", "application/internet-property-stream"},
        {".AddIn", "text/xml"},
        {".ade", "application/msaccess"},
        {".adobebridge", "application/x-bridge-url"},
        {".adp", "application/msaccess"},
        {".ADT", "audio/vnd.dlna.adts"},
        {".ADTS", "audio/aac"},
        {".afm", "application/octet-stream"},
        {".ai", "application/postscript"},
        {".aif", "audio/x-aiff"},
        {".aifc", "audio/aiff"},
        {".aiff", "audio/aiff"},
        {".air", "application/vnd.Adobe.air-application-installer-package+Zip"},
        {".amc", "application/x-mpeg"},
        {".application", "application/x-ms-application"},
        {".art", "image/x-jg"},
        {".asa", "application/xml"},
        {".asax", "application/xml"},
        {".ascx", "application/xml"},
        {".asd", "application/octet-stream"},
        {".asf", "video/x-ms-asf"},
        {".ashx", "application/xml"},
        {".asi", "application/octet-stream"},
        {".asm", "text/plain"},
        {".asmx", "application/xml"},
        {".aspx", "application/xml"},
        {".asr", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".atom", "application/atom+xml"},
        {".au", "audio/basic"},
        {".avi", "video/x-msvideo"},
        {".axs", "application/olescript"},
        {".bas", "text/plain"},
        {".bcpio", "application/x-bcpio"},
        {".bin", "application/octet-stream"},
        {".bmp", "image/bmp"},
        {".c", "text/plain"},
        {".cab", "application/octet-stream"},
        {".caf", "audio/x-caf"},
        {".calx", "application/vnd.ms-office.calx"},
        {".cat", "application/vnd.ms-pki.seccat"},
        {".cc", "text/plain"},
        {".cd", "text/plain"},
        {".cdda", "audio/aiff"},
        {".cdf", "application/x-cdf"},
        {".cer", "application/x-x509-ca-cert"},
        {".chm", "application/octet-stream"},
        {".class", "application/x-Java-applet"},
        {".clp", "application/x-msclip"},
        {".cmx", "image/x-cmx"},
        {".cnf", "text/plain"},
        {".cod", "image/cis-cod"},
        {".config", "application/xml"},
        {".contact", "text/x-ms-contact"},
        {".coverage", "application/xml"},
        {".cpio", "application/x-cpio"},
        {".cpp", "text/plain"},
        {".crd", "application/x-mscardfile"},
        {".crl", "application/pkix-crl"},
        {".crt", "application/x-x509-ca-cert"},
        {".cs", "text/plain"},
        {".csdproj", "text/plain"},
        {".csh", "application/x-csh"},
        {".csproj", "text/plain"},
        {".css", "text/css"},
        {".csv", "text/csv"},
        {".cur", "application/octet-stream"},
        {".cxx", "text/plain"},
        {".dat", "application/octet-stream"},
        {".datasource", "application/xml"},
        {".dbproj", "text/plain"},
        {".dcr", "application/x-director"},
        {".def", "text/plain"},
        {".deploy", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dgml", "application/xml"},
        {".dib", "image/bmp"},
        {".dif", "video/x-dv"},
        {".dir", "application/x-director"},
        {".disco", "text/xml"},
        {".dll", "application/x-msdownload"},
        {".dll.config", "text/xml"},
        {".dlm", "text/dlm"},
        {".doc", "application/msword"},
        {".docm", "application/vnd.ms-Word.document.macroEnabled.12"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".dot", "application/msword"},
        {".dotm", "application/vnd.ms-Word.template.macroEnabled.12"},
        {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
        {".dsp", "application/octet-stream"},
        {".dsw", "text/plain"},
        {".dtd", "text/xml"},
        {".dtsConfig", "text/xml"},
        {".dv", "video/x-dv"},
        {".dvi", "application/x-dvi"},
        {".dwf", "drawing/x-dwf"},
        {".dwp", "application/octet-stream"},
        {".dxr", "application/x-director"},
        {".eml", "message/rfc822"},
        {".emz", "application/octet-stream"},
        {".eot", "application/octet-stream"},
        {".eps", "application/postscript"},
        {".etl", "application/etl"},
        {".etx", "text/x-setext"},
        {".evy", "application/envoy"},
        {".exe", "application/octet-stream"},
        {".exe.config", "text/xml"},
        {".fdf", "application/vnd.fdf"},
        {".fif", "application/fractals"},
        {".filters", "Application/xml"},
        {".fla", "application/octet-stream"},
        {".flr", "x-world/x-vrml"},
        {".flv", "video/x-flv"},
        {".fsscript", "application/fsharp-script"},
        {".fsx", "application/fsharp-script"},
        {".generictest", "application/xml"},
        {".gif", "image/gif"},
        {".group", "text/x-ms-group"},
        {".gsm", "audio/x-gsm"},
        {".gtar", "application/x-gtar"},
        {".gz", "application/x-gzip"},
        {".h", "text/plain"},
        {".hdf", "application/x-hdf"},
        {".hdml", "text/x-hdml"},
        {".hhc", "application/x-oleobject"},
        {".hhk", "application/octet-stream"},
        {".hhp", "application/octet-stream"},
        {".hlp", "application/winhlp"},
        {".hpp", "text/plain"},
        {".hqx", "application/mac-binhex40"},
        {".hta", "application/hta"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".htt", "text/webviewhtml"},
        {".hwp", "application/haansofthwp"},
        {".hxa", "application/xml"},
        {".hxc", "application/xml"},
        {".hxd", "application/octet-stream"},
        {".hxe", "application/xml"},
        {".hxf", "application/xml"},
        {".hxh", "application/octet-stream"},
        {".hxi", "application/octet-stream"},
        {".hxk", "application/xml"},
        {".hxq", "application/octet-stream"},
        {".hxr", "application/octet-stream"},
        {".hxs", "application/octet-stream"},
        {".hxt", "text/html"},
        {".hxv", "application/xml"},
        {".hxw", "application/octet-stream"},
        {".hxx", "text/plain"},
        {".i", "text/plain"},
        {".ico", "image/x-icon"},
        {".ics", "application/octet-stream"},
        {".idl", "text/plain"},
        {".ief", "image/ief"},
        {".iii", "application/x-iphone"},
        {".inc", "text/plain"},
        {".inf", "application/octet-stream"},
        {".inl", "text/plain"},
        {".ins", "application/x-internet-signup"},
        {".ipa", "application/x-iTunes-ipa"},
        {".ipg", "application/x-iTunes-ipg"},
        {".ipproj", "text/plain"},
        {".ipsw", "application/x-iTunes-ipsw"},
        {".iqy", "text/x-ms-iqy"},
        {".isp", "application/x-internet-signup"},
        {".ite", "application/x-iTunes-ite"},
        {".itlp", "application/x-iTunes-itlp"},
        {".itms", "application/x-iTunes-itms"},
        {".itpc", "application/x-iTunes-itpc"},
        {".IVF", "video/x-ivf"},
        {".jar", "application/Java-archive"},
        {".Java", "application/octet-stream"},
        {".jck", "application/liquidmotion"},
        {".jcz", "application/liquidmotion"},
        {".jfif", "image/pjpeg"},
        {".jnlp", "application/x-Java-jnlp-file"},
        {".jpb", "application/octet-stream"},
        {".jpe", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".json", "application/json"},
        {".jsx", "text/jscript"},
        {".jsxbin", "text/plain"},
        {".latex", "application/x-latex"},
        {".library-ms", "application/windows-library+xml"},
        {".lit", "application/x-ms-reader"},
        {".loadtest", "application/xml"},
        {".lpk", "application/octet-stream"},
        {".lsf", "video/x-la-asf"},
        {".lst", "text/plain"},
        {".lsx", "video/x-la-asf"},
        {".lzh", "application/octet-stream"},
        {".m13", "application/x-msmediaview"},
        {".m14", "application/x-msmediaview"},
        {".m1v", "video/mpeg"},
        {".m2t", "video/vnd.dlna.mpeg-tts"},
        {".m2ts", "video/vnd.dlna.mpeg-tts"},
        {".m2v", "video/mpeg"},
        {".m3u", "audio/x-mpegurl"},
        {".m3u8", "audio/x-mpegurl"},
        {".m4a", "audio/m4a"},
        {".m4b", "audio/m4b"},
        {".m4p", "audio/m4p"},
        {".m4r", "audio/x-m4r"},
        {".m4v", "video/x-m4v"},
        {".mac", "image/x-macpaint"},
        {".mak", "text/plain"},
        {".man", "application/x-troff-man"},
        {".manifest", "application/x-ms-manifest"},
        {".map", "text/plain"},
        {".master", "application/xml"},
        {".mda", "application/msaccess"},
        {".mdb", "application/x-msaccess"},
        {".mde", "application/msaccess"},
        {".mdp", "application/octet-stream"},
        {".me", "application/x-troff-me"},
        {".mfp", "application/x-shockwave-flash"},
        {".mht", "message/rfc822"},
        {".mhtml", "message/rfc822"},
        {".mid", "audio/mid"},
        {".midi", "audio/mid"},
        {".mix", "application/octet-stream"},
        {".mk", "text/plain"},
        {".mmf", "application/x-smaf"},
        {".mno", "text/xml"},
        {".mny", "application/x-msmoney"},
        {".mod", "video/mpeg"},
        {".mov", "video/quicktime"},
        {".movie", "video/x-sgi-movie"},
        {".mp2", "video/mpeg"},
        {".mp2v", "video/mpeg"},
        {".mp3", "audio/mpeg"},
        {".mp4", "video/mp4"},
        {".mp4v", "video/mp4"},
        {".mpa", "video/mpeg"},
        {".mpe", "video/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpf", "application/vnd.ms-mediapackage"},
        {".mpg", "video/mpeg"},
        {".mpp", "application/vnd.ms-project"},
        {".mpv2", "video/mpeg"},
        {".mqv", "video/quicktime"},
        {".ms", "application/x-troff-ms"},
        {".msi", "application/octet-stream"},
        {".mso", "application/octet-stream"},
        {".mts", "video/vnd.dlna.mpeg-tts"},
        {".mtx", "application/xml"},
        {".mvb", "application/x-msmediaview"},
        {".mvc", "application/x-miva-compiled"},
        {".mxp", "application/x-mmxp"},
        {".nc", "application/x-netcdf"},
        {".nsc", "video/x-ms-asf"},
        {".nws", "message/rfc822"},
        {".ocx", "application/octet-stream"},
        {".oda", "application/oda"},
        {".odc", "text/x-ms-odc"},
        {".odh", "text/plain"},
        {".odl", "text/plain"},
        {".odp", "application/vnd.oasis.opendocument.presentation"},
        {".ods", "application/oleobject"},
        {".odt", "application/vnd.oasis.opendocument.text"},
        {".one", "application/onenote"},
        {".onea", "application/onenote"},
        {".onepkg", "application/onenote"},
        {".onetmp", "application/onenote"},
        {".onetoc", "application/onenote"},
        {".onetoc2", "application/onenote"},
        {".orderedtest", "application/xml"},
        {".osdx", "application/opensearchdescription+xml"},
        {".p10", "application/pkcs10"},
        {".p12", "application/x-pkcs12"},
        {".p7b", "application/x-pkcs7-certificates"},
        {".p7c", "application/pkcs7-mime"},
        {".p7m", "application/pkcs7-mime"},
        {".p7r", "application/x-pkcs7-certreqresp"},
        {".p7s", "application/pkcs7-signature"},
        {".pbm", "image/x-portable-bitmap"},
        {".pcast", "application/x-podcast"},
        {".pct", "image/pict"},
        {".pcx", "application/octet-stream"},
        {".pcz", "application/octet-stream"},
        {".pdf", "application/pdf"},
        {".pfb", "application/octet-stream"},
        {".pfm", "application/octet-stream"},
        {".pfx", "application/x-pkcs12"},
        {".pgm", "image/x-portable-graymap"},
        {".pic", "image/pict"},
        {".pict", "image/pict"},
        {".pkgdef", "text/plain"},
        {".pkgundef", "text/plain"},
        {".pko", "application/vnd.ms-pki.pko"},
        {".pls", "audio/scpls"},
        {".pma", "application/x-perfmon"},
        {".pmc", "application/x-perfmon"},
        {".pml", "application/x-perfmon"},
        {".pmr", "application/x-perfmon"},
        {".pmw", "application/x-perfmon"},
        {".png", "image/png"},
        {".pnm", "image/x-portable-anymap"},
        {".pnt", "image/x-macpaint"},
        {".pntg", "image/x-macpaint"},
        {".pnz", "image/png"},
        {".pot", "application/vnd.ms-PowerPoint"},
        {".potm", "application/vnd.ms-PowerPoint.template.macroEnabled.12"},
        {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
        {".ppa", "application/vnd.ms-PowerPoint"},
        {".ppam", "application/vnd.ms-PowerPoint.addin.macroEnabled.12"},
        {".ppm", "image/x-portable-pixmap"},
        {".pps", "application/vnd.ms-PowerPoint"},
        {".ppsm", "application/vnd.ms-PowerPoint.slideshow.macroEnabled.12"},
        {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
        {".ppt", "application/vnd.ms-PowerPoint"},
        {".pptm", "application/vnd.ms-PowerPoint.presentation.macroEnabled.12"},
        {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
        {".prf", "application/pics-rules"},
        {".prm", "application/octet-stream"},
        {".prx", "application/octet-stream"},
        {".ps", "application/postscript"},
        {".psc1", "application/PowerShell"},
        {".psd", "application/octet-stream"},
        {".psess", "application/xml"},
        {".psm", "application/octet-stream"},
        {".psp", "application/octet-stream"},
        {".pub", "application/x-mspublisher"},
        {".pwz", "application/vnd.ms-PowerPoint"},
        {".qht", "text/x-html-insertion"},
        {".qhtm", "text/x-html-insertion"},
        {".qt", "video/quicktime"},
        {".qti", "image/x-quicktime"},
        {".qtif", "image/x-quicktime"},
        {".qtl", "application/x-quicktimeplayer"},
        {".qxd", "application/octet-stream"},
        {".ra", "audio/x-pn-realaudio"},
        {".ram", "audio/x-pn-realaudio"},
        {".rar", "application/octet-stream"},
        {".ras", "image/x-cmu-raster"},
        {".rat", "application/rat-file"},
        {".rc", "text/plain"},
        {".rc2", "text/plain"},
        {".rct", "text/plain"},
        {".rdlc", "application/xml"},
        {".resx", "application/xml"},
        {".rf", "image/vnd.rn-realflash"},
        {".rgb", "image/x-rgb"},
        {".rgs", "text/plain"},
        {".rm", "application/vnd.rn-realmedia"},
        {".rmi", "audio/mid"},
        {".rmp", "application/vnd.rn-rn_music_package"},
        {".roff", "application/x-troff"},
        {".rpm", "audio/x-pn-realaudio-plugin"},
        {".rqy", "text/x-ms-rqy"},
        {".rtf", "application/rtf"},
        {".rtx", "text/richtext"},
        {".ruleset", "application/xml"},
        {".s", "text/plain"},
        {".safariextz", "application/x-safari-safariextz"},
        {".scd", "application/x-msschedule"},
        {".sct", "text/scriptlet"},
        {".sd2", "audio/x-sd2"},
        {".sdp", "application/sdp"},
        {".sea", "application/octet-stream"},
        {".searchConnector-ms", "application/windows-search-connector+xml"},
        {".setpay", "application/set-payment-initiation"},
        {".setreg", "application/set-registration-initiation"},
        {".settings", "application/xml"},
        {".sgimb", "application/x-sgimb"},
        {".sgml", "text/sgml"},
        {".sh", "application/x-sh"},
        {".shar", "application/x-shar"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".sitemap", "application/xml"},
        {".skin", "application/xml"},
        {".sldm", "application/vnd.ms-PowerPoint.slide.macroEnabled.12"},
        {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
        {".slk", "application/vnd.ms-Excel"},
        {".sln", "text/plain"},
        {".slupkg-ms", "application/x-ms-license"},
        {".smd", "audio/x-smd"},
        {".smi", "application/octet-stream"},
        {".smx", "audio/x-smd"},
        {".smz", "audio/x-smd"},
        {".snd", "audio/basic"},
        {".snippet", "application/xml"},
        {".snp", "application/octet-stream"},
        {".sol", "text/plain"},
        {".sor", "text/plain"},
        {".spc", "application/x-pkcs7-certificates"},
        {".spl", "application/futuresplash"},
        {".src", "application/x-wais-source"},
        {".srf", "text/plain"},
        {".SSISDeploymentManifest", "text/xml"},
        {".ssm", "application/streamingmedia"},
        {".sst", "application/vnd.ms-pki.certstore"},
        {".stl", "application/vnd.ms-pki.stl"},
        {".sv4cpio", "application/x-sv4cpio"},
        {".sv4crc", "application/x-sv4crc"},
        {".svc", "application/xml"},
        {".swf", "application/x-shockwave-flash"},
        {".t", "application/x-troff"},
        {".tar", "application/x-tar"},
        {".tcl", "application/x-tcl"},
        {".testrunconfig", "application/xml"},
        {".testsettings", "application/xml"},
        {".tex", "application/x-tex"},
        {".texi", "application/x-texinfo"},
        {".texinfo", "application/x-texinfo"},
        {".tgz", "application/x-compressed"},
        {".thmx", "application/vnd.ms-officetheme"},
        {".thn", "application/octet-stream"},
        {".tif", "image/tiff"},
        {".tiff", "image/tiff"},
        {".tlh", "text/plain"},
        {".tli", "text/plain"},
        {".toc", "application/octet-stream"},
        {".tr", "application/x-troff"},
        {".trm", "application/x-msterminal"},
        {".trx", "application/xml"},
        {".ts", "video/vnd.dlna.mpeg-tts"},
        {".tsv", "text/tab-separated-values"},
        {".ttf", "application/octet-stream"},
        {".tts", "video/vnd.dlna.mpeg-tts"},
        {".txt", "text/plain"},
        {".u32", "application/octet-stream"},
        {".uls", "text/iuls"},
        {".user", "text/plain"},
        {".ustar", "application/x-ustar"},
        {".vb", "text/plain"},
        {".vbdproj", "text/plain"},
        {".vbk", "video/mpeg"},
        {".vbproj", "text/plain"},
        {".vbs", "text/vbscript"},
        {".vcf", "text/x-vcard"},
        {".vcproj", "Application/xml"},
        {".vcs", "text/plain"},
        {".vcxproj", "Application/xml"},
        {".vddproj", "text/plain"},
        {".vdp", "text/plain"},
        {".vdproj", "text/plain"},
        {".vdx", "application/vnd.ms-visio.viewer"},
        {".vml", "text/xml"},
        {".vscontent", "application/xml"},
        {".vsct", "text/xml"},
        {".vsd", "application/vnd.visio"},
        {".vsi", "application/ms-vsi"},
        {".vsix", "application/vsix"},
        {".vsixlangpack", "text/xml"},
        {".vsixmanifest", "text/xml"},
        {".vsmdi", "application/xml"},
        {".vspscc", "text/plain"},
        {".vss", "application/vnd.visio"},
        {".vsscc", "text/plain"},
        {".vssettings", "text/xml"},
        {".vssscc", "text/plain"},
        {".vst", "application/vnd.visio"},
        {".vstemplate", "text/xml"},
        {".vsto", "application/x-ms-vsto"},
        {".vsw", "application/vnd.visio"},
        {".vsx", "application/vnd.visio"},
        {".vtx", "application/vnd.visio"},
        {".wav", "audio/wav"},
        {".wave", "audio/wav"},
        {".wax", "audio/x-ms-wax"},
        {".wbk", "application/msword"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wcm", "application/vnd.ms-works"},
        {".wdb", "application/vnd.ms-works"},
        {".wdp", "image/vnd.ms-photo"},
        {".webarchive", "application/x-safari-webarchive"},
        {".webtest", "application/xml"},
        {".wiq", "application/xml"},
        {".wiz", "application/msword"},
        {".wks", "application/vnd.ms-works"},
        {".WLMP", "application/wlmoviemaker"},
        {".wlpginstall", "application/x-wlpg-detect"},
        {".wlpginstall3", "application/x-wlpg3-detect"},
        {".wm", "video/x-ms-wm"},
        {".wma", "audio/x-ms-wma"},
        {".wmd", "application/x-ms-wmd"},
        {".wmf", "application/x-msmetafile"},
        {".wml", "text/vnd.wap.wml"},
        {".wmlc", "application/vnd.wap.wmlc"},
        {".wmls", "text/vnd.wap.wmlscript"},
        {".wmlsc", "application/vnd.wap.wmlscriptc"},
        {".wmp", "video/x-ms-wmp"},
        {".wmv", "video/x-ms-wmv"},
        {".wmx", "video/x-ms-wmx"},
        {".wmz", "application/x-ms-wmz"},
        {".wpl", "application/vnd.ms-wpl"},
        {".wps", "application/vnd.ms-works"},
        {".wri", "application/x-mswrite"},
        {".wrl", "x-world/x-vrml"},
        {".wrz", "x-world/x-vrml"},
        {".wsc", "text/scriptlet"},
        {".wsdl", "text/xml"},
        {".wvx", "video/x-ms-wvx"},
        {".x", "application/directx"},
        {".xaf", "x-world/x-vrml"},
        {".xaml", "application/xaml+xml"},
        {".xap", "application/x-silverlight-app"},
        {".xbap", "application/x-ms-xbap"},
        {".xbm", "image/x-xbitmap"},
        {".xdr", "text/plain"},
        {".xht", "application/xhtml+xml"},
        {".xhtml", "application/xhtml+xml"},
        {".xla", "application/vnd.ms-Excel"},
        {".xlam", "application/vnd.ms-Excel.addin.macroEnabled.12"},
        {".xlc", "application/vnd.ms-Excel"},
        {".xld", "application/vnd.ms-Excel"},
        {".xlk", "application/vnd.ms-Excel"},
        {".xll", "application/vnd.ms-Excel"},
        {".xlm", "application/vnd.ms-Excel"},
        {".xls", "application/vnd.ms-Excel"},
        {".xlsb", "application/vnd.ms-Excel.sheet.binary.macroEnabled.12"},
        {".xlsm", "application/vnd.ms-Excel.sheet.macroEnabled.12"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".xlt", "application/vnd.ms-Excel"},
        {".xltm", "application/vnd.ms-Excel.template.macroEnabled.12"},
        {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
        {".xlw", "application/vnd.ms-Excel"},
        {".xml", "text/xml"},
        {".xmta", "application/xml"},
        {".xof", "x-world/x-vrml"},
        {".XOML", "text/plain"},
        {".xpm", "image/x-xpixmap"},
        {".xps", "application/vnd.ms-xpsdocument"},
        {".xrm-ms", "text/xml"},
        {".xsc", "application/xml"},
        {".xsd", "text/xml"},
        {".xsf", "text/xml"},
        {".xsl", "text/xml"},
        {".xslt", "text/xml"},
        {".xsn", "application/octet-stream"},
        {".xss", "application/xml"},
        {".xtp", "application/octet-stream"},
        {".xwd", "image/x-xwindowdump"},
        {".z", "application/x-compress"},
        {".Zip", "application/x-Zip-compressed"},
        #endregion

        };

        public string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException("extension");
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime;

            return _mappings.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }

        #endregion


    }
}