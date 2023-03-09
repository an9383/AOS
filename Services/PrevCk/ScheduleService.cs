using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.PrevCk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrevCk
{
    public class ScheduleService
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

        public List<Schedule> GridSelect(Schedule.SrchParam srch)
        {
            string sSpName = "SP_Schedule_Y";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", GetParam1(srch));
            List<Schedule> schedules = new List<Schedule>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                Schedule schedule = new Schedule(row);
                schedules.Add(schedule);
            }

            return schedules;
        }

        public string GridDelete(Schedule schedule)
        {
            string sSpName = "SP_Schedule_Y";
            string sGubun = "D";

            string[] pParam = GetParam2(schedule);

            string result = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return result;
        }


        public string GridDeleteAll(Schedule.SrchParam srch)
        {
            string sSpName = "SP_Schedule_Y";
            string sGubun = "DA";

            string[] pParam = GetParam1(srch);

            string result = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return result;
        }

        public string Schedule_TRX(List<Schedule> paramData)
        {
            string sSpName = "SP_Schedule_Y";
            string res = "";

            foreach(Schedule tmp in paramData)
            {
                if (tmp.gubun == "I")
                {
                    string[] pParam = new string[11];
                    pParam[0] = "@schedule_date:" + tmp.schedule_date;
                    pParam[1] = "@obj_type:" + tmp.obj_type;
                    pParam[2] = "@work_type:" + tmp.work_type;
                    pParam[3] = "@schedule_type:" + tmp.schedule_type;
                    pParam[4] = "@obj_cd:" + tmp.obj_cd;
                    pParam[5] = "@obj_nm:" + tmp.obj_nm;
                    pParam[6] = "@schedule_id:" + tmp.schedule_id;
                    pParam[7] = "@schedule_input_gb:" + tmp.schedule_input_gb;
                    pParam[8] = "@sub_cd:" + tmp.sub_cd;
                    pParam[9] = "@schedule_period:" + tmp.period;
                    pParam[10] = "@responsibility_worker:" + tmp.responsibility_worker;
                    
                    res = _bllSpExecute.SpExecuteString(sSpName, tmp.gubun, pParam);

                } else if (tmp.gubun == "U") {

                    string[] pParam = new string[7];
                    pParam[0] = "@schedule_input_gb:" + tmp.schedule_input_gb;
                    pParam[1] = "@insert_emp:" + Public_Function.User_id;
                    pParam[2] = "@sub_cd:" + tmp.sub_cd;
                    pParam[3] = "@responsibility_worker:" + tmp.responsibility_worker;
                    pParam[4] = "@obj_nm:" + tmp.obj_nm;
                    pParam[5] = "@schedule_period:" + tmp.period;
                    pParam[6] = "@schedule_id:" + tmp.schedule_id;

                    res = _bllSpExecute.SpExecuteString(sSpName, tmp.gubun, pParam);

                } else if(tmp.gubun == "D") {

                    string[] pParam = GetParam2(tmp);
                    res = _bllSpExecute.SpExecuteString(sSpName, tmp.gubun, pParam);

                }
            }
            return res;
        }

        public string SaveNodes(Schedule schedule)
        {
            string sSpName = "SP_Schedule_Y";
            string sGubun = schedule.gubun;
            string result = "";
            if(schedule.gubun == "I")
            {
                string[] pParam = new string[11];
                pParam[0] = "@schedule_date:" + schedule.schedule_date;
                pParam[1] = "@obj_type:" + schedule.obj_type;
                pParam[2] = "@work_type:" + schedule.work_type;
                pParam[3] = "@schedule_type:" + schedule.schedule_type;
                pParam[4] = "@obj_cd:" + schedule.obj_cd;
                pParam[5] = "@obj_nm:" + schedule.obj_nm;
                pParam[6] = "@schedule_id:" + schedule.schedule_id;
                pParam[7] = "@schedule_input_gb:" + schedule.schedule_input_gb;
                pParam[8] = "@sub_cd:" + schedule.sub_cd;
                pParam[9] = "@schedule_period:" + schedule.period ;
                pParam[10] = "@responsibility_worker:" + schedule.responsibility_worker;
                result = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            }

            else if(schedule.gubun == "U")
            {

                string[] pParam = new string[7];
                pParam[0] = "@schedule_input_gb:" + schedule.schedule_input_gb;
                pParam[1] = "@insert_emp:" + Public_Function.User_id;
                pParam[2] = "@sub_cd:" + schedule.sub_cd;
                pParam[3] = "@responsibility_worker:" + schedule.responsibility_worker;
                pParam[4] = "@obj_nm:" + schedule.obj_nm;
                pParam[5] = "@schedule_period:" + schedule.period;
                pParam[6] = "@schedule_id:" + schedule.schedule_id;

                result = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return result;
        }

        public string CreateSchedule(Schedule.SrchParam srch)
        {
            string sSpName = "SP_Schedule_Y";
            string sGubun = "C";

            string[] pParam = GetParam1(srch);

            string result = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return result;
        }

        public string CheckSchedule(Schedule schedule)
        {
            string sSpName = "SP_Schedule_Y";
            string sGubun = "CR";

            string[] pParam = GetParam2(schedule);

            string result = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return result;
        }

        public DataTable GetRepositoryItem(string gubun)
        {
            string sSpName = "SP_Schedule_Y";
            DataSet ds;
            if (gubun == "L1")
            {
                ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, "@common_cd:"+ this.common_cd);
            }
            else
            {
                ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun);
            }         

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        //조회 및 상태값
        private string[] GetParam1(Schedule.SrchParam srch)
        {
            //DataTable programSet = GetProgramSet();
            //string commonCd = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString();

            //SP 호출과관련된함수들의파라미터셋팅
            string[] param = new string[10];

            param[0] = "@s_schedule_date1:" + srch.s_schedule_date1;
            param[1] = "@s_schedule_date2:" + srch.s_schedule_date2;
            param[2] = "@s_obj_type:" + srch.s_obj_type;
            param[3] = "@s_work_type:" + srch.s_work_type;
            param[4] = "@s_schedule_type:" + srch.s_schedule_type;
            param[5] = "@s_obj_cd:" + srch.s_obj_cd;
            param[6] = "@s_responsibility_worker:" + srch.s_responsibility_worker;
            param[7] = "@s_dept_cd:" + srch.s_dept_cd;
            param[8] = "@s_schedule_execution_ck:" + srch.s_schedule_execution_ck;
            param[9] = "@common_cd:" + this.common_cd;

            return param;

        }

        //
        private string[] GetParam2(Schedule schedule)
        {
            //DataTable programSet = GetProgramSet();
            //string commonCd = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString();

            //SP 호출과관련된함수들의파라미터셋팅
            string[] param2 = new string[8];
            param2[0] = "@schedule_date:" + schedule.schedule_date;
            param2[1] = "@obj_type:" + schedule.obj_type;
            param2[2] = "@work_type:" + schedule.work_type;
            param2[3] = "@schedule_type:" + schedule.schedule_type;
            param2[4] = "@obj_cd:" + schedule.obj_cd;
            param2[5] = "@schedule_id:" + schedule.schedule_id;
            param2[6] = "@sub_cd:" + schedule.responsibility_worker;
            param2[7] = "@common_cd:" + this.common_cd;
            return param2;

        }


        #region 프로그램 설정값
        public DataTable GetProgramSet()
        {
            string sSpName = "SP_ProgramInit";
            string[] pParam = new string[1];
            pParam[0] = "@code:" + "Schedule";
            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        #endregion
    }
}