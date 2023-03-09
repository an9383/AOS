using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysOp
{
    public class AlarmListServie
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable selectAlarm(string pGubun, string user_cd)
        {
            string sSpName = "SP_LimsAlarm";

            string gubun = pGubun;

            string[] param = new string[1];

            param[0] = "@emp_cd:" + user_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getEmpData()
        {
            string strsql = "SELECT emp_cd, emp_nm, dept_cd, dept_nm";
            strsql += " FROM V_EMPLOYEE";
            strsql += " WHERE (emp_cd  LIKE '%%' OR emp_nm  LIKE '%%')";
            strsql += " AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal string AlarmListConfirm(string table_gb, string code, string code2, string code3, string code4, string code5)
        {
            string sSpName = "SP_LimsAlarm";
            string gubun = "SP";

            string[] param = new string[6];
            param[0] = "@table_gb:" + table_gb;
            param[1] = "@code:" + code;
            param[2] = "@code2:" + code2;
            param[3] = "@code3:" + code3;
            param[4] = "@code4:" + code4;
            param[5] = "@code5:" + code5;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }
    }
}
