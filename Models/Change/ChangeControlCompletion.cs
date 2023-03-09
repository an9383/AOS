using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Change
{
	public class ChangeControlCompletion
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string sdate { get; set; }
			public string edate { get; set; }
			public string emp_cd { get; set; }
			public string changecontrol_status { get; set; }

			public SrchParam()
			{
				this.sdate = DateTime.Now.AddYears(-1).ToShortDateString();
				this.edate = DateTime.Now.ToShortDateString();
				this.emp_cd = "";
				this.changecontrol_status = "3";
			}
		}

		public SrchParam srch;

		public string gubun { get; set; }
		public string row_status { get; set; }
		public string changecontrol_no { get; set; }
		public string request_date { get; set; }
		public string request_emp_cd { get; set; }
		public string request_emp_nm { get; set; }
		public string request_contents { get; set; }
		public string change_evidence { get; set; }
		public string change_level { get; set; }
		public string change_level_nm { get; set; }
		public string change_contents { get; set; }
		public string changecontrol_status { get; set; }
		public string changecontrol_status_nm { get; set; }
		public string change_title { get; set; }
		public string change_level2 { get; set; }
		public string change_contents2 { get; set; }
		public string change_special { get; set; }
		public string changecontrol_cd { get; set; }
		public string change_final_result { get; set; }
		public string changecontrol_recieved_no { get; set; }
		public string emp_cd { get; set; }
		public string emp_type { get; set; }
		public string sign_type { get; set; }
		public string change_action_plan_contents { get; set; }
		public string change_schedule_plan { get; set; }
		public string change_result_plan_contents { get; set; }
		public string change_schedule_result { get; set; }
		public string changecontrol_action_id { get; set; }

	}
}