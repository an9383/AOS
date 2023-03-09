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
    public class UserMenuManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 사용자 리스트 조회
        /// </summary>
        /// <param name="deptCd"></param>
        /// <param name="empCd"></param>
        /// <param name="sGubun"></param>
        /// <returns></returns>
        internal DataTable getEmpData(string deptCd, string empCd, string sGubun)
        {
            if(deptCd == null)
            {
                deptCd = "";
            }
            if(empCd == null)
            {
                empCd = "";
            }

            string sSpName = "SP_UserMenuManage";
            string[] pParam = new string[3];
            pParam[0] = "@dept_cd:" + deptCd;
            pParam[1] = "@emp_cd:" + empCd;
            pParam[2] = "@s_gubun:" + sGubun;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        /// <summary>
        /// 프로그램 리스트 조회
        /// </summary>
        /// <param name="moduleGb"></param>
        /// <param name="empCd"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        internal DataTable getProgramData(string moduleGb, string empCd, string userID)
        {
            var estEncoding = Encoding.GetEncoding(1252);
            var utf = Encoding.UTF8;

            string user_id = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(userID)));

            user_id = Encryption.Encrypt("100", user_id, true);

            string sSpName = "SP_UserMenuManage";
            string[] pParam = new string[3];
            pParam[0] = "@module_gb:" + moduleGb;
            pParam[1] = "@emp_cd:" + empCd;
            pParam[2] = "@user_id:" + user_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S2", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        /// <summary>
        /// 팝업에 필요한 사용자 리스트 조회
        /// </summary>
        /// <returns></returns>
        internal DataTable getEmpPopupData()
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
        /// 팝업에 필요한 부서 리스트 조회
        /// </summary>
        /// <returns></returns>
        internal DataTable getDeptPopupData()
        {
            string strsql = "SELECT dept_cd, dept_nm";
            strsql += " FROM V_DEPARTMENT ";
            strsql += " WHERE (dept_cd  LIKE '%%' OR dept_nm  LIKE '%%') ";
            strsql += " AND dept_cd  LIKE '%%' AND dept_nm  LIKE '%%' ";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        /// <summary>
        /// 특정 유저의 메뉴 권한 수정
        /// </summary>
        /// <param name="programAuthority"></param>
        /// <param name="insertEmp"></param>
        /// <returns></returns>
        internal string updateUserMenu(ProgramAuthority programAuthority, string insertEmp)
        {
            programAuthority = setBool(programAuthority);

            string sSpName = "SP_UserMenuManage";

            string gubun = (programAuthority.isexist.Equals("Y")) ? "U" : "I";

            string[] pParam = new string[10];
            pParam[0] = "@emp_cd:" + programAuthority.empCd;
            pParam[1] = "@form_cd:" + programAuthority.formCd;
            pParam[2] = "@form_query:" + programAuthority.formQuery;
            pParam[3] = "@form_edit:" + programAuthority.formEdit;
            pParam[4] = "@form_insert:" + programAuthority.formInsert;
            pParam[5] = "@form_delete:" + programAuthority.formDelete;
            pParam[6] = "@form_print:" + programAuthority.formPrint;
            pParam[7] = "@form_transmission:" + programAuthority.formTransmission;
            pParam[8] = "@form_favorite_ok:" + programAuthority.formFavorite;
            pParam[9] = "@insert_emp:" + insertEmp;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }

        /// <summary>
        /// 권한 복사
        /// </summary>
        /// <param name="empCd1"></param>
        /// <param name="empCd2"></param>
        /// <returns></returns>
        internal string colpyAuthority(string empCd1, string empCd2)
        {
            string sSpName = "SP_UserMenuManage";

            string gubun = "I2";

            string[] pParam = new string[2];
            pParam[0] = "@emp_cd:" + empCd1;
            pParam[1] = "@emp_cd2:" + empCd2;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }

        /// <summary>
        /// 프로그램별 권한 bool 처리 (DB에 "Y", "N" 으로 삽입하기 위함)
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        internal ProgramAuthority setBool(ProgramAuthority program)
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

            if (program.formFavorite.Equals("Y") || program.formFavorite.Equals("true"))
            {
                program.formFavorite = "Y";
            }
            else
            {
                program.formFavorite = "N";
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

    }
}
