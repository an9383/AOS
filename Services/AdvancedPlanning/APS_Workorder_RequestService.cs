using HACCP.Libs.Database;
using HACCP.Models.AdvancedPlanning;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.AdvancedPlanning
{
    public class APS_Workorder_RequestService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable APS_Workorder_RequestServiceSearch(APS_Workorder_Request.SrchParam srch)
        {
            string[] param = new string[3];
            param[0] = "@sdate:" + srch.Sdate;
            param[1] = "@edate:" + srch.Edate;
            param[2] = "@order_request_h_item_cd:" + srch.order_request_h_item_cd;

            var gubun = srch.OrderStatus == true ? "S1" : "S2";

            DataTable dt = _bllSpExecute.SpExecuteTable("SP_APS_Workorder_Request", gubun, param);

            return dt;
        }

        internal DataTable APS_Workorder_RequestServiceDetailSearch(APS_Workorder_Request request)
        {
            string[] param = new string[5];
            param[0] = "@order_request_h_item_cd:" + request.order_request_h_item_cd;
            param[1] = "@order_request_c_item_cd:" + request.order_request_c_item_cd;
            param[2] = "@batch_size:" + Convert.ToDecimal(request.order_request_h_qty);
            param[3] = "@order_request_no:" + request.order_request_no;
            param[4] = "@order_request_qty:" + Convert.ToDecimal(request.order_request_qty);

            DataTable dt = _bllSpExecute.SpExecuteTable("SP_APS_Workorder_Request", "S3", param);

            return dt;

        }

        internal string APS_Workorder_RequestDetailSave(List<APS_Workorder_Request> paramData)
        {
            string res = "";

            foreach (APS_Workorder_Request tmp in paramData)
            {
                string message = "";
                string[] param = new String[4];
                param[0] = "@order_request_no:" + tmp.order_request_no;
                param[1] = "@req_order_id:" + tmp.req_order_id;
                param[2] = "@req_order_child_cd:" + tmp.req_order_child_cd;
                param[3] = "@order_stock:" + Convert.ToDecimal(tmp.order_stock);

                message = _bllSpExecute.SpExecuteString("SP_APS_Workorder_Request", "U2", param);
                if (message != "") res = message;

            }

            return res;
        }

        #region 소요량 저장
        internal string ReleaseConfirmation(APS_Workorder_Request request)
        {
            string[] param = new string[4];
            param[0] = "@order_request_no:" + request.order_request_no;
            param[1] = "@item_cd:" + request.req_order_child_cd;
            param[2] = "@process_cd:" + request.process_cd;
            param[3] = "@mp_ck:" + request.mp_ck;

            string res = _bllSpExecute.SpExecuteString("SP_APS_Workorder_Request", "INSERT_SAVE", param);

            return res;
        }

        //생산 계획 확인
        internal string InsertPlanProductionItem(string order_request_no)
        {
            string[] param = new string[1];
            param[0] = "@order_request_no:" + order_request_no;

            string res = "";
            res = _bllSpExecute.SpExecuteString("SP_APS_PlanProduction", "CheckPlanProductionItem", param);

            if (res != "M" && res != "ALL" && res != "P")
            {
                res = "생산 계획이 이미 등록되었습니다.";
            }

            return res;
        }

        //제조생산계획 입력
        internal string InsertPlanProductionItem_M(APS_Workorder_Request request)
        {
            //제조제품
            string prod_year = request.order_request_date.Substring(0, 4).ToString();
            string prod_month = request.order_request_date.Substring(5, 2).ToString();

            string[] param1 = new string[2];
            param1[0] = "@prod_year:" + prod_year;
            param1[1] = "@prod_month:" + prod_month;
            string prod_seq = _bllSpExecute.SpExecuteString("SP_APS_PlanProduction", "InsertPlanProductionItem", param1);

            string message = "";

            string[] param = new string[12];

            param[0] = "@prod_year:" + prod_year;
            param[1] = "@prod_month:" + prod_month;
            param[2] = "@prod_seq:" + prod_seq;
            param[3] = "@item_cd:" + request.order_request_h_item_cd;
            param[4] = "@lot_size:" + Convert.ToDecimal(request.order_request_h_qty);
            param[5] = "@lot_size_unit_cd:" + request.item_packunit;
            param[6] = "@lot_qty:" + "0";
            param[7] = "@start_date:" + DateTime.Today.ToString("yyyy-MM-01");
            param[8] = "@end_date:" + DateTime.Today.AddMonths(1).AddDays(0 - DateTime.Today.Day).ToString("yyyy-MM-dd");
            param[9] = "@determination_yn:" + "Y";
            param[10] = "@order_request_no:" + request.order_request_no;
            param[11] = "mp_ck:" + "M";

            message = _bllSpExecute.SpExecuteString("SP_APS_PlanProduction", "InsertPlanProductionItem", param);

            if (message.Length <= 0)
            {
                message = "제조제품 생산 등록이 되지 않았습니다.!";
            }
            else if (message == "return")
            {
                message = "해당 생산의뢰번호로 이미 생산 계획이 등록 되었습니다.!";
            }
            return message;
        }

        //완제품생산계획 입력
        internal string InsertPlanProductionItem_P(APS_Workorder_Request request)
        {
            //포장제품
            string prod_year = request.order_request_date.Substring(0, 4).ToString();
            string prod_month = request.order_request_date.Substring(5, 2).ToString();

            string[] param1 = new string[2];
            param1[0] = "@prod_year:" + prod_year;
            param1[1] = "@prod_month:" + prod_month;
            string prod_seq = _bllSpExecute.SpExecuteString("SP_APS_PlanProduction", "SearchProdSeq", param1);

            string message = "";

            string[] param = new string[12];

            param[0] = "@prod_year:" + prod_year;
            param[1] = "@prod_month:" + prod_month;
            param[2] = "@prod_seq:" + prod_seq;
            param[3] = "@item_cd:" + request.order_request_c_item_cd;
            param[4] = "@lot_size:" + Convert.ToDecimal(request.order_request_qty);
            param[5] = "@lot_size_unit_cd:" + request.item_packunit;
            param[6] = "@lot_qty:" + "0";
            param[7] = "@start_date:" + DateTime.Today.ToString("yyyy-MM-01");
            param[8] = "@end_date:" + DateTime.Today.AddMonths(1).AddDays(0 - DateTime.Today.Day).ToString("yyyy-MM-dd");
            param[9] = "@determination_yn:" + "Y";
            param[10] = "@order_request_no:" + request.order_request_no;
            param[11] = "mp_ck:" + "P";

            message = _bllSpExecute.SpExecuteString("SP_APS_PlanProduction", "InsertPlanProductionItem", param);

            if (message.Length <= 0)
            {
                message = "완제품 생산 등록이 되지 않았습니다.!";
            }
            else
            {
                message = "생산 계획 등록이 정상적으로 처리 되었습니다.";
            }

            return message;
        }
        #endregion

        #region 소요량 저장 취소
        internal string CancelReleaseConfirmation(string order_request_no)
        {
            string[] param = new string[1];
            param[0] = "@order_request_no:" + order_request_no;
            //param[1] = "@req_order_id:" + req_order_id;
            //param[2] = "@process_cd:" + process_cd;
            //param[3] = "@emp_cd:" + emp_cd;

            string res = "";
            res = _bllSpExecute.SpExecuteString("SP_APS_Workorder_Request", "DELETE", param);

            if (int.Parse(res) < 0)
            {
                res = "칭량작업이 완료된 원료는 확정취소를 할 수 없습니다!";
            }
            else
            {
                res = "소요량 저장이 취소되었습니다.!";
            }

            return res;
        }
        #endregion

        #region 소요량 자동계산
        internal string AutoCalc(APS_Workorder_Request request)
        {
            string message = "";
            int chk = 0;

            string[] param = new string[21];
            param[0] = "@order_request_no:" + request.order_request_no;
            param[1] = "@req_order_id:" + request.req_order_id;
            param[2] = "@process_cd:" + request.process_cd;
            param[3] = "@req_order_child_cd:" + request.req_order_child_cd;
            param[4] = "@item_cd:" + request.req_order_child_cd;  //위에랑 동일한 값이 맞는지?
            param[5] = "@pItem_cd:" + request.order_request_h_item_cd;
            param[6] = "@req_order_batch_qty:" + Convert.ToDecimal(request.req_order_batch_qty);
            param[7] = "@item_bom_id:" + request.item_bom_id;
            param[8] = "@item_bom_no:" + request.item_bom_no;
            param[9] = "@req_order_child_packunit:" + request.req_order_child_packunit;

            param[10] = "@wait_stock:" + Convert.ToDecimal(request.wait_stock);     //입고대기량
            param[11] = "@receipt_reserve_qty:" + Convert.ToDecimal(request.receipt_reserve_qty);   //출고예약량
            param[12] = "@receipt_remain_qty:" + Convert.ToDecimal(request.receipt_remain_qty); //재고수량
            param[13] = "@shortage:" + Convert.ToDecimal(request.shortage);   //과부족량
            param[14] = "@shortage2:" + Convert.ToDecimal(request.shortage2);  //계산후재고
                                                                                                          //param[15] = "@method:" + gv_aps_req_order.GetRowCellValue(i, gc_method).ToString(); //조달방법
            param[15] = "@default_stock:" + Convert.ToDecimal(request.default_stock); //미납수량
            param[16] = "@order_stock:" + Convert.ToDecimal(request.order_stock); //발주량
            param[17] = "@ordering:" + request.ordering; //발주처
            param[18] = "@company:" + request.company; //상호
            param[19] = "@etc:" + request.etc; //비고
            param[20] = "@mp_ck:" + request.mp_ck; //원자재 구분

            message = _bllSpExecute.SpExecuteString("SP_APS_Workorder_Request", "INSERT", param);

            return message;
        }

        #endregion

        #region 계산 취소
        internal string CancelAutoCalc(string order_request_no)
        {
            string[] param = new string[1];
            param[0] = "@order_request_no:" + order_request_no;

            string res = "";
            res = _bllSpExecute.SpExecuteString("SP_APS_Workorder_Request", "D3", param);

            if(res.Length > 0)
            {
                res = "소요량 자동 계산이 취소되었습니다.";
            }

            return res;
        }

        #endregion

        #region 결품 확인
        internal void UpdateShortage(APS_Workorder_Request request)
        {
            string[] param = new string[5];

            param[0] = "@order_request_h_item_cd:" + request.order_request_h_item_cd;
            param[1] = "@order_request_c_item_cd:" + request.order_request_c_item_cd;
            param[2] = "@order_request_no:" + request.order_request_no;
            param[3] = "@batch_size:" + Convert.ToDecimal(request.order_request_h_qty);
            param[4] = "@order_request_qty:" + Convert.ToDecimal(request.order_request_qty);

            _bllSpExecute.SpExecuteString("SP_APS_Workorder_Request", "UpdateShortage_M", param);
            _bllSpExecute.SpExecuteString("SP_APS_Workorder_Request", "UpdateShortage_P", param);
        }
        #endregion

        //진행상태 가져오기
        internal DataTable UpdateStatus(string order_request_no)
        {
            string[] param = new string[1];
            param[0] = "@order_request_no:" + order_request_no;

            DataTable dt = _bllSpExecute.SpExecuteTable("SP_APS_Workorder_Request", "UpdateStatus", param);

            return dt;
        }


    }
}