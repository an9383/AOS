using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Change
{
	public class ChangeControlReceive
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string sdate { get; set; }
			public string edate { get; set; }
			public string change_yn { get; set; }
			public string change_level1 { get; set; }
			public string changecontrol_status { get; set; }


			// default 검색 생성자 
			public SrchParam()
			{
				this.sdate = DateTime.Now.AddYears(-1).ToShortDateString();
				this.edate = DateTime.Now.ToShortDateString();
				this.change_yn = "%";
				this.change_level1 = "%";
				this.changecontrol_status = "%";
			}
		}

		public SrchParam srch;
 
		public string gubun { get; set; }
		public string tabIndex { get; set; }
		public bool isChecked { get; set; }
		public string row_status { get; set; }
		public string changecontrol_no { get; set; }
		public string changecontrol_action_id { get; set; }
		public string request_date { get; set; }
		public string emp_cd { get; set; }
		public string action_limit { get; set; }
		public string order_contents { get; set; }
		public string order_dept_cd { get; set; }
		public string order_work_process { get; set; }
		public string request_emp_cd { get; set; }
		public string request_emp_nm { get; set; }
		public string request_contents { get; set; }
		public string changecontrol_status { get; set; }
		public string changecontrol_status_nm { get; set; }
		public string changecontrol_cd { get; set; }
		public string change_evidence { get; set; }
		public string change_level { get; set; }
		public string change_contents { get; set; }
		public string change_level1 { get; set; }
		public string change_contents1 { get; set; }
		public string change_yn { get; set; }
		public string change_n_remark { get; set; }
		public string change_level2 { get; set; }
		public string change_contents2 { get; set; }
		public string changecontrol_recieved_no { get; set; }
		public string change_title { get; set; }
		public string request_gubun { get; set; }
		public string change_a_emp_cd { get; set; }
		public string change_a_emp_type { get; set; }

	}
}