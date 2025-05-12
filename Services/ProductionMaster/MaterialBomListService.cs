using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.ProductionMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.ProductionMaster
{
    public class MaterialBomListService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable GetGubuns(string pSpName, string pGubun, string pItemCd)
        {
            string sSpName = pSpName;
            string[] pParam = new string[1];
            pParam[0] = "@s_item:" + pItemCd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal List<Item> SelectItem(string item_cd, string item_nm)
        {
            string sSpName = "CODEHELP";
            string strsql = "SELECT item_cd, item_nm, item_gb_nm FROM v_item_material WHERE ("
                + "item_cd  LIKE '%" + item_cd + "%' OR "
                + "item_nm LIKE '%" + item_nm + "%') AND "
                + "item_cd  LIKE '%%' AND item_nm  LIKE '%%' ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, strsql);

            List<Item> items = new List<Item>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                Item item = new Item(row, "gb");

                items.Add(item);
            }

            return items;
        }

        internal DataTable SelectBomInfo(string pSpName, string pGubun, string pItemCd, string pItemGubun)
        {
            string sSpName = pSpName;
            string[] pParam = new string[2];
            pParam[0] = "@s_item:" + pItemCd;
            pParam[1] = "@s_item_gb:" + pItemGubun;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectBomDetail(string pSpName, string pGubun, string pItemCd)
        {
            string sSpName = pSpName;
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + pItemCd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }
    }
}
