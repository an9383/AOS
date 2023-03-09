using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PackingManage
{
	public class ItemLabelPrint
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string s_sdate { get; set; }
			public string s_edate { get; set; } 
			public string s_item_cd { get; set; }
			public string s_item_nm { get; set; }
			public string s_lot_no { get; set; }
			public string s_order_status { get; set; }
			public string packing_order_no { get; set; }

			public SrchParam()
			{
				this.s_sdate = DateTime.Now.AddMonths(-1).ToShortDateString();
				this.s_edate = DateTime.Now.ToShortDateString();
				this.s_item_cd = "";
				this.s_item_nm = "";
				this.s_lot_no = "";
				this.s_order_status = "%";
				this.packing_order_no = "";
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
		public string item_cd { get; set; }
		public string sale_item_cd { get; set; }
		public string sale_item_nm { get; set; }
		public string item_pack_size { get; set; }
		public string lot_no { get; set; }
		public string packing_order_qty { get; set; }
		public string packing_qty { get; set; }
		public string packing_order_status { get; set; }
		public string packing_order_no { get; set; }
		public string lot_date { get; set; }
		public string valid_date { get; set; }
		public string OUTER_INTERFACE { get; set; }
		public string ord { get; set; }
		public string new_ck { get; set; }
		public string material_status { get; set; }
		public string order_no { get; set; }
		public string receipt_qty { get; set; }
		public string receipt_date { get; set; }
		public string item_stock_id { get; set; }
		public string prod_qty { get; set; }
		public string pallet_unit { get; set; }
		public string Work_Date { get; set; }
		public string box_barcode_no { get; set; }
		public string seq { get; set; }
		public string gubun { get; set; }
		public string testrequest_no { get; set; }
		

		// 4. default 생성자 
		public ItemLabelPrint()
		{
		}

		// 5. DTO 설정
		public ItemLabelPrint(DataRow row)
		{
			row_status = row["row_status"].ToString();
			packing_order_work_date = row["packing_order_work_date"].ToString();
			sale_item_cd = row["sale_item_cd"].ToString();
			sale_item_nm = row["sale_item_nm"].ToString();
			item_pack_size = row["item_pack_size"].ToString();
			lot_no = row["lot_no"].ToString();
			packing_order_qty = row["packing_order_qty"].ToString();
			packing_qty = row["packing_qty"].ToString();
			packing_order_status = row["packing_order_status"].ToString();
			packing_order_no = row["packing_order_no"].ToString();
			lot_date = row["LOT_DATE"].ToString();
			valid_date = row["VALID_DATE"].ToString();
			OUTER_INTERFACE = row["OUTER_INTERFACE"].ToString();
			ord = row["ord"].ToString();
			new_ck = row["new_ck"].ToString();
			material_status = row["material_status"].ToString();
			order_no = row["order_no"].ToString();
		}
	}


}