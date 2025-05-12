using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.Cp
{
	public class ClaimRequest
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
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string lot_no { get; set; }
		public string cust_nm { get; set; }
		public string cust_cd { get; set; }
		public string claim_date { get; set; }
		public string claim_nm { get; set; }
		public string claim_content { get; set; }
		public string claim_emp_nm { get; set; }
		public string claim_emp_cd { get; set; }
		public string claim_status_nm { get; set; }
		public string claim_status { get; set; }
		public string inform_date { get; set; }
		public string claim_address { get; set; }
		public string claim_phone_no { get; set; }
		public string etc_qty { get; set; }
		public string etc_remark { get; set; }
		public string claim_information { get; set; }
		public string claim_response { get; set; }
		public string claim_hope { get; set; }
		public string time_limit { get; set; }
		public string attach_yn_nm { get; set; }
		public string attach_yn { get; set; }
		public string claim_item { get; set; }
		public string relation_dept_cd { get; set; }
		public string relation_dept_nm { get; set; }
		public string serial_no { get; set; }
		public string circulation_period { get; set; }
		public string product_line { get; set; }
		public string product_time { get; set; }
		public string spot_recall_date { get; set; }
		public string company_recall_date { get; set; }
		public string recall_quantity { get; set; }
		public string open_or_not { get; set; }
		public string first_form { get; set; }
		public string recall_form { get; set; }
		public string substance_kind { get; set; }
		public string substance_form { get; set; }
		public string substance_discovery { get; set; }
		public string substance_keeping_environment { get; set; }

		// 4. default 생성자 
		public ClaimRequest()
		{
		}

		// 5. DTO 설정
		public ClaimRequest(DataRow row)
		{
			//row_status = row["row_status"].ToString();
			claim_id = Int32.Parse(row["claim_id"].ToString());
			claim_no = row["claim_no"].ToString();
			item_cd = row["item_cd"].ToString();
			item_nm = row["item_nm"].ToString();
			lot_no = row["lot_no"].ToString();
			cust_nm = row["cust_nm"].ToString();
			cust_cd = row["cust_cd"].ToString();
			claim_date = row["claim_date"].ToString();
			claim_nm = row["claim_nm"].ToString();
			claim_content = row["claim_content"].ToString();
			claim_emp_nm = row["claim_emp_nm"].ToString();
			claim_emp_cd = row["claim_emp_cd"].ToString();
			claim_status_nm = row["claim_status_nm"].ToString();
			claim_status = row["claim_status"].ToString();
			inform_date = row["inform_date"].ToString();
			claim_address = row["claim_address"].ToString();
			claim_phone_no = row["claim_phone_no"].ToString();
			etc_qty = row["etc_qty"].ToString();
			etc_remark = row["etc_remark"].ToString();
			claim_information = row["claim_information"].ToString();
			claim_response = row["claim_response"].ToString();
			claim_hope = row["claim_hope"].ToString();
			time_limit = row["time_limit"].ToString();
			attach_yn_nm = row["attach_yn_nm"].ToString();
			attach_yn = row["attach_yn"].ToString();
			claim_item = row["claim_item"].ToString();
			relation_dept_cd = row["relation_dept_cd"].ToString();
			relation_dept_nm = row["relation_dept_nm"].ToString();
			serial_no = row["serial_no"].ToString();
			circulation_period = row["circulation_period"].ToString();
			product_line = row["product_line"].ToString();
			product_time = row["product_time"].ToString();
			spot_recall_date = row["spot_recall_date"].ToString();
			company_recall_date = row["company_recall_date"].ToString();
			recall_quantity = row["recall_quantity"].ToString();
			open_or_not = row["open_or_not"].ToString();
			first_form = row["first_form"].ToString();
			recall_form = row["recall_form"].ToString();
			substance_kind = row["substance_kind"].ToString();
			substance_form = row["substance_form"].ToString();
			substance_discovery = row["substance_discovery"].ToString();
			substance_keeping_environment = row["substance_keeping_environment"].ToString();
		}
	}


}