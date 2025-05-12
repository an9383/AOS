using HACCP.Attribute;
using HACCP.Libs;
using HACCP.Models.Cp;
using HACCP.Services.Comm;
using HACCP.Services.Cp;
using HACCP.Services.SysCom;
using log4net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class CpController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CpController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private CodeHelpService codeHelpService = new CodeHelpService();
        private EquipmentService equipmentService = new EquipmentService();

        #region ClaimRequest 불만사항 등록

        ClaimRequestService claimRequestService = new ClaimRequestService();

        public ActionResult ClaimRequest()
        {
            //콤보박스
            //진행상태 (상단 검색조건)
            DataTable claim_status = selectBoxService.GetSelectBox("공통코드", "S", "QC044", "claim_status");
            //소비자 불만 항목(입력 폼)
            DataTable claim_item = selectBoxService.GetSelectBox("공통코드", "D", "QM001", "claim_item");
            //회수량(입력 폼)
            DataTable recall_quantity = selectBoxService.GetSelectBox("공통코드", "D", "QM003", "recall_quantity");
            //개봉여부(입력 폼)
            DataTable open_or_not = selectBoxService.GetSelectBox("공통코드", "D", "QM004", "open_or_not");
            //이물종류(입력 폼)
            DataTable substance_kind = selectBoxService.GetSelectBox("공통코드", "D", "QM005", "substance_kind");

            //팝업
            //품목(입력 폼)
            DataTable claimItem = codeHelpService.GetCode(CodeHelpType.claim_makingitem, "", "");
            //거래처(입력 폼)
            DataTable custItem = codeHelpService.GetCode(CodeHelpType.cust, "", "");
            //의뢰자(입력 폼)
            DataTable empItem = codeHelpService.GetCode(CodeHelpType.employee, "", "");
            //관련부서(입력 폼)
            DataTable deptItem = codeHelpService.GetCode(CodeHelpType.department, "", "");

            ViewBag.claim_status = claim_status;
            ViewBag.claim_item = claim_item;
            ViewBag.recall_quantity = recall_quantity;
            ViewBag.open_or_not = open_or_not;
            ViewBag.substance_kind = substance_kind;

            ViewBag.claimItem = Json(Public_Function.DataTableToJSON(claimItem));
            ViewBag.custItem = Json(Public_Function.DataTableToJSON(custItem));
            ViewBag.empItem = Json(Public_Function.DataTableToJSON(empItem));
            ViewBag.deptItem = Json(Public_Function.DataTableToJSON(deptItem));

            ViewBag.ClaimRequestAuth = Json(HttpContext.Session["ClaimRequestAuth"]);

            return View();
        }

        /// <summary>
        /// 불만사항 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRequest_GridSelect(ClaimRequest.SrchParam srch)
        {
            List<ClaimRequest> items = claimRequestService.GridSelect(srch);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }

        /// <summary>
        /// 불만사항 입력
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRequest_GridInsert(ClaimRequest claimRequest)
        {
            string message = claimRequestService.GridInsert(claimRequest);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 불만사항 수정
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRequest_GridUpdate(ClaimRequest claimRequest)
        {
            string message = claimRequestService.GridUpdate(claimRequest);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 불만사항 삭제
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRequest_GridDelete(string claim_id)
        {
            string message = claimRequestService.GridDelete(claim_id);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }


        /// <summary>
        /// 불만사항 번호 생성
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRequest_GetClaimNO()
        {
            string message = claimRequestService.GetNo();
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 불만사항 번호 중복확인
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRequest_CheckNo(string claim_no)
        {
            string message = claimRequestService.CheckNo(claim_no);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }
        #endregion

        #region ClaimRequest 불만사항 정리

        ClaimReceiptService claimReceiptService = new ClaimReceiptService();

        public ActionResult ClaimReceipt()
        {
            //콤보박스
            //진행상태 (상단 검색조건)
            DataTable claim_status = selectBoxService.GetSelectBox("공통코드", "S", "QC044", "claim_status");
            //소비자 불만 항목(입력 폼)
            DataTable claim_item = selectBoxService.GetSelectBox("공통코드", "D", "QM001", "claim_item");

            //팝업
            //의뢰자(입력 폼)
            DataTable empItem = codeHelpService.GetCode(CodeHelpType.employee, "", "");

            ViewBag.claim_status = claim_status;
            ViewBag.claim_item = claim_item;
            ViewBag.empItem = Json(Public_Function.DataTableToJSON(empItem));

            ViewBag.ClaimReceiptAuth = Json(HttpContext.Session["ClaimReceiptAuth"]);

            return View();
        }

        /// <summary>
        /// 불만사항 정리 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimReceipt_GridSelect(ClaimReceipt.SrchParam srch)
        {
            List<ClaimReceipt> items = claimReceiptService.GridSelect(srch);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }

        /// <summary>
        /// 불만사항 정리 입력
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimReceipt_GridInsert(ClaimReceipt claimReceipt)
        {
            string message = claimReceiptService.GridInsert(claimReceipt);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 불만사항 정리 수정
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimReceipt_GridUpdate(ClaimReceipt claimReceipt)
        {
            string message = claimReceiptService.GridUpdate(claimReceipt);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 불만사항 정리 삭제
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimReceipt_GridDelete(string claim_id)
        {
            string message = claimReceiptService.GridDelete(claim_id);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }
        #endregion

        #region ClaimCheck 불만사항 조사 판정

        ClaimCheckService claimCheckService = new ClaimCheckService();
        public ActionResult ClaimCheck()
        {
            //콤보박스
            //진행상태 (상단 검색조건)
            DataTable claim_status = selectBoxService.GetSelectBox("공통코드", "S", "QC044", "claim_status");
            //조사판정(입력 폼)
            DataTable judge_item = selectBoxService.GetSelectBox("공통코드", "N", "QC042", "judge_item");
            //소비자 불만 구분(입력 폼)
            DataTable claim_item = selectBoxService.GetSelectBox("공통코드", "N", "QM001", "claim_item");

            //팝업
            //조사자(입력 폼)
            DataTable empItem = codeHelpService.GetCode(CodeHelpType.employee, "", "");
            //관련부서(입력 폼)
            DataTable deptItem = codeHelpService.GetCode(CodeHelpType.department, "", "");

            ViewBag.claim_status = claim_status;
            ViewBag.claim_item = claim_item;
            ViewBag.judge_item = judge_item;
            ViewBag.empItem = Json(Public_Function.DataTableToJSON(empItem));
            ViewBag.deptItem = Json(Public_Function.DataTableToJSON(deptItem));

            ViewBag.ClaimCheckAuth = Json(HttpContext.Session["ClaimCheckAuth"]);

            return View();
        }

        /// <summary>
        /// 불만사항 조사 판정 리스트 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimCheck_GridSelect(ClaimCheck.SrchParam srch)
        {
            List<ClaimCheck> items = claimCheckService.GridSelect(srch);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }

        /// <summary>
        /// 불만사항 조사 판정 상세조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimCheck_GridSelect1(string claim_id)
        {
            DataTable items = claimCheckService.GridSelect1(claim_id);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }


        /// <summary>
        /// 불만사항 삭제
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimCheck_GridDelete(string claim_id)
        {
            string message = claimCheckService.GridDelete(claim_id);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }


        /// <summary>
        /// 불만사항 조사완료
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimCheck_GridInsert(ClaimCheck claimCheck)
        {
            string message = claimCheckService.GridInsert(claimCheck);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 불만사항 수정
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimCheck_GridUpdate(ClaimCheck claimCheck)
        {
            string message = claimCheckService.GridUpdate(claimCheck);
            var resJson = "{ \"messege\": \"" + message + "\" }";

            return Json(resJson);
        }


        ///// <summary>
        ///// 불만사항 상세 수정
        ///// </summary>        
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult ClaimCheck_GridUpdate1(string claim_id, string claim_dt_id, string judge_value)
        //{
        //    string message = claimCheckService.GridUpdate1(claim_id, claim_dt_id, judge_value);
        //    var resJson = "{ \"messege\": \"" + message + "\" }";

        //    return Json(resJson);
        //}

        /// <summary>
        /// 불만사항 상세 수정
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimCheck_GridUpdate1([JsonBinder] List<ClaimDetailCheck> claimDetailCheck)
        {
            string res = claimCheckService.GridUpdate1(claimDetailCheck);

            return Json("{ \"messege\": \"" + res + "\" }");
        }


        /// <summary>
        /// 불만사항 조사 판정 파일 등록 및 수정
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public ActionResult ClaimCheck_UploadFile(string name, string claim_id, string gubun, string doc_file_id)
        {
            string result = "";
            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                //var fileName_without_extension = Path.GetFileNameWithoutExtension(fileName);
                var contentType = equipmentService.GetMimeType(fileName);
                var fgubun = name.Substring(name.Length - 1, 1);


                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    if (gubun == "F")
                    {
                        //result = claimCheckService.InsertFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), claim_id, fgubun, fileName_without_extension, contentType);
                        result = claimCheckService.InsertFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), claim_id, fgubun, fileName, contentType);
                    }
                    if (gubun == "FE")
                    {
                        //result = claimCheckService.UpdateFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), doc_file_id, fgubun, fileName_without_extension, contentType);
                        result = claimCheckService.UpdateFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), doc_file_id, fgubun, fileName, contentType);
                    }
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }
            //var resJson = "{ \"messege\": \"" + result + "\" }";
            return Json(result);
        }

        /// <summary>
        /// 불만사항 조사 판정 파일 열기
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public ActionResult ClaimCheck_OpenFile(string doc_file_id)
        {

            DataTable attachmentFileData = claimCheckService.GetFile(doc_file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0].ItemArray[0] //byte
                , mimeType
                , attachmentFileData.Rows[0].ItemArray[1].ToString() //파일 full name
                );


            //string message = "";
            //message = claimCheckService.GetFile(doc_file_id);

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            //return Json(resJson);
        }

        /// <summary>
        /// 불만사항 조사 판정 파일 삭제
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public ActionResult ClaimCheck_DeleteFile(string doc_file_id, string fgubun)
        {
            string message = "";
            message = claimCheckService.DeleteFile(doc_file_id, fgubun);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }
        #endregion

        #region ClaimRecognition 처리 결과 승인

        ClaimRecognitionService claimRecognitionService = new ClaimRecognitionService();
        public ActionResult ClaimRecognition()
        {
            //콤보박스
            //진행상태 (상단 검색조건)
            DataTable claim_status = selectBoxService.GetSelectBox("공통코드", "S", "QC044", "claim_status");
            string IsLoginUserCheckAtElectronicSign = claimRecognitionService.GetProgramSet();

            ViewBag.claim_status = claim_status;
            ViewBag.IsLoginUserCheck = IsLoginUserCheckAtElectronicSign;
            ViewBag.ClaimRecognitionAuth = Json(HttpContext.Session["ClaimRecognitionAuth"]);

            return View();
        }

        /// <summary>
        /// 처리 결과 승인 리스트 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRecognition_GridSelect(ClaimRecognition.SrchParam srch)
        {
            List<ClaimRecognition> items = claimRecognitionService.GridSelect(srch);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }

        /// <summary>
        /// 처리 결과 승인 상세조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRecognition_GridSelect1(string claim_id)
        {
            DataTable items = claimRecognitionService.GridSelect1(claim_id);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }

        /// <summary>
        /// 처리 결과 승인 서명자조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRecognition_GetRepresentativeAuthority(string sign_history_id)
        {
            string message = "";
            message = claimRecognitionService.GetRepresentativeAuthority(sign_history_id);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 처리 결과 승인 전자서명부분 삭제
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRecognition_DeleteSign(string claim_id, string delgubun)
        {
            string message = "";
            message = claimRecognitionService.DeleteSign(claim_id, delgubun);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 처리 결과 승인 전자서명 대리자조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRecognition_GetRepresentativeYN(string emp_cd, string sign_set_dt_seq)
        {
            string message = "";
            message = claimRecognitionService.GetRepresentativeYN(emp_cd, sign_set_dt_seq);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 처리 결과 승인 전자서명 자료 저장
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRecognition_InsSignInfo(string sign_emp_cd, string sign_type, string representative_yn, string sign_set_dt_seq, string claim_id, string remark)
        {
            string message = "";
            message = claimRecognitionService.InsSignInfo(sign_emp_cd, sign_type, representative_yn, sign_set_dt_seq, claim_id, remark);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        /// <summary>
        /// 처리 결과 승인 전자서명lookup 데이터
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRecognition_LookupSetting(string claim_id)
        {
            DataTable items = claimRecognitionService.LookupSetting(claim_id);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }

        /// <summary>
        /// 처리 결과 승인 전자서명 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRecognition_ElecSearchData(string sign_history_id)
        {
            DataTable items = claimRecognitionService.ElecSearchData(sign_history_id);
            string result = Public_Function.DataTableToJSON(items);

            return Json(result);
        }

        /// <summary>
        /// 처리 결과 승인 전자서명 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimRecognitionESInfo(string claim_id, string sign_set_cd)
        {
            DataTable signInfoData = claimRecognitionService.ClaimRecognitionESInfo(claim_id, sign_set_cd);

            return ViewBag.signInfoData = Json(Public_Function.DataTableToJSON(signInfoData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        /// <summary>
        /// 처리 결과 승인 파일 열기
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public ActionResult ClaimRecognition_OpenFile(string doc_file_id)
        {

            DataTable attachmentFileData = claimRecognitionService.GetFile(doc_file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0].ItemArray[0] //byte
                , mimeType
                , attachmentFileData.Rows[0].ItemArray[1].ToString() //파일 full name
                );


            //string message = "";
            //message = claimRecognitionService.GetFile(doc_file_id);

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            //return Json(resJson);
        }

        #endregion

        #region ClaimGraph 제품별 접수통계

        ClaimGraphService claimGraphService = new ClaimGraphService();
        public ActionResult ClaimGraph()
        {
            ViewBag.ClaimGraphAuth = Json(HttpContext.Session["ClaimGraphAuth"]);

            return View();
        }

        /// <summary>
        /// 제품별 접수통계 메인 그리드 및 차트 데이터 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimGraph_GridSelect(string start_date, string end_date)
        {
            DataSet items = claimGraphService.GridSelect(start_date, end_date);
            
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }

        /// <summary>
        /// 제품별 접수통계 상세 그리드 및 차트 데이터 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimGraph_GridSelect1(string start_date, string item_cd)
        {
            DataSet items = claimGraphService.GridSelect2(start_date, item_cd);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }
        #endregion

        #region ClaimGraph2 제형별 접수통계

        ClaimGraph2Service claimGraph2Service = new ClaimGraph2Service();

        public ActionResult ClaimGraph2()
        {
            ViewBag.ClaimGraph2Auth = Json(HttpContext.Session["ClaimGraph2Auth"]);

            return View();
        }

        /// <summary>
        /// 제형별 접수통계 메인 그리드 및 차트 데이터 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimGraph2_GridSelect(string start_date, string end_date)
        {
            DataSet items = claimGraph2Service.GridSelect(start_date, end_date);

            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }

        /// <summary>
        /// 제형별 접수통계 상세 그리드 및 차트 데이터 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimGraph2_GridSelect1(string start_date, string item_form_cd)
        {
            DataSet items = claimGraph2Service.GridSelect2(start_date, item_form_cd);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }
        #endregion

        #region ClaimGraph3 구분별 접수통계

        ClaimGraph3Service claimGraph3Service = new ClaimGraph3Service();
        public ActionResult ClaimGraph3()
        {
            ViewBag.ClaimGraph3Auth = Json(HttpContext.Session["ClaimGraph3Auth"]);

            return View();
        }

        /// <summary>
        /// 구분별 접수통계 메인 그리드 및 차트 데이터 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimGraph3_GridSelect(string start_date, string end_date)
        {
            DataSet items = claimGraph3Service.GridSelect(start_date, end_date);

            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }

        /// <summary>
        /// 구분별 접수통계 상세 그리드 및 차트 데이터 조회
        /// </summary>        
        /// <returns></returns>
        [HttpPost]
        public JsonResult ClaimGraph3_GridSelect1(string start_date, string claim_type)
        {
            DataSet items = claimGraph3Service.GridSelect2(start_date, claim_type);
            string result = JsonConvert.SerializeObject(items);

            return Json(result);
        }
        #endregion
    }
}