using HACCP.Libs.Database;
using HACCP.Models.RowMatLoc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatLoc
{
    public class OrderPackLocationService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_OrderPackLocation";

        internal DataTable Select(OrderPackLocation model)
        {
            string sGubun = "S";
            string[] param = new string[4];

            param[0] = "@s_item:" + "";
            param[1] = "@s_sdate:" + model.s_sdate;
            param[2] = "@s_edate:" + model.s_edate;
            param[3] = "@out_status:" + model.material_status;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable OrderPackList_Select(OrderPackLocation model)
        {
            string sGubun = "S2";

            string[] param = new string[1];

            param[0] = "@order_no:" + model.order_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
    }
}
