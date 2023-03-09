using HACCP.Libs.Database;
using HACCP.Models.SysOp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysOp
{
    public class Group_manageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 조회
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataTable Select(string group_text, string use_yn_S)
        {
            string sSpName = "SP_Group_manage";
            string sGubun = "S";
            string[] pParam = new string[2];
            pParam[0] = "@group_text:" + group_text;
            pParam[1] = "@use_yn_S:" + use_yn_S;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            
            return dt;
        }


        public string Save(Group_manage group_manage)
        {
            string sSpName = "SP_Group_manage";
            string sGubun = group_manage.Gubun;
            string[] pParam = GetParam(group_manage);
            string sRtn = "";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            sRtn = "OK";

            return sRtn;
        }

        internal bool Delete(string pGroup_manage_Code)
        {
            string sSpName = "SP_Group_manage";
            string sGubun = "D";
            string[] pParam = new string[1];
            pParam[0] = "@emp_group_cd:" + pGroup_manage_Code;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (message != null ? true : false);
        }

        private string[] GetParam(Group_manage group_manage)
        {
            string[] param = new string[4];

            //기본정보
            param[0] = "@emp_group_nm:" + group_manage.emp_group_nm;
            param[1] = "@remark:" + group_manage.remark;
            param[2] = "@use_yn:" + group_manage.use_yn;            
            param[3] = "@emp_group_cd:" + group_manage.emp_group_cd;

            return param;
        }

        internal string groupManageCRUD(Group_manage gModel, string gubun)
        {
            string sSpName = "SP_Group_manage";
            string sGubun = gubun;

            string message = "";

            if (gModel.Equals("D"))
            {
                string[] pParam = new string[1];
                pParam[0] = "@emp_group_cd:" + gModel.emp_group_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else
            {
                string[] pParam = GetParam(gModel);

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }


            return message;
        }
    }
}
