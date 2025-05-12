var MenuManage_dataGrid;
var MenuManage_selectedRowNum;

// 페이지 로드시 오른쪽 form, 저장, 취소 버튼 비활성화
$(function () {
    MenuManage_EditCheck(false);
    MenuManage_dataGrid = $("#MenuManage_GridContainer").dxTreeList("instance");
});

// 포커스되어있는 row의 데이터 가져오는 함수
function MenuManage_getData() {
    MenuManage_dataGrid.selectRowsByIndexes(MenuManage_dataGrid.option("focusedRowIndex"));
    var data = MenuManage_dataGrid.getSelectedRowsData();
    MenuManage_fillData(data[0]);
}

// 수정중인지 체크
function MenuManage_EditCheck(nowEdit) {

    if (nowEdit) {
        $("#MenuManage_Save").dxButton().parent().parent().removeClass("display-none");
        $("#MenuManage_Undo").dxButton().parent().parent().removeClass("display-none");
        $("#MenuManage_Search").dxButton().parent().parent().addClass("display-none");
        $("#MenuManage_Input").dxButton().parent().parent().addClass("display-none");
        $("#MenuManage_Edit").dxButton().parent().parent().addClass("display-none");
        $("#MenuManage_Delete").dxButton().parent().parent().addClass("display-none");

        $("#MenuManage_RightForm :disabled").attr('disabled', false);
        $("#MenuManage_GridContainer").dxTreeList("option", "disabled", true);
    }
    if (!nowEdit) {
        $("#MenuManage_Save").dxButton().parent().parent().addClass("display-none");
        $("#MenuManage_Undo").dxButton().parent().parent().addClass("display-none");
        $("#MenuManage_Search").dxButton().parent().parent().removeClass("display-none");
        $("#MenuManage_Input").dxButton().parent().parent().removeClass("display-none");
        $("#MenuManage_Edit").dxButton().parent().parent().removeClass("display-none");
        $("#MenuManage_Delete").dxButton().parent().parent().removeClass("display-none");

        $("#MenuManage_RightForm :enabled").attr('disabled', true);
        $("#MenuManage_GridContainer").dxTreeList("option", "disabled", false);
    }
}

// 원료 팝업 생성
function MenuManage_Popup1() {
    var popup = $("#MenuManage_Popup1").dxPopup("instance");
    popup.option("contentTemplate", $("#MenuManage_List"));
    popup.show();
}

// 좌측 그리드 선택
function MenuManage_Focus_changed(e) {
    MenuManage_fillData(e.row.data);
}

// 좌측 그리드 상세항목 바인딩
function MenuManage_fillData(data) {
    $("#MenuManage_RightForm")[0].reset();

    // === 기본정보 ===
    $("#module_parent").val(data.module_parent);
    $("#module_parent_nm").val(data.module_parent_nm);

    $("#module_level").val(data.module_level);
    $("#module_cd").val(data.module_cd);
    $("#module_nm").val(data.module_nm);
    $("#module_seq").val(data.module_seq);
    $("#module_gb").val(data.module_gb);
}

// 조회
function MenuManage_search() {
    event.preventDefault();
    var form = $('#MenuManage_SearchParentCode')[0];
    var data = new FormData(form);

    $.ajax({
        type: 'POST',
        url: '/SysSet/MenuManageSelect',
        data: data,
        contentType: false,
        processData: false,
        cache: false,
        timeout: 600000,
        success: function (result) {
            $("#MenuManage_GridContainer").dxTreeList("option", "dataSource", result);
            if (result.length < 1)
                $("#MenuManage_RightForm")[0].reset();

            $("#MenuManage_pGubun").val("S");
        }
    })
}

// 입력
function MenuManage_input() {
    //MenuManage_dataGrid.selectRowsByIndexes(MenuManage_dataGrid.option("focusedRowIndex"));
    //MenuManage_selectedRowNum = MenuManage_dataGrid.getSelectedRowsData()[0].module_cd;
    $("#MenuManage_RightForm")[0].reset();

    MenuManage_EditCheck(true);

    MenuManage_input_init();
    $("#MenuManage_pGubun").val("I");
    $("#MenuManage_RightForm input[name=module_cd]").attr("readonly", false);
}
// 입력 초기화
function MenuManage_input_init() {
    console.log("MenuManage_input_init");
    var data = MenuManage_dataGrid.getSelectedRowsData();
    if (data == undefined || data.length < 1) {
        return;
    }

    $("#module_gb").val(data[0].module_gb);
    $("#module_parent").val(data[0].module_cd);
    $("#module_parent_nm").val(data[0].module_nm);
    $("#module_level").val(data[0].module_level + 1);
}

