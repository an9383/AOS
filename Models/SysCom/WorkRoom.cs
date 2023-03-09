using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.SysCom
{
    public class WorkRoom
    {
        public string workroom_cd_1 { get; set; }
        public string workroom_cd_2 { get; set; }
        public string workroom_cd { get; set; }
        public string workroom_nm { get; set; }
        public string workroom_mcd { get; set; }
        public string cleanness_cd { get; set; }
        public string plant_cd { get; set; }
        public string workroom_volume { get; set; }
        public string workroom_bottomtype { get; set; }
        public string workroom_walltype { get; set; }
        public string workroom_toptype { get; set; }
        public string workroom_type { get; set; }
        public string warehouse_type { get; set; }
        public string temp_min { get; set; }
        public string temp_max { get; set; }
        public string hum_min { get; set; }
        public string hum_max { get; set; }
        public string press_min { get; set; }
        public string press_max { get; set; }
        public string Illumination { get; set; }        //조도기준(Lux)
        public string workroom_area { get; set; }       //작업장구역
        public string workroom_no { get; set; }
        public string item_type1 { get; set; }
        public string item_type2 { get; set; }
        public string use_yn { get; set; }
        public string hardware_yn { get; set; }


        public WorkRoom()
        {

        }
    }
}
