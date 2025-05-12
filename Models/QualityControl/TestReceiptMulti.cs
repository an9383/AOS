using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.QualityControl
{
	public class TestReceiptMulti
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string Gubun { get; set; }
			/// <summary> 
			/// 의뢰일자/접수일자구분(의뢰일자:request, 접수일자:receipt)
			/// </summary> 
			public string rg_ReqorRec { get; set; }
			/// <summary> 
			/// Sdate 
			/// </summary> 
			public string de_start_date { get; set; }
			/// <summary> 
			/// Edate 
			/// </summary> 
			public string de_end_date { get; set; }
            /// <summary>
			/// 시험종류
			/// </summary>
			public string le_test_type { get; set; }
			/// <summary>
			/// 진행상태
			/// </summary>
            public string le_test_status { get; set; }
			/// <summary>
			/// 구분
			/// </summary>
            public string ce_gubun_number { get; set; }
			/// <summary>
			/// 구분 입력
			/// </summary>
			public string te_number { get; set; }
			/// <summary>
			/// 제형
			/// </summary>
			public string le_item_form_cd { get; set; }
			/// <summary>
			/// 안정성 시험 체크여부
			/// </summary>
			public string ckStabilityAll { get; set; }


			// default 검색 생성자 
			public SrchParam()
			{
				DateTime dt = DateTime.Now;

				this.Gubun = "S";
				this.rg_ReqorRec = "request";
				this.de_start_date = dt.AddMonths(-1).ToString("yyyy-MM-dd");
				this.de_end_date = dt.ToString("yyyy-MM-dd");
				this.le_test_type = "%";
				this.le_test_status = "1";
				this.ce_gubun_number = "0";
				this.te_number = "";
				this.le_item_form_cd = "%";
				this.ckStabilityAll = "";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public int testcontrol_id { get; set; }
		public string testrequest_no { get; set; }
		public string item_form_cd { get; set; }
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
		public string valid_period { get; set; }
		public string request_emp_cd1 { get; set; }
		public string request_emp_nm1 { get; set; }
		public string request_emp_cd2 { get; set; }
		public string request_emp_nm2 { get; set; }
		public decimal? test_result_value0 { get; set; }
		public string bigo { get; set; }
		public string start_qty { get; set; }
		public string start_qty_unit { get; set; }
		public string result_hope_date { get; set; }
		public string request_remark { get; set; }
		public string start_date { get; set; }
		public string start_no { get; set; }
		public string start_seq { get; set; }
		public string material_maker_cd { get; set; }
		public string material_maker_nm { get; set; }
		public string material_lot_no { get; set; }
		public string test_no { get; set; }
		public string receive_date { get; set; }
		public string enter_no { get; set; }
		public string enter_seq { get; set; }
		public string receive_emp_cd1 { get; set; }
		public string receive_emp_nm1 { get; set; }
		public string receive_emp_cd2 { get; set; }
		public string receive_emp_nm2 { get; set; }
		public string result_plan_date { get; set; }
		public string test_result_yn { get; set; }
		public string test_status { get; set; }
		public string test_status_nm { get; set; }
		public string teststandardYandN { get; set; }
		public string lotorstart_no { get; set; }
		public string makerorprocess { get; set; }
		public int pack_cnt { get; set; }
		public decimal? start_qty_calc { get; set; }
		public string order_qty { get; set; }
		public string receipt_no { get; set; }
		public int? receipt_id { get; set; }
		public string erp_lock_info { get; set; }
		public string erp_lot_end_date { get; set; }
		public string select_yn { get; set; }
		public string pack_type { get; set; }
		public string packing_qty { get; set; }
		public string all_test_check { get; set; }
		public string test_standard_1 { get; set; }
		public string test_standard_2 { get; set; }
		public string test_standard_3 { get; set; }
		public string test_standard_4 { get; set; }
		public string test_standard_5 { get; set; }
		public string test_standard_nm_1 { get; set; }
		public string upload_yn { get; set; }
		public DateTime? upload_dt { get; set; }
		public string asepsis_item_ck { get; set; }
		public string hb_ck { get; set; }

		// 4. default 생성자 
		public TestReceiptMulti()
		{
		}

		// 5. DTO 설정
		public TestReceiptMulti(DataRow row)
		{
			row_status = row["row_status"].ToString();
			testcontrol_id = Convert.ToInt32(row["testcontrol_id"].ToString());
			testrequest_no = row["testrequest_no"].ToString();
			item_form_cd = row["item_form_cd"].ToString();
			test_type = row["test_type"].ToString();
			test_type_nm = row["test_type_nm"].ToString();
			item_cd = row["item_cd"].ToString();
			item_nm = row["item_nm"].ToString();
			item_enm = row["item_enm"].ToString();
			test_standard = row["test_standard"].ToString();
			test_standard_nm = row["test_standard_nm"].ToString();
			process_cd = row["process_cd"].ToString();
			process_nm = row["process_nm"].ToString();
			request_date = row["request_date"].ToString();
			valid_period = row["valid_period"].ToString();
			request_emp_cd1 = row["request_emp_cd1"].ToString();
			request_emp_nm1 = row["request_emp_nm1"].ToString();
			request_emp_cd2 = row["request_emp_cd2"].ToString();
			request_emp_nm2 = row["request_emp_nm2"].ToString();
			test_result_value0 = Convert.ToDecimal(row["test_result_value0"]);
			bigo = row["bigo"].ToString();
			start_qty = row["start_qty"].ToString();
			start_qty_unit = row["start_qty_unit"].ToString();
			result_hope_date = row["result_hope_date"].ToString();
			request_remark = row["request_remark"].ToString();
			start_date = row["start_date"].ToString();
			start_no = row["start_no"].ToString();
			start_seq = row["start_seq"].ToString();
			material_maker_cd = row["material_maker_cd"].ToString();
			material_maker_nm = row["material_maker_nm"].ToString();
			material_lot_no = row["material_lot_no"].ToString();
			test_no = row["test_no"].ToString();
			receive_date = row["receive_date"].ToString();
			enter_no = row["enter_no"].ToString();
			enter_seq = row["enter_seq"].ToString();
			receive_emp_cd1 = row["receive_emp_cd1"].ToString();
			receive_emp_nm1 = row["receive_emp_nm1"].ToString();
			receive_emp_cd2 = row["receive_emp_cd2"].ToString();
			receive_emp_nm2 = row["receive_emp_nm2"].ToString();
			result_plan_date = row["result_plan_date"].ToString();
			test_result_yn = row["test_result_yn"].ToString();
			test_status = row["test_status"].ToString();
			test_status_nm = row["test_status_nm"].ToString();
			teststandardYandN = row["teststandardYandN"].ToString();
			lotorstart_no = row["lotorstart_no"].ToString();
			makerorprocess = row["makerorprocess"].ToString();
			pack_cnt = Convert.ToInt32(row["pack_cnt"]);
			start_qty_calc = Convert.ToDecimal(row["start_qty_calc"]);
			order_qty = row["order_qty"].ToString();
			receipt_no = row["receipt_no"].ToString();
			receipt_id = Convert.ToInt32(row["receipt_id"]);
			erp_lock_info = row["erp_lock_info"].ToString();
			erp_lot_end_date = row["erp_lot_end_date"].ToString();
			select_yn = row["select_yn"].ToString();
			pack_type = row["pack_type"].ToString();
			packing_qty = row["packing_qty"].ToString();
			all_test_check = row["all_test_check"].ToString();
			test_standard_1 = row["test_standard_1"].ToString();
			test_standard_2 = row["test_standard_2"].ToString();
			test_standard_3 = row["test_standard_3"].ToString();
			test_standard_4 = row["test_standard_4"].ToString();
			test_standard_5 = row["test_standard_5"].ToString();
			test_standard_nm_1 = row["test_standard_nm_1"].ToString();
			upload_yn = row["upload_yn"].ToString();
			upload_dt = Convert.ToDateTime(row["upload_dt"]);
			asepsis_item_ck = row["asepsis_item_ck"].ToString();
			hb_ck = row["hb_ck"].ToString();
		}
	}


}