using HACCP.Libs.Database;
using HACCP.Models.SysItem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.SysItem
{
    public class Etc_Item2Service
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_Etc_Item2";

        internal DataTable Etc_Item2Select(EtcItem.SrchParam param)
        {
            string[] pParam = new string[4];
            pParam[0] = "@s_item_cd:" + param.item_cd;
            pParam[1] = "@s_item_type1:" + param.item_type1;
            pParam[2] = "@s_item_type2:" + param.item_type2;
            pParam[3] = "@s_item_gb:" + param.item_gb;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string Etc_Item2TRX(EtcItem etcItem)
        {
            string[] pParam = new string[13];
            pParam[0] = "@item_gb:" + etcItem.item_gb_cd;
            pParam[1] = "@item_cd:" + etcItem.item_cd;
            pParam[2] = "@item_nm:" + etcItem.item_nm;
            pParam[3] = "@prod_end:" + etcItem.prod_end;
            pParam[4] = "@break_type:" + etcItem.break_type;
            pParam[5] = "@abc_gubun:" + etcItem.abc_gubun;
            pParam[6] = "@plant_cd:" + etcItem.plant_cd;
            pParam[7] = "@item_type1:" + etcItem.item_type1;
            pParam[8] = "@item_type2:" + etcItem.item_type2;
            pParam[9] = "@item_unit:" + etcItem.item_unit;
            pParam[10] = "@keeping_warehouse:" + etcItem.keeping_warehouse;
            pParam[11] = "@item_remark:" + etcItem.remark;
            pParam[12] = "@at_emp_cd:" + HttpContext.Current.Session["LoginCD"];

            string res = _bllSpExecute.SpExecuteString(sSpName, etcItem.gubun, pParam);

            return res;
        }
    }
}