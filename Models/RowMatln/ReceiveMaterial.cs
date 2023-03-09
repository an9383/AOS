using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.RowMatln
{
    public class ReceiveMaterial
    {
		/// <summary>
		/// 인터페이스
		/// </summary>
		public string outer_interface { get; set; }

		/// <summary>
		/// 품목코드
		/// </summary>
		public string item_cd { get; set; }

		/// <summary>
		/// 품목명
		/// </summary>
		public string item_nm { get; set; }

		/// <summary>
		/// 무균원료체크
		/// </summary>
		public string asepsis_item_ck { get; set; }

		/// <summary>
		/// 상품 입고 품목
		/// </summary>
		public string item_stock_id { get; set; }

		/// <summary>
		/// 상품 입고 품목
		/// </summary>
		public string stock_qty { get; set; }
		
		/// <summary>
		/// 원자재구분
		/// </summary>
		public string item_gb { get; set; }

		/// <summary>
		/// 원료 3 , 자재 2 , 상품 1
		/// </summary>
		public string option2 { get; set; }

		/// <summary>
		/// 입고일자
		/// </summary>
		public string receipt_time_date { get; set; }

		/// <summary>
		/// 입고일자 (GMS 디자인)
		/// </summary>
		public string receipt_date { get; set; }

		/// <summary>
		/// 입고번호
		/// </summary>
		public string receipt_no { get; set; }

		/// <summary>
		/// 입고순번
		/// </summary>
		public string receipt_id { get; set; }

		/// <summary>
		/// 거래처 명 (공급원)
		/// </summary>
		public string cust_nm { get; set; }

		/// <summary>
		/// 거래처 코드 (공급원)
		/// </summary>
		public string cust_cd { get; set; }

		/// <summary>
		/// 제조처 명
		/// </summary>
		public string receipt_lot_cust_nm { get; set; }

		/// <summary>
		/// 입고량
		/// </summary>
		public string receipt_qty { get; set; }

		/// <summary>
		/// 재고량
		/// </summary>
		public string receipt_remain_qty { get; set; }

		/// <summary>
		/// 포장수량
		/// </summary>
		public string receipt_pack_qty { get; set; }

		/// <summary>
		/// 품목(영문)
		/// </summary>
		public string item_enm { get; set; }

		/// <summary>
		/// 보관조건
		/// </summary>
		public string item_keep_condition { get; set; }

		/// <summary>
		/// 포장재질
		/// </summary>
		public string Pack_quality { get; set; }

		/// <summary>
		/// 체크
		/// </summary>
		public string chk { get; set; }

		/// <summary>
		/// 발주번호
		/// </summary>
		public string purchase_no { get; set; }

		/// <summary>
		/// 발주일련번호
		/// </summary>
		public string purchase_id { get; set; }

		/// <summary>
		/// 재시험여부
		/// </summary>
		public string retest_yn { get; set; }

		/// <summary>
		/// 창고코드
		/// </summary>
		public string interface_field1 { get; set; }

		/// <summary>
		/// 구매그룹
		/// </summary>
		public string interface_field2 { get; set; }

		/// <summary>
		/// 입고유형
		/// </summary>
		public string interface_field3 { get; set; }

		/// <summary>
		/// 매입번호
		/// </summary>
		public string interface_field4 { get; set; }

		/// <summary>
		/// 매입순번
		/// </summary>
		public string interface_field5 { get; set; }

		/// <summary>
		/// 업로드여부
		/// </summary>
		public string upload_yn { get; set; }

		/// <summary>
		/// 업로드일시
		/// </summary>
		public string upload_dt { get; set; }

		/// <summary>
		/// 업로드비고
		/// </summary>
		public string upload_remark { get; set; }

		/// <summary>
		/// 환산계수
		/// </summary>
		public string scale_factor { get; set; }

		/// <summary>
		/// 제조일자
		/// </summary>
		public string receipt_lot_date { get; set; }

		/// <summary>
		/// 제조번호
		/// </summary>
		public string receipt_lot_no { get; set; }

		/// <summary>
		/// 사용기한(재시험일자)
		/// </summary>
		public string receipt_lot_valid_date { get; set; }

		/// <summary>
		/// 시험번호
		/// </summary>
		public string receipt_qc_no3 { get; set; }

		/// <summary>
		/// in_type
		/// </summary>
		public string in_type { get; set; }

		/// <summary>
		/// 라벨출력여부
		/// </summary>
		public string label_print { get; set; }

		/// <summary>
		/// 입고구분
		/// </summary>
		public string receipt_status { get; set; }

		/// <summary>
		/// 의뢰번호
		/// </summary>
		public string receipt_qc_no1 { get; set; }

		/// <summary>
		/// 시험번호
		/// </summary>
		public string receipt_qc_no3_qc { get; set; }

		/// <summary>
		/// 단위
		/// </summary>
		public string item_unit { get; set; }

		/// <summary>
		/// 제조처 코드
		/// </summary>
		public string receipt_material_maker_cd { get; set; }

		/// <summary>
		/// 포장단위
		/// </summary>
		public string receipt_pack_unit { get; set; }

		/// <summary>
		/// 포장량
		/// </summary>
		public string receipt_pack_in_qty { get; set; }

		/// <summary>
		/// receipt_time_hh
		/// </summary>
		public int receipt_time_hh { get; set; }

		/// <summary>
		/// receipt_time_mm
		/// </summary>
		public int receipt_time_mm { get; set; }

		/// <summary>
		/// receipt_rate
		/// </summary>
		public double receipt_rate { get; set; }

		/// <summary>
		/// 창고
		/// </summary>
		public string warehouse { get; set; }

		/// <summary>
		/// 창고명
		/// </summary>
		public string warehouse_nm { get; set; }

		/// <summary>
		/// 창고위치
		/// </summary>
		public string warehouse_location { get; set; }

		/// <summary>
		/// 창고위치 명
		/// </summary>
		public string warehouse_location_nm { get; set; }

		/// <summary>
		/// 비고
		/// </summary>
		public string remark { get; set; }

		/// <summary>
		/// 입고상태 (적합여부)
		/// </summary>
		public string receipt_status_nm { get; set; }

		/// <summary>
		/// test_rdate
		/// </summary>
		public string test_rdate { get; set; }

		/// <summary>
		/// 유효기간
		/// </summary>
		public string valid_period { get; set; }

		/// <summary>
		/// sampler_cd
		/// </summary>
		public string sampler_cd { get; set; }

		/// <summary>
		/// sampler_nm
		/// </summary>
		public string sampler_nm { get; set; }

		/// <summary>
		/// sampling_date
		/// </summary>
		public string sampling_date { get; set; }

		/// <summary>
		/// sampled_qty
		/// </summary>
		public double sampled_qty { get; set; }

		/// <summary>
		/// 승인자 코드
		/// </summary>
		public string receipt_confirm_emp_cd { get; set; }

		/// <summary>
		/// 승인자 명
		/// </summary>
		public string receipt_confirm_emp_nm { get; set; }

		/// <summary>
		/// 승인 날짜
		/// </summary>
		public string receipt_confirm_emp_time { get; set; }

		/// <summary>
		/// receipt_confirm_emp_type
		/// </summary>
		public string receipt_confirm_emp_type { get; set; }

		/// <summary>
		/// receipt_batch
		/// </summary>
		public string receipt_batch { get; set; }

		/// <summary>
		/// 포장내역
		/// </summary>
		public string pack_form { get; set; }

		/// <summary>
		/// 시험필요여부
		/// </summary>
		public string item_exam_yn { get; set; }

		/// <summary>
		/// (QC)함량
		/// </summary>
		public string receipt_qc_pollination { get; set; }

		/// <summary>
		/// 판정역가(QA역가)
		/// </summary>
		public string receipt_qc_rate { get; set; }

		/// <summary>
		/// receipt_ca_rate
		/// </summary>
		public string receipt_ca_rate { get; set; }

		/// <summary>
		/// 통관번호
		/// </summary>
		public string cc_no { get; set; }

		/// <summary>
		/// 통관순번
		/// </summary>
		public string cc_seq { get; set; }

		/// <summary>
		/// LC번호
		/// </summary>
		public string lc_no { get; set; }

		/// <summary>
		/// LC순번
		/// </summary>
		public string lc_seq { get; set; }

		/// <summary>
		/// 재고 상태 코드
		/// </summary>
		public string imported_faulty_goods_change_cd { get; set; }

		/// <summary>
		/// 재고 상태명
		/// </summary>
		public string imported_faulty_goods_change_nm { get; set; }

		/// <summary>
		/// 재고 상태
		/// </summary>
		public string imported_faulty_goods_change { get; set; }

		/// <summary>
		/// 입고타입
		/// </summary>
		public string receipt_type { get; set; }

		/// <summary>
		/// 제조제품 코드
		/// </summary>
		public string make_item_cd { get; set; }

		/// <summary>
		/// 제조제품 명
		/// </summary>
		public string make_item_nm { get; set; }

		/// <summary>
		/// cost_cd
		/// </summary>
		public string cost_cd { get; set; }

		/// <summary>
		/// 발주일자
		/// </summary>
		public string purchase_date { get; set; }

		/// <summary>
		/// 검수번호
		/// </summary>
		public string receipt_check_no { get; set; }

		/// <summary>
		/// 검수번호id
		/// </summary>
		public string receipt_check_id { get; set; }

		/// <summary>
		/// 조달구분
		/// </summary>
		public string obtain_status { get; set; }

		/// <summary>
		/// 생산입고
		/// </summary>
		public string product_receipt { get; set; }

		/// <summary>
		/// keyfield
		/// </summary>
		public string keyfield { get; set; }

		/// <summary>
		/// displayfield
		/// </summary>
		public string displayfield { get; set; }


		/// <summary>
		/// 팩 입고량 순번
		/// </summary>
		public string receipt_pack_seq { get; set; }

		/// <summary>
		/// 랙번호
		/// </summary>
		public string location_no { get; set; }

		/// <summary>
		/// 랙
		/// </summary>
		public string location_nm { get; set; }

		/// <summary>
		/// 창고 코드
		/// </summary>
		public string workroom_cd { get; set; }


		/// <summary>
		/// 창고 명
		/// </summary>
		public string workroom_nm { get; set; }

		/// <summary>
		/// (검색) 시작일
		/// </summary>
		public string start_date { get; set; }

		/// <summary>
		/// (검색) 종료일
		/// </summary>
		public string end_date { get; set; }

		/// <summary>
		/// (검색) 품목코드
		/// </summary>
		public string s_item_cd { get; set; }

		/// <summary>
		/// (검색) 품목명
		/// </summary>
		public string s_item_nm { get; set; }

		/// <summary>
		/// (검색) 시험번호
		/// </summary>
		public string lot_cust { get; set; }

		/// <summary>
		/// (검색) 입고번호
		/// </summary>
		public string s_receipt_no { get; set; }

		/// <summary>
		/// (검색) 원료 자재 구분
		/// </summary>
		public string Material_Or_PackMaterial { get; set; }

		/// <summary>
		/// (검색) ReceiptType
		/// </summary>
		public string ReceiptType { get; set; }

		/// <summary>
		/// (검색) 품목명/품목코드
		/// </summary>
		public string search_date { get; set; }

		/// <summary>
		/// 원자재 조달구분
		/// </summary>
		public string obtail_status { get; set; }

		/// <summary>
		/// 팩 바코드
		/// </summary>
		public string receipt_pack_barcode { get; set; }

		/// <summary>
		/// 팩 재고량
		/// </summary>
		public double receipt_pack_remain_qty { get; set; }

		/// <summary>
		/// (전자서명) 아이디
		/// </summary>
		public string txt_ID { get; set; }

		/// <summary>
		/// (전자서명) 비밀번호
		/// </summary>
		public string txt_Pass { get; set; }

		/// <summary>
		/// (전자서명) 부서
		/// </summary>
		public string dept_nm { get; set; }

		/// <summary>
		/// (전자서명) 성명
		/// </summary>
		public string emp_nm { get; set; }

		/// <summary>
		/// 생성번호
		/// </summary>
		public string request_no { get; set; }

		/// <summary>
		/// 시험의뢰 요청날짜
		/// </summary>
		public string request_date { get; set; }

		/// <summary>
		/// 시험의뢰 번호
		/// </summary>
		public string testrequest_no { get; set; }

		/// <summary>
		/// 입고 제한 여부
		/// </summary>
		public string materialmaker_ck { get; set; }

		/// <summary>
		/// CRUD 구분 값
		/// </summary>
		public string gubun { get; set; }

		/// <summary>
		/// container_shape
		/// </summary>
		public string container_shape { get; set; }

		/// <summary>
		/// 생산입고 체크여부
		/// </summary>
		public string product_receipt_YN { get; set; }


		public double old_receipt_pack_remain_qty { get; set; }

		public double new_receipt_pack_remain_qty { get; set; }

		public string receipt_pack_remark { get; set; }
	}
}