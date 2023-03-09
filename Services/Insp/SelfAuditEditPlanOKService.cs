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
    public class SelfAuditEditPlanOKService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();


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


        public DataTable Select(string sDate, string eDate, string emp_cd, string step, string status)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "S1";

            string[] pParam = new string[5];
            pParam[0] = "@de_Sdate:" + sDate;
            pParam[1] = "@de_Edate:" + eDate;
            pParam[2] = "@emp_cd:" + emp_cd;
            pParam[3] = "@self_audit_step:" + step;
            pParam[4] = "@self_audit_step_status:" + status;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal DataTable EditPlanOKDisplayESInfo(string audit_year, string audit_no, string sign_set_cd)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "S2";
            string[] pParam = new string[3];
            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
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
            table.Columns.Add("sign_comment", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["sign_set_dt_id"] = row["sign_set_dt_id"].ToString();
                _row["displayfield"] = row["displayfield"].ToString();
                _row["sign_yn"] = row["sign_yn"].ToString();
                _row["sign_time"] = row["sign_time"].ToString();
                _row["sign_emp_cd"] = row["sign_emp_cd"].ToString();
                _row["responsible_emp_cd"] = row["responsible_emp_cd"].ToString();
                _row["sign_comment"] = row["sign_comment"].ToString();


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


        internal string EditPlanOKSaveSign(string audit_year, string audit_no, string emp_cd, string type)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "ES";

            string[] pParam = new string[9];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_cw_emp_cd:" + "";
            pParam[3] = "@self_audit_cw_emp_type:" + "";
            pParam[4] = "@self_audit_cc_emp_cd:" + "";
            pParam[5] = "@self_audit_cc_emp_type:" + "";
            pParam[6] = "@self_audit_c_emp_cd:" + "";
            pParam[7] = "@self_audit_c_emp_type:" + "";
            pParam[8] = "@type:" + type;

            if (type == "cw")
            {
                pParam[2] = "@self_audit_cw_emp_cd:" + emp_cd;
                pParam[3] = "@self_audit_cw_emp_type:" + "2";
            }
            else if(type == "cc")
            {
                pParam[4] = "@self_audit_cc_emp_cd:" + emp_cd;
                pParam[5] = "@self_audit_cc_emp_type:" + "2";
            }
            else if (type == "c")
            {
                pParam[6] = "@self_audit_c_emp_cd:" + emp_cd;
                pParam[7] = "@self_audit_c_emp_type:" + "2";
            }

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string EditPlanOKDeleteSign(string audit_year, string audit_no, string type)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "ES_D";

            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@type:" + type;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string ResultInsertComment_C(string self_audit_year, string audit_no, string c_comment)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "UC1";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_rc_comments:" + c_comment;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string ResultInsertComment_M(string self_audit_year, string audit_no, string c_comment)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "UC2";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_r_comments:" + c_comment;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal DataTable EditPlanOKResultSearch(SelfAudit data)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "S3";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + data.self_audit_year;
            pParam[1] = "@self_audit_no:" + data.audit_no;


            DataTable dt= _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal DataTable EditPlanOKRevisionSearch(SelfAudit data)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "S5";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + data.self_audit_year;
            pParam[1] = "@self_audit_no:" + data.audit_no;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal string EditPlanOKSave(SelfAuditCa data)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "CA_U";
            string[] pParam = new string[10];

            //자율점검연도, 자율점검번호, 지시자, 지시내용,부서코드, 부서장, 수정계획, 수정기한
            pParam[0] = "@self_audit_year:" + data.self_audit_year;
            pParam[1] = "@self_audit_no:" + data.self_audit_no;
            pParam[2] = "@self_audit_ca_o_contents:" + data.self_audit_ca_o_contents;
            pParam[3] = "@self_audit_ca_o_date:" + data.self_audit_ca_o_date;
            pParam[4] = "@self_audit_ca_o_emp:" + data.self_audit_ca_o_emp;
            pParam[5] = "@self_audit_ca_plan_contents:" + data.self_audit_ca_plan_contents;
            pParam[6] = "@self_audit_ca_dept_emp:" + data.self_audit_ca_dept_emp;
            pParam[7] = "@self_audit_ca_plan_limit:" + data.self_audit_ca_plan_limit;
            pParam[8] = "@self_audit_ca_dept:" + data.self_audit_ca_dept;
            pParam[9] = "@self_audit_ca_id:" + data.self_audit_ca_id;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string EditPlanOKDelete(SelfAuditCa data)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "CA_D";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + data.self_audit_year;
            pParam[1] = "@self_audit_no:" + data.self_audit_no;
            pParam[2] = "@self_audit_ca_id:" + data.self_audit_ca_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string EditPlanOKCommentInsert_c(string self_audit_year, string audit_no, string c_comment)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "UC1";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_cc_comments:" + c_comment;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string EditPlanOKCommentInsert_m(string self_audit_year, string audit_no, string c_comment)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "UC2";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_c_comments:" + c_comment;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal DataTable EditPlanOKFileSearch(SelfAudit data)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string gubun = "DOC_S";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + data.self_audit_year;
            pParam[1] = "@self_audit_no:" + data.audit_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }


        internal string EditPlanOKFileAdd(byte[] myBytes, string fileName, string contentType, SelfAudit list)
        {
            var doc_type = Path.GetExtension(fileName);

            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "DOC_I";
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


        internal string EditPlanOKFileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "DOC_D";
            string[] pParam = new string[4];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_file:" + audit_file;
            pParam[3] = "@doc_file_id:" + file_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal DataTable EditPlanOKFileOpen(string file_id)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "DOC_O";
            string pParam = "@doc_file_id:" + file_id;

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











        internal DataTable ResultDetailSearch(string audit_year, string audit_no)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "S3";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal string ResultReturnStatus(string audit_year, string audit_no, string audit_dept_cd, string audit_auditor)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "RETURN";
            string[] pParam = new string[4];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_dept_cd:" + audit_dept_cd;
            pParam[3] = "@self_audit_auditor:" + audit_auditor;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string ResultSave(string audit_year, string audit_no, string audit_result, string audit_sepcial)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "SDRR";
            string[] pParam = new string[4];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_doc_revision_result:" + audit_result;
            pParam[3] = "@self_audit_result_special_detail:" + audit_sepcial;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal DataTable ResultCheckListSearch(string audit_year, string audit_no)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "S4";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal DataTable ResultRevisionSearch(string audit_year, string audit_no)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "S5";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal string ResultRevisionSave(string audit_year, string audit_no, SelfAuditCa data)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "CA_U";
            string[] pParam = new string[10];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_ca_o_contents:" + data.self_audit_ca_o_contents;
            pParam[3] = "@self_audit_ca_o_date:" + data.self_audit_ca_o_date;
            pParam[4] = "@self_audit_ca_o_emp:" + data.self_audit_ca_o_emp;
            pParam[5] = "@self_audit_ca_plan_contents:" + data.self_audit_ca_plan_contents;
            pParam[6] = "@self_audit_ca_dept_emp:" + data.self_audit_ca_dept_emp;
            pParam[7] = "@self_audit_ca_plan_limit:" + data.self_audit_ca_plan_limit;
            pParam[8] = "@self_audit_ca_dept:" + data.self_audit_ca_dept;
            pParam[9] = "@self_audit_ca_id:" + data.self_audit_ca_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string ResultRevisionDelete(string audit_year, string audit_no, SelfAuditCa data)
        {
            string sSpName = "SP_SelfAuditEditPlanOK";
            string sGubun = "CA_D";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_ca_id:" + data.self_audit_ca_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }
    }
}