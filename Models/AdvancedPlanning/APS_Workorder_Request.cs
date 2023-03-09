using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.AdvancedPlanning
{
    public class APS_Workorder_Request
    {
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string Sdate { get; set; }
			public string Edate { get; set; }
			public string order_request_h_item_cd { get; set; }
			public bool OrderStatus { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.Sdate = DateTime.Now.AddDays(-1).ToShortDateString();
				this.Edate = DateTime.Now.ToShortDateString();
				this.order_request_h_item_cd = "";
				this.OrderStatus = false;
			}

		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string order_request_no { get; set; }
		public string order_request_c_item_cd { get; set; }
		public string order_request_c_item_nm { get; set; }
		public string order_request_h_item_cd { get; set; }
		public string order_request_h_item_nm { get; set; }
		public string cust_cd { get; set; }
		public string cust_nm { get; set; }
		public string order_request_date { get; set; }
		public string order_request_qty { get; set; }
		public string order_request_h_qty { get; set; }
		public string item_packunit { get; set; }
		public string ck_yn { get; set; }
		public string m_shortage_cnt { get; set; }
		public string p_shortage_cnt { get; set; }
		public string m_bom_no_ck { get; set; }
		public string p_bom_no_ck { get; set; }
		public string sign_status { get; set; }



		//원료, 자재 정보
		public string process_cd { get; set; }
		public int req_order_id { get; set; }
		public string req_order_child_cd { get; set; }
		public string req_order_child_nm { get; set; }
		public string req_order_child_packunit { get; set; }
		public string item_bom_no { get; set; }
		public string item_bom_id { get; set; }
		public string req_order_batch_qty { get; set; }
		public string wait_stock { get; set; }
		public string receipt_reserve_qty { get; set; }
		public string receipt_remain_qty { get; set; }
		public string shortage { get; set; }
		public string shortage2 { get; set; }
		public string default_stock { get; set; }
		public string order_stock { get; set; }
		public string ordering { get; set; }
		public string company { get; set; }
		public string etc { get; set; }
		public string req_order_status_nm { get; set; }
		public string req_order_status { get; set; }
		public string mp_ck { get; set; }
		public string purchase_require_date { get; set; }

		// 4. default 생성자 
		public APS_Workorder_Request()
		{
		}



	}
}