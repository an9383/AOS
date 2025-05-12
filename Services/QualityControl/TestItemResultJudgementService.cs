using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.QualityControl;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace HACCP.Services.QualityControl
{
    public class TestItemResultJudgementService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_TestItemResultJudgement";

        //bool LimsAlram = false;

        #region 조회영역
        /// <summary>
        /// Main 그리드 조회
        /// </summary>
        /// <param name="srch">검색Object</param>
        /// <returns>검색데이터</returns>
        internal DataTable SelectMain(TestItemResultJudgement.SrchParam srch)
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

        internal string TestItemResultJudgementCheckResult(string testcontrol_id, string teststandardmaster_id, string testitem_result)
        {
            string[] pParam = new string[3];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@teststandardmaster_id:" + teststandardmaster_id;
            pParam[2] = "@testitem_result:" + testitem_result;

            string res = _bllSpExecute.SpExecuteString("SP_JudgeForTestItem", "R_C", pParam);

            return res;
        }

        #endregion

        #region Trx영역
        /// <summary>
        /// 데이터 저장/수정/삭제
        /// </summary>
        /// <param name="dto">Data</param>        
        /// <param name="srch">Search</param>
        /// <returns>저장처리후메시지</returns>
        internal string DeleteTrx(TestItemResultJudgement.SrchParam dto)
        {
            var param = "@testcontrol_id:" + dto.testcontrol_id;
            string res = _bllSpExecute.SpExecuteString(this.sSpName, "changeteststatus", param);
            return res;
        }        

        /// <summary>
        /// Multi 데이터 저장/수정/삭제
        /// </summary>
        /// <param name="dto"></param>        
        /// <returns></returns>
        internal string MultiTrx(List<TestItemResultJudgement.TestItemResultJudgementDetailArrange> dto)
        {
            string[] param = null;
            string res = string.Empty;
            string gubun = string.Empty;
            string message = string.Empty;

            // 수정은 multi 처리
            if (dto == null)
            {
                return res;
            }
            else
            {                
                for (int i = 0; i < dto.Count; i++)
                {
                    TrxType rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto[i].row_status);
                    switch (rowStatus)
                    {
                        case TrxType.U:
                            gubun = "U";
                            //시험항목이 코드가 아니면(그룹이면), 체크할 필요가 없다.
                            if (!"C".Equals(dto[i].code_type))   continue;

                            // 1. 체크가 되어 있지 않으면 저장하지 않는다.
                            if (!"Y".Equals(dto[i].select_yn)) continue;

                            param = TrxGetParam(dto[i]);
                            res = _bllSpExecute.SpExecuteString("SP_JudgeForTestItem", gubun, param);

                            break;
                    }
                }                
                //if (res.Length > 0)
                //{
                    res = "수정 되었습니다.";
                //}

            }
            return res;
        }

        /// <summary>
        /// 작성일:2006.06.01
        /// 작성자:최석중
        /// 설  명:필수 전자서명이 설정되지 않은 경우 상태값을 곧바로 변경한다.
        /// </summary>
        //private bool SetNextStatus(TestSchedule dto)
        //{
        //    bool check = false;

        //    //최종 서명자 조회
        //    //int i = gv_TestSchedule.FocusedRowHandle;
        //    if (dto != null)
        //    {
        //        string gubun = "EN";

        //        string testcontrol_id = "@testcontrol_id:" + dto.testcontrol_id;
        //        string test_type = "@test_type:" + dto.test_type;

        //        _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", gubun, testcontrol_id, test_type,  process_kind, program_cd, sign_set_cd);

        //        check = true;
        //    }

        //    return check;
        //}

        /// 설  명:서명 결과에 따른 상태값 변경 내역을 보여준다.
        /// </summary>
        //private bool SetTestInfo(TestSchedule dto)
        //{
        //    //int i = gv_TestSchedule.FocusedRowHandle;

        //    bool check = false;

        //    if (dto != null)
        //    {
        //        string gubun = "SI";
        //        DataTable myTable = new DataTable();
        //        string testcontrol_id = "@testcontrol_id:" + dto.testcontrol_id;

        //        myTable = _bllSpExecute.SpExecuteTable("SP_Test_ES_Manage", gubun, testcontrol_id,
        //            process_kind, program_cd, sign_set_cd);                                
        //        if (myTable.Rows.Count > 0)
        //        {                
        //            check = true;
        //        }
        //    }
        //    return check;
        //}

        /// <summary>
        /// Sampling_check
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        internal bool Sampling_check(TestItemResultJudgement dto)
        {
            bool check = false;
            string message = "";

            try
            {
                //최종 서명자 조회            
                if (dto != null)
                {
                    string gubun = "SC";

                    string testcontrol_id = "@testcontrol_id:" + dto.testcontrol_id;

                    message = _bllSpExecute.SpExecuteString("SP_JudgeForTestItem", gubun, testcontrol_id);
                }

                //서명이 되지 않은 자료는 수정,삭제할 수 있다.
                if (message == "true")
                {
                    check = true;
                }
            }
            catch { }
            
            //else if ("Y".Equals(dto.select_yn))
            //{
            //    check = true;
            //}
            return check;
        }

        #endregion

        #region 파일관리 설정
        internal DataTable SelectFile(string testcontrol_id)
        {
            string[] pParam = new string[1];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_AttachFile", "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        internal void uploadFile(byte[] myBytes, string fileName, string contentType, string testcontrol_id)
        {
            string doc_type = MimeTypeMap.GetExtension(contentType);

            //if (contentType.Equals("application/haansofthwp"))
            //{
            //    doc_type = ".hwp";
            //}
            //else if (contentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
            //{
            //    doc_type = ".docx";
            //}
            //else if (contentType.Equals("application/msword"))
            //{
            //    doc_type = ".doc";
            //}

            string sSpName = "SP_AttachFile";

            string[] pParam = new string[3];
            pParam[0] = "@doc_name:" + fileName;
            pParam[1] = "@doc_type:" + doc_type;
            pParam[2] = "@testcontrol_id:" + testcontrol_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, "I", "@file_image", myBytes, pParam);
        }


        internal DataTable DownloadFile(string testcontrol_id, string file_id)
        {
            string sSpName = "SP_AttachFile";
            string sGubun = "O";

            string[] pParam = new string[2];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@file_id:" + file_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal string DeleteFile(string testcontrol_id, string file_id)
        {
            string sSpName = "SP_AttachFile";

            string[] pParam = new string[2];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@file_id:" + file_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, "D", pParam);

            return message;
        }
        #endregion

        #region SP파라미터 설정
        /// <summary>
        /// SP 파라미터 설정-조회
        /// </summary>
        /// <param name="srch"></param>        
        /// <returns></returns>
        private string[] GetParam(Object gridGb)
        {
            string[] param = null;

            if (gridGb is TestItemResultJudgement.SrchParam)
            {
                TestItemResultJudgement.SrchParam srch = (TestItemResultJudgement.SrchParam)gridGb;
                if ("S".Equals(srch.Gubun))
                {
                    param = new string[8];
                    param[0] = "@s_date_option:" + srch.re_date_option;
                    param[1] = "@s_start_date:" + srch.de_start_date;
                    param[2] = "@s_end_date:" + srch.de_end_date;
                    param[3] = "@s_test_type:" + srch.le_s_test_type;
                    param[4] = "@s_test_status:" + srch.re_test_status;
                    param[5] = "@search_gubun:" + srch.ce_gubun_number;
                    param[6] = "@search_number:" + srch.te_number;                    
                    param[7] = "@item_form_cd:" + srch.le_item_form_cd;
                }
                else if ("S2".Equals(srch.Gubun))
                {
                    param = new string[1];                    
                    param[0] = "@testcontrol_id:" + srch.testcontrol_id;                    
                }
            } 
            
            return param;
        }
        /// <summary>
        /// SP 파라미터 설정-CRUD
        /// </summary>
        /// <param name="dto">Data</param>
        /// <param name="srch">Search</param>
        /// <returns>파라미터배열</returns>
        private string[] TrxGetParam(Object gridGb)
        {
            string[] param = null;
            TrxType rowStatus;

            if (gridGb is TestItemResultJudgement.TestItemResultJudgementDetailArrange)
            {
                TestItemResultJudgement.TestItemResultJudgementDetailArrange dto = (TestItemResultJudgement.TestItemResultJudgementDetailArrange)gridGb;
                rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto.row_status);
                switch (rowStatus)
                {
                    case TrxType.U:
                        // 수정
                        param = new string[15];
                        param[0] = "@testcontrol_id:" + dto.testcontrol_id;
                        param[1] = "@teststandardmaster_id:" + dto.teststandardmaster_id;
                        param[2] = "@testitem_result:" + dto.testitem_result_value;
                        param[3] = "@testitem_result_yn:" + dto.testitem_result_yn;
                        param[4] = "@testitem_start_date:" + dto.testitem_start_time;
                        param[5] = "@testitem_start_time:00:00:00";
                        param[6] = "@testitem_end_date:" + dto.testitem_end_time;
                        param[7] = "@testitem_end_time:00:00:00";
                        param[8] = "@testitem_emp_cd:" + dto.emp_cd;
                        param[9] = "@testitem_type:" + "";
                        param[10] = "@testitem_date:" + DateTime.Today.ToString("yyyy-MM-dd");
                        param[11] = "@testitem_result_remark:" + dto.testitem_result_remark;
                        param[12] = "@testitem_inputtime:" + (dto.testitem_inputtime == null ? 0 : dto.testitem_inputtime);
                        param[13] = "@testitem_totaltime:" + (dto.testitem_totaltime == null ? 0 : dto.testitem_totaltime);
                        param[14] = "@emp_ck:" + "N";

                        break;
                    default:
                        param = null;
                        break;
                }
            }           

            return param;
        }

        internal string TestItemResultJudgementCheckTest(string testcontrol_id, string gubun)
        {
            string[] pParam = new string[1];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;

            string res = _bllSpExecute.SpExecuteString("SP_JudgeForTestItem", gubun, pParam);

            return res;
        }

        internal string TestItemResultJudgementUpdateCheck(string testcontrol_id, string test_type)
        {
            string[] pParam = new string[2];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;

            string res = _bllSpExecute.SpExecuteString("SP_JudgeForTestItem", "UpdateCheck", pParam);

            return res;
        }

        internal string TestItemResultJudgementCancelResult(string testcontrol_id, string teststandardmaster_id)
        {
            string[] pParam = new string[2];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@teststandardmaster_id:" + teststandardmaster_id;

            string res = _bllSpExecute.SpExecuteString("SP_JudgeForTestItem", "UC", pParam);

            return res;
        }

        #endregion
    }
}
