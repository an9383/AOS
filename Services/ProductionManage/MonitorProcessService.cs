using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class MonitorProcessService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectMonitorProcess(string sdate, string edate, string item_cd, string process_status)
        {
            string sSpName = "SP_Monitor_process";

            string[] pParam = new string[4];
            pParam[0] = "@sdate:" + sdate;
            pParam[1] = "@edate:" + edate;
            pParam[2] = "@item_cd:" + item_cd;
            pParam[3] = "@process_status:" + process_status;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }
    }
}