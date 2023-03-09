using HACCP.Libs.Database;
using HACCP.Models.TestReq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.TestReq
{
    public class TestRequestEService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_TestRequestE";

        internal DataTable TestRequestSelect(TestRequest.SrchParam srchParam)
        {
            string[] pParam = new string[8];
            pParam[0] = "@selectdate:" + srchParam.selectdate;
            pParam[1] = "@de_Sdate:" + srchParam.de_Sdate;
            pParam[2] = "@de_Edate:" + srchParam.de_Edate;
            pParam[3] = "@test_type_S:" + srchParam.test_type;
            pParam[4] = "@test_status_S:" + srchParam.test_status;
            pParam[5] = "@search_gubun:" + srchParam.search_gubun;
            pParam[6] = "@search_number:" + srchParam.search_number;
            pParam[7] = "@item_form_cd:" + srchParam.item_form_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable TestRequestSelectSign(string testcontrol_id, string test_type, string process_kind)
        {
            string sign_set_cd = GetSignSetCd(test_type);

            string[] pParam = new string[4];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@process_kind:" + process_kind;
            pParam[3] = "@sign_set_cd:" + sign_set_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_Test_ES_Manage", "SE", pParam);

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
            table.Columns.Add("necessary_yn", typeof(String));

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
                _row["necessary_yn"] = row["necessary_yn"].ToString();

                table.Rows.Add(_row);
            }

            return table;
        }

        private string GetSignSetCd(string test_type)
        {
            string sign_set_cd = "";

            switch (test_type)
            {
                case "1":
                    sign_set_cd = "1001";
                    break;
                case "2":
                    sign_set_cd = "1008";
                    break;
                case "3":
                    sign_set_cd = "1015";
                    break;
                case "6":
                    sign_set_cd = "1038";
                    break;
                case "7":
                case "8":
                case "9":
                case "40":
                    sign_set_cd = "1059";
                    break;
                case "10":
                    sign_set_cd = "1029";
                    break;
                case "20":
                    sign_set_cd = "1081";
                    break;
                case "42":
                    sign_set_cd = "1400";
                    break;
                case "17":
                    sign_set_cd = "1053";
                    break;
            }

            return sign_set_cd;
        }

        internal DataTable TestRequestSelectFile(string testcontrol_id)
        {
            string[] pParam = new string[1];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_AttachFile", "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string TestRequestETRX(TestRequest testRequest)
        {
            string[] pParam = null;

            switch (testRequest.gubun)
            {
                case "I":
                case "U":
                    pParam = GetParam(testRequest);
                    break;

                case "D":

                    pParam = new string[2];

                    pParam[0] = "@testcontrol_id:" + testRequest.testcontrol_id;
                    pParam[1] = "@test_type:" + testRequest.test_type;

                    break;
            }

            string res = _bllSpExecute.SpExecuteString(sSpName, testRequest.gubun, pParam);

            if (testRequest.gubun.Equals("I") || testRequest.gubun.Equals("U"))
            {
                res = AutoWriterSign(testRequest, res);
            }

            return res;
        }

        private string AutoWriterSign(TestRequest testRequest, string res)
        {
            string[] pParam = new string[1];
            pParam[0] = "@test_type:" + testRequest.test_type;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_TestRequestE", "SSCD", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            if (testRequest.gubun.Equals("I"))
            {
                testRequest.testcontrol_id = res;
            }

            string sign_set_cd = GetSignSetCd(testRequest.test_type);

            pParam = new string[4];
            pParam[0] = "@testcontrol_id:" + testRequest.testcontrol_id;
            pParam[1] = "@test_type:" + testRequest.test_type;
            pParam[2] = "@process_kind:" + '1';
            pParam[3] = "@sign_set_cd:" + sign_set_cd;

            ds = _bllSpExecute.SpExecuteDataSet("SP_Test_ES_Manage", "SE", pParam);

            string result = TestRequestESignTRX("U"
                , testRequest.testcontrol_id
                , "1"
                , testRequest.test_type
                , HttpContext.Current.Session["loginCD"].ToString()
                , dt.Rows[0]["sign_set_dt_id"].ToString()
                , "2"
                , "N"
                , "TestRequestE");

            return result;
        }

        private string[] GetParam(TestRequest testRequest)
        {
            string[] pParam = new string[26];

            pParam[0] = "@testcontrol_id:" + testRequest.testcontrol_id;
            pParam[1] = "@testrequest_no:" + testRequest.testrequest_no;
            pParam[2] = "@test_type:" + testRequest.test_type;
            pParam[3] = "@item_cd:" + testRequest.item_cd;
            pParam[4] = "@test_standard:" + testRequest.test_standard;
            pParam[5] = "@process_cd:" + testRequest.process_cd;
            pParam[6] = "@request_date:" + testRequest.request_date;
            pParam[7] = "@start_qty:" + testRequest.start_qty;
            pParam[8] = "@result_hope_date:" + testRequest.result_hope_date;
            pParam[9] = "@request_remark:" + testRequest.request_remark;
            pParam[10] = "@pack_cnt:" + testRequest.pack_cnt;
            pParam[11] = "@start_date:" + testRequest.start_date;
            pParam[12] = "@start_no:" + testRequest.start_no;
            pParam[13] = "@enter_seq:" + testRequest.enter_seq;
            pParam[14] = "@material_maker_cd:" + testRequest.material_maker_cd;
            pParam[15] = "@enter_no:" + testRequest.enter_no;
            pParam[16] = "@start_qty_unit:" + testRequest.start_qty_unit;
            pParam[17] = "@material_maker_nm:" + testRequest.material_maker_nm;
            pParam[18] = "@cust_cd:" + testRequest.cust_cd;
            pParam[19] = "@cust_nm:" + testRequest.cust_nm;
            pParam[20] = "@request_purpose:" + testRequest.request_purpose;
            pParam[21] = "@enter_date:" + testRequest.enter_date;
            pParam[22] = "@log_user_id:" + HttpContext.Current.Session["loginID"];
            pParam[23] = "@pack_type:" + testRequest.pack_type;
            pParam[24] = "@valid_period:" + testRequest.valid_period;
            pParam[25] = "@gmo_yn:" + testRequest.gmo_yn;

            return pParam;
        }

        internal string TestRequestDeleteTestResult(string testcontrol_id, string testrequest_no, string process_cd)
        {
            string[] pParam = new string[3];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@testrequest_no:" + testrequest_no;
            pParam[2] = "@process_cd:" + process_cd;

            string res = _bllSpExecute.SpExecuteString(sSpName, "UpdateOrderProcToCancel", pParam);

            return res;
        }

        internal string TestRequestESignDelegateCheck(string testcontrol_id, string process_kind, string test_type, string emp_cd, string sign_set_dt_id, string program_cd)
        {
            string sign_set_cd = GetSignSetCd(test_type);

            string[] pParam = new string[6];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@process_kind:" + process_kind;
            pParam[2] = "@sign_set_cd:" + sign_set_cd;
            pParam[3] = "@emp_cd:" + emp_cd;
            pParam[4] = "@sign_set_dt_id:" + sign_set_dt_id;
            pParam[5] = "@program_cd:" + program_cd;

            string res = _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", "SR", pParam);

            return res;
        }

        internal string TestRequestESignTRX(string gubun, string testcontrol_id, string process_kind, string test_type, string emp_cd, string sign_set_dt_id, string validation_type, string representative_yn, string program_cd)
        {
            string sign_set_cd = GetSignSetCd(test_type);
            string pGubun = "";

            switch (gubun)
            {
                case "U":

                    pGubun = "EI";

                    break;

                case "D":

                    if (sign_set_dt_id.Equals("1"))
                    {
                        pGubun = "ED";
                    }
                    else
                    {
                        pGubun = "SignCancel";
                    }

                    break;
            }

            string[] pParam = new string[9];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@process_kind:" + process_kind;
            pParam[2] = "@sign_set_cd:" + sign_set_cd;
            pParam[3] = "@emp_cd:" + emp_cd;
            pParam[4] = "@sign_set_dt_id:" + sign_set_dt_id;
            pParam[5] = "@validation_type:" + validation_type;
            pParam[6] = "@representative_yn:" + representative_yn;
            pParam[7] = "@test_type:" + test_type;
            pParam[8] = "@program_cd:" + program_cd;

            string res = _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", pGubun, pParam);

            return res;
        }

        internal void uploadFile(byte[] myBytes, string fileName, string contentType, string testcontrol_id)
        {
            var doc_type = "";

            if (contentType.Equals("application/haansofthwp"))
            {
                doc_type = ".hwp";
            }
            else if (contentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
            {
                doc_type = ".docx";
            }
            else if (contentType.Equals("application/msword"))
            {
                doc_type = ".doc";
            }

            string sSpName = "SP_AttachFile";

            string[] pParam = new string[3];
            pParam[0] = "@doc_name:" + fileName;
            pParam[1] = "@doc_type:" + doc_type;
            pParam[2] = "@testcontrol_id:" + testcontrol_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, "I", "@file_image", myBytes, pParam);
        }


        internal DataTable TestRequestEDownloadFile(string testcontrol_id, string file_id)
        {
            string sSpName = "SP_AttachFile";
            string sGubun = "O";

            string[] pParam = new string[2];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@file_id:" + file_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal string TestRequestEDeleteFile(string testcontrol_id, string file_id)
        {
            string sSpName = "SP_AttachFile";

            string[] pParam = new string[2];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@file_id:" + file_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, "D", pParam);

            return message;
        }

        internal string TestRequestEUpdateTestStandard(string testcontrol_id)
        {
            string[] pParam = new string[1];

            pParam[0] = "@testcontrol_id:" + testcontrol_id;

            string res = _bllSpExecute.SpExecuteString(sSpName, "UpdateTestStandard", pParam);

            if (res.Equals("데이터베이스 처리"))
            {
                res = "등록된 규격이 없습니다. \n시험 규격을 확인하세요.";
            }

            return res;
        }
    }
}