using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdIn
{
    public class ItemProduction
    {
        /// <summary>
        /// 제품입고일련번호
        /// </summary>
        public string item_stock_id { get; set; }

        /// <summary>
        /// 포장실적일련번호
        /// </summary>
        public string packing_result_id { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string prod_return_ck { get; set; }

        /// <summary>
        /// 구분명
        /// </summary>
        public string prod_return_ck_nm { get; set; }

        /// <summary>
        /// 제품코드
        /// </summary>
        public string item_cd { get; set; }

        /// <summary>
        /// 제품명
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// 단위
        /// </summary>
        public string item_unit { get; set; }

        /// <summary>
        /// 제조번호
        /// </summary>
        public string lot_no { get; set; }

        /// <summary>
        /// 제조일자
        /// </summary>
        public string lot_date { get; set; }

        /// <summary>
        /// 입고일자
        /// </summary>
        public string receipt_date { get; set; }

        /// <summary>
        /// 입고량
        /// </summary>
        public string receipt_qty { get; set; }

        /// <summary>
        /// 유통기한
        /// </summary>
        public string end_date { get; set; }

        /// <summary>
        /// 지함적재량
        /// </summary>
        public string second_pack_qty { get; set; }

        /// <summary>
        /// 발주번호
        /// </summary>
        public string purchase_order_no { get; set; }

        /// <summary>
        /// 발주순번
        /// </summary>
        public string purchase_order_seq { get; set; }

        /// <summary>
        /// 세부순번
        /// </summary>
        public string purchase_order_rec { get; set; }

        /// <summary>
        /// 단가
        /// </summary>
        public string item_unit_price { get; set; }

        /// <summary>
        /// 출하승인상태
        /// </summary>
        public string shipping_status { get; set; }

        /// <summary>
        /// 유효기간
        /// </summary>
        public string item_validity_period { get; set; }

        /// <summary>
        /// 거래처제조번호
        /// </summary>
        public string cust_lot_no { get; set; }

        /// <summary>
        /// 거래처제품코드
        /// </summary>
        public string vender_item_cd { get; set; }

        /// <summary>
        /// 제조지시번호
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// 의뢰번호
        /// </summary>
        public string testrequest_no { get; set; }

        /// <summary>
        /// 제형
        /// </summary>
        public string item_form_cd { get; set; }

        /// <summary>
        /// 입고상태
        /// </summary>
        public string receipt_status { get; set; }

        /// <summary>
        /// 외주금액
        /// </summary>
        public string outsource_amt { get; set; }

        /// <summary>
        /// 단가
        /// </summary>
        public string outsource_base_amt { get; set; }

        /// <summary>
        /// 거래처코드
        /// </summary>
        public string outsource_cust_cd { get; set; }

        /// <summary>
        /// 거래처명
        /// </summary>
        public string outsource_cust_nm { get; set; }

        /// <summary>
        /// 팔레트바코드번호
        /// </summary>
        public string box_barcode_no { get; set; }

        /// <summary>
        /// 포장단위
        /// </summary>
        public string prod_qty { get; set; }

        /// <summary>
        /// 재고량
        /// </summary>
        public string stock_qty { get; set; }

        /// <summary>
        /// 팔레트
        /// </summary>
        public string pallet_cd { get; set; }

        /// <summary>
        /// 선택
        /// </summary>
        public string select_ck { get; set; }

        /// <summary>
        /// INSERT_TIME
        /// </summary>
        public string INSERT_TIME { get; set; }

        /// <summary>
        /// seq
        /// </summary>
        public string seq { get; set; }

        /// <summary>
        /// (검색) 시작일
        /// </summary>
        public string start_date_S { get; set; }

        /// <summary>
        /// (검색) 종료일
        /// </summary>
        public string end_date_S { get; set; }

        /// <summary>
        /// (검색) 제품코드
        /// </summary>
        public string item_cd_S { get; set; }

        /// <summary>
        /// (검색) 제품명
        /// </summary>
        public string item_nm_S { get; set; }

        /// <summary>
        /// (검색) 제조번호
        /// </summary>
        public string lot_no_S { get; set; }

        /// <summary>
        /// (검색) 제품입고구분
        /// </summary>
        public string prod_return_S { get; set; }

        /// <summary>
        /// 지시일자
        /// </summary>
        public string order_date { get; set; }

        /// <summary>
        /// 상태명
        /// </summary>
        public string order_status_nm { get; set; }

        /// <summary>
        /// 부서명
        /// </summary>
        public string dept_nm { get; set; }

        /// <summary>
        /// pb_date
        /// </summary>
        public string pb_date { get; set; }

        /// <summary>
        /// 선택
        /// </summary>
        public string order_qty { get; set; }

        /// <summary>
        /// cnt
        /// </summary>
        public string cnt { get; set; }

        /// <summary>
        /// 유통기한(팝업)
        /// </summary>
        public string valid_date { get; set; }

        /// <summary>
        /// CRUD 구분
        /// </summary>
        public string gubun { get; set; }

        /// <summary>
        /// 시험접수일자
        /// </summary>
        public string TestRequestDate { get; set; }

    }
}