using HACCP.Libs.Database;
using HACCP.Models.RowMatWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatWh
{
    public class InOutService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable InOutSelect_S(InOut iModel)
        {
            string sSpName = "SP_InOut";
            string sGubun = "S";
            string[] pParam = new string[5];

            pParam[0] = "@s_item:" + "";
            pParam[1] = "@s_gubun:" + iModel.s_gubun;
            pParam[2] = "@use_ck:" + iModel.use_ck;
            pParam[3] = "@start_date:" + iModel.start_date;
            pParam[4] = "@end_date:" + iModel.end_date;              

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        //시험번호 조회
        internal DataTable InOutSelect_S2(InOut iModel, string item_cd, string s_test_no)
        {
            string sSpName = "SP_InOut";
            string sGubun = "S2";
            string[] pParam = new string[4];

            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@s_test_no:" + "";
            pParam[2] = "@start_date:" + iModel.start_date;
            pParam[3] = "@end_date:" + iModel.end_date;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        //수불현황 조회
        internal DataTable InOutSelect_S3(InOut iModel, string test_no, string item_cd)
        {
            string sSpName = "SP_InOut";
            string sGubun = "S3";
            string[] pParam = new string[5];

            pParam[0] = "@test_no:" + test_no;
            pParam[1] = "@item_cd:" + item_cd;
            pParam[2] = "@start_date:" + iModel.start_date;
            pParam[3] = "@end_date:" + iModel.end_date;
            pParam[4] = "@receipt_no:" + iModel.receipt_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
    }
}
