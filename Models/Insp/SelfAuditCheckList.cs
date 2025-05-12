using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.Insp
{
    public class SelfAuditCheckList
    {
        public string list_no { get; set; }
        public string list_gubun { get; set; }
        public string list_range { get; set; }
        public string list_target { get; set; }
        public string list_seq { get; set; }
        public string list_content { get; set; }
        public string list_wrtier_cd { get; set; }
        public string list_wrtier_nm { get; set; }

        public SelfAuditCheckList()
        {

        }

    }
}
