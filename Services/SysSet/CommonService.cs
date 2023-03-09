using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.SysCom;
using HACCP.Models.SysSet;

namespace HACCP.Services.SysSet
{
    public class CommonService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select(string sCommon_cd, string sCommon_part_nm)
        {
            string sSpName = "SP_Common";
            string sGubun = "S";
            string[] pParam = new string[2];
            pParam[0] = "@S_COMMON_CD:" + sCommon_cd;
            pParam[1] = "@S_COMMON_PART_NM:" + sCommon_part_nm;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable SelectCommonCodePopupData()
        {
            string strsql = "SELECT common_cd, common_nm";
            strsql += " FROM v_common";
            strsql += " WHERE (common_cd  LIKE '%%' OR common_nm  LIKE '%%')";
            strsql += " AND common_cd  LIKE '%%' AND common_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal string CommonCodeCRUD(CommonCode commonCode, string pGubun)
        {
            string sSpName = "SP_Common";
            string sGubun = pGubun;

            string message = "";

            if (pGubun.Equals("D"))
            {
                string[] pParam = new string[2];
                pParam[0] = "@COMMON_CD:" + commonCode.common_cd;
                pParam[1] = "@COMMON_PART_CD:" + commonCode.common_part_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else
            {
                string[] pParam = GetParam(commonCode);

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return message;
        }

        private string[] GetParam(CommonCode commonCode)
        {
            string[] param = new string[13];

            param[0] = "@COMMON_CD:" + commonCode.common_cd;
            param[1] = "@COMMON_PART_CD:" + commonCode.common_part_cd;
            param[2] = "@COMMON_NM:" + commonCode.common_nm;
            param[3] = "@COMMON_PART_NM:" + commonCode.common_part_nm;
            param[4] = "@COMMON_SYS_CK:" + commonCode.common_sys_ck;
            param[5] = "@COMMON_MODULE:" + commonCode.common_module;
            param[6] = "@COMMON_ETC:" + commonCode.common_etc;
            param[7] = "@COMMON_REMARK:" + commonCode.common_remark;
            param[8] = "@USE_YN:" + commonCode.use_yn;
            param[9] = "@INSERT_EMP:" + HttpContext.Current.Session["loginCD"].ToString();
            param[10] = "@dis_seq:" + commonCode.dis_seq;
            param[11] = "@interface_cd:" + commonCode.interface_cd;
            param[12] = "@group_nm:" + commonCode.group_nm;

            return param;
        }
    }
}
