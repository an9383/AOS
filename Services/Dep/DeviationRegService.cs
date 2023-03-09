using HACCP.Libs.Database;
using HACCP.Models.Dep;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Dep
{
    public class DeviationRegService
    {

        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string spName = "SP_DeviationReg_Y";


        // 메인 그리드 데이터 조회
        internal DataTable Select(string sDate, string eDate, string emp_cd, string status)
        {

            string gubun = "S";
            string[] param = new string[4];

            param[0] = "@de_date_S:" + sDate;
            param[1] = "@de_date_E:" + eDate;
            param[2] = "@detect_emp_cd:" + emp_cd;
            param[3] = "@deviation_status:" + status;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

            return dt;
        }


        // 일탈 입력
        internal string DeviationRegSave(Deviation data)
        {

            string gubun = "I";
            string[] param = new string[20];

            param[0] = "@deviation_no:" + data.deviation_no;
            param[1] = "@deviation_title:" + data.deviation_title;
            param[2] = "@detect_date:" + data.detect_date;
            param[3] = "@deviation_place:" + data.deviation_place;
            param[4] = "@detect_emp_cd:" + data.detect_emp_cd;
            param[5] = "@deviation_contents:" + data.deviation_contents;
            param[6] = "@deviation_date:" + data.detect_date;
            param[7] = "@detect_method:" + data.detect_method;
            param[8] = "@deviation_doc:" + data.deviation_doc;
            param[9] = "@deviation_type:" + data.deviation_type;
            param[10] = "@deviation_dept_cd:" + data.deviation_dept_cd;
            param[11] = "@deviation_ref_no:" + data.deviation_ref_no;
            param[12] = "@deviation_ref_process_cd:" + data.deviation_ref_process_cd;
            param[13] = "@deviation_ref_item_cd:" + data.deviation_ref_item_cd;
            param[14] = "@deviation_ref_test_item:" + data.deviation_ref_test_item;
            param[15] = "@deviation_ref_equip_cd:" + data.deviation_ref_equip_cd;
            param[16] = "@deviation_confirm_emp:" + data.deviation_confirm_emp;
            param[17] = "@deviation_status:" + "1";
            param[18] = "@deviation_early_respond:" + data.deviation_early_respond;
            param[19] = "@deviation_request_remark:" + data.deviation_request_remark;


            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 일탈 수정
        internal string DeviationRegEdit(Deviation data)
        {

            string gubun = "U";
            string[] param = new string[20];

            param[0] = "@deviation_no:" + data.deviation_no;
            param[1] = "@deviation_title:" + data.deviation_title;
            param[2] = "@detect_date:" + data.detect_date;
            param[3] = "@deviation_place:" + data.deviation_place;
            param[4] = "@detect_emp_cd:" + data.detect_emp_cd;
            param[5] = "@deviation_contents:" + data.deviation_contents;
            param[6] = "@deviation_date:" + data.detect_date;
            param[7] = "@detect_method:" + data.detect_method;
            param[8] = "@deviation_doc:" + data.deviation_doc;
            param[9] = "@deviation_type:" + data.deviation_type;
            param[10] = "@deviation_dept_cd:" + data.deviation_dept_cd;
            param[11] = "@deviation_ref_no:" + data.deviation_ref_no;
            param[12] = "@deviation_ref_process_cd:" + data.deviation_ref_process_cd;
            param[13] = "@deviation_ref_item_cd:" + data.deviation_ref_item_cd;
            param[14] = "@deviation_ref_test_item:" + data.deviation_ref_test_item;
            param[15] = "@deviation_ref_equip_cd:" + data.deviation_ref_equip_cd;
            param[16] = "@deviation_confirm_emp:" + data.deviation_confirm_emp;
            param[17] = "@deviation_status:" + "1";
            param[18] = "@deviation_early_respond:" + data.deviation_early_respond;
            param[19] = "@deviation_request_remark:" + data.deviation_request_remark;


            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 일탈 삭제
        internal string DeviationRegDelete(string deviation_no)
        {
            string gubun = "D";
            string param = "@deviation_no:" + deviation_no;

            string message = _bllSpExecute.SpExecuteString(spName, gubun, param);

            return message;
        }


        // 상급자 조회
        internal DataTable CheckConfirmEmp(string emp_cd)
        {

            string gubun = "S";
            string param = "@detect_emp_cd:" + emp_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, param);

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


        // 제조번호 조회
        internal DataTable DeviationRefNoSearch(string deviation_ref_item_cd)
        {
            string strsql = "SELECT a.lot_no, c.common_part_nm as order_status";
            strsql += " FROM work_order a";
            strsql += " JOIN item_standard b on a.item_cd = b.item_cd";
            strsql += " JOIN common c on a.order_status = c.common_part_cd and c.common_cd = 'RT005'";
            strsql += " WHERE a.item_cd = '" + deviation_ref_item_cd + "'";
            strsql += " ORDER BY order_date DESC";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt; ;
        }


        // 공정번호 조회
        internal DataTable DeviationRefProcessCDSearch(string deviation_ref_item_cd)
        {
            string strsql = "SELECT distinct a.process_cd, a.process_nm";
            strsql += " FROM process a";
            strsql += " JOIN item_process b on a.process_cd = b.process_cd";
            strsql += " WHERE b.item_cd = '" + deviation_ref_item_cd + "'";
            strsql += " ORDER BY a.process_cd";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt; ;
        }
        #endregion 팝업관련 END


    }
}