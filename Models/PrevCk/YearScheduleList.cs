using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrevCk
{
	public class YearScheduleList
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string year { get; set; }
			public string s_obj_type { get; set; }
			public string s_work_type { get; set; }
			public string s_schedule_type { get; set; }
			public string obj_cd { get; set; }
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string obj_type_nm { get; set; }
		public string work_type_nm { get; set; }
		public string schedule_type_nm { get; set; }
		public string obj_nm { get; set; }
		public string obj_cd { get; set; }
		public int schedule_01 { get; set; }
		public int schedule_02 { get; set; }
		public int schedule_03 { get; set; }
		public int schedule_04 { get; set; }
		public int schedule_05 { get; set; }
		public int schedule_06 { get; set; }
		public int schedule_07 { get; set; }
		public int schedule_08 { get; set; }
		public int schedule_09 { get; set; }
		public int schedule_10 { get; set; }
		public int schedule_11 { get; set; }
		public int schedule_12 { get; set; }
		public int result_01 { get; set; }
		public int result_02 { get; set; }
		public int result_03 { get; set; }
		public int result_04 { get; set; }
		public int result_05 { get; set; }
		public int result_06 { get; set; }
		public int result_07 { get; set; }
		public int result_08 { get; set; }
		public int result_09 { get; set; }
		public int result_10 { get; set; }
		public int result_11 { get; set; }
		public int result_12 { get; set; }
		//public byte[] company_full_image { get; set; }
		public string form_no { get; set; }

		// 4. default 생성자 
		public YearScheduleList()
		{
		}

		// 5. DTO 설정
		public YearScheduleList(DataRow row)
		{
			//row_status = row["row_status"].ToString();
			obj_type_nm = row["obj_type_nm"].ToString();
			work_type_nm = row["work_type_nm"].ToString();
			schedule_type_nm = row["schedule_type_nm"].ToString();
			obj_nm = row["obj_nm"].ToString();
			obj_cd = row["obj_cd"].ToString();
			schedule_01 = Int32.Parse(row["schedule_01"].ToString());
			schedule_02 = Int32.Parse(row["schedule_02"].ToString());
			schedule_03 = Int32.Parse(row["schedule_03"].ToString());
			schedule_04 = Int32.Parse(row["schedule_04"].ToString());
			schedule_05 = Int32.Parse(row["schedule_05"].ToString());
			schedule_06 = Int32.Parse(row["schedule_06"].ToString());
			schedule_07 = Int32.Parse(row["schedule_07"].ToString());
			schedule_08 = Int32.Parse(row["schedule_08"].ToString());
			schedule_09 = Int32.Parse(row["schedule_09"].ToString());
			schedule_10 = Int32.Parse(row["schedule_10"].ToString());
			schedule_11 = Int32.Parse(row["schedule_11"].ToString());
			schedule_12 = Int32.Parse(row["schedule_12"].ToString());
			result_01 = Int32.Parse(row["result_01"].ToString());
			result_02 = Int32.Parse(row["result_02"].ToString());
			result_03 = Int32.Parse(row["result_03"].ToString());
			result_04 = Int32.Parse(row["result_04"].ToString());
			result_05 = Int32.Parse(row["result_05"].ToString());
			result_06 = Int32.Parse(row["result_06"].ToString());
			result_07 = Int32.Parse(row["result_07"].ToString());
			result_08 = Int32.Parse(row["result_08"].ToString());
			result_09 = Int32.Parse(row["result_09"].ToString());
			result_10 = Int32.Parse(row["result_10"].ToString());
			result_11 = Int32.Parse(row["result_11"].ToString());
			result_12 = Int32.Parse(row["result_12"].ToString());
			//company_full_image = row["company_full_image"].ToString();
			form_no = row["form_no"].ToString();
		}
	}


}