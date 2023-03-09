using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.QualityControl
{
	public class TestCheckE
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
			/// 일자구분:의뢰/접수/승인
			/// </summary> 
			public string rg_ReqorRec { get; set; }
			/// <summary> 
			/// 시험종류
			/// </summary> 
			public string le_testitem_nm { get; set; }
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
			public string de_SDate { get; set; }
			/// <summary>
			/// 종료일
			/// </summary>
			public string de_EDate { get; set; }
			/// <summary>
			/// 진행상태
			/// </summary>
			public string rg_status { get; set; }
			/// <summary>
			/// 제형
			/// </summary>
			public string le_item_form_cd { get; set; }
			public string testcontrol_id { get; set; }			

			// default 검색 생성자 
			public SrchParam()
			{
				this.Gubun = "S";
				this.de_SDate = DateTime.Now.AddMonths(-1).ToShortDateString();
				this.de_EDate = DateTime.Now.ToShortDateString();
				this.rg_ReqorRec = "1";
				this.le_testitem_nm = "%";
				this.ce_gubun_number = "0";
				this.te_number = "";
				this.rg_status = "1";
				this.le_item_form_cd = "%";
				this.testcontrol_id = "";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string test_type { get; set; }
		public string test_standard_nm { get; set; }
		public string process_nm { get; set; }
		public string process_cd { get; set; }
		public string test_type_nm { get; set; }
		public string item_nm { get; set; }
		public string item_enm { get; set; }
		public string item_cd { get; set; }
		public string test_no { get; set; }
		public string test_judge_no { get; set; }
		public string test_result_yn { get; set; }
		public string request_date { get; set; }
		public string receive_date { get; set; }
		public string instruction_date { get; set; }
		public string test_date { get; set; }
		public string start_no { get; set; }
		public int testcontrol_id { get; set; }
		public string hb_ck { get; set; }
		public string trade_cd { get; set; }
		public string test_status_nm { get; set; }
		public string test_status_cd { get; set; }
		public string test_inform_remark { get; set; }
		public string test_result_yn_c { get; set; }
		public decimal? test_result_value { get; set; }
		public decimal? test_result_value0 { get; set; }
		public decimal? test_result_pollination { get; set; }
		public decimal? test_result_solvent { get; set; }
		public string valid_period { get; set; }
		public string retest_yn { get; set; }
		public string order_no { get; set; }
		public int? order_proc_id { get; set; }
		public string pack_order_no { get; set; }
		public string test_standard_nm_1 { get; set; }
		public string extend_yn { get; set; }
		public string extend_yn_nm { get; set; }
		public string qc_valid_period { get; set; }
		public string item_type3 { get; set; }
		public string enter_date { get; set; }
		public string testrequest_no { get; set; }

		// 4. default 생성자 
		public TestCheckE()
		{
		}

		public class TestCheckEDetail
		{			
			// 2. 검색파라미터 property
			public SrchParam srch;

			// 3. 조회결과SET 
			/// <summary> 
			/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
			/// </summary> 
			public string row_status { get; set; }
			public string testitem_nm { get; set; }
			public string teststandard_nm { get; set; }
			public string content_check { get; set; }
			public string testitem_result { get; set; }
			public string result_yn { get; set; }
			public string judge_date { get; set; }
			public string testitem_emp_cd { get; set; }
			public string testitem_emp_nm { get; set; }
			public string testitem_result_remark { get; set; }
			public string test_st { get; set; }
			public string t_status { get; set; }
			public int testcontrol_id2 { get; set; }
			public string code_type { get; set; }
			public string testitem_result_yn { get; set; }
			public int teststandardmaster_id { get; set; }
			public string backdata_yn { get; set; }
			public string backdata_button { get; set; }
			public string data_check_yn { get; set; }
			public string retest_check { get; set; }
			public string reference_test_yn { get; set; }

			// 4. default 생성자 
			public TestCheckEDetail()
			{
			}
			
		}
	}
}