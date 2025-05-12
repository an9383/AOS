using HACCP.Libs.Database;
using HACCP.Models.Change;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Change
{
    public class ChangeControlReviewService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectChangeControlReview(ChangeControlReview.SrchParam srchParam)
        {
            string sSpName = "SP_ChangeControlReview_Y";

            string[] pParam = new string[6];
            pParam[0] = "@de_Sdate:" + srchParam.sdate;
            pParam[1] = "@de_Edate:" + srchParam.edate;
            pParam[2] = "@review_result_yn:" + srchParam.review_result_yn;
            pParam[3] = "@review_status:" + srchParam.review_status;
            pParam[4] = "@review_emp_cd:" + srchParam.review_emp_cd;
            pParam[5] = "@emp_cd:" + srchParam.emp_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable ChangeControlReviewSelectSignGridData(ChangeControlReview changeControlReview, int tabIndex)
        {
            string sSpName = "SP_ChangeControlReview_Y";

            string[] pParam = new string[4];
            pParam[0] = "@changecontrol_no:" + changeControlReview.changecontrol_no;
            pParam[1] = "@dept_cd:" + changeControlReview.dept_cd;
            pParam[2] = "@changecontrol_review_no:" + changeControlReview.changecontrol_review_no;
            pParam[3] = "@sign_set_cd:" + "5102";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();
            DataTable tempDt = new DataTable();

            if (ds != null && ds.Tables[0].Rows.Count >= 0)
            {
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }

                if (tabIndex == 0)
                {
                    dt.Rows[4].Delete();
                    dt.Rows[5].Delete();
                }
                else if (tabIndex == 1)
                {
                    dt.Rows[0].Delete();
                    dt.Rows[1].Delete();
                    dt.Rows[2].Delete();
                    dt.Rows[3].Delete();
                    dt.Rows[5].Delete();
                }
                else if (tabIndex == 2)
                {
                    dt.Rows[0].Delete();
                    dt.Rows[1].Delete();
                    dt.Rows[2].Delete();
                    dt.Rows[3].Delete();
                    dt.Rows[4].Delete();
                }

                dt.AcceptChanges();

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
            }

            return tempDt;
        }

        internal DataTable ChangeControlReviewSelectDocGridData(string changecontrol_no)
        {
            string sSpName = "SP_ChangeRequest";

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

        internal DataTable ChangeControlReviewSelectSubGridData(string changecontrol_no, string dept_cd, string changecontrol_review_no)
        {
            string sSpName = "SP_ChangeControlReview_Y";

            string[] pParam = new string[3];
            pParam[0] = "@changecontrol_no:" + changecontrol_no;
            pParam[1] = "@dept_cd:" + dept_cd;
            pParam[2] = "@changecontrol_review_no:" + changecontrol_review_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string ChangeControlReviewSignTRX(ChangeControlReview changeControlReview)
        {
            string sSpName = "SP_ChangeControlReview_Y";
            string res = "";

            if (changeControlReview.tab_index.Equals("0"))
            {
                changeControlReview.sign_type = ("1-" + changeControlReview.sign_type);
            }
            else if (changeControlReview.tab_index.Equals("1"))
            {
                changeControlReview.sign_type = "2";
            }
            else if (changeControlReview.tab_index.Equals("2"))
            {
                changeControlReview.sign_type = "3";
            }

            if (changeControlReview.gubun.Equals("U"))
            {
                string[] pParam = new string[6];
                pParam[0] = "@changecontrol_no:" + changeControlReview.changecontrol_no;
                pParam[1] = "@dept_cd:" + changeControlReview.dept_cd;
                pParam[2] = "@review_emp_cd:" + changeControlReview.review_emp_cd;
                pParam[3] = "@review_emp_type:" + changeControlReview.review_emp_type;
                pParam[4] = "@sign_type:" + changeControlReview.sign_type;
                pParam[5] = "@changecontrol_review_no:" + changeControlReview.changecontrol_review_no;

                res = _bllSpExecute.SpExecuteString(sSpName, "ES", pParam);
            }
            else if (changeControlReview.gubun.Equals("D"))
            {
                string[] pParam = new string[4];
                pParam[0] = "@changecontrol_no:" + changeControlReview.changecontrol_no;
                pParam[1] = "@dept_cd:" + changeControlReview.dept_cd;
                pParam[2] = "@changecontrol_review_no:" + changeControlReview.changecontrol_review_no;
                pParam[3] = "@sign_type:" + changeControlReview.sign_type;

                res = _bllSpExecute.SpExecuteString(sSpName, "ESD", pParam);
            }

            return res;
        }

        internal string ChangeControlReviewTRX(ChangeControlReview paramData)
        {
            string sSpName = "SP_ChangeControlReview_Y";
            string gubun = "";
            string res = "";

            if (paramData.gubun.Equals("U"))
            {
                if (paramData.tab_index.Equals("0"))
                {
                    gubun = "U1";

                    string[] pParam = new string[6];
                    pParam[0] = "@review_emp_cd:" + paramData.review_emp_cd;
                    pParam[1] = "@review_result:" + paramData.review_result;
                    pParam[2] = "@review_change_class:" + paramData.review_change_class;
                    pParam[3] = "@changecontrol_no:" + paramData.changecontrol_no;
                    pParam[4] = "@dept_cd:" + paramData.dept_cd;
                    pParam[5] = "@changecontrol_review_no:" + paramData.changecontrol_review_no;

                    res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                }
                else if (paramData.tab_index.Equals("1"))
                {
                    gubun = "U2";

                    string[] pParam = new string[8];
                    pParam[0] = "@review_emp_cd:" + paramData.review_emp_cd;
                    pParam[1] = "@review_result:" + paramData.review_result;
                    pParam[2] = "@review_special:" + paramData.review_change_special;
                    pParam[3] = "@review_change_class:" + paramData.review_change_class;
                    pParam[4] = "@review_change_level:" + paramData.review_change_level;
                    pParam[5] = "@changecontrol_no:" + paramData.changecontrol_no;
                    pParam[6] = "@dept_cd:" + paramData.dept_cd;
                    pParam[7] = "@changecontrol_review_no:" + paramData.changecontrol_review_no;

                    res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                }
                else if (paramData.tab_index.Equals("2"))
                {
                    gubun = "U3";

                    string[] pParam = new string[9];
                    pParam[0] = "@review_confirm_emp_cd:" + paramData.review_confirm_emp_cd;
                    pParam[1] = "@review_confirm_result:" + paramData.review_confirm_result;
                    pParam[2] = "@review_special:" + paramData.review_change_special;
                    pParam[3] = "@review_change_class:" + paramData.review_change_class;
                    pParam[4] = "@review_change_level:" + paramData.review_change_level;
                    pParam[5] = "@review_result_yn:" + paramData.review_result_yn;
                    pParam[6] = "@changecontrol_no:" + paramData.changecontrol_no;
                    pParam[7] = "@dept_cd:" + paramData.dept_cd;
                    pParam[8] = "@changecontrol_review_no:" + paramData.changecontrol_review_no;

                    res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                }
            }

            return res;
        }

        internal string ChangeControlReviewItemUpdate(List<ChangeControlReview> paramData)
        {
            string sSpName = "SP_ChangeControlReview_Y";
            string res = "";

            foreach (ChangeControlReview tmp in paramData)
            {
                if (String.IsNullOrEmpty(tmp.changecontrol_sop_item_yn) || tmp.changecontrol_sop_item_yn.Equals("false"))
                {
                    tmp.changecontrol_sop_item_yn = "N";
                }else if(tmp.changecontrol_sop_item_yn.Equals("true") || tmp.changecontrol_sop_item_yn.Equals("Y"))
                {
                    tmp.changecontrol_sop_item_yn = "Y";
                }

                string[] pParam = new string[5];
                pParam[0] = "@changecontrol_no:" + tmp.changecontrol_no;
                pParam[1] = "@dept_cd:" + tmp.dept_cd;
                pParam[2] = "@changecontrol_review_id:" + tmp.changecontrol_review_id;
                pParam[3] = "@changecontrol_sop_item_yn:" + tmp.changecontrol_sop_item_yn;
                pParam[4] = "@changecontrol_sop_item_remark:" + tmp.changecontrol_sop_item_remark;

                res = _bllSpExecute.SpExecuteString(sSpName, "UI", pParam);
            }

            return res;
        }
    }
}