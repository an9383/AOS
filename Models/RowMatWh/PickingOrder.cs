using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.RowMatWh
{
	public class PickingOrder
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
			/// Param1 
			/// </summary> 
			public string item_cd { get; set; }
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		//3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		///  
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string item_unit { get; set; }
		public string order_qty { get; set; }
		public string qc_no { get; set; }
		public string cell_cd { get; set; }
		public string cell_nm { get; set; }
		public string receipt_pack_remain_qty { get; set; }
		public string input_part_qty { get; set; }

		//4. default 생성자
		public PickingOrder()
		{
		}

		//5. DTO 설정
		public PickingOrder(DataRow row)
		{
			item_cd = row["item_cd"].ToString();
			item_nm = row["item_nm"].ToString();
			item_unit = row["item_unit"].ToString();
			order_qty = row["order_qty"].ToString();
			qc_no = row["qc_no"].ToString();
			cell_cd = row["cell_cd"].ToString();
			cell_nm = row["cell_nm"].ToString();
			receipt_pack_remain_qty = row["receipt_pack_remain_qty"].ToString();
			input_part_qty = row["input_part_qty"].ToString();
		}
	}


}