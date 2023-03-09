using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.ManufactureWaterMng
{
	public class WaterTestScheduleConfirm
	{
		public class SrchParam
		{
			public string confirm_no { get; set; }
			public string start_date { get; set; }
			public string end_date { get; set; }

			public SrchParam()
			{
				this.confirm_no = "";
				this.start_date = "";
				this.end_date = "";
			}
		}

		public SrchParam srch;
		public string gubun { get; set; }
		public string confirm_no { get; set; }
		public string start_date { get; set; }
		public string end_date { get; set; }
		public string confirm_status { get; set; }
		public string confirm_status_nm { get; set; }
		public string water_gb { get; set; }
		public string point_cd { get; set; }
		public string schedule_date { get; set; }
		public string emp_cd { get; set; }
		public string sign_type { get; set; }
		public string sign_set_dt_id { get; set; }

	}
}