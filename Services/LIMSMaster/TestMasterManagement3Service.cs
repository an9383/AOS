using HACCP.Libs.Database;
using HACCP.Models.LIMSMaster;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.LIMSMaster
{
    public class TestMasterManagement3Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable TestMasterManagement3Select(TestMasterManagement.SrchParam testMasterManagement)
        {
            string sSpName = "SP_TestMasterManagement3";

            string[] pParam = new string[4];
            pParam[0] = "@test_type:" + testMasterManagement.test_type;
            pParam[1] = "@item_cd:" + testMasterManagement.item_cd;
            pParam[2] = "@necessary_check:" + testMasterManagement.necessary_check;
            pParam[3] = "@item_gb:" + testMasterManagement.item_gb;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable TestMasterManagement3SelectRevisionNo(TestMasterManagement.SrchParam testMasterManagement)
        {
            string sSpName = "SP_TestMasterManagement3";

            string[] pParam = new string[4];
            pParam[0] = "@test_type:" + testMasterManagement.test_type;
            pParam[1] = "@item_cd:" + testMasterManagement.item_cd;
            pParam[2] = "@test_standard:" + testMasterManagement.test_standard;
            pParam[3] = "@process_cd:" + testMasterManagement.process_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SR", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string TestMasterManagement3TRX(TestMasterManagement testMasterManagement)
        {
            string sSpName = "SP_TestMasterManagement3";
            string res = "";

            switch (testMasterManagement.gubun)
            {
                case "C1":

                    string[] pParam = new string[5];
                    pParam[0] = "@test_type:" + testMasterManagement.test_type;
                    pParam[1] = "@item_cd:" + testMasterManagement.item_cd;
                    pParam[2] = "@test_standard:" + testMasterManagement.test_standard;
                    pParam[3] = "@process_cd:" + testMasterManagement.process_cd;
                    pParam[4] = "@revision_no:" + testMasterManagement.revision_no;

                    res = _bllSpExecute.SpExecuteString(sSpName, testMasterManagement.gubun, pParam);

                    break;

                case "U1":

                    pParam = GetParam(testMasterManagement);

                    res = _bllSpExecute.SpExecuteString(sSpName, testMasterManagement.gubun, pParam);

                    break;
                case "D1":

                    pParam = new string[5];
                    pParam[0] = "@test_type:" + testMasterManagement.test_type;
                    pParam[1] = "@item_cd:" + testMasterManagement.item_cd;
                    pParam[2] = "@test_standard:" + testMasterManagement.test_standard;
                    pParam[3] = "@process_cd:" + testMasterManagement.process_cd;
                    pParam[4] = "@revision_no:" + testMasterManagement.revision_no;

                    res = _bllSpExecute.SpExecuteString(sSpName, testMasterManagement.gubun, pParam);

                    break;
            }

            return res;
        }

        internal DataSet TestMasterManagement3SelectDetail(TestMasterManagement.SrchParam testMasterManagement)
        {
            string sSpName = "SP_TestMasterManagement3";

            string[] pParam = new string[5];
            pParam[0] = "@test_type:" + testMasterManagement.test_type;
            pParam[1] = "@item_cd:" + testMasterManagement.item_cd;
            pParam[2] = "@test_standard:" + testMasterManagement.test_standard;
            pParam[3] = "@process_cd:" + testMasterManagement.process_cd;
            pParam[4] = "@revision_no:" + testMasterManagement.revision_no;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            return ds;
        }

        internal DataTable TestMasterManagement3SelectRevisionHistory(TestMasterManagement.SrchParam testMasterManagement)
        {
            string sSpName = "SP_TestMasterManagement3";

            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + testMasterManagement.item_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "HISTORY", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable TestMasterManagement3SelectSignData(TestMasterManagement.SrchParam testMasterManagement)
        {
            string sSpName = "SP_TestMasterManagement3";

            string[] pParam = new string[2];
            pParam[0] = "@testmaster_id:" + testMasterManagement.testmaster_id;
            pParam[1] = "@sign_set_cd:" + "1410";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SE", pParam);

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

                table.Rows.Add(_row);
            }

            return table;
        }

        internal DataTable TestMasterManagement3SelectCommonStandard(string testmaster_id)
        {
            string sSpName = "SP_common_standard";

            string[] pParam = new string[1];
            pParam[0] = "@testmaster_id:" + testmaster_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string TestMasterManagement3TRX(List<TestMasterManagement> testMasterManagements)
        {
            //string sSpName = "SP_TestMasterManagement3";
            string res = "";

            foreach (TestMasterManagement testMasterManagement in testMasterManagements)
            {
                //switch (testMasterManagement.gubun)
                //{

                //}

            }

            return res;
        }

        internal DataTable TestMasterManagement3SelectFileData(string testmaster_id)
        {
            string sSpName = "SP_AttachFileDoc";

            string[] pParam = new string[1];
            pParam[0] = "@testmaster_id:" + testmaster_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        private string[] GetParam(TestMasterManagement testMasterManagement)
        {

            string[] pParam = new string[27];
            pParam[0] = "@testmaster_id:" + testMasterManagement.testmaster_id;
            pParam[1] = "@test_type:" + testMasterManagement.test_type;
            pParam[2] = "@item_cd:" + testMasterManagement.item_cd;
            pParam[3] = "@test_standard:" + testMasterManagement.test_standard;
            pParam[4] = "@test_standard_1:" + testMasterManagement.test_standard_1;
            pParam[5] = "@test_standard_2:" + testMasterManagement.test_standard_2;
            pParam[6] = "@test_standard_3:" + testMasterManagement.test_standard_3;
            pParam[7] = "@test_standard_4:" + testMasterManagement.test_standard_4;
            pParam[8] = "@test_standard_5:" + testMasterManagement.test_standard_5;
            pParam[9] = "@process_cd:" + testMasterManagement.process_cd;
            pParam[10] = "@revision_no:" + testMasterManagement.revision_no;
            pParam[11] = "@testmaster_remark:" + testMasterManagement.testmaster_remark;
            pParam[12] = "@testmaster_comment:" + testMasterManagement.testmaster_comment;
            pParam[13] = "@current_revision_yn:" + testMasterManagement.current_revision_yn;
            pParam[14] = "@revision_date:" + testMasterManagement.revision_date;
            pParam[15] = "@revision_doc_no:" + testMasterManagement.revision_doc_no;
            pParam[16] = "@sample_qty:" + testMasterManagement.sample_qty;
            pParam[17] = "@sample_unit:" + testMasterManagement.sample_unit;
            pParam[18] = "@sample_test_qty:" + testMasterManagement.sample_test_qty;
            pParam[19] = "@sample_microbe_qty:" + testMasterManagement.sample_microbe_qty;
            pParam[20] = "@sample_deposit_qty:" + testMasterManagement.sample_deposit_qty;
            pParam[21] = "@avg_sampling_qty:" + testMasterManagement.avg_sampling_qty;
            pParam[22] = "@sample_deposit_place_cd:" + testMasterManagement.sample_deposit_place_cd;
            pParam[23] = "@stability_qty:" + testMasterManagement.stability_qty;
            pParam[24] = "@stability_place_cd:" + testMasterManagement.stability_place_cd;
            pParam[25] = "@test_term:" + testMasterManagement.test_term;
            pParam[26] = "@test_emp_cd:" + testMasterManagement.test_emp_cd;

            return pParam;
        }

        internal void uploadFile(byte[] myBytes, string fileName, string contentType, string testmaster_id)
        {
            string doc_type = MimeTypeMap.GetExtension(contentType);

            //if (contentType.Equals("application/haansofthwp"))
            //{
            //    doc_type = ".hwp";
            //}
            //else if (contentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
            //{
            //    doc_type = ".docx";
            //}
            //else if (contentType.Equals("application/msword"))
            //{
            //    doc_type = ".doc";
            //}

            string sSpName = "SP_AttachFileDoc";

            string[] pParam = new string[3];
            pParam[0] = "@doc_name:" + fileName;
            pParam[1] = "@doc_type:" + doc_type;
            pParam[2] = "@testmaster_id:" + testmaster_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, "I", "@file_image", myBytes, pParam);
        }

        internal DataTable TestMasterManagementSelectTestCriteria()
        {
            string sSpName = "SP_TestMasterManagement3";

            string[] pParam = new string[2];
            pParam[0] = "@testitem_type:" + "%";
            pParam[1] = "@testitem_cd:" + "";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S4", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable AttachmentFileDownload(string testmaster_id, string worksheet_file_id)
        {
            string sSpName = "SP_AttachFileDoc";
            string sGubun = "O";

            string[] pParam = new string[2];
            pParam[0] = "@testmaster_id:" + testmaster_id;
            pParam[1] = "@worksheet_file_id:" + worksheet_file_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string TestMasterManagementDeleteDoc(string testmaster_id, string worksheet_file_id)
        {
            string sSpName = "SP_AttachFileDoc";
            string sGubun = "D";

            string[] pParam = new string[2];
            pParam[0] = "@testmaster_id:" + testmaster_id;
            pParam[1] = "@worksheet_file_id:" + worksheet_file_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string TestMasterManagementSignTRX(TestMasterManagement.SignParam signParam)
        {
            string sSpName = "SP_TestMasterManagement3";
            string[] pParam = null;
            string res = "";

            switch (signParam.gubun)
            {
                case "U":

                    pParam = new string[5];
                    pParam[0] = "@emp_cd:" + signParam.emp_cd;
                    pParam[1] = "@sign_type:" + signParam.sign_type;
                    pParam[2] = "@index_key:" + signParam.index_key;
                    pParam[3] = "@sign_set_cd:" + signParam.sign_set_cd;
                    pParam[4] = "@sign_set_dt_id:" + signParam.sign_set_dt_id;

                    res = _bllSpExecute.SpExecuteString(sSpName, "ESI", pParam);

                    break;

                case "D":

                    pParam = new string[3];
                    pParam[0] = "@sign_set_cd:" + signParam.sign_set_cd;
                    pParam[1] = "@sign_set_dt_id:" + signParam.sign_set_dt_id;
                    pParam[2] = "@index_key:" + signParam.index_key;

                    res = _bllSpExecute.SpExecuteString(sSpName, "ESD", pParam);

                    break;
            }

            return res;

        }

        internal string TestMasterManagementTestCriteriaTRX(TestMasterManagement testMasterManagement)
        {
            string sSpName = "SP_TestMasterManagement3";
            string[] pParam = null;
            string res = "";

            switch (testMasterManagement.addtype)
            {
                case "A":
                case "B":
                case "C":

                    pParam = new string[8];
                    pParam[0] = "@test_type:" + testMasterManagement.test_type;
                    pParam[1] = "@item_cd:" + testMasterManagement.item_cd;
                    pParam[2] = "@test_standard:" + testMasterManagement.test_standard;
                    pParam[3] = "@process_cd:" + testMasterManagement.process_cd;
                    pParam[4] = "@revision_no:" + testMasterManagement.revision_no;
                    pParam[5] = "@teststandardmaster_id:" + testMasterManagement.teststandardmaster_id;
                    pParam[6] = "@testitem_cd:" + testMasterManagement.testitem_cd;
                    pParam[7] = "@addtype:" + testMasterManagement.addtype;

                    res = _bllSpExecute.SpExecuteString(sSpName, "C2", pParam);

                    break;

                case "D":

                    pParam = new string[2];
                    pParam[0] = "@testmaster_id:" + testMasterManagement.testmaster_id;
                    pParam[1] = "@teststandardmaster_id:" + testMasterManagement.teststandardmaster_id;

                    res = _bllSpExecute.SpExecuteString(sSpName, "D3", pParam);

                    break;

                case "UP":

                    pParam = new string[2];
                    pParam[0] = "@testmaster_id:" + testMasterManagement.testmaster_id;
                    pParam[1] = "@teststandardmaster_id:" + testMasterManagement.teststandardmaster_id;

                    res = _bllSpExecute.SpExecuteString(sSpName, "UP", pParam);

                    break;

                case "DOWN":

                    pParam = new string[2];
                    pParam[0] = "@testmaster_id:" + testMasterManagement.testmaster_id;
                    pParam[1] = "@teststandardmaster_id:" + testMasterManagement.teststandardmaster_id;

                    res = _bllSpExecute.SpExecuteString(sSpName, "DOWN", pParam);

                    break;
            }

            return res;
        }

        internal string TestMasterManagement3CriteriaTRX(List<TestMasterManagement.TestCriteria> testMasterManagements)
        {
            string sSpName = "SP_TestMasterManagement3";
            string[] pParam = null;
            string res = "";

            foreach (TestMasterManagement.TestCriteria testCriteria in testMasterManagements)
            {

                switch (testCriteria.gubun)
                {
                    case "I":
                        
                        string testitem_cd = _bllSpExecute.SpExecuteString(sSpName, "S5");

                        pParam = new string[2];
                        pParam[0] = "@testitem_cd:" + testitem_cd;
                        pParam[1] = "@testitem_nm:" + testCriteria.testitem_nm;

                        _bllSpExecute.SpExecuteString("SP_Testitemmaster", "I2", pParam);

                        testCriteria.testitem_cd = testitem_cd;

                        res = _bllSpExecute.SpExecuteString(sSpName, "C2", GetParam2(testCriteria));

                        break;

                    case "U":

                        pParam = GetParam2(testCriteria);

                        res = _bllSpExecute.SpExecuteString(sSpName, "U2", pParam);

                        break;

                    case "D":

                        pParam = new string[2];
                        pParam[0] = "@testmaster_id:" + testCriteria.testmaster_id;
                        pParam[1] = "@teststandardmaster_id:" + testCriteria.teststandardmaster_id;

                        res = _bllSpExecute.SpExecuteString(sSpName, "D3", pParam);

                        break;
                }
            }

            return res;
        }
        internal string TestMasterManagementTestItemUpdate(string testitem_cd, string testitem_nm)
        {
            string sSpName = "SP_Testitemmaster";
            string sGubun = "U2";

            string[] pParam = new string[2];
            pParam[0] = "@testitem_cd:" + testitem_cd;
            pParam[1] = "@testitem_nm:" + testitem_nm;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        private string[] GetParam2(TestMasterManagement.TestCriteria testCriteria)
        {
            string[] pParam = new string[33];

            string contents_yn = String.IsNullOrEmpty(testCriteria.contents_yn) ? "N" : ((testCriteria.contents_yn.Equals("Y") || testCriteria.contents_yn.Equals("true")) ? "Y" : "N");
            string dual_data_yn = String.IsNullOrEmpty(testCriteria.dual_data_yn) ? "N" : ((testCriteria.dual_data_yn.Equals("Y") || testCriteria.dual_data_yn.Equals("true")) ? "Y" : "N");
            string stability_test_yn = String.IsNullOrEmpty(testCriteria.stability_test_yn) ? "N" : ((testCriteria.stability_test_yn.Equals("Y") || testCriteria.stability_test_yn.Equals("true")) ? "Y" : "N");
            string fix_content_yn = String.IsNullOrEmpty(testCriteria.fix_content_yn) ? "N" : ((testCriteria.fix_content_yn.Equals("Y") || testCriteria.fix_content_yn.Equals("true")) ? "Y" : "N");

            pParam[0] = "@testmaster_id:" + testCriteria.testmaster_id;
            pParam[1] = "@teststandardmaster_id:" + testCriteria.teststandardmaster_id;
            pParam[2] = "@teststandard_nm:" + testCriteria.teststandard_nm;
            pParam[3] = "@teststandard_enm:" + testCriteria.teststandard_enm;
            pParam[4] = "@teststandard_type:" + testCriteria.teststandard_type;
            pParam[5] = "@teststandard_min:" + testCriteria.teststandard_min;
            pParam[6] = "@teststandard_max:" + testCriteria.teststandard_max;
            pParam[7] = "@testresult_data_type:" + testCriteria.testresult_data_type;
            pParam[8] = "@testitem_trier:" + testCriteria.testitem_trier;
            pParam[9] = "@testitem_totaltime:" + testCriteria.testitem_totaltime;
            pParam[10] = "@testitem_inputtime:" + testCriteria.testitem_inputtime;
            pParam[11] = "@testitem_remark:" + testCriteria.testitem_remark;
            pParam[12] = "@testresult_example:" + testCriteria.testresult_example;
            pParam[13] = "@teststandard_validpoint:" + testCriteria.teststandard_validpoint;
            pParam[14] = "@sample_qty:" + testCriteria.sample_qty;
            pParam[15] = "@teststandard_nm_self:" + testCriteria.teststandard_nm_self;
            pParam[16] = "@teststandard_min_self:" + testCriteria.teststandard_min_self;
            pParam[17] = "@teststandard_max_self:" + testCriteria.teststandard_max_self;
            pParam[18] = "@contents_yn:" + contents_yn;
            pParam[19] = "@dual_data_yn:" + dual_data_yn;
            pParam[20] = "@tester_cd:" + testCriteria.tester_cd;
            pParam[21] = "@tester_nm:" + testCriteria.tester_nm;
            pParam[22] = "@statutory_standard:" + testCriteria.statutory_standard;
            pParam[23] = "@stability_test_yn:" + stability_test_yn;
            pParam[24] = "@fix_content_yn:" + fix_content_yn;
            pParam[25] = "@fix_content_rate:" + testCriteria.fix_content_rate;
            pParam[26] = "@test_type:" + testCriteria.test_type;
            pParam[27] = "@item_cd:" + testCriteria.item_cd;
            pParam[28] = "@test_standard:" + testCriteria.test_standard;
            pParam[29] = "@process_cd:" + testCriteria.process_cd;
            pParam[30] = "@revision_no:" + testCriteria.revision_no;
            pParam[31] = "@testitem_cd:" + testCriteria.testitem_cd;
            pParam[32] = "@addtype:" + testCriteria.addtype;

            return pParam;
        }

        internal string TestMasterManagement3CommonSpecificationTRX(List<TestMasterManagement.CommonSpecification> commonSpecifications)
        {
            string sSpName = "SP_common_standard";
            string[] pParam = null;
            string res = "";

            foreach (TestMasterManagement.CommonSpecification commonSpecification in commonSpecifications)
            {

                switch (commonSpecification.gubun)
                {
                    case "I":

                        pParam = new string[5];
                        pParam[0] = "@testmaster_id:" + commonSpecification.testmaster_id;
                        pParam[1] = "@test_type:" + commonSpecification.test_type;
                        pParam[2] = "@item_cd:" + commonSpecification.item_cd;
                        pParam[3] = "@test_standard:" + commonSpecification.test_standard;
                        pParam[4] = "@process_cd:" + commonSpecification.process_cd;

                        res = _bllSpExecute.SpExecuteString(sSpName, "I", pParam);

                        break;

                    case "U":

                        pParam = new string[6];
                        pParam[0] = "@testmaster_id:" + commonSpecification.testmaster_id;
                        pParam[1] = "@common_standard_id:" + commonSpecification.common_standard_id;
                        pParam[2] = "@test_type:" + commonSpecification.test_type;
                        pParam[3] = "@item_cd:" + commonSpecification.item_cd;
                        pParam[4] = "@test_standard:" + commonSpecification.test_standard;
                        pParam[5] = "@process_cd:" + commonSpecification.process_cd;

                        res = _bllSpExecute.SpExecuteString(sSpName, "U", pParam);

                        break;

                    case "D":

                        pParam = new string[2];
                        pParam[0] = "@testmaster_id:" + commonSpecification.testmaster_id;
                        pParam[1] = "@common_standard_id:" + commonSpecification.common_standard_id;

                        res = _bllSpExecute.SpExecuteString(sSpName, "D", pParam);

                        break;
                }
            }

            return res;
        }

        internal DataTable TestMasterManagementSelectExistingSpecification(string item_cd)
        {
            string sSpName = "SP_TestMasterManagement3";
            string sGubun = "SI";

            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable TestMasterManagementSelectExistingSpecificationDetail(string testmaster_id)
        {
            string sSpName = "SP_TestMasterManagement3";
            string sGubun = "SID";

            string[] pParam = new string[1];
            pParam[0] = "@testmaster_id:" + testmaster_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable TestMasterManagementSelectExternalSpecification(string item_cd)
        {
            string sSpName = "SP_TestMasterManagement3";
            string sGubun = "SO";

            string[] pParam = new string[1];
            pParam[0] = "@item_cd:" + item_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable TestMasterManagementSelectExternalSpecificationDetail(string testmaster_id)
        {
            string sSpName = "SP_TestMasterManagement3";
            string sGubun = "SOD";

            string[] pParam = new string[1];
            pParam[0] = "@testmaster_id:" + testmaster_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string TestMasterManagementExistingSpecificationCopy(TestMasterManagement.SpecificationCopyParam specificationCopyParam)
        {
            string sSpName = "SP_TestMasterManagement3";
            string sGubun = "COPY";

            string[] pParam = new string[9];
            pParam[0] = "@test_type:" + specificationCopyParam.test_type;
            pParam[1] = "@item_cd:" + specificationCopyParam.item_cd;
            pParam[2] = "@test_standard:" + specificationCopyParam.test_standard;
            pParam[3] = "@process_cd:" + specificationCopyParam.process_cd;
            pParam[4] = "@revision_no:" + specificationCopyParam.revision_no;
            pParam[5] = "@copy_test_type:" + specificationCopyParam.copy_test_type;
            pParam[6] = "@copy_item_cd:" + specificationCopyParam.copy_item_cd;
            pParam[7] = "@copy_test_standard:" + specificationCopyParam.copy_test_standard;
            pParam[8] = "@copy_process_cd:" + specificationCopyParam.copy_process_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal string TestMasterManagementExternalSpecificationCopy(TestMasterManagement.SpecificationCopyParam specificationCopyParam)
        {
            string sSpName = "SP_TestMasterManagement3";
            string sGubun = "COPY_OUTER";

            string[] pParam = new string[1];
            pParam[0] = "@copy_testmaster_id:" + specificationCopyParam.copy_testmaster_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "COPY_CHECK", pParam);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["testitem_nm"].ToString() + " 시험항목이 존재하지 않습니다!!!";
            }

            pParam = new string[6];
            pParam[0] = "@test_type:" + specificationCopyParam.test_type;
            pParam[1] = "@item_cd:" + specificationCopyParam.item_cd;
            pParam[2] = "@test_standard:" + specificationCopyParam.test_standard;
            pParam[3] = "@process_cd:" + specificationCopyParam.process_cd;
            pParam[4] = "@revision_no:" + specificationCopyParam.revision_no;
            pParam[5] = "@copy_testmaster_id:" + specificationCopyParam.copy_testmaster_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string TestMasterManagementUseYn(TestMasterManagement.SrchParam param)
        {
            string sSpName = "SP_TestMasterManagement3";
            string gubun = "";

            switch (param.gubun)
            {
                case "Y":
                    gubun = "U_C";
                    break;

                case "N":
                    gubun = "U_CR";
                    break;
            }

            string[] pParam = new string[5];
            pParam[0] = "@test_type:" + param.test_type;
            pParam[1] = "@item_cd:" + param.item_cd;
            pParam[2] = "@test_standard:" + param.test_standard;
            pParam[3] = "@process_cd:" + param.process_cd;
            pParam[4] = "@revision_no:" + param.revision_no;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }

    }
}