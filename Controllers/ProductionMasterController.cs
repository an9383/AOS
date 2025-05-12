using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HACCP.Libs;
using HACCP.Models.ProductionMaster;
using HACCP.Services.Comm;
using HACCP.Services.ProductionMaster;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
//using DevExpress.XtraBars;
using DevExpress.Web.Internal;
using DevExpress.XtraRichEdit.API.Native;
using System.Text;
using System.IO;
using HACCP.Filter;
using System.Web.Mvc;
using HACCP.Attribute;
using log4net;

namespace HACCP.Controllers
{
    public class ProductionMasterController : Controller
    {
        #region 공통 서비스 설정
        private ItemGuideService ItemGuideService = new ItemGuideService();
        private SelectBoxService selectBoxService = new SelectBoxService();
        private MaterialBomListService materialBomListService = new MaterialBomListService();
        private static readonly ILog log = LogManager.GetLogger(typeof(EquipmentController));
        private CodeHelpService codeHelpService = new CodeHelpService();
        #endregion


        #region 공정검사 마스터(ProcessExam)
        private ProcessExamService processexamService = new ProcessExamService();

        /// <summary>
        /// 최초, 화면 load
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        public ActionResult ProcessExam(ProcessExam.SrchParam srch)
        {

            // 1. 공통코드 List
            // 공정코드 > CCP코드 변견으로 인한 주석처리
            //DataTable processPopupData = codeHelpService.GetCode(CodeHelpType.process, "", "");  //공정코드
            DataTable processPopupData = processexamService.getCcpCdPopupData();  //ccp코드
            DataTable processexamPopupData = codeHelpService.GetCode(CodeHelpType.commoncode, Public_Function.selectedPLANT, "PR001", "");  //검사항목
            //DataTable le_process_exam_start = selectBoxService.GetSelectBox("공통코드", "D", "PR002", "le_process_exam_start");   //검사시기
            //DataTable le_unit = selectBoxService.GetSelectBox("공통코드", "D", "PR003", "le_unit");   //결과유형
            DataTable equipPopupData = codeHelpService.GetCode(CodeHelpType.equipment, "", "");  //검사기기
            DataTable le_grand_item = selectBoxService.GetSelectBox("공통코드", "D", "PR004", "le_grand_item");   //대분류
            DataTable le_middle_item = selectBoxService.GetSelectBox("공통코드", "D", "PR005", "le_middle_item");   //중분류

            // 2. 그리드 List
            DataTable mainGrid = processexamService.SelectMain(srch);

            // 3. View구성   
            // 3.1. 검색 객체
            ViewData["srch"] = srch;

            // 3.2. 좌측Grid
            ViewBag.mainGrid = Json(Public_Function.DataTableToJSON(mainGrid));   // 기존 json라이브러리 방식

            // 3.3. 코드성데이터
            ViewBag.processPopupData = Json(Public_Function.DataTableToJSON(processPopupData));
            ViewBag.processexamPopupData = Json(Public_Function.DataTableToJSON(processexamPopupData));
            //ViewBag.le_process_exam_start = le_process_exam_start;
            //ViewBag.le_unit = le_unit;
            ViewBag.equipPopupData = Json(Public_Function.DataTableToJSON(equipPopupData));
            ViewBag.le_grand_item = le_grand_item;
            ViewBag.le_middle_item = le_middle_item;

            //ViewBag.mainList = Json(mainGrid, JsonRequestBehavior.AllowGet);        // raw json 방식
            return View();
        }
        /// <summary>
        /// 검색 : 로드후
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ProcessExamSelect(ProcessExam.SrchParam srch)
        {
            DataTable mainGrid = processexamService.SelectMain(srch);

            return Json(Public_Function.DataTableToJSON(mainGrid));
        }

        /// <summary>
        /// 트랜잭션(=CRUD) 처리
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>
        /// <returns></returns>
        [HttpPost]
        public string ProcessExamTRX(ProcessExam dto, ProcessExam.SrchParam srch)
        {
            string res = processexamService.DataTrx(dto, srch);
            
            return res;
        }

        [HttpPost]
        public string ProcessExamMasterTRX(string gubun, ProcessExamMaster srch)
        {
            gubun = "M" + gubun;

            string res = processexamService.ProcessExamMasterTRX(gubun, srch);
            return res;
        }

