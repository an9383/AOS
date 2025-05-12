using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Education;

namespace HACCP.Services.Education
{
    public class QualificationInfoService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        //사원목록조회
        internal DataTable QualificationInfo_Search(QualificationInfo iModel)
        {
            string sSpName = "SP_QualificationInfo_AOS";
            string gubun = "S1";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@emp_cd:" + iModel.emp_cd;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        //라이센스 상세정보
        internal DataTable QualificationInfo_SearchLicenseInfo(string emp_cd)
        {
            string sSpName = "SP_QualificationInfo_AOS";
            string gubun = "SelectQualificationInfo";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@emp_cd:" + emp_cd;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        //파일목록 조회
        internal DataTable QualificationInfo_SearchFile(string emp_cd, string license_cd)
        {
            {
                string sSpName = "SP_QualificationInfo_AOS";
                string gubun = "S_File";
                string[] pParam = new string[2];

                pParam[0] = "@emp_cd:" + emp_cd;
                pParam[1] = "@license_cd:" + license_cd;

                DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

                return dt;
            }
        }

        //내부교육
        internal DataTable QualificationInfo_Search_EduIn(string emp_cd)
        {
            string sSpName = "SP_QualificationInfo_AOS";
            string gubun = "SelectEducationIn";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@emp_cd:" + emp_cd;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        //외부교육
        internal DataTable QualificationInfo_Search_EduOut(string emp_cd)
        {
            string sSpName = "SP_QualificationInfo_AOS";
            string gubun = "SelectEducationOut";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@emp_cd:" + emp_cd;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        //라이센스 정보 CRUD
        internal string QualificationInfo_CRUD(QualificationInfo iModel, string gubun)
        {
            string sSpName = "SP_QualificationInfo_AOS";
            string sGubun = gubun; //InsertLicense, UpdateLicense, DeleteLicense, DeleteLicense_attach

            string message = "";

            if (iModel.Equals("Delete"))
            {
                //string[] pParam = new string[1];
                //pParam[0] = "@edu_no:" + iModel.edu_no;


                //message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            else
            {

                string[] pParam = new string[4];
                pParam[0] = "@emp_cd:" + iModel.emp_cd;
                pParam[1] = "@license_cd:" + iModel.license_cd;
                pParam[2] = "@license_info:" + iModel.license_info;
                pParam[3] = "@remark:" + iModel.remark;

                message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);
            }
            //정상 처리 되었습니다.
            return message;
        }



        internal string QualificationInfo_FileAdd(byte[] myBytes, string fileName, string contentType, QualificationInfo iModel)
        {
            var doc_type = Path.GetExtension(fileName);

            string sSpName = "SP_QualificationInfo_AOS";
            string sGubun = "FU";
            string[] pParam = new string[6];

            pParam[0] = "@emp_cd:" + iModel.emp_cd;
            pParam[1] = "@license_cd:" + iModel.license_cd;
            pParam[2] = "@license_no:" + iModel.license_no;
            pParam[3] = "@upload_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[4] = "@doc_name:" + iModel.doc_name;
            pParam[5] = "@doc_type:" + iModel.doc_type;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@file_image", myBytes, pParam);

            return message;
        }

        internal string QualificationInfo_FileDelete(QualificationInfo iModel)
        {
            string sSpName = "SP_QualificationInfo_AOS";
            string sGubun = "D_DOC";
            string[] pParam = new string[3];

            pParam[0] = "@emp_cd:" + iModel.emp_cd;
            pParam[1] = "@license_cd:" + iModel.license_cd;
            pParam[2] = "@file_id:" + iModel.file_id;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string QualificationInfo_FileOpen(string file_id)
        {
            string sSpName = "SP_QualificationInfo_AOS";
            string sGubun = "FS";
            string pParam = "@file_id:" + file_id;

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