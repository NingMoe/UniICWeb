$(function () {
    var req = uni.getReq();
    var line;
    if (req["tab"]) {
        var str = req["tab"];
        line = str.split("_");
    }
    $(".tabs").each(function (i) {
        var head = $(".tab_head", this);
        var h_list = head.children("li");
        h_list.click(selectTab);
        h_list.each(function (i) {
            $(this).attr("index", i);
        });
        var con = $(".tab_con", this);
        var c_list = con.children("div");
        c_list.addClass("item");

        var index = 0;
        if (line && line[i]) {
            var index = parseInt(line[i]);
        }
        h_list.eq(index).addClass("h_sel");
        c_list.eq(index).addClass("c_sel");
    });
});
function selectTab() {
    // 操作标签
    var tabs = $(this).parent().children("li");
    tabs.each(function () {
        $(this).removeClass("h_sel");
    });
    $(this).addClass("h_sel");
    // 操作内容
    var index = 0;
    tabs.each(function (i) {
        if ($(this).hasClass("h_sel")) {
            index = i;
        }
    });
    var con = $(this).parents("div.tabs:first").find(".tab_con");
    var items = con.find(".item");
    items.each(function () {
        $(this).removeClass("c_sel");
    });
    con.find(".item:eq(" + index + ")").addClass("c_sel");
}

//tab 要激活的li
function selTab(tab, i) {
    // 操作标签
    var it = $(tab).children("li").eq(i);
    it.trigger("click");
}

//保存tab状态 页面刷新
function tabReload() {
    var pars = uni.url2Obj(location.href);
    var arr = [];
    $(".tab_head").each(function (i) {
        $(this).find("li").each(function (i) {
            if ($(this).hasClass("h_sel")) {
                arr.push(i);
                return;
            }
        });
    });
    pars.tab = arr.join('_');
    pars.t = "_" + (new Date()).valueOf();
    var p = uni.obj2Url(pars);
    location.href = location.pathname + "?" + p;
}

//创建tab
$.fn.extend({
    unitab: function () {
        var pthis = $(this);
        pthis.addClass("tabs");
        pthis.children("ul:first").addClass("tab_head");
        pthis.children("div:first").addClass("tab_con");
    }
});