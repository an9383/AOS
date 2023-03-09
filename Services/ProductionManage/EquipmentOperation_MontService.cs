using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Libs.File;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace HACCP.Services.ProductionManage
{
    public class EquipmentOperation_MontService
    {
        string sSpName = "SP_EquipmentOperation_Mont";
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select()
        {
            string gubun = "S";

            string[] param = new string[0];

            //param[0] = "@workroom_type:" + workroom_type;
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


                //ImageConverter imgcvt = new ImageConverter();
                //Image img = (Image)imgcvt.ConvertFrom((byte[])row["equip_img"]);
                //string path = HttpContext.Current.Request.PhysicalApplicationPath;
                //path += "Content\\image\\equipImage";

                //DirectoryInfo di = new DirectoryInfo(path);
                //if (di.Exists == false)
                //{
                //    di.Create();
                //}

                //path += "\\" + row["equip_cd"].ToString() + ".png";
                //FileStream fs = new FileStream(path, FileMode.Create);

                //img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                //img.Dispose();
                //fs.Close();


                table.Rows.Add(_row);
            }

            return table;
        }


        internal DataTable EquipSelect()
        {
            string gubun = "ES";
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

        internal DataTable TestSelect()
        {
            string gubun = "TS";

            string[] param = new string[0];

            //param[0] = "@workroom_type:" + workroom_type;
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
            table.Columns.Add("equip_cd", typeof(String));
            table.Columns.Add("equip_nm", typeof(String));
            table.Columns.Add("equip_img", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["workroom_cd"] = row["workroom_cd"].ToString();
                _row["workroom_nm"] = row["workroom_nm"].ToString();
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


        internal DataTable TestEquipSelect()
        {
            string gubun = "TES";
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