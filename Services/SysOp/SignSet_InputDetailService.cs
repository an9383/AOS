using HACCP.Libs.Database;
using HACCP.Models.SysOp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysOp
{
    public class SignSet_InputDetailService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable selectSignData(string signSetCd, string selectS)
        {
            string sSpName = "SP_SignSet_InputDetail";

            string gubun = "S";

            string[] pParam = new string[2];
            pParam[0] = "@sign_set_cd:" + signSetCd;
            pParam[1] = "@select_S:" + selectS;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable selectSignatoryData(string signSetCd)
        {
            string sSpName = "SP_SignSet_InputDetail";

            string gubun = "S1";

            string[] pParam = new string[1];
            pParam[0] = "@sign_set_cd:" + signSetCd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable selectDelegateData(string signSetCd, string signSetDt)
        {
            string sSpName = "SP_SignSet_InputDetail";

            string gubun = "S2";

            string[] pParam = new string[2];
            pParam[0] = "@sign_set_cd:" + signSetCd;
            pParam[1] = "@sign_set_dt_id:" + signSetDt;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable selectEmpData()
        {
            string sSpName = "SP_SignSet_InputDetail";

            string gubun = "E";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable selectSignPopupData()
        {
            string strsql = "SELECT sign_set_cd, sign_set_nm";
            strsql += " FROM sign_set ";
            strsql += " WHERE (sign_set_cd  LIKE '%%' OR sign_set_nm  LIKE '%%') ";
            strsql += " AND sign_set_cd  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal string signatoryCRUD(Signatory signatory, string pGubun)
        {
            string sSpName = "SP_SignSet_InputDetail";
            string gubun = pGubun;

            string[] pParam = new string[2];

            if (pGubun.Equals("D1"))
            {
                pParam[0] = "@sign_set_cd:" + signatory.sign_set_cd;
                pParam[1] = "@sign_set_dt_id:" + signatory.sign_set_dt_id;
            }
            else
            {
                signatory = setBool(signatory);

                pParam = getParam(signatory);
            }

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }
        internal string delegateCRUD(Signatory signatory, string pGubun)
        {
            string sSpName = "SP_SignSet_InputDetail";
            string gubun = pGubun;

            string[] pParam = new string[3];

            pParam[0] = "@sign_set_cd:" + signatory.sign_set_cd;
            pParam[1] = "@sign_set_dt_id:" + signatory.sign_set_dt_id;
            pParam[2] = "@emp_cd:" + signatory.emp_cd;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }


        private string[] getParam(Signatory signatory)
        {
            string[] pParam = new string[7];

            pParam[0] = "@sign_set_cd:" + signatory.sign_set_cd;
            pParam[1] = "@sign_set_dt_id:" + signatory.sign_set_dt_id;
            pParam[2] = "@sign_set_dt_seq:" + signatory.sign_set_dt_seq;
            pParam[3] = "@sign_set_dt_nm:" + signatory.sign_set_dt_nm;
            pParam[4] = "@emp_cd:" + signatory.emp_cd;
            pParam[5] = "@necessary_yn:" + signatory.necessary_yn;
            pParam[6] = "@representative_yn:" + signatory.representative_yn;

            return pParam;
        }

        internal Signatory setBool(Signatory signatory)
        {
            if (signatory.necessary_yn == null || signatory.necessary_yn.Equals("N") || signatory.necessary_yn.Equals("false"))
            {
                signatory.necessary_yn = "N";
            }
            else if (signatory.necessary_yn.Equals("Y") || signatory.necessary_yn.Equals("true"))
            {
                signatory.necessary_yn = "Y";
            }

            if(signatory.representative_yn == null || signatory.representative_yn.Equals("N") || signatory.representative_yn.Equals("false"))
            {
                signatory.representative_yn = "N";
            }
            else if (signatory.representative_yn.Equals("Y") || signatory.representative_yn.Equals("true"))
            {
                signatory.representative_yn = "Y";
            }

            return signatory;
        }


    }
}
