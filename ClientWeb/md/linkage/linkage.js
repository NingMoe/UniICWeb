!function ($) {
    $.extend({
        _jsonp: {
            scripts: {},
            counter: 1,
            charset: "gb2312",
            head: document.getElementsByTagName("head")[0],
            name: function (callback) {
                var name = "_jsonp_" + (new Date).getTime() + "_" + this.counter;
                this.counter++;
                var cb = function (json) {
                    eval("delete " + name),
					callback(json),
					$._jsonp.head.removeChild($._jsonp.scripts[name]),
					delete $._jsonp.scripts[name]
                };
                return eval(name + " = cb"),
				name
            },
            load: function (a, b) {
                var c = document.createElement("script");
                c.type = "text/javascript",
				c.charset = this.charset,
				c.src = a,
				this.head.appendChild(c),
				this.scripts[b] = c
            }
        },
        getJSONP: function (a, b) {
            var c = $._jsonp.name(b),
			a = a.replace(/{callback};/, c);
            return $._jsonp.load(a, c),
			this
        }
    })
}
(jQuery);
(function ($) {
    var iplocation = { "1": "未初始化"};
    var provinceCityJson = { "1": [{ "id": 1, "name": "未初始化" }]};
    var currentLocation = "未初始化";
    var currentProvinceId = 0;
    var getThreeLevelData;//获取数据函数
    var main;//实例对象  jquery对象

    //根据ID获取名称
    function getNameById(provinceId) {
        var name = iplocation[provinceId];
        if (name)
            return name;
        return "不存在";
    }
    function convertProvince() {
        var ret = [];
        for (var o in iplocation) {
            ret.push({ "id": o, "name": iplocation[o] });
        }
        return ret;
    }
    var isUseServiceLoc = true; //是否默认使用服务端地址
    var provinceHtml = '<div class="content"><div data-widget="tabs" class="m JD-stock" id="JD-stock">'
                                    + '<div class="mt">'
                                    + '    <ul class="tab">'
                                    + '        <li data-index="0" data-widget="tab-item" class="curr"><a class="hover click"><em>请选择</em><i></i></a></li>'
                                    + '        <li data-index="1" data-widget="tab-item" style="display:none;"><a class="click"><em>请选择</em><i></i></a></li>'
                                    + '        <li data-index="2" data-widget="tab-item" style="display:none;"><a class="click"><em>请选择</em><i></i></a></li>'
                                    + '        <li data-index="3" data-widget="tab-item" style="display:none;"><a class="click"><em>请选择</em><i></i></a></li>'
                                    + '    </ul>'
                                    + '    <div class="stock-line"></div>'
                                    + '</div>'
                                    + '<div class="mc" data-area="0" data-widget="tab-content" id="stock_province_item">'
                                    + '    <ul class="area-list">'
                                    + '    </ul>'
                                    + '</div>'
                                    + '<div class="mc" data-area="1" data-widget="tab-content" id="stock_city_item"></div>'
                                    + '<div class="mc" data-area="2" data-widget="tab-content" id="stock_area_item"></div>'
                                    + '<div class="mc" data-area="3" data-widget="tab-content" id="stock_town_item"></div>'
                                    + '</div>'
                                    + '<div class="close"></div></div>';
    function getAreaList(result) {
        var html = ["<ul class='area-list'>"];
        var longhtml = [];
        var longerhtml = [];
        if (result && result.length > 0) {
            for (var i = 0, j = result.length; i < j ; i++) {
                result[i].name = result[i].name.replace(" ", "");
                if (result[i].name.length > 12) {
                    longerhtml.push("<li class='longer-area'><a class='click' data-value='" + result[i].id + "'>" + result[i].name + "</a></li>");
                }
                //else if (result[i].name.length > 5) {
                //    longhtml.push("<li class='long-area'><a class='click' data-value='" + result[i].id + "'>" + result[i].name + "</a></li>");
                //}
                else {
                    //html.push("<li><a class='click' data-value='" + result[i].id + "'>" + result[i].name + "</a></li>");
                    longhtml.push("<li class='long-area'><a class='click' data-value='" + result[i].id + "'>" + result[i].name + "</a></li>");
                }
            }
        }
        else {
            html.push("<li><a class='click' data-value='" + currentAreaInfo.currentFid + "'> </a></li>");
        }
        html.push(longhtml.join(""));
        html.push(longerhtml.join(""));
        html.push("</ul>");
        return html.join("");
    }
    function cleanKuohao(str) {
        if (str && str.indexOf("(") > 0) {
            str = str.substring(0, str.indexOf("("));
        }
        if (str && str.indexOf("（") > 0) {
            str = str.substring(0, str.indexOf("（"));
        }
        return str;
    }

    function getStockOpt(id, name) {
        if (currentAreaInfo.currentLv == 3) {
            currentAreaInfo.threeLvId = id;
            currentAreaInfo.threeLvName = name;
            if (!page_load) {
                currentAreaInfo.fourLvId = 0;
                currentAreaInfo.fourLvName = "";
            }
        }
        else if (currentAreaInfo.currentLv == 4) {
            currentAreaInfo.fourLvId = id;
            currentAreaInfo.fourLvName = name;
        }
        main.removeClass('hover');
        if (opt.complete) opt.complete(page_load, currentAreaInfo);
        if (page_load) {
            page_load = false;
        }
        var address = currentAreaInfo.oneLvName + currentAreaInfo.twoLvName + currentAreaInfo.threeLvName + currentAreaInfo.fourLvName;
        $(".text div", main).html(currentAreaInfo.oneLvName + " " + cleanKuohao(currentAreaInfo.twoLvName) + " " + cleanKuohao(currentAreaInfo.threeLvName) + cleanKuohao(currentAreaInfo.fourLvName)).attr("title", address);
    }
    function getAreaListcallback(r) {
        currentDom.html(getAreaList(r));
        if (currentAreaInfo.currentLv >= 2) {
            currentDom.find("a").click(function () {
                if (page_load) {
                    page_load = false;
                }
                if (currentDom.attr("id") == "stock_area_item") {
                    currentAreaInfo.currentLv = 3;
                }
                else if (currentDom.attr("id") == "stock_town_item") {
                    currentAreaInfo.currentLv = 4;
                }
                getStockOpt($(this).attr("data-value"), $(this).html());
            });
            if (page_load) { //初始化加载
                currentAreaInfo.currentLv = currentAreaInfo.currentLv == 2 ? 3 : 4;
                if (currentAreaInfo.threeLvId && new Number(currentAreaInfo.threeLvId) > 0) {
                    getStockOpt(currentAreaInfo.threeLvId, currentDom.find("a[data-value='" + currentAreaInfo.threeLvId + "']").html());
                }
                else {
                    getStockOpt(currentDom.find("a").eq(0).attr("data-value"), currentDom.find("a").eq(0).html());
                }
            }
        }
    }
    function chooseProvince(provinceId) {
        currentAreaInfo.currentLv = 1;
        currentAreaInfo.oneLvId = provinceId;
        currentAreaInfo.oneLvName = getNameById(provinceId);
        if (!page_load) {
            currentAreaInfo.twoLvId = 0;
            currentAreaInfo.twoLvName = "";
            currentAreaInfo.threeLvId = 0;
            currentAreaInfo.threeLvName = "";
            currentAreaInfo.fourLvId = 0;
            currentAreaInfo.fourLvName = "";
        }
        if (opt.level > 1) {
            areaTabContainer.eq(0).removeClass("curr").find("em").html(currentAreaInfo.oneLvName);
            areaTabContainer.eq(1).addClass("curr").show().find("em").html("请选择");
            areaTabContainer.eq(2).hide();
            areaTabContainer.eq(3).hide();
            provinceContainer.hide();
            cityContainer.show();
            areaContainer.hide();
            townaContainer.hide();

            if (opt.getTwoLevelData) {
                cityContainer.html("<div class='iloading'>正在加载中，请稍候...</div>");
                opt.getTwoLevelData("" + provinceId, actTwoLevelData)
            }
            else if (provinceCityJson["" + provinceId]) {
                actTwoLevelData(provinceCityJson["" + provinceId]);
            }
        }
        else {
            areaTabContainer.eq(0).find("em").html(currentAreaInfo.oneLvName);
            $(".text div", main).html(currentAreaInfo.oneLvName);
        }
    }
    function actTwoLevelData(data) {
        cityContainer.html(getAreaList(data));
        cityContainer.find("a").click(function () {
            if (page_load) {
                page_load = false;
            }
            main.unbind("mouseout");
            chooseCity($(this).attr("data-value"), $(this).html());
        });
        if (page_load) { //初始化加载
            if (currentAreaInfo.twoLvId && new Number(currentAreaInfo.twoLvId) > 0) {
                chooseCity(currentAreaInfo.twoLvId, cityContainer.find("a[data-value='" + currentAreaInfo.twoLvId + "']").html());
            }
            else {
                chooseCity(cityContainer.find("a").eq(0).attr("data-value"), cityContainer.find("a").eq(0).html());
            }
        }
    }
    function chooseCity(cityId, cityName) {
        provinceContainer.hide();
        currentAreaInfo.currentLv = 2;
        currentAreaInfo.twoLvId = cityId;
        currentAreaInfo.twoLvName = cityName;
        if (!page_load) {
            currentAreaInfo.threeLvId = 0;
            currentAreaInfo.threeLvName = "";
            currentAreaInfo.fourLvId = 0;
            currentAreaInfo.fourLvName = "";
        }
        if (opt.clickTwoLevel && !page_load) opt.clickTwoLevel(cityId, cityName,currentAreaInfo);
        if (opt.level > 2) {
            cityContainer.hide();
            areaTabContainer.eq(1).removeClass("curr").find("em").html(cityName);
            areaTabContainer.eq(2).addClass("curr").show().find("em").html("请选择");
            areaTabContainer.eq(3).hide();
            areaContainer.show().html("<div class='iloading'>正在加载中，请稍候...</div>");
            townaContainer.hide();
            currentDom = areaContainer;
            if (getThreeLevelData)
                getThreeLevelData(cityId, getAreaListcallback);
        }
        else {
            getStockOpt(cityId, cityName);
        }
    }
    function chooseArea(areaId, areaName) {
        provinceContainer.hide();
        cityContainer.hide();
        areaContainer.hide();
        currentAreaInfo.currentLv = 3;
        currentAreaInfo.threeLvId = areaId;
        currentAreaInfo.threeLvName = areaName;
        if (!page_load) {
            currentAreaInfo.fourLvId = 0;
            currentAreaInfo.fourLvName = "";
        }
        areaTabContainer.eq(2).removeClass("curr").find("em").html(areaName);
        areaTabContainer.eq(3).addClass("curr").show().find("em").html("请选择");
        townaContainer.show().html("<div class='iloading'>正在加载中，请稍候...</div>");
        currentDom = townaContainer;
    }
    var areaTabContainer;
    var provinceContainer;
    var cityContainer;
    var areaContainer;
    var townaContainer;
    var currentDom;
    //当前地域信息
    var currentAreaInfo;
    //初始化当前地域信息
    function CurrentAreaInfoInit() {

        currentAreaInfo = { "currentLv": 1, "oneLvId": 1, "oneLvName": "未初始化", "twoLvId": 0, "twoLvName": "", "threeLvId": 0, "threeLvName": "", "fourLvId": 0, "fourLvName": "" };
        var ipLoc = getCookie("ipLoc-djd");
        ipLoc = ipLoc ? ipLoc.split("-") : opt.curSelect;
        if (ipLoc.length > 0 && ipLoc[0]) {
            currentAreaInfo.oneLvId = ipLoc[0];
            currentAreaInfo.oneLvName = getNameById(ipLoc[0]);
        }
        if (ipLoc.length > 1 && ipLoc[1]) {
            currentAreaInfo.twoLvId = ipLoc[1];
        }
        if (ipLoc.length > 2 && ipLoc[2]) {
            currentAreaInfo.threeLvId = ipLoc[2];
        }
        if (ipLoc.length > 3 && ipLoc[3]) {
            currentAreaInfo.fourLvId = ipLoc[3];
        }
    }
    var page_load = true;
    var opt = {
        level: 3,
        curSelect: [1, 1, 0, 0],
        clickOneLevel: undefined,
        clickTwoLevel: undefined,
        clickThreeLevel: undefined
    };
    $.fn.linkage = function (para) {
        if (para) $.extend(opt, para);
        getThreeLevelData = opt.getThreeLevelData;
        iplocation = opt.oneLevelData;
        provinceCityJson = opt.twoLevelData;

        main = $(this);
        main.html("<div class='text'><div></div><b></b></div>");
        $(".text", main).after(provinceHtml);
        areaTabContainer = $("#JD-stock .tab li", main);
        provinceContainer = $("#stock_province_item", main);
        cityContainer = $("#stock_city_item", main);
        areaContainer = $("#stock_area_item", main);
        townaContainer = $("#stock_town_item", main);
        currentDom = provinceContainer;
        currentDom.html(getAreaList(convertProvince()));
        main.unbind("mouseover").bind("mouseover", function () {
            main.addClass('hover');
            $(".content,#JD-stock", main).show();
        }).find("dl").remove();
        main.mouseout(function () {
            main.removeClass("hover");
        });
        CurrentAreaInfoInit();
        $(".close", main).click(function () { main.removeClass('hover') });
        areaTabContainer.eq(0).find("a").click(function () {
            areaTabContainer.removeClass("curr");
            areaTabContainer.eq(0).addClass("curr").show();
            provinceContainer.show();
            cityContainer.hide();
            areaContainer.hide();
            townaContainer.hide();
            areaTabContainer.eq(1).hide();
            areaTabContainer.eq(2).hide();
            areaTabContainer.eq(3).hide();
        });
        areaTabContainer.eq(1).find("a").click(function () {
            areaTabContainer.removeClass("curr");
            areaTabContainer.eq(1).addClass("curr").show();
            provinceContainer.hide();
            cityContainer.show();
            areaContainer.hide();
            townaContainer.hide();
            areaTabContainer.eq(2).hide();
            areaTabContainer.eq(3).hide();
        });
        areaTabContainer.eq(2).find("a").click(function () {
            areaTabContainer.removeClass("curr");
            areaTabContainer.eq(2).addClass("curr").show();
            provinceContainer.hide();
            cityContainer.hide();
            areaContainer.show();
            townaContainer.hide();
            areaTabContainer.eq(3).hide();
        });
        provinceContainer.find("a").click(function () {
            if (page_load) {
                page_load = false;
            }
            main.unbind("mouseout");
            var provinceId = $(this).attr("data-value");
            if(opt.clickOneLevel) opt.clickOneLevel(provinceId);
            chooseProvince(provinceId);
        }).end();
        chooseProvince(currentAreaInfo.oneLvId);
    };

    function getCookie(name) {
        var start = document.cookie.indexOf(name + "=");
        var len = start + name.length + 1;
        if ((!start) && (name != document.cookie.substring(0, name.length))) {
            return null;
        }
        if (start == -1) return null;
        var end = document.cookie.indexOf(';', len);
        if (end == -1) end = document.cookie.length;
        return unescape(document.cookie.substring(len, end));
    };
})(jQuery);