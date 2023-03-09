using HACCP.Libs.Database;
using HACCP.Models.RowMatln;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.RowMatln
{
    public class ReceiptCheck2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ReceiptCheck2";

        internal DataTable Select(ReceiptCheck2 model)
        {
            string sGubun = "S";

            string[] param = new string[9];

            param[0] = "@purchase_no:" + model.purchase_no;
            param[1] = "@start_date:" + model.start_date;
            param[2] = "@end_date:" + model.end_date;
            //param[3] = "@item_cd:" + model.item_cd_S;
            param[3] = "@item_cd:" + model.item_nm_S;
            param[4] = "@option:" + model.option;
            param[5] = "@option2:" + model.option2;
            param[6] = "@check:" + model.check;
            param[7] = "@purchase_status_Y:" + model.purchase_status_Y;  //Y:포함, N:비포함
            param[8] = "@manufacture_item_cd_S:" + model.manufacture_item_cd_S;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ItemCDPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "item_cd, item_nm ";
            strsql += "FROM  v_item_material3 ";
            strsql += "WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%') ";
            strsql += "AND item_cd  LIKE '%%' AND item_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable ItemPCDPopup_P(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "item_cd, item_nm ";
            strsql += "FROM  v_item_material4 ";
            strsql += "WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%') ";
            strsql += "AND item_cd  LIKE '%%' AND item_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable Select_List(ReceiptCheck2 model)
        {
            string sGubun = "S2";
            string[] param = new string[2];

            param[0] = "@purchase_no:" + model.purchase_no;
            param[1] = "@purchase_seq:" + model.purchase_seq;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
    }
}