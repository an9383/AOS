using HACCP.Libs.Database;
using HACCP.Models.DepositMng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.DepositMng
{
    public class DepositSampleUseService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_DepositSampleUse";

        internal DataTable DepositSampleUseSelect(DepositSampleUse.SrchParam param)
        {
            string[] pParam = new string[10];
            pParam[0] = "@write_gb:" + param.write_gb;
            pParam[1] = "@sdate:" + param.sdate;
            pParam[2] = "@edate:" + param.edate;
            pParam[3] = "@test_type:" + param.test_type;
            pParam[4] = "@item_cd:" + param.item_cd;
            pParam[5] = "@depositsample_id:" + param.depositsample_id;
            pParam[6] = "@selecttype:" + param.selecttype;
            pParam[7] = "@item_type1:" + param.item_type1;
            pParam[8] = "@item_type2:" + param.item_type2;
            pParam[9] = "@selecttype2:" + param.selecttype2;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable DepositSampleUseSelectDetail(string depositsample_id, string write_gb)
        {
            string[] pParam = new string[2];
            pParam[0] = "@write_gb:" + write_gb;
            pParam[1] = "@depositsample_id:" + depositsample_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string DepositSampleUseTRX(DepositSampleUse depositSampleUse)
        {
            //상단 그리드의 재고량
            string total_stock_qty = depositSampleUse.stock_qty.Replace(",", "");
            total_stock_qty = (total_stock_qty == "" ? "0" : total_stock_qty);
            Decimal total_stock_qty_long = Convert.ToDecimal(total_stock_qty);
            //상단 그리드의 재고수량
            string total_stock_unit_qty = depositSampleUse.stock_unit_qty.Replace(",", "");
            total_stock_unit_qty = (total_stock_unit_qty == "" ? "0" : total_stock_unit_qty);
            Decimal total_stock_unit_qty_long = Convert.ToDecimal(total_stock_unit_qty);

            //상단 그리드의 남은 요청수량
            string total_request_qty = depositSampleUse.request_qty.Replace(",", "");
            total_request_qty = (total_request_qty == "" ? "0" : total_request_qty);
            Decimal total_request_qty_long = Convert.ToDecimal(total_request_qty);

            //상단 그리드의 남은 요청량
            string total_request_unit_qty = depositSampleUse.request_unit_qty.Replace(",", "");
            total_request_unit_qty = (total_request_unit_qty == "" ? "0" : total_request_unit_qty);
            Decimal total_request_unit_qty_long = Convert.ToDecimal(total_request_unit_qty);

            //하단 그리드의 요청수량
            string request_qty = depositSampleUse.use_qty.Replace(",", "");
            request_qty = (request_qty == "" ? "0" : request_qty);
            Decimal request_qty_long = Convert.ToDecimal(request_qty);

            //하단그리드의 요청량
            string request_unit_qty = depositSampleUse.use_unit_qty.Replace(",", "");
            request_unit_qty = (request_unit_qty == "" ? "0" : request_unit_qty);
            Decimal request_unit_qty_long = Convert.ToDecimal(request_unit_qty);

            string gubun = "";
            string res = "";
            string[] pParam = null;

            switch (depositSampleUse.gubun)
            {
                case "I":

                    if (depositSampleUse.write_gb == "1") //보관검체사용프로그램의 사용인 경우 요청수, 요청수량의 재고 차감
                    {
                        gubun = "I";

                        //요청수량 입력 후 상단 그리드에 남은 총 요청 수량 셋팅( 남은요청수량 + 입력 된 요청수량)
                        depositSampleUse.request_qty = (total_request_qty_long + request_qty_long).ToString();
                        //남은 총 요청량 셋팅( 남은요청량 + 입력 된 요청량)
                        depositSampleUse.request_unit_qty = (total_request_unit_qty_long + request_unit_qty_long).ToString();
                    }
                    else if (depositSampleUse.write_gb == "2")
                    {
                        gubun = "I2";

                        //사용량 입력 후 상단 그리드에 재고 수량 업데이트(재고수량 = 기존재고수량 - 사용수량)
                        depositSampleUse.stock_qty = (total_stock_qty_long - request_qty_long).ToString();
                        //재고량 업데이트(재고량 = 기존재고량 - 사용량)
                        depositSampleUse.stock_unit_qty = (total_stock_unit_qty_long - request_unit_qty_long).ToString(); ;
                    }

                    pParam = GetParam(depositSampleUse);

                    break;

                case "U":

                    gubun = "U";

                    //수정전요청수량
                    string before_request_qty = depositSampleUse.before_request_qty.Replace(",", "");
                    before_request_qty = (before_request_qty == "" ? "0" : before_request_qty);
                    Decimal before_request_qty_long = Convert.ToDecimal(before_request_qty);
                    //수정전 요청량
                    string before_request_unit_qty = depositSampleUse.before_request_unit_qty.Replace(",", "");
                    before_request_unit_qty = (before_request_unit_qty == "" ? "0" : before_request_unit_qty);
                    Decimal before_request_unit_qty_long = Convert.ToDecimal(before_request_unit_qty);

                    if (depositSampleUse.write_gb == "1")
                    {
                        //남은 요청수량 = 수정전 남은요청수량 - 수정전요청수량 + 수정된 요청수량
                        depositSampleUse.request_qty = (total_request_qty_long - before_request_qty_long + request_qty_long).ToString();
                        //남은 요청수량 = 수정전 남은요청량 - 수정전요청량 + 수정된 요청량
                        depositSampleUse.request_unit_qty = (total_request_unit_qty_long - before_request_unit_qty_long + request_unit_qty_long).ToString();
                    }
                    else if (depositSampleUse.write_gb == "2")
                    {
                        //사용량 입력 후 상단 그리드에 재고 수량 업데이트(재고수량 = 기존재고수량 - (이전사용수량 - 변경된 사용수량))
                        depositSampleUse.stock_qty = (total_stock_qty_long + (before_request_qty_long - request_qty_long)).ToString();
                        //재고량 업데이트(재고량 = 기존재고량 - (이전사용량 -변경된 사용량))
                        depositSampleUse.stock_unit_qty = (total_stock_unit_qty_long + (before_request_unit_qty_long - request_unit_qty_long)).ToString();
                    }

                    pParam = GetParam(depositSampleUse);

                    break;

                case "D":

                    gubun = "D";

                    pParam = new string[2];

                    pParam[0] = "@depositsample_id:" + depositSampleUse.depositsample_id;
                    pParam[1] = "@depositsampleuse_id:" + depositSampleUse.depositsampleuse_id;

                    break;
            }

            res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }

        private string[] GetParam(DepositSampleUse depositSampleUse)
        {
            string[] pParam = new string[9];

            pParam[0] = "@depositsample_id:" + depositSampleUse.depositsample_id;
            pParam[1] = "@use_date:" + depositSampleUse.use_date;
            pParam[2] = "@use_emp_cd:" + depositSampleUse.use_emp_cd;
            pParam[3] = "@use_qty:" + depositSampleUse.use_qty;
            pParam[4] = "@use_unit_qty:" + depositSampleUse.use_unit_qty;
            pParam[5] = "@remark:" + depositSampleUse.remark;
            pParam[6] = "@depositsampleuse_id:" + depositSampleUse.depositsampleuse_id;
            pParam[7] = "@write_gb:" + depositSampleUse.write_gb;
            pParam[8] = "@status:" + depositSampleUse.status_cd;

            return pParam;
        }
    }
}