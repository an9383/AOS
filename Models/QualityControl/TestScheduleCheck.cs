using HACCP.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.QualityControl
{
	public class TestScheduleCheck
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
			/// 종류
			/// </summary> 
			public string le_s_test_type { get; set; }
			/// <summary>
			/// 시작일
			/// </summary>
			public string de_SDate { get; set; }
			/// <summary>
			/// 종료일
			/// </summary>
			public string de_EDate { get; set; }

			/// <summary>
			/// 시험자
			/// </summary>
			public string le_s_emp_cd { get; set; }

			/// <summary>
			/// 진행상태
			/// </summary>
			public string le_s_test_status { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.Gubun = "S";
				this.de_SDate = DateTime.Now.AddMonths(-1).ToShortDateString();
				this.de_EDate = DateTime.Now.ToShortDateString();
				this.le_s_test_type = "%";
				this.le_s_test_status = "%";

				//if (!"".Equals(Public_Function.User_cd))
				//{
				//	this.le_s_emp_cd = Public_Function.User_cd;
				//}
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string test_type_nm { get; set; }
		public string start_no { get; set; }
		public string test_no { get; set; }
		public string item_nm { get; set; }
		public string test_standard_nm { get; set; }
		public string process_nm { get; set; }
		public string result_plan_date { get; set; }
		public string test_emp_nm { get; set; }
		public string test_status_nm { get; set; }
		public string request_date { get; set; }
		public decimal? testitem_inputtime { get; set; }
		public string test_standard_nm_1 { get; set; }

		// 4. default 생성자 
		public TestScheduleCheck()
		{
		}
	}
}