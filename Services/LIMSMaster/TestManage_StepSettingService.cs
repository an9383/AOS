using HACCP.Libs.Database;
using HACCP.Models.LIMSMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.LIMSMaster
{
    public class TestManage_StepSettingService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable TestManage_StepSettingSelect(string test_type, string process_kind)
        {
            if (Int32.Parse(process_kind) > 2)
            {
                DataTable emptyDT = new DataTable();

                return emptyDT;
            }

            string sSpName = "SP_TestManage_StepSetting";
            string gubun = "";

            switch (process_kind)
            {
                case "1":
                    gubun = "ST";

                    break;

                case "2":
                    gubun = "SS";

                    break;
            }

            string[] pParam = new string[1];
            pParam[0] = "@test_type:" + test_type;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectProgram()
        {
            string sSpName = "SP_TestManage_StepSetting";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "P");

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable TestManage_StepSettingSelectProgram(string test_type)
        {
            string sSpName = "SP_TestManage_StepSetting";

            string[] pParam = new string[1];
            pParam[0] = "@test_type:" + test_type;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string TestManage_StepSettingTRX(TestManageStepSetting testManageStepSetting)
        {
            string sSpName = "SP_TestManage_StepSetting";
            string res = "";
            string[] pParam = null;

            switch (testManageStepSetting.gubun)
            {
                case "U":
                case "I":
                    pParam = new string[7];
                    pParam[0] = "@test_type:" + testManageStepSetting.test_type;
                    pParam[1] = "@process_kind:" + testManageStepSetting.process_kind;
                    pParam[2] = "@curr_status:" + testManageStepSetting.curr_status;
                    pParam[3] = "@test_process_set_seq:" + testManageStepSetting.test_process_set_seq;
                    pParam[4] = "@program_cd:" + testManageStepSetting.program_cd;
                    pParam[5] = "@next_status:" + testManageStepSetting.next_status;
                    pParam[6] = "@sign_set_cd:" + testManageStepSetting.sign_set_cd;

                    break;

                case "D":
                    pParam = new string[3];
                    pParam[0] = "@test_type:" + testManageStepSetting.test_type;
                    pParam[1] = "@process_kind:" + testManageStepSetting.process_kind;
                    pParam[2] = "@curr_status:" + testManageStepSetting.curr_status;

                    break;

                case "I1":
                    pParam = new string[4];
                    pParam[0] = "@test_type:" + testManageStepSetting.test_type;
                    pParam[1] = "@process_kind:" + testManageStepSetting.process_kind;
                    pParam[2] = "@curr_status:" + testManageStepSetting.curr_status;
                    pParam[3] = "@program_cd:" + testManageStepSetting.program_cd;

                    break;

                case "U1":
                    pParam = new string[3];
                    pParam[0] = "@test_type:" + testManageStepSetting.test_type;
                    pParam[1] = "@program_cd:" + testManageStepSetting.program_cd;
                    pParam[2] = "@default_rpt_cnt:" + testManageStepSetting.default_rpt_cnt;

                    break;

                case "D1":
                    pParam = new string[2];
                    pParam[0] = "@test_type:" + testManageStepSetting.test_type;
                    pParam[1] = "@program_cd:" + testManageStepSetting.program_cd;

                    break;
            }

            res = _bllSpExecute.SpExecuteString(sSpName, testManageStepSetting.gubun, pParam);

            return res;
        }

        internal string TestManage_StepSettingCopy(string test_type, string from_test_type)
        {
            string sSpName = "SP_TestManage_StepSetting";
            string res = "";

            string[] pParam = new string[2];
            pParam[0] = "@test_type:" + test_type;
            pParam[1] = "@from_test_type:" + from_test_type;

            res = _bllSpExecute.SpExecuteString(sSpName, "C", pParam);

            return res;
        }
    }
}