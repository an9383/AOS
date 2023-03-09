using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Education;

namespace HACCP.Services.Education
{
    public class EmployeeEduHistoryService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable EmployeeEduHistory_Search(EmployeeEduHistory iModel)
        {
            string sSpName = "SP_EmployeeEduHistory_AOS";
            string gubun = "Search";

            string[] pParam = new string[5];
            pParam[0] = "@title:" + iModel.title;
            pParam[1] = "@sdate:" + iModel.sdate;
            pParam[2] = "@edate:" + iModel.edate;
            pParam[3] = "@emp_cd:" + iModel.emp_cd;
            pParam[4] = "@dept_cd:" + iModel.dept_cd;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal DataTable EmployeeEduHistory_EmployeeSearch(EmployeeEduHistory iModel)
        {
            string sSpName = "SP_EmployeeEduHistory_AOS";
            string gubun = "S_EMP";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@sdate:" + iModel.sdate;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }


        internal DataTable EmployeeEduHistory_DepartmentSearch(EmployeeEduHistory iModel)
        {
            string sSpName = "SP_EmployeeEduHistory_AOS";
            string gubun = "S_Dept";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@sdate:" + iModel.sdate;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

    }
}