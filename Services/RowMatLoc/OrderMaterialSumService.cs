using HACCP.Libs.Database;
using HACCP.Models.RowMatLoc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatLoc
{
    public class OrderMaterialSumService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_OrderMaterialSum";

        internal DataTable Select(OrderMaterialSum model)
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

        internal DataTable OrderMaterialOutSum(OrderMaterialSum model, string[] result)
        {
            string sGubun = "OutSum";
            // 선택 한 수
            int result_Length = result.Length;
            string[] param = new string[result_Length];

            for (int i = 0; i < result_Length; i++)
            {
                param[i] = "@order_no_" + (i+1).ToString() + ":" + result[i];
            }
            
            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
    }
}
