using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.RowMatln
{
    public class PurchaseManage2
    {
        /// <summary>
        /// 발주번호
        /// </summary>
        public string purchase_no { get; set; }

        /// <summary>
        /// 발주상세순번
        /// </summary>
        public string purchase_seq { get; set; }

        /// <summary>
        /// 발주일자
        /// </summary>
        public string purchase_date { get; set; }

        /// <summary>
        /// 품목코드
        /// </summary>
        public string purchase_item_cd { get; set; }

        /// <summary>
        /// 품목명
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// 원자재구분
        /// </summary>
        public string item_gb { get; set; }

        /// <summary>
        /// 입고예정일
        /// </summary>
        public string purchase_require_date { get; set; }

        /// <summary>
        /// 발주수량
        /// </summary>
        public string purchase_qty { get; set; }

        /// <summary>
        /// 단위
        /// </summary>
        public string purchase_unit { get; set; }

        /// <summary>
        /// 발주상태
        /// </summary>
        public string purchase_status { get; set; }

        /// <summary>
        /// 발주상태 명
        /// </summary>
        public string purchase_status_nm { get; set; }

        /// <summary>
        /// 단가
        /// </summary>
        public string purchase_price { get; set; }

        /// <summary>
        /// 금액
        /// </summary>
        public string purchase_amt { get; set; }

        /// <summary>
        /// 입고예정량
        /// </summary>
        public string receipt_ye_qty { get; set; }

        /// <summary>
        /// 입고량
        /// </summary>
        public string receipt_qty { get; set; }

        /// <summary>
        /// 입고대기량
        /// </summary>
        public string receipt_waiting_qty { get; set; }

        /// <summary>
        /// 미납량
        /// </summary>
        public string not_receipt_qty { get; set; }

        /// <summary>
        /// 제조처코드
        /// </summary>
        public string manufacture_cd { get; set; }

        /// <summary>
        /// 제조처명
        /// </summary>
        public string manufacture_nm { get; set; }

        /// <summary>
        /// 제조처주소
        /// </summary>
        public string manufacture_address { get; set; }

        /// <summary>
        /// 거래처코드
        /// </summary>
        public string supply_cd { get; set; }

        /// <summary>
        /// 거래처명
        /// </summary>
        public string supply_nm { get; set; }

        /// <summary>
        /// 거래처주소
        /// </summary>
        public string supply_address { get; set; }

        /// <summary>
        /// 비고
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 생산의뢰번호
        /// </summary>
        public string order_request_no { get; set; }

        /// <summary>
        /// 조달구분
        /// </summary>
        public string obtain_status { get; set; }

        /// <summary>
        /// 조달구분 명
        /// </summary>
        public string obtain_status_nm { get; set; }


        /// <summary>
        /// 제조품목코드
        /// </summary>
        public string manufacture_item_cd { get; set; }

        /// <summary>
        /// 제조품목명
        /// </summary>
        public string manufacture_item_nm { get; set; }

        /// <summary>
        /// 최종입고일자
        /// </summary>
        public string last_receipt_time { get; set; }

        /// <summary>
        /// 원료종류
        /// </summary>
        public string purchase_kind { get; set; }

        /// <summary>
        /// 입고담당자 코드
        /// </summary>
        public string emp_cd { get; set; }

        /// <summary>
        /// 입고담당자명
        /// </summary>
        public string emp_nm { get; set; }

        /// <summary>
        /// no
        /// </summary>
        public string no { get; set; }

        /// <summary>
        /// 최근단가
        /// </summary>
        public string purchase_base_price { get; set; }

        /// <summary>
        /// 담당자명
        /// </summary>
        public string commercial_personnel { get; set; }

        /// <summary>
        /// 전화번호
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 팩스번호
        /// </summary>
        public string supply_fax { get; set; }

        /// <summary>
        /// (검색) 기간 상태값 (발주일자 or 입고예정일)
        /// </summary>
        public string search_data { get; set; }

        /// <summary>
        /// (검색) 상태
        /// </summary>
        public string purchase_state { get; set; }

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
        /// (검색) 생산의뢰번호
        /// </summary>
        public string order_request_no_S { get; set; }

        /// <summary>
        /// (검색) 거래처 코드
        /// </summary>
        public string vender_cd_S { get; set; }

        /// <summary>
        /// (검색) 거래처 명
        /// </summary>
        public string vender_nm_S { get; set; }

        /// <summary>
        /// (검색) 제조품목 코드
        /// </summary>
        public string manufacture_item_cd_S { get; set; }

        /// <summary>
        /// (검색) 제조품목 명
        /// </summary>
        public string manufacture_item_nm_S { get; set; }

        /// <summary>
        /// (검색) 품목 코드
        /// </summary>
        public string item_cd_S { get; set; }

        /// <summary>
        /// (검색) 품목 코드
        /// </summary>
        public string item_nm_S { get; set; }

        /// <summary>
        /// CRUD
        /// </summary>
        public string gubun { get; set; }
    }
}
