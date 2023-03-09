using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.TraceabilityManagement
{
	public class MaterialInformation
	{
		public string row_status { get; set; }
		public string gms_seq { get; set; }
		public string gms_item_cd { get; set; }
		public string gms_item_nm { get; set; }
		public string gms_material_cd { get; set; }
		public string gms_material_nm { get; set; }
		public string gms_qc_no { get; set; }
		public string gms_cust_cd { get; set; }
		public string gms_pdtlot_seq { get; set; }
		public string gms_item_stock_id { get; set; }
		public string reg_num { get; set; }
		public string food_histrace_num { get; set; }
		public string orm_std_cd { get; set; }
		public string orm_nm { get; set; }
		public string orm_nm_eng { get; set; }
		public string prv_natn_cd { get; set; }
		public string sup_business_reg_no { get; set; }
		public string sup_psn { get; set; }
		public string sup_psn_zipc { get; set; }
		public string sup_psn_addr1 { get; set; }
		public string sup_psn_addr2 { get; set; }
		public string gmo_yn { get; set; }
		public string remark { get; set; }
		public string send_status { get; set; }
		public string send_status_nm { get; set; }
		public string date_S { get; set; }
		public string ck { get; set; }
		public string temp_1 { get; set; }
		public string temp_2 { get; set; }
		public string temp_3 { get; set; }
		public string test_no { get; set; }

		// 4. default 생성자 
		public MaterialInformation()
		{
		}

		// 5. DTO 설정
		public MaterialInformation(DataRow row)
		{
			row_status = row["row_status"].ToString();
			gms_seq = row["gms_seq"].ToString();
			gms_item_cd = row["gms_item_cd"].ToString();
			gms_item_nm = row["gms_item_nm"].ToString();
			gms_material_cd = row["gms_material_cd"].ToString();
			gms_material_nm = row["gms_material_nm"].ToString();
			gms_qc_no = row["gms_qc_no"].ToString();
			gms_cust_cd = row["gms_cust_cd"].ToString();
			gms_pdtlot_seq = row["gms_pdtlot_seq"].ToString();
			gms_item_stock_id = row["gms_item_stock_id"].ToString();
			reg_num = row["reg_num"].ToString();
			food_histrace_num = row["food_histrace_num"].ToString();
			orm_std_cd = row["orm_std_cd"].ToString();
			orm_nm = row["orm_nm"].ToString();
			orm_nm_eng = row["orm_nm_eng"].ToString();
			prv_natn_cd = row["prv_natn_cd"].ToString();
			sup_business_reg_no = row["sup_business_reg_no"].ToString();
			sup_psn = row["sup_psn"].ToString();
			sup_psn_zipc = row["sup_psn_zipc"].ToString();
			sup_psn_addr1 = row["sup_psn_addr1"].ToString();
			sup_psn_addr2 = row["sup_psn_addr2"].ToString();
			gmo_yn = row["gmo_yn"].ToString();
			remark = row["remark"].ToString();
			send_status = row["send_status"].ToString();
			send_status_nm = row["send_status_nm"].ToString();
			date_S = row["date_S"].ToString();
			ck = row["ck"].ToString();
			temp_1 = row["temp_1"].ToString();
			temp_2 = row["temp_2"].ToString();
			temp_3 = row["temp_3"].ToString();
			test_no = row["test_no"].ToString();
		}
	}


}