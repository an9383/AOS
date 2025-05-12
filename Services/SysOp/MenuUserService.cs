using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACCP.Services.SysOp
{
    public class MenuUserService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 사용자 정보 조회
        /// </summary>
        /// <param name="pEmp_cd"></param>
        /// <param name="pUserSecurity"></param>
        /// <returns></returns>
        public List<MenuUser> Select(string pEmp_cd, string pUserSecurity)
        {
            string sSpName = "SP_MENU_USER";
            string sGubun = "S";
            string[] pParam = new string[2];

            if (pEmp_cd == "" || pEmp_cd == null)
            {
                pParam[0] = "@s_emp_cd:" + "";
            }
            else
            {
                pParam[0] = "@s_emp_cd:" + pEmp_cd;
            }

            if (pUserSecurity == "" || pUserSecurity == null)
            {
                pParam[1] = "@s_gubun:" + "0";
            }
            else
            {
                pParam[1] = "@s_gubun:" + pUserSecurity;
            }

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            List<MenuUser> menuUsers = new List<MenuUser>();

            var estEncoding = Encoding.GetEncoding(1252);
            var utf = Encoding.UTF8;


            foreach (DataRow row in dt.AsEnumerable())
            {
                MenuUser menuUser = new MenuUser(row);

                string user_id = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(menuUser.user_id)));
                string now_user_id = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(menuUser.now_user_id)));
                string user_passwd = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(menuUser.user_passwd)));


                if (menuUser.user_id.Length > 0)
                {
                    menuUser.user_id = DbAgent.Decrypt(user_id, "ZR");
                    menuUser.now_user_id = DbAgent.Decrypt(now_user_id, "ZR");
                    menuUser.user_passwd = DbAgent.Decrypt(user_passwd, "ZR");
                }

                menuUsers.Add(menuUser);
            }

            return menuUsers;
        }

        /// <summary>
        /// 사용자 상세정보 조회
        /// </summary>
        /// <returns></returns>
        internal DataTable SelectUser()
        {
            string strsql = "SELECT emp_cd, emp_nm, dept_cd, dept_nm";
            strsql += " FROM V_EMPLOYEE";
            strsql += " WHERE (emp_cd  LIKE '%%' OR emp_nm  LIKE '%%')";
            strsql += " AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        /// <summary>
        /// 사용자 사인 정보 조회
        /// </summary>
        /// <param name="fingerprint_emp"></param>
        /// <returns></returns>
        internal string selectEmpSign(string fingerprint_emp)
        {
            string sSpName = "SP_FingerPrint";
            string sGubun = "SS";
            string[] pParam = new string[1];
            pParam[0] = "@fingerprint_emp:" + fingerprint_emp;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();
            string src = "";

            if (ds != null)
            {
                dt = ds.Tables[0];

                if(dt.Rows.Count <= 0)
                {
                    return "/Content/image/defaultSign.png";
                }
            }

            if (dt.Rows[0].ItemArray[1] != System.DBNull.Value)
            {
                string str = Convert.ToBase64String((byte[])(dt.Rows[0].ItemArray[1]));
                src = "data:Image/png;base64," + str;
            }

            return src;
        }

        /// <summary>
        /// 사용자 사인 수정
        /// </summary>
        /// <param name="myBytes"></param>
        /// <param name="empCd"></param>
        internal void updateSign(byte[] myBytes, string empCd)
        {
            string sSpName = "SP_FingerPrint";
            string sGubun = "I";
            string[] pParam = new string[1];
            pParam[0] = "@fingerprint_emp:" + empCd;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@sign_image", myBytes, pParam);

            return;
        }

        /// <summary>
        /// 사용자 ID 중복 확인
        /// </summary>
        /// <param name="pMenuUser"></param>
        /// <returns></returns>
        internal int idDuplicateCheck(MenuUser pMenuUser)
        {
            string sSpName = "SP_MENU_USER";
            string sGubun = "SI";
            string[] pParam = new string[2];

            var estEncoding = Encoding.GetEncoding(1252);
            var utf = Encoding.UTF8;

            string user_id = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(pMenuUser.user_id)));

            pParam[0] = "@emp_cd:" + pMenuUser.emp_cd;
            pParam[1] = "@chk_user_id:" + DbAgent.Encrypt(user_id, "ZR");
            //pParam[1] = "@chk_user_id:" + Encryption.Encrypt("100", user_id, true);

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return Int32.Parse(dt.Rows[0].ItemArray[0].ToString());
        }

        /// <summary>
        /// 사용자 상세정보 수정,삭제
        /// </summary>
        /// <param name="pMenuUser"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>
        internal string CRUD(MenuUser pMenuUser, string gubun)
        {
            string sSpName = "SP_MENU_USER";
            string sGubun = gubun;

            string message = "";

            if (gubun.Equals("D"))
            {
                string[] pParam = new string[1];
                pParam[0] = "@emp_cd:" + pMenuUser.emp_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else
            {
                string[] pParam = new string[11];

                var estEncoding = Encoding.GetEncoding(1252);
                var utf = Encoding.UTF8;

                string user_id = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(pMenuUser.user_id)));
                string user_passwd = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(pMenuUser.user_passwd)));

                pParam[0] = "@user_id:" + DbAgent.Encrypt(user_id, "ZR"); //Encryption.Encrypt("100", user_id, true);
                pParam[1] = "@now_user_id:" + DbAgent.Encrypt(user_id, "ZR"); //Encryption.Encrypt("100", user_id, true);
                pParam[2] = "@user_nm:" + pMenuUser.user_nm;
                pParam[3] = "@emp_cd:" + pMenuUser.emp_cd;
                pParam[4] = "@user_passwd:" + DbAgent.Encrypt(user_passwd, "ZR"); //Encryption.Encrypt("100", user_passwd, true);
                pParam[5] = "@use_check:" + pMenuUser.use_check;
                pParam[6] = "@chk_user_id:" + pMenuUser.chk_user_id;
                pParam[7] = "@chk_user_passwd:" + pMenuUser.chk_user_passwd;
                pParam[8] = "@user_doorlock_id:" + pMenuUser.user_doorlock_id;
                pParam[9] = "@user_doorlock_password:" + pMenuUser.user_doorlock_password;
                pParam[10] = "@user_security:" + pMenuUser.user_security;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return message;
        }

        /// <summary>
        /// 비밀번호 재사용 여부 체크
        /// </summary>
        /// <param name="pMenuUser"></param>
        /// <returns></returns>
        internal string passwordReuseCheck(MenuUser pMenuUser)
        {
            string sSpName = "SP_MENU_USER";
            string gubun = "RP";
            string[] pParam = new string[2];

            var estEncoding = Encoding.GetEncoding(1252);
            var utf = Encoding.UTF8;

            string user_passwd = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(pMenuUser.user_passwd)));

            pParam[0] = "@emp_cd:" + pMenuUser.emp_cd;
            pParam[1] = "@user_passwd:" + DbAgent.Encrypt(user_passwd, "ZR"); //Encryption.Encrypt("100", user_passwd, true);

            string message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return message;
        }
    }
}
