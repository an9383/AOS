using HACCP.Libs.Database;
using HACCP.Models.PrevCk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrevCk
{
    public class YearScheduleListService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        public string common_cd
        {
            get
            {
                DataTable programSet = GetProgramSet();

                string commonCd = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString();//Option1
                return commonCd;
            }

        }


        public List<YearScheduleList> GridSelect(YearScheduleList.SrchParam srch)
        {
            string sSpName = "sp_YearScheduleList";

            string[] pParam = new String[6];
            pParam[0] = "@year:" + srch.year;
            pParam[1] = "@s_obj_type:" + srch.s_obj_type;
            pParam[2] = "@s_work_type:" + srch.s_work_type;
            pParam[3] = "@s_schedule_type:" + srch.s_work_type;
            pParam[4] = "@obj_cd:" + srch.obj_cd;
            pParam[5] = "@common_cd:" + this.common_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", pParam);

            List<YearScheduleList> yearScheduleList = new List<YearScheduleList>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                YearScheduleList yearSchedule = new YearScheduleList(row);
                yearScheduleList.Add(yearSchedule);
            }

            return yearScheduleList;
        }


        public DataTable GetProgramSet()
        {
            string sSpName = "SP_ProgramInit";

            string[] pParam = new string[1];
            pParam[0] = "@code:" + "YearScheduleList";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}