using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraSpellChecker.Parser;
using HACCP.Attribute;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models;
using HACCP.Models.SysOp;
using HACCP.Services.Comm;
using HACCP.Services.SysOp;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class SysOpController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SysOpController));
        private Group_manageService group_manageService = new Group_manageService();
        private SelectBoxService selectBoxService = new SelectBoxService();

        /*
         // @message, @sys_plant_cd, @sys_emp_cd 이렇게 세 개 입니다.
         //
         // WHERE	
			(A.emp_cd LIKE @s_emp_cd + '%' OR A.emp_nm LIKE @s_emp_cd + '%')
			AND (@s_gubun = '0' OR (@s_gubun = '1' AND C.emp_cd is not null) OR (@s_gubun = '2' AND C.use_check = 'Y'))   
			AND a.sys_plant_cd = @sys_plant_cd	
            */


        #region Group_manage - 사용자 그룹 등록

        [Route("SysOp/Group_manage")]
        [HttpGet]
        public ActionResult Group_manage(string group_text, string use_yn_S)
        {
            // 입력 파라미터 값이 null 이거나 공백일 경우, "" 으로 설정
            if (group_text == null || group_text == "")
            {
                group_text = "";
            }

            if (use_yn_S == null || use_yn_S == "")
            {
                use_yn_S = "1";
            }

            // 사용자 그룹 등록 데이터
            DataTable Group_manage = group_manageService.Select(group_text, use_yn_S);

            ViewBag.Group_manage = Json(Public_Function.DataTableToJSON(Group_manage));
            ViewBag.Group_manageAuth = Json(HttpContext.Session["Group_manageAuth"]);

            return View();
        }


        [HttpPost]
        public JsonResult Group_manageSelect(string group_text, string use_yn_S)
        {
            if (group_text == null || group_text == "")
            {
                group_text = "";
            }

            if (use_yn_S == null || use_yn_S == "" || use_yn_S == "전체로 보기")
            {
                use_yn_S = "1";
            }
            else
            {
                use_yn_S = "2";
            }

            // 사용자 그룹 등록 데이터
            DataTable Group_manage = group_manageService.Select(group_text, use_yn_S);
            //List<Group_manage> group_manages = null;// = group_manageService.Select(group_text, use_yn_S);

            return Json(Public_Function.DataTableToJSON(Group_manage));
        }

        /// <summary>
        /// 입력, 수정, 삭제 기능
        /// </summary>
        /// <param name="gModel"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult Group_manageCRUD(Group_manage gModel, string gubun)
        {

            if (!gubun.Equals("D"))
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Ok = false });
                }
            }

            string res = group_manageService.groupManageCRUD(gModel, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }


        /*[Route("SysOp/Group_manageSave")]
        [HttpPost]
        public ActionResult Group_manageSave(Group_manage group_manage)
        {
            group_manageService.Save(group_manage);

            return Json(new { Ok = true });
        }*/

       /* [Route("SysOp/Group_manageDelete")]
        [HttpPost]
        public bool Group_manageDelete(string pGroup_manage_Code)
        {
            log.Info("그룹 코드:" + pGroup_manage_Code);

            bool isSucceed = group_manageService.Delete(pGroup_manage_Code);
            return isSucceed;
        }*/
        #endregion Group_manage



        #region GroupMenuManage - 그룹 권한 설정

        private GroupMenuService groupMenuService = new GroupMenuService();

        /// <summary>
        /// 그룹권한 설정 첫 페이지 생성
        /// </summary>
        /// <returns></returns>

        public ActionResult GroupMenuManage()
        {
            DataTable moduleGubun = selectBoxService.GetSelectBox("공통코드", "N", "CM015", "moduleGubun");
            DataTable companyGridData = groupMenuService.getCompanyGridData("", "");
            DataTable programGridData;

            if (moduleGubun.Rows.Count > 0 && companyGridData.Rows.Count > 0)
            {
                object[] firstCompany = companyGridData.Rows[0].ItemArray;
                object[] firstModule = moduleGubun.Rows[0].ItemArray;
                programGridData = groupMenuService.getProgramGridData(firstCompany[0].ToString(), firstModule[0].ToString(), (string)HttpContext.Session["loginID"]);
            }
            else
            {
                programGridData = null;
            }


            if (HttpContext.Session["loginID"] == null)
            {
                return View("Fail");
            }


            ViewBag.moduleGubun = moduleGubun;
            ViewBag.companyGridData = Json(Public_Function.DataTableToJSON(companyGridData));
            ViewBag.programGridData = Json(Public_Function.DataTableToJSON(programGridData));
            ViewBag.groupMenuManageAuth = Json(HttpContext.Session["GroupMenuManageAuth"]);

            return View();
        }

        /// <summary>
        /// 프로그램 권한 데이터 조회
        /// </summary>
        /// <param name="empCd"></param>
        /// <param name="moduleGb"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectProgram(string empCd, string moduleGb)
        {
            DataTable programGridData = groupMenuService.getProgramGridData(empCd, moduleGb, (string)HttpContext.Session["loginID"]);

            return Json(Public_Function.DataTableToJSON(programGridData));
        }

        /// <summary>
        /// 프로그램 권한 데이터 수정
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>

        [HttpPost]
        public bool UpdateProgram(ProgramAuthority program)
        {

            ProgramAuthority groupMenu = groupMenuService.setBool(program);

            string str = groupMenuService.updateProgram(program, (string)HttpContext.Session["loginID"]);

            if (str.Equals(""))
            {
                return false;
            }

            return true;
        }

        #endregion



        #region MenuUser - 사용자 등록

        private MenuUserService menuUserService = new MenuUserService();

        /// <summary>
        /// 사용자 등록 첫 페이지 생성
        /// </summary>
        /// <param name="emp_cd"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>

        [Route("SysOp/MenuUser")]
        [HttpGet]
        public ActionResult MenuUser(string emp_cd, string gubun)
        {
            log.Debug("emp_cd=" + emp_cd);
            log.Debug("gubun=" + gubun);

            List<MenuUser> menuUsers = menuUserService.Select(emp_cd, gubun);

            DataTable userPopupList = menuUserService.SelectUser();

            ViewBag.menuUsers = Json(JsonConvert.SerializeObject(menuUsers));
            ViewBag.userPopupList = Json(Public_Function.DataTableToJSON(userPopupList));
            ViewBag.MenuUserAuth = Json(HttpContext.Session["MenuUserAuth"]);

            return View();
        }

        /// <summary>
        /// 사용자 상세정보 조회
        /// </summary>
        /// <param name="pMenuUser"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectMenuUser(MenuUser pMenuUser)
        {
            List<MenuUser> menuUsers = menuUserService.Select(pMenuUser.emp_cd, pMenuUser.user_security);

            var jsonResult = Json(JsonConvert.SerializeObject(menuUsers));
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        /// <summary>
        /// 사용자 사인 정보 조회
        /// </summary>
        /// <param name="fingerprint_emp"></param>
        /// <returns></returns>

        [HttpPost]
        public string SelectSign(string fingerprint_emp)
        {
            string imageSrc = menuUserService.selectEmpSign(fingerprint_emp);

            return imageSrc;
        }

        /// <summary>
        /// 사용자 사인 수정
        /// </summary>
        /// <param name="empCd"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult UploadSignAsync(string empCd)
        {
            try
            {

                using (var binaryReader = new BinaryReader(Request.Files["signImage"].InputStream))
                {
                    menuUserService.updateSign( binaryReader.ReadBytes(Request.Files["signImage"].ContentLength), empCd);
                }

                //var myBytes = await GetByteArrayFromImageAsync(Request.Files["singImage"]);

                //async Task<byte[]> GetByteArrayFromImageAsync(IFormFile file)
                //{
                //    using (var target = new MemoryStream())
                //    {
                //        await file.CopyToAsync(target);
                //        return target.ToArray();
                //    }
                //}

                //menuUserService.updateSign(myBytes, empCd);

            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 사용자 ID 중복 확인
        /// </summary>
        /// <param name="pMenuUser"></param>
        /// <returns></returns>

        [HttpPost]
        public int IdDuplicateCheck(MenuUser pMenuUser)
        {
            if (String.IsNullOrEmpty(pMenuUser.user_id) || String.IsNullOrEmpty(pMenuUser.emp_cd))
            {
                return -1;
            }

            int res = menuUserService.idDuplicateCheck(pMenuUser);

            return res;
        }

        /// <summary> 
        ///  사용자 비밀번호 재사용 체크
        /// </summary>
        /// <param name="pMenuUser"></param>
        /// <returns></returns>

        [HttpPost]
        public string PasswordReuseCheck(MenuUser pMenuUser)
        {
            string message = menuUserService.passwordReuseCheck(pMenuUser);

            return message;
        }

        /// <summary>
        /// 사용자 상세정보 수정, 삭제
        /// </summary>
        /// <param name="pMenuUser"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>

        [HttpPost]
        public string MenuUserCRUD(MenuUser pMenuUser, string gubun)
        {
            string res = menuUserService.CRUD(pMenuUser, gubun);

            return res;
        }

        #endregion



        #region Employee_group_manage - 사원/그룹 설정

        private Employee_group_manageService employee_Group_ManageService = new Employee_group_manageService();

        /// <summary>
        /// 사원/그룹 설정 첫 페이지 생성
        /// </summary>
        /// <returns></returns>

        public ActionResult Employee_group_manage()
        {
            DataTable employees = employee_Group_ManageService.selectEmployee();
            DataTable groups = employee_Group_ManageService.selectGroup("");
            DataTable employeesInGroup;

            if (groups.Rows.Count > 0)
            {
                employeesInGroup = employee_Group_ManageService.selectEmployeesInGroup(groups.Rows[0].ItemArray[0].ToString(), "");
            }
            else
            {
                employeesInGroup = null;
            }

            ViewBag.employees = Json(Public_Function.DataTableToJSON(employees));
            ViewBag.groups = Json(Public_Function.DataTableToJSON(groups));
            ViewBag.employeesInGroup = Json(Public_Function.DataTableToJSON(employeesInGroup));

            return View();
        }

        /// <summary>
        /// 사원, 부서 정보 조회
        /// </summary>
        /// <param name="empGroupNm"></param>
        /// <param name="empNm"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectEmployeeAndGroup(string empGroupNm, string empNm)
        {
            if(empGroupNm == null)
            {
                empGroupNm = "";
            }
            if (empNm == null)
            {
                empNm = "";
            }

            DataTable groups = employee_Group_ManageService.selectGroup(empGroupNm);
            DataTable employeesInGroup = employee_Group_ManageService.selectEmployeesInGroup(groups.Rows[0].ItemArray[0].ToString(), empNm);

            List<string> jsonList = new List<string>();

            jsonList.Add(Public_Function.DataTableToJSON(groups));
            jsonList.Add(Public_Function.DataTableToJSON(employeesInGroup));

            return Json(jsonList.ToArray());
        }

        /// <summary>
        /// 특정 부서내 사원 조회
        /// </summary>
        /// <param name="empGroupCd"></param>
        /// <param name="empNm"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectEmployeeInGroup(string empGroupCd, string empNm)
        {
            if (empNm == null)
            {
                empNm = "";
            }

            DataTable employeesInGroup = employee_Group_ManageService.selectEmployeesInGroup(empGroupCd, empNm);

            return Json(Public_Function.DataTableToJSON(employeesInGroup));
        }

        /// <summary>
        /// 부서에 사원 추가, 제거
        /// </summary>
        /// <param name="empGroupCd"></param>
        /// <param name="empCd"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult EmployeeCRUD(string empGroupCd, string empCd, string gubun)
        {
            string res = employee_Group_ManageService.employeeCRUD(empGroupCd, empCd, gubun);

            DataTable employeesInGroup = employee_Group_ManageService.selectEmployeesInGroup(empGroupCd, "");
            DataTable groupOfEmployee = employee_Group_ManageService.selectGroupOfEmployee(empCd, "");

            List<string> jsonList = new List<string>();

            jsonList.Add(res);
            jsonList.Add(Public_Function.DataTableToJSON(employeesInGroup));
            jsonList.Add(Public_Function.DataTableToJSON(groupOfEmployee));

            return Json(jsonList.ToArray());
        }

        /// <summary>
        /// 특정 사원의 부서 정보 조회
        /// </summary>
        /// <param name="empCd"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectGroupOfEmployee(string empCd)
        {
            DataTable groupOfEmployee = employee_Group_ManageService.selectGroupOfEmployee(empCd, "");

            return Json(Public_Function.DataTableToJSON(groupOfEmployee));
        }


        #endregion



        #region ProgramUserManage - 프로그램 권한 설정

        private ProgramUserManageService programUserManageService = new ProgramUserManageService();

        /// <summary>
        /// 프로그램 권한 설정 첫 페이지 생성
        /// </summary>
        /// <returns></returns>

        public ActionResult ProgramUserManage()
        {
            DataTable moduleGubun = selectBoxService.GetSelectBox("공통코드", "N", "CM015", "moduleGubun");

            object[] firstModule = moduleGubun.Rows[0].ItemArray;

            if (HttpContext.Session["loginID"] == null)
            {
                return RedirectToAction("LoginForm", "Comm");
            }

            DataTable programGridData = programUserManageService.getProgramData( firstModule[0].ToString(), (string)HttpContext.Session["loginID"]);

            ViewBag.moduleGubun = moduleGubun;
            ViewBag.programGridData = Json(Public_Function.DataTableToJSON(programGridData));

            return View();
        }

        /// <summary>
        /// 특정 메뉴의 사용자별 권한 조회
        /// </summary>
        /// <param name="gubun"></param>
        /// <param name="formCd"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectUser(string gubun, string formCd)
        {
            DataTable userGridData = programUserManageService.getUserData(gubun, formCd);

            return Json(Public_Function.DataTableToJSON(userGridData));
        }

        /// <summary>
        /// 프로그램 리스트 조회
        /// </summary>
        /// <param name="moduleGb"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectrogramUserManageProgram(string moduleGb)
        {
            DataTable programGridData = programUserManageService.getProgramData(moduleGb, (string)HttpContext.Session["loginID"]);

            return Json(Public_Function.DataTableToJSON(programGridData));
        }

        /// <summary>
        /// 프로그램별 사용자 권한 수정
        /// </summary>
        /// <param name="programUser"></param>
        /// <returns></returns>

        [HttpPost]
        public string UpdateUser(ProgramUser programUser)
        {
            programUser = programUserManageService.setBool(programUser);

            string res = programUserManageService.updateUser(programUser, (string)HttpContext.Session["loginID"]);

            return res;
        }

        #endregion



        #region UserMenuManage - 사용자 권한 설정

        private UserMenuManageService userMenuManageService = new UserMenuManageService();

        /// <summary>
        /// 사용자 권한 설정 첫 페이지 생성
        /// </summary>
        /// <returns></returns>

        public ActionResult UserMenuManage()
        {
            DataTable moduleGubun = selectBoxService.GetSelectBox("공통코드", "N", "CM015", "moduleGubun");
            DataTable empData = userMenuManageService.getEmpData("", "", "0");

            object[] firstModule = moduleGubun.Rows[0].ItemArray;
            object[] firstEmp = empData.Rows[0].ItemArray;

            if (HttpContext.Session["loginID"] == null)
            {
                return RedirectToAction("LoginForm", "Comm");
            }

            DataTable programGridData = userMenuManageService.getProgramData(firstModule[0].ToString(), firstEmp[0].ToString(), (string)HttpContext.Session["loginID"]);

            DataTable empPopupData = userMenuManageService.getEmpPopupData();
            DataTable deptPopupData = userMenuManageService.getDeptPopupData();


            ViewBag.moduleGubun = moduleGubun;
            ViewBag.empData = Json(Public_Function.DataTableToJSON(empData));
            ViewBag.programGridData = Json(Public_Function.DataTableToJSON(programGridData));

            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.deptPopupData = Json(Public_Function.DataTableToJSON(deptPopupData));

            return View();
        }

        /// <summary>
        /// 특정 사용자의 프로그램 권한 조회
        /// </summary>
        /// <param name="moduleGb"></param>
        /// <param name="empCd"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult UserMenuSelectProgram(string moduleGb, string empCd)
        {
            DataTable programGridData = userMenuManageService.getProgramData(moduleGb, empCd, (string)HttpContext.Session["loginID"]);

            return Json(Public_Function.DataTableToJSON(programGridData));
        }

        /// <summary>
        /// 사원코드, 부서코드, 모듈구분 으로 사원과 프로그램 권한 조회
        /// </summary>
        /// <param name="moduleGb"></param>
        /// <param name="emp_cd"></param>
        /// <param name="dept_cd"></param>
        /// <param name="sGubun"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult UserMenuSelect(string moduleGb, string emp_cd, string dept_cd, string sGubun)
        {
            DataTable empData = userMenuManageService.getEmpData(dept_cd, emp_cd, sGubun);

            DataTable programGridData = new DataTable();
            if (empData.Rows.Count > 0)
            {
                object[] firstEmp = empData.Rows[0].ItemArray;
                programGridData = userMenuManageService.getProgramData(moduleGb, firstEmp[0].ToString(), (string)HttpContext.Session["loginID"]);
            }

            List<string> jsonList = new List<string>();

            jsonList.Add(Public_Function.DataTableToJSON(empData));
            jsonList.Add(Public_Function.DataTableToJSON(programGridData));

            return Json(jsonList.ToArray());
        }

        /// <summary>
        /// 사용자의 프로그램 권한 수정
        /// </summary>
        /// <param name="programAuthority"></param>
        /// <returns></returns>

        [HttpPost]
        public string UserMenuUpdate(ProgramAuthority programAuthority)
        {
            string res = userMenuManageService.updateUserMenu(programAuthority, (string)HttpContext.Session["loginID"]);

            return res;
        }

        /// <summary>
        /// 권한 복사
        /// </summary>
        /// <param name="empCd1"></param>
        /// <param name="empCd2"></param>
        /// <returns></returns>

        [HttpPost]
        public string CopyAuthority(string empCd1, string empCd2)
        {
            string res = userMenuManageService.colpyAuthority(empCd1, empCd2);

            return "";
        }

        #endregion



        #region SignSet_Input 전자서명 등록

        private SignSet_InputService signSet_InputService = new SignSet_InputService();

        /// <summary>
        /// 전자서명 등록 첫페이지 생성
        /// </summary>
        /// <returns></returns>

        public ActionResult SignSet_Input()
        {
            DataTable connectionPoint = selectBoxService.GetSelectBox("공통코드", "D", "QC053", "connectionPoint");
            DataTable moduleGubun = selectBoxService.GetSelectBox("공통코드", "D", "QC054", "moduleGubun");
            DataTable electronicSigniture = signSet_InputService.SelectSignitrueInfo("1");

            ViewBag.connectionPoint = connectionPoint;
            ViewBag.moduleGubun = moduleGubun;
            ViewBag.electronicSigniture = Json(Public_Function.DataTableToJSON(electronicSigniture));

            return View();
        }

        /// <summary>
        /// 전자서명 정보 CRUD
        /// </summary>
        /// <param name="electronicSigniture"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult SignSetCRUD(ElectronicSigniture electronicSigniture, string gubun)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Ok = false });
            }

            string res = signSet_InputService.CRDU(electronicSigniture, gubun);

            return Json(new { Ok = true });
        }

        /// <summary>
        /// 전자서명 목록 조회
        /// </summary>
        /// <param name="useYN"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectSignSet(string useYN)
        {
            DataTable electronicSigniture = signSet_InputService.SelectSignitrueInfo(useYN);

            return Json(Public_Function.DataTableToJSON(electronicSigniture).Replace("\r\n", "\\r\\n"));
        }

        #endregion



        #region SignSet_InputDetail 전자서명 권한 설정

        private SignSet_InputDetailService signSet_InputDetailService = new SignSet_InputDetailService();

        /// <summary>
        /// 전자서명 권한 설정 첫페이지 생성
        /// </summary>
        /// <returns></returns>

        public ActionResult SignSet_InputDetail()
        {
            DataTable signData = signSet_InputDetailService.selectSignData("", "1");

            object[] firstSignCD = signData.Rows[0].ItemArray;

            DataTable signatoryData = signSet_InputDetailService.selectSignatoryData(firstSignCD[0].ToString());

            object[] firstSignDT = signatoryData.Rows[0].ItemArray;

            DataTable delegateData = signSet_InputDetailService.selectDelegateData(firstSignCD[0].ToString(), firstSignDT[1].ToString());

            DataTable empData = signSet_InputDetailService.selectEmpData();
            DataTable signPopupData = signSet_InputDetailService.selectSignPopupData();

            ViewBag.signData = Json(Public_Function.DataTableToJSON(signData));
            ViewBag.signatoryData = Json(Public_Function.DataTableToJSON(signatoryData));
            ViewBag.delegateData = Json(Public_Function.DataTableToJSON(delegateData));
            ViewBag.empData = Json(Public_Function.DataTableToJSON(empData));
            ViewBag.signPopupData = Json(Public_Function.DataTableToJSON(signPopupData));

            return View();
        }

        [HttpPost]
        public JsonResult SelectSignSetData()
        {
            DataTable signData = signSet_InputDetailService.selectSignData("", "1");

            return Json(Public_Function.DataTableToJSON(signData).Replace("\r\n", "\\r\\n"));
        }

        /// <summary>
        /// 서명자 목록 조회
        /// </summary>
        /// <param name="sign_set_cd"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectSignatory(string sign_set_cd)
        {
            DataTable signatoryData = signSet_InputDetailService.selectSignatoryData(sign_set_cd);

            return Json(Public_Function.DataTableToJSON(signatoryData).Replace("\r\n", "\\r\\n"));
        }

        /// <summary>
        /// 대리자 목록 조회
        /// </summary>
        /// <param name="sign_set_cd"></param>
        /// <param name="sign_set_dt_id"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectDelegate(string sign_set_cd, string sign_set_dt_id)
        {
            DataTable delegateData = signSet_InputDetailService.selectDelegateData(sign_set_cd, sign_set_dt_id);

            return Json(Public_Function.DataTableToJSON(delegateData).Replace("\r\n", "\\r\\n"));
        }

        /// <summary>
        /// 서명자 CRUD
        /// </summary>
        /// <param name="signatory"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>

        [HttpPost]
        public string SignatoryCRUD(Signatory signatory, string gubun)
        {
            if (gubun == null)
            {
                gubun = "";
            }

            string res = signSet_InputDetailService.signatoryCRUD(signatory, gubun);

            return res;
        }

        /// <summary>
        /// 대리자 추가 삭제
        /// </summary>
        /// <param name="signatory"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>

        [HttpPost]
        public string DelegateCRUD(Signatory signatory, string gubun)
        {
            string res = signSet_InputDetailService.delegateCRUD(signatory, gubun);

            return res;
        }

        /// <summary>
        /// 전자서명 목록 조회
        /// </summary>
        /// <param name="select"></param>
        /// <param name="sign_set_cd"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectSignDetail(string select, string sign_set_cd)
        {
            DataTable signData = signSet_InputDetailService.selectSignData(sign_set_cd, select);

            return Json(Public_Function.DataTableToJSON(signData).Replace("\r\n", "\\r\\n"));
        }

        #endregion



        #region Position 컴퓨터 고유번호 관리

        private PositionService positionService = new PositionService();

        /// <summary>
        /// 컴퓨터 고유번호 관리 첫 페이지 생성
        /// </summary>
        /// <returns></returns>
        public ActionResult Position()
        {
            DataTable managementGubun = selectBoxService.GetSelectBox("공통코드", "D", "SC001", "managementGubun");
            DataTable startupProgram = selectBoxService.GetSelectBox("공통코드", "D", "SC014", "startupProgram");
            DataTable startupMonitoring = selectBoxService.GetSelectBox("공통코드", "D", "SC016", "startupMonitoring");
            DataTable positionData = positionService.SelectPositionData();
            DataTable managementTarget = positionService.SelectManagementTarget();
            DataTable monitoringWorkroom = positionService.SelectMonitoringWorkroom();

            ViewBag.managementGubun = managementGubun;
            ViewBag.startupProgram = startupProgram;
            ViewBag.startupMonitoring = startupMonitoring;
            ViewBag.positionData = Json(Public_Function.DataTableToJSON(positionData));
            ViewBag.managementTarget = Json(Public_Function.DataTableToJSON(managementTarget));
            ViewBag.monitoringWorkroom = Json(Public_Function.DataTableToJSON(monitoringWorkroom));

            return View();
        }

        /// <summary>
        /// 컴퓨터 위치정보 CRUD
        /// </summary>
        /// <param name="positionData"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult PositionCRUD(PositionData positionData, string gubun)
        {
            if (!ModelState.IsValid && !gubun.Equals("D"))
            {
                return Json(new { Ok = false });
            }

            string res = positionService.positionCRUD(positionData, gubun);

            return Json(new { Ok = true });
        }

        /// <summary>
        /// 컴퓨터 위치정보 목록 조회
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectPosition()
        {
            DataTable positionData = positionService.SelectPositionData();

            return Json(Public_Function.DataTableToJSON(positionData));
        }

        #endregion



        #region AnniversaryMaster 공휴일 등록

        private AnniversaryMasterService anniversaryMasterService = new AnniversaryMasterService();

        /// <summary>
        /// 공휴일 등록 쳇 페이지 생성
        /// </summary>
        /// <param name="pYear"></param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult AnniversaryMaster(string pYear = null)
        {
            if (pYear == null)
            {
                pYear = DateTime.Now.Year.ToString();
            }

            DataTable dt = anniversaryMasterService.Select(pYear);
            DataTable calendarGubun = selectBoxService.GetSelectBox("공통코드", "D", "PL001", "calendarGubun");
            ViewBag.dt = dt;
            string jsonData = Public_Function.DataTableToJSON(dt);
            ViewData["data"] = jsonData;
            ViewBag.jsonData = jsonData;
            ViewBag.calendarGubun = calendarGubun;
            log.Info(dt.Rows.Count.ToString());
            return View();
        }

        /// <summary>
        /// 회사기념일 CRUD
        /// </summary>
        /// <param name="anniversary"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult AnniversaryCRUD(Anniversary anniversary, string gubun)
        {
            if (!ModelState.IsValid && !gubun.Equals("D"))
            {
                return Json(new { Ok = false });
            }

            string res = anniversaryMasterService.anniversaryCRUD(anniversary, gubun);

            return Json(new { Ok = true });

        }

        /// <summary>
        /// 공휴일 목록 조회
        /// </summary>
        /// <param name="pYear"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SelectAnniversary(string pYear = null)
        {
            if (pYear == null)
            {
                pYear = DateTime.Now.Year.ToString();
            }

            DataTable dt = anniversaryMasterService.Select(pYear);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion



        #region CompanyCalendar Calendar 관리

        private CompanyCalendarService companyCalendarService = new CompanyCalendarService();

        /// <summary>
        /// Calendar 관리 첫 페이지 생성
        /// </summary>
        /// <returns></returns>

        public ActionResult CompanyCalendar()
        {
            DataTable schedule = companyCalendarService.selectSchedule("1", DateTime.Now.Year, DateTime.Now.Month);
            DataTable calendarCd = selectBoxService.GetSelectBox("공통코드", "N", "PL003", "calendarCd");

            ViewBag.schedule = Json(Public_Function.DataTableToJSON(schedule));
            ViewBag.calendarCd = calendarCd;
            ViewBag.CompanyCalendarAuth = Json(HttpContext.Session["CompanyCalendarAuth"]);
            //ViewBag.schedule = schedule;

            return View();
        }


        [HttpPost]
        public JsonResult SelectCalendar(string calendarCd, int year, int month)
        {
            DataTable schedule = companyCalendarService.selectSchedule(calendarCd, year, month);

            return Json(Public_Function.DataTableToJSON(schedule));
        }


        [HttpPost]
        public bool InputCalendar(string calendarCd, int year, int month)
        {
            string res = companyCalendarService.inputMonthSchedule(calendarCd, year, month);

            return true;
        }


        [HttpPost]
        public bool EditCalendar(Calendar calendar)
        {
            string res = companyCalendarService.updateCalndar(calendar);

            return true;
        }

        #endregion



        #region NewsWriteR2 공지사항 등록

        private NewsWriteR2Service newsWriteR2Service = new NewsWriteR2Service();


        public ActionResult NewsWriteR2()
        {
            DateTime now = DateTime.Now;

            DataTable noticeTarget = selectBoxService.GetSelectBox("공지구분", "S", "", "noticeTarget");
            //DataTable news = newsWriteR2Service.SelectNews("%", "%", now.AddDays(-(now.Day-1)).ToShortDateString(), now.ToShortDateString());

            var start_date = DateTime.Today.AddDays(-(DateTime.Today.Day - 1)).ToShortDateString();
            var end_date = DateTime.Today.ToShortDateString();
            //DataTable news = newsWriteR2Service.SelectNews("%", "%", DateTime.Today.AddDays(-(DateTime.Today.Day - 1)).ToShortDateString(), now.ToShortDateString());
            DataTable news = newsWriteR2Service.SelectNews("%", "%", start_date, end_date);

            string news_cd = "";
            string news_gb = "1";

            if (news.Rows.Count > 0)
            {
                object[] arr = news.Rows[0].ItemArray;
                news_cd = arr[0].ToString();
                news_gb = arr[1].ToString();
            }

            DataTable noticeTargetGridData = newsWriteR2Service.SelectNoticeTarget(news_cd, news_gb);

            ViewBag.noticeTarget = noticeTarget;
            ViewBag.news = Json(Public_Function.DataTableToJSON(news));
            ViewBag.noticeTargetGridData = Json(Public_Function.DataTableToJSON(noticeTargetGridData));
            ViewBag.NewsWriteR2Auth = Json(HttpContext.Session["NewsWriteR2Auth"]);

            return View();
        }



        [HttpPost]
        public JsonResult SelectNoticeTarget(string news_cd, string news_gb)
        {
            DataTable noticeTargetGridData = newsWriteR2Service.SelectNoticeTarget(news_cd, news_gb);

            return Json(Public_Function.DataTableToJSON(noticeTargetGridData));
        }



        [HttpPost]
        public JsonResult NewNoticeTarget()
        {
            DataTable noticeTargetGridData1 = newsWriteR2Service.SelectNoticeTarget("", "1");
            DataTable noticeTargetGridData2 = newsWriteR2Service.SelectNoticeTarget("", "2");
            DataTable noticeTargetGridData3 = newsWriteR2Service.SelectNoticeTarget("", "3");

            string[] strArr = new string[3];

            strArr[0] = Public_Function.DataTableToJSON(noticeTargetGridData1);
            strArr[1] = Public_Function.DataTableToJSON(noticeTargetGridData2);
            strArr[2] = Public_Function.DataTableToJSON(noticeTargetGridData3);

            return Json(strArr);
        }


        [HttpPost]
        public string GetNewsCd()
        {
            string newsCd = newsWriteR2Service.GetNewsCd();

            return newsCd;
        }

        [HttpPost]
        public string NewsCRUD(News news, string targetType, string gubun)
        {
            string res = newsWriteR2Service.NewsCRUD(news, targetType, gubun);

            return "";
        }

        [HttpPost]
        public JsonResult SelectNews(string news_gb, string news_start_date, string news_end_date, bool isNotifiedByMe)
        {
            string query_gb = "%";

            if (isNotifiedByMe)
            {
                query_gb = HttpContext.Session["loginCD"].ToString();
            }

            DataTable newsDt = newsWriteR2Service.SelectNews(query_gb, news_gb, news_start_date, news_end_date);

            return Json(Public_Function.DataTableToJSON(newsDt));
        }

        #endregion



        #region AlarmList 업무 스케줄 조회

        private AlarmListServie alarmListServie = new AlarmListServie();

        public ActionResult AlarmList() 
        {
            DataTable alarms = alarmListServie.selectAlarm("GN", HttpContext.Session["loginCD"].ToString());
            DataTable empData = alarmListServie.getEmpData();

            ViewBag.alarms = Json(Public_Function.DataTableToJSON(alarms));
            ViewBag.empData = Json(Public_Function.DataTableToJSON(empData));

            return View();
        }


        [HttpPost]
        public JsonResult SelectAlarm(string gubun, string emp_cd)
        {
            DataTable alarms = alarmListServie.selectAlarm(gubun, emp_cd);

            return Json(Public_Function.DataTableToJSON(alarms));
        }

        [HttpPost]
        public string AlarmListConfirm(string table_gb, string code, string code2, string code3, string code4, string code5)
        {
            string res = alarmListServie.AlarmListConfirm(table_gb, code, code2, code3, code4, code5);

            return res;
        }

        #endregion

        #region AlarmManage 일탈 알람 관리

        private AlarmManageService alarmManage = new AlarmManageService();


        public ActionResult AlarmManage()
        {
            string alm_code = "";
            DataTable alarmList = alarmManage.AlarmManage_Select(alm_code);

            string emp_code = "";
            DataTable empList = alarmManage.AlarmManage_Select_EMP(emp_code);

            ViewBag.alm_List = Json(Public_Function.DataTableToJSON(alarmList)); //알람 리스트
            ViewBag.emp_List = Json(Public_Function.DataTableToJSON(empList)); //사원 목록

            return View();
        }


        [HttpPost]
        public JsonResult AlarmManage_Detail(string alm_code)
        {
            DataTable alarmList = alarmManage.AlarmManage_Select_Detail(alm_code);


            return Json(Public_Function.DataTableToJSON(alarmList));
        }


        [HttpPost]
        public JsonResult AlarmManage_Search()
        {
            string alm_code = "";
            DataTable alarmList = alarmManage.AlarmManage_Select(alm_code);


            return Json(Public_Function.DataTableToJSON(alarmList));
        }


        [HttpPost]
        public JsonResult AlarmManageCRUD(AlarmManage iModel, string gubun)
        {

            if (iModel == null)
                return Json("");
            else
            {
                string res = alarmManage.AlarmManageCRUD(iModel, gubun);
                var resJson = "{ \"messege\": \"" + res + "\" }";

                return Json(resJson);
            }

        }


        [HttpPost]
        public JsonResult AlarmManage_EMP_Popup()
        {
            string emp_code = "";
            DataTable empList = alarmManage.AlarmManage_Select_EMP(emp_code);

            string jsonData = Public_Function.DataTableToJSON(empList).Replace("\r\n", "\\r\\n");

            return Json(jsonData);

        }


        [CheckSession]
        [HttpPost]
        public JsonResult AlarmManage_EMP_CRUD([JsonBinder] List<AlarmManage> iModel)
        {
            if (iModel == null)
                return Json("");
            else
            {
                string res = alarmManage.AlarmManage_EMP_CRUD(iModel);

                return Json(res);
            }

        }

        #endregion


    }
}
