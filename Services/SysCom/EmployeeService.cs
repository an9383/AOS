using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.SysOp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysCom
{
    public class EmployeeService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectEmployee(string dept_cd, string emp_cd, string use_yn)
        {
            string sSpName = "SP_Employee";

            string gubun = "S";

            string[] param = new string[3];

            param[0] = "@s_emp_cd:" + emp_cd;
            param[1] = "@S_DEPT_CD:" + dept_cd;
            param[2] = "@s_use_yn:" + use_yn;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getDepartmentData()
        {
            string strsql = "SELECT dept_cd, dept_nm";
            strsql += " FROM V_DEPARTMENT";
            strsql += " WHERE (dept_cd  LIKE '%%' OR dept_nm  LIKE '%%')";
            strsql += " AND dept_cd  LIKE '%%' AND dept_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt; ;
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

        internal string CRUD(Employee employee, string pGubun)
        {
            string sSpName = "SP_Employee";

            string gubun = pGubun;
            string res = "";

            if (pGubun.Equals("D"))
            {
                string[] param = new string[2];

                param[0] = "@emp_cd:" + employee.emp_cd;
                param[1] = "@at_emp_cd:" + Public_Function.User_cd;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }
            else
            {
                string[] param = getParam(employee);

                if (Public_Function.MasterData_AutoNumbering_yn == "N" && pGubun == "I")
                {
                    string message = _bllSpExecute.SpExecuteString(sSpName, "CEC", param);
                    if (message == "Y")
                        res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
                }
                else
                    res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }

            return res;
        }

        private string[] getParam(Employee employee)
        {
            string[] param = new string[17];

            param[0] = "@emp_cd:" + employee.emp_cd;
            param[1] = "@dept_cd:" + employee.dept_cd;
            param[2] = "@emp_nm:" + employee.emp_nm;
            param[3] = "@emp_post:" + employee.emp_post;
            param[4] = "@emp_rair:" + employee.emp_rair;
            param[5] = "@insert_emp:" + Public_Function.User_cd;
            param[6] = "@enter_date:" + employee.enter_date;
            param[7] = "@retire_date:" + employee.retire_date;
            param[8] = "@manager_emp_cd:" + employee.manager_emp_cd;
            param[9] = "@emp_type:" + employee.emp_type;
            param[10] = "@phone_no:" + employee.phone_no;
            param[11] = "@birth_date:" + employee.birth_date;
            param[12] = "@use_yn:" + employee.use_yn;
            param[13] = "@at_emp_cd:" + Public_Function.User_cd;
            param[14] = "@emp_charge:";
            param[15] = "@calendar_cd:";
            param[16] = "@email_addr:" + employee.email_addr;

            return param;
        }
    }
}
