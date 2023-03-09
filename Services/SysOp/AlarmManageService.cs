using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using HACCP.Libs.Database;
using HACCP.Models.SysOp;

namespace HACCP.Services.SysOp
{
    public class AlarmManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable AlarmManage_Select(string alm_code)
        {
            string sSpName = "[RTEGMS_MONITORING].[dbo].SP_AlarmManage";
            string sGubun = "Select";
            string[] pParam = new string[1];
            pParam[0] = "@alarm_code:" + alm_code;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable AlarmManage_Select_Detail(string alm_code)
        {
            string sSpName = "[RTEGMS_MONITORING].[dbo].SP_AlarmManage";
            string sGubun = "Select_Detail";
            string[] pParam = new string[1];

            pParam[0] = "@alarm_code:" + alm_code;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }
        
        internal string AlarmManageCRUD(AlarmManage imodel, string gubun)
        {
            string sSpName = "[RTEGMS_MONITORING].[dbo].SP_AlarmManage";
            string sGubun = gubun;

            string message = "";

            if (imodel.Equals("Delete"))
            {
                string[] pParam = new string[2];
                pParam[0] = "@alarm_code" + imodel.alarm_cd;
                pParam[1] = "@alarm_name:" + imodel.alarm_nm;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else
            {
                string[] pParam = new string[5];

                pParam[0] = "@alarm_code:" + imodel.alarm_cd;
                pParam[1] = "@alarm_name:" + imodel.alarm_nm;
                pParam[2] = "@alarm_body:" + imodel.alarm_body;
                pParam[3] = "@alarm_kakao:" + imodel.alarm_kakao;
                pParam[4] = "@kakao_format_code:" + imodel.kakao_format_code;
                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            //정상 처리 되었습니다.
            return message;
        }


        internal DataTable AlarmManage_Select_EMP(string alm_code)
        {
            string sSpName = "[RTEGMS_MONITORING].[dbo].SP_AlarmManage";
            string sGubun = "Select_EMP";
            string[] pParam = new string[1];
            pParam[0] = "@alarm_code:" + alm_code;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string AlarmManage_EMP_CRUD(List<AlarmManage> iModel)
        {
            //ALARM_DTL에 저장.

            string sSpName = "[RTEGMS_MONITORING].[dbo].SP_AlarmManage";
            string sGubun = "";
            string message = "";

            for (int i = 0; i < iModel.Count; i++)
            {

                string[] pParam = new string[8];

                pParam[0] = "@alarm_code:" + iModel[i].alarm_cd;
                pParam[1] = "@emp_code:" + iModel[i].emp_cd;
                pParam[2] = "@KAKAO_yn:" + iModel[i].ALD_KAKAO;
                pParam[3] = "@SMS_yn:" + iModel[i].ALD_SMS;
                pParam[4] = "@VIEW_yn:" + iModel[i].ALD_VIEW;
                pParam[5] = "@EMAIL_yn:" + iModel[i].ALD_EMAIL;
                pParam[6] = "@email_addr:" + iModel[i].email_addr;
                pParam[7] = "@phone_no:" + iModel[i].phone_no;

                if (iModel[i].gubun.Equals("Insert_EMP"))
                {
                    sGubun = "Insert_EMP";

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (iModel[i].gubun.Equals("Update_EMP"))
                {
                    sGubun = "Update_EMP";

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
                else if (iModel[i].gubun.Equals("Delete_EMP"))
                {
                    sGubun = "Delete_EMP";

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
                }
            }
            return message;
        }

        internal DataTable getEmpData()
        {
            string strsql = "SELECT emp_cd, emp_nm, dept_cd, dept_nm";
            strsql += " FROM V_EMPLOYEE";
            strsql += " WHERE (emp_cd  LIKE '%%' OR emp_nm  LIKE '%%')";
            strsql += " AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }
    }
}