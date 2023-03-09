
using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Cp
{
    public class ClaimGraphService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        public DataSet GridSelect(string start_date, string end_date)
        {
            string sSpName = "SP_ClaimGraph";
            string sGubun = "S";
            string[] pParam = new string[2];

            pParam[0] = "@start_date:" + start_date;
            pParam[1] = "@end_date:" + end_date;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            return ds;
        }

        public DataSet GridSelect2(string start_date, string item_cd)
        {
            string sSpName = "SP_ClaimGraph";
            string sGubun = "S1";
            string[] pParam = new string[2];

            pParam[0] = "@start_date:" + start_date;
            pParam[1] = "@item_cd:" + item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            return ds;
        }

    }
}