using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Equipment;
using System;
using System.Data;

namespace HACCP.Services.Equipment
{
    public class PartsInOutRegiService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_PartsInOutRegi";

        #region 조회영역
        /// <summary>
        /// Main 그리드 조회
        /// </summary>
        /// <param name="srch">검색Object</param>
        /// <returns>검색데이터</returns>
        internal DataTable SelectMain(PartsInOutRegi.SrchParam srch)
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
        //internal DataTable SelectSub(string rowKey)
        //{
        //    var param = "@afterservice_no:" + rowKey;
        //    string gubun = "SP";

        //    DataTable table = _bllSpExecute.SpExecuteTable(this.sSpName, gubun, param);

        //    return table;
        //}

        /// <summary>
        /// 채번 
        /// </summary>
        /// <param name="edate">조회조건 종료일자</param>        
        /// <returns></returns>
        //internal string GetSeqNo(string edate)
        //{
        //    string seqNo = string.Empty;
        //    DataTable afterservice_no_table = new DataTable();

        //    afterservice_no_table = _bllSpExecute.SpExecuteTable(this.sSpName, "GET_afterservice_no", "@today_date:" + edate);
        //    if (afterservice_no_table.Rows.Count > 0)
        //    {
        //        seqNo = afterservice_no_table.Rows[0]["afterservice_no"].ToString();
        //    }
        //    return seqNo;
        //}

        #endregion

        #region Trx영역
        /// <summary>
        /// 데이터 저장/수정/삭제
        /// </summary>
        /// <param name="dto">Data</param>        
        /// <param name="srch">Search</param>
        /// <returns>저장처리후메시지</returns>
        internal string DataTrx(PartsInOutRegi dto, PartsInOutRegi.SrchParam srch)
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
        private string[] GetParam(PartsInOutRegi.SrchParam srch)
        {
            string[] param;
            if ("S".Equals(srch.Gubun))
            {                
                param = new string[4];

                param[0] = "@sdate:" + srch.S_sdate; //조회조건 시작 날짜
                param[1] = "@edate:" + srch.S_edate; //조회조건 종료 날짜
                param[2] = "@part:"  + srch.S_part;
                param[3] = "@inout:" + srch.S_inOut;
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
        private string[] GetParam(PartsInOutRegi dto, PartsInOutRegi.SrchParam srch)
        {            
            TrxType rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto.row_status);

            string[] param;
            switch (rowStatus)
            {
                case TrxType.I:
                    // 입력
                    param = new string[12];
                    param[0] = "@part_cd:" + dto.part_cd;
                    param[1] = "@part_history_emp_cd:" + dto.part_history_emp_cd;
                    // param[2] = "@part_history_date:" + DateTime.Today.ToShortDateString();  // 입고일자를 오늘날짜로만 들어가도록 되어 있어서 선택할 수 있도록 수정 2011-06-09 RHM
                    param[2] = "@part_inout_date:" + dto.part_inout_date;
                    param[3] = "@part_history_qty:" + dto.part_history_qty;
                    param[4] = "@part_history_price:" + dto.part_history_price;
                    param[5] = "@part_history_status:" + dto.part_history_status;
                    param[6] = "@part_gb:" + dto.part_gb;
                    param[7] = "@part_buy_cust_cd:" + dto.part_buy_cust_cd;
                    param[8] = "@part_prod_cust_cd:" + dto.part_prod_cust_cd;
                    param[9] = "@part_prod_date:" + dto.part_prod_date;
                    param[10] = "@part_prod_num:" + dto.part_prod_num;
                    param[11] = "@part_history_remark:" + dto.part_history_remark;
                    break;
                case TrxType.U:
                    // 수정
                    param = new string[13];
                    param[0] = "@part_cd:" + dto.part_cd;
                    param[1] = "@part_history_emp_cd:" + dto.part_history_emp_cd;
                    param[2] = "@part_history_id:" + dto.part_history_id;
                    param[3] = "@part_history_qty:" + dto.part_history_qty;
                    param[4] = "@part_history_price:" + dto.part_history_price;
                    param[5] = "@part_history_status:" + dto.part_history_status;
                    param[6] = "@part_gb:" + dto.part_gb;
                    param[7] = "@part_buy_cust_cd:" + dto.part_buy_cust_cd;
                    param[8] = "@part_prod_cust_cd:" + dto.part_prod_cust_cd;
                    param[9] = "@part_prod_date:" + dto.part_prod_date;
                    param[10] = "@part_prod_num:" + dto.part_prod_num;
                    param[11] = "@part_history_remark:" + dto.part_history_remark;
                    param[12] = "@part_inout_date:" + dto.part_inout_date;
                    break;
                case TrxType.D:
                    // 삭제
                    param = new string[1];
                    param[0] = "@part_history_id:" + dto.part_history_id;
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
