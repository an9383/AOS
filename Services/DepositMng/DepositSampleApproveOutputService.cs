using HACCP.Libs.Database;
using HACCP.Models.DepositMng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.DepositMng
{
    public class DepositSampleApproveOutputService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_DepositSampleApproveOut";

        internal DataTable DepositSampleApproveOutputSelect(DepositSampleUse.SrchParam param)
        {
            string[] pParam = new string[7];
            pParam[0] = "@selecttype:" + param.selecttype;
            pParam[1] = "@sdate:" + param.sdate;
            pParam[2] = "@edate:" + param.edate;
            pParam[3] = "@test_type:" + param.test_type;
            pParam[4] = "@item_cd:" + param.item_cd;
            pParam[5] = "@status:" + param.status;
            pParam[6] = "@write_gb:" + param.write_gb;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable DepositSampleApproveOutputSelectSign(string depositsampleuse_id, string sign_set_cd)
        {
            string[] pParam = new string[2];
            pParam[0] = "@depositsampleuse_id:" + depositsampleuse_id;
            pParam[1] = "@sign_set_cd:" + sign_set_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "Sign_S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();
            table.Columns.Add("sign_set_dt_id", typeof(String));
            table.Columns.Add("displayfield", typeof(String));
            table.Columns.Add("sign_yn", typeof(String));
            table.Columns.Add("sign_time", typeof(String));
            table.Columns.Add("sign_emp_cd", typeof(String));
            table.Columns.Add("responsible_emp_cd", typeof(String));
            table.Columns.Add("sign_image", typeof(String));
            table.Columns.Add("responsible_emp_nm", typeof(String));
            table.Columns.Add("sign_emp_nm", typeof(String));
            table.Columns.Add("prev_sign_yn", typeof(String));
            table.Columns.Add("next_sign_yn", typeof(String));
            table.Columns.Add("sign_set_dt_seq", typeof(String));
            table.Columns.Add("sign_tot_cnt", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["sign_set_dt_id"] = row["sign_set_dt_id"].ToString();
                _row["displayfield"] = row["displayfield"].ToString();
                _row["sign_yn"] = row["sign_yn"].ToString();
                _row["sign_time"] = row["sign_time"].ToString();
                _row["sign_emp_cd"] = row["sign_emp_cd"].ToString();
                _row["responsible_emp_cd"] = row["responsible_emp_cd"].ToString();

                if (row["sign_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["sign_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["sign_image"] = url;
                }
                else
                {
                    _row["sign_image"] = "/Content/image/defaultSign.png";
                }

                _row["responsible_emp_nm"] = row["responsible_emp_nm"].ToString();
                _row["sign_emp_nm"] = row["sign_emp_nm"].ToString();
                _row["prev_sign_yn"] = row["prev_sign_yn"].ToString();
                _row["next_sign_yn"] = row["next_sign_yn"].ToString();
                _row["sign_set_dt_seq"] = row["sign_set_dt_seq"].ToString();
                _row["sign_tot_cnt"] = row["sign_tot_cnt"].ToString();

                table.Rows.Add(_row);
            }

            return table;
        }

        internal string DepositSampleApproveOutputSelectSignTRX(DepositSampleUse.SignParam signParam)
        {
            string[] pParam = new string[8];
            pParam[0] = "@emp_cd:" + signParam.emp_cd;
            pParam[1] = "@sign_type:" + signParam.sign_type;
            pParam[2] = "@depositsample_id:" + signParam.depositsample_id;
            pParam[3] = "@depositsampleuse_id:" + signParam.depositsampleuse_id;
            pParam[4] = "@sign_set_cd:" + signParam.sign_set_cd;
            pParam[5] = "@sign_set_dt_id:" + signParam.sign_set_dt_id;
            pParam[6] = "@sign_tot_cnt:" + signParam.sign_tot_cnt;
            pParam[7] = "@sign_set_dt_seq:" + signParam.sign_set_dt_seq;

            string res = "";

            switch (signParam.gubun)
            {
                case "U":

                    res = _bllSpExecute.SpExecuteString(sSpName, "Save_Sign", pParam);

                    break;

                case "D":

                    res = _bllSpExecute.SpExecuteString(sSpName, "Del_Sign", pParam);

                    break;
            }

            return res;
        }

        internal string DepositSampleApproveOutput2TRX(DepositSampleUse depositSampleUse)
        {
            string[] pParam = new string[6];
            pParam[0] = "@depositsample_id:" + depositSampleUse.depositsample_id;
            pParam[1] = "@depositsampleuse_id:" + depositSampleUse.depositsampleuse_id;
            pParam[2] = "@output_date:" + depositSampleUse.out_date;
            pParam[3] = "@output_emp_cd:" + depositSampleUse.out_emp_cd;
            pParam[4] = "@status:" + depositSampleUse.status_cd;
            pParam[5] = "@write_gb:" + depositSampleUse.write_gb;

            string res = _bllSpExecute.SpExecuteString(sSpName, depositSampleUse.gubun, pParam);

            return res;
        }

        internal string DepositSampleApproveOutput3TRX(DepositSampleUse depositSampleUse)
        {
            string spGubun = "";

            string[] pParam = new string[7];
            pParam[0] = "@depositsample_id:" + depositSampleUse.depositsample_id;
            pParam[1] = "@depositsampleuse_id:" + depositSampleUse.depositsampleuse_id;
            pParam[2] = "@refund_qty:" + depositSampleUse.refund_qty;
            pParam[3] = "@refund_unit_qty:" + depositSampleUse.refund_unit_qty;
            pParam[4] = "@refund_date:" + depositSampleUse.refund_date;
            pParam[5] = "@refund_emp_cd:" + depositSampleUse.refund_emp_cd;
            pParam[6] = "@write_gb:" + depositSampleUse.write_gb;

            switch (depositSampleUse.gubun)
            {
                case "U":

                    spGubun = "U2";

                    break;

                case "D":

                    spGubun = "D_refund";

                    break;
            }

            string res = _bllSpExecute.SpExecuteString(sSpName, spGubun, pParam);

            return res;
        }
    }
}