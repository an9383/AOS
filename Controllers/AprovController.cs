using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HACCP.Attribute;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models.Aprov;
using HACCP.Models.Common;
using HACCP.Services.Aprov;
using HACCP.Services.Comm;
using HACCP.Services.SysCom;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace HACCP.Controllers
{

    public class AprovController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AprovController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private CodeHelpService codeHelpService = new CodeHelpService();
        private EquipmentService equipmentService = new EquipmentService();

        /* 출하승인 Service */
        private ShippingRecognitionService shippingRecognitionService = new ShippingRecognitionService();
        /* 적/부 판정 라벨 Service*/
        private ReleasedOrRejectedLabelService releasedOrRejectedLabelService = new ReleasedOrRejectedLabelService();

        #region TestRecognitionE_Temp 시험성적승인2

        TestRecognitionE_TempService testRecognitionService = new TestRecognitionE_TempService();

        public ActionResult TestRecognitionE_Temp()
        {
            DataTable testType = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "testType");   // 시험종류
            //DataTable progressStatus = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "progressStatus");   // 진행상태
            DataTable fomulation = selectBoxService.GetSelectBox("공통코드", "S", "CM065", "fomulation");   // 제형

            string IsLoginUserCheckAtElectronicSign = testRecognitionService.GetSystemParameter("IsLoginUserCheckAtElectronicSign");

            DataTable programSet = testRecognitionService.GetProgramSet();
            string InitTestNm = "";
            if (programSet.DataSet.Tables[1].Rows.Count > 0)
            {
                InitTestNm = (programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString());
            }
            InitTestNm = (InitTestNm == "" ? "%" : InitTestNm);

            string plantName = testRecognitionService.GetPlantName();

            ViewBag.testType = testType;
            //ViewBag.progressStatus = progressStatus;
            ViewBag.fomulation = fomulation;
            ViewBag.IsLoginUserCheck = IsLoginUserCheckAtElectronicSign;
            ViewBag.InitTestNm = InitTestNm;
            ViewBag.TestRecognitionE_TempAuth = Json(HttpContext.Session["TestRecognitionE_TempAuth"]);
            ViewBag.PlantName = plantName;

            return View();
        }


        /// <summary>
        /// 시험성적승인2 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestRecognition_GridSelect(TestRecognitionE_Temp.SrchParam srch)
        {
            List<TestRecognitionE_Temp> items = testRecognitionService.GridSelect(srch);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }

        /// <summary>
        /// 전자서명 정보 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestRecognition_GetTestSignset(string testcontrol_id, string test_type, string process_kind, string program_cd, string sign_set_cd)
        {
            DataTable items = testRecognitionService.GetTestSignset(testcontrol_id, test_type, process_kind, program_cd, sign_set_cd);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }

        /// <summary>
        /// 전자서명 상세 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestRecognition_GetTestSignInfo(string testcontrol_id, string test_type, string sign_set_dt_id, string process_kind, string program_cd, string sign_set_cd)
        {
            DataTable items = testRecognitionService.GetTestSignInfo(testcontrol_id, test_type, sign_set_dt_id, process_kind, program_cd, sign_set_cd);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }


        /// <summary>
        /// 전자서명 저장
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestRecognition_ElectronicSignature(string testcontrol_id, string test_type, string sign_set_dt_id, string emp_cd, string representative_yn, string validation_type, string process_kind, string program_cd, string sign_set_cd)
        {
            string message = "";
            message = testRecognitionService.ElectronicSignature(testcontrol_id, test_type, sign_set_dt_id, emp_cd, representative_yn, validation_type, process_kind, program_cd, sign_set_cd);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 대리자 권한 여부 조회
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_GetRepresentativeYN(string testcontrol_id, string test_type, string sign_set_dt_id, string emp_cd, string process_kind, string program_cd, string sign_set_cd)
        {
            string message = "";
            message = testRecognitionService.GetRepresentativeYN(testcontrol_id, test_type, sign_set_dt_id, emp_cd, process_kind, program_cd, sign_set_cd);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 현재 단계의 모든 필수 서명이 완료되었는지 확인
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_CheckEnd(string testcontrol_id, string test_type, string process_kind, string program_cd, string sign_set_cd)
        {
            string message = "";
            message = testRecognitionService.CheckEnd(testcontrol_id, test_type, process_kind, program_cd, sign_set_cd);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 시험정보 적/부 판정 등 업데이트
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_UpdateResultYN(TestRecognitionE_Temp testRecognition)
        {
            string message = "";
            message = testRecognitionService.UpdateResultYN(testRecognition);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 시험정보 업데이트
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_SaveCheckInfo(TestRecognitionE_Temp testRecognition)
        {
            string message = "";
            message = testRecognitionService.SaveCheckInfo(testRecognition);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 시험성적 취소
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_CancelCheckInfo(string testcontrol_id, string test_type)
        {
            string message = "";
            message = testRecognitionService.CancelCheckInfo(testcontrol_id, test_type);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 시험성적 취소 - 서명자 하나씩
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_CancelCheckStep(string testcontrol_id, string sign_set_cd, string sign_set_dt_id, string process_kind)
        {
            string message = "";
            message = testRecognitionService.CancelCheckStep(testcontrol_id, sign_set_cd, sign_set_dt_id, process_kind);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 시험정보 업데이트 내용 조회
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_SetTestInfo(string testcontrol_id, string process_kind, string program_cd, string sign_set_cd)
        {
            DataTable items = testRecognitionService.SetTestInfo(testcontrol_id, process_kind, program_cd, sign_set_cd);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }

        /// <summary>
        /// 업로드?
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_InterfaceUpload(string testcontrol_id, string test_type, string create_type)
        {
            string message = "";
            message = testRecognitionService.InterfaceUpload(testcontrol_id, test_type, create_type);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        /// <summary>
        /// 최종서명자 조회
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_GetLastSignEmpCheck(string testcontrol_id, string test_type, string process_kind, string program_cd, string sign_set_cd)
        {
            string message = "";
            message = testRecognitionService.GetLastSignEmpCheck(testcontrol_id, test_type, process_kind, program_cd, sign_set_cd);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 전자서명이 된 품목인지 확인
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_ESStatusCheck(string test_type, string test_status, string process_kind, string program_cd, string sign_set_cd)
        {
            string message = "";
            message = testRecognitionService.ESStatusCheck(test_type, test_status, process_kind, program_cd, sign_set_cd);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 유효일자 조회
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_SetAutoQCValidPeriod(string testcontrol_id)
        {
            string message = "";
            message = testRecognitionService.SetAutoQCValidPeriod(testcontrol_id);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 시험성적승인2 첨부파일 리스트 조회
        /// </summary>        
        /// <returns></returns>
        public JsonResult TestRecognition_GetFileList(string testcontrol_id)
        {
            DataTable items = testRecognitionService.GetFileList(testcontrol_id);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }

        /// <summary>
        /// 시험성적승인2 첨부파일 등록
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public ActionResult TestRecognition_UploadFile(string name, string testcontrol_id)
        {
            string result = "";
            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                //var fileName_without_extension = Path.GetFileNameWithoutExtension(fileName);
                var contentType = equipmentService.GetMimeType(fileName);

                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    //result = testRecognitionService.InsertFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), testcontrol_id, fileName_without_extension, contentType);
                    result = testRecognitionService.InsertFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), testcontrol_id, fileName, contentType);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }
            var resJson = "{ \"messege\": \"" + result + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 시험성적승인2 첨부파일 열기
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestRecognition_OpenFile(string testcontrol_id, string file_id)
        {

            DataTable attachmentFileData = testRecognitionService.OpenFile(testcontrol_id, file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[0].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0].ItemArray[2] //byte
                , mimeType
                , attachmentFileData.Rows[0].ItemArray[0].ToString() //파일 full name
                );


            //string message = "";
            //message = testRecognitionService.OpenFile(testcontrol_id, file_id);

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            //return Json(resJson);
        }


        /// <summary>
        /// 시험성적승인2 첨부파일 삭제
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public ActionResult TestRecognition_DeleteFile(string testcontrol_id, string file_id)
        {
            string message = "";
            message = testRecognitionService.DeleteFile(testcontrol_id, file_id);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        #endregion


        #region 시험성적승인2 Lookup용 컨트롤러

        [System.Web.Http.Route("api/AprovData/{action}", Name = "Aprov")]
        public class AprovDataController : ApiController
        {
            private SelectBoxService selectBoxService = new SelectBoxService();

            [System.Web.Http.HttpGet]
            public HttpResponseMessage TestRecognition_GetProgressStatus(DataSourceLoadOptions loadOptions)
            {
                DataTable dt = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "progressStatus");   // 진행상태

                List<SelectBoxGubun> list = new List<SelectBoxGubun>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i];

                    SelectBoxGubun temp;

                    temp = new SelectBoxGubun(row, false);

                    list.Add(temp);
                }

                return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));
            }
        }
        #endregion


        #region ReleasedOrRejectedLabel 적/부 판정 라벨
        [HttpGet]
        public ActionResult ReleasedOrRejectedLabel(ReleasedOrRejectedLabel model)
        {
            /* 조회 */
            model.date_option = "2"; // (검색) 의뢰일자 1 / 승인일자 2
            model.start_date_S = DateTime.Today.AddDays(-1).ToShortDateString(); // (검색) 시작일
            model.end_date_S = DateTime.Today.ToShortDateString(); // (검색) 종료일
            model.s_test_type = "%"; // (검색) 시험종류 s_test_type
            model.s_test_status = "%"; // (검색) 진행상태 s_test_status
            model.search_gubun = "0";
            model.search_number = "";
            model.s_yn = "%"; // (검색) 판정 -  적합 : Y / 부적합 N

            DataTable dt = releasedOrRejectedLabelService.Select(model);

            ViewBag.ReleasedOrRejectedLabelData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ReleasedOrRejectedLabelAuth = Json(HttpContext.Session["ReleasedOrRejectedLabelAuth"]);

            /* 드랍 박스 */
            DataTable search_test_type = selectBoxService.GetSelectBox("공통코드", "S", "QC004", "search_test_type"); // 시험종류
            DataTable search_test_status = selectBoxService.GetSelectBox("공통코드", "S", "QC007", "search_test_status"); // 진행상태

            ViewBag.search_test_type = search_test_type;
            ViewBag.search_test_status = search_test_status;

            return View();
        }

        public JsonResult ReleasedOrRejectedLabel_Search(ReleasedOrRejectedLabel model)
        {
            if (model.s_test_type == null || model.s_test_type == "")
                model.s_test_type = "%";

            if (model.s_yn == null || model.s_yn == "")
                model.s_yn = "%";

            model.s_test_status = "%"; // (검색) 진행상태 s_test_status
            model.search_gubun = "0";
            model.search_number = "";

            DataTable dt;

            dt = releasedOrRejectedLabelService.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        public JsonResult ReleasedOrRejectedLabel_S2(ReleasedOrRejectedLabel model)
        {
            DataTable dt;

            dt = releasedOrRejectedLabelService.S2(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        public JsonResult ReleasedOrRejectedLabel_S3(ReleasedOrRejectedLabel model)
        {
            DataTable dt;

            dt = releasedOrRejectedLabelService.S3(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        public JsonResult ReleasedOrRejectedLabel_P(ReleasedOrRejectedLabel model)
        {
            DataTable dt;

            dt = releasedOrRejectedLabelService.ReleaseOrReject_print(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpGet]
        public ActionResult ReleasedOrRejectedLabelCheckPrintCount(string testcontrol_id, string test_type, string report_cd)
        {
            bool res = releasedOrRejectedLabelService.ReleasedOrRejectedLabelCheckPrintCount(testcontrol_id, test_type, report_cd);

            return Json(new { isOk = res }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ReleasedOrRejectedLabelGetAuthorEmp(string testcontrol_id, string test_type)
        {
            string res = releasedOrRejectedLabelService.ReleasedOrRejectedLabelGetAuthorEmp(testcontrol_id, test_type);

            return Json(new { emp_cd = res }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ReleasedOrRejectedLabelAddCanPrtCnt(string testcontrol_id, string test_type, string report_cd, string emp_cd)
        {
            string res = releasedOrRejectedLabelService.ReleasedOrRejectedLabelAddCanPrtCnt(testcontrol_id, test_type, report_cd, emp_cd);

            return Json(new { emp_cd = res }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ReleasedOrRejectedLabelAddCount(string testcontrol_id, string test_type, string report_cd)
        {
            string res = releasedOrRejectedLabelService.ReleasedOrRejectedLabelAddCount(testcontrol_id, test_type, report_cd);

            return Json(new { message = res }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ShippingRecognition 출하승인

        [HttpGet]
        public ActionResult ShippingRecognition(ShippingRecognition model)
        {
            /* 조회 */
            model.start_date_S = DateTime.Today.AddDays(-90).ToShortDateString(); // (검색) 시작일
            model.end_date_S = DateTime.Today.ToShortDateString(); // (검색) 종료일
            model.status_S = "%"; // (검색) 상태
            model.date_type_s = "0"; // (검색) 검색날짜 타입

            DataTable dt = shippingRecognitionService.Select(model);

            ViewBag.ShippingRecognitionData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ShippingRecognitionAuth = Json(HttpContext.Session["ShippingRecognitionAuth"]);

            /* 드랍 박스 */
            DataTable search_status = selectBoxService.GetSelectBox("공통코드", "S", "SR001", "search_status");

            /* 팝업 셋팅 */
            DataTable s_item_Popup = shippingRecognitionService.s_item_Popup("CODEHELP"); // 완제품 품목 팝업

            ViewBag.SR_itemCD = Json(Public_Function.DataTableToJSON(s_item_Popup));
            ViewBag.search_status = search_status;

            return View();
        }

        public JsonResult ShippingRecognition_Select_Grid(ShippingRecognition model)
        {
            if (model.status_S == null || model.status_S == "")
                model.status_S = "%";

            DataTable dt;

            dt = shippingRecognitionService.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        public JsonResult ShippingRecognition_SE_Grid(ShippingRecognition model)
        {
            DataTable dt;

            dt = shippingRecognitionService.SE(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        public JsonResult ShippingRecognition_S2_Grid(ShippingRecognition model)
        {
            DataTable dt;

            dt = shippingRecognitionService.S2(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [HttpPost]
        public JsonResult ShippingRecognition_TRX(ShippingRecognition model)
        {
            string res = shippingRecognitionService.TRX(model);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        [HttpPost]
        public JsonResult ShippingRecognition_batch([JsonBinder] List<ShippingRecognition> model, string gubun)
        {
            for (int i = 0; i < model.Count; i++)
            {
                if (model[i].gubun.Equals("U"))
                {
                    model[i].gubun = "U";
                }
            }

            string res = shippingRecognitionService.batch(model, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);

        }

        public JsonResult SignCRUD(ShippingRecognition model)
        {

            string res = shippingRecognitionService.SignCRUD(model);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        [HttpPost]
        public JsonResult SelectShippingItemImage(string shipping_cd)
        {
            DataTable itemlImage = shippingRecognitionService.selectItemImage(shipping_cd);

            return Json(Public_Function.DataTableToJSON(itemlImage));
        }


        [HttpPost]
        public ActionResult UploadShippingImage(string shipping_cd)
        {
            try
            {
                var fileName = "shipping_image";

                using (var binaryReader = new BinaryReader(Request.Files[fileName].InputStream))
                {
                    shippingRecognitionService.uploadImage(binaryReader.ReadBytes(Request.Files[fileName].ContentLength), shipping_cd, fileName);
                }

            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        [HttpPost]
        public JsonResult DeleteShippingItemImage(string shipping_cd)
        {
            string res = shippingRecognitionService.deleteImage(shipping_cd);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        #endregion

    }
}