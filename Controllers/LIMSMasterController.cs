using HACCP.Attribute;
using HACCP.Libs;
using HACCP.Models.LIMSMaster;
using HACCP.Services.Comm;
using HACCP.Services.LIMSMaster;
using HACCP.Services.SysCom;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class LIMSMasterController : Controller
    {
        // LIMS 마스터 관리

        #region 시험 관리 단계 설정(TestManage_StepSetting)
        public ActionResult TestManage_StepSetting()
        {
            TestManage_StepSettingService testManage_StepSettingService = new TestManage_StepSettingService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable programGridData = testManage_StepSettingService.SelectProgram();

            ViewBag.programGridData = Json(Public_Function.DataTableToJSON(programGridData));

            return View();
        }

        [HttpGet]
        public JsonResult TestManage_StepSettingSelect(string test_type, string process_kind)
        {
            TestManage_StepSettingService testManage_StepSettingService = new TestManage_StepSettingService();

            DataTable gridData1 = testManage_StepSettingService.TestManage_StepSettingSelect(test_type, process_kind);
            DataTable gridData2 = testManage_StepSettingService.TestManage_StepSettingSelectProgram(test_type);

            string[] jsonArr = new string[2];

            jsonArr[0] = Public_Function.DataTableToJSON(gridData1);
            jsonArr[1] = Public_Function.DataTableToJSON(gridData2);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TestManage_StepSettingTRX(TestManageStepSetting testManageStepSetting)
        {
            TestManage_StepSettingService testManage_StepSettingService = new TestManage_StepSettingService();

            string message = testManage_StepSettingService.TestManage_StepSettingTRX(testManageStepSetting);

            return Json(new { message = message });
        }

        [HttpPost]
        public ActionResult TestManage_StepSettingSaveProgramCnt([JsonBinder] List<TestManageStepSetting> programData)
        {
            TestManage_StepSettingService testManage_StepSettingService = new TestManage_StepSettingService();
            string message = string.Empty;

            foreach (TestManageStepSetting testManageStepSetting in programData)
            {
                message = testManage_StepSettingService.TestManage_StepSettingTRX(testManageStepSetting);
            }

            return Json(new { message = message });
        }

        [HttpPost]
        public ActionResult TestManage_StepSettingCopy(string test_type, string from_test_type)
        {
            TestManage_StepSettingService testManage_StepSettingService = new TestManage_StepSettingService();

            string message = testManage_StepSettingService.TestManage_StepSettingCopy(test_type, from_test_type);

            return Json(new { message = message });
        }

        #endregion


        #region 시험항목 관리(TestItemmaster)
        private TestItemmasterService testItemmasterService = new TestItemmasterService();

        public ActionResult TestItemmaster(TestItemmaster.SrchParam srch)
        {
            SelectBoxService selectBoxService = new SelectBoxService();
            CodeHelpService codeHelpService = new CodeHelpService();

            // 2. 그리드 List            
            DataTable mainGrid = testItemmasterService.SelectMain(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }

        /// <summary>
        /// 검색 : 로드후
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestItemmasterSelect(TestItemmaster.SrchParam srch)
        {
            DataTable mainGrid = testItemmasterService.SelectMain(srch);

            return Json(Public_Function.DataTableToJSON(mainGrid));
        }

        /// <summary>
        /// 채번 : testitem_cd 리턴
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string TestItemmasterGetSeqNo()
        {
            return testItemmasterService.GetSeqNo();
        }

        /// <summary>
        /// 트랜잭션(=CRUD) 처리
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>
        /// <returns></returns>
        [HttpPost]
        public string TestitemmasterTRX(TestItemmaster dto, TestItemmaster.SrchParam srch)
        {
            string res = testItemmasterService.DataTrx(dto, srch);
            return res;
        }

        #endregion


        #region 시험규격 관리(TestMasterManagement3)

        public ActionResult TestMasterManagement3()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable items = codeHelpService.GetCode(CodeHelpType.item, HttpContext.Session["plantCD"].ToString(), string.Empty);

            ViewBag.items = Json(Public_Function.DataTableToJSON(items));
            //ViewBag.items = items;

            return View();
        }

        public JsonResult TestMasterManagement3Select(TestMasterManagement.SrchParam testMasterManagement)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            DataTable testMasterManagementDt = testMasterManagement3Service.TestMasterManagement3Select(testMasterManagement);

            //JsonResult jr = new JsonResult();

            JsonResult jr = Json(Public_Function.DataTableToJSON(testMasterManagementDt), JsonRequestBehavior.AllowGet);
            jr.MaxJsonLength = int.MaxValue;

            return jr;
        }

        public JsonResult TestMasterManagement3SelectRevisionNo(TestMasterManagement.SrchParam testMasterManagement)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            DataTable testMasterManagementRevisionDt = testMasterManagement3Service.TestMasterManagement3SelectRevisionNo(testMasterManagement);

            return Json(Public_Function.DataTableToJSON(testMasterManagementRevisionDt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TestMasterManagement3TRX(TestMasterManagement testMasterManagement)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            string res = testMasterManagement3Service.TestMasterManagement3TRX(testMasterManagement);

            return Json(new { message = res });
        }

        [HttpGet]
        public JsonResult TestMasterManagement3SelectDetail(TestMasterManagement.SrchParam testMasterManagement)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            // 개정내역, 시험기준
            DataSet TestMasterManagementDetail1 = testMasterManagement3Service.TestMasterManagement3SelectDetail(testMasterManagement);

            testMasterManagement.testmaster_id = TestMasterManagementDetail1.Tables[0].Rows[0]["testmaster_id"].ToString();

            // 개정 내역
            DataTable TestMasterManagementRevisionHistory = testMasterManagement3Service.TestMasterManagement3SelectRevisionHistory(testMasterManagement);

            // 전자 서명
            DataTable TestMasterManagementSignData = testMasterManagement3Service.TestMasterManagement3SelectSignData(testMasterManagement);

            // 공통 규격
            DataTable CommonStandard = testMasterManagement3Service.TestMasterManagement3SelectCommonStandard(TestMasterManagementDetail1.Tables[0].Rows[0].ItemArray[0].ToString());

            // 첨부파일 그리드
            DataTable FileData = testMasterManagement3Service.TestMasterManagement3SelectFileData(TestMasterManagementDetail1.Tables[0].Rows[0].ItemArray[0].ToString());

            string[] jsonArr = new string[6];
            jsonArr[0] = Public_Function.DataTableToJSON(TestMasterManagementDetail1.Tables[0]);
            jsonArr[1] = Public_Function.DataTableToJSON(TestMasterManagementDetail1.Tables[1]);
            jsonArr[2] = Public_Function.DataTableToJSON(TestMasterManagementRevisionHistory);
            jsonArr[3] = Public_Function.DataTableToJSON(TestMasterManagementSignData);
            jsonArr[4] = Public_Function.DataTableToJSON(CommonStandard);
            jsonArr[5] = Public_Function.DataTableToJSON(FileData);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TestMasterManagementSelectDocList(string testmaster_id)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            DataTable FileData = testMasterManagement3Service.TestMasterManagement3SelectFileData(testmaster_id);

            return Json(Public_Function.DataTableToJSON(FileData), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadChangeAttachedFile(string testmaster_id, string name)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;

                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    testMasterManagement3Service.uploadFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), fileName, contentType, testmaster_id);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult TestMasterManagementDownload(string testmaster_id, string worksheet_file_id)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();
            EquipmentService equipmentService = new EquipmentService();

            DataTable attachmentFileData = testMasterManagement3Service.AttachmentFileDownload(testmaster_id, worksheet_file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0]["file_image"]
                , mimeType
                , attachmentFileData.Rows[0]["doc_name"].ToString()
                );
        }

        [HttpPost]
        public string TestMasterManagementDeleteDoc(string testmaster_id, string worksheet_file_id)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            string res = testMasterManagement3Service.TestMasterManagementDeleteDoc(testmaster_id, worksheet_file_id);

            return res;
        }

        [HttpPost]
        public ActionResult TestMasterManagementSignTRX(TestMasterManagement.SignParam signParam)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            string res = testMasterManagement3Service.TestMasterManagementSignTRX(signParam);

            return Json(new { message = res });
        }

        [HttpGet]
        public JsonResult TestMasterManagementSelectTestCriteria()
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            DataTable dt = testMasterManagement3Service.TestMasterManagementSelectTestCriteria();

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TestMasterManagementTestCriteriaTRX(TestMasterManagement testMasterManagement)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            string res = testMasterManagement3Service.TestMasterManagementTestCriteriaTRX(testMasterManagement);

            return Json(new { message = res });
        }

        [HttpPost]
        public ActionResult TestMasterManagement3CriteriaTRX([JsonBinder] List<TestMasterManagement.TestCriteria> testMasterManagements)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            string res = testMasterManagement3Service.TestMasterManagement3CriteriaTRX(testMasterManagements);

            return Json(new { message = res });
        }

        [HttpPost]
        public ActionResult TestMasterManagementTestItemUpdate(string testitem_cd, string testitem_nm)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            string res = testMasterManagement3Service.TestMasterManagementTestItemUpdate(testitem_cd, testitem_nm);

            return Json(new { message = res });
        }

        [HttpPost]
        public ActionResult TestMasterManagement3CommonSpecificationTRX([JsonBinder] List<TestMasterManagement.CommonSpecification> commonSpecifications)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            string res = testMasterManagement3Service.TestMasterManagement3CommonSpecificationTRX(commonSpecifications);

            return Json(new { message = res });
        }

        [HttpGet]
        public JsonResult TestMasterManagementSelectExistingSpecification(string item_cd)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            DataTable dt = testMasterManagement3Service.TestMasterManagementSelectExistingSpecification(item_cd);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TestMasterManagementSelectExistingSpecificationDetail(string testmaster_id)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            DataTable dt = testMasterManagement3Service.TestMasterManagementSelectExistingSpecificationDetail(testmaster_id);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TestMasterManagementSelectExternalSpecification(string item_cd)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            DataTable dt = testMasterManagement3Service.TestMasterManagementSelectExternalSpecification(item_cd);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TestMasterManagementSelectExternalSpecificationDetail(string testmaster_id)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            DataTable dt = testMasterManagement3Service.TestMasterManagementSelectExternalSpecificationDetail(testmaster_id);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TestMasterManagementExistingSpecificationCopy(TestMasterManagement.SpecificationCopyParam specificationCopyParam)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            string res = testMasterManagement3Service.TestMasterManagementExistingSpecificationCopy(specificationCopyParam);

            return Json(new { message = res });
        }


        [HttpPost]
        public ActionResult TestMasterManagementExternalSpecificationCopy(TestMasterManagement.SpecificationCopyParam specificationCopyParam)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            string res = testMasterManagement3Service.TestMasterManagementExternalSpecificationCopy(specificationCopyParam);

            return Json(new { message = res });
        }

        [HttpPost]
        public ActionResult TestMasterManagementUseYn(TestMasterManagement.SrchParam param)
        {
            TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

            string res = testMasterManagement3Service.TestMasterManagementUseYn(param);

            return Json(new { message = res });
        }

        //[HttpPost]
        //public ActionResult TestMasterManagement3TRX([JsonBinder]List<TestMasterManagement> testMasterManagement)
        //{
        //    TestMasterManagement3Service testMasterManagement3Service = new TestMasterManagement3Service();

        //    string res = testMasterManagement3Service.TestMasterManagement3TRX(testMasterManagement);

        //    return Json( new { message = res } );
        //}

        #endregion


        #region 시험기기 관리(Tester)

        public ActionResult Tester(Tester.SrchParam param)
        {
            TesterService testerService = new TesterService();

            DataTable testerData = testerService.TesterSelect(param);

            ViewBag.testerData = Json(Public_Function.DataTableToJSON(testerData));

            return View();
        }

        public JsonResult TesterSelect(Tester.SrchParam param)
        {
            TesterService testerService = new TesterService();

            DataTable dt = testerService.TesterSelect(param);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpGet]
        public JsonResult TesterSelectDetail(string tester_cd)
        {
            TesterService testerService = new TesterService();

            DataTable Parameter = testerService.TesterSelectParameter(tester_cd);
            DataTable TestImage = testerService.TesterSelectTesterImage(tester_cd);
            DataTable TestItem = testerService.TesterSelectTestItem(tester_cd);

            string[] jsonArr = new string[3];

            jsonArr[0] = Public_Function.DataTableToJSON(Parameter);
            jsonArr[1] = Public_Function.DataTableToJSON(TestImage);
            jsonArr[2] = Public_Function.DataTableToJSON(TestItem);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string TesterTRX(Tester tester, HttpPostedFileBase tester_image)
        {
            TesterService testerService = new TesterService();

            string res = testerService.TesterTRX(tester);

            if (tester_image != null)
            {
                saveTesterImage(tester_image, tester.tester_cd, "tester_image");
            }

            return res;
        }

        private void saveTesterImage(HttpPostedFileBase imageFile, string tester_cd, string imageName)
        {
            try
            {
                TesterService testerService = new TesterService();

                var myBytes = GetByteArrayFromImage(imageFile);

                byte[] GetByteArrayFromImage(HttpPostedFileBase file)
                {
                    var length = file.InputStream.Length;
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        fileData = binaryReader.ReadBytes(file.ContentLength);
                    }

                    return fileData;
                }

                testerService.UploadTesterImage(myBytes, tester_cd, imageName);
            }

            catch
            {
                Response.StatusCode = 400;
            }
        }

        [HttpPost]
        public string TesterParameterTRX([JsonBinder] List<Tester.TesterParameter> testerParameter, string tester_cd)
        {
            TesterService testerService = new TesterService();

            string res = testerService.TesterParameterTRX(testerParameter, tester_cd);

            return res;
        }

        [HttpPost]
        public string TestItemTRX([JsonBinder] List<Tester.TestItem> testItems, string tester_cd)
        {
            TesterService testerService = new TesterService();

            string res = testerService.TestItemTRX(testItems, tester_cd);

            return res;
        }

        [HttpGet]
        public JsonResult TesterSelectStandardTester()
        {
            CodeHelpService codeHelpService = new CodeHelpService();
            DataTable standardTesterPopup = codeHelpService.GetCode(CodeHelpType.standardtester, HttpContext.Session["plantCD"].ToString(), string.Empty);

            return Json(Public_Function.DataTableToJSON(standardTesterPopup), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 시헙방법 입력(TestItemmethod)

        public ActionResult TestItemmethod(TestItemmethod.SrchParam param)
        {
            TestItemmethodService testItemmethodService = new TestItemmethodService();
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable testItemMethodData = testItemmethodService.TestItemmethodSelect(param);

            ViewBag.testItemMethodData = Json(Public_Function.DataTableToJSON(testItemMethodData));

            return View();
        }

        public JsonResult TestItemmethodSelect(TestItemmethod.SrchParam param)
        {
            TestItemmethodService testItemmethodService = new TestItemmethodService();

            DataTable testItemMethodData = testItemmethodService.TestItemmethodSelect(param);

            return Json(Public_Function.DataTableToJSON(testItemMethodData), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TestItemmethodSelectTestMethod(string testitem_cd)
        {
            TestItemmethodService testItemmethodService = new TestItemmethodService();

            DataTable testMethodData = testItemmethodService.TestItemmethodSelectTestMethod(testitem_cd);

            return Json(Public_Function.DataTableToJSON(testMethodData), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TestItemmethodTRX([JsonBinder] List<TestItemmethod> testMethods, string testitem_cd, string calculate_nm_master, string calculate_formula_master)
        {
            TestItemmethodService testItemmethodService = new TestItemmethodService();

            string res = testItemmethodService.TestItemUpdate(testitem_cd, calculate_nm_master, calculate_formula_master);

            if (testMethods.Count > 0)
            {
                res = testItemmethodService.TestMethodTRX(testMethods, testitem_cd);
            }

            return Json(new { message = res });
        }

        [HttpGet]
        public JsonResult TestItemmethodSelectReagentPopupData(string use_gb)
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable dt = new DataTable();

            switch (use_gb)
            {
                case "QC110":

                    dt = codeHelpService.GetCode(CodeHelpType.reagent_qc110, HttpContext.Session["plantCD"].ToString(), string.Empty);

                    break;

                case "QC111":

                    dt = codeHelpService.GetCode(CodeHelpType.reagent_qc111, HttpContext.Session["plantCD"].ToString(), string.Empty);

                    break;

                default:

                    dt = codeHelpService.GetCode(CodeHelpType.reagent_A, HttpContext.Session["plantCD"].ToString(), string.Empty);

                    break;
            }

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TestItemmethodSelectSpecialCharacter(string operator_type)
        {
            TestItemmethodService testItemmethodService = new TestItemmethodService();

            DataTable specialCharacter = testItemmethodService.TestItemmethodSelectSpecialCharacter(operator_type);

            return Json(Public_Function.DataTableToJSON(specialCharacter), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TestItemmethodFomulaCheck(string calculate_formula)
        {
            TestItemmethodService testItemmethodService = new TestItemmethodService();

            string res = testItemmethodService.TestItemmethodFomulaCheck(calculate_formula);

            return Json(new { message = res }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TestItemmethodCopy(string testitem_cd, string copy_testitem_cd)
        {
            TestItemmethodService testItemmethodService = new TestItemmethodService();

            string res = testItemmethodService.TestItemmethodCopy(testitem_cd, copy_testitem_cd);

            return Json(new { message = res });
        }

        #endregion


        #region 보관 위치 관리(WorkroomManage)

        public ActionResult WorkroomManage(WorkroomManage workroomManage)
        {
            //WorkroomManageService workroomManageService = new WorkroomManageService();

            //DataTable workroomData = workroomManageService.WorkroomManageSelect(workroomManage);

            //ViewBag.workroomData = Json(Public_Function.DataTableToJSON(workroomData));

            return View();
        }

        public JsonResult WorkroomManageSelect(WorkroomManage workroomManage)
        {
            WorkroomManageService workroomManageService = new WorkroomManageService();

            DataTable workroomData = workroomManageService.WorkroomManageSelect(workroomManage);

            return Json(Public_Function.DataTableToJSON(workroomData));
        }

        [HttpGet]
        public JsonResult WorkroomManageSelectLocation(WorkroomManage workroomManage)
        {
            WorkroomManageService workroomManageService = new WorkroomManageService();

            DataTable workroomData = workroomManageService.WorkroomManageSelectLocation(workroomManage);

            return Json(Public_Function.DataTableToJSON(workroomData), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string WorkroomManageTRX(WorkroomManage WorkroomManage)
        {
            WorkroomManageService workroomManageService = new WorkroomManageService();

            string res = workroomManageService.WorkroomManageLocationTRX(WorkroomManage);

            return res;
        }

        #endregion
    }
}