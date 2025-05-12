
//$(document).ready(function () {
//    $("#btnLoadReport").click(function () {
//        ReportManager.LoadReport();
//    });
//});

function testReport(param) {

    ReportManager.LoadReport(param);
}

var ReportManager = {
    LoadReport: function (param) {

        var jsonParam = param;
        var serviceUrl = "/Report/ViewReport";
        ReportManager.GetReport(serviceUrl, jsonParam, onFailed);
        function onFailed(error) {
            alert("Found Error");
        }
    },

    GetReport: function (serviceUrl, jsonParams, errorCallback) {

        // 다운로드
        //document.location.assign(serviceUrl + "?" + htmlUtils.jsonToQS(jsonParams));

        jQuery.ajax({
            url: serviceUrl,
            type: "GET",
            data: {
                reportParam: JSON.stringify(jsonParams)
            },
            contentType: "application/json; charset=utf-8",
            success: function (result) {

                window.open('../Report/ReportViewer.aspx', '_newtab');
            },
            error: errorCallback
        });
    }
}