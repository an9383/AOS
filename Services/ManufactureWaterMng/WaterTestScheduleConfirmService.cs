using HACCP.Libs.Database;
using HACCP.Models.ManufactureWaterMng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.ManufactureWaterMng
{
    public class WaterTestScheduleConfirmService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_WaterTestScheduleConfirm";

        internal DataTable WaterTestScheduleConfirmSelect(WaterTestScheduleConfirm.SrchParam param)
        {
            string[] pParam = new string[2];
            pParam[0] = "@start_date:" + param.start_date;
            pParam[1] = "@end_date:" + param.end_date;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable WaterTestScheduleSelect(WaterTestScheduleConfirm.SrchParam param)
        {
            string[] pParam = new string[2];
            pParam[0] = "@start_date:" + param.start_date;
            pParam[1] = "@end_date:" + param.end_date;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SL", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string WaterTestScheduleConfirmTRX(WaterTestScheduleConfirm waterTestSchedule)
        {
            string[] pParam = new string[4];
            pParam[0] = "@confirm_no:" + waterTestSchedule.confirm_no;
            pParam[1] = "@start_date:" + waterTestSchedule.start_date;
            pParam[2] = "@end_date:" + waterTestSchedule.end_date;
            pParam[3] = "@confirm_status:" + waterTestSchedule.confirm_status;

            string res = _bllSpExecute.SpExecuteString(sSpName, waterTestSchedule.gubun, pParam);

            return res;
        }

        internal DataTable WaterTestScheduleSignSelect(string confirm_no)
        {
            string[] pParam = new string[2];
            pParam[0] = "@confirm_no:" + confirm_no;
            pParam[1] = "@sign_set_cd:" + "1031";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SS", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();
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
            table.Columns.Add("sign_tot_cnt", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

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
                _row["sign_tot_cnt"] = row["sign_tot_cnt"].ToString();

                table.Rows.Add(_row);
            }

            return table;
        }

        internal DataTable WaterTestScheduleMatchSelect(string confirm_no)
        {
            string[] pParam = new string[1];
            pParam[0] = "@confirm_no:" + confirm_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SR", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string WaterTestScheduleTRX(List<WaterTestScheduleConfirm> waterTestSchedules
            , string gubun, string confirm_no, string start_date, string end_date)
        {
            string[] pParam = null;
            string res = "";

            int? length = waterTestSchedules?.Count;

            if (length > 0)
            {
                int cnt = 0;

                foreach (WaterTestScheduleConfirm waterTestSchedule in waterTestSchedules)
                {
                    pParam = new string[4];
                    pParam[0] = "@confirm_no:" + confirm_no;
                    pParam[1] = "@water_gb:" + waterTestSchedule.water_gb;
                    pParam[2] = "@pointer_cd:" + waterTestSchedule.point_cd;
                    pParam[3] = "@schedule_date:" + waterTestSchedule.schedule_date;

                    res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

                    if (!String.IsNullOrEmpty(res))
                    {
                        cnt++;
                    }
                }

                res = cnt + "건이 " + (gubun.Equals("IS") ? "입력" : "삭제") + "되었습니다.";
            }
            else
            {
                pParam = new string[3];
                pParam[0] = "@confirm_no:" + confirm_no;
                pParam[1] = "@start_date:" + start_date;
                pParam[2] = "@end_date:" + end_date;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);
            }

            return res;
        }

        internal string WaterTestScheduleSignTRX(WaterTestScheduleConfirm waterTestSchedule)
        {
            string sSpName = "SP_WaterTestScheduleConfirm";
            string[] pParam = null;
            string res = "";

            switch (waterTestSchedule.gubun)
            {
                case "U":

                    pParam = new string[5];
                    pParam[0] = "@emp_cd:" + waterTestSchedule.emp_cd;
                    pParam[1] = "@sign_type:" + waterTestSchedule.sign_type;
                    pParam[2] = "@confirm_no:" + waterTestSchedule.confirm_no;
                    pParam[3] = "@sign_set_cd:" + "1031";
                    pParam[4] = "@sign_set_dt_id:" + waterTestSchedule.sign_set_dt_id;

                    res = _bllSpExecute.SpExecuteString(sSpName, "ESI", pParam);

                    break;

                case "D":

                    pParam = new string[3];
                    pParam[0] = "@sign_set_cd:" + "1031";
                    pParam[1] = "@sign_set_dt_id:" + waterTestSchedule.sign_set_dt_id;
                    pParam[2] = "@confirm_no:" + waterTestSchedule.confirm_no;

                    res = _bllSpExecute.SpExecuteString(sSpName, "ESD", pParam);

                    break;
            }

            return res;
        }
    }
}