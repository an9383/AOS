using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.QualityControl;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace HACCP.Services.QualityControl
{
    public class TestScheduleService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_TestSchedule";

        //전자서명
        private string process_kind = "@process_kind:1";
        private string program_cd = "@program_cd:TestSchedule";
        private string sign_set_cd = "@sign_set_cd:1003";

        //전자서명 루틴에서 현재 인증된 사번과 시스템에 로그인한 사번이 일치해야만 전자서명되는지 여부.
        //Y : 현재 인증된 사번과 시스템에 로그인한 사번이 일치해야 한다.
        //N : 현재 인증된 사번과 시스템에 로그인한 사번이 일치하지 않아도 현재 인증된 사번으로 전자서명된다.
        //private string IsLoginUserCheckAtElectronicSign = "N";

        //private string gTest_status = "";

        //추가 리포트 출력 제어를 위해 사용: 080125
        //private QualityLibrary mQualityLibrary;

        //프로그램별설정프로그램 사용을 위한 수정: 08.02.21
        //string f_program_id = "";

        //문서 파일관리 폴더 조회변수
        //private string Doc_record_folder_value = "";

        //엑셀파일을 저장할 이름 저장공간
        //string fileName;

        //string option1 = "N"; //시험지시 서명 완료후 접수 탭으로 자동 이동하는지 여부
        //string option2 = "";    //리포트에서 전자서명/결재 용어 선택

        //bool LimsAlram = false;

        #region 조회영역
        /// <summary>
        /// Main 그리드 조회
        /// </summary>
        /// <param name="srch">검색Object</param>
        /// <returns>검색데이터</returns>
        internal DataTable SelectMain(TestSchedule.SrchParam srch)
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
        /// 항목조정 그리드 조회
        /// </summary>
        /// <param name="srch"></param>
        /// <returns></returns>
        internal DataTable SelectTestItemMaster(TestSchedule.SrchParamItem srch)
        {
            // 검색 파라미터 설정
            var param = GetParam(srch);

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_TestMasterManagement3", srch.Gubun, param);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// 시험자조회
        /// </summary>
        internal DataTable SelectTester(DataTable le_emp_cd)
        {          
            string gubun = "S4";

            DataTable myTable = new DataTable();

            string schedule = DateTime.Today.ToString("yyyy-MM-dd");
            string emp_cd = string.Empty;

            if (le_emp_cd != null && le_emp_cd.Rows.Count > 0)
            {
                emp_cd = le_emp_cd.Rows[0].Field<string>("keyfield");
            }

            myTable = _bllSpExecute.SpExecuteTable(this.sSpName, gubun,
                "@schedule:" + schedule, "@emp_cd:" + emp_cd);

            return myTable;
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
        /// <param name="dto"></param>
        /// <param name="testcontrol_id"></param>
        /// <param name="instruction_no"></param>
        /// <param name="picking_ordered_emp_cd"></param>
        /// <param name="instruction_date"></param>
        /// <returns></returns>
        internal string DataScheDuleDetailTrx(List<TestSchedule.TestScheduleDetailArrange> dto, string testcontrol_id,  string instruction_no, string picking_ordered_emp_cd, string instruction_date)
        {
            string[] param = null;
            string res = string.Empty;
            string gubun = string.Empty;
            string message = string.Empty;

            // 수정은 multi 처리, 삭제는 단건 처리 기준!!
            if (dto == null)
            {
                return res;
            }
            else
            {
                gubun = "U3";
                for (int i = 0; i < dto.Count; i++)
                {
                    TrxType rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto[i].row_status);
                    switch (rowStatus)
                    {
                        case TrxType.U:
                            param = TrxGetParam(dto[i]);
                            res = _bllSpExecute.SpExecuteString(this.sSpName, gubun, param);

                            break;
                    }
                }                
                //시험지시번호를 입력하지 않았을때 DB에서 자동 생성해서 보여줌.
                if (string.IsNullOrWhiteSpace(instruction_no))
                {
                    gubun = "S6";

                    param = new string[2];
                    param[0] = "@instruction_date:" + instruction_date;
                    param[1] = "@testcontrol_id:" + testcontrol_id;

                    message = _bllSpExecute.SpExecuteString(this.sSpName, gubun, param);
                    instruction_no = message;
                }
                                
                gubun = "U2";

                param = new string[4];
                param[0] = "@testcontrol_id:" + testcontrol_id;
                param[1] = "@instruction_no:" + instruction_no;
                param[2] = "@picking_ordered_emp_cd:" + picking_ordered_emp_cd;
                param[3] = "@instruction_date:" + instruction_date;

                message = _bllSpExecute.SpExecuteString(this.sSpName, gubun, param);

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
        private bool SetNextStatus(TestSchedule dto)
        {
            bool check = false;

            //최종 서명자 조회
            //int i = gv_TestSchedule.FocusedRowHandle;
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
                
        /// 설  명:서명 결과에 따른 상태값 변경 내역을 보여준다.
        /// </summary>
        private bool SetTestInfo(TestSchedule dto)
        {
            //int i = gv_TestSchedule.FocusedRowHandle;

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
        internal bool ESStatusCheck(TestSchedule dto)
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
            //else if ("Y".Equals(dto.select_yn))
            //{
            //    check = true;
            //}
            return check;
        }

        /// <summary>        
        /// 설  명:구분에 따라 시험항목을 추가한다.
        /// </summary>
        internal bool Add_Testitem(string AddType, string p_testcontrol_id, string p_teststandardmaster_id, string p_testitem_cd)
        {
            bool check = false;
            string message = "";            

            //시험관리 프라이머리 키
            string testcontrol_id = "@testcontrol_id:" + p_testcontrol_id;
            
            //시험항목 프라이머리 키
            string teststandardmaster_id = "@teststandardmaster_id:";
            if (p_teststandardmaster_id.Length > 0)
            {
                //시험항목이 선택된 상태이면 선택된 레코드의 ID를 넘긴다.
                teststandardmaster_id = teststandardmaster_id + p_teststandardmaster_id;
            }

            //시험항목 코드            
            string testitem_cd = "@testitem_cd:";
            if (p_testitem_cd.Length > 0)
            {
                testitem_cd = testitem_cd + p_testitem_cd;

                //추가
                message = _bllSpExecute.SpExecuteString(this.sSpName, "IC", testcontrol_id, teststandardmaster_id,
                    testitem_cd, "@addtype:" + AddType);

                if (message.ToString().Length == 0)
                    check = false;
                else
                {
                    check = true;
                }
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
        private string[] GetParam(Object gridGb)
        {
            string[] param = null;

            if (gridGb is TestSchedule.SrchParam)
            {
                TestSchedule.SrchParam srch = (TestSchedule.SrchParam)gridGb;
                if ("S".Equals(srch.Gubun))
                {
                    param = new string[9];
                    param[0] = "@s_date_option:" + srch.rg_date_option;
                    param[1] = "@s_start_date:" + srch.de_start_date;
                    param[2] = "@s_end_date:" + srch.de_end_date;
                    param[3] = "@s_test_type:" + srch.le_s_test_type;
                    param[4] = "@s_test_status:" + srch.re_test_status;
                    param[5] = "@search_gubun:" + srch.ce_gubun_number;
                    param[6] = "@search_number:" + srch.te_number;
                    param[7] = "@test_emp_cd:" + srch.be_s_emp_cd;
                    param[8] = "@item_form_cd:" + srch.le_item_form_cd;
                }
                else if ("S2".Equals(srch.Gubun))
                {
                    param = new string[7];
                    param[0] = "@s_date_option:" + srch.rg_date_option;
                    param[1] = "@s_start_date:" + srch.de_start_date;
                    param[2] = "@s_end_date:" + srch.de_end_date;
                    param[3] = "@s_test_type:" + srch.le_s_test_type;
                    param[4] = "@s_test_status:" + srch.re_test_status;
                    param[5] = "@testcontrol_id:" + srch.le_testcontrol_id;
                    param[6] = "@test_emp_cd:" + srch.be_s_emp_cd;
                }
            }
            else if(gridGb is TestSchedule.SrchParamItem)
            {
                TestSchedule.SrchParamItem srch = (TestSchedule.SrchParamItem)gridGb;
                if ("S4".Equals(srch.Gubun))
                {
                    param = new string[2];
                    param[0] = "@testitem_type:" + srch.le_s_testitem_type;
                    param[1] = "@testitem_cd:" + srch.te_s_testitem;                    
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

            if (gridGb is TestSchedule.TestScheduleDetailArrange)
            {
                TestSchedule.TestScheduleDetailArrange dto = (TestSchedule.TestScheduleDetailArrange)gridGb;
                rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto.row_status);
                switch (rowStatus)
                {                   
                    case TrxType.U:
                        // 수정
                        param = new string[10];
                        param[0] = "@testcontrol_id:" + dto.testcontrol_id;
                        param[1] = "@teststandardmaster_id:" + dto.teststandardmaster_id;
                        param[2] = "@emp_cd:" + dto.testitem_trier;
                        param[3] = "@testitem_schedule_time:" + dto.testitem_schedule_time;
                        param[4] = "@teststandard_nm:" + dto.teststandard_nm;
                        param[5] = "@teststandard_type:" + dto.teststandard_type;
                        param[6] = "@teststandard_min:" + dto.teststandard_min;
                        param[7] = "@teststandard_max:" + dto.teststandard_max;
                        param[8] = "@testresult_data_type:" + dto.testresult_data_type;
                        param[9] = "@teststandard_validpoint:" + dto.teststandard_validpoint;                        

                        break;
                    case TrxType.D:
                        // 삭제
                        param = new string[4];
                        //param[0] = "@testcontrol_id:" + dto.testcontrol_id;
                        //param[1] = "@test_type:" + dto.test_type;
                        //param[2] = "@program_cd:TestReceiptMulti";
                        //param[3] = "@item_cd:" + dto.item_cd;

                        break;
                    default:
                        param = null;
                        break;
                }
            }
            else
            {
                TestSchedule dto = (TestSchedule)gridGb;
                rowStatus = (TrxType)Enum.Parse(typeof(TrxType), dto.row_status);
            }            
            
            return param;
        }

        internal string CheckTestScheduleTestMaster(string testcontrol_id)
        {
            string gubun = "SS";

            string[] param = new string[1];
            param[0] = "@testcontrol_id:" + testcontrol_id;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

        internal string TestScheduleSaveCheckInfo(string testcontrol_id, string instruction_date)
        {
            string gubun = "US";

            string[] param = new string[2];
            param[0] = "@testcontrol_id:" + testcontrol_id;
            param[1] = "@instruction_date:" + instruction_date;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

        internal string TestScheduleGetLastSignEmpCheck(string testcontrol_id, string test_type)
        {
            string gubun = "SL";
            string program_cd = "TestSchedule";

            string[] pParam = new string[5];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@process_kind:" + "1";
            pParam[2] = "@sign_set_cd:" + "";
            pParam[3] = "@test_type:" + test_type;
            pParam[4] = "@program_cd:" + program_cd;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }

        internal string TestScheduleItemDelete(string testcontrol_id, string teststandardmaster_id)
        {
            string gubun = "D";

            string[] pParam = new string[2];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@teststandardmaster_id:" + teststandardmaster_id;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }

        internal string TestScheduleDespatchCancel(TestSchedule dto)
        {
            string gubun = "DC";
            string program_cd = "TestSchedule";

            string[] pParam = new string[5];
            pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
            pParam[1] = "@s_test_type:" + dto.test_type;
            pParam[2] = "@item_cd:" + dto.item_cd;
            pParam[3] = "@request_date:" + dto.request_date;
            pParam[4] = "@program_cd:" + program_cd;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return res;
        }

        internal string TestScheduleSignTRX(string gubun, string testcontrol_id, string process_kind, string test_type, string emp_cd, string sign_set_dt_id, string validation_type, string representative_yn)
        {
            string program_cd = "TestSchedule";
            string pGubun = "EI";

            switch (gubun)
            {
                case "U":
                    pGubun = "EI";
                    break;

                case "D":
                    pGubun = "SignCancel";
                    break;
            }

            string[] pParam = new string[9];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@process_kind:" + process_kind;
            pParam[2] = "@sign_set_cd:" + "";
            pParam[3] = "@emp_cd:" + emp_cd;
            pParam[4] = "@sign_set_dt_id:" + sign_set_dt_id;
            pParam[5] = "@validation_type:" + validation_type;
            pParam[6] = "@representative_yn:" + representative_yn;
            pParam[7] = "@test_type:" + test_type;
            pParam[8] = "@program_cd:" + program_cd;

            string res = _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", pGubun, pParam);

            return res;
        }

        internal string TestScheduleIssueTestReport(string testcontrol_id)
        {
            string gubun = "CR";

            string[] param = new string[1];
            param[0] = "@testcontrol_id:" + testcontrol_id;

            string res = _bllSpExecute.SpExecuteString(sSpName, gubun, param);

            return res;
        }

        internal DataTable SelectTestScheduleSignData(string p_testcontrol_id, string p_test_type)
        {
            string gubun = "SE";

            string[] param = new string[4];
            param[0] = "@testcontrol_id:" + p_testcontrol_id;
            param[1] = "@test_type:" + p_test_type;
            param[2] = "@process_kind:" + "1";
            param[3] = "@program_cd:" + "TestSchedule";

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_Test_ES_Manage", gubun, param);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            DataTable table = new DataTable();
            table.Columns.Add("sign_set_dt_id", typeof(String));
            table.Columns.Add("displayfield", typeof(String));
            table.Columns.Add("sign_yn", typeof(String));
            table.Columns.Add("sign_time", typeof(String));
            table.Columns.Add("sign_emp_cd", typeof(String));
            table.Columns.Add("responsible_emp_cd", typeof(String));
            table.Columns.Add("sign_image", typeof(String));
            table.Columns.Add("responsible_emp_nm", typeof(String));
            table.Columns.Add("sign_emp_nm", typeof(String));
            table.Columns.Add("prev_sign_yn", typeof(String));
            table.Columns.Add("next_sign_yn", typeof(String));
            table.Columns.Add("sign_set_dt_seq", typeof(String));

            foreach (DataRow row in dt.AsEnumerable())
            {
                DataRow _row = table.NewRow();

                _row["sign_set_dt_id"] = row["sign_set_dt_id"].ToString();
                _row["displayfield"] = row["displayfield"].ToString();
                _row["sign_yn"] = row["sign_yn"].ToString();
                _row["sign_time"] = row["sign_time"].ToString();
                _row["sign_emp_cd"] = row["sign_emp_cd"].ToString();
                _row["responsible_emp_cd"] = row["responsible_emp_cd"].ToString();

                if (row["sign_image"] != System.DBNull.Value)
                {
                    string str = Convert.ToBase64String((byte[])row["sign_image"]);
                    string url = "data:Image/png;base64," + str;
                    _row["sign_image"] = url;
                }
                else
                {
                    _row["sign_image"] = "/Content/image/defaultSign.png";
                }

                _row["responsible_emp_nm"] = row["responsible_emp_nm"].ToString();
                _row["sign_emp_nm"] = row["sign_emp_nm"].ToString();
                _row["prev_sign_yn"] = row["prev_sign_yn"].ToString();
                _row["next_sign_yn"] = row["next_sign_yn"].ToString();
                _row["sign_set_dt_seq"] = row["sign_set_dt_seq"].ToString();

                table.Rows.Add(_row);
            }

            return table;
        }

        #endregion
    }
}
