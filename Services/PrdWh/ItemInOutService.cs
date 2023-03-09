using HACCP.Libs.Database;
using HACCP.Models.PrdWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdWh
{
    public class ItemInOutService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ItemInOut";

        internal DataTable Select_S1(ItemInOut model)
        {
            string sGubun = "S";
            string[] param = new string[2];

            //param[0] = "@s_item:" + model.item_cd_S;      // 품목
            param[0] = "@s_item:" + model.item_nm_S;      // 품목
            if (model.use_ck == "Y")       // 사용여부
                param[1] = "@use_ck:Y";
            else
                param[1] = "@use_ck:%";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

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

        internal DataTable Select_S2(ItemInOut model)
        {
            string sGubun = "S2";
            string[] param = new string[2];

            param[0] = "@item_cd:" + model.item_cd;  //제품코드
            param[1] = "@s_lot_no:" + model.lot_no;  //제조번호

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Select_S3(ItemInOut model)
        {
            string sGubun = "S3";
            string[] param = new string[2];

            param[0] = "@lot_no:" + model.lot_no;  //제조번호
            param[1] = "@item_cd:" + model.item_cd;  //품목코드

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }
    }
}