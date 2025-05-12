using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Cp
{
    public class ClaimGraph3Service
    {

        public BllSpExecute _bllSpExecute = new BllSpExecute();

        public DataSet GridSelect(string start_date, string end_date)
        {
            string sSpName = "SP_ClaimGraph3";
            string sGubun = "S";
            string[] pParam = new string[2];

            pParam[0] = "@start_date:" + start_date;
            pParam[1] = "@end_date:" + end_date;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            return ds;
        }

        public DataSet GridSelect2(string start_date, string claim_type)
        {
            string sSpName = "SP_ClaimGraph3";
            string sGubun = "S1";
            string[] pParam = new string[2];

            pParam[0] = "@start_date:" + start_date;
            pParam[1] = "@claim_type:" + claim_type;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            return ds;
        }
    }
}