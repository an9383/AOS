using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrevCk
{
	public class DayScheduleList
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
			public string obj_cd { get; set; }
			public int schedule_id { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{

			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string gubun { get; set; }
		public int schedule_id { get; set; }
		public string obj_type_nm { get; set; }
		public string obj_cd { get; set; }
		public string obj_nm { get; set; }
		public string obj_cd2 { get; set; }
		public string work_type_nm { get; set; }
		public string schedule_type_nm { get; set; }
		public string schedule_date { get; set; }
		public string check_time { get; set; }
		public string check_emp_nm1 { get; set; }
		public string check_emp_nm2 { get; set; }
		public string comment { get; set; }
		public string work_seq { get; set; }
		public string schedule_result_manual_data { get; set; }

		// 4. default 생성자 
		public DayScheduleList()
		{
		}

		// 5. DTO 설정
		public DayScheduleList(DataRow row)
		{
			//row_status = row["row_status"].ToString();
			schedule_id = Int32.Parse(row["schedule_id"].ToString());
			obj_type_nm = row["obj_type_nm"].ToString();
			obj_cd = row["obj_cd"].ToString();
			obj_nm = row["obj_nm"].ToString();
			obj_cd2 = row["obj_cd2"].ToString();
			work_type_nm = row["work_type_nm"].ToString();
			schedule_type_nm = row["schedule_type_nm"].ToString();
			schedule_date = row["schedule_date"].ToString();
			check_time = row["check_time"].ToString();
			check_emp_nm1 = row["check_emp_nm1"].ToString();
			check_emp_nm2 = row["check_emp_nm2"].ToString();
			comment = row["comment"].ToString();
		}

	}

}