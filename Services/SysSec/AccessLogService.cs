using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models;
using HACCP.Models.Sp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HACCP.Services.SysSec
{
    public class AccessLogService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable selectAccesslogUser(string login_time, string logout_time)
        {
            string sSpName = "SP_AccessLog";
            string sGubun = "S1";
            string[] param = new string[2];
            param[0] = "@login_time:" + login_time;
            param[1] = "@logout_time:" + logout_time;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }


        internal DataTable selectAccesslogTime(string emp_cd, string login_time, string logout_time)
        {
            string sSpName = "SP_AccessLog";
            string sGubun = "S2";
            string[] param = new string[3];
            param[0] = "@login_time:" + login_time;
            param[1] = "@logout_time:" + logout_time;
            param[2] = "@emp_cd:" + emp_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }
        internal DataTable selectAccesslogDetail(string id)
        {
            string sSpName = "SP_AccessLog";
            string sGubun = "S3";
            string[] param = new string[1];
            param[0] = "@id:" + id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        public List<User> get()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            DateTime logout_time = new DateTime(year, month, day);
            DateTime login_time = logout_time.AddMonths(-1);

            string sSpName = "SP_AccessLog";
            string gubun = "S1";
            string[] pParam = new string[2];
            pParam[0] = "@login_time:" + login_time;
            pParam[1] = "@logout_time:" + logout_time;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            //System.Data.DataTable dt = new System.Data.DataTable();
            //using (SqlConnection sqlConn = new SqlConnection(context.GetConnStr()))
            //{
            //    string sql = "SP_AccessLog";
            //    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
            //    {
            //        sqlCmd.CommandType = CommandType.StoredProcedure;
            //        sqlCmd.Parameters.AddWithValue("@Gubun", "S1");
            //        sqlCmd.Parameters.AddWithValue("@login_time", login_time);
            //        sqlCmd.Parameters.AddWithValue("@logout_time", logout_time);
            //        sqlCmd.Parameters.AddWithValue("@sys_plant_cd", "PC001");
            //        sqlCmd.Parameters.AddWithValue("@message", "");
            //        sqlConn.Open();
            //        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
            //        {
            //            sqlAdapter.Fill(dt);
            //        }
            //    }
            //}

            List<User> userList = new List<User>();
            foreach (var row in dt.AsEnumerable())
            {
                User AccessLogUser = new User(row);

                userList.Add(AccessLogUser);
            }

            return userList;
        }


        public List<LogDetail> getDetail(int id)
        {

            string sSpName = "SP_AccessLog";
            string gubun = "S3";
            string[] pParam = new string[1];
            pParam[0] = "@id:" + id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            //System.Data.DataTable dt = new System.Data.DataTable();
            //using (SqlConnection sqlConn = new SqlConnection(context.GetConnStr()))
            //{
            //    string sql = "SP_AccessLog";
            //    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
            //    {
            //        sqlCmd.CommandType = CommandType.StoredProcedure;
            //        sqlCmd.Parameters.AddWithValue("@Gubun", "S3");
            //        sqlCmd.Parameters.AddWithValue("@id", id);
            //        sqlCmd.Parameters.AddWithValue("@sys_plant_cd", "PC001");
            //        sqlCmd.Parameters.AddWithValue("@message", "");
            //        sqlConn.Open();
            //        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
            //        {
            //            sqlAdapter.Fill(dt);
            //        }
            //    }
            //}

            List<LogDetail> list = new List<LogDetail>();

            foreach (var row in dt.AsEnumerable())
            {
                LogDetail logDetail = new LogDetail(row);

                logDetail.id = id;

                list.Add(logDetail);
            }

            return list;
        }


        internal string ProgramLogIn(string cd)
        {
            string gubun = "I2";
            string[] param = new string[4];

            param[0] = "@id:" + HttpContext.Current.Session["system_log_id"];
            param[1] = "@emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            param[2] = "@program_id:" + cd;
            param[3] = "@position_no:" + HttpContext.Current.Session["loginPosition"].ToString();

            string id = _bllSpExecute.SpExecuteString("SP_AccessLog", gubun, param);

            if (id.ToString().Length == 0)
                return "Fail";
            else
            {
                return id;
            }
        }

        internal bool ProgramLogOut(string programAuditID)
        {
            string gubun = "U2";
            string param = "";
            string message = "";

            param = "@serial:" + programAuditID;

            message = _bllSpExecute.SpExecuteString("SP_AccessLog", gubun, param);

            if (message.ToString().Length == 0)
                return false;
            else
                return true;
        }
    }
}