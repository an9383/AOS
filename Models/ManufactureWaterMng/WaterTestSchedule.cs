using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.ManufactureWaterMng
{
    public class WaterTestSchedule
    {
        public string gubun { get; set; }
        public string test_type { get; set; }
        public string water_type { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public string calendar_date { get; set; }
        public string testrequest_no { get; set; }

        public WaterTestSchedule()
        {
            this.gubun = "";
            this.test_type = "10";
            this.water_type = "";
            this.year = "";
            this.month = "";
            this.calendar_date = "";
            this.testrequest_no = "";
        }
    }
}