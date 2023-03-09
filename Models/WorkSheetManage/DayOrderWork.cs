using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.WorkSheetManage
{
	public class DayOrderWork
	{
		/// <summary> 
		/// 1. 화면 검색 Parameter CLass 
		/// </summary> 
		public class SrchParam
		{
			/// <summary> 
			/// SP 검색구분 
			/// </summary> 
			public string sdate { get; set; }
			public string edate { get; set; }
			public string status { get; set; }

			// default 검색 생성자 
			public SrchParam()
			{
				this.sdate = DateTime.Now.AddDays(-7).ToShortDateString();
				this.edate = DateTime.Now.ToShortDateString();
				this.status = "%";
			}
		}

		// 2. 검색파라미터 property
		public SrchParam srch;

		// 3. 조회결과SET 
		/// <summary> 
		/// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치 
		/// </summary> 
		public string row_status { get; set; }
		public string order_no { get; set; }
		public string order_proc_id { get; set; }
		public string order_detailproc_id { get; set; }
		public string detailproc_nm { get; set; }
		public string order_detailproc_real_worker { get; set; }
		public string order_detailproc_real_worker_nm { get; set; }
		public string order_detailproc_real_inspector { get; set; }
		public string order_detailproc_real_inspector_nm { get; set; }
		public string order_detailproc_start_time { get; set; }
		public string order_detailproc_end_time { get; set; }
		public string order_detailproc_status_nm { get; set; }

		//작업방법 정보
		public int order_work_id { get; set; }
		public string order_work_start_time { get; set; }
		public string order_work_end_time { get; set; }

		// 4. default 생성자 
		public DayOrderWork()
		{
		}

	
	}


}