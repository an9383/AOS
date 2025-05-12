using HACCP.Libs.Database;
using HACCP.Models.PrevCk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrevCk
{
    public class MonthScheduleListService
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

        public List<MonthScheduleList> GridSelect(MonthScheduleList.SrchParam srch)
        {
            string sSpName = "SP_MonthScheduleList";

            string[] pParam = new String[7];
            pParam[0] = "@s_year:" + srch.s_year;
            pParam[1] = "@s_month:" + srch.s_month;
            pParam[2] = "@s_obj_type:" + srch.s_obj_type;
            pParam[3] = "@s_work_type:" + srch.s_work_type;
            pParam[4] = "@s_schedule_type:" + srch.s_schedule_type;
            pParam[5] = "@obj_cd:" + srch.obj_cd;
            pParam[6] = "@common_cd:" + this.common_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", pParam);

            List<MonthScheduleList> monthScheduleList = new List<MonthScheduleList>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                MonthScheduleList monthSchedule = new MonthScheduleList(row);
                monthScheduleList.Add(monthSchedule);
            }

            return monthScheduleList;
        }


        public DataTable GridSelect2(MonthScheduleList.SrchParam srch)
        {
            string sSpName = "SP_MonthScheduleList";

            string[] pParam = new String[4];
            pParam[0] = "@s_year:" + srch.s_year;
            pParam[1] = "@s_month:" + srch.s_month;
            pParam[2] = "@schedule_master_id:" + srch.schedule_master_id;
            pParam[3] = "@common_cd:" + this.common_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S2", pParam);

            return dt;
        }


        public void ReportPreview(MonthScheduleList.SrchParam srch)
        {
            string sSpName = "SP_MonthScheduleList";
            string sGubun = "";
            string[] pParam = null;

            //srch.report_gubun 
            //1,2 : 일일, 15일(S2)
            //3 : 주 단위(S3)
            //4 : 월별(S4)
            if (srch.report_gubun == "1" || srch.report_gubun == "2"){
                sGubun = "S2";

                pParam = new String[4];
                pParam[0] = "@s_year:" + srch.s_year;
                pParam[1] = "@s_month:" + srch.s_month;
                pParam[2] = "@schedule_master_id:" + srch.schedule_master_id;
                pParam[3] = "@common_cd:" + this.common_cd;
            }
            else if(srch.report_gubun == "3"){
                sGubun = "S4";

                pParam = new String[3];
                pParam[0] = "@s_year:" + srch.s_year;
                pParam[1] = "@schedule_master_id:" + srch.schedule_master_id;
                pParam[2] = "@common_cd:" + this.common_cd;

            }else{ //W1, W2, W3, W4, W5

                sGubun = "S3";

                pParam = new String[5];
                pParam[0] = "@s_year:" + srch.s_year;
                pParam[1] = "@s_month:" + srch.s_month;
                pParam[2] = "@s_week:" + srch.s_week;
                pParam[3] = "@schedule_master_id:" + srch.schedule_master_id;
                pParam[4] = "@common_cd:" + this.common_cd;
            }

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            if (srch.report_gubun == "3"){
                dt.TableName = "MonthScheduleList4T";
            }else
            {
                dt.TableName = "MonthScheduleListT";
            }

            //report  호출
            //일일 : MonthScheduleListR(S2)
            //15일 : MonthScheduleListR2(S2)
            //주단위 : WeekScheduleListR(S3)
            //월별 : MonthScheduleListR3(S4)


        }

        public DataTable MonthSchedule_ReportByWeekSP(string s_year, string s_month, string s_week, string schedule_master_id)
        {
            string sSpName = "SP_MonthScheduleList";

            string[] pParam = new String[5];
            pParam[0] = "@s_year:" + s_year;
            pParam[1] = "@s_month:" + s_month;
            pParam[2] = "@s_week:" + s_week;
            pParam[3] = "@schedule_master_id:" + schedule_master_id;
            pParam[4] = "@common_cd:" + this.common_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S3", pParam);

            return dt;
        }
        
        public DataTable GetProgramSet()
        {
            string sSpName = "SP_ProgramInit";

            string[] pParam = new string[1];
            pParam[0] = "@code:" + "MonthScheduleList";

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