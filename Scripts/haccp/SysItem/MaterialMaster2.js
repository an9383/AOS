var MaterialMaster2_dataGrid;

var MaterialMaster2_selectedItemCd;

var MaterialMaster2_duplicatedCheck = false;

// 페이지 로드시 오른쪽 form, 저장, 취소 버튼 비활성화
$(function () {
    EditCheck(false);
    MaterialMaster2_dataGrid = $("#MaterialMaster2GridContainer").dxDataGrid("instance");
    //$('#MaterialMaster2RightForm').validate();
});

// 포커스되어있는 row의 데이터 가져오는 함수
function getData() {

    MaterialMaster2_dataGrid = $("#MaterialMaster2GridContainer").dxDataGrid("instance");

    MaterialMaster2_dataGrid.selectRowsByIndexes(MaterialMaster2_dataGrid.option("focusedRowIndex"));

    var data = MaterialMaster2_dataGrid.getSelectedRowsData();

    MaterialMaster2FillData(data[0]);
}

// 수정중인지 체크
function EditCheck(nowEdit) {

    if (nowEdit) {

        $("#MaterialMaster2Save").dxButton().parent().parent().removeClass("display-none");
        $("#MaterialMaster2Undo").dxButton().parent().parent().removeClass("display-none");
        $("#MaterialMaster2Search").dxButton().parent().parent().addClass("display-none");
        $("#MaterialMaster2Input").dxButton().parent().parent().addClass("display-none");
        $("#MaterialMaster2Edit").dxButton().parent().parent().addClass("display-none");
        $("#MaterialMaster2Delete").dxButton().parent().parent().addClass("display-none");

        $("#MaterialMaster2RightForm :disabled").attr('disabled', false);
        $("#MaterialMaster2GridContainer").dxDataGrid("option", "disabled", true);
    }
    if (!nowEdit) {

        $("#MaterialMaster2Save").dxButton().parent().parent().addClass("display-none");
        $("#MaterialMaster2Undo").dxButton().parent().parent().addClass("display-none");
        $("#MaterialMaster2Search").dxButton().parent().parent().removeClass("display-none");
        $("#MaterialMaster2Input").dxButton().parent().parent().removeClass("display-none");
        $("#MaterialMaster2Edit").dxButton().parent().parent().removeClass("display-none");
        $("#MaterialMaster2Delete").dxButton().parent().parent().removeClass("display-none");

        $("#MaterialMaster2RightForm :enabled").attr('disabled', true);
        $("#MaterialMaster2GridContainer").dxDataGrid("option", "disabled", false);
    }

}

// 원료 팝업 생성
function searchMaterial() {

    console.log("searchMaterial Called");

    var popup = $("#MaterialMaster2MaterialPopup").dxPopup("instance");

    popup.show();

}

// 원료 팝업 선택
function MaterialMaster2MaterialRowDblClick(selectedItems) {

    console.log(selectedItems.values[1]);

    $("#MaterialMaster2Material_cd").val(selectedItems.values[0]);
    $("#MaterialMaster2Material").val(selectedItems.values[1]);

    var popup = $("#MaterialMaster2MaterialPopup").dxPopup("instance");

    popup.hide();
}

// 죄측 그리드 선택 변경시 실행
function MaterialMaster2Focus_changed(e) {

    MaterialMaster2FillData(e.row.data);
}

