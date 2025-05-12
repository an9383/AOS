using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.Aprov
{
	public class TestRecognitionE_Temp
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string date_select { get; set; }
			public string start_date { get; set; }
			public string end_date { get; set; }
			public string test_typeselc { get; set; }
			public string test_status { get; set; }
			public string search_gubun { get; set; }
			public string search_number { get; set; }
			public string item_form_cd { get; set; }
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public int testcontrol_id { get; set; }
		public string test_type { get; set; }
		public string test_type_cd { get; set; }
		public string item_nm { get; set; }
		public string item_enm { get; set; }
		public string item_cd { get; set; }
		public string test_no { get; set; }
		public string test_judge_no { get; set; }
		public string test_result_yn { get; set; }
		public string test_standard_nm { get; set; }
		public string process_nm { get; set; }
		public string test_result_emp_nm { get; set; }
		public string start_no { get; set; }
		public string instruction_date { get; set; }
		public string test_date { get; set; }
		public string test_status { get; set; }
		public string test_status_no { get; set; }
		public string test_inform_remark { get; set; }
		public string request_date { get; set; }
		public string receive_date { get; set; }
		public string test_result_yn_c { get; set; }
		public string test_result_value0 { get; set; }
		public string test_result_value { get; set; }
		public string test_result_pollination { get; set; }
		public string test_result_solvent { get; set; }
		public string valid_period { get; set; }
		public string retest_yn { get; set; }
		public int pack_cnt { get; set; }
		public string order_no { get; set; }
		public string order_proc_id { get; set; }
		public string pack_order_no { get; set; }
		public string upload_yn { get; set; }
		public string upload_dt { get; set; }
		public string upload_remark { get; set; }
		public string test_standard_1 { get; set; }
		public string test_standard_2 { get; set; }
		public string test_standard_3 { get; set; }
		public string test_standard_4 { get; set; }
		public string test_standard_5 { get; set; }
		public string test_standard_nm_1 { get; set; }
		public string extend_yn { get; set; }
		public string extend_yn_nm { get; set; }
		public string qc_valid_period { get; set; }
		public string item_type3 { get; set; }
		public string enter_date { get; set; }
		public string hb_ck { get; set; }
		public string trade_cd2 { get; set; }
		public string testrequest_no { get; set; }

		public string require_sign { get; set; }

		public string update_or_cancel { get; set; }

		// 4. default 생성자 
		public TestRecognitionE_Temp()
		{
		}

		// 5. DTO 설정
		public TestRecognitionE_Temp(DataRow row)
		{
			//row_status = row["row_status"].ToString();
			testcontrol_id = Int32.Parse(row["testcontrol_id"].ToString());
			test_type = row["test_type"].ToString();
			test_type_cd = row["test_type_cd"].ToString();
			item_nm = row["item_nm"].ToString();
			item_enm = row["item_enm"].ToString();
			item_cd = row["item_cd"].ToString();
			test_no = row["test_no"].ToString();
			test_judge_no = row["test_judge_no"].ToString();
			test_result_yn = row["test_result_yn"].ToString();
			test_standard_nm = row["test_standard_nm"].ToString();
			process_nm = row["process_nm"].ToString();
			test_result_emp_nm = row["test_result_emp_nm"].ToString();
			start_no = row["start_no"].ToString();
			instruction_date = row["instruction_date"].ToString();
			test_date = row["test_date"].ToString();
			test_status = row["test_status"].ToString();
			test_status_no = row["test_status_no"].ToString();
			test_inform_remark = row["test_inform_remark"].ToString();
			request_date = row["request_date"].ToString();
			receive_date = row["receive_date"].ToString();
			test_result_yn_c = row["test_result_yn_c"].ToString();
			test_result_value0 = row["test_result_value0"].ToString();
			test_result_value = row["test_result_value"].ToString();
			test_result_pollination = row["test_result_pollination"].ToString();
			test_result_solvent = row["test_result_solvent"].ToString();
			valid_period = row["valid_period"].ToString();
			retest_yn = row["retest_yn"].ToString();
			pack_cnt = Int32.Parse(row["pack_cnt"].ToString());
			order_no = row["order_no"].ToString();
			order_proc_id = row["order_proc_id"].ToString();
			pack_order_no = row["pack_order_no"].ToString();
			upload_yn = row["upload_yn"].ToString();
			upload_dt = row["upload_dt"].ToString();
			upload_remark = row["upload_remark"].ToString();
			test_standard_1 = row["test_standard_1"].ToString();
			test_standard_2 = row["test_standard_2"].ToString();
			test_standard_3 = row["test_standard_3"].ToString();
			test_standard_4 = row["test_standard_4"].ToString();
			test_standard_5 = row["test_standard_5"].ToString();
			test_standard_nm_1 = row["test_standard_nm_1"].ToString();
			extend_yn = row["extend_yn"].ToString();
			extend_yn_nm = row["extend_yn_nm"].ToString();
			qc_valid_period = row["qc_valid_period"].ToString();
			item_type3 = row["item_type3"].ToString();
			enter_date = row["enter_date"].ToString();
			hb_ck = row["hb_ck"].ToString();
			trade_cd2 = row["trade_cd2"].ToString();
			item_nm = row["item_nm"].ToString();
			testrequest_no = row["testrequest_no"].ToString();
			require_sign = row["require_sign"].ToString();
		}
	}


}