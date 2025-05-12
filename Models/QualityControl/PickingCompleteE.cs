using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.QualityControl
{
	public class PickingCompleteE
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string selectdate { get; set; }
			public string de_Sdate { get; set; }
			public string de_Edate { get; set; }
			public string test_type { get; set; }
			public string test_status { get; set; }
			public string search_gubun { get; set; }
			public string search_number { get; set; }
			public string item_form_cd { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.selectdate = "request";
				this.de_Sdate = DateTime.Now.AddMonths(-1).ToShortDateString();
				this.de_Edate = DateTime.Now.ToShortDateString();
				this.test_type = "%";
				this.test_status = "1";
				this.search_gubun = "0";
				this.search_number = "";
				this.item_form_cd = "%";
			}
		}

		public class PackSampling
		{
			public string testcontrol_id { get; set; }
			public string receipt_no { get; set; }
			public string receipt_id { get; set; }
			public string receipt_pack_seq { get; set; }
			public string sampling_yn { get; set; }
			public string sampling_qty { get; set; }
		}

		public SrchParam srch;

		public PackSampling packSampling;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		//public string row_status { get; set; }
		public string gubun { get; set; }
		public int testcontrol_id { get; set; }
		public string testrequest_no { get; set; }
		public string test_no { get; set; }
		public string test_type { get; set; }
		public string test_type_nm { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string item_enm { get; set; }
		public string test_standard { get; set; }
		public string test_standard_nm { get; set; }
		public string process_cd { get; set; }
		public string process_nm { get; set; }
		public string request_date { get; set; }
		public string start_no { get; set; }
		public int? pack_cnt { get; set; }
		public string pack_unit_nm { get; set; }
		public string result_plan_date { get; set; }
		public string picking_order_emp_cd { get; set; }
		public string picking_order_emp_nm { get; set; }
		public string picking_ordered_emp_cd { get; set; }
		public string picking_ordered_emp_nm { get; set; }
		public string instruction_date { get; set; }
		public string picking_date { get; set; }
		public string picking_emp_cd { get; set; }
		public string picking_emp_nm { get; set; }
		public decimal picking_qty { get; set; }
		public string picking_qty_unit { get; set; }
		public string standard_sample_qty { get; set; }
		public string picking_workroom_cd { get; set; }
		public string picking_workroom_nm { get; set; }
		public string picking_method { get; set; }
		public string picking_sop1 { get; set; }
		public string picking_sop2 { get; set; }
		public string test_result_yn { get; set; }
		public string sample_status { get; set; }
		public string sample_status_nm { get; set; }
		public string test_status { get; set; }
		public string test_status_nm { get; set; }
		public string material_maker_nm { get; set; }
		public decimal test_sample_qty { get; set; }
		public decimal deposit_sample_qty { get; set; }
		public decimal stability_sample_qty { get; set; }
		public string standard_test_sample_qty { get; set; }
		public string standard_deposit_sample_qty { get; set; }
		public string start_qty { get; set; }
		public string valid_period { get; set; }
		public string sampling_calc { get; set; }
		public decimal pack_sampling_qty { get; set; }
		public float? standard_pack_sampling_qty { get; set; }
		public string pack_type { get; set; }
		public string standard_stability_sample_qty { get; set; }
		public decimal? sample_qty { get; set; }
		public string sample_unit { get; set; }
		public string emergency_test_yn { get; set; }
		public string microorganism_yn { get; set; }
		public string standard_sample_microbe_qty { get; set; }
		public string sample_microbe_qty { get; set; }
		public string start_date { get; set; }
		public string container_material { get; set; }
		public string receive_date { get; set; }
		public string lotorstart_no { get; set; }
		public string makerorprocess { get; set; }
		public string test_standard_nm_1 { get; set; }
		public string pack_sampling_ck { get; set; }
		public string request_remark { get; set; }
		public string testsample_yn { get; set; }
		public string sign_time { get; set; }
		public string hb_ck { get; set; }
		public string trade_cd2 { get; set; }
		public string receipt_no { get; set; }	
		
	}

}