using HACCP.Attribute;
using HACCP.Libs;
using HACCP.Models.ManufactureWaterMng;
using HACCP.Services.Comm;
using HACCP.Services.ManufactureWaterMng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class ManufactureWaterMngController : Controller
    {
        #region 제조용수 시험 스케줄 ManufactureWaterScheduleReg

        public ActionResult ManufactureWaterScheduleReg()
        {
            CodeHelpService codeHelpService = new CodeHelpService();

            DataTable itemWater = codeHelpService.GetCode(CodeHelpType.item_water, HttpContext.Session["plantCD"].ToString(), "");
            DataTable employee = codeHelpService.GetCode(CodeHelpType.employee, HttpContext.Session["plantCD"].ToString(), "");

            ViewBag.itemWater = Json(Public_Function.DataTableToJSON(itemWater));
            ViewBag.employee = Json(Public_Function.DataTableToJSON(employee));

            return View();
        }

        [HttpPost]
        public JsonResult ManufactureWaterScheduleRegSelect(ManufactureWaterSchedule.SrchParam param)
        {
            ManufactureWaterScheduleRegService manufactureWaterScheduleRegService = new ManufactureWaterScheduleRegService();

            DataTable dt = manufactureWaterScheduleRegService.ManufactureWaterScheduleRegSelect(param);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string ManufactureWaterScheduleRegTRX(ManufactureWaterSchedule manufactureWaterSchedule)
        {
            ManufactureWaterScheduleRegService manufactureWaterScheduleRegService = new ManufactureWaterScheduleRegService();

            string res = manufactureWaterScheduleRegService.ManufactureWaterScheduleRegTRX(manufactureWaterSchedule);

            return res;
        }

        [HttpGet]
        public JsonResult ManufactureWaterScheduleSelect(ManufactureWaterSchedule.SrchParam param)
        {
            ManufactureWaterScheduleRegService manufactureWaterScheduleRegService = new ManufactureWaterScheduleRegService();

            DataTable dt = manufactureWaterScheduleRegService.ManufactureWaterScheduleSelect(param);

            return Json(Public_Function.DataTableToJSON(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ManufactureWaterScheduleInput(ManufactureWaterSchedule.Schedule schedule)
        {
            ManufactureWaterScheduleRegService manufactureWaterScheduleRegService = new ManufactureWaterScheduleRegService();

            string res = manufactureWaterScheduleRegService.ManufactureWaterScheduleInput(schedule);

            return Json( new { message = res + "건이 입력 되었습니다." } );
        }

        [HttpPost]
        public ActionResult ManufactureWaterScheduleDelete([JsonBinder]List<ManufactureWaterSchedule.Schedule> schedules)
        {
            ManufactureWaterScheduleRegService manufactureWaterScheduleRegService = new ManufactureWaterScheduleRegService();

            string res = manufactureWaterScheduleRegService.ManufactureWaterScheduleDelete(schedules);

            return Json(new { message = res + "건이 삭제 되었습니다." });
        }

        [HttpPost]
        public ActionResult ManufactureWaterTestRequest([JsonBinder]List<ManufactureWaterSchedule.Schedule> schedules, string water_gb)
        {
            ManufactureWaterScheduleRegService manufactureWaterScheduleRegService = new ManufactureWaterScheduleRegService();

            string res = manufactureWaterScheduleRegService.ManufactureWaterTestRequest(schedules, water_gb);

            return Json(new { message = res + "건이 시험의뢰 되었습니다." });
        }

        #endregion


        #region 제조용수 시험 스케줄 승인 WaterTestScheduleConfirm

        public ActionResult WaterTestScheduleConfirm()
        {
            return View();
        }

        [HttpGet]
        public JsonResult WaterTestScheduleConfirmSelect(WaterTestScheduleConfirm.SrchParam param)
        {
            WaterTestScheduleConfirmService waterTestScheduleConfirmService = new WaterTestScheduleConfirmService();

            DataTable WaterTestScheduleConfirmDt = waterTestScheduleConfirmService.WaterTestScheduleConfirmSelect(param);

            return Json(Public_Function.DataTableToJSON(WaterTestScheduleConfirmDt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string WaterTestScheduleConfirmTRX(WaterTestScheduleConfirm waterTestSchedule)
        {
            WaterTestScheduleConfirmService waterTestScheduleConfirmService = new WaterTestScheduleConfirmService();

            string res = waterTestScheduleConfirmService.WaterTestScheduleConfirmTRX(waterTestSchedule);

            return res;
        }

        [HttpGet]
        public JsonResult WaterTestScheduleConfirmSelectDetail(WaterTestScheduleConfirm.SrchParam param)
        {
            WaterTestScheduleConfirmService waterTestScheduleConfirmService = new WaterTestScheduleConfirmService();

            DataTable WaterTestScheduleSignDt = waterTestScheduleConfirmService.WaterTestScheduleSignSelect(param.confirm_no);
            DataTable WaterTestScheduleMatchDt = waterTestScheduleConfirmService.WaterTestScheduleMatchSelect(param.confirm_no);
            DataTable WaterTestScheduleDt = waterTestScheduleConfirmService.WaterTestScheduleSelect(param);

            string[] jsonArr = new string[3];
            jsonArr[0] = Public_Function.DataTableToJSON(WaterTestScheduleSignDt);
            jsonArr[1] = Public_Function.DataTableToJSON(WaterTestScheduleMatchDt);
            jsonArr[2] = Public_Function.DataTableToJSON(WaterTestScheduleDt);

            return Json(jsonArr, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult WaterTestScheduleConfirmScheduleTRX([JsonBinder]List<WaterTestScheduleConfirm> waterTestSchedules
            , string gubun, string confirm_no, string start_date, string end_date)
        {
            WaterTestScheduleConfirmService waterTestScheduleConfirmService = new WaterTestScheduleConfirmService();

            string res = waterTestScheduleConfirmService.WaterTestScheduleTRX(waterTestSchedules, gubun, confirm_no, start_date, end_date);

            return Json( new { message = res });
        }

        [HttpPost]
        public ActionResult WaterTestScheduleConfirmSignTRX(WaterTestScheduleConfirm waterTestSchedule)
        {
            WaterTestScheduleConfirmService waterTestScheduleConfirmService = new WaterTestScheduleConfirmService();

            string res = waterTestScheduleConfirmService.WaterTestScheduleSignTRX(waterTestSchedule);

            return Json(new { message = res });
        }

        #endregion


        #region 제조용수 시험 의뢰 WaterTestSchedule

        public ActionResult WaterTestSchedule()
        {

            return View();
        }

        public JsonResult WaterTestScheduleSelect(WaterTestSchedule waterTestSchedule)
        {
            WaterTestScheduleService waterTestScheduleService = new WaterTestScheduleService();

            DataTable WaterTestScheduleDt = waterTestScheduleService.WaterTestScheduleSelect(waterTestSchedule);

            return Json(Public_Function.DataTableToJSON(WaterTestScheduleDt));
        }

        [HttpPost]
        public ActionResult WaterTestScheduleDelete(WaterTestSchedule waterTestSchedule)
        {
            WaterTestScheduleService waterTestScheduleService = new WaterTestScheduleService();

            string res = waterTestScheduleService.WaterTestScheduleDelete(waterTestSchedule);

            return Json(new { message = res });
        }

        [HttpPost]
        public ActionResult WaterTestScheduleTestRequest(WaterTestSchedule waterTestSchedule)
        {
            WaterTestScheduleService waterTestScheduleService = new WaterTestScheduleService();

            string res = waterTestScheduleService.WaterTestScheduleTestRequest(waterTestSchedule);

            return Json(new { message = res });
        }

        #endregion
    }
}