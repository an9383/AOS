using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HACCP.Models.Education
{
    public class EmployeeEduGrade
    {
		//SP에서 사용하는 Param들

		public string gubun { get; set; }
		public string edu_no { get; set; }
		public string title { get; set; }
		public string sdate { get; set; }
		public string edate { get; set; }
		public string attendance_yn { get; set; } //참석여부
		public string complete_yn { get; set; } //합격(이수)여부
		public string evaluation { get; set; }
		public string doc_file_id { get; set; }
		public string emp_cd { get; set; }
		public string doc_name { get; set; }
		public string file_extension_name { get; set; }
		public string file_id { get; set; }
		public string table_id { get; set; }
		public string table_id2 { get; set; }
		public string edu_remark { get; set; }
		public string comments { get; set; }
		public string target_status { get; set; }
		public string update_status { get; set; }
		public string edu_emp_cd { get; set; }
		public string sign_emp_cd { get; set; }
		public string sign_time { get; set; }
		public string sign_type { get; set; }
		public string edu_gb { get; set; } //image file?
		public string edu_method { get; set; }
		public string effect_status { get; set; }
		public string dept_cd { get; set; }

        public EmployeeEduGrade()
        {
        }
    }
}