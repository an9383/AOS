using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdWh
{
	public class ItemUseList2_Item
	{
		public string Gubun { get; set; }
		public string start_date { get; set; }
		public string end_date { get; set; }

		public string in_type { get; set; }
		public string out_type { get; set; }
		public string s_gubun { get; set; }


		//3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		///  
		public string seq { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string unit { get; set; }
		public string lot_no { get; set; }
		public string list_type { get; set; }
		public string list_gubun { get; set; }
		public string list_date { get; set; }
		public string approval_date { get; set; }
		public string in_qty { get; set; }
		public string out_qty { get; set; }
		public string remark { get; set; }
		public string cust_cd { get; set; }
		public string cust_nm { get; set; }
		public string pass_cust_cd { get; set; }
		public string pass_cust_nm { get; set; }
		public string good_remain_qty { get; set; }

	 //4. default 생성자
		public ItemUseList2_Item()
		{
		}

	}


}