using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.TraceabilityManagement
{
	public class LotInformation
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
		public string item_nm { get; set; }
		public string gms_item_stock_id { get; set; }
		public string gms_receipt_date { get; set; }
		public string reg_num { get; set; }
		public string food_histrace_num { get; set; }
		public string mnft_day { get; set; }
		public string lot_num { get; set; }
		public string crcl_prd_day { get; set; }
		public string imcm_yn { get; set; }
		public string mnft_fact { get; set; }
		public string mnft_fact_addr { get; set; }
		public string incm_decl_ogn { get; set; }
		public string incm_decl_num { get; set; }
		public string mnft_prv { get; set; }
		public string gmo_yn { get; set; }
		public string prod_qty { get; set; }
		public string prod_qty_unt { get; set; }
		public string piaw_dt { get; set; }
		public string remark { get; set; }
		public string ck { get; set; }
		public string send_status { get; set; }
		public string send_status_nm { get; set; }
		public string date_S { get; set; }
		public string temp_1 { get; set; }
		public string temp_2 { get; set; }
		public string temp_3 { get; set; }
		public string confirm_emp_cd { get; set; }
		public string confirm_emp_nm { get; set; }
		public string confirm_emp_time { get; set; }

		// 4. default 생성자 
		public LotInformation()
		{
		}

		// 5. DTO 설정
		public LotInformation(DataRow row)
		{
			row_status = row["row_status"].ToString();
			gms_seq = row["gms_seq"].ToString();
			gms_item_cd = row["gms_item_cd"].ToString();
			item_nm = row["item_nm"].ToString();
			gms_item_stock_id = row["gms_item_stock_id"].ToString();
			gms_receipt_date = row["gms_receipt_date"].ToString();
			reg_num = row["reg_num"].ToString();
			food_histrace_num = row["food_histrace_num"].ToString();
			mnft_day = row["mnft_day"].ToString();
			lot_num = row["lot_num"].ToString();
			crcl_prd_day = row["crcl_prd_day"].ToString();
			imcm_yn = row["imcm_yn"].ToString();
			mnft_fact = row["mnft_fact"].ToString();
			mnft_fact_addr = row["mnft_fact_addr"].ToString();
			incm_decl_ogn = row["incm_decl_ogn"].ToString();
			incm_decl_num = row["incm_decl_num"].ToString();
			mnft_prv = row["mnft_prv"].ToString();
			gmo_yn = row["gmo_yn"].ToString();
			prod_qty = row["prod_qty"].ToString();
			prod_qty_unt = row["prod_qty_unt"].ToString();
			piaw_dt = row["piaw_dt"].ToString();
			remark = row["remark"].ToString();
			ck = row["ck"].ToString();
			send_status = row["send_status"].ToString();
			send_status_nm = row["send_status_nm"].ToString();
			date_S = row["date_S"].ToString();
			temp_1 = row["temp_1"].ToString();
			temp_2 = row["temp_2"].ToString();
			temp_3 = row["temp_3"].ToString();
			confirm_emp_cd = row["confirm_emp_cd"].ToString();
			confirm_emp_nm = row["confirm_emp_nm"].ToString();
			confirm_emp_time = row["confirm_emp_time"].ToString();
		}
	}


}