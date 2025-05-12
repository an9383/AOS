using HACCP.Libs.Database;
using HACCP.Models.WorkSheetManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.WorkSheetManage
{
    public class DayOrderWorkService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable DayOrderWorkSelect(DayOrderWork.SrchParam param)
        {
            string sSpName = "SP_DayOrderWork_Y";

            string[] pParam = new string[3];
            pParam[0] = "@sdate:" + param.sdate;
            pParam[1] = "@edate:" + param.edate;
            pParam[2] = "@status:" + param.status;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;

        }

        internal DataTable DayOrderWorkSelectDetail(string order_no, string order_proc_id, string process_cd, string packing_order_no)
        {
            string sSpName = "SP_DayOrderWork_Y";

            string[] pParam = new string[4];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@order_proc_id:" + order_proc_id;
            pParam[2] = "@process_cd:" + process_cd;
            pParam[3] = "@packing_order_no:" + packing_order_no;


            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;

        }

        internal DataTable DayOrderWorkSelectDetailWork(string order_no, string order_proc_id, string order_detailproc_id)
        {
            string sSpName = "SP_DayOrderWork_Y";

            string[] pParam = new string[3];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@order_proc_id:" + order_proc_id;
            pParam[2] = "@order_detailproc_id:" + order_detailproc_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;

        }

        internal DataTable DayOrderWork_ReportMaster(string order_no, string grouping_cd, string sdate, string edate)
        {

            string[] pParam = new string[4];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@grouping_cd:" +grouping_cd;
            pParam[2] = "@sdate:" + sdate;
            pParam[3] = "@edate:" + edate;

            DataSet ds = new DataSet();
            ds = _bllSpExecute.SpExecuteDataSet("SP_DayWorkReport", "Master", pParam);

            DataTable dt = new DataTable();

            //승인된 제조지시가 있을 때
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[1];
            }

            return dt;

        }


        internal DataTable WorkSheetMasterSP(string order_no)
        {

            string[] pParam = new string[1];
            pParam[0] = "@ORDER_NO:" + order_no;

            DataTable dt = _bllSpExecute.SpExecuteTable("SP_WorkSheetW", "S4", pParam);

            return dt;
        }

        internal DataSet WorkSheetMasterDetailSP(string order_no, string input_order_id, string weighing_no)
        {

            string[] pParam = new string[6];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@input_order_id:" + input_order_id;
            pParam[2] = "@weighing_no:" + weighing_no;
            pParam[3] = "@mp_ck:" + "M";
            pParam[4] = "@sign_set_cd:" + "2001";
            pParam[5] = "@sign_set_cd2:" + "2005";

            DataSet ds = new DataSet();
            ds = _bllSpExecute.SpExecuteDataSet("SP_WorkSheetW", "S5", pParam);

            //if (ds.Tables[0].Rows.Count == 0)
            //{

            //}
            //else
            //{
            //    ds.Tables[0].TableName = "header";
            //    ds.Tables[1].TableName = "order_proc";
            //    ds.Tables[2].TableName = "order_weighing";
            //    ds.Tables[3].TableName = "sum";
            //    ds.Tables[4].TableName = "order_procB";
            //    ds.Tables[5].TableName = "order_item_bom";
            //    ds.Tables[6].TableName = "sum_bom";
            //}
            return ds;
        }

        internal DataTable PrintPackingOrderMasterSP(string mp_ck, string order_no, string packing_order_no, string sign_set_cd, string sign_set_cd2)
        {
            string[] pParam = new string[5];
            pParam[0] = "@MP_CK:" + mp_ck;
            pParam[1] = "@ORDER_NO:" + order_no;
            pParam[2] = "@PACKING_ORDER_NO:" + packing_order_no;
            pParam[3] = "@sign_set_cd:" + sign_set_cd;
            pParam[4] = "@sign_set_cd2:" + sign_set_cd2;
            //pParam[5] = "@item_bom_rpt_seq:";

            DataSet ds = new DataSet(); 

            ds = _bllSpExecute.SpExecuteDataSet("SP_PACKINGORDERISSUE", "S1", pParam);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[1];
                }
              
            }
            return dt;
        }

        internal DataTable PrintWorkSheetMasterSP(string mp_ck, string order_no, string packing_order_no)
        {
            string[] pParam = new string[3];

            pParam[0] = "@mp_ck:" + mp_ck;
            pParam[1] = "@order_no:" + order_no;
            pParam[2] = "@packing_order_no:" + packing_order_no;

            DataTable dt = new DataTable();
            dt = _bllSpExecute.SpExecuteTable("SP_PrintOrderResult", "S", pParam);

            return dt;
        }

        internal DataTable PrintWorkSheetDetailSP(string order_no, string order_proc_id, string order_detailproc_mid)
        {
            string[] pParam = new string[3];

            pParam[0] = "@ORDER_NO:" + order_no;
            pParam[1] = "@ORDER_PROC_ID:" + order_proc_id;
            pParam[2] = "@ORDER_DETAILPROC_MID:" + order_detailproc_mid;

            DataTable dt = new DataTable();
            dt = _bllSpExecute.SpExecuteTable("SP_WORKSHEETPACK", "SI", pParam);

            return dt;
        }

        internal DataSet WorkSheetWeighMasterSP(string order_no)
        {

            string[] pParam = new string[4];

            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@mp_ck:M";
            pParam[2] = "@sign_set_cd:2001";
            pParam[3] = "@sign_set_cd2:2005";


            DataSet ds = new DataSet();
            ds = _bllSpExecute.SpExecuteDataSet("SP_WorkSheetW", "S6", pParam);

            return ds;
        }

        internal string DayOrderWork_DetailProcSave(List<DayOrderWork> paramData)
        {
            string sSpName = "SP_DayOrderWork_Y";
            string res = "";

            foreach (DayOrderWork tmp in paramData)
            {

                string[] pParam = new String[4];
                pParam[0] = "@order_work_start_time:" + tmp.order_work_start_time;
                pParam[1] = "@order_work_end_time:" + tmp.order_work_end_time;
                pParam[2] = "@order_work_id:" + tmp.order_work_id;
                pParam[3] = "@order_no:" + tmp.order_no;

                res = _bllSpExecute.SpExecuteString(sSpName, "U", pParam);

            }

            return res;
        }


    }
}