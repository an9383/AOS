
var _targetMenuId = null;           //서명그리드 화면 Id
var _targetSignSetCd = null;        //서명그리드 서명코드
var _saveSignCallbackFunc = "";     //서명저장후, 실행할 함수 이름
var _cancelSignCallbackFunc = "";   //서명취소후, 실행할 함수 이름

// 문서 로드 완료 후
$(document).ready(function () {

})

//서명 div에 sign_set_cd 클래스 설정 및 툴팁으로 코드 보여주기
function CommonSign_SetSignSetData(signSetData, menuId) {

    _targetMenuId = menuId;
    _targetSignSetCd = signSetData.sign_set_cd;

    $("#" + menuId + " .sign_set_cd_tooltip").text(_targetSignSetCd);
    $("#" + menuId + " .sign_set_nm").text(signSetData.sign_set_nm);
    $("#" + menuId + " .sign_set_nm").attr("id", _targetSignSetCd);

    var p = document.getElementsByClassName("sign_set_nm")[0];

     //서명의미 더블클릭시, 전자서명 권한 설정 화면으로 이동
    p.addEventListener("dblclick", function () {

        var urlInfo = {
            tabName: "전자서명 권한 설정",
            tabId: "SignSet_InputDetail",
            url: "/SysOp/SignSet_InputDetail"
        };

        var param = {

        };

        urlInfo.param = param;

        openOtherPage(urlInfo);
    })

}


// 서명 데이터 조회
function CommonSign_GetSignListData(sign_set_cd, index_key, gridId) {

    $.ajax({
        type: 'POST',
        async: false,
        url: '/Sign/getSignList',
        data: {
            sign_set_cd: sign_set_cd,
            index_key: index_key
        },
        success: function (result) {
            if (result.length > 0) {
                var json = JSON.parse(result);

                $("#" + gridId).dxDataGrid("instance").option("dataSource", json);
               // $("#" + gridId).dxDataGrid("instance").option("focusedRowIndex", 0);


            } else {
                $("#" + gridId).dxDataGrid("instance").option("dataSource", []);
                //$("#" + gridId).dxDataGrid("instance").option("focusedRowIndex", -1);

                toatr.warning("", "조회된 전자서명 데이터가 없습니다.");
            }
        }
    })

    UtilView.setGridFocus("G", _targetMenuId+"SignGrid", true); 

}

// 서명 리스트 입력
function CommonSign_InsertSignList(sign_set_cd, index_key) {

    $.ajax({
        type: 'POST',
        async: false,
        url: '/Sign/InsertSignList',
        data: {
            sign_set_cd: sign_set_cd,
            index_key: index_key
        },
        success: function (result) {
            if (result.length > 0) {
            }
        }
    })

}

// 서명 전체삭제 - 마스터 데이터 삭제시 실행
function CommonSign_DeleteSignListData(sign_set_cd, index_key) {

    $.ajax({
        type: 'POST',
        async: false,
        url: '/Sign/DeleteSignList',
        data: {
            sign_set_cd: sign_set_cd,
            index_key: index_key
        },
        success: function (result) {
            if (result.length > 0) {
            }
        }
    })

}

//유효 사용자 확인
function CommonSignIDValidation() {
    var emp_cd = "";

    var data = new FormData($('#CommonSignForm')[0]);

    data.set("gubun", "S");

    $.ajax({
        type: 'POST',
        url: '/Comm/IDValidation',
        data: data,
        contentType: false,
        processData: false,
        async: false,
        success: function (result) {

            if (!result) {
                //alert("잘못된 ID,PW 입니다.");
                toastr.warning("", "잘못된 ID,PW 입니다.");

            }

            var json = JSON.parse(result)[0];
            emp_cd = json.emp_cd;

            //로그인폼 부서, 사원명 설정
            $("#CommonSignForm input[name='dept_nm']").val(json.dept_nm);
            $("#CommonSignForm input[name='emp_nm']").val(json.emp_nm);
        }
    });

    return emp_cd;
}


//서명팝업 
//확인 클릭
function CommonSignSubmit() {

    setTimeout(function () {
        var popup = $("#CommonSignPopup").dxPopup("instance");
        popup.hide();
    }, 1000);

    //유효한 사용자인지 확인
    var validate_emp_cd = CommonSignIDValidation();
    if (validate_emp_cd == "") return;

    //서명권한이 있는지 확인
    if (!CommonSignAuthorityCheck(validate_emp_cd)) return;
}

//서명 폼 초기화
function CommonSignFormReset() {
    $('#CommonSignForm')[0].reset();
}

