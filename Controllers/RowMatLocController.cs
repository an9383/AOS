using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevExpress.CodeParser;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models.RowMatLoc;
using HACCP.Services.Comm;
using HACCP.Services.LocMng;
using HACCP.Services.RowMatLoc;
using log4net;


namespace HACCP.Controllers
{
    public class RowMatLocController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RowMatLocController));
        private StackSearchService stackSearchService = new StackSearchService(); // 원자재 적치 현황
        private StackManageService stackManageService = new StackManageService(); // 원자재 적치 관리
        private ZoneManageService zoneManageService = new ZoneManageService(); //구역 서비스
        private CellManageService cellManageService = new CellManageService(); //셀 서비스
        private OrderMaterialLocationService orderMaterialLocationService = new OrderMaterialLocationService(); // 원료 불출지시
        private OrderPackLocationService orderPackLocationService = new OrderPackLocationService(); // 자재 불출지시 서비스
        private OrderMaterialSumService orderMaterialSumService = new OrderMaterialSumService(); // 원료 불출합계 서비스
        private CellStackStatusService cellStackStatusService = new CellStackStatusService(); // 원자재 셀 적치 현황 서비스
        private SelectBoxService selectBoxService = new SelectBoxService();

        #region 원료/자재 적치 현황

        /// <summary>
        /// 원료 적치 현황 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("RowMatLoc/StackSearch_M")]
        [HttpGet]
        public ActionResult StackSearch_M(StackSearch sModel)
        {
            sModel.item_gb = "3"; // 원료 3, 자재 2
            DataTable dt = stackSearchService.Select(sModel);

            ViewBag.StackSearch_MData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.StackSearch_MAuth = Json(HttpContext.Session["StackSearch_MAuth"]);

            return View();
        }

        /// <summary>
        /// 원자재 시험번호 별 재고량
        /// (체인지) 좌측 그리드 -> 우측 위 그리드 출력
        /// </summary>
        /// <param name="sModel"></param>
        /// <param name="item_cd"></param>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult StackSearch_Test_Select(StackSearch sModel, string item_cd)
        {
            DataTable dt = stackSearchService.StackSearch_Test_Select(sModel, item_cd);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 원자재 시험번호 별 재고량
        /// (체인지) 좌측 그리드 -> 우측 위 그리드 출력
        /// </summary>
        /// <param name="sModel"></param>
        /// <param name="item_cd"></param>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult StackSearch_ReceiptPack_Select(StackSearch sModel, string receipt_no, string receipt_id)
        {
            DataTable dt = stackSearchService.StackSearch_ReceiptPack_Select(sModel, receipt_no, receipt_id);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 자재 적치 현황 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("RowMatLoc/StackSearch_P")]
        [HttpGet]
        public ActionResult StackSearch_P(StackSearch sModel)
        {
            sModel.item_gb = "2"; // 원료 3, 자재 2
            DataTable dt = stackSearchService.Select(sModel);

            ViewBag.StackSearch_PData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.StackSearch_PAuth = Json(HttpContext.Session["StackSearch_PAuth"]);

            return View();
        }

        #endregion

        #region 원자재 적치 관리

        /// <summary>
        /// 원료 적치 관리 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("RowMatLoc/StackManage_M")]
        [HttpGet]
        public ActionResult StackManage_M(StackManage sModel)
        {
            /* 그리드 조회 */
            sModel.item_gb = "3"; // 원료 3, 자재 2
            DataTable dt = stackManageService.Select(sModel);

            /* 창고 드랍 박스 */
            DataTable WorkroomData_SM = stackManageService.SelectWorkroom(sModel);                        
            
            ViewBag.WorkroomData_SM = Json(Public_Function.DataTableToJSON(WorkroomData_SM));
            ViewBag.StackManage_MData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.StackManage_MAuth = Json(HttpContext.Session["StackManage_MAuth"]);

            return View();
        }

        /// <summary>
        /// 원료 적치 관리 재조회
        /// </summary>
        [CheckSession]
        [HttpPost]
        public JsonResult StackManage_MSelect(StackManage sModel)
        {
            /* 그리드 조회 */
            sModel.item_gb = "3"; // 원료 3, 자재 2
            DataTable dt = stackManageService.Select(sModel);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 원료 적치 관리 우측 그리드
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult StackManage_Loc_Select(StackManage sModel, string receipt_no, string receipt_id)
        {
            /* 그리드 조회 */
            DataTable dt = stackManageService.StackManage_Loc_Select(sModel, receipt_no, receipt_id);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// WorkRoom(창고)에 해당하는 Zone(구역)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult StackManage_Zone_Select(string workroom_cd)
        {
            /* 그리드 조회 */
            DataTable dt = stackManageService.StackManage_Zone_Select(workroom_cd);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// Zone(구역)에 해당하는 Cell
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult StackManage_Cell_Select(string zone_cd)
        {
            /* 그리드 조회 */
            DataTable dt = stackManageService.StackManage_Cell_Select(zone_cd);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 저장
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult StackManageCRUD(StackManage sModel, string[] selectedKeys, string workroom_cd, string zone_cd, string cell_cd)
        {
            string[] result = selectedKeys[0].Split(',');

            sModel.workroom_cd = workroom_cd;
            sModel.zone_cd = zone_cd;
            sModel.cell_cd = cell_cd;

            string res = stackManageService.StackManageCRUD(sModel, result);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        /// <summary>
        /// 자재 적치 관리 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("RowMatLoc/StackManage_P")]
        [HttpGet]
        public ActionResult StackManage_P(StackManage sModel)
        {
            /* 그리드 조회 */
            sModel.item_gb = "2"; // 원료 3, 자재 2
            DataTable dt = stackManageService.Select(sModel);

            /* 창고 드랍 박스 */
            DataTable WorkroomData_SP = stackManageService.SelectWorkroom(sModel);

            ViewBag.WorkroomData_SP = Json(Public_Function.DataTableToJSON(WorkroomData_SP));
            ViewBag.StackManage_PData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.StackManage_PAuth = Json(HttpContext.Session["StackManage_PAuth"]);

            return View();
        }

        #endregion

        #region 원료 불출지시
        /// <summary>
        /// 원료 불출지시 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("RowMatLoc/OrderMaterialLocation")]
        [HttpGet]
        public ActionResult OrderMaterialLocation(OrderMaterialLocation model)
        {
            /* 그리드 조회 */
            model.s_sdate = DateTime.Today.AddDays(-30).ToShortDateString();
            model.s_edate = DateTime.Today.ToShortDateString();
            model.input_order_status = "%";

            DataTable dt = orderMaterialLocationService.Select(model);

            /* 제조상태 드랍박스 */
            DataTable s_out_status = selectBoxService.GetSelectBox("공통코드", "S", "RT005", "s_out_status");

            ViewBag.OrderMaterialLocationData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.OrderMaterialLocationAuth = Json(HttpContext.Session["OrderMaterialLocationAuth"]);

            ViewBag.s_out_status = s_out_status;

            return View();
        }

        /// <summary>
        /// 원료 불출지시 아래 그리드
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult OrderList_Select(OrderMaterialLocation model)
        {
            /* 그리드 조회 */
            model.s_sdate = DateTime.Today.AddDays(-30).ToShortDateString();
            model.s_edate = DateTime.Today.ToShortDateString();

            DataTable dt = orderMaterialLocationService.OrderList_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult OrderMaterialLocation_Search(OrderMaterialLocation model)
        {
            //제조 상태
            model.input_order_status = model.s_out_status;

            DataTable dt = orderMaterialLocationService.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion

        #region 자재 불출지시
        /// <summary>
        /// 자재 불출지시 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("RowMatLoc/OrderPackLocation")]
        [HttpGet]
        public ActionResult OrderPackLocation(OrderPackLocation model)
        {
            /* 그리드 조회 */
            model.s_sdate = DateTime.Today.AddDays(-30).ToShortDateString();
            model.s_edate = DateTime.Today.ToShortDateString();
            model.material_status = "%";

            DataTable dt = orderPackLocationService.Select(model);
            
            /* 제조상태 드랍박스 */
            DataTable s_out_status = selectBoxService.GetSelectBox("공통코드", "S", "PA001", "s_out_status");

            ViewBag.OrderPackLocationData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.OrderPackLocationAuth = Json(HttpContext.Session["OrderPackLocationAuth"]);

            ViewBag.s_out_status = s_out_status;

            return View();
        }

        /// <summary>
        /// 자재 불출지시 아래 그리드
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult OrderPackList_Select(OrderPackLocation model)
        {
            /* 그리드 조회 */
            model.s_sdate = DateTime.Today.AddDays(-30).ToShortDateString();
            model.s_edate = DateTime.Today.ToShortDateString();

            DataTable dt = orderPackLocationService.OrderPackList_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult OrderPackLocation_Search(OrderPackLocation model)
        {
            //제조 상태
            model.material_status = model.s_out_status;

            DataTable dt = orderPackLocationService.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion

        #region 원료 불출합계
        /// <summary>
        /// 원료 불출합계 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("RowMatLoc/OrderMaterialSum")]
        [HttpGet]
        public ActionResult OrderMaterialSum(OrderMaterialSum model)
        {
            /* 그리드 조회 */
            model.s_sdate = DateTime.Today.AddDays(-30).ToShortDateString();
            model.s_edate = DateTime.Today.ToShortDateString();
            model.input_order_status = "%";

            DataTable dt = orderMaterialSumService.Select(model);

            /* 제조상태 드랍박스 */
            DataTable s_out_status = selectBoxService.GetSelectBox("공통코드", "S", "RT005", "s_out_status");

            ViewBag.OrderMaterialSumData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.OrderMaterialSumAuth = Json(HttpContext.Session["OrderMaterialSumAuth"]);

            ViewBag.s_out_status = s_out_status;

            return View();
        }

        /// <summary>
        /// 불출합계 이벤트
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult OrderMaterialOutSum(OrderMaterialSum model, string[] selectedKeys)
        {
            // 선택 된 값들 배열 선언
            string[] result = selectedKeys[0].Split(',');

            DataTable dt = orderMaterialSumService.OrderMaterialOutSum(model, result);
            
            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult OrderMaterialSum_Search(OrderMaterialSum model)
        {
            //제조 상태
            model.input_order_status = model.s_out_status;

            DataTable dt = orderMaterialSumService.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion

        #region 원자재 셀 적치 현황
        /// <summary>
        /// 원자재 셀 적치 현황 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("RowMatLoc/CellStackStatus")]
        [HttpGet]
        public ActionResult CellStackStatus(CellStackStatus model)
        {
            /* 그리드 조회 */
            model.zone_cd = "";
            model.cell_isle = "";

            DataTable dt = cellStackStatusService.Select(model);

            /* 창고 드랍 박스 */
            DataTable WorkroomData_CSS = cellStackStatusService.SelectWorkroom(model);

            ViewBag.WorkroomData_CSS = Json(Public_Function.DataTableToJSON(WorkroomData_CSS));
            ViewBag.CellStackStatusData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.CellStackStatusAuth = Json(HttpContext.Session["CellStackStatusAuth"]);

            return View();
        }


        /// <summary>
        /// WorkRoom(창고)에 해당하는 Zone(구역)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult CellStackStatus_Zone_Select(string workroom_cd)
        {
            /* 그리드 조회 */
            DataTable dt = cellStackStatusService.Zone_Select(workroom_cd);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// Zone(구역)에 해당하는 Cell
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult CellStackStatus_Cell_Select(string zone_cd)
        {
            /* 그리드 조회 */
            DataTable dt = cellStackStatusService.Cell_Select(zone_cd);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 랙 높이
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult MAX_CELL_HEIGHT(CellStackStatus model)
        {
            /* 그리드 조회 */
            DataTable dt = cellStackStatusService.MAX_CELL_HEIGHT(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult CellStackStatus_Search(CellStackStatus model)
        {
            DataTable dt = cellStackStatusService.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 셀 선택 팝업 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult CellStackStatus_Popup_Select(CellStackStatus model, string cell_column, string cell_height)
        {
            model.cell_column = cell_column;
            model.cell_height = cell_height;

            DataTable dt = cellStackStatusService.Popup_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion

    }
}