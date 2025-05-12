using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using HACCP.Libs;
using HACCP.Libs.Database;
using HACCP.Models;
using HACCP.Models.Common;
using HACCP.Services.Comm;
using System;
using System.Data;
using System.IO;
//using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mail;
using System.Web.Services;
using ReportInfo = HACCP.Models.Common.ReportInfo;

namespace HACCP.Report
{

    public partial class ReportViewer : System.Web.UI.Page
    {
        private BllSpExecute _bllSpExecute = new BllSpExecute();
        private static BllSpExecute _bllSpExecute_st = new BllSpExecute();
        public static ReportDocument rd = null;
        public static ReportDocument rd_st = null;

        public string id
        {
            set { ViewState["id"] = value; }
            get
            {
                try
                {
                    if (ViewState["id"] != null)
                    {
                        return ViewState["id"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch { return ""; }
            }

        }
        public string type
        {
            set { ViewState["type"] = value; }
            get
            {
                try
                {
                    if (ViewState["type"] != null)
                    {
                        return ViewState["type"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch { return ""; }
            }

        }
        public string title
        {
            set { ViewState["title"] = value; }
            get
            {
                try
                {
                    if (ViewState["title"] != null)
                    {
                        return ViewState["title"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch { return ""; }
            }

        }
        public string body
        {
            set { ViewState["body"] = value; }
            get
            {
                try
                {
                    if (ViewState["body"] != null)
                    {
                        return ViewState["body"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch { return ""; }
            }

        }
        public string receive
        {
            set { ViewState["receive"] = value; }
            get
            {
                try
                {
                    if (ViewState["receive"] != null)
                    {
                        return ViewState["receive"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch { return ""; }
            }

        }
        public string company_name
        {
            set { ViewState["company_name"] = value; }
            get
            {
                try
                {
                    if (ViewState["company_name"] != null)
                    {
                        return ViewState["company_name"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch { return ""; }
            }

        }
        public string cc
        {
            set { ViewState["cc"] = value; }
            get
            {
                try
                {
                    if (ViewState["cc"] != null)
                    {
                        return ViewState["cc"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch { return ""; }
            }

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {

            string name = Request.QueryString["name"];

            var reportParam = (ReportInfo)HttpContext.Current.Session[name];

            string path = Path.Combine(Server.MapPath("~"), "Report/Rpt/" + reportParam.path + "/" + reportParam.RptFileName + ".rpt");


            if (!string.IsNullOrEmpty(reportParam.type))
            {
                if(rd_st != null)
                {
                    CloseReport();
                }
                rd_st = new ReportDocument();

                rd_st.Load(path);
                rd_st.SetDatabaseLogon(DbAgent.Decrypt(Public_Function.selectedDBId, "ZR"), DbAgent.Decrypt(Public_Function.selectedDBPw, "ZR"));

                if (!string.IsNullOrEmpty(reportParam.SpName))
                {
                    DataTable dt = new DataTable();

                    string spName = reportParam.SpName;
                    string gubun = reportParam.gubun;
                    string[] spParam = reportParam.reportParam.Split(';');

                    DataSet ds = _bllSpExecute.SpExecuteDataSet(spName, gubun, spParam);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {

                            if (!string.IsNullOrEmpty(reportParam.tblName))
                            {
                                string[] tblNm = reportParam.tblName.Split(',');

                                for (int i = 0; i < tblNm.Length; i++)
                                {
                                    ds.Tables[i].TableName = tblNm[i];
                                }
                                rd_st.SetDataSource(ds);

                            }
                            else
                            {
                                dt = ds.Tables[0];
                                rd_st.SetDataSource(dt);

                            }
                        }

                    }

                    //파일이미지 처리 주석
                    //for(int i = 0; i < dt.Rows[0].ItemArray.Length; i++)
                    //{
                    //    if(dt.Rows[0].ItemArray[i].GetType().ToString() == "System.Byte[]")
                    //    {
                    //        string str = Convert.ToBase64String((byte[])dt.Rows[0].ItemArray[i]);
                    //        string url = "data:Image/png;base64," + str;
                    //        dt.Rows[0].ItemArray[i] = url;
                    //    }
                    //}


                    if (!string.IsNullOrEmpty(reportParam.subRptName))
                    {
                        string[] subRptNm = reportParam.subRptName.Split(',');
                        for (int i = 0; i < subRptNm.Length; i++)
                        {
                            if (string.IsNullOrEmpty(subRptNm[i])) continue;
                            rd_st.OpenSubreport(subRptNm[i]).SetDataSource(ds);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(reportParam.subParam))
                {
                    string[] paramArry = reportParam.subParam.Split(';');

                    for (var i = 0; i < paramArry.Length; i++)
                    {
                        var param = paramArry[i].Split('=');
                        rd_st.SetParameterValue(param[0], param[1]);

                    }
                }

                if (!string.IsNullOrEmpty(reportParam.id))
                {
                    id = reportParam.id;

                }
                if (!string.IsNullOrEmpty(reportParam.type))
                {
                    type = reportParam.type;

                }
                if (!string.IsNullOrEmpty(reportParam.title))
                {
                    title = reportParam.title;

                }
                if (!string.IsNullOrEmpty(reportParam.body))
                {
                    body = reportParam.body;

                }
                if (!string.IsNullOrEmpty(reportParam.receive))
                {
                    receive = reportParam.receive;

                }
                if (!string.IsNullOrEmpty(reportParam.company_name))
                {
                    company_name = reportParam.company_name;

                }
                if (!string.IsNullOrEmpty(reportParam.cc))
                {
                    cc = reportParam.cc;

                }

                CrystalReportViewer1.ReportSource = rd_st;
                HttpContext.Current.Session["rd_st"] = rd_st;

                //rd.PrintOptions.PrinterName = localPrinter.PrinterSettings.PrinterName;

            }
            else
            {
                rd = new ReportDocument();

                rd.Load(path);
                rd.SetDatabaseLogon(DbAgent.Decrypt(Public_Function.selectedDBId, "ZR"), DbAgent.Decrypt(Public_Function.selectedDBPw, "ZR"));

                if (!string.IsNullOrEmpty(reportParam.SpName))
                {
                    DataTable dt = new DataTable();

                    string spName = reportParam.SpName;
                    string gubun = reportParam.gubun;
                    string[] spParam = reportParam.reportParam.Split(';');

                    DataSet ds = _bllSpExecute.SpExecuteDataSet(spName, gubun, spParam);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {

                            if (!string.IsNullOrEmpty(reportParam.tblName))
                            {
                                string[] tblNm = reportParam.tblName.Split(',');

                                for (int i = 0; i < tblNm.Length; i++)
                                {
                                    ds.Tables[i].TableName = tblNm[i];
                                }
                                rd.SetDataSource(ds);

                            }
                            else
                            {
                                dt = ds.Tables[0];
                                rd.SetDataSource(dt);

                            }
                        }

                    }

                    //파일이미지 처리 주석
                    //for(int i = 0; i < dt.Rows[0].ItemArray.Length; i++)
                    //{
                    //    if(dt.Rows[0].ItemArray[i].GetType().ToString() == "System.Byte[]")
                    //    {
                    //        string str = Convert.ToBase64String((byte[])dt.Rows[0].ItemArray[i]);
                    //        string url = "data:Image/png;base64," + str;
                    //        dt.Rows[0].ItemArray[i] = url;
                    //    }
                    //}


                    if (!string.IsNullOrEmpty(reportParam.subRptName))
                    {
                        string[] subRptNm = reportParam.subRptName.Split(',');
                        for (int i = 0; i < subRptNm.Length; i++)
                        {
                            if (string.IsNullOrEmpty(subRptNm[i])) continue;
                            rd.OpenSubreport(subRptNm[i]).SetDataSource(ds);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(reportParam.subParam))
                {
                    string[] paramArry = reportParam.subParam.Split(';');

                    for (var i = 0; i < paramArry.Length; i++)
                    {
                        var param = paramArry[i].Split('=');
                        rd.SetParameterValue(param[0], param[1]);

                    }
                }


                CrystalReportViewer1.ReportSource = rd;

                //rd.PrintOptions.PrinterName = localPrinter.PrinterSettings.PrinterName;

                // print 할지 여부
                if ("Print".Equals(reportParam.btnType))
                {
                    /* **************************************************
                      * DATE : 2021-01-15
                      * comment : 프린터 출력시, client의 프린터를 이용하기 위하여, 아래 서버 프린팅기능제거
                      *           또한, client 스크립트에서는 인쇄매수를 받을방법이 없어 구현불가 (nCopies)
                      * **************************************************/
                    //int nCopies = reportParam.nCopies;
                    //rd.PrintToPrinter(nCopies, false, 0, 0);

                    // 서버에 저장후, ReportController의 PrintReport 매서드 호출
                    Guid imageGuid = Guid.NewGuid();
                    string downPath = Path.Combine(Server.MapPath("~"), "Report/download/" + reportParam.RptFileName + ".rpt");
                    string pdfName = String.Format(@"{0}{1}.pdf", downPath, imageGuid);

                    rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfName);
                    Response.Redirect("/Report/PrintReport?filename=" + (reportParam.RptFileName + ".rpt" + imageGuid));
                    Response.End();
                }

                //HttpContext.Current.Session[name] = string.Empty;
                // 사용한 세션 초기화
                //HttpContext.Current.Session.Remove(name);
            }


        }

        [WebMethod]
        public static string SendEmail(string type, string id, string title, string body, string receive, string cc)
        {
            string result = "전송에러";
            //rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "c:\\tascorp\\a.pdf");
            ReportDocument report = (ReportDocument)HttpContext.Current.Session["rd_st"];

            string filePath = "C:\\tascorp";
            DirectoryInfo di = new DirectoryInfo(filePath);

            //c://tascorp 폴더가 없으면 생성
            if(di.Exists == false)
            {
                di.Create();
            }

            string fileName = "C:\\tascorp\\" + id + ".pdf";
            report.ExportToDisk(ExportFormatType.PortableDocFormat, fileName);
            Send(fileName, title, body, receive, cc);

            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();

                if (type == "purchase")
                {
                    string msg = "";
                    string[] pParam = new string[1];
                    pParam[0] = "@purchase_no:" + id;
                    msg = IncSendMailCount("SP_PurchaseManage2", "Mail", pParam);
                }

                result = "전송완료";
            }

            return result;
        }

        public static string IncSendMailCount(string SpName, string gubun, string[] param)
        {
            string result = _bllSpExecute_st.SpExecuteString(SpName, gubun, param);

            return result;
        }

        #region 기존 메일전송
        // 메일
        //    protected static void Send(string pdfFile, string title, string body, string receive, string cc)
        //    {
        //        MailMessage msg = new MailMessage();
        //        try
        //        {
        //            // TASCO NAS SMTP Server
        //            MailMessage objeto_mail = new MailMessage();
        //            SmtpClient client = new SmtpClient();
        //            client.Port = Public_Function.emlPort;  // 25;
        //            client.Host = Public_Function.emlHost;  // "sf.tascorp.co.kr";
        //            client.Timeout = 1000 * 10;
        //            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            client.UseDefaultCredentials = false;
        //            client.Credentials = new System.Net.NetworkCredential(Public_Function.emlCredentialsID, Public_Function.emlCredentialsPW);  // "relay", "!Relay9179"
        //            objeto_mail.From = new MailAddress(Public_Function.emlFromAccount); // "relay@sf.tascorp.co.kr");

        //            // 수신자
        //            string[] addressArray = receive.Split(';');
        //            foreach (var addr in addressArray)
        //            {
        //                objeto_mail.To.Add(new MailAddress(addr));
        //            }

        //            // 참조
        //            addressArray = cc.Split(';');
        //            foreach (var addr in addressArray)
        //            {
        //                objeto_mail.CC.Add(new MailAddress(addr));
        //            }

        //            // 숨은 참조
        //            addressArray = Public_Function.emlBcc.Split(';');
        //            foreach (var addr in addressArray)
        //            {
        //                objeto_mail.Bcc.Add(new MailAddress(addr));
        //            }
        //            //objeto_mail.IsBodyHtml = true;

        //            objeto_mail.Subject = title;

        //            objeto_mail.Body = body;

        //            objeto_mail.Attachments.Add(new Attachment(pdfFile));


        //            client.Send(objeto_mail);

        //            //await client.SendMailAsync(objeto_mail);

        //            objeto_mail.Dispose();
        //            client.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //        finally
        //        {
        //            /*
        //                so that the error "The process cannot access the file because it is being used by another process" should not occur when we immediately
        //                try to send another mail after one has been sent.
        //                w3wp.exe:5800 CREATE D:\\Testcrystal.pdf
        //                w3wp.exe:5800 WRITE D:\\Testcrystal.pdf
        //                w3wp.exe:5800 CLOSE D:\\Testcrystal.pdf
        //                w3wp.exe:5800 OPEN D:\\Testcrystal.pdf
        //                w3wp.exe:5800 READ D:\\Testcrystal.pdf
        //                As you can see, it created the PDF, it wrote the PDF, it closed the PDF (expected).
        //                Then, there was an unexpected Open, Read, without close immediately after the file was created. So that's why we've to close the file after
        //sending
        //             * it through mail because msg will open and read the file but the file is not close automattically after sending it. There will no error while sending
        //the first
        //             * mail but when you try to send the next mail one after the other the above error will rise. to avoid this error we are using the Dispose methods
        //to release
        //             * all the resources which are being used.
        //            */
        //            msg.Dispose();
        //        }
        //    }
        #endregion

        #region 메일전송 수정

        #endregion
        protected static void Send(string pdfFile, string title, string body, string receive, string cc)
        {
            MailMessage msg = new MailMessage();
            try
            {
                msg.Body = body;
                msg.Subject = title;
                //msg.From = DbAgent.Encrypt(Public_Function.emlFromAccount, "ZR");
                msg.From = Public_Function.emlFromAccount;

                int cdoBasic = 1;

                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", true);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", Public_Function.emlPort.ToString());
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
                //msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", Public_Function.emlCredentialsID); //기존 BMI는 메일아이디만 설정
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", Public_Function.emlFromAccount); //아오스는 메일주소전체로 설정해야 전송됨
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", Public_Function.emlCredentialsPW);

                msg.To = receive;
                msg.Cc = cc;
                msg.Bcc = Public_Function.emlBcc;

                string sAttach = pdfFile;

                char[] delim = new char[] { ',' };
                foreach (string sSubstr in sAttach.Split(delim))
                {
                    MailAttachment myAttachment = new MailAttachment(sSubstr);
                    msg.Attachments.Add(myAttachment);
                }

                // mailplug SMTP Server
                //SmtpMail.SmtpServer = DbAgent.Encrypt(Public_Function.emlHost, "ZR");
                SmtpMail.SmtpServer = Public_Function.emlHost;
                SmtpMail.Send(msg);

                //MailMessage objeto_mail = new MailMessage();
                //SmtpClient client = new SmtpClient();
                //client.Port = Public_Function.emlPort;  // 25;
                //client.Host = Public_Function.emlHost;  // "sf.tascorp.co.kr";
                //client.Timeout = 1000 * 10;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;
                //client.Credentials = new System.Net.NetworkCredential(Public_Function.emlCredentialsID, Public_Function.emlCredentialsPW);  // "relay", "!Relay9179"
                //objeto_mail.From = new MailAddress(Public_Function.emlFromAccount); // "relay@sf.tascorp.co.kr");

                //// 수신자
                //string[] addressArray = receive.Split(';');
                //foreach (var addr in addressArray)
                //{
                //    objeto_mail.To.Add(new MailAddress(addr));
                //}

                //// 참조
                //addressArray = cc.Split(';');
                //foreach (var addr in addressArray)
                //{
                //    objeto_mail.CC.Add(new MailAddress(addr));
                //}

                //// 숨은 참조
                //addressArray = Public_Function.emlBcc.Split(';');
                //foreach (var addr in addressArray)
                //{
                //    objeto_mail.Bcc.Add(new MailAddress(addr));
                //}
                ////objeto_mail.IsBodyHtml = true;

                //objeto_mail.Subject = title;

                //objeto_mail.Body = body;

                //objeto_mail.Attachments.Add(new Attachment(pdfFile));


                //client.Send(objeto_mail);

                //objeto_mail.Dispose();
                //client.Dispose();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                /*
                    so that the error "The process cannot access the file because it is being used by another process" should not occur when we immediately
                    try to send another mail after one has been sent.
                    w3wp.exe:5800 CREATE D:\\Testcrystal.pdf
                    w3wp.exe:5800 WRITE D:\\Testcrystal.pdf
                    w3wp.exe:5800 CLOSE D:\\Testcrystal.pdf
                    w3wp.exe:5800 OPEN D:\\Testcrystal.pdf
                    w3wp.exe:5800 READ D:\\Testcrystal.pdf
                    As you can see, it created the PDF, it wrote the PDF, it closed the PDF (expected).
                    Then, there was an unexpected Open, Read, without close immediately after the file was created. So that's why we've to close the file after
    sending
                 * it through mail because msg will open and read the file but the file is not close automattically after sending it. There will no error while sending
    the first
                 * mail but when you try to send the next mail one after the other the above error will rise. to avoid this error we are using the Dispose methods
    to release
                 * all the resources which are being used.
                */
            }
        }


        [WebMethod]
        public static void CloseReport()
        {
            rd_st.Close();
            rd_st.Dispose();
            rd_st = null;
            //_bllSpExecute_st = null;
        }

        protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
        {
            if(rd != null)
            {
                rd.Close();
                rd.Dispose();
                rd = null;
            }
            //this.CrystalReportViewer1.Dispose();
            //this.CrystalReportViewer1 = null;
        }
        //private DataTable GetData(string SpName, string gubun, string reportParam)
        //{
        //    string[] paramArry;
        //    paramArry = reportParam.Split(';');

        //    string[] spParam = new string[paramArry.Length];
        //    for (int i = 0; i < paramArry.Length; i++)
        //    {
        //        var param = paramArry[i].Split('=');
        //        spParam[i] = "@" + param[0] + ":" + param[1];
        //    }

        //    DataTable dt = _bllSpExecute.SpExecuteTable(SpName, gubun, spParam);

        //    return dt;
        //}


        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    LoadReport();
        //}

        //public void LoadReport()
        //{
        //    GenerateReportService generateReportService = new GenerateReportService();

        //    var reportParam = (dynamic)HttpContext.Current.Session["ReportParam"];

        //    ReportDocument rd = new ReportDocument();

        //    string path = Path.Combine(Server.MapPath("~"), "Report/Rpt/" + reportParam.RptFileName + ".rpt");
        //    rd.Load(path);

        //    DataTable dt = new DataTable();

        //    if (!string.IsNullOrEmpty(reportParam.SpName))
        //    {
        //        string spName = reportParam.SpName;
        //        string gubun = reportParam.gubun;
        //        string[] paramArry;

        //        paramArry = reportParam.reportParam.Split(';');

        //        string[] spParam = new string[paramArry.Length];
        //        for (int i = 0; i < paramArry.Length; i++)
        //        {
        //            var param = paramArry[i].Split('=');
        //            spParam[i] = "@" + param[0] + ":" + param[1];
        //        }

        //        dt = _bllSpExecute.SpExecuteTable(spName, gubun, spParam);

        //    }

        //    dt.TableName = "StockStatusDB";

        //    rd.SetDataSource(dt);

        //    if (!string.IsNullOrEmpty(reportParam.subParam))
        //    {
        //        string[] paramArry = reportParam.subParam.Split(';');

        //        for (var i = 0; i < paramArry.Length; i++)
        //        {
        //            var param = paramArry[i].Split('=');
        //            rd.SetParameterValue(param[0], param[1]);
        //        }
        //    }
        //    //rd.SetParameterValue("item_nm2", "test");

        //    CrystalReportViewer1.ReportSource = rd;


        //    //rd.PrintToPrinter(1, false, 0, 0);
        //    //CrystalReportViewer1.SeparatePages = false;
        //    //CrystalReportViewer1.RefreshReport();



        //    //var dataSource = reportParam.DataSource;
        //    ////rd.SetDataSource(dataSource);


        //    //if (!String.IsNullOrEmpty(reportParam.DataSource.SpName))
        //    //{
        //    //    DataSet customDs = generateReportService.SetReportDs(reportParam.DataSource);
        //    //    rd.SetDataSource(customDs);
        //    //}

        //    //CrystalReportViewer1.LogOnInfo = logonInfos;
        //    //CrystalReportViewer1.ReportSource = rd;
        //    //CrystalReportViewer1.RefreshReport();
        //}

    }
}