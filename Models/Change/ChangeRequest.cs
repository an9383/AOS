using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Change
{
	public class ChangeRequest
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string sdate { get; set; }
			public string edate { get; set; }
			public string changecontrol_cd { get; set; }
			public string changecontrol_status { get; set; }
			public string request_emp_cd { get; set; }
			public string request_app_emp_cd { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.sdate = DateTime.Now.AddYears(-1).ToShortDateString();
				this.edate = DateTime.Now.ToShortDateString();
				this.changecontrol_cd = "%";
				this.changecontrol_status = "%";
				this.request_emp_cd = "";
				this.request_app_emp_cd = "";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string changecontrol_no { get; set; }
		public string request_date { get; set; }
		public string request_emp_cd { get; set; }
		public string request_emp_nm { get; set; }
		public string request_contents { get; set; }
		public string change_evidence { get; set; }
		public string temp_change_yn { get; set; }
		public string temp_change_limit { get; set; }
		public string changecontrol_cd { get; set; }
		public string changecontrol_status { get; set; }
		public string changecontrol_status_nm { get; set; }
		public string request_confirm3_emp_nm { get; set; }
		public string change_title { get; set; }
		public string change_date { get; set; }
		public string request_gubun { get; set; }
		public string changecontrol_recieved_no { get; set; }
		public string changecontrol_recieved_emp_cd { get; set; }
		public string changecontrol_recieved_emp_nm { get; set; }
		public string request_special { get; set; }
		public string gubun { get; set; }
		public string sign_type { get; set; }
		public string request_app_emp_cd { get; set; }
		public string request_app_emp_type { get; set; }

		// 4. default 생성자 
		public ChangeRequest()
		{
		}
	}
}