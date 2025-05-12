using HACCP.Libs.Database;
using HACCP.Models.Dep;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Dep
{
    public class InvestigationOrderService
    {

        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string spName = "SP_InvestigationOrder";


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

        
        // 발생보고서 조회
        internal DataTable CapaSearch(string deviation_no)
        {

            string gubun = "S2";
            string param = "@deviation_no:" + deviation_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        // 발생보고서 입력
        internal string CapaInsert(string deviation_no, string request_emp_cd, string request_date, string request_contents, string request_level, string despatch_limit, string despatch_charge_emp_cd)
        {
            string gubun = "I1";
            string[] param = new string[7];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@request_emp_cd:" + request_emp_cd;
            param[2] = "@request_date:" + request_date;
            param[3] = "@request_contents:" + request_contents;
            param[4] = "@request_level:" + request_level;
            param[5] = "@despatch_charge_emp:" + despatch_charge_emp_cd;
            param[6] = "@despatch_limit:" + despatch_limit;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 발생보고서 수정
        internal string CapaEdit(string deviation_no, string request_emp_cd, string request_date, string request_contents, string request_level, string despatch_limit, string despatch_charge_emp, string deviation_capa_id)
        {
            string gubun = "U1";
            string[] param = new string[8];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@request_emp_cd:" + request_emp_cd;
            param[2] = "@request_date:" + request_date;
            param[3] = "@request_contents:" + request_contents;
            param[4] = "@request_level:" + request_level;
            param[5] = "@despatch_charge_emp:" + despatch_charge_emp;
            param[6] = "@despatch_limit:" + despatch_limit;
            param[7] = "@deviation_capa_id:" + deviation_capa_id;


            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 발생보고서 삭제
        internal string CapaDelete(string deviation_no, string deviation_capa_id)
        {
            string gubun = "D1";
            string[] param = new string[2];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_capa_id:" + deviation_capa_id;


            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 조치계획서 조회
        internal DataTable ServeySearch(string deviation_no)
        {

            string gubun = "S3";
            string param = "@deviation_no:" + deviation_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        // 조치계획서 입력
        internal string ServeyInsert(string deviation_no, string survey_order_emp, string survey_order_date, string survey_charge_emp, string survey_limit, string survey_contents)
        {
            string gubun = "I2";
            string[] param = new string[6];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@survey_order_emp:" + survey_order_emp;
            param[2] = "@survey_order_date:" + survey_order_date;
            param[3] = "@survey_charge_emp:" + survey_charge_emp;
            param[4] = "@survey_limit:" + survey_limit;
            param[5] = "@survey_contents:" + survey_contents;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 조치계획서 입력
        internal string ServeyEdit(string deviation_no, string survey_order_emp, string survey_order_date, string survey_charge_emp, string survey_limit, string deviation_survey_id, string survey_contents)
        {
            string gubun = "U2";
            string[] param = new string[7];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@survey_order_emp:" + survey_order_emp;
            param[2] = "@survey_order_date:" + survey_order_date;
            param[3] = "@survey_charge_emp:" + survey_charge_emp;
            param[4] = "@survey_limit:" + survey_limit;
            param[5] = "@deviation_survey_id:" + deviation_survey_id;
            param[6] = "@survey_contents:" + survey_contents;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 조치계획서 삭제
        internal string ServeyDelete(string deviation_no, string deviation_survey_id)
        {
            string gubun = "D2";
            string[] param = new string[2];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_survey_id:" + deviation_survey_id;


            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 서명정보 조회
        internal DataTable DisplayESInfo(string deviation_no, string sign_set_cd)
        {
            string gubun = "Sign1";
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
        internal string InvestigationOrderSignSave(string deviation_no, string emp_cd)
        {
            string gubun = "ES";

            string[] param = new string[3];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_survey_order_emp:" + emp_cd;
            param[2] = "@deviation_survey_order_type:" + "2";


            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 서명 삭제
        internal string InvestigationOrderSignDelete(string deviation_no)
        {
            string gubun = "DEL_ES";

            string param = "@deviation_no:" + deviation_no;


            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }







        // 일탈등록 정보 심각도 업데이트
        internal string updateLevel(string deviation_no, string deviation_level)
        {
            string gubun = "U";
            string[] param = new string[2];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@deviation_level:" + deviation_level;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

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


        // 사업장 조회
        internal DataTable getWorkRoomPopupData()
        {
            string strsql = "SELECT workroom_cd, workroom_nm";
            strsql += " FROM workroom";
            strsql += " WHERE (workroom_cd  LIKE '%%' OR workroom_nm  LIKE '%%')";
            strsql += " AND workroom_cd  LIKE '%%' AND workroom_nm  LIKE '%%'";


            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt; ;
        }


        // 제퓸 조회
        internal DataTable getItemPopupData()
        {
            string strsql = "SELECT item_cd, item_nm";
            strsql += " FROM v_item_standard";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";


            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt; ;
        }


        // 사업장 조회
        internal DataTable getTestItemPopupData()
        {
            string strsql = "SELECT testitem_cd, testitem_nm";
            strsql += " FROM v_testitemmaster";
            strsql += " WHERE (testitem_cd  LIKE '%%' OR testitem_nm  LIKE '%%')";
            strsql += " AND testitem_cd  LIKE '%%' AND testitem_nm  LIKE '%%'";


            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt; ;
        }


        // 사업장 조회
        internal DataTable getEquipPopupData()
        {
            string strsql = "SELECT equip_cd, equip_nm";
            strsql += " FROM equipment";
            strsql += " WHERE (equip_cd  LIKE '%%' OR equip_nm  LIKE '%%')";
            strsql += " AND equip_cd  LIKE '%%' AND equip_nm  LIKE '%%'";


            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt; ;
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
        #endregion 팝업관련 END


    }
}