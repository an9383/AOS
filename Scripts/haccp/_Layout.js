

// 문서 로드 완료 후
$(document).ready(function () {

    // ajax 요청의 기본 설정 
    //  오버라이드 가능
    //      beforeSend : 로딩 인디케이터 표시
    //      error : 401 에러시 (서버 session이 끊겨서 sessionCheckFilter 에서 401에러 리턴) 브라우저 sessionStorage 비우고 로그인 팝업 띄움
    //      complete : 로딩 인디케이터 숨김
    $.ajaxSetup({
        beforeSend: function () {
            if (document.getElementById('loadPanel')) {
                $("#loadPanel").dxLoadPanel("instance").show();
            }
        },
        error: function (request, status, error) {
            if (request.status == 401) {
                sessionStorage.clear();
                reLogIn();
            }
        },
        complete: function () {
            if (document.getElementById('loadPanel')) {
                $("#loadPanel").dxLoadPanel("instance").hide();
            }
        }
    });

    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("active");
    });

    // 탭에 닫기버튼 클릭
    $("#tabsContainer").on("click", ".closeTab", function () {

        var viewName = $(this).prev().text();
        var tab_index = longTabs.findIndex(x => x.text == viewName);

        if ($(this).parent().parent().hasClass("edited") == true) {

            var message = "'" + viewName + "' 탭에 저장되지 않은 변경사항이 있습니다.닫으시겠습니까 ?";
            var title = "탭 종료";

            confirmAction(message, title).done(function (result) {

                if (result) {

                    $("#contentDiv > div").addClass("display-none");

                    longTabs.splice(tab_index, 1);

                    var str = "#" + $(this).attr("id");
                    var divId = str.substring(0, str.length - 4);

                    $("#FocusedName").children().html(
                        "<span class='dx-button-text'>" + longTabs[tab_index - 1].text + "</span>");

                    $(divId).remove();
                    $(divId + "Js").remove();

                    removeProgramSession(divId.substring(1, divId.length));

                    str = "#" + $(this).parent().parent().prev().children().children("button").attr("id");
                    divId = str.substring(0, str.length - 4);

                    $(divId).removeClass("display-none");

                    $(this).parent().parent().remove();
                    moveTabFocus(tab_index - 1);
                } else {

                    return;
                }

            });
        } else {

            $("#contentDiv > div").addClass("display-none");

            longTabs.splice(tab_index, 1);

            var str = "#" + $(this).attr("id");
            var divId = str.substring(0, str.length - 4);

            $("#FocusedName").children().html(
                "<span class='dx-button-text'>" + longTabs[tab_index - 1].text + "</span>");

            $(divId).remove();
            $(divId + "Js").remove();

            removeProgramSession(divId.substring(1, divId.length));

            str = "#" + $(this).parent().parent().prev().children().children("button").attr("id");
            divId = str.substring(0, str.length - 4);

            $(divId).removeClass("display-none");

            $(this).parent().parent().remove();
            moveTabFocus(tab_index - 1);
        }

        

        // 새로고침 제거 
        var data_item_id = $("li.dx-treeview-node.dx-treeview-item-without-checkbox.dx-treeview-node-is-leaf[aria-label='" + viewName + "']").attr("data-item-id");

        //data_item_id undefined로 오류떠서 조건추가
        if (!(data_item_id === undefined)) {
            var position_index = data_item_id.indexOf('*');
            var viewId = data_item_id.substr(position_index + 1);

            UtilView.intervalUtil.intervalSetDelete(viewId);
        }


    });

});

//세션 관리 시작 ---
function reLoginPopup() {

    if (!sessionStorage.getItem('loginCD')) {
        return;
    }

    sessionTimer = setTimeout(reLogIn, 30 * 60 * 1000); // 서버 세션과 동일하게
    //sessionTimer = setTimeout(reLogIn, 5000);
}

function reLogIn() {
    if (!sessionStorage.getItem('loginCD')) {
        return;
    }

    alert("세션이 끊어졌습니다. 다시 로그인 해주세요.");
    $("#LoginPopup").dxPopup("instance").show();
    $("div#loginFieldset div.align-center.p-2 img").attr("src", sessionStorage.getItem("plantLogo"));

    sessionStorage.clear();

}

//세션체크 후 재로그인
function reLogInFormSubmit() {

    var form = $('#loginForm')[0];
    var data = new FormData(form);

    $.ajax({
        type: 'POST',
        url: '/Comm/Login',
        contentType: false,
        processData: false,
        data: data,
        async: false,
        success: function (result) {
            sessionStorage.clear();

            if (!result.OK) {
                alert(result.errorMessage);
                return;
            } else {

                if (new Date() > new Date(result.endTime).setDate(new Date(result.endTime).getDate() - 7)) {
                    alert("패스워드 사용기한이 '" + result.endTime + "'까지 입니다. \n패스워드를 변경하세요.");
                }

                sessionStorage.setItem('loginID', result.loginID);
                sessionStorage.setItem('loginCD', result.loginCD);
                sessionStorage.setItem('loginNM', result.loginNM);
                sessionStorage.setItem('loginPosition', result.loginPosition);
                sessionStorage.setItem('plantCD', result.plantCD);
                sessionStorage.setItem('plantNM', result.plantNM);
                sessionStorage.setItem('deptCD', result.deptCD);
                sessionStorage.setItem('deptNM', result.deptNM);
                sessionStorage.setItem('endTime', result.endTime);

                $("#LoginPopup").dxPopup("instance").hide();

                reLoginPopup();

                return;
            }
        }
    });
}
//세션 관리 종료 ---

function moveTabFocus(tab_index) {

    $("#tabsContainer").dxTabs("instance").option("selectedIndex", tab_index);
    $("#contentDiv > div").addClass("display-none");

    UtilView.intervalUtil.intervalRestart($("#tabsContainer").dxTabs("instance").option("selectedItem"));

    $("#" + longTabs[tab_index].id).removeClass("display-none");

}


// 프로그램 종료전 세션 삭제 및 audit 작성
function removeProgramSession(programCD) {

    $.ajax({
        type: 'POST',
        url: '/Comm/RemoveProgramSession',
        data: {
            programCD: programCD
        },
        success: function (result) {

        }
    });
}

// 팝업 생성 (팝업ID, 팝업 헤더, 데이터소스(JSON), 컬럼정보({ dataField: "", caption: "" }의 배열))
// searchFunc , serch 부분 추가
// searchFunc = 부모창에 값 들어가게 해주는 함수
// serch = 검색하려는 값
function createPopup(popupID, headerName, ds, columns, keyExpr, searchFunc, serch, width = 550, height = 750) {
    $("#" + popupID + "Popup").dxPopup({
        width: width,
        height: height,
        visible: false,
        showTitle: true,
        title: headerName,
        closeOnOutsideClick: false,
        onHidden: function () {
            clearSearchPanel(popupID);
        },
        contentTemplate: function (container) {
            var scrollView = $('<div id = "' + popupID + '_gridContainer" />');

            if (!keyExpr) {
                keyExpr = columns[0].dataField;
            }

            $(function () {

                $("#" + popupID + "_gridContainer").dxDataGrid({
                    dataSource: (ds.length > 0 ? JSON.parse(ds) : ""),
                    columns: columns,
                    searchPanel: {
                        highlightCaseSensitive: false,
                        highlightSearchText: true,
                        placeholder: "Search...",
                        searchVisibleColumnsOnly: false,
                        text: serch,
                        visible: true,
                        width: 160
                    },
                    scrolling: {
                        mode: "virtual",
                        rowRenderingMode: "virtual"
                    },
                    paging: {
                        enabled: false
                    },
                    columnAutoWidth: true,
                    allowColumnResizing: true,
                    columnResizingMode: 'widget',
                    keyExpr: keyExpr,
                    focusedRowEnabled: true,
                    height: '100%',
                    onRowDblClick: function (selectedItems) {

                        if (searchFunc instanceof Function) {
                            searchFunc(selectedItems.data);

                            var popup = $("#" + popupID + "Popup").dxPopup("instance");
                            popup.hide();
                        } else {
                            window[popupID + "RowDblClick"](selectedItems);
                        }
                    },
                    showBorders: true
                });

                //scrollView.dxScrollView({
                //    width: '100%',
                //    height: '100%'
                //});
            });

            return scrollView;
        }
    });

    // 2021-01-12 김상인 추가 >> 팝업 생성 시 검색창에 포커스
    $("#" + popupID + "Popup").dxPopup({
        onShown: function (e) {
            //$("#" + popupID + "Popup .dx-datagrid .dx-datagrid-search-panel").dxTextBox('instance').focus();  
            $("#" + popupID + "_gridContainer .dx-datagrid .dx-textbox ").dxTextBox("instance").focus();
        }
    })
}

// 팝업 생성 (코드헬프타입, 팝업 헤더, 그리드 키, 필터 텍스트, 그리드 로우 더블클릭 콜백 함수)
function CreatePopupWithFilter(codeHelpType, headerName, keyExpr, filterTxt, callbackFunc, width = 550, height = 750) {

    $.ajax({
        type: 'GET',
        url: '/Comm/GetCodeHelp',
        data: {
            codeHelpType: codeHelpType
        },
        success: function (result) {

            result = JSON.parse(result);

            var columns = getcolumns(result[0]);

            var ds = result[1];

            $("#SearchPopup").dxPopup({
                width: width,
                height: height,
                visible: false,
                showTitle: true,
                title: headerName,
                closeOnOutsideClick: false,
                onHidden: function () {
                    $("#Search_gridContainer").dxDataGrid("instance").clearFilter();
                },
                contentTemplate: function (container) {
                    var scrollView = $('<div id="Search_gridContainer" />');

                    if (!keyExpr) {
                        keyExpr = columns[0].dataField;
                    }

                    $(function () {
                        $("#Search_gridContainer").dxDataGrid({
                            dataSource: (ds.length > 0 ? JSON.parse(ds) : []),
                            columns: columns,
                            searchPanel: {
                                highlightCaseSensitive: false,
                                highlightSearchText: true,
                                searchVisibleColumnsOnly: true,
                                text: filterTxt,
                                visible: true,
                                width: 160
                            },
                            scrolling: {
                                mode: "virtual",
                                rowRenderingMode: "virtual"
                            },
                            paging: {
                                enabled: false
                            },
                            columnAutoWidth: true,
                            allowColumnResizing: true,
                            columnResizingMode: 'widget',
                            keyExpr: keyExpr,
                            focusedRowEnabled: true,
                            height: '100%',
                            onRowDblClick: function (selectedItems) {
                                callbackFunc(selectedItems.data);
                                $("#SearchPopup").dxPopup("instance").hide();
                            },
                            showBorders: true
                        });
                    });
                    return scrollView;
                }
            });

            $("#SearchPopup").dxPopup("instance").show();

        },
        error: function (result) {
            alert("팝업 생성 에러");
        }
    })
}

// 컬럼 정보 가져오는 함수
function getcolumns(data) {

    var tmpData = JSON.parse(data);

    var tmpCol = new Array();

    for (var i = 0; i < tmpData.length; i++) {

        if (tmpData[i].fvewing) {
            tmpCol.push(
                {
                    dataField: tmpData[i].clmname,
                    caption: tmpData[i].clmalias
                }
            );
        }
    }

    return tmpCol;
}


