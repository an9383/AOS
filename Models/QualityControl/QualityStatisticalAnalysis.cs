using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.QualityControl
{
	public class QualityStatisticalAnalysis
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string Gubun { get; set; }
			/// <summary> 
			/// 시험종류
			/// </summary> 
			public string le_s_test_type { get; set; }
			/// <summary>
			/// 품목
			/// </summary>
			public string be_s_item_cd { get; set; }
			/// <summary>
			/// 제조원코드
			/// </summary>
			public string be_receipt_material_maker_cd { get; set; }
			/// <summary>
			/// 제조원코드명
			/// </summary>
			public string te_receipt_lot_cust_nm { get; set; }
			/// <summary>
			/// 시작일
			/// </summary>
			public string de_start_date { get; set; }
			/// <summary>
			/// 종료일
			/// </summary>				
			public string de_end_date { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				DateTime dt = DateTime.Now;

				this.Gubun = "S1";
				this.le_s_test_type = "";
				this.be_s_item_cd = "";
				this.be_receipt_material_maker_cd = "";
				this.te_receipt_lot_cust_nm = "";
				this.de_start_date = dt.AddMonths(-1).ToString("yyyy-01-01");
				this.de_end_date = dt.ToString("yyyy-12-31");

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
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string test_standard_nm { get; set; }
		public string process_cd { get; set; }
		public string process_nm { get; set; }
		public string material_maker_cd { get; set; }
		public string material_maker_nm { get; set; }

		// 4. default 생성자 
		public QualityStatisticalAnalysis()
		{
		}

		// 시험항목 조회 (gubun="S2")
		public class TestItem
		{
			// 3. 조회결과SET 
			/// <summary> 
			/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
			/// </summary> 
			public string row_status { get; set; }
			public string test_type { get; set; }
			public string item_cd { get; set; }
			public string process_cd { get; set; }
			public string testitem_cd { get; set; }
			public string testitem_nm { get; set; }

			// 4. default 생성자 
			public TestItem()
			{
			}
		}

		// 시험데이터 조회 (gubun="S3")
		public class TestData
		{
			// 3. 조회결과SET 
			/// <summary> 
			/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
			/// </summary> 
			public string row_status { get; set; }
			public string order_no { get; set; }
			public string start_date { get; set; }
			public string testitem_result { get; set; }
			public string teststandard_min { get; set; }
			public string teststandard_max { get; set; }
			public string testitem_result_min { get; set; }
			public string testitem_result_max { get; set; }
			public string testitem_result_avg { get; set; }
			public string testitem_result_rsd { get; set; }

			// 4. default 생성자 
			public TestData()
			{
			}
			
		}

		// 시험항목 상세조회 (gubun="S4")
		public class TestItemDetail
		{
			// 3. 조회결과SET 
			/// <summary> 
			/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
			/// </summary> 
			public string row_status { get; set; }
			public string order_no { get; set; }
			public string process_nm { get; set; }
			public string start_date { get; set; }
			public string testitem_result1 { get; set; }
			public string testitem_result2 { get; set; }
			public string testitem_result3 { get; set; }
			public string testitem_result4 { get; set; }
			public string testitem_result5 { get; set; }
			public string testitem_result6 { get; set; }
			public string testitem_result7 { get; set; }
			public string testitem_result8 { get; set; }
			public string testitem_result9 { get; set; }
			public string testitem_result10 { get; set; }

			// 4. default 생성자 
			public TestItemDetail()
			{
			}
						
		}

	}
}