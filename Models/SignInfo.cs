using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models
{
    public class SignInfo
    {
        public string sign_set_cd { get; set; }
        public string index_key { get; set; }
        public string sign_history_id { get; set; }
        public string sign_emp_cd { get; set; }
        public string sign_type { get; set; }
        public string sign_representative_yn { get; set; }

        public SignInfo()
        {

        }
    }
}