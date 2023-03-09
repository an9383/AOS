using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models.Insp;

namespace HACCP.Services.Insp
{
    public class SelfAuditScheduleService
    {
        public BllSpExecute _bllSpExecute = new BllSpExecute();

        public DataTable Select(int sYear, string purpose, string step)
        {
            string sSpName = "SP_SelfAuditSchedule";
            string sGubun = "S";

            string[] pParam = new string[3];
            pParam[0] = "@self_audit_year:" + sYear;
            pParam[1] = "@self_audit_purpose:" + purpose;
            pParam[2] = "@self_audit_step:" + step;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, sGubun, pParam);

            return dt;
        }

        internal string modifyData(SelfAudit list)
        {
            string sSpName = "SP_SelfAuditSchedule";
            string sGubun = list.CRUD_gubun;

            string[] pParam = new string[15];
            pParam[0] = "@self_audit_no:" + list.audit_no;
            pParam[1] = "@self_audit_date:" + list.audit_start_date;
            pParam[2] = "@self_audit_director:" + list.audit_director_cd;
            pParam[3] = "@self_audit_object:" + list.audit_object;
            pParam[4] = "@self_audit_purpose:" + list.audit_purpose;
            pParam[5] = "@self_audit_w_date:" + list.audit_writer_date;
            pParam[6] = "@self_audit_w_emp_cd:" + list.audit_writer_cd;
            pParam[7] = "@self_audit_step:" + list.audit_step_cd;
            pParam[8] = "@self_audit_step_status:" + list.audit_step_status_cd;
            pParam[9] = "@self_audit_date_end:" + list.audit_end_date;
            pParam[10] = "@self_audit_purpose_detail:" + list.audit_purpose_d;
            pParam[11] = "@self_audit_plan_detail:" + list.audit_plan;
            pParam[12] = "@self_audit_doc_revision_plan:" + list.audit_doc_plan;
            pParam[13] = "@self_audit_special_detail:" + list.audit_special;
            pParam[14] = "@self_audit_check_gb:" + list.audit_check_gb;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal string SelfAuditScheduleDelete(SelfAudit list)
        {
            string sSpName = "SP_SelfAuditSchedule";
            string sGubun = "D";

            string[] pParam = new string[2];
            pParam[0] = "@self_audit_no:" + list.audit_no;
            pParam[1] = "@self_audit_year:" + list.self_audit_year;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }

        internal DataTable SelectSignInfo(string audit_year, string audit_no, string sign_set_cd)
        {
            string sSpName = "SP_SelfAuditSchedule";
            string sGubun = "Sign_S";
            string[] pParam = new string[3];
            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@sign_set_cd:" + sign_set_cd;

            DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);
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

        internal string DeleteSign(string gubun, string audit_no)
        {
            string sSpName = "SP_SelfAuditSchedule";
            string pParam = "@self_audit_no:" + audit_no;

            string message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return message;
        }

        internal string SaveSign(string gubun, string audit_no, string audit_s_emp_cd)
        {
            string sSpName = "SP_SelfAuditSchedule";
            string[] pParam = new string[4];

            pParam[0] = "@self_audit_no:" + audit_no;
            pParam[1] = "@self_audit_s_emp_cd:" + audit_s_emp_cd;
            pParam[2] = "@self_audit_s_emp_type:" + "2";
            pParam[3] = "@self_audit_step_status:" + "3";

            string message = _bllSpExecute.SpExecuteString(sSpName, gubun, pParam);

            return message;
        }

        internal DataTable FileSearch(SelfAudit list)
        {
            string sSpName = "SP_SelfAuditSchedule";
            string gubun = "DOC_S";
            string[] pParam = new string[2];

            pParam[0] = "@self_audit_year:" + list.self_audit_year;
            pParam[1] = "@self_audit_no:" + list.audit_no;

            DataTable dt = _bllSpExecute.SpExecuteTable(sSpName, gubun, pParam);

            return dt;
        }

        internal string FileAdd(byte[] myBytes, string fileName, string contentType, SelfAudit list)
        {
            var doc_type = Path.GetExtension(fileName);

            string sSpName = "SP_SelfAuditSchedule";
            string sGubun = "DOC_I";
            string[] pParam = new string[6];

            pParam[0] = "@self_audit_year:" + list.self_audit_year;
            pParam[1] = "@self_audit_no:" + list.audit_no;
            pParam[2] = "@upload_step:" + list.audit_step_cd;
            pParam[3] = "@upload_emp_cd:" + HttpContext.Current.Session["loginCD"].ToString();
            pParam[4] = "@doc_name:" + fileName;
            pParam[5] = "@doc_type:" + doc_type;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, "@file_image", myBytes, pParam);

            return message;
        }

