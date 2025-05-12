using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.DepositMng
{
	public class DepositSampleUse
	{
		public class SrchParam
		{
			public string write_gb { get; set; }
			public string sdate { get; set; }
			public string edate { get; set; }
			public string test_type { get; set; }
			public string item_cd { get; set; }
			public string depositsample_id { get; set; }
			public string selecttype { get; set; }
			public string selecttype2 { get; set; }
			public string item_type1 { get; set; }
			public string item_type2 { get; set; }
			public string status { get; set; }

			public SrchParam()
			{
				this.write_gb = "";
				this.sdate = "";
				this.edate = "";
				this.test_type = "%";
				this.item_cd = "";
				this.depositsample_id = "";
				this.selecttype = "";
				this.item_type1 = "%";
				this.item_type2 = "%";
				this.selecttype2 = "1";
				this.status = "";
			}
		}

		public class SignParam
		{
			public string gubun { get; set; }
			public string emp_cd { get; set; }
			public string sign_type { get; set; }
			public string sign_set_cd { get; set; }
			public string depositsample_id { get; set; }
			public string depositsampleuse_id { get; set; }
			public string sign_set_dt_id { get; set; }
			public string sign_set_dt_seq { get; set; }
			public string sign_tot_cnt { get; set; }
		}

		public SrchParam srch;
		public SignParam signParam;

		public string gubun { get; set; }
		public string depositsample_id { get; set; }
		public string test_type { get; set; }
		public string sampling_date { get; set; }
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
		public string deposit_unit_nm { get; set; }
		public string deposit_unit { get; set; }
		public string deposit_condition { get; set; }
		public string deposit_condition_nm { get; set; }
		public string warehouse { get; set; }
		public string warehouse_nm { get; set; }
		public string location { get; set; }
		public string location_nm { get; set; }
		public string stock_qty { get; set; }
		public string stock_unit_qty { get; set; }
		public string testcontrol_id { get; set; }
		public string write_gb { get; set; }
		public string barcode_no { get; set; }
		public string limited_deposit_date { get; set; }
		public string request_qty { get; set; }
		public string request_unit_qty { get; set; }
		public string start_no { get; set; }
		public string test_no { get; set; }
		public string use_qty { get; set; }
		public string use_unit_qty { get; set; }
		public string before_request_qty { get; set; }
		public string before_request_unit_qty { get; set; }
		public string use_date { get; set; }
		public string use_emp_cd { get; set; }
		public string remark { get; set; }
		public string depositsampleuse_id { get; set; }
		public string status_cd { get; set; }
		public string out_date { get; set; }
		public string out_emp_cd { get; set; }
		public string refund_qty { get; set; }
		public string refund_unit_qty { get; set; }
		public string refund_date { get; set; }
		public string refund_emp_cd { get; set; }
		public string select_option { get; set; }

	}
}