// 팝업 그리드 안에 필터 문자열 제거
function clearSearchPanel(popupID) {
    var innerGrid = $("#" + popupID + "_gridContainer").dxDataGrid("instance");
    innerGrid.clearFilter();
}

// 폼이 수정 되었을때 실행하기위한 (onChacge) 함수
function formEdited(divName) {
    longTabs.find(o => o.id === divName).colored = true;

    $("#" + divName + "_Tab").parent().parent().addClass("edited");
}

// 폼을 submit, 입력 취소 하였을때 실행하기 위한 함수
function complete(divName) {

    longTabs.find(o => o.id === divName).colored = false;

    $("#" + divName + "_Tab").parent().parent().removeClass("edited");
}

// 탭의 DataSource
var longTabs = [
    { text: "Clear", id: "Clear" },
    { text: "Main", id: "Main" }
];

// 탭을 그리는 함수
$(function () {
    $("#tabsContainer").dxTabs({
        dataSource: longTabs,
        onItemClick: TabClick,
        scrollByContent: true,
        showNavButtons: true,
        itemTemplate: function (itemData, itemIndex, itemElement) {
            if (itemIndex > 1) {
                itemElement.append('<span>' + itemData.text + '</span>'
                    + '<button id="' + itemData.id + '_Tab" type="button" class="close closeTab" aria-label="Close">'
                    + '<span aria-hidden="true">&times;</span></button>');

                if (itemData.colored === true) {
                    $(itemElement.prevObject).addClass("edited")
                }

            } else {
                itemElement.append("<span>" + itemData.text + "</span>");
            }

        }
    });
        
});

// 트리메뉴 그리는 함수
function mapData(item) {

    return item;
}

// 탭 클릭
function TabClick(e) {
    var viewName = e.itemData.text;
    var tab_index = longTabs.findIndex(x => x.text == viewName);

    $("#tabsContainer").dxTabs("instance").option("selectedIndex", tab_index);

    $("#FocusedName").children().html(
        "<span class='dx-button-text'>" + viewName + "</span>"); removeProgramSession

    var length = longTabs.length;

    // 모두 닫기 버튼 클릭
    if (tab_index == 0) {

        var count = 3;

        for (var i = 2; i < length; i++) {

            //console.log(i - 1 + "번 : " + $(".dx-tabs-wrapper :nth-child(" + count + ")").children().children("span").text());

            if ($(".dx-tabs-wrapper :nth-child(" + count + ")").hasClass("edited") == true) {
                var result = DevExpress.ui.dialog.confirm("<i>'" + $(".dx-tabs-wrapper :nth-child(" + count + ")").children().children("span").text() + "' 탭에 저장되지 않은 변경사항이 있습니다. 닫으시겠습니까?</i>", "탭 종료");
                result.done(function (dialogResult) {

                    if (dialogResult) {
                        $(".dx-tabs-wrapper :nth-child(" + count + ")").remove();

                        $("#" + longTabs[count - 1].id).remove();
                        $("#" + longTabs[count - 1].id + "Js").remove();
                        removeProgramSession(longTabs[count - 1].id);

                        longTabs.splice(count - 1, 1);
                    } else {
                        $("#contentDiv > div").addClass("display-none");

                        $("#" + longTabs[count - 1].id).removeClass("display-none");

                        count++;
                    }

                });

            } else {
                $(".dx-tabs-wrapper :nth-child(" + count + ")").remove();
                $("#" + longTabs[count - 1].id).remove();
                $("#" + longTabs[count - 1].id + "Js").remove();
                UtilView.intervalUtil.intervalSetDelete(longTabs[count - 1].id);

                removeProgramSession(longTabs[count - 1].id);

                longTabs.splice(count - 1, 1);
            }

        }

        $("#Main").removeClass("display-none");
        $("#MainGraphContainer").dxChart("instance").render(); //월별 생산 추이 차트가 줄어들어 다시 그려줌(2020.12.15 박가희 추가)


        return;
    }

    $("#contentDiv > div").addClass("display-none");

    $("#" + e.itemData.id).removeClass("display-none");

    // 각 화면 중 menuId + "_IntervalSet" 변수가 있을 경우(새고로침을 사용하는 경우)
    UtilView.intervalUtil.intervalRestart(e.event.target.outerText);

}

function showMode_valueChanged(data) {
    var drawer = $("#drawer").dxDrawer("instance");
    drawer.option("openedStateMode", data.value);

    if (data.value === 'push') {
        $("#revealMode").hide();
    } else {
        $("#revealMode").show();
    }
}

// 사이드메뉴 열고 닫기
function openButton_click() {
    var drawer = $("#drawer").dxDrawer("instance");
    drawer.toggle();
}

// 홈버튼 클릭
function homeButton_click() {

    $("#test").text("Main");

    $("#contentDiv > div").addClass("display-none");

    $("#Main").removeClass("display-none");

}

// 로그인버튼 클릭
function loginButton_click() {

    if (!document.getElementById("login-form")) {
        $.ajax({
            type: 'GET',
            url: '/Comm/LoginForm',
            success: function (result) {
                $('#contentDiv').append(result);

                $("#contentDiv > div").addClass("display-none");

                $("#login-form").removeClass("display-none");
            }
        });
    } else {
        $("#contentDiv > div").addClass("display-none");

        $("#login-form").removeClass("display-none");
    }
}

// 트리메뉴 클릭
function treeViewItemClick(e) {

    if (!sessionStorage.getItem('loginID')) {
        alert("로그인 후 이용해 주세요.");
        location.replace("/Comm/Login");
    }

    var item = e.itemData;

    var tabName = item.Child_nm;
    var tabId = item.Child_cd;
    var interface = item.Interface;

    if (e.node.expanded) {
        e.component.collapseItem(e.node.key);
    } else {
        e.component.expandItem(e.node.key);
    }

    if (item.ord1 == 9999) {

        $("#FocusedName").children().html(
            "<span class='dx-button-text'>" + tabName + "</span>");

        $("div").removeClass("tab-item-active");
        $("div").removeClass("dx-state-selected");

        $(event.target).closest('li').children().addClass("dx-state-selected");

        //var viewName = e.itemData.Child_nm;
        var tab_index = longTabs.findIndex(x => x.text == tabName);

        if (tab_index > - 1) {
            $("#tabsContainer").dxTabs("instance").option("selectedIndex", tab_index);

            $("#contentDiv > div").addClass("display-none");

            $("#" + tabId).removeClass("display-none");


        } else {

            //#region 변경 - 신규 탭 생성시 맨 앞으로 생성
            longTabs.splice(2, 0, { text: tabName, id: tabId });
            $("#tabsContainer").dxTabs("instance").option("items", longTabs);
            $("#tabsContainer").dxTabs("instance").option("selectedIndex", 2);

            //$("#tabsContainer").dxTabs("instance").option("onContentReady", function () {
            //    UtilView.screenResize();console.log('onItemRendered') });

            $.ajax({
                type: 'POST',
                url: '/Comm/SetMenu',
                data: {
                    cd: tabId,
                    url: item.Child_url
                }
            }).done((...args) => {
                const result = args[0];

                $('#contentDiv').append(result);

                $("#contentDiv > div").addClass("display-none");

                $("#" + tabId).removeClass("display-none");

                searchInputEnter(tabId);

                // 화면 공통 스크립트
                // 조형진, 2020-11-09
                //UtilView.screenResize();
                (() => {
                    var menuId = longTabs[2].id;
                    //console.log("menuId :" + menuId);
                    // 1. Screen Resize                        
                    UtilView.screenResize(menuId);
                    // 2. form element star 표기(required속성)
                    UtilView.setformRequiredStar(menuId);

                    var grid = "";
                    try {
                        grid = $("#" + tabId + "Grid").dxDataGrid("instance");
                    }
                    catch (e) {
                        grid = $("#" + tabId + "Grid").dxTreeList("instance");
                    }

                    if (!grid) return;

                    //인터페이스 여부에 따라 그리드에 interface열 추가/제거
                    if (interface == "1") {
                        grid.columnOption("Data_root", "visible", true);
                    }
                    else {
                        grid.columnOption("Data_root", "visible", false);
                    }

                })();
            });
            //#endregion

            //#region 기존 - 신규 탭 생성시 맨 뒤로 생성
            //longTabs.push({ text: tabName, id: tabId });
            //$("#tabsContainer").dxTabs("instance").option("items", longTabs);
            //$("#tabsContainer").dxTabs("instance").option("selectedIndex", longTabs.length - 1);

            ////$("#tabsContainer").dxTabs("instance").option("onContentReady", function () {
            ////    UtilView.screenResize();console.log('onItemRendered') });


            //$.ajax({
            //    type: 'POST',
            //    url: '/Comm/SetMenu',
            //    data: {
            //        cd: tabId,
            //        url: item.Child_url
            //    },
            //    async:false,
            //    success: function (result) {

            //        $('#contentDiv').append(result);

            //        $("#contentDiv > div").addClass("display-none");

            //        $("#" + tabId).removeClass("display-none");

            //        searchInputEnter(tabId);

            //        // 화면 공통 스크립트
            //        // 조형진, 2020-11-09
            //        //UtilView.screenResize();
            //        (() => {
            //            var menuId = longTabs[longTabs.length - 1].id;
            //            //console.log("menuId :" + menuId);
            //            // 1. Screen Resize                        
            //            UtilView.screenResize(menuId);
            //            // 2. form element star 표기(required속성)
            //            UtilView.setformRequiredStar(menuId);

            //            var grid = "";
            //            try {
            //                grid = $("#" + tabId + "Grid").dxDataGrid("instance");
            //            }
            //            catch (e) {
            //                grid = $("#" + tabId + "Grid").dxTreeList("instance");
            //            }

            //            if (!grid) return;

            //            //인터페이스 여부에 따라 그리드에 interface열 추가/제거
            //            if (interface == "1") {
            //                grid.columnOption("Data_root", "visible", true);
            //            }
            //            else {
            //                grid.columnOption("Data_root", "visible", false);
            //            }

            //        })();
            //    }
            //});
            //#endregion
        }
    }
}

function searchInputEnter(programId) {
    // searchPopupInput 클래스를 가지고 있는 인풋(조회용 팝업 버튼이 있는)에 keydown 이벤트 추가
    // 이주용, 21.05.20
    let programDiv = document.getElementById(programId); // 새로 만드는 메뉴 div 아래에 있는 엘레먼트에서만 적용하기 위함

    for (var i = 0; i < programDiv.getElementsByClassName("searchPopupInput").length; i++) {
        var tmpElement = programDiv.getElementsByClassName("searchPopupInput")[i];
        tmpElement.addEventListener("keydown", function (event) {
            if (event.keyCode === 13) {
                // 폼아래 인풋이 하나일경우 엔터키 누를시 섭밋되는 현상 방지
                event.preventDefault();
                // 부모노드 아래 버튼 태그 클릭이벤트 발생시킴
                event.target.parentNode.getElementsByTagName("button")[0].click();
            }
        })
    }
}

