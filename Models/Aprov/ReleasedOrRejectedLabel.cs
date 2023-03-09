using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Aprov
{
    public class ReleasedOrRejectedLabel
    {
        /// <summary>
        /// 일련번호
        /// </summary>
        public string testcontrol_id { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string test_type_nm { get; set; }

        /// <summary>
        /// 시험종류코드
        /// </summary>
        public string test_type { get; set; }

        /// <summary>
        /// (검색) 시험종류 s_test_type
        /// </summary>
        public string s_test_type { get; set; }

        /// <summary>
        /// 제조번호
        /// </summary>
        public string start_no { get; set; }

        /// <summary>
        /// 시험번호
        /// </summary>
        public string test_no { get; set; }

        /// <summary>
        /// 코드
        /// </summary>
        public string item_cd { get; set; }

        /// <summary>
        /// 품목
        /// </summary>
        public string item_nm { get; set; }

        /// <summary>
        /// item_enm
        /// </summary>
        public string item_enm { get; set; }

        /// <summary>
        /// 기존규격
        /// </summary>
        public string test_standard_nm { get; set; }

        /// <summary>
        /// 공정
        /// </summary>
        public string process_nm { get; set; }

        /// <summary>
        /// 포장수
        /// </summary>
        public string pack_cnt { get; set; }

        /// <summary>
        /// 승인일자
        /// </summary>
        public string test_date { get; set; }

        /// <summary>
        /// 적부코드
        /// </summary>
        public string test_result_yn { get; set; }

        /// <summary>
        /// 진행상태코드
        /// </summary>
        public string test_status { get; set; }

        /// <summary>
        /// (검색) 진행상태 s_test_status
        /// </summary>
        public string s_test_status { get; set; }

        /// <summary>
        /// 판정
        /// </summary>
        public string test_result_yn_nm { get; set; }

        /// <summary>
        /// 단계
        /// </summary>
        public string test_status_nm { get; set; }

        /// <summary>
        /// 배치번호
        /// </summary>
        public string enter_seq { get; set; }

        /// <summary>
        /// 사용(유효)기한
        /// </summary>
        public string valid_period { get; set; }

        /// <summary>
        /// erp T
        /// </summary>
        public string erp_lock_info { get; set; }

        /// <summary>
        /// erp dt
        /// </summary>
        public string erp_lot_end_date { get; set; }

        /// <summary>
        /// 유형
        /// </summary>
        public string item_type_nm { get; set; }

        /// <summary>
        /// 시험번호
        /// </summary>
        public string test_judge_no { get; set; }

        /// <summary>
        /// 제조원
        /// </summary>
        public string material_maker_nm { get; set; }

        /// <summary>
        /// 의뢰일자
        /// </summary>
        public string request_date { get; set; }

        /// <summary>
        /// 채취일자
        /// </summary>
        public string picking_date { get; set; }

        /// <summary>
        /// 기존규격
        /// </summary>
        public string test_standard { get; set; }

        /// <summary>
        /// 규격1
        /// </summary>
        public string test_standard_1 { get; set; }

        /// <summary>
        /// 규격2
        /// </summary>
        public string test_standard_2 { get; set; }

        /// <summary>
        /// 규격3
        /// </summary>
        public string test_standard_3 { get; set; }

        /// <summary>
        /// 규격4
        /// </summary>
        public string test_standard_4 { get; set; }

        /// <summary>
        /// 규격5
        /// </summary>
        public string test_standard_5 { get; set; }

        /// <summary>
        /// 규격
        /// </summary>
        public string test_standard_nm_1 { get; set; }

        /// <summary>
        /// 총입고량
        /// </summary>
        public string receipt_qty { get; set; }

        /// <summary>
        /// 프로그램코드
        /// </summary>
        public string program_cd { get; set; }

        /// <summary>
        /// 출력물
        /// </summary>
        public string program_nm { get; set; }

        /// <summary>
        /// 기본
        /// </summary>
        public string default_rpt_cnt { get; set; }

        /// <summary>
        /// 가능
        /// </summary>
        public string can_rpt_cnt { get; set; }

        /// <summary>
        /// 사원코드
        /// </summary>
        public string emp_cd { get; set; }

        /// <summary>
        /// 출력자
        /// </summary>
        public string emp_nm { get; set; }

        /// <summary>
        /// 출력일시
        /// </summary>
        public string prt_time { get; set; }

        /// <summary>
        /// 출력매수
        /// </summary>
        public string prt_cnt { get; set; }

        /// <summary>
        /// (검색) 의뢰일자 1 / 승인일자 2
        /// </summary>
        public string date_option { get; set; }

        /// <summary>
        /// (검색) 시작일
        /// </summary>
        public string start_date_S { get; set; }

        /// <summary>
        /// (검색) 종료일
        /// </summary>
        public string end_date_S { get; set; }

        /// <summary>
        /// (검색) 구분
        /// </summary>
        public string search_gubun { get; set; }

        /// <summary>
        /// (검색) search_number
        /// </summary>
        public string search_number { get; set; }

        /// <summary>
        /// (검색) 판정 -  적합 : Y / 부적합 N
        /// </summary>
        public string s_yn { get; set; }

        /// <summary>
        /// 리포트용
        /// </summary>

        public string test_result_yn_ap { get; set; }

        public string picking_qty { get; set; }

        public string sign_time { get; set; }

        public string qc_valid_period { get; set; }

        public string report_cd { get; set; }

        public string print_num { get; set; }

    }
}