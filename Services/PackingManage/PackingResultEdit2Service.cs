using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.PackingManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PackingManage
{
    public class PackingResultEdit2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectPackingResultData(PackingResult.SrchParam srchParam)
        {
            string sSpName = "SP_PackingResult_Edit2";
            string gubun = "";

            string[] pParam = new string[5];
            pParam[0] = "@s_item_cd:" + srchParam.s_item_cd;
            pParam[1] = "@s_sdate:" + srchParam.s_sdate;
            pParam[2] = "@s_edate:" + srchParam.s_edate;
            pParam[3] = "@s_lot_no:" + srchParam.s_lot_no;
            pParam[4] = "@s_order_status:" + srchParam.s_order_status;

            if (srchParam.showPriceYn.Equals("true"))
            {
                gubun = "S1_S";
            }
            else
            {
                gubun = "S1";
            }

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectPackingResultDetail(string packing_order_no)
        {
            string sSpName = "SP_PackingResult_Edit2";

            string[] pParam = new string[1];
            pParam[0] = "@packing_order_no:" + packing_order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string PackingResult_Edit2TRX(PackingResult dto, string gubun)
        {
            string sSpName = "SP_PackingResult_Edit2";

            string res = "";

            if (gubun.Equals("U"))
            {
                string[] pParam = new string[19];                                          //파라미터
                pParam[0] = "@packing_result_id:" + dto.packing_result_id;     //포장실적일련번호
                pParam[1] = "@packing_date:" + dto.packing_date;                  //포장일자
                pParam[2] = "@packing_qty:" + dto.packing_qty;                                          //생산량

                pParam[3] = "@test_sample_qty:" + dto.test_sample_qty;                                          //시험보관량

                pParam[4] = "@deposit_sample_qty:" + dto.deposit_sample_qty;
                pParam[5] = "@receive_qty:" + dto.receive_qty;
                pParam[6] = "@bulk_use_qty:" + dto.bulk_use_qty;
                pParam[7] = "@remain_qty:" + dto.remain_qty;                          //잔량
                pParam[8] = "@disuse_qty:" + dto.disuse_qty;                              //폐기량

                pParam[9] = "@result_emp:" + dto.result_emp_cd;                              //작업자

                pParam[10] = "@lot_no:" + dto.lot_no;        //제조번호
                pParam[11] = "@lot_date:" + dto.lot_date;                                  //제조일자
                pParam[12] = "@end_date:" + dto.end_date;                                  //유효기간
                pParam[13] = "@item_cd:" + dto.item_cd; //제품코드
                pParam[14] = "@packing_order_no:" + dto.packing_order_no; //포장지시번호

                pParam[15] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();
                pParam[16] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();
                pParam[17] = "@log_user_id :" + HttpContext.Current.Session["loginCD"].ToString();
                pParam[18] = "@item_stock_id:" + dto.item_stock_id;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);
            }else if (gubun.Equals("D"))
            {
                string[] pParam = new string[2];                                     
                pParam[0] = "@packing_result_id:" + dto.packing_result_id;
                pParam[1] = "@log_user_id:" + HttpContext.Current.Session["loginCD"].ToString();

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);
            }

            return res;
        }

        internal string PackingResultEditRequestTest(PackingResult param, string gubun)
        {
            string sSpName = "SP_PackingResult_Edit2";

            string[] pParam = new string[20];
            pParam[0] = "@test_type:" + param.test_type;
            pParam[1] = "@item_cd:" + param.item_cd;
            pParam[2] = "@start_date:" + param.start_date;
            pParam[3] = "@start_no:" + param.start_no;
            pParam[4] = "@receive_date:" + param.receive_date;
            pParam[5] = "@picking_date:" + param.picking_date;
            pParam[6] = "@picking_emp_cd:" + param.picking_emp_cd;
            pParam[7] = "@test_sample_qty:" + param.test_sample_qty;
            pParam[8] = "@deposit_sample_qty:" + param.deposit_sample_qty;
            pParam[9] = "@picking_method:" + param.picking_method;
            pParam[10] = "@packing_order_no:" + param.packing_order_no;
            pParam[11] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[12] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[13] = "@log_user_id:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[14] = "@packing_result_id:" + param.packing_result_id;
            pParam[15] = "@write_gb:" + param.write_gb;
            pParam[16] = "@item_form_cd:" + param.item_form_cd;
            pParam[17] = "@order_no:" + param.order_no;
            pParam[18] = "@request_no:" + param.request_no;
            pParam[19] = "@request_time1:" + DateTime.Now.ToString("HH:mm:ss.fff");

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }

        internal string PackingResultEdit2LotEnd(string packing_order_no)
        {
            string sSpName = "SP_PackingResult_Edit2";

            string[] pParam = new string[2];
            pParam[0] = "@packing_order_no:" + packing_order_no;
            pParam[1] = "@log_user_id:" + HttpContext.Current.Session["loginCD"].ToString();

            string res = _bllSpExecute.SpExecuteString(sSpName, "Lot_End", pParam);

            return res;
        }
    }
}