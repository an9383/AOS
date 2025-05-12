using HACCP.Libs.Database;
using HACCP.Models.Change;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Change
{
    public class ChangeControlCompletionService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectChangeControlCompletion(ChangeControlCompletion.SrchParam srchParam)
        {
            string sSpName = "SP_ChangeControlExecution_Y";

            string[] pParam = new string[4];
            pParam[0] = "@s_sdate:" + srchParam.sdate;
            pParam[1] = "@s_edate:" + srchParam.edate;
            pParam[2] = "@s_emp_cd:" + srchParam.emp_cd;
            pParam[3] = "@s_changecontrol_status:" + srchParam.changecontrol_status;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "Select", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable ChangeControlCompletionSelectDetailData(string changecontrol_no)
        {
            string sSpName = "SP_ChangeControlExecution_Y";

            string[] pParam = new string[1];
            pParam[0] = "@changecontrol_no:" + changecontrol_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "Select2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable ChangeControlCompletionSelectSignGridData(string changecontrol_no, string sign_set_cd)
        {
            string sSpName = "SP_ChangeControlExecution_Y";

            string[] pParam = new string[2];
            pParam[0] = "@changecontrol_no:" + changecontrol_no;
            pParam[1] = "@sign_set_cd:" + sign_set_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SelectSign", pParam);

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

        internal string ChangeControlCompletionSignTRX(ChangeControlCompletion changeControlCompletion)
        {
            string sSpName = "SP_ChangeControlExecution_Y";
            string res = "";

            if (changeControlCompletion.gubun.Equals("U"))
            {
                string[] pParam = new string[4];
                pParam[0] = "@changecontrol_no:" + changeControlCompletion.changecontrol_no;
                pParam[1] = "@action_a_emp_cd:" + changeControlCompletion.emp_cd;
                pParam[2] = "@action_a_emp_type:" + changeControlCompletion.emp_type;
                pParam[3] = "@sign_type:" + changeControlCompletion.sign_type;

                res = _bllSpExecute.SpExecuteString(sSpName, "ES", pParam);
            }
            else if (changeControlCompletion.gubun.Equals("D"))
            {
                string[] pParam = new string[2];
                pParam[0] = "@changecontrol_no:" + changeControlCompletion.changecontrol_no;
                pParam[1] = "@sign_type:" + changeControlCompletion.sign_type;

                res = _bllSpExecute.SpExecuteString(sSpName, "ESD", pParam);
            }

            return res;
        }

        internal string ChangeControlCompletionTRX(ChangeControlCompletion changeControlCompletion)
        {
            string sSpName = "SP_ChangeControlExecution_Y";
            string res = "";

            if (changeControlCompletion.gubun.Equals("Update1"))
            {
                string[] pParam = new string[5];
                pParam[0] = "@changecontrol_no:" + changeControlCompletion.changecontrol_no;
                pParam[1] = "@emp_cd:" + changeControlCompletion.emp_cd;
                pParam[2] = "@changecontrol_action_id:" + changeControlCompletion.changecontrol_action_id;
                pParam[3] = "@change_action_plan_contents:" + changeControlCompletion.change_action_plan_contents;
                pParam[4] = "@change_schedule_plan:" + changeControlCompletion.change_schedule_plan;

                res = _bllSpExecute.SpExecuteString(sSpName, changeControlCompletion.gubun, pParam);
            }
            else if (changeControlCompletion.gubun.Equals("Update2"))
            {
                string[] pParam = new string[5];
                pParam[0] = "@changecontrol_no:" + changeControlCompletion.changecontrol_no;
                pParam[1] = "@emp_cd:" + changeControlCompletion.emp_cd;
                pParam[2] = "@changecontrol_action_id:" + changeControlCompletion.changecontrol_action_id;
                pParam[3] = "@change_action_plan_contents:" + changeControlCompletion.change_result_plan_contents;
                pParam[4] = "@change_schedule_result:" + changeControlCompletion.change_schedule_result;

                res = _bllSpExecute.SpExecuteString(sSpName, changeControlCompletion.gubun, pParam);
            }

            return res;
        }
    }
}