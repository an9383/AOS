using HACCP.Libs.Database;
using HACCP.Models.Dep;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Dep
{
    public class DeviationReceiptService
    {

        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string spName = "SP_DeviationReceipt";


        // 메인 그리드 데이터 조회
        internal DataTable Select(string sDate, string eDate, string status)
        {

            string gubun = "S1";
            string[] param = new string[3];

            param[0] = "@s_sdate:" + sDate;
            param[1] = "@s_edate:" + eDate;
            param[2] = "@s_deviationStatus:" + status;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        // 일탈접수 수정
        internal string ReceiptEdit(Deviation data)
        {

            string gubun = "U1";
            string[] param = new string[10];

            param[0] = "@deviation_no:" + data.deviation_no;
            param[1] = "@deviation_yn:" + data.deviation_yn;
            param[2] = "@deviation_contents2:" + data.deviation_contents2;
            param[3] = "@deviation_confirm_date:" + data.deviation_confirm_date;
            param[4] = "@deviation_cause:" + data.deviation_cause;
            param[5] = "@deviation_ref_doc:" + data.deviation_ref_doc;
            param[6] = "@deviation_level:" + data.deviation_level;
            param[7] = "@deviation_class:" + data.deviation_class;
            param[8] = "@deviation_status:" + data.deviation_status;
            param[9] = "@deviation_receive_comment:" + data.deviation_receive_comment;


            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 일탈접수 삭제
        internal string ReceiptDelete(string deviation_no)
        {
            string gubun = "D1";
            string param = "@deviation_no:" + deviation_no;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 계획 조회
        internal DataTable DeviationReceiptCapaSearch(string deviation_no)
        {

            string gubun = "S2";
            string param = "@deviation_no:" + deviation_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        // 계획 입력
        internal string CapaInsert(string deviation_no, string request_emp_cd, string request_date, string request_contents, string request_level, string despatch_limit, string despatch_charge_emp_cd)
        {
            string gubun = "I";
            string[] param = new string[7];

            param[0] = "@deviation_no:" + deviation_no;
            param[1] = "@request_emp_cd:" + request_emp_cd;
            param[2] = "@request_date:" + request_date;
            param[3] = "@request_contents:" + request_contents;
            param[4] = "@request_level:" + request_level;
            param[5] = "@despatch_limit:" + despatch_limit;
            param[6] = "@despatch_charge_emp_cd:" + despatch_charge_emp_cd;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 계획 수정
        internal string CapaEdit(string @deviation_capa_id, string request_emp_cd, string request_date, string request_contents, string request_level, string despatch_limit, string despatch_charge_emp_cd)
        {
            string gubun = "U2";
            string[] param = new string[7];

            param[0] = "@deviation_capa_id:" + deviation_capa_id;
            param[1] = "@request_emp_cd:" + request_emp_cd;
            param[2] = "@request_date:" + request_date;
            param[3] = "@request_contents:" + request_contents;
            param[4] = "@request_level:" + request_level;
            param[5] = "@despatch_limit:" + despatch_limit;
            param[6] = "@despatch_charge_emp_cd:" + despatch_charge_emp_cd;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 일탈접수 삭제
        internal string CapaDelete(string deviation_capa_id)
        {
            string gubun = "D2";
            string param = "@deviation_capa_id:" + deviation_capa_id;

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