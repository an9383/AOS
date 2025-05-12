using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysCom
{
    public class Plant
    {
        
        [Required]
        public string plant_cd { get; set; }
        public string plant_nm { get; set; }
        public string plant_busin_no { get; set; }
        public string plant_corpo_no { get; set; }
        public string plant_emp_cd { get; set; }
        public string plant_emp_nm { get; set; }
        public string plant_catgo { get; set; }
        public string plant_busin_nm { get; set; }
        public string plant_post_cd { get; set; }
        public string plant_adress1 { get; set; }
        public string plant_adress2 { get; set; }
        public string plant_telephone { get; set; }
        public string plant_fax { get; set; }
        public string plant_office { get; set; }
        public string plant_open_date { get; set; }
        public string plant_close_date { get; set; }
        public string main_yn { get; set; }

        [Required]
        public string use_yn { get; set; }
        public string insert_emp { get; set; }
        public string at_emp_cd { get; set; }


        public Plant()
        {

        }

    }
}