        internal string FileDelete(string audit_year, string audit_no, string audit_file, string file_id)
        {
            string sSpName = "SP_SelfAuditSchedule";
            string sGubun = "DOC_D";
            string[] pParam = new string[4];

            pParam[0] = "@self_audit_year:" + audit_year;
            pParam[1] = "@self_audit_no:" + audit_no;
            pParam[2] = "@self_audit_file:" + audit_file;
            pParam[3] = "@file_id:" + file_id;

            string message = _bllSpExecute.SpExecuteString(sSpName, sGubun, pParam);

            return message;
        }


        internal DataTable FileOpen(string file_id)
        {
            string sSpName = "SP_SelfAuditSchedule";
            string sGubun = "DOC_O";
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



        //internal DataTable FileOpen(string file_id)
        //{
        //    string sSpName = "SP_SelfAuditSchedule";
        //    string sGubun = "DOC_O";
        //    string pParam = "@doc_file_id:" + file_id;

        //    string message = "";



        //    DataSet ds = _bllSpExecute.SpExecuteDataSet(sSpName, sGubun, pParam);

        //    DataTable dt = new DataTable();

        //    if (ds != null)
        //    {
        //        dt = ds.Tables[0];
        //    }else
        //    {
        //        return null;
        //    }


        //    if (dt.Rows.Count > 0)
        //    {
        //        string fileName = "";

        //        //파일의 결재라인 체크 함수 사용여부를 불러온다,
        //        string DocumentFileStoredFolder = "";   //문서저장 폴더
        //        DocumentFileStoredFolder = _bllSpExecute.SpExecuteString("SP_ProgramInit", "S2", "@code:Doc_record_folder_value"); ;

        //        dt.Columns.Add("downloadPath");
        //        dt.Rows[0]["downloadPath"] = DocumentFileStoredFolder;

        //        //파일관리 폴더가 존재하지 않는경우에는 생성한다.
        //        if (Directory.Exists(DocumentFileStoredFolder) == false)
        //        {
        //            Directory.CreateDirectory(DocumentFileStoredFolder);
        //        }

        //        //이미지 정보를 폴더에 파일로 만든다.
        //        fileName = dt.Rows[0]["doc_name"].ToString();
        //        //파일이 존재하면 삭제하도록 한다.
        //        if (File.Exists(@DocumentFileStoredFolder + "\\" + fileName))
        //        {
        //            File.Delete(@DocumentFileStoredFolder + "\\" + fileName);
        //        }

        //        byte[] bs;
        //        System.IO.FileStream fs = new FileStream(@DocumentFileStoredFolder + "\\" + fileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
        //        bs = (byte[])dt.Rows[0]["file_image"];
        //        System.IO.BinaryWriter bw;
        //        bw = new System.IO.BinaryWriter(fs);
        //        bw.Write(bs);
        //        bw.Close();
        //        fs.Close();

        //        message = "success";

        //        //Process.Start(@DocumentFileStoredFolder + "\\" + fileName);
        //    }
        //    else
        //    {
        //        message = "fail";
        //    }


        //    if (message.Equals("success"))
        //    {
        //        return dt;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}




    }
}
