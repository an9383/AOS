using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysOp
{
    public class Anniversary
    {
        public string id { get; set; }
        [Required]
        public string lunar_yn { get; set; }
        [Required]
        public string anniversary_date { get; set; }
        [Required]
        public string anniversary_nm { get; set; }
        [Required]
        public string calendar_type { get; set; }
        public string attend_time { get; set; }
        public string leave_time { get; set; }
        [Required]
        public string working_minute { get; set; }
        public string rest_minute { get; set; }
        public string lunar_yn_nm { get; set; }
        public string calendar_type_nm { get; set; }

        public Anniversary()
        {

        }
    }
}
