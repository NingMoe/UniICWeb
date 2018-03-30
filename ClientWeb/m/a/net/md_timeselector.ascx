<%@ Control Language="C#" AutoEventWireup="true" CodeFile="md_timeselector.ascx.cs" Inherits="ClientWeb_m_net_md_timeselector" %>
<!--时间选择器-->
<div id="dlg_basic_dt_selector" class="hidden">
    <ul class="tmp_time">
        <li class="md_date">
            <div class="item-content">
                <div class="item-inner">
                    <div class="item-title label"><%=Translate("预约时间")%></div>
                    <div class="item-input">
                        <div class="visual_div"><span class="grey">...</span></div>
                        <input type="text" class="mt_sel_time must" placeholder="..."  style="height:0;"/>
                    </div>
                </div>
            </div>
        </li>
    </ul>
    <ul class="tmp_date">
        <li class="md_date">
            <div class="item-content">
                <div class="item-inner">
                    <div class="item-title label"><%=Translate("结束日期")%></div>
                    <div class="item-input">
                        <input type="text" class="mt_end_date must text-center" name="end_date" placeholder="..." style="padding: 0" />
                        <input type="hidden" name="start_date" class="mt_start_date" />
                        <input type="hidden" class="open_start" name="open_start" />
                        <input type="hidden" class="open_end" name="open_end" />
                    </div>
                </div>
            </div>
        </li>
    </ul>
    <ul class="tmp_datetime">
        <li class="md_date">
            <div class="item-content">
                <div class="item-inner">
                    <div class="item-title label"><%=Translate("开始时间")%></div>
                    <div class="item-input">
                        <input type="text" name="start" class="mt_start_date must" placeholder="..."   style=""/>
                    </div>
                </div>
            </div>
        </li>
        <li class="md_date">
            <div class="item-content">
                <div class="item-inner">
                    <div class="item-title label"><%=Translate("结束时间")%></div>
                    <div class="item-input">
                        <input type="text" name="end" class="mt_end_date must" placeholder="..."   style=""/>
                    </div>
                </div>
            </div>
        </li>
    </ul>
    <ul class="tmp_fix">
        <li>
            <div class="item-content">
                <div class="item-inner">
                    <div class="item-title label"><%=Translate("预约时段")%></div>
                    <div class="item-input">
                        <input type="hidden" name="start" class="mt_start" />
                        <input type="hidden" name="end" class="mt_end" />
                        <select class="mt_fix_time" style="width: 120px;"></select>

                    </div>
                </div>
            </div>
        </li>
        </ul>
