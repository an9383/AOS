using HACCP.Libs.Database;
using HACCP.Models.SysOp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysOp
{
    public class Employee_group_manageService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 사원 정보 조회
        /// </summary>
        /// <returns></returns>
        internal DataTable selectEmployee()
        {
            string sSpName = "SP_Employee_group_manage";
            string sGubun = "S2";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        /// <summary>
        /// 부서 정보 조회
        /// </summary>
        /// <param name="pGroupText"></param>
        /// <returns></returns>
        internal DataTable selectGroup(string pGroupText)
        {
            string sSpName = "SP_Employee_group_manage";
            string sGubun = "S";
            string[] pParam = new string[1];

            pParam[0] = "@group_text:" + pGroupText;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        /// <summary>
        /// 특정 부서의 사원 조회
        /// </summary>
        /// <param name="pGroupCd"></param>
        /// <param name="empText"></param>
        /// <returns></returns>
        internal DataTable selectEmployeesInGroup(string pGroupCd, string empText)
        {
            string sSpName = "SP_Employee_group_manage";
            string sGubun = "S1";
            string[] pParam = new string[2];

            pParam[0] = "@emp_group_cd:" + pGroupCd;
            pParam[1] = "@emp_text:" + empText;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        /// <summary>
        /// 부서에 사원 추가, 제거
        /// </summary>
        /// <param name="empGroupCd"></param>
        /// <param name="empCd"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>
        internal string employeeCRUD(string empGroupCd, string empCd, string gubun)
        {
            string sSpName = "SP_Employee_group_manage";
            string sGubun = gubun;
            string[] pParam = new string[2];

            pParam[0] = "@emp_group_cd:" + empGroupCd;
            pParam[1] = "@emp_cd:" + empCd;

            string res = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            if(res.Equals("") && gubun.Equals("I"))
            {
                res = "해당 사원은 이미 그룹에 포함되어있습니다.";
            }

            return res;
        }

        /// <summary>
        /// 사원의 부서정보 조회
        /// </summary>
        /// <param name="empCd"></param>
        /// <param name="groupText"></param>
        /// <returns></returns>
        internal DataTable selectGroupOfEmployee(string empCd, string groupText)
        {
            string sSpName = "SP_Employee_group_manage";
            string sGubun = "S3";
            string[] pParam = new string[2];

            pParam[0] = "@emp_cd:" + empCd;
            pParam[1] = "@group_text:" + groupText;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }
    }
}
