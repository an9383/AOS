using HACCP.Filter;
using HACCP.Libs;
using HACCP.Services.Comm;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACCP.Models.RowMatln;
using HACCP.Services.RowMatln;
using System.Web.Mvc;
using HACCP.Attribute;

namespace HACCP.Controllers
{
    public class RowMatlnController : Controller
    {
        private ReceiptCheckSOPService receiptCheckSOPService = new ReceiptCheckSOPService();
        private PurchaseManage2Service purchaseManage2Service = new PurchaseManage2Service(); // 원자재 발주 관리
        private ReceiptCheck2Service receiptCheck2Service = new ReceiptCheck2Service(); // 원자재 검수 관리
        private ReceiptCheckMaterial2Service receiptCheckMaterial2Service = new ReceiptCheckMaterial2Service(); // 원료 입고내용 확인
        private ReceiveMaterialService receiveMaterialService = new ReceiveMaterialService(); // 원자재 입고 관리
        private SelectBoxService selectBoxService = new SelectBoxService();

        #region ReceiptCheckSOP - 입고 체크 리스트
        [CheckSession]
        [HttpGet]
        public ActionResult ReceiptCheckSOP(ReceiptCheckSOP rModel)
        {

            rModel.item_gb = "3";
            DataTable ReceiptCheckSOP = receiptCheckSOPService.Select(rModel);
            DataTable Select_item_gb = selectBoxService.GetSelectBox("공통코드", "N", "CM001", "Select_item_gb");

            ViewBag.ReceiptCheckSOP = Json(Public_Function.DataTableToJSON(ReceiptCheckSOP)); 
            ViewBag.ReceiptCheckSOPAuth = Json(HttpContext.Session["ReceiptCheckSOPAuth"]);
            ViewBag.Select_item_gb = Select_item_gb;

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ReceiptCheckSOPSelect(ReceiptCheckSOP rModel)
        {
            DataTable ReceiptCheckSOP = receiptCheckSOPService.Select(rModel);

            return Json(Public_Function.DataTableToJSON(ReceiptCheckSOP));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ReceiptCheckSOPCRUD([JsonBinder] List<ReceiptCheckSOP> rModel)
        {
            for(int i = 0; i < rModel.Count; i++)
            {
                if (rModel[i].gubun.Equals("I"))
                {
                    rModel[i].gubun = "I";
                }
                else if (rModel[i].gubun.Equals("U"))
                {
                    rModel[i].gubun = "U";
                }
                else if (rModel[i].gubun.Equals("D"))
                {
                    rModel[i].gubun = "D";
                }
            }

            string res = receiptCheckSOPService.ReceiptCheckSOPCRUD(rModel);

            var resJson = "{ \"message\": \"" + res + "\" }";

            return Json(resJson);
        }
        #endregion

        #region 원료 발주 관리
        [CheckSession]
        [HttpGet]
        public ActionResult PurchaseManage2_M(PurchaseManage2 model)
        {
            /* 그리드 조회 */
           
            // 초기값 셋팅
            model.search_data = "0"; // (검색) 기간 상태값 (발주일자 or 입고예정일)
            model.purchase_state = "%"; //(검색) 상태
            model.start_date = DateTime.Today.AddDays(-30).ToShortDateString(); // (검색) 시작일
            model.end_date = DateTime.Today.ToShortDateString(); // (검색) 종료일
            model.option = "3"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = "4"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.order_request_no_S = ""; // (검색) 생산의뢰번호
            model.vender_cd_S = ""; // (검색) 거래처 코드
            model.manufacture_item_cd_S = ""; // (검색) 제조품목 코드

            DataTable dt = purchaseManage2Service.Select(model);

            /* 검색 셋팅 */

            model.start_date = DateTime.Today.AddDays(-30).ToShortDateString();
            model.end_date = DateTime.Today.ToShortDateString();

            /* 팝업 셋팅 */
            DataTable manufacture = purchaseManage2Service.ManufacturePopup("CODEHELP"); // 제조처
            DataTable supply = purchaseManage2Service.SupplyPopup("CODEHELP"); // 공급처
            DataTable emp = purchaseManage2Service.EmpPopup("CODEHELP"); // 입고 담당자
            DataTable itemCD = purchaseManage2Service.ItemCDPopup("CODEHELP"); // 원료 품목 팝업

            /* 드랍박스 */
            DataTable s_purchase_kind = selectBoxService.GetSelectBox("공통코드", "P", "CM068", "purchase_kind");
            DataTable s_purchase_state = selectBoxService.GetSelectBox("공통코드", "S", "WR100", "purchase_state");

            ViewBag.PurchaseManage2_MData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.PurchaseManage2_MAuth = Json(HttpContext.Session["PurchaseManage2_MAuth"]);

            ViewBag.manufacture = Json(Public_Function.DataTableToJSON(manufacture));
            ViewBag.supply = Json(Public_Function.DataTableToJSON(supply));
            ViewBag.emp = Json(Public_Function.DataTableToJSON(emp));
            ViewBag.itemCD = Json(Public_Function.DataTableToJSON(itemCD));

            ViewBag.s_purchase_kind = s_purchase_kind;
            ViewBag.s_purchase_state = s_purchase_state;

            return View();
        }

        /// <summary>
        /// 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult PurchaseManage2_M_Select(PurchaseManage2 model)
        {
            DataTable dt = purchaseManage2Service.Select_List(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 발주번호 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult PurchaseManage2_M_NO(PurchaseManage2 model)
        {
            DataTable dt = purchaseManage2Service.S_NO(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 원료 발주 관리 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult PurchaseManage2_M_Search(PurchaseManage2 model)
        {
            model.search_data = model.search_data; // (검색) 기간 상태값 (발주일자 or 입고예정일)
            model.purchase_state = model.purchase_state; //(검색) 상태
            model.start_date = model.start_date; // (검색) 시작일
            model.end_date = model.end_date; // (검색) 종료일
            model.option = "3"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = "4"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.order_request_no_S = ""; // (검색) 생산의뢰번호
            model.vender_cd_S = ""; // (검색) 거래처 코드
            //model.manufacture_item_cd_S = model.item_cd_S; // (검색) 제조품목 코드
            model.manufacture_item_cd_S = model.item_nm_S; // (검색) 제조품목 코드

            DataTable dt = purchaseManage2Service.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 입력, 수정, 삭제 기능
        /// </summary>
        [CheckSession]
        [HttpPost]
        public JsonResult PurchaseManage2_CRUD(PurchaseManage2 model)
        {
            if (!model.gubun.Equals("D") && !model.gubun.Equals("D3"))
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Ok = false });
                }
            }
            
            string res = purchaseManage2Service.CRUD(model, model.gubun);

            var resJson = "{ \"message\": \"" + res + "\" }";

            return Json(resJson);
            
        }

        [HttpPost]
        public JsonResult PurchaseManage2_batch([JsonBinder] List<PurchaseManage2> purchaseManage2, string gubun, string purchase_no)
        {

            for (int i = 0; i < purchaseManage2.Count; i++)
            {
                if (purchaseManage2[i].gubun.Equals("I_D"))
                {
                    purchaseManage2[i].gubun = "I2";
                }
                else if (purchaseManage2[i].gubun.Equals("U_D"))
                {
                    purchaseManage2[i].gubun = "U2";
                }
                else if (purchaseManage2[i].gubun.Equals("D_D"))
                {
                    purchaseManage2[i].gubun = "D2";
                }                
            }

            string res = purchaseManage2Service.PurchaseManage2_batch(purchaseManage2, gubun, purchase_no);

            var resJson = "{ \"message\": \"" + res + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 담당자 정보 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult PurchaseManage2_S_empInfo(PurchaseManage2 model)
        {
            DataTable dt = purchaseManage2Service.S_empInfo(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 최근 단가, master단위 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult PurchaseManage2_PRICE(PurchaseManage2 model)
        {
            DataTable dt = purchaseManage2Service.S_PRICE(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion

        #region 자재 발주 관리
        [CheckSession]
        [HttpGet]
        public ActionResult PurchaseManage2_P()
        {
            /* 그리드 조회 */

            PurchaseManage2 model = new PurchaseManage2();

            // 초기값 셋팅
            model.search_data = "0"; // (검색) 기간 상태값 (발주일자 or 입고예정일)
            model.purchase_state = "%"; //(검색) 상태
            model.start_date = DateTime.Today.AddDays(-30).ToShortDateString(); // (검색) 시작일
            model.end_date = DateTime.Today.ToShortDateString(); // (검색) 종료일
            model.option = "2"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = ""; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.order_request_no_S = ""; // (검색) 생산의뢰번호
            model.vender_cd_S = ""; // (검색) 거래처 코드
            model.manufacture_item_cd_S = ""; // (검색) 제조품목 코드

            DataTable dt = purchaseManage2Service.Select(model);

            /* 검색 셋팅 */

            model.start_date = DateTime.Today.AddDays(-30).ToShortDateString();
            model.end_date = DateTime.Today.ToShortDateString();

            /* 팝업 셋팅 */
            DataTable p_manufacture = purchaseManage2Service.ManufacturePopup("CODEHELP"); // 제조처
            DataTable p_supply = purchaseManage2Service.SupplyPPopup("CODEHELP"); // 공급처
            DataTable p_emp = purchaseManage2Service.EmpPopup("CODEHELP"); // 입고 담당자
            DataTable p_itemCD = purchaseManage2Service.ItemPCDPopup("CODEHELP"); // 자재 품목 팝업

            /* 드랍박스 */
            DataTable s_purchase_kind = selectBoxService.GetSelectBox("공통코드", "P", "CM068", "purchase_kind");
            DataTable s_purchase_state = selectBoxService.GetSelectBox("공통코드", "S", "WR100", "purchase_state");

            ViewBag.PurchaseManage2_PData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.PurchaseManage2_PAuth = Json(HttpContext.Session["PurchaseManage2_PAuth"]);

            ViewBag.p_manufacture = Json(Public_Function.DataTableToJSON(p_manufacture));
            ViewBag.p_supply = Json(Public_Function.DataTableToJSON(p_supply));
            ViewBag.p_emp = Json(Public_Function.DataTableToJSON(p_emp));
            ViewBag.p_itemCD = Json(Public_Function.DataTableToJSON(p_itemCD));

            ViewBag.ps_purchase_kind = s_purchase_kind;
            ViewBag.ps_purchase_state = s_purchase_state;

            return View();
        }

        /// <summary>
        /// 아래 그리드 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult PurchaseManage2_P_Select(PurchaseManage2 model)
        {
            DataTable dt = purchaseManage2Service.Select_List(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 자재 발주 관리 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult PurchaseManage2_P_Search(PurchaseManage2 model)
        {
            model.search_data = model.search_data; // (검색) 기간 상태값 (발주일자 or 입고예정일)
            model.purchase_state = model.purchase_state; //(검색) 상태
            model.start_date = model.start_date; // (검색) 시작일
            model.end_date = model.end_date; // (검색) 종료일
            model.option = "2"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 1 : 상품
            model.option2 = ""; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 1 : 상품
            model.order_request_no_S = ""; // (검색) 생산의뢰번호
            model.vender_cd_S = ""; // (검색) 거래처 코드
            //model.manufacture_item_cd_S = model.item_cd_S; // (검색) 제조품목 코드
            model.manufacture_item_cd_S = model.item_nm_S; // (검색) 제조품목 코드
            DataTable dt = purchaseManage2Service.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion

        #region 원료 검수 관리
        [CheckSession]
        [HttpGet]
        public ActionResult ReceiptCheck2_M()
        {
            /* 그리드 조회 */

            ReceiptCheck2 model = new ReceiptCheck2();

            // 초기값 셋팅
            model.purchase_no = ""; // (검색) 발주번호            
            model.start_date = DateTime.Today.AddDays(-30).ToShortDateString(); // (검색) 시작일
            model.end_date = DateTime.Today.ToShortDateString(); // (검색) 종료일
            model.item_cd_S = ""; // (검색) 품목코드
            model.option = "3";  // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = "4"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.check = "0"; //0:발주일, 1:입고일, 2:입고예정일
            model.purchase_status_Y = "N"; // (검색) 검수완료포함 여부
            model.manufacture_item_cd_S = ""; // (검색) 제조품목 코드

            DataTable dt = receiptCheck2Service.Select(model);

            /* 팝업 */
            DataTable RCM_itemCD = receiptCheck2Service.ItemCDPopup("CODEHELP"); // 원료 품목 팝업

            ViewBag.ReceiptCheck2_MData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ReceiptCheck2_MAuth = Json(HttpContext.Session["ReceiptCheck2_MAuth"]);

            ViewBag.RCM_itemCD = Json(Public_Function.DataTableToJSON(RCM_itemCD));

            return View();
        }

        /// <summary>
        /// 왼쪽 그리드 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiptCheck2_Select(ReceiptCheck2 model)
        {
            DataTable dt = receiptCheck2Service.Select_List(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 원료 검수 관리 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiptCheck2_M_Search(ReceiptCheck2 model)
        {
            model.purchase_no = ""; // (검색) 발주번호            
            //model.start_date = DateTime.Today.AddDays(-30).ToShortDateString(); // (검색) 시작일
            //model.end_date = DateTime.Today.ToShortDateString(); // (검색) 종료일
            //model.item_cd_S = ""; // (검색) 품목코드
            model.option = "3"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = "4"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            //model.check = "0"; //0:발주일, 1:입고일, 2:입고예정일
            if (model.purchase_status_Y == null) // (검색) 검수완료포함 여부
                model.purchase_status_Y = "N";            
            model.manufacture_item_cd_S = ""; // (검색) 제조품목 코드

            DataTable dt = receiptCheck2Service.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        
        /// <summary>
        /// 원료 입고내용 확인
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        public ActionResult ReceiptCheckMaterial2_M(ReceiptCheckMaterial2 model)
        {
            // 체크 리스트 Grid
            DataTable ReceiptCheckMaterial2_M = receiptCheckMaterial2Service.Select(model);
            // 입고 검수 정보
            DataTable dt = receiptCheckMaterial2Service.SelectList(model);

            /* 팝업 */
            DataTable RCMM_Worker = receiptCheckMaterial2Service.workerPopup("CODEHELP"); // 점검자 팝업
            DataTable RCMM_Checker = receiptCheckMaterial2Service.workerPopup("CODEHELP"); // 확인자 팝업

            // Select Box
            DataTable s_receipt_check_state = selectBoxService.GetSelectBox("공통코드", "D", "WR104", "s_receipt_check_state");   // 검수결과
            DataTable s_purchase_status = selectBoxService.GetSelectBox("공통코드", "D", "WR101", "s_purchase_status");   // 입고구분

            ViewBag.RCMM_Gubun = "I";        //'입고확인' 버튼으로 띄우면 입력만 가능.

            ViewBag.s_receipt_check_state = s_receipt_check_state;
            ViewBag.s_purchase_status = s_purchase_status;

            ViewBag.RCMM_Worker = Json(Public_Function.DataTableToJSON(RCMM_Worker));
            ViewBag.RCMM_Checker = Json(Public_Function.DataTableToJSON(RCMM_Checker));

            ViewBag.ReceiptCheckMaterial2_MData = Json(Public_Function.DataTableToJSON(dt));

            return View("ReceiptCheckMaterial2_M", Json(Public_Function.DataTableToJSON(ReceiptCheckMaterial2_M)));
        }

        /// <summary>
        /// 원료 입고내용 수정&삭제
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        public ActionResult ReceiptCheckMaterial2_M_ED(ReceiptCheckMaterial2 model, string receipt_check_rpt_id)
        {
            // 팩 정보 receipt_status값 출력
            Boolean ReceiptCheckStatus = receiptCheckMaterial2Service.ReceiptCheckStatus(model, receipt_check_rpt_id);
            if(ReceiptCheckStatus == true)              //Grid 내 Row 더블클릭으로 조회
            {
                ViewBag.RCMM_Gubun = "U";                //입고완료가 아니면 수정&삭제 가능
            }
            else
            {
                ViewBag.RCMM_Gubun = "D";                //입고완료 상태면 삭제만 가능
            }

            // 체크 리스트 Grid
            DataTable ReceiptCheckMaterial2_M_ED = receiptCheckMaterial2Service.Select(model);
            // 입고 검수 정보 불러오기
            DataTable dt = receiptCheckMaterial2Service.SelectList_Load(model);

            /* 팝업 */
            DataTable RCMM_Worker = receiptCheckMaterial2Service.workerPopup("CODEHELP"); // 점검자 팝업
            DataTable RCMM_Checker = receiptCheckMaterial2Service.workerPopup("CODEHELP"); // 확인자 팝업

            // Select Box
            DataTable s_receipt_check_state = selectBoxService.GetSelectBox("공통코드", "D", "WR104", "s_receipt_check_state");   // 검수결과
            DataTable s_purchase_status = selectBoxService.GetSelectBox("공통코드", "D", "WR101", "s_purchase_status");   // 입고구분

            ViewBag.s_receipt_check_state = s_receipt_check_state;
            ViewBag.s_purchase_status = s_purchase_status;

            ViewBag.RCMM_Worker = Json(Public_Function.DataTableToJSON(RCMM_Worker));
            ViewBag.RCMM_Checker = Json(Public_Function.DataTableToJSON(RCMM_Checker));

            ViewBag.ReceiptCheckMaterial2_MData = Json(Public_Function.DataTableToJSON(dt));

            return View("ReceiptCheckMaterial2_M", Json(Public_Function.DataTableToJSON(ReceiptCheckMaterial2_M_ED)));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ReceiptCheckMaterial2_M_Search(ReceiptCheckMaterial2 model)
        {
            // 체크 리스트 Grid
            model.option = "3"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = "4"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            DataTable ReceiptCheckMaterial2_M = receiptCheckMaterial2Service.Select(model);
            // 입고 검수 정보
            DataTable dt = receiptCheckMaterial2Service.SelectList(model);
                                   
            ViewBag.ReceiptCheckMaterial2_MData = Json(Public_Function.DataTableToJSON(dt));

            return Json(Public_Function.DataTableToJSON(ReceiptCheckMaterial2_M));
        }

        [HttpPost]
        public JsonResult ReceiptCheckMaterial2_M_ED_Search(ReceiptCheckMaterial2 model)
        {
            // 체크 리스트 Grid
            model.option = "3"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = "4"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            DataTable ReceiptCheckMaterial2_M = receiptCheckMaterial2Service.Select(model);
            // 입고 검수 정보
            DataTable dt = receiptCheckMaterial2Service.SelectList_Load(model);

            ViewBag.ReceiptCheckMaterial2_MData = Json(Public_Function.DataTableToJSON(dt));

            return Json(Public_Function.DataTableToJSON(ReceiptCheckMaterial2_M));
        }
        
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiptCheckMaterial2_P_Search(ReceiptCheckMaterial2 model)
        {
            // 체크 리스트 Grid
            model.option = "2"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = ""; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            DataTable ReceiptCheckMaterial2_P = receiptCheckMaterial2Service.Select(model);
            // 입고 검수 정보
            DataTable dt = receiptCheckMaterial2Service.SelectList(model);

            ViewBag.ReceiptCheckMaterial2_PData = Json(Public_Function.DataTableToJSON(dt));

            return Json(Public_Function.DataTableToJSON(ReceiptCheckMaterial2_P));
        }

        [HttpPost]
        public JsonResult ReceiptCheckMaterial2_P_ED_Search(ReceiptCheckMaterial2 model)
        {
            // 체크 리스트 Grid
            model.option = "2"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = ""; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            DataTable ReceiptCheckMaterial2_P = receiptCheckMaterial2Service.Select(model);
            // 입고 검수 정보
            DataTable dt = receiptCheckMaterial2Service.SelectList_Load(model);

            ViewBag.ReceiptCheckMaterial2_PData = Json(Public_Function.DataTableToJSON(dt));

            return Json(Public_Function.DataTableToJSON(ReceiptCheckMaterial2_P));
        }

        public JsonResult Overlap_check_no(ReceiptCheckMaterial2 model)
        {
            DataTable Overlap_check_no = receiptCheckMaterial2Service.Overlap_check_no(model);

            return Json(Public_Function.DataTableToJSON(Overlap_check_no));
        }

        public JsonResult ReceiptCheckMaterial2_TRX(ReceiptCheckMaterial2 model, string gubun, string receipt_check_rpt_id)
        {
            if (!gubun.Equals("D"))
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Ok = false });
                }
            }

            string res = receiptCheckMaterial2Service.ReceiptCheckMaterial2_TRX(model, gubun, receipt_check_rpt_id);

            var resJson = "{ \"message\": \"" + res + "\" }";

            return Json(resJson);
        }

        [HttpPost]
        public JsonResult ReceiptCheckMaterial2_batch([JsonBinder]List<ReceiptCheckMaterial2> receiptCheckMaterial2, string gubun, string receipt_check_rpt_id)
        {
            for (int i = 0; i < receiptCheckMaterial2.Count; i++)
            {
                if (gubun.Equals("ICHK"))
                {
                    receiptCheckMaterial2[i].gubun = "ICHK";
                }else if (gubun.Equals("UCHK"))
                {
                    receiptCheckMaterial2[i].gubun = "UCHK";
                }
            }
            string res = receiptCheckMaterial2Service.ReceiptCheckMaterial2_batch(receiptCheckMaterial2, gubun, receipt_check_rpt_id);

            var resJson = "{ \"message\": \"" + res + "\" }";

            return Json(resJson);
        }
        #endregion

        #region 자재 검수 관리

        [CheckSession]
        [HttpGet]
        public ActionResult ReceiptCheck2_P()
        {
            /* 그리드 조회 */

            ReceiptCheck2 model = new ReceiptCheck2();

            // 초기값 셋팅
            model.purchase_no = ""; // (검색) 발주번호            
            model.start_date = DateTime.Today.AddDays(-30).ToShortDateString(); // (검색) 시작일
            model.end_date = DateTime.Today.ToShortDateString(); // (검색) 종료일
            model.item_cd_S = ""; // (검색) 품목코드
            model.option = "2"; // 원자재 구분 option 3 : 원료 / 2 : 자재
            model.check = "0"; //0:발주일, 1:입고일, 2:입고예정일
            model.purchase_status_Y = "N"; // (검색) 검수완료포함 여부
            model.manufacture_item_cd_S = ""; // (검색) 제조품목 코드

            DataTable dt = receiptCheck2Service.Select(model);

            /* 팝업 */
            DataTable RCP_itemCD = receiptCheck2Service.ItemPCDPopup_P("CODEHELP"); // 원료 품목 팝업

            ViewBag.ReceiptCheck2_PData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ReceiptCheck2_PAuth = Json(HttpContext.Session["ReceiptCheck2_PAuth"]);

            ViewBag.RCP_itemCD = Json(Public_Function.DataTableToJSON(RCP_itemCD));

            return View();
        }


        /// <summary>
        /// 원료 검수 관리 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiptCheck2_P_Search(ReceiptCheck2 model)
        {
            model.purchase_no = ""; // (검색) 발주번호            
            //model.start_date = DateTime.Today.AddDays(-30).ToShortDateString(); // (검색) 시작일
            //model.end_date = DateTime.Today.ToShortDateString(); // (검색) 종료일
            //model.item_cd_S = ""; // (검색) 품목코드
            model.option = "2"; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = ""; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            //model.check = "0"; //0:발주일, 1:입고일, 2:입고예정일
            if (model.purchase_status_Y == null) // (검색) 검수완료포함 여부
                model.purchase_status_Y = "N";
            model.manufacture_item_cd_S = ""; // (검색) 제조품목 코드 

            DataTable dt = receiptCheck2Service.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 자재 입고내용 확인
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        public ActionResult ReceiptCheckMaterial2_P(ReceiptCheckMaterial2 model)
        {
            // 체크 리스트 Grid
            DataTable ReceiptCheckMaterial2_P = receiptCheckMaterial2Service.Select(model);
            // 입고 검수 정보
            DataTable dt = receiptCheckMaterial2Service.SelectList(model);

            /* 팝업 */
            DataTable RCMP_Worker = receiptCheckMaterial2Service.workerPopup("CODEHELP"); // 점검자 팝업
            DataTable RCMP_Checker = receiptCheckMaterial2Service.workerPopup("CODEHELP"); // 확인자 팝업

            // Select Box
            DataTable s_receipt_check_state = selectBoxService.GetSelectBox("공통코드", "D", "WR104", "s_receipt_check_state");   // 검수결과
            DataTable s_purchase_status = selectBoxService.GetSelectBox("공통코드", "D", "WR101", "s_purchase_status");   // 입고구분

            ViewBag.RCMP_Gubun = "I";        //'입고확인' 버튼으로 띄우면 입력만 가능.

            ViewBag.sp_receipt_check_state = s_receipt_check_state;
            ViewBag.sp_purchase_status = s_purchase_status;

            ViewBag.RCMP_Worker = Json(Public_Function.DataTableToJSON(RCMP_Worker));
            ViewBag.RCMP_Checker = Json(Public_Function.DataTableToJSON(RCMP_Checker));

            ViewBag.ReceiptCheckMaterial2_PData = Json(Public_Function.DataTableToJSON(dt));

            return View("ReceiptCheckMaterial2_P", Json(Public_Function.DataTableToJSON(ReceiptCheckMaterial2_P)));
        }

        /// <summary>
        /// 원료 입고내용 수정&삭제
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        public ActionResult ReceiptCheckMaterial2_P_ED(ReceiptCheckMaterial2 model, string receipt_check_rpt_id)
        {
            // 팩 정보 receipt_status값 출력
            Boolean ReceiptCheckStatus = receiptCheckMaterial2Service.ReceiptCheckStatus(model, receipt_check_rpt_id);
            if (ReceiptCheckStatus == true)              //Grid 내 Row 더블클릭으로 조회
            {
                ViewBag.RCMP_Gubun = "U";              //입고완료가 아니면 수정&삭제 가능
            }
            else
            {
                ViewBag.RCMP_Gubun = "D";                //입고완료 상태면 삭제만 가능
            }

            // 체크 리스트 Grid
            DataTable ReceiptCheckMaterial2_P_ED = receiptCheckMaterial2Service.Select(model);
            // 입고 검수 정보 불러오기
            DataTable dt = receiptCheckMaterial2Service.SelectList_Load(model);

            /* 팝업 */
            DataTable RCMP_Worker = receiptCheckMaterial2Service.workerPopup("CODEHELP"); // 점검자 팝업
            DataTable RCMP_Checker = receiptCheckMaterial2Service.workerPopup("CODEHELP"); // 확인자 팝업

            // Select Box
            DataTable s_receipt_check_state = selectBoxService.GetSelectBox("공통코드", "D", "WR104", "s_receipt_check_state");   // 검수결과
            DataTable s_purchase_status = selectBoxService.GetSelectBox("공통코드", "D", "WR101", "s_purchase_status");   // 입고구분

            ViewBag.sp_receipt_check_state = s_receipt_check_state;
            ViewBag.sp_purchase_status = s_purchase_status;

            ViewBag.RCMP_Worker = Json(Public_Function.DataTableToJSON(RCMP_Worker));
            ViewBag.RCMP_Checker = Json(Public_Function.DataTableToJSON(RCMP_Checker));

            ViewBag.ReceiptCheckMaterial2_PData = Json(Public_Function.DataTableToJSON(dt));

            return View("ReceiptCheckMaterial2_P", Json(Public_Function.DataTableToJSON(ReceiptCheckMaterial2_P_ED)));
        }

        #endregion

        #region 원료 입고 관리

        [CheckSession]
        [HttpGet]
        public ActionResult ReceiveMaterial_M()
        {
            /* 입고 정보 그리드 조회 */
            ReceiveMaterial model = new ReceiveMaterial();

            model.start_date = DateTime.Today.AddDays(-7).ToShortDateString(); // (검색) 시작일
            model.end_date = DateTime.Today.ToShortDateString(); // (검색) 종료일
            model.s_item_cd = ""; // (검색) 품목코드
            model.lot_cust = ""; // (검색) 시험번호
            model.s_receipt_no = ""; // (검색) 입고번호
            model.Material_Or_PackMaterial = "A"; // 원료 입고 : A, 원료 재고 : B , 자재 입고 : C, 자재 재고 : D, 제조제품 원료 입고 : E 
            model.ReceiptType = "%"; 
            model.item_gb = "%"; // 원자재구분
            model.search_date = "0"; // (검색) 품목명/품목코드
            model.option2 = "4";     //원료 3 , 자재 2 , 상품 4        => Material_Or_PackMaterial 값으로 원료value를 가지고 온다.

            DataTable dt = receiveMaterialService.Select(model);

            /* 팝업 셋팅 */
            DataTable itemCD = receiveMaterialService.ItemCDPopup("CODEHELP"); // 원료 품목 팝업
            DataTable manufacture = receiveMaterialService.ManufacturePopup("CODEHELP"); // 제조처
            DataTable supply = receiveMaterialService.SupplyPopup("CODEHELP"); // 공급처

            /* 드랍박스 셋팅 */
            DataTable s_obtain_status = selectBoxService.GetSelectBox("공통코드", "D", "WR200", "s_receipt_check_state"); // 조달구분
            DataTable s_receipt_status = selectBoxService.GetSelectBox("공통코드", "D", "WR002", "s_receipt_check_state"); // 입고구분

            ViewBag.ReceiveMaterial_MData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ReceiveMaterial_MAuth = Json(HttpContext.Session["ReceiveMaterial_MAuth"]);

            ViewBag.RMM_itemCD = Json(Public_Function.DataTableToJSON(itemCD));
            ViewBag.RMM_manufacture = Json(Public_Function.DataTableToJSON(manufacture));
            ViewBag.RMM_supply = Json(Public_Function.DataTableToJSON(supply));

            ViewBag.s_obtain_status = s_obtain_status;
            ViewBag.s_receipt_status = s_receipt_status;

            return View();
        }

        /// <summary>
        /// 오른쪽 폼 / 그리드 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiveMaterial_M_Select(ReceiveMaterial model)
        {
            // *** 프로시져 Gubun 값 PQ 는 보류 (분석 - 미사용) ***

            /* 아래 그리드 */
            DataTable dt = receiveMaterialService.Select_List(model);

            /* 위 폼 데이터 */
            DataTable S2 = receiveMaterialService.Select_S2(model);

            /* 창고 정보 조회 */
            DataTable SW = receiveMaterialService.Select_SW(model);

            string[] JsonDataStr = new string[3];

            JsonDataStr[0] = Public_Function.DataTableToJSON(dt);
            JsonDataStr[1] = Public_Function.DataTableToJSON(S2);
            JsonDataStr[2] = Public_Function.DataTableToJSON(SW);

            return Json(JsonDataStr);
        }

        /// <summary>
        /// 왼쪽 그리드 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiveMaterial_M_Search(ReceiveMaterial model)
        {
            model.lot_cust = ""; // (검색) 시험번호
            model.s_receipt_no = ""; // (검색) 입고번호
            model.Material_Or_PackMaterial = "A"; // 원료 입고 : A, 원료 재고 : B , 자재 입고 : C, 자재 재고 : D, 제조제품 원료 입고 : E 
            model.ReceiptType = "%";
            model.item_gb = "%"; // 원자재구분
            model.search_date = "0"; // (검색) 품목명/품목코드
            model.option2 = "4";     //원료 3 , 자재 2 , 상품 4        => Material_Or_PackMaterial 값으로 원료value를 가지고 온다.

            DataTable dt = receiveMaterialService.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 입고번호 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiveMaterial_Receipt_No(ReceiveMaterial model)
        {
            DataTable dt = receiveMaterialService.S4(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }


        /// <summary>
        /// 원료 입고내용 확인
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        public ActionResult ReceiptCheckView_M(ReceiptCheckView model)
        {
            model.option = "3";             // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = "4";

            // 체크 리스트 Grid
            DataTable ReceiptCheckView_M = receiveMaterialService.ReceiptCheckView(model);            

            return View("ReceiptCheckView_M", Json(Public_Function.DataTableToJSON(ReceiptCheckView_M)));
        }

        /// <summary>
        /// 원료 입고내용 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiptCheckView_M_Search(ReceiptCheckView model)
        {
            model.option = "3";             // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = "4";

            DataTable dt = receiveMaterialService.ReceiptCheckView_Search(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        public JsonResult ReceiptCheckView_C1(ReceiptCheckView model)
        {
            model.option = "3";             // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = "4";

            DataTable dt = receiveMaterialService.ReceiptCheckView_Search(model);

            return Json(dt);
        }

        /// <summary>
        /// 입고검수 -> 입고취소
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult receiptStatus_C1(ReceiveMaterial model)
        {
            DataTable dt = receiveMaterialService.receiptStatus_C1(model);
            
            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 입고취소 -> 입고검수
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult receiptStatus_C3(ReceiveMaterial model)
        {
            DataTable dt = receiveMaterialService.receiptStatus_C3(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 시험의뢰
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult receiptStatus_ES(ReceiveMaterial model)
        {
            string res = receiveMaterialService.receiptStatus_ES(model);

            return Json("{ \"message\": \"" + res + "\" }");
        }

        /// <summary>
        /// 시험의뢰 취소-1
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult receiptStatus_DE(ReceiveMaterial model)
        {
            string res = receiveMaterialService.receiptStatus_DE(model);

            return Json("{ \"message\": \"" + res + "\" }");
        }

        /// <summary>
        /// 시험의뢰 취소-2
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult receiptStatus_C2(ReceiveMaterial model)
        {
            string res = receiveMaterialService.receiptStatus_C2(model);

            return Json("{ \"message\": \"" + res + "\" }");
        }

        /// <summary>
        /// 입고취소 -> 입고검수
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult receiptStatus_SA(ReceiveMaterial model)
        {
            DataTable dt = receiveMaterialService.receiptStatus_SA(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 품목에 저장된 제조처인지 확인
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult receiptStatus_SM(string item_cd, string cust_cd)
        {
            DataTable dt = receiveMaterialService.receiptStatus_SM(item_cd, cust_cd);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 입력, 수정, 삭제 기능
        /// </summary>
        [CheckSession]
        [HttpPost]
        public JsonResult receiptStatus_CRUD(ReceiveMaterial model)
        {
            if (!model.gubun.Equals("AllPackDelete") && !model.gubun.Equals("D1"))
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Ok = false });
                }
            }

            string res = receiveMaterialService.CRUD(model);

            var resJson = "{ \"message\": \"" + res + "\" }";

            return Json(resJson);

        }

        /// <summary>
        /// 입력, 수정, 삭제 기능
        /// </summary>
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiveMaterial_batch([JsonBinder] List<ReceiveMaterial> receiveMaterial, string gubun)
        {

            for (int i = 0; i < receiveMaterial.Count; i++)
            {
                if (gubun.Equals("U2"))
                {
                    receiveMaterial[i].gubun = "U2";
                }
            }

            string res = receiveMaterialService.ReceiveMaterial_batch(receiveMaterial, gubun);
            
            var resJson = "{ \"message\": \"" + res + "\" }";

            return Json(resJson);
        }   


        #endregion

        #region 자재 입고 관리
        [CheckSession]
        [HttpGet]
        public ActionResult ReceiveMaterial_P()
        {
            /* 입고 정보 그리드 조회 */
            ReceiveMaterial model = new ReceiveMaterial();

            model.start_date = DateTime.Today.AddDays(-7).ToShortDateString(); // (검색) 시작일
            model.end_date = DateTime.Today.ToShortDateString(); // (검색) 종료일
            model.s_item_cd = ""; // (검색) 품목코드
            model.lot_cust = ""; // (검색) 시험번호
            model.s_receipt_no = ""; // (검색) 입고번호
            model.Material_Or_PackMaterial = "C"; // 원료 입고 : A, 원료 재고 : B , 자재 입고 : C, 자재 재고 : D, 제조제품 원료 입고 : E 
            model.ReceiptType = "%";
            model.item_gb = "%"; // 원자재구분
            model.search_date = "0"; // (검색) 품목명/품목코드
            model.option2 = "";     //원료 3 , 자재 2 , 상품 4        => Material_Or_PackMaterial 값으로 자재value를 가지고 온다.

            DataTable dt = receiveMaterialService.Select(model);

            /* 팝업 셋팅 */
            DataTable ItemCD = receiveMaterialService.ItemPCDPopup("CODEHELP"); // 자재 품목 팝업
            DataTable manufacture = receiveMaterialService.ManufacturePPopup("CODEHELP"); // 제조처
            DataTable supply = receiveMaterialService.SupplyPPopup("CODEHELP"); // 공급처

            /* 드랍박스 셋팅 */
            DataTable s_obtain_status = selectBoxService.GetSelectBox("공통코드", "D", "WR200", "s_receipt_check_state"); // 조달구분
            DataTable s_receipt_status = selectBoxService.GetSelectBox("공통코드", "D", "WR002", "s_receipt_check_state"); // 입고구분

            ViewBag.ReceiveMaterial_PData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ReceiveMaterial_PAuth = Json(HttpContext.Session["ReceiveMaterial_PAuth"]);

            ViewBag.RMP_itemCD = Json(Public_Function.DataTableToJSON(ItemCD));
            ViewBag.RMP_manufacture = Json(Public_Function.DataTableToJSON(manufacture));
            ViewBag.RMP_supply = Json(Public_Function.DataTableToJSON(supply));

            ViewBag.s_obtain_status = s_obtain_status;
            ViewBag.s_receipt_status = s_receipt_status;

            return View();
        }

        /// <summary>
        /// 오른쪽 폼 / 그리드 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiveMaterial_P_Select(ReceiveMaterial model)
        {
            // *** 프로시져 Gubun 값 PQ 는 보류 (분석 - 미사용) ***

            /* 아래 그리드 */
            DataTable dt = receiveMaterialService.Select_List(model);

            /* 위 폼 데이터 */
            DataTable S2 = receiveMaterialService.Select_S2(model);

            /* 창고 정보 조회 */
            DataTable SW = receiveMaterialService.Select_SW(model);

            string[] JsonDataStr = new string[3];

            JsonDataStr[0] = Public_Function.DataTableToJSON(dt);
            JsonDataStr[1] = Public_Function.DataTableToJSON(S2);
            JsonDataStr[2] = Public_Function.DataTableToJSON(SW);

            return Json(JsonDataStr);
        }

        /// <summary>
        /// 왼쪽 그리드 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiveMaterial_P_Search(ReceiveMaterial model)
        {
            model.lot_cust = ""; // (검색) 시험번호
            model.s_receipt_no = ""; // (검색) 입고번호
            model.Material_Or_PackMaterial = "C"; // 원료 입고 : A, 원료 재고 : B , 자재 입고 : C, 자재 재고 : D, 제조제품 원료 입고 : E 
            model.ReceiptType = "%";
            model.item_gb = "%"; // 원자재구분
            model.search_date = "0"; // (검색) 품목명/품목코드
            model.option2 = "";     //원료 3 , 자재 2 , 상품 4        => Material_Or_PackMaterial 값으로 자재value를 가지고 온다.

            DataTable dt = receiveMaterialService.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 자재 입고내용 확인
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        public ActionResult ReceiptCheckView_P(ReceiptCheckView model)
        {
            model.option = "2";             // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = "";

            // 체크 리스트 Grid
            DataTable ReceiptCheckView_P = receiveMaterialService.ReceiptCheckView(model);
            // 발주상태 select Box
            //DataTable purchase_status = selectBoxService.GetSelectBox("공통코드", "D", "WR100", "purchase_status");

            //ViewBag.purchase_status = purchase_status;

            return View("ReceiptCheckView_P", Json(Public_Function.DataTableToJSON(ReceiptCheckView_P)));
        }

        /// <summary>
        /// 자재 입고내용 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ReceiptCheckView_P_Search(ReceiptCheckView model)
        {
            model.option = "2";             // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            model.option2 = "";

            DataTable dt = receiveMaterialService.ReceiptCheckView_Search(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 품목 실힘필요여부 확인
        /// </summary>        
        /// <returns></returns>        
        [HttpPost]
        public JsonResult ReceiveMaterial_ST(ReceiveMaterial model)
        {            
            string res = receiveMaterialService.ReceiveMaterial_ST(model);

            return Json(res);
        }

        /// <summary>
        /// 자재 단위 조회
        /// </summary>        
        /// <returns></returns>    
        [HttpPost]
        public JsonResult ReceiveMaterial_GetItemUnit(string item_cd)
        {

            DataTable dt = receiveMaterialService.ReceiveMaterial_GetItemUnit(item_cd);

            return Json(Public_Function.DataTableToJSON(dt));

        }


        #endregion

    }
}
