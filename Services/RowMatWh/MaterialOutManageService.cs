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
    public class MaterialOutManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable MaterialOutManage_Select(MaterialOutManage mModel)
        {
            string sSpName = "SP_MaterialOutManage";
            string sGubun = "Select";
            string[] pParam = new string[5];

            pParam[0] = "@start_date:" + mModel.start_date;
            pParam[1] = "@end_date:" + mModel.end_date;
            pParam[2] = "@item:" + "";
            pParam[3] = "@s_gubun:" + mModel.s_gubun;            
            pParam[4] = "@out_type:" + mModel.out_type;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable MaterialOutManage_Select2(string receipt_no, string receipt_id, string receipt_pack_seq)
        {
            string sSpName = "SP_MaterialOutManage";
            string sGubun = "Select2";
            string[] pParam = new string[3];

            pParam[0] = "@receipt_no:" + receipt_no;
            pParam[1] = "@receipt_id:" + receipt_id;
            pParam[2] = "@receipt_pack_seq:" + receipt_pack_seq;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable MaterialOutManage_Select3(string receipt_no, string receipt_id, string receipt_pack_seq)
        {
            string sSpName = "SP_MaterialOutManage";
            string sGubun = "Select3";
            string[] pParam = new string[3];

            pParam[0] = "@receipt_no:" + receipt_no;
            pParam[1] = "@receipt_id:" + receipt_id;
            pParam[2] = "@receipt_pack_seq:" + receipt_pack_seq;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable MaterialOutManage_ItemPopup(string start_date, string end_date, string search, string item_gb)
        {
            string sSpName = "SP_ReceiptItemView";
            string sGubun = "ReceiptMaterialOut";
            string[] pParam = new string[4];

            pParam[0] = "@start_date:" + start_date;
            pParam[1] = "@end_date:" + end_date;
            pParam[2] = "@search:" + "";
            pParam[3] = "@item_gb:" + item_gb;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string MaterialOutManageCRUD(List<MaterialOutManage> mModel, string gubun)
        {
            string sSpName = "SP_MaterialOutManage";
            string sGubun = "";
            string message = "";

            for(int i = 0; i < mModel.Count; i++)
            {
                if (mModel[i].gubun.Equals("Insert"))
                {
                    sGubun = "Insert";
                    string[] pParam = new string[7];

                    pParam[0] = "@receipt_no:" + mModel[i].receipt_no;
                    pParam[1] = "@receipt_id:" + mModel[i].receipt_id;
                    pParam[2] = "@receipt_pack_seq:" + mModel[i].receipt_pack_seq;
                    pParam[3] = "@out_type:" + mModel[i].out_type;
                    pParam[4] = "@out_qty:" + mModel[i].out_qty;
                    pParam[5] = "@out_date:" + mModel[i].out_date;
                    pParam[6] = "@out_remark:" + mModel[i].out_remark;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (mModel[i].gubun.Equals("Update"))
                {
                    sGubun = "Update";
                    string[] pParam = new string[8];

                    pParam[0] = "@out_type:" + mModel[i].out_type;
                    pParam[1] = "@out_qty:" + mModel[i].out_qty;
                    pParam[2] = "@out_date:" + mModel[i].out_date;
                    pParam[3] = "@out_remark:" + mModel[i].out_remark;
                    pParam[4] = "@receipt_no:" + mModel[i].receipt_no;
                    pParam[5] = "@receipt_id:" + mModel[i].receipt_id;
                    pParam[6] = "@receipt_pack_seq:" + mModel[i].receipt_pack_seq;
                    pParam[7] = "@history_id:" + mModel[i].history_id;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (mModel[i].gubun.Equals("Delete"))
                {
                    sGubun = "Delete";
                    string[] pParam = new string[4];

                    pParam[0] = "@receipt_no:" + mModel[i].receipt_no;
                    pParam[1] = "@receipt_id:" + mModel[i].receipt_id;
                    pParam[2] = "@receipt_pack_seq:" + mModel[i].receipt_pack_seq;
                    pParam[3] = "@history_id:" + mModel[i].history_id;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
            }
            return message;
        }

    }
}