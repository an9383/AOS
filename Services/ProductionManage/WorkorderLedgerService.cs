using DevExpress.XtraSpreadsheet.Commands;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.ProductionManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.ProductionManage
{
    public class WorkorderLedgerService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal List<Item> SelectItem(string item_cd, string item_nm)
        {
            string sSpName = "CODEHELP";
            string strsql = "SELECT item_cd, item_nm FROM v_item_makingstandard WHERE ("
                + "item_cd  LIKE '%" + item_cd + "%' OR "
                + "item_nm LIKE '%" + item_nm + "%') AND "
                + "item_cd  LIKE '%%' AND item_nm  LIKE '%%' ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, strsql);

            List<Item> items = new List<Item>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                Item item = new Item(row);

                items.Add(item);
            }

            return items;
        }

        internal DataTable SelectAPS(string pGubun, string pSearchDate, string pSearchEndDate, string pChkVisible)
        {
            string sSpName = "SP_WorkorderLedger";
            string[] pParam = new string[4];
            pParam[0] = "@gubun:" + pGubun;
            pParam[1] = "@SearchDate:" + DateTime.ParseExact(pSearchDate, "yyyy-MM-dd", null).ToString("yyyyMM");
            pParam[2] = "@SearchEndDate:" + DateTime.ParseExact(pSearchEndDate, "yyyy-MM-dd", null).ToString("yyyyMM"); ;
            pParam[3] = "@ck_visible:" + pChkVisible;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectWorkOrderAll(string pGubun, string pSearchDate, string pDeptCd, string pDeptCd_1, string pSignStatus, string pSearchEndDate, string pItemCd)
        {
            string sSpName = "SP_WorkorderLedger";
            string[] pParam = new string[6];
            pParam[0] = "@SearchDate:" + pSearchDate;
            pParam[1] = "@MakingDeptCd:" + pDeptCd;
            pParam[2] = "@MakingDeptCd2:" + pDeptCd_1;
            pParam[3] = "@sign_status_s:" + pSignStatus;
            pParam[4] = "@SearchEndDate:" + pSearchEndDate;
            pParam[5] = "@ItemCd:" + pItemCd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectWorkOrderDetail(string pSpName, string pGubun, string pItemCd)
        {
            string sSpName = pSpName;
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + pItemCd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectWorkOrderDetail(string order_id, string item_cd)
        {
            string sSpName = "SP_WorkorderLedger";
            string[] pParam = new string[2];
            pParam[0] = "@OrderId:" + order_id;
            pParam[1] = "@ItemCd:" + item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SelectWorkOrderDetail", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectWorkOrderElectricSign(string mp_ck, string order_id, string sign_set_cd)
        {
            string sSpName = "SP_WorkorderLedger";
            string[] pParam = new string[3];
            pParam[0] = "@mp_ck:" + mp_ck;
            pParam[1] = "@OrderId:" + order_id;
            pParam[2] = "@sign_set_cd:" + sign_set_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "GetElectricSignList", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();
            table.Columns.Add("sign_set_nm", typeof(String));
            table.Columns.Add("sign_set_dt_id", typeof(String));
            table.Columns.Add("displayfield", typeof(String));
            table.Columns.Add("responsible_emp_cd", typeof(String));
            table.Columns.Add("responsible_emp_nm", typeof(String));
            table.Columns.Add("responsible_authority", typeof(String));
            table.Columns.Add("sign_set_dt_seq", typeof(String));
            table.Columns.Add("sign_emp_cd", typeof(String));
            table.Columns.Add("sign_time", typeof(String));
            table.Columns.Add("sign_yn", typeof(String));
            table.Columns.Add("sign_emp_nm", typeof(String));
            table.Columns.Add("sign_image", typeof(String));
            table.Columns.Add("prev_sign_yn", typeof(String));
            table.Columns.Add("next_sign_yn", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["sign_set_nm"] = row["sign_set_nm"].ToString();
                _row["sign_set_dt_id"] = row["sign_set_dt_id"].ToString();
                _row["displayfield"] = row["displayfield"].ToString();
                _row["sign_yn"] = row["sign_yn"].ToString();
                _row["sign_time"] = row["sign_time"].ToString();
                _row["sign_emp_cd"] = row["sign_emp_cd"].ToString();
                _row["responsible_emp_cd"] = row["responsible_emp_cd"].ToString();
                _row["responsible_authority"] = row["responsible_authority"].ToString();

                if (row["sign_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["sign_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["sign_image"] = url;
                }
                else
                {
                    string url = "/Content/image/defaultSign.png";
                    _row["sign_image"] = url;
                }
                _row["responsible_emp_nm"] = row["responsible_emp_nm"].ToString();
                _row["sign_emp_nm"] = row["sign_emp_nm"].ToString();
                _row["prev_sign_yn"] = row["prev_sign_yn"].ToString();
                _row["next_sign_yn"] = row["next_sign_yn"].ToString();
                _row["sign_set_dt_seq"] = row["sign_set_dt_seq"].ToString();

                table.Rows.Add(_row);
            }

            return table;
        }

        internal string DeleteByOrderId(string order_id)
        {
            string sSpName = "SP_WorkorderLedger";
            string[] pParam = new string[1];
            pParam[0] = "@OrderId:" + order_id;

            string res = _bllSpExecute.SpExecuteString(sSpName, "DeleteByOrderId", pParam);

            return res;
        }

        internal string SignCRUD(string gubun, string order_id, string empCd, string validation_type, string sign_gubun, int sign_status)
        {
            string res = "";

            if (gubun.Equals("UpdateSignature"))
            {
                string sSpName = "SP_WorkorderLedger";
                string[] pParam = new string[4];
                pParam[0] = "@OrderId:" + order_id;
                pParam[1] = "@EmpCd:" + empCd;
                pParam[2] = "@validation_type:" + validation_type;
                pParam[3] = "@SignGubun:" + sign_gubun;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                UpdateSignStatus(sign_status + 1, order_id);

                if (sign_status == 2)
                {
                    UpdateWorkorderStatus("R", order_id);
                }
            }
            else if (gubun.Equals("DeleteSignature"))
            {
                if (!checkStatus(order_id))
                {
                    return "이미 불출지시가 된 제조지시는 삭제할 수 없습니다.";
                }

                string sSpName = "SP_WorkorderLedger";
                string[] pParam = new string[2];
                pParam[0] = "@OrderId:" + order_id;
                pParam[1] = "@SignGubun:" + sign_gubun;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                UpdateSignStatus(sign_status - 1, order_id);

                if (sign_status == 3)
                {
                    UpdateWorkorderStatus("W", order_id);
                }
            }

            return res;
        }

        private bool checkStatus(string order_id)
        {
            string sSpName = "SP_WorkorderLedger";
            string[] pParam = new string[1];
            pParam[0] = "@OrderId:" + order_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "CheckStatus", pParam);

            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void UpdateWorkorderStatus(string order_status, string order_id)
        {
            string sSpName = "SP_WorkorderLedger";
            string[] pParam = new string[2];
            pParam[0] = "@order_status:" + order_status;
            pParam[1] = "@OrderId:" + order_id;

            _bllSpExecute.SpExecuteString(sSpName, "UpdateWorkorderStatus", pParam);
        }

        private void UpdateSignStatus(int sign_status, string order_id)
        {
            string sSpName = "SP_WorkorderLedger";
            string[] pParam = new string[2];
            pParam[0] = "@sign_status_s:" + sign_status;
            pParam[1] = "@OrderId:" + order_id;

            _bllSpExecute.SpExecuteString(sSpName, "UpdateSignStatus", pParam);
        }

        internal string GetSignEmpCd(string c_seq, string emp_cd, string sign_set_cd)
        {
            string sSpName = "SP_WorkorderLedger";
            string[] pParam = new string[3];
            pParam[0] = "@c_seq:" + c_seq;
            pParam[1] = "@emp_cd:" + emp_cd;
            pParam[2] = "@sign_set_cd:" + sign_set_cd;

            string empCd = _bllSpExecute.SpExecuteString(sSpName, "SR", pParam);

            return empCd;
        }

        internal string WorkOrderLedgerCRUD(List<WorkorderLedger> workOrders, string order_id)
        {
            string sSpName = "SP_WorkorderLedger";

            string gubun = "";

            string res = "";

            foreach (WorkorderLedger workorderLedger in workOrders)
            {
                if (!workorderLedger.gubun.Equals("D"))
                {
                    if (workorderLedger.gubun.Equals("I"))
                    {
                        workorderLedger.order_no = "";
                    }

                    int cnt = IsSameLotNo(workorderLedger, workorderLedger.gubun);

                    if (cnt > 0)
                    {
                        // lot 번호 중복
                        res = "중복된 lot 번호가 있습니다.";
                        return res;
                    }
                }

                workorderLedger.order_id = order_id;

                if (workorderLedger.gubun.Equals("I"))
                {
                    gubun = "Insert";
                }
                else if (workorderLedger.gubun.Equals("U"))
                {
                    gubun = "Update";
                }

                if (workorderLedger.gubun.Equals("D"))
                {
                    string[] param = new string[1];

                    param[0] = "@OrderNo:" + workorderLedger.order_no;

                    DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "input_order_status", param);

                    DataTable dt = new DataTable();

                    if (ds != null)
                    {
                        dt = ds.Tables[0];
                    }

                    if (dt.Rows.Count > 0)
                    {
                        return "이미 불출지시가 된 제조지시는 삭제할 수 없습니다.";
                    }

                    param = new string[4];

                    param[0] = "@order_request_no:" + workorderLedger.order_request_no;
                    param[1] = "@item_cd:" + workorderLedger.item_cd;
                    param[2] = "@lot_no:" + workorderLedger.lot_no;
                    param[3] = "@OrderNo:" + workorderLedger.order_no;

                    res = _bllSpExecute.SpExecuteString(sSpName, "DeleteAPS", param);

                    param = new string[1];

                    param[0] = "@OrderNo:" + workorderLedger.order_no;

                    res = _bllSpExecute.SpExecuteString(sSpName, "DeleteByOrderNo", param);

                    if (!String.IsNullOrEmpty(res))
                    {
                        if (!String.IsNullOrEmpty(DeleteByOrderId(order_id))){
                            res = "제조지시가 삭제되었습니다.";
                        }
                    }
                }
                else
                {
                    string[] param = getParam(workorderLedger);

                    res = _bllSpExecute.SpExecuteString(sSpName, gubun + "ToWorkOrder", param);

                }

            }

            return res;
        }

        private string[] getParam(WorkorderLedger workorderLedger)
        {
            string[] param = new string[15];

            param[0] = "@SubLot_ck:" + "N";
            param[1] = "@ItemCd:" + workorderLedger.item_cd;
            param[2] = "@LotNo:" + workorderLedger.lot_no;
            param[3] = "@OrderQty:" + workorderLedger.order_qty;
            param[4] = "@OrderBigo:" + workorderLedger.order_bigo;
            param[5] = "@OrderNo:"+ workorderLedger.order_no;
            param[6] = "@OrderId:" + workorderLedger.order_id;
            param[7] = "@OrderDate:" + workorderLedger.order_date;
            param[8] = "@MakingDeptCd2:" + "1";
            param[9] = "@BatchSize:" + workorderLedger.order_qty;
            param[10] = "@PB_date:" + workorderLedger.PB_date;
            param[11] = "@lot_date:" + workorderLedger.lot_date;
            param[12] = "@valid_date:" + workorderLedger.valid_date;
            param[13] = "@revision_no:" + workorderLedger.revision_no;
            param[14] = "@order_request_no:" + workorderLedger.order_request_no;

            return param;
        }

        internal string GetOrderId(WorkorderLedger workorderLedger, string pGubun)
        {
            string sSpName = "SP_WorkorderLedger";

            string gubun = "";

            string res = "";

            if (pGubun.Equals("I"))
            {
                gubun = "Insert";

                string[] pParam = new string[3];
                pParam[0] = "@OrderDate:" + workorderLedger.order_date;
                pParam[1] = "@MakingDeptCd:" + workorderLedger.making_dept_cd;
                pParam[2] = "@MakingDeptCd2:" + "1";

                res = _bllSpExecute.SpExecuteString(sSpName, gubun + "ToWorkOrderLedger", pParam);

            }
            else if (pGubun.Equals("U"))
            {
                gubun = "Update";

                string[] pParam = new string[4];
                pParam[0] = "@OrderDate:" + workorderLedger.order_date;
                pParam[1] = "@MakingDeptCd:" + workorderLedger.making_dept_cd;
                pParam[2] = "@MakingDeptCd2:" + "1";
                pParam[3] = "@OrderId:" + workorderLedger.order_id;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun + "ToWorkOrderLedger", pParam);
            }

            return res;
        }

        internal int IsSameLotNo(WorkorderLedger workorderLedger, string gubun)
        {

            string sSpName = "SP_WorkorderLedger";
            string[] pParam = new string[4];
            pParam[0] = "@MakingDeptCd:" + workorderLedger.making_dept_cd;
            pParam[1] = "@OrderNo:" + workorderLedger.order_no;
            pParam[2] = "@ItemCd:" + workorderLedger.item_cd;
            pParam[3] = "@LotNo:" + workorderLedger.lot_no;

            if (gubun.Equals("I"))
            {
                pParam[0] = "@MakingDeptCd:";
                pParam[1] = "@OrderNo:";
            }

            string res = _bllSpExecute.SpExecuteString(sSpName, "IsSameLotNo", pParam);

            return Int32.Parse(res);
        }

        internal DataTable SelectPopupItem(string pGubun, string pItemCd, string pDeptCd, string pBatchSize, string pType)
        {
            string sSpName = "SP_CodeHelpOrder_Item";
            string[] pParam = new string[2];
            pParam[0] = "@dept_cd:" + "0";
            pParam[1] = "@type:" + "6";

            // pParam[0] = "@s_item_cd:" + pItemCd;
            //pParam[1] = "@dept_cd:" + "0";
            //pParam[2] = "@batch_size:" + pBatchSize;
            //pParam[3] = "@type:" + "6";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectPopupItemDetail(string pGubun, string pItemCd, string pRevisionNo, string pOrderCnt, string pBatchSize)
        {
            pBatchSize = (pBatchSize == null) ? "1" : pBatchSize;

            string sSpName = "SP_CodeHelpOrder_Item";
            string[] pParam = new string[4];
            pParam[0] = "@item_cd:" + pItemCd;
            pParam[1] = "@revision_no:" + pRevisionNo;
            pParam[2] = "@order_cnt:" + pOrderCnt;
            pParam[3] = "@batch_size:" + pBatchSize;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string SelectValidDate(string pGubun, string pItemCd, string pOrderDate)
        {
            string sSpName = "SP_WorkorderLedger";
            string[] pParam = new string[2];
            pParam[0] = "@ItemCd:" + pItemCd;
            pParam[1] = "@OrderDate:" + pOrderDate;

            string validDate = "";
            validDate = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return validDate;
        }

        internal DataTable GetProgramSet()
        {
            string sSpName = "SP_ProgramInit";
            string[] pParam = new string[1];
            pParam[0] = "@code:" + Public_Function.program_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

    }
}
