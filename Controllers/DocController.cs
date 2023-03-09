using HACCP.Attribute;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models;
using HACCP.Models.Doc;
using HACCP.Services.Comm;
using HACCP.Services.Doc;
using HACCP.Services.SysCom;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;

/// <summary>
/// DocController
/// -내용:
///  문서관리
/// -최초생성일 : 2020.05.25
/// -작성자 : 김남수
/// </summary>
namespace HACCP.Controllers
{
    public class DocController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DocController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private CodeHelpService codeHelpService = new CodeHelpService();

        #region Gmp_doc_system_manage 문서체계관리

        private GmpDocSystemManageService gmp_doc_system_manageService = new GmpDocSystemManageService();

        /// <summary>
        /// Gmp_doc_system_manage()
        /// HACCP관리-문서관리-문서체계관리, HttpGet
        /// </summary>
        /// <param name="pSelect_S"></param>
        /// <returns></returns>
        [CheckSession]
        [Route("Doc/Gmp_doc_system_manage")]
        [HttpGet]
        public ActionResult Gmp_doc_system_manage(string pSelect_S)
        {
            // 입력 파라미터 값이 null 이거나 공백일 경우, "전체"모드 (= 1)로 설정
            if (pSelect_S == null || pSelect_S == "")
            {
                pSelect_S = "1";
            }

            log.Info("문서체계관리 구분:" + pSelect_S);
            GmpDocSystemManageService gmp_doc_system_manageService = new GmpDocSystemManageService();
            List<DocStructure> gmp_doc_system_manage = gmp_doc_system_manageService.Select(pSelect_S);
            ViewBag.Gmp_doc_system_manage = gmp_doc_system_manage;
            ViewBag.Gmp_doc_system_manageAuth = Json(HttpContext.Session["Gmp_doc_system_manageAuth"]);

            return View();
        }

