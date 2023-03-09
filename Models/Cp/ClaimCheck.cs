using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.Cp
{
	public class ClaimCheck
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string select_S { get; set; }
			public string Sdate_S { get; set; }
			public string Edate_S { get; set; }
			public string select_gubun_S { get; set; }
			public string searchtext_S { get; set; }
			public string claim_status_S { get; set; }

		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public int claim_id { get; set; }
		public string claim_no { get; set; }
		public string item_nm { get; set; }
		public string lot_no { get; set; }
		public string cust_nm { get; set; }
		public string claim_date { get; set; }
		public string claim_content { get; set; }
		public string claim_emp_nm { get; set; }
		public string receive_date { get; set; }
		public string receive_emp_nm { get; set; }
		public string claim_title { get; set; }
		public string result_opinion { get; set; }
		public string plan_opinion { get; set; }
		public string judge_date { get; set; }
		public string judge_emp_cd { get; set; }
		public string judge_emp_nm { get; set; }
		public string judge_type { get; set; }
		public string judge_type_nm { get; set; }
		public string claim_type { get; set; }
		public string claim_type_nm { get; set; }
		public string judge_opinion { get; set; }
		public string result_file1 { get; set; }
		public string result_file1_nm { get; set; }
		public string result_file2 { get; set; }
		public string result_file2_nm { get; set; }
		public string dept_cd1 { get; set; }
		public string dept_nm1 { get; set; }
		public string dept_cd2 { get; set; }
		public string dept_nm2 { get; set; }
		public string claim_status_nm { get; set; }
		public string claim_status { get; set; }

		// 4. default 생성자 
		public ClaimCheck()
		{
		}

		// 5. DTO 설정
		public ClaimCheck(DataRow row)
		{
			//row_status = row["row_status"].ToString();
			claim_id = Int32.Parse(row["claim_id"].ToString());
			claim_no = row["claim_no"].ToString();
			item_nm = row["item_nm"].ToString();
			lot_no = row["lot_no"].ToString();
			cust_nm = row["cust_nm"].ToString();
			claim_date = row["claim_date"].ToString();
			claim_content = row["claim_content"].ToString();
			claim_emp_nm = row["claim_emp_nm"].ToString();
			receive_date = row["receive_date"].ToString();
			receive_emp_nm = row["receive_emp_nm"].ToString();
			claim_title = row["claim_title"].ToString();
			result_opinion = row["result_opinion"].ToString();
			plan_opinion = row["plan_opinion"].ToString();
			judge_date = row["judge_date"].ToString();
			judge_emp_cd = row["judge_emp_cd"].ToString();
			judge_emp_nm = row["judge_emp_nm"].ToString();
			judge_type = row["judge_type"].ToString();
			judge_type_nm = row["judge_type_nm"].ToString();
			claim_type = row["claim_type"].ToString();
			claim_type_nm = row["claim_type_nm"].ToString();
			judge_opinion = row["judge_opinion"].ToString();
			result_file1 = row["result_file1"].ToString();
			result_file1_nm = row["result_file1_nm"].ToString();
			result_file2 = row["result_file2"].ToString();
			result_file2_nm = row["result_file2_nm"].ToString();
			dept_cd1 = row["dept_cd1"].ToString();
			dept_nm1 = row["dept_nm1"].ToString();
			dept_cd2 = row["dept_cd2"].ToString();
			dept_nm2 = row["dept_nm2"].ToString();
			claim_status_nm = row["claim_status_nm"].ToString();
			claim_status = row["claim_status"].ToString();
		}
	}


}