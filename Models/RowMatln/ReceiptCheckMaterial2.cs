using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.RowMatln
{
    public class ReceiptCheckMaterial2
    {
        /// <summary>
        /// 순번
        /// </summary>
        public string receipt_check_seq { get; set; }

        /// <summary>
        /// 체크사항
        /// </summary>
        public string receipt_check_contents { get; set; }

        /// <summary>
        /// 체크결과
        /// </summary>
        public string receipt_check_result { get; set; }

        /// <summary>
        /// 조치사항
        /// </summary>
        public string receipt_check_fix { get; set; }
        
        /// <summary>
        /// 발주번호
        /// </summary>
        public string purchase_order_no { get; set; }

        /// <summary>
        /// 발주번호 시퀀스
        /// </summary>
        public string purchase_order_seq { get; set; }

        /// <summary>
        /// 품목코드
        /// </summary>
        public string item_cd { get; set; }

        /// <summary>
        /// 품목명
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// 거래처코드
        /// </summary>
        public string cust_cd { get; set; }

        /// <summary>
        /// 거래처명
        /// </summary>
        public string cust_nm { get; set; }

        /// <summary>
        /// 발주수량
        /// </summary>
        public string purchase_order_qty { get; set; }

        /// <summary>
        /// 단위
        /// </summary>
        public string purchase_order_unit { get; set; }

        /// <summary>
        /// 입고가능수량
        /// </summary>
        public string possible_qty { get; set; }

        /// <summary>
        /// 검수결과
        /// </summary>
        public string receipt_check_state { get; set; }

        /// <summary>
        /// 입고구분
        /// </summary>
        public string purchase_status { get; set; }

        /// <summary>
        /// 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
        /// </summary>
        public string option { get; set; }

        /// <summary>
        /// 원자재 구분 option 3 : 원료 / 2 : 자재 / 4 : 상품
        /// </summary>
        public string option2 { get; set; }

        /// <summary>
        /// 검수번호
        /// </summary>
        public string receipt_check_no { get; set; }

        /// <summary>
        /// 점검자 코드
        /// </summary>
        public string worker_cd { get; set; }

        /// <summary>
        /// 점검자
        /// </summary>
        public string worker_nm { get; set; }

        /// <summary>
        /// 확인자 코드
        /// </summary>
        public string checker_cd { get; set; }

        /// <summary>
        /// 확인자
        /// </summary>
        public string checker_nm { get; set; }

        /// <summary>
        /// 검수일자
        /// </summary>
        public string receipt_check_date { get; set; }

        /// <summary>
        /// 포장수량
        /// </summary>
        public string receipt_check_pack_form { get; set; }

        /// <summary>
        /// 원자재 코드
        /// </summary>
        public string material_cd { get; set; }

        /// <summary>
        /// 원자재 명
        /// </summary>
        public string material_nm { get; set; }

        /// <summary>
        /// 검수수량
        /// </summary>
        public string receipt_check_qty { get; set; }

        /// <summary>
        /// 표시된중량
        /// </summary>
        public string receipt_check_unit_wt { get; set; }

        /// <summary>
        /// 비고
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 구분값
        /// </summary>
        public string gubun { get; set; }

    }
}