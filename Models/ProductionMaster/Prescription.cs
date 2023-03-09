using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.ProductionMaster
{
    public class Prescription
    {
        public string pGubun { get; set; }
        public string item_cd { get; set; }
        public string sel_ck { get; set; }
        public string item_bom_id { get; set; }
        public int item_bom_seq { get; set; }
        public string item_bom_shild_cd { get; set; }
        public string item_child_type { get; set; }
        public string item_case_cd { get; set; }
        public string item_case_cd2 { get; set; }
        public string item_case_nm2 { get; set; }
        public string item_bom_tea_qty { get; set; }
        public string item_bom_standard_ucl_qty { get; set; }
        public string item_bom_batch_qty { get; set; }
        public string item_bom_standard_qty { get; set; }
        public string item_bom_tea_unit { get; set; }
        public string item_bom_batch_unit { get; set; }

        [Required(ErrorMessage = "공정코드를 입력해주세요")]
        public string process_cd { get; set; }
        [Required(ErrorMessage = "원료 관리단위를 입력해주세요")]
        public string process_nm { get; set; }
        public string item_bom_child_nm { get; set; }
        public string item_bom_use_rate { get; set; }
        public string item_bom_calc_type { get; set; }
        public string item_bom_last_type { get; set; }
        public string outer_interface { get; set; }
        public string remark { get; set; }
        public string sum_ck { get; set; }
        public string item_bom_liquid_material_yn { get; set; }
        public string item_bom_density { get; set; }
        public string item_bom_rounding { get; set; }
        public string item_bom_current_ck { get; set; }
        public string min_titer { get; set; }
        public string item_bom_standard_rpt_qty { get; set; }
        public string replacement_item_YN { get; set; }
        public string balance_YN { get; set; }
        public string outer_process { get; set; }
        public string material_percent { get; set; }
        public string trade_cd { get; set; }
        public string manufacture_percent { get; set; }
        public string item_bom_manufacture_qty { get; set; }
        public string item_bom_specific_gravity { get; set; }
        public string item_bom_main_material_ck { get; set; }
        public string item_bom_no { get; set; }
        public string batch_size { get; set; }
        public string item_bom_child_cd { get; set; }
        public string item_bom_child_type { get; set; }
        public string target_process_cd { get; set; }
        public string target_process_nm { get; set; }

        public Prescription()
        {

        }

        public Prescription(DataRow row, int type)
        {
            if (type == 1)
            {
                sel_ck = row["sel_ck"].ToString();
                item_bom_id = row["item_bom_id"].ToString();
                item_bom_seq = Int32.Parse(row["item_bom_seq"].ToString());
                item_bom_child_cd = row["item_bom_child_cd"].ToString();
                item_bom_child_type = row["item_bom_child_type"].ToString();
                item_case_cd = row["item_case_cd"].ToString();
                item_case_cd2 = row["item_case_cd2"].ToString();
                item_case_nm2 = row["item_case_nm2"].ToString();
                item_bom_tea_qty = row["item_bom_tea_qty"].ToString();
                item_bom_standard_ucl_qty = row["item_bom_standard_ucl_qty"].ToString();
                item_bom_batch_qty = row["item_bom_batch_qty"].ToString();
                item_bom_standard_qty = row["item_bom_standard_qty"].ToString();
                item_bom_tea_unit = row["item_bom_tea_unit"].ToString();
                item_bom_batch_unit = row["item_bom_batch_unit"].ToString();
                process_cd = row["process_cd"].ToString();
                process_nm = row["process_nm"].ToString();
                item_bom_child_nm = row["item_bom_child_nm"].ToString();
                item_bom_use_rate = row["item_bom_use_rate"].ToString();
                item_bom_calc_type = row["item_bom_calc_type"].ToString();
                item_bom_last_type = row["item_bom_last_type"].ToString();
                outer_interface = row["outer_interface"].ToString();
                remark = row["remark"].ToString();
                sum_ck = row["sum_ck"].ToString();
                item_bom_liquid_material_yn = row["item_bom_liquid_material_yn"].ToString();
                item_bom_density = row["item_bom_density"].ToString();
                item_bom_rounding = row["item_bom_rounding"].ToString();
                item_bom_current_ck = row["item_bom_current_ck"].ToString();
                min_titer = row["min_titer"].ToString();
                item_bom_standard_rpt_qty = row["item_bom_standard_rpt_qty"].ToString();
                replacement_item_YN = row["replacement_item_YN"].ToString();
                balance_YN = row["balance_YN"].ToString();
                outer_process = row["outer_process"].ToString();
                material_percent = row["material_percent"].ToString();
                trade_cd = row["trade_cd"].ToString();
                manufacture_percent = row["manufacture_percent"].ToString();
                item_bom_manufacture_qty = row["item_bom_manufacture_qty"].ToString();
                item_bom_specific_gravity = row["item_bom_specific_gravity"].ToString();
                item_bom_main_material_ck = row["item_bom_main_material_ck"].ToString();
                target_process_cd = row["target_process_cd"] == null ? "" : row["target_process_cd"].ToString();
                target_process_nm = row["target_process_nm"] == null ? "" : row["target_process_nm"].ToString();
            }
            else
            {
                item_cd = row["item_cd"].ToString();
                sel_ck = row["sel_ck"].ToString();
                item_bom_id = row["item_bom_id"].ToString();
                item_bom_seq = Int32.Parse(row["public int item_bom_seq"].ToString());
                //public int item_bom_seq = row["public int item_bom_seq"].ToString();
                item_bom_child_cd = row["item_bom_child_cd"].ToString();
                item_child_type = row["item_child_type"].ToString();
                item_case_cd = row["item_case_cd"].ToString();
                item_case_cd2 = row["item_case_cd2"].ToString();
                item_case_nm2 = row["item_case_nm2"].ToString();
                item_bom_tea_qty = row["item_bom_tea_qty"].ToString();
                item_bom_standard_ucl_qty = row["item_bom_standard_ucl_qty"].ToString();
                item_bom_batch_qty = row["item_bom_batch_qty"].ToString();
                item_bom_standard_qty = row["item_bom_standard_qty"].ToString();
                item_bom_tea_unit = row["item_bom_tea_unit"].ToString();
                item_bom_batch_unit = row["item_bom_batch_unit"].ToString();
                process_cd = row["process_cd"].ToString();
                process_nm = row["process_nm"].ToString();
                item_bom_child_nm = row["item_bom_child_nm"].ToString();
                item_bom_use_rate = row["item_bom_use_rate"].ToString();
                item_bom_calc_type = row["item_bom_calc_type"].ToString();
                item_bom_last_type = row["item_bom_last_type"].ToString();
                outer_interface = row["outer_interface"].ToString();
                remark = row["remark"].ToString();
                sum_ck = row["sum_ck"].ToString();
                item_bom_liquid_material_yn = row["item_bom_liquid_material_yn"].ToString();
                item_bom_density = row["item_bom_density"].ToString();
                item_bom_rounding = row["item_bom_rounding"].ToString();
                item_bom_current_ck = row["item_bom_current_ck"].ToString();
                min_titer = row["min_titer"].ToString();
                item_bom_standard_rpt_qty = row["item_bom_standard_rpt_qty"].ToString();
                replacement_item_YN = row["replacement_item_YN"].ToString();
                balance_YN = row["balance_YN"].ToString();
                outer_process = row["outer_process"].ToString();
                material_percent = row["material_percent"].ToString();
                trade_cd = row["trade_cd"].ToString();
                manufacture_percent = row["manufacture_percent"].ToString();
                item_bom_manufacture_qty = row["item_bom_manufacture_qty"].ToString();
                item_bom_specific_gravity = row["item_bom_specific_gravity"].ToString();
                item_bom_main_material_ck = row["item_bom_main_material_ck"].ToString();
                item_bom_no = row["item_bom_no"].ToString();
                batch_size = row["batch_size"].ToString();
                item_bom_child_cd = row["item_bom_child_cd"].ToString();
                item_bom_child_type = row["item_bom_child_type"].ToString();
            }
        }
    }
}
