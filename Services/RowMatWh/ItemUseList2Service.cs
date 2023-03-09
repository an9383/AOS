using HACCP.Libs.Database;
using HACCP.Models.RowMatWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatWh
{
    public class ItemUseList2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable ItemUseList2Popup()
        {
            string strsql = "SELECT item_cd, item_nm";
            strsql += " FROM  v_item_material3";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }
        internal DataTable ItemUseList2Popup_P()
        {
            string strsql = "SELECT item_cd, item_nm";
            strsql += " FROM  v_item_material2";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable ItemUseList2_Select(ItemUseList2 iModel)
        {
            string sSpName = "SP_ItemUseList2";
            string sGubun = "S";
            string[] pParam = new string[6];

            pParam[0] = "@start_date:" + iModel.start_date;
            pParam[1] = "@end_date:" + iModel.end_date;
            pParam[2] = "@item_cd:" + iModel.item_cd;
            pParam[3] = "@out_type:" + "%";
            pParam[4] = "@in_type:" + "%";
            pParam[5] = "@s_gubun:" + iModel.s_gubun;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
    }
}