// 좌측 그리드 내용 삽입
function MaterialMaster2FillData(data) {

    $("#MaterialMaster2RightForm")[0].reset();
    $("#checkResText").text("");

    // === 기본정보 ===
    $('#MaterialMaster2RightForm input[name="item_cd"]').val(data.item_cd);
    $('#MaterialMaster2RightForm input[name="item_nm"]').val(data.item_nm);
    $('input[name="abbreviation_cd"]').val(data.abbreviation_cd);
    $('input[name="item_s_nm"]').val(data.item_s_nm);
    $('input[name="item_enm"]').val(data.item_enm);
    $('input[name="item_jnm"]').val(data.item_jnm);
    if (data.prod_end == "Y") {
        $('input[name="prod_end"]').attr("checked", true);
    } else {
        $('input[name="prod_end"]').attr("checked", false);
    }
    //$('input[name="break_type"]').val(data.break_type);


    // === 분류정보 ===
    if (data.outer_process != "") {
        $('input[name="outer_process"][value=' + data.outer_process + ']').prop("checked", true);
    }
    if (data.in_out_ck != "") {
        $('input[name="in_out_ck"][value=' + data.in_out_ck + ']').prop("checked", true);
    }
    if (data.abc_gubun != "") {
        $('select[name="abc_gubun"]').val(data.abc_gubun).prop("selected", true);
    }
    if (data.plant_cd != "") {
        $('select[name="plant_cd"]').val(data.plant_cd).prop("selected", true);
    }
    if (data.item_type1 != "") {
        $('select[name="form_item_type1"]').val(data.item_type1).prop("selected", true);
    }
    if (data.item_type2 != "") {
        $('select[name="form_item_type2"]').val(data.item_type2).prop("selected", true);
    }


    // === 주요정보 ===
    if (data.item_type3 != "") {
        $('select[name="item_type3"]').val(data.item_type3).prop("selected", true);
    }
    if (data.item_unit_cd != "") {
        $('select[name="item_unit_cd"]').val(data.item_unit_cd).prop("selected", true);
    }
    $('input[name="item_validity_period"]').val(data.item_validity_period);
    if (data.item_validity_period_unit != "") {
        $('select[name="item_validity_period_unit"]').val(data.item_validity_period_unit).prop("selected", true);
    }
    $('input[name="keeping_term"]').val(data.kepping_term);
    if (data.kepping_term_unit != "") {
        $('select[name="keeping_term_unit"]').val(data.kepping_term_unit).prop("selected", true);
    }
    if (data.item_keep_condition != "") {
        $('select[name="item_keep_condition"]').val(data.item_keep_condition).prop("selected", true);
    }
    if (data.item_keep_temperature != "") {
        $('select[name="item_keep_temperature"]').val(data.item_keep_temperature).prop("selected", true);
    }
    if (data.keeping_warehouse != "") {
        $('select[name="keeping_warehouse"]').val(data.keeping_warehouse).prop("selected", true);
    }


    // === 시험정보 ===
    if (data.item_exam_yn == "Y") {
        $('input[name="item_exam_yn"]').attr("checked", true);
    } else {
        $('input[name="item_exam_yn"]').attr("checked", false);
    }
    if (data.asepsis_item_ck == "Y") {
        $('input[name="asepsis_item_ck"]').attr("checked", true);
    } else {
        $('input[name="asepsis_item_ck"]').attr("checked", false);
    }
    if (data.item_sampling == "Y") {
        $('input[name="item_sampling"]').attr("checked", true);
    } else {
        $('input[name="item_sampling"]').attr("checked", false);
    }
    if (data.sampling_calc != "") {
        $('select[name="sampling_calc"]').val(data.sampling_calc).prop("selected", true);
    }
    if (data.gmo_material_yn == "Y") {
        $('input[name="gmo_material_yn"]').attr("checked", true);
    } else {
        $('input[name="gmo_material_yn"]').attr("checked", false);
    }
    if (data.retest_yn == "Y") {
        $('input[name="retest_yn"]').attr("checked", true);
    } else {
        $('input[name="retest_yn"]').attr("checked", false);
    }


    // === 구매정보 ===
    $('input[name="basic_purchase_qty"]').val(data.basic_purchase_qty);
    if (data.basic_purchase_unit != "") {
        $('select[name="basic_purchase_unit"]').val(data.basic_purchase_unit).prop("selected", true);
    }
    $('input[name="delivery_period"]').val(data.delivery_period);
    $('input[name="test_period"]').val(data.test_period);
    $('input[name="item_safety_day"]').val(data.item_safety_day);
}

// 조회
function materialMaster2Search() {
    $("#pGubun").val("S");

    event.preventDefault();
    var form = $('#MaterialMaster2Form')[0];
    var data = new FormData(form);
    console.log(form);
    console.log(data);

    $.ajax({
        type: 'POST',
        //enctype: 'multipart/form-data',
        url: '/SysItem/SelectMaterials',
        data: data,
        contentType: false,
        processData: false,
        cache: false,
        timeout: 600000,
        success: function (result) {
            $("#MaterialMaster2GridContainer").dxDataGrid("option", "dataSource", result);
        }
    })
}

// 입력
function materialMaster2Input() {

    MaterialMaster2_dataGrid.selectRowsByIndexes(MaterialMaster2_dataGrid.option("focusedRowIndex"));

    MaterialMaster2_selectedItemCd = MaterialMaster2_dataGrid.getSelectedRowsData()[0].item_cd;

    MaterialMaster2_duplicatedCheck = false;

    $("#MaterialMaster2RightForm")[0].reset();
    $("#checkResText").text("");

    $("#pGubun").val("I");
    EditCheck(true);
    //$("#pGubun").focus();
}

// 수정
function materialMaster2Edit() {

    MaterialMaster2_dataGrid.selectRowsByIndexes(MaterialMaster2_dataGrid.option("focusedRowIndex"));

    MaterialMaster2_selectedItemCd = MaterialMaster2_dataGrid.getSelectedRowsData()[0].item_cd;

    $("#pGubun").val("U");
    EditCheck(true);
}

