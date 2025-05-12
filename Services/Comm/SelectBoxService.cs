using HACCP.Libs.Database;
using System;
using System.Data;

namespace HACCP.Services.Comm
{
    public class SelectBoxService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        /// <summary>
        /// 셀렉트 박스 안의 option 들을 가져오는 메소드
        /// </summary>
        /// <param name="pGubun"></param>
        /// <param name="pDiv"></param>
        /// <param name="pStrwhere"></param>
        /// <param name="pDefault"></param>
        /// <returns></returns>
        internal DataTable GetSelectBox(string pGubun, string pDiv, string pStrwhere, string TableName)
        {
            string sSpName = "SP_GETMASTER";
            string[] pParam = new string[3];
            pParam[0] = "@gb:" + pGubun;
            pParam[1] = "@div:" + pDiv;
            pParam[2] = "@strwhere:" + pStrwhere;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, pGubun, pParam);
            dt.TableName = TableName;

            return dt;
        }

        internal DataTable GetSelectBox(string pGubun, string pDiv, string pStrwhere, string pStrwhere2, string TableName)
        {
            string sSpName = "SP_GETMASTER";
            string[] pParam = new string[4];
            pParam[0] = "@gb:" + pGubun;
            pParam[1] = "@div:" + pDiv;
            pParam[2] = "@strwhere:" + pStrwhere;
            pParam[3] = "@strwhere2:" + pStrwhere2;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, pGubun, pParam);
            dt.TableName = TableName;

            return dt;
        }

    }
}
