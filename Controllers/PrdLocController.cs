using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevExpress.CodeParser;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models.PrdLoc;
using HACCP.Services.Comm;
using HACCP.Services.PrdLoc;

using log4net;

namespace HACCP.Controllers
{
    public class PrdLocController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PrdLocController));
        private ItemSearchService itemSearchService = new ItemSearchService();
        private ItemManageService ItemManageService = new ItemManageService();
        private LocationItemSelectService LocationItemSelectService = new LocationItemSelectService();
        private CellStackStatus_IService CellStackStatus_IService = new CellStackStatus_IService();

        #region 완제품 적치 현황
        /// <summary>
        /// 완제품 적치 현황 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("PrdLoc/ItemSearch")]
        [HttpGet]
        public ActionResult ItemSearch(ItemSearch sModel)
        {
            DataTable dt = itemSearchService.Select(sModel);

            ViewBag.ItemSearchData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ItemSearchAuth = Json(HttpContext.Session["ItemSearchAuth"]);

            return View();
        }

        /// <summary>
        /// 완제품 왼쪽 그리드 체인지 -> 오른쪽 위 그리드 조회
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ItemSearch_S2(ItemSearch model)
        {
            DataTable dt = itemSearchService.ItemSearch_S2(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 완제품 오른쪽 위 그리드 체인지 -> 오른쪽 아래 그리드 조회
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ItemSearch_S3(ItemSearch model)
        {
            DataTable dt = itemSearchService.ItemSearch_S3(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion

        #region 완제품 적치 등록
        /// <summary>
        /// 완제품 적치 등록 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("PrdLoc/ItemManage")]
        [HttpGet]
        public ActionResult ItemManage(ItemManage model)
        {
            DataTable dt = ItemManageService.Select(model);

            /* 창고 드랍 박스 */
            DataTable WorkroomData = ItemManageService.SelectWorkroom(model);

            ViewBag.WorkroomData = Json(Public_Function.DataTableToJSON(WorkroomData));
            ViewBag.ItemSearchData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.ItemSearchAuth = Json(HttpContext.Session["ItemManageAuth"]);

            return View();
        }


        /// <summary>
        /// WorkRoom(창고)에 해당하는 Zone(구역)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ItemManage_Zone_Select(ItemManage model)
        {
            /* 그리드 조회 */
            DataTable dt = ItemManageService.ItemManage_Zone_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// Zone(구역)에 해당하는 Cell
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ItemManage_Cell_Select(ItemManage model)
        {
            /* 그리드 조회 */
            DataTable dt = ItemManageService.ItemManage_Cell_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 오른쪽 그리드 조회
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ItemManage_Loc_Select(ItemManage model)
        {
            /* 그리드 조회 */
            DataTable dt = ItemManageService.ItemManage_Loc_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 저장
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult ItemManageCRUD(ItemManage model, string[] selectedKeys)
        {
            string[] result = selectedKeys[0].Split(',');

            string res = ItemManageService.ItemManageCRUD(model, result);
            
            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }

        #endregion

        #region 완제품 위치 조회

        /// <summary>
        /// 완제품 위치 조회 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("PrdLoc/LocationItemSelect")]
        [HttpGet]
        public ActionResult LocationItemSelect(LocationItemSelect model)
        {
            DataTable dt = LocationItemSelectService.Select(model);

            /* 창고 드랍 박스 */
            DataTable WorkroomData = LocationItemSelectService.SelectWorkroom(model);

            ViewBag.WorkroomData = Json(Public_Function.DataTableToJSON(WorkroomData));
            ViewBag.LocationItemSelectData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.LocationItemSelectAuth = Json(HttpContext.Session["LocationItemSelectAuth"]);

            return View();
        }

        /// <summary>
        /// WorkRoom(창고)에 해당하는 Zone(구역)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult LocationItemSelect_Zone_Select(LocationItemSelect model)
        {
            /* 그리드 조회 */
            DataTable dt = LocationItemSelectService.Zone_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// Zone(구역)에 해당하는 Cell
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult LocationItemSelect_Cell_Select(LocationItemSelect model)
        {
            /* 그리드 조회 */
            DataTable dt = LocationItemSelectService.Cell_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult LocationItemSelectSearch(LocationItemSelect model)
        {
            /* 그리드 조회 */
            DataTable dt = LocationItemSelectService.SelectSearch(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion

        #region 완제품 셀 적치 현황

        /// <summary>
        /// 완제품 셀 적치 현황 (첫 페이지 진입)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [Route("PrdLoc/CellStackStatus_I")]
        [HttpGet]
        public ActionResult CellStackStatus_I(CellStackStatus_I model)
        {
            /* 그리드 조회 */
            model.zone_cd = "";
            model.cell_isle = "";

            DataTable dt = CellStackStatus_IService.Select(model);

            ///* 창고 드랍 박스 */
            DataTable WorkroomData = CellStackStatus_IService.SelectWorkroom(model);

            ViewBag.WorkroomData_CSI = Json(Public_Function.DataTableToJSON(WorkroomData));
            ViewBag.CellStackStatus_IData = Json(Public_Function.DataTableToJSON(dt));
            ViewBag.CellStackStatus_IAuth = Json(HttpContext.Session["CellStackStatus_IAuth"]);

            return View();
        }

        /// <summary>
        /// WorkRoom(창고)에 해당하는 Zone(구역)
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult CellStackStatus_Zone_Select(CellStackStatus_I model)
        {
            /* 그리드 조회 */
            DataTable dt = CellStackStatus_IService.Zone_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// Zone(구역)에 해당하는 Cell
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult CellStackStatus_Cell_Select(CellStackStatus_I model)
        {
            /* 그리드 조회 */
            DataTable dt = CellStackStatus_IService.Cell_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 랙 높이
        /// </summary>
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult MAX_CELL_HEIGHT(CellStackStatus_I model)
        {
            /* 그리드 조회 */
            DataTable dt = CellStackStatus_IService.MAX_CELL_HEIGHT(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult CellStackStatus_Search(CellStackStatus_I model)
        {
            DataTable dt = CellStackStatus_IService.Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        /// <summary>
        /// 셀 선택 팝업 조회
        /// </summary>        
        /// <returns></returns>
        [CheckSession]
        [HttpPost]
        public JsonResult CellStackStatus_Popup_Select(CellStackStatus_I model)
        {
            DataTable dt = CellStackStatus_IService.Popup_Select(model);

            return Json(Public_Function.DataTableToJSON(dt));
        }

        #endregion
    }
}