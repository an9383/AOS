using HACCP.Libs.Database;
using HACCP.Models.ProductionManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class EquipPlanService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string _SpName = "SP_EquipPlan";

        internal DataTable SelectEquipPlan(string sdate, string edate, string item_cd)
        {
            string[] pParam = new string[3];
            pParam[0] = "@sdate:" + sdate;
            pParam[1] = "@edate:" + edate;
            pParam[2] = "@item_cd:" + item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(_SpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }


        // 제품 팝업
        internal DataTable getItemPopupData()
        {
            string strsql = "SELECT a.item_cd, a.item_nm, '제조제품' AS 'item_gb_nm'";
            strsql += " FROM item_standard a";
            strsql += " WHERE a.item_gb = 6";
            strsql += " AND a.prod_end = 'Y'";
            strsql += " ORDER BY item_nm, item_gb";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }


        // 지시 팝업
        internal DataTable getOrderPopupData()
        {
            string[] pParam = new string[0];

            DataTable dt = _bllSpExecute.SpExecuteTable(_SpName, "OPS", pParam); /*OrderPopupSelect*/

            return dt;
        }


        // 설비 팝업
        internal DataTable getEquipPopupData(WorkorderLedger orderLedger)
        {
            string[] pParam = new string[3];
            pParam[0] = "@order_no:" + orderLedger.order_no;
            pParam[1] = "@lot_no:" + orderLedger.lot_no;
            pParam[2] = "@revision_no:" + orderLedger.revision_no;


            DataTable dt = _bllSpExecute.SpExecuteTable(_SpName, "EPS", pParam); /*EquipPopupSelect*/

            return dt;
        }


        // 설비 팝업
        internal String EquipPlanGrid_CRUD(EquipPlan equipPlan)
        {
            string gubun = equipPlan.gubun;
            string[] pParam = new string[11];
            pParam[0] = "@order_no:" + equipPlan.order_no;
            pParam[1] = "@equip_cd:" + equipPlan.equip_cd;
            pParam[2] = "@mp_ck:" + equipPlan.mp_ck;
            pParam[3] = "@s_date:" + equipPlan.s_date;
            pParam[4] = "@s_time:" + equipPlan.s_time;
            pParam[5] = "@e_date:" + equipPlan.e_date;
            pParam[6] = "@e_time:" + equipPlan.e_time;
            pParam[7] = "@required_min:" + equipPlan.required_min;
            pParam[8] = "@order_proc_id:" + equipPlan.order_proc_id;
            pParam[9] = "@audittrail_id:" + equipPlan.audittrail_id;
            pParam[10] = "@ccp_cd:" + equipPlan.ccp_cd;

            String msg = _bllSpExecute.SpExecuteString(_SpName, gubun, pParam);


            return msg;
        }
    }
}
