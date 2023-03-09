using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.SysItem
{
	public class EtcItem
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			public string item_cd { get; set; }
			public string item_gb { get; set; }
			public string item_type1 { get; set; }
			public string item_type2 { get; set; }

			public SrchParam()
			{
				this.item_cd = "";
				this.item_gb = "%";
				this.item_type1 = "%";
				this.item_type2 = "%";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		public string gubun { get; set; }
		public string Data_root { get; set; }
		public string item_cd { get; set; }
		public string item_nm { get; set; }
		public string prod_end { get; set; }
		public string break_type { get; set; }
		public string abc_gubun { get; set; }
		public string plant_cd { get; set; }
		public string item_type1 { get; set; }
		public string item_type2 { get; set; }
		public string item_unit { get; set; }
		public string keeping_warehouse { get; set; }
		public string item_gb_cd { get; set; }
		public string item_gb_nm { get; set; }
		public string remark { get; set; }
		public string item_enm { get; set; }

		public EtcItem()
		{
			this.prod_end = "N";
		}
	}
}