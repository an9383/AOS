using HACCP.Libs.Database;
using HACCP.Models.Aprov;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Aprov
{
    public class ShippingRecognitionService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ShippingRecognition";

        internal DataTable Select(ShippingRecognition model)
        {
            string sGubun = "S";
            string[] param = new string[6];

            param[0] = "@request_start_date_S:" + model.start_date_S;
            param[1] = "@request_end_date_S:" + model.end_date_S;
            //param[2] = "@item_S:" + model.item_cd_S;
            param[2] = "@item_S:" + model.item_nm_S;
            param[3] = "@lot_no_S:" + "";
            param[4] = "@status_S:" + model.status_S;
            param[5] = "@date_type_S:" + model.date_type_s;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable s_item_Popup(string pSpName)
        {
            string strsql = "SELECT item_cd, item_nm";
            strsql += " FROM v_packingitem";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable SE(ShippingRecognition model)
        {
            string sGubun = "SE";
            string[] param = new string[1];

            param[0] = "@shipping_cd:" + model.shipping_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();
            table.Columns.Add("sign_set_nm", typeof(String));
            table.Columns.Add("sign_set_dt_id", typeof(String));
            table.Columns.Add("displayfield", typeof(String));
            table.Columns.Add("sign_yn", typeof(String));
            table.Columns.Add("sign_time", typeof(String));
            table.Columns.Add("sign_emp_cd", typeof(String));
            table.Columns.Add("responsible_emp_cd", typeof(String));
            table.Columns.Add("sign_image", typeof(String));
            table.Columns.Add("responsible_emp_nm", typeof(String));
            table.Columns.Add("sign_emp_nm", typeof(String));
            table.Columns.Add("prev_sign_yn", typeof(String));
            table.Columns.Add("next_sign_yn", typeof(String));
            table.Columns.Add("sign_set_dt_seq", typeof(String));
            table.Columns.Add("responsible_authority", typeof(String));

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
                _row["responsible_authority"] = row["responsible_authority"].ToString();

                table.Rows.Add(_row);
            }

            return table;
        }

        internal DataTable S2(ShippingRecognition model)
        {
            string sGubun = "S2";
            string[] param = new string[1];

            param[0] = "@shipping_cd:" + model.shipping_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal string TRX(ShippingRecognition model)
        {
            string sGubun = "";
            string message = "";

            if (model.gubun == "U_date")
            {
                sGubun = "U_date";

                string[] pParam = new string[2];

                pParam[0] = "@approval_date:" + model.approval_date;
                pParam[1] = "@shipping_cd:" + model.shipping_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return message;
        }

        internal string batch(List<ShippingRecognition> model, string gubun)
        {
            string sGubun = "";
            string message = "";

            for(int i = 0; i < model.Count; i++)
            {
                if (model[i].gubun.Equals("U"))
                {
                    sGubun = "U";
                    string[] param = new string[4];

                    param[0] = "@shipping_cd:" + model[i].shipping_cd;
                    param[1] = "@check_no:" + model[i].check_no;
                    param[2] = "@check_opinion:" + model[i].check_opinion;
                    param[3] = "@check_yn:" + model[i].check_yn;
                    //param[4] = "@approval_date:" + model[i].approval_date;

                    message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
                }
            }

            return message;
        }

        internal string SignCRUD(ShippingRecognition model)
        {
            string sGubun = "";
            string message = "";

            if (model.gubun == "EI")
            {
                sGubun = model.gubun;
                string[] param = new string[5];
                param[0] = "@shipping_cd:" + model.shipping_cd;
                param[1] = "@sign_set_dt_id:" + model.sign_set_dt_id;
                param[2] = "@emp_cd:" + model.emp_cd;
                param[3] = "@representative_yn:" + model.representative_yn;
                param[4] = "@validation_type:" + model.validation_type;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            } 
            else if (model.gubun == "ED")
            {
                sGubun = model.gubun;                
                string[] param = new string[2];
                param[0] = "@shipping_cd:" + model.shipping_cd;
                param[1] = "@sign_set_dt_id:" + model.sign_set_dt_id;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, param);
            }

            return message;
        }

        internal DataTable selectItemImage(string shipping_cd)
        {
            string[] pParam = new string[1];
            pParam[0] = "@shipping_cd:" + shipping_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SelectImage", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();
            table.Columns.Add("shipping_image", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                if (row["shipping_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["shipping_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["shipping_image"] = url;
                }
                else
                {
                    _row["shipping_image"] = "/Content/image/defaultImage.png";
                }

                table.Rows.Add(_row);
            }

            return table;
        }

        internal void uploadImage(byte[] myBytes, string shipping_cd, string fileName)
        {
            string[] param = new string[1];
            param[0] = "@shipping_cd:" + shipping_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, "UploadImage", "@" + fileName, myBytes, param);

            return;
        }

        internal string deleteImage(string shipping_cd)
        {
            string[] param = new string[1];
            param[0] = "@shipping_cd:" + shipping_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, "DeleteImage", param);

            return message;
        }
    }
}