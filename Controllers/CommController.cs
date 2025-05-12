using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models;
using HACCP.Models.Common;
using HACCP.Services.Comm;
using HACCP.Services.SysSec;
using log4net;
using Newtonsoft.Json;

namespace HACCP.Controllers
{
    public class CommController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CommController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private AccessLogService accessLogService = new AccessLogService();

        public ActionResult LoginRedirect()
        {
            return View("LoginRedirect");
        }
        /// <summary>
        /// 로그인 폼 띄움
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            LoginService loginService = new LoginService();
            DataTable loginData = loginService.loginPageLoading();


            //ViewBag.loginData = Json(Public_Function.DataTableToJSON(loginData));
            ViewBag.loginData = Json(Public_Function.DataTableToJSON(loginData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            return View();
        }

        /// <summary>
        /// 폼에서 보낸 ID,PW 를 이용해 로그인 가능 여부 확인 후 처리
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(User model)
        {
            LoginService loginService = new LoginService();

            string result = "No";

            if (ModelState.IsValid)
            {
                HttpContext.Session["plantCD"] = Public_Function.selectedPLANT;
                result = loginService.LogIn(model);
            }

            if (result.Equals("OK"))
            {
                // 로그인 성공

                HttpContext.Session["loginCD"] = Public_Function.User_cd;
                HttpContext.Session["loginID"] = Public_Function.User_id;
                HttpContext.Session["loginPosition"] = Public_Function.position;
                HttpContext.Session["loginNM"] = Public_Function.User_nm;
                HttpContext.Session["deptCD"] = Public_Function.Dept_cd;
                HttpContext.Session["deptNM"] = Public_Function.Dept_nm;

                Public_Function.User_id = model.txt_ID;

                loginService.SystemLogIn();

                /* Public_Function.selectedPLANTName 셋팅 */                
                DataTable selectedPLANTName = loginService.selectedPLANTName();
                if (selectedPLANTName.Rows.Count > 0 && selectedPLANTName.Rows[0].ItemArray.Length > 0)
                {
                    Public_Function.selectedPLANTName = selectedPLANTName.Rows[0].ItemArray[0].ToString();
                    HttpContext.Session["plantNM"] = Public_Function.selectedPLANTName;
                }

                /********************************************************
                // 로그인 사용자의 전체 프로그램 권한리스트 설정
                // 2020-10-29, 조형진
                *******************************************************/
                MenuService menuService = new MenuService();
                DataTable authList = menuService.GetToolbarAuthority();
                HttpContext.Session["loginMenuAuthList"] = authList;
                
                // 리다이렉트
                return Json(new { OK = true, loginID = HttpContext.Session["loginID"].ToString(), 
                                             loginCD = HttpContext.Session["loginCD"].ToString(),
                                             loginNM = HttpContext.Session["loginNM"].ToString(),
                                             loginPosition = HttpContext.Session["loginPosition"].ToString(),
                                             plantCD = HttpContext.Session["plantCD"].ToString(),
                                             plantNM = HttpContext.Session["plantNM"].ToString(),                                            
                                             deptCD = HttpContext.Session["deptCD"].ToString(),
                                             deptNM = HttpContext.Session["deptNM"].ToString(),
                                             endTime = HttpContext.Session["endTime"].ToString()
                });
            }

            // 로그인 실패
            return Json(new
            {
                OK = false,
                errorType = result
            });
        }

        /// <summary>
        /// 메인 페이지(세션없을시 로그인 페이지로)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            if (HttpContext.Session["loginID"] == null || HttpContext.Session["loginID"].Equals(""))
            {
                return RedirectToAction("Login", "Comm");
            }

            return View();
        }

        /// <summary>
        /// 메인 화면에 보여줄 공지사항 조회 
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        //[CheckSession]
        //[HttpGet]
        //public object GetNews(DataSourceLoadOptions loadOptions)
        //{
        //    MenuService menuService = new MenuService();
        //    return DataSourceLoader.Load(menuService.GetNews((string)HttpContext.Session["loginCD"]), loadOptions);
        //}

        /// <summary>
        /// 트리 메뉴에 표시할 메뉴 리스틑 조회
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        //[CheckSession]
        //[HttpGet]
        //public object GetMenu(DataSourceLoadOptions loadOptions)
        //{
        //    MenuService menuService = new MenuService();

        //    List<Menu> temp = menuService.GetMenu((string)HttpContext.Session["loginCD"]);

        //    return DataSourceLoader.Load(temp, loadOptions);
        //}


