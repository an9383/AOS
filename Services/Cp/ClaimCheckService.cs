using HACCP.Libs.Database;
using HACCP.Models.Cp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace HACCP.Services.Cp
{
    public class ClaimCheckService
    {

        public BllSpExecute _bllSpExecute = new BllSpExecute();

        //불만사항 리스트 조회
        public List<ClaimCheck> GridSelect(ClaimCheck.SrchParam srch)
        {
            string sSpName = "SP_ClaimCheck_Y";

            string[] pParam = new String[6];
            pParam[0] = "@select_S:" + srch.select_S;
            pParam[1] = "@Sdate_S:" + srch.Sdate_S;
            pParam[2] = "@Edate_S:" + srch.Edate_S;
            pParam[3] = "@select_gubun_S:" + srch.select_gubun_S;
            pParam[4] = "@searchtext_S:" + srch.searchtext_S;
            pParam[5] = "@claim_status_S:" + srch.claim_status_S;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S", pParam);

            List<ClaimCheck> claimCheckList = new List<ClaimCheck>();

            foreach (DataRow row in dt.AsEnumerable())
            {
                ClaimCheck claimCheck = new ClaimCheck(row);
                claimCheckList.Add(claimCheck);
            }

            return claimCheckList;
        }

        //불만사항 세부사항 조회
        public DataTable GridSelect1(string claim_id)
        {
            string sSpName = "SP_ClaimCheck_Y";
            string[] pParam = new string[1];
            pParam[0] = "@claim_id:" + claim_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, "S1", pParam);
            return dt;
        }

        //불만사항 삭제
        public string GridDelete(string claim_id)
        {
            string sSpName = "SP_ClaimCheck_Y";

            string[] pParam = new string[1];
            pParam[0] = "@claim_id:" + claim_id;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "D", pParam);

            return message;
        }


        //불만사항 조사완료
        public string GridInsert(ClaimCheck claimCheck)
        {
            string sSpName = "SP_ClaimCheck_Y";

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "I", GetParam(claimCheck));

            return message;
        }


        //불만사항 수정
        public string GridUpdate(ClaimCheck claimCheck)
        {
            string sSpName = "SP_ClaimCheck_Y";

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, "U", GetParam(claimCheck));

            return message;
        }

        //불만사항 메인 디테일 수정
        public string GridUpdate1(List<ClaimDetailCheck> ClaimDetailCheck)
        {
            string sSpName = "SP_ClaimCheck_Y";
            int count = 0;

            foreach (ClaimDetailCheck detail in ClaimDetailCheck)
            {
                string[] pParam = new string[3];
                pParam[0] = "@claim_id:" + detail.claim_id;
                pParam[1] = "@claim_dt_id:" + detail.claim_dt_id;
                pParam[2] = "@judge_value:" + detail.judge_value;

                string message = _bllSpExecute.SpExecuteString(sSpName, "U1", pParam);
                if(message.Length > 0) count++;
            }
            //string message = "";
            //message = _bllSpExecute.SpExecuteString(sSpName, "U1", pParam);

            if(count > 0) return "입력되었습니다";
            else          return "입력실패하였습니다";

        }

        public string[] GetParam(ClaimCheck claimCheck)
        {
			string[] param = new string[10];

			param[0] = "@claim_id:" + claimCheck.claim_id;
			param[1] = "@result_opinion:" + claimCheck.result_opinion;
			param[2] = "@plan_opinion:" + claimCheck.plan_opinion;
			param[3] = "@judge_date:" + claimCheck.judge_date;
			param[4] = "@judge_emp_cd:" + claimCheck.judge_emp_cd;
			param[5] = "@judge_type:" + claimCheck.judge_type;
			param[6] = "@claim_type:" + claimCheck.claim_type;
			param[7] = "@judge_opinion:" + claimCheck.judge_opinion;
			param[8] = "@dept_cd1:" + claimCheck.dept_cd1;
			param[9] = "@dept_cd2:" + claimCheck.dept_cd2;
			return param;

        }

        #region 파일관리
        //파일 등록
        internal string InsertFile(byte[] myBytes, string claim_id, string fgubun, string doc_name, string contentType)
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

            string sSpName = "SP_ClaimCheck_Y";

            string[] pParam = new string[4];
            pParam[0] = "@claim_id:" + claim_id;
            pParam[1] = "@fgubun:" + fgubun;
            pParam[2] = "@doc_name:" + doc_name;
            pParam[3] = "@doc_type:" + contentType;

            string message = _bllSpExecute.SpExecuteString(sSpName, "F", "@file_image", myBytes, pParam);

            return message;
        }

        //파일 수정
        internal string UpdateFile(byte[] myBytes, string doc_file_id, string fgubun, string doc_name, string contentType)
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

            string sSpName = "SP_ClaimCheck_Y";

            string[] pParam = new string[4];
            pParam[0] = "@doc_file_id:" + doc_file_id;
            pParam[1] = "@fgubun:" + fgubun;
            pParam[2] = "@doc_name:" + doc_name;
            pParam[3] = "@doc_type:" + contentType;

            string message = _bllSpExecute.SpExecuteString(sSpName, "FE", "@file_image", myBytes, pParam);

            return message;
        }

        //파일 삭제
        internal string DeleteFile(string doc_file_id, string fgubun)
        {
            string sSpName = "SP_ClaimCheck_Y";

            string[] pParam = new string[2];
            pParam[0] = "@doc_file_id:" + doc_file_id;
            pParam[1] = "@fgubun:" + fgubun;

            string message = _bllSpExecute.SpExecuteString(sSpName, "FD", pParam);

            return message;
        }
        
        //파일 조회
        internal DataTable GetFile(string doc_file_id)
        {

            string sSpName = "SP_ClaimCheck_Y";
            string sGubun = "FS";
            string pParam = "@doc_file_id:" + doc_file_id;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;

            //string sSpName = "SP_ClaimCheck_Y";
            //string sGubun = "FS";
            //string pParam = "@doc_file_id:" + doc_file_id;

            //string message = "";

            //DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            //if (dt.Rows.Count > 0)
            //{
            //    string fileName = "";

            //    //파일의 결재라인 체크 함수 사용여부를 불러온다,
            //    string DocumentFileStoredFolder = "";   //문서저장 폴더
            //    DocumentFileStoredFolder = _bllSpExecute.SpExecuteString("SP_ProgramInit", "S2", "@code:Doc_record_folder_value"); ;

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

        #endregion
    }
}