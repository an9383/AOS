using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Mont;

namespace HACCP.Services.Mont
{
    public class HACCP_FileManageService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable FileSearch(string doc_cd)
        {
            string sSpName = "SP_HACCP_FileManage";
            string gubun = "S_File";
            string[] pParam = new string[1];

            pParam[0] = "@doc_cd:" + doc_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal string FileAdd(byte[] myBytes, string fileName, string doc_cd, string doc_name, string ccp_emp, string doc_base_date)
        {
            var doc_type = Path.GetExtension(fileName);

            string sSpName = "SP_HACCP_FileManage";
            string sGubun = "FU";
            string[] pParam = new string[6];

            pParam[0] = "@doc_cd:" + doc_cd;
            pParam[1] = "@doc_ccp_emp_cd:" + ccp_emp;
            pParam[2] = "@doc_input_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[3] = "@doc_name:" + doc_name;
            pParam[4] = "@doc_type:" + doc_type;
            pParam[5] = "@doc_base_date:" + doc_base_date;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@file_image", myBytes, pParam);

            return message;
        }

        internal string FileDelete(string doc_cd, string file_id)
        {
            string sSpName = "SP_HACCP_FileManage";
            string sGubun = "FD";
            string[] pParam = new string[2];

            pParam[0] = "@doc_cd:" + doc_cd;
            pParam[1] = "@doc_file_id:" + file_id;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        //다운로드
        internal DataTable EditActionFileOpen(string file_id)
        {
            string sSpName = "SP_HACCP_FileManage";
            string sGubun = "FS";
            string pParam = "@doc_file_id:" + file_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            else
            {
                return null;
            }

            return dt;
        }

        //다운로드
        internal DataTable EditActionEQUIP_FileOpen(string file_id)
        {
            string sSpName = "SP_GET_EQUIPMENT_DATA";
            string sGubun = "FS";
            string pParam = "@FILE_IDX:" + file_id;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
            }
            else
            {
                return null;
            }

            return dt;
        }

        //바로 여는 함수.
        internal string FileOpen(string file_id)
        {
            string sSpName = "SP_HACCP_FileManage";
            string sGubun = "FS";
            string pParam = "@doc_file_id:" + file_id;

            string message = "";


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);


            if (dt.Rows.Count > 0)
            {
                string fileName = "";

                //파일의 결재라인 체크 함수 사용여부를 불러온다,
                string DocumentFileStoredFolder = "";   //문서저장 폴더
                DocumentFileStoredFolder = _bllSpExecute.SpExecuteString("SP_ProgramInit", "S2", "@code:Doc_record_folder_value");

                //파일관리 폴더가 존재하지 않는경우에는 생성한다.
                if (Directory.Exists(DocumentFileStoredFolder) == false)
                {
                    Directory.CreateDirectory(DocumentFileStoredFolder);
                }

                //이미지 정보를 폴더에 파일로 만든다.
                fileName = dt.Rows[0]["doc_name"].ToString();
                //파일이 존재하면 삭제하도록 한다.
                if (File.Exists(@DocumentFileStoredFolder + "\\" + fileName))
                {
                    File.Delete(@DocumentFileStoredFolder + "\\" + fileName);
                }

                byte[] bs;
                System.IO.FileStream fs = new FileStream(@DocumentFileStoredFolder + "\\" + fileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                bs = (byte[])dt.Rows[0]["file_image"];
                System.IO.BinaryWriter bw;
                bw = new System.IO.BinaryWriter(fs);
                bw.Write(bs);
                bw.Close();
                fs.Close();

                message = "success";

                Process.Start(@DocumentFileStoredFolder + "\\" + fileName);
            }
            else
            {
                message = "fail";
            }

            return message;

        }
    }
}