// 헤더 우측 드랍다운 메뉴 클릭
function downloadButton_click(e) {

    if (e.itemData.cd === "logout") {

        for (var i = 2; i < longTabs.length; i++) {
            removeProgramSession(longTabs[i].id);
        }


        if (document.getElementById(e.itemData.id)) {

            $("#contentDiv > div").addClass("display-none");
            $("#" + e.itemData.id).removeClass("display-none");
            return;
        }

        $.ajax({
            type: 'POST',
            url: e.itemData.url,
            success: function (result) {

                if (JSON.parse(result).sessionLoss) {
                    alert("로그아웃 되었습니다.");
                    sessionStorage.clear();
                    location.replace("/Comm/Login");
                }

            }
        });
    } else if (e.itemData.cd === "My Page") {

        $.ajax({
            type: 'GET',
            url: e.itemData.url
        }).done(function (response) {

            var popup = $("#MyPagePopup").dxPopup('instance');

            popup.option("contentTemplate", function (content) {
                content.append(response);
            });

            popup.show();

        }).fail(function (data) {
            alert("Failed: " + data.response);
        });
    }
}

// json 이스케이프문 처리
function jsonEscape(str) {

    var result = str.replace(/\\n/g, '\\n')
        .replace(/\\'/g, "\\'")
        .replace(/\\"./g, '\\"')
        .replace(/\\&/g, "\\&")
        .replace(/\\r/g, "\\r")
        .replace(/\\t/g, "")
        .replace(/\\b/g, "\\b")
        .replace(/\\f/g, "\\f");

    //result = result.replace(/[\u000-\u0019]+/g, "");

    return JSON.stringify(result);
}

function menutab(tabId, divId, tabNum) {
    $("#" + divId + " > div").addClass("display-none");
    $("#" + tabId + " a").removeClass("active");

    $("#" + divId + "_" + tabNum).removeClass("display-none");
    $("#" + tabId + " li:nth-child(" + tabNum + ")").children('a').addClass("active");
}


// 그리드에 포커스 된 로우의 키를 통해서 해당 데이터를 가져온다.
function getGridRowByKey(gridID, key) {
    let grid = $("#" + gridID).dxDataGrid('instance');
    let rows = grid.getVisibleRows();
    let rowIndex = grid.getRowIndexByKey(key);
    if (rowIndex == -1) {
        return null;
    }
    let row = rows[rowIndex];

    return row.data;
}

// form 필수값 체크
function validationCk(formID, cols) {

    // 자리수 체크가 필요하다면, 파라미터 cols 객체에 maxlen, minlen 을 속성으로 받아서, valiation 처리하시면 됩니다.

    for (var i = 0; i < cols.length; i++) {

        //console.log(i);
        var formEl = $("#" + formID + " " + "[name=" + cols[i].name + "]");
        var formElVal = $(formEl).val();
        var formElType = $(formEl).attr('type');

        if (cols[i].type === "radio") {
            if (!$("#" + formID + " input:radio[name=" + cols[i].name + "]").is(':checked')) {
                $(formEl)[0].focus();
                alert(cols[i].text + "은(는) 필수입력 사항입니다.");
                return false;
            }
        } else if (UtilView.isEmpty(formElVal)) {
            $(formEl).focus();
            // 필요시, element type별 action 지정
            switch (formElType) {
                case "number":
                    $(formEl).val(0);
                    break;
                default:
            }
            alert(cols[i].text + "은(는) 필수입력 사항입니다.");
            return false;
        }
    }
    return true;
}

/**
 * form 필수값 체크(개별체크)
 * @param {any} formId : formID (#제외)
 * @param {any} objCol : 폼엘리먼트 (object or string:폼의 단일엘리먼트일때)
 * @param {any} mesgFlag  : validation 체크 실패시 경고창출력 여부 (true, false)
 * @param {any} alertMesg : validation 체크 실패시 경고창 메시지 (mesgFlag가 true일때 적용)
 */
function validationElementCk(formId, objCol, mesgFlag, alertMesg) {

    // objcol이 문자열이라면, objCol 객체를 만들어준다.(단일, 함수사용시 필요함)
    if (typeof objCol == "string") {
        var element = $('[name=' + objCol + ']', $('#' + formId));
        var elName = element.attr("name");
        var elTagName = element.prop("tagName");
        var elType = element.attr("type");

        var type = (elType == "radio") ? elType : elTagName.toLowerCase();
        objCol = { "name": elName, "type": type };
    }

    var formEl = $("#" + formId + " " + "[name=" + objCol.name + "]");
    var formElVal = $(formEl).val();
    var formElType = $(formEl).attr('type');

    if (objCol.type === "radio") {
        if (!$("#" + formId + " input:radio[name=" + objCol.name + "]").is(':checked')) {
            $(formEl)[0].focus();
            if (mesgFlag) alert(alertMesg);
            return false;
        }
    } else if (UtilView.isEmpty(formElVal)) {
        $(formEl).focus();
        // 필요시, element type별 action 지정
        switch (formElType) {
            case "number":
                $(formEl).val(0);
                break;
            default:
        }
        if (mesgFlag) alert(alertMesg);
        return false;
    }
    return true;
}

// 이미지 쿼리스트링 생성
function updateQueryStringParameter(uri, key, value) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
        return uri + separator + key + "=" + value;
    }
}

// 트리리스트 포커스된 로우 데이터 리턴
function treeListGetFocusedRowData(gridID) {

    var treeList = $("#" + gridID).dxTreeList("instance");

    var node = treeList.getNodeByKey(treeList.option("focusedRowKey"));

    return node.data;
}

// 메뉴안에서 다른 페이지 열기 
function openOtherPage(pageInfo) {

    var tabName = pageInfo.tabName;
    var tabId = pageInfo.tabId; 
    var interface = pageInfo.interface;

    $("#FocusedName").children().html(
        "<span class='dx-button-text'>" + tabName + "</span>");

    $("div").removeClass("tab-item-active");
    $("div").removeClass("dx-state-selected");

    $(event.target).closest('li').children().addClass("dx-state-selected");

    var tab_index = longTabs.findIndex(x => x.text == tabName);

    if (tab_index > - 1) {

        $("#" + tabId).remove();
        $("#" + tabId + "Js").remove();

        $("#tabsContainer").dxTabs("instance").option("selectedIndex", tab_index);

        //$("#contentDiv > div").addClass("display-none");

        //$("#" + tabId).removeClass("display-none");
    } else {

        //#region 기존 - 신규 탭 생성시 맨 뒤로 생성
        //longTabs.push({ text: tabName, id: tabId });
        //$("#tabsContainer").dxTabs("instance").option("items", longTabs);
        //$("#tabsContainer").dxTabs("instance").option("selectedIndex", longTabs.length - 1);
        //#endregion

        //#region 변경 - 신규 탭 생성시 맨 앞으로 생성
        longTabs.splice(2, 0, { text: tabName, id: tabId });
        $("#tabsContainer").dxTabs("instance").option("items", longTabs);
        $("#tabsContainer").dxTabs("instance").option("selectedIndex", 2);
        //#endregion
    }

    var querystringParam = htmlUtils.jsonToQS(pageInfo.param);

    $.ajax({
        type: 'POST',
        url: '/Comm/SetMenu',
        data: {
            cd: tabId,
            url: pageInfo.url + "?" + querystringParam
        },
        async: false,
        success: function (result) {

            $('#contentDiv').append(result);

            $("#contentDiv > div").addClass("display-none");

            $("#" + tabId).removeClass("display-none");

            searchInputEnter(tabId);

            // 화면 공통 스크립트
            // 조형진, 2020-11-09
            //UtilView.screenResize();
            (() => {
                var menuId = longTabs[longTabs.length - 1].id;
                //console.log("menuId :" + menuId);
                // 1. Screen Resize                        
                UtilView.screenResize(menuId);
                // 2. form element star 표기(required속성)
                UtilView.setformRequiredStar(menuId);

                var grid = "";
                try {
                    grid = $("#" + tabId + "Grid").dxDataGrid("instance");
                }
                catch (e) {
                    grid = $("#" + tabId + "Grid").dxTreeList("instance");
                }

                if (!grid) return;

                //인터페이스 여부에 따라 그리드에 interface열 추가/제거
                if (interface == "1") {
                    grid.columnOption("Data_root", "visible", true);
                }
                else {
                    grid.columnOption("Data_root", "visible", false);
                }

            })();
        }
    });
}


// json To QueryString --- START ----------------------------------------------------------------
var htmlUtils = {
    encodeURI: function (contents, encoding) {
        var encoding = typeof encoding == 'undefined' || encoding == "" ? "UTF-8" : encoding;
        return encodeURIComponent(contents, encoding);
    },
    encodingCheck: function (contents) {
        var regCheck = /^[A-Za-z0-9]$/;
        return regCheck.test(contents);
    },
    tempQS: "",
    jsonToQS: function (param) {
        htmlUtils.tempQS = "";
        jsonToQSTemp(param);
        return htmlUtils.tempQS;
    }
}

function jsonToQSTemp(param, keyString) {
    if (typeof param == "object") {
        if (Array.isArray(param)) {
            for (var i = 0; i < param.length; i++) {
                if (typeof param[i] == "object") {
                    jsonToQSTemp(param[i], keyString + "[" + i + "]");
                }
                else {
                    if (typeof keyString != 'undefined' && keyString != "") {
                        jsonToQSTemp(param[i], keyString + "[" + i + "]");
                    }
                }
            }
        }
        else {
            for (var key in param) {
                if (typeof param[key] == "object") {
                    if (Array.isArray(param[key])) {
                        jsonToQSTemp(param[key], key);
                    }
                    else {
                        jsonToQSTemp(param[key]);
                    }
                }
                else {
                    if (typeof keyString != 'undefined' && keyString != "") {
                        jsonToQSTemp(param[key], keyString + "." + key);
                    }
                    else {
                        jsonToQSTemp(param[key], key);
                    }
                }
            }
        }
    }
    else {
        if (htmlUtils.tempQS != "") {
            htmlUtils.tempQS += "&"
        }
        if (!htmlUtils.encodingCheck(param)) {
            param = htmlUtils.encodeURI(param);
        }
        htmlUtils.tempQS += keyString + "=" + param;
    }
}
// json To QueryString --- END ------------------------------------------------------

// 그리드 툴바에서 저장, 엑셀 추출 버튼 숨기기
function HideToolbarButton(e) {
    var toolbarItems = e.toolbarOptions.items;
    $.each(toolbarItems, function (_, item) {

        if (item.name == "saveButton" || item.name == "exportButton") {
            item.visible = false;
        }
    });
}

// DropDownBox 닫을 때, DataSet 재조회
function DropDownBoxReload(e) {
    var DropDownId = e.component._$element[0].id;
    $("#" + DropDownId).dxDropDownBox("instance").repaint();
}

// 그리드 엑셀 추출
function gridExportToExcel(gridName, ExcelName) {

    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet("SheetName");

    DevExpress.excelExporter.exportDataGrid({
        component: $("#" + gridName).dxDataGrid("instance"),
        worksheet: worksheet
    }).then(function () {
        workbook.xlsx.writeBuffer().then(function (buffer) {
            saveAs(new Blob([buffer], { type: "application/octet-stream" }), ExcelName + ".xlsx");
        });
    });
}

//트리리스트 엑셀 추출
function treeExportToExcel(treeName, ExcelName) {
    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet("SheetName");

    var tv = $("#" + treeName).dxTreeList("instance");
    var columns = tv.getVisibleColumns();
    var data = tv.getVisibleRows();

    var xlsColumns = new Array();

    for (i = 0; i < columns.length; i++) {
        var column = {};
        column.header = columns[i].caption;
        column.key = columns[i].dataField;
        column.width = 30;
        xlsColumns.push(column);
    }
    worksheet.columns = xlsColumns;

    for (i = 0; i < data.length; i++) {
        worksheet.addRow(data[i].data);
    }

    workbook.xlsx.writeBuffer().then(function (buffer) {
        saveAs(new Blob([buffer], { type: "application/octet-stream" }), ExcelName + ".xlsx");
    });
}

