using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.Cryptography;
using HACCP.Libs.File;
using System.Web;
using DevExpress.Utils.Serializing;

namespace HACCP.Libs.Database
{
    public class DbAgent
    {
        #region Memeber Variables
        private SqlConnection _myConnection;
        private SqlCommand _myCommand;
        private SqlTransaction _myTransaction;
        public SqlDataReader rd;
        //static private string serverIP = "";
        ////실서버(본사)
        //static public string defaultConnectionString = "data source=gms.hopto.org;initial catalog=RTEGMS_COSMAXBIO;password=!123qwe!!;persist security info=true;user id=sa;packet size=8192;Connect Timeout=120";
        //static private string _Catalog = "RTEGMS_COSMAXBIO";
        //static private string _ConnectionString = "data source=gms.hopto.org;password=!123qwe!!;persist security info=true;user id=sa;packet size=8192;Connect Timeout=120;initial catalog=";
        //static public string defaultConnectionString = "data source=.;initial catalog=RTEGMS_COSMAXBIO;password=1112;persist security info=true;user id=sa;packet size=8192;Connect Timeout=120";
        //static private string _Catalog = "RTEGMS_COSMAXBIO";
        //static private string _ConnectionString = "data source=.;password=1112;persist security info=true;user id=sa;packet size=8192;Connect Timeout=120;initial catalog=";

        /////개발서버
        //static public string defaultConnectionString = "data source=106.10.84.124,1400;initial catalog=RTEGMS_COSMAXBIO;password=ureka@1123;persist security info=true;user id=sa;packet size=8192;Connect Timeout=120";
        //static private string _Catalog = "RTEGMS_COSMAXBIO";
        //static private string _ConnectionString = "data source=106.10.84.124,1400;password=ureka@1123;persist security info=true;user id=sa;packet size=8192;Connect Timeout=120;initial catalog=";

        /////로컬
        /*        static public string defaultConnectionString = "data source=Bston-PC;initial catalog=RTEGMS_COSMAXBIO;password=1112;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120";
                static private string _Catalog = "RTEGMS_COSMAXBIO";
                static private string _ConnectionString = "data source=Bston-PC;password=1112;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120;initial catalog=";
        */
        /////로컬 코스맥스
        //static public string defaultConnectionString = "data source=sf.tascorp.co.kr;initial catalog=RTEGMS_COSMAXBIO;password=!1Tascorp;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120";
        //static private string _Catalog = "RTEGMS_COSMAXBIO";
        //static private string _ConnectionString = "data source=sf.tascorp.co.kr;password=!1Tascorp;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120;initial catalog=";

        ///로컬 개발
        //static public string defaultConnectionString = "data source=sf.tascorp.co.kr;initial catalog=RTEGMS_HANPOONG;password=!1Tascorp;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120";
        //static public string defaultConnectionString = "";
        //static private string _Data_source = "sf.tascorp.co.kr";
        //static private string _DbID = "sa";
        //static private string _DbPWD = "!1Tascorp";
        //static private string _Catalog = "RTEGMS_HANPOONG";
        //static private string _ConnectionString = ";persist security info=true;packet size=4096;Connect Timeout=120";

        ///로컬 개발
        //static public string defaultConnectionString = "data source=sf.tascorp.co.kr;initial catalog=RTEGMS_HANPOONG;password=!1Tascorp;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120";
        static public string defaultConnectionString = "";
        static private string _Data_source = "127.0.0.1";
        static private string _DbID = "sa";
        static private string _DbPWD = "RYdbrtjrh@";
        static private string _Catalog = "RTEGMS_AOS";
        static private string _ConnectionString = ";persist security info=true;packet size=4096;Connect Timeout=120";

        //static private string _ConnectionString = "data source=sf.tascorp.co.kr;password=!1Tascorp;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120;initial catalog=";

        //static public string defaultConnectionString = "data source=.;initial catalog=RTEGMS_COSMAXBIO;password=1112;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120";
        //static private string _Catalog = "RTEGMS_COSMAXBIO";
        //static private string _ConnectionString = "data source=.;password=1112;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120;initial catalog=";

