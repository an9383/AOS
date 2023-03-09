using DevExpress.DirectX.Common.Direct2D;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.ProductionMaster
{    
    public class ProcessExam
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
            /// 공정코드
            /// </summary>
            public string s_process_cd { get; set; }

            // default 검색 생성자
            public SrchParam()
            {
                this.Gubun = "MS";
                this.s_process_cd = "";
            }
        }

        // 2. 검색파라미터 property
        public SrchParam srch { get; set; }
        
        // 3. 조회결과SET
        /// <summary>
        /// row의 상태값(I:insert, U:update, D:delete, N:normal,변경사항없음), Enum.TrxType 과 일치
        /// </summary>
        public string row_status { get; set; }

        public string process_cd { get; set; }
        public string process_nm { get; set; }
        public string process_exam_cd { get; set; }
        public string process_exam_nm { get; set; }
        public string process_exam_standard { get; set; }
        public string process_exam_min { get; set; }
        public string process_exam_max { get; set; }
        public string process_exam_period { get; set; }
        public string process_exam_qty { get; set; }
        public string process_exam_manu { get; set; }
        public string process_exam_qc { get; set; }
        public string equip_cd { get; set; }
        public string equip_nm { get; set; }
        public string process_exam_unit { get; set; }
        public string process_exam_unit_nm { get; set; }
        public string process_exam_start { get; set; }
        public string process_exam_start_nm { get; set; }
        public string grand_item { get; set; }
        public string middle_item { get; set; }
        public string item_seq { get; set; }
        public string audittrail_id { get; set; }
        public string ccp_yn { get; set; }
        
        // 4. default 생성자
        public ProcessExam()
        {
            
        }

        // 5. DTO설정
        public ProcessExam(DataRow row)
        {
            row_status = row["row_status"].ToString();
            process_cd = row["process_cd"].ToString();
            process_nm = row["process_nm"].ToString();
            process_exam_cd = row["process_exam_cd"].ToString();
            process_exam_nm = row["process_exam_nm"].ToString();
            process_exam_standard = row["process_exam_standard"].ToString();
            process_exam_min = row["process_exam_min"].ToString();
            process_exam_max = row["process_exam_max"].ToString();
            process_exam_period = row["process_exam_period"].ToString();
            process_exam_qty = row["process_exam_qty"].ToString();
            process_exam_manu = row["process_exam_manu"].ToString();
            process_exam_qc =  row["process_exam_qc"].ToString();
            equip_cd = row["equip_cd"].ToString();
            equip_nm = row["equip_nm"].ToString();
            process_exam_unit = row["process_exam_unit"].ToString();
            process_exam_unit_nm = row["process_exam_unit_nm"].ToString();
            process_exam_start = row["process_exam_start"].ToString();
            process_exam_start_nm = row["process_exam_start_nm"].ToString();
            grand_item = row["grand_item"].ToString();
            middle_item = row["middle_item"].ToString();
            item_seq = row["item_seq"].ToString();
            audittrail_id = row["audittrail_id"].ToString();
        }
    }
}
