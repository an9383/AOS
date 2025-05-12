using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Libs.File;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Mont
{
    public class EquipmentOperationResultMontService
    {
        string sSpName = "SP_EquipmentOperationResultMont";
        private BllSpExecute _bllSpExecute = new BllSpExecute();


        internal DataTable Select(string startDate, string endDate)
        {
            string gubun = "S";
            string[] param = new string[2];

            param[0] = "@startDate:" + startDate;
            param[1] = "@endDate:" + endDate;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);

            return dt;
        }


        internal DataTable Select1(string startDate, string endDate)
        {
            string gubun = "S1";
            string[] param = new string[0];

            //param[0] = "@startDate:" + startDate;
            //param[1] = "@endDate:" + endDate;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);

            return dt;
        }


        internal DataTable WorkroomSelect()
        {
            string gubun = "workroom_select";
            string[] param = new string[0];

            //param[0] = "@startDate:" + startDate;
            //param[1] = "@endDate:" + endDate;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);
            DataTable dt = new DataTable();



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

            return table;
        }


        internal DataTable WeighingSelect()
        {
            string gubun = "weighing_select";
            string[] param = new string[0];

            //param[0] = "@startDate:" + startDate;
            //param[1] = "@endDate:" + endDate;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[1];
            }


            return dt;
        }


        internal DataTable WaitingSelect()
        {
            string gubun = "waiting_select";
            string[] param = new string[0];

            //param[0] = "@startDate:" + startDate;
            //param[1] = "@endDate:" + endDate;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[1];
            }


            return dt;
        }


        internal DataTable Select2(string startDate, string endDate)
        {
            string gubun = "S2";
            string[] param = new string[0];

            //param[0] = "@startDate:" + startDate;
            //param[1] = "@endDate:" + endDate;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);
            DataTable dt = new DataTable();

             if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();
            table.Columns.Add("workroom_cd", typeof(String));
            table.Columns.Add("workroom_nm", typeof(String));
            table.Columns.Add("workroom_type", typeof(String));
            table.Columns.Add("equip_cd", typeof(String));
            table.Columns.Add("equip_nm", typeof(String));
            table.Columns.Add("equip_img", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["workroom_cd"] = row["workroom_cd"].ToString();
                _row["workroom_nm"] = row["workroom_nm"].ToString();
                _row["workroom_type"] = row["workroom_type"].ToString();
                _row["equip_cd"] = row["equip_cd"].ToString();
                _row["equip_nm"] = row["equip_nm"].ToString();


                if (row["equip_img"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["equip_img"]);
                    string url = "data:Image/png;base64," + str;
                    _row["equip_img"] = url;
                }
                else
                {
                    _row["equip_img"] = "/Content/image/NoImage.png";
                }

                table.Rows.Add(_row);
            }


            return table;
        }


        internal DataTable EquipSelect()
        {
            string gubun = "equip_select";
            string[] param = new string[0];

            //param[0] = "@startDate:" + startDate;
            //param[1] = "@endDate:" + endDate;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }


            return dt;
        }

    }
}