function cellWithButton(container, cellInfo) {

    $("<div>").html("<div style='float:left;'>" + (cellInfo.value ? cellInfo.value : "") + "</div><div style='float:right;' id='icon-plus" + (cellInfo.rowIndex + 1) + "'></div>")
        .appendTo(container);

    $("#icon-plus" + (cellInfo.rowIndex + 1)).dxButton({
        icon: "search",
        onClick: function (e) {

        }
    });
}

/*▶▶▶▶▶▶▶▶▶▶▶▶ 공통 Util START ▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶*/
// date : 2020-09-17
// write : 조형진
// comment : view에서 자주 사용되는 Util script
// 사용법 : UtilView.isEmpty(paramValue);
//---------------------------------------------------------------------------------
// history
// Date         |   작업사항
//---------------------------------------------------------------------------------
// 2020-11-04   | page laoyout 네임스페이스 충돌이슈가 있어, UtilView의 객체속성과 관련된 부분 모두 제거
// 2020-11-05   | 객체 활성/비활성 자동처리  (toolbar영역, 그리드영역, form영역) 
//                - setActiveElement, setActiveElement~로 시작하는 함수
//------------------------------------------------------------------------------------
var UtilView = {
    //------------------
    // 필드정의
    //------------------
    // 페이지 공통속성
    //m_controller: "",       //controller명    
    //m_actionPrefix: "",      //action prefix명

    //------------------
    // Method 정의
    //------------------
    /*********************************************************************************
    * 기본  함수 모음
    *********************************************************************************/
    // 메시지구분 생성
    /**
     * ◈ 메시지(alert, console) 앞에붙는 메시지 타이이틀을 리턴하다.
     * @param {any} type     
     */
    mesgTitle: function (type) {
        var retString;
        var aryCurMenu, pid, ctrlname;

        // 현재 매뉴정보 설정
        aryCurMenu = UtilView.getCurMenuInfo("all");
        pid = aryCurMenu[0]
        ctrlname = aryCurMenu[1];

        switch (type) {
            case "VIEW_NAME":
                retString = !UtilView.isEmpty(ctrlname) ? ctrlname : "컨트롤미지정";
                retString += ".";
                retString += !UtilView.isEmpty(pid) ? pid : "action미지정";
                break;
            case "":
                retString = "";
                break;
            default:
        }
        return retString;
    },

    /**
     * ◈ 공통 aelrt 메시지 함수
     * @param {string} mesg : 출력 메시지
     */
    // alert 공통
    alertMesg: function (mesg) {
        var mesgGbn = UtilView.mesgTitle("VIEW_NAME");
        alert("[" + mesgGbn + "] => " + mesg);
    },

    /**
     * ◈ 공통 console.log 메시지 함수
     * @param {any} mesg : 출력 메시지
     */
    consoleLog: function (mesg) {
        var mesgGbn = UtilView.mesgTitle("VIEW_NAME");
        console.log("[" + mesgGbn + "] => " + mesg);
    },

    /**
     * ◈ empty 체크 공통
     * @param {any} value : empty 체크대상
     */
    isEmpty: function (value) {
        // 2021-01-19 nullcheck 체크문장 수정
        if (value == "" || value == null || value == undefined) {
            return true;
            //(value != null && typeof value == "object" && !Object.keys(value).length)) {
            //(value != null && typeof value == "object")) {            
        }
        if (Array.isArray(value) && value.length < 1) {
            return true;
        }
        if (typeof value === 'object' && value.constructor.name === 'Object' && Object.keys(value).length < 1 && Object.getOwnPropertyNames(value) < 1) {
            return true;
        }
        if (typeof value === 'object' && value.constructor.name === 'String' && Object.keys(value).length < 1) {
            return true;
        }

        return false;
    },

    /**
    * ◈ Actioln Url 을 구한다 (controller, action, actionType을 매개로..)
    * @param {string} actType : action type 지정(pagePrefix 다음의 구분문자열 지정) : Select/TRX/Save 등등
    */
    getActionUrl: function (actType) {
        var retUrl, actType;
        var aryCurMenu, pid, ctrlname
        // 현재 매뉴정보 설정
        aryCurMenu = UtilView.getCurMenuInfo("all");
        pid = aryCurMenu[0]
        ctrlname = aryCurMenu[1];

        if (UtilView.isEmpty(ctrlname)) {
            UtilView.alertMesg("controller 지정안됨")
            return null;
        }
        if (UtilView.isEmpty(pid)) {
            UtilView.alertMesg("action 지정안됨")
            return null;
        }
        actType = !UtilView.isEmpty(actType) ? actType : "";
        retUrl = "/" + ctrlname + "/" + pid + actType;
        return retUrl;
    },

    /**
     * ◈ form 의 내용을 자동으로 validation 체크한다. validation 실패시 false, 성공시 true를 리턴
     *  실제, validation 체크는 validationCk() 함수 에서 진행되며, 로직수정시, 해당함수를 수정하면 된다.
     * @param {string} formWriteId : 등록/수정 대상 form ID     
     */
    checkValidForm: function (formWriteId) {
        // 폼요소 적용 tag 리스트.
        var formTags = ["INPUT", "SELECT", "TEXTAREA"];

        var emptyLabel = "라벨명 없음";
        var cols = [];
        var colObj = {};

        $.each($("#" + formWriteId + " .required"), function (index, obj) {
            var elName = $(this).attr("name");
            var elTagName = $(this).prop("tagName");
            var elType = $(this).attr("type");

            var labelText = ($(this).prev().prop("tagName") == "LABEL" && $(this).prev().text())
                || ($(this).parent().prev().prop("tagName") == "LABEL" && $(this).parent().prev().text())
                || emptyLabel;

            // form element 인경우만 Valdation 적용
            var type;
            if (formTags.indexOf(elTagName) > -1) {
                //console.log("name : " + elName + ", tagName : " + elTagName + ", elType :" + elType+ ", labelText" + labelText);                
                type = (elType == "radio") ? elType : elTagName.toLowerCase();
                colObj = { "name": elName, "text": labelText, "type": type }
                cols.push(colObj);
            }
            /*
            console.log("targetName : " + $(this).attr("name")
                + ", targetTagName : "  + $(this).prop("tagName")
                + ", targetType : "     + $(this).attr("type"));            
            */
        });
        //console.log(cols);        

        var isValid = validationCk(formWriteId, cols);
        return isValid;
    },


    /**
     * ◈ 화면영역(공통툴바영역(단수), grid영역(복수), 등록폼영역(단수) )에 있는 element 활성/비활성
     *   => 각영역별 별도 구현시, 관련함수 호출하면됨(setActiveElement~)
     * 
     * @param {any} isEdit      : 수정모드(true : 수정모드, false : 읽기모드)
     * @param {any} status      : 등록폼 모드 설정(N:변경사항없음,I:insert, U:update, D:delete), trasaction이 필요한경우 설정 필수임
     * @param {any} toolBarId   : 화면영역 > 공통 toobar의 ID
     * @param {any} gridIds     : 화면영역 > gridID (복수개일때, 세미콜론(;)으로 구분)
     * @param {any} writeFormId : 화면영역 > 등록폼ID 
     * @param {any} notEditFormElNames : 수정모드(update) 인경우, 수정불가필드(복수개일때, 세미콜론(;)으로 구분) 
     *                                    => pk필드와 유사. 또는 업무적으로 수정하면 안되는 필드
     */
    setActiveElement: function (isEdit, status, toolBarId, gridIds, writeFormId, notEditFormElNames) {
        // 프로그램ID
        var programId = UtilView.getCurMenuInfo("pid");

        /****************************************************************
        // 1. 툴바 활성/비활성
        ******************************************************************/
        UtilView.setActiveElementToolbar(isEdit, toolBarId, programId);

        /****************************************************************
        // 2. 그리드 활성/비활성
        ******************************************************************/
        UtilView.setActiveElementGrids(isEdit, gridIds);

        /****************************************************************
        // 3. 폼 활성/비활성
        ******************************************************************/
        UtilView.setActiveElementFormId(isEdit, status, writeFormId, notEditFormElNames);
    },

    /**
     * ◈ 화면영역(공통툴바영역(복수)) 에 있는 element 활성/비활성
     * @param {any} isEdit    : 수정모드(true : 수정모드, false : 읽기모드)
     * @param {any} toolBarId : 공통 ToolbarId
     * @param {any} programId : 프로그램 ID     
     */
    setActiveElementToolbar: function (isEdit, toolBarId, programId) {
        // 수정모드 : Save,Undo : removeclass
        // 읽기모드 : Save,Undo : addclass
        var toolbarTgtBtns = ["Save", "Undo"];
        var toolbarTgtClass = "display-none";

        var toolbarBaseEl = $('#' + toolBarId + ' > .dx-toolbar-items-container > .dx-toolbar-after > .dx-item');
        $(toolbarBaseEl).each(function (index, item) {
            var btnId = $('.dx-item-content > .dx-button', item).attr('id');
            var btnGbn = btnId.replace(programId, '');
            //console.log(btnId + " => " + btnGbn);
            // 수정모드
            if (isEdit) {
                if (toolbarTgtBtns.indexOf(btnGbn) > -1) {
                    $(item).removeClass(toolbarTgtClass);
                }
                else {
                    $(item).addClass(toolbarTgtClass);
                }
            }
            else {
                // 읽기모드
                if (toolbarTgtBtns.indexOf(btnGbn) > -1) {
                    $(item).addClass(toolbarTgtClass);
                }
                else {
                    $(item).removeClass(toolbarTgtClass);
                }
            }
        });
    },

    /**
     * ◈ 화면영역(grid영역(복수)) 에 있는 element 활성/비활성
     * @param {any} isEdit : 수정모드(true : 수정모드, false : 읽기모드)
     * @param {any} gridIds : gridId (복수개일때, 세미콜론(;)으로 구분)
     */
    setActiveElementGrids: function (isEdit, gridIds) {
        //debugger;
        var tgtGrid = [];
        if (gridIds.indexOf(";") > -1) {
            gridIds.split(";").forEach((element, index) => { tgtGrid[index] = '#' + element; });
            //tgtGrid = aryGrid.join(",");
        }
        else {
            tgtGrid[0] = '#' + gridIds;
        }
        tgtGrid.forEach(element => { $(element).dxDataGrid("instance").option("disabled", isEdit); });
    },

    /**
     * ◈ 화면영역(grid영역(복수)) 에 있는 element 활성/비활성
     * @param {any} isDisabled : 그리드 비활성여부 (true : 비활성, false : 활성)
     * @param {any} objEditing : 그리드 editing object({allowUpdating: true, mode:'batch'} )
                })
     * @param {any} gridIds : gridId (복수개일때, 세미콜론(;)으로 구분)
     */
    setActiveElementGrids2: function (isDisabled, objEditing, gridIds) {
        //debugger;
        var tgtGrid = [];
        if (gridIds.indexOf(";") > -1) {
            gridIds.split(";").forEach((element, index) => { tgtGrid[index] = '#' + element; });
            //tgtGrid = aryGrid.join(",");
        }
        else {
            tgtGrid[0] = '#' + gridIds;
        }
        tgtGrid.forEach(
            element => {
                $(element).dxDataGrid("instance").option("disabled", isDisabled);
                $(element).dxDataGrid("instance").option("editing", objEditing);
            }
        );
    },

    /**
     * ◈ form element 활성/비활성
     * @param {any} isEdit        : 수정모드(true : 수정모드, false : 읽기모드)
     * @param {any} status        : 등록폼 모드 설정(N:변경사항없음,I:insert, U:update, D:delete), trasaction이 필요한경우 설정 필수임
     * @param {any} writeFormId   : 등록폼ID
     * @param {any} notEditFormElNames : 수정모드(update) 인경우, 수정불가필드(복수개일때, 세미콜론(;)으로 구분) => pk필드와 유사. 또는 업무적으로 수정하면 안되는 필드
     */
    setActiveElementFormId: function (isEdit, status, writeFormId, notEditFormElNames) {
        var tgtNotEditFormElNames = [];
        var objInutMode = { INPUT_TEXT: "INPUT_TEXT", INPUT_RADIO: "INPUT_RADIO", INPUT_CHECKBOX: "INPUT_CHECKBOX", SELECT: "SELECT", RESERVED_TEXT: "Reserved" }

        // 등록폼 모드설정
        if (status != null && status != undefined) {
            $('input[type=hidden][name=row_status]', $('#' + writeFormId)).val(status);
        }

        // 수정모드(update) 인경우, 수정불가필드 설정
        if (notEditFormElNames.indexOf(";") > -1) {
            notEditFormElNames.split(";").forEach((element, index) => { tgtNotEditFormElNames[index] = element; });
        }
        else {
            tgtNotEditFormElNames[0] = notEditFormElNames;
        }
        //console.log("tgtNotEditFormElNames :" + tgtNotEditFormElNames);
        // 모두 수정/읽기 모드 초기화
        $(":input", $('#' + writeFormId)).attr("disabled", !isEdit);
        $('select option', $('#' + writeFormId)).attr("disabled", !isEdit);
        //$(':radio n', $('#' + writeFormId)).attr("disabled", !isEdit);


        // 수정모드(update상태)시 update불가필드 비활성처리
        // : 비활성처리는, 기본전제는 disabled 가 아닌 readonly 임 => server 에서는 disabled를 인식못하니깐..
        $(":input", $('#' + writeFormId)).each(function (index, item) {

            if (isEdit && status == "U") {
                if (tgtNotEditFormElNames.indexOf($(item).attr("name")) > -1) {
                    // 입력모드 설정
                    var inputMode = "";
                    switch ($(item).prop('tagName')) {
                        case "INPUT":
                            inputMode = $(item).prop('tagName') + "_" + $(item).prop('type').toUpperCase();
                            break;
                        case "SELECT":
                            inputMode = objInutMode.SELECT;
                            break;
                        default:
                    }

                    //입력모드 따른 로직 분기처리
                    switch (inputMode) {
                        case objInutMode.INPUT_TEXT:
                            //1. readonly/disabled 설정
                            $(item).attr("readonly", true);
                            //2. 확장기능
                            // (1) textbox인경우, 하위에 버튼(예를들면, help팝업버튼..)이 있는경우 그것까지 비활성처리                    
                            $('button', $(item).parent()).attr('disabled', true);
                            break;
                        case objInutMode.INPUT_RADIO:
                        case objInutMode.INPUT_CHECKBOX:
                            //1. readonly/disabled 설정
                            // : RADIO, CHECKBOX 경우에, readonly 속성이 없어, 나머지를 disabled 처리
                            $(item).not(":checked").attr("disabled", true);
                            break;
                        case objInutMode.SELECT:
                            //1. readonly/disabled 설정
                            // : select 경우에, readonly속성이 없어, 나머지를 disabled 처리
                            $('option', item).not(":selected").attr("disabled", true);
                            break;
                        default:
                            //1. readonly/disabled 설정
                            $(item).attr("readonly", true);
                            break;
                    }
                }
            }
        });
    },

    /*********************************************************************************
    * DataGrid 관련 함수 모음    
    *********************************************************************************/
    /**
     * ◈ datagrid의 내용을 자동으로 form에 바인딩
     * @param {string} gridId : 대상 Grid의 ID
     * @param {string} formId : 타겟 폼의 ID
     */
    setDataGridFormBind: function (gridId, formId) {
        var grid = $("#" + gridId).dxDataGrid("instance");
        var data = getGridRowByKey(gridId, grid.option("focusedRowKey"));

        mappingJsonDataToForm(formId, data);
    },

    /**
     * ◈ DataGrid를 설정한다(Json데이터를 이용해서..), 이때 Json데이터가 없으면 DataGrid를 clear시킨다.
     * @param {string | string[]} tGridIds : 타겟 Grid ID(배열,NON배열 모두가능)
     * @param {string | string[]} result   : 타겟 Grid에 할당할 Json Result String
     * @param {object} gridOptions : grid instance 옵션 객체 (미설정시 default로 기본 option이 설정됨)
     */
    setDataGridFromJson: function (tGridIds, result, gridOptions) {

        var jsonResult;

        // 초기화영역
        $('#' + tGridIds).dxDataGrid("option", "dataSource", []);
        $('#' + tGridIds).dxDataGrid("option", "focusedRowKey", "");

        // data 존재여부에 따른 gridoption 디폴트설정
        gridOptionEmptyDef = { "focusedRowEnabled": false };        // empty
        gridOptionDef = { "focusedRowEnabled": true, "focusedRowIndex": 0 };  // no empty

        if (typeof tGridIds != typeof result) {
            var mesg = "parameter [tGridIds] 와 [result]의 데이터 타입 불일치 오류\n\n";
            mesg += "=>tGridIdx : " + typeof tGridIds + ", result:" + typeof result;
            UtilView.alertMesg(mesg);
        }

        // string 처리
        if (!$.isArray(tGridIds)) {
            // if Exist json result
            if (!UtilView.isJsonResultEmpty(result)) {
                gridOptions = UtilView.isEmpty(gridOptions) ? gridOptionDef : gridOptions
                jsonResult = JSON.parse(result);
                $('#' + tGridIds).dxDataGrid("option", "dataSource", jsonResult);
            }
            // instance option 설정
            for (var opt in gridOptions) {
                $('#' + tGridIds).dxDataGrid("option", opt, gridOptions[opt]);
            }
        }
        // string[] 처리
        else {
            $.each(tGridIds, function (index, grid) {
                // if Exist json result
                if (!UtilView.isJsonResultEmpty(result[index])) {
                    gridOptions = UtilView.isEmpty(gridOptions) ? gridOptionDef : gridOptions
                    // tGridIds가 배열이면, JsonResult도 배열이라고 본다.                    
                    jsonResult = JSON.parse(result[index]);
                    $('#' + grid).dxDataGrid("option", "dataSource", jsonResult[index]);
                }
                // instance option 설정
                for (var opt in gridOptions) {
                    $('#' + grid).dxDataGrid("instance").option(opt, gridOptions[opt]);
                }
            });
        }
    },

    /**
     * ◈ json Result결과가 Empty 인지 체크 (result가 배열인경우에는, result 배열 요소중 하나만 true라도 true를 리턴)
     * @param {string | string[]} result - Json Result데이터 String
     */
    isJsonResultEmpty: function (result) {
        var retVal;
        retVal = false;
        if ($.isArray(result)) {
            for (var i = 0; result.length; i++) {
                if (UtilView.isEmpty(result[i])) {
                    retVal = true;
                    break;
                }
            }
        } else {
            if (UtilView.isEmpty(result)) retVal = true;
        }
        return retVal
    },

    /**
     * ◈ 공통 DataGrid Select(form을이용한다)
     * @param {string} tGridId : 타겟 Grid ID
     * @param {string[]} srchFormId : 검색파라미터form ID
     * @param {object} gridOptions : Grid 옵션배열
     * @param {object} callback : callback function
     */
    dataGridSelect: function (tGridId, srchFormId, gridOptions, callback) {
        var form = $('#' + srchFormId)[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            url: UtilView.getActionUrl('Select'),
            contentType: false,
            processData: false,
            data: formData,
            success: function (result) {

                // data바인딩
                UtilView.setDataGridFromJson(tGridId, result, gridOptions);
                // callback
                if (!UtilView.isEmpty(callback)) callback(result);
            },
            error: function (request, status, error) {
                alert("code = " + request.status + " message = " + request.responseText + " error = " + error); // 실패 시 처리
            }
        });
    },

    /**
     * ◈ 공통 DataGrid Select(form을이용한다 with actType 추가)
     * @param {string} tGridId : 타겟 Grid ID
     * @param {string[]} srchFormId : 검색파라미터form ID
     * @param {string} actType : action타입
     * @param {object} gridOptions : Grid 옵션배열
     * @param {object} callback : callback function
     */
    dataGridSelect2: function (tGridId, srchFormId, actType, gridOptions, callback) {
        var form = $('#' + srchFormId)[0];
        var formData = new FormData(form);

        $.ajax({
            type: 'POST',
            url: UtilView.getActionUrl(actType),
            contentType: false,
            processData: false,
            data: formData,
            success: function (result) {
                // data바인딩
                UtilView.setDataGridFromJson(tGridId, result, gridOptions);
                // callback
                if (!UtilView.isEmpty(callback)) callback(result);
            }
        });
    },

    /**
     * ◈ 공통 DataGrid Simple Select(string단위로 ajax호출)
     * @param {string} tGridId : 타겟 Grid ID
     * @param {object} data : 전송할파라미터
     * @param {string} actType : action타입
     * @param {object} gridOptions : Grid 옵션배열
     * @param {object} callback : callback function
     */
    dataGridSimpleSelect: function (tGridId, data, actType, gridOptions, callback) {
        $.ajax({
            type: 'POST',
            url: UtilView.getActionUrl(actType),
            data: data,
            success: function (result) {
                UtilView.setDataGridFromJson(tGridId, result, gridOptions);
                // callback                
                if (!UtilView.isEmpty(callback)) callback();
            }
        })
    },

    /**
     * ◈ 공통 DataGrid TRX(form을이용, data 입력/수정/삭제 처리)
     * @param {object} formData : 전송 forData 콜렉션     
     * @param {object} callback : callback function
     */
    dataGridTRX: function (formData, callback) {
        // please add form parameter When need to form parameter
        //formData.append("edate", $('input[name=edate]',$('#'+formSearchId)).val());
        // formData 확인
        //for (var key of formData.keys()) { console.log("[" + PagePrefix + "]=> " + key + " : " + formData.get(key)); }
        $.ajax({
            type: 'POST',
            url: UtilView.getActionUrl('TRX'),
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                alert(result);
                if (!UtilView.isEmpty(callback)) callback();
            }
        });
    },

    /**
     * ◈ 공통 DataGrid TRX(form을이용, data 입력/수정/삭제 처리),
     *    예를 들어, json 데이터등의 추가적인 전송이 필요하면, 
     *    키를 추가하고 값에 json데이터를 직렬화(stringify) 하여, 이 함수를 호출한다.
     *    참고로, 서버사이드에서는 jsonbinder를 이용, json데이터를 받는다.
     * @param {object} formData : 전송 forData 콜렉션     
     * @param {object} appendData : formData에 추가적으로 덧붙여 보낼데이터(object형식), backend의 jsonbinder를 이용시 stringnify() 직렬화해야함
     * @param {object} callback : callback function     
     */
    dataGridTRX2: function (formData, appendData, callback) {
        // please add form parameter When need to form parameter
        if (typeof appendData == "object") {
            for (var key in appendData) {
                formData.append(key, appendData[key]);
            }
            // 공통DataGrid TRX 호출
            UtilView.dataGridTRX(formData, callback);
        } else {
            UtilView.alertMesg("parameter [appendData]는 object 형식이어야 합니다!");
            return;
        }
    },

    /**
     * ◈ 공통 DataGrid Save TRX(DataGrid의 멀티row를 저장한다)
     *  => 저장후, 성공결과를 callback 을 통해 리턴한다.
     * @param {object} paramDatas : 전송할Data파라미터객체
     * @param {object} callback : callback function
     */
    dataGridSaveTRX: function (paramDatas, callback) {
        //console.log("gridData :" + paramDatas);
        //return;
        $.ajax({
            type: 'POST',
            url: UtilView.getActionUrl('TRX'),
            data: paramDatas,
            dataType: 'json',
            success: function (result) {
                alert(JSON.parse(result).message);
                if (!UtilView.isEmpty(callback)) callback(result);
            }
        });
    },

    /**
     * ◈ 공통 DataGrid Save TRX(DataGrid의 멀티row를 저장한다)
     *  => 저장후, 성공결과를 callback 을 통해 리턴한다.
     * @param {object} paramDatas : 전송할Data파라미터객체
     * @param {string} actType : action타입
     * @param {object} callback : callback function
     */
    dataGridSaveTRX2: function (paramDatas, actType, callback) {
        //console.log("gridData :" + paramDatas);
        //return;
        $.ajax({
            type: 'POST',
            url: UtilView.getActionUrl(actType),
            data: paramDatas,
            dataType: 'json',
            success: function (result) {
                alert(JSON.parse(result).message);
                if (!UtilView.isEmpty(callback)) callback(result);
            }
        });
    },

    /**
     * ◈  (그리드멀티row 저장 공통함수) 저장버튼 클릭시, 그리드의 저장된 Row들을 js 객체로 리턴한다.
     * @param {any} gridId : 그리드 ID
     * @param {any} fields : backend로 넘길 필드지정(필드배열)
     * @param {any} beforeSaveGridFn : row 저장전, 작업정의(예 > row의 특정컬럼수정)(callbackFn)
     * @param {any} filtersFn : 저장할 row 조건필터(callbackFn)
     */
    dataGridSave: async function (gridId, fields, beforeSaveGridFn, filtersFn) {
        // [ROW저장] Grid 객체 가져오기 및 row자동저장
        var gridObj = $('#' + gridId).dxDataGrid('instance');

        // 저장전 callback 호출
        if (!UtilView.isEmpty(beforeSaveGridFn)) {
            await beforeSaveGridFn();
        }

        await gridObj.saveEditData();

        // [전송할 data객체생성] 
        var objData = {};
        var objDatas = [];
        var objRow;
        var objRows = gridObj.getVisibleRows();

        if (!UtilView.isEmpty(filtersFn)) {
            objRows = objRows.filter((row, index) => { return filtersFn(row.data, index) })
        }

        for (var index in objRows) {
            objRow = objRows[index].data;
            objData = {};
            fields.forEach((value, index) => {
                //console.log(value + ":" + objRow[value]);
                objData[value] = objRow[value];
            });
            objDatas.push(objData);
        }
        return objDatas;
    },

    /**
     * ◈  (그리드멀티row 저장 공통함수) 저장버튼 클릭시, 그리드의 저장된 Row들을 js 객체로 리턴한다.(validation check function 추가)
     *     만일, validation 실패시 false를 리턴한다.
     * @param {any} gridId : 그리드 ID
     * @param {any} fields : backend로 넘길 필드지정(필드배열)
     * @param {any} afterSaveRowFn : row 저장후, validation 체크 정의
     * @param {any} filtersFn : 저장할 row 조건필터(callbackFn)
     */
    dataGridSave2: async function (gridId, fields, afterSaveRowFn, filtersFn) {
        // [ROW저장] Grid 객체 가져오기 및 row자동저장
        var gridObj = $('#' + gridId).dxDataGrid('instance');

        await gridObj.saveEditData();

        // 저장후 validation callback 호출
        if (!UtilView.isEmpty(afterSaveRowFn)) {
            if (!afterSaveRowFn()) {
                return false;
            }
        }

        // [전송할 data객체생성] 
        var objData = {};
        var objDatas = [];
        var objRow;
        var objRows = gridObj.getVisibleRows();

        if (!UtilView.isEmpty(filtersFn)) {
            objRows = objRows.filter((row, index) => { return filtersFn(row.data, index) })
        }

        for (var index in objRows) {
            objRow = objRows[index].data;
            objData = {};
            fields.forEach((value, index) => {
                //console.log(value + ":" + objRow[value]);
                objData[value] = objRow[value];
            });
            objDatas.push(objData);
        }
        return objDatas;
    },

    /**
     * ◈  기존 포커스 유지, 기존 포커스에 대한 키값이 데이터셋에 없을 경우엔 첫 번째로 포커스하거나, 
     *   
     * @param {string} t : G(그리드), T(트리리스트)
     * @param {string} gridId : 그리드 ID
     * @param {boolean} focusInit : true(기존 포커스와 상관없이 첫번째로 포커스를 줄 것인지) 아니면 빈값 또는 false
     */
    setGridFocus: function (t, gridId, focusInit) {
        var grid;
        var data;

        var rowKey;
        var isExist = false;

        if (t == "G") {
            grid = $("#" + gridId).dxDataGrid("instance");
            data = $("#" + gridId).dxDataGrid("instance").option("dataSource");

        } else if (t == "T") {
            grid = $("#" + gridId).dxTreeList("instance");
            data = $("#" + gridId).dxTreeList("instance").option("dataSource");
        }

        //조회된 데이터가 없을 경우에는 리턴한다.
        if (UtilView.isEmpty(data)) {
            grid.option("focusedRowIndex", -1);
            return;
        }

        //기존의 포커스 로우키 저장(객체 또는 문자열)
        rowKey = grid.option("focusedRowKey");

        //기존 포커스 상관없이 0으로 포커스를 주고자 할 경우
        if (focusInit || UtilView.isEmpty(rowKey)) {
            grid.option("focusedRowIndex", 0);
            return;
        }

        //포커스 초기화
        grid.option("focusedRowIndex", -1);

        //그리드에 설정된 포커스 키값(배열 또는 문자열)
        var keyExpr = grid._options._optionManager._options.keyExpr;
        var keyIsArr = Array.isArray(keyExpr);

        //새로 조회된 데이터에 기존 포커스 로우키가 존재할 경우, 해당 데이터로 포커스 유지
        $.map(data, function (value, index) {

            //복합키일 경우
            if (keyIsArr) {
                var cnt = 0;
                for (var i = 0; i < keyExpr.length; i++) {
                    if (rowKey[keyExpr[i]] == value[keyExpr[i]]) {
                        cnt++;
                        continue;
                    }
                }

                if (keyExpr.length == cnt) {
                    isExist = true;
                    return;
                }
            }


            //단일키일 경우
            if (rowKey == value[keyExpr]) {
                isExist = true;
                return;
            }
        });

        if (isExist) {
            grid.option("focusedRowKey", rowKey);
        }

        //새로 조회된 데이터에 기존 포커스 로우키가 존재하지 않을 경우, 첫 번째로 포커스 인덱스 설정
        if (!isExist) {
            grid.option("focusedRowIndex", 0);
        }
    },

    /*********************************************************************************
    * 화면제어용 함수
    *********************************************************************************/
    /**
     * ◈  base 엘리먼트를 기준으로, 부모class의 이웃노드중 지정된 class명의 지정된 속성의 값을 가져온다.
     *     속성값이 없으면, empty를 리턴한다.
     * @param {any} baseEl: 기준엘리먼트
     * @param {any} parentClass : 기준엘리먼트의 찾을 상위 부모클래스명
     * @param {any} findClass  : 찾을 클래스명
     * @param {any} attrName  : 찾을 속성명
     */
    getParentAttrValByClass: function (baseEl, parentClass, findClass, attrName) {
        var curNode, attrValue;

        curNode = $(baseEl.target).closest(parentClass);
        curNode = curNode.prev(findClass);
        attrValue = (curNode.length > 0) ? curNode.attr(attrName) : "";

        return attrValue;
    },

    /**
     * ◈ 현재 선택된 탭의 매뉴정보를 리턴한다(program id, controller명)
     *   (주의바람) 해당 프로그램 tab로드가 완료됨이 보장될때, 사용가능(즉, tabload 이벤트중에는 사용하면안됨)
     * @param {any} gbName : 리턴할 구분명(all :  array:[pid,ctrolname], pid : program id, ctrlname : controller name)
     */
    getCurMenuInfo: function (gbName) {
        var curTabMenu, menuId;
        var pid, ctrlname, retInfo;

        curTabMenu = $('main > div#tabsContainer > div.dx-tabs-wrapper > div.dx-tab-selected button').attr('id');
        menuId = curTabMenu.substring(0, curTabMenu.indexOf("_Tab"));

        pid = $('div#' + menuId).attr("id");
        ctrlname = $('div#' + menuId).attr("page-ctrl-name");

        if (gbName == "pid") {
            retInfo = pid
        } else if (gbName == "ctrlname") {
            retInfo = ctrl

        } else if (gbName == "all") {
            retInfo = [pid, ctrlname];
        }
        return retInfo;
    },

    /**
     *  ◈ form의 label에 start(*) 표시를 해준다(적용대상은 => form 입력요소중 class에 "required"가 적용된 label)
     * @param {any} menuId : 매뉴ID
     */
    setformRequiredStar: function (menuId) {
        var basePath;
        basePath = 'div#' + menuId + ' form.required-star > div.input-wrapper';

        //if (basePath.length > 0) {
        //console.log("setformRequiredStar : " + menuId);
        document.querySelectorAll(basePath).forEach(function (wrapper, index) {
            if ($('div :input[class*="required"]', wrapper).length > 0) {
                $('label', wrapper).append('<star>*</star>');
            }
        });
        //}

    },
    /**
     *  Datagrid 기준 screenresize 처리
     *   각 화면에서, autoresize="Y" 로 설정되었을때만 동작함
     *   @param {any} menuId : 매뉴ID
     *   history
     *    2020-11-09  - getCurMenuInfo() 함수주석처리 => 해당, 프로그램tab매뉴의 비동기성을 보장할수없음
     *                - 대신 programid를 인자로 받아 구현함
     *    
     * */
    screenResize: function (menuId) {
        var correctVal = 20;        // 보정치(#content{padding-bottom:16px}, .tabconcontainer 등등 해서 20px)
        var curTabMenu;
        var srchPath;

        // 현재선택된 매뉴ID
        //menuId = UtilView.getCurMenuInfo("pid");

        // 화면별 resize 여부 check
        var isAutoResize = $('div#' + menuId).attr("autoresize");
        if (!isAutoResize || isAutoResize == "N") {
            return;
        }
        // (Contents Main) last Row의 객체.Top 
        //srchPath = 'div#' + menuId + ' > div.mainTop + div.row:last-of-type > div[class*="col-"] .box div[id$="Grid"]';
        srchPath = 'div#' + menuId + ' div.row:last-of-type > div[class*="col-"] .box div[id$="Grid"]';
        //console.log("srchPath :" + srchPath);
        // 객체 resize
        document.querySelectorAll(srchPath).forEach(function (grid, index) {
            // test
            //(() => {
            //    if (menuId == "OrderMaterialSum") {                                        
            //        console.log("window.innerHeight : " + window.innerHeight);
            //        console.log("objGridTop : " + grid.getBoundingClientRect().top);
            //        console.log("gridHeight : " + $(grid).height());
            //        console.log("resizeHeight : " + (window.innerHeight - grid.getBoundingClientRect().top - 20));
            //    };
            //})();

            // skip 조건설정
            // > height가 900보다 작다면, 강제조정대상에서 제외            
            // > 2020.11.19 화면별로 loading시 grid loading이 느린곳이 있어서, gridHeight가 0일때 강제로 900으로셋팅
            var gridHeight = parseInt($(grid).css('height'));
            gridHeight = (gridHeight == 0) ? 900 : gridHeight;

            if (gridHeight < 900) return;

            var objGridTop = grid.getBoundingClientRect().top;
            var resizeHeight = window.innerHeight - objGridTop - correctVal;

            //var mesg = "window.innerHeight : " + window.innerHeight;
            //mesg += " , objGridTop :" + objGridTop;
            //mesg += ",  resizeHeight : " + resizeHeight            

            //console.log(mesg + " : " + mesg);
            //console.log(grid.getBoundingClientRect());
            //console.log((index + 1) + " => " + mesg);

            // 그리드 마지막 row가 inneHeight 영역안에 있을때만 조정한다.
            if (objGridTop < window.innerHeight) {
                //console.log("test success");
                grid.style.height = resizeHeight + "px";
            }

        });
    },

    /*********************************************************************************
    * 새로고침 관련 유틸함수
    *********************************************************************************/
    /**
     * 새로고침 관련 유틸
     * history -------------------
     *   2021-06-21 [김상인] 추가
     */
    intervalUtil: {
        /**
         * 클릭된 view의 정지된 새로고침을 재시작.
         * 탭 닫기 버튼[×] 클릭으로 이벤트 발생 시 새로고침을 재시작 하지 않음.
         * @param {string} text 탭 클릭 시 실제 클릭된 요소의 outerText. 탭 닫기 버튼[×] 클릭 시 × 값을 전달받음.
         */
        intervalRestart: function (text) {
            if (text != "×") { // text × 는 소문자 x 가 아닌 특수문자 ×
                var currentViewId = UtilView.getCurrentViewId();
                var _intervalSetName = currentViewId + "_intervalSet";
                var _intervalSetSize = new Function("if(typeof " + _intervalSetName + " != 'undefined') { return Object.keys(" + _intervalSetName + ").length }")();

                for (var i = 0; i < (_intervalSetSize - 1); i++) {
                    var _excuteString = "if(typeof " + _intervalSetName + " != 'undefined') {"
                        + "clearInterval(" + _intervalSetName + "." + currentViewId + "_intervalMaster.interval_" + i + ");"
                        + "}";

                    new Function(_excuteString)();


                    if (i == (_intervalSetSize - 2)) {
                        var _excuteString = "if(typeof " + _intervalSetName + " != 'undefined') {"
                            + _intervalSetName + "." + currentViewId + "_intervalMaster.intervalExcute();"
                            + "}";
                        new Function(_excuteString)();
                    }
                }
            }
        },
        /**
         * 탭을 닫을 때 해당 view에 [ViewId]_intervalSet 객체가 존재할 경우 해당 객체는 삭제
         * @param {string} viewId 
         */
        intervalSetDelete: function (viewId) {
            var _intervalSetName = viewId + "_intervalSet";
            var _intervalSetSize = new Function("if(typeof " + _intervalSetName + " != 'undefined') { return Object.keys(" + _intervalSetName + ").length }")();

            for (var i = 0; i < (_intervalSetSize - 1); i++) {
                var _excuteString = "if(typeof " + _intervalSetName + " != 'undefined') {"
                    + "clearInterval(" + _intervalSetName + "." + viewId + "_intervalMaster.interval_" + i + ");"
                    + "}";

                new Function(_excuteString)();


                if (i == (_intervalSetSize - 2)) {
                    var _excuteString = "if(typeof " + _intervalSetName + " != 'undefined') {"
                        + _intervalSetName + " = undefined;"
                        + "}";
                    new Function(_excuteString)();
                }
            }
        }
    },

    /*********************************************************************************
    * 기타 유틸함수
    *********************************************************************************/

    /**
     *  현재 선택된 탭id 반환
     *  history -------------------
     *    2021-06-21 [김상인] function추가
     * */
    getCurrentViewId: function () {
        var _tabName = $("#tabsContainer div.dx-item.dx-tab.dx-tab-selected div.dx-item-content.dx-tab-content > span").text();
        var _menuId = $("li.dx-treeview-node.dx-treeview-item-without-checkbox.dx-treeview-node-is-leaf[aria-label='" + _tabName + "']").attr("data-item-id");
        if (typeof _menuId == "undefined") _menuId = "*undefined";

        var _splitIndex = _menuId.indexOf('*');
        var _viewId = _menuId.substr(_splitIndex + 1);

        return _viewId;
    }

}
/*▶▶▶▶▶▶▶▶▶▶▶▶ 공통 Util  END ▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶*/

