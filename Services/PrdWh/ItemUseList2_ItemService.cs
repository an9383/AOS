using HACCP.Libs.Database;
using HACCP.Models.PrdWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdWh
{
    public class ItemUseList2_ItemService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable ItemUseList2_Item_S(ItemUseList2_Item iModel)
        {
            string sSpName = "SP_ItemUseList2_Item";
            string sGubun = "S";
            string[] pParam = new string[6];

            pParam[0] = "@start_date:" + iModel.start_date;
            pParam[1] = "@end_date:" + iModel.end_date;
            pParam[2] = "@item_cd:" + iModel.item_cd;
            pParam[3] = "@out_type:" + "%";
            pParam[4] = "@in_type:" + "%";
            pParam[5] = "@s_gubun:" + "";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable ItemUseList2_ItemPopup()
        {
            string strsql = "SELECT item_cd, item_nm, vender_item_cd, item_s_nm";
            strsql += " FROM v_packingitem";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

    }
}