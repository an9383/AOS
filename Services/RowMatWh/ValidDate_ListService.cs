using HACCP.Libs.Database;
using HACCP.Models.RowMatWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.RowMatWh
{
    public class ValidDate_ListService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable ValidDate_List_S(ValidDate_List vModel)
        {
            string sSpName = "SP_ValidDate_List";
            string sGubun = "S";
            string[] pParam = new string[4];

            pParam[0] = "@Valid_date_S:" + vModel.Valid_date_S;
            pParam[1] = "@Valid_date:" + vModel.Valid_date;
            pParam[2] = "@s_gubun:" + "%";
            pParam[3] = "@item_cd_s:" + "";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable ValidDate_List_SV(ValidDate_List vModel)
        {
            string sSpName = "SP_ValidDate_List";
            string sGubun = "SV";
            string[] pParam = new string[1];

            pParam[0] = "@item_cd_s:" + "";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable ValidDate_List_V_OUT(string receipt_no, string receipt_id)
        {
            string sSpName = "SP_ValidDate_List";
            string sGubun = "V_OUT";
            string[] pParam = new string[2];

            pParam[0] = "@receipt_no:" + receipt_no;
            pParam[1] = "@receipt_id:" + receipt_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable ValidDate_List_CK(string receipt_no, string receipt_id)
        {
            string sSpName = "SP_ValidDate_List";
            string sGubun = "CK";
            string[] pParam = new string[2];

            pParam[0] = "@receipt_no:" + receipt_no;
            pParam[1] = "@receipt_id:" + receipt_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable ValidDate_List_ES(string receipt_no, string receipt_id, string emp_cd, string validation_type, string remain_qty, string item_cd)
        {
            string sSpName = "SP_ValidDate_List";
            string sGubun = "ES";
            string[] pParam = new string[6];

            pParam[0] = "@receipt_no:" + receipt_no;
            pParam[1] = "@receipt_id:" + receipt_id;
            pParam[2] = "@emp_cd:" + emp_cd;
            pParam[3] = "@validation_type:" + validation_type;
            pParam[4] = "@remain_qty:" + remain_qty;
            pParam[5] = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

    }
}