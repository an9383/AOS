using HACCP.Libs.Database;
using HACCP.Models.RowMatWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.RowMatWh
{
    public class PickingOrderService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable PickingOrder_Select(PickingOrder pModel)
        {
            string sSpName = "SP_PickingOrder2";
            string sGubun = "Select";
            string[] pParam = new string[1];

            pParam[0] = "@item_cd:" + "";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
    }
}