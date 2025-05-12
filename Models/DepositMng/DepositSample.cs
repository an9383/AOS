using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.DepositMng
{
	public class DepositSample
	{
		public class SrchParam
		{
			public string selecttype { get; set; }
			public string sdate { get; set; }
			public string edate { get; set; }
			public string test_type { get; set; }
			public string item_cd { get; set; }
			public string write_gb { get; set; }
			public string selecttype2 { get; set; }

			public SrchParam()
			{
				this.selecttype = "";
				this.sdate = "";
				this.edate = "";
				this.test_type = "%";
				this.item_cd = "";
				this.write_gb = "1";
				this.selecttype2 = "1";
			}
		}

		public SrchParam srch;

		public string gubun { get; set; }
		public string depositsample_id { get; set; }
		public string test_type { get; set; }
		public string sampling_date { get; set; }
		public string sampling_emp_cd { get; set; }
		public string sampling_emp_nm { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string item_enm { get; set; }
		public string order_no { get; set; }
		public string start_date { get; set; }
		public string valid_date { get; set; }
		public string deposit_date { get; set; }
		public string deposit_emp_cd { get; set; }
		public string deposit_emp_nm { get; set; }
		public string deposit_qty { get; set; }
		public string deposit_unit_qty { get; set; }
		public string deposit_unit { get; set; }
		public string deposit_unit_nm { get; set; }
		public string deposit_condition { get; set; }
		public string deposit_condition_nm { get; set; }
		public string deposit_temperature { get; set; }
		public string deposit_temperature_nm { get; set; }
		public string warehouse { get; set; }
		public string warehouse_nm { get; set; }
		public string location { get; set; }
		public string location_nm { get; set; }
		public string testcontrol_id { get; set; }
		public string stock_qty { get; set; }
		public string stock_unit_qty { get; set; }
		public string test_no { get; set; }
		public string limited_deposit_date { get; set; }
		public string deposit_sample_remark { get; set; }
		public string item_type2_nm { get; set; }
		public string start_no { get; set; }
		public string barcode_no { get; set; }
		public string edate { get; set; }
		public string deposit_condition_temperature { get; set; }
		public string plant_nm { get; set; }
		public string write_gb { get; set; }

	}
}