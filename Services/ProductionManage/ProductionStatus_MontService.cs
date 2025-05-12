using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.ProductionManage
{
    public class ProductionStatus_MontService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select()
        {
            string sSpName = "SP_Monitoring_ProductionStatus";
            string[] pParam = new string[0];

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", pParam);


            return dt;
        }

      
    }
}
