using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Libs.File;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Mont
{
    public class HACCP_ProcessExam_ManageService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_ProcessExam_Manage";


        internal DataTable Select(string s_date, string e_date)
        {
            string sGubun = "S";
            string[] pParam = new string[2];

            pParam[0] = "@sdate:" + s_date;
            pParam[1] = "@edate:" + e_date;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal DataTable HACCP_ProcessExam_ManageOrderSearch(string s_date, string e_date, string workroom_cd)
        {
            string sGubun = "S1";
            string[] pParam = new string[3];

            pParam[0] = "@sdate:" + s_date;
            pParam[1] = "@edate:" + e_date;
            pParam[2] = "@workroom_cd:" + workroom_cd;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal DataTable HACCP_ProcessExam_ManageProcessSearch(string order_no, string order_proc_id)
        {
            string sGubun = "S2";
            string[] pParam = new string[2];

            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@order_proc_id:" + order_proc_id;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal DataTable HACCP_ProcessExam_ManageWorkDataSearch(string order_no, string order_proc_id, string process_exam_cd)
        {
            string sGubun = "S3";
            string[] pParam = new string[3];

            pParam[0] = "@order_no:" + order_no;
            pParam[1] = "@order_proc_id:" + order_proc_id;
            pParam[2] = "@process_exam_cd:" + process_exam_cd;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }



    }
}