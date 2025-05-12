using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.PrdWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Services.PrdWh
{
    public class ItemInManageService
    {

        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable ItemInManage_Select(ItemInManage mModel)
        {
            string sSpName = "SP_ItemInManage";
            string sGubun = "Select";
            string[] pParam = new string[3];

            pParam[0] = "@start_date:" + mModel.start_date;
            pParam[1] = "@end_date:" + mModel.end_date;
            pParam[2] = "@in_type:" + mModel.in_type;
            //pParam[3] = "@s_gubun:" + mModel.s_gubun;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable ItemInManage_Select2(string box_barcode_no)
        {
            string sSpName = "SP_ItemInManage";
            string sGubun = "Select2";
            string[] pParam = new string[1];

            pParam[0] = "@box_barcode_no:" + box_barcode_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable ItemInManage_ItemPopup(string sGubun, string start_date, string end_date, string search)
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

        internal string ItemInManageCRUD(List<ItemInManage> mModel, string gubun)
        {
            string sSpName = "SP_ItemInManage";
            string sGubun = "";
            string message = "";

            for (int i = 0; i < mModel.Count; i++)
            {
                if (mModel[i].gubun.Equals("Insert"))
                {
                    sGubun = "Insert";
                    string[] pParam = GetParam(mModel[i]);
                    pParam[5] = "@packing_result_pack_in_history_id:" + "";

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

        //internal string ItemInManageCRUD(ItemInManage mModel, string gubun)
        //{
        //    string sSpName = "SP_ItemInManage";
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
        //            pParam[5] = "@packing_result_pack_in_history_id:" + "";
        //        message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
        //    }
        //    //정상 처리 되었습니다.
        //    return message;
        //}

        private string[] GetParam(ItemInManage itemInManage)
        {
            string[] param = new string[11];

            //기본정보
            param[0] = "@box_barcode_no:" + itemInManage.box_barcode_no;
            param[1] = "@in_type:" + itemInManage.in_type;
            param[2] = "@in_qty:" + itemInManage.in_qty;
            param[3] = "@in_date:" + itemInManage.in_date;
            param[4] = "@in_remark:" + itemInManage.in_remark;
            param[5] = "@packing_result_pack_in_history_id:" + itemInManage.packing_result_pack_in_history_id;
            param[6] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
            param[7] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
            param[8] = "@log_user_id:" + HttpContext.Current.Session["loginCD"];
            param[9] = "@past_in_qty:" + itemInManage.prod_qty;
            param[10] = "@past_stock_qty:" + itemInManage.stock_qty;


            return param;
        }

    }
}
