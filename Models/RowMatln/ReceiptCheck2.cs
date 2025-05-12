using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.RowMatln
{
    public class ReceiptCheck2
    {
        /// <summary>
        /// 발주번호
        /// </summary>
        public string purchase_no { get; set; }

        /// <summary>
        /// 발주 seq
        /// </summary>
        public string purchase_seq { get; set; }

        /// <summary>
        /// 품목코드
        /// </summary>
        public string purchase_item_cd { get; set; }

        /// <summary>
        /// 품목명
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// 단위
        /// </summary>
        public string purchase_unit { get; set; }

        /// <summary>
        /// 발주량
        /// </summary>
        public string purchase_qty { get; set; }

        /// <summary>
        /// 단가
        /// </summary>
        public string purchase_price { get; set; }

        /// <summary>
        /// 금액
        /// </summary>
        public string purchase_amt { get; set; }

        /// <summary>
        /// 입고량
        /// </summary>
        public string receipt_qty { get; set; }

        /// <summary>
        /// 미납량
        /// </summary>
        public string not_receipt_qty { get; set; }

        /// <summary>
        /// 입고대기량
        /// </summary>
        public string receipt_waiting_qty { get; set; }

        /// <summary>
        /// 입고예정량
        /// </summary>
        public string receipt_ye_qty { get; set; }

        /// <summary>
        /// 제조처코드
        /// </summary>
        public string manufacture_cd { get; set; }

        /// <summary>
        /// 제조처명
        /// </summary>
        public string manufacture_nm { get; set; }

        /// <summary>
        /// 거래처코드 (공급처 코드)
        /// </summary>
        public string supply_cd { get; set; }

        /// <summary>
        /// 거래처명 (공급처 명)
        /// </summary>
        public string supply_nm { get; set; }

        /// <summary>
        /// 비고
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string item_gb { get; set; }

        /// <summary>
        /// 검수완료
        /// </summary>
        public string purchase_status { get; set; }

        /// <summary>
        /// 입고예정일
        /// </summary>
        public string purchase_require_date { get; set; }

        /// <summary>
        /// 제조제품코드
        /// </summary>
        public string manufacture_item_cd { get; set; }

        /// <summary>
        /// 제조품목명
        /// </summary>
        public string manufacture_item_nm { get; set; }

        /// <summary>
        /// 입고검수번호
        /// </summary>
        public string receipt_check_rpt_no { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public string receipt_check_rpt_id { get; set; }

        /// <summary>
        /// 검수수량
        /// </summary>
        public string receipt_check_rpt_qty { get; set; }

        /// <summary>
        /// 총검수수량
        /// </summary>
        public string receipt_check_rpt_qty_all { get; set; }

        /// <summary>
        /// 포장내역
        /// </summary>
        public string pack_form { get; set; }

        /// <summary>
        /// (검색) 시작일
        /// </summary>
        public string start_date { get; set; }

        /// <summary>
        /// (검색) 종료일
        /// </summary>
        public string end_date { get; set; }

        /// <summary>
        /// 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
        /// </summary>
        public string option { get; set; }

        /// <summary>
        /// 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
        /// </summary>
        public string option2 { get; set; }

        /// <summary>
        /// (검색) 품목 코드
        /// </summary>
        public string item_cd_S { get; set; }

        /// <summary>
        /// (검색) 품목 코드
        /// </summary>
        public string item_nm_S { get; set; }

        /// <summary>
        /// 검수완료 포함 여부
        /// </summary>
        public string purchase_status_Y { get; set; }

        // <summary>
        /// 0:발주일, 1:입고일, 2:입고예정일
        /// </summary>
        public string check { get; set; }

        // <summary>
        /// 제조품목코드
        /// </summary>
        public string manufacture_item_cd_S { get; set; }


    }
}