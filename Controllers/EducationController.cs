using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models;
using HACCP.Models.Common;
using HACCP.Services.Comm;
using HACCP.Services.SysSec;
using HACCP.Services.Education;
using log4net;
using HACCP.Models.Education;
using System.IO;
using Newtonsoft.Json;

namespace HACCP.Controllers
{
    public class EducationController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EducationController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private AccessLogService accessLogService = new AccessLogService();

        private EmployeeEduService employeeEduService = new EmployeeEduService();
        private EmployeeEduHistoryService employeeEduHistoryService = new EmployeeEduHistoryService();
        private EmployeeEduGradeService employeeEduGradeService = new EmployeeEduGradeService();
        private EmployeeEduResultService employeeEduResultService = new EmployeeEduResultService(); 
        private QualificationInfoService qualificationInfoService = new QualificationInfoService();

        #region EmployeeEdu - 교육 등록
        public ActionResult EmployeeEdu(EmployeeEdu iModel)
        {
            //교육구분
            DataTable EduGubun = selectBoxService.GetSelectBox("공통코드", "S", "CM077", "edu_gubun");
            //평가방법
            DataTable EduGrade = selectBoxService.GetSelectBox("공통코드", "S", "CM078", "edu_grade");
            //교육상태
            DataTable EduState = selectBoxService.GetSelectBox("공통코드", "D", "EC001", "edu_state");

            ViewBag.EduGubun = EduGubun;
            ViewBag.EduGubunData = Json(Public_Function.DataTableToJSON(EduGubun));
            ViewBag.EduGrade = EduGrade;
            ViewBag.EduGradeData = Json(Public_Function.DataTableToJSON(EduGrade));
            ViewBag.EduState = EduState;

            iModel.sdate = DateTime.Today.ToShortDateString();
            iModel.edate = DateTime.Today.AddMonths(1).ToShortDateString();
            iModel.title = "";
            iModel.edu_gb = "%";
            iModel.edu_method = "%";
            iModel.dept_cd = "";

            DataTable eduData = employeeEduService.EmployeeEdu_Search(iModel);
            DataTable empData = employeeEduService.Employee_Search(iModel);
            DataTable locData = employeeEduService.EmployeeEdu_FindLocation(iModel);
            DataTable groupData = employeeEduService.EmployeeEdu_FindGroup(iModel);
            DataTable departmentData = employeeEduService.EmployeeEdu_FindDepartment(iModel);

            JsonResult tmpJR = Json(Public_Function.DataTableToJSON(eduData));
            tmpJR.MaxJsonLength = int.MaxValue;

            ViewBag.EmployeeEduData = tmpJR;
            ViewBag.EmployeeData = Json(Public_Function.DataTableToJSON(empData));
            ViewBag.LocationData = Json(Public_Function.DataTableToJSON(locData));
            ViewBag.GroupData = Json(Public_Function.DataTableToJSON(groupData));
            ViewBag.DepartmentData = Json(Public_Function.DataTableToJSON(departmentData));

            string EDU_NO = "";
            //첫번째 행의 데이터 가져오기
            if (eduData.Rows.Count > 0)
            {
                EDU_NO = (eduData.DefaultView.ToTable(false, "edu_no").Rows[0])[0].ToString();
            }

            DataTable attempData = employeeEduService.AttendEmployee_Search(EDU_NO);
            DataTable attgroupData = employeeEduService.EmployeeEdu_FindAttendGroup(EDU_NO);
            DataTable fileData = employeeEduService.FileSearch(EDU_NO);

            ViewBag.attEmployeeData = Json(Public_Function.DataTableToJSON(attempData));
            ViewBag.attGroupData = Json(Public_Function.DataTableToJSON(attgroupData));
            ViewBag.fileData = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            return View();
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduSearch(EmployeeEdu iModel)
        {
            DataTable eduData = employeeEduService.EmployeeEdu_Search(iModel);

            return ViewBag.EmployeeEduData = Json(Public_Function.DataTableToJSON(eduData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeSearch(EmployeeEdu iModel)
        {
            DataTable empData = employeeEduService.Employee_Search(iModel);

            return ViewBag.EmployeeData = Json(Public_Function.DataTableToJSON(empData));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult AttendEmployeeSearch(string edu_no)
        {
            DataTable empData = employeeEduService.AttendEmployee_Search(edu_no);

            return ViewBag.attEmployeeData = Json(Public_Function.DataTableToJSON(empData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [CheckSession]
        [HttpPost]
        public JsonResult GroupSearch(EmployeeEdu iModel)
        {
            DataTable groupData = employeeEduService.EmployeeEdu_FindGroup(iModel);

            return ViewBag.GroupData = Json(Public_Function.DataTableToJSON(groupData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult AttendGroupSearch(string edu_no)
        {
            DataTable groupData = employeeEduService.EmployeeEdu_FindAttendGroup(edu_no);

            return ViewBag.attGroupData = Json(Public_Function.DataTableToJSON(groupData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult AttatchedFileSearch(string edu_no)
        {
            DataTable fileData = employeeEduService.FileSearch(edu_no);

            return ViewBag.fileData = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public ActionResult EmployeeEduFindGroup(string edu_no)
        {
            EmployeeEdu iModel = new EmployeeEdu();
            iModel.edu_no = edu_no;

            DataTable deptData = employeeEduService.EmployeeEdu_FindGroup(iModel);
            ViewBag.DepartmentData = Json(Public_Function.DataTableToJSON(deptData));
            return View();
        }

        [CheckSession]
        [HttpPost]
        public ActionResult EmployeeFindLocation(string edu_no)
        {
            EmployeeEdu iModel = new EmployeeEdu();
            iModel.edu_no = edu_no;

            DataTable locData = employeeEduService.EmployeeEdu_FindLocation(iModel);
            ViewBag.LocationData = Json(Public_Function.DataTableToJSON(locData));
            return View();

        }

        
        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduGetInsertInfo()
        {
            List<string> result = employeeEduService.EmployeeEdu_GetInsertInfo();

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduCRUD(EmployeeEdu iModel, string gubun)
            
        {

            if (iModel == null)
                return Json("");
            else
            {
                string res = employeeEduService.EmployeeEdu_CRUD(iModel, gubun);
                //var resJson = "{ \"messege\": \"" + res + "\" }";

                return Json(res);
            }

        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEdu_YearPlan(EmployeeEdu iModel)
        {

            if (iModel == null)
                return Json("");
            else
            {
                string res = employeeEduService.EmployeeEdu_YearPlan(iModel);
                //var resJson = "{ \"messege\": \"" + res + "\" }";

                return Json(res);
            }
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduPushNotice(string edu_no)
        {

            if (edu_no == null)
                return Json("");
            else
            {
                string res = employeeEduService.EmployeeEdu_PushNotice(edu_no);
                //var resJson = "{ \"messege\": \"" + res + "\" }";

                return Json(res);
            }
        }



        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduAddEmployee(string edu_no, string emp_cd)
        {
            EmployeeEdu iModel = new EmployeeEdu();

            iModel.edu_no = edu_no;
            iModel.emp_cd = emp_cd;

            if (iModel.edu_no == null)
                return Json("");
            else
            {
                string res = employeeEduService.EmployeeEdu_AddEmployee(iModel);
                //var resJson = "{ \"messege\": \"" + res + "\" }";

                return Json(res);
            }

        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduDeleteEmployee(string edu_no, string emp_cd)
        {
            EmployeeEdu iModel = new EmployeeEdu();

            iModel.edu_no = edu_no;
            iModel.emp_cd = emp_cd;

            if (iModel.edu_no == null)
                return Json("");
            else
            {
                string res = employeeEduService.EmployeeEdu_DeleteEmployee(iModel);
                //var resJson = "{ \"messege\": \"" + res + "\" }";

                return Json(res);
            }

        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduAddGroup(string edu_no, string dept_cd)
        {
            EmployeeEdu iModel = new EmployeeEdu();

            iModel.edu_no = edu_no;
            iModel.emp_group_cd = dept_cd;

            if (iModel.edu_no == null)
                return Json("");
            else
            {
                string res = employeeEduService.EmployeeEdu_AddEmployeeGroup(iModel);
                //var resJson = "{ \"messege\": \"" + res + "\" }";

                return Json(res);
            }

        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduDeleteGroup(string edu_no, string dept_cd)
        {
            EmployeeEdu iModel = new EmployeeEdu();

            iModel.edu_no = edu_no;
            iModel.emp_group_cd = dept_cd;

            if (iModel.edu_no == null)
                return Json("");
            else
            {
                string res = employeeEduService.EmployeeEdu_DeleteEmployeeGroup(iModel);
                //var resJson = "{ \"messege\": \"" + res + "\" }";

                return Json(res);
            }

        }

        [CheckSession]
        [HttpPost]
        public ActionResult FileAdd(string edu_no)
        {
            EmployeeEdu iModel = new EmployeeEdu();
            string message = "";

            iModel.edu_no = edu_no;

            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                iModel.doc_name = fileName;
                iModel.doc_type = contentType;

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = employeeEduService.FileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, contentType, iModel);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
        }

        [CheckSession]
        [HttpPost]
        public ActionResult FileDelete(string edu_no, string file_id)
        {
            string message = "";

            if (file_id == null)
                return Json(message);

            message = employeeEduService.FileDelete(edu_no, file_id);

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
        }

        [HttpPost]
        public ActionResult FileOpen1(string file_id)
        {
            string message = "";

            message = employeeEduService.FileOpen(file_id);


            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
        }

        [HttpGet]
        public ActionResult FileOpen(string file_id)
        {
            DataTable dt = employeeEduService.EditActionFileOpen(file_id);

            var tmp = Path.GetExtension(dt.Rows[0].ItemArray[1].ToString());

            string mimeType = GetMimeType(tmp);


            return File(
                (byte[])dt.Rows[0].ItemArray[0]
                , mimeType
                , dt.Rows[0].ItemArray[1].ToString()
                );
        }

        #endregion

        #region EmployeeEduResult - 교육 확인 (결과입력)

        public ActionResult EmployeeEduResult(EmployeeEduResult iModel)
        {
            //교육상태
            DataTable EduState = selectBoxService.GetSelectBox("공통코드", "D", "EC001", "edu_state");
            ViewBag.EduState = EduState;

            string moduleType="", signCode = "";
            DataTable programSet = employeeEduResultService.GetProgramSet();

            foreach (DataRow row in programSet.DataSet.Tables[1].Rows)
            {
                //parameter_cd == sign_cod -> parameter_value
                if(row["parameter_cd"].ToString() == "sign_cod")
                {
                    signCode = row["parameter_value"].ToString();
                }
                //parameter_cd == module_type -> parameter_value
                if (row["parameter_cd"].ToString() == "module_type")
                {
                    moduleType = row["parameter_value"].ToString();
                }
            }

            //moduleType = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString(); //module_type
            //signCode = programSet.DataSet.Tables[1].Rows[1].ItemArray[1].ToString(); //sign_code

            ViewBag.EER_moduleType = moduleType;
            ViewBag.EER_signCode = signCode;


            iModel.sdate = DateTime.Today.ToShortDateString();
            iModel.edate = DateTime.Today.AddMonths(1).ToShortDateString();
            iModel.title = "";
            iModel.emp_cd = "";

            DataTable eduData = employeeEduResultService.EmployeeEduResult_Search(iModel);
            DataTable empData = employeeEduResultService.EmployeeEduResult_SearchEmp(iModel);

            //edu_no 구하기
            string EDU_NO = "";
            //첫번째 행의 데이터 가져오기
            if (eduData.Rows.Count > 0)
            {
                EDU_NO = (eduData.DefaultView.ToTable(false, "edu_no").Rows[0])[0].ToString();
            }
            DataTable AttempData = employeeEduResultService.EmployeeEduResult_SearchAttendEmp(EDU_NO);

            ViewBag.EmployeeEduResult = Json(Public_Function.DataTableToJSON(eduData));
            ViewBag.EmployeeEduResultAttEmployee = Json(Public_Function.DataTableToJSON(AttempData));
            ViewBag.EmployeeEduResultEmp = Json(Public_Function.DataTableToJSON(empData));

            DataTable signData = employeeEduResultService.EmployeeEduResult_SearchSignInfo("9101");
            ViewBag.EmployeeEduResultSign = Json(Public_Function.DataTableToJSON(signData));

            return View();
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduResult_Search(EmployeeEduResult iModel)
        {
            DataTable eduData = employeeEduResultService.EmployeeEduResult_Search(iModel);

            return ViewBag.EmployeeEduResult = Json(Public_Function.DataTableToJSON(eduData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduResult_AttendEmployeeSearch(string edu_no)
        {
            DataTable empData = employeeEduResultService.EmployeeEduResult_SearchAttendEmp(edu_no);

            return ViewBag.EmployeeEduResultAttEmployee = Json(Public_Function.DataTableToJSON(empData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduResult_Update(EmployeeEduResult iModel)

        {

            if (iModel == null)
                return Json("");
            else
            {
                string res = employeeEduResultService.EmployeeEduResult_Update(iModel);
                //var resJson = "{ \"messege\": \"" + res + "\" }";

                return Json(res);
            }

        }

        [CheckSession]
        [HttpPost]
        public ActionResult EmployeeEduResult_FileAdd(string edu_no, string emp_cd)
        {
            EmployeeEduResult iModel = new EmployeeEduResult();
            string message = "";

            iModel.edu_no = edu_no;
            iModel.upload_emp_cd = emp_cd;

            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                iModel.doc_name = fileName;
                iModel.doc_type = contentType;

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = employeeEduResultService.EmployeeEduResult_FileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, contentType, iModel);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
        }

        [CheckSession]
        [HttpPost]
        public ActionResult EmployeeEduResult_FileDelete(string edu_no, string file_id)
        {
            string message = "";

            if (file_id == null)
                return Json(message);

            message = employeeEduResultService.EmployeeEduResult_FileDelete(edu_no, file_id);

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
        }

        [HttpPost]
        public ActionResult EmployeeEduResult_FileOpen(string file_id)
        {
            string message = "";

            message = employeeEduResultService.EmployeeEduResult_FileOpen(file_id);


            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduResult_SettingSign(string index_key, string moduleType, string signCode, string signSeq)
        {
            DataTable item = employeeEduResultService.SettingSign(index_key, moduleType, signCode);
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
        public JsonResult EmployeeEduResult_SignerSearch(string index_key, string moduleType, string signCode, string signSeq)
        {
            DataTable item = employeeEduResultService.SignerSearch(index_key, moduleType, signCode, signSeq);
            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduResult_GetModifiableAuthority(string index_key, string moduleType, string emp_cd)
        {
            string result = "";

            bool check = employeeEduResultService.GetModifiableAuthority(index_key, moduleType, emp_cd);
            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduResult_CancelSign(string index_key, string moduleType, string signSeq, string docNo, string revisionNo)
        {
            string result = "";

            bool check = employeeEduResultService.CancelSign(index_key, moduleType, signSeq, docNo, revisionNo);

            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduResult_SaveSign(string index_key, string moduleType, string signCode, string signSeq, string emp_cd, string validation_type, string representative_yn, string docNo, string revisionNo)
        {
            string result = "";

            bool check = employeeEduResultService.SaveSign(index_key, moduleType, signCode, signSeq, emp_cd, validation_type, representative_yn, docNo, revisionNo);

            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduResult_GetRepresentativeAuthority(string emp_cd, string signSeq, string signCode)
        {
            string result = "";

            bool check = employeeEduResultService.GetRepresentativeAuthority(emp_cd, signSeq, signCode);

            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }


        #endregion

        #region EmployeeEduGrade - 교육 평가

        public ActionResult EmployeeEduGrade(EmployeeEduGrade iModel)
        {

            iModel.sdate = DateTime.Today.ToShortDateString();
            iModel.edate = DateTime.Today.AddMonths(1).ToShortDateString();
            iModel.title = "";

            DataTable eduData = employeeEduGradeService.EmployeeEduGrade_Search(iModel);

            //string moduleType, signCode = "";
            //DataTable programSet = employeeEduGradeService.GetProgramSet();
            //moduleType = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString(); //module_type ->이거 아니여-0- 다 다르자나
            //signCode = programSet.DataSet.Tables[1].Rows[1].ItemArray[1].ToString(); //sign_code

            string moduleType = "", signCode = "";
            DataTable programSet = employeeEduGradeService.GetProgramSet();

            foreach (DataRow row in programSet.DataSet.Tables[1].Rows)
            {
                //parameter_cd == sign_cod -> parameter_value
                if (row["parameter_cd"].ToString() == "sign_cod")
                {
                    signCode = row["parameter_value"].ToString();
                }
                //parameter_cd == module_type -> parameter_value
                if (row["parameter_cd"].ToString() == "module_type")
                {
                    moduleType = row["parameter_value"].ToString();
                }
            }


            ViewBag.EEG_moduleType = moduleType;
            ViewBag.EEG_signCode = signCode;

            string EDU_NO = "";
            //첫번째 행의 데이터 가져오기
            if (eduData.Rows.Count > 0)
            {
                EDU_NO = (eduData.DefaultView.ToTable(false, "edu_no").Rows[0])[0].ToString();
            }

            DataTable AttempData = employeeEduGradeService.EmployeeEduGrade_SearchEmp(EDU_NO);
            DataTable fileData = employeeEduGradeService.EmployeeEduGrade_SearchFile(EDU_NO);

            ViewBag.EmployeeEduGrade = Json(Public_Function.DataTableToJSON(eduData));
            ViewBag.EmployeeEduGradeAttEmployee = Json(Public_Function.DataTableToJSON(AttempData));
            ViewBag.EmployeeEduGradeFile = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));


            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduGrade_AttatchedFileSearch(string edu_no)
        {
            DataTable fileData = employeeEduGradeService.EmployeeEduGrade_SearchFile(edu_no);

            return ViewBag.EmployeeEduGradeFile = Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }



        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduGrade_Search(EmployeeEduGrade iModel)
        {
            DataTable eduData = employeeEduGradeService.EmployeeEduGrade_Search(iModel);

            return ViewBag.EmployeeEduGrade = Json(Public_Function.DataTableToJSON(eduData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduGrade_AttendEmployeeSearch(string edu_no)
        {
            DataTable empData = employeeEduGradeService.EmployeeEduGrade_SearchEmp(edu_no);

            return ViewBag.EmployeeEduGradeAttEmployee = Json(Public_Function.DataTableToJSON(empData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduGrade_Update(EmployeeEduGrade iModel)

        {

            if (iModel == null)
                return Json("");
            else
            {
                string res = employeeEduGradeService.EmployeeEduGrade_Update(iModel);
                //var resJson = "{ \"messege\": \"" + res + "\" }";

                return Json(res);
            }

        }


        [HttpPost]
        public ActionResult EmployeeEduGrade_FileOpen(string file_id)
        {
            string message = "";

            message = employeeEduGradeService.EmployeeEduGrade_FileOpen(file_id);


            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message); ;
        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduGrade_SettingSign(string index_key, string moduleType, string signCode, string signSeq)
        {
            DataTable item = employeeEduGradeService.SettingSign(index_key, moduleType, signCode);
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
        public JsonResult EmployeeEduGrade_SignerSearch(string index_key, string moduleType, string signCode, string signSeq)
        {
            DataTable item = employeeEduGradeService.SignerSearch(index_key, moduleType, signCode, signSeq);
            string jsonData = Public_Function.DataTableToJSON(item);

            return Json(jsonData);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduGrade_GetModifiableAuthority(string index_key, string moduleType, string emp_cd)
        {
            string result = "";

            bool check = employeeEduGradeService.GetModifiableAuthority(index_key, moduleType, emp_cd);
            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduGrade_CancelSign(string index_key, string moduleType, string signSeq, string docNo, string revisionNo)
        {
            string result = "";

            bool check = employeeEduGradeService.CancelSign(index_key, moduleType, signSeq, docNo, revisionNo);

            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduGrade_SaveSign(string index_key, string moduleType, string signCode, string signSeq, string emp_cd, string validation_type, string representative_yn, string docNo, string revisionNo)
        {
            string result = "";

            bool check = employeeEduGradeService.SaveSign(index_key, moduleType, signCode, signSeq, emp_cd, validation_type, representative_yn, docNo, revisionNo);

            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }


        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduGrade_GetRepresentativeAuthority(string emp_cd, string signSeq, string signCode)
        {
            string result = "";

            bool check = employeeEduGradeService.GetRepresentativeAuthority(emp_cd, signSeq, signCode);

            if (check == true) result = "Y";
            else result = "N";

            return Json(result);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduGrade_EduFinished(string edu_no)
        {
            string result = "";

            result = employeeEduGradeService.EmployeeEduGradeEduFinished(edu_no);

            return Json(result);
        }


        #endregion

        #region EmployeeEduHistory - 교육 이력 조회

        public ActionResult EmployeeEduHistory(EmployeeEduHistory iModel)
        {
            iModel.sdate = DateTime.Today.ToShortDateString();
            iModel.edate = DateTime.Today.AddMonths(1).ToShortDateString();

            DataTable eduHistoryData = employeeEduHistoryService.EmployeeEduHistory_Search(iModel);

            DataTable empData = employeeEduHistoryService.EmployeeEduHistory_EmployeeSearch(iModel);
            DataTable deptData = employeeEduHistoryService.EmployeeEduHistory_DepartmentSearch(iModel);

            ViewBag.EmployeeEduHistoryData = Json(Public_Function.DataTableToJSON(eduHistoryData));
            ViewBag.EmployeeEduHistoryEmp = Json(Public_Function.DataTableToJSON(empData));
            ViewBag.EmployeeEduHistoryDept = Json(Public_Function.DataTableToJSON(deptData));

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult EmployeeEduHistorySearch(EmployeeEduHistory iModel)
        {
            DataTable eduHistoryData = employeeEduHistoryService.EmployeeEduHistory_Search(iModel);

            return ViewBag.EmployeeEduHistoryData = Json(Public_Function.DataTableToJSON(eduHistoryData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }
        #endregion

        #region QualificationInfo - 사원별 자격관리

        public ActionResult QualificationInfo(QualificationInfo iModel)
        {
            iModel.emp_cd = "";
            //교육상태
            DataTable License_cd = selectBoxService.GetSelectBox("공통코드", "D", "CM055", "License_cd");

            ViewBag.LicenseData = License_cd;

            DataTable empData = qualificationInfoService.QualificationInfo_Search(iModel);

            //emp_cd 구하기
            string EMP_CD = "";
            //첫번째 행의 데이터 가져오기
            if (empData.Rows.Count > 0)
            {
                EMP_CD = (empData.DefaultView.ToTable(false, "emp_cd").Rows[0])[0].ToString();
            }
            DataTable licenseData = qualificationInfoService.QualificationInfo_SearchLicenseInfo(EMP_CD);


            ViewBag.QualificationInfoEmpData = Json(Public_Function.DataTableToJSON(empData));
            ViewBag.QualificationInfoLicenseData = Json(Public_Function.DataTableToJSON(licenseData));


            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult QualificationInfo_Search(QualificationInfo iModel)
        {
            DataTable empData = qualificationInfoService.QualificationInfo_Search(iModel);

            return ViewBag.QualificationInfoEmpData = Json(Public_Function.DataTableToJSON(empData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult QualificationInfo_SearchLicense(string emp_cd)
        {
            DataTable empData = qualificationInfoService.QualificationInfo_SearchLicenseInfo(emp_cd);

            return ViewBag.QualificationInfoLicenseData = Json(Public_Function.DataTableToJSON(empData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult QualificationInfo_SearchEduIn(string emp_cd)
        {
            DataTable eduData = qualificationInfoService.QualificationInfo_Search_EduIn(emp_cd);

            return Json(Public_Function.DataTableToJSON(eduData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult QualificationInfo_SearchEduOut(string emp_cd)
        {
            DataTable eduData = qualificationInfoService.QualificationInfo_Search_EduOut(emp_cd);

            return Json(Public_Function.DataTableToJSON(eduData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult QualificationInfo_SearchFile(string emp_cd, string license_cd)
        {
            DataTable fileData = qualificationInfoService.QualificationInfo_SearchFile(emp_cd, license_cd);

            return Json(Public_Function.DataTableToJSON(fileData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult QualificationInfo_CRUD(QualificationInfo iModel, string gubun)

        {

            if (iModel == null)
                return Json("");
            else
            {
                string res = qualificationInfoService.QualificationInfo_CRUD(iModel, gubun);
                //var resJson = "{ \"messege\": \"" + res + "\" }";

                return Json(res);
            }

        }
        

        [CheckSession]
        [HttpPost]
        public ActionResult QualificationInfo_FileAdd(QualificationInfo iModel)
        {
            string message = "";

            try
            {
                var uploadFile = Request.Files[0];

                var fileName = uploadFile.FileName;
                var contentType = uploadFile.ContentType;
                //var fgubun = name.Substring(name.Length - 1, 1);

                iModel.doc_name = fileName;
                iModel.doc_type = contentType;

                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    message = qualificationInfoService.QualificationInfo_FileAdd(binaryReader.ReadBytes(Request.Files[0].ContentLength), fileName, contentType, iModel);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
        }

        [CheckSession]
        [HttpPost]
        public ActionResult QualificationInfo_FileDelete(QualificationInfo iModel)
        {
            string message = "";

            if (iModel.file_id == null)
                return Json(message);

            message = qualificationInfoService.QualificationInfo_FileDelete(iModel);

            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
        }

        [CheckSession]
        [HttpPost]
        public ActionResult QualificationInfo_FileOpen(string file_id)
        {
            string message = "";

            message = qualificationInfoService.QualificationInfo_FileOpen(file_id);


            //var resJson = "{ \"messege\": \"" + message + "\" }";
            return Json(message);
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