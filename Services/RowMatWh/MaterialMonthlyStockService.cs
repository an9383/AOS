using HACCP.Libs.Database;
using HACCP.Models.RowMatWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatWh
{
    public class MaterialMonthlyStockService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable MaterialMonthlyStock_S_Standard(MaterialMonthlyStock mModel)
        {
            string sSpName = "SP_MaterialMonthlyStock";
            string sGubun = "S_Standard";
            string[] pParam = new string[6];

            pParam[0] = "@item_gb:" + mModel.item_gb;
            pParam[1] = "@start_date:" + mModel.start_date;
            pParam[2] = "@end_date:" + mModel.end_date;
            pParam[3] = "@item_cd_S:" + "";
            if(mModel.use_ck_S == "Y")
            {
                pParam[4] = "@use_ck_S:Y";
            }
            else
            {
                pParam[4] = "@use_ck_S:%";
            }
            pParam[5] = "@obtain_status:" + mModel.obtain_status;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable MaterialMonthlyStock_Select(MaterialMonthlyStock mModel)
        {
            string sSpName = "SP_MaterialMonthlyStock";
            string sGubun = "S";
            string[] pParam = new string[6];

            pParam[0] = "@item_gb:" + mModel.item_gb;
            pParam[1] = "@start_date:" + mModel.start_date;
            pParam[2] = "@end_date:" + mModel.end_date;
            pParam[3] = "@item_cd_S:" + "";
            if (mModel.use_ck_S == "Y")
            {
                pParam[4] = "@use_ck_S:Y";
            }
            else
            {
                pParam[4] = "@use_ck_S:%";
            }
            pParam[5] = "@obtain_status:" + mModel.obtain_status;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
    }
}
