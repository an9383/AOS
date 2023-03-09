using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.ProductionManage
{
    public class WorkorderRequest
    {
        public string ITEM_BOM_USE_RATE { get; set; }
        public string REQ_CONFIRM_DATE { get; set; }
        public string REQ_ORDER_PRINT_YN { get; set; }
        public string confirm { get; set; }
        public string item_bom_id { get; set; }
        public string item_bom_no { get; set; }
        public string item_bom_standard_ucl_qty { get; set; }
        public string item_cd { get; set; }
        public string item_nm { get; set; }
        public string material_percent { get; set; }
        public string mp_ck { get; set; }
        public string order_no { get; set; }
        public string process_cd { get; set; }
        public string process_nm { get; set; }
        public string req_order_batch_qty { get; set; }
        public string req_order_batch_unit { get; set; }
        public string req_order_calc_type { get; set; }
        public string req_order_calc_type_nm { get; set; }
        public string req_order_case_cd { get; set; }
        public string req_order_child_cd { get; set; }
        public string req_order_child_nm { get; set; }
        public string req_order_child_packunit { get; set; }
        public string req_order_child_type { get; set; }
        public string req_order_conversion_qty { get; set; }
        public string req_order_conversion_unit { get; set; }
        public string req_order_density { get; set; }
        public string req_order_gb { get; set; }
        public string req_order_gb_nm { get; set; }
        public string req_order_id { get; set; }
        public string req_order_last_type { get; set; }
        public string req_order_manufacture_qty { get; set; }
        public string req_order_qty { get; set; }
        public string req_order_real_qty { get; set; }
        public string req_order_remark { get; set; }
        public string req_order_seq { get; set; }
        public string req_order_standard_qty { get; set; }
        public string req_order_status { get; set; }
        public string req_order_status_nm { get; set; }
        public string req_order_tea_qty { get; set; }
        public string req_order_tea_unit { get; set; }
        public string req_order_unit { get; set; }
        public string req_order_use_rate { get; set; }
        public string sum_ck { get; set; }
        public string use_chk { get; set; }
        public string std_pollination_rate { get; set; }
        public string INPUT_ORDER_ID { get; set; }
        public string PROCESS_CD { get; set; }
        public string RECEIPT_QC_RATE { get; set; }
        public string REQ_ORDER_MATH { get; set; }
        public string material_density { get; set; }
        public string qc_remark { get; set; }
        public string receipt_qc_pollination { get; set; }
        public string receipt_qc_solvent { get; set; }
        public string remarks { get; set; }
        public string req_order_material_id { get; set; }
        public string req_order_material_qc_no { get; set; }
        public string req_order_material_qty { get; set; }
        public string req_order_real_weight { get; set; }
        public string separate_count { get; set; }
    }
}
