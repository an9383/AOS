using DevExpress.ClipboardSource.SpreadsheetML;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace HACCP.Services.Comm
{
    public class LoginService
    {
        private string _user_id;
        private string _pass;
        private string _user_id_nm;
        private string _user_cd;
        private string _user_nm;
        private string _dept_nm;
        private string _dept_cd;
        private string _company;
        private string _product_type;
        private string _version;
        private string _emp_charge;
        private string _project;
        private string _process_transfer_yn;
        private string _MasterData_AutoNumbering_yn;

        private string _end_time;
        private string _new_user;
        private string _error_message;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //private AOSContext context = new AOSContext();

        public BllSpExecute _bllSpExecute = new BllSpExecute();

        #region    //접근자
        public string user_id
        {
            set
            {
                _user_id = value;
            }
        }

        public string pass
        {
            set
            {
                _pass = value;
            }
        }

        public string user_id_nm
        {
            get
            {
                return _user_id_nm;
            }
        }

        public string user_cd
        {
            get
            {
                return _user_cd;
            }
        }

        public string user_nm
        {
            get
            {
                return _user_nm;
            }
        }
        public string dept_nm
        {
            get
            {
                return _dept_nm;
            }
        }

        public string log
        {
            get
            {
                return ID_Search();
            }
        }
        public string dept_cd
        {
            get
            {
                return _dept_cd;
            }
        }
        public string plant_nm { get; set; }

        internal DataTable selectedPLANTName()
        {
            string sSpName = "SP_Login";
            string sGubun = "PNM";            
            string[] param = new string[1];

            param[0] = "@user_id:" + HttpContext.Current.Session["loginCD"].ToString();

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        public string company
        {
            get
            {
                return _company;
            }
        }

        public string product_type
        {
            get
            {
                return _product_type;
            }
        }

        public string version
        {
            get
            {
                return _version;
            }
        }

        public string cv
        {
            get
            {
                return CV_Search();
            }
        }

        public string emp_charge
        {
            get
            {
                return _emp_charge;
            }
        }

        public string end_time
        {
            get
            {
                return _end_time;
            }
        }

        public string new_user
        {
            get
            {
                return _new_user;
            }
        }
        #endregion

        public LoginService()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //
        }



        public DataTable loginPageLoading()
        {
            string sSpName = "SP_Login";
            string gubun = "lpl";  /* LogIn Page Loadn */

            string[] param = new string[0];

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, param);

            DataTable table = new DataTable();
            table.Columns.Add("company_full_image", typeof(string));

            foreach(DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();
                if (row["company_full_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["company_full_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["company_full_image"] = url;
                }
                else
                {
                    //_row["company_full_image"] = "/Content/image/empty_logo.jpg";
                }

                table.Rows.Add(_row);
            }
          

            return table;
        }


        private string ID_Search()
        {
            try
            {
                //webDb = new WebDbContext();

                //var user = webDb.Users.FirstOrDefault(
                //	u => u.txt_ID.Equals(_user_id)
                //	&& u.txt_Pass.Equals(_pass));
                DataSet myDataSet = CheckLogin(_user_id, _pass);

                if (myDataSet.Tables[0].Rows.Count <= 0)
                {
                    return myDataSet.Tables[1].Rows[0][0].ToString();
                }
                else if (myDataSet.Tables[0].Rows[0]["user_id"].ToString().Trim() == "")
                {
                    return "3";
                }
                else
                {
                    _user_id_nm = myDataSet.Tables[0].Rows[0]["user_id_nm"].ToString().Trim();
                    _user_cd = myDataSet.Tables[0].Rows[0]["user_cd"].ToString().Trim();
                    _user_nm = myDataSet.Tables[0].Rows[0]["user_nm"].ToString().Trim();
                    _dept_cd = myDataSet.Tables[0].Rows[0]["user_dept_cd"].ToString().Trim();
                    _dept_nm = myDataSet.Tables[0].Rows[0]["user_dept_nm"].ToString().Trim();
                    _emp_charge = myDataSet.Tables[0].Rows[0]["emp_charge"].ToString().Trim();
                    _end_time = myDataSet.Tables[0].Rows[0]["end_time"].ToString().Trim();
                    _new_user = myDataSet.Tables[0].Rows[0]["new_user"].ToString().Trim();
                    _project = myDataSet.Tables[0].Rows[0]["project"] == null ? "" : myDataSet.Tables[0].Rows[0]["project"].ToString().Trim();
                    _process_transfer_yn = myDataSet.Tables[0].Rows[0]["process_transfer_yn"].ToString().Trim();
                    _MasterData_AutoNumbering_yn = myDataSet.Tables[0].Rows[0]["MasterData_AutoNumbering_yn"].ToString().Trim();

                    HttpContext.Current.Session["userIdNm"] = myDataSet.Tables[0].Rows[0]["user_id_nm"].ToString().Trim();
                    HttpContext.Current.Session["loginCD"] = myDataSet.Tables[0].Rows[0]["user_cd"].ToString().Trim();
                    HttpContext.Current.Session["loginNm"] = myDataSet.Tables[0].Rows[0]["user_nm"].ToString().Trim();
                    HttpContext.Current.Session["loginDeptCd"] = myDataSet.Tables[0].Rows[0]["user_dept_cd"].ToString().Trim();
                    HttpContext.Current.Session["loginDeptnm"] = myDataSet.Tables[0].Rows[0]["user_dept_nm"].ToString().Trim();
                    HttpContext.Current.Session["empCharge"] = myDataSet.Tables[0].Rows[0]["emp_charge"].ToString().Trim();
                    HttpContext.Current.Session["endTime"] = myDataSet.Tables[0].Rows[0]["end_time"].ToString().Trim();
                    HttpContext.Current.Session["newUser"] = myDataSet.Tables[0].Rows[0]["new_user"].ToString().Trim();
                    return "OK";
                }
            }
            catch (Exception e)
            {
                string message = "Error : " + e.Message.ToString();
                return message;
            }
        }

        private DataSet CheckLogin(string user_id, string Pass)
        {
            var estEncoding = Encoding.GetEncoding(1252);
            var utf = Encoding.UTF8;

            user_id = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(user_id)));
            Pass = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(Pass)));

            string sSpName = "SP_Login";
            string[] pParam = new string[3];
            pParam[0] = "@YorN:" + "S";
            pParam[1] = "@user_id:" + DbAgent.Encrypt(user_id, "ZR");
            pParam[2] = "@pass:" + DbAgent.Encrypt(Pass, "ZR");

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName,"" ,pParam);

            //DataSet dt = new DataSet();
            //using (SqlConnection sqlConn = new SqlConnection(context.GetConnStr()))
            //{
            //    string sql = "SP_Login";

            //    user_id = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(user_id)));
            //    Pass = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(Pass)));

            //    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
            //    {
            //        sqlCmd.CommandType = CommandType.StoredProcedure;

            //        sqlCmd.Parameters.AddWithValue("@YorN", "S");
            //        sqlCmd.Parameters.AddWithValue("@user_id", Encryption.Encrypt("100", user_id, true));
            //        sqlCmd.Parameters.AddWithValue("@pass", Encryption.Encrypt("100", Pass, true));


            //        sqlCmd.Parameters.AddWithValue("@sys_emp_cd", Public_Function.User_cd);
            //        sqlCmd.Parameters.AddWithValue("@sys_plant_cd", "PC001");
            //        sqlCmd.Parameters.AddWithValue("@message", "");

            //        sqlConn.Open();
            //        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
            //        {
            //            sqlAdapter.Fill(dt);
            //        }
            //    }
            //}

            return ds;

        }

        //사용자 조회
        private string CV_Search()
        {
            try
            {
                DataSet myDataSet = CV_Check();

                if (myDataSet.Tables.Count != 3)
                {
                    return "NO";
                }
                else
                {
                    _product_type = myDataSet.Tables[0].Rows[0][0].ToString();
                    _version = myDataSet.Tables[1].Rows[0][0].ToString();
                    _company = myDataSet.Tables[2].Rows[0][0].ToString();
                    return "OK";
                }
            }
            catch (Exception e)
            {
                string message = "Error : " + e.Message.ToString();
                return message;
            }
        }

        //프로그램 버젼 및 상호 check
        private DataSet CV_Check()
        {
            string sSpName = "SP_Login";
            string gubun = "SF";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun);

            //DataSet dt = new DataSet();

            //using (SqlConnection sqlConn = new SqlConnection(context.GetConnStr()))
            //{
            //    string sql = "SP_Login";
            //    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
            //    {
            //        sqlCmd.CommandType = CommandType.StoredProcedure;

            //        sqlCmd.Parameters.AddWithValue("@Gubun", "SF");
            //        sqlCmd.Parameters.AddWithValue("@sys_plant_cd", "PC001");
            //        sqlCmd.Parameters.AddWithValue("@sys_emp_cd", Public_Function.User_cd);
            //        sqlCmd.Parameters.AddWithValue("@message", "");

            //        sqlConn.Open();
            //        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
            //        {
            //            sqlAdapter.Fill(dt);
            //        }
            //    }
            //}

            return ds;
        }

        public void SystemLogIn()
        {
            string sSpName = "SP_AccessLog";
            string sGubun = "I1";
            string[] pParam = new string[1];
            pParam[0] = "@emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();

            string auditID = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            HttpContext.Current.Session["system_log_id"] = auditID;

            //using (SqlConnection sqlConn = new SqlConnection(context.GetConnStr()))
            //{
            //    string sql = "SP_AccessLog";
            //    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
            //    {
            //        sqlCmd.CommandType = CommandType.StoredProcedure;

            //        sqlCmd.Parameters.AddWithValue("@Gubun", "I1");
            //        sqlCmd.Parameters.AddWithValue("@emp_cd", Public_Function.User_cd);
            //        sqlCmd.Parameters.AddWithValue("@sys_plant_cd", "PC001");
            //        sqlCmd.Parameters.AddWithValue("@message", "");

            //        sqlConn.Open();
            //        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
            //        {
            //            sqlAdapter.Fill(dt);
            //        }
            //    }
            //}
        }

        //시스템로그아웃
        public void SystemLogOut()
        {
            string sSpName = "SP_AccessLog";
            string sGubun = "U1";
            string[] pParam = new string[1];
            pParam[0] = "@id:" + HttpContext.Current.Session["system_log_id"].ToString();

            string res = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            Public_Function.User_cd = "";
            Public_Function.User_id = "";
            HttpContext.Current.Session["loginID"] = "";
            HttpContext.Current.Session["loginCD"] = "";

            //using (SqlConnection sqlConn = new SqlConnection(context.GetConnStr()))
            //{
            //    string sql = "SP_AccessLog";

            //    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
            //    {
            //        sqlCmd.CommandType = CommandType.StoredProcedure;

            //        sqlCmd.Parameters.AddWithValue("@Gubun", "U1");
            //        sqlCmd.Parameters.AddWithValue("@id", Public_Function.User_id);
            //        sqlCmd.Parameters.AddWithValue("@message", "");

            //        sqlConn.Open();
            //        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
            //        {
            //            sqlAdapter.Fill();
            //        }
            //    }
            //}

            //string gubun = "U1";
            //string param = "";
            //string message = "";

            //param = "@id:" + Public_Function.system_log_id;
            //message = mBllSpExecute.SpExecuteString("SP_AccessLog", gubun, param);
        }


        internal string LogIn(User model)
        {
            //DateTime end_time = DateTime.Now;

            this.user_id = model.txt_ID;
            this.pass = model.txt_Pass;

            string res = this.log;

            if (res == "OK")
            {
                Public_Function.User_id = model.txt_ID;
                Public_Function.User_id_nm = this._dept_nm;
                Public_Function.User_cd = this._user_cd;
                Public_Function.User_nm = this._user_nm;
                Public_Function.Dept_cd = this._dept_cd;
                Public_Function.Dept_nm = this._dept_nm;
                Public_Function.emp_charge = this._emp_charge;
                Public_Function.NewUser = this._new_user;
                Public_Function.project = this._project;
                Public_Function.process_transfer_yn = this._process_transfer_yn;
                Public_Function.MasterData_AutoNumbering_yn = this._MasterData_AutoNumbering_yn;
                Public_Function.End_time = this._end_time;
                //end_time = DateTime.Parse(this._end_time);
                //BarcodePrefix_Set();

            }
            return res;
        }

        internal DataTable IDValidation(User model)
        {
            var estEncoding = Encoding.GetEncoding(1252);
            var utf = Encoding.UTF8;

            if (String.IsNullOrEmpty(model.txt_ID))
            {
                return new DataTable();
            }

            if (String.IsNullOrEmpty(model.txt_Pass))
            {
                return new DataTable();
            }

            string user_id = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(model.txt_ID)));
            string user_passwd = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(model.txt_Pass)));

            //user_id = Encryption.Encrypt("100", user_id, true);
            //user_passwd = Encryption.Encrypt("100", user_passwd, true);
            user_id = DbAgent.Encrypt(user_id, "ZR");
            user_passwd = DbAgent.Encrypt(user_passwd, "ZR");

            string sSpName = "SP_IDValidation";
            string[] pParam = new string[2];
            pParam[0] = "@user_id:" + user_id;
            pParam[1] = "@user_passwd:" + user_passwd;

            DataTable dt = new DataTable();

            try
            {
                dt = _bllSpExecute.SpExecuteTable(sSpName, model.gubun, pParam);
            }
            catch
            {
                return null;
            }

            return dt;
        }

        internal string AuthorityCheck(string emp_cd, string sign_set_cd, string sign_set_seq)
        {
            string sSpName = "SP_IDValidation";

            string[] pParam = new string[3];
            pParam[0] = "@emp_cd:" + emp_cd;
            pParam[1] = "@sign_set_cd:" + sign_set_cd;
            pParam[2] = "@sign_set_seq:" + sign_set_seq;

            string res = _bllSpExecute.SpExecuteString(sSpName, "AC", pParam);

            return res;
        }

        internal string DelegateCheck(string emp_cd, string sign_set_cd, string sign_set_seq)
        {
            string sSpName = "SP_IDValidation";

            string[] pParam = new string[3];
            pParam[0] = "@emp_cd:" + emp_cd;
            pParam[1] = "@sign_set_cd:" + sign_set_cd;
            pParam[2] = "@sign_set_seq:" + sign_set_seq;

            string res = _bllSpExecute.SpExecuteString(sSpName, "SR", pParam);

            return res;
        }
    }
}
