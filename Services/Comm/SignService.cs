using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Comm
{
    public class SignService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        
        //서명의미 조회
        internal DataTable GetSignSetNm(string sign_set_cd)
        {
            string[] param = new string[1];
            param[0] = "@sign_set_cd:" + sign_set_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable("SP_SignCommon", "GetSignSetNm", param);

            return dt;
        }


        //서명리스트 조회
        internal DataTable getSignList(string sign_set_cd, string index_key)
        {
            string[] param = new string[2];
            param[0] = "@sign_set_cd:" + sign_set_cd;
            param[1] = "@index_key:" + index_key;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_SignCommon", "GetSignList", param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();

            table.Columns.Add("sign_set_nm", typeof(String));    //서명의미
            table.Columns.Add("sign_set_dt_nm", typeof(String)); //서명라인 이름
            table.Columns.Add("sign_set_dt_id", typeof(String)); //서명라인 id
            table.Columns.Add("sign_set_dt_seq", typeof(String));//서명라인 seq
            table.Columns.Add("sign_set_cd", typeof(String));//서명라인 seq

            table.Columns.Add("sign_emp_cd", typeof(String));    //서명자 코드
            table.Columns.Add("sign_emp_nm", typeof(String));    //서명자 이름
            table.Columns.Add("sign_time", typeof(String));      //서명시간
            table.Columns.Add("sign_image", typeof(String));     //서명이미지

            table.Columns.Add("necessary_yn", typeof(String));     //필수서명여부
            table.Columns.Add("representative_yn", typeof(String));//대리서명가능여부
            table.Columns.Add("sign_representative_yn", typeof(String));//대리서명했는지 확인

            table.Columns.Add("sign_yn", typeof(String));        //서명여부
            table.Columns.Add("prev_sign_yn", typeof(String));   //전단계 서명여부
            table.Columns.Add("next_sign_yn", typeof(String));   //다음단계 서명여부

            table.Columns.Add("index_key", typeof(String));      //서명에 대한 key값
            table.Columns.Add("sign_history_id", typeof(String));//개별서명에 대한 key값

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["sign_set_nm"] = row["sign_set_nm"].ToString();
                _row["sign_set_dt_nm"] = row["sign_set_dt_nm"].ToString();
                _row["sign_set_dt_id"] = row["sign_set_dt_id"].ToString();
                _row["sign_set_dt_seq"] = row["sign_set_dt_seq"].ToString();
                _row["sign_set_cd"] = row["sign_set_cd"].ToString();

                _row["sign_emp_cd"] = row["sign_emp_cd"].ToString();
                _row["sign_emp_nm"] = row["sign_emp_nm"].ToString();
                _row["sign_time"] = row["sign_time"].ToString();
                if (row["sign_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["sign_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["sign_image"] = url;
                }
                else
                {
                    string url = "/Content/image/defaultSign.png";
                    _row["sign_image"] = url;
                }

                _row["necessary_yn"] = row["necessary_yn"].ToString();
                _row["representative_yn"] = row["representative_yn"].ToString();
                _row["sign_representative_yn"] = row["sign_representative_yn"].ToString();

                _row["sign_yn"] = row["sign_yn"].ToString();
                _row["prev_sign_yn"] = row["prev_sign_yn"].ToString();
                _row["next_sign_yn"] = row["next_sign_yn"].ToString();

                _row["index_key"] = row["index_key"].ToString();
                _row["sign_history_id"] = row["sign_history_id"].ToString();

                table.Rows.Add(_row);
            }

            return table;
        }

        //서명 권한 조회
        internal DataTable CheckAuthority(string sign_emp_cd, string sign_set_cd, string sign_set_dt_id)
        {
            string[] param = new string[3];
            param[0] = "@sign_emp_cd:" + sign_emp_cd;
            param[1] = "@sign_set_cd:" + sign_set_cd;
            param[2] = "@sign_set_dt_id:" + sign_set_dt_id;

            DataTable dt = _bllSpExecute.SpExecuteTable("SP_SignCommon", "CheckAuthority", param);

            return dt;
        }

        //서명 입력
        internal string InsertSignList(string sign_set_cd, string index_key)
        {
            string[] param = new string[2];
            param[0] = "@sign_set_cd:" + sign_set_cd;
            param[1] = "@index_key:" + index_key;
            string res = _bllSpExecute.SpExecuteString("SP_SignCommon", "InsertSignList", param);

            return res;
        }

        internal string DeleteSignList(string sign_set_cd, string index_key)
        {
            string[] param = new string[2];
            param[0] = "@sign_set_cd:" + sign_set_cd;
            param[1] = "@index_key:" + index_key;
            string res = _bllSpExecute.SpExecuteString("SP_SignCommon", "DeleteSignList", param);

            return res;
        }

        //서명 저장
        internal string SaveSign(string sign_emp_cd, string sign_type, string sign_representative_yn, string index_key, string sign_history_id)

        {
            string[] param = new string[5];
            param[0] = "@sign_emp_cd:" + sign_emp_cd;
            param[1] = "@sign_type:" + sign_type;
            param[2] = "@sign_representative_yn:" + sign_representative_yn;
            param[3] = "@index_key:" + index_key;
            param[4] = "@sign_history_id:" + sign_history_id;

            string res = _bllSpExecute.SpExecuteString("SP_SignCommon", "SaveSign", param);

            return res;
        }

        //서명 취소
        internal string CancelSign(string index_key, string sign_history_id)
        {
            string[] param = new string[2];
            param[0] = "@index_key:" + index_key;
            param[1] = "@sign_history_id:" + sign_history_id;

            string res = _bllSpExecute.SpExecuteString("SP_SignCommon", "CancelSign", param);

            return res;
        }


    }
}