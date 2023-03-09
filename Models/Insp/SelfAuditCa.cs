using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.Insp
{
	public class SelfAuditCa
	{
		//property > 조회결과SET 
		public string self_audit_year { get; set; }
		public string self_audit_no { get; set; }
		public int self_audit_ca_id { get; set; }
		public string self_audit_ca_type { get; set; }
		public string self_audit_ca_o_emp { get; set; }
		public string self_audit_ca_o_date { get; set; }
		public string self_audit_ca_o_contents { get; set; }
		public string self_audit_ca_dept { get; set; }
		public string self_audit_ca_dept_emp { get; set; }
		public string self_audit_ca_plan_contents { get; set; }
		public string self_audit_ca_plan_limit { get; set; }
		public string self_audit_ca_emp_cd { get; set; }
		public string self_audit_ca_date { get; set; }
		public string self_audit_ca_contents { get; set; }
		public string self_audit_ca_status { get; set; }
		public string self_audit_ca_doc_plan_contents { get; set; }
		public string self_audit_ca_plan_limit_end { get; set; }
		public string self_audit_ca_doc_result_contents { get; set; }
		public string sys_plant_cd { get; set; }
		public int audittrail_id { get; set; }
		public string self_audit_ca_workroom_cd { get; set; }

		// default 생성자 
		public SelfAuditCa()
		{
		}

	}


}