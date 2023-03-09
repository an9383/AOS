using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.Doc
{
    public class DocRevision
    {
        public string doc_no { get; set; }
        public string revision_no { get; set; }  
        public string revision_date { get; set; }
        public string revise_emp_cd { get; set; }
        public string revise_emp_nm { get; set; }
        public string revision_content { get; set; }
        public string revision_reason { get; set; }
        public string revision_ground { get; set; }
        public string current_yn { get; set; }
        public string current_yn_nm { get; set; }
        public string main_file_id { get; set; }
        public string main_file_nm { get; set; }
        public string reference_file_id1 { get; set; }
        public string reference_file_nm1 { get; set; }
        public string reference_file_id2 { get; set; }
        public string reference_file_nm2 { get; set; }
        public string reference_file_id3 { get; set; }
        public string reference_file_nm3 { get; set; }
        public string reference_file_id4 { get; set; }
        public string reference_file_nm4 { get; set; }
        public string approval_end_yn { get; set; }
        public string operation_date { get; set; }
        public string void_date { get; set; }
        public string use_select { get; set; }
        public string docRecord_elecsignuse_yn { get; set; }
        public string revision_dept_cd { get; set; }
        public string revision_dept_nm { get; set; }

        //파일 관련
        public string doc_file_id { get; set; }
        public string gubun { get; set; }
        public string fgubun { get; set; }

        public DocRevision()
        {

        }

        public DocRevision(DataRow row)
        {
            this.doc_no = row["doc_no"].ToString();
            this.revision_no = row["revision_no"].ToString();
            this.revision_date = row["revision_date"].ToString();
            this.revise_emp_cd = row["revise_emp_cd"].ToString();
            this.revise_emp_nm = row["revise_emp_nm"].ToString();
            this.revision_content = row["revision_content"].ToString();
            this.revision_reason = row["revision_reason"].ToString();
            this.current_yn = row["current_yn"].ToString();
            this.current_yn_nm = row["current_yn_nm"].ToString();
            this.main_file_id = row["main_file_id"].ToString();
            this.main_file_nm = row["main_file_nm"].ToString();
            this.reference_file_id1 = row["reference_file_id1"].ToString();
            this.reference_file_nm1 = row["reference_file_nm1"].ToString();
            this.reference_file_id2 = row["reference_file_id2"].ToString();
            this.reference_file_nm2 = row["reference_file_nm2"].ToString();
            this.reference_file_id3 = row["reference_file_id3"].ToString();
            this.reference_file_nm3 = row["reference_file_nm3"].ToString();
            this.reference_file_id4 = row["reference_file_id4"].ToString();
            this.reference_file_nm4 = row["reference_file_nm4"].ToString();
            this.approval_end_yn = row["approval_end_yn"].ToString();
            this.operation_date = row["operation_date"].ToString();
            this.void_date = row["void_date"].ToString();
            this.use_select = row["use_select"].ToString();
            this.revision_dept_cd = row["revision_dept_cd"].ToString();
            this.revision_dept_nm = row["revision_dept_nm"].ToString();
        }
    }

}