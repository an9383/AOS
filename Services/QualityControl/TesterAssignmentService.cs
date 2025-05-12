using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.QualityControl
{
    public class TesterAssignmentService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_TesterAssignment";

        internal DataTable TesterAssignmentSelect(string testitem_schedule_time)
        {
            string[] pParam = new string[1];
            pParam[0] = "@testitem_schedule_time:" + testitem_schedule_time;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable TesterAssignmentTesterSelect(string testitem_schedule_time, string tester_cd)
        {
            string[] pParam = new string[2];
            pParam[0] = "@testitem_schedule_time:" + testitem_schedule_time;
            pParam[1] = "@tester_gcd:" + tester_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable TesterAssignmentTestStandardSelect(string testitem_schedule_time, string tester_cd)
        {
            string[] pParam = new string[2];
            pParam[0] = "@testitem_schedule_time:" + testitem_schedule_time;
            pParam[1] = "@tester_gcd:" + tester_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable TesterAssignmentAssignmentTesterSelect(string testitem_schedule_time, string tester_cd)
        {
            string[] pParam = new string[2];
            pParam[0] = "@testitem_schedule_time:" + testitem_schedule_time;
            pParam[1] = "@pre_tester_cd:" + tester_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S4", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable TesterAssignmentSelectScheduleDate()
        {
            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SD");

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string TesterAssignmentUpdate(string tester_cd, string testcontrol_id, string teststandardmaster_id, string gubun)
        {
            string[] pParam = new string[3];
            pParam[0] = "@pre_tester_cd:" + tester_cd;
            pParam[1] = "@testcontrol_id:" + testcontrol_id;
            pParam[2] = "@teststandardmaster_id:" + teststandardmaster_id;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }
    }
}