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
    public class SelfAuditPlanService
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

        public DataTable Select(string sDate, string eDate, string emp_cd, string step, string status)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "S1";

            string[] pParam = new string[5];
            pParam[0] = "@de_Sdate:" + sDate;
            pParam[1] = "@de_Edate:" + eDate;
            pParam[2] = "@emp_cd:" + emp_cd;
            pParam[3] = "@self_audit_step:" + "%" + step;
            pParam[4] = "@self_audit_step_status:" + "%" + status;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal DataTable SelectSignInfo(string audit_year, string audit_no, string sign_set_cd)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
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


        internal DataTable FileSearch(string self_audit_year, string audit_no)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string gubun = "DOC_S";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal string FileAdd(byte[] myBytes, string fileName, string contentType, SelfAudit list)
        {
            var doc_type = Path.GetExtension(fileName);

            string sSpName = "SP_SelfAuditPlan_Y";
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


        internal DataTable FileOpen(string file_id)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
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


        internal string FileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "DOC_D";
            string[] pParam = new string[4];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_file:" + audit_file;
            pParam[3] = "@file_id:" + file_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal DataTable teamSearch(string audit_year, string audit_no)
        {
            DataTable dt;

            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "S3";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;

            dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal string teamNameSetting(string audit_year, string audit_no, string team_nm)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "TEAM_NM_U";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_team_nm:" + team_nm;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string MemberInsert(string audit_year, string audit_no, string input_team_cd, string input_team_role)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "TEAM_U";
            string[] pParam = new string[4];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_auditor:" + input_team_cd;
            pParam[3] = "@self_audit_auditor_role:" + input_team_role;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string MemberDelete(string audit_year, string audit_no, string input_team_cd)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "TEAM_D";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_auditor:" + input_team_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal DataTable DeptSearch(string audit_year, string audit_no)
        {
            DataTable dt;

            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "S4";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;

            dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string DeptInsert(string self_audit_year, string audit_no, string dept_cd, string dept_emp_cd, string dept_date, string dept_object)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "DEPT_U";
            string[] pParam = new string[6];

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_dept_cd:" + dept_cd;
            pParam[3] = "@self_audit_dept_emp_cd:" + dept_emp_cd;
            pParam[4] = "@self_audit_plan_date:" + dept_date;
            pParam[5] = "@self_audit_plan_object:" + dept_object;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string DeptDelete(string audit_year, string audit_no, string dept_cd)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "DEPT_D";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_dept_cd:" + dept_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal DataTable AuditorSearch(string self_audit_year, string audit_no, string dept_cd)
        {
            DataTable dt;

            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "S5";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_dept_cd:" + dept_cd;

            dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string AuditorInsert(string self_audit_year, string audit_no, string dept_cd, string[] auditor_cd)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "AUDITOR_U";
            string[] pParam = new string[5];
            string message = "";

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_dept_cd:" + dept_cd;

            for (int i = 0; i < auditor_cd.Length; i++)
            {
                pParam[3] = "@self_audit_auditor:" + auditor_cd[i];
                pParam[4] = "@self_audit_status:" + "";
            
                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }




            return message;
        }

        internal string AuditorAllDelete(string self_audit_year, string audit_no, string dept_cd)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "AUDITOR_All_D";
            string[] pParam = new string[3];
            string message = "";

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_dept_cd:" + dept_cd;

            message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string SaveSign(string audit_year, string audit_no, string emp_cd, string check_gb, string audit_date, string type)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "ES";

            string[] pParam = new string[7];
            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_pc_emp_cd:";
            pParam[3] = "@self_audit_pc_emp_type:" + "2";
            pParam[4] = "@self_audit_p_emp_cd:";
            pParam[5] = "@self_audit_p_emp_type:" + "2";
            pParam[6] = "@type:" + type;


            if (type == "pc")
            {
                pParam[2] = "@self_audit_pc_emp_cd:" + emp_cd;
            }
            else
            {
                pParam[4] = "@self_audit_p_emp_cd:" + emp_cd;
            }

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            if(type == "p")
            {
                sGubun = "Create_Record";
                pParam[2] = "@self_audit_check_gb:" + check_gb;
                pParam[3] = "@self_audit_date:" + audit_date;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return message;
        }

        internal string DeleteSign(string audit_year, string audit_no, string emp_cd, string check_gb, string type)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "ES_D";

            string[] pParam = new string[3];
            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@type:" + type;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            if (type == "p")
            {
                sGubun = "Delete_Record";

                pParam = new string[4];
                pParam[0] = "@self_audit_year:" + audit_year;
                pParam[1] = "@self_audit_no:" + audit_no;
                pParam[2] = "@self_audit_p_emp_cd:" + emp_cd;
                pParam[3] = "@self_audit_check_gb:" + check_gb;


                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }


            return message;
        }

        internal string InsertCComment(string self_audit_year, string audit_no, string c_comment)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "UC1";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_pc_comments:" + c_comment;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string InsertMComment(string self_audit_year, string audit_no, string m_comment)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "UC2";
            string[] pParam = new string[3];

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_p_comments:" + m_comment;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string CheckAuditor(string self_audit_year, string audit_no)
        {
            string sSpName = "SP_SelfAuditPlan_Y";
            string sGubun = "AUDITOR_C";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + self_audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }
    }
}