        /// <summary>
        /// SearchGmp_doc_system_manage()
        /// HACCP관리-문서관리-문서체계관리, HttpPost
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("Doc/SearchGmp_doc_system_manage")]
        [HttpPost]
        public JsonResult SearchGmp_doc_system_manage(string select_S)
        {
            string sSelect_S = "1";
            if (select_S == null || select_S == "")
            {
                sSelect_S = "1";
            }
            else
            {
                sSelect_S = select_S;
            }
            GmpDocSystemManageService gmp_doc_system_manageService = new GmpDocSystemManageService();
            List<DocStructure> gmp_doc_system_manage = gmp_doc_system_manageService.Select(sSelect_S);
            string result = "";
            result = JsonConvert.SerializeObject(gmp_doc_system_manage);
            return Json(result);
        }

        [CheckSession]
        [Route("Doc/SaveGmp_doc_system_manage")]
        [HttpPost]
        public JsonResult SaveGmp_doc_system_manage(DocStructure docStructure)
        {
            string res = gmp_doc_system_manageService.Save(docStructure);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [Route("Doc/DeleteGmp_doc_system_manage")]
        [HttpPost]
        public JsonResult DeleteGmp_doc_system_manage(string pStructureCd)
        {

            string res = gmp_doc_system_manageService.Delete(pStructureCd);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }
        #endregion


        #region Gmp_doc_registration 문서등록

        private GmpDocRegistrationService gmpDocRegistrationService = new GmpDocRegistrationService();

        [CheckSession]
        [HttpGet]
        public ActionResult Gmp_doc_registration()
        {
            DataTable storageLocation = selectBoxService.GetSelectBox("공통코드", "D", "GD006", "storageLocation"); //보관장소 
            DataTable documentGubun = selectBoxService.GetSelectBox("공통코드", "D", "GD004", "documentGubun"); //문서구분
            DataTable documentClassification = selectBoxService.GetSelectBox("공통코드", "D", "GD005", "documentClassification"); //문서분류
            DataTable relevantRegulations = selectBoxService.GetSelectBox("공통코드", "D", "GD007", "relevantRegulations"); //관련규정
            DataTable documentCycle = selectBoxService.GetSelectBox("공통코드", "D", "GD003", "documentCycle"); //검토주기
            DataTable currentYN = selectBoxService.GetSelectBox("공통코드", "D", "GD077", "currentYN"); //개정관리 탭 > 사용여부 
            DataTable ccpRptList = selectBoxService.GetSelectBox("공통코드", "D", "REPORT", "ccpRptList"); //CCP일지 사용시, 일지구분 코드

            DataTable docRegisterDocumentData = gmpDocRegistrationService.GridSelect("", "Y", "2", "", ""); //전체 트리리스트
            DataTable docRegisterGroup = gmpDocRegistrationService.SelectGroupEmp("GT"); //그룹 리스트(탭)
            DataTable docRegisterEmp = gmpDocRegistrationService.SelectGroupEmp("ET"); //사원 리스트(탭)
            //DataTable empPopupData = gmpDocRegistrationService.GetEmpPopupData();
            DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");
            DataTable deptPopupData = codeHelpService.GetCode(CodeHelpType.department, "", "");

            // 박가희
            // 2020.08.28 파일 관련 파라미터 관리 위해 추가함(기존 파라미터 관리 화면에서 관리했지만, 현재 없어서 공통코드로 관리)
            DataTable dt1 = selectBoxService.GetSelectBox("공통코드", "J", "PARAM", "File_editable_ck", "param1");
            DataTable dt2 = selectBoxService.GetSelectBox("공통코드", "J", "PARAM", "DocRecord_ElecSignuse_yn", "param3");

            string file_editable_ck = (dt1.Rows.Count > 0) ? dt1.Rows[0].ItemArray[0].ToString() : "";
            string docRecord_elecsignuse_yn = (dt2.Rows.Count > 0) ? dt2.Rows[0].ItemArray[0].ToString() : "";

            // 2020.10.08 파일 관련 파라미터 조회 수정(파라미터 관리 화면 연동시 주석풀기)
            //string file_editable_ck = gmpDocRegistrationService.GetProgramSet("File_editable_ck");
            //string docRecord_elecsignuse_yn = gmpDocRegistrationService.GetProgramSet("DocRecord_ElecSignuse_yn");

            //lookup
            ViewBag.storageLocation = storageLocation;
            ViewBag.documentGubun = documentGubun;
            ViewBag.documentClassification = documentClassification;
            ViewBag.relevantRegulations = relevantRegulations;
            ViewBag.documentCycle = documentCycle;
            ViewBag.currentYN = currentYN;
            ViewBag.ccpRptList = ccpRptList;

            //그리드 
            ViewBag.docRegisterDocumentData = Json(Public_Function.DataTableToJSON(docRegisterDocumentData));         
            ViewBag.docRegisterGroup = Json(Public_Function.DataTableToJSON(docRegisterGroup));
            ViewBag.docRegisterEmp = Json(Public_Function.DataTableToJSON(docRegisterEmp));
            
            //팝업
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.deptPopupData = Json(Public_Function.DataTableToJSON(deptPopupData));

            //권한
            ViewBag.file_editable_ck = file_editable_ck;
            ViewBag.docRecord_elecsignuse_yn = docRecord_elecsignuse_yn;
            ViewBag.gmpDocRegistrationAuth = Json(HttpContext.Session["Gmp_doc_registrationAuth"]);

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_SearchRevision(string doc_no)
        {

            DataTable docRevisionData = gmpDocRegistrationService.SearchRevision(doc_no);
            string result = Public_Function.DataTableToJSON(docRevisionData);

            return Json(result);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_SearchGrid(string doc_nm, string use_yn, string s_gubun, string parent_cd, string writer_dept_cd)
        {

            DataTable docReisterData = gmpDocRegistrationService.GridSelect(doc_nm, use_yn, s_gubun, parent_cd, writer_dept_cd);
            string result = Public_Function.DataTableToJSON(docReisterData);

            return Json(result);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_DoclineSearch(string doc_no)
        {
            DataTable item = gmpDocRegistrationService.DoclineSearch(doc_no);

            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_DoclineDetailSearch(string sign_set_cd)
        {
            DataTable item = gmpDocRegistrationService.DoclineDetailSearch(sign_set_cd);

            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_RecordlineSearch(string doc_no)
        {
            DataTable item = gmpDocRegistrationService.RecordlineSearch(doc_no);

            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_RecordlineDetailSearch(string sign_set_cd)
        {
            DataTable item = gmpDocRegistrationService.RecordlineDetailSearch(sign_set_cd);

            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_RecordReaderSearch(string doc_no)
        {
            DataTable item = gmpDocRegistrationService.RecordReaderSearch(doc_no);

            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_DocReaderSearch(string doc_no)
        {
            DataTable item = gmpDocRegistrationService.DocReaderSearch(doc_no);

            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_GetDocNo(string structure_cd)
        {

            string result = "";

            result = gmpDocRegistrationService.GetDocNo(structure_cd);

            return Json(result);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_CheckRecord(string doc_no)
        {
            string result = "";

            bool check = gmpDocRegistrationService.CheckRecord(doc_no);
            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_ApprovalCheck(string doc_no, string revision_no)
        {
            string result = "";

            bool check = gmpDocRegistrationService.ApprovalCheck(doc_no, revision_no);
            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_CheckDist(string doc_no, string revision_no)
        {
            string result = "";

            bool check = gmpDocRegistrationService.CheckDist(doc_no, revision_no);
            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_DeleteRevision(string doc_no, string revision_no)
        {
            string result = gmpDocRegistrationService.DeleteRevision(doc_no, revision_no);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_GridDelete(string doc_no)
        {

            string result = gmpDocRegistrationService.GridDelete(doc_no);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }



        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_GridInsert(DocRegister docRegister)
        {
            string result = gmpDocRegistrationService.GridInsert(docRegister);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_GridEdit(DocRegister docRegister)
        {

            string result = gmpDocRegistrationService.GridEdit(docRegister);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_InsertRevisionData(DocRevision docRevision)
        {
            string result = gmpDocRegistrationService.InsertRevisionData(docRevision);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_InsertApprovalLine(string sign_set_cd, string doc_no, string revision_no)
        {
            string result = gmpDocRegistrationService.InsertApprovalLine(sign_set_cd, doc_no, revision_no);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public void Gmp_doc_registration_InsertFileData(string main_file_id, string reference_file_id1, string reference_file_id2, string reference_file_id3, string reference_file_id4, string doc_no, string revision_no)
        {
            gmpDocRegistrationService.InsertFileData(main_file_id, reference_file_id1, reference_file_id2, reference_file_id3, reference_file_id4, doc_no, revision_no);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_InsertToEmpower(string doc_no, string group_emp_ck, string group_emp_cd, string pGubun)
        {
            string result = gmpDocRegistrationService.InsertToEmpower(doc_no, group_emp_ck, group_emp_cd, pGubun);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_DeleteToEmpower(string doc_no, string doc_record_ck, string group_emp_ck, string group_emp_cd)
        {
            string result = gmpDocRegistrationService.DeleteToEmpower(doc_no, doc_record_ck, group_emp_ck, group_emp_cd);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);

        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_UpdateRevisionData(DocRevision docRevision)
        {
            string result = gmpDocRegistrationService.UpdateRevisionData(docRevision);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_UseSelect(string doc_no, string revision_no, string use_select)
        {
            string result = gmpDocRegistrationService.UseSelect(doc_no, revision_no, use_select);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_UpdateConfirmDate(string doc_no, string confirm_date, string archive_period, string archive_period_unit)
        {
            string result = gmpDocRegistrationService.UpdateConfirmDate(doc_no, confirm_date, archive_period, archive_period_unit);

            var resJson = "{ \"messege\": \"" + result + "\" }";

            return Json(resJson);
        }


        [CheckSession]
        [HttpPost]
        public string Gmp_doc_registration_CheckVoidDate(string voidDate)
        {
            string res = gmpDocRegistrationService.ChkVoidDate(voidDate);

            return res;
        }

        [CheckSession]
        [HttpPost]
        public void Gmp_doc_registration_DeleteSignLine(string sign_set_cd, string revision_no, string doc_no, string gubun_D)
        {
            gmpDocRegistrationService.DeleteSignLine(sign_set_cd, revision_no, doc_no, gubun_D);

        }

        [CheckSession]
        [HttpPost]
        public void Gmp_doc_registration_UpdateSignLine_1(string sign_set_cd, string revision_no, string doc_no)
        {
            gmpDocRegistrationService.UpdateSignLine_1(sign_set_cd, revision_no, doc_no);

        }


        [CheckSession]
        [HttpPost]
        public void Gmp_doc_registration_UpdateSignLine_2(string sign_set_cd, string doc_no)
        {
            gmpDocRegistrationService.UpdateSignLine_2(sign_set_cd, doc_no);

        }

        [CheckSession]
        [HttpPost]
        public ActionResult Gmp_doc_registration_UploadFile(string name, string doc_no, string revision_no, string doc_file_id, string gubun)
        {
            string result = "";
            try
            {
                var uploadFile = Request.Files[name];

                var fileName = uploadFile.FileName;
                //var fileName_without_extension = Path.GetFileNameWithoutExtension(fileName);
                //var contentType = uploadFile.ContentType;
                var contentType = equipmentService.GetMimeType(fileName);
                var fgubun = name.Substring(name.Length - 1, 1);


                using (var binaryReader = new BinaryReader(Request.Files[name].InputStream))
                {
                    if (gubun == "F")
                    {
                        //result = gmpDocRegistrationService.InsertFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), doc_no, revision_no, fgubun, fileName_without_extension, contentType);
                        result = gmpDocRegistrationService.InsertFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), doc_no, revision_no, fgubun, fileName, contentType);

                    }
                    if (gubun == "FE")
                    {
                        //result = gmpDocRegistrationService.UpdateFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), doc_file_id, fgubun, fileName_without_extension, contentType);
                        result = gmpDocRegistrationService.UpdateFile(binaryReader.ReadBytes(Request.Files[name].ContentLength), doc_file_id, fgubun, fileName, contentType);

                    }
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }
            //var resJson = "{ \"messege\": \"" + result + "\" }";
            //return Json(resJson);
            return Json(result);
        }

        //[CheckSession]
        //[HttpPost]
        //public ActionResult Gmp_doc_registration_UploadFile(DocRevision doc)
        //{
        //    string message = "";

        //    try
        //    {
        //        var uploadFile = Request.Files[0];

        //        var fileName = uploadFile.FileName;
        //        var docName = Path.GetFileNameWithoutExtension(fileName);
        //        var extension = Path.GetExtension(fileName);

        //        //var contentType = uploadFile.ContentType;

        //        using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
        //        {
        //            if (doc.gubun == "F")
        //            {
        //                //gmpDocRegistrationService.InsertFile(binaryReader.ReadBytes(Request.Files[0].ContentLength), doc.doc_no, doc.revision_no, doc.fgubun, fileName, contentType);
        //                message = gmpDocRegistrationService.InsertFile(binaryReader.ReadBytes(Request.Files[0].ContentLength), doc.doc_no, doc.revision_no, doc.fgubun, docName, extension);
        //            }
        //            if (doc.gubun == "FE")
        //            {
        //                //gmpDocRegistrationService.UpdateFile(binaryReader.ReadBytes(Request.Files[0].ContentLength), doc.doc_file_id, doc.fgubun, fileName, contentType);
        //                message = gmpDocRegistrationService.UpdateFile(binaryReader.ReadBytes(Request.Files[0].ContentLength), doc.doc_file_id, doc.fgubun, docName, extension);

        //            }
        //        }
        //    }
        //    catch
        //    {
        //        Response.StatusCode = 400;
        //    }

        //    var resJson = "{ \"messege\": \"" + message + "\" }";
        //    return Json(resJson);
        //}

        [CheckSession]
        [HttpGet]
        public ActionResult Gmp_doc_registration_GetFile(string doc_file_id)
        {
            DataTable attachmentFileData = gmpDocRegistrationService.GetFile(doc_file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0].ItemArray[0] //byte
                , mimeType
                , attachmentFileData.Rows[0].ItemArray[1].ToString() //파일 full name
                );


            //string message = "";
            //message = gmpDocRegistrationService.GetFile(doc_file_id);

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            //return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_registration_DeleteFile(string doc_no, string revision_no, string fgubun, string doc_file_id)
        {
            string result = gmpDocRegistrationService.DeleteFile(doc_no, revision_no, fgubun, doc_file_id);

            var resJson = "{ \"messege\": \"" + result + "\" }";
            return Json(resJson);
        }

        #endregion

        #region Gmp_doc_record_search 문서조회

        private GmpDocRecordSearchService gmpDocRecordSearchService = new GmpDocRecordSearchService();
        private EquipmentService equipmentService = new EquipmentService();

        [CheckSession]
        [HttpGet]
        public ActionResult Gmp_doc_record_search()
        {
            // 박가희
            // 2020.08.20 파일 관련 파라미터 관리 위해 추가함(기존 파라미터 관리 화면에서 관리했지만, 현재 없어서 공통코드로 관리)
            DataTable dt1 = selectBoxService.GetSelectBox("공통코드", "J", "PARAM", "Elect_check_ck", "param1");
            DataTable dt2 = selectBoxService.GetSelectBox("공통코드", "J", "PARAM", "File_editable_ck", "param2");

            string elect_check_ck = (dt1.Rows.Count > 0) ? dt1.Rows[0].ItemArray[0].ToString() : "";
            string file_editable_ck = (dt2.Rows.Count > 0) ? dt2.Rows[0].ItemArray[0].ToString() : "";

            // 2020.10.08 파일 관련 파라미터 조회 수정(파라미터 관리 화면 연동시 주석풀기)
            //string elect_check_ck = gmpDocRecordSearchService.GetProgramSet("Elect_check_ck");
            //string file_editable_ck = gmpDocRecordSearchService.GetProgramSet("File_editable_ck");

            ViewBag.elect_check_ck = elect_check_ck;
            ViewBag.file_editable_ck = file_editable_ck;

            return View();
        }


        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_record_search_SearchDocRecord_1(string elect_check_ck, string tabGubun, string start, string end, string author, string docName)
        {
            // 문서 조회
            DataTable item = gmpDocRecordSearchService.SearchDocRecord_1(elect_check_ck, tabGubun, start, end, author, docName);

            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_record_search_SearchDocRecord_2(string elect_check_ck, string start, string end, string author, string docName)
        {
            //기록서 조회
            DataTable item = gmpDocRecordSearchService.SearchDocRecord_2(elect_check_ck, start, end, author, docName);

            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }


        [CheckSession]
        [HttpPost]
        public string Gmp_doc_record_search_CheckVoidDate(string voidDate)
        {
            string res = gmpDocRecordSearchService.CheckVoidDate(voidDate);

            return res;
        }

        [CheckSession]
        [HttpGet]
        public ActionResult Gmp_doc_record_search_GetFile(string doc_file_id)
        {

            DataTable attachmentFileData = gmpDocRecordSearchService.GetFile(doc_file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[0].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0].ItemArray[1] //byte
                , mimeType
                , attachmentFileData.Rows[0].ItemArray[0].ToString() //파일 full name
                );


            //string message = "";
            //message = gmpDocRecordSearchService.GetFile(doc_file_id);

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            //return Json(resJson);
        }


        #endregion

        #region Gmp_doc_distribute_check 문서 배포 확인
        private GmpDocDistributeCheckService gmpDocDistributeCheckService = new GmpDocDistributeCheckService();

        [CheckSession]
        [HttpGet]
        public ActionResult Gmp_doc_distribute_check()
        {
            string moduleType, signCode = "";
            DataTable status = selectBoxService.GetSelectBox("공통코드", "S", "GD002", "status"); //상태
            DataTable programSet = gmpDocDistributeCheckService.GetProgramSet();
            moduleType = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString(); //module_type
            signCode = programSet.DataSet.Tables[1].Rows[1].ItemArray[1].ToString(); //sign_code

            ViewBag.Gmp_doc_distribute_checkAuth = Json(HttpContext.Session["Gmp_doc_distribute_checkAuth"]);
            ViewBag.status = status;
            ViewBag.moduleType = moduleType;
            ViewBag.signCode = signCode;

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_distribute_check_SearchDocDistribute(string start, string end, string status, string gubun)
        {
            DataTable item = gmpDocDistributeCheckService.Search(start, end, status, gubun);

            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }

        [CheckSession]
        [HttpGet]
        public ActionResult Gmp_doc_distribute_check_GetFile(string doc_file_id)
        {

            DataTable attachmentFileData = gmpDocDistributeCheckService.GetFile(doc_file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[0].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0].ItemArray[2] //byte
                , mimeType
                , attachmentFileData.Rows[0].ItemArray[0].ToString() //파일 full name
                );


            //string message = "";
            //message = gmpDocDistributeCheckService.GetFile(doc_file_id);

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            //return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_distribute_check_SettingSign(string distId, string moduleType, string signCode, string signSeq)
        {
            DataTable item = gmpDocDistributeCheckService.SettingSign(distId, moduleType, signCode);
            DataRow row = null;

            if (item.Rows.Count > 0)
            {
                int index = 0;
                if (string.IsNullOrEmpty(signSeq))
                {
                    for (int i = item.Rows.Count - 1; i >= 0; i--)
                    {
                        if (item.Rows[i]["sign_yn"].ToString() == "Y")
                        {
                            index = i;
                            break;
                        }

                        if (i == 0)
                        {
                            index = i;
                            break;
                        }
                    }
                }
                else
                    index = int.Parse(signSeq) - 1;

                if (index >= 0)
                {
                    row = item.Rows[index];
                }
            }          

            string jsonData = JsonConvert.SerializeObject(row.Table);
            return Json(jsonData);

        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_distribute_check_SignerSearch(string distId, string moduleType, string signCode, string signSeq)
        {
            DataTable item = gmpDocDistributeCheckService.SignerSearch(distId, moduleType, signCode, signSeq);
            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_distribute_check_GetModifiableAuthority(string distId, string moduleType, string emp_cd)
        {
            string result = "";

            bool check = gmpDocDistributeCheckService.GetModifiableAuthority(distId, moduleType, emp_cd);
            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_distribute_check_CancelSign(string distId, string moduleType, string signSeq, string docNo, string revisionNo)
        {
            string result = "";

            bool check = gmpDocDistributeCheckService.CancelSign(distId, moduleType, signSeq, docNo, revisionNo);

            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_distribute_check_SaveSign(string distId, string moduleType, string signCode, string signSeq, string emp_cd, string validation_type, string representative_yn, string docNo, string revisionNo)
        {
            string result = "";

            bool check = gmpDocDistributeCheckService.SaveSign(distId, moduleType, signCode, signSeq, emp_cd, validation_type, representative_yn, docNo, revisionNo);

            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_distribute_check_GetRepresentativeAuthority(string emp_cd, string signSeq, string signCode)
        {
            string result = "";

            bool check = gmpDocDistributeCheckService.GetRepresentativeAuthority(emp_cd, signSeq, signCode);

            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }

        #endregion

        #region Gmp_doc_manage 배포/폐기 관리
        private GmpDocManageService gmpDocManageService = new GmpDocManageService();

        [CheckSession]
        [HttpGet]
        public ActionResult Gmp_doc_manage()
        {
            string moduleType, signCode = "";
            DataTable item = gmpDocManageService.getEmpData();
            string empData = Public_Function.DataTableToJSON(item);

            DataTable programSet = gmpDocManageService.GetProgramSet();
            moduleType = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString(); //module_type
            signCode = programSet.DataSet.Tables[1].Rows[1].ItemArray[1].ToString(); //sign_code

            ViewBag.Gmp_doc_manageAuth = Json(HttpContext.Session["Gmp_doc_manageAuth"]);
            ViewBag.empData = Json(empData);
            ViewBag.moduleType = moduleType;
            ViewBag.signCode = signCode;

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_manage_Search(string doc_nm_S, string search_gubun_S, string search_select, string s_gubun_S)
        {
            DataTable item = gmpDocManageService.Search(doc_nm_S, search_gubun_S, search_select, s_gubun_S);
            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }
        
        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_manage_GetLookupData(string doc_no)
        {
            DataTable item = gmpDocManageService.GetLookupData(doc_no);
            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }
        
        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_manage_GetRevisionData(string doc_no, string revision_no)
        {
            DataTable item = gmpDocManageService.GetRevisionData(doc_no, revision_no);
            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }
        
        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_manage_GridSelect1(string doc_no, string revision_no)
        {
            DataTable item = gmpDocManageService.GridSelect1(doc_no, revision_no);
            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }
        
        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_manage_GridSelect2(string doc_no, string revision_no, string dist_id)
        {
            DataTable item = gmpDocManageService.GridSelect2(doc_no, revision_no, dist_id);
            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }
       
        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_manage_GetOverPersonInChargeEmpCd(string sign_set_cd)
        {
            DataTable item = gmpDocManageService.GetOverPersonInChargeEmpCd(sign_set_cd);
            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_manage_GridDelete(string doc_no, string revision_no, string dist_id)
        {
            string result = "";

            bool check = gmpDocManageService.GridDelete(doc_no, revision_no, dist_id);

            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Gmp_doc_manage_GridDelete1(string doc_no, string revision_no, string dist_id, string disuse_id)
        {
            string result = "";

            bool check = gmpDocManageService.GridDelete_1(doc_no, revision_no, dist_id, disuse_id);

            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }


        [HttpPost]
        public string Gmp_doc_manage_D1_TRX([JsonBinder] List<DocDistribute> paramData)
        {
            string res = gmpDocManageService.GmpDocManage_D1_TRX(paramData);

            return res;
        }

        [HttpPost]
        public string Gmp_doc_manage_D2_TRX([JsonBinder] List<DocDisuse> paramData)
        {
            string res = gmpDocManageService.GmpDocManage_D2_TRX(paramData);

            return res;
        }

        [CheckSession]
        [HttpPost]
        public void Gmp_doc_manage_DeleteDistribution(string indexKey)
        {
            gmpDocManageService.DeleteDistribution(indexKey);
        }

        [CheckSession]
        [HttpPost]
        public string Gmp_doc_manage_CheckVoidDate(string voidDate)
        {
            string res = gmpDocManageService.ChkVoidDate(voidDate);

            return res;
        }


        [CheckSession]
        [HttpGet]
        public ActionResult Gmp_doc_manage_GetFile(string doc_file_id)
        {
            DataTable attachmentFileData = gmpDocManageService.GetFile(doc_file_id);

            var tmp = Path.GetExtension(attachmentFileData.Rows[0].ItemArray[1].ToString());

            string mimeType = equipmentService.GetMimeType(tmp);

            return File(
                (byte[])attachmentFileData.Rows[0].ItemArray[0]
                , mimeType
                , attachmentFileData.Rows[0].ItemArray[1].ToString()
                );

            //string message = "";
            //message = gmpDocManageService.GetFile(doc_file_id);


            //var resJson = "{ \"messege\": \"" + message + "\" }";
            //return Json(resJson);

        }

        #endregion
    }





}

