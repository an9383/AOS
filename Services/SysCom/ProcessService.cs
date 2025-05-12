using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.SysCom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Services.SysCom
{
    public class ProcessService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectProcess(string process_cd)
        {
            string sSpName = "SP_Process";

            string gubun = "S";

            string[] param = new string[1];

            param[0] = "@s_process_cd:" + process_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectProcessRateType(string process_rate_type)
        {
            string sSpName = "SP_Process";

            string gubun = "SR";

            string[] param = new string[1];

            param[0] = "@process_rate_type:" + process_rate_type;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable selectWorkroomPopupData()
        {
            string strsql = "	SELECT workroom_cd, workroom_nm, common_part_nm";
            strsql += " 	FROM v_workroom2 	";
            strsql += " 	WHERE (workroom_cd  LIKE '%%' OR workroom_nm  LIKE '%%') 	";
            strsql += " 	AND workroom_cd  LIKE '%%' AND workroom_nm  LIKE '%%' 	";
            strsql += " 	ORDER BY 1	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectEmpPopupData()
        {
            string strsql = "	SELECT emp_cd, emp_nm, dept_cd, dept_nm 	";
            strsql += " 	FROM V_EMPLOYEE 	";
            strsql += " 	WHERE (emp_cd  LIKE '%%' OR emp_nm  LIKE '%%') 	";
            strsql += " 	AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%' 	";
            strsql += " 	ORDER BY 1 	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectCCPData()
        {
            string strsql = "	SELECT ccp_cd, ccp_nm, ccp_description 	";
            strsql += " 	FROM process_exam_master 	";
            strsql += " 	WHERE middle_item = '06'";
            strsql += " 	ORDER BY ccp_cd 	";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal string processManagerCRUD(string process_cd, string user_id, string pGubun)
        {
            string sSpName = "SP_Process";

            string gubun = pGubun;
            string res = "";

            string[] param = new string[2];

            param[0] = "@process_cd:" + process_cd;
            param[1] = "@INSERT_EMP:" + user_id;

            res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

        internal DataTable SelectProcessManager(string process_cd)
        {
            string sSpName = "SP_Process";

            string gubun = "SC";

            string[] param = new string[1];

            param[0] = "@s_process_cd:" + process_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string processCRUD(Process process, string pGubun)
        {
            string sSpName = "SP_Process";

            string gubun = pGubun;
            string res = "";

            if (pGubun.Equals("D"))
            {
                string[] param = new string[1];

                param[0] = "@process_cd:" + process.process_cd;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }
            else
            {
                string[] param = getParam(process);

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            }

            return res;
        }

        private string[] getParam(Process process)
        {
            string[] param = new string[18];

            param[0] = "@process_cd:" + process.process_cd;
            param[1] = "@process_nm:" + process.process_nm;
            param[2] = "@process_qc_ck:" + process.process_qc_ck;
            param[3] = "@process_transfer_ck:" + process.process_transfer_ck;
            param[4] = "@process_worker1:" + process.process_worker1;
            param[5] = "@process_worker2:" + process.process_worker2;
            param[6] = "@process_inspector:" + process.process_inspector;
            param[7] = "@process_personincharge:" + process.process_personincharge;
            param[8] = "@workroom_cd:" + process.workroom_cd;
            param[9] = "@process_type:" + process.process_type;
            param[10] = "@process_rate_type:" + process.process_rate_type;
            param[11] = "@process_remark:" + process.process_remark;
            param[12] = "@process_keep_method:" + process.process_keep_method;
            param[13] = "@process_valid_period:" + process.valid_period;
            param[14] = "@item_form_cd:" + process.item_form_cd;
            param[15] = "@insert_emp:" + HttpContext.Current.Session["loginCD"].ToString();
            param[16] = "@at_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            param[17] = "@ccp_cd:" + process.ccp_cd;

            return param;
        }
    }
}
