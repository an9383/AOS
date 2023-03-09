using HACCP.Libs.Database;
using HACCP.Models.ProductionManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.ProductionManage
{
    public class OrderGuideService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectWorkorderPopupData(string start_date, string end_date, string lot_no, string item_cd)
        {
            string sSpName = "CODEHELP";

            string strsql = "  SELECT	a.order_no, a.lot_no, a.item_cd, b.item_nm, a.order_date, 			    c.common_part_nm as order_status_nm  , isnull(sign_set_dt_seq,0) sign_status  , case when l2.cnt2 > 0 and l1.cnt1 = l2.cnt2 then '확정' else '미확정' end gubun  FROM WORK_ORDER a 			JOIN item_license b on a.item_cd = b.item_cd AND a.revision_id= b.item_license_no			LEFT OUTER JOIN common c on c.common_cd = 'RT005' and a.order_status = c.common_part_cd         LEFT JOIN (SELECT MAX(sign_set_dt_seq) sign_set_dt_seq, order_no FROM order_sign_list                                     WHERE mp_ck = 'M' and sign_set_cd = '2001' and isnull(sign_history_id, '') <> ''                                     GROUP BY order_no) e on e.order_no = a.order_no             LEFT OUTER JOIN (select isnull(count(*),0) as cnt1, order_no                              from REQ_ORDER                              where REQ_ORDER_STATUS = '03'                              group by order_no) l1 on l1.ORDER_NO = a.order_no             LEFT OUTER JOIN (select isNull(COUNT(*),0) as cnt2, order_no 					            from REQ_ORDER 					            group by ORDER_NO) l2 on l2.ORDER_NO = a.order_no  where 	a.lot_no like '%' + '"+ lot_no + "' + '%'  and	a.order_date between '"+ start_date + "' and '"+ end_date + "'  and	(  a.item_cd like '%' + '"+ item_cd + "' + '%'  OR	  b.item_nm like '%' + '"+ item_cd + "' + '%')";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, strsql);

            return dt;
        }

        internal DataTable GetSelectbox(string gubun)
        {
            string sSpName = "SP_OrderGuide";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable GetSelectbox2(string gubun)
        {
            string sSpName = "SP_OrderGuide2";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectWorkorderData(string order_no)
        {
            string sSpName = "SP_OrderGuide";

            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectItemProcessData(string mp_ck, string order_no)
        {
            string sSpName = "SP_OrderGuide";

            string[] pParam = new string[2];
            pParam[0] = "@mp_ck:" + mp_ck;
            pParam[1] = "@order_no:" + order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectBomData(string order_no)
        {
            string sSpName = "SP_OrderGuide";

            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S4", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectReleaseData(string order_no)
        {
            string sSpName = "SP_OrderGuide";

            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectSignData(string mp_ck, string order_no, string sign_set_id)
        {
            string sSpName = "SP_ORDER_ES_Manage";

            string[] pParam = new string[3];
            pParam[0] = "@mp_ck:" + mp_ck;
            pParam[1] = "@order_no:" + order_no;
            pParam[2] = "@sign_set_cd:" + sign_set_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "ES", pParam);

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

        internal string OrderGuideSignCRUD(string gubun, string mp_ck, string order_no, string sign_set_cd, string sign_set_dt_id, string emp_cd, string representative_yn, string validation_type)
        {
            string sSpName = "SP_ORDER_ES_Manage";
            string[] pParam = new string[7];

            if (gubun.Equals("EI"))
            {

                pParam[0] = "@mp_ck:" + mp_ck;
                pParam[1] = "@order_no:" + order_no;
                pParam[2] = "@sign_set_cd:" + sign_set_cd;
                pParam[3] = "@sign_set_dt_id:" + sign_set_dt_id;
                pParam[4] = "@emp_cd:" + emp_cd;
                pParam[5] = "@representative_yn:" + representative_yn;
                pParam[6] = "@validation_type:" + validation_type;

            }
            else if (gubun.Equals("ED"))
            {
                pParam = new string[4];

                pParam[0] = "@mp_ck:" + mp_ck;
                pParam[1] = "@order_no:" + order_no;
                pParam[2] = "@sign_set_cd:" + sign_set_cd;
                pParam[3] = "@sign_set_dt_id:" + sign_set_dt_id;

            }

            string str = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return str;

        }

        internal string OrderGuideUpdateSignStatus(string mp_ck, string order_no, string sign_set_cd)
        {
            string sSpName = "SP_OrderGuide";
            string[] pParam = new string[3];
            pParam[0] = "@mp_ck:" + mp_ck;
            pParam[1] = "@order_no:" + order_no;
            pParam[2] = "@sign_set_cd:" + sign_set_cd;

            string str = _bllSpExecute.SpExecuteString(sSpName, "SS", pParam);

            return str;

        }

        internal string OrderGuideSignRepresentativeCk(string mp_ck, string order_no, string sign_set_cd, string sign_set_dt_id, string emp_cd)
        {
            string sSpName = "SP_ORDER_ES_Manage";
            string[] pParam = new string[5];

            pParam[0] = "@mp_ck:" + mp_ck;
            pParam[1] = "@order_no:" + order_no;
            pParam[2] = "@sign_set_cd:" + sign_set_cd;
            pParam[3] = "@sign_set_dt_id:" + sign_set_dt_id;
            pParam[4] = "@emp_cd:" + emp_cd;

            string str = _bllSpExecute.SpExecuteString(sSpName, "SR", pParam);

            return str;
        }

        internal string OrderGuideAutoReleaseCheck(string order_no)
        {
            string sSpName = "SP_OrderGuide";
            string[] pParam = new string[1];

            pParam[0] = "@order_no:" + order_no;

            string str = _bllSpExecute.SpExecuteString(sSpName, "TMP_WF_CK", pParam);

            return str;
        }

        internal string OrderGuideAutoRelease(string order_no)
        {
            string sSpName = "SP_OrderGuide";
            string[] pParam = new string[1];

            pParam[0] = "@order_no:" + order_no;

            string str = _bllSpExecute.SpExecuteString(sSpName, "TMP_WF", pParam);

            return str;
        }

        internal string orderGuideAutoReleaseNotIncludeTransfer(string order_no)
        {
            string sSpName = "SP_OrderGuide";
            string[] pParam = new string[1];

            pParam[0] = "@order_no:" + order_no;

            string str = _bllSpExecute.SpExecuteString(sSpName, "TMP_WF_NIT", pParam);

            return str;
        }


        internal string OrderGuideUpdateWorkOrder(WorkOrder workOrder)
        {
            string sSpName = "SP_OrderGuide";
            string[] param = new string[9];

            param[0] = "@order_status:" + workOrder.order_status;
            param[1] = "@order_date:" + workOrder.order_date;
            param[2] = "@planned_date:" + workOrder.planned_date;
            param[3] = "@start_date:" + workOrder.start_date;
            param[4] = "@order_qty:" + workOrder.order_qty;
            param[5] = "@order_bigo:" + workOrder.order_bigo;
            param[6] = "@cost_cd:" + workOrder.cost_cd;
            param[7] = "@valid_date:" + workOrder.valid_date;
            param[8] = "@order_no:" + workOrder.order_no;

            string str = _bllSpExecute.SpExecuteString(sSpName, "U", param);

            return str;
        }

        internal string OrderGuideStop(string order_no, string remark, string stopYN)
        {
            string sSpName = "SP_OrderGuide";
            string[] param = new string[3];

            param[0] = "@order_no:" + order_no;
            param[1] = "@order_bigo:" + remark;
            param[2] = "@stopYN:" + stopYN;

            string str = _bllSpExecute.SpExecuteString(sSpName, "stop", param);

            return str;
        }
    }
}
