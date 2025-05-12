using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.Doc
{
    public class DocRegister
    {

        public int child_cnt { get; set; }  
        public string status { get; set; }
        public string last_ck { get; set; }
        public string ParentID { get; set; }
        public string ID { get; set; }
        public int structure_seq { get; set; }
        public string structure_nm { get; set; }
        public int structure_level { get; set; }
        public int ImageIndex { get; set; }
        public string doc_no { get; set; }
        public string structure_cd { get; set; }
        public string child_no { get; set; }
        public string doc_name { get; set; }
        public string doc_search_keyword { get; set; }
        public string doc_class { get; set; }
        public string doc_type { get; set; }
        public string approval_date { get; set; }
        public string start_date { get; set; }
        public int archive_period { get; set; }
        public string archive_period_unit { get; set; }
        public int exam_cycle { get; set; }
        public string exam_cycle_unit { get; set; }
        public string archive_position_cd { get; set; }
        public string archive_position { get; set; }
        public string binding_no { get; set; }
        public string writer_cd { get; set; }
        public string writer_nm { get; set; }
        public string charge_cd { get; set; }
        public string charge_nm { get; set; }
        public string rule_cd { get; set; }
        public string rule_no { get; set; }
        public string use_yn { get; set; }
        public string use_yn_nm { get; set; }
        public string doc_sign_set_cd { get; set; }
        public string record_sign_set_cd { get; set; }
        public string structure_doc_gubun { get; set; }
        public string secret_doc_ck { get; set; }
        public string checkme { get; set; }
        public string doc_ename { get; set; }
        public string writer_dept_cd { get; set; }
        public string writer_dept_nm { get; set; }
        public string doc_seq { get; set; }


        public DocRegister()
        {

        }

        public DocRegister(DataRow row)
        {
            this.child_cnt = Int32.Parse(row["child_cnt"].ToString());
            this.status = row["status"].ToString();
            this.last_ck = row["last_ck"].ToString();
            this.ParentID = row["ParentID"].ToString();
            this.ID = row["ID"].ToString();
            this.structure_seq = Int32.Parse(row["structure_seq"].ToString());
            this.structure_nm = row["structure_nm"].ToString();
            this.structure_level = Int32.Parse(row["structure_level"].ToString());
            this.ImageIndex = Int32.Parse(row["ImageIndex"].ToString());
            this.doc_no = row["doc_no"].ToString();
            this.structure_cd = row["structure_cd"].ToString();
            this.child_no = row["child_no"].ToString();
            this.doc_name = row["doc_name"].ToString();
            this.doc_search_keyword = row["doc_search_keyword"].ToString();
            this.doc_class = row["doc_class"].ToString();
            this.doc_type = row["doc_type"].ToString();
            this.approval_date = row["approval_date"].ToString();
            this.start_date = row["start_date"].ToString();
            this.archive_period = Int32.Parse(row["archive_period"].ToString());
            this.archive_period_unit = row["archive_period_unit"].ToString();
            this.exam_cycle = Int32.Parse(row["exam_cycle"].ToString());
            this.exam_cycle_unit = row["exam_cycle_unit"].ToString();
            this.archive_position_cd = row["archive_position_cd"].ToString();
            this.archive_position = row["archive_position"].ToString();
            this.binding_no = row["binding_no"].ToString();
            this.writer_cd = row["writer_cd"].ToString();
            this.writer_nm = row["writer_nm"].ToString();
            this.charge_cd = row["charge_cd"].ToString();
            this.charge_nm = row["charge_nm"].ToString();
            this.rule_cd = row["rule_cd"].ToString();
            this.rule_no = row["rule_no"].ToString();
            this.use_yn = row["use_yn"].ToString();
            this.use_yn_nm = row["use_yn_nm"].ToString();
            this.doc_sign_set_cd = row["doc_sign_set_cd"].ToString();
            this.record_sign_set_cd = row["record_sign_set_cd"].ToString();
            this.structure_doc_gubun = row["structure_doc_gubun"].ToString();
            this.secret_doc_ck = row["secret_doc_ck"].ToString();
            this.checkme = row["checkme"].ToString();
            this.doc_ename = row["doc_ename"].ToString();
            this.writer_dept_cd = row["writer_dept_cd"].ToString();
            this.writer_dept_nm = row["writer_dept_nm"].ToString();
            this.doc_seq = row["doc_seq"].ToString();

        }
    }

}