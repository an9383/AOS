using HACCP.Libs.Database;
using HACCP.Models.DepositMng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.DepositMng
{
    public class DepositSampleService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_DepositSample";

        internal DataTable DepositSampleSelect(DepositSample.SrchParam param)
        {
            string[] pParam = new string[7];
            pParam[0] = "@selecttype:" + param.selecttype;
            pParam[1] = "@sdate:" + param.sdate;
            pParam[2] = "@edate:" + param.edate;
            pParam[3] = "@test_type:" + param.test_type;
            pParam[4] = "@item_cd:" + param.item_cd;
            pParam[5] = "@write_gb:" + param.write_gb;
            pParam[6] = "@selecttype2:" + param.selecttype2;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string DepositSampleTRX(DepositSample depositSample)
        {
            string[] pParam = null;

            switch (depositSample.gubun)
            {
                case "I":

                    pParam = GetParam(depositSample);

                    break;

                case "U":

                    pParam = GetParam(depositSample);

                    break;

                case "D":

                    pParam = new string[2];

                    pParam[0] = "@depositsample_id:" + depositSample.depositsample_id;
                    pParam[1] = "@write_gb:" + depositSample.write_gb;

                    break;
            }

            string res = _bllSpExecute.SpExecuteString(sSpName, depositSample.gubun, pParam);

            return res;
        }

        private string[] GetParam(DepositSample depositSample)
        {
            string[] pParam = new string[27];

            pParam[0] = "@depositsample_id:" + depositSample.depositsample_id;
            pParam[1] = "@test_type:" + depositSample.test_type;
            pParam[2] = "@sampling_date:" + depositSample.sampling_date;
            pParam[3] = "@item_cd:" + depositSample.item_cd;
            pParam[4] = "@order_no:" + depositSample.order_no;
            pParam[5] = "@start_date:" + depositSample.start_date;
            pParam[6] = "@valid_date:" + depositSample.valid_date;
            pParam[7] = "@deposit_date:" + depositSample.deposit_date;
            pParam[8] = "@deposit_emp_cd:" + depositSample.deposit_emp_cd;
            pParam[9] = "@deposit_qty:" + depositSample.deposit_qty;
            pParam[10] = "@deposit_unit_qty:" + depositSample.deposit_unit_qty;
            pParam[11] = "@deposit_unit:" + depositSample.deposit_unit;
            pParam[12] = "@deposit_condition:" + depositSample.deposit_condition;
            pParam[13] = "@deposit_temperature:" + depositSample.deposit_temperature;
            pParam[14] = "@warehouse:" + depositSample.warehouse;
            pParam[15] = "@location:" + depositSample.location;
            pParam[16] = "@stock_qty:" + depositSample.deposit_qty;
            pParam[17] = "@testcontrol_id:" + depositSample.testcontrol_id;
            pParam[18] = "@write_gb:" + depositSample.write_gb;
            pParam[19] = "@stock_unit_qty:" + depositSample.deposit_unit_qty;
            pParam[20] = "@test_no:" + depositSample.test_no;
            pParam[21] = "@deposit_sample_remark:" + depositSample.deposit_sample_remark;
            pParam[22] = "@limited_deposit_date:" + depositSample.limited_deposit_date;
            pParam[23] = "@sampling_emp_cd:" + depositSample.sampling_emp_cd;
            pParam[24] = "@start_no:" + depositSample.start_no;
            pParam[25] = "@barcode_no:" + depositSample.barcode_no;
            pParam[26] = "@edate:" + depositSample.edate;

            return pParam;
        }

        //internal string DepositSampleGetValidDate(string item_cd, string start_date)
        //{
        //    string[] pParam = new string[2];

        //    pParam[0] = "@item_cd:" + item_cd;
        //    pParam[1] = "@start_date:" + start_date;

        //    string res = _bllSpExecute.SpExecuteString(sSpName, "GetValidDate", pParam);

        //    return res;
        //}
    }
}