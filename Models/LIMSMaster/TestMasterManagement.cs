using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.LIMSMaster
{
	public class TestMasterManagement
	{
		public class SrchParam
		{
			public string gubun { get; set; }
			public string test_type { get; set; }
			public string item_cd { get; set; } 
			public string necessary_check { get; set; }
			public string item_gb { get; set; }
			public string revision_no { get; set; }
			public string test_standard { get; set; }
			public string process_cd { get; set; }
			public string testmaster_id { get; set; }
			
			// default 검색 생성자 
			public SrchParam()
			{
				this.gubun = "";
				this.test_type = "";
				this.item_cd = "";
				this.necessary_check = "A";
				this.item_gb = "%";
				this.revision_no = "";
				this.test_standard = "";
				this.process_cd = "";
				this.testmaster_id = "";
			}
		}

		public class SignParam
		{
			public string gubun { get; set; }
			public string emp_cd { get; set; }
			public string sign_type { get; set; }
			public string index_key { get; set; }
			public string sign_set_cd { get; set; }
			public string sign_set_dt_id { get; set; }

			// default 검색 생성자 
			public SignParam()
			{
				this.gubun = "";
				this.emp_cd = "";
				this.sign_type = "";
				this.index_key = "";
				this.sign_set_cd = "1410";
				this.sign_set_dt_id = "";
			}
		}

		public class TestCriteria
		{
			public string gubun { get; set; }
			public string testmaster_id { get; set; }
			public string teststandardmaster_id { get; set; }
			public string teststandard_nm { get; set; }
			public string teststandard_enm { get; set; }
			public string teststandard_type { get; set; }
			public string teststandard_min { get; set; }
			public string teststandard_max { get; set; }
			public string testresult_data_type { get; set; }
			public string testitem_trier { get; set; }
			public string testitem_totaltime { get; set; }
			public string testitem_inputtime { get; set; }
			public string testitem_remark { get; set; }
			public string testresult_example { get; set; }
			public string teststandard_validpoint { get; set; }
			public string sample_qty { get; set; }
			public string teststandard_nm_self { get; set; }
			public string teststandard_min_self { get; set; }
			public string teststandard_max_self { get; set; }
			public string contents_yn { get; set; }
			public string dual_data_yn { get; set; }
			public string tester_cd { get; set; }
			public string tester_nm { get; set; }
			public string statutory_standard { get; set; }
			public string stability_test_yn { get; set; }
			public string fix_content_yn { get; set; }
			public string fix_content_rate { get; set; }
			public string testitem_cd { get; set; }
			public string testitem_nm { get; set; }
			public string test_type { get; set; }
			public string item_cd { get; set; }
			public string test_standard { get; set; }
			public string process_cd { get; set; }
			public string revision_no { get; set; }
			public string addtype { get; set; }

			public TestCriteria()
			{
				this.contents_yn = "N";
				this.dual_data_yn = "N";
				this.stability_test_yn = "N";
				this.fix_content_yn = "N";
			}
		}

		public class CommonSpecification
		{
			public string gubun { get; set; }
			public string test_type { get; set; }
			public string item_cd { get; set; }
			public string test_standard { get; set; }
			public string process_cd { get; set; }
			public string testmaster_id { get; set; }
			public string common_standard_id { get; set; }
		}

		public class SpecificationCopyParam
		{
			public string test_type { get; set; }
			public string item_cd { get; set; }
			public string test_standard { get; set; }
			public string process_cd { get; set; }
			public string revision_no { get; set; }
			public string copy_test_type { get; set; }
			public string copy_item_cd { get; set; }
			public string copy_test_standard { get; set; }
			public string copy_process_cd { get; set; }
			public string copy_testmaster_id { get; set; }
		}

		public SrchParam srch;
		public SignParam sign;
		public TestCriteria testCriteria;

		public string gubun { get; set; }
		public string row_status { get; set; }
		public string registration { get; set; }
		public string test_type { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string display_nm_item { get; set; }
		public string test_standard { get; set; }
		public string process_cd { get; set; }
		public string test_standard_nm { get; set; }
		public string process_nm { get; set; }
		public string current_revision_no { get; set; }
		public string testmaster_id { get; set; }
		public string item_keep_condition { get; set; }
		public string revision_no { get; set; }
		public string test_standard_1 { get; set; }
		public string test_standard_2 { get; set; }
		public string test_standard_3 { get; set; }
		public string test_standard_4 { get; set; }
		public string test_standard_5 { get; set; }
		public string testmaster_remark { get; set; }
		public string testmaster_comment { get; set; }
		public string current_revision_yn { get; set; }
		public string revision_date { get; set; }
		public string revision_doc_no { get; set; }
		public string sample_qty { get; set; }
		public string sample_unit { get; set; }
		public string sample_test_qty { get; set; }
		public string sample_microbe_qty { get; set; }
		public string sample_deposit_qty { get; set; }
		public string avg_sampling_qty { get; set; }
		public string sample_deposit_place_cd { get; set; }
		public string stability_qty { get; set; }
		public string stability_place_cd { get; set; }
		public string test_term { get; set; }
		public string test_emp_cd { get; set; }
		public string teststandardmaster_id { get; set; }
		public string testitem_cd { get; set; }
		public string addtype { get; set; }

		public TestMasterManagement()
		{
		}
	}
}