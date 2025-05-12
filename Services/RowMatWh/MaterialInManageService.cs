using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.RowMatWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatWh
{
    public class MaterialInManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable MaterialInManage_Select(MaterialInManage mModel)
        {
            string sSpName = "SP_MaterialInManage";
            string sGubun = "Select";
            string[] pParam = new string[4];

            pParam[0] = "@start_date:" + mModel.start_date;
            pParam[1] = "@end_date:" + mModel.end_date;
            pParam[2] = "@item:" + "";
            pParam[3] = "@s_gubun:" + mModel.s_gubun;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable MaterialInManage_Select2(string receipt_no, string receipt_id, string receipt_pack_seq)
        {
            string sSpName = "SP_MaterialInManage";
            string sGubun = "Select2";
            string[] pParam = new string[3];

            pParam[0] = "@receipt_no:" + receipt_no;
            pParam[1] = "@receipt_id:" + receipt_id;
            pParam[2] = "@receipt_pack_seq:" + receipt_pack_seq;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable MaterialInManage_Select3(string receipt_no, string receipt_id, string receipt_pack_seq)
        {
            string sSpName = "SP_MaterialInManage";
            string sGubun = "Select3";
            string[] pParam = new string[3];

            pParam[0] = "@receipt_no:" + receipt_no;
            pParam[1] = "@receipt_id:" + receipt_id;
            pParam[2] = "@receipt_pack_seq:" + receipt_pack_seq;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable MaterialInManage_ItemPopup(string start_date, string end_date, string search, string item_gb)
        {
            string sSpName = "SP_ReceiptItemView";
            string sGubun = "ReceiptMaterialIn";
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

        internal string MaterialInManageCRUD(List<MaterialInManage> mModel, string gubun)
        {
            string sSpName = "SP_MaterialInManage";
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
                    pParam[3] = "@in_type:" + mModel[i].in_type;
                    pParam[4] = "@in_qty:" + mModel[i].in_qty;
                    pParam[5] = "@in_date:" + mModel[i].in_date;
                    pParam[6] = "@in_remark:" + mModel[i].in_remark;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (mModel[i].gubun.Equals("Update"))
                {
                    sGubun = "Update";
                    string[] pParam = new string[8];

                    pParam[0] = "@in_type:" + mModel[i].in_type;
                    pParam[1] = "@in_qty:" + mModel[i].in_qty;
                    pParam[2] = "@in_date:" + mModel[i].in_date;
                    pParam[3] = "@in_remark:" + mModel[i].in_remark;
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
