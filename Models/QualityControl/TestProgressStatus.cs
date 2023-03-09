using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.QualityControl
{
	public class TestProgressStatus
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
			public string le_test_type { get; set; }
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
			public string le_test_status { get; set; }
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
				this.le_test_type = "%";
				this.ce_gubun_number = "0";
				this.te_number = "";
				this.le_test_status = "%";
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
		public string start_no { get; set; }
		public string test_no { get; set; }
		public string test_type_nm { get; set; }
		public string test_type { get; set; }
		public string item_cd { get; set; }
		public string item_code { get; set; }
		public string process_nm_2 { get; set; }
		public string item_enm { get; set; }
		public string test_standard { get; set; }
		public string process_nm { get; set; }
		public string request_date { get; set; }
		public string request_emp_cd1 { get; set; }
		public string result_hope_date { get; set; }
		public string receive_date { get; set; }
		public string result_plan_date { get; set; }
		public int? total_cnt { get; set; }
		public int end_cnt { get; set; }
		public int ing_cnt { get; set; }
		public string test_status { get; set; }
		public string test_status_nm { get; set; }
		public decimal? test_result_value { get; set; }
		public string company_nm { get; set; }
		public string test_result_yn { get; set; }
		public int testcontrol_id { get; set; }
		public string instruction_date { get; set; }
		public string picking_date { get; set; }
		public string test_date { get; set; }
		public string start_qty { get; set; }
		public string picking_qty { get; set; }
		public string start_date { get; set; }
		public string test_emp_cd { get; set; }
		public string test_emp_nm { get; set; }
		public string test_standard_nm_1 { get; set; }
		public string testrequest_no { get; set; }
		public string test_judge_no { get; set; }
		public string test_inform_remark { get; set; }
		public string hb_ck { get; set; }
		public string trade_cd2 { get; set; }

		// 4. default 생성자 
		public TestProgressStatus()
		{
		}
				
	}

}