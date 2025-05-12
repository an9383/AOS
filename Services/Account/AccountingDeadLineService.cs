using HACCP.Libs.Database;
using HACCP.Models.Account;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Account
{
    public class AccountingDeadLineService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable AccountingDeadLineSelect(AccountingDeadLine.SrchParam srch)
        {

            string[] pParam = new string[4];
            pParam[0] = "@p_b_status:" + srch.b_status;
            pParam[1] = "@p_b_sdate:" + srch.sdate;
            pParam[2] = "@p_b_edate:" + srch.edate;
            pParam[3] = "@p_no_type:" + srch.gu;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_AccountingDeadLine", "s1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;

        }

        internal string AccountingDeadLineUpdate(AccountingDeadLine accountingDeadLine)
        {
            string message = "";

            if (accountingDeadLine.row_status == "I")
            {
                //DateTime dt = new DateTime();

                string[] pParam = new string[15];
                pParam[0] = "@p_n_no:" + accountingDeadLine.n_no;
                pParam[1] = "@p_b_no:" + accountingDeadLine.b_no;
                pParam[2] = "@p_i_no:" + accountingDeadLine.i_no;

                if(accountingDeadLine.gu == "매입") pParam[3] = "@p_i_amt:" + accountingDeadLine.i_amt;
                else if (accountingDeadLine.gu == "매출") pParam[3] = "@p_i_amt:" + accountingDeadLine.b_amt;

                pParam[4] = "@p_dc1:" + accountingDeadLine.cust_cd+"/"+ accountingDeadLine.cust_nm;
                pParam[5] = "@p_dc2:" + accountingDeadLine.item_cd+"/"+ accountingDeadLine.item_nm;
                pParam[6] = "@p_final_yn:" + accountingDeadLine.final_yn;
                pParam[7] = "@p_no_gu:" + accountingDeadLine.part_cd;
                pParam[8] = "@p_b_dt:" + accountingDeadLine.b_date;
                pParam[9] = "@p_i_dt:" + accountingDeadLine.i_date;
                pParam[10] = "@p_gu:" + accountingDeadLine.row_status;
                pParam[11] = "@p_no_type:" + accountingDeadLine.gu;
                pParam[12] = "@p_taxfree_yn:" + accountingDeadLine.taxfree_yn;
                pParam[13] = "@p_cust_cd:" + accountingDeadLine.cust_cd;
                pParam[14] = "@p_acct_dt:" + accountingDeadLine.acct_dt;

                message = _bllSpExecute.SpExecuteString("SP_AccountingDeadLine", "e1", pParam);

            }

            if (accountingDeadLine.row_status == "D")
            {
                string[] pParam = new string[3];
               // pParam[0] = "@p_b_no:" + accountingDeadLine.b_no;
                //pParam[1] = "@p_i_no:" + accountingDeadLine.i_no;
                pParam[0] = "@p_gu:" + accountingDeadLine.row_status;
                pParam[1] = "@p_n_no:" + accountingDeadLine.n_no;
                pParam[2] = "@p_cust_cd:" + accountingDeadLine.cust_cd;


                message = _bllSpExecute.SpExecuteString("SP_AccountingDeadLine", "e1", pParam);

            }

            return message;
        }

        internal DataTable AccountingDeadLineDetail1(string i_dt, string cust_cd, string no_type, string acct_dt)
        {
            //DateTime p_i_dt = new DateTime();
            //p_i_dt = DateTime.Parse(i_dt);

            string[] pParam = new string[4];
            pParam[0] = "@p_i_dt:" + i_dt;
            pParam[1] = "@p_cust_cd:" + cust_cd;
            pParam[2] = "@p_no_type:" + no_type;
            pParam[3] = "@p_acct_dt:" + acct_dt;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_AccountingDeadLine", "s2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;

        }

        internal DataTable AccountingDeadLineDetail2(string n_no, string cust_cd, string no_type, string acct_dt)
        {
            //DateTime p_s_dt = Convert.ToDateTime(s_dt);

            string[] pParam = new string[4];
            pParam[0] = "@p_n_no:" + n_no;
            pParam[1] = "@p_cust_cd:" + cust_cd;
            pParam[2] = "@p_no_type:" + no_type;
            pParam[3] = "@p_acct_dt:" + acct_dt;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_AccountingDeadLine", "s3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;

        }


        internal string AccountingDeadLineGetNo()
        {
            string AccountingDeadLineNo = "";

            AccountingDeadLineNo = _bllSpExecute.SpExecuteString("SP_AccountingDeadLine", "num");

            return AccountingDeadLineNo;

        }
    }
}