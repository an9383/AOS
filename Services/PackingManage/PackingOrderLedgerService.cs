
using HACCP.Libs.Database;
using HACCP.Models.PackingManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PackingManage
{
    public class PackingOrderLedgerService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectPackingOrder(PackingOrderLedger.SrchParam packingOrder)
        {
            string sSpName = "SP_PackingOrderLedger";
            string[] pParam = new string[6];
            pParam[0] = "@SearchDate:" + packingOrder.SearchDate;
            pParam[1] = "@MakingDeptCd:" + packingOrder.MakingDeptCd;
            pParam[2] = "@MakingDeptCd2:" + packingOrder.MakingDeptCd2;
            pParam[3] = "@sign_status_s:" + packingOrder.sign_status_s;
            pParam[4] = "@SearchEndDate:" + packingOrder.SearchEndDate;
            pParam[5] = "@ItemCd:" + packingOrder.ItemCd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SelectPackingOrderLedgerAll", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectPackingOrderDetail(string order_id, string item_cd)
        {
            string sSpName = "SP_PackingOrderLedger";
            string[] pParam = new string[2];
            pParam[0] = "@OrderId:" + order_id;
            pParam[1] = "@ItemCd:" + item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SelectPackingOrderDetail", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectPackingOrderElectricSign(string mp_ck, string order_id, string sign_set_cd)
        {
            string sSpName = "SP_PackingOrderLedger";
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
                    _row["sign_image"] = "/Content/image/defaultSign.png";
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


        internal DataTable SelectItemPopup(string item_cd, string dept_cd, string type, string batch_size)
        {
            string sSpName = "SP_CodeHelpOrder_Item";
            string[] pParam = new string[4];
            pParam[0] = "@s_item_cd:" + item_cd;
            pParam[1] = "@dept_cd:" + dept_cd;
            pParam[2] = "@type:" + type;
            pParam[3] = "@batch_size:" + batch_size;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SP", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }



        internal DataTable SelectItemDetail(string item_cd, string revision_no, string order_cnt, string batch_size)
        {
            string sSpName = "SP_CodeHelpOrder_Item";
            string[] pParam = new string[4];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@order_cnt:" + order_cnt;
            pParam[3] = "@batch_size:" + batch_size;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SP2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectOrderPopup(string item_cd, string sdate, string edate)
        {
            string sSpName = "CodeHelp";
            string strsql = " SELECT   "
                + "  a.order_no  "
                + "  , a.lot_no  "
                + "  , a.item_cd  "
                + "  , b.item_nm  "
                + "  , a.order_date  "
                + "  , d.common_part_nm AS order_status_nm  "
                + "  , e.common_part_nm AS dept_nm  "
                + "  , a.lot_date  "
                + "  , a.pb_date  "
                + "  , a.order_qty as order_qty  "
                + "  , count(f.order_no) as cnt   "
                + " FROM   "
                + "  work_order a            "
                + "  INNER JOIN item_license B  "
                + "  ON A.item_cd = B.item_cd   "
                + "  AND a.revision_id = b.item_license_no           "
                + "  LEFT OUTER JOIN item_process_revision c   "
                + "  ON a.item_cd = c.item_cd   "
                + "  AND a.revision_no = c.revision_no   "
                + "  AND c.mp_ck = 'M'  "
                + "  AND a.sys_plant_cd = c.sys_plant_cd   "
                + "  LEFT OUTER JOIN common d   "
                + "  ON d.common_cd = 'RT005'  "
                + "   AND a.order_status = d.common_part_cd   "
                + "   AND a.sys_plant_cd = d.sys_plant_cd    "
                + "   LEFT OUTER JOIN common e   "
                + "   ON e.common_cd = 'MD001'   "
                + "   AND c.making_dept_cd = e.common_part_cd   "
                + "   AND a.sys_plant_cd = e.sys_plant_cd    "
                + "   LEFT OUTER JOIN packing_order f   "
                + "   ON a.order_no = f.order_no   "
                + "   AND a.sys_plant_cd = f.sys_plant_cd              "
                + "   LEFT OUTER JOIN item_standard g   "
                + "   ON a.item_cd = g.item_make_cd   "
                + "   and g.item_cd = '"+ item_cd + "'   "
                + "   WHERE  a.order_status in ('1','2','3','4','5','6','11','12')    "
                + "   AND a.order_date between '"+ sdate + "' and '"+edate+"'   "
                + "   AND a.sys_plant_cd = 'PC001'   "
                + "   AND a.item_cd = (   "
                + "    SELECT isnull(nullif(item_make_cd,''),item_cd) as item_make_cd  "
                + "    FROM item_standard  "
                + "    WHERE item_cd = '"+ item_cd+"'  )   "
                + "   AND ( a.lot_no like '%%')    "
                + "   AND (a.input_manufacture <> 'Y' or a.input_manufacture is null)    "
                + "   GROUP BY   "
                + "   a.order_no  "
                + "   , a.lot_no  "
                + "   , a.item_cd  "
                + "   , b.item_nm  "
                + "   , a.order_date  "
                + "   , d.common_part_nm  "
                + "   , e.common_part_nm  "
                + "   , a.lot_date  "
                + "   , a.pb_date  "
                + "   , a.order_qty   "
                + "   , g.item_pack_size  "
                + "   , b.item_unit  "
                + "   , g.item_packunit  ";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, strsql);

            return dt;
        }

        internal string[] GetValidDate(string item_cd, string lot_no)
        {
            string sSpName = "SP_PackingOrderLedger";
            string[] pParam = new string[2];
            pParam[0] = "@ItemCd:" + item_cd;
            pParam[1] = "@lot_no:" + lot_no;

            string[] tmpArr = new string[2];

            tmpArr[0] = _bllSpExecute.SpExecuteString(sSpName, "SelectStartDate", pParam);
            tmpArr[1] = _bllSpExecute.SpExecuteString(sSpName, "SelectValidDate", pParam);

            return tmpArr;
        }

        internal string GetSignEmpCd(string c_seq, string emp_cd, string sign_set_cd)
        {
            string sSpName = "SP_PackingOrderLedger";
            string[] pParam = new string[3];
            pParam[0] = "@c_seq:" + c_seq;
            pParam[1] = "@EmpCd:" + emp_cd;
            pParam[2] = "@sign_set_cd:" + sign_set_cd;

            string empCd = _bllSpExecute.SpExecuteString(sSpName, "SR", pParam);

            return empCd;
        }


        internal string SignCRUD(string gubun, string order_id, string empCd, string validation_type, string sign_gubun, int sign_status)
        {
            string res = "";

            if (gubun.Equals("UpdateSignature"))
            {
                string sSpName = "SP_PackingOrderLedger";
                string[] pParam = new string[4];
                pParam[0] = "@OrderId:" + order_id;
                pParam[1] = "@EmpCd:" + empCd;
                pParam[2] = "@validation_type:" + validation_type;
                pParam[3] = "@SignGubun:" + sign_gubun;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                UpdateSignStatus(sign_status + 1, order_id);

                if (sign_status == 2)
                {
                    UpdatePackingOrderStatus("R", order_id);
                }
            }
            else if (gubun.Equals("DeleteSignature"))
            {
                if (!checkStatus(order_id))
                {
                    return "이미 불출지시가 된 포장지시는 삭제할 수 없습니다.";
                }

                string sSpName = "SP_PackingOrderLedger";
                string[] pParam = new string[4];
                pParam[0] = "@OrderId:" + order_id;
                pParam[1] = "@EmpCd:" + empCd;
                pParam[2] = "@validation_type:" + validation_type;
                pParam[3] = "@SignGubun:" + sign_gubun;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                UpdateSignStatus(sign_status - 1, order_id);

                if (sign_status == 3)
                {
                    UpdatePackingOrderStatus("W", order_id);
                }
            }

            return res;
        }

        private bool checkStatus(string order_id)
        {
            string sSpName = "SP_PackingOrderLedger";
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

        private void UpdatePackingOrderStatus(string order_status, string order_id)
        {
            string sSpName = "SP_PackingOrderLedger";
            string[] pParam = new string[2];
            pParam[0] = "@order_status:" + order_status;
            pParam[1] = "@OrderId:" + order_id;

            _bllSpExecute.SpExecuteString(sSpName, "UpdatePackingorderStatus", pParam);
        }

        private void UpdateSignStatus(int sign_status, string order_id)
        {
            string sSpName = "SP_PackingOrderLedger";
            string[] pParam = new string[2];
            pParam[0] = "@sign_status_s:" + sign_status;
            pParam[1] = "@OrderId:" + order_id;

            _bllSpExecute.SpExecuteString(sSpName, "UpdateSignStatus", pParam);
        }

        internal string GetPackSize(string item_cd)
        {
            string sSpName = "SP_PackingOrderLedger";
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SelectItemPackSize", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt.Rows[0][0].ToString();
        }

        internal string GetPackingOrderId(string order_date, string making_dept_cd, string making_dept_cd2, string order_id)
        {
            string sSpName = "SP_PackingOrderLedger";

            if (String.IsNullOrEmpty(order_id))
            {
                string[] pParam = new string[3];
                pParam[0] = "@OrderDate:" + order_date;
                pParam[1] = "@MakingDeptCd:" + making_dept_cd;
                pParam[2] = "@MakingDeptCd2:" + making_dept_cd2;

                string res = _bllSpExecute.SpExecuteString(sSpName, "InsertToPackingOrderLedger", pParam);

                return res;
            }
            else
            {
                string[] pParam = new string[4];
                pParam[0] = "@OrderDate:" + order_date;
                pParam[1] = "@MakingDeptCd:" + making_dept_cd;
                pParam[2] = "@MakingDeptCd2:" + making_dept_cd2;
                pParam[3] = "@OrderId:" + order_id;

                string res = _bllSpExecute.SpExecuteString(sSpName, "UpdateToPackingOrderLedger", pParam);

                return order_id;
            }
        }

        internal string PackingOrderLedgerCRUD(List<PackingOrderLedger> packingOrders, string order_date, string making_dept_cd2, string order_id)
        {
            string sSpName = "SP_PackingOrderLedger";

            string res = "";

            for (int i = 0; i < packingOrders.Count; i++)
            {

                if (packingOrders[i].gubun.Equals("I"))
                {
                    string[] pParam = new string[14];
                    pParam[0] = "@lot_date:" + packingOrders[i].lot_date;
                    pParam[1] = "@ItemCd:" + packingOrders[i].item_cd;
                    pParam[2] = "@LotNo:" + packingOrders[i].lot_no;
                    pParam[3] = "@OrderQty:" + packingOrders[i].order_qty;
                    pParam[4] = "@OrderBigo:" + packingOrders[i].order_bigo;
                    pParam[5] = "@OrderNo:" + packingOrders[i].order_no;
                    pParam[6] = "@OrderId:" + order_id;
                    pParam[7] = "@OrderDate:" + order_date;
                    pParam[8] = "@MakingDeptCd2:" + making_dept_cd2;
                    pParam[9] = "@BatchSize:" + packingOrders[i].order_batch_size;
                    pParam[10] = "@PB_date:" + packingOrders[i].pb_date;
                    pParam[11] = "@new_ck:" + "N";
                    pParam[12] = "@make_order_no:" + packingOrders[i].order_no_M;
                    pParam[13] = "@Set_Yn:" + "N";

                    res = _bllSpExecute.SpExecuteString(sSpName, "InsertToPackingOrder", pParam);
                }
                else if (packingOrders[i].gubun.Equals("U"))
                {
                    string[] pParam = new string[14];
                    pParam[0] = "@lot_date:" + packingOrders[i].lot_date.Replace("/", "-");
                    pParam[1] = "@ItemCd:" + packingOrders[i].item_cd;
                    pParam[2] = "@LotNo:" + packingOrders[i].lot_no;
                    pParam[3] = "@OrderQty:" + packingOrders[i].order_qty;
                    pParam[4] = "@OrderBigo:" + packingOrders[i].order_bigo;
                    pParam[5] = "@OrderNo:" + packingOrders[i].order_no;
                    pParam[6] = "@OrderId:" + order_id;
                    pParam[7] = "@OrderDate:" + order_date;
                    pParam[8] = "@MakingDeptCd2:" + making_dept_cd2;
                    pParam[9] = "@BatchSize:" + packingOrders[i].order_batch_size;
                    pParam[10] = "@PB_date:" + packingOrders[i].pb_date;
                    pParam[11] = "@new_ck:" + "N";
                    pParam[12] = "@make_order_no:" + packingOrders[i].order_no_M;
                    pParam[13] = "@Set_Yn:" + "N";

                    res = _bllSpExecute.SpExecuteString(sSpName, "UpdateToPackingOrder", pParam);
                }
                else if (packingOrders[i].gubun.Equals("D"))
                {
                    string[] pParam = new string[1];
                    pParam[0] = "@OrderNo:" + packingOrders[i].order_no;

                    res = _bllSpExecute.SpExecuteString(sSpName, "DeleteByOrderNo", pParam);
                }
            }

            return res;
        }

        internal int GetInputOrderStatus(string order_no)
        {
            string sSpName = "SP_PackingOrderLedger";
            string[] pParam = new string[1];
            pParam[0] = "@OrderNo:" + order_no;

            DataTable inpuOrderStatus = _bllSpExecute.SpExecuteTable(sSpName, "input_order_status", pParam);

            return inpuOrderStatus.Rows.Count;
        }

        internal DataTable GetItemInfo(string item_cd)
        {
            string sSpName = "SP_PackingOrderLedger";
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + item_cd;

            DataTable ItemData = _bllSpExecute.SpExecuteTable(sSpName, "SI", pParam);

            return ItemData;
        }

        internal DataSet GetPlanInfo(string packing_order_no)
        {
            string sSpName = "SP_PackingOrderLedger";
            string[] pParam = new string[1];
            pParam[0] = "@OrderNo:" + packing_order_no;

            DataSet ItemData = _bllSpExecute.SpExecuteDataSet(sSpName, "SP", pParam);

            return ItemData;
        }



        internal DataTable GetEmployeePopupData()
        {
            string sSpName = "CodeHelp";
            string strsql = "SELECT emp_cd, emp_nm, b.common_part_nm as emp_post_nm, a.dept_cd, c.dept_nm";
            strsql += " FROM employee a";
            strsql += " LEFT OUTER JOIN common b ON a.emp_post = b.common_part_cd AND b.common_cd = 'CM005'";
            strsql += " LEFT OUTER JOIN department c ON a.dept_cd = c.dept_cd";
            strsql += " WHERE a.use_yn = 'y'";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, strsql);

            return dt;
        }


        internal string plan_add(string packing_order_no, string start_hour, string stop_hour, string worker_qty, string UPH_qty, string UPS_qty, string MAN_qty, string plan_emp_cd)
        {
            string sSpName = "SP_PackingOrderLedger";

            string[] pParam = new string[8];
            pParam[0] = "@OrderNo:" + packing_order_no;
            pParam[1] = "@start_time:" + start_hour;
            pParam[2] = "@stop_time:" + stop_hour;
            pParam[3] = "@worker_qty:" + worker_qty;
            pParam[4] = "@EmpCd:" + plan_emp_cd;
            pParam[5] = "@UPH_qty:" + UPH_qty;
            pParam[6] = "@UPS_qty:" + UPS_qty;
            pParam[7] = "@MAN_qty:" + MAN_qty;

            string res = _bllSpExecute.SpExecuteString(sSpName, "PI", pParam);

            return res;
        }


        internal string result_add(string packing_order_no, string start_time, string stop_time, string worker_qty)
        {
            string sSpName = "SP_PackingOrderLedger";
            string res;

            string[] pParam = new string[4];
            pParam[0] = "@OrderNo:" + packing_order_no;
            pParam[1] = "@start_time:" + start_time;
            pParam[2] = "@stop_time:" + stop_time;
            pParam[3] = "@worker_qty:" + worker_qty;

            res = _bllSpExecute.SpExecuteString(sSpName, "RI", pParam);


            return res;
        }


        internal string check_add(string packing_order_no, string goal_qty, string progress_rate, string UPH_qty, string UPS_qty,
                                string gd_rate, string defect_qty, string defect_rate, string item_content_avg, string item_error_range, string start_time, string stop_time,
                                string worker_qty, string MAN_qty, string remark)
        {
            string sSpName = "SP_PackingOrderLedger";
            string res;

            string[] pParam = new string[15];
            pParam[0] = "@OrderNo:" + packing_order_no;
            pParam[1] = "@goal_qty:" + goal_qty;
            pParam[2] = "@progress_rate:" + progress_rate;
            pParam[3] = "@UPH_qty:" + UPH_qty;
            pParam[4] = "@UPS_qty:" + UPS_qty;
            pParam[5] = "@OK_rate:" + gd_rate;
            pParam[6] = "@defect_qty:" + defect_qty;
            pParam[7] = "@defect_rate:" + defect_rate;
            pParam[8] = "@start_time:" + start_time;
            pParam[9] = "@stop_time:" + stop_time;
            pParam[10] = "@item_content_avg:" + item_content_avg;
            pParam[11] = "@item_error_range:" + item_error_range;
            pParam[12] = "@worker_qty:" + worker_qty;
            pParam[13] = "@MAN_qty:" + MAN_qty;
            pParam[14] = "@remark:" + remark;


            res = _bllSpExecute.SpExecuteString(sSpName, "CI", pParam);


            return res;
        }


        internal string check_total(string packing_order_no, string start_time, string stop_time)
        {
            string sSpName = "SP_PackingOrderLedger";
            string res;

            string[] pParam = new string[3];
            pParam[0] = "@OrderNo:" + packing_order_no;
            pParam[1] = "@start_time:" + start_time;
            pParam[2] = "@stop_time:" + stop_time;

            res = _bllSpExecute.SpExecuteString(sSpName, "TD", pParam);


            return res;
        }

    }
}