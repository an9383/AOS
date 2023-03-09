using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HACCP.Models.SysOp
{
    public class AlarmManage
	{
		public string gubun { get; set; }

		public string alarm_cd { get; set; }

		public string alarm_nm { get; set; }

		public string alarm_body { get; set; }

		public string alarm_kakao { get; set; }

		public string kakao_format_code { get; set; }

		public string emp_cd { get; set; }

		//MST
		public string ALD_CODE{ get; set; }
		public string ALD_KAKAO { get; set; }
		public string ALD_SMS { get; set; }
		public string ALD_VIEW { get; set; }
		public string ALD_EMAIL { get; set; }

		public string email_addr { get; set; }
		public string phone_no { get; set; }

		public AlarmManage ()
        {

        }
	}
}