using HACCP.Libs;
using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.Comm
{
    public class CodeHelp_TestNoService
    {
        private string mstrdefaultsql;
        private string mstrsql;
        //private bool mresult;
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        //whPlant m_whplant;
        //tgTable m_tgtable;
        //whColumns m_whcolumn;
        //dsColumns[] m_dscolumns;
        //outColumns[] m_outcolumns;

        #region 초기 셋팅
        public CodeHelp_TestNoService()
        {
            mstrsql = "";
            //mresult = false;
        }

        #endregion

        private void makeDefaultQuery()
        {
            mstrdefaultsql = "SELECT     a.item_cd, b.item_nm, a.testrequest_no, a.test_no, a.start_no, a.request_date , a.testcontrol_id"
            + " FROM         testcontrol a "
            + "LEFT OUTER JOIN item_standard b ON a.item_cd = b.item_cd";

        }

        private void addFirstParameter(string item_cd)
        {
            mstrsql = mstrdefaultsql
                + " where a.item_cd = '" + item_cd + "'";
        }

        private void addSecondParameter(string start_no)
        {
            mstrsql = mstrsql
                + " and a.start_no = '" + start_no + "'";
        }

        public DataTable GetCode(string item_cd, string item_nm, string start_no)
        {
            makeDefaultQuery();

            addFirstParameter(item_cd);
            addSecondParameter(start_no);

            DataTable dt = Grid_Select(mstrsql);

            //반환
            return dt;
        }

        private DataTable Grid_Select(string strsql)
        {
            /* Grid Setting */
            DataTable myTable = new DataTable();
            myTable = _bllSpExecute.SpExecuteTable("CODEHELP", strsql); //그리드 데이타 적용 =>필드, 밴드, 모양등을 결정하고 나서...            			

            return myTable;
        }

    }
}