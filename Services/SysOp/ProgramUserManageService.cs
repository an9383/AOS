using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.SysOp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HACCP.Services.SysOp
{
    public class ProgramUserManageService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 프로그램 리스트 조회
        /// </summary>
        /// <param name="moduleGubun"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        internal DataTable getProgramData(string moduleGubun, string userID)
        {
            var estEncoding = Encoding.GetEncoding(1252);
            var utf = Encoding.UTF8;

            userID = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(userID)));

            string sSpName = "SP_ProgramUserManage";
            string sGubun = "S1";

            string[] pParam = new string[2];
            pParam[0] = "@module_gb:" + moduleGubun;
            pParam[1] = "@user_id:" + Encryption.Encrypt("100", userID, true);

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        /// <summary>
        /// 특정 메뉴의 사용자별 권한 조회
        /// </summary>
        /// <param name="gubun"></param>
        /// <param name="formCd"></param>
        /// <returns></returns>
        internal DataTable getUserData(string gubun, string formCd)
        {
            string sSpName = "SP_ProgramUserManage";
            string sGubun = gubun;

            string[] pParam = new string[1];
            pParam[0] = "@form_cd:" + formCd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        /// <summary>
        /// 프로그램별 권한 bool 처리 (DB에 "Y", "N" 으로 삽입하기 위함)
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        internal ProgramUser setBool(ProgramUser program)
        {

            if (program.formQuery.Equals("Y") || program.formQuery.Equals("true"))
            {
                program.formQuery = "Y";
            }
            else
            {
                program.formQuery = "N";
            }

            if (program.formEdit.Equals("Y") || program.formEdit.Equals("true"))
            {
                program.formEdit = "Y";
            }
            else
            {
                program.formEdit = "N";
            }

            if (program.formInsert.Equals("Y") || program.formInsert.Equals("true"))
            {
                program.formInsert = "Y";
            }
            else
            {
                program.formInsert = "N";
            }

            if (program.formDelete.Equals("Y") || program.formDelete.Equals("true"))
            {
                program.formDelete = "Y";
            }
            else
            {
                program.formDelete = "N";
            }

            if (program.formPrint.Equals("Y") || program.formPrint.Equals("true"))
            {
                program.formPrint = "Y";
            }
            else
            {
                program.formPrint = "N";
            }

            if (program.formFavoriteOk.Equals("Y") || program.formFavoriteOk.Equals("true"))
            {
                program.formFavoriteOk = "Y";
            }
            else
            {
                program.formFavoriteOk = "N";
            }

            if (program.formTransmission.Equals("Y") || program.formTransmission.Equals("true"))
            {
                program.formTransmission = "Y";
            }
            else
            {
                program.formTransmission = "N";
            }

            return program;
        }

        /// <summary>
        /// 프로그램별 사용자의 권한 수정
        /// </summary>
        /// <param name="programUser"></param>
        /// <param name="insertEmp"></param>
        /// <returns></returns>
        internal string updateUser(ProgramUser programUser, string insertEmp)
        {
            string sSpName = "SP_ProgramUserManage";
            string sGubun = "I";

            string[] pParam = new string[10];
            pParam[0] = "@emp_cd:" + programUser.empCd;
            pParam[1] = "@form_cd:" + programUser.formCd;
            pParam[2] = "@form_query:" + programUser.formQuery;
            pParam[3] = "@form_edit:" + programUser.formEdit;
            pParam[4] = "@form_insert:" + programUser.formInsert;
            pParam[5] = "@form_print:" + programUser.formPrint;
            pParam[6] = "@form_delete:" + programUser.formDelete;
            pParam[7] = "@form_transmission:" + programUser.formTransmission;
            pParam[8] = "@form_favorite_ok:" + programUser.formFavoriteOk;
            pParam[9] = "@insert_emp:" + insertEmp;

            string res = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return res;
        }
    }
}
