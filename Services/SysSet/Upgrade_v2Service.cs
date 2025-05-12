using HACCP.Libs.Database;
using HACCP.Models.SysSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysSet
{
    public class Upgrade_v2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 조회
        /// 
        /// </summary>
        /// <returns></returns>
        internal List<Upgrade_v2> Select()
        {
            string sSpName = "SP_Upgrade_v2";
            string sGubun = "S";
            string[] pParam = new string[1];
            pParam[0] = "@file_use_yn:" + "Y";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            List<Upgrade_v2> Upgrade_v2s = new List<Upgrade_v2>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                Upgrade_v2 upgrade_v2 = new Upgrade_v2(row);
                Upgrade_v2s.Add(upgrade_v2);
            }

            return Upgrade_v2s;
        }
    }
}
