using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.QualityControl
{
    public class BackDataDownload
    {
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string test_type { get; set; }
        public string test_status { get; set; }
        public string file_type { get; set; }
    }
}