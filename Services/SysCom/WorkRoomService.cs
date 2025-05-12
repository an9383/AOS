using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.SysCom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.SysCom
{
    public class WorkRoomService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable selectWorkRoom(string workroom_cd, string use_yn)
        {
            string sSpName = "SP_WorkRoom";

            string gubun = "S";

            string[] param = new string[2];

            param[0] = "@s_workroom_cd:" + workroom_cd;
            param[1] = "@s_use_yn:" + use_yn;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable selectWorkRoomProductData(string workroom_cd)
        {
            string sSpName = "SP_WorkRoom";

            string gubun = "SC";

            string[] param = new string[1];

            param[0] = "@workroom_cd:" + workroom_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun, param);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable selectWorkRoomPopupData()
        {
            string strsql = "SELECT workroom_cd, workroom_nm, common_part_nm";
            strsql += " FROM v_workroom2";
            strsql += " WHERE (workroom_cd  LIKE '%%' OR workroom_nm  LIKE '%%')";
            strsql += " AND workroom_cd  LIKE '%%' AND workroom_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal DataTable selectWorkRoomProductData()
        {
            string strsql = "SELECT item_cd, item_nm, item_keep_condition_nm, item_keep_temperature_nm, abbreviation_cd";
            strsql += " FROM v_item_standard";
            strsql += " WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%')";
            strsql += " AND item_cd  LIKE '%%' AND item_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt; ;
        }

        internal string CRUD(WorkRoom workRoom, string pGubun)
        {
            string sSpName = "SP_WorkRoom";

            string gubun = pGubun;
            string res = "";

            if (pGubun.Equals("D"))
            {
                string[] param = new string[1];

                param[0] = "@workroom_cd:" + workRoom.workroom_cd;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }
            else
            {
                string[] param = getParam(workRoom);

                if (Public_Function.MasterData_AutoNumbering_yn == "N" && pGubun == "I")
                {
                    string message = _bllSpExecute.SpExecuteString(sSpName, "CWC", param);
                    if (message == "Y")
                        res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
                }
                else
                    res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }

            return res;
        }

        internal string ProductCRUD(string workroom_cd, string item_cd, string item_seq, string pGubun)
        {
            clearProduct(workroom_cd);

            string sSpName = "SP_WorkRoom";

            string gubun = pGubun;
            string res = "";

            if (pGubun.Equals("DC"))
            {
                string[] param = new string[2];

                param[0] = "@workroom_cd:" + workroom_cd;
                param[1] = "@item_cd:" + item_cd;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }
            else
            {
                string[] param = new string[3];

                param[0] = "@workroom_cd:" + workroom_cd;
                param[1] = "@item_cd:" + item_cd;
                param[2] = "@item_seq:" + item_seq;

                res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
            }

            return res;
        }

        private void clearProduct(string workroom_cd)
        {
            string sSpName = "SP_WorkRoom";

            string gubun = "DDC";

            string[] param = new string[1];

            param[0] = "@workroom_cd:" + workroom_cd;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);
        }

        private string[] getParam(WorkRoom workRoom)
        {
            string[] param = new string[24];

            param[0] = "@workroom_cd:" + workRoom.workroom_cd;
            param[1] = "@workroom_nm:" + workRoom.workroom_nm;
            param[2] = "@workroom_mcd:" + workRoom.workroom_mcd;
            param[3] = "@cleanness_cd:" + workRoom.cleanness_cd;
            param[4] = "@plant_cd:" + workRoom.plant_cd;
            param[5] = "@workroom_volume:" + workRoom.workroom_volume;
            param[6] = "@workroom_bottomtype:" + workRoom.workroom_bottomtype;
            param[7] = "@workroom_walltype:" + workRoom.workroom_walltype;
            param[8] = "@workroom_toptype:" + workRoom.workroom_toptype;
            param[9] = "@workroom_type:" + workRoom.workroom_type;
            param[10] = "@warehouse_type:" + workRoom.warehouse_type;
            param[11] = "@temp_min:" + workRoom.temp_min;
            param[12] = "@temp_max:" + workRoom.temp_max;
            param[13] = "@hum_min:" + workRoom.hum_min;
            param[14] = "@hum_max:" + workRoom.hum_max;
            param[15] = "@workroom_no:" + workRoom.workroom_no;
            param[16] = "@item_type1:" + workRoom.item_type1;
            param[17] = "@item_type2:" + workRoom.item_type2;
            param[18] = "@use_yn:" + workRoom.use_yn;
            param[19] = "@hardware_yn:" + workRoom.hardware_yn;
            param[20] = "@Illumination:" + workRoom.Illumination;
            param[21] = "@workroom_area:" + workRoom.workroom_area;
            param[22] = "@press_min:" + workRoom.press_min;
            param[23] = "@press_max:" + workRoom.press_max;

            return param;
        }
    }
}
