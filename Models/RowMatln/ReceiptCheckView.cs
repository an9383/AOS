using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.RowMatln
{
    public class ReceiptCheckView
    {
        /// <summary>
        /// 입고검수번호
        /// </summary>
        public string receipt_check_rpt_no { get; set; }

        /// <summary>
        /// 입고검수일련번호
        /// </summary>
        public string receipt_check_rpt_id { get; set; }

        /// <summary>
        /// 품목코드
        /// </summary>
        public string purchase_item_cd { get; set; }

        /// <summary>
        /// 품목명
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// 검수수량
        /// </summary>
        public string receipt_check_rpt_qty { get; set; }

        /// <summary>
        /// 포장내역
        /// </summary>
        public string pack_form { get; set; }

        /// <summary>
        /// 단위
        /// </summary>
        public string purchase_unit { get; set; }

        /// <summary>
        /// 검수일자
        /// </summary>
        public string receipt_check_rpt_dt { get; set; }

        /// <summary>
        /// 발주번호
        /// </summary>
        public string purchase_no { get; set; }

        /// <summary>
        /// 발주일련번호
        /// </summary>
        public string purchase_seq { get; set; }

        /// <summary>
        /// 제조처
        /// </summary>
        public string manufacture_cd { get; set; }

        /// <summary>
        /// 제조처명
        /// </summary>
        public string manufacture_nm { get; set; }

        /// <summary>
        /// 공급처
        /// </summary>
        public string supply_cd { get; set; }

        /// <summary>
        /// 공급처명
        /// </summary>
        public string supply_nm { get; set; }

        /// <summary>
        /// 발주일자
        /// </summary>
        public string purchase_date { get; set; }

        /// <summary>
        /// 발주상태
        /// </summary>
        public string purchase_status { get; set; }

        /// <summary>
        /// 조달구분
        /// </summary>
        public string obtain_status { get; set; }

        /// <summary>
        /// 원료 3 , 자재 2 , 상품 1
        /// </summary>
        public string option { get; set; }

        /// <summary>
        /// 원료 3 , 자재 2 , 상품 1
        /// </summary>
        public string option2 { get; set; }

        /// <summary>
        /// (검색) 시작일
        /// </summary>
        public string start_date { get; set; }

        /// <summary>
        /// (검색) 종료일
        /// </summary>
        public string end_date { get; set; }
    }
}