        /************************************************************************************************** 사용안함 ***************************************************************************************
        //static public string defaultConnectionString = "data source=(local);initial catalog=RTEGMS_COSMAXBIO;password=ureka@1123;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120";
        //static private string _Catalog = "RTEGMS_COSMAXBIO";
        //static private string _ConnectionString = "data source=(local);password=ureka@1123;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120;initial catalog=";
        
        //static public string defaultConnectionString = "data source=106.10.84.124,1400;initial catalog=RTEGMS_COSMAXBIO;password=cosmax909;persist security info=true;user id=Cosmax;packet size=4096;Connect Timeout=120";
        //static private string _Catalog = "RTEGMS_COSMAXBIO_PHARM";
        //static private string _ConnectionString = "data source=106.10.84.124,1400;password=cosmax909;persist security info=true;user id=Cosmax;packet size=4096;Connect Timeout=120;initial catalog=";
        
        //static public string defaultConnectionString = "data source=bston\\MSSQL11.MSSQLSERVER2012;initial catalog=RTEGMS_COSMAXBIO_1031;password=1112;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120";
        //static private string _Catalog = "RTEGMS_COSMAXBIO_1031";
        //static private string _ConnectionString = "data source=bston\\MSSQL11.MSSQLSERVER2012;password=1112;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120;initial catalog=";

        //static public string defaultConnectionString = "data source=.;initial catalog=RTEGMS_COSMAXBIO;password=1112;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120";
        //static private string _Catalog = "RTEGMS_COSMAXBIO";
        //static private string _ConnectionString = "data source=.;password=1112;persist security info=true;user id=sa;packet size=4096;Connect Timeout=120;initial catalog=";
        ****************************************************************************************************************************************************************************************************/
        #endregion

        #region Property 부분(DBType, ConnectionString, SqlCommand)

        /// <value> 현재 ConnectionString값에 대한 속성</value> 
        public string ConnectionString
        {
            get
            {
                return _myConnection.ConnectionString;
            }

            set
            {
                _myConnection.ConnectionString = value;
            }

        }

        /// <value> 현재 가지고 있는 (System.Data.SqlClient.SqlConnection)myConnection 속성</value> 
        public SqlConnection MyConnection
        {
            get
            {
                return _myConnection;
            }

            set
            {
                _myConnection = value;
            }
        }

        /// <value> 현재 가지고 있는 (System.Data.SqlClient.SqlCommand)MyCommand 속성</value> 
        public SqlCommand MyCommand
        {
            get
            {
                return _myCommand;
            }

            set
            {
                _myCommand = value;
            }
        }


        /// <value> 현재 가지고 있는 (System.Data.SqlClient.SqlCommand)MyCommand.CommandType.
        ///			(System.Data.CommandType)CommandType 속성으로 System.Data.CommandType중 하나</value> 
        public CommandType CommandType
        {
            set
            {
                _myCommand.CommandType = value;
            }
            get
            {
                return _myCommand.CommandType;
            }
        }

        /// <value> 현재 가지고 있는 (System.Data.SqlClient.SqlCommand)MyCommand.CommandText.
        ///			(string)CommandText 속성으로 실행할 Stored procedure명 or T-SQL 문장 </value> 
        public string CommandText
        {
            get
            {
                return _myCommand.CommandText;
            }
            set
            {
                //CommandText가 바뀌었으므로, Parameters 정보를 초기화시킨다.
                _myCommand.Parameters.Clear();
                _myCommand.CommandText = value;
            }
        }

        public SqlTransaction MyTransaction
        {
            get
            {
                return _myTransaction;
            }

            set
            {
                _myTransaction = value;
            }
        }
        #endregion

        #region 생성자 DbAgent(string connectionString, string cmdText, CommandType cmdType)

