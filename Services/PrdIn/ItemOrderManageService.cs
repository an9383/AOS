using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.PrdIn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrdIn
{
    public class ItemOrderManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ItemOrderManage";

        internal DataTable ItemOrderManage_Select(ItemOrderManage model)
        {
            string sGubun = "S1";
            string[] param = new string[5];

            param[0] = "@start_date:" + model.start_date_S;
            param[1] = "@end_date:" + model.end_date_S;
            //param[2] = "@item_cd:" + model.item_cd_S;
            param[2] = "@item_cd:" + model.item_nm_S;
            param[3] = "@order_status_S:" + model.order_state_S;
            param[4] = "@search_date:" + model.search_date_S;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ItemOrderManage_SelectDetail(ItemOrderManage model)
        {
            string sGubun = "S2";
            string[] param = new string[1];

            param[0] = "@order_no:" + model.order_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal string ItemOrderManage_CRUD(ItemOrderManage model, string gubun)
        {
            string sGubun = "";
            string message = "";

            if (gubun == "I" || gubun == "U")
            {

                // "추가" I1 , 수정 "U1"
                sGubun = (gubun == "I") ? "I1" : "U1";

                string[] param = new string[8];

                param[0] = "@order_no:" + model.order_no;
                param[1] = "@order_date:" + model.order_date;
                param[2] = "@order_gb:" + model.order_gb;
                param[3] = "@sales_emp_cd:" + model.sales_emp_cd;
                param[4] = "@cust_cd:" + model.cust_cd;
                param[5] = "@pass_cust_cd:" + model.pass_cust_cd;
                param[6] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
                param[7] = "@remark:" + model.remark;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);

            }
            else if (gubun == "I_D")
            {
                sGubun = "I2";
                string[] param = new string[9];

                param[0] = "@order_no:" + model.order_no;
                param[1] = "@order_seq:" + model.order_seq;
                param[2] = "@order_item_cd:" + model.order_item_cd;
                param[3] = "@order_qty:" + model.order_qty;
                if(model.order_require_date != null)
                param[4] = "@order_require_date:" + DateTime.Parse(model.order_require_date).ToShortDateString();
                else
                    param[4] = "@order_require_date:";
                param[5] = "@order_price:" + model.order_price;
                param[6] = "@order_status:" + model.order_status;
                param[7] = "@order_priority:" + model.order_priority;
                param[8] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);

            }
            else if (gubun == "U_D")
            {
                sGubun = "U2";
                string[] param = new string[9];

                param[0] = "@order_no:" + model.order_no;
                param[1] = "@order_seq:" + model.order_seq;
                param[2] = "@order_item_cd:" + model.order_item_cd;
                param[3] = "@order_qty:" + model.order_qty;
                param[4] = "@order_require_date:" + DateTime.Parse(model.order_require_date).ToShortDateString();
                param[5] = "@order_price:" + model.order_price;
                param[6] = "@order_status:" + model.order_status;
                param[7] = "@order_priority:" + model.order_priority;
                param[8] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);

            }
            else if (gubun == "D")
            {
                sGubun = "D1";

                string[] param = new string[1];

                param[0] = "@order_no:" + model.order_no;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }
            else if (gubun == "D2")
            {
                sGubun = "D2";

                string[] param = new string[2];

                param[0] = "@order_no:" + model.order_no;
                param[1] = "@order_seq:" + model.order_seq;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }

            return message;
        }

        internal DataTable Searchitem_Popup(string pSpName)
        {
            string strsql = "SELECT item_cd, item_nm";
            strsql += " FROM v_packingitem";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable Vender_Popup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "vender_cd, vender_nm, license, ";
            strsql += "owner_nm, phone, zipcode, address, bigo ";
            strsql += "FROM  v_vender_pack ";
            strsql += "WHERE (vender_cd  LIKE '%%' OR vender_nm  LIKE '%%') ";
            strsql += "AND vender_cd  LIKE '%%' AND vender_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable Emp_Popup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "emp_cd, emp_nm, ";
            strsql += "phone_no, dept_nm ";
            strsql += "FROM V_EMPLOYEE WHERE(emp_cd LIKE '%%' OR emp_nm  LIKE '%%') ";
            strsql += "AND emp_cd LIKE '%%' AND emp_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal List<string> S_PRICE(ItemOrderManage model)
        {
            string sGubun = "S_PRICE";
            string[] param = new string[1];

            param[0] = "@item_cd:" + model.order_item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, param);

            if (ds == null)
            {
                return null;
            }

            List<string> jsonList = new List<string>();

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                string jsonStr = Public_Function.DataTableToJSON(ds.Tables[i]);

                if (jsonStr.Length > 0)
                {
                    jsonList.Add(Public_Function.DataTableToJSON(ds.Tables[i]));
                }
                else if (jsonStr.Length <= 0)
                {
                    jsonList.Add("{ \"data\" : \"empty\" }");
                }
            }

            return jsonList;

        }
    }
}