using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.RowMatWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.RowMatWh
{
    public class PickingManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable PickingManage_Select(PickingManage pModel)
        {
            string sSpName = "SP_PickingManage";
            string sGubun = "Select";
            string[] pParam = new string[4];

            pParam[0] = "@start_date:" + pModel.start_date;
            pParam[1] = "@end_date:" + pModel.end_date;
            pParam[2] = "@item_cd:" + "";
            pParam[3] = "@s_gubun:" + pModel.s_gubun;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable PickingManage_Select2(string order_no, string input_order_id, string process_cd, string s_gubun)
        {
            string sSpName = "SP_PickingManage";
            string sGubun = "Select2";
            string[] pParam = new string[4];

            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@input_order_id:" + input_order_id;
            pParam[2] = "@process_cd:" + process_cd;
            pParam[3] = "@s_gubun:" + s_gubun;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable PickingManage_Select3(string order_no, string input_order_id, string process_cd, string qc_no, string s_gubun)
        {
            string sSpName = "SP_PickingManage";
            string sGubun = "Select3";
            string[] pParam = new string[5];

            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@input_order_id:" + input_order_id;
            pParam[2] = "@process_cd:" + process_cd;
            pParam[3] = "@qc_no:" + qc_no;
            pParam[4] = "@s_gubun:" + s_gubun;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string PickingManage_TRX(List<PickingManage> pModel, string gubun)
        {
            string sSpName = "SP_PickingManage";
            string sGubun = "";
            string message = "";

            for(int i = 0; i < pModel.Count; i++)
            {
                if (pModel[i].gubun.Equals("Update"))
                {
                    sGubun = "Update";
                    string[] pParam = new string[2];

                    pParam[0] = "@receipt_pack_barcode:" + pModel[i].receipt_pack_barcode;
                    pParam[1] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
            }

            return message;
        }


    }
}