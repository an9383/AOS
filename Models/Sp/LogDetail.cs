using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models.Sp
{
    public class LogDetail
    {

        public int id { get; set; }
        public string program_id { get; set; }
        public string program_nm { get; set; }

        public DateTime start_time { get; set; }

        public DateTime end_time { get; set; }

        public LogDetail()
        {

        }

        public LogDetail(DataRow row)
        {
            program_id = row["program_id"].ToString();
            program_nm = row["program_nm"].ToString();
            start_time = DateTime.Parse(row["start_time"].ToString());
            end_time = DateTime.Parse(row["end_time"].ToString());
        }
    }
}
