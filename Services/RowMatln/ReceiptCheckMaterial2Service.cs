using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.RowMatln;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.RowMatln
{
    public class ReceiptCheckMaterial2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ReceiptCheckMaterial2";

        internal DataTable Select(ReceiptCheckMaterial2 model)
        {
            string sGubun = "S1";
            string[] param = new string[3];

            param[0] = "@s_receipt_check_no:" + model.receipt_check_no;    //입고검수번호
            param[1] = "@option:" + model.option; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            param[2] = "@option2:" + model.option2; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable SelectList(ReceiptCheckMaterial2 model)
        {
            string sGubun = "setPurchase";
            string[] param = new string[2];

            param[0] = "@s_purchase_order_no:" + model.purchase_order_no; //발주번호
            param[1] = "@s_purchase_order_seq:" + model.purchase_order_seq; //발주번호 시퀀스

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable SelectList_Load(ReceiptCheckMaterial2 model)
        {
            string sGubun = "S";
            string[] param = new string[3];

            param[0] = "@receipt_check_rpt_no:" + model.receipt_check_no;
            param[1] = "@option:" + model.option; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
            param[2] = "@option2:" + model.option2; // 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal Boolean ReceiptCheckStatus(ReceiptCheckMaterial2 model, string receipt_check_rpt_id)
        {
            string sGubun = "ReceiptCheck";
            string[] param = new string[2];            

            param[0] = "@receipt_check_rpt_no:" + model.receipt_check_no;
            param[1] = "@receipt_check_rpt_id:" + receipt_check_rpt_id;

            DataTable dt = new DataTable();

            dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["receipt_status"].ToString() == "1")
                {
                     return false;      //1이면 입고완료 상태이므로 '삭제'만 가능
                }
                else
                {                   
                    return true;        //입고완료된 상태가 아니면 '수정', '삭제' 가능
                }
            }
            return true;
        }

        internal DataTable Overlap_check_no(ReceiptCheckMaterial2 model)
        {
            string sGubun = "Overlap_check_no";
            string[] param = new string[1];

            param[0] = "@receipt_check_rpt_no:" + model.receipt_check_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;

        }

        internal DataTable workerPopup(string pSpName)
        {
            string strsql = "SELECT emp_cd, emp_nm, ";
            strsql += "dept_cd, dept_nm ";
            strsql += "FROM V_EMPLOYEE ";
            strsql += "WHERE (emp_cd  LIKE '%%' OR emp_nm  LIKE '%%') ";
            strsql += "AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal string ReceiptCheckMaterial2_TRX(ReceiptCheckMaterial2 model, string gubun, string receipt_check_rpt_id)
        {
            string sGubun = "";
            string message = "";

            if (gubun == "I" || gubun == "U")
            {
                sGubun = gubun;

                if (model.receipt_check_pack_form == null || model.receipt_check_pack_form == "")
                    model.receipt_check_pack_form = "0";
                if (model.receipt_check_qty == null || model.receipt_check_qty == "")
                    model.receipt_check_qty = "0";
                if (model.receipt_check_unit_wt == null || model.receipt_check_unit_wt == "")
                    model.receipt_check_unit_wt = "0";

                string[] param = new string[16];
                param[0] = "@receipt_check_rpt_no:" + model.receipt_check_no;                               //입고검수번호
                param[1] = "@receipt_check_rpt_id:" + receipt_check_rpt_id;                                 //검수순번
                param[2] = "@receipt_check_state:" + model.receipt_check_state;                             //검수결과
                param[3] = "@receipt_check_rpt_dt:" + model.receipt_check_date;                             //검수일자  
                param[4] = "@pack_form:" + model.receipt_check_pack_form;                                   //포장내역  
                param[5] = "@receipt_check_rpt_qty:" + Convert.ToDecimal(model.receipt_check_qty);          //검수수량
                param[6] = "@receipt_check_rpt_net_qty:" + Convert.ToInt32(model.receipt_check_unit_wt);    //표시된중량
                param[7] = "@worker_emp_cd:" + model.worker_cd;                                             //작업자
                param[8] = "@checker_emp_cd:" + model.checker_cd;                                           //확인자
                param[9] = "@receipt_check_remark:" + model.remark;                                         //비고
                param[10] = "@log_user_id :" + HttpContext.Current.Session["loginCD"];
                param[11] = "@purchase_order_no:" + model.purchase_order_no;
                param[12] = "@purchase_order_seq:" + model.purchase_order_seq;
                param[13] = "@purchase_status:" + model.purchase_status;
                param[14] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
                param[15] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);

            }else if(gubun == "D")
            {
                sGubun = gubun;

                string[] param = new string[4];

                param[0] = "@receipt_check_rpt_no:" + model.receipt_check_no;                              //입고검수번호
                param[1] = "@purchase_order_no:" + model.purchase_order_no;                                //발주번호
                param[2] = "@purchase_order_seq:" + model.purchase_order_seq;                              //발주번호seq
                param[3] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);

            } 

            return message;
        }
        
        internal string ReceiptCheckMaterial2_batch(List<ReceiptCheckMaterial2> receiptCheckMaterial2, string gubun, string receipt_check_rpt_id)
        {
            string sGubun = "";
            string message = "";

            for(int i = 0; i < receiptCheckMaterial2.Count; i++)
            {
                if (receiptCheckMaterial2[i].gubun.Equals("ICHK") || receiptCheckMaterial2[i].gubun.Equals("UCHK"))
                {
                    //sGubun = "ICHK";
                    sGubun = receiptCheckMaterial2[i].gubun;
                    string[] param = new string[11];

                    param[0] = "@purchase_order_no:" + receiptCheckMaterial2[i].purchase_order_no;              //발주번호
                    param[1] = "@receipt_check_rpt_no:" + receiptCheckMaterial2[i].receipt_check_no;            //입고검수번호
                    param[2] = "@receipt_check_rpt_id:" + receipt_check_rpt_id;                                 //검수순번
                    param[3] = "@receipt_check_seq:" + receiptCheckMaterial2[i].receipt_check_seq;              //순번
                    param[4] = "@receipt_check_contents:" + receiptCheckMaterial2[i].receipt_check_contents;    //SOP내용
                    param[5] = "@receipt_check_result:" + receiptCheckMaterial2[i].receipt_check_result;        //검수결과
                    param[6] = "@receipt_check_fix:" + receiptCheckMaterial2[i].receipt_check_fix;              //조치사항
                    param[7] = "@purchase_order_seq:" + receiptCheckMaterial2[i].purchase_order_seq;
                    param[8] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
                    param[9] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
                    param[10] = "@log_user_id :" + HttpContext.Current.Session["loginCD"];                      //20101003박민준추가

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
                }
            }
            return message;
        }


    }
}