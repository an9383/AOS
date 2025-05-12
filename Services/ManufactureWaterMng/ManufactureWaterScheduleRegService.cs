using HACCP.Libs.Database;
using HACCP.Models.ManufactureWaterMng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ManufactureWaterMng
{
    public class ManufactureWaterScheduleRegService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_ManufactureWaterScheduleReg";

        internal DataTable ManufactureWaterScheduleRegSelect(ManufactureWaterSchedule.SrchParam param)
        {
            string[] pParam = new string[4];
            pParam[0] = "@s_sdate:" + param.s_sdate;
            pParam[1] = "@s_edate:" + param.s_edate;
            pParam[2] = "@s_water_gb:" + param.s_water_gb;
            pParam[3] = "@s_point:" + param.s_point_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string ManufactureWaterScheduleRegTRX(ManufactureWaterSchedule manufactureWaterSchedule)
        {
            string[] pParam = new string[6];
            pParam[0] = "@water_gb:" + manufactureWaterSchedule.water_gb;
            pParam[1] = "@point_cd:" + manufactureWaterSchedule.point_cd;
            pParam[2] = "@period_qty:" + manufactureWaterSchedule.period_qty;
            pParam[3] = "@period_unit:" + manufactureWaterSchedule.period_unit;
            pParam[4] = "@charge_cd:" + manufactureWaterSchedule.charge_cd;
            pParam[5] = "@start_date:" + manufactureWaterSchedule.start_date;

            string res = _bllSpExecute.SpExecuteString(sSpName, manufactureWaterSchedule.gubun, pParam);

            return res;
        }

        internal DataTable ManufactureWaterScheduleSelect(ManufactureWaterSchedule.SrchParam param)
        {
            string[] pParam = new string[4];
            pParam[0] = "@schedule_date_start:" + param.s_sdate;
            pParam[1] = "@schedule_date_end:" + param.s_edate;
            pParam[2] = "@water_gb:" + param.s_water_gb;
            pParam[3] = "@point_cd:" + param.s_point_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SelectSchedule", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string ManufactureWaterScheduleInput(ManufactureWaterSchedule.Schedule schedule)
        {
            string[] pParam = new string[20];

            int repeat_days = 0;

            int? length = schedule.repeat_days?.Length;

            if (length > 0)
            {
                for (int i = 0; i < schedule.repeat_days.Length; i++)
                {
                    repeat_days += int.Parse(schedule.repeat_days[i]);
                }
            }

            pParam[0] = "@schedule_master_id:" + schedule.schedule_master_id;
            pParam[1] = "@obj_type:" + schedule.obj_type;
            pParam[2] = "@obj_cd:" + schedule.obj_cd;
            pParam[3] = "@work_type:" + schedule.work_type;
            pParam[4] = "@work_item:" + schedule.work_item;
            pParam[5] = "@frequency:" + schedule.frequency;
            pParam[6] = "@schedule_period:" + schedule.schedule_period;
            pParam[7] = "@period_type:" + schedule.period_type;
            pParam[8] = "@repeat_standard:" + schedule.repeat_standard;
            pParam[9] = "@holiday_yn:" + schedule.holiday_yn;
            pParam[10] = "@holiday_option:" + schedule.holiday_option;
            pParam[11] = "@start_date:" + schedule.start_date;
            pParam[12] = "@end_date:" + schedule.end_date;
            pParam[13] = "@water_gb:" + schedule.water_gb;
            pParam[14] = "@item_cd:" + schedule.item_cd;
            pParam[15] = "@repeat_days:" + repeat_days;
            pParam[16] = "@day:" + schedule.day;
            pParam[17] = "@month:" + schedule.month;
            pParam[18] = "@weekofmonth:" + schedule.weekofmonth;
            pParam[19] = "@dayofweek:" + schedule.dayofweek;

            string res = _bllSpExecute.SpExecuteString("SP_ScheduleCreate", "CreateScheduleWaterTestPoint", pParam);

            return res;
        }

        internal string ManufactureWaterScheduleDelete(List<ManufactureWaterSchedule.Schedule> schedules)
        {
            string[] pParam = new string[3];

            int cnt = 0;

            foreach (ManufactureWaterSchedule.Schedule schedule in schedules)
            {
                //pParam[0] = "@water_gb:" + schedule.water_gb;
                pParam[0] = "@water_gb:" + schedule.test_type;
                pParam[1] = "@point_cd:" + schedule.point_cd;
                pParam[2] = "@schedule_date:" + schedule.schedule_date;

                string res = _bllSpExecute.SpExecuteString(sSpName, "DeleteSchedule", pParam);

                cnt += int.Parse(res);
            }

            return cnt.ToString();
        }

        internal string ManufactureWaterTestRequest(List<ManufactureWaterSchedule.Schedule> schedules, string water_gb)
        {
            string[] pParam = new string[3];

            int cnt = 0;

            foreach (ManufactureWaterSchedule.Schedule schedule in schedules)
            {
                if (!String.IsNullOrEmpty(schedule.test_no))
                {
                    continue;
                }

                if (schedule.schedule_status_cd != "1")
                {
                    continue;
                }

                pParam[0] = "@water_gb:" + water_gb;
                pParam[1] = "@point_cd:" + schedule.point_cd;
                pParam[2] = "@schedule_date:" + schedule.schedule_date;

                string res = _bllSpExecute.SpExecuteString(sSpName, "R", pParam);

                if (res.Length > 0)
                {
                    cnt++;
                }
            }

            return cnt.ToString();
        }
    }
}