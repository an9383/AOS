using HACCP.Libs.Database;
using HACCP.Models.PrdWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdWh
{
    public class ItemInOutStatusService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable ItemInOutStatus_Select(ItemInOutStatus iModel)
        {
            string sSpName = "SP_ItemInOutStatus";
            string sGubun = "Select";
            string[] pParam = new string[4];

            pParam[0] = "@start_date:" + iModel.start_date;
            pParam[1] = "@end_date:" + iModel.end_date;
            pParam[2] = "@item:" + iModel.item;
            if (iModel.use_ck == "Y")
            {
                pParam[3] = "@use_ck:Y";
            }
            else
            {
                pParam[3] = "@use_ck:N";
            }

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable ItemInOutStatus_Select2(string box_barcode_no)
        {
            string sSpName = "SP_ItemInOutStatus";
            string sGubun = "Select2";
            string[] pParam = new string[1];

            pParam[0] = "@box_barcode_no:" + box_barcode_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
    }
}