</div>
<script>
    //添加时间选择器 panel 容器 obj 预约状态对象 type 选择器类别 obj参数除resvTimeClick需求外：时间[start] [end]日期[startDate] [endDate] 整日跨天需要openStart openEnd
    pro.md.timeselector = function (app, obj, container) {

        var $ = Dom7;
        if (!uni.isNoNull([obj, obj.date])) {
            uni.msgBox("参数有误：pro.md.timeselector");
            return;
        }
        var date = obj.date;
        var qz = $("#dlg_basic_dt_selector");
        var para = obj;
        var str = "";
        para.unit = "<%=GetConfig("resvTimeUnit")%>"||"10";
        var hours = [];
        var minutes = [];
        
        for (var m = 0; m <obj.ops.length; m++) {
            var h_start = obj.ops[m].start ? parseInt(obj.ops[m].start.split(':')[0]) : 0;
            var h_end = obj.ops[m].end ? parseInt(obj.ops[m].end.split(':')[0]) : 23;
            //var h_start = para.openStart ? parseInt(para.openStart.split(':')[0]) : 0;
            //var h_end = para.openEnd ? parseInt(para.openEnd.split(':')[0]) : 23;
            for (var i = h_start; i <= h_end; i++) {
                if (i < 10) hours.push('0' + i);
                else hours.push('' + i);
            }
            var unit = parseInt(para.unit);
            for (var j = 0; j < (60 / unit) ; j++) {
                var v = j * unit;
                if (v < 10) minutes.push('0' + v);
            else 
                minutes.push('' + v);
            }
        
        }
        var picker;
        if (para.fix) {//固定时段
            picker = container || $(".tmp_fix li", qz).clone();
            var sel = $(".mt_fix_time", picker).html("");
            var now = new Date();
            var today = now.format("yyyy-MM-dd") == date;
            var e = now.getHours() * 100 + now.getMinutes();
            for (var i = 0; i < obj.ops.length; i++) {
                var op = obj.ops[i];
                var start = op.start;
                var end = op.end;
                var flg = false;
                if (today) {
                    if (timeInt(end) <= e) {
                        continue;
                    }
                    if (timeInt(start) <= e) {
                        flg = true;
                    }
                }
                sel.append("<option value='" + date + " " + start + "&" + date + " " + end + "'>" + (flg ? uni.translate("现在") : start) + " - " + end + "</option>");
            }
            var mt_start = picker.find(".mt_start");
            var mt_end = picker.find(".mt_end");
            if (!container) {
                sel.change(function () {
                    var v = $(this).val();
                    var tm = v.split("&");
                    if (tm.length == 2) {
                        mt_start.val(tm[0]);
                        mt_end.val(tm[1]);
                    }
                });
            }
            sel.change();
        }
        else if (obj.allowLong) {//长期
            if ("<%=GetConfig("resvAllDay")%>" == "1") {//整日
                picker = container || $(".tmp_date li", qz).clone();
                picker.find(".mt_start_date").val(date);
                dateSel(picker.find(".mt_end_date"), date, container ? picker.find(".mt_end_date").val() : obj.endDate);
                picker.find(".open_start").val(obj.openStart);
                picker.find(".open_end").val(obj.openEnd);
            }
            else {//跨天
                picker = container || $(".tmp_datetime li", qz).clone();
                var mt_start = picker.find(".mt_start_date");
                var mt_end = picker.find(".mt_end_date");
                if (uni.isNull(container)) {
                    mt_start.val(obj.start || '');
                    mt_end.val(obj.end || '');
                }
                dateSel(mt_start, date, true);
                dateSel(mt_end, date);
            }
        }
        else {//当日
            picker = container || $(".tmp_time li", qz).clone();
            if (!uni.isNull(container)) {
                var vir = picker.find(".visual_div").html().split('-');
                if (vir.length > 1) {
                    para.start = $.trim(vir[0]);
                    para.end = $.trim(vir[1]);
                }
            }
            var mt = picker.find(".mt_sel_time").resvTimeClick(app, para);
        }
        //处理函数
        function timeInt(v) {
            if (v.length > 5)
                v = v.substr(v.length - 5);
            var tmp = v.split(":");
            if (tmp.length < 2) return 0;
            return parseInt(tmp[0], 10) * 100 + parseInt(tmp[1], 10);
        }
        function dateSel(input, dt, extra) {
            var max = Math.ceil((obj.max || 1440) / 1440);
            var min = Math.floor((obj.min || 0) / 1440);
            var minDate = (uni.parseDate(dt)).addDays(min);
            var maxDate = max ? ((uni.parseDate(dt)).addDays(max)) : undefined;
            if ("<%=GetConfig("resvAllDay")%>" == "1") {//整日
                var end = uni.parseDate(extra || dt);
                if (uni.compareDate(minDate, end) > 0) end = minDate;
                else if (uni.compareDate(maxDate, end) < 0) end = maxDate;
                input.val(end.format("yyyy-MM-dd"));
                if (input[0].calendar) {
                    input[0].calendar.destroy();
                }
                input[0].calendar = app.calendar($.extend(pro.getCldPara(), {
                    input: input[0],
                    value: [end],
                    minDate: minDate,
                    maxDate: maxDate
                }));
            }
            else {//跨天
                var dates = [];
                if (extra) {
                    dates.push(dt);
                }
                else {
                    for (var begin = minDate; uni.compareDate(begin, maxDate) <= 0; begin.addDays(1)) {
                        dates.push(begin.format("yyyy-MM-dd"));
                    }
                }
                if (input[0].picker) {
                    input[0].picker.destroy();
                }
                    //实例化控件
                    input[0].picker = app.picker({
                        input: input[0],
                        toolbarCloseText: uni.translate('确认'),
                        formatValue: function (picker, values) {
                            if (values.length == 4)
                                return values[0] + " " + values[1]+":"+values[3];
                            else
                                return "";
                        },
                        cols: [
                            {
                                values: dates
                            },
                            {
                                values: hours
                            },
                            {
                                values: [':']
                            },
                            {
                                values: minutes
                            }
                        ]
                    });
            }
        }
        //返回
        picker.find(".mt_date").val(date);
        return picker;
    }
</script>
