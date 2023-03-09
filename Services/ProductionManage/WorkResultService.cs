using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class WorkResultService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataSet SelectWorkResult(string year, string month, string item_cd)
        {
            string sSpName = "SP_WorkResult";

            string[] pParam = new string[3];
            pParam[0] = "@year:" + year;
            pParam[1] = "@month:" + month;
            pParam[2] = "@item_cd:" + item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            return ds;
        }
    }
}