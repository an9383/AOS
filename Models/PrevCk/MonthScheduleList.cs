using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrevCk
{
	public class MonthScheduleList
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string s_year { get; set; }
			public string s_month { get; set; }
			public string s_obj_type { get; set; }
			public string s_work_type { get; set; }
			public string s_schedule_type { get; set; }
			public string obj_cd { get; set; }
			public string schedule_master_id { get; set; }

			//report 관련 
			public string report_gubun { get; set; }
			public string s_week { get; set; }
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public int schedule_master_id { get; set; }
		public string obj_cd { get; set; }
		public string obj_cd2 { get; set; }
		public string obj_type_nm { get; set; }
		public string dis_nm { get; set; }
		public string work_type_nm { get; set; }
		public string schedule_type_nm { get; set; }
		public int schedule_cnt { get; set; }
		public int result_cnt { get; set; }
		public int diff_cnt { get; set; }

		// 4. default 생성자 
		public MonthScheduleList()
		{
		}

		// 5. DTO 설정
		public MonthScheduleList(DataRow row)
		{
			//row_status = row["row_status"].ToString();
			schedule_master_id = Int32.Parse(row["schedule_master_id"].ToString());
			obj_cd = row["obj_cd"].ToString();
			obj_cd2 = row["obj_cd2"].ToString();
			obj_type_nm = row["obj_type_nm"].ToString();
			dis_nm = row["dis_nm"].ToString();
			work_type_nm = row["work_type_nm"].ToString();
			schedule_type_nm = row["schedule_type_nm"].ToString();
			schedule_cnt = Int32.Parse(row["schedule_cnt"].ToString());
			result_cnt = Int32.Parse(row["result_cnt"].ToString());
			diff_cnt = Int32.Parse(row["diff_cnt"].ToString());
		}
	}


}