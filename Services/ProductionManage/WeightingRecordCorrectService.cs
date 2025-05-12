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
    public class WeightingRecordCorrectService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select(string item_cd, string order_no, string start_date, string end_date)
        {
            string sSpName = "SP_WeightingRecordCorrect";
            string[] pParam = new string[4];
            pParam[0] = "@item_cd:" + item_cd;
            pParam[1] = "@order_no:" + order_no;
            pParam[2] = "@start_date:" + start_date;
            pParam[3] = "@end_date:" + end_date;

            DataTable items = _bllSpExecute.SpExecuteTable(sSpName, "S1", pParam);

            return items;
        }

        internal List<string> SelectWeighingInfo(string gubun, string order_no, string input_order_id, string weighing_no, string weighing_detailproc_id)
        {
            string sSpName = "SP_WeightingRecordCorrect";
            string[] pParam = new string[4];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@input_order_id:" + input_order_id;
            pParam[2] = "@weighing_no:" + weighing_no;
            pParam[3] = "@weighing_detailproc_id:" + weighing_detailproc_id;

            //"S3"은 테이블 2개 반환
            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            if (ds == null)
            {
                return null;
            }

            List<string> jsonList = new List<string>();

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                string jsonStr = Public_Function.DataTableToJSON(ds.Tables[i]);

                if (jsonStr.Length > 0)
                {
                    jsonList.Add(Public_Function.DataTableToJSON(ds.Tables[i]));
                }
                else if (jsonStr.Length <= 0)
                {
                    jsonList.Add("{ \"data\" : \"empty\" }");
                }
            }

            return jsonList;
        }

        internal DataTable SelectItem()
        {
            string sSpName = "SP_WeightingRecordCorrect";
            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + "";

            DataTable items = _bllSpExecute.SpExecuteTable(sSpName, "SI", pParam);

            return items;
        }

        internal String WeightingRecordCorrectSave(string order_no, string lot_no, string input_order_id, string weighing_no, string start_time, string end_time)
        {
            string sSpName = "SP_WeightingRecordCorrect";
            string[] pParam = new string[6];
            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@lot_no:" + lot_no;
            pParam[2] = "@input_order_id:" + input_order_id;
            pParam[3] = "@weighing_no:" + weighing_no;
            pParam[4] = "@start_date:" + start_time;
            pParam[5] = "@end_date:" + end_time;

            string message = _bllSpExecute.SpExecuteString(sSpName, "U8", pParam);

            return message;
        }

    }
}
