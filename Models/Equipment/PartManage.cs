using DevExpress.XtraRichEdit.Import.OpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.Equipment
{
	/// <summary>
	/// PartManage Main
	/// </summary>
	public class PartManage
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
			/// 부품종류 
			/// </summary> 
			public string S_part_kind { get; set; }

			/// <summary> 
			/// 사용여부 (전체:%, 사용:1, 미사용:2)
			/// </summary> 
			public string S_use_gb { get; set; }

			/// <summary>
			/// 부품코드
			/// </summary>
            public string S_part { get; set; }

            // default 검색 생성자 
            public SrchParam()
			{
				this.Gubun = "S";
				this.S_part_kind = "%";
				this.S_use_gb = "%";
				this.S_part = string.Empty;
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 		
		public string row_status { get; set; }
		public string part_cd { get; set; }
		public string part_nm { get; set; }
		public string part_enm { get; set; }
		public string part_kind { get; set; }
		public string part_kind_cd { get; set; }
		public string part_response_emp_cd { get; set; }
		public string part_response_emp_nm { get; set; }
		public string use_gb { get; set; }
		public string use_gb_cd { get; set; }
		public decimal? part_buy_amt { get; set; }
		public string part_in_date { get; set; }
		public string part_shape { get; set; }
		public decimal? part_stock_qty { get; set; }

		// 4. default 생성자 
		public PartManage()
		{
		}

		// 5. DTO 설정
		public PartManage(DataRow row)
		{
			row_status = row["row_status"].ToString();
			part_cd = row["part_cd"].ToString();
			part_nm = row["part_nm"].ToString();
			part_enm = row["part_enm"].ToString();
			part_kind = row["part_kind"].ToString();
			part_kind_cd = row["part_kind_cd"].ToString();
			part_response_emp_cd = row["part_response_emp_cd"].ToString();
			part_response_emp_nm = row["part_response_emp_nm"].ToString();
			use_gb = row["use_gb"].ToString();
			use_gb_cd = row["use_gb_cd"].ToString();
			part_buy_amt = Convert.ToDecimal(row["part_buy_amt"]);
			part_in_date = row["part_in_date"].ToString();
			part_shape = row["part_shape"].ToString();
			part_stock_qty = Convert.ToDecimal(row["part_stock_qty"]);
		}
	}

	/// <summary>
	/// PartManage Sub
	/// </summary>
	public class PartManageUseEquip
	{
		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string equip_cd { get; set; }
		public string equip_nm { get; set; }

		// 4. default 생성자 
		public PartManageUseEquip()
		{
		}

		// 5. DTO 설정
		public PartManageUseEquip(DataRow row)
		{
			row_status = row["row_status"].ToString();
			equip_cd = row["equip_cd"].ToString();
			equip_nm = row["equip_nm"].ToString();
		}
	}

}