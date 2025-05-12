using HACCP.Libs.Database;
using HACCP.Models.PrdLoc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdLoc
{
    public class ItemSearchService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ItemSearch";

        internal DataTable Select(ItemSearch sModel)
        {
            string sGubun = "S";
            string[] param = new string[1];

            param[0] = "@s_item_cd:" + "";      // 품목

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ItemSearch_S2(ItemSearch model)
        {
            string sGubun = "S2";
            string[] param = new string[1];

            param[0] = "@item_cd:" + model.item_cd;      // 품목

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ItemSearch_S3(ItemSearch model)
        {
            string sGubun = "S3";
            string[] param = new string[1];

            param[0] = "@item_stock_id:" + model.item_stock_id;      // 제품입고일련번호

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
    }
}