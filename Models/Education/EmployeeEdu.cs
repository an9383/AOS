using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HACCP.Models.Education
{
    public class EmployeeEdu
    {
		//SP에서 사용하는 Param들

		public string gubun { get; set; }
		public string edu_no { get; set; }
		public string title { get; set; }
		public string contents { get; set; }
		public string lecturer_cd { get; set; }
        public string lecturer_nm { get; set; }
        public string purpose { get; set; }
		public string sdate { get; set; }
		public string edate { get; set; }
		public string emp_cd { get; set; }
		public string attendance_yn { get; set; }
		public string evaluation { get; set; }
		public string request_date { get; set; }
		public string edu_time { get; set; }
		public string sort_contents { get; set; }
		public string institute { get; set; }
		public string edu_method { get; set; }
		public string edu_start_date { get; set; }
		public string edu_end_date { get; set; }
		public string edu_start_time { get; set; }
		public string edu_end_time { get; set; }
		public string write_emp_cd { get; set; }
		public string edu_manage_no { get; set; }
		public string table_id { get; set; }
		public string file_id { get; set; }
		public string edu_gb { get; set; }
		public string doc_name { get; set; }
		public string doc_type { get; set; }
		public string file_image { get; set; } //image file?
		public string doc_file_id { get; set; }
		public string upload_emp_cd { get; set; }
		public string edu_place_cd { get; set; }
		public string edu_place_nm { get; set; }
		public string comments { get; set; }
		public string emp_group_cd { get; set; }
		public string sort_host_S { get; set; }
		public string dept_cd_S { get; set; }
		public string dept_cd { get; set; }
		public string effect_status { get; set; }

        public EmployeeEdu()
        {
        }
    }
}