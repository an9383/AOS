using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysItem
{
    public class ItemStandard
    {
        public string abbreviation_cd { get; set; } // 약식코드
        public string abc_gubun { get; set; }   // abc 구분
        public string break_type { get; set; }  // 사용중지 사유
        public string concentrate_yn { get; set; }  // 농축액추출
        public string container_shape { get; set; }
        public string in_out_ck { get; set; }
        public string item_cd { get; set; } // 제제코드
        public string item_cd_n { get; set; }
        public string item_class { get; set; }  // 분류코드
        public string item_content { get; set; }    // 단위중량
        public string item_error_range { get; set; } //단위중량 오차범위
        public string item_content_unit { get; set; }   // 단위중량 단위
        public string item_content2 { get; set; }   // 피막단위중량1
        public string item_content3 { get; set; }   // 피막단위중량2
        public string item_enm { get; set; }    // 영문명
        public string item_exchangerate { get; set; }   // 함량
        public string item_form_cd { get; set; }
        public string item_gb { get; set; }
        public string item_input_content { get; set; }  // 실충전량
        public string item_jnm { get; set; }    // 일문명
        public string item_keep_condition { get; set; } // 보관조건
        public string item_keep_temperature { get; set; }   // 보관온도
        public string item_keeping_term { get; set; }   // 보관기간
        public string item_keeping_unit { get; set; }   // 보관기간 단위
        public string item_license_caution { get; set; }
        public string item_license_current_ck { get; set; }
        public string item_license_dosage { get; set; }
        public string item_license_effect { get; set; }
        public string item_license_no { get; set; } // 개정번호
        public string item_license_start_date { get; set; } // 발효일자
        public string item_license_storage { get; set; }
        public string item_license_unit { get; set; }
        public string item_lot_real_size { get; set; }
        public string item_lot_real_size2 { get; set; }
        public string item_lot_size { get; set; }   // 대표배치단위(수량)
        public string item_lot_size_unit { get; set; }  // 대표배치단위(중량) 단위
        public string item_lot_size2 { get; set; }  // 대표배치단위(중량)
        public string item_make_cd { get; set; }
        public string item_nm { get; set; } // 제품명
        public string item_pack_barcode { get; set; }
        public string item_pack_size { get; set; }
        public string item_packing_type { get; set; }
        public string item_packunit { get; set; }   // 제제 단위
        public string item_packunit2 { get; set; }
        public string item_permission_date { get; set; }    // 허가일자
        public string item_permission_no { get; set; }  // 허가번호
        public string item_proc_no { get; set; }    // 제품표준서번호
        public string item_s_nm { get; set; }   // 약식명
        public string item_safety_day { get; set; }
        public string item_surface { get; set; }    // 성상
        public string item_type1 { get; set; }
        public string item_type2 { get; set; }
        public string item_type3 { get; set; }
        public string item_unit { get; set; }
        public string item_validity_period { get; set; }    // 유효기간
        public string item_validity_period_unit { get; set; }   // 유효기간 단위
        public string item_write_date { get; set; } 
        public string keeping_warehouse { get; set; }   // 보관창고
        public string lot_size_display_type { get; set; }
        public string Multi_Yn { get; set; }
        public string outer_process { get; set; }
        public string packing_major_ck { get; set; }
        public string plant_cd { get; set; }
        public string prod_end { get; set; }
        public string revision_remark { get; set; } // 개정사유
        public string safe_stock { get; set; }
        public string sampling_calc { get; set; }
        public string second_pack_qt { get; set; }
        public string second_pack_type { get; set; }
        public string set_Yn { get; set; }
        public string traceability_ck { get; set; }
        public string traceability_reg_num { get; set; }
        public string trade_cd { get; set; }
        public string trade_cd2 { get; set; }
        public string vender_item_cd { get; set; }
        public string weighing_time { get; set; }
        public int itemLicenseCount { get; set; }
        public string taxfree_yn { get; set; }
        public string gubun { get; set; }
        public string item_permission_type { get; set; }
        public string certification_type { get; set; } 

        public ItemStandard()
        {

        }
    }
}
