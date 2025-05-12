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
    public class MyPageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        public MyPageService()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //
        }


        public DataTable MyInfoSelect()
        {
            DataTable dt = new DataTable();

            string[] pParam = new string[2];
            pParam[0] = "@s_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[1] = "@s_use_yn:" + "Y";

            dt = _bllSpExecute.SpExecuteTable("SP_Employee", "S", pParam);

            return dt;
        }


        public string PWDReuse(string newPassword, string oldPassword)
        {
            var estEncoding = Encoding.GetEncoding(1252);
            var utf = Encoding.UTF8;

            string user_id = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(HttpContext.Current.Session["loginID"].ToString())));
            string pre_passwd = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(oldPassword)));
            string user_passwd = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(newPassword)));

            string[] pParam = new string[3];
            //pParam[0] = "@user_id:" + Encryption.Encrypt("100", user_id, true);
            //pParam[1] = "@pre_passwd:" + Encryption.Encrypt("100", pre_passwd, true);
            //pParam[2] = "@user_passwd:" + Encryption.Encrypt("100", user_passwd, true);
            pParam[0] = "@user_id:" + DbAgent.Encrypt(user_id, "ZR");
            pParam[1] = "@pre_passwd:" + DbAgent.Encrypt(pre_passwd, "ZR");
            pParam[2] = "@user_passwd:" + DbAgent.Encrypt(user_passwd, "ZR");
            

            string message = _bllSpExecute.SpExecuteString("SP_Alter_PWD", "RP", pParam);

            return message;
        }


        public string AlterPassword(string newPassword, string oldPassword)
        {
            var estEncoding = Encoding.GetEncoding(1252);
            var utf = Encoding.UTF8;

            string user_id = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(HttpContext.Current.Session["loginID"].ToString())));
            string pre_passwd = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(oldPassword)));
            string user_passwd = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(newPassword)));

            string[] pParam = new string[3];
            //pParam[0] = "@user_id:" + Encryption.Encrypt("100", user_id, true);
            //pParam[1] = "@pre_passwd:" + Encryption.Encrypt("100", pre_passwd, true);
            //pParam[2] = "@user_passwd:" + Encryption.Encrypt("100", user_passwd, true);
            pParam[0] = "@user_id:" + DbAgent.Encrypt(user_id, "ZR");
            pParam[1] = "@pre_passwd:" + DbAgent.Encrypt(pre_passwd, "ZR");
            pParam[2] = "@user_passwd:" + DbAgent.Encrypt(user_passwd, "ZR");

            string message = _bllSpExecute.SpExecuteString("SP_Alter_PWD", "U", pParam);

            return message;
        }



    }
}
