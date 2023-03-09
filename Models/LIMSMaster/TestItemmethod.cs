using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.LIMSMaster
{
	public class TestItemmethod
	{
		public class SrchParam
		{
			public string testitem_type { get; set; }
			public string testitem_cd { get; set; }

			public SrchParam()
			{
				this.testitem_type = "%";
				this.testitem_cd = "";
			}
		}

		public SrchParam srch;

		public string gubun { get; set; }
		public string testitem_cd { get; set; }
		public string testmethod_id { get; set; }
		public string testmethod_seq { get; set; }
		public string testmethod_nm { get; set; }
		public string data_type { get; set; }
		public string data_min_manage { get; set; }
		public string data_max_manage { get; set; }
		public string test_parameter_cd { get; set; }
		public string calculate_nm { get; set; }
		public string calculate_formula { get; set; }
		public string tester_cd { get; set; }
		public string reagent_cd { get; set; }
		public string tester_parameter_cd { get; set; }
		public string use_gb { get; set; }
	}
}