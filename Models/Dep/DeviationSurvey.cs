using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Dep
{
	public class DeviationSurvey
	{
		//property > 조회결과SET 
		public string deviation_no { get; set; }
		public int deviation_survey_id { get; set; }
		public string survey_order_emp { get; set; }
		public string survey_order_date { get; set; }
		public string survey_charge_emp { get; set; }
		public string survey_limit { get; set; }
		public string survey_emp_cd { get; set; }
		public string survey_emp_time { get; set; }
		public string survey_emp_type { get; set; }
		public string survey_result { get; set; }
		public string survey_result_type { get; set; }
		public string survey_cause { get; set; }
		public string necessary_work { get; set; }
		public string deviation_survey_status { get; set; }
		public string survey_contents { get; set; }
		public string sys_plant_cd { get; set; }
		public int audittrail_id { get; set; }
		public string survey_order_dept_cd { get; set; }
		public string survey_date { get; set; }
		public string survey_order_content { get; set; }
		public string survey_plan_content { get; set; }
		public string etc_no { get; set; }

		// default 생성자 
		public DeviationSurvey()
		{
		}

	}


}