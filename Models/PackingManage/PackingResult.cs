using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PackingManage
{
	public class PackingResult
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{

			public string s_item_cd { get; set; }
			public string s_sdate { get; set; }
			public string s_edate { get; set; }
			public string s_lot_no { get; set; }
			public string s_order_status { get; set; }
			public string packing_order_no { get; set; }
			public string showPriceYn { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.s_item_cd = "";
				this.s_sdate = DateTime.Now.AddMonths(-1).ToShortDateString();
				this.s_edate = DateTime.Now.ToShortDateString();
				this.s_lot_no = "";
				this.s_order_status = "%";
				this.packing_order_no = "";
				this.showPriceYn = "false";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string packing_order_work_date { get; set; }
		public string sale_item_cd { get; set; }
		public string sale_item_nm { get; set; }
		public string item_pack_size { get; set; }
		public string lot_no { get; set; }
		public string packing_order_qty { get; set; }
		public string packing_qty { get; set; }
		public string packing_order_status { get; set; }
		public string packing_order_no { get; set; }
		public string VALID_DATE { get; set; }
		public string OUTER_INTERFACE { get; set; }
		public string ord { get; set; }
		public string new_ck { get; set; }
		public string order_no { get; set; }
		public string lot_end { get; set; }
		public string packing_result_id { get; set; }
		public string packing_date { get; set; }
		public string test_sample_qty { get; set; }
		public string deposit_sample_qty { get; set; }
		public string receive_qty { get; set; }
		public string bulk_use_qty { get; set; }
		public string remain_qty { get; set; }
		public string disuse_qty { get; set; }
		public string result_emp { get; set; }
		public string lot_date { get; set; }
		public string end_date { get; set; }
		public string item_cd { get; set; }
		public string insert_user_cd { get; set; }
		public string update_user_cd { get; set; }
		public string log_user_id { get; set; }
		public string item_stock_id { get; set; }
		public string test_type { get; set; }
		public string start_date { get; set; }
		public string start_no { get; set; }
		public string receive_date { get; set; }
		public string picking_date { get; set; }
		public string picking_emp_cd { get; set; }
		public string picking_method { get; set; }
		public string write_gb { get; set; }
		public string item_form_cd { get; set; }
		public string request_no { get; set; }
		public string request_time1 { get; set; }
		public string result_emp_cd { get; set; }
		public string result_emp_nm { get; set; }

		// 4. default 생성자 
		public PackingResult()
		{
		}

	}


}