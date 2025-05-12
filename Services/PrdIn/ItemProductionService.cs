using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.PrdIn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdIn
{
    public class ItemProductionService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ItemProduction";

        internal DataTable Select(ItemProduction model)
        {
            string sGubun = "Select";
            string[] param = new string[5];

            param[0] = "@start_date_S:" + model.start_date_S;
            param[1] = "@end_date_S:" + model.end_date_S;
            //param[2] = "@item:" + model.item_cd_S;
            param[2] = "@item:" + model.item_nm_S;
            param[3] = "@lot_no_S:" + model.lot_no_S;
            param[4] = "@prod_return_ck_S:" + model.prod_return_S;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable s_item_Popup(string pSpName)
        {
            string strsql = "SELECT item_cd, item_nm";
            strsql += " FROM v_packingitem";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable s2_item_Popup(string pSpName)
        {
            string strsql = "SELECT item_cd, item_nm, vender_item_cd, item_s_nm";
            strsql += " FROM v_packingitem";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable s_vender_Popup(string pSpName)
        {
            string strsql = "SELECT vender_cd, vender_nm, license, owner_nm, phone, zipcode, address";
            strsql += " FROM vender";
            strsql += " WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%')";
            strsql += " AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable SelectBox(ItemProduction model)
        {
            string sGubun = "SelectBox";
            string[] param = new string[1];

            param[0] = "@item_stock_id:" + model.item_stock_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Lot_Popup(ItemProduction model)
        {
            string sGubun = "Lot_Popup";
            string[] param = new string[3];

            param[0] = "@start_date_S:" + model.start_date_S;
            param[1] = "@end_date_S:" + model.end_date_S;
            param[2] = "@item:" + model.item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal string CRUD(ItemProduction model)
        {
            string sGubun = "";
            string message = "";

            if (model.gubun == "Insert" || model.gubun == "Update")
            {
                sGubun = model.gubun;

                string[] param = new string[16];
                param[0] = "@prod_return_ck:" + model.prod_return_ck;
                param[1] = "@item_cd:" + model.item_cd;
                param[2] = "@lot_no:" + model.lot_no;
                param[3] = "@lot_date:" + model.lot_date;
                param[4] = "@receipt_date:" + model.receipt_date;
                param[5] = "@receipt_qty:" + model.receipt_qty;
                param[6] = "@end_date:" + model.end_date;
                param[7] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
                param[8] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
                param[9] = "@item_stock_id:" + model.item_stock_id;
                param[10] = "@log_user_id :" + HttpContext.Current.Session["loginCD"];
                param[11] = "@cust_lot_no:" + model.cust_lot_no;
                param[12] = "@outsource_amt:" + model.outsource_amt;
                param[13] = "@order_no:" + model.order_no;
                param[14] = "@outsource_base_amt:" + model.outsource_base_amt;
                param[15] = "@outsource_cust_cd:" + model.outsource_cust_cd;

                //message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
                DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

                message = "" + dt.Rows[0].ItemArray[0];
            }
            else if (model.gubun == "BoxInsert" || model.gubun == "BoxUpdate")
            {
                sGubun = model.gubun;
                string[] param = new string[7];
                param[0] = "@item_stock_id:" + model.item_stock_id;
                param[1] = "@prod_qty:" + model.prod_qty;
                param[2] = "@stock_qty:" + model.stock_qty;
                param[3] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
                param[4] = "@box_barcode_no:" + model.box_barcode_no; 
                param[5] = "@pallet_cd:" + model.pallet_cd;
                param[6] = "@log_user_id :" + HttpContext.Current.Session["loginCD"];

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }
            else if (model.gubun == "Delete")
            {
                sGubun = model.gubun;
                string[] param = new string[6];
                param[0] = "@item_stock_id:" + model.item_stock_id;
                param[1] = "@log_user_id :" + HttpContext.Current.Session["loginCD"];
                param[2] = "@purchase_order_no:" + model.purchase_order_no;
                param[3] = "@purchase_order_seq:" + model.purchase_order_seq;
                param[4] = "@purchase_order_rec:" + model.purchase_order_rec;
                param[5] = "@receipt_qty:" + model.receipt_qty;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }

            return message;
        }

        internal string TestReg(ItemProduction model)
        {
            string gubun = "TestReg"; // SP의 구분값
            
            string res = ""; // 리턴메시지            

            string[] param = new string[17];
            if (model.prod_return_ck == "4" || model.prod_return_ck == "5")
            {
                param[0] = "@test_type:" + "42"; //dlg.test_type;   //시험구분 QC004 : 외주완제품
            }
            else if (model.prod_return_ck == "1" || model.prod_return_ck == "3")
            {
                param[0] = "@test_type:" + "6"; //dlg.test_type;   //시험구분 QC004 : 완제품
            }
            else if(model.prod_return_ck == "2")
            {
                param[0] = "@test_type:" + "17"; //dlg.test_type;   //시험구분 QC004 : 기타완제품
            }

            param[1] = "@item_cd:" + model.item_cd; //제품코드
            param[2] = "@start_date:" + model.lot_date; // 제조일자
            param[3] = "@start_no:" + model.lot_no; // 제조번호
            //param[4] = "@receive_date:" + model.TestRequestDate;  // 시험접수일자
            //param[5] = "@picking_date:" + model.TestRequestDate;
            param[4] = "@receive_date:" + DateTime.Today.ToShortDateString();  // 시험접수일자            
            param[5] = "@picking_date:" + DateTime.Today.ToShortDateString();
            param[6] = "@picking_emp_cd:" + HttpContext.Current.Session["loginCD"];
            param[7] = "@test_sample_qty:" + "";    // 시험검체수량(EA)
            param[8] = "@deposit_sample_qty:" + ""; // 보관검체수량(EA)
            param[9] = "@picking_method:" + "";     // 채취방법
            param[10] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
            param[11] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
            param[12] = "@log_user_id :" + HttpContext.Current.Session["loginCD"];
            param[13] = "@item_stock_id:" + model.item_stock_id; //제품번호
            param[14] = "@write_gb:" + "Y"; 
            param[15] = "@item_form_cd:" + model.item_form_cd;
            param[16] = "@order_no:" + model.order_no;
            //param[17] = "@request_no:" + model.testrequest_no;
            //param[18] = "@request_time1:" + string.Format("{0} {1}", TestRequestDate, DateTime.Now.ToString("HH:mm:ss.fff"));

            res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

    }
}