using HACCP.Libs.Database;
using HACCP.Models.SysSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysSet
{
    public class MenuProgramManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select(string module_gb)
        {
            string sSpName = "SP_MenuProgramManage";
            string sGubun = "S1";
            string[] pParam = new string[1];
            pParam[0] = "@module_gb:" + module_gb;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable SelectProgramGridData()
        {
            string sSpName = "SP_MenuProgramManage";
            string sGubun = "S2";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string MenuProgramCRUD(MenuProgramManage menuProgram)
        {
            string sSpName = "SP_MenuProgramManage";
            string sGubun = menuProgram.gubun;
            string res = "";

            if (menuProgram.gubun.Equals("U2") || menuProgram.gubun.Equals("U3"))
            {
                string[] pParam = new string[4];
                pParam[0] = "@module_gb:" + menuProgram.module_gb;
                pParam[1] = "@module_cd:" + menuProgram.module_cd;
                pParam[2] = "@form_cd:" + menuProgram.form_cd;
                pParam[3] = "@form_seq:" + menuProgram.form_seq;

                res = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            } else if (menuProgram.gubun.Equals("I"))
            {
                string[] pParam = new string[5];
                pParam[0] = "@module_gb:" + menuProgram.module_gb;
                pParam[1] = "@module_cd:" + menuProgram.module_cd;
                pParam[2] = "@module_seq:" + menuProgram.module_seq;
                pParam[3] = "@form_cd:" + menuProgram.form_cd;
                pParam[4] = "@form_seq:" + menuProgram.form_seq;

                res = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            } else if (menuProgram.gubun.Equals("D"))
            {
                string[] pParam = new string[3];
                pParam[0] = "@module_gb:" + menuProgram.module_gb;
                pParam[1] = "@module_cd:" + menuProgram.module_cd;
                pParam[2] = "@form_cd:" + menuProgram.form_cd;

                res = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return res;
        }
    }
}
