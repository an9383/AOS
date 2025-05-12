using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Insp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace HACCP.Services.Insp
{
    public class SelfAuditEditActionService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_SelfAuditEditAction";


        #region Pop관련 Data
        // 사원조회
        internal DataTable getEmpPopupData()
        {
            string strsql = "SELECT emp_cd, emp_nm, dept_cd, dept_nm";
            strsql += " FROM V_EMPLOYEE";
            strsql += " WHERE (emp_cd  LIKE '%%' OR emp_nm  LIKE '%%')";
            strsql += " AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }


        // 부서조회
        internal DataTable getDeptPopupData()
        {
            string strsql = "SELECT dept_cd, dept_nm";
            strsql += " FROM V_DEPARTMENT";
            strsql += " WHERE (dept_cd  LIKE '%%' OR dept_nm  LIKE '%%')";
            strsql += " AND dept_cd  LIKE '%%' AND dept_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt; ;
        }
        #endregion


        #region 기본CRUD

        public DataTable Select(string sDate, string eDate, string dept_nm, string deptEmp_nm, string status)
        {
            string sGubun = "S";

            string[] pParam = new string[5];

            pParam[0] = "@self_audit_s_date:" + sDate;
            pParam[1] = "@self_audit_e_date:" + eDate;
            pParam[2] = "@self_audit_ca_dept:" + dept_nm;
            pParam[3] = "@self_audit_ca_dept_emp:" + deptEmp_nm;
            pParam[4] = "@self_audit_ca_status:" + status;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        public string SelfAuditEditActionSave(SelfAuditCa data)
        {
            string sGubun = "U";

            string[] pParam = new string[7];

            pParam[0] = "@self_audit_year:" + data.self_audit_year;
            pParam[1] = "@self_audit_no:" + data.self_audit_no;
            pParam[2] = "@self_audit_ca_id:" + data.self_audit_ca_id;
            pParam[3] = "@self_audit_ca_emp_cd:" + data.self_audit_ca_emp_cd;
            pParam[4] = "@self_audit_ca_date:" + data.self_audit_ca_date;
            pParam[5] = "@self_audit_ca_contents:" + data.self_audit_ca_contents;
            pParam[6] = "@self_audit_ca_status:" + data.self_audit_ca_status;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            sGubun = "U2";
            pParam = new string[2];
            pParam[0] = "@self_audit_year:" + data.self_audit_year;
            pParam[1] = "@self_audit_no:" + data.self_audit_no;

            message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        public string SelfAuditEditActionDelete(SelfAuditCa data)
        {
            string sGubun = "D";

            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + data.self_audit_year;
            pParam[1] = "@self_audit_no:" + data.self_audit_no;
            pParam[2] = "@self_audit_ca_id:" + data.self_audit_ca_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            if(message.Length > 7)
            {
                sGubun = "D_C";
                pParam = new string[2];
                pParam[0] = "@self_audit_year:" + data.self_audit_year;
                pParam[1] = "@self_audit_no:" + data.self_audit_no;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            }

            return message;
        }
        #endregion


        #region 첨부파일 관련
        public DataTable EditActionFileSearch(SelfAuditCa data)
        {
            string sGubun = "DOC_S";

            string[] pParam = new string[2];
            pParam[0] = "@self_audit_year:" + data.self_audit_year;
            pParam[1] = "@self_audit_no:" + data.self_audit_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal string EditActionFileAdd(byte[] myBytes, string fileName, string contentType, SelfAudit data)
        {
            var doc_type = Path.GetExtension(fileName);

            string sGubun = "F";
            string[] pParam = new string[6];

            pParam[0] = "@self_audit_year:" + data.self_audit_year;
            pParam[1] = "@self_audit_no:" + data.audit_no;
            pParam[2] = "@upload_step:" + data.audit_step;
            pParam[3] = "@upload_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[4] = "@doc_name:" + fileName;
            pParam[5] = "@doc_type:" + doc_type;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@file_image", myBytes, pParam);

            return message;
        }


        internal string EditActionFileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string sGubun = "D_DOC";
            string[] pParam = new string[4];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_file:" + audit_file;
            pParam[3] = "@file_id:" + file_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal DataTable EditActionFileOpen(string file_id)
        {
            string sGubun = "FS";
            string pParam = "@file_id:" + file_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            else
            {
                return null;
            }

            return dt;
        }
        #endregion
    }
}