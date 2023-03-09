using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysItem
{
    public class Material
    {
        public string pGubun { get; set; }

        public string Data_root { get; set; }

        [Key]
        [Required(ErrorMessage = "원료코드를 입력해주세요")]
        [DisplayName("원료코드")]
        public string item_cd { get; set; }

        [Required(ErrorMessage = "원료명을 입력해주세요")]
        public string item_nm { get; set; }
        public string item_nm_registry { get; set; }        
        public string abbreviation_cd { get; set; }
        public string item_s_nm { get; set; }
        public string item_enm { get; set; }
        public string item_jnm { get; set; }
        public string prod_end { get; set; }
        public string break_type { get; set; }
        public string outer_process { get; set; }
        public string in_out_ck { get; set; }
        public string abc_gubun { get; set; }
        public string plant_cd { get; set; }
        public string item_type1 { get; set; }
        public string form_item_type1 { get; set; }
        public string item_type2 { get; set; }
        public string form_item_type2 { get; set; }

        [Required(ErrorMessage = "원료 관리단위를 입력해주세요")]
        public string item_unit_cd { get; set; }
        public string unit_price { get; set; } // 구매단가
        public string item_exam_yn { get; set; }
        public string sampling_calc { get; set; }
        public string item_validity_period { get; set; }

        [Required(ErrorMessage = "유효기간 단위를 입력해주세요")]
        public string item_validity_period_unit { get; set; }
        public string item_keep_condition { get; set; }
        public string item_keep_temperature { get; set; }
        public string keeping_warehouse { get; set; }
        public string hs_no { get; set; }
        public string safe_stock { get; set; }
        public string monthly_qt { get; set; }
        public string basic_purchase_qty { get; set; }
        public string basic_purchase_unit { get; set; }
        public string basic_purchase_unit_nm { get; set; }
        public string delivery_period { get; set; }
        public string test_period { get; set; }
        public string material_maker { get; set; }
        public string item_case_cd { get; set; }
        public string sampling_qt { get; set; }
        public string sampling_unit { get; set; }
        public string sampling_emp_cd { get; set; }
        public string sampling_emp_nm { get; set; }
        public string ordering_type { get; set; }
        public string materialmaker_ck { get; set; }
        public string asepsis_item_ck { get; set; }
        public string kepping_term { get; set; }
        public string kepping_term_unit { get; set; }
        public string item_type3 { get; set; }
        public string container_shape { get; set; }
        public string pack_sampling_ck { get; set; }
        public string container_shape1_2 { get; set; }
        public string container_shape1_3 { get; set; }
        public string container_shape2_1 { get; set; }
        public string container_shape2_2 { get; set; }
        public string container_shape2_3 { get; set; }
        public string container_shape3_1 { get; set; }
        public string container_shape3_2 { get; set; }
        public string container_shape3_3 { get; set; }
        public string scale_factor { get; set; }
        public string item_safety_day { get; set; }
        public string trade_cd { get; set; }
        public string trade_nm { get; set; }
        public string hb_ck { get; set; }
        public string standard_cd { get; set; }
        public string standard_nm { get; set; }
        public string country_cd { get; set; }
        public string country_nm { get; set; }
        public string test_standard { get; set; }
        public string test_standard_1 { get; set; }
        public string gmo_material_yn { get; set; }
        public string retest_yn { get; set; }
        public string material_maker_cd { get; set; }
        public string material_maker_nm { get; set; }
        public string u_material_maker_cd { get; set; }
        public string material_cd { get; set; }
        public string manufacture_qty_yn { get; set; }
        public string taxfree_yn { get; set; }
        public string material_content_nm { get; set; }
        public string material_content_percent { get; set; }
        public int idx { get; set; }


        public Material()
        {

        }

        public Material(DataRow row)
        {
            //Data_root = row["Data_root"].ToString();
            //item_cd = row["item_cd"].ToString();
            //item_nm = row["item_nm"].ToString();
            //prod_end = row["prod_end"].ToString();

            Data_root = row["Data_root"].ToString();
            item_cd = row["item_cd"].ToString();
            item_nm = row["item_nm"].ToString();
            abbreviation_cd = row["abbreviation_cd"].ToString();
            item_s_nm = row["item_s_nm"].ToString();
            item_enm = row["item_enm"].ToString();
            item_jnm = row["item_jnm"].ToString();
            prod_end = row["prod_end"].ToString();
            break_type = row["break_type"].ToString();
            outer_process = row["outer_process"].ToString();
            in_out_ck = row["in_out_ck"].ToString();
            abc_gubun = row["abc_gubun"].ToString();
            plant_cd = row["plant_cd"].ToString();
            item_type1 = row["item_type1"].ToString();
            item_type2 = row["item_type2"].ToString();
            item_unit_cd = row["item_unit_cd"].ToString();
            item_exam_yn = row["item_exam_yn"].ToString();
            sampling_calc = row["sampling_calc"].ToString();
            item_validity_period = row["item_validity_period"].ToString();
            item_validity_period_unit = row["item_validity_period_unit"].ToString();
            item_keep_condition = row["item_keep_condition"].ToString();
            item_keep_temperature = row["item_keep_temperature"].ToString();
            keeping_warehouse = row["keeping_warehouse"].ToString();
            hs_no = row["hs_no"].ToString();
            safe_stock = row["safe_stock"].ToString();
            monthly_qt = row["monthly_qt"].ToString();
            basic_purchase_qty = row["basic_purchase_qty"].ToString();
            basic_purchase_unit = row["basic_purchase_unit"].ToString();
            basic_purchase_unit_nm = row["basic_purchase_unit_nm"].ToString();
            delivery_period = row["delivery_period"].ToString();
            test_period = row["test_period"].ToString();
            material_maker = row["material_maker"].ToString();
            item_case_cd = row["item_case_cd"].ToString();
            sampling_qt = row["sampling_qt"].ToString();
            sampling_unit = row["sampling_unit"].ToString();
            sampling_emp_cd = row["sampling_emp_cd"].ToString();
            sampling_emp_nm = row["sampling_emp_nm"].ToString();
            ordering_type = row["ordering_type"].ToString();
            materialmaker_ck = row["materialmaker_ck"].ToString();
            asepsis_item_ck = row["asepsis_item_ck"].ToString();
            kepping_term = row["kepping_term"].ToString();
            kepping_term_unit = row["kepping_term_unit"].ToString();
            item_type3 = row["item_type3"].ToString();
            container_shape = row["container_shape"].ToString();
            pack_sampling_ck = row["pack_sampling_ck"].ToString();
            container_shape1_2 = row["container_shape1_2"].ToString();
            container_shape1_3 = row["container_shape1_3"].ToString();
            container_shape2_1 = row["container_shape2_1"].ToString();
            container_shape2_2 = row["container_shape2_2"].ToString();
            container_shape2_3 = row["container_shape2_3"].ToString();
            container_shape3_1 = row["container_shape3_1"].ToString();
            container_shape3_2 = row["container_shape3_2"].ToString();
            container_shape3_3 = row["container_shape3_3"].ToString();
            scale_factor = row["scale_factor"].ToString();
            item_safety_day = row["item_safety_day"].ToString();
            trade_cd = row["trade_cd"].ToString();
            hb_ck = row["hb_ck"].ToString();
            standard_cd = row["standard_cd"].ToString();
            standard_nm = row["standard_nm"].ToString();
            country_cd = row["country_cd"].ToString();
            country_nm = row["country_nm"].ToString();
            test_standard_1 = row["test_standard_1"].ToString();
            gmo_material_yn = row["gmo_material_yn"].ToString();
            retest_yn = row["retest_yn"].ToString();

        }

    }
}