/*▶▶▶▶▶▶▶▶▶▶▶▶ Factory Util START ▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶*/
// 1. (공통)동적생성 -> 로딩바 
const loadingbarFactory = {
    _factoryId: "",     // loding바 구분
    _htmlStr: "",       // loading바 전체 html문자열
    _htmlElId: "",      // loading영역 element id
    _htmlElStyle: "",   //inline style
    _htmlMesg:"",       //loadingbar 메시지
    _destroyDelay: 1,   //destroy전 dealy second(m/s아님)

    factoryCode: { ReportPrint: "ReportPrint", Reserved1 : "reserved1"},

    /**
     * 로딩바 생성
     * @param {any} factoryId  : 로딩바종류(factory구분:factoryCode)
     */
    create: function (factoryId) {

        _factoryId = factoryId;

        switch (_factoryId) {
            case this.factoryCode.ReportPrint:
                // 로딩바를 설정해준다.
                _destroyDelay = 2;
                _htmlElId = "reportPrintLoadingBtn";
                _htmlElStyle = "position:absolute;top:30%;left:50%; z-index:1";
                _htmlMesg = "Report Loadding..";
                _htmlStr = '<button id="' + _htmlElId + '" style="' + _htmlElStyle+'" class="btn btn-primary" disabled="">';
                _htmlStr += '<span class= "spinner-border spinner-border-sm" ></span> ' + _htmlMesg;
                _htmlStr += '</button>';

                //console.log("_factoryId !!");
                break;
            case this.factoryCode.Reserved1:
                break;            
            default:
                break;
        }
        // 로딩바 생성
        if (!UtilView.isEmpty(_htmlStr)) $('body').append(_htmlStr);
    },
    /**
     * 로딩바 제거
     * */
    destroy: function () {
        // 객체존재시만 제거        
        if (!UtilView.isEmpty(_htmlElId)) {
            setTimeout(() => {
                $('#' + _htmlElId).remove();
            }, _destroyDelay * 1000);
        } else {
            console.warn("객체가 존재하지않는다!");
        }
    }
};

