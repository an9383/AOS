using HACCP.Libs.Database;
using HACCP.Models.PrdWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdWh
{
    public class ItemStockStatusService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ItemStockStatus";

        internal DataTable Select_Tab1(ItemStockStatus model)
        {
            string sGubun = "T1S";
            string[] param = new string[3];

            //param[0] = "@s_item:" + model.item_cd_S;      // 품목
            param[0] = "@s_item:" + model.item_nm_S;      // 품목
            param[1] = "@s_lot_no:" + "";  // 제조번호
            if (model.use_ck == "Y")       // 사용여부
                param[2] = "@use_ck:Y";
            else
                param[2] = "@use_ck:%";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Select_Tab2(ItemStockStatus model)
        {
            string sGubun = "T3S";
            string[] param = new string[3];

            //param[0] = "@s_item:" + model.item_cd_S;      // 품목
            param[0] = "@s_item:" + model.item_nm_S;      // 품목
            param[1] = "@s_lot_no:" + "";  // 제조번호
            if (model.use_ck == "Y")       // 사용여부
                param[2] = "@use_ck:Y";
            else
                param[2] = "@use_ck:%";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Select_Tab3(ItemStockStatus model)
        {
            string sGubun = "T2S";
            string[] param = new string[4];

            //param[0] = "@s_item:" + model.item_cd_S;      // 품목
            param[0] = "@s_item:" + model.item_nm_S;      // 품목
            param[1] = "@s_lot_no:" + "";  // 제조번호
            param[2] = "@item_stock_id:" + model.item_stock_id;  // ID
            if (model.use_ck == "Y")       // 사용여부
                param[3] = "@use_ck:Y";
            else
                param[3] = "@use_ck:%";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        //품목팝업
        internal DataTable s_item_Popup(string pSpName)
        {
            string strsql = "SELECT item_cd, item_nm";
            strsql += " FROM v_packingitem";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable Tab3_Grid2(ItemStockStatus model)
        {
            string sGubun = "T2S2";

            string[] param = new string[3];

            param[0] = "@item_stock_id:" + model.item_stock_id;  //제품입고일련번호
            param[1] = "@s_item:" + model.item_cd;    //제품코드
            param[2] = "@s_lot_no:" + model.lot_no; //제조번호

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Tab3_Grid3(ItemStockStatus model)
        {
            string sGubun = "T2S3";

            string[] param = new string[1];

            param[0] = "@box_barcode_no:" + model.box_barcode_no;  //팔레트바코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
    }
}