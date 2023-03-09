using HACCP.Libs.Database;
using HACCP.Models.PrevCk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace HACCP.Services.PrevCk
{
    public class DayScheduleListService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();
       
        public string common_cd
        {
            get
            {
                DataTable programSet = GetProgramSet();

                string commonCd = programSet.DataSet.Tables[1].Rows[1].ItemArray[1].ToString();//Option1
                return commonCd;
            }

        }

        //그리드 자료종류 Lookup
        public DataTable GetRepositoryItem(string gubun)
        {
            string sSpName = "sp_DayScheduleList_Y";
            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, gubun);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        //수정 - 상단 점검 기록서 
        public string SaveMaster(DayScheduleList dayScheduleList)
        {
            string sSpName = "sp_DayScheduleList_Y";

            string[] pParam = new String[2];
            pParam[0] = "@schedule_id:" + dayScheduleList.schedule_id;
            pParam[1] = "@comment:" + dayScheduleList.comment;

            string result = _bllSpExecute.SpExecuteString(sSpName, "U", pParam);

            return result;
        }

        //수정 - 하단 점검 기록서 체크사항 리스트
        //public string SaveChecklist(string schedule_id, string work_seq, string schedule_result_manual_data)
        //{
        //    string sSpName = "sp_DayScheduleList_Y";

        //    string[] pParam = new String[3];
        //    pParam[0] = "@schedule_id:" + schedule_id;
        //    pParam[1] = "@work_seq:" + work_seq;
        //    pParam[2] = "@schedule_result_manual_data:" + schedule_result_manual_data;

        //    string result = _bllSpExecute.SpExecuteString(sSpName, "U1", pParam);

        //    return result;
        //}

        //수정 - 하단 점검 기록서 체크사항 리스트
        public string DayScheduleList_TRX(List<DayScheduleList> paramData)
        {
            //string sSpName = "sp_DayScheduleList_Y";

            //string[] pParam = new String[3];
            //pParam[0] = "@schedule_id:" + schedule_id;
            //pParam[1] = "@work_seq:" + work_seq;
            //pParam[2] = "@schedule_result_manual_data:" + schedule_result_manual_data;

            //string result = _bllSpExecute.SpExecuteString(sSpName, "U1", pParam);

            //return result;


            string sSpName = "sp_DayScheduleList_Y";
            string res = "";

            foreach (DayScheduleList tmp in paramData)
            {
                if (tmp.gubun == "U1")
                {
                    string[] pParam = new String[3];
                    pParam[0] = "@schedule_id:" + tmp.schedule_id;
                    pParam[1] = "@work_seq:" + tmp.work_seq;
                    pParam[2] = "@schedule_result_manual_data:" + tmp.schedule_result_manual_data;

                    res = _bllSpExecute.SpExecuteString(sSpName, tmp.gubun, pParam);
                }
            }

            return res;
        }

        //조회 - 점검 기록서 
        public List<DayScheduleList> GridSelect(DayScheduleList.SrchParam srch)
        {
            string sSpName = "sp_DayScheduleList_Y";

            string[] pParam = new String[7];
            pParam[0] = "@s_schedule_date1:" + srch.s_schedule_date1;
            pParam[1] = "@s_schedule_date2:" + srch.s_schedule_date2;
            pParam[2] = "@s_obj_type:" + srch.s_obj_type;
            pParam[3] = "@s_work_type:" + srch.s_work_type;
            pParam[4] = "@s_schedule_type:" + srch.s_schedule_type;
            pParam[5] = "@obj_cd:" + srch.obj_cd;
            pParam[6] = "@common_cd:" + this.common_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", pParam);

            List<DayScheduleList> daySchedulesList = new List<DayScheduleList>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                DayScheduleList daySchedule = new DayScheduleList(row);
                daySchedulesList.Add(daySchedule);
            }

            return daySchedulesList;
        }

        //조회 - 점검 기록서 체크사항
        public DataTable GridSelect2(DayScheduleList.SrchParam srch)
        {
            string sSpName = "sp_DayScheduleList_Y";

            string[] pParam = new String[4];
            pParam[0] = "@s_schedule_date1:" + srch.s_schedule_date1;
            pParam[1] = "@s_schedule_date2:" + srch.s_schedule_date2;
            pParam[2] = "@schedule_id:" + srch.schedule_id;
            pParam[3] = "@common_cd:" + this.common_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S2", pParam);
         
            return dt;

        }

        //삭제 - 점검 기록서
        public string GridDelete(string schedule_id)
        {
            string sSpName = "sp_DayScheduleList_Y";

            string[] pParam = new String[1];
            pParam[0] = "@schedule_id:" + schedule_id;

            string result = _bllSpExecute.SpExecuteString(sSpName, "D", pParam);

            return result;
        }

        //파일 정보 조회
        public DataTable GetFileList(string schedule_id)
        {
            string sSpName = "sp_DayScheduleList_Y";

            string[] pParam = new String[1];
            pParam[0] = "@schedule_id:" + schedule_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "SelectFile", pParam);

            return dt;
        }

        //파일 삭제
        public string DeleteFile(string schedule_id, string file_id)
        {
            string sSpName = "sp_DayScheduleList_Y";

            string[] pParam = new String[2];
            pParam[0] = "@schedule_id:" + schedule_id;
            pParam[1] = "@file_id:" + file_id;

            string result = _bllSpExecute.SpExecuteString(sSpName, "DeleteFile", pParam);

            return result;
        }

        //파일 열기
        public DataTable OpenFile(string schedule_id, string file_id)
        {
            string sSpName = "sp_DayScheduleList_Y";

            string[] pParam = new String[2];
            pParam[0] = "@schedule_id:" + schedule_id;
            pParam[1] = "@file_id:" + file_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "OpenFile", pParam);

            return dt;

            //string sSpName = "sp_DayScheduleList_Y";

            //string[] pParam = new String[2];
            //pParam[0] = "@schedule_id:" + schedule_id;
            //pParam[1] = "@file_id:" + file_id;

            //string message = "";

            //DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "OpenFile", pParam);

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

        //파일 등록
        public string InsertFile(byte[] myBytes, string schedule_id, string doc_name, string contentType)
        {
            //var doc_type = "";

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

            string sSpName = "sp_DayScheduleList_Y";

            string[] pParam = new string[3];
            pParam[0] = "@schedule_id:" + schedule_id;
            pParam[1] = "@doc_name:" + doc_name;
            pParam[2] = "@doc_type:" + contentType;

            string message = _bllSpExecute.SpExecuteString(sSpName, "InsertFile", "@file_image", myBytes, pParam);

            return message;
        }

        //전자서명 정보 조회
        public DataTable SignerSearch(string schedule_id, string sign_set_cd, string sign_seq)
        {
            string sSpName = "sp_DayScheduleList_Y";

            string[] pParam = new string[2];
            pParam[0] = "@schedule_id:" + schedule_id;
            pParam[1] = "@sign_set_cd:" + sign_set_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "SelectE", pParam);

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


        // 점검기록서 대리자 권한 조회
        internal bool GetRepresentativeAuthority(string emp_cd, string sign_set_dt_seq, string signCode)
        {
            string sSpName = "SP_ElectronicSignature";
            string pGubun = "RepresentativeSearch";

            bool check = false;


            string[] pParam = new string[3];
            pParam[0] = "@sign_emp_cd:" + emp_cd;
            pParam[1] = "@sign_set_dt_seq:" + sign_set_dt_seq;
            pParam[2] = "@sign_set_cd:" + signCode;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (message == "Y")
                check = true;

            return check;
        }


        //점검자/확인자 전자서명
        public string SettingSign(string schedule_id, string emp_cd, string gubun)
        {
            string sSpName = "sp_DayScheduleList_Y";

            // C : 점검자 / Q : 확인자
            string pGubun = (gubun == "C") ? "ES_C" : "ES_Q";

            string[] pParam = new string[3];
            pParam[0] = "@schedule_id:" + schedule_id;
            pParam[1] = "@emp_cd:" + emp_cd;
            pParam[2] = "@validation_type:" + "2";

            string result = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return result;
        }

        //확인자 서명 삭제
        public string CancelSign(string schedule_id, string gubun)
        {
            string sSpName = "sp_DayScheduleList_Y";

            // C : 점검자 / Q : 확인자
            string pGubun = (gubun == "C") ? "ES_CD" : "ES_QD";

            string[] pParam = new string[1];
            pParam[0] = "@schedule_id:" + schedule_id;

            string result = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return result;
        }

        public DataTable GetProgramSet()
        {
            string sSpName = "SP_ProgramInit";

            string[] pParam = new string[1];
            pParam[0] = "@code:" + "DayScheduleList";

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, "S3", pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

    }
}