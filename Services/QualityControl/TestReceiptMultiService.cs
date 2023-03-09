using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.QualityControl;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace HACCP.Services.QualityControl
{
    public class TestReceiptMultiService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "sp_TestReceiptE";

        //전자서명
        private string process_kind = "@process_kind:1";
        private string program_cd = "@program_cd:TestReceiptMulti";
        private string sign_set_cd = "@sign_set_cd:1002";

        #region 조회영역
        /// <summary>
        /// Main 그리드 조회
        /// </summary>
        /// <param name="srch">검색Object</param>
        /// <returns>검색데이터</returns>
        internal DataTable SelectMain(TestReceiptMulti.SrchParam srch)
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
        /// <param name="edate">조회조건 종료일자</param>        
        /// <returns></returns>
        internal string GetSeqNo(string edate)
        {
            string seqNo = string.Empty;
            DataTable afterservice_no_table = new DataTable();

            afterservice_no_table = _bllSpExecute.SpExecuteTable(this.sSpName, "GET_afterservice_no", "@today_date:" + edate);

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
        internal string DataTrx(List<TestReceiptMulti> dto, TestReceiptMulti.SrchParam srch)
        {
            string[] param = null;
            string res = string.Empty;

            // 수정은 multi 처리, 삭제는 단건 처리 기준!!
            if (dto == null)
            {
                return res;
            }
            else
            {
                TrxType rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto[0].row_status);
                switch (rowStatus) {
                    case TrxType.U: 
                        for (int i = 0; i < dto.Count; i++)
                        {
                            if ("Y".Equals(dto[i].select_yn))
                            {
                                param = GetParam(dto[i], srch);
                                res = _bllSpExecute.SpExecuteString(this.sSpName, dto[i].row_status, param);
                                if (res.Length != 0)
                                {
                                    if (SetNextStatus(dto[i]))
                                    {
                                        SetTestInfo(dto[i]);
                                    }
                                }
                            }                            
                        }
                        if (res.Length > 0)
                        {
                            res = "저장 되었습니다.";
                        }
                        break;

                    case TrxType.D:
                        param = GetParam(dto[0], srch);
                        res = _bllSpExecute.SpExecuteString(this.sSpName, dto[0].row_status, param);
                        
                        if(res.Length != 0)
                        {
                            SetTestInfo(dto[0]);
                            res = "접수 취소되었습니다.";
                        }
                        break;
                }
            }
            return res;
        }

        /// <summary>
        /// 작성일:2006.06.01
        /// 작성자:최석중
        /// 설  명:필수 전자서명이 설정되지 않은 경우 상태값을 곧바로 변경한다.
        /// </summary>
        private bool SetNextStatus(TestReceiptMulti dto)
        {
            bool check = false;

            //최종 서명자 조회
            //int i = gv_TestReceiptMulti.FocusedRowHandle;
            if (dto != null)
            {
                string gubun = "EN";

                string testcontrol_id = "@testcontrol_id:" + dto.testcontrol_id;
                string test_type = "@test_type:" + dto.test_type;

                _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", gubun, testcontrol_id, test_type,  process_kind, program_cd, sign_set_cd);

                check = true;
            }

            return check;
        }

        /// <summary>
        /// 작성일:2006.06.01
        /// 작성자:최석중
        /// 설  명:서명 결과에 따른 상태값 변경 내역을 보여준다.
        /// </summary>
        private bool SetTestInfo(TestReceiptMulti dto)
        {
            //int i = gv_TestReceiptMulti.FocusedRowHandle;

            bool check = false;

            if (dto != null)
            {
                string gubun = "SI";
                DataTable myTable = new DataTable();
                string testcontrol_id = "@testcontrol_id:" + dto.testcontrol_id;

                myTable = _bllSpExecute.SpExecuteTable("SP_Test_ES_Manage", gubun, testcontrol_id,
                    process_kind, program_cd, sign_set_cd);                                
                if (myTable.Rows.Count > 0)
                {                
                    check = true;
                }
            }
            return check;
        }

        /// <summary>
        /// 삭제(취소) 가능한 상태인지 체크한다
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        internal bool ESStatusCheck(TestReceiptMulti dto)
        {
            bool check = false;
            string message = "";

            //최종 서명자 조회            
            if (dto != null)
            {
                string gubun = "ST";

                string test_type = "@test_type:" + dto.test_type;
                string test_status = "@test_status:" + dto.test_status;

                message = _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", gubun, test_type, test_status,
                    process_kind, program_cd, sign_set_cd);
            }

            //서명이 되지 않은 자료는 수정,삭제할 수 있다.
            if (message == "ok")
            {
                check = true;
            }
            else if ("Y".Equals(dto.select_yn))
            {
                check = true;
            }
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
        private string[] GetParam(TestReceiptMulti.SrchParam srch)
        {
            string[] param;
            if ("S".Equals(srch.Gubun))
            {                
                param = new string[9];
                param[0] = "@reqorrec:" + srch.rg_ReqorRec;
                param[1] = "@sdate:" + srch.de_start_date;
                param[2] = "@edate:" + srch.de_end_date;
                param[3] = "@test_type:" + srch.le_test_type;
                param[4] = "@test_status:" + srch.le_test_status;
                param[5] = "@search_gubun:" + srch.ce_gubun_number;
                param[6] = "@search_number:" + srch.te_number;
                param[7] = "@item_form_cd:" + srch.le_item_form_cd;
                param[8] = "@search_stability:" + srch.ckStabilityAll;
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
        private string[] GetParam(TestReceiptMulti dto, TestReceiptMulti.SrchParam srch)
        {            
            TrxType rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto.row_status);
            
            string[] param = null;

            switch (rowStatus)
            {
                //case TrxType.I:
                //    입력
                //   param = new string[6];
                //    param[0] = "@equip_cd:" + dto.equip_cd;
                //    param[1] = "@notify_emp_cd:" + dto.notify_emp_cd;
                //    param[2] = "@notify_date:" + dto.notify_date;
                //    param[3] = "@notify_reason:" + dto.notify_reason;
                //    param[4] = "@pre_fix:" + dto.pre_fix;
                //    param[5] = "@today_date:" + srch.edate;
                //    break;
                case TrxType.U:
                    // 수정
                    param = new string[12];
                    //param[0] = "@afterservice_no:" + dto.afterservice_no;
                    //param[1] = "@equip_cd:" + dto.equip_cd;
                    //param[2] = "@notify_emp_cd:" + dto.notify_emp_cd;
                    //param[3] = "@notify_date:" + dto.notify_date;
                    //param[4] = "@notify_reason:" + dto.notify_reason;

                    //param[5] = "@pre_fix:" + dto.pre_fix;
                    param[0] = "@receive_date:" + dto.receive_date;
                    param[1] = "@receive_emp_cd1:";// be_receive_emp_cd1.Text;
                    param[2] = "@receive_emp_cd2:";// be_receive_emp_cd2.Text;
                    param[3] = "@result_plan_date:" + dto.result_plan_date;
                    param[4] = "@testcontrol_id:" + dto.testcontrol_id;
                    param[5] = "@test_type:" + dto.test_type;
                    param[6] = "@test_no:" + dto.test_no;
                    param[7] = "@test_standard:" + dto.test_standard;
                    param[8] = "@all_test_check:" + dto.all_test_check;
                    param[9] = "@test_result_value0:" + dto.test_result_value0;
                    param[10] = "@bigo:" + dto.bigo;
                    param[11] = "@test_status:" + "2";

                    break;
                case TrxType.D:
                    // 삭제
                    param = new string[4];
                    param[0] = "@testcontrol_id:"+dto.testcontrol_id;
                    param[1] = "@test_type:"+dto.test_type;
                    param[2] = "@program_cd:TestReceiptMulti";
                    param[3] = "@item_cd:"+dto.item_cd;

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
