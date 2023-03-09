using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HACCP.Services.Comm
{
    public class MenuService
    {
        
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        public List<News> GetNews(string emp_cd)
        {

            string sSpName = "SP_MainManage";
            string gubun = "N";
            string[] pParam = new string[1];
            pParam[0] = "@emp_cd:" + emp_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            //DataTable dt = new DataTable();
            //using (SqlConnection sqlConn = new SqlConnection(context.GetConnStr()))
            //{
            //    string sql = "SP_MainManage";
            //    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
            //    {
            //        sqlCmd.CommandType = CommandType.StoredProcedure;

            //        sqlCmd.Parameters.AddWithValue("@Gubun", "N");
            //        sqlCmd.Parameters.AddWithValue("@emp_cd", emp_cd);
            //        sqlCmd.Parameters.AddWithValue("@sys_plant_cd", "PC001");
            //        sqlCmd.Parameters.AddWithValue("@message", "");

            //        sqlConn.Open();
            //        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
            //        {
            //            sqlAdapter.Fill(dt);
            //        }
            //    }
            //}

            List<News> list = new List<News>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];

                var temp = new News(row);

                list.Add(temp);
            }

            return list;
        }

        public DataSet GetMain(string emp_cd)
        {

            string sSpName = "SP_MainManage";
            string gubun = "N";
            string[] pParam = new string[1];
            pParam[0] = "@emp_cd:" + emp_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);
            return ds;
        }

        public DataTable GetMain_LIMS(string emp_cd)
        {

            string sSpName = "SP_LimsAlarm";
            string gubun = "GN";
            string[] pParam = new string[1];
            pParam[0] = "@emp_cd:" + emp_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);
            return dt;
        }


        public List<Menu> GetMenu(string emp_cd)
        {

            string sSpName = "SP_Menu_GetMenu";
            string gubun = "T";
            string[] pParam = new string[1];
            pParam[0] = "@emp_cd:" + emp_cd;

            DataTable dt = _bllSpExecute.SpExecuteTableWithoutMessage(sSpName, gubun, pParam);

            //DataTable dt = new DataTable();
            //using (SqlConnection sqlConn = new SqlConnection(context.GetConnStr()))
            //{
            //    string sql = "SP_Menu_GetMenu";
            //    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
            //    {
            //        sqlCmd.CommandType = CommandType.StoredProcedure;

            //        sqlCmd.Parameters.AddWithValue("@Gubun", "T");
            //        sqlCmd.Parameters.AddWithValue("@emp_cd", emp_cd);
            //        sqlCmd.Parameters.AddWithValue("@sys_plant_cd", "PC001");

            //        sqlConn.Open();
            //        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
            //        {
            //            sqlAdapter.Fill(dt);
            //        }
            //    }
            //}

            List<Menu> list = new List<Menu>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];

                Menu temp;

                temp = new Menu(row);

                list.Add(temp);
            }

            return list;
        }
                
        public DataTable GetMenuAuthority(string form_id)
        {
            string sSpName = "SP_Menu_GetMenu";
            string gubun = "F";
            string[] pParam = new string[2];
            pParam[0] = "@emp_cd:" + Public_Function.User_cd;
            pParam[1] = "@form_id:" + form_id;

            DataTable dt = _bllSpExecute.SpExecuteTableWithoutMessage(sSpName, gubun, pParam);

            //DataTable dt = new DataTable();
            //using (SqlConnection sqlConn = new SqlConnection(context.GetConnStr()))
            //{
            //    string sql = "SP_Menu_GetMenu";
            //    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
            //    {
            //        sqlCmd.CommandType = CommandType.StoredProcedure;

            //        sqlCmd.Parameters.AddWithValue("@Gubun", "F");
            //        sqlCmd.Parameters.AddWithValue("@emp_cd", Public_Function.User_cd);
            //        sqlCmd.Parameters.AddWithValue("@form_id", form_id);
            //        sqlCmd.Parameters.AddWithValue("@sys_plant_cd", "PC001");

            //        sqlConn.Open();
            //        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
            //        {
            //            sqlAdapter.Fill(dt);
            //        }
            //    }
            //}

            //string sSpName = "SP_Menu_GetMenu";
            //string gubun = "F";
            //string[] pParam = new string[2];

            //pParam[0] = "@emp_cd:" + Public_Function.User_cd;
            //pParam[1] = "@form_id:" + form_id;

            ////DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);
            //DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, pParam);

            //DataTable dt = new DataTable();

            //if (ds != null)
            //{
            //    dt = ds.Tables[0];
            //}

            return dt;
        }

        /// <summary>
        /// 로그인한 사용자의 프로그램권한 리스트(for 툴바버튼)를 불러온다.
        /// </summary>                
        /// <param name="form_id"></param>        
        /// <retruns>사용자의 프로그램권한리스트 DataTable</returns>
        ///        
        public DataTable GetToolbarAuthority()
        {
            string sSpName = "SP_Menu_GetMenu";
            string gubun = "L";
            string[] pParam = new string[1];
            pParam[0] = "@emp_cd:" + Public_Function.User_cd;            

            DataTable dt = _bllSpExecute.SpExecuteTableWithoutMessage(sSpName, gubun, pParam);           

            return dt;
        }

    }
}