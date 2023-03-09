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
    public class ProductTransferService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ProductTransfer";

        internal DataTable Select(ProductTransfer model)
        {
            string sGubun = "Select";
            string[] param = new string[8];

            param[0] = "@packing_order_work_date_start_s:" + model.start_date_S;
            param[1] = "@packing_order_work_date_end_s:" + model.end_date_S;
            //param[2] = "@item_cd_s:" + model.item_cd_S;
            param[2] = "@item_cd_s:" + model.item_nm_S;
            param[3] = "@lot_no_s:" + "";
            param[4] = "@issue_status:" + model.s_issue_status;
            param[5] = "@receipt_status:" + model.s_receipt_status;
            param[6] = "@search_ck:" + model.search_ck;
            param[7] = "@prod_return_status:" + model.prod_return_status;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable SelectBox(ProductTransfer model)
        {
            string sGubun = "SelectBox";
            string[] param = new string[1];

            param[0] = "@item_stock_id:" + model.item_stock_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        //품목팝업
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

        internal string Trans_Stock(ProductTransfer model, string[] selectedKeys)
        {
            string gubun = "Trans_Stock"; // SP의 구분값

            string res = ""; // 리턴메시지

            for (int i = 0; i < selectedKeys.Length; i++)
            {
                string[] param = new string[5];

                param[0] = "@item_stock_id:" + selectedKeys[i];
                param[1] = "@log_user_id :" + HttpContext.Current.Session["loginCD"];
                param[2] = "@transfer_date:" + model.transfer_date;
                param[3] = "@ipgo_seq:" + "";
                param[4] = "@igwan_seq:" + "";
                
                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }

            return res;
        }
    }
}