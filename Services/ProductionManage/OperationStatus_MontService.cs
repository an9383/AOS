using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Libs.File;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class OperationStatus_MontService
    {
        string sSpName = "SP_OperationStatus_Mont";
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select()
        {
            string gubun = "S";

          
            string[] param = new string[0];

            //param[0] = "@workroom_type:" + workroom_type;
            //param[1] = "@endDate:" + endDate;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);

            return dt;
        }


        internal DataSet WorkingSelect()
        {
            string gubun = "WKS";
            string[] param = new string[0];

            //param[0] = "@startDate:" + startDate;
            //param[1] = "@endDate:" + endDate;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);
            DataTable dt = new DataTable();

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(ds.Tables[0].Copy());

            if (ds != null)
            {
                dt = ds.Tables[1];
            }

            DataTable table = new DataTable();
            table.Columns.Add("workroom_cd", typeof(String));
            table.Columns.Add("workroom_title", typeof(String));
            table.Columns.Add("plan_end", typeof(String));
            table.Columns.Add("ing_yn", typeof(String));
            table.Columns.Add("ing_item_nm", typeof(String));
            table.Columns.Add("rate", typeof(String));
            table.Columns.Add("detailproc_status", typeof(String));
            table.Columns.Add("rest_yn", typeof(String));
            table.Columns.Add("detailproc_status_nm", typeof(String));
            table.Columns.Add("workroom_temp", typeof(String));
            table.Columns.Add("workroom_hum", typeof(String));
            table.Columns.Add("item_img", typeof(String));
            table.Columns.Add("order_operate_time", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["workroom_cd"] = row["workroom_cd"].ToString();
                _row["workroom_title"] = row["workroom_title"].ToString();
                _row["plan_end"] = row["plan_end"].ToString();
                _row["ing_yn"] = row["ing_yn"].ToString();
                _row["ing_item_nm"] = row["ing_item_nm"].ToString();
                _row["rate"] = row["rate"].ToString();
                _row["detailproc_status"] = row["detailproc_status"].ToString();
                _row["rest_yn"] = row["rest_yn"].ToString();
                _row["detailproc_status_nm"] = row["detailproc_status_nm"].ToString();
                _row["workroom_temp"] = row["workroom_temp"].ToString();
                _row["workroom_hum"] = row["workroom_hum"].ToString();
                _row["order_operate_time"] = row["order_operate_time"].ToString();


                if (row["item_img"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["item_img"]);
                    string url = "data:Image/png;base64," + str;
                    _row["item_img"] = url;
                }
                else
                {
                    _row["item_img"] = "/Content/image/NoImage.png";
                }

                table.Rows.Add(_row);
            }
            dataSet.Tables.Add(table);

            return dataSet;
        }


        internal DataSet WeighingSelect()
        {
            string gubun = "WS";
            string[] param = new string[0];

            //param[0] = "@startDate:" + startDate;
            //param[1] = "@endDate:" + endDate;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();
            DataSet dataSet = new DataSet();

            dataSet.Tables.Add(ds.Tables[0].Copy());

            if (ds != null)
            {
                dt = ds.Tables[1];
            }

            DataTable table = new DataTable();
            table.Columns.Add("workroom_cd", typeof(String));
            table.Columns.Add("workroom_title", typeof(String));
            table.Columns.Add("ing_item_nm", typeof(String));
            table.Columns.Add("plan_end", typeof(String));
            table.Columns.Add("ing_yn", typeof(String));
            table.Columns.Add("detailproc_status", typeof(String));
            table.Columns.Add("detailproc_status_nm", typeof(String));
            table.Columns.Add("rate", typeof(String));
            table.Columns.Add("workroom_temp", typeof(String));
            table.Columns.Add("workroom_hum", typeof(String));
            table.Columns.Add("item_img", typeof(String));
            table.Columns.Add("order_operate_time", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["workroom_cd"] = row["workroom_cd"].ToString();
                _row["workroom_title"] = row["workroom_title"].ToString();
                _row["ing_item_nm"] = row["ing_item_nm"].ToString();
                _row["plan_end"] = row["plan_end"].ToString();
                _row["ing_yn"] = row["ing_yn"].ToString();
                _row["detailproc_status"] = row["detailproc_status"].ToString();
                _row["detailproc_status_nm"] = row["detailproc_status_nm"].ToString();
                _row["rate"] = row["rate"].ToString();
                _row["workroom_temp"] = row["workroom_temp"].ToString();
                _row["workroom_hum"] = row["workroom_hum"].ToString();
                _row["order_operate_time"] = row["order_operate_time"].ToString();



                if (row["item_img"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["item_img"]);
                    string url = "data:Image/png;base64," + str;
                    _row["item_img"] = url;
                }
                else
                {
                    _row["item_img"] = "/Content/image/NoImage.png";
                }

                table.Rows.Add(_row);
            }

            dataSet.Tables.Add(table);

            return dataSet;
        }


        internal DataSet WaitingSelect()
        {
            string gubun = "WTS";
            string[] param = new string[0];

            //param[0] = "@startDate:" + startDate;
            //param[1] = "@endDate:" + endDate;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();
            DataSet dataSet = new DataSet();

            dataSet.Tables.Add(ds.Tables[0].Copy());

            if (ds != null)
            {
                dt = ds.Tables[1];
            }
            DataTable table = new DataTable();
            table.Columns.Add("workroom_cd", typeof(String));
            table.Columns.Add("workroom_title", typeof(String));
            table.Columns.Add("ing_item_nm", typeof(String));
            table.Columns.Add("plan_end", typeof(String));
            table.Columns.Add("ing_yn", typeof(String));
            table.Columns.Add("detailproc_status", typeof(String));
            table.Columns.Add("detailproc_status_nm", typeof(String));
            table.Columns.Add("rate", typeof(String));
            table.Columns.Add("workroom_temp", typeof(String));
            table.Columns.Add("workroom_hum", typeof(String));
            table.Columns.Add("item_img", typeof(String));
            table.Columns.Add("order_operate_time", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["workroom_cd"] = row["workroom_cd"].ToString();
                _row["workroom_title"] = row["workroom_title"].ToString();
                _row["ing_item_nm"] = row["ing_item_nm"].ToString();
                _row["plan_end"] = row["plan_end"].ToString();
                _row["ing_yn"] = row["ing_yn"].ToString();
                _row["detailproc_status"] = row["detailproc_status"].ToString();
                _row["detailproc_status_nm"] = row["detailproc_status_nm"].ToString();
                _row["rate"] = row["rate"].ToString();
                _row["workroom_temp"] = row["workroom_temp"].ToString();
                _row["workroom_hum"] = row["workroom_hum"].ToString();
                _row["order_operate_time"] = row["order_operate_time"].ToString();


                if (row["item_img"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["item_img"]);
                    string url = "data:Image/png;base64," + str;
                    _row["item_img"] = url;
                }
                else
                {
                    _row["item_img"] = "/Content/image/NoImage.png";
                }

                table.Rows.Add(_row);
            }

            dataSet.Tables.Add(table);

            return dataSet;
        }
        
    }
}