// 2. (공통)동적생성 -> iframe 전송용 factory
const iframeTransFactory = {
    _factoryId: "",     // iframe 구분
    _htmlStr: "",       // ifame html문자열
    _iframeStyle : "",  // iframe style
    _iframeId: "",      // iframe element id
    _iframeOpt: "",     // iframe option(width,height...)
    _destroyDelay: 1,   //destroy전 dealy second(m/s아님)

    factoryCode: { ReportPrint: "ReportPrint", Reserved1: "" },

    /**
     * 로딩바 생성
     * @param {any} factoryId  : iframe(factory구분):factoryCode
     */
    create: function (factoryId) {

        _factoryId = factoryId;

        switch (_factoryId) {
            case this.factoryCode.ReportPrint:
                // 로딩바를 설정해준다.
                _destroyDelay = 1;
                _iframeId = 'reportPrintIframe';
                _iframeStyle = "position:absolute;top:30%"; // 요게 있어야 와꾸 무너지지않는다.
                _iframeOpt = ' width="0", height="0" ';
                _htmlStr = '<iframe  id="' + _iframeId + '" style="' + _iframeStyle+'" name="' + _iframeId + '" '+_iframeOpt+'>';

                //console.log("_factoryId !!");
                break;
            case this.factoryCode.Reserved1:
                break;
            default:
                break;
        }
        // iframe 생성
        if (!UtilView.isEmpty(_htmlStr)) $('body').append(_htmlStr);
    },
    /**
     * 로딩바 제거
     * */
    destroy: function () {
        // 객체존재시만 제거        
        if (!UtilView.isEmpty(_iframeId)) {
            setTimeout(() => {
                $('#' + _iframeId).remove();
            }, _destroyDelay * 1000);
        } else {
            console.warn("객체가 존재하지않는다!");
        }
    }
};
/*▶▶▶▶▶▶▶▶▶▶▶▶ Factory Util END ▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶*/

