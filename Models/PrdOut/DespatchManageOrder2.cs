using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdOut
{
	public class DespatchManageOrder2
	{
		public string s_cust_cd { get; set; }				//거래처코드
		public string s_sdate { get; set; }					//시작일자
		public string s_edate { get; set; }					//끝일자
		public string s_issue_status { get; set; }			//출고상태
		public string search_ck { get; set; }
		public string s_sign_status { get; set; }
		public string item_cd { get; set; }					//제품코드
		public string despatch_no { get; set; }				
		public string gubun { get; set; }


		//3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		///  
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
		//public string ITEM_CD { get; set; }		//검색조건에 'item_cd' 가 존재해서 충돌남.
		public string Issue_Price { get; set; }		
		public string Issue_Price2 { get; set; }
		public string ISSUE_QTY { get; set; }
		public string item_nm { get; set; }
		public string RECEIPT_STATUS { get; set; }
		public string emp_nm { get; set; }
		public string release_type { get; set; }


		public string lot_no { get; set; }
		public string lot_date { get; set; }
		public string end_date { get; set; }
		public string item_issue_id { get; set; }
		public string box_barcode_no { get; set; }
		public string box_qty { get; set; }
		public string keeping_status { get; set; }


		//4. default 생성자
		public DespatchManageOrder2()
		{
		}

	}


}