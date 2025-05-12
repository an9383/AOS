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
    public class DeviationInvestigationService
    {

        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string spName = "SP_DeviationInvestigation";


        // 메인 그리드 데이터 조회
        internal DataTable Select(string sDate, string eDate, string charge_emp, string status)
        {

            string gubun = "S1";
            string[] param = new string[4];

            param[0] = "@search_start_date:" + sDate;
            param[1] = "@search_end_date:" + eDate;
            param[2] = "@survey_charge_emp:" + charge_emp;
            param[3] = "@deviation_survey_status:" + status;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        // 첨부파일 조회
        internal DataTable FileSearch(string deviation_no, string deviation_survey_id)
        {
            string gubun = "S2";
            string[] param = new string[2];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_survey_id:" + deviation_survey_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        // 일탈조사 등록
        internal string DeviationInvestigationSave(string deviation_no, DeviationSurvey data)
        {
            string gubun = "U";
            string[] param = new string[6];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_survey_id:" + data.deviation_survey_id;
            param[2] = "@survey_result:" + data.survey_result;
            param[3] = "@survey_result_type:" + data.survey_result_type;
            param[4] = "@survey_cause:" + data.survey_cause;
            param[5] = "@necessary_work:" + data.necessary_work;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 일탈조사 삭제
        internal string DeviationInvestigationDelete(string deviation_no, string deviation_survey_id)
        {
            string gubun = "D";
            string[] param = new string[2];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_survey_id:" + deviation_survey_id;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 파일 첨부
        internal string FileAdd(byte[] myBytes, string fileName, string deviation_no, string deviation_survey_id)
        {
            var doc_type = Path.GetExtension(fileName);

            string gubun = "F";
            string[] param = new string[2];
            param[0] = "@doc_name:" + fileName;
            param[1] = "@doc_type:" + doc_type;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, "@file_image", myBytes, param);

            gubun = "F1";
            param = new string[3];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_survey_id:" + deviation_survey_id;
            param[2] = "@doc_file_id:" + message;

            message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 파일 열기
        internal DataTable FileOpen(string doc_file_id)
        {
            string gubun = "FS";
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


        // 파일 삭제
        internal string FileDelete(string deviation_no, string deviation_survey_id, string deviation_survey_data_id, string doc_file_id)
        {
            string gubun = "D_DOC";
            string[] param = new string[4];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_survey_id:" + deviation_survey_id;
            param[2] = "@deviation_survey_data_id:" + deviation_survey_data_id;
            param[3] = "@doc_file_id:" + doc_file_id;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 파일 비고 수정
        internal string UpdateFileComment(string doc_file_id, string file_remark)
        {
            string message = "";

            string gubun = "UC";
            string[] param = new string[2];

            param[0] = "@doc_file_id:" + doc_file_id;
            param[1] = "@deviation_survey_data_remark:" + file_remark;

            message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 서명정보 조회
        internal DataTable SignSearch(string deviation_no, string deviation_survey_id, string sign_set_cd)
        {
            string gubun = "S3";
            string[] param = new string[3];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_survey_id:" + deviation_survey_id;
            param[2] = "@sign_set_cd:" + sign_set_cd;

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
        internal string SignSave(string deviation_no, string deviation_survey_id, string survey_emp_cd, string deviation_sign_set_seq)
        {
            string gubun = "ES";

            string[] param = new string[5];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_survey_id:" + deviation_survey_id;
            param[2] = "@survey_emp_cd:" + survey_emp_cd;
            param[3] = "@survey_emp_type:" + "2";
            param[4] = "@sign_set_cd:" + deviation_sign_set_seq;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);



            gubun = "CK";
            string param2 = "@deviation_no:" + deviation_no;

            message = _bllSpExecute.SpExecuteString(spName, gubun, param2);

            return message;
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