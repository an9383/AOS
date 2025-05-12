using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models
{
    public class ReportParams<T>
    {
        public string RptFileName { get; set; }
        public string ReportTitle { get; set; }
        public Object DataSource { get; set; }
        public bool IsPassParamToCr { get; set; }
    }
}