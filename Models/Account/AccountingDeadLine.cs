using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Account
{
    public class AccountingDeadLine
    {

		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string sdate { get; set; }
			public string edate { get; set; }
			public string b_status { get; set; }
			public string gu { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.sdate = DateTime.Now.AddYears(-1).ToShortDateString();
				this.edate = DateTime.Now.ToShortDateString();
				this.b_status = "%";
				this.gu = "매입";

			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string gu { get; set; }
		public string part_cd { get; set; }
		public string part_nm { get; set; }
		public string b_no { get; set; }
		public string cust_cd { get; set; }
		public string cust_nm { get; set; }
		public string b_date { get; set; }
		public string b_qty { get; set; }
		public string b_price { get; set; }
		public string b_amt { get; set; }
		public string b_status_cd { get; set; }
		public string b_status_nm { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string i_no { get; set; }
		public string i_date { get; set; }
		public string i_qty { get; set; }
		public string i_amt { get; set; }
		public string b_status { get; set; }
		public string cnt_status { get; set; }
		public string itf_completed { get; set; }
		public string n_no { get; set; }
		public string final_yn { get; set; }
		public string taxfree_yn { get; set; }

		public string acct_dt { get; set; }

		// 4. default 생성자 
		public AccountingDeadLine()
		{
		}

		// 5. DTO 설정
		//public AccountingDeadLine(DataRow row)
		//{
		//	row_status = row["row_status"].ToString();
		//	gu = row["gu"].ToString();
		//	part_cd = row["part_cd"].ToString();
		//	part_nm = row["part_nm"].ToString();
		//	b_no = row["b_no"].ToString();
		//	cust_cd = row["cust_cd"].ToString();
		//	cust_nm = row["cust_nm"].ToString();
		//	b_date = row["b_date"].ToString();
		//	b_qty = row["b_qty"].ToString();
		//	b_price = row["b_price"].ToString();
		//	b_amt = row["b_amt"].ToString();
		//	b_status_cd = row["b_status_cd"].ToString();
		//	b_status_nm = row["b_status_nm"].ToString();
		//	item_cd = row["item_cd"].ToString();
		//	item_nm = row["item_nm"].ToString();
		//	i_no = row["i_no"].ToString();
		//	i_date = row["i_date"].ToString();
		//	i_qty = row["i_qty"].ToString();
		//	i_amt = row["i_amt"].ToString();
		//	b_status = row["b_status"].ToString();
		//	cnt_status = row["cnt_status"].ToString();
		//	itf_completed = row["itf_completed"].ToString();
		//}
	}
}