        public DbAgent(string connectionString, string cmdText, CommandType cmdType)
        {
            if (Public_Function.selectedDBId.Equals(""))
            {
                Public_Function.selectedDBId = IniReadWrite.G_IniReadValue("CONFIG", "dbId");
                Public_Function.selectedDBPw = IniReadWrite.G_IniReadValue("CONFIG", "dbPw");
                Public_Function.selectedDBIp = IniReadWrite.G_IniReadValue("CONFIG", "dbAddress");
                Public_Function.selectedPLANT = IniReadWrite.G_IniReadValue("CONFIG", "plantCD");
                Public_Function.selectedDB = IniReadWrite.G_IniReadValue("CONFIG", "dbName");
                Public_Function.position = IniReadWrite.G_IniReadValue("CONFIG", "position");

                // SMTP
                string emlPort = IniReadWrite.G_IniReadValue("CONFIG", "emlPort");
                string emlHost = IniReadWrite.G_IniReadValue("CONFIG", "emlHost");
                string emlCredentialsID = IniReadWrite.G_IniReadValue("CONFIG", "emlCredentialsID");
                string emlCredentialsPW = IniReadWrite.G_IniReadValue("CONFIG", "emlCredentialsPW");
                string emlFromAccount = IniReadWrite.G_IniReadValue("CONFIG", "emlFromAccount");
                Public_Function.emlBcc = IniReadWrite.G_IniReadValue("CONFIG", "emlBcc");

                Public_Function.emlPort = emlPort == "" ? 0 : Convert.ToInt32(Decrypt(emlPort, "ZR"));
                Public_Function.emlHost = emlHost == "" ? "" : Decrypt(emlHost, "ZR");
                Public_Function.emlCredentialsID = emlCredentialsID == "" ? "" : Decrypt(emlCredentialsID, "ZR");
                Public_Function.emlCredentialsPW = emlCredentialsPW == "" ? "" : Decrypt(emlCredentialsPW, "ZR");
                Public_Function.emlFromAccount = emlFromAccount == "" ? "" : Decrypt(emlFromAccount, "ZR");
            }

            _myConnection = new SqlConnection();
            _myCommand = new SqlCommand(cmdText, (SqlConnection)_myConnection);
            _myCommand.CommandTimeout = 0;

            if(defaultConnectionString.Equals(""))
            {
                string serverIP = "data source=" + (Public_Function.selectedDBIp == "" ? _Data_source : Public_Function.selectedDBIp);
                string dbId = ";user id=" + (Public_Function.selectedDBId == "" ? _DbID : Decrypt(Public_Function.selectedDBId, "ZR"));
                string dbPw = ";password=" + (Public_Function.selectedDBPw == "" ? _DbPWD : Decrypt(Public_Function.selectedDBPw, "ZR"));
                defaultConnectionString = serverIP + dbId + dbPw + ";initial catalog=" + (Public_Function.selectedDB == "" ? _Catalog : Public_Function.selectedDB) + _ConnectionString;
            }
            //#. memeber variable의 속성 설정 
            _myConnection.ConnectionString = (connectionString == "") ? defaultConnectionString : connectionString;
            _myCommand.CommandType = (((int)cmdType) == -1) ? CommandType.Text : cmdType;
        }  // end of public constructor
        public DbAgent(string connectionString, string cmdText, CommandType cmdType, string selectedDB)
        {
            _myConnection = new SqlConnection();
            _myCommand = new SqlCommand(cmdText, (SqlConnection)_myConnection);
            _myCommand.CommandTimeout = 0;

            if (defaultConnectionString.Equals(""))
            {
                string serverIP = "data source=" + (Public_Function.selectedDBIp == "" ? _Data_source : Public_Function.selectedDBIp);
                string dbId = ";user id=" + (Public_Function.selectedDBId == "" ? _DbID : Decrypt(Public_Function.selectedDBId, "ZR"));
                string dbPw = ";password=" + (Public_Function.selectedDBPw == "" ? _DbPWD : Decrypt(Public_Function.selectedDBPw, "ZR"));
                defaultConnectionString = serverIP + dbId + dbPw + ";initial catalog=" + (selectedDB == "" ? _Catalog : selectedDB) + _ConnectionString;
            }
            //#. memeber variable의 속성 설정 
            _myConnection.ConnectionString = (connectionString == "") ? defaultConnectionString : connectionString;
            _myCommand.CommandType = (((int)cmdType) == -1) ? CommandType.Text : cmdType;
        }  // end of public constructor

        public DbAgent(string cmdText, CommandType cmdType, string selectedDB) : this("", cmdText, cmdType, selectedDB) { }

        public DbAgent(string cmdText, string selectedDB) : this("", cmdText, (CommandType)(-1), selectedDB) { }

