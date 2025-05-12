using HACCP.Libs.Database;
using HACCP.Models.SysOp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysOp
{
    public class PositionService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SelectPositionData()
        {
            string sSpName = "SP_Position";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S");

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SelectManagementTarget()
        {
            string strsql = "SELECT standard_cd, standard_nm";
            strsql += " FROM v_schedulecode";
            strsql += " WHERE (standard_cd  LIKE '%%' OR standard_nm  LIKE '%%')";
            strsql += " AND standard_type_cd  LIKE '%2%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable SelectMonitoringWorkroom()
        {
            string strsql = "SELECT workroom_cd, workroom_nm, common_part_nm ";
            strsql += " FROM v_workroom2 ";
            strsql += " WHERE (workroom_cd  LIKE '%%' OR workroom_nm  LIKE '%%')";
            strsql += " AND workroom_cd  LIKE '%%'";
            strsql += " AND workroom_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal string positionCRUD(PositionData positionData, string pGubun)
        {
            string sSpName = "SP_Position";
            string gubun = pGubun;

            string[] param = new string[1];

            if (pGubun.Equals("D"))
            {
                param[0] = "@position_no:" + positionData.position_no;
            }
            else
            {
                param = getParam(positionData);
            }

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

        private string[] getParam(PositionData positionData)
        {
            string[] param = new string[8];

            param[0] = "@position_no:" + positionData.position_no;
            param[1] = "@obj_type:" + positionData.obj_type;
            param[2] = "@obj_cd:" + positionData.obj_cd;
            param[3] = "@program_id:" + positionData.program_id;
            param[4] = "@program_ex1_id:" + positionData.program_ex1_id;
            param[5] = "@other_position_no1:" + positionData.other_position_no1;
            param[6] = "@other_position_no2:" + positionData.other_position_no2;
            param[7] = "@other_position_no3:" + positionData.other_position_no3;

            return param;
        }
    }
}
