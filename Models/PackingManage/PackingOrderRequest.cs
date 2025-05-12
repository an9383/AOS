using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PackingManage
{
	public class PackingOrderRequest
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{

			public string sdate { get; set; }
			public string edate { get; set; }
			public string item_cd { get; set; }
			public string material_status { get; set; }
			public string packing_order_no { get; set; }
			public string packing_order_qty { get; set; }
			public string batch_size { get; set; }
			public string req_order_gb { get; set; }
			public string req_order_id { get; set; }
			public string material_item_cd { get; set; }


			// default 검색 생성자 
			public SrchParam()
			{
				this.sdate = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)).ToShortDateString();
				this.edate = DateTime.Now.ToShortDateString();
				this.item_cd = "";
				this.material_status = "%";
				this.packing_order_no = "";
				this.packing_order_qty = "";
				this.batch_size = "";
				this.req_order_gb = "";
				this.req_order_id = "";
				this.material_item_cd = "";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string packing_order_no { get; set; }
		public string sale_item_cd { get; set; }
		public string sale_item_nm { get; set; }
		public string packing_order_date { get; set; }
		public string packing_order_work_date { get; set; }
		public string packing_order_qty { get; set; }
		public string item_packunit { get; set; }
		public string batch_size_sub { get; set; }
		public string item_packunit2 { get; set; }
		public string order_no { get; set; }
		public string material_status { get; set; }
		public string material_status_nm { get; set; }
		public string issue_emp_cd { get; set; }
		public string issue_emp_nm { get; set; }
		public string receive_emp_cd { get; set; }
		public string receive_emp_nm { get; set; }
		public string packing_order_status { get; set; }
		public string lot_no { get; set; }
		public string req_order_gb { get; set; }
		public string req_order_id { get; set; }
		public string process_cd { get; set; }
		public string req_order_child_cd { get; set; }
		public string req_order_qty { get; set; }
		public string req_order_calc_type { get; set; }
		public string req_order_last_type { get; set; }
		public string req_order_real_qty { get; set; }
		public string req_order_print_yn { get; set; }
		public string item_cd { get; set; }
		public string pItem_cd { get; set; }
		public string req_order_batch_qty { get; set; }
		public string item_bom_id { get; set; }
		public string item_bom_no { get; set; }
		public string req_order_batch_unit { get; set; }
		public string REQ_ORDER_PRINT_YN { get; set; }
		public string req_order_child_packunit { get; set; }
		public string req_order_material_id { get; set; }
		public string req_order_material_qc_no { get; set; }
		public string req_order_material_qty { get; set; }

		// 4. default 생성자 
		public PackingOrderRequest()
		{

		}
	}


}