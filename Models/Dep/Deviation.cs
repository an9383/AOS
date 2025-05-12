using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Dep
{
	public class Deviation
	{
		//property > 조회결과SET 
		public string deviation_no { get; set; }
		public string detect_date { get; set; }
		public string deviation_place { get; set; }
		public string detect_emp_cd { get; set; }
		public string deviation_contents { get; set; }
		public string deviation_date { get; set; }
		public string detect_method { get; set; }
		public string deviation_doc { get; set; }
		public string deviation_type { get; set; }
		public string deviation_ref_no { get; set; }
		public string deviation_ref_item_cd { get; set; }
		public string deviation_ref_test_item { get; set; }
		public string deviation_ref_equip_cd { get; set; }
		public string deviation_confirm_emp { get; set; }
		public string deviation_yn { get; set; }
		public string deviation_confirm_date { get; set; }
		public string deviation_contents2 { get; set; }
		public string deviation_cause { get; set; }
		public string deviation_ref_doc { get; set; }
		public string deviation_level { get; set; }
		public string deviation_survey_order_emp { get; set; }
		public string deviation_survey_order_time { get; set; }
		public string deviation_survey_order_type { get; set; }
		public string deviation_result_yn { get; set; }
		public string deviation_result { get; set; }
		public string deviation_m_emp_cd { get; set; }
		public string deviation_m_emp_time { get; set; }
		public string deviation_m_emp_type { get; set; }
		public string deviation_q_emp_cd { get; set; }
		public string deviation_q_emp_time { get; set; }
		public string deviation_q_emp_type { get; set; }
		public string deviation_end_date { get; set; }
		public string deviation_status { get; set; }
		public string deviation_class { get; set; }
		public string etc_no { get; set; }
		public string sys_plant_cd { get; set; }
		public int audittrail_id { get; set; }
		public string capa_necessary_yn { get; set; }
		public string capa_no { get; set; }
		public string deviation_advice_contents { get; set; }
		public string deviation_confirm_no { get; set; }
		public string deviation_dept_cd { get; set; }
		public string deviation_detail_class { get; set; }
		public string deviation_direct_contents { get; set; }
		public string deviation_emp_cd { get; set; }
		public string deviation_inquiry_cause { get; set; }
		public string deviation_inquiry_contents { get; set; }
		public string deviation_measures_contents { get; set; }
		public string deviation_opinion_producer { get; set; }
		public string deviation_opinion_qa { get; set; }
		public string deviation_opinion_qc { get; set; }
		public string deviation_order_contents { get; set; }
		public string deviation_place_nm { get; set; }
		public string deviation_public_yn { get; set; }
		public string deviation_ref_item_nm { get; set; }
		public string deviation_reject_contents { get; set; }
		public string deviation_title { get; set; }
		public string registry_sign_set_cd { get; set; }
		public int circulation_dept { get; set; }
		public string deviation_early_respond { get; set; }
		public string deviation_receive_content { get; set; }
		public string deviation_receive_comment { get; set; }
		public string deviation_request_remark { get; set; }
		public string deviation_result_remark { get; set; }
		public string deviation_ref_process_cd { get; set; }
		public string deviation_emergency_yn { get; set; }
		public string circulation_emp_group_cd { get; set; }

		// default 생성자 
		public Deviation()
		{
		}

	}


}