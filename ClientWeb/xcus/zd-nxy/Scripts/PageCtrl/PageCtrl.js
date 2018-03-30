
//-----------------------
//floor,服务端从0开始
//何昆鹏  json方式分页控件

$.fn.jsonPageCtrl = function (subFun, pageLines) {
    if (subFun==undefined||subFun==null) {
        return;
    }
    if (pageLines == undefined || pageLines + "" == "NaN") {
        pageLines = 15;
    }
    var pThis = $(this);
    var pageCtrlID = pThis.attr('id');
    pageCtrlID = $.trim(pageCtrlID);
    if (pageCtrlID == "" || pageCtrlID == null) {
        return false;
    }
    pThis.addClass("PageCtrl");
    pThis.html("<div class=\"info\"><label>"+uni.translate("总")+"</label><input name=\"" + pageCtrlID + "_dwTotolLines\" type=\"hidden\" value=\"0\"/><span name=\"" + pageCtrlID + "_dwTotolLines\">0</span><label>"+uni.translate("条，　开始于第")+"</label><input name=\"" + pageCtrlID + "_dwStartLine\" type=\"hidden\" value=\"0\"/><span name=\"" + pageCtrlID + "_dwStartLine\">0</span><label>"+uni.translate("条")+"</label><div class=\"hidden\"><label>"+uni.translate("，　每页记录数")+"：</label><input name=\"" + pageCtrlID + "_dwNeedLines\" type=\"hidden\" value=\"" + pageLines + "\" /><span name=\"" + pageCtrlID + "_dwNeedLines\">" + pageLines + "</span></div></div><div class=\"ctrl\"><button class=\"firstPage\">首页</button> | <button class=\"prevPage\">上一页</button> | <button class=\"nextPage\">下一页</button> | <button class=\"lastPage\">尾1页</button></div>");

    $("input", pThis).focus(function () { $(this).blur(); }).each(function () {
        this.onselectstart = function () { return false; };
    });


    var firstPage = $(".firstPage", pThis).button();
    var prevPage = $(".prevPage", pThis).button();
    var nextPage = $(".nextPage", pThis).button();
    var lastPage = $(".lastPage", pThis).button();

    //初始化点击事件
    firstPage.click(function () { if ($(this).hasClass("ui-state-disabled")) return false; ctrlPage(pageCtrlID, subFun, "first", pageLines); return false; });
    prevPage.click(function () { if ($(this).hasClass("ui-state-disabled")) return false; ctrlPage(pageCtrlID, subFun, "prev", pageLines); return false; });
    nextPage.click(function () { if ($(this).hasClass("ui-state-disabled")) return false; ctrlPage(pageCtrlID, subFun, "next", pageLines); return false; });
    lastPage.click(function () { if ($(this).hasClass("ui-state-disabled")) return false; ctrlPage(pageCtrlID, subFun, "last", pageLines); return false; });
    //初始不可用
    firstPage.button("disable");
    prevPage.button("disable");
    nextPage.button("disable");
    lastPage.button("disable");
}
function ctrlPage(pageCtrlID, subFun, ctrlCom, pageLines) {
    var pThis = $("#" + pageCtrlID);

    var StartLine = $("input[name='" + pageCtrlID + "_dwStartLine']", pThis);
    var dwStartLine = parseInt($("input[name='" + pageCtrlID + "_dwStartLine']", pThis).val());
    var dwTotolLines = parseInt($("input[name='" + pageCtrlID + "_dwTotolLines']", pThis).val());
    var dwNeedLines = pageLines;

    if (dwStartLine == 0 || dwStartLine + "" == "NaN") {
        dwStartLine = 1;
        StartLine.val("1");
    }
    else {
        var dwLastLines = Math.floor(dwTotolLines / dwNeedLines) * dwNeedLines + 1;
        if (dwLastLines > dwTotolLines) {
            dwLastLines -= dwNeedLines;
        }

        if (ctrlCom == "first") {
            StartLine.val("1");
        }
        else if (ctrlCom == "prev") {
            StartLine.val(dwStartLine - dwNeedLines);
        }
        else if (ctrlCom == "next") {
            StartLine.val(dwStartLine + dwNeedLines);
        }
        else if (ctrlCom == "last") {
            StartLine.val(dwLastLines);
        }
        else {
            return;
        }
        dwStartLine = parseInt(StartLine.val());
    }

    subFun(pageCtrlID, dwStartLine, pageLines);
}

//更新分页控件，每次重新获取数据都需要调用此函数 
function updatePageCtrl(pageCtrlID, dwTotolLines, dwStartLine, dwNeedLines) {
    if (pageCtrlID == undefined) {
        return;
    }
    var pThis = $("#" + pageCtrlID);
    var StartLine = $("input[name='" + pageCtrlID + "_dwStartLine']", pThis);
    var TotolLines = $("input[name='" + pageCtrlID + "_dwTotolLines']", pThis);
    var NeedLines=$("input[name='" + pageCtrlID + "_dwNeedLines']", pThis);

    var firstPage = $(".firstPage", pThis).button();
    var prevPage = $(".prevPage", pThis).button();
    var nextPage = $(".nextPage", pThis).button();
    var lastPage = $(".lastPage", pThis).button();

    if (typeof (dwTotolLines) == "number" && dwTotolLines > 0) {
        StartLine.val(dwStartLine);
        if (dwTotolLines == undefined || dwTotolLines + "" == "NaN") {
            dwTotolLines = parseInt(TotolLines.val());
        }
        else {
            TotolLines.val(dwTotolLines);
        }
        if (dwNeedLines == undefined || dwNeedLines + "" == "NaN") {
            dwNeedLines = parseInt(NeedLines.val());
        }
        else {
            NeedLines.val(dwNeedLines);
        }

        $("span[name='" + pageCtrlID + "_dwStartLine']", pThis).html(dwStartLine);
        $("span[name='" + pageCtrlID + "_dwTotolLines']", pThis).html(dwTotolLines);
        $("span[name='" + pageCtrlID + "_dwNeedLines']", pThis).html(dwNeedLines);

        if (dwStartLine <= 1) {
            firstPage.button("disable");
            prevPage.button("disable");
        } else {
            firstPage.button("enable");
            prevPage.button("enable");
        }
        if (dwStartLine + dwNeedLines > dwTotolLines) {
            nextPage.button("disable");
            lastPage.button("disable");
        } else {
            nextPage.button("enable");
            lastPage.button("enable");
        }
    }
    else {
        TotolLines.val("0");
        StartLine.val("0");
        firstPage.button("disable");
        prevPage.button("disable");
        nextPage.button("disable");
        lastPage.button("disable");

        $("span[name='" + pageCtrlID + "_dwStartLine']", pThis).html("0");
        $("span[name='" + pageCtrlID + "_dwTotolLines']", pThis).html("0");
    }
}