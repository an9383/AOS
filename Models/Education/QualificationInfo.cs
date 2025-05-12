using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HACCP.Models.Education
{
    public class QualificationInfo
	{
		//SP에서 사용하는 Param들

		public string gubun { get; set; }
		public string emp_cd { get; set; }
		public string regist_no { get; set; }
		public string etc { get; set; }
        public string license_cd { get; set; }
		public string license_no { get; set; }
		public string license_info { get; set; }
		public string remark { get; set; }
		public string table_id { get; set; }
		public string checkup_date { get; set; }
		public string checkup_type { get; set; }
		public string checkup_status { get; set; }
		public string checkup_period { get; set; }
		public string period_type { get; set; }
		public string manage_yn { get; set; }
		public string manage_contant { get; set; }
		public string doc_name { get; set; }
		public string doc_type { get; set; }
		public string file_image { get; set; } //image file?
		public string file_id { get; set; } 


		public QualificationInfo()
        {
        }
    }
}