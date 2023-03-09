using HACCP.Libs.Database;
using HACCP.Models.RowMatWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatWh
{
    public class Material_Used_Results2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Material_Used_Results2_S(Material_Used_Results2 mModel)
        {
            string sSpName = "SP_Material_Used_Results2";
            string sGubun = "S";
            string[] pParam = new string[4];

            pParam[0] = "@S_ORDER_START_DATE:" + mModel.S_ORDER_START_DATE;
            pParam[1] = "@S_ORDER_END_DATE:" + mModel.S_ORDER_END_DATE;
            pParam[2] = "@S_ITEM_CD:" + "";
            pParam[3] = "@S_LOT_NO:" + "";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable Material_Used_Results2_S2(Material_Used_Results2 mModel, string order_no, string input_order_id, string process_cd)
        {
            string sSpName = "SP_Material_Used_Results2";
            string sGubun = "S2";
            string[] pParam = new string[3];

            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@input_order_id:" + input_order_id;
            pParam[2] = "@process_cd:" + process_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
    }
}
