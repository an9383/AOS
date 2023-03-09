using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class OrderProcResultMonitorService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectOrderProcResultMonitor(string sdate, string edate, string item_cd, string zero_work_yn, string trade_cd2, string trade_cd3)
        {
            string sSpName = "SP_OrderProcResult_Monitor";

            string[] pParam = new string[6];
            pParam[0] = "@sdate:" + sdate;
            pParam[1] = "@edate:" + edate;
            pParam[2] = "@item_cd:" + item_cd;
            pParam[3] = "@zero_work_yn:" + ( !String.IsNullOrEmpty(zero_work_yn) ? zero_work_yn : "N");
            pParam[4] = "@trade_cd2:" + trade_cd2;
            pParam[5] = "@trade_cd3:" + trade_cd3;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable OrderProcResultMonitorSelectChartData(string order_no)
        {
            string sSpName = "SP_OrderProcResult_Monitor";

            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable resDT = new DataTable();

            return dt;
        }
    }
}