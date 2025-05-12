using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.ProductionManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class WorkorderRequestService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectWorkOrder(string sdate, string edate, string item_cd, string order_no)
        {
            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[6];
            pParam[0] = "@sdate:" + sdate;
            pParam[1] = "@edate:" + edate;
            pParam[2] = "@item_cd:" + item_cd;
            pParam[3] = "@order_no:" + order_no;
            pParam[4] = "@MakingDeptCd:";
            pParam[5] = "@MakingDeptCd2:";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectItemPopupData(string item_cd, string item_nm)
        {
            string sSpName = "CODEHELP";
            string strsql = "SELECT item_cd, item_nm FROM v_item_makingstandard WHERE ("
                + "item_cd  LIKE '%" + item_cd + "%' OR "
                + "item_nm LIKE '%" + item_nm + "%') AND "
                + "item_cd  LIKE '%%' AND item_nm  LIKE '%%' ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, strsql);

            return dt;
        }

        internal DataTable SelectReleaseStandard(string order_no, string item_cd, string batch_size, string real_order_qty)
        {
            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[4];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@item_cd:" + item_cd;
            pParam[2] = "@batch_size:" + batch_size;
            pParam[3] = "@real_order_qty:" + real_order_qty.Replace(",", "");

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectUsage(string order_no, string req_order_gb, string req_order_id)
        {
            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[3];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@req_order_gb:" + req_order_gb;
            pParam[2] = "@req_order_id:" + req_order_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectCurrentStock(string req_order_child_cd, string item_cd, string order_no)
        {
            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[3];
            pParam[0] = "@req_order_child_cd:" + req_order_child_cd;
            pParam[1] = "@item_cd:" + item_cd;
            pParam[2] = "@order_no:" + order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S4", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string InsertUsage(List<WorkorderRequest> workorderRequests, string order_no)
        {
            string res = "";
            int calcCount = 0;

            using (TransactionScope transScope = new TransactionScope())
            {
                foreach (WorkorderRequest workorderRequest in workorderRequests)
                {
                    if (workorderRequest.req_order_status == "00")
                    { 
                        string sSpName = "SP_Workorder_Workorder_Request";
                        string[] param = new string[20];
                        param[0] = "@order_no:" + order_no;
                        param[1] = "@req_order_gb:" + workorderRequest.req_order_gb;
                        param[2] = "@req_order_id:" + workorderRequest.req_order_id;
                        param[3] = "@process_cd:" + workorderRequest.process_cd;
                        param[4] = "@req_order_child_cd:" + workorderRequest.req_order_child_cd;
                        param[5] = "@req_order_qty:" + workorderRequest.req_order_qty;
                        param[6] = "@req_order_calc_type:" + workorderRequest.req_order_calc_type;
                        param[7] = "@req_order_last_type:" + workorderRequest.req_order_last_type;
                        param[8] = "@req_order_real_qty:" + workorderRequest.req_order_real_qty;
                        param[9] = "@req_order_print_yn:" + workorderRequest.REQ_ORDER_PRINT_YN;
                        param[10] = "@item_cd:" + workorderRequest.req_order_child_cd;
                        param[11] = "@pItem_cd:" + workorderRequest.item_cd;
                        param[12] = "@req_order_batch_qty:" + String.Format("{0:0.00000000000000}", double.Parse(workorderRequest.req_order_real_qty));
                        param[13] = "@item_bom_id:" + workorderRequest.item_bom_id;
                        param[14] = "@item_bom_no:" + workorderRequest.item_bom_no;
                        param[15] = "@req_order_child_packunit:" + workorderRequest.req_order_child_packunit;
                        param[16] = "@REQ_ORDER_CASE_CD:" + workorderRequest.req_order_case_cd;
                        param[17] = "@req_order_manufacture_qty:" + String.Format("{0:0.00000000000000}", double.Parse(workorderRequest.req_order_manufacture_qty));
                        param[18] = "@std_pollination_rate:" + workorderRequest.std_pollination_rate;
                        param[19] = "@separate_cnt:" + workorderRequest.separate_count;

                        res = _bllSpExecute.SpExecuteString(sSpName, "IOO", param);

                        if (String.IsNullOrEmpty(res))
                        {
                            transScope.Dispose();
                            return res;
                        }
                        else
                            calcCount++;
                    }
                }

                transScope.Complete();
            }

            if (calcCount == 0)
                return "이미 계산이 완료되었습니다.";
            else
                return "계산 완료하였습니다.";
        }

        internal string SelectQty(string order_no, string req_order_gb, string req_order_id)
        {
            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[3];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@req_order_gb:" + req_order_gb;
            pParam[2] = "@req_order_id:" + req_order_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S5", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt.Rows[0].ItemArray[0].ToString();
        }

        internal string WorkorderRequestConfirm(string order_no, string req_order_gb, string req_order_id)
        {
            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[3];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@req_order_gb:" + req_order_gb;
            pParam[2] = "@req_order_id:" + req_order_id;

            string res = _bllSpExecute.SpExecuteString(sSpName, "CK_U", pParam);

            return res;
        }

        internal string WorkorderRequestFinalConfirm(string order_no)
        {
            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[2];
            pParam[0] = "@emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[1] = "@order_no:" + order_no;

            string res = _bllSpExecute.SpExecuteString(sSpName, "CK_F", pParam);

            return res;
        }

        internal string WorkorderRequestCheckStatus(string order_no)
        {
            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + order_no;

            string res = _bllSpExecute.SpExecuteString(sSpName, "CHK", pParam);

            return res;
        }

        internal string CancelReleaseConfirmation(string order_no)
        {
            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[2];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();

            string res = _bllSpExecute.SpExecuteString(sSpName, "D3", pParam);

            return res;
        }

        internal string WorkorderRequestDeleteAllUsage(string order_no)
        {
            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + order_no;

            string res = _bllSpExecute.SpExecuteString(sSpName, "D_ALL", pParam);

            return res;
        }

        internal string WorkorderRequestDeleteUsage(WorkorderRequest workorderRequest)
        {
            string res = "";

            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[4];
            pParam[0] = "@order_no:" + workorderRequest.order_no;
            pParam[1] = "@req_order_gb:" + workorderRequest.req_order_gb;
            pParam[2] = "@req_order_id:" + workorderRequest.req_order_id;
            pParam[3] = "@req_order_material_id:" + workorderRequest.req_order_material_id;

            res = _bllSpExecute.SpExecuteString(sSpName, "D2", pParam);

            return res;
        }

        internal string WorkorderRequestInsertUsage(WorkorderRequest workorderRequest)
        {
            string res = "";

            string sSpName = "SP_Workorder_Workorder_Request";
            string[] pParam = new string[10];
            pParam[0] = "@order_no:" + workorderRequest.order_no;
            pParam[1] = "@req_order_gb:" + workorderRequest.req_order_gb;
            pParam[2] = "@req_order_id:" + workorderRequest.req_order_id;
            pParam[3] = "@req_order_material_id:" + workorderRequest.req_order_material_id;
            pParam[4] = "@req_order_material_qc_no:" + workorderRequest.req_order_material_qc_no;
            pParam[5] = "@req_order_material_qty:" + workorderRequest.req_order_material_qty;
            pParam[6] = "@REQ_ORDER_MATH:" + workorderRequest.REQ_ORDER_MATH;
            pParam[7] = "@req_order_density:" + workorderRequest.req_order_density;
            pParam[8] = "@req_order_real_weight:" + workorderRequest.req_order_real_weight;
            pParam[9] = "@remarks:" + workorderRequest.qc_remark;

            res = _bllSpExecute.SpExecuteString(sSpName, "I2", pParam);

            return res;
        }

        internal string WorkorderRequestSeperateUsage(WorkorderRequest workorderRequest, string seperateNum)
        {
            Double seperatedQty = Double.Parse(workorderRequest.req_order_material_qty) / int.Parse(seperateNum);

            string[] pParam = new string[5];
            pParam[0] = "@order_no:" + workorderRequest.order_no;
            pParam[1] = "@req_order_gb:" + workorderRequest.req_order_gb;
            pParam[2] = "@req_order_id:" + workorderRequest.req_order_id;
            pParam[3] = "@separate_qty:" + seperatedQty;
            pParam[4] = "@separate_qty2:" + seperatedQty;

            try
            {
                _bllSpExecute.SpExecuteString("SP_Separate_calc", "A", pParam);

                _bllSpExecute.SpExecuteString("SP_Separate_calc2", "A", pParam);

                return "분할성공하였습니다.";
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        internal string CalcSeperate(List<WorkorderRequest> workorderRequests, string order_no, int separate_cnt)
        {
            string res = "";
            int calcCount = 0;
            using (TransactionScope transScope = new TransactionScope())
            {
                foreach (WorkorderRequest workorderRequest in workorderRequests)
                {
                    if (workorderRequest.req_order_status == "00")
                    { 
                        string sSpName = "SP_Workorder_Workorder_Request";
                        string[] param = new string[20];
                        param[0] = "@order_no:" + order_no;
                        param[1] = "@req_order_gb:" + workorderRequest.req_order_gb;
                        param[2] = "@req_order_id:" + workorderRequest.req_order_id;
                        param[3] = "@process_cd:" + workorderRequest.process_cd;
                        param[4] = "@req_order_child_cd:" + workorderRequest.req_order_child_cd;
                        param[5] = "@req_order_qty:" + workorderRequest.req_order_qty;
                        param[6] = "@req_order_calc_type:" + workorderRequest.req_order_calc_type;
                        param[7] = "@req_order_last_type:" + workorderRequest.req_order_last_type;
                        param[8] = "@req_order_real_qty:" + workorderRequest.req_order_real_qty;
                        param[9] = "@req_order_print_yn:" + workorderRequest.REQ_ORDER_PRINT_YN;
                        param[10] = "@item_cd:" + workorderRequest.req_order_child_cd;
                        param[11] = "@pItem_cd:" + workorderRequest.item_cd;
                        param[12] = "@req_order_batch_qty:" + String.Format("{0:0.00000000000000}", double.Parse(workorderRequest.req_order_real_qty));
                        param[13] = "@item_bom_id:" + workorderRequest.item_bom_id;
                        param[14] = "@item_bom_no:" + workorderRequest.item_bom_no;
                        param[15] = "@req_order_child_packunit:" + workorderRequest.req_order_child_packunit;
                        param[16] = "@REQ_ORDER_CASE_CD:" + workorderRequest.req_order_case_cd;
                        param[17] = "@req_order_manufacture_qty:" + String.Format("{0:0.00000000000000}", double.Parse(workorderRequest.req_order_manufacture_qty));
                        param[18] = "@std_pollination_rate:" + workorderRequest.std_pollination_rate;
                        param[19] = "@separate_cnt:" + separate_cnt;

                        res = _bllSpExecute.SpExecuteString(sSpName, "IOO", param);

                        if (String.IsNullOrEmpty(res))
                        {
                            transScope.Dispose();
                            return res;
                        }
                        else
                            calcCount++;
                    }
                }

                transScope.Complete();
            }

            if (calcCount == 0)
                return "이미 계산이 완료되었습니다.";
            else
                return "계산 완료하였습니다.";
        }
    }
}
