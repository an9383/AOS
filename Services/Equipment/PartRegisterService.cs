using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Equipment;
using System;
using System.Data;

namespace HACCP.Services.Equipment
{
    public class PartRegisterService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_PartRegister";

        #region 조회영역
        /// <summary>
        /// Main 그리드 조회
        /// </summary>
        /// <param name="srch">검색Object</param>
        /// <returns>검색데이터</returns>
        internal DataTable SelectMain(PartRegister.SrchParam srch)
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
        /// Sub 그리드 조회 : $기능입력$ 그리드 조회
        /// </summary>
        /// <param name="rowKey">Main 그리드 키값</param>
        /// <returns></returns>
        internal DataTable SelectSub(string rowKey)
        {
            var param = "@equip_cd:" + rowKey;
            string gubun = "SR";

            DataTable table = _bllSpExecute.SpExecuteTable(this.sSpName, gubun, param);

            return table;
        }
        
        #endregion

        #region Trx영역
        /// <summary>
        /// 데이터 저장/수정/삭제
        /// </summary>
        /// <param name="dto">Data</param>        
        /// <param name="srch">Search</param>
        /// <returns>저장처리후메시지</returns>
        internal string DataTrx(string equip_cd, PartRegisterEqupPart dto)
        {           
            var param = GetParam(equip_cd, dto);            

            string res = _bllSpExecute.SpExecuteString(this.sSpName, dto.row_status, param);
            return res;
        }

        /// <summary>
        /// 데이터 저장/수정/삭제
        /// </summary>
        /// <param name="dto">Data</param>        
        /// <param name="srch">Search</param>
        /// <returns>저장처리후메시지</returns>        
        //internal string DataTrx(PartRegister dto, PartRegister.SrchParam srch)
        //{
        //    var param = GetParam(dto, srch);

        //    string res = _bllSpExecute.SpExecuteString(this.sSpName, dto.row_status, param);
        //    return res;
        //}
        #endregion

        #region SP파라미터 설정
        /// <summary>
        /// SP 파라미터 설정-조회
        /// </summary>
        /// <param name="srch"></param>        
        /// <returns></returns>
        private string[] GetParam(PartRegister.SrchParam srch)
        {
            string[] param;
            if ("S".Equals(srch.Gubun))
            {
                param = new string[1];
                param[0] = "@equip_cd:" + srch.S_equipment;
            }
            else
            {
                param = null;
            }
            return param;
        }
        /// <summary>
        /// SP 파라미터 설정-CRUD
        /// </summary>
        /// <param name="equip_cd">equip_cd</param>
        /// <param name="dto">Data</param>
        /// <returns>파라미터배열</returns>
        private string[] GetParam(string equip_cd, PartRegisterEqupPart dto)
        {            
            TrxType rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto.row_status);

            string[] param;
            switch (rowStatus)
            {
                case TrxType.I:
                case TrxType.D:
                    // 입력
                    param = new string[2];
                    param[0] = "@equip_cd:" + equip_cd;
                    param[1] = "@part_cd:" + dto.part_cd;
                    
                    break;                        
                default:
                    param = null;
                    break;
            }

            return param;
        }
        #endregion
    }
}
