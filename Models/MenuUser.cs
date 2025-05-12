using System.Data;

namespace HACCP.Models
{
    public class MenuUser
    {
        public string emp_cd { get; set; }
        public string emp_nm { get; set; }
        public string dept_cd { get; set; }
        public string dept_nm { get; set; }
        public string user_id { get; set; }
        public string user_nm { get; set; }
        public string user_passwd { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string failed_num { get; set; }
        public string now_user_id { get; set; }
        public string use_check { get; set; }
        public string pre_user_id { get; set; }
        public string pre_user_passwd { get; set; }
        public string current_on { get; set; }
        public string system_log_id { get; set; }
        public string log_out { get; set; }
        public string retire_date { get; set; }
        public string user_doorlock_id { get; set; }
        public string user_doorlock_password { get; set; }
        public string user_security { get; set; }
        public string emp_barcode { get; set; }
        public string finger_ck { get; set; }
        public string door_finger_ck { get; set; }
        public string door2_finger_ck { get; set; }
        public string retire_yn { get; set; }
        public string chk_user_id { get; set; }
        public string chk_user_passwd { get; set; }

        public MenuUser()
        {

        }

        public MenuUser(DataRow row)
        {
            this.emp_cd = row["emp_cd"].ToString();
            this.emp_nm = row["emp_nm"].ToString();
            this.dept_cd = row["dept_cd"].ToString();
            this.dept_nm = row["dept_nm"].ToString();
            this.user_id = row["user_id"].ToString();
            this.user_nm = row["user_nm"].ToString();
            this.user_passwd = row["user_passwd"].ToString();
            this.start_time = row["start_time"].ToString();
            this.end_time = row["end_time"].ToString();
            if (row["failed_num"] == null)
            {
                row["failed_num"] = 0;
            }
            else
            {
                this.failed_num = row["failed_num"].ToString();
            }
            this.now_user_id = row["now_user_id"].ToString();
            this.use_check = row["use_check"].ToString();
            this.pre_user_id = row["pre_user_id"].ToString();
            this.pre_user_passwd = row["pre_user_passwd"].ToString();
            this.current_on = row["current_on"].ToString();
            this.system_log_id = row["system_log_id"].ToString();
            this.log_out = row["log_out"].ToString();
            this.retire_date = row["retire_date"].ToString();
            this.user_doorlock_id = row["user_doorlock_id"].ToString();
            this.user_doorlock_password = row["user_doorlock_password"].ToString();
            if (row["user_security"] == null)
            {
                row["user_security"] = 0;
            }
            else
            {
                this.user_security = row["user_security"].ToString();
            }
            this.emp_barcode = row["emp_barcode"].ToString();
            this.finger_ck = row["finger_ck"].ToString();
            this.door_finger_ck = row["door_finger_ck"].ToString();
            this.door2_finger_ck = row["door2_finger_ck"].ToString();
            this.retire_yn = row["retire_yn"].ToString();

        }

        //public MenuUser(string emp_cd, string emp_nm, string dept_cd, string dept_nm, string emp_type, string emp_post, string emp_rair, string emp_charge, string enter_date, string retire_date, string calendar_cd, string manager_emp_cd, string gender, string insert_time, string insert_emp, string update_time, string update_emp, string outer_interface, string phone_no, string use_yn, string sys_plant_cd)
        //{
        //    this.emp_cd = emp_cd;
        //    this.emp_nm = emp_nm;
        //    this.dept_cd = dept_cd;
        //    this.dept_nm = dept_nm;
        //    this.emp_type = emp_type;
        //    this.emp_post = emp_post;
        //    this.emp_rair = emp_rair;
        //    this.emp_charge = emp_charge;
        //    this.enter_date = enter_date;
        //    this.retire_date = retire_date;
        //    this.calendar_cd = calendar_cd;
        //    this.manager_emp_cd = manager_emp_cd;
        //    this.gender = gender;
        //    this.insert_time = insert_time;
        //    this.insert_emp = insert_emp;
        //    this.update_time = update_time;
        //    this.update_emp = update_emp;
        //    this.outer_interface = outer_interface;
        //    this.phone_no = phone_no;
        //    this.use_yn = use_yn;
        //    this.sys_plant_cd = sys_plant_cd;
        //}
    }
}
