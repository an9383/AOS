using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HACCP.Models
{
    public class News
    {
        public string news_cd { get; set; }
        public string news_gb { get; set; }
        public string news_seq_id { get; set; }
        public string target_cd { get; set; }
        public string news_reader { get; set; }
        public string news_title { get; set; }
        public string news_content { get; set; }
        public string news_start_date { get; set; }
        public string news_end_date { get; set; }
        public string news_writer { get; set; }
        public string news_gb_nm { get; set; }
        public string news_writer_nm { get; set; }

        public News()
        {

        }

        public News(DataRow row)
        {
            news_gb_nm = row["news_gb_nm"].ToString();
            news_title = row["news_title"].ToString();
            news_content = row["news_content"].ToString();
            news_writer_nm = row["news_writer_nm"].ToString();
            news_cd = row["news_cd"].ToString();
        }
    }
}
