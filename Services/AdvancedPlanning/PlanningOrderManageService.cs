using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models;
using HACCP.Models.AdvancedPlanning;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace HACCP.Services.AdvancedPlanning
{
    public class PlanningOrderManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        //조회
        internal DataTable PlanningOrderManageSearch(PlanningOrderManage.SrchParam srch)
        {

            string[] param = new string[5];
            param[0] = "@de_Sdate:" + srch.start_date;
            param[1] = "@de_Edate:" + srch.end_date;
            param[2] = "@order_request_c_item_cd:" + srch.order_request_item_cd;
            param[3] = "@order_request_c_item_nm:" + srch.order_request_item_nm;
            param[4] = "@s_cust_cd:" + srch.cust_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_APS_PlanningOrderManage", "S", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;

        }

        //수정 및 저장
        internal string PlanningOrderManageSave(PlanningOrderManage planningOrderManage)
        {
            string res = "";

            if(planningOrderManage.row_status == "I2")
            {
                var order_request_no = PlanningOrderManage_NewRequestNo(planningOrderManage.order_request_date);

                string[] param = new string[22];
                param[0] = "@order_request_no:" + order_request_no;
                param[1] = "@order_request_h_item_cd:" + planningOrderManage.order_request_h_item_cd;
                param[2] = "@order_request_c_item_cd:" + planningOrderManage.order_request_c_item_cd;
                param[3] = "@order_request_date:" + planningOrderManage.order_request_date;
                param[4] = "@cust_cd:" + planningOrderManage.cust_cd;
                param[5] = "@sales_emp_cd:" + planningOrderManage.sales_emp_cd;

                param[6] = "@order_request_qty:" + Convert.ToDecimal(planningOrderManage.order_request_qty);
                param[7] = "@order_request_price:" + Convert.ToDecimal(planningOrderManage.order_request_price);
                param[8] = "@order_request_amt:" + Convert.ToDecimal(planningOrderManage.order_request_amt);
                param[9] = "@require_date:" + planningOrderManage.require_date;
                param[10] = "@remark:" + planningOrderManage.remark;
                param[11] = "@insert_user_cd:" + Public_Function.User_cd;
                param[12] = "@order_request_h_qty:" + Convert.ToDecimal(planningOrderManage.loss_h_qty);
                param[13] = "@item_packunit:" + planningOrderManage.item_unit;
                param[14] = "@order_request_h_item_nm:" + planningOrderManage.order_request_h_item_nm;
                param[15] = "@order_request_c_item_nm:" + planningOrderManage.order_request_c_item_nm;
                param[16] = "@m_loss:" + Convert.ToDecimal(planningOrderManage.m_loss);
                param[17] = "@p_loss:" + Convert.ToDecimal(planningOrderManage.p_loss);
                param[18] = "@m_bom_no:" + planningOrderManage.m_bom_no;
                param[19] = "@p_bom_no:" + planningOrderManage.p_bom_no;
                param[20] = "@c_qty:" + Convert.ToDecimal(planningOrderManage.c_qty);
                param[21] = "@add_delivery_ck:" + planningOrderManage.add_delivery_ck;

                res = _bllSpExecute.SpExecuteString("SP_APS_PlanningOrderManage", "I2", param);
                res = order_request_no;
            }

            if (planningOrderManage.row_status == "U2")
            {
                string[] param = new string[20];
                param[0] = "@order_request_no:" + planningOrderManage.order_request_no;
                param[1] = "@order_request_h_item_cd:" + planningOrderManage.order_request_h_item_cd;
                param[2] = "@order_request_c_item_cd:" + planningOrderManage.order_request_c_item_cd;
                param[3] = "@order_request_date:" + planningOrderManage.order_request_date;
                param[4] = "@cust_cd:" + planningOrderManage.cust_cd;
                param[5] = "@sales_emp_cd:" + planningOrderManage.sales_emp_cd;

                param[6] = "@order_request_qty:" + Convert.ToDecimal(planningOrderManage.order_request_qty);
                param[7] = "@order_request_price:" + Convert.ToDecimal(planningOrderManage.order_request_price);
                param[8] = "@order_request_amt:" + Convert.ToDecimal(planningOrderManage.order_request_amt);
                param[9] = "@require_date:" + planningOrderManage.require_date;
                param[10] = "@remark:" + planningOrderManage.remark;
                param[11] = "@insert_user_cd:" + Public_Function.User_cd;
                param[12] = "@order_request_h_qty:" + Convert.ToDecimal(planningOrderManage.loss_h_qty);
                param[13] = "@item_packunit:" + planningOrderManage.item_unit;
                param[14] = "@m_loss:" + Convert.ToDecimal(planningOrderManage.m_loss);
                param[15] = "@p_loss:" + Convert.ToDecimal(planningOrderManage.p_loss);
                param[16] = "@m_bom_no:" + planningOrderManage.m_bom_no;
                param[17] = "@p_bom_no:" + planningOrderManage.p_bom_no;
                param[18] = "@c_qty:" + Convert.ToDecimal(planningOrderManage.c_qty);
                param[19] = "@add_delivery_ck:" + planningOrderManage.add_delivery_ck;

                res = _bllSpExecute.SpExecuteString("SP_APS_PlanningOrderManage", "U2", param);

                if(res == "N")
                {
                    return "생산 계획이 등록된 데이터는 수정할 수 없습니다"; //ck_yn이 00(생산의뢰)일때 수정 및 삭제 가능
                }
                else
                {
                    InputPlanProductionItem(planningOrderManage);
                }
            }

                return res;
        }

        //삭제
        internal string PlanningOrderManageDelete(string order_request_no)
        {
            string res = "";

            string message = "";

            string param = "@order_request_no:" + order_request_no;

            message = _bllSpExecute.SpExecuteString("SP_APS_PlanningOrderManage", "D2", param);

            if (message == "N")
            {
                res = "생산 계획이 등록된 데이터는 삭제할 수 없습니다.!";
            }

            return res;
        }


        //생산의뢰번호 생성
        internal string PlanningOrderManage_NewRequestNo(string order_request_date)
        {
            string[] param = new string[1];
            param[0] = "@order_request_date:" + order_request_date;

            string res = _bllSpExecute.SpExecuteString("SP_APS_PlanningOrderManage", "MakingNo", param);

            return res;
        }


        internal void InputPlanProductionItem(PlanningOrderManage planningOrderManage)
        {
            string res = "";

            string[] param = new string[11];

            string prod_year = Convert.ToString(planningOrderManage.order_request_date.Substring(0, 4));
            string prod_month = Convert.ToString(planningOrderManage.order_request_date.Substring(5, 2));

            DateTime dt = System.DateTime.Parse(prod_year + "-" + prod_month);
            DateTime last_day = dt.AddMonths(1).AddDays(0 - dt.Day);

            string prod_seq = _bllSpExecute.SpExecuteString("SP_APS_PlanProduction", "SearchProdSeq", "@prod_year:", "@prod_month:");
            string start_date = dt.ToString("yyyy-MM-01");
            string end_date = last_day.ToString("yyyy-MM-dd");

            param[0] = "@prod_year:" + prod_year;
            param[1] = "@prod_month:" + prod_month;
            param[2] = "@item_cd:" + planningOrderManage.order_request_h_item_cd;
            param[3] = "@lot_qty:" + Convert.ToDecimal(planningOrderManage.loss_h_qty);
            param[4] = "@lot_size_unit_cd:" + planningOrderManage.item_unit;
            param[5] = "@start_date:" + start_date;
            param[6] = "@end_date:" + end_date;
            param[7] = "@remark:" + planningOrderManage.remark;
            param[8] = "@determination_yn:" + "N";
            param[9] = "@prod_seq:" + prod_seq;
            param[10] = "@lot_no:" + "";

            //prod_year, prod_month, item_cd, prod_seq 가 pk가 되어 plan_production_item 테이블에 pk가 중복되면 insert되지 않음 -> 수정시에 연월, 품목코드가 달라야 insert됨
            res = _bllSpExecute.SpExecuteString("SP_APS_PlanProduction", "InsertPlanProductionItem", param); //gubun 값 중에 InsertPlanProductionItem2는 없음(코스맥스 바이오도 동일함)
        }

        //포장단위 설정
        internal string GetItemPackunit(string item_cd)
        {
            string[] param = new string[1];
            param[0] = "@item_cd:" + item_cd;

            string res = _bllSpExecute.SpExecuteString("SP_APS_PlanningOrderManage", "item_packunit", param);

            return res;
        }

        //포장품목 설정
        internal DataTable Setting_h_item(string item_cd)
        {
            string[] param = new string[1];
            param[0] = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable("SP_APS_PlanningOrderManage", "setting_h_item", param);

            return dt;
        }

        //제조, 포장 bom 번호 가져오기
        internal string Setting_bom_no(string order_request_c_item_cd, string order_request_h_item_cd)
        {
            string p_bom_no = "";
            string m_bom_no = "";

            m_bom_no = _bllSpExecute.SpExecuteString("SP_APS_PlanningOrderManage", "setting_m_bom_no", "@order_request_h_item_cd:" + order_request_h_item_cd);
            p_bom_no = _bllSpExecute.SpExecuteString("SP_APS_PlanningOrderManage", "setting_p_bom_no", "@order_request_c_item_cd:" + order_request_c_item_cd);

            return m_bom_no + "," + p_bom_no;
        }

        //제조환산수량 계산
        internal string Setting_loss_c_qty(PlanningOrderManage planningOrderManage)
        {
            string[] param = new string[3];
            param[0] = "@item_cd:" + planningOrderManage.order_request_c_item_cd;
            param[1] = "@c_item_qty:" + planningOrderManage.c_qty;
            param[2] = "@p_loss:" + planningOrderManage.p_loss;

            string res = _bllSpExecute.SpExecuteString("SP_APS_PlanningOrderManage", "setting_c_item_qty", param);

            return res;
        }

        //포장환산수량 계산
        internal string Setting_loss_h_qty(PlanningOrderManage planningOrderManage)
        {
            string[] param = new string[3];
            param[0] = "@item_cd:" + planningOrderManage.order_request_c_item_cd;
            param[1] = "@c_item_qty:" + planningOrderManage.c_qty;
            param[2] = "@m_loss:" + planningOrderManage.m_loss;

            string res = _bllSpExecute.SpExecuteString("SP_APS_PlanningOrderManage", "setting_h_item_qty", param);

            return res;
        }

        //서명시 생산의뢰 상태 변경
        internal string StatusBySaveSign(string order_request_no, string sign_set_dt_id)
        {
            string[] param = new string[2];
            param[0] = "@order_request_no:" + order_request_no;
            param[1] = "@sign_set_dt_id:" + sign_set_dt_id;

            string res = _bllSpExecute.SpExecuteString("SP_APS_PlanningOrderManage", "STATUS_EI", param);

            return res;
        }

        //서명 취소
        internal string StatusByCancelSign(string order_request_no, string sign_history_id)
        {
            string[] param = new string[2];
            param[0] = "@order_request_no:" + order_request_no;
            param[1] = "@sign_history_id:" + sign_history_id;

            string res = _bllSpExecute.SpExecuteString("SP_APS_PlanningOrderManage", "STATUS_DI", param);

            return res;
        }
    }
}