/*▶▶▶▶▶▶▶▶▶▶▶▶ class Util START ▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶*/
// date : 2020-11-10
// write : 조형진
// comment : CrystalReport Helper 
// 사용법 : new ReportHelper(event객체); 
//---------------------------------------------------------------------------------
// history
// Date         |   작업사항
//---------------------------------------------------------------------------------
// 
class ReportHelper {
    
    isValid = false;
    btnType = null;

    // 파라미터객체 배열 : 리포트 여러개 띄울때 배열로사용
    objParam = { objFile: "", objSp: "", objEtcInfo: "", objTblNm: "", objSub: "" };
    objParams = [];
    objFile = { path: "", RptFileName: "", RptSeq:0 };
    objSp = { SpName: "", gubun: "", reportParam: "" };
    objEtcInfo = { subParam: "", viewerName: "", nCopies: 1, id: "", type: "", title: "", body: "", receive: "", company_name:""};     
    objTblNm = { tblName: "" };  
    objSub = { subRptName: "" };  

    // code 정의
    codeBtnType = { Preview: "Preview", Print: "Print" };

    constructor(btnType) {
        this.btnType = $(btnType).closest('.dx-button').attr('id') || null;

        // btnType 미지정시, Preview로 강제설정
        if (this.btnType == null) {
            console.log("(default) report 객체 생성완료!");
            //this.btnType = this.codeBtnType.Preview;
        } else {
            console.log("(overloading)report 객체 생성완료!");
            if (this.btnType.indexOf('Preview') > -1) {
                this.btnType = this.codeBtnType.Preview;
            } else if (this.btnType.indexOf('Print') > -1) {
                this.btnType = this.codeBtnType.Print;
            } else {
                this.btnType = null;
                alert("잘못된 btnType 입니다!");               
            }
        }   
    }

    /**
     * 레포트 파라미터 추가
     * @param {any} objParam  : 파라미터객체
     */
    addParam(objParam) {
        //console.log("addParam 2")                
        //if (!UtilView.isEmpty(objParam)) {
        // 기본값 설정
        if (UtilView.isEmpty(objParam.objEtcInfo.nCopies)) {
            objParam.objEtcInfo.nCopies = 1;
        }
        objParam = this.parseReportParamr(objParam);
        this.objParams.push(objParam);            
        //}
    }

