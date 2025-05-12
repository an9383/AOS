using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdWh
{
	public class ItemMonthlyStock
	{
		public string start_date { get; set; }
		public string end_date { get; set; }
		public string item_cd_S { get; set; }
		public string search_data_S { get; set; }
		public string use_ck_S { get; set; }
		public string obtain_status { get; set; }
		public string item_gb { get; set; }

		//3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		///  
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string unit { get; set; }
		public string old_qty { get; set; }
		public string ye_qty { get; set; }
		public string in_qty { get; set; }
		public string etc_in_qty { get; set; }
		public string out_qty { get; set; }
		public string etc_out_qty { get; set; }
		public string stock_qty { get; set; }
		public string theory_qty { get; set; }
		public string detail { get; set; }

	 //4. default 생성자
		public ItemMonthlyStock()
		{
		}
	}


}