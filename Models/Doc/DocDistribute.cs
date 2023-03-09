using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.Doc
{
    //배포관리 
    public class DocDistribute
    {
        public string gubun { get; set; }
        public string doc_no { get; set; } 
        public string revision_no { get; set; }   
        public string dist_id { get; set; }
        public string dist_type { get; set; }
        public string dist_dept_cd { get; set; }
        public string dist_dept_nm { get; set; }
        public string dist_date { get; set; }
        public string dist_qty { get; set; }
        public string over_emp_cd { get; set; }
        public string over_emp_nm { get; set; }
        public string accept_emp_cd { get; set; }
        public string accept_emp_nm { get; set; }
        public string dist_status { get; set; }

    }

    //폐기관리
    public class DocDisuse
    {
        public string gubun { get; set; }
        public string doc_no { get; set; }
        public string revision_no { get; set; }
        public string dist_id { get; set; }
        public string disuse_id { get; set; }
        public string disuse_date { get; set; }
        public string disuse_qty { get; set; }
        public string disuse_emp_cd { get; set; }
        public string disuse_emp_nm { get; set; }
        public string disuse_reason { get; set; }

    }
}