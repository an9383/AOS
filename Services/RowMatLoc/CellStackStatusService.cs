using HACCP.Libs.Database;
using HACCP.Models.RowMatLoc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatLoc
{
    public class CellStackStatusService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_CellStackStatus";

        internal DataTable Select(CellStackStatus model)
        {
            string sGubun = "S1";
            string[] param = new string[2];

            param[0] = "@zone_cd:" + model.zone_cd;
            param[1] = "@cell_isle:" + model.cell_isle;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        /// <summary>
        //  창고 드랍박스
        /// </summary>
        internal DataTable SelectWorkroom(CellStackStatus model)
        {
            string sGubun = "W_MP";

            string[] param = new string[1];
            param[0] = "@workroom_cd:" + "";  // 구역코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Zone_Select(string workroom_cd)
        {
            string sGubun = "Z";
            string[] param = new string[1];

            param[0] = "@workroom_cd:" + workroom_cd;  //창고코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Cell_Select(string zone_cd)
        {
            string sGubun = "C";
            string[] param = new string[1];

            param[0] = "@zone_cd:" + zone_cd;  // 구역코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable MAX_CELL_HEIGHT(CellStackStatus model)
        {
            string sGubun = "CH";
            string[] param = new string[2];

            param[0] = "@zone_cd:" + model.zone_cd;  // 구역코드
            param[1] = "@cell_isle:" + model.cell_isle;  // 랙 명

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Popup_Select(CellStackStatus model)
        {
            string sGubun = "S2";
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
