using DevExpress.DirectX.Common.Direct2D;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.Equipment
{    
    public class Notify
    {
        /// <summary>
        /// 화면 검색 Parameter CLass
        /// </summary>
        /// 

        public class SrchParam
        {
            /// <summary>
            ///  SP 검색구분
            /// </summary>
            public string Gubun { get; set; }
            /// <summary>
            /// 라디오구분(신고일자:1, 처리일자:2)
            /// </summary>
            public string rb_gb { get; set; }
            /// <summary>
            /// 검색시작일
            /// </summary>
            public string sdate { get; set; }
            /// <summary>
            /// 검색종료일
            /// </summary>
            public string edate { get; set; }

            /// <summary>
            /// 상태
            /// </summary>
            public string status { get; set; }

            /// <summary>
            /// 설비
            /// </summary>
            public string equipment { get; set; }

            // default 검색 생성자
            public SrchParam()
            {
                this.Gubun = "S";
                this.rb_gb = "1";
                this.sdate = new DateTime(2019, 1, 1).ToShortDateString();
                this.edate = DateTime.Now.ToShortDateString();
                this.status = "%";
                this.equipment = "";                  
            }
        }

        // 2. 검색파라미터 property
        public SrchParam srch { get; set; }
        
        // 3. 조회결과SET
        /// <summary>
        /// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치
        /// </summary>
        public string row_status { get; set; }
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

        // 4. default 생성자
        public Notify()
        {
            
        }

        // 5. DTO설정
        public Notify(DataRow row)
        {
            row_status = row["row_status"].ToString();
            afterservice_no = row["afterservice_no"].ToString();
            equip_cd = row["equip_cd"].ToString();
            equip_nm = row["equip_nm"].ToString();
            notify_emp_cd = row["notify_emp_cd"].ToString();
            notify_emp_nm = row["notify_emp_nm"].ToString();
            notify_date = row["notify_date"].ToString();
            notify_reason = row["notify_reason"].ToString();
            repair_date = row["repair_date"].ToString();
            repair_status_cd = row["repair_status_cd"].ToString();
            repair_status_nm = row["repair_status_nm"].ToString();
            pre_fix = row["pre_fix"].ToString();
        }
    }
}