        /// <summary>
        /// 트리 메뉴클릭시 메뉴별 조작 권한 세션에 추가하고 페이지 이동
        /// cshtml 에서 razor 구문으로 변수에 담고 CRUD 버튼 노출 제어 필요
        /// 
        /// ex)
        /// 
        /// @{
        ///     var 권한이름 = @Html.Raw(ViewBag.권한이름.Value);
        /// }
        /// 
        /// <script>
        ///     var _권한이름;
        ///     $(function () {
        ///         _권한이름 = JSON.parse('@권한이름')[0];
        ///     	if (_권한이름.form_edit != "Y") {
        ///     	    $("CRUD버튼ID").remove();
        ///     	}
        ///     });
        /// </script>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cd"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public ActionResult SetMenu(string cd, string url)
        {
            MenuService menuService = new MenuService();

            DataTable dt = menuService.GetMenuAuthority(cd);

            var auth = Public_Function.DataTableToJSON(dt);
                
            HttpContext.Session[cd + "Auth"] = auth;
            

            string programAuditID = accessLogService.ProgramLogIn(cd);

            if(!(programAuditID.Equals("") || programAuditID.Equals("Fail") || programAuditID == null))
            {
                HttpContext.Session[cd + "AuditID"] = programAuditID;
            }
            return Redirect(url);
        }

        /// <summary>
        /// 로그아웃하고 세션 제거 / 리턴 받은 ajax 에서 페이지 이동처리 필요 
        /// 
        /// ex) if (JSON.parse(result).sessionLoss) {
        ///     sessionStorage.clear(); - 브라우저 SessionStorage 제거
        ///     location.replace("/Comm/Login");
        /// }
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult LogOut()
        {
            LoginService loginService = new LoginService();

            loginService.SystemLogOut();

            HttpContext.Session.Clear();

            var res = "{ \"sessionLoss\": true }";

            return Json(res);
        }

        [HttpPost]
        public ActionResult UpdateSession()
        {
            HttpContext.Session["Time"] = DateTime.Now.ToString();
            return Json(new { data = HttpContext.Session["Time"] });
        }

        /// <summary>
        /// dev 위젯 로컬라이제이션
        /// </summary>
        /// <returns></returns>
        public ActionResult CldrData()
        {
            return new CldrDataScriptBuilder()
                .SetCldrPath("~/Content/cldr-data")
                .SetInitialLocale("ko")
                .UseLocales("ko")
                .Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <param name="pGubun"></param>
        /// <param name="pDiv"></param>
        /// <param name="pStrWhere"></param>
        /// <param name="pTableName"></param>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public object GetCommon(DataSourceLoadOptions loadOptions, string pGubun , string pDiv, string pStrWhere, string pTableName)
        {
            DataTable dt;

            dt = selectBoxService.GetSelectBox(pGubun, pDiv, pStrWhere, pTableName);

            List<SelectBoxGubun> list = new List<SelectBoxGubun>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];

                SelectBoxGubun temp;

                temp = new SelectBoxGubun(row, false);

                list.Add(temp);
            }

