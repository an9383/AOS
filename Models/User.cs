using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models
{
    public class User
    {
        [Required(ErrorMessage = "ID를 입력해주세요")]
        public string txt_ID { get; set; }

        [Required(ErrorMessage = "PassWord를 입력해주세요")]
        public string txt_Pass { get; set; }

        public string emp_cd { get; set; }
        public string emp_nm { get; set; }
        public string dept_cd { get; set; }
        public string dept_nm { get; set; }
        public string ck_login { get; set; }
        public DateTime login_time { get; set; }
        public string log_out { get; set; }
        public string gubun { get; set; }
        public User()
        {

        }

        public User(DataRow row)
        {
            emp_cd = row["emp_cd"].ToString();
            emp_nm = row["emp_nm"].ToString();
            dept_cd = row["dept_cd"].ToString();
            dept_nm = row["dept_nm"].ToString();
            ck_login = row["ck_login"].ToString();
            login_time = DateTime.Parse(row["login_time"].ToString());
            if(row["log_out"] != null)
            {
                log_out = row["log_out"].ToString();
            }
        }
        
    }
}