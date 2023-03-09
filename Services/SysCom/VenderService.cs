using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.SysCom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysCom
{
    public class VenderService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable selectVender(string vender_cd, string use_yn, string vender_gb)
        {
            string sSpName = "SP_Vender";

            string gubun = "S";

            string[] param = new string[3];

            param[0] = "@vender_cd_S:" + vender_cd;
            param[1] = "@s_use_yn:" + use_yn;
            param[2] = "@s_vender_gb:" + vender_gb;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable selectVenderPopupData()
        {
            string strsql = "SELECT vender_cd, vender_nm, license, owner_nm, phone, zipcode, address";
            strsql += " FROM vender";
            strsql += " WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%')";
            strsql += " AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }
        internal string venderCdValidation(string vender_cd)
        {
            string sSpName = "SP_Vender";
            string gubun = "vender_cd_S";
            string res = "";

            string[] param = new string[1];

            param[0] = "@vender_cd:" + vender_cd;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);
            if (dt.Rows.Count > 0)
            {
                res = dt.Rows[0][0].ToString();
            }
            else
            {
                res = "";
            }
            
            return res;
        }

        internal string venderCRUD(Vender vender, string pGubun)
        {
            string sSpName = "SP_Vender";

            string gubun = pGubun;
            string message = "";


            if (pGubun.Equals("D"))
            {
                string[] param = new string[1];

                param[0] = "@vender_cd:" + vender.vender_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }
            else
            {
                string[] param = getParam(vender);

                message = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            }

            return message;
        }

        private string[] getParam(Vender vender)
        {
            string[] param = new string[25];

            param[0] = "@vender_cd:" + vender.vender_cd;
            param[1] = "@vender_nm:" + vender.vender_nm;
            param[2] = "@vender_enm:" + vender.vender_enm;
            param[3] = "@uptae:" + vender.uptae;
            param[4] = "@upjong:" + vender.upjong;
            param[5] = "@license:" + vender.license;
            param[6] = "@owner_nm:" + vender.owner_nm;
            param[7] = "@phone:" + vender.phone;
            param[8] = "@fax:" + vender.fax;
            param[9] = "@zipcode:" + vender.zipcode;
            param[10] = "@address:" + vender.address;
            param[11] = "@commercial_personnel:" + vender.commercial_personnel;
            param[12] = "@technical_personnel:" + vender.technical_personnel;
            param[13] = "@personnel_tel1:" + vender.personnel_tel1;
            param[14] = "@personnel_tel2:" + vender.personnel_tel2;
            param[15] = "@personnel_email1:" + vender.personnel_email1;
            param[16] = "@personnel_email2:" + vender.personnel_email2;
            param[17] = "@idate:" + vender.idate;
            param[18] = "@bigo:" + vender.bigo;
            param[19] = "@sell_cust_ck:" + vender.sell_cust_ck;
            param[20] = "@buy_ck:" + vender.buy_ck;
            param[21] = "@email_ad:" + vender.email_ad;
            param[22] = "@use_yn:" + vender.use_yn;
            param[23] = "@at_emp_cd:" + Public_Function.User_cd;
            param[24] = "@vender_gb:" + vender.vender_gb;

            return param;
        }
    }
}
