using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysSet
{
    public class FormManage
    {
		[Required]
		public string form_cd { get; set; }
		[Required]
		public string form_nm { get; set; }
		public string form_seq { get; set; }
		public string form_web_ck { get; set; }
		public string form_url { get; set; }
		public string form_dll { get; set; }
		public string form_security { get; set; }
		public string source_cd { get; set; }
		public string source_nm { get; set; }
		public string form_doc { get; set; }
		public string form_remark { get; set; }
		public string form_main_ck { get; set; }
        public string form_interface { get; set; }
        public string form_seq_yn { get; set; }
        public string form_seq_prefix { get; set; }
        public string form_seq_type { get; set; }
        public string form_seq_size { get; set; }

        public FormManage()
		{

		}

	}
}
