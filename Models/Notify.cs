using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models
{
    public class Notify
    {
        // SP 구분자
        public string pGubun { get; set; }


        ///검색 조건들
        // 신고일자 또는 처리일자 구분
        public string period { get; set; }
        // 시작일자
        public string sdate { get; set; }
        // 끝일자
        public string edate { get; set; }
        // 상태
        public string status { get; set; }
        // 설비
        public string equip { get; set; }


        /// SP 결과값들
        public string afterservice_no { get; set; }
		public string equip_cd { get; set; }
		public string equip_nm { get; set; }
		public string notify_emp_cd { get; set; }
		public string notify_emp_nm { get; set; }
		public string notify_date { get; set; }
		public string notify_reason { get; set; }
		public string repair_date { get; set; }
		public string repair_status_cd { get; set; }
		public string repair_status_nm { get; set; }
		public string pre_fix { get; set; }

		public Notify()
        {

        }

        public Notify(DataRow row)
        {
            this.afterservice_no = row["afterservice_no"].ToString();
            this.equip_cd = row["equip_cd"].ToString();
            this.equip_nm = row["equip_nm"].ToString();
            this.notify_emp_cd = row["notify_emp_cd"].ToString();
            this.notify_emp_nm = row["notify_emp_nm"].ToString();
            this.notify_date = row["notify_date"].ToString();
            this.notify_reason = row["notify_reason"].ToString();
            this.repair_date = row["repair_date"].ToString();
            this.repair_status_cd = row["repair_status_cd"].ToString();
            this.repair_status_nm = row["repair_status_nm"].ToString();
            this.pre_fix = row["pre_fix"].ToString();
        }
    }
}
