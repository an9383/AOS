var curGubun = '';

function search() {
    curGubun = 'S';
    //$("#MessageInfoRightMain input").prop('readonly', true);

    var data = {
        pMessage: $("#pMessage").val().toString(),
    }

    var dataSource = {
        load: function () {
            var items = $.Deferred();
            $.ajax({
                type: 'GET',
                url: '/SysSet/MessageInfoSelect',
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                data: data,
                success: function (data) {

                    if (data != "") {
                        var objJson = JSON.parse(data);// jQuery.parseJSON(data);   // JSON.parse(data);
                        console.log(objJson);
                        items.resolve(objJson);
                    }
                    else {
                        items.resolve(null);
                    }
                }
            });
            //console.log(items.promise());
            return items.promise();
        }
    };

    //$.ajax({
    //    type: 'GET',
    //    url: '/SysSet/MessageInfoSelect',
    //    datatype: 'json',
    //    contentType: 'application/json; charset=utf-8',
    //    data: data,
    //    success: function (data) {
    //        console.log(result);
    //        $("#gridContainer").dxDataGrid({
    //            dataSource: data,
    //        })
    //    },
    //});

    $("#gridContainer").dxDataGrid({
        dataSource: dataSource,
        focusedRowEnabled: true,
        focusedRowIndex: 0,
        selectedRowIndex:0,
    });

    //$.ajax({
    //    type: 'GET',
    //    url: '/SysSet/MessageInfo',
    //    datatype: 'json',
    //    contentType: 'application/json; charset=utf-8',
    //    data: data,
    //    success: function (result) {
    //        console.log(result);
    //        //$('#MessageInfoLeftMain').html(result);
    //    },
    //});
}

function input() {
    $("#MessageInfoRightMain input").prop('readonly', false);
    curGubun = 'I';
    $("#messageInfo_code").val("");
    $("#messageInfo_title").val("");
    $("#messageInfo_name").val("");
    $("#messageInfo_bigo").val("");
}

function edit() {
    $("#MessageInfoRightMain input").prop('readonly', false);
    curGubun = 'U';
}

function del() {
    curGubun = 'D';
}

function save() {
    // if readonly == false

    var data = {
        pGubun: curGubun,
        pCode : $("#messageInfo_code").val(),
        pTitle : $("#messageInfo_title").val(),
        pName : $("#messageInfo_name").val(),
        pBigo : $("#messageInfo_bigo").val()
    }

    $.ajax({
        type: 'GET',
        url: '/SysSet/MessageInfoSave',
        datatype: 'json',
        contentType: 'application/json; charset=utf-8',
        data: data,
        success: function (data) {
            console.log(data);
            if (data == "") {
                alert('정상');
            }
            else {
                alert('비정상 : ' + data);
            }
        },
        error: function (request, status, error) {
            alert("code:" + request.status + "\n" + "message:" + request.responseText + "\n" + "error:" + error);
        }
    });

    search();
    //$("#MessageInfoRightMain input").prop('readonly', true);
}

function executeProcedure() {
    alert("Called executeProcedure");
}
