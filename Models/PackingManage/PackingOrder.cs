using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PackingManage
{
	public class PackingOrder
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string sdate { get; set; }
			public string edate { get; set; }
			public string order_type { get; set; }
			public string s_item { get; set; }
			public string s_lot_no { get; set; }
			public string order_no { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.sdate = DateTime.Now.AddMonths(-1).ToShortDateString();
				this.edate = DateTime.Now.ToShortDateString();
				this.order_type = "%";
				this.s_item = "";
				this.s_lot_no = "";
				this.order_no = "";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		///  
		public string order_no { get; set; }
		public string order_type { get; set; }
		public string order_type_nm { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string item_packunit { get; set; }
		public string item_unit_qty { get; set; }
		public string item_lot_size { get; set; }
		public string item_validity_period { get; set; }
		public string lot_no { get; set; }
		public string order_qty { get; set; }
		public string planned_date { get; set; }
		public string lot_date { get; set; }
		public string valid_date { get; set; }
		public string order_status { get; set; }
		public string order_status_nm { get; set; }
		public string outer_interface { get; set; }
		public string order_batch_size { get; set; }
		public string order_batch_size_unit { get; set; }
		public string item_pack_size { get; set; }

		// 4. default 생성자 
		public PackingOrder()
		{
		}

		// 5. DTO 설정
		public PackingOrder(DataRow row)
		{
			order_no = row["order_no"].ToString();
			order_type = row["order_type"].ToString();
			order_type_nm = row["order_type_nm"].ToString();
			item_cd = row["item_cd"].ToString();
			item_nm = row["item_nm"].ToString();
			item_packunit = row["item_packunit"].ToString();
			item_unit_qty = row["item_unit_qty"].ToString();
			item_lot_size = row["item_lot_size"].ToString();
			item_validity_period = row["item_validity_period"].ToString();
			lot_no = row["lot_no"].ToString();
			order_qty = row["order_qty"].ToString();
			planned_date = row["planned_date"].ToString();
			lot_date = row["lot_date"].ToString();
			valid_date = row["valid_date"].ToString();
			order_status = row["order_status"].ToString();
			order_status_nm = row["order_status_nm"].ToString();
			outer_interface = row["outer_interface"].ToString();
			order_batch_size = row["order_batch_size"].ToString();
			order_batch_size_unit = row["order_batch_size_unit"].ToString();
			item_pack_size = row["item_pack_size"].ToString();
		}
	}


}