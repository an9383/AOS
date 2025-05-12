using HACCP.Libs;
using HACCP.Libs.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HACCP.Models.Doc;
using DevExpress.DataAccess.Native.Web;
using System.IO;
using System.Diagnostics;
using System.Web;

namespace HACCP.Services.Doc
{
    public class GmpDocRegistrationService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        internal DataTable GridSelect(string doc_nm, string use_yn, string s_gubun, string parent_cd, string writer_dept_cd)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string sGubun = "S";
            string[] pParam = new string[6];

            pParam[0] = "@doc_nm_S:" + doc_nm;
            pParam[1] = "@use_yn_S:" + use_yn;
            pParam[2] = "@login_nm_S:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[3] = "@s_gubun_S:" + s_gubun;
            pParam[4] = "@parent_cd:" + parent_cd;
            pParam[5] = "@writer_dept_cd_S:" + writer_dept_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }


        internal DataTable SelectGroupEmp(string gubun)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string sGubun = gubun;

            DataTable dt = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun).Tables[0];

            return dt;
        }

        internal DataTable SearchRevision(string doc_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string sGubun = "S1";
            string[] pParam = new string[1];

            pParam[0] = "@doc_no:" + doc_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DoclineSearch(string doc_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string sGubun = "SD1";
            string[] pParam = new string[1];

            pParam[0] = "@doc_no:" + doc_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DoclineDetailSearch(string sign_set_cd)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string sGubun = "SD2";
            string[] pParam = new string[1];

            pParam[0] = "@sign_set_cd:" + sign_set_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable RecordlineSearch(string doc_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string sGubun = "SR1";
            string[] pParam = new string[1];

            pParam[0] = "@doc_no:" + doc_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable RecordlineDetailSearch(string sign_set_cd)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string sGubun = "SD2";
            string[] pParam = new string[1];

            pParam[0] = "@sign_set_cd:" + sign_set_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable DocReaderSearch(string doc_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string sGubun = "GS";
            string[] pParam = new string[2];

            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@gubun_ck:" + "1";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal DataTable RecordReaderSearch(string doc_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string sGubun = "GS";
            string[] pParam = new string[2];

            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@gubun_ck:" + "2";

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string GetDocNo(string structure_cd)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "MDN";

            string[] pParam = new string[1];
            pParam[0] = "@structure_cd:" + structure_cd;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return message;
        }

        internal bool CheckRecord(string doc_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "CKD";

            string[] pParam = new string[1];
            pParam[0] = "@doc_no:" + doc_no;

            string checkcnt = "";
            checkcnt = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (int.Parse(checkcnt) > 0)
                return true;
            else
                return false;
        }

        internal string GridDelete(string doc_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "D";

            string[] pParam = new string[1];
            pParam[0] = "@doc_no:" + doc_no;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return message;
        }

        internal string DeleteRevision(string doc_no, string revision_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "D1";

            string[] pParam = new string[2];
            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;

          string message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);
          return message;
        }

        internal bool CheckDist(string doc_no, string revision_no)
        {
            bool check = false;

            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "CD";

            string[] pParam = new string[2];
            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (message != "0")
                check = true;

            return check;
        }


        internal string GridInsert(DocRegister doc)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "I";

            string[] pParam = new string[23];
            pParam[0] = "@doc_no:" + doc.doc_no;
            pParam[1] = "@structure_cd:" + doc.structure_cd;
            pParam[2] = "@child_no:" + doc.child_no;
            pParam[3] = "@doc_name:" + doc.doc_name;
            pParam[4] = "@doc_search_keyword:" + doc.doc_search_keyword;
            pParam[5] = "@doc_class:" + doc.doc_class;
            pParam[6] = "@doc_type:" + doc.doc_type;
            pParam[7] = "@approval_date:" + doc.approval_date;
            pParam[8] = "@start_date:" + doc.start_date;
            pParam[9] = "@archive_period:" + doc.archive_period;
            pParam[10] = "@archive_period_unit:" + doc.archive_period_unit;
            pParam[11] = "@exam_cycle:" + doc.exam_cycle;
            pParam[12] = "@exam_cycle_unit:" + doc.exam_cycle_unit;
            pParam[13] = "@archive_position_cd:" + doc.archive_position_cd;
            pParam[14] = "@archive_position:" + doc.archive_position;
            pParam[15] = "@binding_no:" + doc.binding_no;
            //작성자 코드(Public_Function.User_cd ->be_writer_cd값으로 변경)
            pParam[16] = "@writer_cd:" + doc.writer_cd;
            pParam[17] = "@charge_cd:" + doc.charge_cd;
            pParam[18] = "@rule_cd:" + doc.rule_cd;
            pParam[19] = "@rule_no:" + doc.rule_no;
            pParam[20] = "@use_yn:" + doc.use_yn;
            pParam[21] = "@secret_doc_ck:" + doc.secret_doc_ck;
            pParam[22] = "@doc_seq:" + doc.doc_seq;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return message;
        }

        internal string InsertRevisionData(DocRevision doc)
        { 
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "I1";

            string[] pParam = new string[12];
            pParam[0] = "@doc_no:" + doc.doc_no;
            pParam[1] = "@revision_no:" + doc.revision_no;
            pParam[2] = "@current_yn:" + doc.current_yn;
            pParam[3] = "@revision_date:" + doc.revision_date;
            pParam[4] = "@operation_date:" + doc.operation_date;
            pParam[5] = "@revision_content:" + doc.revision_content;
            pParam[6] = "@revision_reason:" + doc.revision_reason;
            pParam[7] = "@revision_ground:" + doc.revision_ground;
            pParam[8] = "@revise_emp_cd:" + doc.revise_emp_cd;
            pParam[9] = "@void_date:" + doc.void_date;
            pParam[10] = "@revision_dept_cd:" + doc.revision_dept_cd;

            if (doc.docRecord_elecsignuse_yn == "N")           
                pParam[11] = "@approval_end_yn:" + "Y";           
            else          
                pParam[11] = "@approval_end_yn:" + "N";
            
            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return message;
        }


        internal string UpdateRevisionData(DocRevision doc)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "U1";

            string[] pParam = new string[11];
            pParam[0] = "@doc_no:" + doc.doc_no;
            pParam[1] = "@revision_no:" + doc.revision_no;
            pParam[2] = "@current_yn:" + doc.current_yn;
            pParam[3] = "@revision_date:" + doc.revision_date;
            pParam[4] = "@operation_date:" + doc.operation_date;
            pParam[5] = "@revision_content:" + doc.revision_content;
            pParam[6] = "@revision_reason:" + doc.revision_reason;
            pParam[7] = "@revision_ground:" + doc.revision_ground;
            pParam[8] = "@revise_emp_cd:" + doc.revise_emp_cd;
            //pParam[10] = "@writer_cd:" + doc.void_date;
            //pParam[11] = "@charge_cd:" + doc.void_date;
            pParam[9] = "@void_date:" + doc.void_date;
            pParam[10] = "@revision_dept_cd:" + doc.revision_dept_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);
            return message;

        }


        internal string InsertToEmpower(string doc_no, string group_emp_ck, string writer_cd, string pGubun)
        {
            string sSpName = "SP_Gmp_doc_registration";

            string[] pParam = new string[3];
            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@group_emp_ck:" + group_emp_ck;
            pParam[2] = "@group_emp_cd:" + writer_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);
            return message;

        }

        internal string DeleteToEmpower(string doc_no, string doc_record_ck, string group_emp_ck, string group_emp_cd)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "RDD";

            string[] pParam = new string[4];
            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@doc_record_ck:" + doc_record_ck;
            pParam[2] = "@group_emp_ck:" + group_emp_ck;
            pParam[3] = "@group_emp_cd:" + group_emp_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);
            return message;

        }

        internal void DeleteSignLine(string sign_set_cd, string revision_no, string doc_no, string gubun)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "UDD";

            string[] pParam = new string[4];
            pParam[0] = "@sign_set_cd:" + sign_set_cd;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@doc_no:" + doc_no;
            pParam[3] = "@gubun_D:" + gubun;

            _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);
        }

        internal void UpdateSignLine_1(string sign_set_cd, string revision_no, string doc_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "UDL";

            string[] pParam = new string[3];
            pParam[0] = "@sign_set_cd:" + sign_set_cd;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@doc_no:" + doc_no;

            _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);
        }

        internal void UpdateSignLine_2(string sign_set_cd, string doc_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "URL";

            string[] pParam = new string[2];
            pParam[0] = "@sign_set_cd:" + sign_set_cd;
            pParam[1] = "@doc_no:" + doc_no;

            _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);
        }

        internal string UpdateConfirmDate(string doc_no, string confirm_date, string archive_period, string archive_period_unit)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "UpdateConfirmDate";

            string[] pParam = new string[4];
            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@confirm_date:" + confirm_date;
            pParam[2] = "@archive_period:" + archive_period;
            pParam[3] = "@archive_period_unit:" + archive_period_unit;

            string message = "";

            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);
            return message;
        }


        internal string UseSelect(string doc_no, string revision_no, string use_select)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "USF";

            string[] pParam = new string[3];
            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@use_select:" + use_select;

            string message = "";

            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);
            return message;
        }

        internal string InsertApprovalLine(string doc_sign_set_cd, string doc_no, string revision_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "IL";

            string[] pParam = new string[3];
            pParam[0] = "@sign_set_cd:" + doc_sign_set_cd;
            pParam[1] = "@doc_no:" + doc_no;
            pParam[2] = "@revision_no:" + revision_no;

            string message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);
            return message;
        }

        internal void InsertFileData(string main_file_id, string reference_file_id1, string reference_file_id2, string reference_file_id3, string reference_file_id4, string doc_no, string revision_no)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "IF";

            string[] pParam = new string[7];
            pParam[0] = "@main_file_id:" + main_file_id;
            pParam[1] = "@sub_file1:" + reference_file_id1;
            pParam[2] = "@sub_file2:" + reference_file_id2;
            pParam[3] = "@sub_file3:" + reference_file_id3;
            pParam[4] = "@sub_file4:" + reference_file_id4;
            pParam[5] = "@doc_no:" + doc_no;
            pParam[6] = "@revision_no:" + revision_no;

            _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

        }


        internal string GridEdit(DocRegister doc)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "U";

            string[] pParam = new string[25];
            pParam[0] = "@doc_no:" + doc.doc_no;
            pParam[1] = "@structure_cd:" + doc.structure_cd;
            pParam[2] = "@child_no:" + doc.child_no;
            pParam[3] = "@doc_name:" + doc.doc_name;
            pParam[4] = "@doc_search_keyword:" + doc.doc_search_keyword;
            pParam[5] = "@doc_class:" + doc.doc_class;
            pParam[6] = "@doc_type:" + doc.doc_type;
            pParam[7] = "@approval_date:" + doc.approval_date;
            pParam[8] = "@start_date:" + doc.start_date;
            pParam[9] = "@archive_period:" + doc.archive_period;
            pParam[10] = "@archive_period_unit:" + doc.archive_period_unit;
            pParam[11] = "@exam_cycle:" + doc.exam_cycle;
            pParam[12] = "@exam_cycle_unit:" + doc.exam_cycle_unit;
            pParam[13] = "@archive_position_cd:" + doc.archive_position_cd;
            pParam[14] = "@archive_position:" + doc.archive_position;
            pParam[15] = "@binding_no:" + doc.binding_no;
            //작성자 코드(Public_Function.User_cd ->be_writer_cd값으로 변경)
            pParam[16] = "@writer_cd:" + doc.writer_cd;
            pParam[17] = "@charge_cd:" + doc.charge_cd;
            pParam[18] = "@rule_cd:" + doc.rule_cd;
            pParam[19] = "@rule_no:" + doc.rule_no;
            pParam[20] = "@use_yn:" + doc.use_yn;
            pParam[21] = "@secret_doc_ck:" + doc.secret_doc_ck;
            pParam[22] = "@doc_ename:" + doc.doc_ename;
            pParam[23] = "@writer_dept_cd:" + doc.writer_dept_cd;
            pParam[24] = "@doc_seq:" + doc.doc_seq;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);
          
            return message;

        }

        internal bool ApprovalCheck(string doc_no, string revision_no)
        {
            var check = false;

            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "AC";

            string[] pParam = new string[2];
            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if(int.Parse(message) > 0)
            {
                check = true;
            }
            return check;
        }

        internal bool RegisterOrDeleteDocument(string sign_set_cd)
        {
            var check = true;

            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "registerOrDelete_document";

            string[] pParam = new string[2];
            pParam[0] = "@sign_set_cd:"+ sign_set_cd;
            pParam[1] = "@emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();

            string message = "";
            message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            if (int.Parse(message) == 0)
            {
                check = false;
            }
            return check;
        }

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

            string sSpName = "SP_Gmp_doc_registration";

            string[] pParam = new string[4];
            pParam[0] = "@doc_file_id:" + doc_file_id;
            pParam[1] = "@fgubun:" + fgubun;
            pParam[2] = "@doc_name:" + doc_name;
            pParam[3] = "@doc_type:" + contentType;

            string message = _bllSpExecute.SpExecuteString(sSpName, "FE", "@file_image", myBytes, pParam);

            return message;
        }

        internal string DeleteFile(string doc_no, string revision_no, string fgubun, string doc_file_id)
        {
            string sSpName = "SP_Gmp_doc_registration";

            string[] pParam = new string[4];
            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@fgubun:" + fgubun;
            pParam[3] = "@doc_file_id:" + doc_file_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, "FD", pParam);

            return message;
        }


        internal string InsertFile(byte[] myBytes, string doc_no, string revision_no, string fgubun, string doc_name, string contentType)
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

            string sSpName = "SP_Gmp_doc_registration";

            string[] pParam = new string[5];
            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@fgubun:" + fgubun;
            pParam[3] = "@doc_name:" + doc_name;
            pParam[4] = "@doc_type:" + contentType;

            string message = _bllSpExecute.SpExecuteString(sSpName, "F", "@file_image", myBytes, pParam);

            return message;
        }

        internal DataTable GetFile(string doc_file_id)
        {
            string sSpName = "SP_Gmp_doc_registration";
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

            //string sSpName = "SP_Gmp_doc_registration";
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



        internal string ChkVoidDate(string void_date)
        {
            string sSpName = "SP_Gmp_doc_registration";

            string pGubun = "CHK_V";
            string[] pParam = new string[1];

            void_date = (void_date == "") ? "2100-01-01" : void_date;
            pParam[0] = "@void_date:" + void_date;

            string result = "";
            result = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return result;
        }

        internal DataTable GetEmpPopupData()
        {
            string strsql = "SELECT emp_cd, emp_nm, dept_cd, dept_nm";
            strsql += " FROM V_EMPLOYEE";
            strsql += " WHERE (emp_cd  LIKE '%%' OR emp_nm  LIKE '%%')";
            strsql += " AND emp_cd  LIKE '%%' AND emp_nm  LIKE '%%'";
            strsql += " ORDER BY 1";

            DataTable dt = _bllSpExecute.SpExecuteTable("CODEHELP", strsql);

            return dt;
        }

        internal string EmpExclusion(string doc_no, string revision_no, string accept_emp_cd)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "DDED";
            string[] pParam = new string[3];

            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@accept_emp_cd:" + accept_emp_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return message;
        }


        internal string Distribute(string doc_no, string revision_no, string over_emp_cd, string accept_emp_cd)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string pGubun = "DDE";
            string[] pParam = new string[4];

            pParam[0] = "@doc_no:" + doc_no;
            pParam[1] = "@revision_no:" + revision_no;
            pParam[2] = "@over_emp_cd:" + over_emp_cd;
            pParam[3] = "@accept_emp_cd:" + accept_emp_cd;

            string message = _bllSpExecute.SpExecuteString(sSpName, pGubun, pParam);

            return message;
        }

        #region Report 관련
        internal DataTable GetReportData(string doc_nm, string use_yn, string s_gubun, string parent_cd)
        {
            string sSpName = "SP_Gmp_doc_registration";
            string sGubun = "R";
            string[] pParam = new string[5];

            pParam[0] = "@doc_nm_S:" + doc_nm;
            pParam[1] = "@use_yn_S:" + use_yn;
            pParam[2] = "@login_nm_S:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[3] = "@s_gubun_S:" + s_gubun;
            pParam[4] = "@parent_cd:" + parent_cd;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);
            dt.TableName = "Gmp_doc_listT";

            return dt;
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

        #endregion

        #region EDMS
        internal bool FileInfomationTransToEDM(string doc_name, string doc_search_keyword, string structure_cd, string child_no, int fileCount, long fileTotalSize, string fileFolderPath, string fileList, string writer_cd)
        {
            string sSpName = "SP_Gmp_doc_registration";

            string[] param = new string[13];
            param[0] = "@title:" + doc_name;                                    //제목
            param[1] = "@KeywordList:" + doc_search_keyword;                    //검색어 ,로 구분
            param[2] = "@doc_no:" + structure_cd + child_no;                    //문서번호
            param[3] = "@doc_security:" + "4";                                  //(복합파라메터 설정)문서등급
            param[4] = "@FileCount:" + fileCount;                               //첨부파일수
            param[5] = "@FileTotalSize:" + fileTotalSize;                       //첨부파일용량
            param[6] = "@FileFolderPath:" + fileFolderPath;                     //FTP파일경로
            param[7] = "@FileList:" + fileList;                                 //첨부파일명
            param[8] = "@SystemName:" + "LIMS";                                 //(복합파라메터 설정)시스템명
            param[9] = "@FolderFullPath:" + "LIMS/A폴더";                       //(복합파라메터 설정)이관절대경로
            param[10] = "@MultimapFolderID:" + "0";                             //(복합파라메터 설정)EDMS의 업무폴더ID
            param[11] = "@Creator:" + "admin";                                  //(복합파라메터 설정)계정정보로 설정(기본 admin)
            param[12] = "@AuthorInfoList:" + writer_cd;                         //작성자

            string message = "";

            message = _bllSpExecute.SpExecuteString(sSpName, "FT", param);
            if (message.ToString().Length == 0)
                return false;
            else
            {
                return true;
            }
        }
        #endregion
    }
}
