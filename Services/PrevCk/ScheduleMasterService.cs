using DevExtreme.AspNet.Mvc.Builders;
using HACCP.Libs.Database;
using HACCP.Models;
using HACCP.Models.PrevCk;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HACCP.Services.PrevCk
{
    public class ScheduleMasterService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();



        public DataTable GetObjCd()
        {
            string strsql = "SELECT standard_cd, standard_nm 	";
            strsql += " 	FROM v_schedulecode 	";
            strsql += " 	WHERE (standard_cd  LIKE '%%' OR standard_nm  LIKE '%%') 	";
            strsql += " 	AND standard_type_cd  LIKE '%%' AND standard_type_cd  LIKE '%%' 	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        public DataTable GetDept()
        {
            string strsql = "SELECT dept_cd, dept_nm 	";
            strsql += " 	FROM V_DEPARTMENT_ALL 	";
            strsql += " 	WHERE(dept_cd LIKE '%%' OR dept_nm  LIKE '%%')  	";
            strsql += " 	AND dept_cd  LIKE '%%' AND dept_nm  LIKE '%%' 	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        public DataTable GetEmp()
        {
               
            string strsql = "SELECT emp_cd, emp_nm, dept_cd, dept_nm 	";
            strsql += " 	FROM V_EMPLOYEE 	";
            strsql += " 	WHERE(emp_cd  LIKE '%%' OR emp_nm  LIKE '%%')  	";
            strsql += " 	AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%' 	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }


        public DataTable GridSelect(string obj_type, string work_type, string schedule_type, string obj_cd)
        {
            DataTable programSet = GetProgramSet();
            string commonCd = programSet.DataSet.Tables[1].Rows[0].ItemArray[1].ToString();

            string sSpName = "SP_ScheduleMaster_Y";
            string sGubun = "S";
            string[] pParam = new string[5];
            pParam[0] = "@obj_type:" + obj_type;
            pParam[1] = "@work_type:" + work_type;
            pParam[2] = "@schedule_type:" + schedule_type;
            pParam[3] = "@obj_cd:" + obj_cd;
            pParam[4] = "@common_cd:" + commonCd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        public string GridInsert(ScheduleMaster schedule)
        {
            string sSpName = "SP_ScheduleMaster_Y";
            string sGubun = "I";

            string[] pParam = GetParam(schedule);


            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;

        }

        public string GridUpdate(ScheduleMaster schedule)
        {
            string sSpName = "SP_ScheduleMaster_Y";
            string sGubun = "U";

            string[] pParam = GetParam(schedule);

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;

        }

        public string GridDelete(string obj_type, string work_type, string schedule_type, string obj_cd, string schedule_master_id)
        {
            string sSpName = "SP_ScheduleMaster_Y";
            string sGubun = "D";

            string[] pParam = new string[5];
            pParam[0] = "@obj_type:" + obj_type;
            pParam[1] = "@work_type:" + work_type;
            pParam[2] = "@schedule_type:" + schedule_type;
            pParam[3] = "@obj_cd:" + obj_cd;
            pParam[4] = "@schedule_master_id:" + schedule_master_id;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;

        }

        public string ScheduleCreate(string schedule_master_id)
        {
            string sSpName = "SP_ScheduleMaster_Y";
            string sGubun = "C";

            string[] pParam = new string[1];
            pParam[0] = "@schedule_master_id:" + schedule_master_id;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        public string[] GetParam(ScheduleMaster schedule)
        {
            string[] pParam = new string[13];
            pParam[0] = "@obj_type:" + schedule.obj_type;
            pParam[1] = "@work_type:" + schedule.work_type;
            pParam[2] = "@schedule_type:" + schedule.schedule_type;
            pParam[3] = "@obj_cd:" + schedule.obj_cd;
            pParam[4] = "@obj_nm:" + schedule.obj_nm;
            pParam[5] = "@schedule_period:" + schedule.schedule_period;
            pParam[6] = "@start_date:" + schedule.start_date;
            pParam[7] = "@responsibility_worker:" + schedule.responsibility_worker;
            pParam[8] = "@auto_create_yn:" + schedule.auto_create_yn;
            pParam[9] = "@regist_yn:" + schedule.regist_yn;
            pParam[10] = "@schedule_master_id:" + schedule.schedule_master_id;
            pParam[11] = "@dept_cd:" + schedule.dept_cd;
            pParam[12] = "@period_type:" + schedule.period_type;
            return pParam;
        }

        #region 프로그램 설정값
        public DataTable GetProgramSet()
        {
            string sSpName = "SP_ProgramInit";
            string[] pParam = new string[1];
            //pParam[0] = "@code:" + Public_Function.program_id;
            pParam[0] = "@code:" + "ScheduleMaster";

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
