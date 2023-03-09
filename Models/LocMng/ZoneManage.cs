using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.LocMng
{
    public class ZoneManage
    {
        //SP 구분자
        public string Gubun { get; set; }

        
        //검색 조건
        //파라미터, 설명 구분
        public string workroom_cd { get; set; }
        

        //SP 결과값들
        public string workroom_nm { get; set; }
        public string zone_cd { get; set; }
        public string zone_nm { get; set; }
        public string zone_type { get; set; }
        public string zone_priority { get; set; }
        public string zone_status { get; set; }
        public string zone_temperature_max { get; set; }
        public string zone_temperature_min { get; set; }
        public string zone_humidity_max { get; set; }
        public string zone_humidity_min { get; set; }
        public string insert_user_cd { get; set; }
        public string insert_time { get; set; }
        public string update_user_cd { get; set; }
        public string update_time { get; set; }
        public string zone_remark { get; set; }
        public string select_check { get; set; }
        public ZoneManage()
        {

        }

        public ZoneManage(DataRow row)
        {
            this.workroom_cd = row["workroom_cd"].ToString();
            this.workroom_nm = row["workroom_nm"].ToString();
            this.zone_cd = row["zone_cd"].ToString();
            this.zone_nm = row["zone_nm"].ToString();
            this.zone_type = row["zone_type"].ToString();
            this.zone_priority = row["zone_priority"].ToString();            
            this.zone_status = row["zone_status"].ToString();
            this.zone_temperature_max = row["zone_temperature_max"].ToString();
            this.zone_temperature_min = row["zone_temperature_min"].ToString();
            this.zone_humidity_max = row["zone_humidity_max"].ToString();
            this.zone_humidity_min = row["zone_humidity_min"].ToString();
            this.insert_user_cd = row["insert_user_cd"].ToString();
            this.insert_time = row["insert_time"].ToString();
            this.update_user_cd = row["update_user_cd"].ToString();
            this.update_time = row["update_time"].ToString();
            this.zone_remark = row["zone_remark"].ToString();
            this.select_check = row["select_check"].ToString();
        }
    }
}
