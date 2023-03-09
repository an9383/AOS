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
    public class SelfAuditStartService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        // 부서조회
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


        public DataTable Select(string sDate, string eDate, string auditor_cd, string status)
        {
            string sSpName = "SP_SelfAuditStart";
            string sGubun = "S";

            string[] pParam = new string[4];
            pParam[0] = "@self_audit_s_date:" + sDate;
            pParam[1] = "@self_audit_e_date:" + eDate;
            pParam[2] = "@self_audit_auditor:" + auditor_cd;
            pParam[3] = "@self_audit_status:" + status;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        public DataTable CheckListSelect(string audit_year, string audit_no, string record_emp_cd)
        {
            string sSpName = "SP_SelfAuditStart";
            string sGubun = "Check_List_Search";

            string[] pParam = new string[3];
            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_check_record_emp_cd:" + record_emp_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal DataTable StartFileSearch(SelfAudit list)
        {
            string sSpName = "SP_SelfAuditStart";
            string gubun = "DOC_S";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + list.self_audit_year;
            pParam[1] = "@self_audit_no:" + list.audit_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }


        internal string StartFileAdd(byte[] myBytes, string fileName, string contentType, SelfAudit list)
        {
            var doc_type = Path.GetExtension(fileName);

            string sSpName = "SP_SelfAuditStart";
            string sGubun = "F";
            string[] pParam = new string[6];

            pParam[0] = "@self_audit_year:" + list.self_audit_year;
            pParam[1] = "@self_audit_no:" + list.audit_no;
            pParam[2] = "@upload_step:" + list.audit_step_cd;
            pParam[3] = "@upload_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[4] = "@doc_name:" + fileName;
            pParam[5] = "@doc_type:" + doc_type;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@file_image", myBytes, pParam);

            return message;
        }


        internal string StartFileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string sSpName = "SP_SelfAuditStart";
            string sGubun = "D_DOC";
            string[] pParam = new string[4];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_file:" + audit_file;
            pParam[3] = "@file_id:" + file_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal DataTable StartFileOpen(string file_id)
        {
            string sSpName = "SP_SelfAuditStart";
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


        internal string StartGridEdit(string audit_year, string audit_no, string audit_dept_cd, string audit_auditor, string audit_date, string audit_result)
        {
            string sSpName = "SP_SelfAuditStart";
            string sGubun = "U";
            string[] pParam = new string[7];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_dept_cd:" + audit_dept_cd;
            pParam[3] = "@self_audit_auditor:" + audit_auditor;

            pParam[4] = "@self_audit_date:" + audit_date;
            pParam[5] = "@self_audit_result:" + audit_result;
            pParam[6] = "@self_audit_status:" + "2";

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string StartGridUpdate2(string audit_year, string audit_no)
        {
            string sSpName = "SP_SelfAuditStart";
            string sGubun = "U2";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string StartGridDelete(string audit_year, string audit_no, string audit_dept_cd, string audit_auditor)
        {
            string sSpName = "SP_SelfAuditStart";
            string sGubun = "D";
            string[] pParam = new string[4];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_dept_cd:" + audit_dept_cd;
            pParam[3] = "@self_audit_auditor:" + audit_auditor;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string StartGridDelete2(string audit_year, string audit_no)
        {
            string sSpName = "SP_SelfAuditStart";
            string sGubun = "D_C";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string ListResultInsert(string audit_item_no, string audit_result, string audit_evaluation, string audit_opinion)
        {
            string sSpName = "SP_SelfAuditStart";
            string sGubun = "Check_List_Update";
            string[] pParam = new string[4];

            pParam[0] = "@self_audit_check_record_item_no:" + audit_item_no;
            pParam[1] = "@self_audit_check_record_result:" + audit_result;
            pParam[2] = "@self_audit_check_record_evaluation:" + audit_evaluation;
            pParam[3] = "@self_audit_check_record_opinion:" + audit_opinion;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal DataTable SelfAuditStartSignInfo(string self_record_no, string self_record_emp_cd, string sign_set_cd)
        {
            string sSpName = "SP_SelfAuditStart";
            string sGubun = "Check_List_Sign_Search";

            string[] pParam = new string[3];
            pParam[0] = "@self_audit_check_record_no:" + self_record_no;
            pParam[1] = "@self_audit_check_record_emp_cd:" + self_record_emp_cd;
            pParam[2] = "@sign_set_cd:" + sign_set_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();
            table.Columns.Add("sign_set_dt_id", typeof(String));
            table.Columns.Add("displayfield", typeof(String));
            table.Columns.Add("sign_yn", typeof(String));
            table.Columns.Add("sign_time", typeof(String));
            table.Columns.Add("sign_emp_cd", typeof(String));
            table.Columns.Add("responsible_emp_cd", typeof(String));
            table.Columns.Add("sign_image", typeof(String));
            table.Columns.Add("responsible_emp_nm", typeof(String));
            table.Columns.Add("sign_emp_nm", typeof(String));
            table.Columns.Add("prev_sign_yn", typeof(String));
            table.Columns.Add("next_sign_yn", typeof(String));
            table.Columns.Add("sign_set_dt_seq", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["sign_set_dt_id"] = row["sign_set_dt_id"].ToString();
                _row["displayfield"] = row["displayfield"].ToString();
                _row["sign_yn"] = row["sign_yn"].ToString();
                _row["sign_time"] = row["sign_time"].ToString();
                _row["sign_emp_cd"] = row["sign_emp_cd"].ToString();
                _row["responsible_emp_cd"] = row["responsible_emp_cd"].ToString();


                if (row["sign_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["sign_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["sign_image"] = url;
                }
                else
                {
                    _row["sign_image"] = "/Content/image/defaultSign.png";
                }

                _row["responsible_emp_nm"] = row["responsible_emp_nm"].ToString();
                _row["sign_emp_nm"] = row["sign_emp_nm"].ToString();
                _row["prev_sign_yn"] = row["prev_sign_yn"].ToString();
                _row["next_sign_yn"] = row["next_sign_yn"].ToString();
                _row["sign_set_dt_seq"] = row["sign_set_dt_seq"].ToString();

                table.Rows.Add(_row);
            }

            return table;
        }


        internal string SaveSign(string audit_record_no, string emp_cd)
        {
            string sSpName = "SP_SelfAuditStart";
            string sGubun = "Check_List_Sign";

            string[] pParam = new string[3];

            pParam[0] = "@self_audit_check_record_no:" + audit_record_no;
            pParam[1] = "@self_audit_checked_emp_cd:" + emp_cd;
            pParam[2] = "@self_audit_checked_emp_type:" + "1";

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

    }
}