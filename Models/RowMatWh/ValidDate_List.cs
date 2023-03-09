using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.RowMatWh
{
	public class ValidDate_List
	{
		public string Gubun { get; set; }
		public string Valid_date_S { get; set; }
		public string Valid_date { get; set; }
		public string s_gubun { get; set; }
		public string item_cd_s { get; set; }

		public string receipt_no { get; set; }
		public string receipt_id { get; set; }
		public string log_user_id { get; set; }
		public string remain_qty { get; set; }
		public string emp_cd { get; set; }
		public string validation_type { get; set; }
		public string retest_yn { get; set; }
		public string item_cd { get; set; }
		

		public ValidDate_List()
		{
		}
	}


}