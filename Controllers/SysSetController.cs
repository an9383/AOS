using HACCP.Attribute;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models.SysCom;
using HACCP.Models.SysSet;
using HACCP.Services.Comm;
using HACCP.Services.SysSet;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class SysSetController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SysSetController));

        private SelectBoxService selectBoxService = new SelectBoxService();
        private MenuManageService menuManageService = new MenuManageService();
        private ProgramManageService programManageService = new ProgramManageService();
        private ParameterService parameterService = new ParameterService();
        private ComplexParameterService complexParameterService = new ComplexParameterService();
        private MessageInfoService messageInfoService = new MessageInfoService();

        #region MenuManage 메뉴등록

        [CheckSession]
        public ActionResult MenuManage()
        {
            // 메인 그리드 불러오기
            List<MenuManage> mainData = menuManageService.Select(null);

            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(mainData);

            // 셀렉트 박스 옵션들 불러오기             
            DataTable moduleGubuns = selectBoxService.GetSelectBox("공통코드", "N", "CM015", "moduleGubuns");

            ViewBag.mainData = Json(JSONresult);
            ViewBag.moduleGubuns = moduleGubuns;

            return View();
        }
        [HttpPost]
        public JsonResult MenuManageSelect(string MenuManage_pModuleGubuns)
        {
            List<MenuManage> mainData = menuManageService.Select(MenuManage_pModuleGubuns);

            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(mainData);

            return Json(JSONresult);
        }
        [HttpPost]
        public ActionResult MenuManageCRUD(MenuManage pModel)
        {
            if (ModelState.IsValid)
            {
                menuManageService.CRUD(pModel);
                string gubun = pModel.MenuManage_pGubun;

                return Json(new { Ok = true });
            }
            else
            {
                return Json(new { Ok = false });
            }
        }
        [HttpPost]
        public bool MenuManageDelete(string module_gb, string module_cd)
        {
            bool isSucceed = menuManageService.Delete(module_gb, module_cd);

            return isSucceed;
        }

        #endregion



        #region ProgramManage 프로그램 등록

        [CheckSession]
        public ActionResult ProgramManage()
        {
            // 메인 그리드 불러오기
            DataTable programData = programManageService.Select("");
            // Model로 넘길 DataSet 생성
            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();

            // 셀렉트 박스 옵션들 불러오기             
            //DataTable moduleGubuns = selectBoxService.GetSelectBox("공통코드", "N", "CM015", "moduleGubuns");
            ViewBag.programData = Json(Public_Function.DataTableToJSON(programData));
            //ViewBag.moduleGubuns = moduleGubuns;

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ProgramManageSelect(string form_cd)
        {
            DataTable programData = programManageService.Select(form_cd);

            return Json(Public_Function.DataTableToJSON(programData));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ProgramCRUD(FormManage pModel, string gubun)
        {

            if (!gubun.Equals("D"))
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Ok = false });
                }
            }

            string res = programManageService.programManageCRUD(pModel, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        [HttpGet]
        public JsonResult ProgramManageSelectParameter(string form_cd)
        {
            DataTable programParameterData = programManageService.ParameterSelect(form_cd);

            return Json(Public_Function.DataTableToJSON(programParameterData), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProgramParamCRUD([JsonBinder] List<ProgramParameter> programParameters)
        {
            string res = programManageService.ProgramParamCRUD(programParameters);

            return Json(new { message = res });
        }

        #endregion



        #region MenuProgramManage 메뉴구성

        private MenuProgramManageService menuProgramManageService = new MenuProgramManageService();

        [CheckSession]
        public ActionResult MenuProgramManage()
        {
            DataTable menuProgramData = menuProgramManageService.Select("1");
            DataTable programData = menuProgramManageService.SelectProgramGridData();
            DataTable moduleGubuns = selectBoxService.GetSelectBox("공통코드", "N", "CM015", "moduleGubuns");

            ViewBag.menuProgramData = Json(Public_Function.DataTableToJSON(menuProgramData));
            ViewBag.programData = Json(Public_Function.DataTableToJSON(programData));
            ViewBag.moduleGubuns = moduleGubuns;

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult MenuProgramCRUD(MenuProgramManage menuProgram)
        {
            string res = menuProgramManageService.MenuProgramCRUD(menuProgram);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        [CheckSession]
        [HttpPost]
        public JsonResult MenuProgramSelect(MenuProgramManage menuProgram)
        {
            DataTable menuProgramData = menuProgramManageService.Select(menuProgram.module_gb);

            return Json(Public_Function.DataTableToJSON(menuProgramData)); ;
        }

        #endregion MenuProgram_Manage



        #region Common 공통코드 관리

        private CommonService commonService = new CommonService();

        [HttpGet]
        public ActionResult Common()
        {
            DataTable commonCodeData = commonService.Select("", "");
            DataTable commonCodePopupData = commonService.SelectCommonCodePopupData();

            //ViewBag.commonCodeData = Json(Public_Function.DataTableToJSON(commonCodeData).Replace("\t", string.Empty));
            ViewBag.commonCodeData = commonCodeData;
            ViewBag.commonCodePopupData = Json(Public_Function.DataTableToJSON(commonCodePopupData).Replace("\t", string.Empty));

            return View();
        }

        [HttpPost]
        public string CommonCodeSelect(string common_cd)
        {
            DataTable commonCodeData = commonService.Select(common_cd, "");

            //return Json(Public_Function.DataTableToJSON(commonCodeData).Replace("'", "\'").Replace("\t", string.Empty).Replace("\r\n", "\\r\\n"));
            return JsonConvert.SerializeObject(commonCodeData);
        }

        [HttpPost]
        public JsonResult CommonCodeCRUD(CommonCode commonCode, string gubun)
        {
            if (!gubun.Equals("D"))
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Ok = false });
                }
            }

            string res = commonService.CommonCodeCRUD(commonCode, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        #endregion Common



        #region MessageInfo
        /// <summary>
        /// 메세지 정보, 조회
        /// </summary>
        /// <param name="MessageInfo.SrchParam"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MessageInfo(MessageInfo.SrchParam param)
        {
            MessageInfoService messageInfoService = new MessageInfoService();
            DataTable messageInfo = messageInfoService.Select(param);

            ViewBag.MessageInfo = Json(Public_Function.DataTableToJSON(messageInfo));

            return View();
        }

        /// <summary>
        /// 메세지 정보, 조회
        /// </summary>
        /// <param name="MessageInfo.SrchParam"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult MessageInfoSelect(MessageInfo.SrchParam param)
        {
            MessageInfoService messageInfoService = new MessageInfoService();
            DataTable messageInfos = messageInfoService.Select(param);

            return Json(Public_Function.DataTableToJSON(messageInfos));
        }

        /// <summary>
        /// 메세지 정보, 저장
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <returns></returns>
        [Route("SysSet/MessageInfoSave")]
        [HttpPost]
        public ActionResult MessageInfoSave(MessageInfo messageInfo)
        {
            messageInfoService.Save(messageInfo);

            return Json(new { Ok = true });
        }

        /// <summary>
        /// 메세지 정보, 삭제
        /// </summary>
        /// <param name="sMsg_code"></param>
        /// <returns></returns>
        [Route("SysSet/MessageInfoDelete")]
        [HttpPost]
        public bool MessageInfoDelete(string sMsg_code)
        {
            //log.Info("메세지 코드:" + sMsg_code);

            bool isSucceed = messageInfoService.Delete(sMsg_code);
            return isSucceed;
        }
        #endregion MessageInfo

        #region Parameter
        /// <summary>
        /// 파라미터 관리, 조회
        /// </summary>
        /// <param name="Parameter.SrchParam"></param>
        /// <returns></returns>

        public ActionResult Parameter(Parameter.SrchParam param)
        {
            ParameterService parameterService = new ParameterService();
            DataTable parameter = parameterService.Select(param);

            ViewBag.Parameter = Json(Public_Function.DataTableToJSON(parameter));

            return View();
        }

        /// <summary>
        /// 파라미터 관리, 조회버튼 기능
        /// 
        /// </summary>
        /// <param name="Parameter.SrchParam"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ParameterSelect(Parameter.SrchParam param)
        {

            ParameterService parameterService = new ParameterService();
            DataTable parameters = parameterService.Select(param);

            return Json(Public_Function.DataTableToJSON(parameters));
        }

        /// <summary>
        /// 파라미터 관리, 저장
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [Route("SysSet/ParameterSave")]
        [HttpPost]
        public ActionResult ParameterSave(Parameter parameter)
        {
            parameterService.Save(parameter);

            return Json(new { Ok = true });
        }

        /// <summary>
        /// 파라미터 관리, 삭제
        /// </summary>
        /// <param name="pParameter_code"></param>
        /// <returns></returns>
        [Route("SysSet/ParameterDelete")]
        [HttpPost]
        public bool ParameterDelete(string pParameter_code)
        {
            //log.Info("파라미터 코드:" + pParameter_code);

            bool isSucceed = parameterService.Delete(pParameter_code);
            return isSucceed;
        }

        #endregion Parameter

        #region ComplexParameter
        /// <summary>
        /// 복합파라미터 관리, 조회
        /// </summary>
        /// <param name="ComplexParameter.SrchParam"></param>
        /// <returns></returns>
        public ActionResult ComplexParameter(ComplexParameter.SrchParam param)
        {
            ComplexParameterService complexParameterService = new ComplexParameterService();
            DataTable complexParameter = complexParameterService.Select(param);
            ViewBag.ComplexParameter = Json(Public_Function.DataTableToJSON(complexParameter));

            return View();
        }

        /// <summary>
        /// 복합파라미터 관리, 조회
        /// </summary>
        /// <param name="ComplexParameter.SrchParam "></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ComplexParameterSelect(ComplexParameter.SrchParam param)
        {
            ComplexParameterService complexParameterService = new ComplexParameterService();
            DataTable complexParameters = complexParameterService.Select(param);

            return Json(Public_Function.DataTableToJSON(complexParameters));
        }

        /// <summary>
        /// 복합파라미터 관리, 저장
        /// </summary>
        /// <param name="complexParameter"></param>
        /// <returns></returns>
        [Route("SysSet/ComplexParameterSave")]
        [HttpPost]
        public ActionResult ComplexParameterSave(ComplexParameter complexParameter)
        {
            complexParameterService.Save(complexParameter);

            return Json(new { Ok = true });
        }

        /// <summary>
        /// 복합파라미터 관리, 삭제
        /// </summary>
        /// <param name="pComplex_cd"></param>
        /// <param name="pComplex_id"></param>
        /// <returns></returns>
        [Route("SysSet/ComplexParameterDelete")]
        [HttpPost]
        public bool ComplexParameterDelete(string pComplex_cd, string pComplex_id)
        {
            //log.Info("복합파라미터 cd:" + pComplex_cd);
            //log.Info("복합파라미터 id:" + pComplex_id);

            bool isSucceed = complexParameterService.Delete(pComplex_cd, pComplex_id);
            return isSucceed;
        }

        #endregion ComplexParameter


        #region 테이블명세서 관리

        /// <summary>
        /// 테이블명세서 관리, 조회
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult TableLayout()
        {
            TableLayoutService tableLayoutService = new TableLayoutService();
            DataTable TableLayout = tableLayoutService.SelectTableList();

            ViewBag.TableLayout = Json(Public_Function.DataTableToJSON(TableLayout));

            return View();
        }

        /// <summary>
        /// 테이블명세서 조회
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TableLayoutSelect(TableLayout.SrchParam param)
        {
            TableLayoutService tableLayoutService = new TableLayoutService();
            DataTable tableLayout = tableLayoutService.SelectTableLayout(param);

            return Json(Public_Function.DataTableToJSON(tableLayout));
        }

        /// <summary>
        /// AuditTarget 조회
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SelectAuditTarget(TableLayout.SrchParam param)
        {
            TableLayoutService tableLayoutService = new TableLayoutService();
            DataTable auditTarget = tableLayoutService.SelectAuditTarget(param);

            return Json(Public_Function.DataTableToJSON(auditTarget));
        }


        /// <summary>
        /// TableLayout 수정
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public string TableLayoutUpdate([JsonBinder] List<TableLayout> param)
        {
            TableLayoutService tableLayoutService = new TableLayoutService();

            string res = tableLayoutService.TableLayoutUpdate(param);

            return res;
        }

        #endregion

    }
}