using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.RowMatWh
{
    public class StockStatus2
    {
        /// <summary>
        /// 코드
        /// </summary>
        public string item_cd { get; set; }

        /// <summary>
        /// 품목명
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// 단위
        /// </summary>
        public string item_unit { get; set; }

        /// <summary>
        /// 입고예정수량
        /// </summary>
        public string receipt_ye_qty { get; set; }

        /// <summary>
        /// 재고량
        /// </summary>
        public string receipt_remain_qty { get; set; }

        /// <summary>
        /// 출고예정량
        /// </summary>
        public string receipt_reserve_qty { get; set; }

        /// <summary>
        /// 가용재고량
        /// </summary>
        public string available_inventory { get; set; }

        /// <summary>
        /// 이론재고량
        /// </summary>
        public string theory_qty { get; set; }

        /// <summary>
        /// 체크
        /// </summary>
        public string check { get; set; }

        /// <summary>
        /// 사용여부
        /// </summary>
        public string prod_end { get; set; }

        /// <summary>
        /// param 값 s_gubun
        /// </summary>
        public string s_gubun { get; set; }

        /// <summary>
        /// param 값 s_gubun2
        /// </summary>
        public string s_gubun2 { get; set; }

        /// <summary>
        /// 시험번호
        /// </summary>
        public string test_no { get; set; }

        /// <summary>
        /// 입고일자
        /// </summary>
        public string test_date { get; set; }

        /// <summary>
        /// 입고번호
        /// </summary>
        public string receipt_no { get; set; }

        /// <summary>
        /// 입고순번
        /// </summary>
        public string receipt_id { get; set; }

        /// <summary>
        /// 입고량
        /// </summary>
        public string receipt_qty { get; set; }

        /// <summary>
        /// 출고량
        /// </summary>
        public string delivery_qty { get; set; }

        /// <summary>
        /// 불용재고
        /// </summary>
        public string receipt_disuse_qty { get; set; }

        /// <summary>
        /// 불량재고
        /// </summary>
        public string receipt_bad_qty { get; set; }

        /// <summary>
        /// 조달구분 코드
        /// </summary>
        public string obtain_status { get; set; }

        /// <summary>
        /// 조달구분
        /// </summary>
        public string obtain_status_nm { get; set; }

        /// <summary>
        /// 기타입고량
        /// </summary>
        public string receipt_etc_qty { get; set; }

        /// <summary>
        /// 기타출고량
        /// </summary>
        public string delivery_etc_qty { get; set; }

        /// <summary>
        /// 유효기간
        /// </summary>
        public string valid_period { get; set; }

        /// <summary>
        /// 시작 날짜
        /// </summary>
        public string start_date { get; set; }

        /// <summary>
        /// 종료 날짜
        /// </summary>
        public string end_date { get; set; }

        /// <summary>
        /// 검색 날짜
        /// </summary>
        public string item { get; set; }

        /// <summary>
        /// 검색 상태 값
        /// </summary>
        public string obtain_status_S { get; set; }

        /// <summary>
        /// 입고일자
        /// </summary>
        public string receipt_date { get; set; }

        /// <summary>
        /// 팩 바코드
        /// </summary>
        public string receipt_pack_barcode { get; set; }

        /// <summary>
        /// 시험번호
        /// </summary>
        public string qc_no { get; set; }

        /// <summary>
        /// 팩 단위
        /// </summary>
        public string receipt_pack_unit { get; set; }

        /// <summary>
        /// 팩 입고량
        /// </summary>
        public string receipt_pack_qty { get; set; }

        /// <summary>
        /// 팩 기타 입고량
        /// </summary>
        public string receipt_pack_etc_qty { get; set; }

        /// <summary>
        /// 팩 출고량
        /// </summary>
        public string delivery_pack_qty { get; set; }

        /// <summary>
        /// 팩 기타 출고량
        /// </summary>
        public string delivery_pack_etc_qty { get; set; }

        /// <summary>
        /// 팩 재고량
        /// </summary>
        public string receipt_pack_remain_qty { get; set; }

        /// <summary>
        /// 원료/자재 구분
        /// </summary>
        public string StockStatus2_status { get; set; }

        /// <summary>
        /// (검색) 품목코드
        /// </summary>
        public string item_cd_S { get; set; }

        /// <summary>
        /// (검색) 품목코드
        /// </summary>
        public string item_nm_S { get; set; }
    }
}
