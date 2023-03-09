using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.AdvancedPlanning
{
    public class PlanningOrderManage
    {

		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string start_date { get; set; }
			public string end_date { get; set; }
			public string order_request_item_cd { get; set; }
			public string order_request_item_nm { get; set; }

			public string cust_cd { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.start_date = DateTime.Now.AddDays(-1).ToShortDateString();
				this.end_date = DateTime.Now.ToShortDateString();
				this.order_request_item_cd = "";
				this.order_request_item_nm = "";
				this.cust_cd = "";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;


		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string order_request_no { get; set; }
		public string order_request_c_item_cd { get; set; }
		public string order_request_c_item_nm { get; set; }
		public string order_request_h_item_cd { get; set; }
		public string order_request_h_item_nm { get; set; }
		public string order_request_date { get; set; }
		public string cust_cd { get; set; }
		public string cust_nm { get; set; }
		public string sales_emp_cd { get; set; }
		public string sales_emp_nm { get; set; }
		public string order_request_qty { get; set; }
		public string order_request_h_qty { get; set; }
		public string loss_h_qty { get; set; }
		public string item_packunit { get; set; }
		public string item_unit { get; set; }
		public string order_request_price { get; set; }
		public string order_request_amt { get; set; }
		public string require_date { get; set; }
		public string remark { get; set; }
		public string m_loss { get; set; }
		public string p_loss { get; set; }
		public string c_qty { get; set; }
		public string ck_yn { get; set; }
		public string sign_status_nm { get; set; }
		public string sign_status { get; set; }
		public string m_bom_no { get; set; }
		public string p_bom_no { get; set; }
		public string m_bom_no_ck { get; set; }
		public string p_bom_no_ck { get; set; }
		public string add_delivery_ck { get; set; }

		// 4. default 생성자 
		public PlanningOrderManage()
		{
		}

		// 5. DTO 설정
		public PlanningOrderManage(DataRow row)
		{
			row_status = row["row_status"].ToString();
			order_request_no = row["order_request_no"].ToString();
			order_request_c_item_cd = row["order_request_c_item_cd"].ToString();
			order_request_c_item_nm = row["order_request_c_item_nm"].ToString();
			order_request_h_item_cd = row["order_request_h_item_cd"].ToString();
			order_request_h_item_nm = row["order_request_h_item_nm"].ToString();
			order_request_date = row["order_request_date"].ToString();
			cust_cd = row["cust_cd"].ToString();
			cust_nm = row["cust_nm"].ToString();
			sales_emp_cd = row["sales_emp_cd"].ToString();
			sales_emp_nm = row["sales_emp_nm"].ToString();
			order_request_qty = row["order_request_qty"].ToString();
			order_request_h_qty = row["order_request_h_qty"].ToString();
			item_packunit = row["item_packunit"].ToString();
			order_request_price = row["order_request_price"].ToString();
			order_request_amt = row["order_request_amt"].ToString();
			require_date = row["require_date"].ToString();
			remark = row["remark"].ToString();
			m_loss = row["m_loss"].ToString();
			p_loss = row["p_loss"].ToString();
			c_qty = row["c_qty"].ToString();
			ck_yn = row["ck_yn"].ToString();
			sign_status_nm = row["sign_status_nm"].ToString();
			sign_status = row["sign_status"].ToString();
			m_bom_no = row["m_bom_no"].ToString();
			p_bom_no = row["p_bom_no"].ToString();
			m_bom_no_ck = row["m_bom_no_ck"].ToString();
			p_bom_no_ck = row["p_bom_no_ck"].ToString();
			add_delivery_ck = row["add_delivery_ck"].ToString();
		}
	}
}