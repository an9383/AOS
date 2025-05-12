using HACCP.Libs.Database;
using HACCP.Models.Change;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Change
{
    public class ChangeControlReceiveService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectChangeControlReceive(ChangeControlReceive.SrchParam srchParam)
        {
            string sSpName = "SP_ChangeControlApprove";

            string[] pParam = new string[5];
            pParam[0] = "@de_Sdate:" + srchParam.sdate;
            pParam[1] = "@de_Edate:" + srchParam.edate;
            pParam[2] = "@change_yn:" + srchParam.change_yn;
            pParam[3] = "@change_level1:" + srchParam.change_level1;
            pParam[4] = "@changecontrol_status:" + srchParam.changecontrol_status;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable ChangeControlReceiveSelectSignGridData(string changecontrol_no, string sign_set_cd)
        {
            string sSpName = "SP_ChangeControlApprove";

            string[] pParam = new string[2];
            pParam[0] = "@changecontrol_no:" + changecontrol_no;
            pParam[1] = "@sign_set_cd:" + sign_set_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable tempDt = new DataTable();
            tempDt.TableName = "tempSignDt";
            tempDt.Columns.Add("sign_set_dt_id", typeof(String));
            tempDt.Columns.Add("displayfield", typeof(String));
            tempDt.Columns.Add("sign_yn", typeof(String));
            tempDt.Columns.Add("sign_time", typeof(String));
            tempDt.Columns.Add("sign_emp_cd", typeof(String));
            tempDt.Columns.Add("responsible_emp_cd", typeof(String));
            tempDt.Columns.Add("sign_image", typeof(String));
            tempDt.Columns.Add("responsible_emp_nm", typeof(String));
            tempDt.Columns.Add("sign_emp_nm", typeof(String));
            tempDt.Columns.Add("prev_sign_yn", typeof(String));
            tempDt.Columns.Add("next_sign_yn", typeof(String));
            tempDt.Columns.Add("sign_set_dt_seq", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = tempDt.NewRow();

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

                tempDt.Rows.Add(_row);
            }

            return tempDt;
        }

        internal DataTable ChangeControlReceiveSelectReviewResultData(string changecontrol_no)
        {
            string sSpName = "SP_ChangeControlApprove";

            string[] pParam = new string[1];
            pParam[0] = "@changecontrol_no:" + changecontrol_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S4", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable ChangeControlReceiveSelectChangeOrderData(string changecontrol_no)
        {
            string sSpName = "SP_ChangeControlApprove";

            string[] pParam = new string[1];
            pParam[0] = "@changecontrol_no:" + changecontrol_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable ChangeControlReceiveSelectChangeReviewDocData(string changecontrol_cd)
        {
            string sSpName = "SP_ChangeControlApprove";

            string[] pParam = new string[1];
            pParam[0] = "@changecontrol_cd:" + changecontrol_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S5", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string ChangeControlReceiveSignTRX(ChangeControlReceive changeControlReceive)
        {
            string sSpName = "SP_ChangeControlApprove";
            string res = "";

            if (changeControlReceive.gubun.Equals("U"))
            {
                string[] pParam = new string[3];
                pParam[0] = "@changecontrol_no:" + changeControlReceive.changecontrol_no;
                pParam[1] = "@change_a_emp_cd:" + changeControlReceive.change_a_emp_cd;
                pParam[2] = "@change_a_emp_type:" + changeControlReceive.change_a_emp_type;

                res = _bllSpExecute.SpExecuteString(sSpName, "ES" + (int.Parse(changeControlReceive.tabIndex) + 1).ToString(), pParam);
            }
            else if (changeControlReceive.gubun.Equals("D"))
            {
                string[] pParam = new string[1];
                pParam[0] = "@changecontrol_no:" + changeControlReceive.changecontrol_no;

                res = _bllSpExecute.SpExecuteString(sSpName, "ESD" + (int.Parse(changeControlReceive.tabIndex) + 1).ToString(), pParam);
            }

            return res;
        }

        internal string ChangeControlReceiveTRX(ChangeControlReceive changeControlReceive)
        {
            string sSpName = "SP_ChangeControlApprove";
            string res = "";

            if (changeControlReceive.gubun.Equals("D"))
            {
                res = ChangeControlReviewChangeOrderTRX(changeControlReceive);
            }
            else if (changeControlReceive.isChecked)
            {
                if (changeControlReceive.tabIndex.Equals("1"))
                {
                    res = ChangeControlReviewChangeOrderTRX(changeControlReceive);
                }
            }
            else
            {
                if (changeControlReceive.tabIndex.Equals("0"))
                {
                    string[] pParam = new string[3];
                    pParam[0] = "@changecontrol_no:" + changeControlReceive.changecontrol_no;
                    pParam[1] = "@change_level1:" + changeControlReceive.change_level;
                    pParam[2] = "@change_contents1:" + changeControlReceive.change_contents;

                    res = _bllSpExecute.SpExecuteString(sSpName, "U1", pParam);
                }
                else if (changeControlReceive.tabIndex.Equals("1"))
                {
                    string[] pParam = new string[5];
                    pParam[0] = "@changecontrol_no:" + changeControlReceive.changecontrol_no;
                    pParam[1] = "@change_yn:" + changeControlReceive.change_yn;
                    pParam[2] = "@change_n_remark:" + changeControlReceive.change_n_remark;
                    pParam[3] = "@change_level2:" + changeControlReceive.change_level2;
                    pParam[4] = "@change_contents2:" + changeControlReceive.change_contents2;

                    res = _bllSpExecute.SpExecuteString(sSpName,  "U2", pParam);
                }
            }

            return res;
        }

        private string ChangeControlReviewChangeOrderTRX(ChangeControlReceive changeControlReceive)
        {
            string sSpName = "SP_ChangeControlApprove";
            string res = "";

            if (changeControlReceive.gubun.Equals("I") || changeControlReceive.gubun.Equals("U"))
            {
                string[] pParam = new string[7];
                pParam[0] = "@changecontrol_no:" + changeControlReceive.changecontrol_no;
                pParam[1] = "@changecontrol_action_id:" + changeControlReceive.changecontrol_action_id;
                pParam[2] = "@emp_cd:" + changeControlReceive.emp_cd;
                pParam[3] = "@action_limit:" + changeControlReceive.action_limit;
                pParam[4] = "@order_contents:" + changeControlReceive.order_contents;
                pParam[5] = "@order_dept_cd:" + changeControlReceive.order_dept_cd;
                pParam[6] = "@order_work_process:" + changeControlReceive.order_work_process;

                res = _bllSpExecute.SpExecuteString(sSpName, changeControlReceive.gubun + "O", pParam);
            }
            else if (changeControlReceive.gubun.Equals("D"))
            {
                string[] pParam = new string[2];
                pParam[0] = "@changecontrol_no:" + changeControlReceive.changecontrol_no;
                pParam[1] = "@changecontrol_action_id:" + changeControlReceive.changecontrol_action_id;

                res = _bllSpExecute.SpExecuteString(sSpName, changeControlReceive.gubun + "O", pParam);
            }

            return res;
        }
    }
}