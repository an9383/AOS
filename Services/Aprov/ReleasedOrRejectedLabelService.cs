using HACCP.Libs.Database;
using HACCP.Models.Aprov;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Aprov
{
    public class ReleasedOrRejectedLabelService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ReleasedOrRejectedLabel";

        internal DataTable Select(ReleasedOrRejectedLabel model)
        {
            string sGubun = "S";
            string[] param = new string[8];

            param[0] = "@date_option:" + model.date_option;
            param[1] = "@start_date:" + model.start_date_S;
            param[2] = "@end_date:" + model.end_date_S;
            param[3] = "@test_type:" + model.s_test_type;
            param[4] = "@test_status:" + model.s_test_status;
            param[5] = "@search_gubun:" + model.search_gubun;
            param[6] = "@search_number:" + model.search_number;
            param[7] = "@s_yn:" + model.s_yn;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable S2(ReleasedOrRejectedLabel model)
        {
            string sGubun = "S2";
            string[] param = new string[1];

            param[0] = "@testcontrol_id:" + model.testcontrol_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable S3(ReleasedOrRejectedLabel model)
        {
            string sGubun = "S3";
            string[] param = new string[2];

            param[0] = "@testcontrol_id:" + model.testcontrol_id;
            param[1] = "@program_cd:" + model.program_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ReleaseOrReject_print(ReleasedOrRejectedLabel model)
        {
            string sGubun = "P";
            string[] param = new string[3];

            param[0] = "@testcontrol_id:" + model.testcontrol_id;
            param[1] = "@test_type:" + model.test_type;
            if (model.test_result_yn == "Y")
            {
                param[2] = "@form_cd:" + "ReleaseLabel";
            }
            else
            {
                param[2] = "@form_cd:" + "RejectLabel";
            }

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            param = new string[5];

            param[0] = "@testcontrol_id:" + model.testcontrol_id;
            param[1] = "@test_type:" + model.test_type;
            param[2] = "@report_cd:" + model.report_cd;
            param[3] = "@emp_cd:" + HttpContext.Current.Session["LoginCD"];
            param[4] = "@print_page_count:" + model.print_num;

            _bllSpExecute.SpExecuteString("SP_Test_Rpt_Manage", "UR", param);

            return dt;
        }

        internal bool ReleasedOrRejectedLabelCheckPrintCount(string testcontrol_id, string test_type, string report_cd)
        {
            bool check = false;

            string sGubun = "SC";
            string[] param = new string[3];

            param[0] = "@testcontrol_id:" + testcontrol_id;
            param[1] = "@test_type:" + test_type;
            param[2] = "@report_cd:" + report_cd;

            string res = _bllSpExecute.SpExecuteString("SP_Test_Rpt_Manage", sGubun, param);

            int temp = int.Parse(res);

            if (temp > 0)
                check = true;

            return check;
        }

        internal string ReleasedOrRejectedLabelGetAuthorEmp(string testcontrol_id, string test_type)
        {
            string sGubun = "SL";
            string[] param = new string[2];

            param[0] = "@testcontrol_id:" + testcontrol_id;
            param[1] = "@test_type:" + test_type;

            string res = _bllSpExecute.SpExecuteString("SP_Test_Rpt_Manage", sGubun, param);

            return res;
        }

        internal string ReleasedOrRejectedLabelAddCanPrtCnt(string testcontrol_id, string test_type, string report_cd, string emp_cd)
        {
            string sGubun = "SC";
            string[] param = new string[4];

            param[0] = "@testcontrol_id:" + testcontrol_id;
            param[1] = "@test_type:" + test_type;
            param[2] = "@report_cd:" + report_cd;
            param[3] = "@emp_cd:" + emp_cd;

            string res = _bllSpExecute.SpExecuteString("SP_Test_Rpt_Manage", sGubun, param);
            throw new NotImplementedException();
        }

        internal string ReleasedOrRejectedLabelAddCount(string testcontrol_id, string test_type, string report_cd)
        {
            string sGubun = "UA";
            string[] param = new string[3];

            param[0] = "@testcontrol_id:" + testcontrol_id;
            param[1] = "@test_type:" + test_type;
            param[2] = "@report_cd:" + report_cd;

            string res = _bllSpExecute.SpExecuteString("SP_Test_Rpt_Manage", sGubun, param);

            return res;
        }
    }
}