using HACCP.Libs.Database;
using HACCP.Models.QualityControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.QualityControl
{
    public class RDMSFileDownloadService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_TestScheduleFile";

        internal DataTable RDMSFileDownloadSelect(RDMSFileDownload.SrchParam srchParam)
        {
            string[] param = new string[9];
            param[0] = "@s_date_option:" + srchParam.date_option;
            param[1] = "@s_start_date:" + srchParam.start_date;
            param[2] = "@s_end_date:" + srchParam.end_date;
            param[3] = "@s_test_type:" + srchParam.test_type;
            param[4] = "@s_test_status:" + srchParam.test_status;
            param[5] = "@search_number0:" + srchParam.search_number0;
            param[6] = "@search_number" + srchParam.search_gubun + ":" + srchParam.search_number;
            param[7] = "@test_emp_cd:" + srchParam.test_emp_cd;
            param[8] = "@item_form_cd:" + srchParam.item_form_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(this.sSpName, "S", param);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        internal DataTable RDMSFileDownloadTestDetailSelect(string testcontrol_id)
        {
            string[] param = new string[1];
            param[0] = "@testcontrol_id:" + testcontrol_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(this.sSpName, "S3", param);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        internal DataTable RDMSFileDownloadTestFileSelect(string testcontrol_id, string teststandardmaster_id)
        {
            string[] param = new string[2];
            param[0] = "@testcontrol_id:" + testcontrol_id;
            param[1] = "@teststandardmaster_id:" + teststandardmaster_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(this.sSpName, "SF", param);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        internal DataTable RDMSFileDownloadTestFileDownload(string testcontrol_id, string teststandardmaster_id, string doc_file_id)
        {
            string[] param = new string[3];
            param[0] = "@testcontrol_id:" + testcontrol_id;
            param[1] = "@teststandardmaster_id:" + teststandardmaster_id;
            param[2] = "@doc_file_id:" + doc_file_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(this.sSpName, "DF", param);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}