// 저장
function materialMaster2Save() {

    validationCheck();

    if (!MaterialMaster2_duplicatedCheck) {
        alert("원료 코드 중복체크를 확인해주세요.");
        return;
    }

    event.preventDefault();
    var form = $('#MaterialMaster2RightForm')[0];
    var data = new FormData(form);
    // checkbox 값 제어
    data.set("prod_end", $('#prod_end').is(':checked') ? 'Y' : 'N');

    //$('#MaterialMaster2RightForm').submit();

    if (form.checkValidity()) {
        $.ajax({
            type: 'POST',
            //enctype: 'multipart/form-data',
            url: '/SysItem/MaterialCRUD',
            data: data,
            processData: false,
            contentType: false,
            success: function (result) {
                //console.log(CRUDstate + "성공");

                if (result.Ok) {
                    //alert('success');

                    EditCheck(false);
                    materialMaster2Search();
                    MaterialMaster2_duplicatedCheck = false;

                    MaterialMaster2_dataGrid = $("#MaterialMaster2GridContainer").dxDataGrid("instance");
                    MaterialMaster2_dataGrid.option("focusedRowKey", data.get("item_cd"));
                    MaterialMaster2_dataGrid.selectRowsByIndexes(MaterialMaster2_dataGrid.option("focusedRowIndex"));

                    complete('MaterialMaster2');
                }
                else {
                    $("#valdationChk").removeClass("display-none");
                    $("#valdationChk").text("* : 필수 입력사항입니다.");
                }
            }
        })
    }
    else {
        form.reportValidity();
    }
}

// 취소
function materialMaster2Undo() {
    console.log("undo");

    validationCheck();

    getData();
    //setTimeout(getData, 200);

    MaterialMaster2_duplicatedCheck = false;

    EditCheck(false);
    $("#pGubun").val("S");
    complete('MaterialMaster2');
}

// 삭제
function materialMaster2Delete() {

    CRUDstate = "delete";

    var MaterialMaster2_dataGrid = $("#MaterialMaster2GridContainer").dxDataGrid("instance");

    MaterialMaster2_dataGrid.selectRowsByIndexes(MaterialMaster2_dataGrid.option("focusedRowIndex"));

    var data = MaterialMaster2_dataGrid.getSelectedRowsData();

    console.log(data[0].item_cd);

    var result = DevExpress.ui.dialog.confirm("<i> '" + data[0].item_cd + "' 항목을 삭제하시겠습니까? </i>", "원료코드");

    result.done(function (dialogResult) {

        if (dialogResult) {
            $.ajax({
                type: 'POST',
                url: "/SysItem/MaterialDelete",
                dataType: "json",
                data: { item_cd: data[0].item_cd },
                success: function (result) {
                    //console.log(CRUDstate + "성공");

                    var type = result ? "success" : "error",
                        text = result ? "삭제 성공" : "삭제 실패";

                    console.log(result);

                    DevExpress.ui.notify(text, type, 2000);

                    materialMaster2Search();
                }
            })
        } else {
            return;
        }

    });
}

function validationCheck() {
    $("#valdationChk").addClass("display-none");
}

// 우측 하단 탭 이동(주요정보, 시험정보, 구매정보, 제조처정보)
function menutab(tabId, divId, tabNum) {
    $("#" + divId + " > div").addClass("display-none");
    $("#" + tabId + " a").removeClass("active");

    $("#" + divId + "_" + tabNum).removeClass("display-none");
    $("#" + tabId + " li:nth-child(" + tabNum + ")").children('a').addClass("active");
}

// 원료 코드 중복 체크
function duplicateCheck() {

    console.log(MaterialMaster2_selectedItemCd);

    var data = '{ "pItemCd":"' + $('input[name = "item_cd"]:nth-child(2)').val() + '", '
        + '"pGubun": "' + $('#pGubun').val() + '", '
        + '"pSelectedItemCd": "' + MaterialMaster2_selectedItemCd + '"'
        + '}';

    if ($('input[name = "item_cd"]:nth-child(2)').val() == "" || $('input[name = "item_cd"]:nth-child(2)').val() == null) {

        MaterialMaster2_duplicatedCheck = false;

        $("#checkResText").text("빈 문자열 입니다.");
        $("#checkResText").css("color", "red");

        return;
    }

    $.ajax({
        type: 'GET',
        url: '/SysItem/ItemCdDuplicateCheck',
        datatype: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.parse(data),
        success: function (result) {
            $("#checkResText").text(result[1]);

            if (result[0] == "0") {
                MaterialMaster2_duplicatedCheck = false;
                $("#checkResText").css("color", "red");
            }
            else if (result[0] == "1") {
                MaterialMaster2_duplicatedCheck = true;
                $("#checkResText").css("color", "green");
            }
        }
    })
}

// 원료 코드 수정했을시
function rewriteItemCd() {
    MaterialMaster2_duplicatedCheck = false;
    $("#checkResText").text("");
}