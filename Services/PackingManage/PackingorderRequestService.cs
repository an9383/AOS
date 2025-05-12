using HACCP.Libs.Database;
using HACCP.Models.PackingManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Transactions;

namespace HACCP.Services.PackingManage
{
    public class PackingorderRequestService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        internal DataTable SelectPackingOrderRquest(PackingOrderRequest.SrchParam packingOrderRequest)
        {
            string sSpName = "SP_Packingorder_Request";
            string[] pParam = new string[5];
            pParam[0] = "@s_sDate:" + packingOrderRequest.sdate;
            pParam[1] = "@s_eDate:" + packingOrderRequest.edate;
            pParam[2] = "@s_item:" + packingOrderRequest.item_cd;
            pParam[3] = "@s_material_status:" + packingOrderRequest.material_status;
            pParam[4] = "@pack_no:" + packingOrderRequest.packing_order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectReleaseStandard(PackingOrderRequest.SrchParam packingOrderRequest)
        {
            string sSpName = "SP_Packingorder_Request";
            string[] pParam = new string[4];
            pParam[0] = "@packing_order_no:" + packingOrderRequest.packing_order_no;
            pParam[1] = "@packing_order_qty:" + packingOrderRequest.packing_order_qty;
            pParam[2] = "@item_cd:" + packingOrderRequest.item_cd;
            pParam[3] = "@batch_size:" + packingOrderRequest.batch_size;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectUsage(PackingOrderRequest.SrchParam packingOrderRequest)
        {
            string sSpName = "SP_Packingorder_Request";
            string[] pParam = new string[3];
            pParam[0] = "@packing_order_no:" + packingOrderRequest.packing_order_no;
            pParam[1] = "@req_order_gb:" + packingOrderRequest.req_order_gb;
            pParam[2] = "@req_order_id:" + packingOrderRequest.req_order_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectCurrentStock(PackingOrderRequest.SrchParam packingOrderRequest)
        {
            string sSpName = "SP_Packingorder_Request";
            string[] pParam = new string[2];
            pParam[0] = "@material_item_cd:" + packingOrderRequest.material_item_cd;
            pParam[1] = "@sale_item_cd:" + packingOrderRequest.item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S4", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal bool InsertUsage(List<PackingOrderRequest> packingOrderRequests, string packing_order_no, string sale_item_cd)
        {
            using (TransactionScope transScope = new TransactionScope())
            {
                foreach (PackingOrderRequest packingOrderRequest in packingOrderRequests)
                {
                    string sSpName = "SP_Packingorder_Request";
                    string[] param = new string[16];
                    param[0] = "@packing_order_no:" + packing_order_no;
                    param[1] = "@req_order_gb:" + packingOrderRequest.req_order_gb;
                    param[2] = "@req_order_id:" + packingOrderRequest.req_order_id;
                    param[3] = "@process_cd:" + packingOrderRequest.process_cd;
                    param[4] = "@req_order_child_cd:" + packingOrderRequest.req_order_child_cd;
                    param[5] = "@req_order_qty:" + packingOrderRequest.req_order_qty;
                    param[6] = "@req_order_calc_type:" + packingOrderRequest.req_order_calc_type;
                    param[7] = "@req_order_last_type:" + packingOrderRequest.req_order_last_type;
                    param[8] = "@req_order_real_qty:" + packingOrderRequest.req_order_real_qty;
                    param[9] = "@req_order_print_yn:" + packingOrderRequest.REQ_ORDER_PRINT_YN;
                    param[10] = "@item_cd:" + packingOrderRequest.req_order_child_cd;
                    param[11] = "@pItem_cd:" + sale_item_cd;
                    param[12] = "@req_order_batch_qty:" + packingOrderRequest.req_order_batch_qty;
                    param[13] = "@item_bom_id:" + packingOrderRequest.item_bom_id;
                    param[14] = "@item_bom_no:" + packingOrderRequest.item_bom_no;
                    param[15] = "@req_order_batch_unit:" + packingOrderRequest.req_order_child_packunit;

                    string res = _bllSpExecute.SpExecuteString(sSpName, "IOO", param).Replace("@req_order_id : ", "");

                    if (String.IsNullOrEmpty(res))
                    {
                        transScope.Dispose();
                        return false;
                    }
                }

                transScope.Complete();
            }

            return true;
        }

        internal string PackingOrderRequestDeleteAllUsage(string order_no)
        {
            string sSpName = "SP_Packingorder_Request";
            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + order_no;

            string res = _bllSpExecute.SpExecuteString(sSpName, "D_ALL", pParam);

            return res;
        }

        internal string PackingOrderRequestDeleteUsage(PackingOrderRequest packingOrderRequest)
        {
            string sSpName = "SP_Packingorder_Request";
            string[] pParam = new string[4];
            pParam[0] = "@order_no:" + packingOrderRequest.packing_order_no;
            pParam[1] = "@req_order_gb:" + packingOrderRequest.req_order_gb;
            pParam[2] = "@req_order_id:" + packingOrderRequest.req_order_id;
            pParam[3] = "@req_order_material_id:" + packingOrderRequest.req_order_material_id;

            string res = _bllSpExecute.SpExecuteString(sSpName, "D2", pParam);

            return res;
        }

        internal string pakcingOrderRequestInsertUsage(PackingOrderRequest packingOrderRequest)
        {
            string res = "";

            string sSpName = "SP_Packingorder_Request";
            string[] pParam = new string[6];
            pParam[0] = "@packing_order_no:" + packingOrderRequest.order_no;
            pParam[1] = "@req_order_gb:" + packingOrderRequest.req_order_gb;
            pParam[2] = "@req_order_id:" + packingOrderRequest.req_order_id;
            pParam[3] = "@req_order_material_id:" + packingOrderRequest.req_order_material_id;
            pParam[4] = "@req_order_material_qc_no:" + packingOrderRequest.req_order_material_qc_no;
            pParam[5] = "@req_order_material_qty:" + packingOrderRequest.req_order_material_qty;

            res = _bllSpExecute.SpExecuteString(sSpName, "I2", pParam);

            return res;
        }

        internal string PackingOrderRequestConfirm(List<PackingOrderRequest> packingOrderRequests)
        {
            try
            {
                string sSpName = "SP_Packingorder_Request";
                string[] pParam = new string[3];

                foreach (PackingOrderRequest tmp in packingOrderRequests)
                {
                    pParam[0] = "@packing_order_no:" + tmp.order_no;
                    pParam[1] = "@req_order_gb:" + tmp.req_order_gb;
                    pParam[2] = "@req_order_id:" + tmp.req_order_id;

                    _bllSpExecute.SpExecuteString(sSpName, "CK_U", pParam);
                }

                _bllSpExecute.SpExecuteString(sSpName, "CK_F");

            }
            catch
            {
                return "불출 확정에 실패했습니다.";
            }

            return "불출 확정 성공하였습니다.";
        }

        internal string CancelReleaseConfirmation(string packing_order_no, string process_cd)
        {
            string sSpName = "SP_Packingorder_Request";
            string[] pParam = new string[2];

            pParam[0] = "@packing_order_no:" + packing_order_no;
            pParam[1] = "@process_cd:" + process_cd;

            string res = _bllSpExecute.SpExecuteString(sSpName, "D3", pParam);

            return int.Parse(res) >= 0 ? "불출취소되었습니다." : "자재출고된 자재는 확정취소를 할 수 없습니다!";
        }
    }
}