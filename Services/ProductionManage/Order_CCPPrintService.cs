using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class Order_CCPPrintService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable getCCP(string order_no)
        {
            string sSpName = "SP_ImHaccpCodeMaster";
            string sGubun = "";
            string[] pParam = new string[1];

            pParam[0] = "@order_no:" + order_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


    }
}