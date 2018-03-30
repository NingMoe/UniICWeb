/*
 * UniCalendar v1.0.0
 * http://www.unifound.net
 * 日期: 2014/2/12
 * 作者: 何昆鹏
 */
(function ($, uni) {
    var defaults = {
        mode: 'd',
        modes: "dwm",
        style: 'dft',//dft：默认 cld： 纯日历  mini：简化版 
        alone: false,//alone：独立版（只显示时间）
        allDay: false,//整日
        operate: 'click',
        ui: false,
        allowHistory: false,//是否允许查看历史
        width: 700,
        cellHeight: 50,
        pctrl: {//列表分页显示 对象 属性分别num trigger triggerHeight （月模式不支持）以num为标准判断是否分页
            trigger: "drop",
            triggerHeight: 120
        },
        objTitleMinWidth: 76,
        borderWidth: 1,
        schedule: false,//日程表
        relative: false,//相对时间
        secnum: 8,
        secTime: [],
        closeDate: undefined,
        dateStart: undefined,
        dateEnd: undefined,
        dayStart: "06:00",
        dayEnd: "21:00",
        startWeek: 1,
        pageNum: 7,
        dayOpt: {
            dayNum: 7,
            snipTime: 1,//必须正整数
            unit: 10,//拖拽操作方式下 每格分钟数
            occupy: true,//是否独占时段
            evBeforeUp: undefined,
            evSelObj: undefined//日表对象点击
        },
        weekOpt: {
        },
        stateSetOpt: {
            deadline: []//从1起  需另起计算实验时间的节次(相对时间)
        },
        objs: [],
        plans: [],
        evSelObj: undefined,//月表对象点击
        evSelPlan: undefined,
        evSelTime: undefined,
        evUpTime: undefined,
        evFinishDraw: undefined,
        evRefState: undefined,//课表状态变更
        evClickSchedule: undefined,//点击课表
        evInitFaile:undefined,//初始化失败
        dateFmt: "yyyy年MM月dd日",
        shortDateFmt: "MM月dd日",
        shortDateFmtY: "yyyy年MM月",
        fullDateFmt: "yyyy年MM月dd日 HH:mm",
        timeFmt: "HH:mm",
        monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
        dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
        dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
        snipName: ['第一节', '第二节', '第三节', '第四节', '第五节', '第六节', '第七节', '第八节', '第九节', '第十节', '第十一节', '第十二节'],
        dayEnShort: ["sun", "mon", "tue", "wed", "thu", "fri", "sat"],
        buttonText: {
            prev: '&nbsp;&#9668;&nbsp;',
            next: '&nbsp;&#9658;&nbsp;',
            prevYear: '&nbsp;&lt;&lt;&nbsp;',
            nextYear: '&nbsp;&gt;&gt;&nbsp;',
            today: '今日',
            month: '月视图',
            week: '周视图',
            day: '日视图'
        },
        titleText: {
            pgpre: '上一页',
            pgnext: '下一页',
            weekpre: '上一周',
            weeknext: '下一周',
            weekorder: '周次',
            week: '周',
            monthorder: '月份'
        },
        language: {
            close: "当前不开放",
            over: "当前不开放",
            waiting: "载入中...",
            time_early: "需早于",
            time_last: "需晚于",
            time_min: "至少",
            time_max: "最长",
            H: "时",
            m: "分",
            rect_out:uni.translate("过期/关闭"),
            rect_doing:uni.translate("已预约")
        },
        buttonIcons: {
            prev: 'circle-triangle-w',
            next: 'circle-triangle-e'
        }
    };

    //公共配置对象
    var opt;
    //公共对象
    var idate;
    var iqz;
    var iobj;
    var redLine;//选时红线
    var stateSet = {
        /*用于课表*/
        tchs: [],//单节次数组 无重复
        tests: [],//实验数（多个单节次组合成一个实验） 数组
        testDic: [],//实验对象集合
        line: [],//临时单节次数组
        addTest: addTest,//添加多实验数、实验数组
        removeTest: removeTest,
        clear: function () {//清空
            if (opt.schedule) {
                this.tchs = [];
                this.tests = [];
                this.testDic = [];
                actTch();
                refCld();
            }
        },
        reset: function () {//重置 保留对象集合
            this.tchs = [];
            this.tests = [];
            actTch();
            refCld();
        }
    }
    //公共语言对象
    var language;
    var funs = {
        option: opt,
        date: idate,
        qzone: iqz,
        stateSet: stateSet,
        //刷新
        uploadCld: function (date, options, mode) {
            if (date) idate = date;
            if (mode) opt.mode = mode;
            if (options) $.extend(true, opt, options);
            initView();
        },
        //跳转周次 需schedule
        setWeeks: setWeeks
    };
    $.fn.uniCalendar = function (options) {
        cld = $(this);
        if (cld.length > 1) { uni.msgBox("只支持一个实例"); return; }
        opt = $.extend(true, {}, defaults, options);
        //语言翻译
        translate();
        //
        //公共内部事件
        uni.lifeEvent.add("ev_window_scroll", function (fun) {
            $(window).scroll(fun);
        });
        //
        if (opt.alone)
            opt.objTitleMinWidth = 0;
        //
        iqz = $("<div class='cld-zone' style='padding:5px 0px;height:auto;width:" + opt.width + "px' onselectstart='return false'/>");
        iqz.hide();
        cld.html(iqz);
        iqz.before("<div class='cld-waiting' style='color:#333;font-size:14px;'>" + opt.language.waiting + "</div>");
        if (opt.beginDay)
            idate = uni.parseDate(opt.beginDay);
        else
            idate = new Date();
        initView();
        redLine = $("<div class='cld-red-line' style='display:none;position:absolute;top:-1000px;left:-1000px;'><div class='title'>00:00</div><div class='arrw'></div></div>");
        cld.after(redLine);
        if (opt.operate == "drag") redLine.show();
        funs.option = opt;
        funs.date= idate;
        funs.qzone = iqz;
        return funs;
    };

    function getObjById(id) {
        var objs = opt.objs;
        var rlt;
        for (var i = 0; i < objs.length; i++) {
            if (objs[i].id == id) {
                rlt = objs[i];
                break;
            }
        }
        return rlt;
    }
    function translate() {
        if (uni.language) {
            for (var key in opt.language) {
                opt.language[key] = uni.translate(opt.language[key]);
            }
            for (var key in opt.buttonText) {
                opt.buttonText[key] = uni.translate(opt.buttonText[key]);
            }
            for (var i = 0; i < opt.dayNames.length; i++) {
                opt.dayNames[i] = uni.translate(opt.dayNames[i]);
            }
            opt.shortDateFmt = uni.translate(opt.shortDateFmt);
        }
    }
    //-----------------view----------------------------

    function initView() {
        language = opt.language;
        stateSet.line = opt.stateSetOpt.deadline;
        var top = $("<div class='cld-top'/>");
        initTop(top);
        iqz.html(top);
        var md = opt.mode;
        var con = $("<div class='cld-zone-" + md + "'/>");
        switch (md) {
            case "d": dayView(con); break;
            case "w": weekView(con); break;
            case "m": monthView(con); break;
        }
        iqz.append(con);
        iqz.append("<div class='cld-pctrl'></div>");
        iqz.addClass("cld-style-" + opt.style);
        iqz.addClass("cld-operate-" + opt.operate);
        initTime(idate);
    }
    function initTop(top) {
        var tr = $("<tr class='cld-t-row'></tr>");
        var today = new Date().format("yyyy.MM.dd");
        tr.append("<td class='cld-top-l'><span>TODAY </span>" + today + "</td>");
        tr.append("<td class='cld-top-c'><ul class='cld-prompt'><li><span class='cld-op-doing cld-rect'/> "+opt.language["rect_doing"]+"</li>" +
            "<li><span class='cld-op-out cld-rect'/> "+opt.language["rect_out"]+"</li></ul></td>");
        var mds = $("<ul class='cld-mode'/>");
        var modes = opt.modes = $.trim(opt.modes);
        if (modes.indexOf(opt.mode) < 0) {
            if (modes == '')
                opt.mode = opt.modes = "d";
            else
                opt.mode = modes.charAt(0);
        }
        for (var i = 0; i < modes.length; i++) {
            var s = modes.charAt(i);
            var bt;
            if (s == "d")
                bt = $("<li class='cld-md-" + s + "'><span>" + opt.buttonText.day + "</span></li>");
            else if (s == "w")
                bt = $("<li class='cld-md-" + s + "'><span>" + opt.buttonText.week + "</span></li>");
            else if (s == "m")
                bt = $("<li class='cld-md-" + s + "'><span>" + opt.buttonText.month + "</span></li>");
            if (bt) {
                bt.click(s, function (ev) {
                    opt.mode = ev.data;
                    idate = new Date();
                    initView();
                });
                if (opt.mode == s)
                    bt.addClass("cld-md-sel");
                mds.append(bt);
            }
        }
        if (modes.length == 1) mds.hide();
        tr.append($("<td style='width:140px;' class='cld-top-r'></td>").append(mds));
        top.append($("<table style='width:100%;'></table>").append(tr));
    }
    //单日模式
    function dayView(con) {
        var dayOpt = opt.dayOpt;
        var h = $("<div class='cld-header'/>");
        var m = $("<div class='cld-body' style='width:100%;'/>");
        //头部
        var n = dayOpt.dayNum;
        var htd = $("<tr class='cld-h-row'/>");
        for (var i = 0; i < n; i++) {
            htd.append("<td class='cld-h-cell cld-h-dt " + (opt.ui ? "ui-state-default ui-corner-top" : "") + "' index='" + i + "'/>");
        }
        var todate = $("<td class='cld-h-cell cld-h-bt'/>");//时期跳转
        if (opt.schedule && !uni.isNull(opt.openWeeks)) {//课表模式 周次选择
            todate.addClass("cld-h-bt-week");
            if (opt.openWeeks) {
                var wk = $("<div class='cld-sel-week'/>").append("<div class='cld-wk-text'>" + opt.titleText.weekorder + "</div>");
                var sel = $("<select class='cld-sel-ctrl' style='width:100%;border:none;'/>").appendTo(wk);
                var td_wk = parseInt(dateToWWD(idate) / 10);
                var iscur = false;
                for (var i = 0; i < opt.openWeeks.length; i++) {
                    var _w=opt.openWeeks[i];
                    if (_w < td_wk) continue;
                    if (_w == td_wk) iscur = true;
                    sel.append("<option value='" + _w + "'>" + _w + opt.titleText.week + "</option>");
                }
                if (!iscur) {//不含本周
                    var v = sel.val();
                    if (v) {
                        v = parseInt(v);
                        var d=idate.getDay();
                        idate.addDays((v - td_wk) * 7 + 1 - (d == 0 ? 7 : d));
                    }
                    else {
                        if (opt.evInitFaile()) opt.evInitFaile("no_available_date");
                        return;
                    }
                }
                todate.html(wk);
                //选择事件
                sel.change(function () {
                    var v = $(this).val();
                    selectWeek(v);
                });
            }
            htd.append(todate);
        }
        else {//普通模式 周次跳转与当日
            //上一周
            var prevb = $("<td class='cld-h-cell cld-h-bt cld-h-bt-prev'/>")
                .html("<div class='cld-bt-prev'><span class='cld-prev-txt'>" + opt.buttonText.prev + "</span></div>");
            htd.append(prevb);
            //今天
            todate.addClass("cld-h-bt-today")
                .html("<div class='cld-bt-date'><span class='cld-todate-txt'>" + (opt.schedule ? opt.titleText.weekorder : opt.buttonText.today) + "</span></div>")
                .click(clickToday);
            htd.append(todate);
            //下一周
            var nextb = $("<td class='cld-h-cell cld-h-bt cld-h-bt-next'/>")
        .html("<div class='cld-bt-next'><span class='cld-next-txt'>" + opt.buttonText.next + "</span></div>");
            htd.append(nextb);
        }
        var ttbl = $("<table class='cld-h-tbl'/>").append(htd);
        h.append(ttbl);
        con.append(h);
        //主体
        con.append(m);
    }
    //周模式
    function weekView(con) {
        var weekOpt = opt.weekOpt;
        var cw = getDayCellWidth(7);
        tw = opt.width - (cw * len + bw);
        var tbl = $("<table class='cld-week-tbl' style='width:100%;'/>");
        var h = $("<thead class='cld-header'/>");
        var m = $("<tbody class='cld-body'/>");
        var htr = $("<tr class='cld-h-row'/>");
        var htd_bts = $("<th class='cld-h-cell cld-h-bts' style='width:" + opt.objTitleMinWidth + "px'/>");
        var bts = $("<ul/>").appendTo(htd_bts);
        var prevb = $("<li><div class='cld-bt-prev'><span class='cld-prev-txt'>" + opt.buttonText.prev + "</span></div></li>");
        bts.append(prevb);
        var todate = $("<li><div class='cld-bt-date'><span class='cld-todate-txt'>" + opt.buttonText.today + "</span></div></li>").click(clickToday);
        bts.append(todate);
        var nextb = $("<li><div class='cld-bt-next'><span class='cld-next-txt'>" + opt.buttonText.next + "</span></div></li>");
        bts.append(nextb);
        htr.append(htd_bts);
        for (var i = 0; i < 7; i++) {
            htr.append("<th class='cld-h-cell cld-h-dt' index='" + i + "'><div class='cld-dt-txt'><div class='cld-week-title'>"
                + opt.dayNames[d.getDay()] + "</div><div class='cld-week-date'/></div></th>");
        }
        h.append(htr).appendTo(tbl);
        tbl.append(m);
        con.append(tbl);
    }
    //月模式
    function monthView(con) {
        iqz.find(".cld-top-c").html('<span style="display: inline-block;"><span class="dot-grey"></span>不开放</span><span style="display: inline-block;"><span class="dot-blue"></span>有预约</span>');
        var monthOpt = opt.monthOpt;
        var h = $("<div class='cld-header'/>");
        var m = $("<div class='cld-body' style='width:100%;overflow:hidden;'/>");
        //头部
        var ttbl = $("<table class='cld-h-tbl'/>").appendTo(h);
        var htd = $("<tr class='cld-h-row'/>").appendTo(ttbl);
        htd.append("<td class='cld-h-cell cld-h-date'><div class='cld-date-txt'></div></td>");
        var prevb = $("<td class='cld-h-cell cld-h-bt cld-h-bt-prev'/>")
            .html("<div class='cld-bt-prev'><span class='cld-prev-txt'>" + opt.buttonText.prev + "</span></div>");
        htd.append(prevb);
        var todate = $("<td class='cld-h-cell cld-h-bt cld-h-bt-today'/>")
    .html("<div class='cld-bt-date'><span class='cld-todate-txt'>" + (opt.schedule ? opt.titleText.monthorder : opt.buttonText.today) + "</span></div>");
        if (!opt.schedule) todate.click(clickToday);
        htd.append(todate);
        var nextb = $("<td class='cld-h-cell cld-h-bt cld-h-bt-next'/>")
    .html("<div class='cld-bt-next'><span class='cld-next-txt'>" + opt.buttonText.next + "</span></div>");
        htd.append(nextb);
        //主体
        var bw = opt.borderWidth;
        var wshort = opt.dayEnShort;
        var mw;
        var cw;
        var num = opt.schedule ? 8 : 7;
        if (opt.style == "cld" || opt.style == "mini") {
            mw = opt.width - 4;
            cw = parseInt((mw - 1) / num);
        }
        else {
            opt.objTitleMinWidth += 4;
            cw = getDayCellWidth(num);
            opt.objTitleMinWidth -= 4;
            mw = (cw * num + bw);
            var objw = opt.width - mw - 4;
            var obj_list = $("<div class='cld-mth-objs' style='margin:0;padding:0;float:left;height:auto;width:" + objw + "px'/>").appendTo(m);
        }
        var qzs = $("<div class='cld-mth-tm' style='margin:0;padding:2px;float:left;height:auto'/>").appendTo(m);
        var tbl = $("<table class='cld-mth-tbl' style='width:" + mw + "px'/>");
        var week = $("<tr class='cld-row cld-row-week'/>").appendTo(tbl);
        if (opt.schedule) week.append("<td class='cld-wtd cld-wk-sn' style='width:" + (cw - bw) + "px;border-width:" + bw + "px;'><span class='cld-week-txt'>"
                + opt.titleText.weekorder + "</span></td>");
        for (var dy = 0; dy < 7; dy++) {
            week.append("<td class='cld-wtd cld-week-" + wshort[(parseInt(opt.startWeek) + dy) % 7] + "' style='width:" + (cw - bw) + "px;border-width:" + bw + "px;'><span class='cld-week-txt'>"
                + opt.dayNamesShort[(parseInt(opt.startWeek) + dy) % 7] + "</span></td>");
        }
        for (var i = 0; i < 6; i++) {
            var row = $("<tr class='cld-row'/>").appendTo(tbl);
            for (var j = 0; j < num; j++) {
                if (j == 0 && opt.schedule) {
                    row.append("<td class='cld-wntd'  left='" + bw + "'  style='border-width:" + bw + "px;'><div></div><span class='cld-m-cell-wk'></span><span>" + opt.titleText.week + "</span></td>");
                    continue;
                }
                var ttd = $("<td class='cld-ttd'  left='" + (cw * j + bw) + "'  style='border-width:" + bw + "px;'/>").appendTo(row);
                var cell = $("<div class='cld-m-cell'  style='margin:0;padding:0;'/>").appendTo(ttd);
                if (j == 0) {
                    ttd.addClass("cld-ttd-l");
                    cell.addClass("cld-m-cell-l");
                }
                else if (j == num - 1) {
                    ttd.addClass("cld-ttd-r");
                    cell.addClass("cld-m-cell-r");
                }
                cell.addClass("cld-week-" + wshort[(parseInt(opt.startWeek) + j) % 7]);
            }
        }
        qzs.html(tbl);
        con.append(h);
        con.append(m);
    }
    function getDayHoursNum(obj) {
        if (obj.state == "close") return 1;
        var len = parseInt(obj.openEnd.split(":")[0], 10) - parseInt(obj.openStart.split(":")[0], 10);
        if (parseInt(obj.openEnd.split(":")[1], 10) > 0) len++;
        return Math.ceil(len / opt.dayOpt.snipTime);
    }
    function getDayCellWidth(len) {
        var hsw = opt.width - opt.borderWidth - opt.objTitleMinWidth;
        var cw = hsw / len;
        return cw;
    }
    function clickToday() {
        var d = new Date();
        idate = d;
        initTime(d);
    }
    function checkDate() {
        
        if (opt.schedule) {
            if(opt.dateStart && opt.dateEnd){
            var start = uni.parseDate(opt.dateStart);
            var end = uni.parseDate(opt.dateEnd);
            if (uni.compareDate(idate, start) < 0) {
                idate = start;
            }
            else if (uni.compareDate(idate, end) > 0) {
                idate = end;
            }
            }
        }
    }
    function setWeeks(num) {
        if (idate.wwd) {
            var wk = parseInt(idate.wwd / 10);
            idate.addDays((num - wk) * 7);
            initView();
        }
    }
    function selectWeek(wk) {
        var wwd = dateToWWD(idate);
        var ww = parseInt(wwd / 10);
        wk=parseInt(wk);
        if (ww != wk) {
            idate.addDays((wk-ww) * 7);
            initTime();
        }
    }
    //------------------------time-----------------------------
    function initTime(origin) {
        checkDate();
        if (opt.evUpTime) {
            opt.evUpTime(idate, function (data, pro, para) {
                var t;
                if ($.trim(pro) == "unilab3") {
                    t = cvtUniLab3(data, para);
                }
                else {
                    t = cvtUniLab3(data, para);
                }
                if (t) {
                    opt.objs = t.objs;
                    opt.plans = t.plans;
                    modeTime(origin);
                }
            }, opt);
        }
        else {
            modeTime(origin);
        }
    }
    function modeTime(origin) {
        var rg;
        switch (opt.mode) {
            case "d": rg = dayTime(); break;
            case "w": rg = weekTime(); break;
            case "m": rg = monthTime(); break;
        }
        if (rg)
            initPlan(rg.start, rg.end);
    }
    function dayTime() {
        iqz = $(iqz);
        var dayOpt = opt.dayOpt;
        var now = new Date();
        //头部
        var h = iqz.find(".cld-header");
        var cells = h.find(".cld-h-dt");
        var startDay = getStartDay(idate);
        cells.each(function (i) {
            pthis = $(this);
            var d = startDay.getObj();
            d.addDays(i);
            pthis.unbind('click');
            pthis.attr("date", d.format("yyyy-MM-dd"));
            if (uni.compareDate(now, d) == 0) pthis.addClass("cld-h-dt-today");
            else pthis.removeClass("cld-h-dt-today");
            if (uni.compareDate(d, idate) == 0) { pthis.addClass("cld-d-sel"); idate.index = parseInt(pthis.attr("index"), 10); }
            else
                pthis.removeClass("cld-d-sel");
            //是否应许查询历史时间
            if (!opt.allowHistory && uni.compareDate(now, d) > 0) pthis.addClass("cld-h-dt-history");
            else {
                pthis.removeClass("cld-h-dt-history");
                if (!pthis.hasClass("cld-d-sel")) {
                    pthis.click(getEvenFun({ date: d }, function (data) {
                        if (dayOpt.evBeforeUp) {
                            if (!dayOpt.evBeforeUp(data))//阻止刷新
                                return false;
                        }
                        idate = d;
                        initTime();
                    }));
                }
            }

            if (opt.ui) { pthis.hasClass("cld-d-sel") ? pthis.addClass("ui-tabs-active ui-state-active") : pthis.removeClass("ui-tabs-active ui-state-active"); }
            var t = $("<div class='cld-dt-txt'><div>" + uni.formatDate(opt.shortDateFmt, d) + "</div><div>"
                + opt.dayNames[d.getDay()] + "</div></div>");
            pthis.html(t);
        });
        //按钮事件
        if (uni.isNull(opt.openWeeks)) {
            var prevb = h.find(".cld-h-bt-prev").unbind("click");
            var nextb = h.find(".cld-h-bt-next").unbind("click");
            var num = dayOpt.dayNum;
            //var tm_date = idate.getObj();
            //tm_date.addDays(-(idate.index + 1));
            if (!opt.allowHistory && uni.compareDate((idate.getObj()).addDays(-(idate.index + 1)), new Date()) < 0) {
                prevb.addClass("cld-h-bt-disable");
            }
            else {
                prevb.removeClass("cld-h-bt-disable");
                prevb.click(function () {
                    var i = idate.index;
                    num = i + 1;
                    idate.addDays(-num);
                    initTime();
                });
            }
            nextb.click(function () {
                var i = idate.index;
                num = 7 - i;
                idate.addDays(num);
                initTime();
            });
        }
        //主体
        var m = iqz.find(".cld-body");
        var qzs = $("<div class='cld-list-qzs' style='margin:0;padding:0;'/>");
        m.html(qzs);
        idate.wwd = dateToWWD(idate);
        var len = opt.pctrl.num || opt.objs.length;
        if (opt.schedule)
            qzs.html("<table class='cld-sch-tbl' date='" + idate.format("yyyy-MM-dd") + "' wwd='" + idate.wwd + "' secnum='" + opt.secnum + "' style='width:auto;border-collapse:collapse;'/>");
        appendObjPanel(0, len);
        var region = {};
        region.start = idate.getObj();
        region.end = idate.getObj();
        return region;
    }
    //追加对象
    function appendObjPanel(startL, endL) {
        var qzs = iqz.find(".cld-list-qzs");
        var o = opt.objs;
        if (endL > o.length) endL = o.length;
        var tw = opt.objTitleMinWidth;
        var bw = opt.borderWidth;
        var dt = idate.format("yyyy-MM-dd");
        var now = new Date();
        //课表视图
        if (opt.schedule) {
            var wwd = idate.wwd;
            var qzTbl = $(".cld-sch-tbl", qzs);
            $(".cld-todate-txt", iqz).html(parseInt(wwd / 10) + opt.titleText.week);
            if (opt.height) {
                qzs.css({ "overflow-y": "scroll", "height": opt.height + "px" });
            }
            for (var j = startL; j < endL; j++) {
                if (!uni.isNoNull([o[j].id, o[j].name])) continue;
                var len = opt.secnum;
                var cw = getDayCellWidth(len);
                tw = opt.width - (cw * len + bw);
                var tr = $("<tr class='cld-row cld-sec-qz cld-row-" + (j % 2 == 0 ? 'even' : 'odd') + "' objId='" + o[j].id + "' objName='" + o[j].title + "' />").appendTo(qzTbl);
                var otd = $("<td class='cld-otd' style='border-width:" + bw + "px;'><div class='cld-obj-title'  style='overflow:hidden;height:" + opt.cellHeight + "px;' >" + o[j].name + "<div class='detail'>" + o[j].detail || '' + "</div></div></td>");
                if (!opt.alone)
                    otd.width(tw - bw);
                else
                    otd.hide();
                if (opt.dayOpt.evSelObj) {
                    otd.click(getEvenFun({ obj: o[j] }, opt.dayOpt.evSelObj));
                }
                tr.append(otd);
                if (o[j].state == "close") {
                    tr.append("<td colspan = " + len + " class='cld-ttd cld-op-out' style='border-width:" + bw + "px;'><div class='cld-ttd-title' style='height:" + opt.cellHeight + "px;'>" + (o[j].ext || opt.language["over"]) + "</div></td>");
                    continue;
                }
                for (var k = 0; k < len; k++) {
                    var nw = k + 1;
                    var tch = parseInt(o[j].id) * 100000 + wwd * 100 + nw;
                    var ttd = $("<td class='cld-ttd' stav='" + tch + "' sec='" + nw + "'   style='border-width:" + bw + "px;'><div class='cld-d-cell' style='height:" + opt.cellHeight + "px;'><div class='cld-d-cell-title'>" + opt.snipName[k] + "</div><div class='cld-d-cell-info'></div></div></td>");
                    if (!opt.alone) {
                        ttd.width(cw - bw);
                        ttd.find(".cld-d-cell-info").width(cw - 3 * bw);
                    }
                    if (isInArray(tch, stateSet.tchs))
                        ttd.addClass("cld-ttd-sel");
                    else
                        ttd.removeClass("cld-ttd-sel");
                    if (k == 0) ttd.addClass("cld-ttd-l");
                    if (k == (len - 1)) ttd.addClass("cld-ttd-r");
                    if (opt.evSelTime) {
                        ttd.click(getEvenFun({ obj: o[j], dt: dt, wwd: wwd, sec: nw, org: ttd }, opt.evSelTime));
                    }
                    ttd.click(ttdClick);

                    tr.append(ttd);
                }
            }
            qzs.append(qzTbl);
        }
        else {//普通视图  
            qzs.children().removeClass("cld-pctrl-new");
            for (var j = startL; j < endL; j++) {
                if (!uni.isNoNull([o[j].id, o[j].name])) continue;
                //比较时区（日期）
                var op_start = now.getObj();
                if (o[j].latest && !isNaN(o[j].latest)) {
                    op_start.addMinutes(o[j].latest);
                }
                //var op_end; 不过滤未到期
                //if (o[j].earliest && !isNaN(o[j].earliest)) {
                //    op_end = now.getObj();
                //    op_end.addMinutes(o[j].earliest);
                //}
                if ((!opt.relative && uni.compareDate(idate, op_start) < 0))//比较日期  || (op_end && (uni.compareDate(idate, op_end) > 0))
                    o[j].state = "close";//开放时区外
                ////
                var len = opt.relative ? opt.secnum : getDayHoursNum(o[j]);
                var cw = getDayCellWidth(len);
                tw = opt.width - (cw * len + bw);
                var odd_even = (j % 2 == 0 ? "even" : "odd");
                var start = parseInt(o[j].openStart.split(":")[0], 10);
                var objQz = $("<div class='cld-obj-qz cld-pctrl-new " + odd_even + "' objId='" + o[j].id + "' objName='" + o[j].title + "' style='min-height:" + opt.cellHeight + "px;position:relative;'/>").appendTo(qzs);
                var objTbl = $("<table class='cld-obj-tbl cld-obj-state-" + o[j].state + "' style='width:100%;border-collapse:collapse;'/>").appendTo(objQz);
                var tr = $("<tr class='cld-row'/>").appendTo(objTbl);
                var detail = o[j].detail || '';
                var otd = $("<td class='cld-otd' style='border-width:" + bw + "px;'><div class='cld-obj-title'  style='overflow:hidden;height:" + opt.cellHeight + "px;' >" + o[j].name + "<div class='detail' title='" + detail + "'>" + detail + "</div></div></td>");
                if (!opt.alone)
                    otd.width(tw - bw);
                else
                    otd.hide();
                if (opt.dayOpt.evSelObj) {
                    otd.click(getEvenFun({ obj: o[j] }, opt.dayOpt.evSelObj));
                }
                tr.append(otd);
                if (o[j].state == "close") {
                    tr.append("<td class='cld-ttd cld-op-out' style='width:" + (cw - bw) + "px;border-width:" + bw + "px;'><div class='cld-ttd-title' style='height:" + opt.cellHeight + "px;'>" + (o[j].ext || language.over) + "</div></td>");
                    continue;
                }
                objTbl.mouseleave(function () {
                    redLine.offset({ top: -1000, left: -1000 });
                });
                for (var k = 0; k < len; k++) {
                    var nw = k;
                    var ttd = $("<td class='cld-ttd'  style='border-width:" + bw + "px;height:" + opt.cellHeight + "px;'></td>");// left='" + (cw * k + tw + bw) + "'
                    if (!opt.alone)
                        ttd.width(cw - bw);
                    if (opt.relative) {
                        ttd.attr("sec", nw + '');
                        ttd.attr("start", opt.secTime[nw * 2] || '');
                        ttd.attr("end", opt.secTime[nw * 2 + 1] || '');
                        ttd.html("<div class='cld-d-cell'><div class='cld-d-cell-title'>" + opt.snipName[nw] + "</div><div class='cld-d-cell-info'></div></div>");
                        nw = start + nw * opt.dayOpt.snipTime;
                        ttd.attr("hour", nw + '');
                        ttd.html("<div class='cld-ttd-title'>" + nw + ":00</div>");
                        var ndt = idate.getObj();
                        var sec_start = opt.secTime[k * 2];
                        var sec_end = opt.secTime[k * 2 + 1];
                        if (sec_start && sec_end) {
                            var sec_start_h = parseInt(sec_start.split(':')[0], 10),
                            sec_start_m = parseInt(sec_start.split(':')[1], 10);
                            var sec_end_h = parseInt(sec_end.split(':')[0], 10),
                                sec_end_m = parseInt(sec_end.split(':')[1], 10);
                            ndt.setHours(sec_start_h);
                            ndt.setMinutes(sec_start_m);
                            //if (op_end && (uni.compareDate(ndt, op_end) > 0))//比较日期
                            //    ttd.addClass("cld-op-out");//开放时区外
                            ndt.setHours(sec_end_h);
                            ndt.setMinutes(sec_end_m);
                            if (uni.compareDate(op_start, ndt) > 0)
                                ttd.addClass("cld-op-out");
                        }
                    }
                    else {
                        nw = start + nw * opt.dayOpt.snipTime;
                        ttd.attr("hour", nw + '');
                        ttd.html("<div class='cld-ttd-title'>" + nw + ":00</div>");
                        var ndt = idate.getObj();
                        ndt.setHours(nw);
                        //if (uni.compareDate(op_start, ndt) > 0 || (op_end && (uni.compareDate(ndt, op_end) > 0)))//比较日期
                        //    ttd.addClass("cld-op-out");//开放时区外
                        if (uni.compareDate(now, ndt) == 0 && uni.compareDate(now, ndt, 'h') > (opt.dayOpt.snipTime - 1))//当天过期时间
                            ttd.addClass("cld-op-out");
                    }
                    if (k == 0) ttd.addClass("cld-ttd-l");
                    if (k == (len - 1)) ttd.addClass("cld-ttd-r");
                    if (!ttd.hasClass("cld-op-out")&&o[j].state!="noresv") {
                        if (opt.evSelTime) {
                            ttd.css("cursor", "pointer");
                            ttd.addClass("cld-op-in");
                            if (opt.operate == "drag") {
                                ttd.mousemove(redLineHandler({ opt: opt, dt: dt, h: nw, obj: o[j] }));
                                ttd.mousedown(dragHandler({ z: iqz, opt: opt, md: opt.mode, objTbl: objTbl, obj: o[j], dt: dt, h: nw, evSelTime: opt.evSelTime }));
                            }
                            else
                                ttd.click(getEvenFun({ obj: o[j], dt: dt, h: nw }, opt.evSelTime));
                        }
                        ttd.click(ttdClick);
                    }
                    tr.append(ttd);
                }
            }
        }

    }
    function ttdClick() {
        var pthis = $(this);
        if (pthis.hasClass("cld-plan-sec")) return;
        var tch = pthis.attr("stav");
        if (tch) {
            tch = parseInt(tch);
            if (opt.stateSetOpt.external) {//外部操作模式，不增不减
                if (opt.evClickSchedule && !pthis.hasClass("cld-ttd-sel")) {
                    opt.evClickSchedule(tch);
                }
            }
            else {
                if (!pthis.hasClass("cld-ttd-sel")) {
                    pthis.addClass("cld-ttd-sel");
                    addTch(tch);
                }
                else {
                    pthis.removeClass("cld-ttd-sel");
                    removeTch(tch);
                }
            }
        }
    }
    function redLineHandler(data) {
        return function (e) {
            var pthis = data.ttd || $(this);
            var dayOpt = data.opt.dayOpt;
            //暂存数据
            var m_w = (pthis.width() + 2) / (dayOpt.snipTime);//每格像素宽度
            var offset = pthis.offset();
            redLine.offset({ top: offset.top - redLine.height(), left: e.pageX - (redLine.width() / 2) });
            var bar_time;
            if (data.bar_end) bar_time = data.bar_end;
            else
                bar_time = m2approx(data.h * 60 + Math.ceil((e.offsetX || getOffset(e).X) / m_w * 60 * dayOpt.snipTime));
            var time = m2ms(bar_time);
            redLine.time = time;
            //计算允许时间区间(单位天)  暂时不用
            /*var now = new Date();
            var here=uni.parseDate(data.dt + " " + time)
            var t_diff = uni.compareDate(here, now);
            var earliest = parseInt(data.obj.earliest)/1440;
            var lastest = parseInt(data.obj.latest)/1440;
            if (t_diff < lastest) {
                now.addDays(lastest);
                time = language.time_last + m2ms(m2approx(now.format("MM-dd")));
                redLine.ready = false;
            }
            else if (t_diff > earliest) {
                now.addDays(earliest);
                time = language.time_early + m2ms(m2approx(now.format("MM-dd")));
                redLine.ready = false;
            }
            */
            var now = new Date();
            var here = uni.parseDate(data.dt + " " + time)
            var t_diff = uni.compareDate(here, now, "m");
            if (t_diff < 0) {
                time = language.time_last + m2ms(m2approx(now.getHours() * 60 + now.getMinutes()));
                redLine.ready = false;
            }
            else
                redLine.ready = true;
            $(".title", redLine).html(time);
        }
    }
    function dragHandler(data) {
        return function (e) {
            var pthis = $(this);
            var obj = data.obj;
            var dayOpt = data.opt.dayOpt;
            //暂存数据
            var m_w = (pthis.width() + 2) / (dayOpt.snipTime);//每格像素宽度
            var bar = $("<div class='cld-drag-bar'  onselectstart='return false'></div>");
            bar.height(pthis.height() + 1);
            var offset = pthis.offset();
            bar.offset({ top: offset.top + 1, left: e.pageX });
            var bar_basic_m = data.h * 60;
            var bar_start_x = e.offsetX || getOffset(e).X;
            var bd = $("body").append(bar);
            e.stopPropagation();
            var bar_start = m2approx(bar_basic_m + Math.ceil(bar_start_x / m_w * 60 * dayOpt.snipTime));
            var bar_end = bar_start;//选择结束时间初始等于开始时间
            bar.mousemove(function (e) {
                data.bar_end = bar_end;
                data.ttd = pthis;
                (redLineHandler(data))(e);
                cptWidth(e);
                e.stopPropagation();
            });
            data.objTbl.mousemove(function (e) {
                cptWidth(e);
                e.stopPropagation();
            });
            bd.one("mouseup", function () {
                bar.remove();
                data.objTbl.unbind("mousemove");
                redLine.title = undefined;
                if (redLine.ready) {
                    data.start = m2ms(bar_start);
                    data.end = m2ms(bar_end);
                    data.evSelTime(data);
                }
            });
            function cptWidth(e) {
                width = (e.pageX - bar.offset().left)
                if (width < 0) width = 0;
                bar_end = m2approx(bar_basic_m + Math.ceil((bar_start_x + width) / m_w * 60 * dayOpt.snipTime));
                var diff = bar_end - bar_start;
                if (diff < obj.min) $(".title", redLine).html(language.time_min + m2msL(obj.min));
                if (diff > obj.max) { $(".title", redLine).html(language.time_max + m2msL(obj.max)); bar_end = bar_start + obj.max; }
                var title = "<div style='padding-left:1px;'>" + m2ms(bar_start) + "</div><div style='padding-left:1px;'>" + m2ms(bar_end) + "</div>";
                bar.html(title);
                if (diff > obj.max) return;//超出允许最多时间 宽度停留
                bar.width(width);
            }
        }
    }
    function m2ms(m) {
        var t = parseInt(m),
        resv_t = uni.num2Str(parseInt(t / 60)) + ":";
        resv_t += uni.num2Str(t % 60);
        return resv_t;
    }
    function m2msL(m) {
        var t = parseInt(m), resv_t = "";
        var h = parseInt(t / 60);
        var m = (t % 60);
        if (h > 0)
            resv_t += h + language.H;
        if (m > 0)
            resv_t += m + language.m;
        return resv_t;
    }
    function m2approx(m) {
        var unit = opt.dayOpt.unit;
        if ((m % unit) > unit / 2)//过半入
            m = Math.ceil(m / unit) * unit;
        else//不过半舍
            m = Math.floor(m / unit) * unit;
        return m;
    }
    function weekTime() {
        var objs = opt.objs;
        for (var j = 0; j < objs.length; j++) {
            var o = objs[j];
            var otr = $("<tr class='cld-row'/>");
            var otd = $("<td class='cld-otd' style='width:" + opt.objTitleMinWidth + "px'><div class='cld-obj-title'>" + o.name + "</div></td>");
            if (opt.evSelObj) {
                otd.click(getEvenFun({ obj: o }, opt.evSelObj));
            }
            otr.append(otd);
            for (var k = 0; k < 7; k++) {
                var ttd = $("<td class='cld-ttd'></td>");
                if (k == 0) ttd.addClass("cld-ttd-l");
                if (k == (6)) ttd.addClass("cld-ttd-r");
                otr.append(ttd);
            }
            m.append(otr);
        }
    }
    function monthTime() {
        //对象
        if (opt.style != "cld" && opt.style != "mini") {
            var oq = $(".cld-mth-objs", iqz);
            var tmp_oq = $("<div class='cld-obj-list'/>");
            var tmp_pg = $("<div style='overflow:hidden;' class='cld-obj-page'/>");
            var objs = $(opt.objs);
            var pnum = opt.pageNum;
            var maxp = Math.ceil(objs.length / pnum) - 1;
            var npg = 0;
            if (!iobj) {
                if (objs.length > 0)
                    iobj = opt.objs[0];
                else
                    return false;
            }
            objs.each(function (i) {
                var pg = Math.floor(i / pnum);
                if (iobj.id == this.id) {
                    iobj = this;
                    npg = pg;
                    return false;
                }
            });
            tmp_pg.attr("page", npg);
            var pg_pre = $("<div style='float:left;width:50%;'><div class='cld-pg-bt cld-pg-pre' title='" + opt.titleText.pgpre + "'><span class='cld-prev-txt'>"
                + opt.buttonText.prev + "</span></div></div>").appendTo(tmp_pg);
            if (npg == 0) {
                pg_pre.addClass("cld-pg-none");
            }
            else if (npg > 0) {
                pg_pre.css("cursor", "pointer");
                pg_pre.click(function () {
                    iobj = opt.objs[(npg - 1) * pnum];
                    initTime();
                });
            }
            var pg_next = $("<div  style='float:left;width:50%;'><div class='cld-pg-bt cld-pg-next' title='" + opt.titleText.pgnext + "'><span class='cld-next-txt'>"
                + opt.buttonText.next + "</span></div></div>").appendTo(tmp_pg);
            if (npg == maxp) {
                pg_next.addClass("cld-pg-none");
            }
            else if (npg < maxp) {
                pg_next.css("cursor", "pointer");
                pg_next.click(function () {
                    iobj = opt.objs[(npg + 1) * pnum];
                    initTime();
                });
            }
            objs.each(function (i) {
                var pg = Math.floor(i / pnum);
                if (npg != pg) return true;
                var obj = this;
                var obt = $("<div class='cld-bt-obj' page='" + npg + "' objId='" + obj.id + "' objName='" + obj.title + "' style='height:" + opt.cellHeight + "px;overflow:hidden;'/>");
                if (iobj.id == obj.id) obt.addClass("cld-obj-sel");
                var otitle = "<div class='cld-obj-title'>" + obj.name + "</div>";
                obt.append(otitle);
                obt.click(getEvenFun({ obj: iobj, date: idate }, function (data) {
                    if (opt.evSelObj) {
                        if (!opt.evSelObj(data))
                            return false;
                    }
                    iobj = obj;
                    initTime();
                }));
                tmp_oq.append(obt);
            })
            oq.html(tmp_pg);
            oq.append(tmp_oq);
        }
        //按钮事件
        var prevb = $(".cld-h-bt-prev", iqz).unbind("click");
        var nextb = $(".cld-h-bt-next", iqz).unbind("click");
        prevb.click(function () {
            idate.addMonths(-1);
            initTime();
        });
        nextb.click(function () {
            debugger;
            idate.addMonths(1);
            initTime();
        });
        //头部
        var h = iqz.find(".cld-header");
        var hdate = $(".cld-date-txt", h);
        hdate.html(idate.format(opt.shortDateFmtY));
        if (idate && iobj) {
            hdate.append("&nbsp;&nbsp;" + $("<div>" + iobj.name + "</div>").text());
        }
        //月历
        debugger;
        var region = {};
        var tblcell = $(".cld-mth-tbl", iqz).find(".cld-m-cell");
        var today = new Date();
        var date = getClearTime(idate);
        date.setDate(1);
        var mth = date.getMonth();
        date = getStartDay(date);
        region.start = date.getObj();
        if (opt.schedule) {
            idate.wwd = dateToWWD(idate);
        }
        var tww = parseInt(dateToWWD(new Date()) / 10);
        tblcell.each(function (i) {
            var cell = $(this);
            var ttd = cell.parent();
            cell.attr("wwd", dateToWWD(date));
            if (date.getMonth() != mth)
                ttd.addClass("cld-mth-oth");
            else
                ttd.removeClass("cld-mth-oth");
            if (uni.compareDate(date, today) == 0)
                ttd.addClass("cld-mth-today");
            else
                ttd.removeClass("cld-mth-today");
            if (opt.schedule && date.getDay() == 1) {
                var wwd = dateToWWD(date),
                    ww = parseInt(wwd / 10);
                if (ww == tww && ww != 0) cell.parents(".cld-row").addClass("cld-current-week");
                cell.parents(".cld-row").find(".cld-m-cell-wk").html(ww + " ");
            }
            var selDate = date.format("yyyy-MM-dd");
            cell.attr("date", selDate);
            var dt_d = date.getDate();
            
            var dtitle = "<div class='cld-m-cell-title'>" + dt_d + (dt_d == 1 && opt.style != "mini" ? "<span class='cld-m-cell-title-month'>" + opt.monthNames[date.getMonth()] + "</span>" : "") + "</div>";
            var dinfo = "<div class='cld-m-cell-info' style='min-height:" + opt.cellHeight + "px;'/>";
            cell.html(dtitle);
            cell.append(dinfo);
            if (opt.evSelTime) {
                cell.css("cursor", "pointer");
                ttd.addClass("cld-op-in");
                cell.unbind("click");
                cell.click(getEvenFun({ obj: iobj, dt: selDate }, opt.evSelTime));
            }
            date.addDays(1);
        });
        //date.addDays(-1);
        region.end = date.getObj();
        return region;
    }
    function getEvenFun(data, evFun) {
        return function () {
            data.z = iqz;
            data.opt = opt;
            data.md = opt.mode;
            if (opt.mode == "d") {
                data.start = data.end = redLine.time;
            }
            evFun(data);
        }
    }
    function getStartDay(date) {
        var dt = date.getObj();
        var tw = dt.getDay();
        var sw = parseInt(opt.startWeek);
        if (sw - tw > 0)
            dt.addDays((sw - tw) - 7);
        else
            dt.addDays(sw - tw);
        return dt;
    }
    function dateToWWD(date) {
        if (!date || !opt.dateStart) return 0;
        var start = uni.parseDate(opt.dateStart);
        var first = ((7 - start.getDay()) % 7) + opt.startWeek;
        if (first > 7) first = first % 7;
        var dt1 = start.getTime();
        var dt2 = date.getTime();
        if (opt.dateEnd) {
            var end = uni.parseDate(opt.dateEnd);
            if (uni.compareDate(date, end) > 0) return 0;
        }
        var diff = parseInt((dt2 - dt1) / 1000 / 60 / 60 / 24);
        if (diff < (7-first)) return 0;
        var ww = (diff < first) ? 1 : parseInt((diff - first) / 7) + 2;
        return ww * 10 + (6 - ((7 - date.getDay()) % 7));
    }
    //--------------------------plan-------------------------

    function initPlan(start, end) {
        if (opt.evUpPlans) {
            opt.evUpPlans(iobj, start, end, function (plans, pro) {
                if (plans != undefined)
                    opt.plans = plans;
                modePlan();
            }, opt);
        }
        else {
            modePlan();
        }
    }
    function modePlan() {
        iqz.siblings(".cld-waiting").remove();
        iqz.fadeIn("200");
        switch (opt.mode) {
            case "d": iqz.find(".cld-plan-bar").remove(); dayPlan(); break;
            case "w": weekPlan(); break;
            case "m": monthPlan(); break;
            default:
                dayPlan();
        }
        //分级显示 暂只支持天
        if (opt.pctrl.num && opt.mode == "d") {
            var num = opt.pctrl.num;
            pCtrl(function (i) {
                appendObjPanel(i * num, (i + 1) * num);
                dayPlan();
            });
        }
        if (opt.evFinishDraw) opt.evFinishDraw(idate.format("yyyy-MM-dd"), idate, opt, iqz, { iobj: opt.mode == "m" ? iobj : null });
    }
    //日视图
    function dayPlan() {
        var m = iqz.find(".cld-body");
        //m.find(".cld-plan-bar").remove();
        var qzs = opt.schedule ? m.find(".cld-sec-qz") : m.find(".cld-pctrl-new");
        var ps = opt.plans;
        qzs.each(function () {
            var qz = $(this);
            var id = qz.attr("objId");
            for (var i = 0; i < ps.length; i++) {
                if (!uni.isNoNull([ps[i].objId, ps[i].start, ps[i].end]))
                    continue;
                if (ps[i].objId == id) {
                    if (opt.schedule) {
                        if (idate.wwd == parseInt(ps[i].start / 100)) {
                            drawDayRelative(qz, ps[i]);
                        }
                    }
                    else {
                        var start = uni.parseDate(ps[i].start);
                        if (uni.compareDate(start, idate) == 0) {
                            drawDayPlan(qz, ps[i]);
                        }
                    }
                }
            }
        });
    }
    function drawDayRelative(qz, plan) {
        qz = $(qz);
        var ttds = qz.find(".cld-ttd");
        var obj = getObjById($(qz).attr("objId"));
        var start = plan.start % 100;
        var len = (plan.end % 100) - start + 1;
        ttds.each(function (i) {
            var sec = parseInt($(this).attr("sec"), 10);
            if (sec == parseInt(start)) {
                for (var j = 0; j < len; j++) {
                    var p = ttds.eq(i + j);
                    var title = plan.title || "";
                    var state = plan.state || "busy";
                    $(".cld-d-cell-info", p).html(title);
                    p.addClass("cld-plan-sec cld-st-" + state);
                    if (plan.color) p.css("color", plan.color);
                    if (plan.background) p.css("background", plan.background);
                    if (opt.evSelPlan) {
                        p.click(getEvenFun({ plan: plan }, opt.evSelPlan));
                    }
                }
                return false;
            }
        });
    }
    function drawDayPlan(qz, plan) {
        var ttds = qz.find(".cld-ttd");
        if (ttds.eq(ttds.length - 1).hasClass("cld-op-out"))//全天不开放
            return;
        var obj = getObjById($(qz).attr("objId"));
        var start = uni.parseDate(plan.start);
        var pstart = idate.getObj();
        pstart.setMinutes(0);
        pstart.setHours(obj.openStart.split(":")[0]);
        if (start < pstart)
            start = pstart;
        var end = uni.parseDate(plan.end);
        var pend = idate.getObj();
        pend.setHours(obj.openEnd.split(":")[0]);
        var mui = parseInt(obj.openEnd.split(":")[1], 10);
        if (mui > 0) mui = 59;
        pend.setMinutes(mui);
        if (end > pend)
            end = pend;
        var len = getDayHoursNum(obj);
        var cw = getDayCellWidth(len);
        var shift = parseInt(cw * (start.getMinutes() / 60));
        var width = Math.ceil((getMDiff(start, end) / 60) * cw);
        var occupy = opt.dayOpt.occupy;
        if (plan.occupy) occupy = true;
        var height = parseInt(qz.find("tr").height()) - opt.borderWidth - (occupy ? 0 : 15);
        ttds.each(function (i) {
            var pthis = $(this);
            var hour = parseInt(pthis.attr("hour"));
            if (hour == start.getHours()) {
                var positon = pthis.position();
                var left = shift + positon.left;
                var title = plan.title || "";
                var state = plan.state || "busy";
                var p = $("<div class='cld-plan-bar " + (occupy ? "cld-occupy" : "cld-no-occupy") + " cld-st-" + plan.state + "' style='position:absolute;" + (occupy ? "top:" : "bottom:") + opt.borderWidth + "px;left:" + left +
                    "px;overflow:hidden;width:" + width + "px;height:" + height + "px'>" + title + "</div>");
                if (plan.color) p.css("color", plan.color);
                if (plan.background) p.css("background", plan.background);
                if (opt.evSelPlan) {
                    p.click(getEvenFun({ plan: plan }, opt.evSelPlan));
                }
                qz.append(p);
                return false;
            }
        });
    }
    //月视图
    function monthPlan() {
        var m = $(".cld-mth-tm", iqz);
        var cells = $(".cld-m-cell", m);
        var plans = opt.plans;
        var now = new Date();
        var today = getClearTime(now);
        cells.each(function (i, _cell) {
            var cell = $(_cell);
            var title = "";
            var nofree = false;
            var content = cell.find(".cld-m-cell-info");//内容
            if (opt.schedule) {//课表时间
                var wwd = parseInt(cell.attr("wwd"));
                var wtoday = dateToWWD(today);
                var expire = false;
                title = "";//提示文字
                if (wwd < wtoday) {
                    nofree = true;
                    expire = true;
                }
                for (var j = 0; j < plans.length; j++) {
                    var plan = plans[j];
                    if (plan.state == "busy" && (opt.style != "cld" && opt.style != "mini" && plan.objId != iobj.id)) continue;
                    var start = plan.start;
                    var end = plan.end;
                    if (wwd == parseInt(start / 100)) {
                        var v_title = "第" + (start % 100) + "节";
                        if (start != end)
                            v_title += "--第" + (end % 100) + "节";
                        if (opt.style == "mini") {
                            cell.addClass("cld-mini-busy");
                            title += v_title + "(" +plan.title + ")<br/>";
                        }
                        else {
                            var rsvt = "";//预约时间
                            rsvt = "<span class='cld-plan-info-sec' data-content='" + plan.detail + "'>" + v_title + "</span><br/>" + plan.title;
                            if (expire) {
                                content.append("<div class='cld-plan-info  cld-st-done'><span class='dot-grey'></span>" + rsvt + "</div>");
                            }
                            else if (plan.state == "doing") {
                                content.append("<div class='cld-plan-info cld-st-doing'><span class='dot-green'></span>" + rsvt + "</div>");
                            }
                            else if (plan.state == "undo" || plan.state == "busy") {
                                content.append("<div class='cld-plan-info cld-st-" + plan.state + "'><span class='dot-blue'></span>" + rsvt + "</div>");
                            }
                            else if (plan.state == "done") {
                                content.append("<div class='cld-plan-info  cld-st-done'><span class='dot-grey'></span>" + rsvt + "</div>");
                            }
                        }
                    }
                }
            }
            else {//绝对时间
                var fmt = cell.attr("date");
                var date = uni.parseDate(fmt);
                title = fmt;//提示文字
                //初始化
                cell.removeClass("cld-cell-allday");
                content.html("");
                if (date < today) {
                    nofree = true;
                    content.append("<div class='cld-plan-info cld-st-done'><span class='dot-grey'></span></div>");
                    title = '已过期';
                }
                else if (iobj && (parseInt(uni.compareDate(date, today)) < parseInt((iobj.latest || 0) / 1440) || (iobj.earliest && parseInt(uni.compareDate(date, today)) >= Math.ceil(iobj.earliest / 1440)))) {
                    nofree = true;
                    content.append("<div class='cld-plan-info cld-st-done'><span class='dot-grey'></span></div>");
                    title = '不开放';
                }
                else if (opt.closeDate && isCloseDate(date)) {
                    nofree = true;
                    content.append("<div class='cld-plan-info cld-st-done'><span class='dot-grey'></span></div>");
                    title = '不开放';
                }
                else {
                    for (var j = 0; j < plans.length; j++) {
                        var plan = plans[j];
                        if (plan.state == "busy" && (opt.style != "cld" && opt.style != "mini" && plan.objId != iobj.id)) continue;
                        var start = uni.parseDate(plan.start);
                        var end = uni.parseDate(plan.end);
                        if (end < today) {
                            continue;
                        }
                        
                        if (uni.compareDate(date, start) >= 0 && uni.compareDate(date, end, "m") <= 0) {
                            var rsvt = "";//预约时间
                            //天预约
                            if (plan.allDay) {
                                nofree = true;
                                //if (uni.compareDate(date, end) == 0) continue;//过滤结束时间
                                rsvt = "<span style='font-style:italic;'>" + formatDateM(start) + "-" + formatDateM(end) + "</span>";
                            }//非天预约
                            else if (date > start && date < getClearTime(end)) {//全天被预约 标记状态
                                if (plan.state != "done") nofree = true;
                                rsvt = "<span style='font-style:italic;'>" + formatDateM(start) + "-" + formatDateM(end) + "</span>";
                            }
                            else if (date > start) {
                                rsvt = "<span style='font-style:italic;'>" + formatDateM(start) + "</span>-" + formatTime(end);
                            }
                            else if (date < getClearTime(end)) {
                                rsvt = formatTime(start) + "-<span style='font-style:italic;'>" + formatDateM(end) + "</span>";
                            }
                            else {
                                rsvt = formatTime(start) + "-" + formatTime(end);
                            }
                            if (uni.compareDate(end, start, "m") > 1438) nofree = true;//0:0到23:59 记为无空闲
                            if (end <= now) {
                                plan.state = "done"
                            }
                            if (plan.state == "doing") {
                                content.append("<div class='cld-plan-info cld-st-doing'><span class='dot-green'></span>" + rsvt + "</div>");
                            }
                            else if (plan.state == "undo" || plan.state == "busy") {
                                content.append("<div class='cld-plan-info cld-st-" + plan.state + "'><span class='dot-blue'></span>" + rsvt + "</div>");
                            }
                            else if (plan.state == "done") {
                                content.append("<div class='cld-plan-info  cld-st-done'><span class='dot-grey'></span>" + rsvt + "</div>");
                            }
                            var rsvd = "";//预约详细
                            if (plan.allDay) {
                                rsvd = formatDateM(start) + "至" + formatDateM(end);
                                cell.addClass("cld-cell-allday");
                            }
                            else {
                                rsvd = formatTime(start) + "至" + formatTime(end);
                            }
                            title += " (" + (plan.title ? plan.title + "," : "") + rsvd + ")";
                        }
                    }
                }
            }
            if (nofree) {
                cell.css("cursor", "default");
                cell.unbind("click");
                cell.addClass("cld-cell-nofree").parent("td").addClass("cld-ttd-nofree").removeClass("cld-op-in");;
            }
            else {
                cell.removeClass("cld-cell-nofree").parent("td").removeClass("cld-ttd-nofree");
            }
            if (opt.style == "mini")
                cell.attr('data-content', title);
            else
                cell.attr('title', title);
        });
    }
    function isCloseDate(date) {
        if (typeof (date) == 'object')
            date = date.format("yyyy-MM-dd");
        if (opt.closeDate) {
            for (var i = 0; i < opt.closeDate.length; i++) {
                if (date == opt.closeDate[i])
                    return true;
            }
        }
        return false;
    }
    function getMDiff(start, end) {
        return getTimeDiff(start, end, 60 * 1000);
    }
    function getTimeDiff(start, end, k) {
        var t = (end.getTime() - start.getTime()) / k;
        return t;
    }
    function getLatestTime(date) {
        var d = new Date();
        d.setFullYear(date.getFullYear());
        d.setMonth(date.getMonth());
        d.setDate(date.getDate());
        d.setHours(23);
        d.setMinutes(59);
        d.setSeconds(59);
        return d;
    }
    function getClearTime(date) {
        debugger;
        var d = new Date();
        d.setFullYear(date.getFullYear());
        if (d.getDate() == 31) {
            d.setDate(30);
        }
        d.setMonth(date.getMonth());

        d.setDate(date.getDate());
        d.setHours(0);
        d.setMinutes(0);
        d.setSeconds(0);
        d.setMilliseconds(0);
        return d;
    }
    function str2(obj) {
        if (obj.toString().length == 1) {
            return "0" + obj;
        }
        else {
            return obj;
        }
    }
    function formatDate(date) {
        
        var rel = date.getFullYear() + "-" + str2(date.getMonth() + 1) + "-" + str2(date.getDate());
        return rel;
    }
    function formatDateM(date) {
        var rel = str2(date.getMonth() + 1) + "/" + str2(date.getDate());
        return rel;
    }
    function formatDateS(date) {
        var rel = str2(date.getMonth() + 1) + "/" + str2(date.getDate());
        var t = str2(date.getHours()) + ":" + str2(date.getMinutes());
        return rel + " " + t;
    }
    function formatTime(date) {
        var t = str2(date.getHours()) + ":" + str2(date.getMinutes());
        return t;
    }
    //火狐浏览器获取offsetX/offsetY
    function getOffset(e) {
        var target = e.target, // 当前触发的目标对象
            eventCoord,
            pageCoord,
            offsetCoord;

        // 计算当前触发元素到文档的距离
        pageCoord = getPageCoord(target);

        // 计算光标到文档的距离
        eventCoord = {
            X: window.pageXOffset + e.clientX,
            Y: window.pageYOffset + e.clientY
        };

        // 相减获取光标到第一个定位的父元素的坐标
        offsetCoord = {
            X: eventCoord.X - pageCoord.X,
            Y: eventCoord.Y - pageCoord.Y
        };
        return offsetCoord;
        function getPageCoord(element) {
            var coord = { X: 0, Y: 0 };
            // 计算从当前触发元素到根节点为止，
            // 各级 offsetParent 元素的 offsetLeft 或 offsetTop 值之和
            while (element) {
                coord.X += element.offsetLeft;
                coord.Y += element.offsetTop;
                element = element.offsetParent;
            }
            return coord;
        }
    }

    //-------------------------othe----------------
    function pCtrl(callback) {
        var len = opt.objs.length;
        var pctrl = opt.pctrl;
        var ctrl = iqz.find(".cld-pctrl");
        var num = pctrl.num;
        var count = Math.ceil(len / num);
        var ready = 0;
        ctrl.html("<div class='cld-more-panel'><span class='cld-m-info'>总<span class='cld-m-total'>" + len + "</span>条数据，已显示<span class='cld-m-ready'></span>条，" + (pctrl.trigger == "click" ? "点击加载更多" : "滚动显示更多") + "...</span></div>");
        if (pctrl.trigger == "click") {
            ctrl.css("cursor", "pointer");
            ctrl.click(function () {
                more();
            });
        }
        else {
            uni.lifeEvent.ev_window_scroll = function () {
                if (!iqz.is(":hidden") && ready < count && ($(window).height() - (ctrl.height() + (ctrl.offset().top - $(document).scrollTop())) > (pctrl.triggerHeight || 120))) {
                    more();
                }
                //if (ready >= count)
                //    $(window).unbind("scroll", tmpScrollFun);
            };
        }
        more();
        function more() {
            if (ready < count) {
                if (ready > 0) callback(ready);//初始化后才触发回调
                ready++;
            }
            if (ready < count)
                ctrl.find(".cld-m-ready").html(ready * num);
            else
                $(".cld-more-panel", ctrl).html(len == 0 ? "没有获取到任何数据" : (uni.translate("已加载全部") + len + uni.translate("条数据")));
        }
    }

    function isInArray(v, arr) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == v)
                return true;
        }
        return false;
    }
    function removeTest(ltch) {
        var array = [];
        if (!isNaN(ltch)) array.push(ltch);
        else if (Object.prototype.toString.call(ltch) == '[object Array]') array = ltch;
        else return;
        var tchs = stateSet.tchs;
        for (var v = 0; v < array.length; v++) {
            var id = parseInt(array[v] / 100),
                diff = array[v] % 100;
            for (var i = 0; i < tchs.length; i++) {
                if (tchs[i] == id) { tchs.splice(i, diff + 1); break };
            }
        }
        actTch();
        refCld();
    }
    function addTest(ltch) {//添加多节次数/数组
        var array = [];
        if (!isNaN(ltch)) array.push(ltch);
        else if (Object.prototype.toString.call(ltch) == '[object Array]') array = ltch;
        else return;
        var tchs = stateSet.tchs;
        for (var v = 0; v < array.length; v++) {
            var id = parseInt(array[v] / 100),
                diff = array[v] % 100;
            for (var j = 0; j <= diff; j++) {
                tchs.push(id + j);
            }
        }
        actTch(ltch);
        refCld();
    }
    function addTch(tch) {
        stateSet.tchs.push(tch);
        actTch(tch);
    }
    function removeTch(tch) {
        var tchs = stateSet.tchs;
        for (var i = 0; i < tchs.length; i++) {
            if (tchs[i] == tch) { tchs.splice(i, 1); break };
        }
        actTch();
    }
    function actTch(tch) {//合并单节次为实验 以第一节为标识 diff为节次差
        var tchs = stateSet.tchs;
        tchs.sort();
        var len = tchs.length,
            i = 0,
            list = [];
        stateSet.tests = [];
        while (i < len) {
            var tc = tchs[i];
            if (tc == 0) { i++; continue };
            var diff = 0,
                j = 1;
            list.push(tc);
            for (; i + j < len; j++) {
                var t1 = tchs[i + j];
                var t2 = tchs[i + j - 1];
                var sec = t1 % 100;
                if (t1 - t2 == 0) continue;
                if (isInArray(sec, stateSet.line)) break;//在line内节次 重新开始计算实验
                if (t1 - t2 == 1) {
                    list.push(t1);
                    diff++;
                }
                else if (t1 - t2 > 1)
                    break;
            }
            i += j;
            stateSet.tests.push(tc);
            var key = "" + tc;
            if (stateSet.testDic[key] && stateSet.testDic[key].id) {//更新或存入实验
                stateSet.testDic[key].diff = diff;
                stateSet.testDic[key].ltch = tc * 100 + diff;
            }
            else {
                var v = stateSet.testDic[key] || {};
                v.id = tc;
                v.oid = parseInt(tc / 100000) + "";
                v.ids = v.oid;
                v.ltch = tc * 100 + diff;
                v.wwd = parseInt((tc % 100000) / 100);
                v.ww = parseInt(v.wwd / 10);
                v.d = v.wwd % 10;
                v.sec = tc % 100;
                v.diff = diff;
                stateSet.testDic[key] = v;
            }
        }
        stateSet.tchs = list;//过滤重复后的单节次数组
        if (opt.evRefState) opt.evRefState(stateSet.tests, stateSet.testDic, tch);
    }
    function refCld() {
        $(".cld-ttd", iqz).each(function (i) {
            var tch = $(this).attr("stav");
            if (tch) {
                tch = parseInt(tch);
                if (isInArray(tch, stateSet.tchs))
                    $(this).addClass("cld-ttd-sel");
                else
                    $(this).removeClass("cld-ttd-sel");
            }
        });
    }
    //-------------------------传值转换

    //pro:实验室 值转换
    function cvtUniLab3(list, para) {
        var temp = $(list);
        var objs = [];
        var plans = [];
        if (!para) para = {};
        if (para.plans) plans = para.plans;
        temp.each(function (i) {
            if ((this.prop & 524288) > 0) return true;//不支持预约
            if (this.state == "close" && !para.showClose) {
                return true;
            }
            var obj = this;
            if (this.state == "forbid") obj.state = "close";//禁用按不开放处理
            if (para.byLab || para.byType == "lab") this.name += "<br/>(" + this.labName + ")";
            else if ((para.byRoom || para.byType == "room") && !this.iskind) this.name += "<br/>(" + this.roomName + ")";
            else if (para.byType == "devcls") this.name += "<br/>(" + this.className + ")";
            else if (para.byType == "volume") this.name += "<br/>(" + this.minUser + "-" + this.maxUser + "人)";
            if (para.detailFun) {
                this.name = "<strong>" + this.name + "</strong>";
                obj.detail = para.detailFun(this);
            }
            obj.type = this.iskind ? "kind" : "dev";
            obj.typeId = this.kindId;
            obj.latest = this.latest || 0;
            opt.closeDate = this.clsDate;//不开放日期数组
            var open = this.open;
            if (open && open.length > 1) {
                obj.openStart = open[0];
                obj.openEnd = open[1];
            }
            else {
                obj.openStart = opt.dayStart;
                obj.openEnd = opt.dayEnd;
            }
            objs.push(obj);
            var ts = this.ts;//繁忙时段
            if (ts) {
                var len = ts.length;
                for (var i = 0; i < len; i++) {
                    var plan = {};
                    plan.objId = this.id;
                    plan.allDay = this.islong;
                    plan.start = ts[i].start;
                    plan.end = ts[i].end;
                    plan.state = ts[i].state || "busy";
                    plan.occupy = ts[i].occupy;
                    plan.title = ts[i].title;
                    plans.push(plan);
                }
            }
            var rs = this.rsvs;//预约时段 教学系统
            if (rs) {
                for (var j = 0; j < rs.length; j++) {
                    var plan = {};
                    var ltch = rs[j].teachTime;
                    plan.objId = this.id;
                    plan.start = parseInt(ltch / 100);
                    plan.end = parseInt(ltch / 10000) * 100 + (ltch % 100);
                    plan.state = rs[j].state;
                    plan.title = rs[j].teacher;//+ "<br/>" + rs[j].groupName;
                    plans.push(plan);
                }
            }
            var cls = this.cls;//关闭时段
            if (cls) {
                for (var k = 0; k < cls.length; k++) {
                    var plan = {};
                    plan.objId = this.id;
                    plan.start = cls[k].start;
                    plan.end = cls[k].end;
                    plan.state = "close";
                    plan.occupy = "true";
                    plan.title = cls[k].title;
                    plans.push(plan);
                }
            }
        });
        var t = {};
        t.objs = objs;
        t.plans = plans;
        return t;
    }
})(jQuery, uni);