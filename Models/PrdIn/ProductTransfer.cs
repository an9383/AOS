using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.PrdIn
{
    public class ProductTransfer
    {
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
        public string item_packunit2 { get; set; }

        /// <summary>
        /// 포장일자
        /// </summary>
        public string packing_date { get; set; }

        /// <summary>
        /// 입고량
        /// </summary>
        public string packing_qty { get; set; }

        /// <summary>
        /// 출하승인상태 (검색)
        /// </summary>
        public string issue_status { get; set; }

        /// <summary>
        /// 출하승인상태
        /// </summary>
        public string shipping_recognition_status { get; set; }

        /// <summary>
        /// 제조번호
        /// </summary>
        public string lot_no { get; set; }

        /// <summary>
        /// 유통기한
        /// </summary>
        public string end_date { get; set; }

        /// <summary>
        /// 입고상태
        /// </summary>
        public string receipt_status { get; set; }

        /// <summary>
        /// 제품입고일련번호
        /// </summary>
        public string item_stock_id { get; set; }

        /// <summary>
        /// 선택
        /// </summary>
        public string receipt_check { get; set; }

        /// <summary>
        /// 입고구분
        /// </summary>
        public string prod_return_ck { get; set; }

        /// <summary>
        /// 입고구분명
        /// </summary>
        public string prod_return_ck_nm { get; set; }

        /// <summary>
        /// 입고일자
        /// </summary>
        public string transfer_date { get; set; }

        /// <summary>
        /// 출하승인일자
        /// </summary>
        public string shipping_recognition_date { get; set; }

        /// <summary>
        /// 승인일자
        /// </summary>
        public string issue_time3 { get; set; }

        /// <summary>
        /// 의뢰번호
        /// </summary>
        public string testrequest_no { get; set; }

        /// <summary>
        /// 시험상태
        /// </summary>
        public string test_status { get; set; }

        /// <summary>
        /// 시험상태명
        /// </summary>
        public string test_status_nm { get; set; }

        /// <summary>
        /// 시험의뢰일자
        /// </summary>
        public string test_request_date { get; set; }

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
        /// (검색) 시작일
        /// </summary>
        public string start_date_S { get; set; }

        /// <summary>
        /// (검색) 종료일
        /// </summary>
        public string end_date_S { get; set; }

        /// <summary>
        /// (검색) 출하승인
        /// </summary>
        public string s_issue_status { get; set; }

        /// <summary>
        /// (검색) 입고상태
        /// </summary>
        public string s_receipt_status { get; set; }

        /// <summary>
        /// (검색) 입고구분
        /// </summary>
        public string prod_return_status { get; set; }

        /// <summary>
        /// (검색) 날짜 검색조건
        /// </summary>
        public string search_ck { get; set; }

        /// <summary>
        /// (검색) 제품코드
        /// </summary>
        public string item_cd_S { get; set; }

        /// <summary>
        /// (검색) 제품명
        /// </summary>
        public string item_nm_S { get; set; }
    }
}