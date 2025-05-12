using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysOp
{
    public class ElectronicSigniture
    {
        [Required]
        public string sign_set_cd { get; set; }
        [Required]
        public string sign_set_nm { get; set; }
        [Required]
        public string sign_set_nm_dis { get; set; }
        public string sign_set_content { get; set; }
        [Required]
        public string use_yn { get; set; }
        [Required]
        public string test_module_yn { get; set; }
        [Required]
        public string doc_module_yn { get; set; }
        public string link_point { get; set; }
        public string linked_module { get; set; }
        public string linked_table { get; set; }
        public string linked_remark { get; set; }
        public string linked_value1 { get; set; }
        public string linked_value2 { get; set; }
        public string linked_value3 { get; set; }

        public ElectronicSigniture(DataRow row)
        {
            this.sign_set_cd = row["sign_set_cd"].ToString();
            this.sign_set_nm = row["sign_set_nm"].ToString();
            this.sign_set_nm_dis = row["sign_set_nm_dis"].ToString();
            this.sign_set_content = row["sign_set_content"].ToString();
            this.use_yn = row["use_yn"].ToString();
            this.test_module_yn = row["test_module_yn"].ToString();
            this.doc_module_yn = row["doc_module_yn"].ToString();
            this.link_point = row["link_point"].ToString();
            this.linked_module = row["linked_module"].ToString();
            this.linked_table = row["linked_table"].ToString();
            this.linked_remark = row["linked_remark"].ToString();
            this.linked_value1 = row["linked_value1"].ToString();
            this.linked_value2 = row["linked_value2"].ToString();
            this.linked_value3 = row["linked_value3"].ToString();
        }

        public ElectronicSigniture()
        {

        }
    }
}
