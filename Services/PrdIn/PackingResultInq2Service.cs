using HACCP.Libs.Database;
using HACCP.Models.PrdIn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdIn
{
    public class PackingResultInq2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_PackingResultInq2";

        internal DataTable Select(PackingResultInq2 model)
        {
            string sGubun = "Select";
            string[] param = new string[5];

            param[0] = "@packing_order_work_date_start_s:" + model.start_date_S;
            param[1] = "@packing_order_work_date_end_s:" + model.end_date_S;
            //param[2] = "@item_cd_s:" + model.item_cd_S;
            param[2] = "@item_cd_s:" + model.item_nm_S;
            param[3] = "@lot_no_s:" + model.lot_no_S;
            param[4] = "@issue_status:" + model.s_issue_status;

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
    }
}