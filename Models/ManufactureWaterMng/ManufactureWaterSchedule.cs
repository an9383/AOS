using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.ManufactureWaterMng
{
	public class ManufactureWaterSchedule
	{
		public class SrchParam
		{

			public string s_sdate { get; set; }
			public string s_edate { get; set; }
			public string s_water_gb { get; set; }
			public string s_point_cd { get; set; }

			public SrchParam()
			{
				this.s_sdate = "";
				this.s_edate = "";
				this.s_water_gb = "%";
				this.s_point_cd = "";
			}
		}

		public class Schedule
		{
			public string gubun { get; set; }
			public string schedule_master_id { get; set; }
			public string obj_type { get; set; }
			public string obj_cd { get; set; }
			public string work_type { get; set; }
			public string work_item { get; set; }
			public string frequency { get; set; }
			public string schedule_period { get; set; }
			public string period_type { get; set; }
			public string repeat_standard { get; set; }
			public string holiday_yn { get; set; }
			public string holiday_option { get; set; }
			public string start_date { get; set; }
			public string end_date { get; set; }
			public string water_gb { get; set; }
			public string item_cd { get; set; }
			public string[] repeat_days { get; set; }
			public string day { get; set; }
			public string month { get; set; }
			public string weekofmonth { get; set; }
			public string dayofweek { get; set; }
			public string point_cd { get; set; }
			public string schedule_date { get; set; }
			public string test_no { get; set; }
			public string schedule_status_cd { get; set; }
			public string test_type { get; set; }

		}

		public SrchParam srch;
		public Schedule schedule;

		public string gubun { get; set; }
		public string test_type { get; set; }
		public string water_gb { get; set; }
		public string point_cd { get; set; }
		public string point_nm { get; set; }
		public string period_qty { get; set; }
		public string period_unit { get; set; }
		public string charge_cd { get; set; }
		public string start_date { get; set; }
		public string schedule_date { get; set; }
		public string schedule_status_cd { get; set; }
		public string schedule_status { get; set; }
		public string test_no { get; set; }
		public string schedule_result { get; set; }

	}
}