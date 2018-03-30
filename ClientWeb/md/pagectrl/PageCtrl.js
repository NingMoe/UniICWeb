
//-----------------------
//服务端从0开始 前端从1开始
//何昆鹏  json方式分页控件
(function ($) {
    var name;
    $.fn.pageCtrl = function (changeFun, pageLines, pCtrlName) {
        if (typeof (changeFun) != 'function') {
            return;
        }
        if (pageLines == undefined || pageLines + "" == "NaN") {
            pageLines = 15;
        }
        var pThis = $(this);
        var name = pCtrlName ||pThis.attr("pc")|| "pCtrl";
        
        pThis.addClass("PageCtrl");
        pThis.html("<div class=\"info\"><input name=\"pCtrlName\" type=\"hidden\" value=\"" + name + "\"/><label>总:</label><input name=\"" + name + "_total\" type=\"hidden\" value=\"0\"/><span id=\"" + name + "_total\">0</span><label>页，<div style='display:none;'>每页:</label><input name=\"" + name + "_need\" type=\"hidden\" value=\"" + pageLines + "\" /><span id=\"" + name + "_need\">" + pageLines + "</span><label>条，</div>当前第</label><input name=\"" + name + "_start\" type=\"hidden\" value=\"1\"/><span style='color:red;' id=\"" + name + "_num\">0</span><label>页</label></div>" +
            "<div class=\"ctrl\"><button class=\"" + name + "_firstPage\">首页</button> | <button class=\"" + name + "_prevPage\">上一页</button> | <button class=\"" + name + "_nextPage\">下一页</button> | <button class=\"" + name + "_lastPage\">尾页</button></div>");

        $("input", pThis).focus(function () { $(this).blur(); }).each(function () {
            this.onselectstart = function () { return false; };
        });


        var firstPage = $("." + name + "_firstPage", pThis).button();
        var prevPage = $("." + name + "_prevPage", pThis).button();
        var nextPage = $("." + name + "_nextPage", pThis).button();
        var lastPage = $("." + name + "_lastPage", pThis).button();

        //初始化点击事件
        firstPage.click(function () { if ($(this).hasClass("ui-state-disabled")) return false; _pc_ctrlPage(name, changeFun, "first", pageLines); return false; });
        prevPage.click(function () { if ($(this).hasClass("ui-state-disabled")) return false; _pc_ctrlPage(name, changeFun, "prev", pageLines); return false; });
        nextPage.click(function () { if ($(this).hasClass("ui-state-disabled")) return false; _pc_ctrlPage(name, changeFun, "next", pageLines); return false; });
        lastPage.click(function () { if ($(this).hasClass("ui-state-disabled")) return false; _pc_ctrlPage(name, changeFun, "last", pageLines); return false; });
        //初始不可用
        firstPage.button("disable");
        prevPage.button("disable");
        nextPage.button("disable");
        lastPage.button("disable");
        return {
            //更新页面
            update: function (total,start,need) {
                updatePageCtrl(total, start, need, name)
            },
            getStart: function () {
                return (getPageCtrl(name)).start;
            },
            getTotal: function () {
                return (getPageCtrl(name)).total;
            },
            getNeed: function () {
                return (getPageCtrl(name)).need;
            }
        };
    }
    function _pc_ctrlPage(name, changeFun, ctrlCom, needLines) {
        var StartLine = $("input[name='" + name + "_start']");
        var dwStartLine = parseInt(StartLine.val());
        var dwTotalLines = parseInt($("input[name='" + name + "_total']").val());
        var dwNeedLines = needLines;

        if (dwStartLine == 0 || dwStartLine + "" == "NaN") {
            dwStartLine = 1;
            StartLine.val("1");
        }
        else {
            var dwLastLines = Math.floor(dwTotalLines / dwNeedLines) * dwNeedLines + 1;
            if (dwLastLines > dwTotalLines) {
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

        changeFun(name, dwStartLine, needLines);
    }
})(jQuery);
//更新分页控件，每次重新获取数据都需要调用此函数 
function updatePageCtrl(dwTotalLines, dwStartLine, dwNeedLines, pageCtrlName) {
    var name = pageCtrlName || "pCtrl";
    var StartLine = $("input[name='" + name + "_start']");
    var TotolLines = $("input[name='" + name + "_total']");
    var NeedLines = $("input[name='" + name + "_need']");

    var firstPage = $("." + name + "_firstPage").button();
    var prevPage = $("." + name + "_prevPage").button();
    var nextPage = $("." + name + "_nextPage").button();
    var lastPage = $("." + name + "_lastPage").button();
    dwTotalLines = parseInt(dwTotalLines);
    dwStartLine = parseInt(dwStartLine);
    dwNeedLines = parseInt(dwNeedLines);
    if (typeof (dwTotalLines) == "number" && dwTotalLines > 0) {
        StartLine.val(dwStartLine);
        if (dwTotalLines == undefined || dwTotalLines + "" == "NaN") {
            dwTotalLines = parseInt(TotolLines.val());
        }
        else {
            TotolLines.val(dwTotalLines);
        }
        if (dwNeedLines == undefined || dwNeedLines + "" == "NaN") {
            dwNeedLines = parseInt(NeedLines.val());
        }
        else {
            NeedLines.val(dwNeedLines);
        }
        var num = Math.floor(dwStartLine / dwNeedLines) + 1;
        if (dwStartLine % dwNeedLines == 0) num--;
        var total = Math.floor(dwTotalLines / dwNeedLines) + 1;
        if (dwTotalLines % dwNeedLines == 0) total--;
        $("#" + name + "_num").html(num);
        $("#" + name + "_total").html(total);
        $("#" + name + "_need").html(dwNeedLines);

        if (dwStartLine <= 1) {
            firstPage.button("disable");
            prevPage.button("disable");
        } else {
            firstPage.button("enable");
            prevPage.button("enable");
        }
        if (dwStartLine + dwNeedLines > dwTotalLines) {
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

        $("#" + name + "_num").html("0");
        $("#" + name + "_total").html("0");
    }
}
function getPageCtrl(pageCtrlName) {
    var name = pageCtrlName || "pCtrl";
    var pc = {};
    pc.name = name;
    pc.start = $("input[name='" + name + "_start']").val();
    pc.total = $("input[name='" + name + "_total']").val();
    pc.need = $("input[name='" + name + "_need']").val();
    return pc;
}
