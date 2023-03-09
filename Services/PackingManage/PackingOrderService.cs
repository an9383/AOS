using HACCP.Libs.Database;
using HACCP.Models.PackingManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PackingManage
{
    public class PackingOrderService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectPackingOrder(PackingOrder.SrchParam packingOrder)
        {
            string sSpName = "SP_Packingorder_Order";
            string[] pParam = new string[5];
            pParam[0] = "@s_sDate:" + packingOrder.sdate;
            pParam[1] = "@s_eDate:" + packingOrder.edate;
            pParam[2] = "@order_type:" + packingOrder.order_type;
            pParam[3] = "@s_item:" + packingOrder.s_item;
            pParam[4] = "@s_lot_no:" + packingOrder.s_lot_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectPackingOrderDetail(PackingOrder.SrchParam packingOrder)
        {
            string sSpName = "SP_Packingorder_Order";
            string[] pParam = new string[3];
            pParam[0] = "@s_sDate:" + packingOrder.sdate;
            pParam[1] = "@s_eDate:" + packingOrder.edate;
            pParam[2] = "@order_no:" + packingOrder.order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }
    }
}