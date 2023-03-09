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
    public class Deviation_CapaService
    {

        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string spName = "SP_Deviation_Capa_Y";


        // 메인 그리드 데이터 조회
        internal DataTable Select(string sDate, string eDate, string emp_cd, string status)
        {

            string gubun = "S";
            string[] param = new string[4];

            param[0] = "@s_sdate:" + sDate;
            param[1] = "@s_edate:" + eDate;
            param[2] = "@s_despatch_charge_emp:" + emp_cd;
            param[3] = "@s_deviation_capa_status:" + status;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        // 예방조치 수정
        internal string Deviation_CapaSave(string request_contents, string despatch_date, string despatch_emp_cd, string despatch_contents, string deviation_capa_status, string deviation_capa_id)
        {
            string gubun = "U";
            string[] param = new string[6];

            param[0] = "@request_contents:" + request_contents;
            param[1] = "@despatch_date:" + despatch_date;
            param[2] = "@despatch_emp_cd:" + despatch_emp_cd;
            param[3] = "@despatch_contents:" + despatch_contents;
            param[4] = "@deviation_capa_status:" + deviation_capa_status;
            param[5] = "@deviation_capa_id:" + deviation_capa_id;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 예방조치 삭제
        internal string Deviation_CapaDelete(string deviation_capa_id)
        {
            string gubun = "D";
            string param = "@deviation_capa_id:" + deviation_capa_id;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }




        // 서명정보 조회
        internal DataTable SignSearch(string deviation_capa_id, string sign_set_cd)
        {
            string gubun = "S3";
            string[] param = new string[2];
            param[0] = "@deviation_capa_id:" + deviation_capa_id;
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
        internal string SignSave(string deviation_capa_id, string despatch_emp_cd)
        {
            string message = "";

            string gubun = "ES";

            string[] param = new string[3];

            param[0] = "@deviation_capa_id:" + deviation_capa_id;
            param[1] = "@despatch_emp_cd:" + despatch_emp_cd;
            param[2] = "@despatch_emp_type:" + "2";

            message = _bllSpExecute.SpExecuteString(spName, gubun, param);

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