using HACCP.Attribute;
using HACCP.Libs;
using HACCP.Models.QualityControl;
using HACCP.Services.Comm;
using HACCP.Services.QualityControl;
using HACCP.Services.SysCom;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class QualityControlController : Controller
    {
        #region 공통 서비스 설정
        private static readonly ILog log = LogManager.GetLogger(typeof(EquipmentController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private CodeHelpService codeHelpService = new CodeHelpService();
        #endregion

        #region 시험접수(Multi)
        private TestReceiptMultiService testReceiptMultiService = new TestReceiptMultiService();
                
        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult TestReceiptMulti(TestReceiptMulti.SrchParam srch)
        {
            // 1. 공통코드 List
            DataTable le_test_type = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "le_test_type");
            DataTable le_test_status = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "le_test_status");

            DataTable le_item_form_cd = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "le_item_form_cd");

            //DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");
            //DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");
                      
            // 2. 그리드 List            
            DataTable mainGrid = testReceiptMultiService.SelectMain(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식

            // 3.3. 코드성데이터            
            ViewBag.le_test_type = le_test_type;
            ViewBag.le_test_status = le_test_status;
            ViewBag.le_item_form_cd = le_item_form_cd;

            //ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            //ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));


            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 검색 : 로드후
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestReceiptMultiSelect(TestReceiptMulti.SrchParam srch)
        {
            DataTable mainGrid = testReceiptMultiService.SelectMain(srch);            

            return Json(Public_Function.DataTableToJSON(mainGrid));
        }

        /// <summary>
        /// 채번 : afterservice_no 리턴
        /// </summary>
        /// <param name="edate"></param>
        /// <returns></returns>
        [HttpPost]
        public string TestReceiptMultiGetSeqNo(string edate)
        {
            return testReceiptMultiService.GetSeqNo(edate);
        }

        /// <summary>
        /// 트랜잭션(=CRUD) 처리
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestReceiptMultiTRX([JsonBinder] List<TestReceiptMulti> dto, TestReceiptMulti.SrchParam srch)
        //public string TestReceiptMultiTRX(List<TestReceiptMulti> dto, TestReceiptMulti.SrchParam srch)
        {
            string res = testReceiptMultiService.DataTrx(dto, srch);

            if(res.Length ==0)
            {
                res = "실패 되었습니다. 관리자에게 문의하세요";
            }
            var resJson = "{ \"message\": \"" + res + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 삭제(취소) 가능한 상태인지 체크한다
        /// </summary>
        /// <returns>가능여부 리턴(Y/N)</returns>
        [HttpPost]
        public string TestReceiptMultiESStatusCheck([JsonBinder] TestReceiptMulti dto)
        {
            string res;
            bool chkFlag = testReceiptMultiService.ESStatusCheck(dto);

            res = chkFlag ? "Y" : "N";
            return res;
        }

        /// <summary>
        /// 파일조회
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult TestReceiptMultiSelectFile(string testcontrol_id)
        {
            DataTable table = testReceiptMultiService.SelectFile(testcontrol_id);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 파일업로드
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TestReceiptMultiUploadFile(string testcontrol_id, string name)
        {
            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;

                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    testReceiptMultiService.uploadFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), fileName, contentType, testcontrol_id);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 파일다운로드
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestReceiptMultiDownloadFile(string testcontrol_id, string file_id)
        {
            EquipmentService equipmentService = new EquipmentService();

            DataTable attachmentFileData = testReceiptMultiService.DownloadFile(testcontrol_id, file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0]["file_image"]
                , mimeType
                , attachmentFileData.Rows[0]["doc_name"].ToString()
                );
        }

        /// <summary>
        /// 파일삭제
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpPost]
        public string TestReceiptMultiDeleteFile(string testcontrol_id, string file_id)
        {
            string res = testReceiptMultiService.DeleteFile(testcontrol_id, file_id);
            return res;
        }

        #endregion

        #region 시험 지시
        private TestScheduleService testScheduleService = new TestScheduleService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult TestSchedule(TestSchedule.SrchParam srch, TestSchedule.SrchParamItem srchItem)
        {
            // 1. 공통코드 List
            DataTable le_s_test_type = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "le_s_test_type");
            //DataTable le_emp_option = selectBoxService.GetSelectBox("공통코드", "N", "QC014", "le_emp_option");
            //DataTable le_date_option = selectBoxService.GetSelectBox("공통코드", "N", "QC015", "le_date_option");
            //DataTable le_calc_option = selectBoxService.GetSelectBox("공통코드", "N", "QC016", "le_calc_option");
            DataTable le_item_form_cd = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "le_item_form_cd");
            DataTable le_s_testitem_type = selectBoxService.GetSelectBox("공통코드", "S", "QC003", "le_s_testitem_type");

            //DataTable grid_lookup_gubun = selectBoxService.GetSelectBox("공통코드", "S", "QC005", "grid_lookup_gubun");
            //DataTable grid_lookup_unit = selectBoxService.GetSelectBox("공통코드", "S", "QC020", "grid_lookup_unit");

            //DataTable le_emp_cd = selectBoxService.GetSelectBox("시험자", "N", "","le_emp_cd");
            //DataTable le_emp_cd2 = selectBoxService.GetSelectBox("시험자", "N", "", "le_emp_cd2");

            //DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");
            DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");

            // 2. 그리드 List            
            DataTable mainGrid = testScheduleService.SelectMain(srch);
            DataTable itemGrid = testScheduleService.SelectTestItemMaster(srchItem);
            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;
            ViewData["srchItem"] = srchItem;

            // 3.2. 좌측Grid
            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식
            ViewBag.itemGrid = Json(Public_Function.DataTableToJSON(itemGrid) );   // 기존 json라이브러리 방식
            

            // 3.3. 코드성데이터            
            ViewBag.le_s_test_type = le_s_test_type;
            ViewBag.le_item_form_cd = le_item_form_cd;
            ViewBag.le_s_testitem_type = le_s_testitem_type;


            //ViewBag.grid_lookup_gubun = Json(Public_Function.DataTableToJSON(grid_lookup_gubun));
            //ViewBag.grid_lookup_unit = Json(Public_Function.DataTableToJSON(grid_lookup_unit));
            //ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 검색 : 로드후
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestScheduleSelect(TestSchedule.SrchParam srch)
        {
            srch.Gubun = string.IsNullOrEmpty(srch.Gubun) ? "S" : srch.Gubun;
            DataTable table = testScheduleService.SelectMain(srch);
            return Json(Public_Function.DataTableToJSON(table));
        }

        /// <summary>
        /// 항목조정 아이템 조회
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestScheduleItemMasterSelect(TestSchedule.SrchParamItem srch)
        {
            srch.Gubun = string.IsNullOrEmpty(srch.Gubun) ? "S4" : srch.Gubun;

            DataTable table = testScheduleService.SelectTestItemMaster(srch);
            return Json(Public_Function.DataTableToJSON(table));
        }

        /// <summary>
        /// 시험자 조회
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestScheduleTesterSelect()
        {
            DataTable le_emp_cd = selectBoxService.GetSelectBox("시험자", "N", "", "le_emp_cd");
            DataTable table = testScheduleService.SelectTester(le_emp_cd);

            return Json(Public_Function.DataTableToJSON(table));
        }

        /// <summary>
        /// 파일조회
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult TestScheduleSelectFile(string testcontrol_id)
        {
            DataTable table = testScheduleService.SelectFile(testcontrol_id);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 파일업로드
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TestScheduleUploadFile(string testcontrol_id, string name)
        {
            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;

                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    testScheduleService.uploadFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), fileName, contentType, testcontrol_id);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 파일다운로드
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestScheduleDownloadFile(string testcontrol_id, string file_id)
        {
            EquipmentService equipmentService = new EquipmentService();

            DataTable attachmentFileData = testScheduleService.DownloadFile(testcontrol_id, file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0]["file_image"]
                , mimeType
                , attachmentFileData.Rows[0]["doc_name"].ToString()
                );
        }

        /// <summary>
        /// 파일삭제
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpPost]
        public string TestScheduleDeleteFile(string testcontrol_id, string file_id)
        {
            string res = testScheduleService.DeleteFile(testcontrol_id, file_id);
            return res;
        }

        /// <summary>
        /// 채번 : afterservice_no 리턴
        /// </summary>
        /// <param name="edate"></param>
        /// <returns></returns>
        //[HttpPost]
        //public string TestReceiptMultiGetSeqNo(string edate)
        //{
        //    return testReceiptMultiService.GetSeqNo(edate);
        //}

        /// <summary>
        /// 트랜잭션(=CRUD) 처리
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestScheduleDetailTRX([JsonBinder] List<TestSchedule.TestScheduleDetailArrange> dto, string testcontrol_id, string instruction_no, string picking_ordered_emp_cd, string instruction_date)
        //public string TestScheduleTRX(List<TestSchedule> dto, TestSchedule.SrchParam srch)
        {
            string res = testScheduleService.DataScheDuleDetailTrx(dto, testcontrol_id, instruction_no, picking_ordered_emp_cd, instruction_date);

            if (res.Length == 0)
            {
                res = "실패 되었습니다. 관리자에게 문의하세요";
            }
            var resJson = "{ \"message\": \"" + res + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 삭제(취소) 가능한 상태인지 체크한다
        /// </summary>
        /// <returns>가능여부 리턴(Y/N)</returns>
        [HttpPost]
        public string TestScheduleESStatusCheck([JsonBinder] TestSchedule dto)
        {
            string res;
            bool chkFlag = testScheduleService.ESStatusCheck(dto);

            res = chkFlag ? "Y" : "N";
            return res;
        }

        /// <summary>
        /// 구분에 따라 시험항목을 추가후 그결과를 리턴한다.
        /// </summary>
        /// <returns>가능여부 리턴(Y/N)</returns>
        [HttpPost]
        public string TestScheduleAddTestitem(string addType, string testcontrol_id, string teststandardmaster_id, string testitem_cd)
        {
            string res;
            bool chkFlag = testScheduleService.Add_Testitem(addType, testcontrol_id, teststandardmaster_id, testitem_cd);
            res = chkFlag ? "Y" : "N";
            return res;
        }


        [HttpGet]
        public JsonResult SelectTestScheduleSignData(string testcontrol_id, string test_type)
        {
            DataTable dt = testScheduleService.SelectTestScheduleSignData(testcontrol_id, test_type);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult CheckTestScheduleTestMaster(string testcontrol_id)
        {
            string res = testScheduleService.CheckTestScheduleTestMaster(testcontrol_id);

            return Json( new { testmaster_id = res }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult TestScheduleIssueTestReport(string testcontrol_id)
        {
            string res = testScheduleService.TestScheduleIssueTestReport(testcontrol_id);

            return Json(new { message = res });
        }

        [HttpPost]
        public ActionResult TestScheduleSaveCheckInfo(string testcontrol_id, string instruction_date)
        {
            string res = testScheduleService.TestScheduleSaveCheckInfo(testcontrol_id, instruction_date);

            return Json(new { message = res });
        }

        [HttpPost]
        public ActionResult TestScheduleSignTRX(string gubun, string testcontrol_id, string process_kind, string test_type
            , string emp_cd, string sign_set_dt_id, string validation_type, string representative_yn)
        {
            string res = testScheduleService.TestScheduleSignTRX(gubun, testcontrol_id, process_kind, test_type, emp_cd, sign_set_dt_id, validation_type, representative_yn);

            return Json(new { message = res });
        }

        [HttpGet]
        public ActionResult TestScheduleGetLastSignEmpCheck(string testcontrol_id, string test_type)
        {
            string res = testScheduleService.TestScheduleGetLastSignEmpCheck(testcontrol_id, test_type);

            return Json(new { res = res }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TestScheduleDespatchCancel(TestSchedule dto)
        {
            string res = testScheduleService.TestScheduleDespatchCancel(dto);

            return Json(new { message = res });
        }

        [HttpPost]
        public ActionResult TestScheduleItemDelete(string testcontrol_id, string teststandardmaster_id)
        {
            string res = testScheduleService.TestScheduleItemDelete(testcontrol_id, teststandardmaster_id);

            return Json(new { message = res });
        }

        #endregion

        #region 검체 채취 입력
        private PickingCompleteEService pickingCompleteEService = new PickingCompleteEService();

        public ActionResult PickingCompleteE(PickingCompleteE.SrchParam srchParam)
        {
            //DataTable venderBuy = codeHelpService.GetCode(CodeHelpType.vender_buy, HttpContext.Session["plantCD"].ToString(), "");
            //DataTable itemMaker = codeHelpService.GetCode(CodeHelpType.item_maker, HttpContext.Session["plantCD"].ToString(), "");
            DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");
            DataTable workRoomPopupData = codeHelpService.GetCode(CodeHelpType.workroom, "", "");

            //ViewBag.venderBuy = Json(Public_Function.DataTableToJSON(venderBuy));
            //ViewBag.itemMaker = Json(Public_Function.DataTableToJSON(itemMaker));
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.workRoomPopupData = Json(Public_Function.DataTableToJSON(workRoomPopupData));

            return View();
        }

        public JsonResult PickingCompleteESelect(PickingCompleteE.SrchParam srchParam)
        {
            DataTable testRequestData = pickingCompleteEService.PickingCompleteESelect(srchParam);

            return Json(Public_Function.DataTableToJSON(testRequestData));
        }

        [HttpGet]
        public JsonResult PickingCompleteESelectDetail(string testcontrol_id, string test_type, string process_kind)
        {
            DataTable testRequestSignData = pickingCompleteEService.PickingCompleteESelectSign(testcontrol_id, test_type, process_kind);
            DataTable testRequestFileData = pickingCompleteEService.PickingCompleteESelectFile(testcontrol_id);

            string[] jsonArr = new string[2];

            jsonArr[0] = Public_Function.DataTableToJSON(testRequestSignData);
            jsonArr[1] = Public_Function.DataTableToJSON(testRequestFileData);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PickingCompleteESelectTestItemPopup(string test_type)
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable testItem = codeHelpService.GetCode(CodeHelpType.item_testmaster, HttpContext.Session["plantCD"].ToString(), test_type, "");

            return Json(Public_Function.DataTableToJSON(testItem), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string PickingCompleteETRX(PickingCompleteE dto)
        {
            string res = pickingCompleteEService.PickingCompleteETRX(dto);

            return res;
        }

        [HttpPost]
        public string PickingCompleteELabelCnt(string testcontrol_id, string test_type, string report_cd, string print_num)
        {
            string res = pickingCompleteEService.PickingCompleteELabelCnt(testcontrol_id, test_type, report_cd, print_num);

            return res;
        }

        [HttpGet]
        public JsonResult PickingCompleteEPackSamplingSelect(string testcontrol_id)
        {
            DataTable testItem = pickingCompleteEService.PickingCompleteEPackSamplingSelect(testcontrol_id);

            return Json(Public_Function.DataTableToJSON(testItem), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string PickingCompleteEPackSamplingSave([JsonBinder]List<PickingCompleteE.PackSampling> packSampling, string testcontrol_id)
        {
            string res = pickingCompleteEService.PickingCompleteEPackSamplingSave(packSampling, testcontrol_id);

            return res;
        }

        #endregion

        #region 시험 성적서 작성
        private TestItemResultJudgementService testItemResultJudgementService = new TestItemResultJudgementService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult TestItemResultJudgement(TestItemResultJudgement.SrchParam srch)
        {
            // 1. 공통코드 List
            DataTable le_s_test_type = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "le_s_test_type");
            DataTable rpluE_item_form = selectBoxService.GetSelectBox("공통코드", "S", "CM083", "rpluE_item_form");
            DataTable le_item_form_cd = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "le_item_form_cd");
            DataTable gcle_testitem_result_yn = selectBoxService.GetSelectBox("공통코드", "D", "QC071", "gcle_testitem_result_yn");
            
            //DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");
            DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");

            // 2. 그리드 List            
            DataTable mainGrid = testItemResultJudgementService.SelectMain(srch); 
            
            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;            

            // 3.2. 좌측Grid
            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식            

            // 3.3. 코드성데이터            
            ViewBag.le_s_test_type = le_s_test_type;
            ViewBag.rpluE_item_form = rpluE_item_form;
            ViewBag.le_item_form_cd = le_item_form_cd;
            ViewBag.gcle_testitem_result_yn = gcle_testitem_result_yn;

            //ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 검색 : 로드후
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestItemResultJudgementSelect(TestItemResultJudgement.SrchParam srch)
        {
            srch.Gubun = string.IsNullOrEmpty(srch.Gubun) ? "S" : srch.Gubun;
            DataTable table = testItemResultJudgementService.SelectMain(srch);
            return Json(Public_Function.DataTableToJSON(table));
        }

        /// <summary>
        /// 삭제(취소) 가능한 상태인지 체크한다
        /// </summary>
        /// <returns>가능여부 리턴(Y/N)</returns>
        [HttpPost]
        public string TestItemResultJudgementSamplingCheck([JsonBinder] TestItemResultJudgement dto)
        {
            string res;
            bool chkFlag = testItemResultJudgementService.Sampling_check(dto);

            res = chkFlag ? "Y" : "N";
            return res;
        }

        /// <summary>
        /// 트랜잭션(=CRUD) 처리 : 수정
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>        
        /// <returns></returns>
        //[HttpPost]
        //public JsonResult TestScheduleDetailTRX([JsonBinder] List<TestSchedule.TestScheduleDetailArrange> dto, string testcontrol_id, string instruction_no, string picking_ordered_emp_cd, string instruction_date)
        public JsonResult TestItemResultJudgementMultiTrx([JsonBinder] List<TestItemResultJudgement.TestItemResultJudgementDetailArrange> dto)
        {
            string res = testItemResultJudgementService.MultiTrx(dto);

            if (res.Length == 0)
            {
                res = "실패 되었습니다. 관리자에게 문의하세요";
            }
            var resJson = "{ \"message\": \"" + res + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 트랜잭션 처리 : 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public JsonResult TestItemResultJudgementDeleteTrx(TestItemResultJudgement.SrchParam dto)
        {
            string res = testItemResultJudgementService.DeleteTrx(dto);
            if (res.Length == 0)
            {
                res = "실패 되었습니다. 관리자에게 문의하세요";
            }
            var resJson = "{ \"message\": \"" + res + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 파일조회
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult TestItemResultJudgementSelectFile(string testcontrol_id)
        {
            DataTable table = testItemResultJudgementService.SelectFile(testcontrol_id);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 파일업로드
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TestItemResultJudgementUploadFile(string testcontrol_id, string name)
        {
            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;

                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    testItemResultJudgementService.uploadFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), fileName, contentType, testcontrol_id);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 파일다운로드
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestItemResultJudgementDownloadFile(string testcontrol_id, string file_id)
        {
            EquipmentService equipmentService = new EquipmentService();

            DataTable attachmentFileData = testItemResultJudgementService.DownloadFile(testcontrol_id, file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0]["file_image"]
                , mimeType
                , attachmentFileData.Rows[0]["doc_name"].ToString()
                );
        }

        /// <summary>
        /// 파일삭제
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpPost]
        public string TestItemResultJudgementDeleteFile(string testcontrol_id, string file_id)
        {
            string res = testItemResultJudgementService.DeleteFile(testcontrol_id, file_id);
            return res;
        }

        [HttpGet]
        public string TestItemResultJudgementCheckResult(string testcontrol_id, string teststandardmaster_id, string testitem_result)
        {
            string res = testItemResultJudgementService.TestItemResultJudgementCheckResult(testcontrol_id, teststandardmaster_id, testitem_result);
            return res;
        }

        [HttpGet]
        public ActionResult TestItemResultJudgementCheckTest(string testcontrol_id, string test_type)
        {
            string res = testItemResultJudgementService.TestItemResultJudgementCheckTest(testcontrol_id, "C1");
            bool checktest = false;

            if (res == "1")
            {
                string message2 = testItemResultJudgementService.TestItemResultJudgementCheckTest(testcontrol_id, "C2");
                if (message2 == "1")
                {
                    string message3 = testItemResultJudgementService.TestItemResultJudgementUpdateCheck(testcontrol_id, test_type);
                    checktest = true;
                }
                else
                {
                    string message3 = testItemResultJudgementService.TestItemResultJudgementCheckTest(testcontrol_id, "UpdateTestStatus");
                    checktest = true;
                }
            }

            return Json( new { checktest  = checktest});
        }

        [HttpGet]
        public ActionResult TestItemResultJudgementCheckTestStandard(string testcontrol_id, string test_type)
        {
            string res = testItemResultJudgementService.TestItemResultJudgementCheckTest(testcontrol_id, "CheckTestStandard1");
            bool checktest = false;

            if (res == "1")
            {
                string message2 = testItemResultJudgementService.TestItemResultJudgementCheckTest(testcontrol_id, "CheckTestStandard2");
                if (message2 == "1")
                {
                    string message3 = testItemResultJudgementService.TestItemResultJudgementUpdateCheck(testcontrol_id, test_type);
                    checktest = true;
                }
                else
                {
                    string message3 = testItemResultJudgementService.TestItemResultJudgementCheckTest(testcontrol_id, "UpdateTestStatus");
                    checktest = true;
                }
            }

            return Json(new { checktest = checktest });
        }

        [HttpPost]
        public JsonResult TestItemResultJudgementCancelResult(string testcontrol_id, string teststandardmaster_id)
        {
            string res = testItemResultJudgementService.TestItemResultJudgementCancelResult(testcontrol_id, teststandardmaster_id);

            return Json(new { message = res });
        }

        [HttpPost]
        public JsonResult TestItemResultJudgementChangeStatus(TestItemResultJudgement.SrchParam dto)
        {
            string res = testItemResultJudgementService.DeleteTrx(dto);

            return Json(new { message = res });
        }

        #endregion

        #region 시험 성적서 확인
        private TestCheckEService testCheckEService = new TestCheckEService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult TestCheckE(TestCheckE.SrchParam srch)
        {
            // 1. 공통코드 List
            //DataTable le_testitem_nm = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "le_testitem_nm");
            //DataTable replue_test_status = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "replue_test_status");
            //DataTable le_item_form_cd = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "le_item_form_cd");

            //DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");
            //DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");

            // 2. 그리드 List            
            //DataTable mainGrid = testCheckEService.Select(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            //ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식            

            // 3.3. 코드성데이터            
            //ViewBag.le_testitem_nm = le_testitem_nm;
            //ViewBag.replue_test_status = replue_test_status;
            //ViewBag.le_item_form_cd = le_item_form_cd;

            //ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            //ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 그리드 조회(Main)
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult TestCheckESelect(TestCheckE.SrchParam srch)
        {
            srch.Gubun = "S";
            DataTable table = testCheckEService.Select(srch);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 그리드조회(detail)
        /// </summary>
        /// <param name="srch"></param>
        /// <param name="dto"></param>
        /// <param name="process_kind"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult TestCheckESelectDetail([JsonBinder] TestCheckE dto, string process_kind )
        {
            // 구분 설정
            TestCheckE.SrchParam srch = new TestCheckE.SrchParam();
            srch.Gubun = "S1";

            // detail data
            DataTable detailTable = testCheckEService.SelectDetail(srch, dto);

            // sign data
            DataTable signTable = testCheckEService.SelectSign(dto.testcontrol_id.ToString(), dto.test_type, process_kind );
            
            // File data
            //DataTable fileTable = testCheckEService.SelectFile(dto.testcontrol_id.ToString());

            string[] jsonArr = new string[2];
            jsonArr[0] = Public_Function.DataTableToJSON(detailTable);
            jsonArr[1] = Public_Function.DataTableToJSON(signTable);
            //jsonArr[2] = Public_Function.DataTableToJSON(fileTable);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 파일조회
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult TestCheckESelectFile(string testcontrol_id, string teststandardmaster_id)
        {
            DataTable table = testCheckEService.SelectFile(testcontrol_id, teststandardmaster_id);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 파일업로드
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="teststandardmaster_id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TestCheckEUploadFile(string testcontrol_id, string teststandardmaster_id, string name)
        {
            try
            {
                EquipmentService equipmentService = new EquipmentService();

                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;

                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    testCheckEService.uploadFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), fileName, contentType, testcontrol_id, teststandardmaster_id);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 파일다운로드
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestCheckEDownloadFile(string testcontrol_id, string teststandardmaster_id, string file_id)
        {         
            EquipmentService equipmentService = new EquipmentService();

            DataTable attachmentFileData = testCheckEService.DownloadFile(testcontrol_id, teststandardmaster_id, file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0]["file_image"]
                , mimeType
                , attachmentFileData.Rows[0]["doc_name"].ToString()
                );
        }

        /// <summary>
        /// 파일삭제
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="teststandardmaster_id"></param>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpPost]
        public string TestCheckEDeleteFile(string testcontrol_id, string teststandardmaster_id, string file_id)
        {
            string res = testCheckEService.DeleteFile(testcontrol_id, teststandardmaster_id, file_id);
            return res;
        }

        /// <summary>
        /// CancelCheckInfo
        /// </summary>
        /// <returns>가능여부 리턴(Y/N)</returns>
        [HttpPost]
        public JsonResult TestCheckECancelCheckInfo([JsonBinder] TestCheckE dto)
        {
            string res;
            res = testCheckEService.CancelCheckInfo(dto);
            string resJson;

            if (res.Length > 0)
            {                
               resJson = "{ \"message\": \"" + res + "\" }";
            }
            else
            {
                resJson = string.Empty;
            }            
            return Json(resJson);
        }

        /// <summary>
        /// 최종 서명자 여부를 결정한다.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public string testCheckEGetLastSignEmpCheck([JsonBinder] TestCheckE dto)
        {            
            string res = (testCheckEService.GetLastSignEmpCheck(dto) ? "Y" : "N");
            return res;
        }

        /// <summary>
        /// 시험 결과 취소
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestCheckECancelSign([JsonBinder] TestCheckE dto)
        {
            string res = (testCheckEService.CancelSign(dto) ? "Y" : "N");
            return Json( new { message = res });
        }

        [HttpPost]
        public string TestCheckECheckTestStandard([JsonBinder] TestCheckE dto)
        {
            string res = (testCheckEService.CheckTestStandard(dto)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestCheckEDataCheckYN([JsonBinder]TestCheckE dto)
        {
            string res = (testCheckEService.DataCheckYN(dto)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestCheckECheckTestDate([JsonBinder]TestCheckE dto)
        {
            string res = (testCheckEService.CheckTestDate(dto)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestCheckESetStatusTestCompleteForReject(string testcontrol_id)
        {
            string res = (testCheckEService.SetStatusTestCompleteForReject(testcontrol_id)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestCheckESetNextStatus(string testcontrol_id, string test_type, string process_kind)
        {
            string res = (testCheckEService.SetNextStatus(testcontrol_id, test_type, process_kind)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestCheckESaveTestJudgment([JsonBinder]TestCheckE dto)
        {
            string res = (testCheckEService.SaveTestJudgment(dto)) ? "Y" : "N";
            return res;
        }

        [HttpGet]
        public string TestCheckESignDelegateCheck(string emp_cd, string testcontrol_id, string process_kind, string test_type, string sign_set_dt_id)
        {
            string res = (testCheckEService.SignDelegateCheck(emp_cd, testcontrol_id, process_kind, test_type, sign_set_dt_id)) ? "Y" : "N";
            return res;
        }

        public string TestCheckESaveElectronicSignature(string gubun, string emp_cd, string testcontrol_id, string process_kind, string test_type, string sign_set_dt_id, string validation_type, string representative_yn)
        {
            string res = (testCheckEService.SaveElectronicSignature(gubun, emp_cd, testcontrol_id, process_kind, test_type, sign_set_dt_id, validation_type, representative_yn)) ? "Y" : "N";
            return res;
        }

        public void TestcheckEReTest(string testcontrol_id, string teststandardmaster_id)
        {
            string res = testCheckEService.ReTest(testcontrol_id, teststandardmaster_id);

        }

        /// <summary>
        /// 트랜잭션(=CRUD) 처리 : 수정
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>        
        /// <returns></returns>
        //[HttpPost]
        //public JsonResult TestScheduleDetailTRX([JsonBinder] List<TestSchedule.TestScheduleDetailArrange> dto, string testcontrol_id, string instruction_no, string picking_ordered_emp_cd, string instruction_date)
        //public JsonResult TestItemResultJudgementMultiTrx([JsonBinder] List<TestItemResultJudgement.TestItemResultJudgementDetailArrange> dto)
        //{
        //    string res = testItemResultJudgementService.MultiTrx(dto);

        //    if (res.Length == 0)
        //    {
        //        res = "실패 되었습니다. 관리자에게 문의하세요";
        //    }
        //    var resJson = "{ \"message\": \"" + res + "\" }";
        //    return Json(resJson);
        //}

        /// <summary>
        /// 트랜잭션 처리 : 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public JsonResult TestItemResultJudgementDeleteTrx(TestItemResultJudgement.SrchParam dto)
        //{
        //    string res = testItemResultJudgementService.DeleteTrx(dto);
        //    if (res.Length == 0)
        //    {
        //        res = "실패 되었습니다. 관리자에게 문의하세요";
        //    }
        //    var resJson = "{ \"message\": \"" + res + "\" }";
        //    return Json(resJson);
        //}

        #endregion

        #region 시험기록서 승인
        private TestRecognitionEService testRecognitionEService = new TestRecognitionEService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult TestRecognitionE(TestRecognitionE.SrchParam srch)
        {
            // 1. 공통코드 List
            //DataTable le_testitem_nm = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "le_testitem_nm");
            //DataTable replue_test_status = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "replue_test_status");
            //DataTable le_item_form_cd = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "le_item_form_cd");

            //DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");
            //DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");

            // 2. 그리드 List            
            //DataTable mainGrid = testRecognitionEService.Select(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            //ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식            

            // 3.3. 코드성데이터            
            //ViewBag.le_testitem_nm = le_testitem_nm;
            //ViewBag.replue_test_status = replue_test_status;
            //ViewBag.le_item_form_cd = le_item_form_cd;

            //ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            //ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 그리드 조회(Main)
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult TestRecognitionESelect(TestRecognitionE.SrchParam srch)
        {
            srch.Gubun = "S";
            DataTable table = testRecognitionEService.Select(srch);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 그리드조회(detail)
        /// </summary>
        /// <param name="srch"></param>
        /// <param name="dto"></param>
        /// <param name="process_kind"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult TestRecognitionESelectDetail([JsonBinder] TestRecognitionE dto, string process_kind)
        {
            // 구분 설정
            TestRecognitionE.SrchParam srch = new TestRecognitionE.SrchParam();
            srch.Gubun = "S1";

            // detail data
            DataTable detailTable = testRecognitionEService.SelectDetail(srch, dto);

            // sign data
            DataTable signTable = testRecognitionEService.SelectSign(dto.testcontrol_id.ToString(), dto.test_type, process_kind);

            // File data
            //DataTable fileTable = testRecognitionEService.SelectFile(dto.testcontrol_id.ToString());

            string[] jsonArr = new string[2];
            jsonArr[0] = Public_Function.DataTableToJSON(detailTable);
            jsonArr[1] = Public_Function.DataTableToJSON(signTable);
            //jsonArr[2] = Public_Function.DataTableToJSON(fileTable);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 파일조회
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult TestRecognitionESelectFile(string testcontrol_id, string teststandardmaster_id)
        {
            DataTable table = testRecognitionEService.SelectFile(testcontrol_id, teststandardmaster_id);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 파일업로드
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="teststandardmaster_id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TestRecognitionEUploadFile(string testcontrol_id, string teststandardmaster_id, string name)
        {
            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;

                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    testRecognitionEService.uploadFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), fileName, contentType, testcontrol_id, teststandardmaster_id);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 파일다운로드
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestRecognitionEDownloadFile(string testcontrol_id, string teststandardmaster_id, string file_id)
        {
            EquipmentService equipmentService = new EquipmentService();

            DataTable attachmentFileData = testRecognitionEService.DownloadFile(testcontrol_id, teststandardmaster_id, file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0]["file_image"]
                , mimeType
                , attachmentFileData.Rows[0]["doc_name"].ToString()
                );
        }

        /// <summary>
        /// 파일삭제
        /// </summary>
        /// <param name="testcontrol_id"></param>
        /// <param name="teststandardmaster_id"></param>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpPost]
        public string TestRecognitionEDeleteFile(string testcontrol_id, string teststandardmaster_id, string file_id)
        {
            string res = testRecognitionEService.DeleteFile(testcontrol_id, teststandardmaster_id, file_id);
            return res;
        }

        /// <summary>
        /// 최종 서명자 여부를 결정한다.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public string testCheckEGetLastSignEmpCheck([JsonBinder] TestRecognitionE dto)
        {
            string res = (testRecognitionEService.GetLastSignEmpCheck(dto) ? "Y" : "N");
            return res;
        }

        /// <summary>
        /// 시험 결과 취소
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public string TestRecognitionECancelSign([JsonBinder] TestRecognitionE dto)
        {
            string res = (testRecognitionEService.CancelSign(dto) ? "Y" : "N");
            return res;
        }

        [HttpPost]
        public string TestRecognitionECheckTestStandard([JsonBinder] TestRecognitionE dto)
        {
            string res = (testRecognitionEService.CheckTestStandard(dto)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestRecognitionEDataCheckYN([JsonBinder] TestRecognitionE dto)
        {
            string res = (testRecognitionEService.DataCheckYN(dto)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestRecognitionECheckTestDate([JsonBinder] TestRecognitionE dto)
        {
            string res = (testRecognitionEService.CheckTestDate(dto)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestRecognitionESetStatusTestCompleteForReject(string testcontrol_id)
        {
            string res = (testRecognitionEService.SetStatusTestCompleteForReject(testcontrol_id)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestRecognitionESetNextStatus(string testcontrol_id, string test_type, string process_kind)
        {
            string res = (testRecognitionEService.SetNextStatus(testcontrol_id, test_type, process_kind)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public void TestRecognitionESaveCheckInfo([JsonBinder] TestRecognitionE dto)
        {
            string res = testRecognitionEService.SaveCheckInfo(dto);            
        }

        [HttpGet]
        public string TestRecognitionESignDelegateCheck(string emp_cd, string testcontrol_id, string process_kind, string test_type, string sign_set_dt_id)
        {
            string res = (testRecognitionEService.SignDelegateCheck(emp_cd, testcontrol_id, process_kind, test_type, sign_set_dt_id)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestRecognitionESaveElectronicSignature(string gubun, string emp_cd, string testcontrol_id, string process_kind, string test_type, string sign_set_dt_id, string validation_type, string representative_yn)
        {
            string res = (testRecognitionEService.SaveElectronicSignature(gubun, emp_cd, testcontrol_id, process_kind, test_type, sign_set_dt_id, validation_type, representative_yn)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestRecognitionECheckEnd([JsonBinder] TestRecognitionE dto)
        {
            string res = (testRecognitionEService.Check_End(dto)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestRecognitionEInterfaceUpload(string testcontrol_id, string test_type, string create_type)
        {
            string res = testRecognitionEService.InterfaceUpload(testcontrol_id, test_type, create_type);
            return res;
        }

        [HttpPost]
        public string TestRecognitionEESStatusCheck(string test_type, string test_status, string process_kind)
        {
            string res = (testRecognitionEService.ESStatusCheck(test_type, test_status, process_kind)) ? "Y" : "N";
            return res;
        }

        [HttpPost]
        public string TestRecognitionECancelCheckInfo(string testcontrol_id, string test_type)
        {
            string res = testRecognitionEService.CancelCheckInfo(testcontrol_id, test_type);
            return res;
        }

        [HttpGet]
        public JsonResult TestCheckECheckLastSignEmp(string testcontrol_id, string test_type, string process_kind, string program_cd)
        {
            string res = testRecognitionEService.TestCheckECheckLastSignEmp(testcontrol_id, test_type, process_kind, program_cd);
            return Json(new { message = res }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 트랜잭션(=CRUD) 처리 : 수정
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>        
        /// <returns></returns>
        //[HttpPost]
        //public JsonResult TestScheduleDetailTRX([JsonBinder] List<TestSchedule.TestScheduleDetailArrange> dto, string testcontrol_id, string instruction_no, string picking_ordered_emp_cd, string instruction_date)
        //public JsonResult TestItemResultJudgementMultiTrx([JsonBinder] List<TestItemResultJudgement.TestItemResultJudgementDetailArrange> dto)
        //{
        //    string res = testItemResultJudgementService.MultiTrx(dto);

        //    if (res.Length == 0)
        //    {
        //        res = "실패 되었습니다. 관리자에게 문의하세요";
        //    }
        //    var resJson = "{ \"message\": \"" + res + "\" }";
        //    return Json(resJson);
        //}

        /// <summary>
        /// 트랜잭션 처리 : 삭제
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        //public JsonResult TestItemResultJudgementDeleteTrx(TestItemResultJudgement.SrchParam dto)
        //{
        //    string res = testItemResultJudgementService.DeleteTrx(dto);
        //    if (res.Length == 0)
        //    {
        //        res = "실패 되었습니다. 관리자에게 문의하세요";
        //    }
        //    var resJson = "{ \"message\": \"" + res + "\" }";
        //    return Json(resJson);
        //}

        #endregion

        #region 항목별검수 확인
        private WorksheetListService worksheetListService = new WorksheetListService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult WorksheetList(WorksheetList.SrchParam srch)
        {
            // 1. 공통코드 List
            //DataTable le_testitem_nm = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "le_testitem_nm");
            //DataTable replue_test_status = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "replue_test_status");
            //DataTable le_item_form_cd = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "le_item_form_cd");

            //DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");
            //DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");

            // 2. 그리드 List            
            //DataTable mainGrid = worksheetListService.Select(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            //ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식            

            // 3.3. 코드성데이터            
            //ViewBag.le_testitem_nm = le_testitem_nm;
            //ViewBag.replue_test_status = replue_test_status;
            //ViewBag.le_item_form_cd = le_item_form_cd;

            //ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            //ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 그리드 조회(Main)
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult WorksheetListSelect(WorksheetList.SrchParam srch)
        {
            srch.Gubun = "S";
            DataTable table = worksheetListService.Select(srch);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 시험별공수 확인
        private TestScheduleCheckService testScheduleCheckService = new TestScheduleCheckService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult TestScheduleCheck(TestScheduleCheck.SrchParam srch)
        {
            // 1. 공통코드 List
            //DataTable le_testitem_nm = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "le_testitem_nm");
            //DataTable replue_test_status = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "replue_test_status");
            //DataTable le_item_form_cd = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "le_item_form_cd");

            //DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");
            //DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");

            // 2. 그리드 List            
            //DataTable mainGrid = worksheetListService.Select(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            //ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식            

            // 3.3. 코드성데이터            
            //ViewBag.le_testitem_nm = le_testitem_nm;
            //ViewBag.replue_test_status = replue_test_status;
            //ViewBag.le_item_form_cd = le_item_form_cd;

            //ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            //ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 그리드 조회(Main)
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult TestScheduleCheckSelect(TestScheduleCheck.SrchParam srch)
        {
            srch.Gubun = "S";
            DataTable table = testScheduleCheckService.Select(srch);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 시험진행단계
        private TestProgressStatusService testProgressStatusService = new TestProgressStatusService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult TestProgressStatus(TestProgressStatus.SrchParam srch)
        {
            // 1. 공통코드 List
            //DataTable le_testitem_nm = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "le_testitem_nm");
            //DataTable replue_test_status = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "replue_test_status");
            //DataTable le_item_form_cd = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "le_item_form_cd");

            //DataTable itemCodePopupData = codeHelpService.GetCode(CodeHelpType.item, "", "");
            //DataTable venderProdPopupData = codeHelpService.GetCode(CodeHelpType.vender_prod, "", "");

            // 2. 그리드 List            
            //DataTable mainGrid = worksheetListService.Select(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            //ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식            

            // 3.3. 코드성데이터            
            //ViewBag.le_testitem_nm = le_testitem_nm;
            //ViewBag.replue_test_status = replue_test_status;
            //ViewBag.le_item_form_cd = le_item_form_cd;

            //ViewBag.itemCodePopupData = Json(Public_Function.DataTableToJSON(itemCodePopupData));
            //ViewBag.venderProdPopupData = Json(Public_Function.DataTableToJSON(venderProdPopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 그리드 조회(Main)
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult TestProgressStatusSelect(TestProgressStatus.SrchParam srch)
        {
            srch.Gubun = "S";
            DataTable table = testProgressStatusService.Select(srch);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 시험통계분석
        private QualityStatisticalAnalysisService qualityStatisticalAnalysisService = new QualityStatisticalAnalysisService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult QualityStatisticalAnalysis(QualityStatisticalAnalysis.SrchParam srch)
        {
            // 1. 공통코드 List
            //DataTable le_testitem_nm = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "le_testitem_nm");
            //DataTable replue_test_status = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "replue_test_status");
            //DataTable le_item_form_cd = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "le_item_form_cd");

            DataTable itemCodePopupData = codeHelpService.GetCode(CodeHelpType.item, "", "");
            DataTable venderProdPopupData = codeHelpService.GetCode(CodeHelpType.vender_prod, "", "");
            DataTable testItemCodePopupData = codeHelpService.GetCode(CodeHelpType.testitemmaster, "", "");

            // 2. 그리드 List            
            //DataTable mainGrid = worksheetListService.Select(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            //ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식            

            // 3.3. 코드성데이터            
            //ViewBag.le_testitem_nm = le_testitem_nm;
            //ViewBag.replue_test_status = replue_test_status;
            //ViewBag.le_item_form_cd = le_item_form_cd;

            //ViewBag.itemCodePopupData = Json(Public_Function.DataTableToJSON(itemCodePopupData));
            ViewBag.itemCodePopupData = itemCodePopupData;
            ViewBag.venderProdPopupData = Json(Public_Function.DataTableToJSON(venderProdPopupData));
            ViewBag.testItemCodePopupData = Json(Public_Function.DataTableToJSON(testItemCodePopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 그리드 조회(Main)
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult QualityStatisticalAnalysisSelect(QualityStatisticalAnalysis.SrchParam srch)
        {
            srch.Gubun = "S1";
            DataTable table = qualityStatisticalAnalysisService.Select(srch);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QualityStatisticalAnalysisSelectTestItem([JsonBinder] QualityStatisticalAnalysis.SrchParam srch, [JsonBinder] QualityStatisticalAnalysis dto)
        {
            srch.Gubun = "S2";
            DataTable table = qualityStatisticalAnalysisService.SelectTestItem(srch, dto);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QualityStatisticalAnalysisSelectTestData([JsonBinder] QualityStatisticalAnalysis.SrchParam srch, [JsonBinder] QualityStatisticalAnalysis.TestItem dto)
        {
            srch.Gubun = "S3";
            DataTableCollection tables = qualityStatisticalAnalysisService.SelectTestData(srch, dto);
            string[] jsonArray = new string[2];
            
            jsonArray[0] = Public_Function.DataTableToJSON(tables[0]);
            jsonArray[1] = Public_Function.DataTableToJSON(tables[1]);
            JsonResult result = Json(jsonArray, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public JsonResult QualityStatisticalAnalysisSelectItemDetail([JsonBinder] QualityStatisticalAnalysis.SrchParam srch, [JsonBinder] QualityStatisticalAnalysis dto, string itemParam)
        {
            srch.Gubun = "S4";
            DataTable table = qualityStatisticalAnalysisService.SelectTestItemDetail(srch, dto, itemParam);
            return Json(Public_Function.DataTableToJSON(table), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 시험기록서 보관
        private TestReportDepositService testReportDepositService = new TestReportDepositService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult TestReportDeposit(TestReportDeposit.SrchParam srch)
        {
            // 1. 공통코드 List
            //DataTable le_testitem_nm = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "le_testitem_nm");
            //DataTable replue_test_status = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "replue_test_status");
            //DataTable le_item_form_cd = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "le_item_form_cd");

            //DataTable itemCodePopupData = codeHelpService.GetCode(CodeHelpType.item, "", "");
            //DataTable venderProdPopupData = codeHelpService.GetCode(CodeHelpType.vender_prod, "", "");
            //DataTable testItemCodePopupData = codeHelpService.GetCode(CodeHelpType.testitemmaster, "", "");

            // 2. 그리드 List            
            //DataTable mainGrid = worksheetListService.Select(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            //ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식            

            // 3.3. 코드성데이터            
            //ViewBag.le_testitem_nm = le_testitem_nm;
            //ViewBag.replue_test_status = replue_test_status;
            //ViewBag.le_item_form_cd = le_item_form_cd;

            //ViewBag.itemCodePopupData = Json(Public_Function.DataTableToJSON(itemCodePopupData));
            //ViewBag.venderProdPopupData = Json(Public_Function.DataTableToJSON(venderProdPopupData));
            //ViewBag.testItemCodePopupData = Json(Public_Function.DataTableToJSON(testItemCodePopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 그리드 조회(Main)
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestReportDepositSelect( int testcontrol_id, string test_no)
        {
            // detail data
            TestRecognitionE.SrchParam srch = new TestRecognitionE.SrchParam() { Gubun = "S1" };
            TestRecognitionE dto = new TestRecognitionE() { testcontrol_id = testcontrol_id };
            DataTable detailTable = testRecognitionEService.SelectDetail(srch, dto);

            // file data(문서)
            HACCP.Models.QualityControl.TestReportDeposit.SrchParam srch2 = new TestReportDeposit.SrchParam() { Gubun = "S", Test_no=test_no };
            DataTable docTable = testReportDepositService.Select(srch2);

            string[] jsonArr = new string[2];
            jsonArr[0] = Public_Function.DataTableToJSON(detailTable);
            jsonArr[1] = Public_Function.DataTableToJSON(docTable);
            //jsonArr[2] = Public_Function.DataTableToJSON(fileTable);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 파일다운로드
        /// </summary>
        /// <param name="doc_file_id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestReportDepositDownloadFile(string file_id)
        {
            EquipmentService equipmentService = new EquipmentService();
            DataTable attachmentFileData = testReportDepositService.DownloadFile(file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());
            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0]["file_image"]
                , mimeType
                , attachmentFileData.Rows[0]["doc_name"].ToString()
                );
        }

        #endregion

        #region 첨부파일 관리 BackDataDownload

        public ActionResult BackDataDownload()
        {
            return View();
        }

        [HttpGet]
        public JsonResult BackDataDownloadSelect(BackDataDownload backDataDownload)
        {
            BackDataDownloadService backDataDownloadService = new BackDataDownloadService();

            DataTable dt = backDataDownloadService.BackDataDownloadSelect(backDataDownload);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BackDataDownloadFile(string doc_file_id)
        {
            BackDataDownloadService backDataDownloadService = new BackDataDownloadService();
            EquipmentService equipmentService = new EquipmentService();

            DataTable attachmentFileData = backDataDownloadService.BackDataDownloadFile(doc_file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0]["file_image"].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0]["file_image"]
                , mimeType
                , attachmentFileData.Rows[0]["doc_name"].ToString()
                );
        }

        #endregion

        #region 시험기기 사용 계획 TesterAssignment

        public ActionResult TesterAssignment()
        {
            TesterAssignmentService testerAssignmentService = new TesterAssignmentService();

            DataTable dt = testerAssignmentService.TesterAssignmentSelectScheduleDate();

            ViewBag.testitemScheduleTime = Json(Public_Function.DataTableToJSON(dt));

            return View();
        }

        [HttpGet]
        public JsonResult TesterAssignmentSelect(string testitem_schedule_time)
        {
            TesterAssignmentService testerAssignmentService = new TesterAssignmentService();

            DataTable dt = testerAssignmentService.TesterAssignmentSelect(testitem_schedule_time);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TesterAssignmentTesterSelect(string testitem_schedule_time, string tester_cd)
        {
            TesterAssignmentService testerAssignmentService = new TesterAssignmentService();

            DataTable dt = testerAssignmentService.TesterAssignmentTesterSelect(testitem_schedule_time, tester_cd);
            DataTable dt2 = testerAssignmentService.TesterAssignmentTestStandardSelect(testitem_schedule_time, tester_cd);

            string[] jsonArr = new string[2];

            jsonArr[0] = Public_Function.DataTableToJSON(dt);
            jsonArr[1] = Public_Function.DataTableToJSON(dt2);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TesterAssignmentAssignmentTesterSelect(string testitem_schedule_time, string tester_cd)
        {
            TesterAssignmentService testerAssignmentService = new TesterAssignmentService();

            DataTable dt = testerAssignmentService.TesterAssignmentAssignmentTesterSelect(testitem_schedule_time, tester_cd);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TesterAssignmentUpdate(string tester_cd, string testcontrol_id, string teststandardmaster_id, string gubun)
        {
            TesterAssignmentService testerAssignmentService = new TesterAssignmentService();

            string res = testerAssignmentService.TesterAssignmentUpdate(tester_cd, testcontrol_id, teststandardmaster_id, gubun);

            return Json(new { message = res });
        }

        #endregion

        #region RDMS 파일 검색/출력 RDMSFileDownload

        public ActionResult RDMSFileDownload()
        {

            return View();
        }

        [HttpPost]
        public JsonResult RDMSFileDownloadSelect(RDMSFileDownload.SrchParam srchParam)
        {
            RDMSFileDownloadService fileDownloadService = new RDMSFileDownloadService();

            DataTable dt = fileDownloadService.RDMSFileDownloadSelect(srchParam);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpGet]
        public JsonResult RDMSFileDownloadTestDetailSelect(string testcontrol_id)
        {
            RDMSFileDownloadService fileDownloadService = new RDMSFileDownloadService();

            DataTable dt = fileDownloadService.RDMSFileDownloadTestDetailSelect(testcontrol_id);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RDMSFileDownloadTestFileSelect(string testcontrol_id, string teststandardmaster_id)
        {
            RDMSFileDownloadService fileDownloadService = new RDMSFileDownloadService();

            DataTable dt = fileDownloadService.RDMSFileDownloadTestFileSelect(testcontrol_id, teststandardmaster_id);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RDMSFileDownloadTestFileDownload(string testcontrol_id, string teststandardmaster_id, string doc_file_id)
        {
            RDMSFileDownloadService fileDownloadService = new RDMSFileDownloadService();
            EquipmentService equipmentService = new EquipmentService();

            DataTable attachmentFileData = fileDownloadService.RDMSFileDownloadTestFileDownload(testcontrol_id, teststandardmaster_id, doc_file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0]["file_extension_name"].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0]["file_image"]
                , mimeType
                , attachmentFileData.Rows[0]["doc_name"].ToString()
                );
        }

        [HttpPost]
        public HttpResponseMessage RDMSFileDownloadTestFilePreview(string testcontrol_id, string teststandardmaster_id, string doc_file_id)
        {
            RDMSFileDownloadService fileDownloadService = new RDMSFileDownloadService();

            DataTable attachmentFileData = fileDownloadService.RDMSFileDownloadTestFileDownload(testcontrol_id, teststandardmaster_id, doc_file_id);

            byte[] ImageByte = (byte[])attachmentFileData.Rows[0]["file_image"];
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            MemoryStream oMemoryStream = new MemoryStream(ImageByte);
            result.Content = new StreamContent(oMemoryStream);
            result.Content.Headers.Add("Content-Type", "application/octet-stream");

            return result;
        }

        #endregion

        [HttpGet]
        public JsonResult TestTest(CodeHelpType codeHelpType, string plant, string defaultCode)
        {
            DataTable itemCodePopupData = codeHelpService.GetCode(codeHelpType, plant, defaultCode);

            return Json(Public_Function.DataTableToJSON(itemCodePopupData), JsonRequestBehavior.AllowGet);
        }



    }
}
