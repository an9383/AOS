using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.ProductionManage
{
	public class EquipPlan
	{
		
		public string row_status { get; set; }
		public string mp_ck { get; set; }
		public string order_no { get; set; }
		public string equip_cd { get; set; }
		public string ccp_cd { get; set; }
		public string s_date { get; set; }
		public string s_time { get; set; }
		public string e_date { get; set; }
		public string e_time { get; set; }
		public int required_min { get; set; }
		public string order_proc_id { get; set; }
		public string sys_plant_cd { get; set; }
		public int audittrail_id { get; set; }
		public string gubun { get; set; }

		// 4. default 생성자 
		public EquipPlan()
		{
		}

	
	}


}