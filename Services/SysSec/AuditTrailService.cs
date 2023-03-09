using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysSec
{
    public class AuditTrailService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

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

        internal DataTable getColumnData(string tableName)
        {
            string sSpName = "SP_AuditTrailSelectCommon";

            string gubun = "ColumnListFromTargetTable";

            string[] param = new string[1];

            param[0] = "@TableName:" + tableName;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getAuditTrailData(string fromDate, string toDate, string tableName, string empCd)
        {
            string sSpName = "SP_AuditTrailSelectCommon";

            string gubun = "SelectAllItemsFromTargetTable";

            string[] param = new string[4];

            param[0] = "@TableName:" + tableName;
            param[1] = "@FromDate:" + fromDate;
            param[2] = "@ToDate:" + toDate;
            param[3] = "@EmpCd:" + empCd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable getAuditTrailDataDetail(string tableName, string auditTrail_ID)
        {
            string sSpName = "SP_AuditTrailSelectCommon";

            string gubun = "SelectAuditDetailFromTargetTableWithSingleItem";

            string[] param = new string[2];

            param[0] = "@TableName:" + tableName;
            param[1] = "@AuditTrail_ID:" + auditTrail_ID;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }
    }
}
