using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.AdvancedPlanning
{
	public class ForcastingOrderManage
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string start_date { get; set; }
			/// <summary> 
			/// Param1 
			/// </summary> 
			public string end_date { get; set; }
			/// <summary> 
			/// Param2 
			/// </summary> 
			public string order_request_item_cd { get; set; }
			/// <summary> 
			/// Param3 
			/// </summary> 
			public string order_request_item_nm { get; set; }
			// default 검색 생성자 
			public SrchParam()
			{
				this.start_date = DateTime.Now.AddDays(-1).ToShortDateString();
				this.end_date = DateTime.Now.ToShortDateString();
				this.order_request_item_cd = "";
				this.order_request_item_nm = "";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string forcast_order_no { get; set; }
		public string order_request_c_item_cd { get; set; }
		public string order_request_c_item_nm { get; set; }
		public string order_request_h_item_cd { get; set; }
		public string order_request_h_item_nm { get; set; }
		public string request_date { get; set; }
		public string c_qty { get; set; }
		public string h_qty { get; set; }
		public string item_unit { get; set; }
		public string require_date { get; set; }
		public string remark { get; set; }

		// 4. default 생성자 
		public ForcastingOrderManage()
		{
		}

		// 5. DTO 설정
		public ForcastingOrderManage(DataRow row)
		{
			row_status = row["row_status"].ToString();
			forcast_order_no = row["forcast_order_no"].ToString();
			order_request_c_item_cd = row["order_request_c_item_cd"].ToString();
			order_request_c_item_nm = row["order_request_c_item_nm"].ToString();
			order_request_h_item_cd = row["order_request_h_item_cd"].ToString();
			order_request_h_item_nm = row["order_request_h_item_nm"].ToString();
			request_date = row["request_date"].ToString();
			c_qty = row["c_qty"].ToString();
			h_qty = row["h_qty"].ToString();
			item_unit = row["item_unit"].ToString();
			require_date = row["require_date"].ToString();
			remark = row["remark"].ToString();
		}
	}

}