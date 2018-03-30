
//-----------------------
//floor,服务端从0开始
$.fn.UIPageCtrl = function (bLoaded) {
    var pThis = $(this);
   
    $("input", pThis).focus(function () { $(this).blur(); }).each(function () {
        this.onselectstart = function () { return false; };
    });
    var ctlNeedLines = $("select[name='_dwNeedLines']", pThis);

    var StartLine = $("input[name='_dwStartLine']", pThis);
    var dwTotolLines = parseInt($("input[name='_dwTotolLines']", pThis).val());
    var dwStartLine = parseInt(StartLine.val());
    var dwNeedLines = parseInt(ctlNeedLines.val());

    if (dwStartLine == 0 || dwStartLine+"" == "NaN") {
        dwStartLine = 1;
        StartLine.val("1");
    }

    var dwLastLines = Math.floor(dwTotolLines / dwNeedLines) * dwNeedLines + 1;
    if (dwLastLines > dwTotolLines) {
        dwLastLines -= dwNeedLines;
    }


    var firstPage = $(".firstPage", pThis).button();
    var prevPage = $(".prevPage", pThis).button();
    var nextPage = $(".nextPage", pThis).button();
    var lastPage = $(".lastPage", pThis).button();

    if(bLoaded)
    {
        firstPage.click(function () {if($(this).hasClass("ui-state-disabled"))return false; StartLine.val("1"); return ReloadPage(this); });
        prevPage.click(function () {if($(this).hasClass("ui-state-disabled"))return false; StartLine.val(dwStartLine - dwNeedLines); return ReloadPage(this); });
        nextPage.click(function () {if($(this).hasClass("ui-state-disabled"))return false; StartLine.val(dwStartLine + dwNeedLines); return ReloadPage(this); });
        lastPage.click(function () { if ($(this).hasClass("ui-state-disabled")) return false; StartLine.val(dwLastLines); return ReloadPage(this); });

        ctlNeedLines.change(function () {
            var cur = parseInt(StartLine.val());
            if (cur + "" == "NaN") {
                cur = 1;
            }

            var dwNewNeedLine = $(this).val();
            cur = (((cur - 1) / dwNeedLines) * dwNewNeedLine) + 1;
            if (cur > dwTotolLines)
            {
                cur = 1;
            }

            StartLine.val(cur);
            return ReloadPage(this);
        });
    }
    
    if (dwTotolLines+"" == "NaN" || dwTotolLines == 0) {
        $("input[name='_dwStartLine']", pThis).val("0");
        firstPage.button("disable");
        prevPage.button("disable");
        nextPage.button("disable");
        lastPage.button("disable");
        return;
    }
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
};
