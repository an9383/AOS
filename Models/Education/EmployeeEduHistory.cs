using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HACCP.Models.Education
{
    public class EmployeeEduHistory
	{
		//SP에서 사용하는 Param들

		public string gubun { get; set; }
		public string edu_no { get; set; }
		public string title { get; set; }
		public string sdate { get; set; }
		public string edate { get; set; }
		public string emp_cd { get; set; }
		public string complete_yn { get; set; }
		public string dept_cd { get; set; }

        public EmployeeEduHistory()
        {
        }
    }
}