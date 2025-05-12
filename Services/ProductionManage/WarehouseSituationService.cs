using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class WarehouseSituationService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectWarehouseSituation(string item_cd, string warehouse_cd, string transfer_status)
        {
            string sSpName = "SP_Warehouse_Situation";

            string[] pParam = new string[3];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@warehouse_cd:" + warehouse_cd;
            pParam[2] = "@transfer_status:" + transfer_status;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SS", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable Warehouse_SituationSelectSub(string order_no, string order_proc_id)
        {
            string sSpName = "SP_Warehouse_Situation";

            string[] pParam = new string[2];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@order_proc_id:" + order_proc_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SC", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }
    }
}