        #endregion


        #region 제제표준서 마스터 관리 컨트롤러
        public ActionResult ItemGuide()
        {
            List<Item> items = ItemGuideService.SelectItem("","");

            DataTable unitGubuns = selectBoxService.GetSelectBox("공통코드", "D","CM012", "unitGubuns");
            DataTable itemBomCalcType = selectBoxService.GetSelectBox("공통코드", "D", "WR004", "itemBomCalcType");
            DataTable itemBomLastType = selectBoxService.GetSelectBox("공통코드", "D", "CM003", "itemBomLastType");
            DataTable itemBomRounding = selectBoxService.GetSelectBox("소수점자리수", "N","CM083", "itemBomRounding");
            DataTable trade = selectBoxService.GetSelectBox("공통코드", "D","CM064", "trade");
            DataTable materials = ItemGuideService.GetMaterials("CODEHELP");
            DataTable workrooms1 = ItemGuideService.GetWorkRooms("v_workroom2");
            DataTable workrooms2 = ItemGuideService.GetWorkRooms("v_workroom_hardware_yn");
            DataTable equip = ItemGuideService.GetEquip("CODEHELP");
            DataTable ccp = ItemGuideService.GetCcp("CODEHELP");
            List<DataTable> ItemProcess = ItemGuideService.selectItemProcess("SP_StandardItemGuide", "S10", "");

            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(items);

            ViewBag.items = Json(JSONresult);

            ViewBag.unitGubuns = unitGubuns;
            ViewBag.itemBomCalcType = Json(Public_Function.DataTableToJSON(itemBomCalcType));
            ViewBag.itemBomLastType = Json(Public_Function.DataTableToJSON(itemBomLastType));
            ViewBag.itemBomRounding = Json(Public_Function.DataTableToJSON(itemBomRounding));
            ViewBag.trade = Json(Public_Function.DataTableToJSON(trade));
            ViewBag.materials = Json(Public_Function.DataTableToJSON(materials));
            ViewBag.workrooms1 = Json(Public_Function.DataTableToJSON(workrooms1));
            ViewBag.workrooms2 = Json(Public_Function.DataTableToJSON(workrooms2));
            ViewBag.equip = Json(Public_Function.DataTableToJSON(equip));
            ViewBag.ccp = Json(Public_Function.DataTableToJSON(ccp));
            ViewBag.ItemProcess = Json(Public_Function.DataTableToJSON(ItemProcess[0]));
            ViewBag.ItemProcessDetail = Json(Public_Function.DataTableToJSON(ItemProcess[1]));

            ViewBag.itemUnit = Json(Public_Function.DataTableToJSON(unitGubuns));

            return View();
        }

        [HttpPost]
        public JsonResult GetItemInfo(string pSpName, string pGubun, string pItemCd, string pBatchSize, string pRevisionNo)
        {
            if (pBatchSize.Equals("NEW"))
            {
                pBatchSize = "";
            }

            if (pRevisionNo.Equals("NEW"))
            {
                pRevisionNo = "";
            }

            DataTable ItemInfo = ItemGuideService.selectItemDetail(pSpName, pGubun, pItemCd, pBatchSize, pRevisionNo);
            DataTable SignInfo = ItemGuideService.selectSignInfo(pSpName, "EL", "2003", pItemCd, "M", pRevisionNo);
            DataTable ProcessInfo = ItemGuideService.selectProcessInfo(pSpName, "S2", pItemCd, pRevisionNo, pBatchSize);
            DataTable BatchSizeInfo = ItemGuideService.selectBatchSize("SP_Item_Workguide", "SS", pItemCd);
            DataTable PrescriptionInfo = ItemGuideService.selectprescriptionInfo(pSpName, "S6", pItemCd, pBatchSize, pRevisionNo);

            string jsonData1 = Public_Function.DataTableToJSON(ItemInfo).Replace("\r\n", "\\r\\n");
            string jsonData2 = Public_Function.DataTableToJSON(SignInfo);
            string jsonData3 = Public_Function.DataTableToJSON(ProcessInfo).Replace("N", "");
            string jsonData4 = Public_Function.DataTableToJSON(BatchSizeInfo);
            string jsonData5 = Public_Function.DataTableToJSON(PrescriptionInfo);

            jsonData1 = jsonData1.Replace("[", "");
            jsonData1 = jsonData1.Replace("]", "");

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData1);
            jsonList.Add(jsonData2);
            jsonList.Add(jsonData3);
            jsonList.Add(jsonData4);
            jsonList.Add(jsonData5);

