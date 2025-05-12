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
using HACCP.Models.Education;

namespace HACCP.Services.Education
{
    public class EmployeeEduService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable EmployeeEdu_Search(EmployeeEdu iModel)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "S";

            string[] pParam = new string[6];
            //조회시 사용
            pParam[0] = "@title:" + iModel.title;
            pParam[1] = "@sdate:" + iModel.sdate;
            pParam[2] = "@edate:" + iModel.edate;
            pParam[3] = "@edu_gb:" + iModel.edu_gb;
            pParam[4] = "@edu_method:" + iModel.edu_method;
            pParam[5] = "@dept_cd:" + iModel.dept_cd;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal DataTable Employee_Search(EmployeeEdu iModel)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "S2";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@sdate:" + iModel.sdate;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal DataTable AttendEmployee_Search(string edu_no)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "S1";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@edu_no:" + edu_no;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal DataTable EmployeeEdu_FindGroup(EmployeeEdu iModel)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "S1_group";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@edu_no:" + iModel.edu_no;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal DataTable EmployeeEdu_FindAttendGroup(string edu_no)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "S2_group";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@edu_no:" + edu_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal DataTable EmployeeEdu_FindDepartment(EmployeeEdu iModel)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "S_Dept";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@edu_no:" + iModel.edu_no;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal DataTable EmployeeEdu_FindLocation(EmployeeEdu iModel)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "S_Location";

            string[] pParam = new string[1];
            //조회시 사용
            pParam[0] = "@edu_no:" + iModel.edu_no;


            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        //교육번호 자동생성
        internal List<string> EmployeeEdu_GetInsertInfo()
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "S3";
            string[] pParam = new string[1];
            pParam[0] = "@write_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();

            List<string> result = new List<string>();
            result.Add(_bllSpExecute.SpExecuteString(sSpName, gubun));
            result.Add(HttpContext.Current.Session["loginCD"].ToString());

