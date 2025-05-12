using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdWh
{
	public class ItemInOutStatus
	{

		public string gubun { get; set; }
		public string start_date { get; set; }
		public string end_date { get; set; }
		public string item { get; set; }
		public string use_ck { get; set; }


		//3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		///  
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string item_lot_size { get; set; }
		public string lot_no { get; set; }
		public string box_barcode_no { get; set; }
		public string prod_qty { get; set; }
		public string delivery_qty { get; set; }
		public string stock_qty { get; set; }
		public string location { get; set; }
		public string lot_date { get; set; }

	 //4. default 생성자
		public ItemInOutStatus()
		{
		}
	}


}