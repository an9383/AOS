using CrystalDecisions.CrystalReports.Engine;
using HACCP.Libs.Database;
using HACCP.Models;
using HACCP.Models.Common;
using HACCP.Services.Comm;
using System;
using System.IO;
using System.Web.Mvc;

namespace HACCP.Controllers
{
    public class ReportController : Controller
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();

        [HttpGet]
        public void ViewReport(ReportInfo reportInfo)
        {

            GenerateReportService generateReportService = new GenerateReportService();

            ReportParams<String> objReportParam = new ReportParams<String>();

            //string[] paramArry = reportInfo.reportParam.Split(';');
            //objReportParam.DataSource = paramArry;
            //objReportParam.RptFileName = reportInfo.RptFileName + ".rpt";

            //this.HttpContext.Session["ReportType"] = reportInfo.RptFileName;
            //this.HttpContext.Session["ReportParam"] = reportInfo;

            this.HttpContext.Session["ReportType"] = reportInfo.RptFileName;            
            this.HttpContext.Session[reportInfo.RptFileName + reportInfo.RptSeq] = reportInfo;
            
            //objReportParam.DataSource = reportParam;
            ////objReportParam.DataSource = test(reportParam);
            //objReportParam.RptFileName = reportParam.RptFileName + ".rpt";

            //this.HttpContext.Session["ReportType"] = reportParam.RptFileName;
            //this.HttpContext.Session["ReportParam"] = objReportParam;






            //GenerateReportService generateReportService = new GenerateReportService();

            //var reportParam = (dynamic)HttpContext.Current.Session["ReportParam"];

            //ReportDocument rd = new ReportDocument();

            //string path = Path.Combine(Server.MapPath("~"), "Report/Rpt/" + reportInfo.RptFileName + ".rpt");
            //rd.Load(path);


            //if (!String.IsNullOrEmpty(reportInfo.SpName))
            //{
            //    string spName = reportInfo.SpName;
            //    string gubun = reportInfo.gubun;
            //    string[] paramArry;

            //    paramArry = reportInfo.reportParam.Split(';');

            //    string[] spParam = new string[paramArry.Length];
            //    for (int i = 0; i < spParam.Length; i++)
            //    {
            //        var param = paramArry[i].Split('=');
            //        spParam[i] = "@" + param[0] + ":" + param[1];
            //    }

            //    DataTable dt = _bllSpExecute.SpExecuteTable(spName, gubun, spParam);

            //    rd.SetDataSource(dt);
            //}



            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();

            //rd.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            //rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA5;

            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //stream.Seek(0, SeekOrigin.Begin);

            //return File(stream, "application/pdf", "TestReport.pdf");

        }

        // 다운로드
        [HttpGet]
        public ActionResult DownloadReport(ReportParam reportParam)
        {
            GenerateReportService generateReportService = new GenerateReportService();

            ReportParams<String> objReportParam = new ReportParams<string>();

            objReportParam.DataSource = reportParam;

            this.HttpContext.Session["ReportParam"] = objReportParam;

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~"), "Report/Rpt/" + reportParam.RptFileName + ".rpt"));

            if (!String.IsNullOrEmpty(reportParam.SpName))
            {
                rd.SetDataSource(generateReportService.SetReportDs(reportParam));
            }

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            rd.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA5;

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "TestReport.pdf");
        }

        /// <summary>
        /// PDF print 출력
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ViewResult PrintReport(string fileName)
        {
            string path = "/report/download";
            string fullPath = path + "/" + fileName + ".pdf";

            ViewBag.pdfile = fullPath;
            ViewBag.onlyFileName = fileName;

            return View("~/Views/Comm/PrintReport.cshtml");
        }
        /// <summary>
        /// 레포트 삭제 : pdf파일 생성후, 사용자가 프린트시작시, 자동 제거됨
        /// </summary>
        /// <param name="fileName"></param>
        public void DeleteReport(string fileName)
        {
            string path = "/report/download";
            string fullPath = path + "/" + fileName + ".pdf";

            string physicalPath = Server.MapPath(fullPath);

            if (System.IO.File.Exists(physicalPath))
            {
                System.IO.File.Delete(physicalPath);
            }
        }
    }
}