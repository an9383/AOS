using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.PrdOut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdOut
{
    public class DespatchManage2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable DespatchManage2_S1(DespatchManage2.SrchParam despatchManage2)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "S1";
            string[] pParam = new string[7];
            pParam[0] = "@s_cust_cd:" + despatchManage2.s_cust_cd;
            pParam[1] = "@s_sdate:" + despatchManage2.s_sdate;
            pParam[2] = "@s_edate:" + despatchManage2.s_edate;
            pParam[3] = "@s_issue_status:" + despatchManage2.s_issue_status;
            pParam[4] = "@search_ck:" + despatchManage2.search_ck;
            pParam[5] = "@s_sign_status:" + despatchManage2.s_sign_status;
            pParam[6] = "@item_cd:" + despatchManage2.item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DespatchManage2_S4(string despatch_no, string item_cd)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "S4";
            string[] pParam = new string[2];

            pParam[0] = "@despatch_no:" + despatch_no;
            pParam[1] = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DespatchManage2_S5(string despatch_no, string item_cd, string lot_no)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "S5";
            string[] pParam = new string[3];

            pParam[0] = "@despatch_no:" + despatch_no;
            pParam[1] = "@item_cd:" + item_cd;
            pParam[2] = "@lot_no:" + lot_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DespatchManage2_GetLotNo2(string item_cd, string check, string despatch_no, string lot_no, string receipt_status)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "GetLotNo2";
            string[] pParam = new string[5];
            
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@check:" + check;
            pParam[2] = "@despatch_no:" + despatch_no;
            pParam[3] = "@lot_no:" + lot_no;
            pParam[4] = "@receipt_status:" + receipt_status;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DespatchManage2_DespatchOrderOK(string DESPATCH_ORDER_NO, string ISSUE_DATE, string issue_work_seq)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "DespatchOrderOK";
            string[] pParam = new string[4];

            pParam[0] = "@despatch_no:" + DESPATCH_ORDER_NO;
            pParam[1] = "@issue_work_date:" + ISSUE_DATE;
            pParam[2] = "@issue_work_seq:" + issue_work_seq;
            pParam[3] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DespatchManage2_SelectLotNoCheck(string despatch_order_no)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "SelectLotNoCheck";
            string[] pParam = new string[1];

            pParam[0] = "@despatch_order_no:" + despatch_order_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DespatchManage2_StockQtyCheck(string despatch_order_no)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "StockQtyCheck";
            string[] pParam = new string[1];

            pParam[0] = "@despatch_order_no:" + despatch_order_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DespatchManage2_DespatchOk(string despatch_no, string issue_work_date)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "DespatchOk";
            string[] pParam = new string[3];

            pParam[0] = "@despatch_no:" + despatch_no;
            pParam[1] = "@issue_work_date:" + issue_work_date;
            pParam[2] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DespatchManage2_DespatchOrderSearch(string despatch_no)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "DespatchOrderSearch";
            string[] pParam = new string[1];

            pParam[0] = "@despatch_no:" + despatch_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DespatchManage2_DespatchOrderEnd2(string despatch_no, string item_cd, string item_issue_id, string box_barcode_no, string issue_qty, string issue_work_date, string keeping_status, string lot_no, string issue_date, string cust_cd)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "DespatchOrderEnd2";
            string[] pParam = new string[12];

            pParam[0] = "@despatch_no:" + despatch_no;
            pParam[1] = "@item_cd:" + item_cd;
            pParam[2] = "@item_issue_id:" + item_issue_id;
            pParam[3] = "@box_barcode_no:" + box_barcode_no;
            pParam[4] = "@issue_qty:" + issue_qty;
            pParam[5] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
            pParam[6] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
            pParam[7] = "@issue_work_date:" + DateTime.Today.ToShortDateString();
            pParam[8] = "@keeping_status:" + keeping_status;
            pParam[9] = "@lot_no:" + lot_no;
            pParam[10] = "@issue_date:" + issue_date;
            pParam[11] = "@cust_cd:" + cust_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string DespatchManage2_batch(List<DespatchManage2> despatchManage2, string gubun, string despatch_no, string Issue_Price, string box_barcode_no, string car_number, string issue_detail_date, string item_issue_id)
        {
            string sSpName = "SP_DespatchManage2";
            string sGubun = "";
            string message = "";

            for(int i = 0; i < despatchManage2.Count; i++)
            {
                if (despatchManage2[i].gubun.Equals("I2"))
                {
                    sGubun = "I2";
                    string[] pParam = new string[15];

                    pParam[0] = "@issue_detail_date:" + issue_detail_date;
                    pParam[1] = "@item_cd:" + despatchManage2[i].ITEM_CD;
                    pParam[2] = "@issue_qty:" + despatchManage2[i].ISSUE_QTY;
                    pParam[3] = "@lot_no:" + despatchManage2[i].lot_no;
                    pParam[4] = "@lot_date:" + despatchManage2[i].lot_date;
                    pParam[5] = "@end_date:" + despatchManage2[i].end_date;
                    pParam[6] = "@despatch_no:" + despatch_no;
                    pParam[7] = "@item_issue_id:" + item_issue_id;
                    pParam[8] = "@box_barcode_no:" + despatchManage2[i].box_barcode_no;
                    pParam[9] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
                    pParam[10] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
                    pParam[11] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];
                    pParam[12] = "@box_qty:" + despatchManage2[i].box_qty;

                    if (despatchManage2[i].keeping_status == "Y" || despatchManage2[i].keeping_status.Equals("true"))
                    {
                        pParam[13] = "@keeping_status:" + "Y";
                    }
                    else
                    {
                        pParam[13] = "@keeping_status:" + "N";
                    }

                    pParam[14] = "@Issue_Price:" + Issue_Price;         /*despatchManage2[i].Issue_Price;*/

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (despatchManage2[i].gubun.Equals("U3"))
                {
                    sGubun = "U3";
                    string[] pParam = new string[10];

                    pParam[0] = "@despatch_no:" + despatch_no;
                    pParam[1] = "@item_cd:" + despatchManage2[i].ITEM_CD;
                    pParam[2] = "@issue_qty:" + despatchManage2[i].ISSUE_QTY;
                    pParam[3] = "@lot_no:" + despatchManage2[i].lot_no;
                    pParam[4] = "@box_barcode_no:" + despatchManage2[i].box_barcode_no;
                    pParam[5] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
                    pParam[6] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];
                    pParam[7] = "@box_qty:" + despatchManage2[i].box_qty;
                    if (despatchManage2[i].keeping_status == "Y" || despatchManage2[i].keeping_status.Equals("true"))
                    {
                        pParam[8] = "@keeping_status:" + "Y";
                    }
                    else
                    {
                        pParam[8] = "@keeping_status:" + "N";
                    }
                    pParam[9] = "@Issue_Price:" + Issue_Price;         /*despatchManage2[i].Issue_Price;*/

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (despatchManage2[i].gubun.Equals("D3"))
                {
                    sGubun = "D3";
                    string[] pParam = new string[4];

                    pParam[0] = "@despatch_no:" + despatch_no;
                    pParam[1] = "@item_cd:" + despatchManage2[i].ITEM_CD;
                    pParam[2] = "@lot_no:" + despatchManage2[i].lot_no;
                    pParam[3] = "@box_barcode_no:" + despatchManage2[i].box_barcode_no;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (despatchManage2[i].gubun.Equals("U5"))
                {
                    sGubun = "U5";
                    string[] pParam = new string[2];

                    pParam[0] = "@box_barcode_no:" + despatchManage2[i].box_barcode_no;
                    pParam[1] = "@car_number:" + (String.IsNullOrEmpty(despatchManage2[i].car_number) ? "1" : despatchManage2[i].car_number);

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
            }

            return message;
        }

    }
}