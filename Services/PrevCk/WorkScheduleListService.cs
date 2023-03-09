using HACCP.Libs.Database;
using HACCP.Models.PrevCk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrevCk
{
    public class WorkScheduleListService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        public List<WorkScheduleList> GridSelect(WorkScheduleList.SrchParam srch)
        {
            string sSpName = "sp_WorkScheduleList";

            string[] pParam = new String[5];
            pParam[0] = "@s_schedule_date1:" + srch.s_schedule_date1;
            pParam[1] = "@s_schedule_date2:" + srch.s_schedule_date2;
            pParam[2] = "@s_obj_type:" + srch.s_obj_type;
            pParam[3] = "@s_work_type:" + srch.s_work_type;
            pParam[4] = "@s_schedule_type:" + srch.s_schedule_type;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", pParam);

            List<WorkScheduleList> workScheduleList = new List<WorkScheduleList>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                WorkScheduleList workSchedule = new WorkScheduleList(row);
                workScheduleList.Add(workSchedule);
            }

            return workScheduleList;
        }
    }
}