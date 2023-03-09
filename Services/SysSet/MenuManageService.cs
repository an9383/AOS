using HACCP.Libs.Database;
using HACCP.Models.SysSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysSet
{

    public class MenuManageService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal List<MenuManage> Select(string MenuManage_pModuleGubuns = null)
        {
            string spName = "SP_MenuManage";
            string spGubun = "S";
            string[] param = new string[1]; ;
            if (MenuManage_pModuleGubuns == null)
            {
                param[0] = "@module_gb:1";
            }
            else
            {
                param[0] = "@module_gb:" + MenuManage_pModuleGubuns;
            }

            DataTable dt = _bllSpExecute.SpExecuteDataSet(spName, spGubun, param).Tables[0];
            List<MenuManage> modelList = new List<MenuManage>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                MenuManage model = new MenuManage(row);
                modelList.Add(model);
            }

            return modelList;
        }

        /// <summary>
        /// 원료 코드 중복체크 메소드
        /// </summary>
        /// <param name="pItemCd"></param>
        /// <returns></returns>
        internal string CdDuplicateCheck(string pItemCd)
        {
            string sSpName = "SP_MaterialMaster2";
            string sGubun = "SC";
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + pItemCd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        /// <summary>
        /// 원료 DB에 삽입
        /// </summary>
        /// <param name="pMaterial"></param>
        /// <returns></returns>
        internal bool CRUD(MenuManage pModel)
        {
            string sSpName = "SP_MenuManage";
            //string sGubun = pModel.pGubun;
            string sGubun = pModel.MenuManage_pGubun;
            string[] pParam = GetParam(pModel);

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (message.Equals("1") ? true : false);
        }
        internal bool Delete(string module_gb, string module_cd)
        {
            string sSpName = "SP_MenuManage";
            string sGubun = "D";
            string[] pParam = new string[2];
            pParam[0] = "@module_gb:" + module_gb;
            pParam[1] = "@module_cd:" + module_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return (message != null ? true : false);
        }
        private string[] GetParam(MenuManage pModel)
        {
            string[] param = new string[6];

            //기본정보
            param[0] = "@module_gb:" + pModel.module_gb;
            param[1] = "@module_cd:" + pModel.module_cd;
            param[2] = "@module_nm:" + pModel.module_nm;
            param[3] = "@module_seq:" + pModel.module_seq;
            param[4] = "@module_level:" + pModel.module_level;
            param[5] = "@module_parent:" + pModel.module_parent;

            return param;
        }
    }
}
