using HACCP.Libs.Database;
using HACCP.Models.Dep;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace HACCP.Services.Dep
{
    public class ConfirmDeviationProcessService
    {

        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string spName = "SP_ConfirmDeviationProcess_Y";


        // 메인 그리드 데이터 조회
        internal DataTable Select(string sDate, string eDate, string type, string level, string status)
        {

            string gubun = "S1";
            string[] param = new string[5];

            param[0] = "@search_start_date:" + sDate;
            param[1] = "@search_end_date:" + eDate;
            param[2] = "@deviation_type:" + type;
            param[3] = "@deviation_level:" + level;
            param[4] = "@deviation_status:" + status;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        internal DataTable request2Search(string deviation_no)
        {
            string gubun = "S2";
            string param = "@deviation_no:" + deviation_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        internal DataTable ConfirmDeviationProcessDeviationSearch(string deviation_no)
        {
            string gubun = "S3";
            string param = "@deviation_no:" + deviation_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        internal DataTable resultSearch(string deviation_no)
        {
            string gubun = "S4";
            string param = "@deviation_no:" + deviation_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        internal DataTable requestSearch(string deviation_no)
        {
            string gubun = "S5";
            string param = "@deviation_no:" + deviation_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        // 예방조치 입력
        internal string ResultSave(string deviation_no, string request_emp_cd, string request_date, string request_level, string despatch_limit, string despatch_charge_emp, string request_contents)
        {
            string gubun = "I";
            string[] param = new string[7];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@request_emp_cd:" + request_emp_cd;
            param[2] = "@request_date:" + request_date;
            param[3] = "@request_level:" + request_level;
            param[4] = "@despatch_limit:" + despatch_limit;
            param[5] = "@despatch_charge_emp:" + despatch_charge_emp;
            param[6] = "@request_contents:" + request_contents;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 예방조치 입력
        internal string ConfirmDeviationProcessSave(string deviation_no, string deviation_result_yn, string deviation_result, string deviation_end_date, string deviation_inquiry_contents, string deviation_inquiry_cause, string deviation_result_remark)
        {
            string gubun = "U";
            string[] param = new string[7];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_result_yn:" + deviation_result_yn;
            param[2] = "@deviation_result:" + deviation_result;
            param[3] = "@deviation_end_date:" + deviation_end_date;
            param[4] = "@deviation_inquiry_contents:" + deviation_inquiry_contents;
            param[5] = "@deviation_inquiry_cause:" + deviation_inquiry_cause;
            param[6] = "@deviation_result_remark:" + deviation_result_remark;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 처리결과/예방조치 삭제
        internal string RequestCheck(string deviation_no)
        {
            string gubun = "C";
            string param = "@deviation_no:" + deviation_no;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }




        // 서명정보 조회
        internal DataTable SignSearch(string deviation_no, string sign_set_cd)
        {
            string gubun = "SD";
            string[] param = new string[2];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@sign_set_cd:" + sign_set_cd;


            DataSet ds = _bllSpExecute.SpExecuteDataSet(spName, gubun, param);
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


        internal DataTable SignSearch_Q(string deviation_no, string sign_set_cd)
        {
            string gubun = "SD2";
            string[] param = new string[2];
            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@sign_set_cd:" + sign_set_cd;


            DataSet ds = _bllSpExecute.SpExecuteDataSet(spName, gubun, param);
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


        // 서명 저장
        internal string SignSave(string deviation_no, string emp_cd, string type)
        {
            string message = "";

            if(type == "M")
            {
                string gubun = "ES";

                string[] param = new string[3];

                param[0] = "@deviation_no:" + deviation_no;
                param[1] = "@deviation_m_emp_cd:" + emp_cd;
                param[2] = "@deviation_m_emp_type:" + "2";

                message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            }
            else if(type == "Q")
            {
                string gubun = "ES2";

                string[] param = new string[3];

                param[0] = "@deviation_no:" + deviation_no;
                param[1] = "@deviation_q_emp_cd:" + emp_cd;
                param[2] = "@deviation_q_emp_type:" + "2";

                message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            }

            return message;
        }




        // 처리결과/예방조치 삭제
        internal string ConfirmDeviationProcessDelete(string deviation_no)
        {
            string gubun = "D";
            string param = "@deviation_no:" + deviation_no;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }



        // 첨부파일 조회
        internal DataTable FileSearch(string deviation_no)
        {
            string gubun = "FS";
            string[] param = new string[1];

            param[0] = "@deviation_no:" + deviation_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        // 첨부파일 다운로드
        internal DataTable FileOpen(string doc_file_id)
        {
            string gubun = "FO";
            string param = "@doc_file_id:" + doc_file_id;


            DataSet ds = _bllSpExecute.SpExecuteDataSet(spName, gubun, param);

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


        #region 팝업관련
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

        #endregion 팝업관련 END


    }
}