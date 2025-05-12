using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.ProductionManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class OrderProcResultService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable GetProcessName(string sdate, string edate, string order_no)
        {
            string sSpName = "SP_OrderProcResult";
            string[] param = new string[3];
            param[0] = "@sdate:" + sdate;
            param[1] = "@edate:" + edate;
            param[2] = "@order_no:" + order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SP", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectOrderProcResultData(string process_cd, string sdate, string edate, string order_no, string item_cd, string order_status, string lot_no, string trade_cd3, string outsource_YN)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[9];
            pParam[0] = "@process_cd:" + process_cd;
            pParam[1] = "@sdate:" + sdate;
            pParam[2] = "@edate:" + edate;
            pParam[3] = "@order_no:" + order_no;
            pParam[4] = "@item_cd:" + item_cd;
            pParam[5] = "@status_yn:" + order_status;
            pParam[6] = "@lot_no:" + lot_no;
            pParam[7] = "@trade_cd3:" + trade_cd3;
            pParam[8] = "@outsource_YN:" + outsource_YN;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SO", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal List<string> SelectOrderProcResultDetail(string order_no, string order_proc_id, string m_order_no, string process_cd)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[4];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@order_proc_id:" + order_proc_id;
            pParam[2] = "@m_order_no:" + m_order_no;
            pParam[3] = "@process_cd:" + process_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SR", pParam);

            if (ds == null)
            {
                return null;
            }

            List<string> jsonList = new List<string>();

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                string jsonStr = Public_Function.DataTableToJSON(ds.Tables[i]);

                if (jsonStr.Length <= 0)
                {
                    jsonList.Add("{ \"data\" : \"empty\" }");
                }
                else
                {
                    jsonList.Add(Public_Function.DataTableToJSON(ds.Tables[i]));
                }
            }

            return jsonList;
        }

        internal List<string> OrderProcResultCheckLock(OrderProcResult orderProcResult)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[3];
            pParam[0] = "@order_no:" + orderProcResult.order_no;
            pParam[1] = "@process_cd:" + orderProcResult.process_cd;
            pParam[2] = "@m_order_no:" + orderProcResult.m_order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "lock", pParam);

            DataTable dt = new DataTable();

            if (ds == null)
            {
                return null;
            }

            List<string> jsonList = new List<string>();

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                string jsonStr = Public_Function.DataTableToJSON(ds.Tables[i]);

                if (jsonStr.Length <= 0)
                {
                    jsonList.Add("{ \"data\" : \"empty\" }");
                }
                else
                {
                    jsonList.Add(Public_Function.DataTableToJSON(ds.Tables[i]));
                }
            }

            return jsonList;
        }

        internal string OrderProcResultCheck(OrderProcResult orderProcResult)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[2];
            pParam[0] = "@m_order_no:" + orderProcResult.m_order_no;
            pParam[1] = "@process_cd:" + orderProcResult.process_cd;

            string res = _bllSpExecute.SpExecuteString(sSpName, "check", pParam);

            return res;
        }

        internal string OrderProcResultGetMaterialStatus(string order_no, string order_proc_id)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[2];
            pParam[0] = "@m_order_no:" + order_no;
            pParam[1] = "@order_proc_id:" + order_proc_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "material_status", pParam);

            string res = dt.Rows[0].ItemArray[0].ToString();
            //string res = _bllSpExecute.SpExecuteString(sSpName, "material_status", pParam);

            return res;
        }
        internal string OrderProcResultGetReceiveFieldUseYN()
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[1];
            pParam[0] = "@m_order_no:" + "";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "CRU", pParam);

            string res = dt.Rows[0].ItemArray[0].ToString();

            return res;
        }

        internal DataTable OrderProcResultGetPackList_AddPack(string item_cd, string test_no)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[2];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@test_no:" + test_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "GPL_AP", pParam);

            return dt;
        }

        internal DataTable OrderProcResultGetPackList_ReturnPack(string packing_order_no, string process_cd)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[2];
            pParam[0] = "@m_order_no:" + packing_order_no;
            pParam[1] = "@process_cd:" + process_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "GPL_RP", pParam);

            return dt;
        }

        internal string OrderProcResultAddPackCRUD(List<OrderProcResultAddPack> orderProcResultAddPack, string packing_order_no, string packing_order_id, string issue_emp_cd)
        {
            string sSpName = "SP_OrderProcResult";

            string res = "";

            foreach (OrderProcResultAddPack tmp in orderProcResultAddPack)
            {
                string[] param = new string[5];
                param[0] = "@packing_order_no:" + packing_order_no;
                param[1] = "@packing_order_id:" + packing_order_id;
                param[2] = "@barcode_no:" + tmp.pack_barcode;
                param[3] = "@use_qty:" + tmp.add_qty;
                param[4] = "@update_user_cd:" + issue_emp_cd;

                res = _bllSpExecute.SpExecuteString(sSpName, "UA", param);
            }

            return res;
        }

        internal string OrderProcResultReturnPackCRUD(List<OrderProcResultAddPack> orderProcResultAddPack, string packing_order_no, string issue_emp_cd)
        {
            string sSpName = "SP_OrderProcResult";

            string res = "";

            foreach (OrderProcResultAddPack tmp in orderProcResultAddPack)
            {
                string[] param = new string[5];
                param[0] = "@packing_order_no:" + packing_order_no;
                param[1] = "@packing_order_id:" + tmp.packing_order_id;
                param[2] = "@barcode_no:" + tmp.pack_barcode;
                param[3] = "@return_qty:" + tmp.return_qty;
                param[4] = "@update_user_cd:" + issue_emp_cd;

                res = _bllSpExecute.SpExecuteString(sSpName, "UR", param);
            }

            return res;
        }

        internal string OrderProcResultReturnEmpSign_Update(string packing_order_no, string process_cd, string issue_emp_cd)
        {
            string sSpName = "SP_OrderProcResult";

            string[] param = new string[3];
            param[0] = "@packing_order_no:" + packing_order_no;
            param[1] = "@process_cd:" + process_cd;
            param[2] = "@update_user_cd:" + issue_emp_cd;

            string res = _bllSpExecute.SpExecuteString(sSpName, "URS", param);


            return res;
        }

        internal DataTable OrderProcResultPRCheck(OrderProcResult orderProcResult)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[2];
            pParam[0] = "@m_order_no:" + orderProcResult.m_order_no;
            pParam[1] = "@process_cd:" + orderProcResult.process_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "PR_check", pParam);

            return dt;
        }

        internal string OrderProcResultSelectSign(string item_cd, string m_order_no, string process_cd, string gubun)
        {
            string sSpName = "SP_OrderProcResult";

            if (gubun.Equals("SelectSign_P"))
            {
                string[] pParam = new string[2];
                pParam[0] = "@item_cd:" + item_cd;
                pParam[1] = "@m_order_no:" + m_order_no;

                string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                return res;
            }
            else if (gubun.Equals("SelectSign"))
            {
                string[] pParam = new string[3];
                pParam[0] = "@item_cd:" + item_cd;
                pParam[1] = "@order_no:" + m_order_no;
                pParam[2] = "@process_cd:" + process_cd;

                string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                return res;
            }

            return "";
        }

        internal string OrderProcResultDeleteTest(string order_no1, string order_proc_id1, string testrequest_no, string m_order_no, string packing_result_id, string gubun)
        {
            if (gubun.Equals("DE_P") || gubun.Equals("DE"))
            {
                string sSpName = "SP_OrderProcResult";

                string[] pParam = new string[3];
                pParam[0] = "@order_no:" + order_no1;
                pParam[1] = "@order_proc_id:" + order_proc_id1;
                pParam[2] = "@testrequest_no:" + testrequest_no;

                string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                return res;
            }else if (gubun.Equals("MR_delete"))
            {
                string sSpName = "SP_OrderProcResult";

                string[] pParam = new string[1];
                pParam[0] = "@m_order_no:" + m_order_no;

                string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                return res;
            }else if (gubun.Equals("PR2D"))
            {
                string sSpName = "SP_OrderProcResult";

                string[] pParam = new string[1];
                pParam[0] = "@packing_result_id:" + packing_result_id;

                string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                return res;
            }

            return "";
        }

        internal DataTable OrderProcResultDeleteCheck(string packing_result_id)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[1];
            pParam[0] = "@packing_result_id:" + packing_result_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "DeleteCheck", pParam);

            return dt;
        }

        internal string OrderProcResultInsert1(OrderProcResultForInsert1 orderProcResult, string gubun, string update_check)
        {
            string sSpName = "SP_OrderProcResult";

            string[] param = new string[19];
            param[0] = "@order_no:" + orderProcResult.order_no;
            param[1] = "@order_proc_id:" + orderProcResult.order_proc_id;
            param[2] = "@order_qty:" + orderProcResult.order_qty;
            param[3] = "@order_ea_qty:" + orderProcResult.order_ea_qty;
            param[4] = "@order_proc_real_qty:" + orderProcResult.order_proc_real_qty;
            param[5] = "@order_proc_real_ea_qty:" + orderProcResult.order_proc_real_ea_qty;
            param[6] = "@order_proc_sample_qty:" + orderProcResult.order_proc_sample_qty;
            param[7] = "@order_proc_sample_ea_qty:" + orderProcResult.order_proc_sample_ea_qty;
            param[8] = "@order_proc_content:" + orderProcResult.order_proc_content;
            param[9] = "@receive_date:" + orderProcResult.receive_date;
            param[10] = "@receive_time:" + orderProcResult.receive_time;
            param[11] = "@order_enter_date:" + orderProcResult.order_enter_date;
            param[12] = "@enter_time:" + orderProcResult.enter_time;
            param[13] = "@order_proc_rate:" + orderProcResult.order_proc_rate;
            param[14] = "@m_order_no:" + orderProcResult.m_order_no;
            param[15] = "@test_deposit_qty:" + orderProcResult.test_deposit_qty;
            param[16] = "@customer_sample_qty:" + orderProcResult.customer_sample_qty;
            param[17] = "@rate_type:" + orderProcResult.rate_type;
            param[18] = "@update_check:" + update_check;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

        internal string OrderProcResultSQ(string order_no, string process_cd)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[2];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@process_cd:" + process_cd;

            string res = _bllSpExecute.SpExecuteString(sSpName, "SQ", pParam);

            return res;
        }

        internal string OrderProcResultSave(string order_no, string gubun)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + order_no;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }

        internal string OrderProcResultUQ(OrderProcResultForInsert2 procResultForInsert2, string gubun)
        {
            string sSpName = "SP_OrderProcResult";

            string res = "";

            if (gubun.Equals("UQ2"))
            {
                string[] param = new string[15];
                param[0] = "@m_order_no:" + procResultForInsert2.m_order_no;
                param[1] = "@item_cd:" + procResultForInsert2.item_cd;
                param[2] = "@qcquest_date:" + procResultForInsert2.request_date;
                param[3] = "@qcquest_end_date:" + procResultForInsert2.result_hope_date;
                param[4] = "@item_form_cd:" + procResultForInsert2.item_form_cd;
                param[5] = "@order_proc_id:" + procResultForInsert2.order_proc_id;
                param[6] = "@worker:" + HttpContext.Current.Session["loginCD"].ToString();
                param[7] = "@process_cd:" + procResultForInsert2.process_cd;
                param[8] = "@order_no:" + procResultForInsert2.order_no;
                param[9] = "@order_proc_qt_status:" + procResultForInsert2.order_proc_qt_status;
                param[10] = "@order_proc_transfer_status:" + procResultForInsert2.order_proc_transfer_status;
                param[11] = "@mp_ck:" + procResultForInsert2.mp_ck;
                param[12] = "@testrequest_no:" + procResultForInsert2.test_no;
                param[13] = "@request_no:" + procResultForInsert2.request_no;
                param[14] = "@request_time1:" + string.Format("{0} {1}", procResultForInsert2.request_time, DateTime.Now.ToString("HH:mm:ss.fff"));

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            }else if (gubun.Equals("UQ"))
            {
                string[] param = new string[13];
                param[0] = "@worker:" + HttpContext.Current.Session["loginCD"].ToString();
                param[1] = "@process_cd:" + procResultForInsert2.process_cd;
                param[2] = "@order_no:" + procResultForInsert2.order_no;
                param[3] = "@order_proc_id:" + procResultForInsert2.order_proc_id;
                param[4] = "@order_proc_qt_status:" + procResultForInsert2.order_proc_qt_status;
                param[5] = "@order_proc_transfer_status:" + procResultForInsert2.order_proc_transfer_status;
                param[6] = "@item_cd:" + procResultForInsert2.item_cd;
                param[7] = "@qcquest_date:" + procResultForInsert2.request_date;
                param[8] = "@qcquest_end_date:" + procResultForInsert2.result_hope_date;
                param[9] = "@item_form_cd:" + procResultForInsert2.item_form_cd;
                param[10] = "@mp_ck:" + procResultForInsert2.mp_ck;
                param[11] = "@testrequest_no:" + procResultForInsert2.test_no;
                param[12] = "@request_time1:" + string.Format("{0} {1}", procResultForInsert2.request_date, DateTime.Now.ToString("HH:mm:ss.fff"));

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            }else if (gubun.Equals("UQ3"))
            {
                string[] param = new string[16];
                param[0] = "@m_order_no:" + procResultForInsert2.m_order_no;
                param[1] = "@item_cd:" + procResultForInsert2.item_cd;
                param[2] = "@qcquest_date:" + DateTime.Now.ToShortDateString();
                param[3] = "@qcquest_end_date:" + DateTime.Now.ToShortDateString();
                param[4] = "@item_form_cd:" + procResultForInsert2.item_form_cd;
                param[5] = "@order_proc_id:" + procResultForInsert2.order_proc_id;
                param[6] = "@worker:" + procResultForInsert2.worker;
                param[7] = "@process_cd:" + procResultForInsert2.process_cd;
                param[8] = "@order_no:" + procResultForInsert2.order_no;
                param[9] = "@order_proc_qt_status:" + procResultForInsert2.order_proc_qt_status;
                param[10] = "@order_proc_transfer_status:" + procResultForInsert2.order_proc_transfer_status;
                param[11] = "@mp_ck:" + procResultForInsert2.mp_ck;
                param[12] = "@testrequest_no:" + procResultForInsert2.test_no;
                param[13] = "@pack_cnt:" + procResultForInsert2.pack_cnt;
                param[14] = "@request_no:" + procResultForInsert2.request_no;
                param[15] = "@request_time1:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }

            return res;
        }

        internal string OrderProcResultWorkerTRX(List<OrderProcResultWorkerEquipment> orderProcResultWorkerEquipment)
        {
            string sSpName = "SP_OrderProcResult";

            string res = "";

            foreach (OrderProcResultWorkerEquipment tmp in orderProcResultWorkerEquipment)
            {
                if (tmp.gubun.Equals("I") || tmp.gubun.Equals("U"))
                {
                    string[] param = new string[17];
                    param[0] = "@order_no:" + tmp.order_no;
                    param[1] = "@order_proc_id:" + tmp.order_proc_id;
                    param[2] = "@ew_flag:" + tmp.ew_flag;
                    param[3] = "@result_seq:" + tmp.result_seq;
                    param[4] = "@order_proc_sdate:" + tmp.order_proc_sdate;
                    param[5] = "@order_proc_edate:" + tmp.order_proc_edate;
                    param[6] = "@total_time:" + tmp.total_time;
                    param[7] = "@normal_time:" + tmp.normal_time;
                    param[8] = "@added_time:" + tmp.added_time;
                    param[9] = "@night_time:" + tmp.night_time;
                    param[10] = "@special_time:" + tmp.special_time;
                    param[11] = "@ew_cd:" + tmp.ew_cd;
                    param[12] = "@ew_nm:" + tmp.ew_nm;
                    param[13] = "@ew_enm:" + tmp.ew_enm;
                    param[14] = "@workroom_cd:" + tmp.workroom_cd;
                    param[15] = "@order_gubun:" + tmp.order_gubun;
                    param[16] = "@worker_cnt:" + tmp.worker_cnt;

                    res = _bllSpExecute.SpExecuteString(sSpName, "IR", param);
                }
                else if (tmp.gubun.Equals("D"))
                {
                    string[] param = new string[4];
                    param[0] = "@order_no:" + tmp.order_no;
                    param[1] = "@order_proc_id:" + tmp.order_proc_id;
                    param[2] = "@ew_flag:" + tmp.ew_flag;
                    param[3] = "@result_seq:" + tmp.result_seq;

                    res = _bllSpExecute.SpExecuteString(sSpName, "DR", param);
                }

            }

            return res;
        }

        internal DataTable OrderProcResultCalcPackingUsage(string m_order_no, string packing_result, string process_cd)
        {
            string sSpName = "SP_OrderProcResult";

            string[] pParam = new string[3];
            pParam[0] = "@m_order_no:" + m_order_no;
            pParam[1] = "@packing_result:" + packing_result;
            pParam[2] = "@process_cd:" + process_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "PR", pParam);

            return dt;
        }

        internal string OrderProcResultMaterialTRX(List<OrderProcResultMaterial> paramData)
        {
            string sSpName = "SP_OrderProcResult";
            string res = "";

            foreach (OrderProcResultMaterial tmp in paramData)
            {
                string[] param = new string[5];
                param[0] = "@use_qty:" + tmp.use_qty;
                param[1] = "@disuse_qty:" + tmp.disuse_qty;
                param[2] = "@return_qty:" + tmp.return_qty;
                param[3] = "@return_issue_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
                param[4] = "@packing_order_id:" + tmp.packing_order_id;

                res = _bllSpExecute.SpExecuteString(sSpName, "PRI", param);
            }
            return res;
        }

        internal string OrderProcResultSelectMaterialStatus(string order_no)
        {
            string sSpName = "SP_OrderProcResult";

            string[] param = new string[1];
            param[0] = "@m_order_no:" + order_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "material_status", param);

            return dt.Rows[0].ItemArray[0].ToString();
        }
    }
}
