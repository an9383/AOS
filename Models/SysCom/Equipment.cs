using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysCom
{
    public class Equipment
    {
        [Required]
        public string equip_cd { get; set; }
        [Required]
        public string equip_nm { get; set; }
        public string equip_enm { get; set; }
        public string equip_manage_num { get; set; }
        public string equip_item_nm { get; set; }
        public string equip_model_num { get; set; }
        public string equip_serial_num { get; set; }
        public string equip_volume { get; set; }
        [Required]
        public string equip_type { get; set; }
        public string equip_type2 { get; set; }
        public string equip_prod_cust { get; set; }
        public string equip_prod_num { get; set; }
        public string equip_buy_cust { get; set; }
        public string equip_manage_cust { get; set; }
        public string equip_response_emp { get; set; }
        public string equip_buy_date { get; set; }
        public string equip_install_date { get; set; }
        public string equip_work_date { get; set; }
        public string equip_disuse_date { get; set; }
        public string equip_buy_amt { get; set; }
        [Required]
        public string equip_use_gb { get; set; }
        public string equip_property { get; set; }
        [Required]
        public string plant_cd { get; set; }
        public string equip_mcd { get; set; }
        [Required]
        public string workroom_cd { get; set; }
        public string interface_type { get; set; }
        public string interface_cd { get; set; }
        public string plc_node_no { get; set; }
        public string insert_emp { get; set; }
        public string equip_essential_level { get; set; }
        public string equip_prod_cust_cd { get; set; }
        public string equip_buy_cust_cd { get; set; }
        public string equip_manage_cust_cd { get; set; }
        public string equip_disuse_reason { get; set; }
        public string error_plus { get; set; }
        public string error_minus { get; set; }
        public string error_unit { get; set; }
        public string w_port_no { get; set; }
        public string interface_position_no { get; set; }
        public string interval { get; set; }
        public string equip_purpose { get; set; }
        public string manufacture_date { get; set; }
        public string equip_standard { get; set; }
        public string dept_cd { get; set; }
        public string workroom_response_emp { get; set; }
        public string unusual_memo { get; set; }
        public string sop_no { get; set; }
        public string line_equip_cd { get; set; }
        public string ip_address { get; set; }
        public string ip_subnet { get; set; }
        public string ip_port { get; set; }
        public string com_port { get; set; }
        public string com_baud_rate { get; set; }
        public string com_data_bits { get; set; }
        public string com_parity { get; set; }
        public string com_stop_bits { get; set; }
        public string com_handshaking { get; set; }
        public string interface_use_gb { get; set; }
        public string ccp_mont_gb { get; set; }
        public string alarm_code { get; set; }


        public Equipment()
        {

        }
    }
}