//권한 체크
function CommonSignAuthorityCheck(validate_emp_cd) {
    var grid = $("#" + _targetMenuId + "SignGrid").dxDataGrid("instance");
    var gridData = getGridRowByKey(_targetMenuId + "SignGrid", grid.option("focusedRowKey"));

    var signInfo = gridData;

    if (signInfo != null) {

        $.ajax({
            type: 'POST',
            url: '/Sign/CheckAuthority',
            data: {
                sign_emp_cd: validate_emp_cd,
                sign_set_cd: _targetSignSetCd,
                sign_set_dt_id: signInfo.sign_set_dt_id
            },
            async: false,
            success: function (result) {

                if (result != "") {

                    var json = JSON.parse(result);

                    if (json.length > 0) {

                        var representative_yn = json[0].representative_yn;
                        var necessary_yn = json[0].necessary_yn;

                        //대리자 또는 필수자 권한에 해당될때(권한 있음)
                        if (representative_yn == "Y" || necessary_yn == "Y") {

                            //서명 저장
                            if (UtilView.isEmpty(signInfo.sign_time)) {
                                signInfo.sign_representative_yn = (representative_yn == "Y" && necessary_yn == "N") ? "Y" : "N";
                                signInfo.sign_emp_cd = validate_emp_cd;

                                //서명시간 없을때 서명저장
                                SaveSignData(signInfo);
                                //window[_targetMenuId + "Search"](); //데이터 재조회
                                CommonSign_GetSignListData(_targetSignSetCd, signInfo.index_key, _targetMenuId + "SignGrid");
                            } else {
                             //서명 취소
                                //서명시간 있을때 서명취소
                                CancelSignData(signInfo);
                                //window[_targetMenuId + "Search"]();//데이터 재조회
                                CommonSign_GetSignListData(_targetSignSetCd, signInfo.index_key, _targetMenuId + "SignGrid");
                            }
                            
                            //else if ((signInfo.sign_time != null && signInfo.sign_time != "") && (validate_emp_cd == signInfo.sign_emp_cd)) {
                            //    //서명시간 있을때 서명취소
                            //    CancelSignData(signInfo);
                            //    window[_targetMenuId + "Search"]();//데이터 재조회
                            //    CommonSign_GetSignListData(_targetSignSetCd, signInfo.index_key, _targetMenuId + "SignGrid"); 

                            //}
                            //else if ((signInfo.sign_time != null && signInfo.sign_time != "") && (validate_emp_cd != signInfo.sign_emp_cd)) {

                            //    //기존 서명 취소시, 기존 서명자가 취소하는지 확인
                            //    //alert("기존서명자와 다릅니다."); return false;
                            //    toastr.warning("", "기존서명자와 다릅니다."); return false;
                            //}

                        } else if (representative_yn != "Y" && necessary_yn != "Y") {
                            //alert("서명 권한이 없습니다."); return false;
                            toastr.warning("", "서명 권한이 없습니다."); return false;

                        }
                    }
                } else {
                    //alert("서명 권한이 없습니다."); return false;
                    toastr.warning("", "서명 권한이 없습니다."); return false;

                }

            }
        });
    }

}

// 서명 저장
function SaveSignData(signInfo) { 

    $.ajax({
        type: 'POST',
        async: false,
        url: '/Sign/SaveSign',
        data: {
            sign_emp_cd: signInfo.sign_emp_cd,
            sign_type: '2',
            sign_representative_yn: signInfo.sign_representative_yn,
            index_key: signInfo.index_key,
            sign_history_id: signInfo.sign_history_id
        },
        success: function (result) {
            if (result.length > 0) {
                var json = JSON.parse(result);
                var message = json.message;

                if (message != "") {
                    toastr.success("성공", "서명 저장하였습니다.");

                    if (_saveSignCallbackFunc != "") window[_saveSignCallbackFunc]();

                } else {
                    toastr.warning("실패", "서명 실패하였습니다.");

                }
            } else {
                toastr.warning("실패", "서명 실패하였습니다.");
            }
        }
    })

}

// 서명 취소
function CancelSignData(signInfo) {

    $.ajax({
        type: 'POST',
        async: false,
        url: '/Sign/CancelSign',
        data: {
            index_key: signInfo.index_key,
            sign_history_id: signInfo.sign_history_id
        },
        success: function (result) {
            if (result.length > 0) {
                var json = JSON.parse(result);
                var message = json.message;

                if (message != "") {
                    //alert("서명 취소하였습니다.");
                    toastr.success("성공", "서명 취소되었습니다.");

                    if (_cancelSignCallbackFunc != "") window[_cancelSignCallbackFunc]();

                } else {
                    //alert("서명 취소 실패하였습니다.");
                    toastr.warning("실패", "서명 취소 실패하였습니다.");

                }
            } else {
                //alert("서명 취소 실패하였습니다.");
                toastr.warning("실패", "서명 취소 실패하였습니다.");

            }

            //GetSignListData(_targetSignSetCd, signInfo.index_key, _targetMenuId + "SignGrid"); 
        }
    })
}


//서명그리드 이벤트

//서명칸 클릭시
function CommonSignGrid_SignClick(e) {

    if (e.columnIndex != 3) return;

    var grid = $("#" + _targetMenuId + "SignGrid").dxDataGrid("instance");
    var gridData = getGridRowByKey(_targetMenuId + "SignGrid", grid.option("focusedRowKey"));

    //전자서명 저장 또는 취소할 수 있는 조건이 되는지 확인
    if (gridData != null) {

        //서명시간 있을때 -> 취소
        if (gridData.sign_time != null && gridData.sign_time != "") {

            //다음단계의 서명이 되어있으면 취소하지 못함
            if (gridData.next_sign_yn == "Y") {
                //alert("다음단계 서명이 이미 진행되었습니다."); return;
                toastr.warning("", "다음단계 서명이 이미 진행되었습니다."); return;
            }

        }

        //서명이 없을때 -> 저장
        else {
            //다음단계의 서명이 되어있으면 취소하지 못함
            if (gridData.sign_set_dt_seq > 1 && gridData.prev_sign_yn == "N") {
                //alert("전단계 서명을 먼저 진행해주세요."); return;
                toastr.warning("", "전단계 서명을 먼저 진행해주세요."); return;

            }
        }

        _targetMenuId = (e.element[0].id).split("SignGrid")[0];
        _targetSignSetCd = gridData.sign_set_cd;

        //전자서명 로직
        var popup = $("#CommonSignPopup").dxPopup("instance");
        popup.option("contentTemplate", $("#CommonSignPopupTemplate"));
        popup.show();

    } else {
        //alert("관련 데이터가 없습니다."); return;
        toastr.warning("", "서명 관련 데이터가 없습니다."); return;

    }
}


