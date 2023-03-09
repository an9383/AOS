<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="HACCP.Report.ReportViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
       <%-- <input id="btnPrint" type="button" value="Print" onclick="Print ()" /> --%>
        <div id="Purchase">
            <input type="hidden" id="id" name="id" value="<%=id%>" />
            <input type="hidden" id="type" name="type" value="<%=type%>" />
            <input type="hidden" id="title" name="title" value="<%=title%>" />
            <input type="hidden" id="body" name="body" value="<%=body%>" />
            <input type="hidden" id="receive" name="receive" value="<%=receive%>" />
            <input type="hidden" id="cc" name="cc" value="<%=cc%>" />
            <input type="hidden" id="company_name" name="company_name" value="<%=company_name%>" />
            <input type="button" style="background-color:lightblue;display:none" name="send_mail" id="send_mail" value="메일발송" onclick="OnSendEmail()"  />
            
            <asp:ScriptManager ID="ScriptManager1" runat="server"   
                 EnablePageMethods="True">
            </asp:ScriptManager>
        </div>
        <div id = "dvReport" > 
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" PrintMode="Pdf" OnInit="CrystalReportViewer1_Init" OnUnload="CrystalReportViewer1_Unload" />  
        </div>
    </form>
</body>
</html>


<script type="text/javascript">  

    window.onload = function () {

        console.log(document.getElementById('type').value);

        //파라미터 = Purchase 외에는 버튼 보이지 않도록 숨김
        if (document.getElementById('type').value == null || document.getElementById('type').value == "") {
            document.getElementById('send_mail').style.display = "none";
        }
        else {
        //파라미터가 Purchase 일때만 버튼 보여주기
            document.getElementById('send_mail').style.display = "block";
        }

    }

    window.onclose = function () {
        PageMethods.CloseReport(OnSuccess, OnError);
    }


    function OnSendEmail() {
        //이메일이 있는지 체크
        var receive = document.getElementById('receive').value;
        if (receive == null || receive == "") {
            alert("거래처의 이메일이 없습니다. 거래처 정보를 확인하세요.");
            return;
        }

        if (confirm("메일을 전송하시겠습니까?")) {


            var type = document.getElementById('type').value;
            var id = document.getElementById('id').value;
            var company_name = document.getElementById('company_name').value;
            var cc = document.getElementById('cc').value;

            var title = "";
            var body = "";


            if (type == "purchase") {
                title = "[" +company_name + "] 발주서(" + id + ") 요청 건";
                body =
                        "안녕하세요.\n\n" +
                "[" + company_name + "] 에서 발주 요청 드립니다.\n" +
                        "첨부된 발주서 확인 부탁 드립니다.\n\n" +
                        "감사합니다.\n\n\n" +
                        "메일발송 전용 메일입니다.";
            }

            PageMethods.SendEmail(type, id, title, body, receive, cc, OnSuccess, OnError);

            return false;
        }
    }

    function OnSuccess(response) {
        alert(response);
    }
    function OnError(error) {
        alert(error);
    }

    //function Print() {  
    //    var dvReport = document.getElementById("dvReport");  
    //    var frame1 = dvReport.getElementsByTagName("iframe")[0];  
    //    if (navigator.appName.indexOf("Internet Explorer") != -1 || navigator.appVersion.indexOf("Trident") != -1) {  
    //        frame1.name = frame1.id;  
    //        window.frames[frame1.id].focus();  
    //        window.frames[frame1.id].print();  
    //    } else {  
    //        var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;  
    //        frameDoc.print();  
    //    }  
    //}  
</script>  