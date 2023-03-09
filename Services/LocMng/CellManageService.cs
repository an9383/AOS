using HACCP.Libs.Database;
using HACCP.Libs;
using HACCP.Models.LocMng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HACCP.Services.LocMng
{
    public class CellManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable Select(string workroom_cd, string zone_cd, string cell_isle, string cell_height)
        {
            string sSpName = "SP_MA_CellManage";
            string sGubun = "Select";
            string[] pParam = new string[4];
            pParam[0] = "@workroom_cd:" + workroom_cd;
            pParam[1] = "@zone_cd:" + zone_cd;
            pParam[2] = "@cell_isle:" + cell_isle;
            pParam[3] = "@cell_height:" + cell_height;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable SelectData(string workroom_cd)
        {
            string sSpName = "SP_MA_CellManage";
            string sGubun = "SelectData";
            string[] pParam = new string[1];
            pParam[0] = "@workroom_cd:" + workroom_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        //창고 코드 조회
        internal DataTable SelectTable(string workroom_cd)
        {
            string sSpName = "SP_MA_CellManage";
            string sGubun = "SelectTable";
            string[] pParam = new string[1];
            pParam[0] = "@workroom_cd:" + workroom_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        //랙 코드 조회
        internal DataTable SelectCell(string zone_cd)
        {
            string sSpName = "SP_MA_CellManage";
            string sGubun = "SelectCell";
            string[] pParam = new string[1];
            pParam[0] = "@zone_cd:" + zone_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        //단 코드 조회
        internal DataTable SelectHeight(string cell_isle, string zone_cd)
        {
            string sSpName = "SP_MA_CellManage";
            string sGubun = "SelectHeight";
            string[] pParam = new string[2];
            pParam[0] = "@cell_isle:" + cell_isle;
            pParam[1] = "@zone_cd:" + zone_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }        

        internal string CellManageCRUD(CellManage cModel, string gubun)
        {
            string sSpName = "SP_MA_CellManage";
            string sGubun = gubun;

            string message = "";

            if (cModel.gubun.Equals("Delete"))
            {
                string[] pParam = new string[2];
                pParam[0] = "@zone_cd" + cModel.zone_cd;
                pParam[1] = "@cell_cd:" + cModel.cell_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            if (cModel.gubun.Equals("barcode_print_U"))
            {
                string[] pParam = new string[1];

                pParam[0] = "@cell_cd:" + cModel.cell_cd;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else
            {
                string[] pParam = GetParam(cModel);

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return message;
        }

        private string[] GetParam(CellManage cellManage)
        {
            string[] param = new string[14];

            //기본정보
            param[0] = "@cell_cd:" + cellManage.cell_cd;
            param[1] = "@zone_cd:" + cellManage.zone_cd;
            param[2] = "@cell_nm:" + cellManage.cell_nm;
            param[3] = "@cell_type:" + cellManage.cell_type;
            param[4] = "@cell_priority:" + cellManage.cell_priority;
            param[5] = "@cell_isle:" + cellManage.cell_isle;
            param[6] = "@cell_height:" + cellManage.cell_height;
            param[7] = "@cell_column:" + cellManage.cell_column;
            param[8] = "@cell_status:" + cellManage.cell_status;
            param[9] = "@cell_remark:" + cellManage.cell_remark;
            param[10] = "@insert_user_cd:" + HttpContext.Current.Session["loginCD"];
            param[11] = "@update_user_cd:" + HttpContext.Current.Session["loginCD"];
            param[12] = "@cell_max_capacity:" + cellManage.cell_max_capacity;
            param[13] = "@cell_min_capacity:" + cellManage.cell_min_capacity;

            return param;
        }

    }
}