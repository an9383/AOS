using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Libs.File;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Mont
{
    public class EquipmentOperationStatusService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();


        internal DataTable Select(string operate_type, string equipt_category)
        {
            string sSpName = "SP_GET_EQUIPMENT_DATA";

            string gubun = "S2";
            string[] param = new string[2];

            param[0] = "@s_equip_use_gb:" + operate_type;
            param[1] = "@equipt_category:" + equipt_category;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);

            return dt;
        }


        internal DataTable OperationGridSearch(string equip_cd, string equip_type)
        {
            string sSpName = "SP_GET_EQUIPMENT_DATA";

            string gubun = "S3";
            string[] param = new string[2];

            param[0] = "@EQUIP_CD:" + equip_cd;
            param[1] = "@EQUIP_TYPE_NM:" + equip_type;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);

            return dt;
        }


        internal DataTable WorkroomEnvironmentChartSelect(string workroom_cd)
        {
            string sSpName = "SP_GET_EQUIPMENT_DATA";

            string gubun = "WECS";
            string[] param = new string[1];

            param[0] = "@workroom_cd:" + workroom_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);

            return dt;
        }


    }
}