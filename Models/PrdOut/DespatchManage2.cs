using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdOut
{
	public class DespatchManage2
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary> 
			/// 거래처 
			/// </summary> 
			public string s_cust_cd { get; set; }
			/// <summary> 
			///  출고일자 From
			/// </summary> 
			public string s_sdate { get; set; }
			/// <summary> 
			/// 출고일자 To 
			/// </summary> 
			public string s_edate { get; set; }
			/// <summary> 
			/// 상태
			/// </summary> 
			public string s_issue_status { get; set; }
			/// <summary> 
			///  
			/// </summary> 
			public string search_ck { get; set; }
			/// <summary> 
			/// 
			/// </summary> 
			public string s_sign_status { get; set; }
			/// <summary> 
			/// 
			/// </summary> 
			public string item_cd { get; set; }											
				
			// default 검색 생성자 
			public SrchParam()
			{
				this.s_cust_cd = "";
				this.s_sdate = DateTime.Today.AddMonths(-1).ToShortDateString();
				this.s_edate = DateTime.Today.ToShortDateString();
				this.s_issue_status = "%";
				this.search_ck = "0";
				this.s_sign_status = "3";
				this.item_cd = "";

			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string gubun { get; set; }
		public string row_status { get; set; }
		public string DESPATCH_ORDER_NO { get; set; }
		public string ISSUE_GB_CD { get; set; }
		public string ISSUE_GB { get; set; }
		public string ISSUE_DATE { get; set; }
		public string CUST_CD { get; set; }		
		public string CUST_NM { get; set; }
		public string CUST_AD1 { get; set; }
		public string PASS_CUST_CD { get; set; }
		public string PASS_CUST_NM { get; set; }
		public string PASS_CUST_AD1 { get; set; }
		public string issue_ck_cd { get; set; }
		public string issue_ck { get; set; }
		public string issue_chk { get; set; }
		public string ISSUE_TRANS_CUST_CD { get; set; }
		public string ISSUE_TRANS_GB { get; set; }
		public string issue_trans_gb_nm { get; set; }
		public string issue_trans_cust_nm { get; set; }
		public string ISSUE_DPS_DOWN_CK { get; set; }
		public string issue_work_date { get; set; }
		public string issue_work_Seq { get; set; }
		public string request_no { get; set; }
		public string pallet_print { get; set; }
		public string end_date_print { get; set; }
		public string TaxBillYn { get; set; }
		public string sign_status { get; set; }
		public string sign_status_nm { get; set; }
		public string ITEM_CD { get; set; }
		public string Issue_Price { get; set; }
		public string ISSUE_QTY { get; set; }
		public string item_nm { get; set; }
		public string RECEIPT_STATUS { get; set; }
		public string emp_nm { get; set; }
		public string release_type { get; set; }

		public string lot_no { get; set; }
		public string box_barcode_no { get; set; }
		public string box_qty { get; set; }
		public string keeping_status { get; set; }
		public string car_number { get; set; }
		public string issue_detail_date { get; set; }
		public string lot_date { get; set; }
		public string end_date { get; set; }
		public string item_issue_id { get; set; }
		public string despatch_no { get; set; }
		

		// 4. default 생성자 
		public DespatchManage2()
		{
		}

		// 5. DTO 설정
		public DespatchManage2(DataRow row)
		{
			row_status = row["row_status"].ToString();
			DESPATCH_ORDER_NO = row["DESPATCH_ORDER_NO"].ToString();
			ISSUE_GB_CD = row["ISSUE_GB_CD"].ToString();
			ISSUE_GB = row["ISSUE_GB"].ToString();
			ISSUE_DATE = row["ISSUE_DATE"].ToString();
			CUST_CD = row["CUST_CD"].ToString();
			CUST_NM = row["CUST_NM"].ToString();
			CUST_AD1 = row["CUST_AD1"].ToString();
			PASS_CUST_CD = row["PASS_CUST_CD"].ToString();
			PASS_CUST_NM = row["PASS_CUST_NM"].ToString();
			PASS_CUST_AD1 = row["PASS_CUST_AD1"].ToString();
			issue_ck_cd = row["issue_ck_cd"].ToString();
			issue_ck = row["issue_ck"].ToString();
			issue_chk = row["issue_chk"].ToString();
			ISSUE_TRANS_CUST_CD = row["ISSUE_TRANS_CUST_CD"].ToString();
			ISSUE_TRANS_GB = row["ISSUE_TRANS_GB"].ToString();
			issue_trans_gb_nm = row["issue_trans_gb_nm"].ToString();
			issue_trans_cust_nm = row["issue_trans_cust_nm"].ToString();
			ISSUE_DPS_DOWN_CK = row["ISSUE_DPS_DOWN_CK"].ToString();
			issue_work_date = row["issue_work_date"].ToString();
			issue_work_Seq = row["issue_work_Seq"].ToString();
			request_no = row["request_no"].ToString();
			pallet_print = row["pallet_print"].ToString();
			end_date_print = row["end_date_print"].ToString();
			TaxBillYn = row["TaxBillYn"].ToString();
			sign_status = row["sign_status"].ToString();
			sign_status_nm = row["sign_status_nm"].ToString();
			ITEM_CD = row["ITEM_CD"].ToString();
			Issue_Price = row["Issue_Price"].ToString();
			ISSUE_QTY = row["ISSUE_QTY"].ToString();
			item_nm = row["item_nm"].ToString();
			RECEIPT_STATUS = row["RECEIPT_STATUS"].ToString();
			emp_nm = row["emp_nm"].ToString();
			release_type = row["release_type"].ToString();
		}
	}


}