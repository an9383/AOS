using HACCP.Libs.Database;
using HACCP.Models.PrdWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdWh
{
    public class ItemMonthlyStockService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable ItemMonthlyStock_S(ItemMonthlyStock iModel)
        {
            string sSpName = "SP_ItemMonthlyStock";
            string sGubun = "S";
            string[] pParam = new string[7];

            pParam[0] = "@item_gb:" + "";
            pParam[1] = "@start_date:" + iModel.start_date;
            pParam[2] = "@end_date:" + iModel.end_date;
            pParam[3] = "@item_cd_S:" + iModel.item_cd_S;
            pParam[4] = "@search_data_S:" + iModel.search_data_S;
            if (iModel.use_ck_S == "Y")
            {
                pParam[5] = "@use_ck_S:Y";
            }
            else
            {
                pParam[5] = "@use_ck_S:N";
            }
            pParam[6] = "@obtain_status:" + iModel.obtain_status;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

    }
}