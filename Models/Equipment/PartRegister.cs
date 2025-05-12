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
	public class PartRegister
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary>
			///  SP 검색구분
			/// </summary>
			public string Gubun { get; set; }

			/// <summary>
			/// 설비
			/// </summary>
			public string S_equipment { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.Gubun = "S";
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
		public string equip_cd { get; set; }
		public string equip_nm { get; set; }

		// 4. default 생성자 
		public PartRegister()
		{
		}

		// 5. DTO 설정
		public PartRegister(DataRow row)
		{
			row_status = row["row_status"].ToString();
			equip_cd = row["equip_cd"].ToString();
			equip_nm = row["equip_nm"].ToString();
		}
	}

	
	/// <summary>
	/// PartRegister sub
	/// </summary>
	public class PartRegisterEqupPart
	{
		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string part_cd { get; set; }
		public string part_nm { get; set; }
		public string part_enm { get; set; }
		public string part_kind { get; set; }
		public string charge_emp_cd { get; set; }
		public string charge_emp_nm { get; set; }
		public decimal? stock_qty { get; set; }
		public string part_shape { get; set; }

		// 4. default 생성자 
		public PartRegisterEqupPart()
		{
		}

		// 5. DTO 설정
		public PartRegisterEqupPart(DataRow row)
		{
			row_status = row["row_status"].ToString();
			part_cd = row["part_cd"].ToString();
			part_nm = row["part_nm"].ToString();
			part_enm = row["part_enm"].ToString();
			part_kind = row["part_kind"].ToString();
			charge_emp_cd = row["charge_emp_cd"].ToString();
			charge_emp_nm = row["charge_emp_nm"].ToString();
			stock_qty = Convert.ToDecimal(row["stock_qty"].ToString());
			part_shape = row["part_shape"].ToString();
		}
	}


}