            return DataSourceLoader.Load(list, loadOptions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pGb"></param>
        /// <param name="pDiv"></param>
        /// <param name="pItemCd"></param>
        /// <param name="pStrWhere2"></param>
        /// <param name="pTableName"></param>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public ActionResult GetSelectBoxOption(string pGb, string pDiv, string pItemCd , string pStrWhere2, string pTableName)
        {
            DataTable selectOptions = new DataTable();

            if (pStrWhere2 == null)
            {
                selectOptions = selectBoxService.GetSelectBox(pGb, pDiv, pItemCd, pTableName);
            }else
            {
                selectOptions = selectBoxService.GetSelectBox(pGb, pDiv, pItemCd, pStrWhere2, pTableName);
            }

            ViewBag.selectOptions = selectOptions;
            
            return View();
        }

        /// <summary>
        /// ID 중복체크
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult IDValidation(User model)
        {
            LoginService loginService = new LoginService();

            DataTable dt = loginService.IDValidation(model);

            string jsonData = Public_Function.DataTableToJSON(dt);

            return Json(jsonData);
        }

        /// <summary>
        /// 서명권한 체크
        /// </summary>
        /// <param name="emp_cd"></param>
        /// <param name="sign_set_cd"></param>
        /// <param name="sign_set_seq"></param>
        /// <returns></returns>
        public string AuthorityCheck(string emp_cd, string sign_set_cd, string sign_set_seq)
        {
            LoginService loginService = new LoginService();

            string res = loginService.AuthorityCheck(emp_cd, sign_set_cd, sign_set_seq);

            return res;
        }

        /// <summary>
        /// 대리자 체크
        /// </summary>
        /// <param name="emp_cd"></param>
        /// <param name="sign_set_cd"></param>
        /// <param name="sign_set_seq"></param>
        /// <returns></returns>
        public string DelegateCheck(string emp_cd, string sign_set_cd, string sign_set_seq)
        {
            LoginService loginService = new LoginService();

            string res = loginService.DelegateCheck(emp_cd, sign_set_cd, sign_set_seq);

            return res;
        }

        public ActionResult Fail()
        {
            return View();
        }

        /// <summary>
        /// 메뉴 조작 권한 세션 삭제
        /// </summary>
        /// <param name="programCD"></param>
        /// <returns></returns>
        [HttpPost]
        public bool RemoveProgramSession(string programCD)
        {
            string programAuditID = (string)HttpContext.Session[programCD + "AuditID"];

            bool isRecorded = accessLogService.ProgramLogOut(programAuditID);

            HttpContext.Session.Remove(programCD+"Auth");

            var auth = HttpContext.Session[programCD + "Auth"];

            return (auth == null ? true : false);
        }


        //[CheckSession]
        //[HttpPost]
        //public ActionResult UploadImage()
        //{

        //    try
        //    {
        //        var myBytes = GetByteArrayFromImage(Request.Form.Files["equipment-image"]);

        //        string fileName = Request.Form.Files["equipment-image"].FileName;

        //        byte[] GetByteArrayFromImage(IFormFile file)
        //        {
        //            using (var target = new MemoryStream())
        //            {
        //                file.CopyToAsync(target);
        //                return target.ToArray();
        //            }
        //        }


        //        fileUploadService.uploadImage(myBytes, fileName, "QWE", "ASD", DateTime.Now);

        //    }
        //    catch
        //    {
        //        Response.StatusCode = 400;
        //    }

        //    return new EmptyResult();
        //}

        [HttpGet]
        public JsonResult CodeValidCheck(string codeCaption, string tmpData, CodeHelpType codehelptype)
        {
            bool check = false;

            if (tmpData != "")
            {
                CodeHelpService codeHelpService = new CodeHelpService();
                
                //codehelp 종류, 사업장코드, default 코드 or 명, 반환값
                check = codeHelpService.CheckValidity(codehelptype, "", tmpData);

                if (check == false)
                {

                }
            }
            else
                check = true;

            return Json("{ \"check\": \"" + check + "\" }", JsonRequestBehavior.AllowGet);
        }


        #region 공지사항 컨텐츠

        MenuService menuService = new MenuService();

        /// <summary>
        /// 공지사항, Top10 메뉴, 월별 생산 추이, 제조/포장 지시 목록
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMain()
        {
            if (HttpContext.Session["loginID"] != null || !HttpContext.Session["loginID"].Equals("")) {

                DataSet ds = menuService.GetMain(HttpContext.Session["loginCD"].ToString());
                string result = JsonConvert.SerializeObject(ds);
                return Json(result);

            } else {
                return null;
            }
           
        }

        /// <summary>
        /// To do list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMain_LIMS()
        {

            if (HttpContext.Session["loginID"] != null || !HttpContext.Session["loginID"].Equals(""))
            {

                DataTable dt = menuService.GetMain_LIMS(HttpContext.Session["loginCD"].ToString());
                string result = JsonConvert.SerializeObject(dt);
                return Json(result);

            } else {
                return null;
            }

        }

        #endregion


        #region 마이페이지 관련
        private MyPageService myPageService = new MyPageService();

        [HttpGet]
        public ActionResult MyPage()
        {
            DataTable myInfoData = myPageService.MyInfoSelect();

            ViewBag.myInfoData = Json(Public_Function.DataTableToJSON(myInfoData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));

            return View();
        }


        [HttpPost]
        public ActionResult PasswordEdit(string newPassword, string oldPassword)
        {
            string message = "";
            message = myPageService.PWDReuse(newPassword, oldPassword);

            if (!message.Equals("Y"))
            {
                message = "예전에 사용했던 동일 패스워드를 사용할 수 없습니다. \n패스워드를 다시 입력해 주십시오.";
            }

            Public_Function.PrePWD = oldPassword;
            Public_Function.MinimumLengthToPWD = 5;
            Public_Function.PreCommonCharPassword = 20;

            if (Public_Function.AllowanceCheck("PWD", newPassword, HttpContext.Session["loginID"].ToString(), ref message))
            {
                message = myPageService.AlterPassword(newPassword, oldPassword);
            }
            Public_Function.PrePWD = null;
            Public_Function.MinimumLengthToPWD = 0;
            Public_Function.PreCommonCharPassword = 0;


            var resJson = "{ \"message\": \"" + message + "\" }";
            return Json(resJson); ;
        }




        #endregion 마이페이지 끝


        [HttpPost]
        public JsonResult plantLogoSearch()
        {
            LoginService loginService = new LoginService();
            DataTable loginData = loginService.loginPageLoading();

            JsonResult jsonResultData = Json(Public_Function.DataTableToJSON(loginData).Replace("'", @"\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            jsonResultData.MaxJsonLength = int.MaxValue;

            return jsonResultData;
        }

        [HttpGet]
        public string GetCodeHelp(CodeHelpType codeHelpType)
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            string[] itemCodePopupDataSet = codeHelpService.GetCodeSet(codeHelpType, "", "");

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.MaxJsonLength = int.MaxValue;
            string jsonobj = jsSerializer.Serialize(itemCodePopupDataSet);

            return jsonobj;
        }


    }
}