            return Json(jsonList.ToArray());
        }

        [HttpPost]
        public JsonResult GetProcessInfo(Process process)
        {
            DataTable PrescriptionInfo = ItemGuideService.selectProcessInfo("SP_StandardItemGuide", process.pGubun, process.item_cd, process.revision_no, process.batch_size);

            string jsonData = Public_Function.DataTableToJSON(PrescriptionInfo);

            return Json(jsonData);
        }

        [HttpPost]
        public JsonResult GetPrescirptionInfo(string pSpName, string pGubun, string pItemCd, string pBatchSize, string pRevisionNo)
        {
            DataTable PrescriptionInfo = ItemGuideService.selectprescriptionInfo(pSpName, pGubun, pItemCd, pBatchSize, pRevisionNo);

            string jsonData = Public_Function.DataTableToJSON(PrescriptionInfo);

            return Json(jsonData);
        }

        [HttpPost]
        public JsonResult GetItemProcess(string pSpName, string pGubun, string pItemCd)
        {
            List<DataTable> ItemProcess = ItemGuideService.selectItemProcess(pSpName, pGubun, pItemCd);

            string jsonData1 = Public_Function.DataTableToJSON(ItemProcess[0]);
            string jsonData2 = Public_Function.DataTableToJSON(ItemProcess[1]);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData1);
            jsonList.Add(jsonData2);

