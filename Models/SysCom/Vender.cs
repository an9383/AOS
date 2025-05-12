using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysCom
{
    public class Vender
    {
        [Required]
        public string vender_cd { get; set; }
        [Required]
        public string vender_nm { get; set; }
        public string vender_enm { get; set; }
        public string uptae { get; set; }
        public string upjong { get; set; }
        public string license { get; set; }
        public string owner_nm { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string zipcode { get; set; }
        public string address { get; set; }
        public string commercial_personnel { get; set; }
        public string technical_personnel { get; set; }
        public string personnel_tel1 { get; set; }
        public string personnel_tel2 { get; set; }
        public string personnel_email1 { get; set; }
        public string personnel_email2 { get; set; }
        public string idate { get; set; }
        public string bigo { get; set; }
        public string prod_cust_ck { get; set; }
        [Required]
        public string sell_cust_ck { get; set; }
        public string manage_cust_ck { get; set; }
        [Required]
        public string buy_ck { get; set; }
        public string discount_rate { get; set; }
        public string email_ad { get; set; }
        [Required]
        public string use_yn { get; set; }
        public string at_emp_cd { get; set; }
        public string sys_plant_cd { get; set; }
        [Required]
        public string vender_gb { get; set; }


        public Vender()
        {

        }
    }
}
