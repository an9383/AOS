using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.PackingManage;
using HACCP.Models.ProductionManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PackingManage
{
    public class ItemLabelPrintService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectPackingOrderData(ItemLabelPrint.SrchParam srchParam)
        {
            string sSpName = "SP_ItemLabelPrint";

            string[] pParam = new string[5];
            pParam[0] = "@s_sdate:" + srchParam.s_sdate;
            pParam[1] = "@s_edate:" + srchParam.s_edate;
            pParam[2] = "@s_item_cd:" + srchParam.s_item_cd;
            pParam[3] = "@s_lot_no:" + srchParam.s_lot_no;
            pParam[4] = "@s_order_status:" + srchParam.s_order_status;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataSet SelectProductReceivingData(ItemLabelPrint.SrchParam srchParam)
        {
            string sSpName = "SP_ItemLabelPrint";

            string[] pParam = new string[1];
            pParam[0] = "@packing_order_no:" + srchParam.packing_order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            return ds;
        }

        internal DataTable SelectPalletData(string item_stock_id)
        {
            string sSpName = "SP_ItemLabelPrint";

            string[] pParam = new string[1];
            pParam[0] = "@item_stock_id:" + item_stock_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string ItemLabelPrintPalletCRUD(ItemLabelPrint palletMain)
        {
            string sSpName = "SP_ItemLabelPrint";

            string[] pParam;

            if (palletMain.gubun.Equals("D1"))
            {
                pParam = new string[1];
                pParam[0] = "@item_stock_id:" + palletMain.item_stock_id;

                _bllSpExecute.SpExecuteString(sSpName, palletMain.gubun, pParam);
                return "삭제에 성공하였습니다.";
            }

            pParam = new string[10];
            pParam[0] = "@item_cd:" + palletMain.item_cd;
            pParam[1] = "@lot_date:" + palletMain.lot_date;
            pParam[2] = "@valid_date:" + palletMain.valid_date;
            pParam[3] = "@lot_no:" + palletMain.lot_no;
            pParam[4] = "@packing_order_no:" + palletMain.packing_order_no;
            pParam[5] = "@receipt_qty:" + palletMain.receipt_qty;
            pParam[6] = "@receipt_date:" + palletMain.receipt_date;
            pParam[7] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[8] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[9] = "@item_stock_id:" + palletMain.item_stock_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, palletMain.gubun, pParam);

            if (palletMain.gubun == "I1")
            {
                DataTable dt = new DataTable();

                if (ds != null)
                {
                    dt = ds.Tables[0];
                }

                string item_stock_id = "";

                if (dt.Rows.Count > 0)
                {
                    item_stock_id = dt.Rows[0]["item_stock_id"].ToString();
                }
                else
                {
                    item_stock_id = "";
                }

                return item_stock_id;
            }
            else
            {
                return palletMain.item_stock_id;
            }
        }

        internal string ItemLabelPrintPalletDetailCRUD(List<ItemLabelPrint> palletData, string item_stock_id, string pallet_unit)
        {
            string sSpName = "SP_ItemLabelPrint";

            foreach (ItemLabelPrint tmp in palletData)
            {
                if (tmp.gubun == "I2")
                {
                    string[] pParam = new string[6];
                    pParam[0] = "@item_stock_id:" + item_stock_id;
                    pParam[1] = "@prod_qty:" + tmp.prod_qty;
                    pParam[2] = "@pallet_unit:" + pallet_unit;
                    pParam[3] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();
                    pParam[4] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();
                    pParam[5] = "@receipt_date:" + tmp.Work_Date;

                    _bllSpExecute.SpExecuteString(sSpName, tmp.gubun, pParam);

                }
                else if (tmp.gubun == "U2")
                {
                    string[] pParam = new string[6];
                    pParam[0] = "@box_barcode_no:" + tmp.box_barcode_no;
                    pParam[1] = "@prod_qty:" + tmp.prod_qty;
                    pParam[2] = "@pallet_unit:" + pallet_unit;
                    pParam[3] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();
                    pParam[4] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();
                    pParam[5] = "@receipt_date:" + tmp.Work_Date;

                     _bllSpExecute.SpExecuteString(sSpName, tmp.gubun, pParam);

                }
                else if (tmp.gubun == "PalletDelete")
                {
                    string[] pParam = new string[2];
                    pParam[0] = "@box_barcode_no:" + tmp.box_barcode_no;
                    pParam[1] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();

                    _bllSpExecute.SpExecuteString(sSpName, tmp.gubun, pParam);

                }

            }

            return "저장하였습니다.";
        }

        internal DataTable ItemLabelPrintTransferStatus(string packing_order_no, string order_no)
        {
            string sSpName = "SP_ItemLabelPrint";

            string[] pParam = new string[2];
            pParam[0] = "@packing_order_no:" + packing_order_no;
            pParam[1] = "@order_no:" + order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "TransferStatus", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable ItemLabelPrintSelectPalletDate(string item_stock_id)
        {
            string sSpName = "SP_ItemLabelPrint";

            string[] pParam = new string[1];
            pParam[0] = "@item_stock_id:" + item_stock_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S4", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string CancelRequestTest(OrderProcResultForInsert2 param)
        {
            string sSpName = "SP_ItemLabelPrint";

            string[] pParam = new string[5];
            pParam[0] = "@packing_order_no:" + param.packing_order_no;
            pParam[1] = "@order_no:" + param.order_no;
            pParam[2] = "@order_proc_id:" + param.order_proc_id;
            pParam[3] = "@process_cd:" + param.process_cd;
            pParam[4] = "@testrequest_no:" + param.testrequest_no;

            string res = _bllSpExecute.SpExecuteString(sSpName, "DE", pParam);

            return res;
        }

        internal string SelectPalletCnt(string item_stock_id)
        {
            string sSpName = "SP_ItemLabelPrint";

            string[] pParam = new string[1];
            pParam[0] = "@item_stock_id:" + item_stock_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S5", pParam);

            return dt.Rows[0].ItemArray[0].ToString();
        }

        internal string PerformanceComplete(ItemLabelPrint itemLabelPrint)
        {
            string sSpName = "SP_ItemLabelPrint";

            string[] pParam = new string[5];
            pParam[0] = "@item_cd:" + itemLabelPrint.item_cd;
            pParam[1] = "@lot_no:" + itemLabelPrint.lot_no;
            pParam[2] = "@packing_order_no:" + itemLabelPrint.packing_order_no;
            pParam[3] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[4] = "@testrequest_no:" + itemLabelPrint.testrequest_no;

            string res = _bllSpExecute.SpExecuteString(sSpName, "U3", pParam);

            return res;
        }

        internal string ItemLabelPrintPackingStatusChange(string lot_no, string packing_order_no)
        {
            string sSpName = "SP_ItemLabelPrint";

            string[] pParam = new string[2];
            pParam[0] = "@lot_no:" + lot_no;
            pParam[1] = "@packing_order_no:" + packing_order_no;


            string res = _bllSpExecute.SpExecuteString(sSpName, "U4", pParam);

            return res;
        }
    }
}