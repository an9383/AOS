using HACCP.Libs.Database;
using HACCP.Libs;
using HACCP.Models.LocMng;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Services.LocMng
{
    public class CustomCustReg2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select(string s_cust_cd)
        {
            string sSpName = "SP_CustomCustReg2";
            string sGubun = "S";
            string[] pParam = new string[1];

            pParam[0] = "@cust_cd: " + s_cust_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable CustomCustReg2Popup()
        {
            string strsql = "SELECT vender_cd, vender_nm, license, owner_nm, phone, zipcode, address";
            strsql += " FROM vender";
            strsql += " WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%')";
            strsql += " AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }


        internal string CustomCustReg2CRUD(CustomCustReg2 cModel, string gubun)
        {
            string sSpName = "SP_CustomCustReg2";
            string sGubun = gubun;

            string message = "";

            if (cModel.Equals("D"))
            {
                string[] pParam = new string[3];
                pParam[0] = "@plant_cd:" + cModel.plant_cd;
                pParam[1] = "@cust_cd:" + cModel.cust_cd;
                pParam[2] = "@custIn_cd:" + cModel.custIn_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else
            {
                string[] pParam = GetParam(cModel);

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return message;
        }

        private string[] GetParam(CustomCustReg2 customCustReg2)
        {
            string[] param = new string[5];

            //기본정보
            param[0] = "@s_cust_cd:" + customCustReg2.s_cust_cd;
            param[1] = "@plant_cd:" + HttpContext.Current.Session["plantCD"];
            param[2] = "@cust_cd:" + customCustReg2.cust_cd;
            param[3] = "@custIn_cd:" + customCustReg2.custIn_cd;
            param[4] = "@custIn_remark:" + customCustReg2.custIn_remark;
            return param;
        }
    }
}