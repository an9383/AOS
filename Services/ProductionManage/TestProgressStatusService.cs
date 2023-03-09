using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.ProductionManage
{
    public class TestProgressStatusService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelecttestProgressStatus(string date_option, string start_date, string end_date, string test_type, string test_status, string search_gubun, string search_number, string item_form_cd)
        {
            string sSpName = "sp_TestProgressStatus";
            string[] pParam = new string[8];
            pParam[0] = "@date_option:" + date_option;
            pParam[1] = "@start_date:" + start_date;
            pParam[2] = "@end_date:" + end_date;
            pParam[3] = "@test_type:" + test_type;
            pParam[4] = "@test_status:" + test_status;
            pParam[5] = "@search_gubun:" + search_gubun;
            pParam[6] = "@search_number:" + search_number;
            pParam[7] = "@item_form_cd:" + item_form_cd;

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
