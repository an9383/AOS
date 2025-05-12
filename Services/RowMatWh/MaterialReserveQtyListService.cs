using HACCP.Libs.Database;
using HACCP.Models.ProductionManage;
using HACCP.Models.RowMatWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatWh
{
    public class MaterialReserveQtyListService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable MaterialReserveQtyListSelect_S1(MaterialReserveQtyList mModel)
        {
            string sSpName = "SP_MaterialReserveQtyList";
            string sGubun = "S1";
            string[] pParam = new string[2];

            pParam[0] = "@s_item_cd:" + "";      
            pParam[1] = "@option:" + mModel.option;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable MaterialReserveQtyListSelect_S2(MaterialReserveQtyList mModel, string item_cd)
        {
            string sSpName = "SP_MaterialReserveQtyList";
            string sGubun = "S2";
            string[] pParam = new string[2];

            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@option:" + mModel.option;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

    }
}
