using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.ProductionMaster
{
    public class RevisionData
    {
        public string gubun { get; set; }
        public string item_cd { get; set; }
        public string t_item_cd { get; set; }
        public string s_dept_cd { get; set; }
        public string revision_no { get; set; }
        public string new_revision_no { get; set; }
        public string batch_size { get; set; }
        public string batch_size_unit { get; set; }
        public string revision_num { get; set; }
        public string revision_use_date { get; set; }
        public string revision_remark { get; set; }
        public string mbr_type { get; set; }
        public string export_words { get; set; }
        public string batch_weight { get; set; }
        public string batch_size_sub { get; set; }
        public string permit_standard_qty { get; set; }
        public string permit_standard_qty_unit { get; set; }
        public string revision_special_mention { get; set; }
        public string t_making_dept_cd { get; set; }
        public string t_making_dept_cd2 { get; set; }
        public string startingkey { get; set; }
        public string process_cd { get; set; }
        public string process_seq { get; set; }
        public string standard_process_cd { get; set; }
        public string type { get; set; }
        public string item_proc_seq { get; set; }
        public string item_proc_qc_ck { get; set; }
        public string test_type { get; set; }
        public string test_standby_yn { get; set; }
        public string item_proc_transfer_ck { get; set; }
        public string item_proc_worker1 { get; set; }
        public string item_proc_worker2 { get; set; }
        public string item_proc_inspector { get; set; }
        public string item_proc_warehouse { get; set; }
        public string remark { get; set; }
        public bool ccpChanged { get; set; }
        public string ccp_cd { get; set; }
        public string item_proc_content { get; set; }
        public string item_proc_standard_rate { get; set; }
        public string item_proc_manage_rate { get; set; }
        public string standard_proc_time { get; set; }
        public string workroom_cd { get; set; }
        public string item_proc_manage_rate_max { get; set; }
        public string item_proc_manage_rate_mIN { get; set; }
        public string next_process_cd { get; set; }
        public string item_proc_standard_weight_qty { get; set; }
        public string item_proc_standard_weight_unit { get; set; }
        public string grouping_cd { get; set; }
    }
}
