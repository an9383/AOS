using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrevCk
{
	public class WorkScheduleList
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
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string obj_nm { get; set; }
		public string work_method1 { get; set; }
		public string work_method2 { get; set; }
		public string work_method3 { get; set; }
		public string work_method4 { get; set; }
		public string work_method5 { get; set; }
		public string work_method6 { get; set; }
		public string schedule_result_manual_data1 { get; set; }
		public string schedule_result_manual_data2 { get; set; }
		public string schedule_result_manual_data3 { get; set; }
		public string schedule_result_manual_data4 { get; set; }
		public string schedule_result_manual_data5 { get; set; }
		public string schedule_result_manual_data6 { get; set; }

		// 4. default 생성자 
		public WorkScheduleList()
		{
		}

		// 5. DTO 설정
		public WorkScheduleList(DataRow row)
		{
			//row_status = row["row_status"].ToString();
			obj_nm = row["obj_nm"].ToString();
			work_method1 = row["work_method1"].ToString();
			work_method2 = row["work_method2"].ToString();
			work_method3 = row["work_method3"].ToString();
			work_method4 = row["work_method4"].ToString();
			work_method5 = row["work_method5"].ToString();
			work_method6 = row["work_method6"].ToString();
			schedule_result_manual_data1 = row["schedule_result_manual_data1"].ToString();
			schedule_result_manual_data2 = row["schedule_result_manual_data2"].ToString();
			schedule_result_manual_data3 = row["schedule_result_manual_data3"].ToString();
			schedule_result_manual_data4 = row["schedule_result_manual_data4"].ToString();
			schedule_result_manual_data5 = row["schedule_result_manual_data5"].ToString();
			schedule_result_manual_data6 = row["schedule_result_manual_data6"].ToString();
		}
	}


}