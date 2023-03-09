using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.ProductionMaster
{
    public class ItemWorkGuide
    {
		//작업방법
		public string pGubun { get; set; }
		public string item_cd { get; set; }
		public string revision_no { get; set; }
		public string batch_size { get; set; }
		public string process_cd { get; set; }
		public string detailproc_id { get; set; }
		public string workguide_id { get; set; }
		public string workguide_seq { get; set; }
		public string detailproc_cd { get; set; }
		public string workguide_method { get; set; }
		public string workguide_data_type { get; set; }
		public string workguide_min_manage { get; set; }
		public string workguide_max_manage { get; set; }
		public string workguide_min_permit { get; set; }
		public string workguide_max_permit { get; set; }
		public string equip_cd { get; set; }
		public string workguide_sub_item { get; set; }
		public string equip_parameter { get; set; }
		public string workguide_es_yn { get; set; }
		public string workguide_unnecessary_yn { get; set; }
		public string decimal_cnt { get; set; }
		public string proc_rep_yn { get; set; }
		public string check_proc { get; set; }

	}
}