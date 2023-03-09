using HACCP.Libs.Database;
using HACCP.Models.PrdLoc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdLoc
{
    public class CellStackStatus_IService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_CellStackStatus";

        internal DataTable SelectWorkroom(CellStackStatus_I model)
        {
            string sGubun = "W_PK";

            string[] param = new string[1];
            param[0] = "@workroom_cd:" + "";  // 구역코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Zone_Select(CellStackStatus_I model)
        {
            string sGubun = "Z";
            string[] param = new string[1];

            param[0] = "@workroom_cd:" + model.workroom_cd;  //창고코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Cell_Select(CellStackStatus_I model)
        {
            string sGubun = "C";
            string[] param = new string[1];

            param[0] = "@zone_cd:" + model.zone_cd;  // 구역코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable MAX_CELL_HEIGHT(CellStackStatus_I model)
        {
            string sGubun = "CH";
            string[] param = new string[2];

            param[0] = "@zone_cd:" + model.zone_cd;  // 구역코드
            param[1] = "@cell_isle:" + model.cell_isle;  // 랙 명

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Select(CellStackStatus_I model)
        {
            string sGubun = "SI";
            string[] param = new string[2];

            param[0] = "@zone_cd:" + model.zone_cd;
            param[1] = "@cell_isle:" + model.cell_isle;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Popup_Select(CellStackStatus_I model)
        {
            string sGubun = "SI2";
            string[] param = new string[4];

            param[0] = "@zone_cd:" + model.zone_cd;  // 구역코드
            param[1] = "@cell_isle:" + model.cell_isle;  // 랙 명
            param[2] = "@cell_column:" + model.cell_column;  // cell_column
            param[3] = "@cell_height:" + model.cell_height;  // cell_height

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
    }
}