using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Comm
{
    public class GenerateReportService
    {
        internal DataSet SetReportDs(ReportParam reportParam)
        {
            if (String.IsNullOrEmpty(reportParam.SpName))
            {
                return new DataSet();
            }

            BllSpExecute _bllSpExecute = new BllSpExecute();

            string sSpName = reportParam.SpName;
            string gubun = reportParam.Gubun;
            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + reportParam.Param1;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            return ds;
        }
    }
}