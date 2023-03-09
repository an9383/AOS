using HACCP.Libs.Database;
using HACCP.Models.StabilityTest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.StabilityTest
{
    public class StabilityTestReceiptService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_StabilityTestReceipt";

        internal DataTable StabilityTestReceiptSelect(StabilityTestReceipt.SrchParam param)
        {
            string[] pParam = new string[6];
            pParam[0] = "@start_date_s:" + param.start_date;
            pParam[1] = "@end_date_s:" + param.end_date;
            pParam[2] = "@stability_test_type_s:" + param.stability_test_type;
            pParam[3] = "@item_cd:" + param.s_item_cd;
            pParam[4] = "@date_type_s:" + param.date_type;
            pParam[5] = "@stability_test_type2_s:" + param.stability_test_type2;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SM", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string StabilityTestReceipt(StabilityTestReceipt stabilityTestReceipt)
        {
            string[] pParam = new string[22];

            pParam[0] = "@item_cd:" + stabilityTestReceipt.item_cd;
            pParam[1] = "@order_no:" + stabilityTestReceipt.order_no;
            pParam[2] = "@start_date:" + stabilityTestReceipt.start_date;
            pParam[3] = "@start_qty:" + stabilityTestReceipt.start_qty;
            pParam[4] = "@valid_month:" + stabilityTestReceipt.valid_month;
            pParam[5] = "@end_date:" + stabilityTestReceipt.end_date;
            pParam[6] = "@stability_qty:" + stabilityTestReceipt.stability_qty;
            pParam[7] = "@stability_unit_qty:" + stabilityTestReceipt.stability_unit_qty;
            pParam[8] = "@testcontrol_id:" + stabilityTestReceipt.testcontrol_id;
            pParam[9] = "@stability_test_type:" + stabilityTestReceipt.stability_test_type;
            pParam[10] = "@stability_test_type2:" + stabilityTestReceipt.stability_test_type2;
            pParam[11] = "@comment:" + stabilityTestReceipt.comment;
            pParam[12] = "@packing_type:" + stabilityTestReceipt.packing_type;
            pParam[13] = "@create_emp_cd:" + stabilityTestReceipt.create_emp_cd;
            pParam[14] = "@create_date:" + stabilityTestReceipt.create_date;
            pParam[15] = "@keeping_condition:" + stabilityTestReceipt.keeping_condition;
            pParam[16] = "@stability_test_remark:" + stabilityTestReceipt.stability_test_remark;
            pParam[17] = "@stability_test_id:" + stabilityTestReceipt.stability_test_id;
            pParam[18] = "@doc_no:" + stabilityTestReceipt.doc_no;
            pParam[19] = "@depositsample_id:" + stabilityTestReceipt.depositsample_id;
            pParam[20] = "@stability_test_purpose:" + stabilityTestReceipt.stability_test_purpose;
            pParam[21] = "@test_standard:" + stabilityTestReceipt.test_standard;

            string res = _bllSpExecute.SpExecuteString(sSpName, stabilityTestReceipt.gubun, pParam);

            return res;
        }

        internal DataTable StabilityTestReceiptGetValidDate(string item_cd)
        {
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "GetValidMonth", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }


        internal DataTable StabilityTestReceiptGetLotDate(string item_cd)
        {
            string[] pParam = new string[1];
            pParam[0] = "@s_item:" + item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_ItemStockStatus", "T3S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string StabilityTestReceiptGetKeepingCondition(string stability_test_type)
        {
            string[] pParam = new string[1];
            pParam[0] = "@stability_test_type:" + stability_test_type;

            string res = _bllSpExecute.SpExecuteString(sSpName, "T", pParam);

            return res;
        }
    }
}