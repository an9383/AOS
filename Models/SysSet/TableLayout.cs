using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.SysSet
{
	public class TableLayout
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string table_name { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.table_name = "";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		//public string row_status { get; set; }
		public string table_name { get; set; }
		public short? column_id { get; set; }
		public string column_name { get; set; }
		public string column_type { get; set; }
		public string column_length { get; set; }
		public string column_caption { get; set; }
		public string column_description { get; set; }
		public string isnullable { get; set; }
		public string isidentity { get; set; }
		public string identity_definition { get; set; }
		public string isdefault { get; set; }
		public string default_definition { get; set; }
		public string ischeck { get; set; }
		public string check_definition { get; set; }
		public string isprimarykey { get; set; }
		public string isuniquekey { get; set; }
		public string isforeignkey { get; set; }
		public string fk_name { get; set; }
		public string iskeyclustered { get; set; }
		public string iskeynonclustered { get; set; }
		public string iskeyunique { get; set; }
		public string isindex { get; set; }
		public string idx_name { get; set; }
		public string isidxclustered { get; set; }
		public string isidxnonclustered { get; set; }
		public string isidxunique { get; set; }
		public string isidxfilter { get; set; }

		// 4. default 생성자 
		public TableLayout()
		{
		}


	}
}