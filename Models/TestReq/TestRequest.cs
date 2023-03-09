using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.TestReq
{
	public class TestRequest
	{
		public class SrchParam
		{
			public string selectdate { get; set; }
			public string de_Sdate { get; set; }
			public string de_Edate { get; set; }
			public string test_type { get; set; }
			public string test_status { get; set; }
			public string search_gubun { get; set; }
			public string search_number { get; set; }
			public string item_form_cd { get; set; }

			public SrchParam()
			{
				this.selectdate = "Y";
				this.de_Sdate = DateTime.Now.AddMonths(-1).ToShortDateString();
				this.de_Edate = DateTime.Now.ToShortDateString();
				this.test_type = "%";
				this.test_status = "%";
				this.search_gubun = "0";
				this.search_number = "";
				this.item_form_cd = "%";
			}
		}

		public SrchParam srch;

		public string gubun { get; set; }
		public string outer_interface { get; set; }
		public string testcontrol_id { get; set; }
		public string testrequest_no { get; set; }
		public string test_type_nm { get; set; }
		public string test_type { get; set; }
		public string item_nm { get; set; }
		public string item_enm { get; set; }
		public string item_cd { get; set; }
		public string test_standard { get; set; }
		public string test_standard_nm { get; set; }
		public string process_nm { get; set; }
		public string process_cd { get; set; }
		public string request_date { get; set; }
		public string request_emp_cd1_nm { get; set; }
		public string request_emp_cd1 { get; set; }
		public decimal? start_qty { get; set; }
		public string result_hope_date { get; set; }
		public string pack_cnt { get; set; }
		public string start_qty_unit { get; set; }
		public string start_date { get; set; }
		public string start_no { get; set; }
		public string start_seq { get; set; }
		public string enter_no { get; set; }
		public string enter_seq { get; set; }
		public string material_maker_nm { get; set; }
		public string material_maker_cd { get; set; }
		public string material_lot_no { get; set; }
		public string test_no { get; set; }
		public string result_plan_date { get; set; }
		public string test_status { get; set; }
		public string test_result_yn { get; set; }
		public string test_status_nm { get; set; }
		public string cust_nm { get; set; }
		public string cust_cd { get; set; }
		public string request_remark { get; set; }
		public string request_purpose_nm { get; set; }
		public string request_purpose { get; set; }
		public string enter_date { get; set; }
		public string order_qty { get; set; }
		public string pack_type { get; set; }
		public string test_standard_1 { get; set; }
		public string test_standard_2 { get; set; }
		public string test_standard_3 { get; set; }
		public string test_standard_4 { get; set; }
		public string test_standard_5 { get; set; }
		public string test_standard_nm_1 { get; set; }
		public string valid_period { get; set; }
		public string item_form_cd { get; set; }
		public string item_form_nm { get; set; }
		public string hb_ck { get; set; }
		public string gmo_material_yn { get; set; }
		public string gmo_yn { get; set; }

	}
}