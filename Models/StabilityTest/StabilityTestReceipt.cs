using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.StabilityTest
{
    public class StabilityTestReceipt
    {
		public class SrchParam
		{
			public string start_date { get; set; }
			public string end_date { get; set; }
			public string stability_test_type { get; set; }
			public string s_item_cd { get; set; }
			public string date_type { get; set; }
			public string stability_test_type2 { get; set; }

			public SrchParam()
			{
				this.start_date = "";
				this.end_date = "";
				this.stability_test_type = "";
				this.s_item_cd = "";
				this.date_type = "";
				this.stability_test_type2 = "";
			}
		}

		public SrchParam srch;

		public string gubun { get; set; }
		public string stability_test_id { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string order_no { get; set; }
		public string start_date { get; set; }
		public string start_qty { get; set; }
		public string valid_month { get; set; }
		public string end_date { get; set; }
		public string stability_qty { get; set; }
		public string stability_unit_qty { get; set; }
		public string testcontrol_id { get; set; }
		public string stability_test_type { get; set; }
		public string stability_test_type_nm { get; set; }
		public string stability_test_type2 { get; set; }
		public string stability_test_type2_nm { get; set; }
		public string comment { get; set; }
		public string packing_type { get; set; }
		public string stability_test_status { get; set; }
		public string stability_test_status_nm { get; set; }
		public string create_emp_cd { get; set; }
		public string create_emp_nm { get; set; }
		public string create_date { get; set; }
		public string keeping_condition { get; set; }
		public string stability_test_remark { get; set; }
		public string stability_test_purpose { get; set; }
		public string item_unit { get; set; }
		public string doc_no { get; set; }
		public string depositsample_id { get; set; }
		public string test_standard { get; set; }
		public string stability_unit_cd { get; set; }
		public string stability_unit_nm { get; set; }

	}
}