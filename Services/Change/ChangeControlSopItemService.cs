using HACCP.Libs.Database;
using HACCP.Models.Change;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Change
{
    public class ChangeControlSopItemService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable ChangeControlSopItemSelect(string changecontrol_sop_item_cd)
        {
            string sSpName = "SP_ChangeControlSopItem";

            string[] pParam = new string[1];
            pParam[0] = "@s_changecontrol_sop_item_cd:" + changecontrol_sop_item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "Select", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable ChangeControlSopItemSelectDetail(string changecontrol_sop_item_cd)
        {
            string sSpName = "SP_ChangeControlSopItem";

            string[] pParam = new string[1];
            pParam[0] = "@changecontrol_sop_item_cd:" + changecontrol_sop_item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SelectSop", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string ChangeControlSopItemTRX(List<ChangeControlSopItem> paramData)
        {
            string sSpName = "SP_ChangeControlSopItem";
            string res = "";

            foreach (ChangeControlSopItem tmp in paramData)
            {
                if (tmp.gubun.Equals("I"))
                {
                    string[] pParam = new string[4];
                    pParam[0] = "@changecontrol_sop_item_cd:" + tmp.changecontrol_sop_item_cd;
                    pParam[1] = "@changecontrol_sop_item_id:" + "";
                    pParam[2] = "@changecontrol_sop_item_seq:" + tmp.changecontrol_sop_item_seq;
                    pParam[3] = "@changecontrol_sop_item_contents:" + tmp.changecontrol_sop_item_contents;

                    res = _bllSpExecute.SpExecuteString(sSpName, "Insert", pParam);
                }
                else if (tmp.gubun.Equals("U"))
                {
                    string[] pParam = new string[4];
                    pParam[0] = "@changecontrol_sop_item_cd:" + tmp.changecontrol_sop_item_cd;
                    pParam[1] = "@changecontrol_sop_item_id:" + tmp.changecontrol_sop_item_id;
                    pParam[2] = "@changecontrol_sop_item_seq:" + tmp.changecontrol_sop_item_seq;
                    pParam[3] = "@changecontrol_sop_item_contents:" + tmp.changecontrol_sop_item_contents;

                    res = _bllSpExecute.SpExecuteString(sSpName, "Update", pParam);
                }
                else if (tmp.gubun.Equals("D"))
                {
                    string[] pParam = new string[2];
                    pParam[0] = "@changecontrol_sop_item_cd:" + tmp.changecontrol_sop_item_cd;
                    pParam[1] = "@changecontrol_sop_item_id:" + tmp.changecontrol_sop_item_id;

                    res = _bllSpExecute.SpExecuteString(sSpName, "Delete", pParam);
                }
            }

            return res;
        }
    }
}