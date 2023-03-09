using DevExpress.CodeParser;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.LocMng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Services.LocMng
{
    public class ZoneManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 조회
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataTable Select(string workroom_cd)
        {
            string sSpName = "SP_MA_ZoneManage";
            string sGubun = "Select";
            string[] pParam = new string[1];
            pParam[0] = "@workroom_cd:" + workroom_cd;            

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        //창고에 필요한 창고코드 조회
        internal DataTable getWorkRoomPopupData(string workroom_cd)
        {
            string sSpName = "SP_MA_ZoneManage";
            string sGubun = "SelectTable";
            string[] pParam = new string[1];
            pParam[0] = "@workroom_cd:" + workroom_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string ZoneManageCRUD(ZoneManage zModel, string gubun)
        {
            string sSpName = "SP_MA_ZoneManage";
            string sGubun = gubun;

            string message = "";

            if (zModel.Equals("Delete"))
            {
                string[] pParam = new string[2];
                pParam[0] = "@workroom_cd" + zModel.workroom_cd;
                pParam[1] = "@zone_cd:" + zModel.zone_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else
            {
                string[] pParam = GetParam(zModel);

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return message;

        }


        private string[] GetParam(ZoneManage zoneManage)
        {
            string[] param = new string[13];

            //기본정보        
            param[0] = "@zone_cd:" + zoneManage.zone_cd;
            param[1] = "@workroom_cd:" + zoneManage.workroom_cd;                        
            param[2] = "@zone_nm:" + zoneManage.zone_nm;
            param[3] = "@zone_type:" + zoneManage.zone_type;
            param[4] = "@zone_priority:" + zoneManage.zone_priority;
            param[5] = "@zone_status:" + zoneManage.zone_status;
            param[6] = "@zone_temperature_max:" + zoneManage.zone_temperature_max;
            param[7] = "@zone_temperature_min:" + zoneManage.zone_temperature_min;
            param[8] = "@zone_humidity_max:" + zoneManage.zone_humidity_max;
            param[9] = "@zone_humidity_min:" + zoneManage.zone_humidity_min;
            param[10] = "@zone_remark:" + zoneManage.zone_remark;
            param[11] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
            param[12] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];

            return param;
        }
    }
}
