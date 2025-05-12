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
    public class GroupMenuService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 그룹 데이터
        /// </summary>
        /// <param name="deptCd">부서코드</param>
        /// <param name="empCd">사원코드</param>
        /// <returns></returns>
        internal DataTable getCompanyGridData(string deptCd, string empCd)
        {
            string sSpName = "SP_GroupMenuManage";
            string[] pParam = new string[2];
            pParam[0] = "@dept_cd:" + deptCd;
            pParam[1] = "@emp_cd:" + empCd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S1", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        /// <summary>
        /// 프로그램 권한 데이터
        /// </summary>
        /// <param name="empCd">사원코드</param>
        /// <param name="moduleGb">모듈구분</param>
        /// <param name="userID">로그인한 사용자 ID</param>
        /// <returns></returns>
        internal DataTable getProgramGridData(string empCd, string moduleGb, string userID)
        {
            var estEncoding = Encoding.GetEncoding(1252);
            var utf = Encoding.UTF8;

            string user_id = utf.GetString(Encoding.Convert(estEncoding, utf, estEncoding.GetBytes(userID)));

            user_id = Encryption.Encrypt("100", user_id, true);

            string sSpName = "SP_GroupMenuManage";
            string[] pParam = new string[3];
            pParam[0] = "@emp_cd:" + empCd;
            pParam[1] = "@module_gb:" + moduleGb;
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
        /// 프로그램 권한 수정
        /// </summary>
        /// <param name="program">프로그램 권한 모델</param>
        /// <param name="insertEmp">수정한 사용자 사원코드</param>
        /// <returns></returns>
        internal string updateProgram(ProgramAuthority program, string insertEmp)
        {
            string sSpName = "SP_GroupMenuManage";
            string gubun = "";

            string[] pParam = new string[10];
            pParam[0] = "@emp_cd:" + program.empCd;
            pParam[1] = "@form_cd:" + program.formCd;
            pParam[2] = "@form_query:" + program.formQuery;
            pParam[3] = "@form_edit:" + program.formEdit;
            pParam[4] = "@form_insert:" + program.formInsert;
            pParam[5] = "@form_delete:" + program.formDelete;
            pParam[6] = "@form_print:" + program.formPrint;
            pParam[7] = "@form_transmission:" + program.formTransmission;
            pParam[8] = "@form_favorite_ok:" + program.formFavorite;
            pParam[9] = "@insert_emp:" + insertEmp;

            if (program.isexist.Equals("Y"))
            {
                gubun = "U";
            }
            else if(program.isexist.Equals("N"))
            {
                gubun = "I";
            }

            string str = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return str;
        }

        /// <summary>
        /// 권한 bool 처리 (DB에 "Y", "N" 으로 삽입하기 위함)
        /// </summary>
        /// <param name="program">프로그램 권한 모델</param>
        /// <returns></returns>
        internal ProgramAuthority setBool(ProgramAuthority program)
        {

            if (program.formQuery.Equals("Y")|| program.formQuery.Equals("true"))
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
