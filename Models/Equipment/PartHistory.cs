using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.Equipment
{
	/// <summary>
	///  PartHistory Main
	/// </summary>
	public class PartHistory
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
			/// 설비종류
			/// </summary> 
			public string S_equip_type { get; set; }
			/// <summary> 
			/// 설비 
			/// </summary> 
			public string S_equipment { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.Gubun = "S";
				this.S_equip_type = "%";
				this.S_equipment = string.Empty;
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string equip_manage_num { get; set; }
		public string equip_cd { get; set; }
		public string equip_nm { get; set; }
		public string equip_enm { get; set; }
		public string equip_response_emp_nm { get; set; }
		public string equip_type { get; set; }
		public string equip_type_nm { get; set; }
		public string equip_buy_date { get; set; }
		public string equip_serial_num { get; set; }
		public string equip_prod_cust { get; set; }

		// 4. default 생성자 
		public PartHistory()
		{
		}

		// 5. DTO 설정
		public PartHistory(DataRow row)
		{
			row_status = row["row_status"].ToString();
			equip_manage_num = row["equip_manage_num"].ToString();
			equip_cd = row["equip_cd"].ToString();
			equip_nm = row["equip_nm"].ToString();
			equip_enm = row["equip_enm"].ToString();
			equip_response_emp_nm = row["equip_response_emp_nm"].ToString();
			equip_type = row["equip_type"].ToString();
			equip_type_nm = row["equip_type_nm"].ToString();
			equip_buy_date = row["equip_buy_date"].ToString();
			equip_serial_num = row["equip_serial_num"].ToString();
			equip_prod_cust = row["equip_prod_cust"].ToString();
		}
	}

	/// <summary>
	/// 예방점검 이력
	/// </summary>
	public class PartHistorySS
	{
		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string equip_cd { get; set; }
		public string schedule_date { get; set; }
		public string work_type_nm { get; set; }
		public string schedule_type_nm { get; set; }
		public int? amt { get; set; }
		public string emp_nm { get; set; }

		// 4. default 생성자 
		public PartHistorySS()
		{
		}

		// 5. DTO 설정
		public PartHistorySS(DataRow row)
		{
			row_status = row["row_status"].ToString();
			equip_cd = row["equip_cd"].ToString();
			schedule_date = row["schedule_date"].ToString();
			work_type_nm = row["work_type_nm"].ToString();
			schedule_type_nm = row["schedule_type_nm"].ToString();
			amt = Convert.ToInt32(row["amt"]);
			emp_nm = row["emp_nm"].ToString();
		}
	}

	/// <summary>
	/// 고장수리이력
	/// </summary>
	public class PartHistorySP
	{
		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string equip_cd { get; set; }
		public string repair_date { get; set; }
		public string work_type_nm { get; set; }
		public string error_content { get; set; }
		public decimal? repair_price { get; set; }
		public string emp_nm { get; set; }

		// 4. default 생성자 
		public PartHistorySP()
		{
		}

		// 5. DTO 설정
		public PartHistorySP(DataRow row)
		{
			row_status = row["row_status"].ToString();
			equip_cd = row["equip_cd"].ToString();
			repair_date = row["repair_date"].ToString();
			work_type_nm = row["work_type_nm"].ToString();
			error_content = row["error_content"].ToString();
			repair_price = Convert.ToInt32(row["repair_price"]);
			emp_nm = row["emp_nm"].ToString();
		}
	}
}