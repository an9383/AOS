using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysCom
{
    public class Process
    {
        public string process_cd { get; set; }
        public string process_nm { get; set; }
        public string process_qc_ck { get; set; }
        public string process_transfer_ck { get; set; }
        public string process_worker1 { get; set; }
        public string process_worker2 { get; set; }
        public string process_inspector { get; set; }
        public string process_personincharge { get; set; }
        public string workroom_cd { get; set; }
        public string process_type { get; set; }
        public string process_rate_type { get; set; }
        public string process_remark { get; set; }
        public string process_keep_method { get; set; }
        public string valid_period { get; set; }
        public string item_form_cd { get; set; }
        public string ccp_cd { get; set; }

        public Process()
        {

        }
    }
}
