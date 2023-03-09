using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HACCP.Models.PrevCk
{
	public class Schedule
	{

		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string s_schedule_date1 { get; set; }
			public string s_schedule_date2 { get; set; }
			public string s_obj_type { get; set; }
			public string s_work_type { get; set; }
			public string s_schedule_type { get; set; }
			public string s_obj_cd { get; set; }
			public string s_responsibility_worker { get; set; }
			public string s_dept_cd { get; set; }
			public string s_schedule_execution_ck { get; set; }
			//public string common_cd { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{

			}
		}

		/// <summary> 
		/// 2. 검색파라미터 property
		/// </summary> 
		public SrchParam srch;

		public string schedule_date { get; set; }
		public string obj_type { get; set; }
		public string work_type { get; set; }
		public string schedule_type { get; set; }
		public string obj_cd { get; set; }
		public string schedule_id { get; set; }
		public string schedule_input_gb { get; set; }
		public string standard_nm { get; set; }
		public string obj_nm { get; set; }
		public string sub_cd { get; set; }
		public string sub_nm { get; set; }
		public string work_yn { get; set; }
		public string auto_create_yn { get; set; }
		public string period { get; set; }
		//public string period_type { get; set; }
		public string responsibility_worker { get; set; }
		public string responsibility_worker_nm { get; set; }
		public string regist_yn { get; set; }
		public string regist_yn_nm { get; set; }
		public string dept_cd { get; set; }
		public string dept_nm { get; set; }
		public string gubun { get; set; }

		//public string workroom_nm { get; set; }
		//public string check_time1 { get; set; }
		//public string dept_nm2 { get; set; }
		//public string work_item { get; set; }
		//public string period_type_nm { get; set; }
		//public string comment { get; set; }
		//public string plan_doc_no { get; set; }
		//public string plan_doc_approve_date { get; set; }
		//public string result_doc_no { get; set; }
		//public string result_doc_approve_date { get; set; }
		//public string company_full_image { get; set; }
		//public string company_icon { get; set; }
		//public string form_no { get; set; }
		//public string plant_nm { get; set; }
		//public int seq { get; set; }
		//public string obj_type_nm { get; set; }
		//public string work_type_nm { get; set; }
		//public string remark { get; set; }
		//public string equip_prod_cust { get; set; }
		//public string equip_model_num { get; set; }
		//public string equip_serial_num { get; set; }

		public Schedule()
		{
		}

		public Schedule(DataRow row)
		{
			schedule_date = row["schedule_date"].ToString();
			obj_type = row["obj_type"].ToString();
			work_type = row["work_type"].ToString();
			schedule_type = row["schedule_type"].ToString();
			obj_cd = row["obj_cd"].ToString();
			schedule_id = row["schedule_id"].ToString();
			schedule_input_gb = row["schedule_input_gb"].ToString();
			standard_nm = row["standard_nm"].ToString();
			obj_nm = row["obj_nm"].ToString();
			sub_cd = row["sub_cd"].ToString();
			sub_nm = row["sub_nm"].ToString();
			work_yn = row["work_yn"].ToString();
			auto_create_yn = row["auto_create_yn"].ToString();
			period = row["period"].ToString();
			//period_type = row["period_type"].ToString();
			responsibility_worker = row["responsibility_worker"].ToString();
			responsibility_worker_nm = row["responsibility_worker_nm"].ToString();
			regist_yn = row["regist_yn"].ToString();
			regist_yn_nm = row["regist_yn_nm"].ToString();
			dept_cd = row["dept_cd"].ToString();
			dept_nm = row["dept_nm"].ToString();

			//한풍 SP 
			//workroom_nm = row["workroom_nm"].ToString();
			//check_time1 = row["check_time1"].ToString();
			//dept_nm2 = row["dept_nm2"].ToString();
			//work_item = row["work_item"].ToString();
			//period_type_nm = row["period_type_nm"].ToString();
			//comment = row["comment"].ToString();
			//plan_doc_no = row["plan_doc_no"].ToString();
			//plan_doc_approve_date = row["plan_doc_approve_date"].ToString();
			//result_doc_no = row["result_doc_no"].ToString();
			//result_doc_approve_date = row["result_doc_approve_date"].ToString();
			//company_full_image = "data:Image/png;base64," + Convert.ToBase64String((byte[])row["company_full_image"]);
			//company_icon = "data:Image/png;base64," + Convert.ToBase64String((byte[])row["company_icon"]);
			//form_no = row["form_no"].ToString();
			//plant_nm = row["plant_nm"].ToString();
			//seq = Int32.Parse(row["seq"].ToString());
			//obj_type_nm = row["obj_type_nm"].ToString();
			//work_type_nm = row["work_type_nm"].ToString();
			//remark = row["remark"].ToString();
			//equip_prod_cust = row["equip_prod_cust"].ToString();
			//equip_model_num = row["equip_model_num"].ToString();
			//equip_serial_num = row["equip_serial_num"].ToString();
		}
	}


}