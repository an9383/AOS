using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PackingManage
{
	public class PackingOrderLedger
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary>
			/// 지시일자 From
			/// </summary>
			public string SearchDate { get; set; }
			/// <summary>
			/// 구분
			/// </summary>
			public string MakingDeptCd { get; set; }
			/// <summary> 
			/// 상태
			/// </summary> 
			public string MakingDeptCd2 { get; set; }
			/// <summary>
			/// 
			/// </summary>
			public string sign_status_s { get; set; }
			/// <summary>
			/// 지시일자 To
			/// </summary>
			public string SearchEndDate { get; set; }
			/// <summary>
			/// 제품
			/// </summary>
			public string ItemCd { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.SearchDate = DateTime.Now.AddDays(-DateTime.Now.Day+1).ToShortDateString();
				this.MakingDeptCd = "%";
				this.MakingDeptCd2 = "%";
				this.sign_status_s = "%";
				this.SearchEndDate = DateTime.Now.ToShortDateString();
				this.ItemCd = "";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		//3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		///  
		public string order_id { get; set; }
		public string order_date { get; set; }
		public string making_dept_cd { get; set; }
		public string making_dept_nm { get; set; }
		public string making_dept_cd2 { get; set; }
		public string making_dept_nm2 { get; set; }
		public string order_check_emp1 { get; set; }
		public string order_check_emp_nm1 { get; set; }
		public string order_check_date1 { get; set; }
		public string order_check_type1 { get; set; }
		public string order_check_emp2 { get; set; }
		public string order_check_emp_nm2 { get; set; }
		public string order_check_date2 { get; set; }
		public string order_check_type2 { get; set; }
		public string order_check_emp3 { get; set; }
		public string order_check_emp_nm3 { get; set; }
		public string order_check_date3 { get; set; }
		public string order_check_type3 { get; set; }
		public string sign_status { get; set; }
		public string sign_status_nm { get; set; }
		public string order_no { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string lot_no { get; set; }
		public string order_qty { get; set; }
		public string order_bigo { get; set; }
		public string order_yn { get; set; }
		public string order_status_nm { get; set; }
		public string lot_date { get; set; }
		public string pb_date { get; set; }
		public string item_pack_size { get; set; }
		public string order_batch_size { get; set; }
		public string valid_date { get; set; }
		public string new_ck { get; set; }
		public string order_no_M { get; set; }
		public string item_pack_size_batch { get; set; }
		public string order_batch_size_batch { get; set; }
		public string order_request_no { get; set; }
		public string revision_no { get; set; }
		public string set_yn { get; set; }
		public string Multi_Yn { get; set; }
		public string gubun { get; set; }

		// 4. default 생성자
		public PackingOrderLedger()
		{
		}

		// 5. DTO 설정
		public PackingOrderLedger(DataRow row)
		{
			order_id = row["order_id"].ToString();
			order_date = row["order_date"].ToString();
			making_dept_cd = row["making_dept_cd"].ToString();
			making_dept_nm = row["making_dept_nm"].ToString();
			making_dept_cd2 = row["making_dept_cd2"].ToString();
			making_dept_nm2 = row["making_dept_nm2"].ToString();
			order_check_emp1 = row["order_check_emp1"].ToString();
			order_check_emp_nm1 = row["order_check_emp_nm1"].ToString();
			order_check_date1 = row["order_check_date1"].ToString();
			order_check_type1 = row["order_check_type1"].ToString();
			order_check_emp2 = row["order_check_emp2"].ToString();
			order_check_emp_nm2 = row["order_check_emp_nm2"].ToString();
			order_check_date2 = row["order_check_date2"].ToString();
			order_check_type2 = row["order_check_type2"].ToString();
			order_check_emp3 = row["order_check_emp3"].ToString();
			order_check_emp_nm3 = row["order_check_emp_nm3"].ToString();
			order_check_date3 = row["order_check_date3"].ToString();
			order_check_type3 = row["order_check_type3"].ToString();
			sign_status = row["sign_status"].ToString();
			sign_status_nm = row["sign_status_nm"].ToString();
			order_no = row["order_no"].ToString();
			item_cd = row["item_cd"].ToString();
			item_nm = row["item_nm"].ToString();
			lot_no = row["lot_no"].ToString();
			order_qty = row["order_qty"].ToString();
			order_bigo = row["order_bigo"].ToString();
			order_yn = row["order_yn"].ToString();
			order_status_nm = row["order_status_nm"].ToString();
			lot_date = row["lot_date"].ToString();
			pb_date = row["pb_date"].ToString();
			item_pack_size = row["item_pack_size"].ToString();
			order_batch_size = row["order_batch_size"].ToString();
			valid_date = row["valid_date"].ToString();
			new_ck = row["new_ck"].ToString();
			order_no_M = row["order_no_M"].ToString();
			item_pack_size_batch = row["item_pack_size_batch"].ToString();
			order_batch_size_batch = row["order_batch_size_batch"].ToString();
			order_request_no = row["order_request_no"].ToString();
			revision_no = row["revision_no"].ToString();
			set_yn = row["set_yn"].ToString();
			Multi_Yn = row["Multi_Yn"].ToString();
		}
	}
}