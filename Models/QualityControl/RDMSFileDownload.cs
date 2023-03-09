using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.QualityControl
{
	public class RDMSFileDownload
	{
		public class SrchParam
		{
			public string date_option { get; set; }
			public string start_date { get; set; }
			public string end_date { get; set; }
			public string test_type { get; set; }
			public string test_status { get; set; }
			public string search_number { get; set; }
			public string search_number0 { get; set; }
			public string test_emp_cd { get; set; }
			public string item_form_cd { get; set; }
			public string search_gubun { get; set; }
		}

		public SrchParam srch;

		public string testcontrol_id { get; set; }
		public string start_no { get; set; }
		public string test_no { get; set; }
		public string test_type { get; set; }
		public string test_type_nm { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string item_enm { get; set; }
		public string test_standard { get; set; }
		public string test_standard_nm { get; set; }
		public string process_cd { get; set; }
		public string process_nm { get; set; }
		public string request_emp_cd2 { get; set; }
		public string request_emp_nm2 { get; set; }
		public string result_hope_date { get; set; }
		public string instruction_date { get; set; }
		public string test_result_yn { get; set; }
		public string test_priority { get; set; }
		public string schedule_confirm_yn { get; set; }
		public string request_date { get; set; }
		public string result_plan_date { get; set; }
		public string calculate_date { get; set; }
		public string test_emp_cd { get; set; }
		public string test_emp_nm { get; set; }
		public string test_status { get; set; }
		public string test_status_nm { get; set; }
		public string original_result_plan_date { get; set; }
		public string arrow_yn { get; set; }
		public string request_calc_diff { get; set; }
		public string emergency_test_yn { get; set; }
		public string request_remark { get; set; }
		public string testrequest_no { get; set; }
		public string test_standard_1 { get; set; }
		public string test_standard_2 { get; set; }
		public string test_standard_3 { get; set; }
		public string test_standard_4 { get; set; }
		public string test_standard_5 { get; set; }
		public string test_standard_nm_1 { get; set; }
		public string all_test_check { get; set; }
		public string hb_ck { get; set; }
		public string item_form_cd { get; set; }
		public string pack_cnt { get; set; }


	}
}