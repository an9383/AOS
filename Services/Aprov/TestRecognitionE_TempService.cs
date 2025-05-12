using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Aprov;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace HACCP.Services.Aprov
{
    public class TestRecognitionE_TempService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        //시험성적승인2 리스트
        public List<TestRecognitionE_Temp> GridSelect(TestRecognitionE_Temp.SrchParam srch)
        {
            string sSpName = "SP_TestRecognitionE";

            string[] pParam = new String[8];
            pParam[0] = "@date_select:" + srch.date_select;
            pParam[1] = "@start_date:" + srch.start_date;
            pParam[2] = "@end_date:" + srch.end_date;
            pParam[3] = "@test_typeselc:" + srch.test_typeselc;
            pParam[4] = "@test_status:" + srch.test_status;
            pParam[5] = "@search_gubun:" + srch.search_gubun;
            pParam[6] = "@search_number:" + srch.search_number;
            pParam[7] = "@item_form_cd:" + srch.item_form_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", pParam);

            List<TestRecognitionE_Temp> testRecognitionList = new List<TestRecognitionE_Temp>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                TestRecognitionE_Temp testRecognitionE = new TestRecognitionE_Temp(row);
                testRecognitionList.Add(testRecognitionE);
            }

            return testRecognitionList;
        }

        //시험성적승인2 세부사항
        public DataTable GridSelect1(string testcontrol_id)
        {
            string sSpName = "SP_TestRecognitionE";
            string[] pParam = new string[1];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S1", pParam);
            return dt;
        }

        //시험성적승인2 세부사항(시험번호 관련..?)
        public DataTable GridSelect2(string testcontrol_id)
        {
            string sSpName = "SP_TestRecognitionE";
            string[] pParam = new string[1];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "SW", pParam);
            return dt;
        }

        //사업장명 조회
        public string GetPlantName()
        {
            string sSpName = "SP_TestRecognitionE";

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "Plant");

            return result;
        }

        #region 전자서명 로직

        //전자서명 정보 조회
        public DataTable GetTestSignset(string testcontrol_id, string test_type, string process_kind, string program_cd, string sign_set_cd)
        {
            string sSpName = "SP_Test_ES_Manage";

            string[] pParam = new string[5];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@process_kind:" + process_kind;
            pParam[3] = "@program_cd:" + program_cd;
            pParam[4] = "@sign_set_cd:" + sign_set_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SE", pParam);

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

            //DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "SE", pParam);
            return table;
        }

        //서명권한과 서명정보 조회
        public DataTable GetTestSignInfo(string testcontrol_id, string test_type, string sign_set_dt_id, string process_kind, string program_cd, string sign_set_cd)
        {
            string sSpName = "SP_Test_ES_Manage";

            string[] pParam = new string[6];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@sign_set_dt_id:" + sign_set_dt_id;
            pParam[3] = "@process_kind:" + process_kind;
            pParam[4] = "@program_cd:" + program_cd;
            pParam[5] = "@sign_set_cd:" + sign_set_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "SD", pParam);
            DataTable dt_1 = new DataTable();

            if (dt.Rows.Count > 0)
            {
                dt_1.Columns.Add("responsible_emp_cd", typeof(String));
                dt_1.Columns.Add("responsible_emp_nm", typeof(String));
                dt_1.Columns.Add("responsible_authority", typeof(String));
                dt_1.Columns.Add("sign_emp_nm", typeof(String));
                dt_1.Columns.Add("sign_emp_authority", typeof(String));
                dt_1.Columns.Add("sign_time", typeof(String));
                dt_1.Columns.Add("sign_image", typeof(String));

                foreach (DataRow row in dt.AsEnumerable())
                {
                    DataRow _row = dt_1.NewRow();
                    _row["responsible_emp_cd"] = row["responsible_emp_cd"].ToString();
                    _row["responsible_emp_nm"] = row["responsible_emp_nm"].ToString();
                    _row["responsible_authority"] = row["responsible_authority"].ToString();
                    _row["sign_emp_nm"] = row["sign_emp_nm"].ToString();
                    _row["sign_emp_authority"] = row["sign_emp_authority"].ToString();
                    _row["sign_time"] = row["sign_time"].ToString();
                    _row["sign_image"] = row["sign_image"].ToString();

                    if (row["sign_image"] != System.DBNull.Value)
                    {
                        string str = Convert.ToBase64String((byte[])row["sign_image"]);
                        string url = "data:Image/png;base64," + str;
                        _row["sign_image"] = url;
                    }
                    dt_1.Rows.Add(_row);
                }

            }
            return dt_1;

        }

        //서명 순서와 필수여부에 따른 서명 가능 여부 파악
        public string CheckESPosition(string testcontrol_id, string test_type, string sign_set_dt_id, string process_kind, string program_cd, string sign_set_cd)
        {
            string sSpName = "SP_Test_ES_Manage";

            string[] pParam = new string[6];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@sign_set_dt_id:" + sign_set_dt_id;
            pParam[3] = "@process_kind:" + process_kind;
            pParam[4] = "@program_cd:" + program_cd;
            pParam[5] = "@sign_set_cd:" + sign_set_cd;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "SP", pParam);

            return result;
        }

        //최종 서명자 조회
        public string SetNextStatus(string testcontrol_id, string test_type, string process_kind, string program_cd, string sign_set_cd)
        {
            string sSpName = "SP_Test_ES_Manage";

            string[] pParam = new string[5];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@process_kind:" + process_kind;
            pParam[3] = "@program_cd:" + program_cd;
            pParam[4] = "@sign_set_cd:" + sign_set_cd;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "EN", pParam);

            return result;
        }

        //대리자 권한 여부 파악
        public string GetRepresentativeYN(string testcontrol_id, string test_type, string sign_set_dt_id, string emp_cd, string process_kind, string program_cd, string sign_set_cd)
        {
            string sSpName = "SP_Test_ES_Manage";

            string[] pParam = new string[7];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@sign_set_dt_id:" + sign_set_dt_id;
            pParam[3] = "@emp_cd:" + emp_cd;
            pParam[4] = "@process_kind:" + process_kind;
            pParam[5] = "@program_cd:" + program_cd;
            pParam[6] = "@sign_set_cd:" + sign_set_cd;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "SR", pParam);

            return result;
        }

        //전자서명
        public string ElectronicSignature(string testcontrol_id, string test_type, string sign_set_dt_id, string emp_cd, string representative_yn, string validation_type, string process_kind, string program_cd, string sign_set_cd)
        {
            string sSpName = "SP_Test_ES_Manage";

            string[] pParam = new string[9];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@sign_set_dt_id:" + sign_set_dt_id;
            pParam[3] = "@emp_cd:" + emp_cd;
            pParam[4] = "@representative_yn:" + representative_yn;
            pParam[5] = "@validation_type:" + validation_type;
            pParam[6] = "@process_kind:" + process_kind;
            pParam[7] = "@program_cd:" + program_cd;
            pParam[8] = "@sign_set_cd:" + sign_set_cd;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "EI", pParam);

            return result;
        }

        //현재 단계의 모든 필수 서명이 완료되었는지 확인
        public string CheckEnd(string testcontrol_id, string test_type, string process_kind, string program_cd, string sign_set_cd)
        {
            string sSpName = "SP_Test_ES_Manage";

            string[] pParam = new string[5];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@process_kind:" + process_kind;
            pParam[3] = "@program_cd:" + program_cd;
            pParam[4] = "@sign_set_cd:" + sign_set_cd;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "SA", pParam);

            return result;
        }

        //서명결과에 따른 상태값 변경 내역 조회
        public DataTable SetTestInfo(string testcontrol_id, string process_kind, string program_cd, string sign_set_cd)
        {
            string sSpName = "SP_Test_ES_Manage";

            string[] pParam = new string[4];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@process_kind:" + process_kind;
            pParam[2] = "@program_cd:" + program_cd;
            pParam[3] = "@sign_set_cd:" + sign_set_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "SI", pParam);
            return dt;
        }

        //삭제(취소) 가능 상태 체크
        public string ESStatusCheck(string test_type, string test_status, string process_kind, string program_cd, string sign_set_cd)
        {
            string sSpName = "SP_Test_ES_Manage";

            string[] pParam = new string[5];
            pParam[0] = "@test_type:" + test_type;
            pParam[1] = "@test_status:" + test_status;
            pParam[2] = "@process_kind:" + process_kind;
            pParam[3] = "@program_cd:" + program_cd;
            pParam[4] = "@sign_set_cd:" + sign_set_cd;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "ST", pParam);

            return result;
        }

        //최종 서명자 여부를 결정
        public string GetLastSignEmpCheck(string testcontrol_id, string test_type, string process_kind, string program_cd, string sign_set_cd)
        {
            string sSpName = "SP_Test_ES_Manage";

            string[] pParam = new string[5];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@process_kind:" + process_kind;
            pParam[3] = "@program_cd:" + program_cd;
            pParam[4] = "@sign_set_cd:" + sign_set_cd;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "SL", pParam);

            return result;
        }

        //전자서명 취소 - 하나씩
        public string CancelCheckStep(string testcontrol_id, string sign_set_cd, string sign_set_dt_id, string process_kind)
        {
            string sSpName = "SP_Test_ES_Manage";

            string[] pParam = new string[4];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@sign_set_cd:" + sign_set_cd;
            pParam[2] = "@sign_set_dt_id:" + sign_set_dt_id;
            pParam[3] = "@process_kind:" + process_kind;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "SignCancel", pParam);

            return result;
        }

        #endregion


        #region 시험성적 정보 저장 / 취소

        //적/부 판정 정보 저장
        public string UpdateResultYN(TestRecognitionE_Temp testRecognitionE)
        {
            string sSpName = "SP_TestCheckE";

            string[] pParam = new string[12];
            pParam[0] = "@testcontrol_id:" + testRecognitionE.testcontrol_id;
            pParam[1] = "@update_or_cancel:" + testRecognitionE.update_or_cancel;
            pParam[2] = "@test_result_yn:" + testRecognitionE.test_result_yn;
            pParam[3] = "@test_result_value0:" + testRecognitionE.test_result_value0;
            pParam[4] = "@test_result_value:" + testRecognitionE.test_result_value;
            pParam[5] = "@test_result_pollination:" + testRecognitionE.test_result_pollination;
            pParam[6] = "@test_result_solvent:" + testRecognitionE.test_result_solvent;
            pParam[7] = "@test_informt_remark:" + testRecognitionE.test_inform_remark;
            pParam[8] = "@retest_yn:" + testRecognitionE.retest_yn;
            pParam[9] = "@valid_period:" + testRecognitionE.valid_period;
            pParam[10] = "@qc_valid_period:" + testRecognitionE.qc_valid_period;
            pParam[11] = "@extend_yn:" + testRecognitionE.extend_yn;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "U", pParam);

            return result;
        }

        //시험성적 정보 저장
        public string SaveCheckInfo(TestRecognitionE_Temp testRecognitionE)
        {
            string sSpName = "SP_TestRecognitionE";

            string[] pParam = new string[14];
            pParam[0] = "@testcontrol_id:" + testRecognitionE.testcontrol_id;
            pParam[1] = "@update_or_cancel:" + "ud";
            pParam[2] = "@emp_cd:" + HttpContext.Current.Session["loginCD"];
            pParam[3] = "@test_result_yn:" + testRecognitionE.test_result_yn;
            pParam[4] = "@test_result_value0:" + testRecognitionE.test_result_value0;
            pParam[5] = "@test_result_value:" + testRecognitionE.test_result_value;
            pParam[6] = "@test_result_pollination:" + testRecognitionE.test_result_pollination;
            pParam[7] = "@test_result_solvent:" + testRecognitionE.test_result_solvent;
            pParam[8] = "@test_date:" + testRecognitionE.test_date;
            pParam[9] = "@test_inform_remark:" + testRecognitionE.test_inform_remark;
            pParam[10] = "@retest_yn:" + testRecognitionE.retest_yn;
            pParam[11] = "@valid_period:" + testRecognitionE.valid_period;
            pParam[12] = "@qc_valid_period:" + testRecognitionE.qc_valid_period;
            pParam[13] = "@extend_yn:" + testRecognitionE.extend_yn;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "U", pParam);

            return result;
        }

        //시험성적 취소
        public string CancelCheckInfo(string testcontrol_id, string test_type)
        {
            string sSpName = "SP_TestRecognitionE";

            string[] pParam = new string[4];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@update_or_cancel:" + "cl";
            pParam[2] = "@test_type:" + test_type;
            pParam[3] = "@program_cd:" + "TestRecognitionE";

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "U", pParam);

            return result;
        }
        #endregion


        #region 함량 및 역가, 유효일자 로직

        //유효일자 자동설정
        public string SetAutoQCValidPeriod(string testcontrol_id)
        {
            string sSpName = "SP_TestRecognitionE";

            string[] pParam = new string[1];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "SV", pParam);

            return result;
        }

        #endregion

        #region 시험의뢰번호 수정 로직

        //의뢰번호 양식 확인
        public string CheckRequestNo(string u_test_request_no)
        {
            string sSpName = "SP_TestRecognitionE";

            string[] pParam = new string[1];
            pParam[0] = "@u_test_request_no:" + u_test_request_no;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "CRN", pParam);

            return result;
        }

        //시험의뢰번호 변경
        public string UpdateRequestNo(string u_test_request_no, string prev_test_request_no, string u_test_judge_no, string u_test_request_date)
        {
            string sSpName = "SP_TestRecognitionE";

            string[] pParam = new string[4];
            pParam[0] = "@u_test_request_no:" + u_test_request_no;
            pParam[1] = "@prev_test_request_no:" + prev_test_request_no;
            pParam[2] = "@u_test_judge_no:" + u_test_judge_no;
            pParam[3] = "@u_test_request_date:" + u_test_request_date;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "URN", pParam);

            return result;
        }
        #endregion

        #region 파일 관리 로직
        //파일 리스트 조회
        public DataTable GetFileList(string testcontrol_id)
        {
            string sSpName = "SP_AttachFile";

            string[] pParam = new string[1];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", pParam);
            return dt;
        }

        //파일 등록
        public string InsertFile(byte[] myBytes, string testcontrol_id, string doc_name, string contentType)
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
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@doc_name:" + doc_name;
            pParam[2] = "@doc_type:" + contentType;

            string message = _bllSpExecute.SpExecuteString(sSpName, "I", "@file_image", myBytes, pParam);

            return message;
        }

        //파일 열기
        public DataTable OpenFile(string testcontrol_id, string file_id)
        {
            string sSpName = "SP_AttachFile";

            string[] pParam = new String[2];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@file_id:" + file_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "O", pParam);
            return dt;
            //if (dt.Rows.Count > 0)
            //{
            //    string fileName = "";

            //    //파일의 결재라인 체크 함수 사용여부를 불러온다,
            //    string DocumentFileStoredFolder = "";   //문서저장 폴더
            //    DocumentFileStoredFolder = _bllSpExecute.SpExecuteString("SP_ProgramInit", "S2", "@code:Doc_record_folder_value");

            //    //파일관리 폴더가 존재하지 않는경우에는 생성한다.
            //    if (Directory.Exists(DocumentFileStoredFolder) == false)
            //    {
            //        Directory.CreateDirectory(DocumentFileStoredFolder);
            //    }

            //    //이미지 정보를 폴더에 파일로 만든다.
            //    fileName = dt.Rows[0]["doc_name"].ToString() + dt.Rows[0]["file_extension_name"].ToString();
            //    //파일이 존재하면 삭제하도록 한다.
            //    if (File.Exists(@DocumentFileStoredFolder + "\\" + fileName))
            //    {
            //        File.Delete(@DocumentFileStoredFolder + "\\" + fileName);
            //    }

            //    byte[] bs;
            //    System.IO.FileStream fs = new FileStream(@DocumentFileStoredFolder + "\\" + fileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            //    bs = (byte[])dt.Rows[0]["file_image"];
            //    System.IO.BinaryWriter bw;
            //    bw = new System.IO.BinaryWriter(fs);
            //    bw.Write(bs);
            //    bw.Close();
            //    fs.Close();

            //    message = "success";

            //    Process.Start(@DocumentFileStoredFolder + "\\" + fileName);
            //}
            //else
            //{
            //    message = "fail";
            //}

            //return message;
        }


        //파일 삭제
        public string DeleteFile(string testcontrol_id, string file_id)
        {
            string sSpName = "SP_AttachFile";

            string[] pParam = new String[2];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@file_id:" + file_id;

            string result = _bllSpExecute.SpExecuteString(sSpName, "D", pParam);

            return result;
        }

        #endregion


        //업로드할 키(전체:%, 선택:구매요청번호), 생성구분(A:생성, C:취소)
        public string InterfaceUpload(string testcontrol_id, string test_type, string create_type)
        {
            string sSpName = "SP_InterfaceTestRecognition";

            string[] pParam = new string[3];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@update_or_cancel:" + test_type;
            pParam[2] = "@create_type:" + create_type;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "UploadTestResult", pParam);

            return result;
        }

        public string GetSystemParameter(string code)
        {
            string sSpName = "SP_ProgramInit";

            string[] pParam = new string[1];
            pParam[0] = "@code:" + code;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "S2", pParam);

            return result;
        }

        public DataTable GetProgramSet()
        {
            string sSpName = "SP_ProgramInit";

            string[] pParam = new string[1];
            pParam[0] = "@code:" + "TestRecognitionE_Temp";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S3", pParam);

            return dt;
        }

    }
}