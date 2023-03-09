using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.PrdWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdWh
{
    public class ItemOutManageService
    {

        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable ItemOutManage_Select(ItemOutManage mModel)
        {
            string sSpName = "SP_ItemOutManage";
            string sGubun = "Select";
            string[] pParam = new string[3];

            pParam[0] = "@start_date:" + mModel.start_date;
            pParam[1] = "@end_date:" + mModel.end_date;
            pParam[2] = "@out_type:" + mModel.out_type;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable ItemOutManage_Select2(string box_barcode_no)
        {
            string sSpName = "SP_ItemOutManage";
            string sGubun = "Select2";
            string[] pParam = new string[1];

            pParam[0] = "@box_barcode_no:" + box_barcode_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable ItemOutManage_ItemPopup(string sGubun, string start_date, string end_date, string search)
        {
            string sSpName = "SP_ReceiptItemView"; //gubun=ReceiptItem
            string[] pParam = new string[3];
            pParam[0] = "@start_date:" + start_date;
            pParam[1] = "@end_date:" + end_date;
            pParam[2] = "@search:" + "%";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string ItemOutManageCRUD(List<ItemOutManage> mModel, string gubun)
        {
            string sSpName = "SP_ItemOutManage";
            string sGubun = "";
            string message = "";

            for (int i = 0; i < mModel.Count; i++)
            {
                if (mModel[i].gubun.Equals("Insert"))
                {
                    sGubun = "Insert";
                    string[] pParam = GetParam(mModel[i]);
                    pParam[5] = "@packing_result_pack_out_history_id:" + "";

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (mModel[i].gubun.Equals("Update"))
                {
                    sGubun = "Update";
                    string[] pParam = GetParam(mModel[i]);

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (mModel[i].gubun.Equals("Delete"))
                {
                    sGubun = "Delete";
                    string[] pParam = GetParam(mModel[i]);

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
            }
            return message;
        }

        //internal string ItemOutManageCRUD(ItemOutManage mModel, string gubun)
        //{
        //    string sSpName = "SP_ItemOutManage";
        //    string sGubun = gubun;

        //    string message = "";

        //    if (mModel.Equals("Delete"))
        //    {
        //        string[] pParam = new string[2];
        //        pParam[0] = "@box_barcode_no" + mModel.box_barcode_no;
        //        pParam[1] = "@item_cd" + mModel.item_cd;

        //        message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
        //    }
        //    else
        //    {
        //        string[] pParam = GetParam(mModel);
        //        if (gubun == "Insert")
        //            pParam[5] = "@packing_result_pack_out_history_id:" + "";
        //        message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
        //    }
        //    //정상 처리 되었습니다.
        //    return message;
        //}

        private string[] GetParam(ItemOutManage itemOutManage)
        {
            string[] param = new string[10];

            //기본정보
            param[0] = "@box_barcode_no:" + itemOutManage.box_barcode_no;
            param[1] = "@out_type:" + itemOutManage.out_type;
            param[2] = "@out_qty:" + itemOutManage.out_qty;
            param[3] = "@out_date:" + itemOutManage.out_date;
            param[4] = "@out_remark:" + itemOutManage.out_remark;
            param[5] = "@packing_result_pack_out_history_id:" + itemOutManage.packing_result_pack_out_history_id;
            param[6] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
            param[7] = "@past_out_qty:" + itemOutManage.delivery_qty;
            param[8] = "@past_stock_qty:" + itemOutManage.stock_qty;
            param[9] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];


            return param;
        }

    }
}