        public static string Decrypt(string cipherText, string Password)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                var pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                                                                    0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                                                   });

                byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                return Encoding.Unicode.GetString(decryptedData);
            }
            catch (FormatException e)
            {
                return "입력된 값이 정상적으로 암호화 되지 않은 값 입니다 : " + e.Message;
            }
        }
        public static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {

            var ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            var cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();

            return decryptedData;

        }
        public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {

            var ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            var cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(clearData, 0, clearData.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();

            return encryptedData;
        }
        public static string Encrypt(string clearText, string Password)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

            var pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                                                                0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                                                });
            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }

        #endregion

        #region  트랜잭션 관리

        public bool BeginTransaction()
        {
            OpenConnection();
            _myTransaction = _myConnection.BeginTransaction();
            return true;
        }

        public bool CommitTran()
        {
            _myTransaction.Commit();
            CloseConnection();

            return true;
        }

        public bool RollbackTran()
        {
            _myTransaction.Rollback();
            CloseConnection();

            return true;
        }

        #endregion

        #region ExcuteDataReader
        //IDataReader 반환(db 특성이 반영되지 않은 상태에서 넘긴다.)
        public SqlDataReader ExcuteDataReader()
        {
            SqlDataReader rd = null;

            try
            {
                OpenConnection();
                rd = _myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                return null;
            }
            return rd;
        }
        #endregion

        #region ExcuteNonQuery
        public string ExcuteNonQuery()
        {
            int result;
            string message;

            try
            {
                OpenConnection();

                result = _myCommand.ExecuteNonQuery();

                message = "";
                return message;

            }
            catch (SqlException e)
            {
                message = "Error : " + e.Message.ToString();

                return message;
            }
            finally
            {
                CloseConnection();

            }
        }

        //트랜잭션 처리용
        public string ExcuteNonQuery(int TransFlowType)
        {
            string message;

            try
            {
                if (TransFlowType == 0 || TransFlowType == 3)
                {
                    OpenConnection();
                    _myTransaction = _myConnection.BeginTransaction();
                }

                _myCommand.Transaction = _myTransaction;

                _myCommand.ExecuteNonQuery();

                if (TransFlowType == 2 || TransFlowType == 3)
                {
                    _myTransaction.Commit();
                    CloseConnection();
                }

                message = "";

                return message;

            }
            catch (SqlException e)
            {
                //sql server에서 raiserror를 이용하여 에러를 발생시키면서 에러 메시지를 text로 입력한 경우 : 업무 로직에 의한 프로세스 중단
                if (e.Number == 50000)
                    message = "경고!! : " + e.Message.ToString();
                else
                    message = "Error : " + e.Message.ToString();

                _myTransaction.Rollback();
                CloseConnection();

                return message;
            }
        }

        public void ExcuteNonQuery(ref int rValue, ref string Msg, bool TransactionType)
        {
            try
            {
                OpenConnection();

                _myCommand.ExecuteNonQuery();
                rValue = System.Convert.ToInt16(_myCommand.Parameters["ReturnValue"].Value);
                Msg = _myCommand.Parameters["@message"].Value.ToString();
            }
            catch (SqlException e)
            {
                rValue = -1;//성공시 SP에서 return @@error 값인 0을 보낸다.
                Msg = "Error : " + e.Message.ToString();

            }
            finally
            {
                if (TransactionType == false) CloseConnection();
            }
        }

        public void ExcuteNonQuery(ref int rValue, ref string Msg, ref int recordsAffected, bool TranscationType)
        {

            try
            {
                OpenConnection();

                recordsAffected = _myCommand.ExecuteNonQuery();
                rValue = System.Convert.ToInt16(_myCommand.Parameters["ReturnValue"].Value);
                Msg = _myCommand.Parameters["@message"].Value.ToString();

            }
            catch (SqlException e)
            {
                recordsAffected = -1;//
                rValue = -1;//성공시 SP에서 return @@error 값인 0을 보낸다.
                Msg = "Error : " + e.Message.ToString();

            }
            finally
            {
                if (TranscationType == false) CloseConnection();

            }
        }
        #endregion

        #region ExcuteDataSet

        /// <summary>
        /// DbAgent의 생성자로 3개의 override method로 구성되어, ConnectionString, CommandText, CommandType 속성 등을
        /// 설정해준다.
        /// </summary>
        /// <returns> 생성자이므로 Return값없음ted by the command</returns>
        public DataSet ExcuteDataSet()
        {
            DataSet myDataSet = new DataSet();

            try
            {
                OpenConnection();

                SqlDataAdapter myDataAdapter = new SqlDataAdapter(_myCommand);
                myDataAdapter.Fill(myDataSet);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                CloseConnection();
            }
            return myDataSet;
        }
        #endregion

        #region Private Fuction
        private void OpenConnection()
        {
            if (_myConnection.State == ConnectionState.Closed)
                _myConnection.Open();

        }

        private void CloseConnection()
        {
            if (_myConnection.State == ConnectionState.Open)
                _myConnection.Close();
        }

        #endregion
    }//end of DbAgent Class
    
}
