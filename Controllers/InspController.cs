using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models.Common;
using HACCP.Models.Insp;
using HACCP.Services.Comm;
using HACCP.Services.Insp;
using HACCP.Services.SysOp;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class InspController : Controller
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(SysSetController));


        private UserMenuManageService userMenuManageService = new UserMenuManageService();
        private SelectBoxService selectBoxService = new SelectBoxService();

        DateTime ed = new DateTime();
        DateTime sd = new DateTime();

        #region 자율점검 체크리스트

        private SelfAuditCheckListService selfAuditCheckListService = new SelfAuditCheckListService();

        [CheckSession]
        [HttpGet]
        public ActionResult SelfAuditCheckList()
        {
            // 메인 그리드 데이터 조회
            DataTable selfAuditCheckListData = selfAuditCheckListService.Select("", "");
            // 셀렉트 박스 데이터 조회
            DataTable sListGubuns = selectBoxService.GetSelectBox("공통코드", "N", "SF007", "listGubuns"); // 구분 조회(검색용)
            DataTable listGubuns = selectBoxService.GetSelectBox("공통코드", "D", "SF007", "listChecker"); // 구분 조회
            DataTable listRanges = selectBoxService.GetSelectBox("공통코드", "D", "SF009", "listChecker"); // 점검 범위 조회
            // 사원 정보 검색용 팝업창 데이터 조회
            DataTable empPopupData = selfAuditCheckListService.getEmpPopupData();

            ViewBag.selfAuditCheckListData = Json(Public_Function.DataTableToJSON(selfAuditCheckListData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.sListGubuns = sListGubuns;
            ViewBag.listGubuns = listGubuns;
            ViewBag.listRanges = listRanges;
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            ViewBag.SelfAuditCheckListAuth = Json(HttpContext.Session["SelfAuditCheckListAuth"]);

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelfAuditCheckListSelect(string list_gubun_cd, string list_checker_cd)
        {
            DataTable selfAuditCheckListData = selfAuditCheckListService.Select(list_gubun_cd, list_checker_cd);

            return ViewBag.selfAuditCheckListData = Json(Public_Function.DataTableToJSON(selfAuditCheckListData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelfAuditCheckListSignInfo(string checkList_gubun, string sign_set_cd)
        {
            DataTable signInfoData = selfAuditCheckListService.SelectSignInfo(checkList_gubun, sign_set_cd);

            return ViewBag.signInfoData = Json(Public_Function.DataTableToJSON(signInfoData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult saveSelfauditchecklist(SelfAuditCheckList list, string CRUD_gubun)
        {
            string message = "";

            if (CRUD_gubun == "I")
            {
                message = selfAuditCheckListService.Insert(list);

            }
            else if (CRUD_gubun == "U")
            {
                message = selfAuditCheckListService.Update(list);
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult DeleteSelfAuditCheckList(string list_no)
        {
            string message = "";

            message = selfAuditCheckListService.Delete(list_no);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        [HttpPost]
        public JsonResult List_SignCRUD(string gubun, string check_gb, string write_emp_cd)
        {
            string message = "";

            //if (gubun == "DEL_ES")
            //{
            //    message = selfAuditScheduleService.DeleteSign(gubun, audit_no);
            //}
            //else 
            if (gubun == "ES")
            {
                message = selfAuditCheckListService.SaveSign(check_gb, write_emp_cd);
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }
        #endregion SelfAuditCheckList


        #region 자율점검 계획 작성

        private SelfAuditScheduleService selfAuditScheduleService = new SelfAuditScheduleService();

        [CheckSession]
        [HttpGet]
        public ActionResult SelfAuditSchedule()
        {

            DateTime dt = System.DateTime.Now;

            // 메인 그리드 데이터 조회
            DataTable SelfAuditScheduleData = selfAuditScheduleService.Select(dt.Year, "", "");

            // 셀렉트 박스 데이터 조회
            DataTable checkPurpose = selectBoxService.GetSelectBox("공통코드", "S", "SF001", "checkPurpose"); // 점검목적 조회
            DataTable checkStep = selectBoxService.GetSelectBox("공통코드", "S", "SF002", "checkStep"); // 점검단계 조회
            DataTable checkGubuns = selectBoxService.GetSelectBox("공통코드", "D", "SF007", "listChecker"); // 구분 조회
            DataTable checkPurpose_d = selectBoxService.GetSelectBox("공통코드", "D", "SF001", "checkPurpose_d"); // 점검목적 조회
            // 사원 정보 검색용 팝업창 데이터 조회
            DataTable empPopupData = userMenuManageService.getEmpPopupData();

            ViewBag.checkPurpose = checkPurpose;
            ViewBag.checkStep = checkStep;
            ViewBag.checkGubuns = checkGubuns;
            ViewBag.CheckPurpose_d = checkPurpose_d;
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            ViewBag.SelfAuditScheduleData = Json(Public_Function.DataTableToJSON(SelfAuditScheduleData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.SelfAuditScheduleAuth = Json(HttpContext.Session["SelfAuditScheduleAuth"]);
            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelfAuditScheduleSelect(int check_year, string check_Purpose, string check_Step)
        {
            DataTable SelfAuditScheduleData = selfAuditScheduleService.Select(check_year, check_Purpose, check_Step);

            return ViewBag.SelfAuditScheduleData = Json(Public_Function.DataTableToJSON(SelfAuditScheduleData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult saveSelfAuditSchedule(SelfAudit list)
        {
            string message = "";

            message = selfAuditScheduleService.modifyData(list);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult SelfAuditScheduleDelete(SelfAudit list)
        {
            string message = "";

            message = selfAuditScheduleService.SelfAuditScheduleDelete(list);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult SelfAuditScheduleSignInfo(string self_audit_year, string self_audit_no, string sign_set_cd)
        {
            DataTable signInfoData = selfAuditScheduleService.SelectSignInfo(self_audit_year, self_audit_no, sign_set_cd);

            return ViewBag.signInfoData = Json(Public_Function.DataTableToJSON(signInfoData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult Schedule_SignCRUD(string gubun, string audit_no, string audit_s_emp_cd)
        {
            string message = "";

            if (gubun == "DEL_ES")
            {
                message = selfAuditScheduleService.DeleteSign(gubun, audit_no);
            }
            else if (gubun == "ES")
            {
                message = selfAuditScheduleService.SaveSign(gubun, audit_no, audit_s_emp_cd);
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelfAuditScheduleFileSearch(SelfAudit list)
        {
            DataTable fileData = selfAuditScheduleService.FileSearch(list);

            return ViewBag.fileData = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public ActionResult FileAdd(SelfAudit list)
        {
            string message = "";


            //if (Request.Files.Count > 0)
            //{
            //    var uploadFile = Request.Files[0];
            //}
            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = selfAuditScheduleService.FileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, contentType, list);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        [HttpPost]
        public ActionResult FileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string message;

            message = selfAuditScheduleService.FileDelete(audit_year, audit_no, audit_file, file_id);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        //[HttpPost]
        //public ActionResult FileOpen(string file_id)
        //{
        //    string message = "";
        //    message = selfAuditScheduleService.FileOpen(file_id);

        //    var resJson = "{ \"messege\": \"" + message + "\" }";
        //    return Json(resJson);
        //}


        [HttpGet]
        public ActionResult FileOpen(string file_id)
        {
            DataTable dt = selfAuditScheduleService.FileOpen(file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }

        #endregion


        #region 자율점검 계획 승인

        private SelfAuditPlanService selfAuditPlanService = new SelfAuditPlanService();

        [HttpGet]
        public ActionResult SelfAuditPlan()
        {

            DateTime ed = new DateTime();
            ed = DateTime.Now;
            DateTime sd = new DateTime();
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");


            // 메인 그리드 데이터 조회
            DataTable SelfAuditPlanData = selfAuditPlanService.Select(sDate, eDate, "", "", "");

            // 셀렉트 박스 데이터 조회
            DataTable auditStep = selectBoxService.GetSelectBox("공통코드", "S", "SF002", "auditStep"); // 단계
            DataTable auditStep_status = selectBoxService.GetSelectBox("공통코드", "S", "SF003", "auditStep_status"); // 상태
            // 사원 정보 검색용 팝업창 데이터 조회
            DataTable empPopupData = selfAuditPlanService.getEmpPopupData();
            DataTable departmentData = selfAuditPlanService.getDepartmentData();

            ViewBag.auditStep = auditStep;
            ViewBag.auditStep_status = auditStep_status;
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.departmentData = Json(Public_Function.DataTableToJSON(departmentData));

            ViewBag.SelfAuditPlanData = Json(Public_Function.DataTableToJSON(SelfAuditPlanData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.SelfAuditPlanAuth = Json(HttpContext.Session["SelfAuditPlanAuth"]);
            return View();
        }


        [HttpPost]
        public JsonResult SelfAuditPlanSelect(string sDate, string eDate, string emp_cd, string step, string status)
        {
            DataTable SelfAuditPlanData = selfAuditPlanService.Select(sDate, eDate, emp_cd, step, status);

            return ViewBag.SelfAuditPlanData = Json(Public_Function.DataTableToJSON(SelfAuditPlanData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [HttpPost]
        public JsonResult SelfAuditPlanSignInfo(string self_audit_year, string self_audit_no, string sign_set_cd)
        {
            DataTable signInfoData = selfAuditPlanService.SelectSignInfo(self_audit_year, self_audit_no, sign_set_cd);

            return ViewBag.signInfoData = Json(Public_Function.DataTableToJSON(signInfoData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult SelfAuditPlanFileSearch(string self_audit_year, string audit_no)
        {
            DataTable fileData = selfAuditPlanService.FileSearch(self_audit_year, audit_no);

            return ViewBag.fileData = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult SelfAuditPlanFileAdd(SelfAudit list)
        {
            string message = "";

            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = selfAuditPlanService.FileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, contentType, list);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpGet]
        public ActionResult SelfAuditPlanFileOpen(string file_id)
        {
            DataTable dt = selfAuditPlanService.FileOpen(file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }

        [HttpPost]
        public ActionResult SelfAuditPlanFileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string message;

            message = selfAuditPlanService.FileDelete(audit_year, audit_no, audit_file, file_id);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        [HttpPost]
        public ActionResult teamSearch(string self_audit_year, string audit_no)
        {
            DataTable teamSearchData = selfAuditPlanService.teamSearch(self_audit_year, audit_no);

            return ViewBag.teamSearchData = Json(Public_Function.DataTableToJSON(teamSearchData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [HttpPost]
        public ActionResult MemberInsert(string self_audit_year, string audit_no, string team_nm, string input_team_cd, string input_team_role)
        {
            string message;
            
            message = selfAuditPlanService.teamNameSetting(self_audit_year, audit_no, team_nm);

            if(input_team_cd.Length != 0)
            {
                message = selfAuditPlanService.MemberInsert(self_audit_year, audit_no, input_team_cd, input_team_role);
            }
            

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        [HttpPost]
        public ActionResult MemberDelete(string self_audit_year, string audit_no, string input_team_cd)
        {
            string message;

            message = selfAuditPlanService.MemberDelete(self_audit_year, audit_no, input_team_cd);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult DeptSearch(string self_audit_year, string audit_no)
        {
            DataTable teamSearchData = selfAuditPlanService.DeptSearch(self_audit_year, audit_no);

            return ViewBag.teamSearchData = Json(Public_Function.DataTableToJSON(teamSearchData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult DeptInsert(string self_audit_year, string audit_no, string dept_cd, string dept_emp_cd, string dept_date, string dept_object)
        {
            string message;

            message = selfAuditPlanService.DeptInsert(self_audit_year, audit_no, dept_cd, dept_emp_cd, dept_date, dept_object);


            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        [HttpPost]
        public ActionResult DeptDelete(string self_audit_year, string audit_no, string dept_cd)
        {
            string message;

            message = selfAuditPlanService.DeptDelete(self_audit_year, audit_no, dept_cd);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult DeptSearch2(string self_audit_year, string audit_no)
        {
            DataTable teamSearchData = selfAuditPlanService.DeptSearch(self_audit_year, audit_no);

            return ViewBag.teamSearchData = Json(Public_Function.DataTableToJSON(teamSearchData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [HttpPost]
        public ActionResult AuditorSearch(string self_audit_year, string audit_no, string dept_cd)
        {
            DataTable teamSearchData = selfAuditPlanService.AuditorSearch(self_audit_year, audit_no, dept_cd);

            return ViewBag.teamSearchData = Json(Public_Function.DataTableToJSON(teamSearchData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult AuditorInsert(string self_audit_year, string audit_no, string dept_cd, string[] auditor_cd)
        {
            string message;

            if(auditor_cd == null)
            {
                message = selfAuditPlanService.AuditorAllDelete(self_audit_year, audit_no, dept_cd);

            }else
            {
                message = selfAuditPlanService.AuditorAllDelete(self_audit_year, audit_no, dept_cd);

                message = selfAuditPlanService.AuditorInsert(self_audit_year, audit_no, dept_cd, auditor_cd);
            }



            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        public JsonResult Plan_SignCRUD(string gubun, string audit_year, string audit_no, string emp_cd, string check_gb, string audit_date, string type)
        {
            string message = "";

            if (gubun == "DEL_ES")
            {
                message = selfAuditPlanService.DeleteSign(audit_year, audit_no, emp_cd, check_gb, type);
            }
            else if (gubun == "ES")
            {
                message = selfAuditPlanService.SaveSign(audit_year, audit_no, emp_cd, check_gb, audit_date, type);
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        [HttpPost]
        public ActionResult InsertCComment(string self_audit_year, string audit_no, string c_comment) 
        { 
            string message;

            message = selfAuditPlanService.InsertCComment(self_audit_year, audit_no, c_comment);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult InsertMComment(string self_audit_year, string audit_no, string m_comment)
        {
            string message;

            message = selfAuditPlanService.InsertMComment(self_audit_year, audit_no, m_comment);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        [HttpPost]
        public ActionResult CheckAuditor(string self_audit_year, string audit_no)
        {
            string message;

            message = selfAuditPlanService.CheckAuditor(self_audit_year, audit_no);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        #endregion


        #region 자율점검 실시
        private SelfAuditStartService selfAuditStartService = new SelfAuditStartService();

        [HttpGet]
        public ActionResult SelfAuditStart()
        {
            ed = DateTime.Now;
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");


            // 메인 그리드 데이터 조회
            DataTable SelfAuditStartData = selfAuditStartService.Select(sDate, eDate, HttpContext.Session["loginCD"].ToString(), "");

            // 셀렉트 박스 데이터 조회
            DataTable auditStatus = selectBoxService.GetSelectBox("공통코드", "S", "SF004", "auditStatus"); //상태
            DataTable auditStatus_d = selectBoxService.GetSelectBox("공통코드", "D", "SF004", "auditStatus_d"); //상태_d
            DataTable auditRange_d = selectBoxService.GetSelectBox("공통코드", "D", "SF009", "auditRange_d"); //범위_d
            DataTable auditEvaluation_d = selectBoxService.GetSelectBox("공통코드", "D", "SF008", "auditEvaluation_d"); //평가_d

            // 팝업창 데이터 조회
            DataTable empPopupData = selfAuditStartService.getEmpPopupData();


            // 메인 데이터 ViewBag
            ViewBag.SelfAuditStartData = Json(Public_Function.DataTableToJSON(SelfAuditStartData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.SelfAuditStartAuth = Json(HttpContext.Session["SelfAuditStartAuth"]);

            // 셀렉트박스 데이터 ViewBag
            ViewBag.auditStatus = auditStatus;
            ViewBag.auditStatus_d = auditStatus_d;
            ViewBag.auditRange_d = auditRange_d;
            ViewBag.auditEvaluation_d = auditEvaluation_d;

            // 팝업창 데이터 ViewBag
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            return View();
        }

        [HttpPost]
        public JsonResult SelfAuditListSelect(string sDate, string eDate, string emp_cd, string status)
        {
            DataTable SelfAuditStartData = selfAuditStartService.Select(sDate, eDate, emp_cd, status);

            return ViewBag.SelfAuditStartData = Json(Public_Function.DataTableToJSON(SelfAuditStartData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [HttpPost]
        public JsonResult StartCheckListSearch(string audit_year, string audit_no, string record_emp_cd)
        {
            DataTable SelfAuditStartData = selfAuditStartService.CheckListSelect(audit_year, audit_no, record_emp_cd);

            return ViewBag.SelfAuditStartData = Json(Public_Function.DataTableToJSON(SelfAuditStartData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [HttpPost]
        public ActionResult StartResultInsert(string audit_year, string audit_no, string audit_dept_cd, string audit_auditor, string audit_date, string audit_result)
        {
            string message = "";
            message = selfAuditStartService.StartGridEdit(audit_year, audit_no, audit_dept_cd, audit_auditor, audit_date, audit_result);

            message = selfAuditStartService.StartGridUpdate2(audit_year, audit_no);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson); ;
        }


        [HttpPost]
        public ActionResult ListResultInsert(string audit_item_no, string audit_result, string audit_evaluation, string audit_opinion)
        {
            string message = "";
            message = selfAuditStartService.ListResultInsert(audit_item_no, audit_result, audit_evaluation, audit_opinion);


            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson); ;
        }

        [HttpPost]
        public JsonResult StartResultDelete(string audit_year, string audit_no, string audit_dept_cd, string audit_auditor)
        {
            string message = "";

            message = selfAuditStartService.StartGridDelete(audit_year, audit_no, audit_dept_cd, audit_auditor);

            message = selfAuditStartService.StartGridDelete2(audit_year, audit_no);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        [HttpPost]
        public JsonResult StartFileSearch(SelfAudit list)
        {
            DataTable fileData = selfAuditStartService.StartFileSearch(list);

            return ViewBag.fileData = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [HttpPost]
        public ActionResult StartFileAdd(SelfAudit list)
        {

            string message = "";

            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = selfAuditStartService.StartFileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, contentType, list);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        [HttpPost]
        public ActionResult StartFileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string message;

            message = selfAuditStartService.StartFileDelete(audit_year, audit_no, audit_file, file_id);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpGet]
        public ActionResult StartFileOpen(string file_id)
        {
            DataTable dt = selfAuditStartService.StartFileOpen(file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }


        [HttpPost]
        public JsonResult SelfAuditStartSignInfo(string self_record_no, string self_record_emp_cd, string sign_set_cd)
        {
            DataTable signInfoData = selfAuditStartService.SelfAuditStartSignInfo(self_record_no, self_record_emp_cd, sign_set_cd);

            return ViewBag.signInfoData = Json(Public_Function.DataTableToJSON(signInfoData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [HttpPost]
        public JsonResult Start_SignCRUD(string audit_record_no, string emp_cd)
        {
            string message = "";

            message = selfAuditStartService.SaveSign(audit_record_no, emp_cd);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }
        #endregion


        #region 자율점검 결과 

        private SelfAuditResultService selfAuditResultService = new SelfAuditResultService();

        [HttpGet]
        public ActionResult SelfAuditResult()
        {

            ed = DateTime.Now;
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");


            // 메인 그리드 데이터 조회
            DataTable SelfAuditResultData = selfAuditResultService.Select(sDate, eDate, HttpContext.Session["loginCD"].ToString(), "%%", "%%");

            // 셀렉트 박스 데이터 조회
            DataTable auditStep = selectBoxService.GetSelectBox("공통코드", "S", "SF002", "auditStep"); // 단계
            DataTable auditStepStatus = selectBoxService.GetSelectBox("공통코드", "S", "SF003", "auditStepStatus"); // 상태
            DataTable auditResultStatus = selectBoxService.GetSelectBox("공통코드", "S", "SF004", "auditResultStatus"); // 상태

            // 팝업창 데이터 조회
            DataTable empPopupData = selfAuditResultService.getEmpPopupData();
            DataTable deptPopupData = selfAuditResultService.getDeptPopupData();


            // 메인 데이터 ViewBag
            ViewBag.SelfAuditResultData = Json(Public_Function.DataTableToJSON(SelfAuditResultData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.SelfAuditResultAuth = Json(HttpContext.Session["SelfAuditResultAuth"]);

            // 셀렉트박스 데이터 ViewBag
            ViewBag.auditStep = auditStep;
            ViewBag.auditStepStatus = auditStepStatus;
            ViewBag.auditResultStatus = auditResultStatus;

            // 팝업창 데이터 ViewBag
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.deptPopupData = Json(Public_Function.DataTableToJSON(deptPopupData));

            return View();
        }


        [HttpPost]
        public JsonResult AuditResultListSearch(string sDate, string eDate, string emp_cd, string step, string status)
        {
            DataTable SelfAuditResultData = selfAuditResultService.Select(sDate, eDate, emp_cd, step, status);

            return ViewBag.SelfAuditResultData = Json(Public_Function.DataTableToJSON(SelfAuditResultData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult ResultCheckListSearch(string audit_year, string audit_no)
        {
            DataTable ResultCheckListData = selfAuditResultService.ResultCheckListSearch(audit_year, audit_no);

            return ViewBag.ResultCheckListData = Json(Public_Function.DataTableToJSON(ResultCheckListData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        #region 서명 관련
        [HttpPost]
        public JsonResult ResultDisplayESInfo(string audit_year, string audit_no, string sign_set_cd)
        {
            DataTable signInfoData = selfAuditResultService.ResultDisplayESInfo(audit_year, audit_no, sign_set_cd);

            return ViewBag.signInfoData = Json(Public_Function.DataTableToJSON(signInfoData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult ResultSign_CRUD(string gubun, string audit_year, string audit_no, string emp_cd, string type)
        {
            string message = "";

            if(gubun == "ES")
            {
                message = selfAuditResultService.ResultSaveSign(audit_year, audit_no, emp_cd, type);
            }
            else if(gubun == "DEL_ES")
            {
                message = selfAuditResultService.ResultDeleteSign(audit_year, audit_no, type);
            }


            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        
        [HttpPost]
        public ActionResult ResultInsertComment_C(string self_audit_year, string audit_no, string c_comment)
        {
            string message;

            message = selfAuditResultService.ResultInsertComment_C(self_audit_year, audit_no, c_comment);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult ResultInsertComment_M(string self_audit_year, string audit_no, string c_comment)
        {
            string message;

            message = selfAuditResultService.ResultInsertComment_M(self_audit_year, audit_no, c_comment);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }
        #endregion 서명 관련 END


        #region 첨부파일 관련
        [HttpPost]
        public JsonResult ResultFileSearch(SelfAudit list)
        {
            DataTable fileData = selfAuditResultService.ResultFileSearch(list);

            return ViewBag.fileData = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult ResultFileAdd(SelfAudit list)
        {
            string message = "";

            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = selfAuditResultService.ResultFileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, contentType, list);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult ResultFileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string message;

            message = selfAuditResultService.ResultFileDelete(audit_year, audit_no, audit_file, file_id);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpGet]
        public ActionResult ResultFileOpen(string file_id)
        {
            DataTable dt = selfAuditResultService.ResultFileOpen(file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }
        #endregion 첨부파일 관련 END


        #region 점검결과 관련
        [HttpPost]
        public JsonResult ResultDetailSearch(string aduit_year, string audit_no)
        {
            DataTable ResultDetailData = selfAuditResultService.ResultDetailSearch(aduit_year, audit_no);

            return ViewBag.ResultDetailData = Json(Public_Function.DataTableToJSON(ResultDetailData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult ResultReturnStatus(string audit_year, string audit_no, string audit_dept_cd, string audit_auditor)
        {
            string message = "";

            message = selfAuditResultService.ResultReturnStatus(audit_year, audit_no, audit_dept_cd, audit_auditor);


            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }



        [HttpPost]
        public JsonResult ResultSave(string audit_year, string audit_no, string audit_result, string audit_sepcial)
        {
            string message = "";

            message = selfAuditResultService.ResultSave(audit_year, audit_no, audit_result, audit_sepcial);


            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }
        #endregion 점검결과 관련 END


        #region 수정조치 관련
        [HttpPost]
        public JsonResult ResultRevisionSearch(string audit_year, string audit_no)
        {
            DataTable ResultCheckListData = selfAuditResultService.ResultRevisionSearch(audit_year, audit_no);

            return ViewBag.ResultCheckListData = Json(Public_Function.DataTableToJSON(ResultCheckListData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult ResultRevisionSave(string audit_year, string audit_no, SelfAuditCa data)
        {
            string message = "";

            message = selfAuditResultService.ResultRevisionSave(audit_year, audit_no, data);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public JsonResult ResultRevisionDelete(string audit_year, string audit_no, SelfAuditCa data)
        {
            string message = "";

            message = selfAuditResultService.ResultRevisionDelete(audit_year, audit_no, data);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }
        #endregion 수정조치 관련 END

        #endregion 자율점검 결과 END


        #region 자율점검 조치 계획 작성
        private SelfAuditEditPlanService selfAuditEditPlanService = new SelfAuditEditPlanService();

        [HttpGet]
        public ActionResult SelfAuditEditPlan()
        {
            DateTime ed = new DateTime();
            ed = DateTime.Now;
            DateTime sd = new DateTime();
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");


            // 메인 그리드 데이터 조회
            DataTable SelfAuditEditPlanData = selfAuditEditPlanService.Select(sDate, eDate, HttpContext.Session["deptNM"].ToString(), HttpContext.Session["loginNM"].ToString(), "1");

            // 셀렉트 박스 데이터 조회
            DataTable auditStep = selectBoxService.GetSelectBox("공통코드", "S", "SF006", "auditStep"); // 상태
            DataTable auditStep_s = selectBoxService.GetSelectBox("공통코드", "D", "SF006", "auditStep_s"); // 상태_s

            // 사원 정보 검색용 팝업창 데이터 조회
            DataTable empPopupData = selfAuditEditPlanService.getEmpPopupData();
            DataTable deptPopupData = selfAuditEditPlanService.getDeptPopupData();

            ViewBag.auditStep = auditStep;
            ViewBag.auditStep_s = auditStep_s;

            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.deptPopupData = Json(Public_Function.DataTableToJSON(deptPopupData));

            ViewBag.SelfAuditEditPlanData = Json(Public_Function.DataTableToJSON(SelfAuditEditPlanData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.SelfAuditEditPlanAuth = Json(HttpContext.Session["SelfAuditEditPlanAuth"]);

            return View();
        }


        [HttpPost]
        public JsonResult SelfAuditEditPlanSearch(string sDate, string eDate, string dept_cd, string deptEmp_cd, string status)
        {
            DataTable SelfAuditEditPlanData = selfAuditEditPlanService.Select(sDate, eDate, dept_cd, deptEmp_cd, status);

            return ViewBag.SelfAuditEditPlanData = Json(Public_Function.DataTableToJSON(SelfAuditEditPlanData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult SelfAuditEditPlanSave(SelfAuditCa data)
        {
            string message = "";

            message = selfAuditEditPlanService.EditPlanSave(data);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public JsonResult SelfAuditEditPlanDelete(SelfAuditCa data)
        {
            string message = "";

            message = selfAuditEditPlanService.EditPlanDelete(data);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public JsonResult EditPlanFileSearch(SelfAuditCa data)
        {
            DataTable EditPlanFileData = selfAuditEditPlanService.EditPlanFileSearch(data);

            return ViewBag.EditPlanFileData = Json(Public_Function.DataTableToJSON(EditPlanFileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult EditPlanFileAdd(SelfAudit data)
        {
            string message = "";

            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = selfAuditEditPlanService.EditPlanFileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, contentType, data);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public JsonResult EditPlanFileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string message = "";

            message = selfAuditEditPlanService.EditPlanFileDelete(audit_year, audit_no, audit_file, file_id);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpGet]
        public ActionResult EditPlanFileOpen(string file_id)
        {
            DataTable dt = selfAuditEditPlanService.EditPlanFileOpen(file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }
        #endregion


        #region 자율점검 조치 계획 승인
        private SelfAuditEditPlanOKService selfAuditEditPlanOKService = new SelfAuditEditPlanOKService();

        [HttpGet]
        public ActionResult SelfAuditEditPlanOK()
        {
            DateTime ed = new DateTime();
            ed = DateTime.Now;
            DateTime sd = new DateTime();
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");


            // 메인 그리드 데이터 조회
            DataTable SelfAuditEditPlanOKData = selfAuditEditPlanOKService.Select(sDate, eDate, "", "5", "2");

            // 셀렉트 박스 데이터 조회
            DataTable auditStep = selectBoxService.GetSelectBox("공통코드", "S", "SF002", "auditStep"); // 상태
            DataTable auditStepStatus = selectBoxService.GetSelectBox("공통코드", "S", "SF003", "auditStatus"); // 상태
            DataTable auditStatus = selectBoxService.GetSelectBox("공통코드", "s", "SF004", "auditStatus"); // 상태_s

            //사원 정보 검색용 팝업창 데이터 조회
            DataTable empPopupData = selfAuditEditPlanService.getEmpPopupData();
            DataTable deptPopupData = selfAuditEditPlanService.getDeptPopupData();

            ViewBag.auditStep = auditStep;
            ViewBag.auditStepStatus = auditStepStatus;
            ViewBag.auditStatus = auditStatus;

            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.deptPopupData = Json(Public_Function.DataTableToJSON(deptPopupData));

            ViewBag.SelfAuditEditPlanOKData = Json(Public_Function.DataTableToJSON(SelfAuditEditPlanOKData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.SelfAuditEditPlanOKAuth = Json(HttpContext.Session["SelfAuditEditPlanOKAuth"]);

            return View();
        }


        [HttpPost]
        public JsonResult EditPlanOKSearch(string sDate, string eDate, string step, string emp_cd, string status)
        {
            DataTable SelfAuditEditPlanOKData = selfAuditEditPlanOKService.Select(sDate, eDate, emp_cd, step, status);

            return ViewBag.signSelfAuditEditPlanOKDataInfoData = Json(Public_Function.DataTableToJSON(SelfAuditEditPlanOKData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        #region 서명 관련
        [HttpPost]
        public JsonResult EditPlanOKDisplayESInfo(string audit_year, string audit_no, string sign_set_cd)
        {
            DataTable signInfoData = selfAuditEditPlanOKService.EditPlanOKDisplayESInfo(audit_year, audit_no, sign_set_cd);

            return ViewBag.signInfoData = Json(Public_Function.DataTableToJSON(signInfoData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult EditPlanOKSign_CRUD(string gubun, string audit_year, string audit_no, string emp_cd, string type)
        {
            string message = "";

            if (gubun == "ES")
            {
                message = selfAuditEditPlanOKService.EditPlanOKSaveSign(audit_year, audit_no, emp_cd, type);
            }
            else if (gubun == "DEL_ES")
            {
                message = selfAuditEditPlanOKService.EditPlanOKDeleteSign(audit_year, audit_no, type);
            }


            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult EditPlanOKCommentInsert_c(string self_audit_year, string audit_no, string c_comment)
        {
            string message;

            message = selfAuditEditPlanOKService.EditPlanOKCommentInsert_c(self_audit_year, audit_no, c_comment);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult EditPlanOKCommentInsert_m(string self_audit_year, string audit_no, string c_comment)
        {
            string message;

            message = selfAuditEditPlanOKService.EditPlanOKCommentInsert_m(self_audit_year, audit_no, c_comment);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }
        #endregion 서명 관련 END


        #region 수정조치 관련
        [HttpPost]
        public JsonResult EditPlanOKRevisionSearch(SelfAudit data)
        {
            DataTable EditPlanOKRevisionData = selfAuditEditPlanOKService.EditPlanOKRevisionSearch(data);

            return ViewBag.EditPlanOKRevisionData = Json(Public_Function.DataTableToJSON(EditPlanOKRevisionData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult SelfAuditEditPlanOKSave(SelfAuditCa data)
        {
            string message = "";

            message = selfAuditEditPlanOKService.EditPlanOKSave(data);


            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public JsonResult SelfAuditEditPlanOKDelete(SelfAuditCa data)
        {
            string message = "";

            message = selfAuditEditPlanOKService.EditPlanOKDelete(data);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }
        #endregion


        #region 점검결과 관련
        [HttpPost]
        public JsonResult EditPlanOKResultSearch(SelfAudit data)
        {
            DataTable EditPlanOKResultData = selfAuditEditPlanOKService.EditPlanOKResultSearch(data);

            return ViewBag.EditPlanOKResultData = Json(Public_Function.DataTableToJSON(EditPlanOKResultData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }
        #endregion


        #region 첨부파일 관련
        [HttpPost]
        public JsonResult EditPlanOKFileSearch(SelfAudit data)
        {
            DataTable fileData = selfAuditEditPlanOKService.EditPlanOKFileSearch(data);

            return ViewBag.fileData = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult EditPlanOKFileAdd(SelfAudit data)
        {
            string message = "";

            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = selfAuditEditPlanOKService.EditPlanOKFileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, contentType, data);
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
        public ActionResult EditPlanOKFileOpen(string file_id)
        {
            DataTable dt = selfAuditEditPlanOKService.EditPlanOKFileOpen(file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }


        [HttpPost]
        public ActionResult EditPlanOKFileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string message;

            message = selfAuditEditPlanOKService.EditPlanOKFileDelete(audit_year, audit_no, audit_file, file_id);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }

        #endregion 첨부파일 관련 END
        #endregion


        #region 자율점검 조치 실시
        private SelfAuditEditActionService SelfAuditEditActionService = new SelfAuditEditActionService();

        [HttpGet]
        public ActionResult SelfAuditEditAction()
        {
            DateTime ed = new DateTime();
            ed = DateTime.Now;
            DateTime sd = new DateTime();
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");


            // 메인 그리드 데이터 조회
            DataTable SelfAuditEditActionData = SelfAuditEditActionService.Select(sDate, eDate, HttpContext.Session["deptNM"].ToString(), HttpContext.Session["loginNM"].ToString(), "3");
            

            // 셀렉트 박스 데이터 조회
            DataTable auditCaStatus_s = selectBoxService.GetSelectBox("공통코드", "S", "SF006", "auditCaStatus_s"); // 상태_s
            DataTable auditCaStatus_d = selectBoxService.GetSelectBox("공통코드", "D", "SF006", "auditCaStatus_d"); // 상태_d

            //사원 정보 검색용 팝업창 데이터 조회
            DataTable empPopupData = selfAuditEditPlanService.getEmpPopupData();
            DataTable deptPopupData = selfAuditEditPlanService.getDeptPopupData();

            ViewBag.auditCaStatus_s = auditCaStatus_s;
            ViewBag.auditCaStatus_d = auditCaStatus_d;


            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.deptPopupData = Json(Public_Function.DataTableToJSON(deptPopupData));

            ViewBag.SelfAuditEditActionData = Json(Public_Function.DataTableToJSON(SelfAuditEditActionData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.SelfAuditEditActionAuth = Json(HttpContext.Session["SelfAuditEditActionAuth"]);

            return View();
        }


        #region 기본CRUD
        [HttpPost]
        public JsonResult SelfAuditEditActionSearch(string sDate, string eDate, string dept_nm, string deptEmp_nm, string status)
        {
            DataTable SelfAuditEditActionData = SelfAuditEditActionService.Select(sDate, eDate, dept_nm, deptEmp_nm, status);

            return ViewBag.SelfAuditEditActionData = Json(Public_Function.DataTableToJSON(SelfAuditEditActionData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult SelfAuditEditActionSave(SelfAuditCa data)
        {
            string message = "";

            message = SelfAuditEditActionService.SelfAuditEditActionSave(data);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public JsonResult SelfAuditEditActionDelete(SelfAuditCa data)
        {
            string message = "";

            message = SelfAuditEditActionService.SelfAuditEditActionDelete(data);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }
        #endregion


        #region 첨부파일관련
        [HttpPost]
        public JsonResult EditActionFileSearch(SelfAuditCa data)
        {            

            DataTable dt = SelfAuditEditActionService.EditActionFileSearch(data);
            
            return ViewBag.fileData = Json(Public_Function.DataTableToJSON(dt).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult EditActionFileAdd(SelfAudit data)
        {
            string message = "";

            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = SelfAuditEditActionService.EditActionFileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, contentType, data);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public JsonResult EditActionFileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string message = "";

            message = SelfAuditEditActionService.EditActionFileDelete(audit_year, audit_no, audit_file, file_id);

            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpGet]
        public ActionResult EditActionFileOpen(string file_id)
        {
            DataTable dt = SelfAuditEditActionService.EditActionFileOpen(file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }

        #endregion
        #endregion


        #region 자율점검 조치 결과

        private SelfAuditResult2Service selfAuditResult2Service = new SelfAuditResult2Service();

        [HttpGet]
        public ActionResult SelfAuditResult2()
        {

            ed = DateTime.Now;
            sd = ed.AddMonths(-1);

            string sDate = sd.ToString("yyyy-MM-dd");
            string eDate = ed.ToString("yyyy-MM-dd");


            // 메인 그리드 데이터 조회
            DataTable SelfAuditResult2Data = selfAuditResultService.Select(sDate, eDate, HttpContext.Session["loginCD"].ToString(), "5", "5");

            // 셀렉트 박스 데이터 조회
            DataTable auditStep = selectBoxService.GetSelectBox("공통코드", "S", "SF002", "auditStep"); // 단계
            DataTable auditStepStatus = selectBoxService.GetSelectBox("공통코드", "S", "SF003", "auditStepStatus"); // 상태
            DataTable auditResultStatus = selectBoxService.GetSelectBox("공통코드", "S", "SF004", "auditResultStatus"); 
            DataTable auditResultCaStatus = selectBoxService.GetSelectBox("공통코드", "S", "SF006", "auditResultCaStatus"); 

            //// 팝업창 데이터 조회
            DataTable empPopupData = selfAuditResult2Service.getEmpPopupData();
            DataTable deptPopupData = selfAuditResult2Service.getDeptPopupData();


            // 메인 데이터 ViewBag
            ViewBag.SelfAuditResult2Data = Json(Public_Function.DataTableToJSON(SelfAuditResult2Data).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            ViewBag.SelfAuditResult2Auth = Json(HttpContext.Session["SelfAuditResult2Auth"]);

            // 셀렉트박스 데이터 ViewBag
            ViewBag.auditStep = auditStep;
            ViewBag.auditStepStatus = auditStepStatus;
            ViewBag.auditResultStatus = auditResultStatus;
            ViewBag.auditResultCaStatus = auditResultCaStatus;

            // 팝업창 데이터 ViewBag
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.deptPopupData = Json(Public_Function.DataTableToJSON(deptPopupData));

            return View();
        }


        #region 기본CRUD
        [HttpPost]
        public JsonResult SelfAuditResult2Search(string sDate, string eDate, string emp_cd, string step, string status)
        {
            DataTable SelfAuditResult2Data = selfAuditResult2Service.Select(sDate, eDate, emp_cd, step, status);

            return ViewBag.SelfAuditResult2Data = Json(Public_Function.DataTableToJSON(SelfAuditResult2Data).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        #endregion


        #region 서명 관련
        [HttpPost]
        public JsonResult Result2DisplayESInfo(string audit_year, string audit_no, string sign_set_cd)
        {
            DataTable signInfoData = selfAuditResult2Service.Result2DisplayESInfo(audit_year, audit_no, sign_set_cd);

            return ViewBag.signInfoData = Json(Public_Function.DataTableToJSON(signInfoData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        public JsonResult Result2_SignCRUD(string gubun, string audit_year, string audit_no, string emp_cd, string type)
        {
            string message = "";

            if (gubun == "DEL_ES")
            {
                message = selfAuditResult2Service.Result2DeleteSign(audit_year, audit_no, type);
            }
            else if (gubun == "ES")
            {
                message = selfAuditResult2Service.Result2SaveSign(audit_year, audit_no, emp_cd, type);
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult Result2CommentInsert_ac(string self_audit_year, string audit_no, string ac_comment)
        {
            string message;

            message = selfAuditResult2Service.ResultInsertComment_ac(self_audit_year, audit_no, ac_comment);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult Result2CommentInsert_a(string self_audit_year, string audit_no, string a_comment)
        {
            string message;

            message = selfAuditResult2Service.ResultInsertComment_a(self_audit_year, audit_no, a_comment);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        #endregion


        #region 점검결과 관련
        [HttpPost]
        public JsonResult Result2DetailSearch(string aduit_year, string audit_no)
        {
            DataTable Result2DetailData = selfAuditResult2Service.Result2DetailSearch(aduit_year, audit_no);

            return ViewBag.Result2DetailData = Json(Public_Function.DataTableToJSON(Result2DetailData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult Result2ReturnStatus(string audit_year, string audit_no, string audit_dept_cd, string audit_auditor)
        {
            string message = "";

            message = selfAuditResult2Service.Result2ReturnStatus(audit_year, audit_no, audit_dept_cd, audit_auditor);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }
        #endregion 점검결과 관련 END


        #region 수정조치 관련
        [HttpPost]
        public JsonResult Result2RevisionSearch(string audit_year, string audit_no)
        {
            DataTable Result2RevisionData = selfAuditResult2Service.Result2RevisionSearch(audit_year, audit_no);

            return ViewBag.Result2RevisionData = Json(Public_Function.DataTableToJSON(Result2RevisionData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult Result2RevisionSave(string audit_year, string audit_no, SelfAuditCa data)
        {
            string message = "";

            message = selfAuditResult2Service.Result2RevisionSave(audit_year, audit_no, data);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public JsonResult Result2RevisionDelete(string audit_year, string audit_no, SelfAuditCa data)
        {
            string message = "";

            message = selfAuditResult2Service.Result2RevisionDelete(audit_year, audit_no, data);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        #endregion 수정조치 관련 END


        // region 조치결과 관련
        [HttpPost]
        public JsonResult Result2RevisionResultSearch(string audit_year, string audit_no)
        {
            DataTable Result2RevisionResultData = selfAuditResult2Service.Result2RevisionResultSearch(audit_year, audit_no);

            return ViewBag.Result2RevisionResultData = Json(Public_Function.DataTableToJSON(Result2RevisionResultData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public JsonResult Result2RevisionResultSave(string audit_year, string audit_no, string audit_result, string audit_sepcial)
        {
            string message = "";

            message = selfAuditResult2Service.Result2RevisionResultSave(audit_year, audit_no, audit_result, audit_sepcial);


            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }

        // endregion 조치결과 관련 END


        #region 첨부파일 관련
        [HttpPost]
        public JsonResult Result2FileSearch(SelfAudit list)
        {
            DataTable fileData = selfAuditResult2Service.Result2FileSearch(list);

            return ViewBag.fileData = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [HttpPost]
        public ActionResult Result2FileAdd(SelfAudit list)
        {
            string message = "";

            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = selfAuditResult2Service.Result2FileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, contentType, list);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpPost]
        public ActionResult Result2FileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string message;

            message = selfAuditResult2Service.Result2FileDelete(audit_year, audit_no, audit_file, file_id);

            var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(resJson);
        }


        [HttpGet]
        public ActionResult Result2FileOpen(string file_id)
        {
            DataTable dt = selfAuditResult2Service.Result2FileOpen(file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }

        #endregion 첨부파일 관련 END




        [HttpPost]
        public JsonResult Result2CheckListSearch(string audit_year, string audit_no)
        {
            DataTable Result2CheckListData = selfAuditResult2Service.Result2CheckListSearch(audit_year, audit_no);

            return ViewBag.Result2CheckListData = Json(Public_Function.DataTableToJSON(Result2CheckListData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }
        #endregion




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
