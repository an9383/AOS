using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Equipment;
using System;
using System.Data;

namespace HACCP.Services.Equipment
{
    public class TroubleConductService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_TroubleConduct";

        #region 조회영역
        /// <summary>
        /// Main 그리드 조회
        /// </summary>
        /// <param name="srch">검색Object</param>
        /// <returns>검색데이터</returns>
        internal DataTable SelectMain(TroubleConduct.SrchParam srch)
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
        /// Sub 그리드 조회 : 사용부품 그리드 조회
        /// </summary>
        /// <param name="rowKey">Main 그리드 키값(afterserivce_no)</param>
        /// <returns></returns>
        internal DataTable SelectSub(string rowKey)
        {
            var param = "@afterservice_no:" + rowKey;
            string gubun = "SP";            
           
            DataTable table = _bllSpExecute.SpExecuteTable(this.sSpName, gubun, param);

            /*if (UsedPart_Table.Rows.Count > 0)
            {
                gc_parts.DataSource = UsedPart_Table;
                check = true;
            }
            else
            {
                gc_parts.DataSource = null;
                check = false;
            }*/

            return table;
        }

        /// <summary>
        /// 채번 
        /// </summary>
        /// <param name="edate">조회조건 종료일자</param>        
        /// <returns></returns>
        internal string GetSeqNo(string edate)
        {
            var param = "@today_date:" + edate;
            string gubun = "GET_afterservice_no";

            string seqNo = string.Empty;
            DataTable afterservice_no_table = new DataTable();

            afterservice_no_table = _bllSpExecute.SpExecuteTable(this.sSpName, gubun, param);

            if (afterservice_no_table.Rows.Count > 0)
            {
                seqNo = afterservice_no_table.Rows[0]["afterservice_no"].ToString();
            }
            return seqNo;
        }

        #endregion

        #region Trx영역
        /// <summary>
        /// 데이터 저장/수정/삭제
        /// </summary>
        /// <param name="dto">Data</param>        
        /// <param name="srch">Search</param>
        /// <returns>저장처리후메시지</returns>
        internal string DataTrx(TroubleConduct dto, TroubleConduct.SrchParam srch)
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
        private string[] GetParam(TroubleConduct.SrchParam srch)
        {
            string[] param;
            if ("S".Equals(srch.Gubun))
            {                
                param = new string[5];
                param[0] = "@sdate:" + srch.sdate;
                param[1] = "@edate:" + srch.edate;
                param[2] = "@equip:" + srch.equipment;
                param[3] = "@period:" + srch.rb_gb;
                param[4] = "@status:" + srch.status;             
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
        private string[] GetParam(TroubleConduct dto, TroubleConduct.SrchParam srch)
        {
            TrxType rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto.row_status);

            string[] param;
            switch (rowStatus)
            {
                case TrxType.U:
                    // 수정
                    param = new string[8];
                    param[0] = "@receipt_emp_cd:" + dto.receipt_emp_cd;
                    param[1] = "@repair_emp_cd:" + dto.repair_emp_cd;
                    param[2] = "@error_contents:" + dto.error_content;
                    param[3] = "@repair_method:" + dto.repair_method;
                    param[4] = "@repair_date:" + dto.repair_date;
                    param[5] = "@repair_status:" + dto.repair_status_cd;
                    //param[6] = "@repair_price:" + (ce_repair_price.Text == "" ? "0" : ce_repair_price.Text.Replace(",", ""));
                    param[6] = "@repair_price:" + dto.repair_price;
                    param[7] = "@afterservice_no:" + dto.afterservice_no;

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
