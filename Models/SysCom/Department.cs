using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysCom
{
    public class Department
    {
        public string dept_cd { get; set; }
        public string dept_nm { get; set; }
        public string dept_gb { get; set; }
        public string dept_mcd { get; set; }
        public string high_dept_nm { get; set; }
        public string dept_level { get; set; }
        public string plant_cd { get; set; }
        public string dept_use_gb { get; set; }

        public Department()
        {

        }
    }
}
