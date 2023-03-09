using HACCP.Libs.Database;
using HACCP.Models.ManufactureWaterMng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ManufactureWaterMng
{
    public class WaterTestScheduleService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_WaterTestSchedule";

        internal DataTable WaterTestScheduleSelect(WaterTestSchedule waterTestSchedule)
        {
            string[] pParam = new string[4];
            pParam[0] = "@test_type:" + waterTestSchedule.test_type;
            pParam[1] = "@water_type:" + waterTestSchedule.water_type;
            pParam[2] = "@s_year:" + waterTestSchedule.year;
            pParam[3] = "@s_month:" + waterTestSchedule.month;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string WaterTestScheduleDelete(WaterTestSchedule waterTestSchedule)
        {
            string calendar_date = Convert.ToDateTime(waterTestSchedule.calendar_date).ToString();

            string[] pParam = new string[4];
            pParam[0] = "@test_type:" + waterTestSchedule.test_type;
            pParam[1] = "@water_type:" + waterTestSchedule.water_type;
            pParam[2] = "@calendar_date:" + calendar_date;
            pParam[3] = "@testrequest_no:" + waterTestSchedule.testrequest_no;

            string res = _bllSpExecute.SpExecuteString(sSpName, "D", pParam);

            return res;
        }

        internal string WaterTestScheduleTestRequest(WaterTestSchedule waterTestSchedule)
        {
            string calendar_date = Convert.ToDateTime(waterTestSchedule.calendar_date).ToString();


            string[] pParam = new string[4];
            pParam[0] = "@test_type:" + waterTestSchedule.test_type;
            pParam[1] = "@water_type:" + waterTestSchedule.water_type;
            pParam[2] = "@calendar_date:" + calendar_date;
            pParam[3] = "@emp_cd:" + HttpContext.Current.Session["loginCD"];

            string res = _bllSpExecute.SpExecuteString(sSpName, "R", pParam);

            return res;
        }
    }
}