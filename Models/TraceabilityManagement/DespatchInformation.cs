using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.TraceabilityManagement
{
	public class DespatchInformation
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
			/// Param1 
			/// </summary> 
			public string create_date_S { get; set; }
			/// <summary> 
			/// Param2 
			/// </summary> 
			public string create_date_E { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.Gubun = "S";
				this.create_date_S = DateTime.Now.AddDays(-7).ToShortDateString();
				this.create_date_E = DateTime.Now.ToShortDateString();
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string gms_seq { get; set; }
		public string gms_item_cd { get; set; }
		public string gms_item_nm { get; set; }
		public string gms_lot_no { get; set; }
		public string gms_pdtlot_seq { get; set; }
		public string gms_cust_cd { get; set; }
		public string gms_despatch_order_no { get; set; }
		public string gms_item_issue_id { get; set; }
		public string tgow_dt { get; set; }
		public string reg_num { get; set; }
		public string food_histrace_num { get; set; }
		public string to_pst_nm { get; set; }
		public string to_pst_cd { get; set; }
		public string to_pst_addr { get; set; }
		public string tgow_unt { get; set; }
		public string qty { get; set; }
		public string remark { get; set; }
		public string temp_1 { get; set; }
		public string temp_2 { get; set; }
		public string temp_3 { get; set; }
		public string send_status { get; set; }
		public string send_status_nm { get; set; }
		public string date_S { get; set; }
		public string ck { get; set; }
		public string confirm_emp_cd { get; set; }
		public string confirm_emp_nm { get; set; }
		public string confirm_emp_time { get; set; }
		public string to_pst_lcns_no { get; set; }

		// 4. default 생성자 
		public DespatchInformation()
		{
		}

        // 5. DTO 설정
        public DespatchInformation(DataRow row)
        {
            row_status = row["row_status"].ToString();
            gms_seq = row["gms_seq"].ToString();
            gms_item_cd = row["gms_item_cd"].ToString();
            gms_item_nm = row["gms_item_nm"].ToString();
            gms_lot_no = row["gms_lot_no"].ToString();
            gms_pdtlot_seq = row["gms_pdtlot_seq"].ToString();
            gms_cust_cd = row["gms_cust_cd"].ToString();
            gms_despatch_order_no = row["gms_despatch_order_no"].ToString();
            gms_item_issue_id = row["gms_item_issue_id"].ToString();
            tgow_dt = row["tgow_dt"].ToString();
            reg_num = row["reg_num"].ToString();
            food_histrace_num = row["food_histrace_num"].ToString();
            to_pst_nm = row["to_pst_nm"].ToString();
            to_pst_cd = row["to_pst_cd"].ToString();
            to_pst_addr = row["to_pst_addr"].ToString();
            tgow_unt = row["tgow_unt"].ToString();
            qty = row["qty"].ToString();
            remark = row["remark"].ToString();
            temp_1 = row["temp_1"].ToString();
            temp_2 = row["temp_2"].ToString();
            temp_3 = row["temp_3"].ToString();
            send_status = row["send_status"].ToString();
            send_status_nm = row["send_status_nm"].ToString();
            date_S = row["date_S"].ToString();
            ck = row["ck"].ToString();
            confirm_emp_cd = row["confirm_emp_cd"].ToString();
            confirm_emp_nm = row["confirm_emp_nm"].ToString();
            confirm_emp_time = row["confirm_emp_time"].ToString();
			to_pst_lcns_no = row["to_pst_lcns_no"].ToString();
		}

	}


}