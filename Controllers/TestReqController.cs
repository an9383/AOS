using HACCP.Libs;
using HACCP.Models.TestReq;
using HACCP.Services.Comm;
using HACCP.Services.SysCom;
using HACCP.Services.TestReq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class TestReqController : Controller
    {
        public ActionResult TestRequestE(TestRequest.SrchParam srchParam)
        {

            return View();
        }

        [HttpGet]
        public JsonResult TestRequestESelect(TestRequest.SrchParam srchParam)
        {
            TestRequestEService testRequestEService = new TestRequestEService();

            DataTable testRequestData = testRequestEService.TestRequestSelect(srchParam);

            return Json(Public_Function.DataTableToJSON(testRequestData), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TestRequestSelectDetail(string testcontrol_id, string test_type, string process_kind)
        {
            TestRequestEService testRequestEService = new TestRequestEService();

            DataTable testRequestSignData = testRequestEService.TestRequestSelectSign(testcontrol_id, test_type, process_kind);
            DataTable testRequestFileData = testRequestEService.TestRequestSelectFile(testcontrol_id);

            string[] jsonArr = new string[2];

            jsonArr[0] = Public_Function.DataTableToJSON(testRequestSignData);
            jsonArr[1] = Public_Function.DataTableToJSON(testRequestFileData);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TestRequestSelectTestItemPopup(string test_type)
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable testItem = codeHelpService.GetCode(CodeHelpType.item_testmaster, HttpContext.Session["plantCD"].ToString(), test_type, "");

            return Json(Public_Function.DataTableToJSON(testItem), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string TestRequestETRX(TestRequest testRequest)
        {
            TestRequestEService testRequestEService = new TestRequestEService();

            string res = testRequestEService.TestRequestETRX(testRequest);

            return res;
        }

        [HttpPost]
        public ActionResult TestRequestDeleteTestResult(string testcontrol_id, string testrequest_no, string process_cd)
        {
            TestRequestEService testRequestEService = new TestRequestEService();

            string res = testRequestEService.TestRequestDeleteTestResult(testcontrol_id, testrequest_no, process_cd);

            return Json( new { message = res } );
        }

        [HttpGet]
        public string TestRequestESignDelegateCheck(string testcontrol_id, string process_kind, string test_type, string emp_cd, string sign_set_dt_id, string program_cd)
        {
            TestRequestEService testRequestEService = new TestRequestEService();

            string res = testRequestEService.TestRequestESignDelegateCheck(testcontrol_id, process_kind, test_type, emp_cd, sign_set_dt_id, program_cd);

            return res;
        }

        [HttpPost]
        public ActionResult TestRequestESignTRX(
            string gubun, string testcontrol_id, string process_kind, string test_type
            , string emp_cd, string sign_set_dt_id, string validation_type, string representative_yn, string program_cd)
        {
            TestRequestEService testRequestEService = new TestRequestEService();

            string res = testRequestEService.TestRequestESignTRX(gubun, testcontrol_id, process_kind, test_type, emp_cd, sign_set_dt_id, validation_type, representative_yn, program_cd);

            return Json(new { message = res });
        }

        [HttpPost]
        public ActionResult TestRequestEUploadFile(string testcontrol_id, string name)
        {
            TestRequestEService testRequestEService = new TestRequestEService();

            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;

                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    testRequestEService.uploadFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), fileName, contentType, testcontrol_id);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }
        
        [HttpGet]
        public ActionResult TestRequestEDownloadFile(string testcontrol_id, string file_id)
        {
            TestRequestEService testRequestEService = new TestRequestEService();
            EquipmentService equipmentService = new EquipmentService();

            DataTable attachmentFileData = testRequestEService.TestRequestEDownloadFile(testcontrol_id, file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0]["file_image"]
                , mimeType
                , attachmentFileData.Rows[0]["doc_name"].ToString()
                );
        }


        [HttpPost]
        public string TestRequestEDeleteFile(string testcontrol_id, string file_id)
        {
            TestRequestEService testRequestEService = new TestRequestEService();

            string res = testRequestEService.TestRequestEDeleteFile(testcontrol_id, file_id);

            return res;
        }

        [HttpGet]
        public JsonResult TestRequestESelectFileList(string testcontrol_id)
        {
            TestRequestEService testRequestEService = new TestRequestEService();

            DataTable testRequestFileData = testRequestEService.TestRequestSelectFile(testcontrol_id);

            return Json(Public_Function.DataTableToJSON(testRequestFileData), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TestRequestEUpdateTestStandard(string testcontrol_id)
        {
            TestRequestEService testRequestEService = new TestRequestEService();

            string res = testRequestEService.TestRequestEUpdateTestStandard(testcontrol_id);

            return Json(new { message = res });
        }

    }
}