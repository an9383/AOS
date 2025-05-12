using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class EquipResultsService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectEquipResults(string sdate, string edate, string equip_cd)
        {
            string sSpName = "SP_EquipResults";

            string[] pParam = new string[3];
            pParam[0] = "@sdate:" + sdate;
            pParam[1] = "@edate:" + edate;
            pParam[2] = "@equip_cd:" + equip_cd;

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