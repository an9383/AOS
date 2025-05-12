using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Aprov
{
    public class ShippingRecognition
    {
        /// <summary>
        /// 출하승인코드
        /// </summary>
        public string shipping_cd { get; set; }

        /// <summary>
        /// 제품코드
        /// </summary>
        public string item_cd { get; set; }

        /// <summary>
        /// 제품명
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// 제조번호
        /// </summary>
        public string lot_no { get; set; }

        /// <summary>
        /// 시험번호
        /// </summary>
        public string test_no { get; set; }

        /// <summary>
        /// prod_return_ck
        /// </summary>
        public string prod_return_ck { get; set; }

        /// <summary>
        /// 의뢰수량
        /// </summary>
        public string request_qty { get; set; }

        /// <summary>
        /// 단위
        /// </summary>
        public string request_unit { get; set; }

        /// <summary>
        /// 의뢰일자
        /// </summary>
        public string request_date { get; set; }

        /// <summary>
        /// 상태
        /// </summary>
        public string shipping_status { get; set; }

        /// <summary>
        /// 상태명
        /// </summary>
        public string shipping_status_nm { get; set; }

        /// <summary>
        /// approval_emp_cd
        /// </summary>
        public string approval_emp_cd { get; set; }

        /// <summary>
        /// 승인일자
        /// </summary>
        public string approval_date { get; set; }

        /// <summary>
        /// 제조제품
        /// </summary>
        public string item_make_cd { get; set; }

        /// <summary>
        /// 포장단위
        /// </summary>
        public string pack_unit { get; set; }

        /// <summary>
        /// 업로드 여부
        /// </summary>
        public string upload_yn { get; set; }

        /// <summary>
        /// 업로드 일시
        /// </summary>
        public string upload_dt { get; set; }

        /// <summary>
        /// 제조일자
        /// </summary>
        public string lot_date { get; set; }

        /// <summary>
        /// 유효기간
        /// </summary>
        public string item_validity_period { get; set; }

        /// <summary>
        /// 유효기간타입명
        /// </summary>
        public string item_validity_period_unit_name { get; set; }

        /// <summary>
        /// 서명이름
        /// </summary>
        public string sign_set_nm { get; set; }

        /// <summary>
        /// sign_set_dt_id
        /// </summary>
        public string sign_set_dt_id { get; set; }

        /// <summary>
        /// displayfield
        /// </summary>
        public string displayfield { get; set; }

        /// <summary>
        /// responsible_emp_cd
        /// </summary>
        public string responsible_emp_cd { get; set; }

        /// <summary>
        /// responsible_emp_nm
        /// </summary>
        public string responsible_emp_nm { get; set; }

        /// <summary>
        /// responsible_authority
        /// </summary>
        public string responsible_authority { get; set; }

        /// <summary>
        /// sign_set_dt_seq
        /// </summary>
        public string sign_set_dt_seq { get; set; }

        /// <summary>
        /// sign_emp_cd
        /// </summary>
        public string sign_emp_cd { get; set; }

        /// <summary>
        /// sign_time
        /// </summary>
        public string sign_time { get; set; }

        /// <summary>
        /// sign_yn
        /// </summary>
        public string sign_yn { get; set; }

        /// <summary>
        /// sign_emp_nm
        /// </summary>
        public string sign_emp_nm { get; set; }

        /// <summary>
        /// sign_image
        /// </summary>
        public string sign_image { get; set; }

        /// <summary>
        /// prev_sign_yn
        /// </summary>
        public string prev_sign_yn { get; set; }

        /// <summary>
        /// next_sign_yn
        /// </summary>
        public string next_sign_yn { get; set; }

        /// <summary>
        /// ck1
        /// </summary>
        public string ck1 { get; set; }

        /// <summary>
        /// ck2
        /// </summary>
        public string ck2 { get; set; }

        /// <summary>
        /// 제조지시번호
        /// </summary>
        public string order_no { get; set; }

        /// <summary>
        /// pack_order_no
        /// </summary>
        public string pack_order_no { get; set; }

        /// <summary>
        /// test_status
        /// </summary>
        public string test_status { get; set; }

        /// <summary>
        /// status
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 점검번호
        /// </summary>
        public string check_no { get; set; }

        /// <summary>
        /// 점검항목코드
        /// </summary>
        public string check_item_cd { get; set; }

        /// <summary>
        /// 점검항목
        /// </summary>
        public string check_item_nm { get; set; }

        /// <summary>
        /// 확인
        /// </summary>
        public string check_yn { get; set; }

        /// <summary>
        /// 검토의견
        /// </summary>
        public string check_opinion { get; set; }

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
        /// (검색) 상태
        /// </summary>
        public string status_S { get; set; }

        /// <summary>
        /// (검색) 검색날짜 타입
        /// </summary>
        public string date_type_s { get; set; }


        public string gubun { get; set; }

        // (서명) emp_cd
        public string emp_cd { get; set; }

        // (서명) representative_yn
        public string representative_yn { get; set; }

        // (서명) emp_cd
        public string validation_type { get; set; }


    }
}