using HACCP.Libs;
using HACCP.Models.Equipment;
using HACCP.Services.Comm;
using HACCP.Services.Equipment;
using log4net;
using System.Data;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class EquipmentController : Controller
    {
        #region 공통 서비스 설정
        private static readonly ILog log = LogManager.GetLogger(typeof(EquipmentController));
        private SelectBoxService selectBoxService = new SelectBoxService();
        private CodeHelpService codeHelpService = new CodeHelpService();
        #endregion

        #region 설비 고장 신고(Notify)
        private NotifyService notifyService = new NotifyService();
                
        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult Notify(Notify.SrchParam srch)
        {
            // 1. 공통코드 List
            DataTable le_repair_status = selectBoxService.GetSelectBox("공통코드", "S", "CM047", "le_repair_status");
            DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");
            DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");
                      
            // 2. 그리드 List            
            DataTable mainGrid = notifyService.SelectMain(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식

            // 3.3. 코드성데이터            
            ViewBag.le_repair_status = le_repair_status;
            ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));


            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 검색 : 로드후
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult NotifySelect(Notify.SrchParam srch)
        {
            DataTable mainGrid = notifyService.SelectMain(srch);            

            return Json(Public_Function.DataTableToJSON(mainGrid));
        }

        /// <summary>
        /// 채번 : afterservice_no 리턴
        /// </summary>
        /// <param name="edate"></param>
        /// <returns></returns>
        [HttpPost]
        public string NotifyGetSeqNo(string edate)
        {
            return notifyService.GetSeqNo(edate);
        }

        /// <summary>
        /// 트랜잭션(=CRUD) 처리
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>
        /// <returns></returns>
        [HttpPost]
        public string NotifyTRX(Notify dto, Notify.SrchParam srch)
        {
            string res = notifyService.DataTrx(dto, srch);
            return res;
        }

        #endregion

        #region 설비 고장수리 관리(TroubleConduct)
        private TroubleConductService troubleService = new TroubleConductService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult TroubleConduct(TroubleConduct.SrchParam srch)
        {
            // 1. 공통코드 List
            DataTable le_repair_status = selectBoxService.GetSelectBox("공통코드", "S", "CM047", "le_repair_status");   //검색
            DataTable le_repair_status2 = selectBoxService.GetSelectBox("공통코드", "N", "CM044", "le_repair_status2"); //우측폼
            DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");
            DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");

            // 2. 그리드 List            
            DataTable mainGrid = troubleService.SelectMain(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식

            // 3.3. 코드성데이터            
            ViewBag.le_repair_status = le_repair_status;
            ViewBag.le_repair_status2 = le_repair_status2;
            ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 검색 : 로드후
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TroubleConductSelect(TroubleConduct.SrchParam srch)
        {
            DataTable mainGrid = troubleService.SelectMain(srch);

            return Json(Public_Function.DataTableToJSON(mainGrid));
        }

        /// <summary>
        /// Sub그리드조회
        /// </summary>
        /// <param name="afterservice_no"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TroubleConductSelectSub(string afterservice_no)
        {
            DataTable subGrid = troubleService.SelectSub(afterservice_no);
            return Json(Public_Function.DataTableToJSON(subGrid));
        }

        /// <summary>
        /// 트랜잭션(=CRUD) 처리
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>        
        /// <returns></returns>
        [HttpPost]
        //public string TroubleConductTRX(TroubleConduct dto, TroubleConduct.SrchParam srch, [JsonBinder] List<TroubleConductUsedPart> dtos)
        public string TroubleConductTRX(TroubleConduct dto, TroubleConduct.SrchParam srch)
        {            
            string res = troubleService.DataTrx(dto, srch);
            return res;
        }

        #endregion

        #region 부품 코드 등록(PartManage)
        private PartManageService partManageService = new PartManageService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult PartManage(PartManage.SrchParam srch)
        {
            // 1. 공통코드 List
            DataTable s_part_kind = selectBoxService.GetSelectBox("부품종류", "S", "", "s_part_kind");   //검색
            DataTable part_kind = selectBoxService.GetSelectBox("부품종류", "D", "", "part_kind");      //우측폼
            DataTable partPopupData = codeHelpService.GetCode(CodeHelpType.part, "", "");
            DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");

            // 2. 그리드 List            
            DataTable mainGrid = partManageService.SelectMain(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식

            // 3.3. 코드성데이터            
            ViewBag.s_part_kind = s_part_kind;
            ViewBag.part_kind = part_kind;
            ViewBag.partPopupData = Json(Public_Function.DataTableToJSON(partPopupData));
            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 검색 : 로드후
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PartManageSelect(PartManage.SrchParam srch)
        {
            DataTable mainGrid = partManageService.SelectMain(srch);

            return Json(Public_Function.DataTableToJSON(mainGrid));
        }

        /// <summary>
        /// Sub그리드조회
        /// </summary>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PartManageSelectSub(string rowKey)
        {
            DataTable subGrid = partManageService.SelectSub(rowKey);
            return Json(Public_Function.DataTableToJSON(subGrid));
        }

        /// <summary>
        /// 트랜잭션(=CRUD) 처리
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>
        /// <returns></returns>
        [HttpPost]
        public string PartManageTRX(PartManage dto, PartManage.SrchParam srch)
        {
            string res = partManageService.DataTrx(dto, srch);
            return res;
        }

        #endregion

        #region 설비 부품 등록(PartRegister)
        private PartRegisterService partRegisterService = new PartRegisterService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult PartRegister(PartRegister.SrchParam srch)
        {
            // 1. 공통코드 List
            //DataTable s_part_kind = selectBoxService.GetSelectBox("부품종류", "S", "", "s_part_kind");   //검색
            //DataTable part_kind = selectBoxService.GetSelectBox("부품종류", "D", "", "part_kind");      //우측폼
            //DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");            // 사원
            DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");           // 설비 
            DataTable partPopupData = codeHelpService.GetCode(CodeHelpType.part, "", "");                 //부품코드
                       

            // 2. 그리드 List            
            DataTable mainGrid = partRegisterService.SelectMain(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식

            // 3.3. 코드성데이터            
            //ViewBag.s_part_kind = s_part_kind;
            //ViewBag.part_kind = part_kind;
            //ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.partPopupData = Json(Public_Function.DataTableToJSON(partPopupData));
            ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            
            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 검색 : 로드후
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PartRegisterSelect(PartRegister.SrchParam srch)
        {
            DataTable mainGrid = partRegisterService.SelectMain(srch);

            return Json(Public_Function.DataTableToJSON(mainGrid));
        }

        /// <summary>
        /// Sub그리드조회
        /// </summary>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PartRegisterSelectSub(string rowKey)
        {
            DataTable subGrid = partRegisterService.SelectSub(rowKey);
            return Json(Public_Function.DataTableToJSON(subGrid));
        }

        /// <summary>
        /// 트랜잭션(=CRUD) 처리
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>
        /// <returns></returns>
        [HttpPost]
        public string PartRegisterTRX(string equip_cd, PartRegisterEqupPart dtos)
        {
            string res = partRegisterService.DataTrx(equip_cd, dtos);
            return res;
        }

        #endregion

        #region 부품 입출고 관리(PartsInOutRegi)
        private PartsInOutRegiService inOutService = new PartsInOutRegiService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult PartsInOutRegi(PartsInOutRegi.SrchParam srch)
        {
            // 1. 공통코드 List
            DataTable le_part_history_status = selectBoxService.GetSelectBox("공통코드", "D", "CM045", "le_part_history_status");
            DataTable le_part_gb = selectBoxService.GetSelectBox("공통코드", "D", "CM046", "le_part_gb");
            DataTable le_inOut = selectBoxService.GetSelectBox("공통코드", "S", "CM046", "le_InOut");

            DataTable empPopupData = codeHelpService.GetCode(CodeHelpType.employee, "", "");            
            DataTable partPopupData = codeHelpService.GetCode(CodeHelpType.part, "", "");       //부품코드
            DataTable custPopupData = codeHelpService.GetCode(CodeHelpType.cust, "", "");       //거래처/제조처
            
            // 2. 그리드 List            
            DataTable mainGrid = inOutService.SelectMain(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식

            // 3.3. 코드성데이터            
            ViewBag.le_part_history_status = le_part_history_status;
            ViewBag.le_part_gb             = le_part_gb;
            ViewBag.le_InOut               = le_inOut;

            ViewBag.empPopupData = Json(Public_Function.DataTableToJSON(empPopupData));
            ViewBag.partPopupData = Json(Public_Function.DataTableToJSON(partPopupData));
            ViewBag.custPopupData = Json(Public_Function.DataTableToJSON(custPopupData));

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 검색 : 로드후
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PartsInOutRegiSelect(PartsInOutRegi.SrchParam srch)
        {
            DataTable mainGrid = inOutService.SelectMain(srch);

            return Json(Public_Function.DataTableToJSON(mainGrid));
        }

        /// <summary>
        /// 트랜잭션(=CRUD) 처리
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>
        /// <returns></returns>
        [HttpPost]
        public string PartsInOutRegiTRX(PartsInOutRegi dto, PartsInOutRegi.SrchParam srch)
        {
            string res = inOutService.DataTrx(dto, srch);
            return res;
        }

        #endregion

        #region 설비점검수리 로그북(PartHistory)
        private PartHistoryService historyService = new PartHistoryService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult PartHistory(PartHistory.SrchParam srch)
        {
            // 1. 공통코드 List
            DataTable S_equip_type = selectBoxService.GetSelectBox("공통코드", "S", "SC011", "S_equip_type"); // 설비종류
                        
            DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");       //부품코드            

            // 2. 그리드 List            
            DataTable mainGrid = historyService.SelectMain(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식

            // 3.3. 코드성데이터            
            ViewBag.S_equip_type = S_equip_type;

            ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));

            return View();
        }
        /// <summary>
        /// 검색 : 로드후
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PartHistorySelect(PartHistory.SrchParam srch)
        {
            DataTable mainGrid = historyService.SelectMain(srch);

            return Json(Public_Function.DataTableToJSON(mainGrid));
        }

        /// <summary>
        /// Sub그리드조회(SS) : 예방점검
        /// </summary>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PartHistorySelectSubSS(string rowKey)
        {
            DataTable subGrid = historyService.SelectSubSS(rowKey);
            return Json(Public_Function.DataTableToJSON(subGrid));
        }

        /// <summary>
        /// Sub그리드조회(SP) : 고장수리
        /// </summary>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PartHistorySelectSubSP(string rowKey)
        {
            DataTable subGrid = historyService.SelectSubSP(rowKey);
            return Json(Public_Function.DataTableToJSON(subGrid));
        }

        #endregion
    }
}
