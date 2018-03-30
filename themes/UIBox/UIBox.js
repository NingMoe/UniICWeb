function GetCurrentPath() {
    var jsNodes = document.getElementsByTagName('script');
    for (var i = 0; i < jsNodes.length; i++) {
        var pathArray = jsNodes[i].src.split("/");
        if (pathArray[pathArray.length - 1].toLowerCase() == "uibox.js") {
            delete pathArray[pathArray.length - 1];
            var ret = pathArray.join("/");
            return ret;
        }
    }
}

$.fn.UIBox = function (options) {
$(this).each(function () {
    var pThis = $(this);
    var pInited = pThis.data("UIBoxInited");
    if (pInited != null && pInited != "") {
        return;
    } else {
        pThis.data("UIBoxInited", "true");
    }

    var defaults = {
        width: "960px",
        height: "100%",
        boxClassName: "Box",
        hiddenUnBox: true
    };
    var opts = $.extend(defaults, options);
    pThis.addClass("UIBox");
    pThis.css({ width: opts.width, height: opts.height });


    if (opts.hiddenUnBox) {
        var UnBoxs = pThis.children().not("." + opts.boxClassName);
        UnBoxs.css({ display: "none", visibility: "hidden" });
    }

    var Boxs = pThis.children("." + opts.boxClassName);
    
    var curPath = GetCurrentPath();

    var Boxdefaults = {
        width: 300,
        height: 300,
        borderWeight: 3,
        theme: curPath + "defaultbg.png"
    };

    $.each(Boxs, function (i, nbox) {
        var box = $(nbox);

        var opts = $.extend(Boxdefaults);

        var opt_n = box.attr("theme");
        if (opt_n && opt_n != "") {
            opts.theme = curPath + opt_n;
        }
        opt_n = box.attr("width");
        if (opt_n && opt_n != "") {
            opts.width = opt_n;
        }
        opt_n = box.attr("height");
        if (opt_n && opt_n != "") {
            opts.height = opt_n;
        }
        opt_n = box.attr("borderWeight");
        if (opt_n && opt_n != "") {
            opts.borderWeight = parseInt(opt_n);
        }

        var boxct = $("<div></div>");
        boxct.addClass("UIBOX_boxct");
        boxct.css({ width: opts.width, height: opts.height });
        boxct.appendTo(pThis);

        var boximg = $("<img/>");
        boximg.attr("src", opts.theme);
        boximg.addClass("UIBOX_img");

        var titleHeight = 0;
        var boxtitle = box.children(".title");
        if (boxtitle && boxtitle.length > 0) {
            if (boxtitle.length && boxtitle.length > 1) {
                boxtitle = boxtitle[0];
            }
            boxtitle.addClass("UIBOX_title");
            boxtitle.css({
                left: opts.borderWeight + "px",
                top: opts.borderWeight + "px",
                width: (boxct.width() - opts.borderWeight * 2) + "px"
            });
        
            var boxmore = boxtitle.children(".more");
            if (boxmore && boxmore.length > 0) {
                if (boxmore.length > 1) {
                    boxmore = boxmore[0];
                }
                boxmore.addClass("UIBOX_more");
            }
            titleHeight = boxtitle.height();
        }

        box.addClass("UIBOX_box");
        box.css({
            left: opts.borderWeight + "px",
            top: (opts.borderWeight + titleHeight) + "px",
            width: (boxct.width() - opts.borderWeight * 2) + "px",
            height: (boxct.height() - opts.borderWeight * 2 - titleHeight) + "px"
        });

        boximg.appendTo(boxct);
        boxtitle.appendTo(boxct);
        box.appendTo(boxct);
    });
});
};

$.fn.UIAPanel = function (options) {
    $(this).each(function () {
        var pThis = $(this);
        var pInited = pThis.data("UIAPanelInited");
        if (pInited != null && pInited != "") {
            return;
        } else {
            pThis.data("UIAPanelInited", "true");
        }

        var defaults = {
            theme: "defaultbg.png", borderWidth: 5, minWidth: "355", maxWidth: "355", minHeight: "35", maxHeight: "270", minOpacity: 0.9, maxOpacity: 1, speed: 100, OnOpen: null, OnClose: null, page:null
        };
        var opts = $.extend(defaults, options);
        var attr = pThis.attr("height");
        if (attr && attr.length > 0) {
            opts.maxHeight = attr;
        }
        attr = pThis.attr("width");
        if (attr && attr.length > 0) {
            opts.maxWidth = attr;
        }

        attr = pThis.attr("page");
        if (attr && attr.length > 0) {
            opts.page = attr;
        }


        pThis.css({
            width: opts.minWidth,
            padding: opts.borderWidth,
            height: opts.minHeight - opts.borderWidth,
            overflow: "hidden",
            border: "0px"
        });
        var szTheme = GetCurrentPath() + opts.theme;

        var boximg = $("<img/>");
        boximg.attr("src", szTheme);
        boximg.addClass("UIBOX_img");
        boximg.css({ zIndex: -1,width:0,height:0 });
        boximg.appendTo(pThis);

        pThis.find("input").change(function () { userChanged = true; });
        function LoadDynPage() {
            if (opts.page) {
                $.ajax({
                    type: "get",
                    url: opts.page,
                    error: function () { MessageBox('加载页面' + opts.page + '时出错！'); },
                    success: function (msg) {
                        if (dynContent) {
                            dynContent.remove();
                            dynContent = null;
                        }
                        dynContent = $("<div>" + msg + "</div>");
                        pThis.append(dynContent);
                        try {
                            if (SetDefaultValue) {
                                SetDefaultValue();
                            }
                        } catch (e) { }

                        dynContent.find("input").change(function () { userChanged = true; });
                    }
                });
            }
        }

        var userChanged = false;
        var dynContent = null;
        function OnHover() {
            userChanged = false;
            LoadDynPage();

            pThis.stop();
            boximg.css({ width: "100%", height: "100%" });
            if(opts.maxHeight == "auto")
            {
                pThis.animate({ width: opts.maxWidth,height:pThis.innerHeight(), opacity: opts.maxOpacity }, opts.speed, function () {
                    pThis.css({height:"auto"});
                    opts.maxHeight = pThis.height();
                });
            }else{
                pThis.animate({ width: opts.maxWidth, height: opts.maxHeight, opacity: opts.maxOpacity }, opts.speed, function () {
                });
            }
            
            if (opts.OnOpen) {
                opts.OnOpen();
            }
        }
        function OnOver() {
            pThis.stop();
            pThis.animate({ width: opts.minWidth, height: opts.minHeight - opts.borderWidth, opacity:opts.minOpacity }, opts.speed, function () {
                boximg.css({ width: 0, height: 0 });
                if (opts.OnClose) {
                    opts.OnClose(userChanged);
                }
            });
        }
        if (opts.IsTableOP) {
            pThis.parents("tr").hover(OnHover, OnOver);
        }
        pThis.hover(OnHover, OnOver);
        pThis.focus(OnHover);
        //pThis.blur(OnOver);  //点击子元素时会触发此事件
        OnOver();
        LoadDynPage();
    });
    $("*", $(this)).tooltip({ hide: { duration: 0 } });
};
