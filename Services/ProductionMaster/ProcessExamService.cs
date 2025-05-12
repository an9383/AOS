using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.ProductionMaster;
using System;
using System.Data;
using System.Web;

namespace HACCP.Services.ProductionMaster
{
    public class ProcessExamService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_PROCESSEXAM";

        #region 조회영역
        /// <summary>
        /// Main 그리드 조회
        /// </summary>
        /// <param name="srch">검색Object</param>
        /// <returns>검색데이터</returns>
        internal DataTable SelectMain(ProcessExam.SrchParam srch)
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
        #endregion

        #region Trx영역
        /// <summary>
        /// 데이터 저장/수정/삭제
        /// </summary>
        /// <param name="dto">Data</param>        
        /// <param name="srch">Search</param>
        /// <returns>저장처리후메시지</returns>
        internal string DataTrx(ProcessExam dto, ProcessExam.SrchParam srch)
        {
            var param = GetParam(dto, srch);

            string res = _bllSpExecute.SpExecuteString(this.sSpName, dto.row_status, param);
            return res;
        }

        internal string ProcessExamMasterTRX(string gubun, ProcessExamMaster srch)
        {
            string[] param;
            string res;

            if (gubun.Equals("MD"))
            {
                param = new string[1];
                param[0] = "@ccp_cd:" + srch.ccp_cd;
                
                res = _bllSpExecute.SpExecuteString(this.sSpName, gubun, param);

            }
            else if(gubun.Equals("MU") || gubun.Equals("MI"))
            {
                param = new string[12];
                param[0] = "@ccp_cd:" + srch.ccp_cd;
                param[1] = "@ccp_nm:" + srch.ccp_nm;
                param[2] = "@ccp_description:" + srch.ccp_description;
                param[3] = "@equip_cd:" + srch.equip_cd;
                param[4] = "@grand_item:" + srch.grand_item;
                param[5] = "@middle_item:" + srch.middle_item;
                param[6] = "@cause_harm:" + srch.cause_harm;
                param[7] = "@limit_standard:" + srch.limit_standard;
                param[8] = "@element_harm:" + srch.element_harm;
                param[9] = "@monitoring_way:" + srch.monitoring_way;
                param[10] = "@process_exam_manu:" + srch.process_exam_manu;
                param[11] = "@process_exam_qc:" + srch.process_exam_qc;
                
                res = _bllSpExecute.SpExecuteString(this.sSpName, gubun, param);
            }
            else
            {
                res = "";
            }


            return res;
        }
        #endregion

        #region SP파라미터 설정
        /// <summary>
        /// SP 파라미터 설정-조회
        /// </summary>
        /// <param name="srch"></param>        
        /// <returns></returns>
        private string[] GetParam(ProcessExam.SrchParam srch)
        {
            string[] param;
            if ("S".Equals(srch.Gubun) || "MS".Equals(srch.Gubun))
            {                
                param = new string[1];
                param[0] = "@s_process_cd:" + srch.s_process_cd;
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
        private string[] GetParam(ProcessExam dto, ProcessExam.SrchParam srch)
        {            
            TrxType rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto.row_status);
            
            string[] param;
            
            param = new string[19];
            param[0] = "@s_process_cd:" + srch.s_process_cd;
            param[1] = "@process_cd:" + dto.process_cd;
            param[2] = "@process_exam_cd:" + dto.process_exam_cd;
            param[3] = "@process_exam_standard:" + dto.process_exam_standard;
            param[4] = "@process_exam_period:" + dto.process_exam_period;
            param[5] = "@process_exam_qty:" + dto.process_exam_qty;
            param[6] = "@process_exam_manu:" + dto.process_exam_manu;
            param[7] = "@process_exam_qc:" + dto.process_exam_qc;
            param[8] = "@insert_emp:" + HttpContext.Current.Session["loginCD"].ToString();
            param[9] = "@equip_cd:" + dto.equip_cd;
            param[10] = "@process_exam_unit:" + dto.process_exam_unit;
            param[11] = "@process_exam_min:" + dto.process_exam_min;
            param[12] = "@process_exam_max:" + dto.process_exam_max;
            param[13] = "@process_exam_start:" + dto.process_exam_start;
            param[14] = "@grand_item:" + dto.grand_item;
            param[15] = "@middle_item:" + dto.middle_item;
            param[16] = "@item_seq:" + dto.item_seq;
            param[17] = "@audittrail_id:" + dto.audittrail_id;
            param[18] = "@ccp_yn:" + dto.ccp_yn;

            return param;
        }

        #endregion

        #region 팝업관련
        // ccp코드조회
        internal DataTable getCcpCdPopupData()
        {
            string strsql = "SELECT ccp_cd, ccp_nm";
            strsql += " FROM process_exam_master";
            strsql += " GROUP BY ccp_cd, ccp_nm";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }
        #endregion

    }
}