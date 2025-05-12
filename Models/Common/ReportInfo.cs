using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HACCP.Models.Common
{
    public class ReportInfo
    {
        public string RptFileName { get; set; }
        public string path { get; set; }
        public string SpName { get; set; }
        public string gubun { get; set; }
        public string reportParam { get; set; }
        public string subParam { get; set; }
        public string tblName { get; set; }
        public string subRptName { get; set; }
        public string viewerName { get; set; }      // 뷰어명
        public string btnType { get; set; }         // Preview, Print 설정
        public int    nCopies { get; set; }         // 프린트할 본수
        public int    RptSeq { get; set; }          // 레포트 순서
        public string dataSet { get; set; }
        public string dataTable { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string receive { get; set; }
        public string company_name { get; set; }
        public string cc { get; set; }
    }
}