using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.Equipment
{
	/// <summary>
	/// PartRegister Main
	/// </summary>
	public class PartsInOutRegi
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
			/// 입출고구분(1:입고, 2:출고)
			/// </summary> 			
			public string S_inOut { get; set; }

			/// <summary>
			/// 검색시작일
			/// </summary>
			public string S_sdate { get; set; }

			/// <summary>
			/// 검색종료일
			/// </summary>
			public string S_edate { get; set; }

			/// <summary>
			/// 부품코드
			/// </summary>
			public string S_part { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.Gubun = "S";
				this.S_inOut = "%";
				this.S_sdate = new DateTime(2019, 1, 1).ToShortDateString();
				this.S_edate = DateTime.Now.ToShortDateString();
				this.S_part = String.Empty;
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public int part_history_id { get; set; }
		public string part_cd { get; set; }
		public string part_nm { get; set; }
		public string part_inout_date { get; set; }
		public decimal part_history_qty { get; set; }
		public decimal part_history_price { get; set; }
		public string part_gb { get; set; }
		public string part_gb_nm { get; set; }
		public string part_history_emp_cd { get; set; }
		public string part_history_emp_nm { get; set; }
		public string part_history_status { get; set; }
		public string part_history_status_nm { get; set; }
		public string part_buy_cust_cd { get; set; }
		public string part_buy_cust_nm { get; set; }
		public string part_prod_cust_cd { get; set; }
		public string part_prod_cust_nm { get; set; }
		public string part_prod_date { get; set; }
		public string part_prod_num { get; set; }
		public string part_history_remark { get; set; }
		public decimal? cost { get; set; }

		// 4. default 생성자 
		public PartsInOutRegi()
		{
		}

		// 5. DTO 설정
		public PartsInOutRegi(DataRow row)
		{
			row_status = row["row_status"].ToString();
			part_history_id = Convert.ToInt32(row["part_history_id"]);
			part_cd = row["part_cd"].ToString();
			part_nm = row["part_nm"].ToString();
			part_inout_date = row["part_inout_date"].ToString();
			part_history_qty = Convert.ToDecimal(row["part_history_qty"]);
			part_history_price = Convert.ToDecimal(row["part_history_price"]);
			part_gb = row["part_gb"].ToString();
			part_gb_nm = row["part_gb_nm"].ToString();
			part_history_emp_cd = row["part_history_emp_cd"].ToString();
			part_history_emp_nm = row["part_history_emp_nm"].ToString();
			part_history_status = row["part_history_status"].ToString();
			part_history_status_nm = row["part_history_status_nm"].ToString();
			part_buy_cust_cd = row["part_buy_cust_cd"].ToString();
			part_buy_cust_nm = row["part_buy_cust_nm"].ToString();
			part_prod_cust_cd = row["part_prod_cust_cd"].ToString();
			part_prod_cust_nm = row["part_prod_cust_nm"].ToString();
			part_prod_date = row["part_prod_date"].ToString();
			part_prod_num = row["part_prod_num"].ToString();
			part_history_remark = row["part_history_remark"].ToString();
			cost = Convert.ToDecimal(row["cost"]);
		}
	}


}