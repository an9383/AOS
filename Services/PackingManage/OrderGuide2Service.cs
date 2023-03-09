using HACCP.Libs.Database;
using HACCP.Models.ProductionManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.PackingManage
{
    public class OrderGuide2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable GetSelectbox(string gubun)
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

        internal DataTable SelectPackingOrderData(string packing_order_no)
        {
            string sSpName = "SP_OrderGuide2";

            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + packing_order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectItemProcessData(string order_no, string packing_order_no)
        {
            string sSpName = "SP_OrderGuide2";

            string[] pParam = new string[2];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@packing_order_no:" + packing_order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectReleaseData(string packing_order_no)
        {
            string sSpName = "SP_OrderGuide2";

            string[] pParam = new string[1];
            pParam[0] = "@order_no:" + packing_order_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectWorkorderPopupData(string sDate, string eDate, string item_cd)
        {
            string sSpName = "CODEHELP";

            string strsql = "SELECT DISTINCT   A.packing_order_date as order_date, B.item_cd, B.item_nm, D.lot_no, "
                + "A.packing_order_no, A.order_no , case when e.cnt > 0 then 'Y' else 'N' end sign_status "
                + " , case when l2.cnt2 > 0 and l1.cnt1 = l2.cnt2 then '확정' else '미확정' end gubun "
                + " FROM  packing_order A "
                + " INNER JOIN   work_order D ON A.order_no = D.order_no "
                + " INNER JOIN item_license B ON A.sale_item_cd = B.item_cd AND a.revision_id = b.item_license_no "
                + " LEFT JOIN order_proc C ON A.packing_order_no = C.pack_order_no  and c.process_level ='1' "
                + " left join (select COUNT(*) cnt, order_no from order_sign_list "
                + " where mp_ck = 'P' and sign_set_cd = '2002' and isnull(sign_history_id, '') <> '' "
                + " group by order_no) e on e.order_no = a.packing_order_no"
                + "            LEFT OUTER JOIN (select isnull(count(*),0) as cnt1, order_no "
                + "                             from REQ_ORDER "
                + "                             where REQ_ORDER_STATUS = '03' "
                + "                             group by order_no) l1 on l1.ORDER_NO = a.packing_order_no "
                + "            LEFT OUTER JOIN (select isNull(COUNT(*),0) as cnt2, order_no "
                + "                 from REQ_ORDER "
                + "                 group by ORDER_NO) l2 on l2.ORDER_NO = a.packing_order_no "
                + "  where("
                + "   a.sale_item_cd like '%" + item_cd + "%'"
                + "   or b.item_nm like '%" + item_cd + "%')"
                + "   and a.packing_order_date between '" + sDate + "' and '" + eDate + "' ";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, strsql);

            return dt;
        }

        internal DataTable SelectWorkorderPopupData2(string sDate, string eDate, string item_cd)
        {
            string sSpName = "CODEHELP";

            string strsql = "SELECT DISTINCT   A.packing_order_date as order_date, B.item_cd, B.item_nm, a.lot_num as lot_no, "
                + "A.packing_order_no, A.order_no , case when e.cnt > 0 then 'Y' else 'N' end sign_status "
                + " , case when l2.cnt2 > 0 and l1.cnt1 = l2.cnt2 then '확정' else '미확정' end gubun "
                + " FROM  packing_order A "
                + " INNER JOIN item_license B ON A.sale_item_cd = B.item_cd AND a.revision_id = b.item_license_no "
                + " LEFT JOIN order_proc C ON A.packing_order_no = C.pack_order_no  and c.process_level ='1' "
                + " left join (select COUNT(*) cnt, order_no from order_sign_list "
                + " where mp_ck = 'P' and sign_set_cd = '2002' and isnull(sign_history_id, '') <> '' "
                + " group by order_no) e on e.order_no = a.packing_order_no"
                + "            LEFT OUTER JOIN (select isnull(count(*),0) as cnt1, order_no "
                + "                             from REQ_ORDER "
                + "                             where REQ_ORDER_STATUS = '03' "
                + "                             group by order_no) l1 on l1.ORDER_NO = a.packing_order_no "
                + "            LEFT OUTER JOIN (select isNull(COUNT(*),0) as cnt2, order_no "
                + "                 from REQ_ORDER "
                + "                 group by ORDER_NO) l2 on l2.ORDER_NO = a.packing_order_no "
                + "  where("
                + "   a.sale_item_cd like '%" + item_cd + "%'"
                + "   or b.item_nm like '%" + item_cd + "%')"
                + "   and a.packing_order_date between '" + sDate + "' and '" + eDate + "' ";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, strsql);

            return dt;
        }

        internal string OrderGuide2AutoRelease(string order_no)
        {
            string sSpName = "SP_OrderGuide2";
            string[] pParam = new string[1];

            pParam[0] = "@order_no:" + order_no;

            string str = _bllSpExecute.SpExecuteString(sSpName, "TMP_WF2", pParam);

            return str;
        }

        internal string OrderGuideUpdateWorkOrder(WorkOrder workOrder)
        {
            string sSpName = "SP_OrderGuide2";
            string[] param = new string[8];

            param[0] = "@order_no:" + workOrder.order_no;
            param[1] = "@order_status:" + workOrder.order_status;
            param[2] = "@order_date:" + workOrder.order_date;
            param[3] = "@planned_date:" + workOrder.order_date;
            param[4] = "@start_date:" + workOrder.start_date;
            param[5] = "@order_qty:" + workOrder.order_qty;
            param[6] = "@cost_cd:" + workOrder.cost_cd;
            param[7] = "@valid_date:" + workOrder.valid_date;

            string str = _bllSpExecute.SpExecuteString(sSpName, "U", param);

            return str;
        }

        internal string OrderGuide2Stop(string packing_order_no, string remark)
        {
            string sSpName = "SP_OrderGuide2";
            string[] param = new string[2];

            param[0] = "@order_no:" + packing_order_no;
            param[1] = "@order_bigo:" + remark;

            string str = _bllSpExecute.SpExecuteString(sSpName, "stop", param);

            return str;
        }
        
    }
}