using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Mont
{
    public class SmartFactory_InfomationStateService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        string sSpName = "SP_SmartFactory_InfomationStatus";


        #region CHEBIGEN 용
        internal DataSet SelectFactoryInfomation_Che()
        {
            string[] param = new string[0];

            DataSet dt = _bllSpExecute.SpExecuteDataSet(sSpName, "S", param);

            return dt;
        }

        internal DataTable ChartDataSearch_Che(string start_time, string stop_time)
        {
            string[] param = new string[2];
            param[0] = "@start_time:" + start_time;
            param[1] = "@stop_time:" + stop_time;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "CS", param);

            return dt;
        }
        #endregion CHEBIGEN용 END


        internal DataTable EquipSelect()
        {
            string[] param = new string[0];

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "ES", param);

            return dt;
        }


        internal DataSet SelectFactoryInfomation(string equip_cd)
        {
            string[] param = new string[1];
            param[0] = "@equip_cd:" + equip_cd;

            DataSet dt = _bllSpExecute.SpExecuteDataSet(sSpName, "S", param);

            return dt;
        }

        internal DataTable ItemImgSearch(string item_cd)
        {
            string[] param = new string[1];
            param[0] = "@item_cd:" + item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "IS", param);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();
            table.Columns.Add("item_cd", typeof(String));
            table.Columns.Add("upper_image", typeof(String));



            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow _row = table.NewRow();
                _row["item_cd"] = item_cd;
                _row["upper_image"] = "/Content/image/NoImage.png";

                table.Rows.Add(_row);
            }
            else
            {
                foreach (DataRow row in dt.AsEnumerable())
                {
                    DataRow _row = table.NewRow();

                    _row["item_cd"] = row["item_cd"].ToString();


                    if (row["upper_image"] != System.DBNull.Value)
                    {
                        string str = Convert.ToBase64String((byte[])row["upper_image"]);
                        string url = "data:Image/png;base64," + str;
                        _row["upper_image"] = url;
                    }
                    else
                    {
                        _row["upper_image"] = "/Content/image/NoImage.png";
                    }

                    table.Rows.Add(_row);
                }

            }

           

            return table;
        }

        internal DataTable ChartDataSearch(string start_time, string stop_time, string equip_cd, string equip_type2, string item_cd)
        {
            string[] param = new string[5];
            param[0] = "@start_time:" + start_time;
            param[1] = "@stop_time:" + stop_time;
            param[2] = "@equip_cd:" + equip_cd;
            param[3] = "@equip_type2:" + equip_type2;
            param[4] = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "CS", param);

            return dt;
        }

        internal DataTable SelectTotalData(string start_time, string stop_time, string item_cd)
        {
            string[] param = new string[3];
            param[0] = "@start_time:" + start_time;
            param[1] = "@stop_time:" + stop_time;
            param[2] = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "GS", param);

            return dt;
        }

        internal DataTable SelectChartData(string start_time, string stop_time, string item_cd)
        {
            string[] param = new string[3];
            param[0] = "@start_time:" + start_time;
            param[1] = "@stop_time:" + stop_time;
            param[2] = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "GCS", param);

            return dt;
        }
    }
}