using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdWh
{
    public class ItemStockStatus
    {
        /// <summary>
        /// 품목코드
        /// </summary>
        public string item_cd { get; set; }

        /// <summary>
        /// 품목명
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// 단위
        /// </summary>
        public string item_lot_size { get; set; }

        /// <summary>
        /// 생산량
        /// </summary>
        public string prod_qty_sum { get; set; }

        /// <summary>
        /// 기타입고량
        /// </summary>
        public string etc_in_qty_sum { get; set; }

        /// <summary>
        /// 출고량
        /// </summary>
        public string issue_qty_sum { get; set; }

        /// <summary>
        /// 기타출고량
        /// </summary>
        public string etc_out_qty_sum { get; set; }

        /// <summary>
        /// 이론재고량
        /// </summary>
        public string theory_qty { get; set; }

        /// <summary>
        /// 재고량
        /// </summary>
        public string stock_qty_sum { get; set; }

        /// <summary>
        /// 재고량체크
        /// </summary>
        public string stock_qty_chk { get; set; }

        /// <summary>
        /// 입고예정량
        /// </summary>
        public string prod_qty_plan { get; set; }

        /// <summary>
        /// amt
        /// </summary>
        public string amt { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string item_stock_id { get; set; }

        /// <summary>
        /// 제조번호
        /// </summary>
        public string lot_no { get; set; }

        /// <summary>
        /// 제조일자
        /// </summary>
        public string lot_date { get; set; }

        /// <summary>
        /// 출하승인상태
        /// </summary>
        public string issue_status { get; set; }

        /// <summary>
        /// 팔레트
        /// </summary>
        public string item_move { get; set; }

        /// <summary>
        /// 팔레트바코드
        /// </summary>
        public string box_barcode_no { get; set; }

        /// <summary>
        /// 팔레트
        /// </summary>
        public string prod_qty { get; set; }

        /// <summary>
        /// 생산량
        /// </summary>
        public string stock_qty { get; set; }

        /// <summary>
        /// 기타입고량
        /// </summary>
        public string etc_in_qty { get; set; }

        /// <summary>
        /// 출고량
        /// </summary>
        public string issue_qty { get; set; }

        /// <summary>
        /// 기타출고량
        /// </summary>
        public string etc_out_qty { get; set; }

        /// <summary>
        /// receipt_status
        /// </summary>
        public string receipt_status { get; set; }

        /// <summary>
        /// 입출고일자
        /// </summary>
        public string in_out_date { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string in_out_type { get; set; }

        /// <summary>
        /// 상세구분
        /// </summary>
        public string in_out_type_detail { get; set; }

        /// <summary>
        /// 입고량
        /// </summary>
        public string in_qty { get; set; }

        /// <summary>
        /// 출고량
        /// </summary>
        public string out_qty { get; set; }

        /// <summary>
        /// 비고
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 사용여부
        /// </summary>
        public string use_ck { get; set; }

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