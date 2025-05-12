using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HACCP.Models.Education
{
    public class EmployeeEduResult
    {
		//SP에서 사용하는 Param들

		public string gubun { get; set; }
		public string title { get; set; }
		public string sdate { get; set; }
		public string edate { get; set; }
		public string edu_no { get; set; }
		public string emp_cd { get; set; }
		public string employee_edu_result { get; set; }
		public string doc_file_id { get; set; }
		public string edu_gb { get; set; }
        public string edu_method { get; set; }
        public string attendance_yn { get; set; }
		public string complete_yn { get; set; }
		public string table_id { get; set; }
		public string table_id2 { get; set; }
		public string target_status { get; set; }
		public string update_status { get; set; }
		public string doc_name { get; set; }
		public string doc_type { get; set; }
		public string file_image { get; set; } //image file?
		public string upload_emp_cd { get; set; }

		public EmployeeEduResult()
        {
        }
    }
}