            gubun = "S_empcode";
            DataTable s = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);


            result.Add(s.Rows[0][0].ToString());

            return result;
        }

        internal string EmployeeEdu_CRUD(EmployeeEdu iModel, string gubun)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string sGubun = gubun;

            string message = "";

            if (sGubun == "D")
            {
                //삭제하기전에 첨부파일 먼저 삭제
                message = _bllSpExecute.SpExecuteString(sSpName, "FD", iModel.edu_no);

            }

            string[] pParam = new string[23];
            pParam[0] = "@edu_no:" + iModel.edu_no;
            pParam[1] = "@edu_manage_no:" + iModel.edu_manage_no;
            pParam[2] = "@title:" + iModel.title;
            pParam[3] = "@edu_gb:" + iModel.edu_gb;
            pParam[4] = "@edu_method:" + iModel.edu_method;
            pParam[5] = "@lecturer_cd:" + iModel.lecturer_cd;
            pParam[6] = "@lecturer_nm:" + iModel.lecturer_nm;
            pParam[7] = "@edu_start_date:" + iModel.edu_start_date;
            pParam[8] = "@edu_end_date:" + iModel.edu_end_date;
            pParam[9] = "@edu_start_time:" + iModel.edu_start_time;
            pParam[10] = "@edu_end_time:" + iModel.edu_end_time;
            pParam[11] = "@edu_time:" + iModel.edu_time;
            pParam[12] = "@edu_place_cd:" + iModel.edu_place_cd;
            pParam[13] = "@edu_place_nm:" + iModel.edu_place_nm;
            pParam[14] = "@sort_contents:" + iModel.sort_contents;
            pParam[15] = "@contents:" + iModel.contents;
            pParam[16] = "@purpose:" + iModel.purpose;
            pParam[17] = "@dept_cd:" + iModel.dept_cd;
            pParam[18] = "@institute:" + iModel.institute;
            pParam[19] = "@comments:" + iModel.comments;
            pParam[20] = "@write_emp_cd:" + iModel.write_emp_cd;
            pParam[21] = "@effect_status:" + iModel.effect_status;
            pParam[22] = "@doc_file_id:" + iModel.doc_file_id;

            message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            string temp_msg = "";

            //서명정보 처리
            if (message != "0")
            {
                // 교육평가의 서명정보 코드에 강사를 대리자로 등록 //교육평가 코드 : "9102" (교육확인 : 9101)
                string sing_set_cd = "9102";

                string[] pParam0 = new string[1];
                pParam0[0] = "@sign_set_cd:" + sing_set_cd;

                string sign_set_dt_id = _bllSpExecute.SpExecuteString(sSpName, "SignInfo", pParam0);

                if (sGubun == "D")
                {
                    //강사이름으로 조회
                    string[] pParam3 = new string[1];
                    pParam3[0] = "@lecturer_cd:" + iModel.lecturer_cd;
                    DataTable dt_lecturer = _bllSpExecute.SpExecuteTable(sSpName, "lecturer", pParam3);

                    //두가지 교육 이상에 같은 강사가 할당되어 있는경우 삭제안함.
                    //현재 교육 1개만 할당되어 있는 경우에만 삭제
                    if (dt_lecturer.Rows.Count == 0) //이미 지웠으니까 0이어야함.
                    {
                        //서명권한 삭제
                        string[] pParam1 = new string[3];
                        pParam1[0] = "@sign_set_cd:" + sing_set_cd;
                        pParam1[1] = "@sign_set_dt_id:" + sign_set_dt_id;
                        pParam1[2] = "@emp_cd:" + iModel.lecturer_cd;

                        temp_msg = _bllSpExecute.SpExecuteString("SP_SignSet_InputDetail", "D2", pParam1);
                    }
                }
                else if (sGubun == "I")
                {
                    //서명권한 입력
                    string[] pParam2 = new string[3];
                    pParam2[0] = "@sign_set_cd:" + sing_set_cd; //서명 코드
                    pParam2[1] = "@sign_set_dt_id:" + sign_set_dt_id; //작성자필드의 id
                    pParam2[2] = "@emp_cd:" + iModel.lecturer_cd; //강사코드

                    string dt_SinInfo = _bllSpExecute.SpExecuteString(sSpName, "SignlecturerInfo", pParam2);

                    if (dt_SinInfo == "" || dt_SinInfo == null) //같은교육에 같은 강사의 데이터가 없을때만 입력.
                    {
                        temp_msg = _bllSpExecute.SpExecuteString("SP_SignSet_InputDetail", "I2", pParam2);
                    }
                }
            }

            return message;
        }

        internal string EmployeeEdu_YearPlan(EmployeeEdu iModel)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "SR_Year";
            string message = "";

            string[] pParam = new string[4];
            pParam[0] = "@edu_no:" + iModel.edu_no;
            pParam[1] = "@edu_gb:" + iModel.edu_gb;
            pParam[2] = "@edu_method:" + iModel.edu_method;
            pParam[3] = "@sdate:" + iModel.edu_start_date;

            message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return message;
        }

        internal string EmployeeEdu_PushNotice(string edu_no)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "PushNotice";
            string message = "";

            string[] pParam = new string[1];
            pParam[0] = "@edu_no:" + edu_no;

            message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return message;
        }

        internal string EmployeeEdu_AddEmployee(EmployeeEdu iModel)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "I2";
            string message = "";

            string[] pParam = new string[2];
            pParam[0] = "@edu_no:" + iModel.edu_no;
            pParam[1] = "@emp_cd:" + iModel.emp_cd;

            message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return message;
        }


        internal string EmployeeEdu_AddEmployeeAll(EmployeeEdu iModel)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "I3";
            string message = "";

            string[] pParam = new string[4];
            pParam[0] = "@edu_no:" + iModel.edu_no;
            pParam[1] = "@attendance_yn:" + iModel.attendance_yn;
            pParam[2] = "@evaluation:" + iModel.evaluation;
            pParam[3] = "@doc_file_id:" + iModel.doc_file_id;

            message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return message;
        }


        internal string EmployeeEdu_AddEmployeeGroup(EmployeeEdu iModel)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "I_group";
            string message = "";

            string[] pParam = new string[2];
            pParam[0] = "@edu_no:" + iModel.edu_no;
            pParam[1] = "@emp_group_cd:" + iModel.emp_group_cd;

            message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return message;
        }

        internal string EmployeeEdu_DeleteEmployee(EmployeeEdu iModel)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "D2";
            string message = "";

            string[] pParam = new string[2];
            pParam[0] = "@edu_no:" + iModel.edu_no;
            pParam[1] = "@emp_cd:" + iModel.emp_cd;

            message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return message;
        }

        internal string EmployeeEdu_DeleteEmployeeGroup(EmployeeEdu iModel)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "D_group";
            string message = "";

            string[] pParam = new string[2];
            pParam[0] = "@edu_no:" + iModel.edu_no;
            pParam[1] = "@emp_group_cd:" + iModel.emp_group_cd;

            message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return message;
        }

        internal DataTable FileSearch(string edu_no)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string gubun = "S_File";
            string[] pParam = new string[1];

            pParam[0] = "@edu_no:" + edu_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal string FileAdd(byte[] myBytes, string fileName, string contentType, EmployeeEdu iModel)
        {
            var doc_type = Path.GetExtension(fileName);

            string sSpName = "SP_EmployeeEdu_AOS";
            string sGubun = "FU";
            string[] pParam = new string[4];

            pParam[0] = "@edu_no:" + iModel.edu_no;
            pParam[1] = "@upload_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[2] = "@doc_name:" + iModel.doc_name;
            pParam[3] = "@doc_type:" + iModel.doc_type;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@file_image", myBytes, pParam);

            return message;
        }

        internal string FileDelete(string edu_no, string file_id)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string sGubun = "D_DOC";
            string[] pParam = new string[2];

            pParam[0] = "@edu_no:" + edu_no;
            pParam[1] = "@file_id:" + file_id;


            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        //다운로드
        internal DataTable EditActionFileOpen(string file_id)
        {
            string sSpName = "SP_EmployeeEdu_AOS";
            string sGubun = "FS";
            string pParam = "@file_id:" + file_id;

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
            string sSpName = "SP_EmployeeEdu_AOS";
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