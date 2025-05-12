using HACCP.Libs.Database;
using HACCP.Models.RowMatWh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Services.RowMatWh
{
    public class StockStatus2Service
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
        string sSpName = "SP_StockStatus2";

        /// <summary>
        /// 1. (첫 페이지 진입) 원료 품목별 재고량
        /// </summary>
        /// <returns></returns>
        internal DataTable Select_MP1(StockStatus2 sModel)
        {
            string sGubun = "Select4";
            string[] param = new string[4];

            param[0] = "@item:" + sModel.item_cd_S;      // 품목
            param[1] = "@s_gubun:" + sModel.StockStatus2_status;  // 원료 3, 자재 2
            param[2] = "@s_gubun2:notall"; // 첫 진입 검색 조건 없음
            if (sModel.check == "Y")       // 사용여부
                param[3] = "@use_ck:Y";
            else
                param[3] = "@use_ck:%";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Select_MP2(StockStatus2 sModel)
        {
            string sGubun = "Select1";

            string[] param = new string[6];
            param[0] = "@start_date:" + "";//sModel.start_date; // 시작일
            param[1] = "@end_date:" + "";//sModel.end_date; // 종료일
            param[2] = "@item:" + sModel.item_cd_S; // 품목
            param[3] = "@s_gubun:" + sModel.StockStatus2_status;  // 원료 3, 자재 2
            // 상태 값이 전체 일 때 "%"
            sModel.obtain_status_S = (sModel.obtain_status_S == "0") ? "%" : sModel.obtain_status_S;
            param[4] = "@obtain_status_S:" + sModel.obtain_status_S; // 상태 값
            if (sModel.check == "Y")
                param[5] = "@use_ck:Y";
            else
                param[5] = "@use_ck:%";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable Select_MP3(StockStatus2 sModel)
        {
            string sGubun = "Select2";

            string[] param = new string[5];
            param[0] = "@start_date:" + ""; //sModel.start_date; // 시작일
            param[1] = "@end_date:" + ""; //sModel.end_date; // 종료일
            param[2] = "@item:" + sModel.item_cd_S; // 품목
            param[3] = "@s_gubun:" + sModel.StockStatus2_status;  // 원료 3, 자재 2
            if (sModel.check == "Y")
                param[4] = "@use_ck:Y";
            else
                param[4] = "@use_ck:%";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, param);

            return dt;
        }

        internal DataTable ItemCDPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "item_cd, item_nm ";
            strsql += "FROM  v_item_material3 ";
            strsql += "WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%') ";
            strsql += "AND item_cd  LIKE '%%' AND item_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }

        internal DataTable ItemPCDPopup(string pSpName)
        {
            string strsql = "SELECT ";
            strsql += "item_cd, item_nm ";
            strsql += "FROM  v_item_material4 ";
            strsql += "WHERE (item_cd  LIKE '%%' OR item_nm  LIKE '%%') ";
            strsql += "AND item_cd  LIKE '%%' AND item_nm  LIKE '%%' ";
            strsql += "ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable(pSpName, strsql);

            return dt;
        }
    }
}
