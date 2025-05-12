using HACCP.Libs.Database;
using HACCP.Models.RowMatLoc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatLoc
{
    public class StackSearchService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_StackSearch";

        /// <summary>
        /// 원자재 적치 현황 (좌측 그리드)
        /// </summary>
        /// <param name="sModel"></param>
        /// <returns></returns>
        internal DataTable Select(StackSearch sModel)
        {
            string sGubun = "S";
            string[] param = new string[2];

            param[0] = "@s_item_cd:" + "";      // 품목
            param[1] = "@s_gubun:" + sModel.item_gb;  // 원료 3, 자재 2

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable StackSearch_Test_Select(StackSearch sModel, string item_cd)
        {
            string sGubun = "S2";
            string[] param = new string[1];

            param[0] = "@item_cd:" + item_cd;      // 품목

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable StackSearch_ReceiptPack_Select(StackSearch sModel, string receipt_no, string receipt_id)
        {
            string sGubun = "S3";
            string[] param = new string[2];

            param[0] = "@receipt_no:" + receipt_no; //입고번호
            param[1] = "@receipt_id:" + receipt_id; //입고순번 

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
    }
}
