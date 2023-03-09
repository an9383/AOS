using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Equipment;
using System;
using System.Data;

namespace HACCP.Services.Equipment
{
    public class PartHistoryService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_PartHistory";

        #region 조회영역
        /// <summary>
        /// Main 그리드 조회
        /// </summary>
        /// <param name="srch">검색Object</param>
        /// <returns>검색데이터</returns>
        internal DataTable SelectMain(PartHistory.SrchParam srch)
        {
            // 검색 파라미터 설정
            var param = GetParam(srch);

            DataSet ds = _bllSpExecute.SpExecuteDataSet(this.sSpName, srch.Gubun, param);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// Sub 그리드 조회 : 예방점검이력 그리드 조회
        /// </summary>
        /// <param name="rowKey">Main 그리드 키값</param>
        /// <returns></returns>
        internal DataTable SelectSubSS(string rowKey)
        {
            var param = "@equip_cd:" + rowKey;
            string gubun = "SS";

            DataTable table = _bllSpExecute.SpExecuteTable(this.sSpName, gubun, param);

            return table;
        }

        /// <summary>
        /// Sub 그리드 조회 : 고장수리이력 그리드 조회
        /// </summary>
        /// <param name="rowKey">Main 그리드 키값</param>
        /// <returns></returns>
        internal DataTable SelectSubSP(string rowKey)
        {
            var param = "@equip_cd:" + rowKey;
            string gubun = "SP";

            DataTable table = _bllSpExecute.SpExecuteTable(this.sSpName, gubun, param);

            return table;
        }


        #endregion


        #region SP파라미터 설정
        /// <summary>
        /// SP 파라미터 설정-조회
        /// </summary>
        /// <param name="srch"></param>        
        /// <returns></returns>
        private string[] GetParam(PartHistory.SrchParam srch)
        {
            string[] param;
            if ("S".Equals(srch.Gubun))
            {                
                param = new string[2];
                param[0] = "@equip_type:" + srch.S_equip_type;
                param[1] = "@equip_cd:" + srch.S_equipment;
            }
            else
            {
                param = null;
            }
            return param;
        }
        
        #endregion
    }
}
