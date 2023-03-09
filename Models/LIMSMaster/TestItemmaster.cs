using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.LIMSMaster
{
	public class TestItemmaster
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string Gubun { get; set; }
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string s_testitem_type { get; set; }
			/// <summary> 
			/// 시험항목코드 
			/// </summary> 
			public string s_testitem_cd { get; set; }
			/// <summary> 
			/// 담당자
			/// </summary> 
			public string s_testitem_charge { get; set; }
			/// <summary> 
			/// 사용여부 
			/// </summary> 
			public string s_use_yn { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.Gubun = "S";
				this.s_testitem_type = "%";
				this.s_testitem_cd = "";
				this.s_testitem_charge = "";
				this.s_use_yn = "Y";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string testitem_cd { get; set; }
		public string testitem_nm { get; set; }
		public string code_type { get; set; }
		public string testitem_enm { get; set; }
		public string testitem_type { get; set; }
		public string testitem_charge { get; set; }
		public string use_yn { get; set; }
		public string code_type_nm { get; set; }
		public string testitem_type_nm { get; set; }
		public string testitem_charge_nm { get; set; }
		public string number { get; set; }
		public float test_cost { get; set; }
		public string dept_cd { get; set; }
		public string dept_nm { get; set; }

		// 4. default 생성자 
		public TestItemmaster()
		{
		}

		// 5. DTO 설정
		public TestItemmaster(DataRow row)
		{
			row_status = row["row_status"].ToString();
			testitem_cd = row["testitem_cd"].ToString();
			testitem_nm = row["testitem_nm"].ToString();
			code_type = row["code_type"].ToString();
			testitem_enm = row["testitem_enm"].ToString();
			testitem_type = row["testitem_type"].ToString();
			testitem_charge = row["testitem_charge"].ToString();
			use_yn = row["use_yn"].ToString();
			code_type_nm = row["code_type_nm"].ToString();
			testitem_type_nm = row["testitem_type_nm"].ToString();
			testitem_charge_nm = row["testitem_charge_nm"].ToString();
			number = row["number"].ToString();
			test_cost = (float)row["test_cost"];
			dept_cd = row["dept_cd"].ToString();
			dept_nm = row["dept_nm"].ToString();
		}
	}


}