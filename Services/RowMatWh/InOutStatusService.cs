using HACCP.Libs.Database;
using HACCP.Models.RowMatWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatWh
{
    public class InOutStatusService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        
        internal DataTable InOutStatusSelect(InOutStatus oModel)
        {
            string sSpName = "SP_InOutStatus";
            string sGubun = "Select";
            string[] pParam = new string[5];

            pParam[0] = "@start_date:" + oModel.start_date;
            pParam[1] = "@end_date:" + oModel.end_date;
            pParam[2] = "@item:" + "";
            pParam[3] = "@s_gubun:" + oModel.s_gubun;
            pParam[4] = "@use_ck:" + oModel.use_ck;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable InOutStatusSelect_S2(InOutStatus oModel, string receipt_no, string receipt_id, string receipt_pack_seq)
        {
            string sSpName = "SP_InOutStatus";
            string sGubun = "Select2";
            string[] pParam = new string[3];

            pParam[0] = "@receipt_no:" + receipt_no;
            pParam[1] = "@receipt_id:" + receipt_id;
            pParam[2] = "@receipt_pack_seq:" + receipt_pack_seq;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
    }
}
