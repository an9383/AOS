
using HACCP.Attribute;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Change;
using HACCP.Services.Change;
using HACCP.Services.Comm;
using HACCP.Services.SysCom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class ChangeController : Controller
    {

        #region ChangeControlRequest 변경의뢰

        public ActionResult ChangeControlRequest(ChangeRequest.SrchParam param)
        {
            CodeHelpService codeHelpService = new CodeHelpService();
            ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();

            DataTable Employee = codeHelpService.GetCode(CodeHelpType.employee, Public_Function.selectedPLANT, "");
            DataTable ChangeRequest = changeControlRequestService.SelectChangeRequest(param);

            ViewBag.Employee = Json(Public_Function.DataTableToJSON(Employee));
            ViewBag.ChangeRequest = Json(Public_Function.DataTableToJSON(ChangeRequest));

            return View();
        }

        public JsonResult ChangeControlRequestSelect(ChangeRequest.SrchParam param)
        {
            ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();

            DataTable ChangeRequest = changeControlRequestService.SelectChangeRequest(param);

            return Json(Public_Function.DataTableToJSON(ChangeRequest));
        }

        [HttpGet]
        public JsonResult ChangeControlRequestSelectDetail(string changecontrol_no)
        {
            ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();

            DataSet ds = changeControlRequestService.ChangeControlRequestSelectDetail(changecontrol_no, "5101");

            return Json(JsonConvert.SerializeObject(ds), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string ChangeControlRequestTRX(ChangeRequest changeRequest)
        {
            ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();

            //if (changeRequest.gubun.Equals("I"))
            //{
            //    changeRequest = changeControlRequestService.SetChangeControlNo(changeRequest);
            //}

            string res = changeControlRequestService.ChangeControlRequestTRX(changeRequest);

            return res;
        }

        [HttpPost]
        public ActionResult UploadChangeAttachedFile(string changecontrol_no, string name)
        {
            ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();

            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;

                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    changeControlRequestService.uploadFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), fileName, contentType, changecontrol_no);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult ChangeControlRequestDownload(string file_id)
        {
            ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();
            EquipmentService equipmentService = new EquipmentService();

            DataTable attachmentFileData = changeControlRequestService.AttachmentFileDownload(file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0]["file_image"]
                , mimeType
                , attachmentFileData.Rows[0]["doc_name"].ToString()
                );

            //ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();

            //string message = "";
            //message = changeControlRequestService.ChangeControlRequestFileOpen(file_id);


            //var resJson = "{ \"messege\": \"" + message + "\" }";
            //return Json(resJson); ;
        }

        [HttpGet]
        public JsonResult ChangeControlRequestSelectDocList(string changecontrol_no)
        {
            ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();

            DataSet ds = changeControlRequestService.ChangeControlRequestSelectDetail(changecontrol_no, "5101");

            return Json(Public_Function.DataTableToJSON(ds.Tables[0]), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ChangeControlRequestSelectSignList(string changecontrol_no)
        {
            ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();

            DataSet ds = changeControlRequestService.ChangeControlRequestSelectDetail(changecontrol_no, "5101");

            return Json(Public_Function.DataTableToJSON(ds.Tables[1]), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string ChangeControlRequestSignTRX(ChangeRequest changeRequest)
        {
            ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();

            string res = changeControlRequestService.ChangeControlRequestSignTRX(changeRequest);

            return res;
        }

        [HttpPost]
        public string ChangeControlRequestSignReciept(string changecontrol_no)
        {
            ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();

            string res = changeControlRequestService.ChangeControlRequestSignReciept(changecontrol_no);

            return (String.IsNullOrEmpty(res) ? "접수에 실패하였습니다." : "접수에 성공하였습니다.");
        }

        [HttpPost]
        public string ChangeControlDeleteDoc(string changecontrol_no, string changecontrol_attatch_doc_id)
        {
            ChangeControlRequestService changeControlRequestService = new ChangeControlRequestService();

            string res = changeControlRequestService.ChangeControlDeleteDoc(changecontrol_no, changecontrol_attatch_doc_id);

            return res;
        }

        #endregion


        #region ChangeControlObject 변경관리 대상 설정

        public ActionResult ChangeControlObject()
        {
            CodeHelpService codeHelpService = new CodeHelpService();
            ChangeControlObjectService changeControlObjectService = new ChangeControlObjectService();

            DataTable Employee = codeHelpService.GetCode(CodeHelpType.employee, Public_Function.selectedPLANT, "");
            DataTable DepartMent = codeHelpService.GetCode(CodeHelpType.department, Public_Function.selectedPLANT, "");
            DataTable ChangeControlObject = changeControlObjectService.SelectChangeControlObject("%");

            ViewBag.Employee = Json(Public_Function.DataTableToJSON(Employee));
            ViewBag.DepartMent = Json(Public_Function.DataTableToJSON(DepartMent));
            ViewBag.ChangeControlObject = Json(Public_Function.DataTableToJSON(ChangeControlObject));

            return View();
        }

        public JsonResult ChangeControlObjectSelect(string changecontrol_cd)
        {
            ChangeControlObjectService changeControlObjectService = new ChangeControlObjectService();

            DataTable ChangeControlObject = changeControlObjectService.SelectChangeControlObject(changecontrol_cd);

            return Json(Public_Function.DataTableToJSON(ChangeControlObject));
        }

        [HttpGet]
        public JsonResult ChangeControlObjectSelectDetail(string changecontrol_cd)
        {
            ChangeControlObjectService changeControlObjectService = new ChangeControlObjectService();

            DataTable ChangeControlObjectEmp = changeControlObjectService.SelectChangeControlObjectSelectDept(changecontrol_cd);
            DataTable ChangeControlObjectDoc = changeControlObjectService.SelectChangeControlObjectSelectDoc(changecontrol_cd);

            string[] jsonArr = new string[2];

            jsonArr[0] = Public_Function.DataTableToJSON(ChangeControlObjectEmp);
            jsonArr[1] = Public_Function.DataTableToJSON(ChangeControlObjectDoc);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string ChangeControlObjectDeptTRX([JsonBinder]List<ChangeControlObject> paramData)
        {
            ChangeControlObjectService changeControlObjectService = new ChangeControlObjectService();

            string res = changeControlObjectService.ChangeControlObjectDeptTRX(paramData);

            return res;
        }


        [HttpPost]
        public string ChangeControlObjectDocTRX([JsonBinder]List<ChangeControlObject> paramData)
        {
            ChangeControlObjectService changeControlObjectService = new ChangeControlObjectService();

            string res = changeControlObjectService.ChangeControlObjectDocTRX(paramData);

            return res;
        }

        #endregion


        #region ChangeControlSopItem 영향평가 항목 설정

        public ActionResult ChangeControlSopItem()
        {
            ChangeControlSopItemService changeControlSopItemService = new ChangeControlSopItemService();

            DataTable ChangeControlSopItem = changeControlSopItemService.ChangeControlSopItemSelect("%");

            ViewBag.ChangeControlSopItem = Json(Public_Function.DataTableToJSON(ChangeControlSopItem));

            return View();
        }

        public JsonResult ChangeControlSopItemSelect(string changecontrol_sop_item_cd)
        {
            ChangeControlSopItemService changeControlSopItemService = new ChangeControlSopItemService();

            DataTable ChangeControlSopItem = changeControlSopItemService.ChangeControlSopItemSelect(changecontrol_sop_item_cd);

            return Json(Public_Function.DataTableToJSON(ChangeControlSopItem));
        }

        [HttpGet]
        public JsonResult ChangeControlSopItemSelectDetail(string changecontrol_sop_item_cd)
        {
            ChangeControlSopItemService changeControlSopItemService = new ChangeControlSopItemService();

            DataTable ChangeControlSopItemDetail = changeControlSopItemService.ChangeControlSopItemSelectDetail(changecontrol_sop_item_cd);

            return Json(Public_Function.DataTableToJSON(ChangeControlSopItemDetail), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string ChangeControlSopItemTRX([JsonBinder]List<ChangeControlSopItem> paramData)
        {
            ChangeControlSopItemService changeControlSopItemService = new ChangeControlSopItemService();

            string res = changeControlSopItemService.ChangeControlSopItemTRX(paramData);

            return res;
        }

        #endregion


        #region ChangeControlReview 변경검토

        public ActionResult ChangeControlReview(ChangeControlReview.SrchParam srchParam)
        {
            srchParam.sdate = DateTime.Now.AddYears(-20).ToShortDateString();

            CodeHelpService codeHelpService = new CodeHelpService();
            ChangeControlReviewService changeControlReviewService = new ChangeControlReviewService();

            DataTable Employee = codeHelpService.GetCode(CodeHelpType.employee, Public_Function.selectedPLANT, "");
            DataTable ChangeControlReview = changeControlReviewService.SelectChangeControlReview(srchParam);

            ViewBag.Employee = Json(Public_Function.DataTableToJSON(Employee));
            ViewBag.ChangeControlReview = Json(Public_Function.DataTableToJSON(ChangeControlReview));

            ViewData["srch"] = srchParam;

            return View();
        }

        [HttpGet]
        public JsonResult ChangeControlReviewSelectSignGridData(ChangeControlReview changeControlReview, int tabIndex)
        {
            ChangeControlReviewService changeControlReviewService = new ChangeControlReviewService();

            DataTable dt = changeControlReviewService.ChangeControlReviewSelectSignGridData(changeControlReview, tabIndex);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ChangeControlReviewSelectSubGridData(string changecontrol_no, string dept_cd, string changecontrol_review_no)
        {
            ChangeControlReviewService changeControlReviewService = new ChangeControlReviewService();

            DataTable dt1 = changeControlReviewService.ChangeControlReviewSelectDocGridData(changecontrol_no);
            DataTable dt2 = changeControlReviewService.ChangeControlReviewSelectSubGridData(changecontrol_no, dept_cd, changecontrol_review_no);

            string[] jsonArr = new string[2];

            jsonArr[0] = Public_Function.DataTableToJSON(dt1);
            jsonArr[1] = Public_Function.DataTableToJSON(dt2);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeControlReviewSelect(ChangeControlReview.SrchParam srchParam)
        {
            ChangeControlReviewService changeControlReviewService = new ChangeControlReviewService();

            DataTable ChangeControlReview = changeControlReviewService.SelectChangeControlReview(srchParam);

            return Json(Public_Function.DataTableToJSON(ChangeControlReview));
        }

        [HttpGet]
        public JsonResult ChangeControlReviewSelectDocGridData(string changecontrol_no)
        {
            ChangeControlReviewService changeControlReviewService = new ChangeControlReviewService();

            DataTable dt = changeControlReviewService.ChangeControlReviewSelectDocGridData(changecontrol_no);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string ChangeControlReviewSignTRX(ChangeControlReview paramData)
        {
            ChangeControlReviewService changeControlReviewService = new ChangeControlReviewService();

            string res = changeControlReviewService.ChangeControlReviewSignTRX(paramData);

            return res;
        }

        [HttpPost]
        public string ChangeControlReviewTRX(ChangeControlReview paramData)
        {
            ChangeControlReviewService changeControlReviewService = new ChangeControlReviewService();

            string res = changeControlReviewService.ChangeControlReviewTRX(paramData);

            return res;
        }

        [HttpPost]
        public string ChangeControlReviewItemUpdate([JsonBinder]List<ChangeControlReview> paramData)
        {
            ChangeControlReviewService changeControlReviewService = new ChangeControlReviewService();

            string res = changeControlReviewService.ChangeControlReviewItemUpdate(paramData);

            return res;
        }

        #endregion


        #region ChangeControlReceive 변경승인

        public ActionResult ChangeControlReceive(ChangeControlReceive.SrchParam srchParam)
        {
            srchParam.sdate = DateTime.Now.AddYears(-20).ToShortDateString();

            CodeHelpService codeHelpService = new CodeHelpService();
            ChangeControlReceiveService changeControlReceiveService = new ChangeControlReceiveService();

            DataTable Employee = codeHelpService.GetCode(CodeHelpType.employee, Public_Function.selectedPLANT, "");
            DataTable Department = codeHelpService.GetCode(CodeHelpType.department, Public_Function.selectedPLANT, "");
            DataTable ChangeControlReceive = changeControlReceiveService.SelectChangeControlReceive(srchParam);

            ViewBag.Employee = Json(Public_Function.DataTableToJSON(Employee));
            ViewBag.Department = Json(Public_Function.DataTableToJSON(Department));
            ViewBag.ChangeControlReceive = Json(Public_Function.DataTableToJSON(ChangeControlReceive));

            ViewData["ChangeControlReceiveSrch"] = srchParam;

            return View();
        }

        public JsonResult ChangeControlReceiveSelect(ChangeControlReceive.SrchParam srchParam)
        {
            ChangeControlReceiveService changeControlReceiveService = new ChangeControlReceiveService();

            DataTable ChangeControlReceive = changeControlReceiveService.SelectChangeControlReceive(srchParam);

            return Json(Public_Function.DataTableToJSON(ChangeControlReceive));
        }

        [HttpGet]
        public JsonResult ChangeControlReceiveSelectDetailData(string changecontrol_no, string changecontrol_cd)
        {
            ChangeControlReviewService changeControlReviewService = new ChangeControlReviewService();
            ChangeControlReceiveService changeControlReceiveService = new ChangeControlReceiveService();

            DataTable DocData = changeControlReviewService.ChangeControlReviewSelectDocGridData(changecontrol_no);
            DataTable SignData = changeControlReceiveService.ChangeControlReceiveSelectSignGridData(changecontrol_no, "5104");
            DataTable ReviewResult = changeControlReceiveService.ChangeControlReceiveSelectReviewResultData(changecontrol_no);
            DataTable ChangeOrder = changeControlReceiveService.ChangeControlReceiveSelectChangeOrderData(changecontrol_no);
            DataTable ChangeReviewDoc = changeControlReceiveService.ChangeControlReceiveSelectChangeReviewDocData(changecontrol_cd);

            List<string> JsonList = new List<string>();

            JsonList.Add(Public_Function.DataTableToJSON(DocData));             // 문서
            JsonList.Add(Public_Function.DataTableToJSON(SignData));            // 사인
            JsonList.Add(Public_Function.DataTableToJSON(ReviewResult));        // 검토결과
            JsonList.Add(Public_Function.DataTableToJSON(ChangeReviewDoc));     // 변경검토문서
            JsonList.Add(Public_Function.DataTableToJSON(ChangeOrder));         // 변경지시

            return Json(JsonConvert.SerializeObject(JsonList), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ChangeControlReceiveSelectSignGridData(string changecontrol_no)
        {
            ChangeControlReceiveService changeControlReceiveService = new ChangeControlReceiveService();

            DataTable SignData = changeControlReceiveService.ChangeControlReceiveSelectSignGridData(changecontrol_no, "5104");

            return Json(Public_Function.DataTableToJSON(SignData), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string ChangeControlReceiveSignTRX(ChangeControlReceive changeControlReceive)
        {
            ChangeControlReceiveService changeControlReceiveService = new ChangeControlReceiveService();

            string res = changeControlReceiveService.ChangeControlReceiveSignTRX(changeControlReceive);

            return res;
        }

        [HttpPost]
        public string ChangeControlReceiveTRX(ChangeControlReceive changeControlReceive)
        {
            ChangeControlReceiveService changeControlReceiveService = new ChangeControlReceiveService();

            string res = changeControlReceiveService.ChangeControlReceiveTRX(changeControlReceive);

            return res;
        }

        #endregion


        #region ChangeControlCompletion  변경 진행/완료

        public ActionResult ChangeControlCompletion(ChangeControlCompletion.SrchParam srchParam)
        {
            srchParam.sdate = DateTime.Now.AddYears(-20).ToShortDateString();

            CodeHelpService codeHelpService = new CodeHelpService();
            ChangeControlCompletionService changeControlCompletionService = new ChangeControlCompletionService();

            DataTable Employee = codeHelpService.GetCode(CodeHelpType.employee, Public_Function.selectedPLANT, "");
            DataTable ChangeControlCompletion = changeControlCompletionService.SelectChangeControlCompletion(srchParam);

            ViewBag.Employee = Json(Public_Function.DataTableToJSON(Employee));
            ViewBag.ChangeControlCompletion = Json(Public_Function.DataTableToJSON(ChangeControlCompletion));

            ViewData["ChangeControlCompletionSrch"] = srchParam;

            return View();
        }

        [HttpGet]
        public JsonResult ChangeControlCompletionSelectDetailData(string changecontrol_no)
        {
            ChangeControlReviewService changeControlReviewService = new ChangeControlReviewService();
            ChangeControlCompletionService changeControlCompletionService = new ChangeControlCompletionService();

            DataTable DocData = changeControlReviewService.ChangeControlReviewSelectDocGridData(changecontrol_no);
            DataTable SignData = changeControlCompletionService.ChangeControlCompletionSelectSignGridData(changecontrol_no, "5105");
            DataTable DetailData = changeControlCompletionService.ChangeControlCompletionSelectDetailData(changecontrol_no);

            List<string> JsonList = new List<string>();

            JsonList.Add(Public_Function.DataTableToJSON(DocData));             // 문서
            JsonList.Add(Public_Function.DataTableToJSON(SignData));            // 사인
            JsonList.Add(Public_Function.DataTableToJSON(DetailData));

            return Json(JsonConvert.SerializeObject(JsonList), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeControlCompletionSelect(ChangeControlCompletion.SrchParam srchParam)
        {
            ChangeControlCompletionService changeControlCompletionService = new ChangeControlCompletionService();

            DataTable ChangeControlCompletion = changeControlCompletionService.SelectChangeControlCompletion(srchParam);

            return Json(Public_Function.DataTableToJSON(ChangeControlCompletion));
        }

        [HttpPost]
        public string ChangeControlCompletionSignTRX(ChangeControlCompletion changeControlCompletion)
        {
            ChangeControlCompletionService changeControlCompletionService = new ChangeControlCompletionService();

            string res = changeControlCompletionService.ChangeControlCompletionSignTRX(changeControlCompletion);

            return res;
        }

        [HttpPost]
        public string ChangeControlCompletionTRX(ChangeControlCompletion changeControlCompletion)
        {
            ChangeControlCompletionService changeControlCompletionService = new ChangeControlCompletionService();

            string res = changeControlCompletionService.ChangeControlCompletionTRX(changeControlCompletion);

            return res;
        }

        #endregion

    }

}