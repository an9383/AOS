using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Change
{
    public class ChangeControlObject
    {
        public string gubun { get; set; }
        public string changecontrol_cd { get; set; }
        public string changecontrol_sop_dept_id { get; set; }
        public string dept_cd { get; set; }
        public string dept_nm { get; set; }
        public string review_contents { get; set; }
        public string emp_cd { get; set; }
        public string emp_nm { get; set; }
        public string review_dept_gubun_yn { get; set; }
        public string changecontrol_sop_doc_contents { get; set; }
    }
}