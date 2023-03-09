using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdWh
{
    public class ItemOutManage
    {
        public string gubun { get; set; }
        public string packing_result_pack_out_history_id { get; set; }//
        public string item_stock_id { get; set; }//item_stock_id
        public string out_date { get; set; }//출고일자
        public string item_cd { get; set; }//품목코드
        public string item_nm { get; set; }//제품명
        public string lot_no { get; set; }//제조번호
        public string item_unit { get; set; }//
        public string packing_result_id { get; set; }//
        public string box_barcode_no { get; set; }//박스바코드번호
        public string prod_qty { get; set; }//입고량
        public string delivery_qty { get; set; }//출고량
        public string stock_qty { get; set; }//재고량
        public string out_type { get; set; }//출고구분
        public string out_qty { get; set; }//기타출고량
        public string out_remark { get; set; }//비고

        public string insert_user_cd { get; set; }//입력자코드
        public string insert_time { get; set; }//입력일시
        public string update_user_cd { get; set; }//수정자코드
        public string update_time { get; set; }//수정일시

        public string start_date { get; set; }//시작일
        public string end_date { get; set; }//종료일

        public string past_stock_qty { get; set; }//과거 재고량
        public string past_out_qty { get; set; }//과거 입고 량?
        public string log_user_id { get; set; } //로그입력자?
        public string identity { get; set; }

    }
}