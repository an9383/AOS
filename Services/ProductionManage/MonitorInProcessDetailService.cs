using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class MonitorInProcessDetailService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectMonitorInProcessDetail(string item_cd, string sdate, string edate, string lot_no)
        {
            string sSpName = "sp_Monitor_InProcessDetail";

            string[] pParam = new string[4];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@sdate:" + sdate;
            pParam[2] = "@edate:" + edate;
            pParam[3] = "@lot_no:" + lot_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SIP", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable MonitorInProcessDetailSelectDetail1(string order_no)
        {
            string sSpName = "sp_Monitor_InProcessDetail";

            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SR", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable MonitorInProcessDetailSelectDetail2(string order_no)
        {
            string sSpName = "sp_Monitor_InProcessDetail";

            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SRD", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable MonitorInProcessDetailSelectProcessDetail(string order_no, string order_proc_id)
        {
            string sSpName = "sp_Monitor_InProcessDetail";

            string[] pParam = new string[2];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@order_proc_id:" + order_proc_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SPR", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }
    }
}