    /**
     * 레포트 파라미터 초기화
     *  : loop를 돌면서, 파라미터를 호출하는 report인경우, removeParam() 매서드를 사용하여 파라미터를 초기화 해준다.
     * */
    removeParam() {
        this.objParams.length = 0;
        this.objParams = [];
    }

    /**
     * (private) sp 파라미터 parsing 
     * @param {any} objParam  parsing할 sp파라미터문자열
     */
    parseReportParamr(objParam) {
        var splitStr = ";";
        var params = objParam.objSp.reportParam;
        //console.log("params :" + params);
        if (params.indexOf(splitStr) > -1) {
            var aryParam = params.split(splitStr);
            aryParam = aryParam.map(element => '@' + element);
            objParam.objSp.reportParam = aryParam.join(splitStr);
            
        }
        return objParam;
    }

    /**
     * report 실행, this.btnType을 확인하여, Preview / Print 매서드를 자동으로 호출한다.
     * */
    run() {
        if (this.btnType != null) {
            if (this.btnType == this.codeBtnType.Preview) {
                this.preview();
            } else if(this.btnType == this.codeBtnType.Print) {
                this.print();
            } else {
                var mesg = "실행오류";
                alert(mesg);                
            }
        }
    }

    /**
     * 미리보기
     * */
    async preview() {       
        //if (!this.isValid) {
        //    console.log("report 파라미터 유효하지않음");
        //    return;
        //}
        this.btnType = this.codeBtnType.Preview;
        //loadingbar 생성
        loadingbarFactory.create(loadingbarFactory.factoryCode.ReportPrint);

        await this.executeMain();

        //loadingbar 제거
        loadingbarFactory.destroy();
    }

    /**
     * 레포트 출력
     * */
    async print() {
        this.btnType = this.codeBtnType.Print;
       
        // iframe 생성, loadingbar 생성
        /************************************************************************
         * date : 2021-01-18
         * comment : client의 인쇄기능을 이용(즉, 화면을  _blank로 open해야함)
         *           => 고로, iframe 생성부분 필요없어 주석처리
         ************************************************************************/
        //iframeTransFactory.create(iframeTransFactory.factoryCode.ReportPrint);
        loadingbarFactory.create(loadingbarFactory.factoryCode.ReportPrint);

        // 레포트출력 수행
        await this.executeMain();
                
        // loadingbar, iframe 제거
        loadingbarFactory.destroy();        
        /************************************************************************
         * date : 2021-01-15
         * comment : client의 인쇄기능을 이용하기 위해서(javascript사용:print.js 라이블러리)
         *           => iframe destory 주석처리
         ************************************************************************/
       // iframeTransFactory.destroy();
    }

    /**
     * (private) 실제 report preview 및 print 실행 Main
     * */
    async executeMain() {
        var paramArry = this.objParams;
        var objMergeData;
        //console.log("report출력 start")
        for (var i = 0; i < paramArry.length; i++) {
            //console.log(paramArry[i]);
            objMergeData = this.objectMerge(paramArry[i]);
            await this.executeReport(objMergeData);
        }
        console.log("report출력 end")
    }

    /**
     * (private ) 실제 report preview 및 print 실행
     * @param {any} data 전송대상 data
     */
    executeReport(data) {              
        //return;
        var rptFileName = "";
        var path, viewerName;
        var wUrl, wParam, wTarget;

        //btnType(preview, print 설정) 추가
        data["btnType"] = this.btnType;
        rptFileName = data.RptFileName;
        if (!UtilView.isEmpty(data.viewerName)) {
            path = data.path;
            viewerName = data.viewerName;            
        } else {
            // default 설정
            path = "Report";
            viewerName = "ReportViewer";
        }        
        // report호출
        wUrl = '/' + path + '/' + viewerName + '.aspx?name=' + rptFileName + (data.RptSeq ? data.RptSeq : 0);
        /*****************************************
         * date : 2021-01-15
         * comment : 인쇄버튼 클릭시, client의 인쇄기능을 사용하기 위해서, 항상 _blank로 띄우도록 수정
         * *****************************************/
        if (this.btnType == this.codeBtnType.Preview) {
            wParam = 'height=' + screen.height + ',width=' + screen.width / 2
        } else {
            wParam = 'height=' + screen.height / 2 + ',width=' + screen.width / 2
        }
        wTarget = "_blank";

        //wTarget = (this.btnType == this.codeBtnType.Preview) ? "_blank" : "_blank";
        //console.log("wtarg :" + wTarget);
        jQuery.ajax({
            url: "/Report/ViewReport",
            type: "GET",
            data: data,
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                window.open(wUrl, wTarget, wParam);
                //window.open(wUrl, '_blank', wParam);
            },
            error: function () {
                alert("Found Error");
            }
        });
    }

    /**
     * (private) 객체병합
     * @param {any} objParam : 대상 객체
     */
    objectMerge(objParam) {
        let objConvert;
        objConvert = Object.assign(objParam.objFile, objParam.objSp, objParam.objEtcInfo, objParam.objTblNm, objParam.objSub);
        //console.log(objConvert);
        return objConvert;
    }

    /**
    *  인쇄대상 테스트 객체 확인
    * */
    testObj() {
        console.log("[report파람] Test 결과 Start]");
        console.log(this.objParams);
        console.log("[report파람] Test 결과 End]");
    }
}
/*▶▶▶▶▶▶▶▶▶▶▶▶ class Util END ▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶*/

/*▶▶▶▶▶▶▶▶▶▶▶▶ ProtoType START ▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶*/
// date : 2020-11-10
// write : 조형진
// comment : view에서 자주 사용되는 prototype 정의
// 사용법 : 기존 raw객체.매서드명
//---------------------------------------------------------------------------------
// history
// Date         |   작업사항
//---------------------------------------------------------------------------------
// 2020-11-10   | prototype 추가 : String 객체, Data객체 
/*********************************************************************************
 * String 객체 prototype
 *********************************************************************************/
String.prototype.left = function (length) {
    if (this.length <= length) {
        return this;
    }
    else {
        return this.substring(0, length);
    }
}

String.prototype.right = function (length) {
    if (this.length <= length) {
        return this;
    }
    else {
        return this.substring(this.length - length, this.length);
    }

}

/*********************************************************************************
 * Date 객체 prototype 
 *********************************************************************************/
Date.prototype.format = function (f) {
    if (!this.valueOf()) return " ";

    var weekName = ["일요일", "월요일", "화요일", "수요일", "목요일", "금요일", "토요일"];
    var d = this;

    return f.replace(/(yyyy|yy|MM|dd|E|hh|mm|ss|a\/p)/gi, function ($1) {
        switch ($1) {
            case "yyyy": return d.getFullYear();
            case "yy": return ("0"+ (d.getFullYear() % 1000)).right(2);
            case "MM": return ("0"+ (d.getMonth()+1)).right(2);
            case "dd": return ("0"+ d.getDate()).right(2);
            case "E": return weekName[d.getDay()];
            case "HH": return ("0"+(d.getHours())).right(2);
            case "hh": return ("0"+ ((h = d.getHours() % 12) ? h : 12)).right(2);
            case "mm": return ("0"+ d.getMinutes()).right(2);
            case "ss": return ("0"+ d.getSeconds()).right(2);
            case "a/p": return d.getHours() < 12 ? "오전" : "오후";
            default: return $1;
        }
    });
};

/*▶▶▶▶▶▶▶▶▶▶▶▶ ProtoType End ▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶▶*/


// json데이터 form에 바인딩
function mappingJsonDataToForm(formId, data) {

    // 테스트 consolg.log
    //for (var key in data) { console.log("[" + PagePrefix + "]=> " + key + " : " + data[key]); }
    for (var key in data) {
        //var tagName = $('[name='+key+']').prop('tagName').toUpperCase();
        var type = $('[name=' + key + ']').prop('type');
        type = type ? type.toLowerCase() : "";
        if (type == "radio" || type == "checkbox") {
            $('input[type=' + type + '][name=' + key + '][value="' + data[key] + '"]', $('#' + formId)).prop('checked', true);
        } else if (type == "text") {
            if ($('[name=' + key + ']', $('#' + formId)).hasClass("datepicker")) {
                $('[name=' + key + ']', $('#' + formId)).datepicker("update", data[key]);
            } else {
                $('[name=' + key + ']', $('#' + formId)).val(data[key]);
            }
        } else {
            $('[name=' + key + ']', $('#' + formId)).val(data[key]);
        }
    }

}

// datagrid에 새로운 로우 추가하면서 jsonData를 Set
function gridAddNowRowSetValue(gridId, data) {

    var grid = $("#" + gridId).dxDataGrid("instance");
    grid.addRow();

    for (var key in data) {
        grid.cellValue(0, key, data[key]);
    }
}

// formData를 json으로
function FormDataToJSON(formId) {
    var formData = new FormData($("#" + formId)[0]);
    var ConvertedJSON = {};
    for (const [key, value] of formData.entries()) {
        ConvertedJSON[key] = value;
    }
    return ConvertedJSON
}

function getFormatDate(date) {
    var year = date.getFullYear();
    var month = (1 + date.getMonth());
    month = month >= 10 ? month : '0' + month;
    var day = date.getDate();
    day = day >= 10 ? day : '0' + day;
    return year + '-' + month + '-' + day;
}

function alertInfo(text, displayTime = 1000) {
    DevExpress.ui.notify(text, "info", displayTime);
}

function alertError(text, displayTime = 1000) {
    DevExpress.ui.notify(text, "error", displayTime);
}

function alertSuccess(text, displayTime = 1000) {
    DevExpress.ui.notify(text, "success", displayTime);
}

function alertWarning(text, displayTime = 1000) {
    DevExpress.ui.notify(text, "warning", displayTime);
}

// bootstrap datepicker 공통 세팅
function setDatePicker(selector) {
    $(selector).datepicker({
        format: "yyyy-mm-dd",	//데이터 포맷 형식(yyyy : 년 mm : 월 dd : 일 )
        clearBtn: false, //날짜 선택한 값 초기화 해주는 버튼 보여주는 옵션 기본값 false 보여주려면 true
        autoclose: true,
        templates: {
            leftArrow: '&laquo;',
            rightArrow: '&raquo;'
        }, //다음달 이전달로 넘어가는 화살표 모양 커스텀 마이징
        todayHighlight: true,	//오늘 날짜에 하이라이팅 기능 기본값 :false
        weekStart: 0,//달력 시작 요일 선택하는 것 기본값은 0인 일요일
        language: "ko"	//달력의 언어 선택, 그에 맞는 js로 교체해줘야한다.
    });

    var datepickerInput = document.querySelectorAll(selector);

    for (var i = 0; i < datepickerInput.length; i++) {
        var item = datepickerInput.item(i);
        item.setAttribute("maxlength", 10);
        item.onkeyup = function () {
            this.value = autoHypenDate(this.value);
        }
    }
}

// yyyy-dd-MM 포맷에 맞춰 하이픈 자동 입력
var autoHypenDate = function (str) {
    str = str.replace(/[^0-9]/g, '');
    var tmp = '';
    if (str.length < 5) {
        return str;
    } else if (str.length < 7) {
        tmp += str.substr(0, 4);
        tmp += '-';
        tmp += str.substr(4);
        return tmp;
    } else if (str.length < 11) {
        tmp += str.substr(0, 4);
        tmp += '-';
        tmp += str.substr(4, 2);
        tmp += '-';
        tmp += str.substr(6);
        return tmp;
    }

    return str;
}