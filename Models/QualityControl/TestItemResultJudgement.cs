using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.QualityControl
{
	public class TestItemResultJudgement
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string Gubun { get; set; }
			/// <summary> 
			/// 일자구분:의로일자/요청일자/지시일자
			/// </summary> 
			public string re_date_option { get; set; }
			/// <summary> 
			/// 시험종류
			/// </summary> 
			public string le_s_test_type { get; set; }
			/// <summary>
			/// 구분 : 의뢰품목/의뢰번호/제조(관리)번호
			/// </summary>
			public string ce_gubun_number { get; set; }
			/// <summary>
			/// 구분text
			/// </summary>
			public string te_number { get; set; }
			/// <summary>
			/// 시작일
			/// </summary>
			public string de_start_date { get; set; }
			/// <summary>
			/// 종료일
			/// </summary>
			public string de_end_date { get; set; }
			/// <summary>
			/// 진행상태
			/// </summary>
			public string re_test_status { get; set; }
			/// <summary>
			/// 제형
			/// </summary>
			public string le_item_form_cd { get; set; }

			public string testcontrol_id { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				DateTime dt = DateTime.Now;

				this.Gubun = "S";
				this.re_date_option = "A";
				this.le_s_test_type = "%";
				this.ce_gubun_number = "0";
				this.te_number = "";
				this.de_start_date = dt.AddMonths(-1).ToString("yyyy-MM-dd");
				this.de_end_date = dt.ToString("yyyy-MM-dd");
				this.re_test_status = "B";
				this.le_item_form_cd = "%";			
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public int testcontrol_id { get; set; }
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
		public string request_date { get; set; }
		public string receive_date { get; set; }
		public string instruction_date { get; set; }
		public string result_plan_date { get; set; }
		public string test_result_yn { get; set; }
		public string test_status { get; set; }
		public string test_status_nm { get; set; }
		public decimal? test_result_value { get; set; }
		public string packing_qty { get; set; }
		public string emergency_test_yn { get; set; }
		public string linked_request_no { get; set; }
		public string item_form { get; set; }
		public string test_standard_1 { get; set; }
		public string test_standard_2 { get; set; }
		public string test_standard_3 { get; set; }
		public string test_standard_4 { get; set; }
		public string test_standard_5 { get; set; }
		public string test_standard_nm_1 { get; set; }
		public string request_remark { get; set; }
		public string sign_time { get; set; }
		public string start_date { get; set; }
		public string picking_date { get; set; }
		public string hb_ck { get; set; }
		public int? pack_cnt { get; set; }
		public string item_form_cd { get; set; }

		// 4. default 생성자 
		public TestItemResultJudgement()
		{
		}

		public class TestItemResultJudgementDetailArrange
		{
			/// <summary> 
			/// 1. 화면 검색 Parameter CLass 
			/// </summary> 
			public class SrchParam
			{
				///// <summary> 
				///// SP 검색구분 
				///// </summary> 
				//public string Gubun { get; set; }
				///// <summary> 
				///// Param1 
				///// </summary> 
				//public string Param1 { get; set; }
				///// <summary> 
				///// Param2 
				///// </summary> 
				//public string Param2 { get; set; }

				//// default 검색 생성자 
				//public SrchParam()
				//{
				//	this.Gubun = "S";
				//	this.Param1 = "S";
				//	this.Param2 = "S";
				//}
			}

			// 2. 검색파라미터 property
			public SrchParam srch;

			// 3. 조회결과SET 
			/// <summary> 
			/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
			/// </summary> 
			public string row_status { get; set; }
			public int testcontrol_id { get; set; }
			public int testmaster_id { get; set; }
			public int teststandardmaster_id { get; set; }
			public string item_cd { get; set; }
			public string item_nm { get; set; }
			public string item_enm { get; set; }
			public string item_packunit { get; set; }
			public string start_no { get; set; }
			public string test_no { get; set; }
			public string start_qty { get; set; }
			public string gubun_nm { get; set; }
			public string test_type { get; set; }
			public string process_cd { get; set; }
			public string process_nm { get; set; }
			public string test_standard { get; set; }
			public string test_standard_nm { get; set; }
			public string test_judge_no { get; set; }
			public string content_check { get; set; }
			public string testitem_nm { get; set; }
			public string teststandard_nm { get; set; }
			public string testitem_result { get; set; }
			public string testitem_result_value { get; set; }
			public string testresult_data_type_nm { get; set; }
			public string testitem_start_time { get; set; }
			public string testitem_end_time { get; set; }
			public string select_yn { get; set; }
			public string testitem_status { get; set; }
			public decimal? teststandard_min { get; set; }
			public decimal? teststandard_max { get; set; }
			public string teststandard_type { get; set; }
			public string testitem_result_yn { get; set; }
			public string testitem_result_yn_nm { get; set; }
			public string testitem_time { get; set; }
			public string emp_cd { get; set; }
			public string emp_nm { get; set; }
			public string testitem_result_remark { get; set; }
			public string code_type { get; set; }
			public string request_date { get; set; }
			public string test_status { get; set; }
			public int? qc_inst_data_id { get; set; }
			public string tester_nm { get; set; }
			public string interface_cd { get; set; }
			public string backdata_yn { get; set; }
			public string backdata_button { get; set; }
			public string testmethod_yn { get; set; }
			public string testmethod_yn_dis { get; set; }
			public string worksheet_yn { get; set; }
			public int? worksheet_id { get; set; }
			public string start_date { get; set; }
			public decimal? testitem_inputtime { get; set; }
			public decimal? testitem_totaltime { get; set; }
			public int? teststandard_validpoint { get; set; }
			public char? retest_check { get; set; }
			public string data_check_yn { get; set; }
			public string reference_test_yn { get; set; }
			public string instruction_date { get; set; }
			public string picking_date { get; set; }
			public string testitem_type { get; set; }
			public string test_standard_cd { get; set; }

			// 4. default 생성자 
			public TestItemResultJudgementDetailArrange()
			{
			}
			
		}

		
	}


}