// 수정
function MenuManage_edit() {
    //MenuManage_dataGrid.selectRowsByIndexes(MenuManage_dataGrid.option("focusedRowIndex"));
    //MenuManage_selectedRowNum = MenuManage_dataGrid.getSelectedRowsData()[0].module_cd;

    MenuManage_EditCheck(true);

    $("#MenuManage_pGubun").val("U");

    $("#MenuManage_RightForm input[name=module_cd]").attr("readonly", true);


}

// 저장
function MenuManage_save() {
    MenuManage_validationCheck();

    event.preventDefault();
    DisableElements(false);
    var form = $('#MenuManage_RightForm')[0];
    var data = new FormData(form);
    DisableElements(true);
    MenuManage_selectedRowNum = data.get("module_cd");

    if (form.checkValidity()) {
        console.log("MenuManage_save");
        $.ajax({
            type: 'POST',

            url: '/SysSet/MenuManageCRUD',
            data: data,
            processData: false,
            contentType: false,
            cache: false,
            timeout: 600000,
            success: function (result) {
                var rstType = "success";
                var rstMsg = "";
                var rowIdx = 0;
                if (result.Ok) {
                    switch ($("#MenuManage_pGubun").val()) {
                        case "I":
                            MenuManage_dataGrid.option("focusedRowKey", MenuManage_selectedRowNum);
                            rstMsg = "입력이 완료 되었습니다";
                            break;
                        case "U":
                            rstMsg = "수정이 완료 되었습니다";
                            break;
                        case "D":
                            $("#MenuManage_RightForm")[0].reset();
                            MenuManage_dataGrid.option("focusedRowIndex", rowIdx);

                            rowIdx = MenuManage_dataGrid.option("focusedRowIndex");
                            console.log(rowIdx);
                            rstMsg = "삭제가 완료 되었습니다";
                            break;
                    }

                    MenuManage_EditCheck(false);
                    //if ($("#MenuManage_pGubun").val() == "D") {
                    //    MenuManage_dataGrid.deleteRow(MenuManage_dataGrid.option("focusedRowIndex"));
                    //} else {
                    //    MenuManage_search();
                    //}
                    MenuManage_search();
                    complete('MenuManage');
                }
                else {
                    rstType = "error";
                    rstMsg = "데이터 처리 중 오류가 발생 하였습니다";
                    $("#valdationChk").removeClass("display-none");
                    $("#valdationChk").text("* : 필수 입력사항입니다.");
                }
                ShowResultMessage(rstMsg, rstType)
            }
        })
    }
    else {
        form.reportValidity();
    }
}
function DisableElements(flag) {
    $("#module_cd").attr("disabled", flag);
    $("#module_gb").attr("disabled", flag);
    $("#MenuManage_pGubun").attr("disabled", flag);
}

function MenuManage_validationCheck() {
    $("#valdationChk").addClass("display-none");
}

// 취소
function MenuManage_undo() {
    MenuManage_validationCheck();
    MenuManage_getData();
    MenuManage_EditCheck(false);
    $("#MenuManage_pGubun").val("S");
    complete('MenuManage');
}

// 삭제
function MenuManage_del() {
    var curData = MenuManage_dataGrid.getSelectedRowsData()[0].module_nm;
    if (curData == "")
        return;

    var result = DevExpress.ui.dialog.confirm("<b>'" + curData + "' 데이터를 삭제 하시겠습니까?</b>", "모듈명");
    result.done(function (dialogResult) {
        if (dialogResult) {
            $("#MenuManage_pGubun").val("D");
            MenuManage_save();
        }
    });
}

// 팝업 폼
function MenuManage_searchParentCode() {
    var popup = $("#MenuManage_SearchParentCodePopup").dxPopup("instance");

    popup.show();
}
// 팝업 선택 이벤트
function MenuManage_SearchParentCodeRowDblClick(selectedItems) {
    $("#module_parent").val(selectedItems.values[0]);
    $("#module_parent_nm").val(selectedItems.values[1]);

    var popup = $("#MenuManage_SearchParentCodePopup").dxPopup("instance");
    popup.hide();
}

// 토스트 메세지
function ShowResultMessage(rstMsg, rstType) {
    DevExpress.ui.notify({
        message: rstMsg,
        animation: {
            show: { type: 'slide', duration: 500, from: { position: { my: 'top', at: 'bottom', of: window } } }, hide: { type: 'slide', duration: 1000, to: { position: { my: 'top', at: 'bottom', of: window } } }
        }
    }, rstType, 1000);
}