using HACCP.Libs;
using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HACCP.Models.Doc;
using System.IO;
using System.Diagnostics;
using System.Web;

namespace HACCP.Services.Doc
{
    public class GmpDocRecordSearchService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable SearchDocRecord_1(string elect_check_ck, string tabGubun, string start, string end, string author, string docName)
        {
            string sSpName = "SP_Gmp_doc_record_search";
            string pGubun = "SD";
            string[] pParam = new string[7];

            pParam[0] = "@emp_cd_S:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[1] = "@s_date_S:" + start;
            pParam[2] = "@e_date_S:" + end;
            pParam[3] = "@elect_check:" + elect_check_ck;
            pParam[4] = "@s_gubun_S:" + author;
            pParam[5] = "@doc_record_nm_S:" + docName;
            pParam[6] = "@search_select:" + tabGubun;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal DataTable SearchDocRecord_2(string elect_check_ck, string start, string end, string author, string docName)
        {
            string sSpName = "SP_Gmp_doc_record_search";
            string pGubun = "SR";
            string[] pParam = new string[6];

            pParam[0] = "@emp_cd_S:" + HttpContext.Current.Session["loginCD"].ToString(); 
            pParam[1] = "@s_date_S:" + start;
            pParam[2] = "@e_date_S:" + end;
            pParam[3] = "@elect_check:" + elect_check_ck;
            pParam[4] = "@s_gubun_S:" + author;
            pParam[5] = "@doc_record_nm_S:" + docName;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;
        }

        internal string CheckVoidDate(string voidDate)
        {
            string sSpName = "SP_Gmp_doc_record_search";
            string pGubun = "CHK_V";
            string[] pParam = new string[1];

            voidDate = (voidDate == "") ? "2100-01-01" : voidDate;
            pParam[0] = "@void_date:" + voidDate;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return result;
        }

        internal DataTable GetFile(string doc_file_id)
        {

            string sSpName = "SP_Gmp_doc_record_search";
            string pGubun = "FS";
            string[] pParam = new string[1];

            pParam[0] = "@doc_file_id:" + doc_file_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, pGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }

            return dt;

            //string sSpName = "SP_Gmp_doc_record_search";
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

        public string GetProgramSet(string code)
        {
            string sSpName = "SP_ProgramInit";

            string[] pParam = new string[1];
            pParam[0] = "@code:" + code;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, "S2", pParam);

            return result;
        }

    }
}
