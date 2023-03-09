using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Equipment;
using System;
using System.Data;

namespace HACCP.Services.Equipment
{
    public class PartManageService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_PartManage";

        #region 조회영역
        /// <summary>
        /// Main 그리드 조회
        /// </summary>
        /// <param name="srch">검색Object</param>
        /// <returns>검색데이터</returns>
        internal DataTable SelectMain(PartManage.SrchParam srch)
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
            var param = "@part_cd:" + rowKey;
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
        internal string DataTrx(PartManage dto, PartManage.SrchParam srch)
        {           
            var param = GetParam(dto, srch);            

            string res = _bllSpExecute.SpExecuteString(this.sSpName, dto.row_status, param);
            return res;
        }
        #endregion

        #region SP파라미터 설정
        /// <summary>
        /// SP 파라미터 설정-조회
        /// </summary>
        /// <param name="srch"></param>        
        /// <returns></returns>
        private string[] GetParam(PartManage.SrchParam srch)
        {
            string[] param;
            if ("S".Equals(srch.Gubun))
            {                
                param = new string[3];
                param[0] = "@part_kind:" + srch.S_part_kind;
                param[1] = "@use_gb:" + srch.S_use_gb;
                param[2] = "@part_cd:" + srch.S_part;
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
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>
        /// <returns>파라미터배열</returns>
        private string[] GetParam(PartManage dto, PartManage.SrchParam srch)
        {            
            TrxType rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto.row_status);

            string[] param;
            switch (rowStatus)
            {
                case TrxType.I:
                case TrxType.U:
                    param = new string[10];
                    // 입력
                    param[0] = "@part_cd:" + dto.part_cd;  //부품코드
                    param[1] = "@part_nm:" + dto.part_nm;  //부품명
                    param[2] = "@part_enm:" + dto.part_enm;  //부품명(영문)
                    param[3] = "@part_kind:" + dto.part_kind_cd;  //부품종류
                    param[4] = "@part_response_emp_cd:" + dto.part_response_emp_cd;  //책임자코드
                    param[5] = "@use_gb:" + dto.use_gb_cd;  //사용여부  1:사용,  2:미사용
                    param[6] = "@part_buy_amt:" + dto.part_buy_amt; //단가
                    param[7] = "@part_in_date:" + dto.part_in_date;  //입출고일자
                    param[8] = "@part_shape:" + dto.part_shape;  //부품모양
                    param[9] = "@part_stock_qty:" + dto.part_stock_qty;  //재고수
                    break;
                case TrxType.D:
                    param = new string[1];
                    // 삭제
                    param[0] = "@part_cd:" + dto.part_cd;  //부품코드           
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
