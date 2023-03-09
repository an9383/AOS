function getUserList() {

    var data = {
        pLoginTime: $("#logForm [name='login_time']").val().toString(),
        pLogoutTime: $("#logForm [name='logout_time']").val().toString()
    }

    console.log('Submitting form...');

    $.ajax({
        type: 'GET',
        url: '/SysSec/AccessLogUser',
        datatype: 'json',
        contentType: 'application/json; charset=utf-8',
        data: data,
        success: function (result) {
            $('#userListDiv').html(result);
        },
    });
}

function getDetail(selectedItems) {

    var data = selectedItems.selectedRowsData[0];

    if (data) {

        $('#LogDetailDiv').text(data.id);

        $.ajax({
            cache: false,
            url: "/SysSec/DetailLog?id=" + data.id,
            processData: false,
            contentType: false,
            type: 'GET',
            success: function (result) {
                $('#LogDetailDiv').html(result);
            }
        });
    }
}

function AccessLog(selectedItems) {
    alert("AccessLog of JS");
}