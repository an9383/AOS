using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.ProductionManage
{
    public class WorkorderService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectWorkOrder(string sdate, string edate, string item_cd, string order_type, string lot_no_export)
        {
            string sSpName = "SP_Workorder_Workorder";
            string[] pParam = new string[8];
            pParam[0] = "@sdate:" + sdate;
            pParam[1] = "@edate:" + edate;
            pParam[2] = "@item_cd:" + item_cd;
            pParam[3] = "@order_type:" + order_type;
            pParam[4] = "@S_makeing_dept_cd:" + "%";
            pParam[5] = "@S_makeing_dept_cd2:" + "%";
            pParam[6] = "@order_status_s:" + "%";
            pParam[7] = "@lot_no_export_s:" + lot_no_export;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectItemPopupData(string item_cd, string item_nm)
        {
            string sSpName = "CODEHELP";
            string strsql = "SELECT item_cd, item_nm FROM v_item_makingstandard WHERE ("
                + "item_cd  LIKE '%" + item_cd + "%' OR "
                + "item_nm LIKE '%" + item_nm + "%') AND "
                + "item_cd  LIKE '%%' AND item_nm  LIKE '%%' ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, strsql);

            return dt;
        }
    }
}
