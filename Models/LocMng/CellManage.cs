using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.LocMng
{
    public class CellManage
    {
        //SP 구분자
        public string gubun { get; set; }

        //검색조건
        public string workroom_cd { get; set; }
        public string zone_cd { get; set; }
        public string cell_isle { get; set; }
        public string cell_height { get; set; }

        //SP 결과값들
        public string cell_cd { get; set; }
        public string cell_nm { get; set; }
        public string cell_type { get; set; }
        public string cell_priority { get; set; }
        public string cell_column { get; set; }
        public string cell_status { get; set; }
        public string cell_remark { get; set; }
        public string insert_user_id { get; set; }
        public string insert_time { get; set; }
        public string update_user_id { get; set; }
        public string update_time { get; set; }
        public string cell_max_capacity { get; set; }
        public string cell_min_capacity { get; set; }


        public CellManage()
        {

        }

        public CellManage(DataRow row)
        {
            this.workroom_cd = row["workroom_cd"].ToString();
            this.zone_cd = row["zone_cd"].ToString();
            this.cell_isle = row["cell_isle"].ToString();
            this.cell_height = row["cell_height"].ToString();
            this.cell_cd = row["cell_cd"].ToString();
            this.cell_nm = row["cell_nm"].ToString();
            this.cell_type = row["cell_type"].ToString();
            this.cell_priority = row["cell_priority"].ToString();
            this.cell_column = row["cell_column"].ToString();
            this.cell_status = row["cell_status"].ToString();
            this.cell_remark = row["cell_remark"].ToString();
            this.insert_user_id = row["insert_user_id"].ToString();
            this.insert_time = row["insert_time"].ToString();
            this.update_user_id = row["update_user_id"].ToString();
            this.update_time = row["update_time"].ToString();
            this.cell_max_capacity = row["cell_max_capacity"].ToString();
            this.cell_min_capacity = row["cell_min_capacity"].ToString();
        }
    }
}
