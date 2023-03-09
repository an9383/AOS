using HACCP.Libs.Database;
using HACCP.Models.SysSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.SysSet
{
    public class TableLayoutService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();


        /// <summary>
        /// 테이블리스트 조회(Lookup용)
        /// </summary>
        /// <returns></returns>
        internal DataTable SelectTableList()
        {
            string sSpName = "SP_TableLayout";
            string sGubun = "SelectTableList";
            string[] pParam = new string[1];
            pParam[0] = "@table_name:";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            return dt;
        }

        /// <summary>
        /// 테이블 조회(Grid용)
        /// </summary>
        /// <param name="TableLayout.SrchParam"></param>
        /// <returns></returns>
        internal DataTable SelectTableLayout(TableLayout.SrchParam param)
        {
            string sSpName = "SP_TableLayout";
            string sGubun = "SelectTableLayout";

            string[] pParam = new string[1];
            pParam[0] = "@table_name:" + param.table_name;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        /// <summary>
        /// AuditTarget 조회
        /// </summary>
        /// <param name="TableLayout.SrchParam"></param>
        /// <returns></returns>
        internal DataTable SelectAuditTarget(TableLayout.SrchParam param)
        {
            string sSpName = "SP_TableLayout";
            string sGubun = "AuditTarget";

            string[] pParam = new string[1];
            pParam[0] = "@table_name:" + param.table_name;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string TableLayoutUpdate(List<TableLayout> param)
        {
            string sSpName = "SP_TableLayout";
            string sGubun = "UpdateProperty";
            string res = "";

            foreach (TableLayout tmp in param)
            {
                string[] pParam = new string[4];
                pParam[0] = "@table_name:" + tmp.table_name;
                pParam[1] = "@column_name:" + tmp.column_name;
                pParam[2] = "@column_caption:" + tmp.column_caption;
                pParam[3] = "@column_desc:" + tmp.column_description;

                res = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }

            return res;
        }
    }
}