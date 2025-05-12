using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.RowMatWh
{
	public class PickingManage
	{
		//SP 구분
		public string s_gubun { get; set; }
		public string start_date { get; set; }
		public string end_date { get; set; }
		public string qc_no { get; set; }
		public string weighing_no { get; set; }
		public string receipt_pack_barcode { get; set; }
		public string update_user_cd { get; set; }
		public string gubun { get; set; }

		//property > 조회결과SET 
		public string planned_date { get; set; }
		public string order_no { get; set; }
		public string lot_no { get; set; }
		public string input_order_id { get; set; }
		public string process_cd { get; set; }
		public string process_nm { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string item_unit { get; set; }

		// default 생성자 
		public PickingManage()
		{
		}
	}




}