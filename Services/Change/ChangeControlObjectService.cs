using HACCP.Libs.Database;
using HACCP.Models.Change;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Change
{
    public class ChangeControlObjectService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectChangeControlObject(string changecontrol_cd)
        {
            string sSpName = "SP_ChangeControlObject";

            string[] pParam = new string[1];
            pParam[0] = "@s_changecontrol_cd:" + changecontrol_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "Select", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectChangeControlObjectSelectDept(string changecontrol_cd)
        {
            string sSpName = "SP_ChangeControlObject";

            string[] pParam = new string[1];
            pParam[0] = "@changecontrol_cd:" + changecontrol_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SelectDept", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectChangeControlObjectSelectDoc(string changecontrol_cd)
        {
            string sSpName = "SP_ChangeControlObject";

            string[] pParam = new string[1];
            pParam[0] = "@changecontrol_cd:" + changecontrol_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SelectDoc", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string ChangeControlObjectDeptTRX(List<ChangeControlObject> paramData)
        {
            string sSpName = "SP_ChangeControlObject";
            string res = "";

            foreach (ChangeControlObject tmp in paramData)
            {
                if (String.IsNullOrEmpty(tmp.review_dept_gubun_yn))
                {
                    tmp.review_dept_gubun_yn = "N";
                }

                tmp.review_dept_gubun_yn = ((tmp.review_dept_gubun_yn.Equals("Y") || tmp.review_dept_gubun_yn.Equals("true"))
                    ? "Y" : "N");

                if (tmp.gubun.Equals("I"))
                {
                    string[] pParam = new string[5];
                    pParam[0] = "@changecontrol_cd:" + tmp.changecontrol_cd;
                    pParam[1] = "@dept_cd:" + tmp.dept_cd;
                    pParam[2] = "@review_contents:" + tmp.review_contents;
                    pParam[3] = "@emp_cd:" + tmp.emp_cd;
                    pParam[4] = "@review_dept_gubun_yn:" + tmp.review_dept_gubun_yn;

                    res = _bllSpExecute.SpExecuteString(sSpName, "InsertDept", pParam);
                }
                else if (tmp.gubun.Equals("U"))
                {
                    string[] pParam = new string[6];
                    pParam[0] = "@changecontrol_cd:" + tmp.changecontrol_cd;
                    pParam[1] = "@changecontrol_sop_dept_id:" + tmp.changecontrol_sop_dept_id;
                    pParam[2] = "@dept_cd:" + tmp.dept_cd;
                    pParam[3] = "@review_contents:" + tmp.review_contents;
                    pParam[4] = "@emp_cd:" + tmp.emp_cd;
                    pParam[5] = "@review_dept_gubun_yn:" + tmp.review_dept_gubun_yn;

                    res = _bllSpExecute.SpExecuteString(sSpName, "UpdateDept", pParam);
                }
                else if (tmp.gubun.Equals("D"))
                {
                    string[] pParam = new string[2];
                    pParam[0] = "@changecontrol_cd:" + tmp.changecontrol_cd;
                    pParam[1] = "@changecontrol_sop_dept_id:" + tmp.changecontrol_sop_dept_id;

                    res = _bllSpExecute.SpExecuteString(sSpName, "DeleteDept", pParam);
                }
            }

            return res;
        }

        internal string ChangeControlObjectDocTRX(List<ChangeControlObject> paramData)
        {
            string sSpName = "SP_ChangeControlObject";
            string res = "";

            foreach (ChangeControlObject tmp in paramData)
            {
                if (tmp.gubun.Equals("I"))
                {
                    string[] pParam = new string[2];
                    pParam[0] = "@changecontrol_cd:" + tmp.changecontrol_cd;
                    pParam[1] = "@changecontrol_sop_doc_contents:" + tmp.changecontrol_sop_doc_contents;

                    res = _bllSpExecute.SpExecuteString(sSpName, "InsertDoc", pParam);
                }
                else if (tmp.gubun.Equals("U"))
                {
                    string[] pParam = new string[3];
                    pParam[0] = "@changecontrol_cd:" + tmp.changecontrol_cd;
                    pParam[1] = "@changecontrol_sop_dept_id:" + tmp.changecontrol_sop_dept_id;
                    pParam[2] = "@changecontrol_sop_doc_contents:" + tmp.changecontrol_sop_doc_contents;

                    res = _bllSpExecute.SpExecuteString(sSpName, "UpdateDoc", pParam);
                }
                else if (tmp.gubun.Equals("D"))
                {
                    string[] pParam = new string[2];
                    pParam[0] = "@changecontrol_cd:" + tmp.changecontrol_cd;
                    pParam[1] = "@changecontrol_sop_dept_id:" + tmp.changecontrol_sop_dept_id;

                    res = _bllSpExecute.SpExecuteString(sSpName, "DeleteDoc", pParam);
                }
            }

            return res;
        }

    }
}