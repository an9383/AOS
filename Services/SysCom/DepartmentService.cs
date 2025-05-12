using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.SysCom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysCom
{
    public class DepartmentService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable selectDepartmentData(string use_yn)
        {
            string sSpName = "SP_Department";

            string gubun = "S";

            string[] param = new string[1];

            param[0] = "@s_dept_use_gb:" + use_yn;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getDepartmentPopupData()
        {
            string strsql = "SELECT dept_cd, dept_nm";
            strsql += " FROM V_DEPARTMENT";
            strsql += " WHERE (dept_cd  LIKE '%%' OR dept_nm  LIKE '%%')";
            strsql += " AND dept_cd  LIKE '%%' AND dept_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt; ;
        }

        internal string CRUD(Department department, string pGubun)
        {
            string sSpName = "SP_Department";

            string gubun = pGubun;

            string[] param = new string[9];

            if (pGubun.Equals("C"))
            {
                param = new string[2];

                param[0] = "@dept_cd:" + department.dept_cd;
                param[1] = "@at_emp_cd:" + Public_Function.User_cd;
            }
            else
            {
                param[0] = "@dept_cd:" + department.dept_cd;
                param[1] = "@dept_nm:" + department.dept_nm;
                param[2] = "@dept_gb:" + department.dept_gb;
                param[3] = "@dept_mcd:" + department.dept_mcd;
                param[4] = "@dept_level:" + department.dept_level;
                param[5] = "@dept_use_gb:" + department.dept_use_gb;
                param[6] = "@insert_emp:" + Public_Function.User_cd;
                param[7] = "@at_emp_cd:" + Public_Function.User_cd;
                param[8] = "@plant_cd:" + department.plant_cd;
            }

            string res = string.Empty;
            if (Public_Function.MasterData_AutoNumbering_yn == "N" && pGubun == "I")
            {
                string message = _bllSpExecute.SpExecuteString(sSpName, "CDC", param);
                if (message == "Y")
                    res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }
            else
                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

    }
}