            return Json(jsonList.ToArray());
        }

        [HttpPost]
        public JsonResult GetRevisionData(string pSpName, string pItemCd, bool pLatestRevisionCk, bool pRevisionSignEndCk, bool pRevisionCurrentCk)
        {

            DataTable dt = ItemGuideService.selectRevisionData(pSpName, pItemCd, pLatestRevisionCk, pRevisionSignEndCk, pRevisionCurrentCk);

            string jsonData = Public_Function.DataTableToJSON(dt).Replace("\r\n", "\\r\\n");

            return Json(jsonData);
        }

        [HttpPost]
        public JsonResult GetPresciprtionData(string pSpName, string pGubun, string pItemCd, string pItemBomNo, string pBatchSize)
        {

            DataTable dt = ItemGuideService.selectPrescriptionData(pSpName, pGubun, pItemCd, pItemBomNo, pBatchSize);

            string jsonData = Public_Function.DataTableToJSON(dt);

            return Json(jsonData);
        }

        [HttpPost]
        public JsonResult GetBomData(string pSpName, string pGubun, string pItemCd, string pItemBomNo, string pBatchSize)
        {

            DataTable dt = ItemGuideService.selectPrescriptionData(pSpName, pGubun, pItemCd, pItemBomNo, pBatchSize);

            string jsonData = Public_Function.DataTableToJSON(dt);

            return Json(jsonData);
        }

        [HttpPost]
        public JsonResult GetItemBomData(string pSpName, string pGubun, string pItemCd, string pBatchSize, string pRevisionNo)
        {

            DataTable dt = ItemGuideService.selectItemBOMData(pSpName, pGubun, pItemCd, pBatchSize, pRevisionNo);

            string jsonData = Public_Function.DataTableToJSON(dt);

            return Json(jsonData);
        }

        [HttpPost]
        public int RevisonCRUD(RevisionData revisionData)
        {
            int res = ItemGuideService.CRUD(revisionData);

            return res;
        }

        [HttpPost]
        public ActionResult PrescriptionCRUD(Prescription prescription, Boolean excelInput=false)
        {
            if(prescription.sum_ck == null)
            {
                prescription.sum_ck = "";
            }


            if ((ModelState.IsValid || prescription.pGubun == "DD") || excelInput)
            {
                ItemGuideService.CRUD(prescription);

                return Json(new { Ok = true });
            }
            else
            {
                return Json(new { Ok = false });
            }
        }

        [HttpPost]
        public void CopyPrescription(Prescription data1, Prescription data2)
        {
            DataTable dt = ItemGuideService.selectPrescriptionData("SP_ItemBomFull2", data1.pGubun, data1.item_cd, data1.item_bom_no, data1.batch_size);

            foreach (DataRow row in dt.AsEnumerable())
            {
                Prescription tmp = new Prescription(row, 1);

                tmp.pGubun = "ID";
                tmp.item_cd = data2.item_cd;
                tmp.item_bom_no = data2.item_bom_no;
                tmp.batch_size = data2.batch_size;

                ItemGuideService.CRUD(tmp);
            }
        }

        [HttpPost]
        public void ProcessCRUD(Process process)
        {

            ItemGuideService.CRUD(process);

        }

        [HttpPost]
        public void MoveProcess(Process process, string type)
        {

            ItemGuideService.moveProcess(process, type);

        }

        [HttpPost]
        public void CopyProcess(Process process)
        {
            DataTable dt = ItemGuideService.copyProcessData(process);
        }

        [HttpPost]
        public void SignCRUD(string gubun, string c_seq, string emp_cd, string item_cd, string revision_no, string batch_size, string validation_type, string current_ck, bool last_seq) {

            ItemGuideService.SignCRUD(gubun, c_seq, emp_cd, item_cd, revision_no, batch_size, validation_type);

            if(gubun.Equals("U") && last_seq)
            {
                current_ck = current_ck == "true" ? "Y" : "N";

                ItemGuideService.finalApproval("U10", item_cd, batch_size, revision_no, "Y", current_ck);
            }
            else if (gubun.Equals("U3") && last_seq)
            {
                current_ck = current_ck == "true" ? "N" : "N";

                ItemGuideService.finalApproval("U10", item_cd, batch_size, revision_no, "N", current_ck);
            }
        }

        [HttpPost]
        public string ItemGuideUseChk(string item_cd, string revision_no, string batch_size)
        {
            string res = ItemGuideService.ItemGuideUseChk(item_cd, revision_no, batch_size);

            return res;
        }

        //공정검사 조회
        [HttpPost]
        public JsonResult GetProcExam(SetProcExam setProcExam)
        {
            DataTable ProcExam = ItemGuideService.selectProcExam("SP_Item_Workguide", setProcExam.pGubun, setProcExam.item_cd, setProcExam.revision_no, setProcExam.batch_size, setProcExam.process_cd);

            string jsonData = Public_Function.DataTableToJSON(ProcExam);

            return Json(jsonData);
        }

        //공정검사 수정
        [HttpPost]
        public void ProcExamCRUD(SetProcExam setProcExam)
        {

            ItemGuideService.CRUD(setProcExam);

        }

        //작업방법 조회
        [HttpPost]
        public JsonResult GetWorkGuide(ItemWorkGuide itemWorkGuide)
        {
            DataTable WorkGuide = ItemGuideService.selectWorkGuide("SP_Item_Workguide", itemWorkGuide.pGubun, itemWorkGuide.item_cd, itemWorkGuide.revision_no, itemWorkGuide.batch_size, itemWorkGuide.process_cd);

            string jsonData = Public_Function.DataTableToJSON(WorkGuide);

            return Json(jsonData);
        }

        //작업방법 수정
        [HttpPost]
        public void ItemWorkGuideCRUD(ItemWorkGuide itemWorkGuide)
        {

            ItemGuideService.CRUD(itemWorkGuide);

        }

        [HttpPost]
        public string ItemGuideDeleteAll(string mp_ck, string item_cd, string batch_size, string item_bom_no)
        {
            string res = ItemGuideService.ItemGuideDeleteAll(mp_ck, item_cd, batch_size, item_bom_no);

            return res;
        }


        // 작업방법 복사 팝업
        [HttpGet]
        public ActionResult ItemWorkGuidePopupView(string mp_ck, string search_info)
        {
            DataTable WorkGuideData = ItemGuideService.SelectWorkGuide(mp_ck);
            ViewBag.WorkGuideData = Json(Public_Function.DataTableToJSON(WorkGuideData));
            ViewBag.mp_ck = mp_ck;
            ViewBag.search_info = search_info;

            return View();
        }

        // 개정에 대한 대장이 있는지 확인
        [HttpPost]
        public string ItemGuideCheckWorkOrder(string item_cd, string revision_no)
        {
            string res = ItemGuideService.CheckWorkOrder(item_cd, revision_no);

            return res;
        }
        #endregion


        #region 포장표준서 마스터 관리

        public ActionResult ItemGuide2()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable PackingItems = codeHelpService.GetCode(CodeHelpType.packingitem, Public_Function.selectedPLANT, "");
            DataTable Employees = codeHelpService.GetCode(CodeHelpType.employee, Public_Function.selectedPLANT, "");
            DataTable Workrooms = codeHelpService.GetCode(CodeHelpType.workroom, Public_Function.selectedPLANT, "");
            DataTable WorkroomHardwares = codeHelpService.GetCode(CodeHelpType.workroom_hardware_yn, Public_Function.selectedPLANT, "");
            DataTable Items = codeHelpService.GetCode(CodeHelpType.item, Public_Function.selectedPLANT, "");
            DataTable Equipments = codeHelpService.GetCode(CodeHelpType.equipment, Public_Function.selectedPLANT, "");
            DataTable CommonCodes = codeHelpService.GetCode(CodeHelpType.commoncode, Public_Function.selectedPLANT, "");
            DataTable Processes = codeHelpService.GetCode(CodeHelpType.process, Public_Function.selectedPLANT, "");
            DataTable Packingmaterial = codeHelpService.GetCode(CodeHelpType.material4, Public_Function.selectedPLANT, "");
            List<DataTable> ItemProcess = ItemGuideService.selectItemProcess("SP_StandardItemGuide2", "S10", "");
            DataTable ccp = ItemGuideService.GetCcp("CODEHELP");

            ViewBag.PackingItems = Json(Public_Function.DataTableToJSON(PackingItems));
            ViewBag.Employees = Json(Public_Function.DataTableToJSON(Employees));
            ViewBag.Workrooms = Json(Public_Function.DataTableToJSON(Workrooms));
            ViewBag.WorkroomHardwares = Json(Public_Function.DataTableToJSON(WorkroomHardwares));
            ViewBag.Items = Json(Public_Function.DataTableToJSON(Items));
            ViewBag.Equipments = Json(Public_Function.DataTableToJSON(Equipments));
            ViewBag.CommonCodes = Json(Public_Function.DataTableToJSON(CommonCodes));
            ViewBag.Processes = Json(Public_Function.DataTableToJSON(Processes));
            ViewBag.ItemProcess = Json(Public_Function.DataTableToJSON(ItemProcess[0]));
            ViewBag.ItemProcessDetail = Json(Public_Function.DataTableToJSON(ItemProcess[1]));
            ViewBag.Packingmaterial = Json(Public_Function.DataTableToJSON(Packingmaterial));
            ViewBag.ccp = Json(Public_Function.DataTableToJSON(ccp));

            return View();
        }

        [HttpGet]
        public JsonResult ItemGuide2GetItemInfo(string item_cd, string revision_no)
        {
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            if (revision_no.Equals("NEW"))
            {
                revision_no = "";
            }

            string pSpName = "SP_StandardItemGuide2";

            DataTable PackingItemInfo = ItemGuideService.selectItemDetail(pSpName, "S5", item_cd, "1", revision_no);
            DataTable SignInfo = ItemGuideService.selectSignInfo(pSpName, "EL", "2004", item_cd, "P", revision_no);
            DataTable ProcessInfo = ItemGuideService.selectProcessInfo(pSpName, "S2", item_cd, revision_no, "1");
            DataTable PrescriptionInfo = ItemGuideService.selectprescriptionInfo(pSpName, "S6", item_cd, "1", revision_no);

            string[] jsonArr = new string[4];

            jsonArr[0] = Public_Function.DataTableToJSON(PackingItemInfo);
            jsonArr[1] = Public_Function.DataTableToJSON(SignInfo);
            jsonArr[2] = Public_Function.DataTableToJSON(ProcessInfo);
            jsonArr[3] = Public_Function.DataTableToJSON(PrescriptionInfo);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetItemGuide2RevisionData(bool latestRevisionCk, bool revisionSignEndCk, bool revisionCurrentCk)
        {
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            DataTable dt = itemGuide2Service.selectRevisionData(latestRevisionCk, revisionSignEndCk, revisionCurrentCk);

            string jsonData = Public_Function.DataTableToJSON(dt);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetProcessInfo2(Process process)
        {
            DataTable PrescriptionInfo = ItemGuideService.selectProcessInfo("SP_StandardItemGuide2", process.pGubun, process.item_cd, process.revision_no, "1");

            string jsonData = Public_Function.DataTableToJSON(PrescriptionInfo);

            return Json(jsonData);
        }


        [HttpPost]
        public string ItemGuide2RevisionTRX(RevisionData revisionData)
        {
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            string res = itemGuide2Service.ItemGuide2RevisionTRX(revisionData);

            return res;
        }

        [HttpPost]
        public string ItemGuide2CopyProcess(RevisionData revisionData)
        {
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            string res = itemGuide2Service.ItemGuide2CopyProcess(revisionData);

            return res;
        }

        [HttpPost]
        public string ItemGuide2ProcessTRX([JsonBinder]List<RevisionData> processsData)
        {
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            string res = itemGuide2Service.ItemGuide2ProcessTRX(processsData);

            return res;
        }

        [HttpPost]
        public void ItemGuide2MoveProcess(RevisionData revisionData)
        {
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            string res = itemGuide2Service.ItemGuide2MoveProcess(revisionData);
        }

        [HttpPost]
        public string ItemGuide2SignCRUD(string gubun, string c_seq, string emp_cd, string item_cd, string revision_no, string validation_type, int signLength, string current_ck, bool last_seq)
        {
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            itemGuide2Service.SignCRUD(gubun, c_seq, emp_cd, item_cd, revision_no, validation_type);

            if (gubun.Equals("U") && last_seq)
            {
                current_ck = current_ck == "true" ? "Y" : "N";

                itemGuide2Service.finalApproval("U10", item_cd, revision_no, "Y", current_ck);
            }
            else if (gubun.Equals("U3") && last_seq)
            {
                current_ck = current_ck == "true" ? "N" : "N";

                itemGuide2Service.finalApproval("U10", item_cd, revision_no, "N", current_ck);
            }

            return "";
        }

        [HttpPost]
        public string ItemGuide2UseChk(string item_cd, string revision_no)
        {
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            string res = itemGuide2Service.ItemGuide2UseChk(item_cd, revision_no);

            return res;
        }

        [HttpGet]
        public ActionResult BomPopupView(string item_cd, string item_bom_no, int sign_cnt, string revision_no)
        {
            CodeHelpService codeHelpService = new CodeHelpService();
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            DataTable unitGubuns = selectBoxService.GetSelectBox("공통코드", "D", "CM012", "unitGubuns");
            //DataTable process = codeHelpService.GetCode(CodeHelpType.process, Public_Function.selectedPLANT, "");
            DataTable packingitem = codeHelpService.GetCode(CodeHelpType.packingitem, Public_Function.selectedPLANT, "");
            DataTable material = codeHelpService.GetCode(CodeHelpType.material4, Public_Function.selectedPLANT, "");

            DataTable bomData = itemGuide2Service.SelectBomData(item_cd, item_bom_no);
            DataTable bomDetailData = itemGuide2Service.SelectBomDetailData(item_cd, item_bom_no, "SD");

            DataTable ProcessInfo = ItemGuideService.selectProcessInfo("SP_StandardItemGuide2", "S2", item_cd, revision_no, "1");


            ViewBag.process = Json(Public_Function.DataTableToJSON(ProcessInfo));
            ViewBag.packingitem = Json(Public_Function.DataTableToJSON(packingitem));
            ViewBag.material = Json(Public_Function.DataTableToJSON(material));

            ViewBag.bomData = Json(Public_Function.DataTableToJSON(bomData));
            ViewBag.bomDetailData = Json(Public_Function.DataTableToJSON(bomDetailData));
            ViewBag.itemUnit = Json(Public_Function.DataTableToJSON(unitGubuns));

            ViewBag.sign_cnt = sign_cnt;

            return View();
        }

        [HttpGet]
        public JsonResult BomSearch(string item_cd, string item_bom_no, string gubun)
        {
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            DataTable bomDetailData = itemGuide2Service.SelectBomDetailData(item_cd, item_bom_no, gubun);

            return Json(Public_Function.DataTableToJSON(bomDetailData), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string BomTRX([JsonBinder]Prescription bomData)
        {
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            string res = itemGuide2Service.BomTRX(bomData);

            return res;
        }

        //공정검사 조회 : 포장
        [HttpPost]
        public JsonResult ItemGuide2GetProcExam(SetProcExam setProcExam)
        {
            DataTable ProcExam = ItemGuideService.selectProcExam("SP_Item_Workguide2", setProcExam.pGubun, setProcExam.item_cd, setProcExam.revision_no, setProcExam.batch_size, setProcExam.process_cd);

            string jsonData = Public_Function.DataTableToJSON(ProcExam);

            return Json(jsonData);
        }

        //공정검사 수정 : 포장
        [HttpPost]
        public void ItemGuide2ProcExamCRUD(SetProcExam setProcExam)
        {

            ItemGuideService.ItemGuide2CRUD(setProcExam);

        }

        //작업방법 조회 : 포장
        [HttpPost]
        public JsonResult ItemGuide2GetWorkGuide(ItemWorkGuide itemWorkGuide)
        {
            DataTable WorkGuide = ItemGuideService.selectWorkGuide("SP_Item_Workguide2", itemWorkGuide.pGubun, itemWorkGuide.item_cd, itemWorkGuide.revision_no, itemWorkGuide.batch_size, itemWorkGuide.process_cd);

            string jsonData = Public_Function.DataTableToJSON(WorkGuide);

            return Json(jsonData);
        }

        //작업방법 수정
        [HttpPost]
        public void ItemGuide2ItemWorkGuideCRUD(ItemWorkGuide itemWorkGuide)
        {

            ItemGuideService.ItemGuide2CRUD(itemWorkGuide);

        }

        [HttpPost]
        public string ItemGuide2CheckWorkOrder(string item_cd, string revision_no)
        {
            ItemGuide2Service itemGuide2Service = new ItemGuide2Service();

            string res = itemGuide2Service.CheckWorkOrder(item_cd, revision_no);

            return res;
        }
        #endregion


        #region 제제/포장 표준서마스터 공통
        /// <summary>
        ///  <para>공정 정보를 다이어그램으로 가지고 오기 위한 데이터 조회</para>
        ///  <para>jsonArr[0] : 다이어그램 Node 데이터 (도형)</para>
        ///  <para>jsonArr[1] : 다이어그램 Edge 데이터 (도형과 도형을 연결하는 선)</para>
        /// </summary>
        /// <param name="item_cd"></param>
        /// <param name="revision_no"></param>
        /// <param name="order_type"></param>
        /// <returns></returns>
        public JsonResult ProcInfoPopupView(string item_cd, string revision_no, string order_type)
        {
            DataSet processInfomationSet = ItemGuideService.GetProcessInfomationSet(item_cd, revision_no, order_type);

            string[] jsonArr = new string[2];

            jsonArr[0] = processInfomationSet.Tables[0].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(processInfomationSet.Tables[0]);
            jsonArr[1] = processInfomationSet.Tables[1].Rows.Count == 0 ? null : Public_Function.DataTableToJSON(processInfomationSet.Tables[1]);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 원자재 품목별 사용현황 컨트롤러
        [CheckSession]
        public ActionResult MaterialBomList()
        {
            List<Item> items = materialBomListService.SelectItem("", "");

            DataTable Gubuns = materialBomListService.GetGubuns("SP_MaterialBomList", "item_gb_search", "");
            DataTable MaterialBomList = materialBomListService.SelectBomInfo("SP_MaterialBomList", "S", "", "%");

            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(items);

            ViewBag.items = Json(JSONresult);

            ViewBag.Gubuns = Gubuns;
            ViewBag.MaterialBomList = Json(Public_Function.DataTableToJSON(MaterialBomList));

            return View();
        }

        [HttpPost]
        public JsonResult GetBomInfo(string pSpName, string pGubun, string pItemCd, string pItemGubun)
        {

            DataTable bomInfo = materialBomListService.SelectBomInfo(pSpName, pGubun, pItemCd, pItemGubun);

            string jsonData = Public_Function.DataTableToJSON(bomInfo);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);

            return Json(jsonList.ToArray());
        }

        public JsonResult GetBomDetail(string pSpName, string pGubun, string pItemCd)
        {

            DataTable bomInfo = materialBomListService.SelectBomDetail(pSpName, pGubun, pItemCd);

            string jsonData = Public_Function.DataTableToJSON(bomInfo);

            List<string> jsonList = new List<string>();
            jsonList.Add(jsonData);

            return Json(jsonList.ToArray());
        }
        #endregion
    }
}