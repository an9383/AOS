using System;
using System.Data;
using System.Linq;
using System.Web;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Insp;

namespace HACCP.Services.Insp
{
    public class SelfAuditCheckListService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 팝업에 필요한 사용자 리스트 조회
        /// </summary>
        /// <returns></returns>
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


        internal DataTable Select(string sCheck_gb, string sCheck_emp_cd)
        {
            string sSpName = "SP_SelfAuditCheckList";
            string sGubun = "S";            
            string gb = sCheck_gb.Equals("") ? "1" : sCheck_gb;

            string[] pParam = new string[2];
            pParam[0] = "@check_gb:" + gb;
            pParam[1] = "@check_emp_cd:" + sCheck_emp_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string Insert(SelfAuditCheckList list)
        {
            string sSpName = "SP_SelfAuditCheckList";
            string sGubun = "I";

            string[] pParam = new string[8];
            pParam[0] = "@check_gb:" + list.list_gubun;
            pParam[1] = "@check_range:" + list.list_range;
            pParam[2] = "@check_object:" + list.list_target;
            pParam[3] = "@check_seq:" + list.list_seq;
            pParam[4] = "@check_item:" + list.list_content;
            pParam[5] = "@check_emp_cd:" + list.list_wrtier_cd;
            pParam[6] = "@write_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[7] = "@write_emp_type:" + "1";

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string Update(SelfAuditCheckList list)
        {
            string sSpName = "SP_SelfAuditCheckList";
            string sGubun = "U";

            string[] pParam = new string[7];
            pParam[0] = "@check_item_no:" + list.list_no;
            pParam[1] = "@check_gb:" + list.list_gubun;
            pParam[2] = "@check_range:" + list.list_range;
            pParam[3] = "@check_object:" + list.list_target;
            pParam[4] = "@check_seq:" + list.list_seq;
            pParam[5] = "@check_item:" + list.list_content;
            pParam[6] = "@check_emp_cd:" + list.list_wrtier_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string Delete(string list_no)
        {
            string sSpName = "SP_SelfAuditCheckList";
            string sGubun = "D";

            string[] pParam = new string[1];
            pParam[0] = "@check_item_no:" + list_no;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal DataTable SelectSignInfo(string checkList_gubun, string sign_set_cd)
        {
            string sSpName = "SP_SelfAuditCheckList";
            string sGubun = "Check_List_Sign_Search";

            string[] pParam = new string[2];
            pParam[0] = "@check_gb:" + checkList_gubun;
            pParam[1] = "@sign_set_cd:" + sign_set_cd;

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

        internal string SaveSign(string check_gb, string write_emp_cd)
        {
            string sSpName = "SP_SelfAuditCheckList";
            string sGubun = "Check_List_Sign";

            string[] pParam = new string[3];
            pParam[0] = "@check_gb:" + check_gb;
            pParam[1] = "@write_emp_cd:" + write_emp_cd;
            pParam[2] = "@write_emp_type:" + "1";

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

    }
}
