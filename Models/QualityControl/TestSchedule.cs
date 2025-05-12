using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.QualityControl
{
	public class TestSchedule
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
			public string rg_date_option { get; set; }
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
			/// <summary>
			/// 담당자
			/// </summary>
            public string be_s_emp_cd { get; set; }
			/// <summary>
			/// testcontrol_id (form hidden필드)
			/// </summary>
            public string le_testcontrol_id { get; set; }

            // default 검색 생성자 
            public SrchParam()
			{
				DateTime dt = DateTime.Now;

				this.Gubun = "S";
				this.rg_date_option = "request";
				this.le_s_test_type = "%";
				this.ce_gubun_number = "0";
				this.te_number = "";
				this.de_start_date = dt.AddMonths(-1).ToString("yyyy-MM-dd");
				this.de_end_date = dt.ToString("yyyy-MM-dd");
				this.re_test_status = "B";
				this.le_item_form_cd = "%";
				this.be_s_emp_cd = "";
			}
		}

		// testItemmaster 검색 파라미터
		public class SrchParamItem
		{
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string Gubun { get; set; }
			/// <summary>
			/// 검색타입
			/// </summary>
			public string le_s_testitem_type { get; set; }
			/// <summary>
			/// 검색어
			/// /// </summary>
			public string te_s_testitem { get; set; }

			public SrchParamItem()
			{			
				this.Gubun = "S4";
				this.le_s_testitem_type = "%";
				this.te_s_testitem = "";
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
		public string result_hope_date { get; set; }
		public string instruction_date { get; set; }
		public string test_result_yn { get; set; }
		public int? test_priority { get; set; }
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
		public int? pack_cnt { get; set; }

		// 4. default 생성자 
		public TestSchedule()
		{
		}

		/// <summary>
		/// 시험지시
		/// </summary>
		public class TestScheduleDetailArrange
        {
			public string row_status { get; set; }
			public int testcontrol_id { get; set; }
			public string test_status { get; set; }
			public int testmaster_id { get; set; }
			public int teststandardmaster_id { get; set; }
			public string start_no { get; set; }
			public string test_no { get; set; }
			public string test_type { get; set; }
			public string test_type_nm { get; set; }
			public string item_cd { get; set; }
			public string item_nm { get; set; }
			public string item_enm { get; set; }
			public string test_result_yn { get; set; }
			public string test_standard { get; set; }
			public string test_standard_nm { get; set; }
			public string process_cd { get; set; }
			public string process_nm { get; set; }
			public string result_hope_date { get; set; }
			public string result_plan_date { get; set; }
			public string calculate_date { get; set; }
			public string testitem_nm { get; set; }
			public string teststandard_nm { get; set; }
			public string testitem_totaltime { get; set; }
			public string testitem_inputtime { get; set; }
			public string testitem_status_nm { get; set; }
			public string testitem_trier { get; set; }
			public string testitem_trier_nm { get; set; }
			public string trier_ck { get; set; }
			public string testitem_schedule_time { get; set; }
			public string code_type { get; set; }
			public string picking_ordered_emp_cd { get; set; }
			public string picking_ordered_emp_nm { get; set; }
			public string instruction_date { get; set; }
			public string instruction_no { get; set; }
			public string teststandard_type { get; set; }
			public string teststandard_type_nm { get; set; }
			public decimal teststandard_min { get; set; }
			public decimal teststandard_max { get; set; }
			public string testresult_data_type { get; set; }
			public string testresult_data_type_nm { get; set; }
			public string teststandard_validpoint { get; set; }
			public string top_level_testitem_cd { get; set; }
			public int? level { get; set; }
			public int? parent_id { get; set; }
			public string contents_yn { get; set; }
			public string test_standard_nm_1 { get; set; }
			public string reference_test_yn { get; set; }
			public string test_standard_g { get; set; }
		}

	}


}