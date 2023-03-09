using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.PrdLoc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdLoc
{
    public class ItemManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ItemManage";

        internal DataTable Select(ItemManage model)
        {
            string sGubun = "S";
            string[] param = new string[3];

            param[0] = "@s_item_cd:" + "";  //원자재코드
            param[1] = "@s_lot_no:" + "";    //제조번호
            param[2] = "@keeping_ck:" + "1";  // 보관품 포함

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable SelectWorkroom(ItemManage model)
        {
            string sGubun = "W";
            string[] param = new string[1];
            param[0] = "@workroom_cd:" + "";  // 구역코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ItemManage_Zone_Select(ItemManage model)
        {
            string sGubun = "Z";
            string[] param = new string[1];

            param[0] = "@workroom_cd:" + model.workroom_cd;  //창고코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ItemManage_Cell_Select(ItemManage model)
        {
            string sGubun = "C";
            string[] param = new string[1];

            param[0] = "@zone_cd:" + model.zone_cd;  // 구역코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ItemManage_Loc_Select(ItemManage model)
        {
            string sGubun = "S2";
            string[] param = new string[2];

            param[0] = "@item_gubun:" + model.item_gubun;       //일반(0)/보관(1) 구분
            param[1] = "@item_stock_id:" + model.item_stock_id; //제품입고일련번호          

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal string ItemManageCRUD(ItemManage model, string[] selectedKeys)
        {
            string sGubun = "U";
            string message = "";

            for (int i = 0; i < selectedKeys.Length; i++)
            {
                string[] param = new string[8];
                param[0] = "@workroom_cd:" + model.workroom_cd; //창고코드
                param[1] = "@zone_cd:" + model.zone_cd;         //구역코드
                param[2] = "@cell_cd:" + model.cell_cd;         //셀코드   
                param[3] = "@pallet_cd:" + model.pallet_cd;    //팔레트코드
                param[4] = "@box_barcode_no:" + selectedKeys[i]; //바코드코드
                param[5] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
                param[6] = "@log_user_id :" + HttpContext.Current.Session["loginCD"];
                param[7] = "@item_gubun:" + model.item_gubun;    //일반(0)/보관(1) 구분

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }

            return message;
        }
    }
}