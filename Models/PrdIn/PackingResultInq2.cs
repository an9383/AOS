using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdIn
{
    public class PackingResultInq2
    {
        /// <summary>
        /// 포장지시번호
        /// </summary>
        public string packing_order_no { get; set; }

        /// <summary>
        /// 포장타입코드
        /// </summary>
        public string packing_order_type { get; set; }

        /// <summary>
        /// 제품코드
        /// </summary>
        public string sale_item_cd { get; set; }

        /// <summary>
        /// 제품명
        /// </summary>
        public string sale_item_nm { get; set; }

        /// <summary>
        /// 단위
        /// </summary>
        public string item_packunit2 { get; set; }

        /// <summary>
        /// 지시수량
        /// </summary>
        public string packing_order_qty { get; set; }

        /// <summary>
        /// 제조지시번호
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// material_status
        /// </summary>
        public string material_status { get; set; }

        /// <summary>
        /// material_status 명
        /// </summary>
        public string material_status_nm { get; set; }

        /// <summary>
        /// conversion_qty
        /// </summary>
        public string conversion_qty { get; set; }

        /// <summary>
        /// packing_order_remark
        /// </summary>
        public string packing_order_remark { get; set; }

        /// <summary>
        /// 포장일자
        /// </summary>
        public string packing_date { get; set; }

        /// <summary>
        /// 생산량
        /// </summary>
        public string packing_qty { get; set; }

        /// <summary>
        /// 시험보관량
        /// </summary>
        public string sample_qty { get; set; }

        /// <summary>
        /// 잔량
        /// </summary>
        public string remain_qty { get; set; }

        /// <summary>
        /// 폐기량
        /// </summary>
        public string disuse_qty { get; set; }

        /// <summary>
        /// 출하승인상태
        /// </summary>
        public string issue_status { get; set; }

        /// <summary>
        /// 출하승인상태
        /// </summary>
        public string issue_status_nm { get; set; }

        /// <summary>
        /// 제조번호
        /// </summary>
        public string lot_no { get; set; }

        /// <summary>
        /// end_date
        /// </summary>
        public string end_date { get; set; }

        /// <summary>
        /// 제품이관상태
        /// </summary>
        public string receipt_status { get; set; }

        /// <summary>
        /// (검색) 시작일
        /// </summary>
        public string start_date_S { get; set; }

        /// <summary>
        /// (검색) 종료일
        /// </summary>
        public string end_date_S { get; set; }

        /// <summary>
        /// (검색) 품목코드
        /// </summary>
        public string item_cd_S { get; set; }

        /// <summary>
        /// (검색) 품목명
        /// </summary>
        public string item_nm_S { get; set; }

        /// <summary>
        /// (검색) 제조번호
        /// </summary>
        public string lot_no_S { get; set; }

        /// <summary>
        /// (검색) 출하승인상태
        /// </summary>
        public string s_issue_status { get; set; }
    }
}