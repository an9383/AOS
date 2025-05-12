using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.Tb
{
    public class system_access_history
    {

        public int id { get; set; }
        public string emp_cd { get; set; }
        public DateTime? login_time { get; set; }
        public DateTime? logout_time { get; set; }
        public string sys_plant_cd { get; set; }
        public int audittrail_id { get; set; }

        public system_access_history()
        {

        }
        public system_access_history(DataRow row)
        {
            id = Int32.Parse(row["id"].ToString());
            emp_cd = row["emp_cd"].ToString();
            login_time = DateTime.Parse(row["login_time"].ToString());
            logout_time = DateTime.Parse(row["logout_time"].ToString());
            sys_plant_cd = row["sys_plant_cd"].ToString();
            audittrail_id = Int32.Parse(row["audittrail_id"].ToString());
        }
    }
}
