using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.Insp
{
    public class SelfAudit
    {
        public SelfAudit()
        {

        }


        public string audit_no{ get; set;}
        public string audit_object { get; set; }
        public string audit_start_date { get; set; }
        public string audit_end_date { get; set; }
        public string audit_purpose_d { get; set; }
        public string audit_plan { get; set; }
        public string audit_doc_plan { get; set; }
        public string audit_special { get; set; }
        public string audit_writer_cd { get; set; }
        public string audit_writer_nm { get; set; }
        public string audit_writer_date { get; set; }
        public string audit_director_cd { get; set; }
        public string audit_director_nm { get; set; }
        public string audit_step { get; set; }
        public string audit_step_status { get; set; }
        public string audit_purpose { get; set; }
        public string audit_check_gb { get; set; }       
        public string CRUD_gubun { get; set; }
        public string audit_step_cd { get; set; }
        public string audit_step_status_cd { get; set; }
        public int self_audit_year { get; set; }
    }
}
