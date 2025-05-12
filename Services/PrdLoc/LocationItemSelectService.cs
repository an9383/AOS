using HACCP.Libs.Database;
using HACCP.Models.PrdLoc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdLoc
{
    public class LocationItemSelectService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_LocationItemSelect";

        internal DataTable Select(LocationItemSelect model)
        {
            string sGubun = "SETLE";
            string[] param = new string[1];

            param[0] = "@s_workroom_cd:" + "";  // 창고코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable SelectWorkroom(LocationItemSelect model)
        {
            string sGubun = "W";
            string[] param = new string[1];
            param[0] = "@s_workroom_cd:" + "";  // 구역코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Zone_Select(LocationItemSelect model)
        {
            string sGubun = "Z";
            string[] param = new string[1];

            param[0] = "@s_workroom_cd:" + model.workroom_cd;  //창고코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Cell_Select(LocationItemSelect model)
        {
            string sGubun = "C";
            string[] param = new string[1];

            param[0] = "@s_zone_cd:" + model.zone_cd;  // 구역코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable SelectSearch(LocationItemSelect model)
        {
            string sGubun = "Select";
            string[] param = new string[4];

            param[0] = "@s_workroom_cd:" + (model.workroom_cd != null ? model.workroom_cd  : "");  // 창고코드
            param[1] = "@s_zone_cd:" + (model.zone_cd != null ? model.zone_cd : "");  // 구역코드
            param[2] = "@s_cell_cd:" + (model.cell_isle != null ? model.cell_isle : "");  // 셀코드
            param[3] = "@s_item_cd:" + "";  // 품목코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
    }
}