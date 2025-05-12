using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraSpellChecker.Parser;
using HACCP.Filter;
using HACCP.Libs;
using HACCP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using HACCP.Models.LocMng;
using System.Web.Mvc;
using HACCP.Services.LocMng;
using HACCP.Services.Comm;

namespace HACCP.Controllers
{
    public class LocMngController : Controller
    {
        private ZoneManageService zoneManageService = new ZoneManageService();
        private SelectBoxService selectBoxService = new SelectBoxService();
        private CellManageService cellManageService = new CellManageService();
        private CustomCustReg2Service customCustReg2Service = new CustomCustReg2Service();

        #region ZoneManage - 구역(Zone) 등록
        [CheckSession]
        [Route("LocMng/ZoneManage")]
        [HttpGet]
        public ActionResult ZoneManage(string workroom_cd)
        {
            DataTable ZoneManage = zoneManageService.Select(workroom_cd);            

            DataTable zone_type = selectBoxService.GetSelectBox("공통코드", "D", "WA011", "zone_type");
            DataTable zone_status = selectBoxService.GetSelectBox("공통코드", "D", "WA012", "zone_status");
            
            DataTable WorkRoomPopupData = zoneManageService.getWorkRoomPopupData(workroom_cd);


            ViewBag.ZoneManage = Json(Public_Function.DataTableToJSON(ZoneManage));
            ViewBag.WorkRoomPopupData = Json(Public_Function.DataTableToJSON(WorkRoomPopupData));

            ViewBag.ZoneManageAuth = Json(HttpContext.Session["ZoneManageAuth"]);
            ViewBag.zone_type = zone_type;
            ViewBag.zone_status = zone_status;
            

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ZoneManageSelect(string workroom_cd)
        {
            DataTable ZoneManage = zoneManageService.Select(workroom_cd);           

            return Json(Public_Function.DataTableToJSON(ZoneManage));
        }

        [HttpPost]
        public JsonResult getWorkRoomPopupData(string workroom_cd)
        {
            DataTable getWorkRoomPopupData = zoneManageService.getWorkRoomPopupData(workroom_cd);

            return Json(Public_Function.DataTableToJSON(getWorkRoomPopupData));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult ZoneManageCRUD(ZoneManage zModel, string gubun)
        {
            if (!gubun.Equals("Delete"))
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Ok = false });
                }
            }

            string res = zoneManageService.ZoneManageCRUD(zModel, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }
        #endregion ZoneManage

        #region CellManage - 셀(Cell) 등록
        [CheckSession]
        [Route("LocMng/CellManage")]
        [HttpGet]
        public ActionResult CellManage(string workroom_cd, string zone_cd, string cell_isle, string cell_height)
        {
            DataTable CellManage = cellManageService.Select(workroom_cd, zone_cd, cell_isle, cell_height);
            DataTable WorkroomData = cellManageService.SelectData(workroom_cd);
            

            DataTable cell_type = selectBoxService.GetSelectBox("공통코드", "D", "WA021", "cell_type");
            DataTable cell_status = selectBoxService.GetSelectBox("공통코드", "D", "WA022", "cell_type");

            ViewBag.CellManage = Json(Public_Function.DataTableToJSON(CellManage));
            ViewBag.WorkroomData = Json(Public_Function.DataTableToJSON(WorkroomData));

            ViewBag.CellManageAuth = Json(HttpContext.Session["CellManageAuth"]);
            ViewBag.cell_type = cell_type;
            ViewBag.cell_status = cell_status;

            return View();
        }

        [HttpPost]
        public JsonResult SelectData(string workroom_cd)
        {
            DataTable SelectData = cellManageService.SelectData(workroom_cd);

            return Json(Public_Function.DataTableToJSON(SelectData));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult CellManageSelect(string workroom_cd, string zone_cd, string cell_isle, string cell_height)
        {
            DataTable CellManage = cellManageService.Select(workroom_cd, zone_cd, cell_isle, cell_height);

            return Json(Public_Function.DataTableToJSON(CellManage));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult SelectTable(string workroom_cd)
        {
            DataTable ZonenmData = cellManageService.SelectTable(workroom_cd);

            return Json(Public_Function.DataTableToJSON(ZonenmData));
        }

        [HttpPost]
        public JsonResult SelectCell(string zone_cd)
        {
            DataTable CellData = cellManageService.SelectCell(zone_cd);

            return Json(Public_Function.DataTableToJSON(CellData));
        }

        [HttpPost]
        public JsonResult SelectHeight(string cell_isle, string zone_cd)
        {
            DataTable HeightData = cellManageService.SelectHeight(cell_isle, zone_cd);

            return Json(Public_Function.DataTableToJSON(HeightData));
        }
        
        [CheckSession]
        [HttpPost]
        public JsonResult CellManageCRUD(CellManage cModel, string gubun)
        {
            if (!gubun.Equals("Delete"))
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Ok = false });
                }
            }

            string res = cellManageService.CellManageCRUD(cModel, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }
        #endregion

        #region CustomCustReg2 - 간납처 등록
        [CheckSession]
        [Route("LocMng/CustomCustReg2")]
        [HttpGet]
        public ActionResult CustomCustReg2(string s_cust_cd)
        {
            DataTable CustomCustReg2 = customCustReg2Service.Select(s_cust_cd);
            DataTable custPopupList = customCustReg2Service.CustomCustReg2Popup();

            ViewBag.CustomCustReg2 = Json(Public_Function.DataTableToJSON(CustomCustReg2));
            ViewBag.custPopupList = Json(Public_Function.DataTableToJSON(custPopupList));
            ViewBag.CustomCustReg2Auth = Json(HttpContext.Session["CustomCustReg2Auth"]);

            return View();
        }

        [CheckSession]
        [HttpPost]
        public JsonResult CustomCustReg2Select(string s_cust_cd)
        {
            DataTable CustomCustReg2 = customCustReg2Service.Select(s_cust_cd);

            return Json(Public_Function.DataTableToJSON(CustomCustReg2));
        }

        [CheckSession]
        [HttpPost]
        public JsonResult CustomCustReg2CRUD(CustomCustReg2 cModel, string gubun)
        {
            if (!gubun.Equals("D"))
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Ok = false });
                }
            }

            string res = customCustReg2Service.CustomCustReg2CRUD(cModel, gubun);

            var resJson = "{ \"messege\": \"" + res + "\" }";

            return Json(resJson);
        }
        #endregion

    }
}
