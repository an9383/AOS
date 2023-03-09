using DevExpress.XtraSpreadsheet.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysSet
{
	public class MenuManage
	{
		//public string pGubun { get; set; }
		public string MenuManage_pGubun { get; set; }
		public string Data_root { get; set; }
		[Key]
		public string module_gb { get; set; }
		[Key]
		public string module_cd { get; set; }
		public string module_nm { get; set; }
		public int module_seq { get; set; }
		public int module_level { get; set; }
		public string module_parent { get; set; }
		public string module_parent_nm { get; set; }
		public string ID { get; set; }
		public string ParentID { get; set; }

		public MenuManage(DataRow row)
		{
			module_gb = row["module_gb"].ToString();
			module_cd = row["module_cd"].ToString();
			module_nm = row["module_nm"].ToString();
			module_seq = Convert.ToInt32(row["module_seq"].ToString());
			module_level = Convert.ToInt32(row["module_level"].ToString());
			module_parent = row["module_parent"].ToString();
			module_parent_nm = row["module_parent_nm"].ToString();
			ID = row["ID"].ToString();
			ParentID = row["ParentID"].ToString();
		}

		public MenuManage(){}
	}
}
