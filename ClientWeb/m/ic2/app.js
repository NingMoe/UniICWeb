//--------------基本
//初始化
var $ = $$ = Dom7;
var userAgent = navigator.userAgent, app = navigator.appVersion;
var isIOS = (userAgent.indexOf('Safari') > -1 && !!userAgent.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/));
var isMultiLanguage = document.getElementById("cfg_is_multilanguage").value == "1";//多语言
var currentLan = document.getElementsByClassName('lanClass')[0].value;//当前语言
$(".lang_set img").click(function () {
    var pthis = $(this);
    var lang = pthis.data("lang");
    pro.setLanguage(lang);
});
if (isMultiLanguage) {
   // alert(currentLan);
    /*
    var lan = "en-gb";//英语美语统一用en-gb
    pro.initLanguage(lan, function () {
        pro.transPage($("body"));
        startAPP();
    });
   */
   
    var lan =currentLan;//(navigator.browserLanguage || navigator.language).toLowerCase();//20170705多语言修改
    if (lan == "")
    {
        lan = "zh-cn";
    }
    
    if (lan == "zh-cn") {
        //中文无需翻译 关闭多语言翻译开关
        isMultiLanguage = false;
        startAPP();
    }
    else {
        if (lan == "en-us") lan = "en-gb";//英语美语统一用en-gb
        pro.initLanguage(lan, function () {
            pro.transPage($("body"));
            startAPP();
        });
    }
    
}
else {
    startAPP();
}

