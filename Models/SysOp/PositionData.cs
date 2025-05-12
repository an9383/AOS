using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysOp
{
    public class PositionData
    {
        [Required]
        public string position_no { get; set; }

        [Required]
        public string obj_type { get; set; }

        [Required]
        public string obj_cd { get; set; }

        public string obj_type_nm { get; set; }

        public string obj_nm { get; set; }

        [Required]
        public string program_id { get; set; }

        public string program_nm { get; set; }

        public string program_ex1_id { get; set; }

        public string other_position_no1 { get; set; }

        public string other_position_no1_nm { get; set; }

        public string other_position_no2 { get; set; }

        public string other_position_no2_nm { get; set; }

        public string other_position_no3 { get; set; }

        public string other_position_no3_nm { get; set; }


        public PositionData()
        {

        }
    }
}
