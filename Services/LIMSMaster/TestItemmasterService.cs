using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.LIMSMaster;
using System;
using System.Data;

namespace HACCP.Services.LIMSMaster
{
    public class TestItemmasterService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_Testitemmaster";

        #region 조회영역
        /// <summary>
        /// Main 그리드 조회
        /// </summary>
        /// <param name="srch">검색Object</param>
        /// <returns>검색데이터</returns>
        internal DataTable SelectMain(TestItemmaster.SrchParam srch)
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
        /// 채번 
        /// </summary>   
        /// <returns></returns>
        internal string GetSeqNo()
        {
            string seqNo = string.Empty;
            /*
                        DataTable testitem_cd_table = new DataTable();

                        testitem_cd_table = _bllSpExecute.SpExecuteTable(this.sSpName, "N", "");

                        if (testitem_cd_table.Rows.Count > 0)
                        {
                            seqNo = testitem_cd_table.Rows[0]["testitem_cd"].ToString();
                        }
            */
            seqNo = _bllSpExecute.SpExecuteString(this.sSpName, "N");

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
        internal string DataTrx(TestItemmaster dto, TestItemmaster.SrchParam srch)
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
        private string[] GetParam(TestItemmaster.SrchParam srch)
        {
            string[] param;
            if ("S".Equals(srch.Gubun))
            {                
                param = new string[4];
                param[0] = "@testitem_type:" + srch.s_testitem_type;
                param[1] = "@testitem_cd:" + srch.s_testitem_cd;
                param[2] = "@testitem_charge:" + srch.s_testitem_charge;
                param[3] = "@use_yn:" + srch.s_use_yn;
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
        private string[] GetParam(TestItemmaster dto, TestItemmaster.SrchParam srch)
        {            
            TrxType rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto.row_status);
            
            string[] param;
            switch (rowStatus)
            {
                case TrxType.I:
                    // 입력
                    param = new string[8]; 
                    param[0] = "@testitem_cd:" + dto.testitem_cd;
                    param[1] = "@testitem_nm:" + dto.testitem_nm;
                    param[2] = "@testitem_enm:" + dto.testitem_enm;
                    param[3] = "@testitem_type:" + dto.testitem_type;
                    param[4] = "@testitem_charge:" + dto.testitem_charge;
                    param[5] = "@use_yn:" + dto.use_yn;
                    param[6] = "@dept_cd:" + dto.dept_cd;
                    param[7] = "@code_type:" + dto.code_type;
                    //param[8] = "@test_cost:" + dto.test_cost;
                    break;
                case TrxType.U:
                    // 수정
                    param = new string[8];
                    param[0] = "@testitem_cd:" + dto.testitem_cd;
                    param[1] = "@testitem_nm:" + dto.testitem_nm;
                    param[2] = "@testitem_enm:" + dto.testitem_enm;
                    param[3] = "@testitem_type:" + dto.testitem_type;
                    param[4] = "@testitem_charge:" + dto.testitem_charge;
                    param[5] = "@use_yn:" + dto.use_yn;
                    param[6] = "@dept_cd:" + dto.dept_cd;
                    param[7] = "@code_type:" + dto.code_type;
                    //param[8] = "@test_cost:" + dto.test_cost;
                    break;
                case TrxType.D:
                    // 삭제
                    param = new string[1];
                    param[0] = "@testitem_cd:" + dto.testitem_cd;
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
