using HACCP.Libs.Database;
using HACCP.Models.RowMatLoc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatLoc
{
    public class OrderMaterialLocationService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_OrderMaterialLocation";

        internal DataTable Select(OrderMaterialLocation model)
        {
            string sGubun = "S";
            string[] param = new string[4];

            param[0] = "@s_item:" + "";
            param[1] = "@s_sdate:" + model.s_sdate;
            param[2] = "@s_edate:" + model.s_edate;
            param[3] = "@out_status:" + model.input_order_status;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable OrderList_Select(OrderMaterialLocation model)
        {
            string sGubun = "S2";

            string[] param = new string[2];

            param[0] = "@order_no:" + model.order_no;
            param[1] = "@input_order_id:" + model.input_order_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
    }
}
