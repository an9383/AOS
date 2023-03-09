using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.QualityControl;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Services.QualityControl
{
    public class TestRecognitionEService
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private String sSpName = "SP_TestRecognitionE";        
        private string sign_set_cd = "";

        #region 조회영역
        internal DataTable Select(TestRecognitionE.SrchParam srchParam)
        {

            string[] pParam = GetParam(srchParam, null);
           
            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, srchParam.Gubun, pParam);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        internal DataTable SelectDetail(TestRecognitionE.SrchParam srchParam, TestRecognitionE dto)
        {
            string[] pParam = GetParam(srchParam, dto);

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, srchParam.Gubun, pParam);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        internal DataTable SelectSign(string testcontrol_id, string test_type, string process_kind)
        {
            string[] pParam = new string[4];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@process_kind:" + process_kind;
            pParam[3] = "@program_cd:" + "TestRecognitionE";

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_Test_ES_Manage", "SE", pParam);

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
            table.Columns.Add("necessary_yn", typeof(String));

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
                _row["necessary_yn"] = row["necessary_yn"].ToString();

                table.Rows.Add(_row);
            }

            return table;
        }

        internal DataTable SelectFile(string testcontrol_id, string teststandardmaster_id)
        {
            string[] pParam = new string[2];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@teststandardmaster_id:" + teststandardmaster_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet("SP_AttachBackData", "S", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal void uploadFile(byte[] myBytes, string fileName, string contentType, string testcontrol_id, string teststandardmaster_id)
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

            string sSpName = "SP_test_doc_registration";

            string[] pParam = new string[5];
            pParam[0] = "@doc_name:" + fileName;
            pParam[1] = "@doc_type:" + doc_type;
            pParam[2] = "@testcontrol_id:" + testcontrol_id;
            pParam[3] = "@teststandardmaster_id:" + teststandardmaster_id;
            pParam[4] = "@insert_root:" + "L";

            string message = _bllSpExecute.SpExecuteString(sSpName, "F", "@file_image", myBytes, pParam);
        }


        internal DataTable DownloadFile(string testcontrol_id, string teststandardmaster_id, string file_id)
        {
            string sSpName = "SP_test_doc_registration";
            string sGubun = "FS";

            string[] pParam = new string[3];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@teststandardmaster_id:" + teststandardmaster_id;
            pParam[2] = "@doc_file_id:" + file_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal string DeleteFile(string testcontrol_id, string teststandardmaster_id, string file_id)
        {
            string sSpName = "SP_test_doc_registration";

            string[] pParam = new string[4];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@teststandardmaster_id:" + teststandardmaster_id;
            pParam[2] = "@doc_file_id:" + file_id;
            pParam[3] = "@p_insert_root:" + "L";

            string message = _bllSpExecute.SpExecuteString(sSpName, "AD", pParam);

            return message;
        }
             

        #endregion

        #region Trx영역
        // 필요시 주석해지하여 사용
        //internal string TestRecognitionETRX(TestRecognitionE dto)
        //{
        //    string[] pParam = null;

        //    switch (dto.gubun)
        //    {
        //        case "I":
        //        case "U":
        //            pParam = GetParam(dto);
        //            break;

        //        case "D":

        //            pParam = new string[3];

        //            pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
        //            pParam[1] = "@program_cd:" + program_cd;
        //            pParam[2] = "@test_type:" + dto.test_type;

        //            break;
        //    }

        //    string res = _bllSpExecute.SpExecuteString(sSpName, dto.gubun, pParam);

        //    return res;
        //}

        #endregion

        #region 프로그램별 영역
        //최종 서명자 여부를 결정한다.
        internal bool GetLastSignEmpCheck(TestRecognitionE dto)
        {
            bool check = false;
            string message = "XXXXXXXX";

            string gubun = "SL";

            string[] pParam = new string[2];
            pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
            pParam[1] = "@test_type:" + dto.test_type;
            message = _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", gubun, pParam);

            //서명이 되지 않은 자료는 수정,삭제할 수 있다.
            if (message == "")
            {
                check = true;
            }
            else
            {
                //전자서명으로 서명자를 확인한다.
                check = false;  // 전사서명해야함다
            }
            return check;
        }

        //시험 결과 취소
        internal bool CancelSign(TestRecognitionE dto)
        {
            string gubun = "U";
            string[] pParam = new string[5];

            pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
            pParam[1] = "@update_or_cancel:" + "cl";
            pParam[2] = "@test_type:" + dto.test_type;
            pParam[3] = "@program_cd:" + "TestRecognitionE";
            pParam[4] = "@request_root:" + "lims";

            string message = _bllSpExecute.SpExecuteString(this.sSpName, gubun, pParam);


            if (HACCP.Libs.Public_Function.IsNumeric(message))
                return true;
            else
                return false;
        }

        //판정시 시험결과값의 존재여부를 판단하여 전체시험이 완료되었는가를 체크한다
        internal bool CheckTestStandard(TestRecognitionE dto)
        {
            bool check = false;
            string gubun = "C";
            string[] pParam = new string[1];

            pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
            
            string message = _bllSpExecute.SpExecuteString(this.sSpName, gubun, pParam);

            check = ("1".Equals(message)) ? true : false;
            return check;
        }

        
        internal bool DataCheckYN(TestRecognitionE dto)
        {
            bool check = false;
            string gubun = "C2";
            string[] pParam = new string[1];

            pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
            string message = _bllSpExecute.SpExecuteString(this.sSpName, gubun, pParam);

            check = ("Y".Equals(message)) ? true : false;
            return check;
        }

        //성적일자와 시험일자 비교
        internal bool CheckTestDate(TestRecognitionE dto)
        {
            bool check = false;
            string gubun = "CK";
            string[] pParam = new string[2];

            pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
            pParam[1] = "@test_date:" + dto.test_date;
            DataTable dt = _bllSpExecute.SpExecuteTable("SP_TestRecognitionE", gubun, pParam);

            check = (dt.Rows.Count > 0) ? false : true;
            return check;
        }

        //대리자 권한 여부 파악
        internal bool SignDelegateCheck(string emp_cd, string testcontrol_id, string process_kind, string test_type, string sign_set_dt_id)
        {
            bool check = false;
            string gubun = "SR";
            string[] pParam = new string[7];

            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@sign_set_dt_id:" + sign_set_dt_id;
            pParam[3] = "@emp_cd:" + emp_cd;
            pParam[4] = "@process_kind:" + process_kind;
            pParam[5] = "@program_cd:TestRecognitionE";
            pParam[6] = "@sign_set_cd:"+ sign_set_cd;

            string message = _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", gubun, pParam);

            check = (message.Length >0) ? true : false;
            return check;
        }

        //부적합 선택후 인증하면 인증정보 저장전에 시험완료 처리한다. 모든 시험항목을 시험하지 않더라도 상태값을 변경하기 위함.
        internal bool SetStatusTestCompleteForReject(string testcontrol_id)
        {
            bool check = false;
            string gubun = "TE";
            string[] pParam = new string[1];

            pParam[0] = "@testcontrol_id:" + testcontrol_id;            
            string message = _bllSpExecute.SpExecuteString(this.sSpName, gubun, pParam);

            check = Public_Function.IsNumeric(message) ? true : false;
            return check;
        }

        //전자 서명 자료를 저장한다.
        internal bool SaveElectronicSignature(string gubun, string emp_cd, string testcontrol_id, string process_kind, string test_type, string sign_set_dt_id, string validation_type, string representative_yn)
        {
            bool check = false;
            string[] pParam = new string[9];

            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@sign_set_dt_id:" + sign_set_dt_id;
            pParam[3] = "@emp_cd:" + emp_cd;
            pParam[4] = "@representative_yn:" + representative_yn;
            pParam[5] = "@validation_type:" + validation_type;
            pParam[6] = "@process_kind:" + process_kind;
            pParam[7] = "@program_cd:TestRecognitionE";
            pParam[8] = "@sign_set_cd:"+ sign_set_cd;
            
            string message = _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", gubun, pParam);

            check = Public_Function.IsNumeric(message) ? true : false;
            
            return check;
        }
        
        //판정결과 저장 -리턴값을 체크하지 않음.
        internal string SaveCheckInfo(TestRecognitionE dto)
        {
            string[] param = new string[14];

            param[0] = "@testcontrol_id:" + dto.testcontrol_id;
            param[1] = "@update_or_cancel:" + "ud";
            param[2] = "@test_result_yn:" + dto.test_result_yn_c;
            param[3] = "@test_date:" + dto.test_date;
            param[4] = "@valid_period:" + dto.valid_period;
            param[5] = "@test_inform_remark:" + dto.test_inform_remark;
            param[6] = "@retest_yn:" + dto.retest_yn;
            param[7] = "@test_result_pollination:" + dto.test_result_pollination;
            param[8] = "@test_result_solvent:" + dto.test_result_solvent;
            param[9] = "@emp_cd:" + Public_Function.User_cd;
            param[10] = "@test_result_value:" + dto.test_result_value;
            param[11] = "@test_result_value0:" + dto.test_result_value0;
            param[12] = "@qc_valid_period:" + dto.qc_valid_period;   //  de_qc_valid_date.Text; //2012.07.19 황안례 유효기한 컨트롤  EditValue 데이터 변경하도록 수정 (유효기간 오류 수정)
            param[13] = "@extend_yn:" + dto.extend_yn;

            //2012.05.29 황안례 추가
            string message = _bllSpExecute.SpExecuteString(this.sSpName, "U", param);
            
            return message;
        }

        internal bool SetNextStatus(string testcontrol_id, string test_type, string process_kind)
        {
            bool check = false;
            string gubun = "EN";
            string[] pParam = new string[5];

            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@process_kind:" + process_kind;
            pParam[3] = "@program_cd:TestRecognitionE";
            pParam[4] = "@sign_set_cd:"+this.sign_set_cd;
            string message = _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", gubun, pParam);

            check = Public_Function.IsNumeric(message) ? true : false;
            return check;
        }

        internal bool Check_End(TestRecognitionE dto)
        {
            bool check = false;
            string gubun = "SA";
            string[] pParam = new string[4];

            pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
            pParam[1] = "@test_type:" + dto.test_type_cd;
            pParam[2] = "@process_kind:" + dto.test_type_cd;
            pParam[3] = "@program_cd:TestRecognitionE";

            string message = _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", gubun, pParam);

            check = ("1".Equals(message)) ? true : false;
            return check;
        }

        internal string InterfaceUpload(string testcontrol_id, string test_type, string create_type)
        {            
            string gubun = "UploadTestResult";
            string[] pParam = new string[3];

            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@create_type:" + create_type;

            string message;
            try
            {
                message = _bllSpExecute.SpExecuteString("SP_InterfaceTestRecognition", gubun, pParam);
            }catch(Exception)
            {
                
                message = "데이터 처리 중 오류가 발생하였습니다";
            }

            return message;
        }

        internal bool ESStatusCheck(string test_type, string test_status, string process_kind )
        {
            bool check = false;
            string gubun = "ST";
            string[] pParam = new string[5];

            pParam[0] = "@test_type:" + test_type;
            pParam[1] = "@test_status:" + test_status;
            pParam[2] = "@process_kind:" + process_kind;
            pParam[3] = "@program_cd:TestRecognitionE";
            pParam[4] = "@sign_set_cd:"+this.sign_set_cd;

            string message = _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", gubun, pParam);

            //서명이 되지 않은 자료는 수정,삭제할 수 있다.
            check = ("ok".Equals(message)) ? true : false;
            return check;
        }

        internal string CancelCheckInfo(string testcontrol_id, string test_type)
        {
            string gubun = "U";
            string[] pParam = new string[4];

            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@update_or_cancel:" + "cl";
            pParam[2] = "@test_type:" + test_type;
            pParam[3] = "@program_cd:TestRecognitionE";

            string message;
            message = _bllSpExecute.SpExecuteString(this.sSpName, gubun, pParam);            
            return message;
        }

        #endregion
        #region sp param 설정
        private string[] GetParam(TestRecognitionE.SrchParam srch, TestRecognitionE dto)
        {
            string[] pParam = null;
                       
            if ("S".Equals(srch.Gubun))
            {
                pParam = new string[8];

                pParam[0] = "@date_select:" + srch.rg_ReqorRec;
                pParam[1] = "@start_date:" + srch.de_SDate;
                pParam[2] = "@end_date:" + srch.de_EDate;
                pParam[3] = "@test_typeselc:" + srch.le_testitem_nm;
                pParam[4] = "@test_status:" + srch.rg_status;
                pParam[5] = "@search_gubun:" + srch.ce_gubun_number;
                pParam[6] = "@search_number:" + srch.te_number;
                pParam[7] = "@item_form_cd:" + srch.le_item_form_cd;

            }
            else if ("S1".Equals(srch.Gubun))
            {
                pParam = new string[1];
                pParam[0] = "@testcontrol_id:" + dto.testcontrol_id;
            }
            return pParam;
        }

        private string[] TrxGetParam(TestRecognitionE dto)
        {
            string[] pParam = new string[0];
            return pParam;
        }

        internal string TestCheckECheckLastSignEmp(string testcontrol_id, string test_type, string process_kind, string program_cd)
        {
            string gubun = "SL";

            string[] pParam = new string[4];
            pParam[0] = "@testcontrol_id:" + testcontrol_id;
            pParam[1] = "@test_type:" + test_type;
            pParam[2] = "@process_kind:" + process_kind;
            pParam[3] = "@program_cd:" + program_cd;

            string message = _bllSpExecute.SpExecuteString("SP_Test_ES_Manage", gubun, pParam);

            return message;
        }
        #endregion
    }
}