//启动应用
function startAPP() {
    var app = new Framework7({
        pushState: !isIOS,
        animateNavBackIcon: true,
        precompileTemplates: true,
        template7Pages: true,
        modalTitle: uni.translate('消息'),
        modalButtonOk: uni.translate('确定'),
        modalButtonCancel: uni.translate('取消'),
        onAjaxStart: function (xhr) {
            app.showIndicator();
        },
        onAjaxComplete: function (xhr) {
            app.hideIndicator();
        },
        onPageInit: function (a, page) {
            var container = page.container;
            if (isMultiLanguage) {
                if (page.navbarInnerContainer)
                pro.transPage(page.navbarInnerContainer);
                pro.transPage(container);
            }
            //标头背景
            var test = $(".banner", container);
            test.each(function () {
                var pthis = $(this);
                var h = pthis.height() || 0;
                var w = pthis.width() || document.body.clientWidth;
                var rs = pthis.height() / 6;
                var svg = $('<svg class="banner_svg"  width="100%" height="100%" xmlns="http://www.w3.org/2000/svg"></svg>');
                var arr = [];
                for (var i = 0; i < 70; i++) {
                    var rou = Math.random();
                    var op = Math.random();
                    while (rou < 0.2) {
                        rou = Math.random();
                    }
                    while (op > 0.6) {
                        op = Math.random();
                    }
                    arr.push([w * Math.random(), h * Math.random(), rs * rou, op]);
                }
                var colors = ["#92C13F", "#FFF301", "#01AEF0", "#ED008C"];
                var isPure = pthis.hasClass("pure");
                for (var i = 0; i < arr.length; i++) {
                    var node = document.createElementNS("http://www.w3.org/2000/svg", "circle");
                    node.setAttribute("cx", arr[i][0]);
                    node.setAttribute("cy", arr[i][1]);
                    node.setAttribute("r", arr[i][2]);
                    node.setAttribute("style", "fill:" + (isPure ? "#ddd" : colors[i % 4]) + ";fill-opacity:" + arr[i][3]);
                    svg[0].appendChild(node);
                }
                if (this.hasChildNodes()) {
                    this.insertBefore(svg[0], this.firstChild);
                }
                else {
                    pthis.append(svg);
                }
            });
            //动画效果
            var rotate = $(".am_rotate", container);
            if (rotate.length > 0) {
                setTimeout(function () { rotate.css("transform", "rotate(360deg)"); }, 500);
                setTimeout(function () { rotate.css("transform", "none"); }, 1600);
            }
        }
    });
    //views
    var vpara = {
        dynamicNavbar: true,
        domCache: true,
        animateNavBackIcon: true
    };
    var view = app.addView(".view-main", vpara);
    //全局公共参数
    var global = {
        version: "20151110",
        app: app,
        view: view,
        isIOS: isIOS,
        isThirdLogin: $("#is_third_login").val() == "true",//单点登录
        config: {
            //默认预约时长
            resvInterval: parseInt($("#cfg_resv_interval").val() || 0),
            //状态全天
            allDayState: parseInt($("#cfg_all_day_state").val() || 0),
            //默认开放时间
            dftOpenTime: (function () {
                var v = $("#cfg_resv_range").val();
                if (v) {
                    var list = v.split(',')[0].split('@');
                    if (list.length == 2) {
                        return list[1].split('-');
                    }
                    else return [0, 0];
                }
                else return [0, 0];
            })(),
            //手机端属性
            mobileProp: parseInt($("#cfg_mobile_prop").val() || 0),
            //定位坐标
            coords: $("#cfg_coords").val().split(','),
            //座位模式
            seatRscMode: parseInt($("#cfg_seat_resource_mode").val() || 0),
            //开放活动
            openAty: parseInt($("#cfg_open_aty").val() || 0),
            //显示不开放
            showClose: $("#cfg_show_close").val() == "1",
            //时间粒度
            resvUnit: $("#cfg_resv_unit").val() || 10
        }
    };
    pro.global = global;
    //pro 必须app与views后
    pro.initExMethods(app);
    //坐标定位初始化
    uni.locationPara.beginLoc = function () {
        app.showIndicator();
    };
    uni.locationPara.endLoc = function () {
        app.hideIndicator();
    };
    //用户登录
    (function () {
        var lg = $(".login-screen");
        var openid = (uni.getReq())["openid"];
        var id = $.cookie("user");
        //if (openid) {
        //    login(openid,"@openid")
        //}
        //else
        if ($("#re_login").val() == "true") {
            login("@relogin", "");
        }
        else if (id) {
            var pwd = $.cookie("pwd");
            $("#username").val(id);
            $("#password").val(pwd);
            login(id, pwd);
        }
        else {
            app.loginScreen();
        }

        lg.find(".click_login").click(function () {
            login($("#username").val(), $("#password").val());
        });
        function login(id, pwd) {
            if (!id) return;
            pro.j.lg.login(id, pwd, function (rlt) {
                //登录后操作
                app.closeModal();
                initUser();
                $(window).trigger("app_logined");
                    $.cookie("user", id, { expires: 365 });
                    $.cookie("pwd", pwd, { expires: 365 });
            }, function (rlt) { app.loginScreen(); pro.msgBox(rlt.msg); }, 512, $("#aluserid").val(), $("#schoolCode").val(), $("#wxuserid").val());
        }
        //初始化用户
        function initUser() {
            var acc = pro.acc;
            $(".user_name").html(acc.name);
            $(".user_dept").html(acc.id + "<br/>" + acc.dept);
            $(".user_phone").attr("value", acc.phone);
            $(".user_email").attr("value", acc.email);
            var rec = $(".user_receive");
            if (rec.length > 0) {
                if (acc.receive)
                    rec.attr("checked", "checked");
                else
                    rec.removeAttr("checked");
            }
        }
        //对外接口
        app.outLogin = { initUser: initUser, login: login };
    })();
    //用户退出
    $(".click_logout").click(function () {
        pro.j.lg.logout();
    });
    //登录后跳转 全局方法
    function loginedLoad(url) {
        pro.j.lg.isLogin(function () {
            setTimeout(function () {
                view.loadPage({ url: url });
            }, 100);
        }, function () {
            pro.msgBox("登录已超时");
            app.loginScreen();
        });
    }
    window.loginedLoad = loginedLoad;//全局
    //------------------------ 主体页面
    //tab事件
    var tabReserve = $("#tab_reserve").on("show", function () {
        $("#current_tab_title").html(uni.translate("资源分类"));
        var scr = $(this).parents(".native-scroll");
        if (scr.length > 0) scr[0].scrollTop = 0;
    });
    var tabState = $("#tab_state").on("show", function () {
        $("#current_tab_title").html(uni.translate("预约列表"));
        var scr = $(this).parents(".native-scroll");
        if (scr.length > 0) scr[0].scrollTop = 0;
        var i = 0;
        var remind = $("#my_resv_remind .icon-remind");
        var int = setInterval(function () {
            if (i == 0) {
                remind.css({ position: "absolute;", transform: "scale(1.5,1.5)" });
            }
            else {
                var num = 45 - i * 3;
                remind.css({ transform: "rotate(" + ((i % 2) == 0 ? "-" : "") + num + "deg)" });
            }
            if (++i > 10) { clearInterval(int); remind.css({ transform: "none", position: "relative" }); }
        }, 300);
    });
    var tabCenter = $("#tab_center").on("show", function () {
        $("#current_tab_title").html(uni.translate("个人中心"));
        var scr = $(this).parents(".native-scroll");
        if (scr.length > 0) scr[0].scrollTop = 0;
    });
    //主题按钮
    if ($.cookie("theme")) {
        var color = $.cookie("theme");
        $(".sel_theme").removeClass("active");
        $(".sel_theme.bg-" + color).addClass("active");
    }
    //改变主题
    $(".sel_theme").click(function () {
        var color = $(this).data("theme");
        $("body").attr("class", "theme-" + color);
        $.cookie("theme", color, { expires: 365 });
        $(".sel_theme").removeClass("active");
        $(this).addClass("active");
    });
    //------------------------预约状态
    (function () {
        var pResvstat;
        var resvstatOrientationHandle;//翻转处理函数handle
        app.onPageInit("p-resvstat", function (page) {
            //初始化
            
            
            pResvstat = page;
            var container = page.container;
            var query = page.query;
            var dt = $("#ic_resv_stat .stat_date");
            dt.val(page.query.date || (new Date()).format("yyyy-MM-dd"));
            $("#ic_resv_stat").on("show", function () {
                loadResvStat(query);
            });
            //初始化公共参数
            if (query.classkind) global.curClassKind = query.classkind;
            //初始化日期选择控件
            var cld = app.calendar(datePicker($("#resv_stat_picker"), {}, function (dt) {
                var flg = $(".flag_today", page.container);
                if (dt == (new Date()).format("yyyy-MM-dd")) flg.show();
                else flg.hide();
                cld.value = [uni.parseDate(dt)];
                //
                
                if (page.url.indexOf("resvstat.aspx") > -1) {
                    page.query.act = "get_rm_sta";
                    page.query.date = dt;
                    $$.ajax({
                        url: pro.dir + "ajax/room.aspx", async: false, method: 'get', data: page.query, success: function (resData) {
                            resData = JSON.parse(resData);
                            if (resData.data != null && resData.data.length > 0) {
                                for (var i = 0; i < resData.data.length; i++) {
                                    if (resData.data[i].name == page.query.name) {
                                        query.open_start = resData.data[i].roomStat.dwOpenBegin;
                                        query.open_end = resData.data[i].roomStat.dwOpenEnd;
                                        break;
                                    }
                                }
                            }
                        }, error: function (xhr, status) {
                            
                        }, complete: function (xhr, status) {
                            
                        }, statusCode: {
                            404: function (xhr) {
                                alert('page not found');
                            }
                        }
                    });
                    page.query.act = null;

                }
                
              //  picker.openStart = query.open_start;
                //picker.openEnd = query.open_end;
                
               picker.initTimePicker(app, {
                    uint: global.config.resvUnit,
                    start: query.start,
                    end: query.end,
                    dateInput: $("#ic_resv_stat .stat_date"),
                    openStart: query.open_start || open[0], openEnd: query.open_end || open[1], isAllDay: isAllDay,
                    startInput: $(".stat_time_start", container), endInput: $(".stat_time_end", container),
                    onClose: function () {
                        loadResvStat(query);
                    }
                });
                
                loadResvStat(query);
            }));
            var li = $(".stat_time_li", container);
            if ((parseInt(query.pro) & 4) == 0) {
                var open = global.config.dftOpenTime;
                var isAllDay = global.config.allDayState * 8 > 0;
                var picker = $(".stat_time", container);

                if (page.url.indexOf("resvstat.aspx") > -1)
                {
                    page.query.act = "get_rm_sta";
                    $$.ajax({
                        url: pro.dir + "ajax/room.aspx", async: false, method: 'get', data: page.query, success: function (resData) {
                            resData = JSON.parse(resData);
                            if (resData.data != null && resData.data.length > 0)
                            {
                                for (var i = 0; i < resData.data.length; i++)
                                {
                                    if (resData.data[i].name == page.query.name)
                                    {       
                                        query.open_start = resData.data[i].roomStat.dwOpenBegin;
                                        query.open_end = resData.data[i].roomStat.dwOpenEnd;
                                        break;
                                    }
                                }
                            }
                        }, error: function (xhr, status) {
                            
                        }, complete: function (xhr, status) {
                            
                        },statusCode: {
                        404: function (xhr) {
                            alert('page not found');
                        }
                    }
                    });
                      page.query.act = null;
                   
                }
                
                picker.initTimePicker(app, {
                    uint: global.config.resvUnit,
                    start: query.start,
                    end: query.end,
                    dateInput: dt,
                    openStart: query.open_start || open[0], openEnd: query.open_end || open[1], isAllDay: isAllDay,
                    startInput: $(".stat_time_start", container), endInput: $(".stat_time_end", container),
                    onClose: function () {
                        
                        loadResvStat(query);
                    }
                });
                isAllDay && picker.attr("disabled", "disabled");
            }
            else {//长期
                li.hide();
            }
            //横屏事件
            resvstatOrientationHandle = function () {
                if ($("#ic_resv_stat").hasClass("active") && $(pResvstat.container).hasClass("page-on-center")) {
                    loadResvStat(page.query);
                }
            };
            window.addEventListener("orientationchange", resvstatOrientationHandle);
            //第一次加载
            setTimeout(function () {
                loadResvStat(page.query);
            }, 300);
        });
        app.onPageBack("p-resvstat", function () {
            window.removeEventListener("orientationchange", resvstatOrientationHandle);
        });
        function loadResvStat(para) {
            var tab = $("#ic_resv_stat");
            //参数
            para.date = $(".stat_date", tab).val();
            para.fr_start = $(".stat_time_start", tab).val();
            para.fr_end = $(".stat_time_end", tab).val();
            //是否长期
            if (para.islong == "true") {
                var psd = uni.parseDate(para.date);
                para.start = psd.format("yyyyMMdd");
                para.end = psd.addDays(30).format("yyyyMMdd");
            }
            var fpKind = parseInt($(".floor_plan_kind").val() || 0);
            if (para.classkind && (parseInt(para.classkind) & fpKind) > 0) {
                //状态图
                tab.find(".resv_floor_con").show();
                paintFloorPlan(tab.find(".resv_floor_panel"), para);
                //提交事件
                $(".click_sel_apply", tab).touchend(function () {
                    var g = $(".fp-g.selected", tab);
                    if (g.length > 0) {
                        var date = $(".stat_date", tab).val();
                        var start = $(".stat_time_start", tab).val();
                        var end = $(".stat_time_end", tab).val();
                        var query = "&date=" + date + "&start=" + (start || "") + "&end=" + (end || "");
                        view.loadPage({ url: "../a/resvsub.aspx?" + g.attr("para") + query });
                    }
                });
            }
            else {
                //状态表
                tab.find(".resv_list_con").show();
                paintResvTbl(tab.find(".resv_list_panel"), para);
            }
        }
    })();
    //------------------------资源介绍
    (function () {
        app.onPageInit("p-detail", function (page) {
            app.swiper("#ic_rsc_detail .swiper-container", {
                slidesPerView: 1,
                pagination: ".swiper_pagination_resvstat",
                lazyLoading: true
            });
        });
    })();
    //------------------------查找资源
    (function () {
        var pSearch;
        var searchOrientationHandle;//翻转处理函数handle
        app.onPageInit("p-search", function (page) {
            //初始化参数
            pSearch = page;
            var query = page.query;
            var container = page.container;
            var filter = uni.getObj(query);
            filter.prop = $(".filter_prop", container).val();
            filter.kinds = filter.kind_id = $(".filter_kinds", container).val();
            //初始化公共参数
            if (query.classkind) global.curClassKind = query.classkind;
            //初始化时间选择
            var dt = $("#search_stat_picker .stat_date");
            dt.val((new Date()).format("yyyy-MM-dd"));
            var li = $(".stat_time_li", container);
            if ((parseInt(query.pro) & 4) == 0) {
                var open = global.config.dftOpenTime;
                $(".stat_time", container).initTimePicker(app, {
                    uint: global.config.resvUnit,
                    openStart: query.open_start || open[0], openEnd: query.open_end || open[1],
                    dateInput: dt,
                    startInput: $(".stat_time_start", container), endInput: $(".stat_time_end", container),
                    onClose: function () {
                        loadSearch(filter);
                    }
                });
            }
            else {//长期
                li.hide();
            }
            //
            //初始化筛选dialog
            $("#dft_dialog_nav .center").html(uni.translate("筛选条件"));
            $("#dft_dialog_content").html($("#search_filter_content").html());
            $("#dft_dialog_nav .left").click(function () {
                var data = app.formToJSON("#dft_dialog_content form");
                for (var key in data) {
                    var p = data[key];
                    if (p instanceof Array) filter[key] = p.join();
                    else if (typeof (data[key]) == "object") filter[key] = p;
                }
                setTimeout(function () { loadSearch(filter); }, 300);
            });
            //初始化日期选择控件
            var cld = app.calendar(datePicker($("#search_stat_picker"), {}, function (dt) {
                var flg = $(".flag_today", page.container);
                if (dt == (new Date()).format("yyyy-MM-dd")) flg.show();
                else flg.hide();
                cld.value = [uni.parseDate(dt)];
                loadSearch(filter);
            }));
            //横屏事件
            searchOrientationHandle = function () {
                loadSearch(filter);
            };
            window.addEventListener("orientationchange", searchOrientationHandle);
            //第一次加载
            setTimeout(function () {
                loadSearch(filter);
            }, 300);
        });
        app.onPageBack("p-search", function () {
            window.removeEventListener("orientationchange", searchOrientationHandle);
        });
        function loadSearch(para) {
            //参数
            var container = pSearch.container;
            para.date = $(".stat_date", container).val();
            var picker = $(".stat_time", container);
            if (picker.hasClass("inited")) {
                para.fr_start = $(".stat_time_start", container).val();
                para.fr_end = $(".stat_time_end", container).val();
            }
            var panel = $(".search_list_panel", container);
            panel.html('loading...');
            //状态表
            paintResvTbl(panel, para);
        }
    })();
    //------------------------快速预约
    (function () {
        app.onPageInit("p-quick", function (page) {
            var con = page.container;
            //初始化日期选择控件
            $(".stat_date", con).val((new Date()).format("yyyy-MM-dd"));
            ckDate(con);
            var cld = app.calendar(datePicker($("#quick_date_picker"), {}, function (dt) {
                var flg = $(".flag_today", page.container);
                if (dt == (new Date()).format("yyyy-MM-dd")) flg.show();
                else flg.hide();
                cld.value = [uni.parseDate(dt)];
                ckDate(con, dt);
            }));
            //初始化时间选择
            var picker = $(".stat_time", con);
            var open = global.config.dftOpenTime;
            
            picker.initTimePicker(app, {
                uint: global.config.resvUnit,
                openStart: (open.length == 2 ? open[0] : "08:00"),
                openEnd: (open.length == 2 ? open[1] : "22:00"),
                dateInput: $(".stat_date", con),
                timeInput: $(".range_time", con),
                onClose: function () {
                    $(".stat_time_text", con).html("(" + picker.val().replace(/\s/g, '') + ")");
                }
            });
            $(".sel_cur_range", con).touchend(function () {
                picker.click();
            });
            $(".range_list input[type=radio]", con).on("change", function () {
                if ($(this).hasClass("range_time")) {
                    $(".sel_cur_range", con).removeClass("disabled");
                }
                else {
                    $(".sel_cur_range", con).addClass("disabled");
                }
            });
            //初始化公共参数
            var query = page.query;
            if (query.classkind) global.curClassKind = query.classkind;
            //提交事件
            $(".quick_submit", page.container).click(function () {
                var para = app.formToJSON($("form", page.container)[0]);
                para.classkind = "8";
                pro.j.rsv.quickResv(para, function (rlt) {
                    var data = rlt.data;
                    uni.msgBox(pro.transPick("<div class='bold'>预约成功</div>位置：" + data.dev + "<br/>时间：") + data.time, function () {
                        pro.back("p-index", function (e) {
                            app.showTab("#tab_state");
                            app.outTabStat.loadMyResv();
                            history.replaceState(null, document.title, location.origin + location.pathname);//清空历史状态
                        });
                    });
                });
            });
        })
        function ckDate(container, date) {
            var now = new Date();
            var ranges = $(".radio_range", container);
            var ck = true;
            if (date && now.format("yyyy-MM-dd") != date) ck = false;
            var m = now.getHours() * 60 + now.getMinutes();
            ranges.each(function () {
                var pthis = $(this);
                var im = uni.str2m(pthis.val().split("-")[1]);
                var li = pthis.parent().parent();
                if (ck && im < m) li.addClass("disabled");
                else li.removeClass("disabled");
            });
        }
    })();
    //------------------------房间状态
    (function () {
        app.onPageInit("p-roomstat", function (page) {
            //初始化公共参数
            var query = page.query;
            if (query.classkind) global.curClassKind = query.classkind;
            //初始化日期选择控件
            $(".stat_date", page.container).val((new Date()).format("yyyy-MM-dd"));
            var cld = app.calendar(datePicker($("#room_stat_picker"), {}, function (dt) {
                var flg = $(".flag_today", page.container);
                if (dt == (new Date()).format("yyyy-MM-dd")) flg.show();
                else flg.hide();
                cld.value = [uni.parseDate(dt)];
                loadRooms(page);
            }));
            loadRooms(page);
        })
        function loadRooms(page) {
            var con = page.container;
            var para = page.query;
            //参数
            para.date = $(".stat_date", con).val();
            var picker = $(".stat_time", con);
            if (picker.hasClass("inited")) {
                para.start = $(".stat_time_start", con).val();
                para.end = $(".stat_time_end", con).val();
            }
            else {
                var dftTime = new Date();
                var diff = Math.ceil(dftTime.getMinutes() / 10) * 10 - dftTime.getMinutes();
                if (diff) dftTime.addMinutes(diff);
                var tstart = pro.dt.timeFull(dftTime.getHours() + ":" + dftTime.getMinutes());
                if (global.config.resvInterval) dftTime.addMinutes(global.config.resvInterval);
                var tend = pro.dt.timeFull(dftTime.getHours() + ":" + dftTime.getMinutes());
                para.start = tstart;
                para.end = tend;
            }
            pro.j.rm.getRmSta(para, function (rlt) {
                rlt = rlt.data;
                var isAllDay = global.config.allDayState * 8 > 0;
                // if (!picker.hasClass("inited") && rlt.length > 0) {--
                if (rlt.length > 0) {
                    var d = rlt[0].roomStat;
                    var bg = d.dwOpenBegin;
                    var en = d.dwOpenEnd;
                    $(".open_time_info", con).html(uni.translate("开放时间") + ":" + pro.dt.timeFull(bg) + uni.translate(" - ") + pro.dt.timeFull(en));
                    
                    var vOpenEndTimeTemp = para.end;
                    if (global.config.resvInterval == 0) {
                        var vOpenEndTimeTempMin=(en % 100);
                        if(vOpenEndTimeTempMin<10)
                        {
                            vOpenEndTimeTempMin="0"+vOpenEndTimeTempMin;
                        }
                        vOpenEndTimeTemp = parseInt(en / 100) + ":" + vOpenEndTimeTempMin;
                        
                    }

                    picker.initTimePicker(app, {
                        uint: global.config.resvUnit,
                        openStart: bg, openEnd: en, start: para.start, end: vOpenEndTimeTemp, isAllDay: isAllDay,
                        dateInput: $(".stat_date", con),
                        startInput: $(".stat_time_start", con), endInput: $(".stat_time_end", con),
                        onClose: function () {
                            loadRooms(page);
                        }
                    });
                    isAllDay && picker.attr("disabled", "disabled");
                }
                //var pr="&classkind=" + para.classkind + "&date=" + para.date +"&start="+para.start+"&end="+para.end;
                var staList = $("#room_stat_list");
                var srp = $.serializeObject(para);
                if (!staList.hasClass("inited")) {
                    staList.addClass("inited");
                    var byLab = (global.config.seatRscMode & 32) > 0;
                    var list = "";
                    for (var i = 0; i < rlt.length; i++) {
                        if (byLab && (i == 0 || rlt[i].labId != rlt[i - 1].labId))//二级分类
                        {
                            list += '<li class="accordion-item"><a href="#" class="item-content item-link"><div class="item-inner"><div class="item-title">' + rlt[i].labName + '</div>'
                                + '</div></a><div class="accordion-item-content"><div class="content-block"><div class="list-block media-list"><ul>';
                        }
                        var item = rlt[i].roomStat;
                        var url = "../a/resvstat.aspx?right=detail&fr_all_day=" + (global.config.allDayState * 8 > 0 ? "true" : "false") + "&room_id=" + item.dwRoomID + "&name=" + item.szRoomName +
                            "&open_start=" + item.dwOpenBegin + "&open_end=" + item.dwOpenEnd;
                        list += '<li class="rm_item rm_' + rlt[i].id + '"><a url="' + url + '" para="' + srp + '" data-id="' + item.dwRoomID + '" class="item-link item-content">' +
                '<div class="item-inner">' +
                  '<div class="item-title-row">' +
                    '<div class="item-title">' + item.szRoomName + '</div>' +
                    '<div class="item-after"><span class="green">' + item.dwUsableNum + '</span>/' + item.dwTotalNum + '</div></div>' +
                  '</div></a></li>';
                        if (byLab && (i == rlt.length - 1 || rlt[i].labId != rlt[i + 1].labId)) {
                            list += "</ul></div></div></div></li>";
                        }
                    }
                    if (byLab) {
                        staList.html("<div class='list-block accordion-list touch_top'><ul>" + list + "</ul>");
                    }
                    else {
                        staList.html("<div class='list-block media-list touch_top'><ul>" + list + "</ul></div>");
                    }
                    staList.find("li.rm_item a").click(function () {
                        var pthis = $(this);
                        var url = pthis.attr("url") + "&" + pthis.attr("para");
                        view.loadPage(url);
                    });
                }
                else {
                    for (var i = 0; i < rlt.length; i++) {
                        var li = staList.find(".rm_" + rlt[i].id);
                        li.children("a").attr("para", srp);
                        li.find(".item-after").html("<span class='green'>" + rlt[i].roomStat.dwUsableNum + "</span>/" + rlt[i].roomStat.dwTotalNum);
                    }
                }
                //var list = app.virtualList("#room_stat_list", {
                //    height: 44,
                //    items: rlt.data,
                //    renderItem: function (index, it) {
                //        var item = it.roomStat;
                //        var url = "../a/resvstat.aspx?right=detail&classkind=8&room_id=" + item.dwRoomID + "&name=" + item.szRoomName +
                //            "&open_start=" + item.dwOpenBegin + "&open_end=" + item.dwOpenEnd + $.serializeObject(para);
                //        return '<li><a href="' + url + '" data-id="' + item.dwRoomID + '" class="item-link item-content">' +
                //'<div class="item-inner">' +
                //  '<div class="item-title-row">' +
                //    '<div class="item-title">' + item.szRoomName + '</div>' +
                //    '<div class="item-after"><span class="green">' + item.dwUsableNum + '</span>/' + item.dwTotalNum + '</div></div>' +
                //  '</div></a></li>';
                //    }
                //});
            });
        }
    })();
    //------------------------申请页面
    (function () {
        var pResvsub;
        var resvsubOrientationHandle
        app.onPageInit("p-resvsub", function (page) {
            var query = page.query;
            var container = $(page.container);
            //初始化
            pResvsub = page;
            container.find(".resv_id").val(query.resv_id || '');//修改
            container.find(".type").val(query.type);
            container.find(".dev_id").val(query.dev || '');
            container.find(".kind_id").val(query.kind || '');
            container.find(".lab_id").val(query.lab);
            container.find(".room_id").val(query.room_id || '');
            container.find(".stat_date").val(query.date);
            initResvStat(query, container);//预约状态
            //主题显示
            var classkind = global.curClassKind;
            if (classkind) {
                var ck = parseInt($("#cfg_resv_theme_kind").val());
                if ((classkind & ck) == 0) $(".tr_theme ", container).hide();
            }
            //提交
            var fm = $("#resvsub_form");
            $("#sub_resvsub_form").touchend(function () {
                if (fm.mustItem()) {
                    var data = app.formToJSON(fm[0]);
                    var dev_name = $(".apply_info", container).html();
                    pro.confirm("<div class='bold'>" + uni.translate("确认预约") + " \"" + dev_name + "\"？</div>" + uni.translate("开始")
                        + "：" + data.start + "<br/>" + uni.translate("结束") + "：" + data.end, function () {
                        pro.j.rsv.setResv(data, function () {
                            pro.msgBox("提交成功", function () {
                                pro.back("p-index", function () {
                                    app.showTab("#tab_state");
                                    app.outTabStat.loadMyResv();
                                    history.replaceState(null, document.title, location.origin + location.pathname);//清空历史状态
                                }, "p-resvsub");
                            });
                        });
                    });
                }
            });
            //横屏事件
            resvsubOrientationHandle = function () {
                if ($(pResvsub.container).hasClass("page-on-center")) {
                    initResvStat();
                }
            };
            window.addEventListener("orientationchange", resvsubOrientationHandle);
        });
        app.onPageBack("p-resvsub", function (page) {
            window.removeEventListener("orientationchange", resvsubOrientationHandle);
        });
        //初始化状态
        var old_para;
        var old_container;
        function initResvStat(para, container) {
            //缓存
            para = para || old_para;
            container = container || old_container;
            old_para = para;
            old_container = container;
            //
            var ds = {};
            ds.date = para.date.replace(/-/g, '');
            if (para.type == "kind") {
                ds.kind_id = para.kind;
                ds.iskind = 'true';
            }
            else {
                ds.dev_id = para.dev;
            }
            pro.j.dev.getRsvSta(ds, function (rlt) {
                var obj = rlt.data[0];
                obj.date = para.date;
                //初始化修改时间
                if (para.start && para.end) {
                    //过滤指定时间 不占据
                    var ts = obj.ts;
                    if (para.alter && ts) {
                        for (var i = 0; i < ts.length; i++) {
                            if (!obj.allowLong && (ts[i].start == para.start && ts[i].end == para.end)
                                || obj.allowLong && (uni.compareDate(ts[i].start, para.start, 'm') >= 0 && uni.compareDate(ts[i].end, para.end, 'm') <= 0)) {
                                obj.ts.splice(i, 1);
                                break;
                            }
                        }
                    }
                    obj.startDate = para.date;//一定要date不能是start
                    obj.endDate = para.end.substr(0, 10);
                    obj.start = para.start;
                    obj.end = para.end;
                }
                var isLong = $("#uni_config .is_allday").val() == "1";
                //预约状态并检查
                var stat = container.find(".obj_resv_stat");
                var slider = pro.stateSlider(obj, { width: stat.width() });
                if (slider) {
                    if (obj.allowLong) {
                        if (isLong) stat.css("min-height", "24px");
                        else stat.css("min-height", "46px");
                        var p = uni.getObj(ds);
                        p.islong = 'true';
                        pro.j.dev.getRsvSta(p, function (rlt2) {
                            var o = rlt2.data[0];
                            o.date = para.date;
                            var psd = uni.parseDate(para.date);
                            o.start = psd.format("yyyyMMdd");
                            o.end = psd.addDays(30).format("yyyyMMdd");
                            var sd = pro.stateSlider(o, { start: para.start, end: para.end });
                            stat.html(sd);
                            if (!isLong)
                                stat.append(slider);
                        });
                    }
                    else
                        stat.html(slider);
                }
                else {
                    if (para.oldDate) container.find(".stat_date").val(para.oldDate);
                    uni.msgBox("所选日期不支持预约");
                    return;
                }
                //固定时段
                if (obj.ops.length > 0 && (obj.ops[0].limit & 2) > 0) {
                    obj.fix = true;
                }
                //if ($("#time_fix").val()) obj.fix = eval("({" + $("#time_fix").val() + "})");
                //预约时间规则
                container.find(".resv_info_time").html(pro.htm.getResvRule(obj));
                //后续操作
                if (container.hasClass("inited")) {//更新
                    pro.md.timeselector(app, obj, container);
                }
                else {//初始化
                    container.addClass("inited");
                    container.find(".apply_user").html(obj.labName + ' · ' + obj.roomName);
                    container.find(".apply_info").html((obj.iskind ? obj.kindName : obj.devName));
                    //预约属性
                    $(".resv_info_prop", container).html(obj.rule);
                    if (obj.cancel) {
                        $(".resv_info_prop .rule_late", container).append("/" + uni.translate('迟到') + "<span class='red'>" + pro.dt.m2dms(obj.cancel) + "</span> " + uni.translate('预约将被取消'));
                    }
                    //成员管理
                    if (obj.maxUser > 1 && !para.resv_id) {
                        container.find(".md_group").show();
                        container.find(".resv_info_group").html(obj.minUser + "-" + obj.maxUser);
                        container.find(".min_user").val(obj.minUser);
                        container.find(".max_user").val(obj.maxUser);
                        //插件
                        var hash = uni.getHash();
                        $(".group_link", container).autocomplete(function (data) {
                            var mbs = hash.size() > 0 ? ("&" + hash.keys().join()) : "";
                            $(".mb_list", container).val(mbs);
                            $(".md_group .item-after", container).html(uni.translate("已选") + hash.size() + uni.translate("人"));
                        }, hash, { min: obj.minUser, max: obj.maxUser, owner: true });
                    }
                    //日期插件
                    
                    var myCalendar = app.calendar(datePicker($("#resv_sub_picker"), {
                        earliest: parseInt(obj.earliest / 1440)
                    }, function (dt) {
                        para.oldDate = para.date;
                        para.date = dt;
                        myCalendar.value = [uni.parseDate(dt)];
                        initResvStat(para, container);
                    }));
                    //时间插件
                    var test = pro.md.timeselector(app, obj);
                    var ulForm = $(".ul_form", container);
                    test.each(function () {
                        ulForm.append(this);
                    });
                    $("#sub_resvsub_form", container).show();
                }
            });
        }
    })();
    //------------------------预约动态
    (function () {
        //登录成功
        $(window).on("app_logined", function () {
            loadMyResv();
        });
        tabState.once("show", function () {
            loadMyResv();
        });
        //下拉刷新
        $(".pull-to-refresh-content").on("refresh", function () {
            if ($("#tab_state").hasClass("active")) {
                loadMyResv();
            }
            app.pullToRefreshDone(this);
        });
        //按钮操作
        //刷新
        $("#tab_state .act_refresh").click(function () {
            loadMyResv();
        });
        //签到
        $("#tab_state .act_checkin").click(function () {
            
            var p = $(this).parent();
            var item = global.curResvInstance;
            if ((global.config.mobileProp & 16) > 0) {
                pro.msgBox("请确保身处允许签到的区域", function () {
                    pro.j.rsv.resvCheckin({ dev_id: item.devId, lab_id: item.labId, resv_id: item.id }, function () {
                        pro.msgBox("签到成功");
                        loadMyResv();
                    });
                });
            }
            else {
                app.showIndicator();
                pro.locationSignIn(function (flg) {
                    app.hideIndicator();
                    if (flg)
                        pro.j.rsv.resvCheckin({ dev_id: item.devId, lab_id: item.labId, resv_id: item.id }, function () {
                            pro.msgBox("签到成功");
                            loadMyResv();
                        });
                    else
                        pro.msgBox("对不起，你不在允许签到的区域。<br/>手机获取地理位置可能有误差，若你已身处可签到区域，请稍后重试。");
                });
            }
        });
        //返回
        $("#tab_state .act_back").click(function () {
            var p = $(this).parent();
            var item = global.curResvInstance;
            pro.j.rsv.resvBack({ dev_id: item.devId, lab_id: item.labId, resv_id: item.id }, function () {
                pro.msgBox("返回成功");
                loadMyResv();
            });
        });
        //离开
        $("#tab_state .act_leave").click(function () {
            var p = $(this).parent();
            var item = global.curResvInstance;
            pro.j.rsv.resvLeave({ dev_id: item.devId, lab_id: item.labId, resv_id: item.id }, function (rlt) {
                pro.msgBox("操作成功，最多离开" + rlt.msg + "分钟");
                loadMyResv();
            });
        });
        //更多操作
        $("#tab_state .act_more").click(function () {
            var item = global.curResvInstance;
            var buttons = [];
            buttons.push({ text: item.devName + " " + item.timeDesc, label: true });
            if ((item.actSN & 4) > 0) {
                buttons.push({
                    text: uni.translate("提前结束"), onClick: function () {
                        pro.j.rsv.finish(item.id, function (rlt) {
                            pro.msgBox(rlt.msg);
                            loadMyResv();
                        });
                    }
                });
            }
            //暂时不支持
            //if ((item.status & 512) > 0) {
            //    buttons.push({
            //        text: "使用延时", onClick: function () {
            //            app.prompt("需延长多少分钟？", "使用延时", function (v) {
            //                var t = parseInt(v || 0);
            //                if (t == NaN || t < 1) { pro.msgBox("输入的内容无效"); return; }
            //                pro.j.rsv.resvExtend({ dev_id: item.devId, lab_id: item.labId, resv_id: item.id, time: t }, function () {
            //                    pro.msgBox("预约延时成功");
            //                    loadMyResv();
            //                });
            //            });
            //        }
            //    });
            //}
            buttons.push({
                text: uni.translate("详细信息"), onClick: function () {
                    item.title = uni.translate("详细信息");
                    item.name = uni.translate(item.name || "预约信息");
                    view.router.load({ url: "../a/resvdetail.aspx", context: item });
                }
            });
            app.actions(this, [buttons, [{ text: uni.translate("取消"), color: "red" }]]);
        });
        //延时
        //$("#tab_state .act_extend").click(function () {
        //    var p = $(this).parent();
        //    app.prompt("需延长多少分钟？", "使用延时", function (v) {
        //        var t = parseInt(v || 0);
        //        if (t == NaN || t == 0) { pro.msgBox("输入的内容无效"); return; }
        //        pro.j.rsv.resvExtend({ dev_id: p.data("dev"), lab_id: p.data("lab"), resv_id: p.data("resv"), time: t }, function () {
        //            pro.msgBox("预约延时成功");
        //            loadMyResv();
        //        });
        //    });
        //});
        //加载我的预约列表
        function loadMyResv() {
            pro.j.rsv.getMyResv(function (rlt) {
                updateCurrent(rlt.data);
                if (!$("#tab_state").hasClass("active")) return;//防虚拟列表bug 不激活不加载
                var validList = [], preList = [];
                for (var i = 0; i < rlt.data.length; i++) {
                    var it = rlt.data[i];
                    if ((it.status & 512) > 0)
                        validList.push(it);
                    else
                        preList.push(it);
                }
                if (rlt.data.length > 0) {
                    $("#no_resv_emotion").hide();
                }
                else {
                    $("#no_resv_emotion").show();
                }
                //已生效
                var curList = $("#valid_resv_list");
                var str = '';
                for (var i = 0; i < validList.length; i++) {
                    var item = validList[i];
                    str += '<li data-id="' + item.id + '" class="item-content ' + (global.curValidResv ? (global.curValidResv == item.id ? 'valid_selected' : '') : (i == 0 ? 'valid_selected' : '')) +
                        '"><div class="item-inner">' +
                  '<div class="item-title-row">' +
                    '<div class="item-title">' + item.devName + '(' + item.labName + ')</div>' +
                    '<div class="item-after theme-color">' + uni.translate('当前预约') + '</div></div>' +
                  '<div class="item-subtitle">' + item.timeDesc + '</div>' +
                  '<div class="item-text">' + item.members + '</div></div></li>';
                }
                curList.html(str);
                if (validList.length == 0) global.curValidResv = null;
                curList.find("li").click(function () {
                    var pthis = $(this);
                    if (!pthis.hasClass("valid_selected")) {
                        curList.find("li.valid_selected").removeClass("valid_selected");
                        global.curValidResv = pthis.data("id");
                        pthis.addClass("valid_selected");
                        updateCurrent(rlt.data);
                    }
                });
                //未生效
                var myList = $("#my_resv_list");
                var pre = '';
                for (var i = 0; i < preList.length; i++) {
                    var item = preList[i];
                    pre += '<li><a href="#" data-id="' + item.id + '" class="item-link item-content">' +
                    '<div class="item-inner">' +
                    '<div class="item-subtitle" style="margin-bottom:5px">' + item.timeDesc + '<span class="pull-right">' + item.state + '<span></div>' +
    '<div class="item-title-row">' +
    '<div class="item-title">' + item.devName + '(' + item.labName + ')</div>' +
    '<div class="item-after">更多</div></div>' +
    '<div class="item-text">' + item.members + '</div></div></a></li>';
                }
                myList.html(pre);
                pro.transPage(myList);
                if (pre) {
                    $("#tab_state .my_resv_list_panel").show();
                }
                else {
                    $("#tab_state .my_resv_list_panel").hide();
                }
                if (preList.length > 0) {
                    //列表
                    $("#my_resv_list a.item-link").click(function () {
                        var index = -1;
                        var item;
                        for (var i = 0; i < preList.length; i++) {
                            if ($(this).data("id") == preList[i].id) {
                                item = preList[i];
                                index = i;
                                break;
                            }
                        }
                        var buttons = [];
                        buttons.push({ text: item.devName + " " + item.timeDesc, label: true });
                        if ((item.actSN & 1) > 0) {
                            buttons.push({
                                text: uni.translate("删除"), onClick: function () {
                                    pro.confirm("确定删除预约？", function () {
                                        pro.j.rsv.delResv(item.id, function () {
                                            loadMyResv();
                                        });
                                    });
                                }
                            });
                        }
                        if ((item.actSN & 2) > 0) {
                            buttons.push({
                                text: uni.translate("修改时间"), onClick: function () {
                                    var it = item;
                                    var query = { date: item.start.substr(0, 10), start: item.start, end: item.end, lab: item.labId, resv_id: item.id, alter: true };
                                    if (item.devId) {
                                        query.type = 'dev';
                                        query.dev = item.devId;
                                    }
                                    else {
                                        query.type = 'kind';
                                        query.kind = item.kindId;
                                        query.room_id = item.roomId;
                                    }
                                    view.loadPage({
                                        url: "../a/resvsub.aspx",
                                        query: query
                                    });
                                }
                            });
                        }
                        if ((item.actSN & 8) > 0) {
                            
                          var vInfo=  uni.translate("成员维护");
                            buttons.push({
                                text: uni.translate("成员维护"), onClick: function () {
                                    pro.j.group.getMbs(item.groupId, function (rlt) {
                                        var list = rlt.data;
                                        var hash = uni.getHash();
                                        for (var i = 0; i < list.length; i++) {
                                            var mb = list[i];
                                            hash.set(mb.dwAccNo, {
                                                id: mb.dwAccNo,
                                                name: mb.szTrueName,
                                                label: mb.szTrueName + "(" + mb.szDeptName + ")"
                                            });
                                        }
                                        hash.onSet = function (key, v) {
                                            pro.j.group.addMbsByAccNo(item.groupId, key, function () { });
                                        };
                                        hash.onRemove = function (key, v) {
                                            pro.j.group.delMemByAccNo(item.groupId, key, function () { });
                                        };
                                        pro.autocomplete("searchAccount.aspx", function () {
                                            loadMyResv();
                                        }, hash, { min: item.minUser, max: item.maxUser, limit: true, owner: true });
                                    });
                                }
                            });
                        }
                        buttons.push({
                            text: uni.translate("详细信息"), onClick: function () {
                                item.title = uni.translate("详细信息");
                                item.name = uni.translate(item.name || "预约信息");
                                view.router.load({ url: "../a/resvdetail.aspx", context: item });
                            }
                        });
                        var groups = [[{ text: "ok" }, { text: "good", onClick: function () { pro.msgBox(item.id) } }], ];
                        app.actions(this, [buttons, [{ text: uni.translate("取消"), color: "red" }]]);
                    });
                }
            }, { stat_flag: 9 });
        }
        //当前预约
        function updateCurrent(items) {
            $("#cur_resv_num").html(items.length);
            var tab = $("#tab_state")
            if (tab.hasClass("active")) {
                var rmd = $("#my_resv_remind");
                var specialty;
                //var first = true;
                for (var i = 0; i < items.length; i++) {
                    var it = items[i];
                    if (global.curValidResv) {
                        if (global.curValidResv == it.id) {
                            specialty = it;
                            break;
                        }
                    }
                    else {
                        if ((it.status & (256 + 512)) > 0) {//等待or生效
                            //if (first) {
                            //    first = false;
                            //    specialty = it;
                            //}
                            //if (uni.compareDate(it.end, it.start) > 0) {
                            //    continue;
                            //}
                            //else {
                            specialty = it;
                            break;
                            //}
                        }
                    }
                }
                tab.find(".act_item").hide();
                if (!uni.isNull(specialty)) {//提醒内容
                    global.curResvInstance = specialty;
                    var it = specialty;
                    var str = it.timeDesc.substr(0, 5);
                    if (str == (new Date()).format("MM/dd")) str = it.timeDesc.substr(6, 5);
                    rmd.find(".item-title").html(str);
                    rmd.find(".item-after").html(it.state);
                    rmd.find(".item-subtitle").html(it.timeDesc);
                    rmd.find(".item-text").html(it.devName + " " + it.labName);
                    //
                    var panel = tab.find(".cur_resv_act");
                    pro.j.dev.getUseStat(function (rlt) {
                        if (rlt.data.length > 0) {
                            var data = rlt.data[0];
                            var it_title = rmd.find(".item-title");
                            var it_text = rmd.find(".item-text");
                            if (data.deadline) {
                                var deadline = data.deadline;
                                if (uni.compareDate(it.end, data.deadline, "m") < 0) deadline = it.end;
                                it_text.append(pro.transPick("<br/>请于 " + deadline.substr(11) + " 前返回"));
                            }
                            if ((it.status & 512) > 0) {//已生效
                                panel.find(".act_more").show();//todo 更多操作

                                if ((global.config.mobileProp & 8) > 0) {//支持签到
                                    var sta = uni.translate("生效中");
                                    //((it.status & 256) > 0 && (uni.compareDate(uni.parseDate(it.start), new Date(), "m") < 10)) || 提前10分钟未生效or
                                    if ((it.status & 131072) == 0) {//未签到
                                        sta = uni.translate("未签到");
                                        if ((data.classkind & 9) > 0) {//座位+房间
                                            panel.find(".act_checkin").show();//todo 签到
                                        }
                                    }
                                    else if ((data.code & 256) > 0) {//暂时离开
                                        sta = uni.translate("暂时离开");
                                        if ((data.classkind & 8) > 0) {
                                            panel.find(".act_back").show();//todo 返回使用
                                        }
                                    }
                                    else {
                                        sta = uni.translate("使用中");
                                        if ((data.classkind & 8) > 0) {
                                            panel.find(".act_leave").show();//todo 暂时离开
                                        }
                                    }
                                    if (sta) it_title.html(sta);
                                }
                                else if (data.status) {
                                    it_title.html($(data.status).text());
                                }
                            }
                        }
                    }, { resv_id: it.id });
                }
                else {
                    rmd.find(".item-title").html(uni.translate('提醒'));
                    rmd.find(".item-after").html('');
                    rmd.find(".item-subtitle").html('');
                    rmd.find(".item-text").html(uni.translate('没有已生效或即将生效的预约'));
                }
            }
        }
        //对外接口
        app.outTabStat = { loadMyResv: loadMyResv }
    })();
    //------------------------个人中心
    (function () {
        //标记坐标
        $("#tab_center .mark_coords").click(function () {
            var pthis = $(this);
            //pthis.addClass("disabled");
            //setTimeout(function () { pthis.removeClass("disabled") }, 1000);
            app.showIndicator();
            uni.getLocation(function (lon, lat) {
                app.hideIndicator();
                var dt = new Date;
                var tm = dt.format("yyyy-MM-dd HH:mm:ss");
                pro.j.util.setXmlData(tm + ">>经纬度：" + lon + "," + lat, dt.getTime(), "cfg_coords", function () {
                    pro.msgBox("标记坐标成功<br/>时间：" + tm);
                });
            });
        });
        //联系方式
        app.onPageInit("cc-contact", function () {
            var contact = $("#page_contact");
            $(".click_update_contact", contact).click(function () {
                var para = {};
                var receive = $(".user_receive", contact);
                var phone = $(".user_phone", contact).val();
                var email = $(".user_email", contact).val();
                if (receive.length > 0) para.note_alert = receive.is(":checked");
                if (phone.length == 0) { pro.msgBox("手机号不能为空！"); return false; }
                else if (!uni.ckMobile(phone)) { pro.msgBox("手机填写有误！"); return false; }
                if (email.length == 0) { pro.msgBox("邮箱不能为空！"); return false; }
                else if (!uni.ckEmail(email)) { pro.msgBox("邮箱填写有误！"); return false; }
                pro.j.acc.upContact(phone, email, function () {
                    pro.msgBox("保存成功", function () {
                        pro.j.lg.initAcc(function () {
                            view.router.back();
                        });
                    });
                }, para);
            });
        });
        //修改密码
        app.onPageInit("cc-pwd", function () {
            var change = $("#page_pwd");
            $(".click_change_pwd", change).click(function () {
                var old = $(".old_pwd", change).val();
                var pwd1 = $(".new_pwd1", change).val();
                var pwd2 = $(".new_pwd2", change).val();
                pro.j.acc.changePwd(old, pwd1, pwd2, function () {
                    pro.msgBox("修改密码成功", function () {
                        view.router.back();
                        change.find("input[type=password]").val("");
                    });
                });
            });
        });
    })();
    //-----------------------活动安排详情
    (function () {
        app.onPageInit("p-atydetail", function (page) {
            var container = page.container;
            var url = page.url.substring(0, page.url.indexOf("&_t="));
            $(".btn_mb", container).click(function () {
                var pthis = $(this);
                var gid = pthis.attr("gid");
                var purpose = pthis.attr("purpose");
                if (gid) {
                    if (purpose == "in")
                        pro.j.group.addMbsByAccNo(gid, pro.acc.accno, function () {
                            uni.msgBox("加入成功", "", function () {
                                view.router.reloadPage(url + "&_t=" + (new Date()).getTime());
                            });
                        });
                    else
                        pro.j.group.delMemByAccNo(gid, pro.acc.accno, function () {
                            uni.msgBox("退出成功", "", function () {
                                view.router.reloadPage(url + "&_t=" + (new Date()).getTime());
                            });
                        });
                }

            });
        });
    })();

    //-------------------------一些共用的方法

    //日期选择器
    function datePicker(selector, para, callback) {
        para = para || {};
        var input = selector.find("input");
        var minDate = (new Date()).addDays(para.latest ? (para.latest - 1) : -1);
        var maxDate = para.earliest ? ((new Date()).addDays(para.earliest)) : undefined;
        var cldPara = $.extend(pro.getCldPara(), {
            input: input[0],
            minDate: minDate,
            maxDate: maxDate,
            onClose: function () {
                callback(input.val());
            }
        });
        selector.find(".add").click(function () {
            var here = uni.parseDate(input.val()).addDays(1);
            if (!maxDate || uni.compareDate(here, maxDate) <= 0) {
                var dt = here.format("yyyy-MM-dd");
                input.val(dt);
                callback(dt);
            }
        });
        selector.find(".minus").click(function () {
            var here = uni.parseDate(input.val()).addDays(-1);
            if (uni.compareDate(here, minDate) > 0) {
                var dt = here.format("yyyy-MM-dd");
                input.val(dt);
                callback(dt);
            }
        });
        return cldPara;
    }

    //绘制预约状态平面图
    function paintFloorPlan(panel, para) {
        if (!panel.hasClass("inited")) {
            panel.addClass("inited");
            pro.j.dev.getDevCoord(para, function (rlt) {
                var data = rlt.data;
                var objs = panel[0].objs = data.objs;
                var cw = document.body.clientWidth;
                var ch = parseInt(cw * parseInt(data.height) / parseInt(data.width));
                panel.html("<div calss='fp-zoom' style='overflow:hidden;position: relative;width:" + cw + "px;height:" + (ch > cw ? cw : ch) + "px'><div class='fp-content' style='position:absolute;top:0;left:0;height: " + ch + "px; width: " + cw + "px;' stat='min' minheight='" + ch + "px' minwidth='" + cw + "px' maxheight='" + data.height + "px' maxwidth='" + data.width + "px'>" +
                "<img src='../../upload/DevImg/FloorPlan/rm" + para.room_id + ".jpg' style='width:100%;height:100%;'/><svg xmlns='http://www.w3.org/2000/svg' class='fp-user-con' style='width:100%;height:100%;position:absolute;top:0;left:0;' viewBox='0 0 " + data.width + " " + data.height + "'></svg></div></div>");
                var con = panel.find(".fp-content");
                fpDrag(con);
                //双击缩放  默认两倍
                var maxh = ch * 2;//parseInt(data.height);
                var maxw = cw * 2;//parseInt(data.width);
                var minh = ch;
                var minw = cw;
                var w_ratio = (maxw - minw) / (maxh - minh);
                con[0].addEventListener("touchstart", function (event) {
                    if (this.dbl && this.dbl == 2) {
                        var stat = con.attr("stat");
                        //宽高参数
                        var h = con.height();
                        var w = con.width();
                        var d_h = 8;//越大越快
                        var d_w = d_h * w_ratio;
                        if (stat == "min") {//放大
                            con.attr("stat", "max");
                            //放大参数
                            var offsetX = event.touches[0].clientX - this.offsetParent.offsetLeft;//event.offsetX;
                            var offsetY = event.touches[0].clientY - this.offsetParent.offsetTop;//event.offsetY;
                            var top = 0;
                            var left = 0;
                            var moveX = minw / 2 - offsetX * (maxw / minw);
                            if (moveX > 0) moveX = 0;
                            else if (moveX < (minw - maxw)) moveX = minw - maxw;
                            var moveY = minh / 2 - offsetY * (maxh / minh);
                            if (moveY > 0) moveY = 0;
                            else if (moveY < (minh - maxh)) moveY = minh - maxh;
                            var d_top = d_h * (moveY / (maxh - minh));
                            var d_left = d_h * (moveX / (maxh - minh));
                            //放大动画
                            var fg = setInterval(function () {
                                h += d_h;
                                w += d_w;
                                top += d_top;
                                left += d_left;
                                con.css({ height: h + "px", width: w + "px", top: top + "px", left: left + "px" });
                                if (h == maxh || h > maxh) {
                                    clearInterval(fg);
                                }
                            }, 1);
                        }
                        else {//缩小
                            con.attr("stat", "min");
                            //缩小参数
                            var top = parseInt(con.css("top"));
                            var left = parseInt(con.css("left"));
                            var d_top = d_h * (top / (maxh - minh));
                            var d_left = d_h * (left / (maxh - minh));
                            var fg = setInterval(function () {
                                h -= d_h;
                                w -= d_w;
                                top -= d_top;
                                left -= d_left;
                                con.css({ height: h + "px", width: w + "px", top: top + "px", left: left + "px" });
                                if (h == minh || h < minh) {
                                    con.css({ height: minh + "px", width: minw + "px", top: "0", left: "0" });
                                    clearInterval(fg);
                                }
                            }, 1);
                        }
                    }
                    else {
                        var p = this;
                        p.dbl = 2;
                        setTimeout(function () {
                            p.dbl = 1;
                        }, 300);
                    }
                });
                refreshStat(para, objs, panel.find(".fp-user-con"));
                //隐藏提交框
                $("#ic_resv_stat .click_cancel").click(function () {
                    clearFilter(panel.find(".fp-user-con"));
                    $("#ic_resv_stat .floor_fixed_sub").css("bottom", "-160px");
                });
            });
        }
        else {
            refreshStat(para, panel[0].objs, panel.find(".fp-user-con"));
        }
    }
    function refreshStat(para, objs, con) {
        pro.j.dev.getRsvSta(para, function (rlt) {
            var list = rlt.data;
            var str = '';
            var dots = cvtUniLab3(list, objs);
            for (var i = 0; i < dots.length; i++) {
                createDot(dots[i], para, con[0]);
            }
            var clk = $("#ic_resv_stat .click_sel_apply");
            var span = $("#ic_resv_stat .sel_intro");
            var gs = con.find("g");
            gs.touchstart(function () {
                clearFilter(con);
                var cls = this.getAttribute("class");
                if (cls.indexOf("selected") < 0) {
                    this.setAttribute("class", cls + " selected");
                    this.setAttribute("fill-opacity", "1");
                    $("circle", this)[0].setAttribute("filter", "url(#fp-drop-shadow)");
                }
                con[0].selected = this.getAttribute("key");
                span.html(uni.translate("已选") + " <span class='theme-color'>" + this.getAttribute("title") + "</span>");
                var sub = $("#ic_resv_stat .click_sel_apply");
                if (cls.indexOf("stat-ok") < 0) {
                    sub.addClass("disabled");
                    sub.html(uni.translate("不可预约") + (para.fr_all_day == "true" ? "" : (" " + para.fr_start + "-" + para.fr_end)));
                }
                else {
                    sub.removeClass("disabled");
                    sub.html(uni.translate("确认预约") + (para.fr_all_day == "true" ? "" : (" " + para.fr_start + "-" + para.fr_end)));
                }
                $("#ic_resv_stat .floor_fixed_sub").css("bottom", "20px");
            });
            clearFilter(con);
            $("#ic_resv_stat .floor_fixed_sub").css("bottom", "-160px");
        });
    }
    function clearFilter(con) {
        con.find("g").each(function () {
            var tm = this.getAttribute("class");
            if (tm.indexOf("selected") > 0) {
                this.setAttribute("class", tm.replace(" selected", ""));
                this.setAttribute("fill-opacity", "0.6");
                $("circle", this)[0].removeAttribute("filter");
            }
        });
    }
    function fpDrag(z) {
        var fa = z.parent();
        var min_left;
        var min_top;
        var o_top;
        var o_left;
        var o_tchs;
        var o_x;
        var o_y;
        var last_len;
        var min_h = parseInt(z.attr("minheight"));
        var min_w = parseInt(z.attr("minwidth"));
        var max_h = parseInt(z.attr("maxheight"));
        var max_w = parseInt(z.attr("maxwidth"));
        var d_height = 6;
        var d_width = d_height * (max_w - min_w) / (max_h - min_h);
        z.touchstart(function (event) {
            min_left = fa.width() - z.width();
            min_top = fa.height() - z.height();
            last_len = 0;
            if (min_left > 0) min_left = 0;
            if (min_top > 0) min_top = 0;
            o_x = event.touches[0].clientX;
            o_y = event.touches[0].clientY;
            o_top = parseInt(z.css("top"));
            o_left = parseInt(z.css("left"));
        });

        z.touchmove(function (event) {
            if (event.touches.length == 1) {
                var tch = event.touches[0];
                var _x = tch.clientX - o_x;
                var _y = tch.clientY - o_y;
                var top = o_top + _y;
                var left = o_left + _x;
                if (top > 0) { top = 0; o_top = 0; }
                else if (top < min_top) { top = min_top; o_top = min_top; }
                if (left > 0) { left = 0, o_left = 0; }
                else if (left < min_left) { left = min_left; o_left = min_left; }
                z.css("top", top + "px");
                z.css("left", left + "px");
            }
            else if (event.touches.length == 2) {
                var tch0 = event.touches[0];
                var tch1 = event.touches[1];
                var diff_x = Math.abs(tch1.clientX - tch0.clientX);
                var diff_y = Math.abs(tch1.clientY - tch0.clientY);
                var len = Math.sqrt(diff_x * diff_x + diff_y * diff_y);
                if (last_len > 0) {
                    var h = z.height();
                    var w = z.width();
                    if (len < last_len) {
                        if (h > min_h) {
                            var top = parseInt(z.css("top"));
                            var left = parseInt(z.css("left"));
                            var d_top = d_height * top / (h - min_h);
                            var d_left = d_height * left / (h - min_h);
                            z.css({ height: (h - d_height) + "px", width: (w - d_width) + "px", top: (top - d_top) + "px", left: (left - d_left) + "px" });
                        }
                        else {
                            z.css({ top: "0", left: "0" });
                        }
                    }
                    else {
                        if (h < max_h && w < max_w) {
                            z.css("height", (h + d_height) + "px");
                            z.css("width", (w + d_width) + "px");
                        }
                    }
                }
                last_len = len;
            }
            if (z.height() != min_h)
                event.preventDefault();
        });
        z.touchend(function (event) {
            var min_top = min_h - z.height();
            var min_left = min_w - z.width();
            var top = parseInt(z.css("top"));
            var left = parseInt(z.css("left"));
            if (top > 0) top == 0;
            else if (top < min_top) top = min_top;
            if (left > 0) left == 0;
            else if (left < min_left) left = min_left;
            z.css({ top: top + "px", left: left + "px" });
        });
    }
    function createDot(dot, para, con) {
        var dt = para.date;
        var isday = para.fr_all_day == "true";
        var start = isday ? dot.open[0] : para.fr_start;
        var end = isday ? dot.open[1] : para.fr_end;
        var sz = parseInt(dot.size);
        var title = dot.name;
        var r = sz / 2;
        var x = parseInt(dot.left) + r;
        var y = parseInt(dot.top) + r;
        var sta = "close";
        //扇形
        var path;
        if (dot.state != "close") {
            if (dot.freeSta == 0) {
                sta = "ok";
            }
            else if (dot.freeSta > -2 && dot.freeTime == 0) {
                sta = "busy";
            }
            else if (dot.freeTime > 0) {
                if (isday) {
                    var now = new Date();
                    var st = uni.parseDate(dt + " " + start);
                    if (uni.compareDate(now, st, 'm') > 0) {//起始时间比当前早
                        start = now.format("HH:mm");
                    }
                }
                sta = "ok";
                var d;
                var dv = parseInt(dot.freeTime * 100 / uni.compareDate(dt + " " + end, dt + " " + start, 'm'));
                if (dv < 25) {
                    d = "M" + x + " " + y + " L" + x + " " + (y - r) + " A" + r + " " + r + " 0 1 1 " + (x - r) + " " + y + " Z";
                }
                else if (dv > 24 && dv < 50) {
                    d = "M" + x + " " + y + " L" + x + " " + (y - r) + " A" + r + " " + r + " 0 0 1 " + x + " " + (y + r) + " Z";
                }
                else {
                    d = "M" + x + " " + y + " L" + x + " " + (y - r) + " A" + r + " " + r + " 0 0 1 " + (x + r) + " " + y + " Z";
                }
                title += "(" + uni.translate("空闲") + " " + pro.dt.m2ms(dot.freeTime) + ")";
                path = document.createElementNS("http://www.w3.org/2000/svg", "path");
                path.setAttribute("class", "fp-dot-busy");
                path.setAttribute("d", d);
            }
        }
        //圆点
        var circle = document.createElementNS("http://www.w3.org/2000/svg", "circle");
        circle.setAttribute("class", "fp-dot-" + sta);
        circle.setAttribute("cx", x);
        circle.setAttribute("cy", y);
        circle.setAttribute("r", r);
        //容器
        var g = document.createElementNS("http://www.w3.org/2000/svg", "g");
        g.setAttribute("key", dot.id);
        g.setAttribute("fill-opacity", dot.id == con.selected ? " 1" : "0.6");
        g.setAttribute("class", "fp-g stat-" + sta + " fp-g-" + dot.id + (dot.id == con.selected ? " selected" : ""));
        g.setAttribute("para", "kind=" + dot.kindId + "&dev=" + dot.devId + "&lab=" + dot.labId + "&type=" + (dot.iskind ? "kind" : "dev"));
        g.setAttribute("stat", sta);
        g.setAttribute("title", title);
        if (dot.id == con.selected) circle.setAttribute("filter", "url(#fp-drop-shadow)");
        g.appendChild(circle);
        if (!uni.isNull(path)) g.appendChild(path);
        $(".fp-g-" + dot.id, con).remove();
        con.appendChild(g);
    }
    //值转换
    function cvtUniLab3(list, cs) {
        var boxs = [];
        for (var i = 0; i < list.length; i++) {
            var box = list[i];
            if ((box.prop & 524288) > 0) continue;//不支持预约
            if (box.state == "close" && $(".is_show_close").val() != "1") {
                continue;
            }
            if (box.state == "forbid") box.state = "close";//禁用按不开放处理
            box.id = box.devId;
            box.name = box.devName;
            //开放时间
            var open = box.open;
            if (open && open.length > 1) {
                box.openStart = open[0];
                box.openEnd = open[1];
            }
            //坐标
            for (var j = 0; j < cs.length; j++) {
                var c = cs[j];
                if (c.id == box.id) {
                    if (c.size && (c.top || c.left)) {
                        box.top = c.top || 0;
                        box.left = c.left || 0;
                        box.size = c.size;
                        boxs.push(box);
                    }
                    break;
                }
            }
        }
        return boxs;
    }
    //绘制预约状态列表
    function paintResvTbl(panel, para) {
        pro.j.dev.getRsvSta(para, function (rlt) {
            var objs = rlt.data;
            panel.html('');
            var clsRooms = ($(".filter_cls_rooms").length > 0) ? $(".filter_cls_rooms").val() : "";//临时 过滤不开放房间
            for (var i = 0; i < rlt.data.length; i++) {
                var item = rlt.data[i];
                if (item.freeSta) continue;
                //临时 过滤不开放的房间
                if (clsRooms && item.roomId) {
                    if (clsRooms.indexOf(item.roomId) > -1) {
                        continue;
                    }
                }
                //
                //开放活动
                if (global.config.openAty == 2 && item.type != "kind" && item.prop && (parseInt(item.prop) & 65536) > 0) {
                    //disabled = "disabled";
                    //item.devName = item.devName + "(请到电脑端预约)";
                    continue;
                }
                item.date = para.date;
                var slider = pro.stateSlider(item, { width: panel.width() - 40, start: para.fr_start, end: para.fr_end, showClose: global.config.showClose });
                if (slider) {
                    slider = $('<div class="card-content-inner">' + slider + '</div>');
                    var disabled = !item.open && !item.islong ? 'disabled' : '';
                    var url = "../a/resvsub.aspx";
                    var p = { date: item.date, kind: item.kindId, dev: item.devId, room_id: item.roomId, lab: item.labId, start: para.fr_start, end: para.fr_end, type: (item.iskind ? 'kind' : 'dev') };
                    if (!disabled) {//操作
                        slider[0].query = p;
                        if (item.islong) {
                            slider.find(".sb-free").click(function () {
                                var query = uni.getObj($(this).parents(".card-content-inner")[0].query);
                                $(this).addClass("enable-click");
                                var dt = $(this).data("date");
                                query.date = dt;
                                view.loadPage({ url: url, query: query });
                            });
                        }
                        else {
                            slider.find(".ss-td-h").click(function () {
                                var query = uni.getObj($(this).parents(".card-content-inner")[0].query);
                                $(this).addClass("enable-click");
                                var h = $(this).data("h");
                                query.start = h + ":00";
                                query.end = (parseInt(h) + 1) + ":00";
                                view.loadPage({ url: url, query: query });
                            });
                        }
                    }
                    var user_num = item.minUser == item.maxUser ? item.minUser : (item.minUser + '-' + item.maxUser);
                    var it = $('<div class="card resv_card">' +
      '<div class="card-header"><a ' + disabled + ' href="' + url + '?' + $.serializeObject(p) + '">' +
          '<div class="obj_name">' + (item.iskind ? item.kindName : item.devName) + '</div>' +
          '<div class="obj_position text-ellipsis">' + uni.translate('支持') + user_num + uni.translate('人使用 位于') + item.labName + '</div>' +
          (disabled ? '' : '<span class="icon-only"><i class="icon icon-pullright"></i></span>') + '</a></div>' +
      '<div class="card-content"></div></div>');
                    it.find(".card-content").htmlExt(slider);
                    panel.appendExt(it);
                }
            }
            if (panel.html() == "") panel.html("<div class='text-center'>" + uni.translate("当前无可用的资源") + "</div>");
        });
    }
}