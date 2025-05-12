using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysItem
{
    public class PackMaterial
    {
        [Required]
        public string item_cd { get; set; } // 자재코드
        [Required]
        public string item_nm { get; set; } // 자재명
        public string abbreviation_cd { get; set; } // 약식코드
        public string item_s_nm { get; set; }   // 약식품명
        public string item_enm { get; set; }    // 영문명
        public string item_jnm { get; set; }    // 일문명
        public string prod_end { get; set; }    // 사용여부
        public string break_type { get; set; }  // 사용중지사유
        public string print_item { get; set; }  // 표시자재여부
        public string outer_process { get; set; }   // 외주구분
        public string in_out_ck { get; set; }   // 국내외구분
        public string abc_gubun { get; set; }   // abc 구분
        [Required]
        public string plant_cd { get; set; }    // 사업장
        public string item_type1 { get; set; }  // 생산동
        public string item_type2 { get; set; }  // 팀
        [Required]
        public string item_type3 { get; set; }  // 제형구분
        [Required]
        public string item_unit { get; set; }   // 자재 관리단위
        public string item_exam_yn { get; set; }    // 시험필요여부
        public string sampling_calc { get; set; }   // 샘플링방법
        public string item_validity_period { get; set; }    // 보관기간
        public string item_validity_period_unit { get; set; }   // 보관기간 단위(년, 월, 일)
        public string item_keep_condition { get; set; } // 보관조건
        public string item_keep_temperature { get; set; }   // 보관온도
        public string keeping_warehouse { get; set; }   // ~보관창고~
        public string item_pack_barcode { get; set; }   // 제품바코드
        public string material_out_type { get; set; }   // 소수점 계산법
        public string basic_purchase_qty { get; set; }  // 발주량
        public string basic_purchase_unit { get; set; } // 발주단위
        public string delivery_period { get; set; } // 입고기간
        public string test_period { get; set; } // 시험기간
        public string container_shape { get; set; } // 포장재질
        public string scale_factor { get; set; }    // 환산계수
        public string item_sampling { get; set; }   // 통별샘플링대상
        public string item_pack_size { get; set; }  // 환산개수
        public string trade_cd { get; set; }    // 브랜드
        public string trade_nm { get; set; }    // 브랜드명
        public string item_form_cd { get; set; }    // 제형
        public string trade_cd2 { get; set; }   // 브랜드제형
        public string management_type { get; set; } // 관리유형
        public string unit_price { get; set; } // 구매단가
        public string hs_no { get; set; }
        public string sampling_report_type { get; set; }
        public string test_level { get; set; }
        public string safe_stock { get; set; }
        public string monthly_qt { get; set; }
        public string material_maker { get; set; }
        public string item_case_cd { get; set; }
        public string sampling_qt { get; set; }
        public string sampling_unit { get; set; }
        public string sampling_emp_cd { get; set; }
        public string ordering_type { get; set; }
        public string materialmaker_ck { get; set; }
        public string at_emp_cd { get; set; }
        public string item_keeping_term { get; set; }
        public string item_keeping_unit { get; set; }
        public string item_safety_day { get; set; }
        public string item_cd_n { get; set; }
        public string material_maker_cd { get; set; }
        public string material_maker_nm { get; set; }
        public string u_material_maker_cd { get; set; }
        public string material_cd { get; set; }
        public string pGubun { get; set; }
        public string manufacture_qty_yn { get; set; }
        public string taxfree_yn { get; set; }

        public string item_spec_type { get; set; }
        public string item_spec_qty { get; set; }


        public PackMaterial()
        {

        }
    }
}
