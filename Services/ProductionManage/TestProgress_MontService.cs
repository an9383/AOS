using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Libs.File;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class TestProgress_MontService
    {
        string sSpName = "sp_TestProgress_Mont";
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataSet Select()
        {
            string gubun = "S";

            string[] param = new string[0];

            //param[0] = "@workroom_type:" + workroom_type;
            //param[1] = "@endDate:" + endDate;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);
          


            return ds;
        }


    }
}