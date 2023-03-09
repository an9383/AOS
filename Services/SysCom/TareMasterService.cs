using HACCP.Libs.Database;
using HACCP.Models.SysCom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysCom
{
    public class TareMasterService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectTareMaster(string taremaster_type, string taremaster_material, string taremaster_class)
        {
            string sSpName = "SP_TareMaster";

            string gubun = "S";

            string[] param = new string[3];

            param[0] = "@taremaster_type:" + taremaster_type;
            param[1] = "@taremaster_material:" + taremaster_material;
            param[2] = "@taremaster_class:" + taremaster_class;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string TareMasterTRX(Tare tare)
        {
            string sSpName = "SP_TareMaster";
            string res = "";

            if (tare.gubun.Equals("I"))
            {
                string[] param = new string[5];

                param[0] = "@taremaster_type:" + tare.taremaster_type;
                param[1] = "@taremaster_material:" + tare.taremaster_material;
                param[2] = "@taremaster_class:" + tare.taremaster_class;
                param[3] = "@taremaster_volume:" + tare.taremaster_volume;
                param[4] = "@taremaster_dimension:" + tare.taremaster_dimension;

                res = _bllSpExecute.SpExecuteString(sSpName, "I", param);
            }
            else if (tare.gubun.Equals("U"))
            {
                string[] param = new string[5];

                param[0] = "@taremaster_type:" + tare.taremaster_type;
                param[1] = "@taremaster_material:" + tare.taremaster_material;
                param[2] = "@taremaster_class:" + tare.taremaster_class;
                param[3] = "@taremaster_volume:" + tare.taremaster_volume;
                param[4] = "@taremaster_dimension:" + tare.taremaster_dimension;

                res = _bllSpExecute.SpExecuteString(sSpName, "UM", param);
            }
            else if (tare.gubun.Equals("D"))
            {
                string[] param = new string[3];

                param[0] = "@taremaster_type:" + tare.taremaster_type;
                param[1] = "@taremaster_material:" + tare.taremaster_material;
                param[2] = "@taremaster_class:" + tare.taremaster_class;

                res = _bllSpExecute.SpExecuteString(sSpName, "DM", param);
            }

            return res;
        }
    }
}
