using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using HACCP.Models.AdvancedPlanning;
using HACCP.Libs;

namespace HACCP.Services.AdvancedPlanning
{
    public class ForcastingOrderManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable ForcastingOrderManageSearch(ForcastingOrderManage.SrchParam srch)
        {

            string[] param = new string[4];
            param[0] = "@de_Sdate:" + srch.start_date;
            param[1] = "@de_Edate:" + srch.end_date;
            param[2] = "@c_item_cd:" + srch.order_request_item_cd;
            param[3] = "@c_item_nm:" + srch.order_request_item_nm;

            DataTable dt = _bllSpExecute.SpExecuteTable("SP_ForcastingOrderManage", "S", param);
            
            return dt;

        }

        //생산의뢰번호 생성
        internal string NewForcastNo(string request_date)
        {
            string[] param = new string[1];
            param[0] = "@request_date:" + request_date;

            string res = _bllSpExecute.SpExecuteString("SP_ForcastingOrderManage", "MakingNo", param);

            return res;
        }

        internal string ForcastingOrderManageSave(ForcastingOrderManage forcastingOrderManage)
        {
            string res = "";

            if (forcastingOrderManage.row_status == "I2")
            {
                var forcast_no = NewForcastNo(forcastingOrderManage.request_date);

                string[] param = new string[12];
                param[0] = "@forcast_order_no:" + forcast_no;
                param[1] = "@h_item_cd:" + forcastingOrderManage.order_request_h_item_cd;
                param[2] = "@c_item_cd:" + forcastingOrderManage.order_request_c_item_cd;
                param[3] = "@request_date:" + forcastingOrderManage.request_date;
                param[4] = "@c_qty:" + Convert.ToDecimal(forcastingOrderManage.c_qty);
                param[5] = "@require_date:" + forcastingOrderManage.require_date;
                param[6] = "@remark:" + forcastingOrderManage.remark;
                param[7] = "@insert_user_cd:" + Public_Function.User_cd;
                param[8] = "@h_qty:" + Convert.ToDecimal(forcastingOrderManage.h_qty);
                param[9] = "@item_unit:" + forcastingOrderManage.item_unit;
                param[10] = "@h_item_nm:" + forcastingOrderManage.order_request_h_item_nm;
                param[11] = "@c_item_nm:" + forcastingOrderManage.order_request_c_item_nm;

                res = _bllSpExecute.SpExecuteString("SP_ForcastingOrderManage", "I2", param);
            }

            if (forcastingOrderManage.row_status == "U2")
            {
                string[] param = new string[10];
                param[0] = "@forcast_order_no:" + forcastingOrderManage.forcast_order_no;
                param[1] = "@h_item_cd:" + forcastingOrderManage.order_request_h_item_cd;
                param[2] = "@c_item_cd:" + forcastingOrderManage.order_request_c_item_cd;
                param[3] = "@request_date:" + forcastingOrderManage.request_date;
                param[4] = "@c_qty:" + Convert.ToDecimal(forcastingOrderManage.c_qty);
                param[5] = "@require_date:" + forcastingOrderManage.require_date;
                param[6] = "@remark:" + forcastingOrderManage.remark;
                param[7] = "@update_user_cd:" + Public_Function.User_cd;
                param[8] = "@h_qty:" + Convert.ToDecimal(forcastingOrderManage.h_qty);
                param[9] = "@item_unit:" + forcastingOrderManage.item_unit;

                res = _bllSpExecute.SpExecuteString("SP_ForcastingOrderManage", "U2", param);

            }

            return res;
        }

        internal string ForcastingOrderManageDelete(string forcast_order_no)
        {
            string res = "";

            string message = "";

            string param = "@forcast_order_no:" + forcast_order_no;

            message = _bllSpExecute.SpExecuteString("SP_ForcastingOrderManage", "D2", param);

            if (message == "N")
            {
                res = "발주량이 집계된 등록된 데이터는 삭제할 수 없습니다.!";
            }

            return res;
        }

        internal string GetItemUnit(string item_cd)
        {
            string res = "";
            string param = "@item_cd:" + item_cd;

            res = _bllSpExecute.SpExecuteString("SP_ForcastingOrderManage", "item_unit", param);
            return res;

        }

        internal string Setting_loss_h_qty(ForcastingOrderManage ForcastingOrderManage)
        {
            string res = "";

            res = _bllSpExecute.SpExecuteString("SP_ForcastingOrderManage", "setting_h_item_qty", "@item_cd:" + ForcastingOrderManage.order_request_c_item_cd, "@c_item_qty:" + ForcastingOrderManage.c_qty);
           
            return res;
        }

        internal DataTable Setting_h_item(string item_cd)
        {
            string[] param = new string[1];
            param[0] = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable("SP_ForcastingOrderManage", "setting_h_item", param);

            return dt;
        }

    }
}