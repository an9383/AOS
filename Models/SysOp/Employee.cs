using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysOp
{
    public class Employee
    {
        public string emp_cd { get; set; }
        public string emp_nm { get; set; }
        public string dept_cd { get; set; }
        public string dept_nm { get; set; }
        public string emp_post { get; set; }
        public string emp_rair { get; set; }
        public string enter_date { get; set; }
        public string retire_date { get; set; }
        public string manager_emp_cd { get; set; }
        public string manager_emp_nm { get; set; }
        public string emp_type { get; set; }
        public string phone_no { get; set; }
        public string birth_date { get; set; }
        public string use_yn { get; set; }
        public string email_addr { get; set; }

        public Employee()
        {

        }

        public Employee(DataRow row)
        {
            this.emp_cd = row["emp_cd"].ToString();
            this.emp_nm = row["emp_nm"].ToString();
            this.dept_cd = row["dept_cd"].ToString();
            this.dept_nm = row["dept_nm"].ToString();
        }

    }
}