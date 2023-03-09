using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrevCk
{
	public class ScheduleMaster
	{
		//property > 조회결과SET 
		public string obj_type { get; set; }
		public string obj_type_nm { get; set; }
		public string work_type { get; set; }
		public string work_type_nm { get; set; }
		public string schedule_type { get; set; }
		public string schedule_type_nm { get; set; }
		public string obj_cd { get; set; }
		public string standard_nm { get; set; }
		public string obj_nm { get; set; }
		public int schedule_period { get; set; }
		public string start_date { get; set; }
		public string responsibility_worker { get; set; }
		public string responsibility_worker_nm { get; set; }
		public string auto_create_yn { get; set; }
		public string regist_yn { get; set; }
		public int schedule_master_id { get; set; }
		public string dept_cd { get; set; }
		public string dept_nm { get; set; }
		public string period_type { get; set; }
		public string period_type_nm { get; set; }

		// default 생성자 
		public ScheduleMaster()
		{
		}

		// DTO 설정
		public ScheduleMaster(DataRow row)
		{
			this.obj_type = row["obj_type"].ToString();
			this.obj_type_nm = row["obj_type_nm"].ToString();
			this.work_type = row["work_type"].ToString();
			this.work_type_nm = row["work_type_nm"].ToString();
			this.schedule_type = row["schedule_type"].ToString();
			this.schedule_type_nm = row["schedule_type_nm"].ToString();
			this.obj_cd = row["obj_cd"].ToString();
			this.standard_nm = row["standard_nm"].ToString();
			this.obj_nm = row["obj_nm"].ToString();
			this.schedule_period = Int32.Parse(row["schedule_period"].ToString());
			this.start_date = row["start_date"].ToString();
			this.responsibility_worker = row["responsibility_worker"].ToString();
			this.responsibility_worker_nm = row["responsibility_worker_nm"].ToString();
			this.auto_create_yn = row["auto_create_yn"].ToString();
			this.regist_yn = row["regist_yn"].ToString();
			this.schedule_master_id = Int32.Parse(row["schedule_master_id"].ToString());
			this.dept_cd = row["dept_cd"].ToString();
			this.dept_nm = row["dept_nm"].ToString();
			this.period_type = row["period_type"].ToString();
			this.period_type_nm = row["period_type_nm"].ToString();
		}
	}
}