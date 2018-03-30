<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dlg_basic.ascx.cs" Inherits="ClientWeb_pro_net_dlg_basic" %>
<!--时间选择器-->
<div id="dlg_basic_dt_selecter" class="hidden">
    <table>
        <tbody class="tmp_time">
            <tr class="md_date">
                <td><span class="uni_trans">日期</span></td>
                <td>
                    <span class="mt_date"></span>
                </td>
            </tr>
            <tr class="md_date">
                <td><span class="uni_trans">时间</span></td>
                <td>
                    <div>
                        <span>
                            <select name="start_time" class="mt_start_time" style="width: 80px;"></select>
                        </span>
                        <span>&nbsp;-&nbsp;</span>
                        <span>
                            <select name="end_time" class="mt_end_time" style="width: 80px;"></select>
                            <a class="sub_picker hidden" onclick="$(this).parents('tr:first').remove();">&nbsp;<span class="glyphicon glyphicon-minus-sign text-danger"></span></a>
                            <a class="add_picker hidden">&nbsp;<span class="glyphicon glyphicon-plus-sign text-primary"></span></a></span>
                    </div>
                </td>
            </tr>
        </tbody>
        <tbody class="tmp_date">
            <tr class="md_date">
                <td><span class="uni_trans">开始日期</span></td>
                <td><span class="mt_date"></span>
                    <input type="hidden" name="start_date" class="mt_start_date control-form" style="width: 120px;" />
                    <input type="hidden" class="open_start" name="open_start" />
                    <input type="hidden" class="open_end" name="open_end" />
                </td>
            </tr>
            <tr class="md_date">
                <td><span class="uni_trans">结束日期</span></td>
                <td>
                    <select name="end_date" class="mt_end_date" style="width: 140px;"></select></td>
            </tr>
        </tbody>
        <tbody class="tmp_cycledate">
            <tr class="md_date">
                <td><span class="uni_trans">时间</span></td>
                <td>
                    <input type="hidden" name="start" class="cycle_start" />
                    <input type="hidden" name="end" class="cycle_end" />
                                <div class="btn-group">
                <button type="button" class="btn btn-info set_cycle_date"><span class="uni_trans">设置时间</span></button>
                <button type="button" class="btn btn-default calc_detail_date" disabled><span class="uni_trans">查看详细时间</span></button>
            </div>
                </td>
            </tr>
            <tr class="md_date">
                <td><span class="uni_trans">描述</span></td>
                <td><span class="uni_trans cycle_desc">时间未设置</span></td>
            </tr>
        </tbody>
        <tbody class="tmp_datetime">
            <tr class="md_date">
                <td><span class="uni_trans">开始时间</span></td>
                <td><span class="mt_date" style="width: 140px; display: inline-block;"></span>
                    <span>
                        <select name="start_time" class="mt_start_time" style="width: 80px;"></select></span>
                </td>
            </tr>
            <tr class="md_date">
                <td><span class="uni_trans">结束时间</span></td>
                <td>
                    <span style="width: 140px; display: inline-block;">
                        <select name="end_date" class="mt_end_date" style="width: 140px;"></select></span>
                    <span>
                        <select name="end_time" class="mt_end_time" style="width: 80px;"></select>
                        <a class="sub_picker hidden" onclick="$(this).parents('tr:first').remove();">&nbsp;<span class="glyphicon glyphicon-minus-sign text-danger"></span></a>
                        <a class="add_picker hidden">&nbsp;<span class="glyphicon glyphicon-plus-sign text-primary"></span></a></span>
                </td>
            </tr>
        </tbody>
        <tbody class="tmp_fix">
            <tr class="md_date">
                <td><span class="uni_trans">预约时段</span></td>
                <td>
                    <span>
                        <input type="hidden" name="start" class="mt_start" />
                        <input type="hidden" name="end" class="mt_end" />
                        <select class="mt_fix_time" style="width: 120px;"></select></span>
                </td>
            </tr>
        </tbody>
    </table>
</div>
<div id="dlg_basic_mb_add" class="hidden">
    <div class="tmp_complex">
        <div class="form-group" style="margin-bottom: 0;">
            <input type="hidden" name="group_id" class="group_id" />
            <input type='hidden' class="mb_list" name='mb_list' />
            <div class="btn-group">
                <button type="button" class="btn btn-default group_name" disabled><span class="uni_trans">小组未创建</span></button>
                <button type="button" class="btn btn-info set_group_mb"><span class="uni_trans">设置小组</span></button>
            </div>
        </div>
    </div>
    <div class="tmp_simple">
        <div>
            <input type="hidden" class="min_user" name="min_user" />
            <input type="hidden" class="max_user" name="max_user" />
            <input type='hidden' class="mb_list" name='mb_list' />
            <div class="input-group" style="width: 180px;">
                <span class="input-group-addon" title="<%=Translate(szSearchKey) %>">+</span>
                <input class="mb_name_ipt form-control hint" type="text" url="searchAccount.aspx" placeholder="<%=Translate(szSearchKey) %>" onclick="this.value = ''" />
            </div>
        </div>
        <div class="dialog">
            <div style="width: 200px; color: grey;"><span class="uni_trans"><%=Translate("组成员名单")%></span></div>
            <%--            <ul class="ul_member memList ul_items">
            </ul>--%>
        </div>
    </div>
</div>
<div id="dlg_basic_cycle_dt" class="hidden">
    <table>
        <tbody class="tmp_cycle">
            <tr>
                <td class="title text-right">选择日期：</td>
                <td>
                    <input type="text" class="date_start sel_date must" data-msg="开始日期必填" name="cycle_date_start" readonly="readonly">
                    <span class="single_hide">-
                    <input type="text" class="date_end sel_date must" data-msg="结束日期必填" name="cycle_date_end" readonly="readonly">
                    <span style="float: right;">&nbsp;<a class="click cmp_detail">查看详细日期</a>&nbsp;|&nbsp;<a class="click cvt_single_day">转单日时间</a>&nbsp;</span></span>
                    <span style="float: right;" class="cycle_hide">&nbsp;<a class="click cvt_cycle_day">转周期时间</a>&nbsp;</span>
                </td>
            </tr>
            <tr>
                <td class="title text-right">选择时间：</td>
                <td>
                    <input type="text" name="cycle_time_start" class="time_start sel_time must" data-msg="开始时间必填" readonly="readonly">
                    -
                    <input type="text" name="cycle_time_end" class="time_end sel_time must" data-msg="结束时间必填" readonly="readonly"></td>
            </tr>
            <tr class="single_hide">
                <td class="title text-right">时间周期：</td>
                <td>
                    <div class="sel_time_panel">
                        每&nbsp;<select name="cycle_freq" class="cycle_freq" style="width: 40px">
                            <option value="1" selected>1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                        </select>&nbsp;<select name="cycle_type" class="cycle_type">
                            <option value="d" selected>天</option>
                            <option value="w">周</option>
                            <option value="m">月</option>
                        </select>
                        <span class="view_week" style="display: none;">
                            <label>
                                <input type="checkbox" class="cycle_week" name="cycle_week" value="1" />星期一&nbsp;</label>
                            <label>
                                <input type="checkbox" class="cycle_week" name="cycle_week" value="2" />星期二&nbsp;</label>
                            <label>
                                <input type="checkbox" class="cycle_week" name="cycle_week" value="3" />星期三&nbsp;</label>
                            <label>
                                <input type="checkbox" class="cycle_week" name="cycle_week" value="4" />星期四&nbsp;</label>
                            <label>
                                <input type="checkbox" class="cycle_week" name="cycle_week" value="5" />星期五&nbsp;</label>
                            <label>
                                <input type="checkbox" class="cycle_week" name="cycle_week" value="6" />星期六&nbsp;</label>
                            <label>
                                <input type="checkbox" class="cycle_week" name="cycle_week" value="0" />星期日&nbsp;</label>
                        </span>
                        <span class="view_month" style="display: none;">，<input type="text" name="cycle_day" class="cycle_day must" data-msg="请选择日期" style="width: 40px;" />日
                        </span>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>
<script>
    //基础模块
    pro.d.basic = {
        //添加时间选择器 panel 容器 obj 预约状态对象 type 选择器类别 obj参数除resvTimeClick需求外：时间[start] [end]日期[startDate] [endDate] 整日跨天需要openStart openEnd
        addDateTimePicker: function (panel, obj, type) {
            function dateSel($sel, v,dft) {
                $sel.html();
                var day = uni.parseDate(v + " 00:00");
                var end = Math.ceil((obj.max || 1440) / 1440);
                var start = Math.floor((obj.min || 0) / 1440);
                for (var i = 0; i < end; i++) {
                    var dt = day.format("yyyy-MM-dd");
                    $sel.append('<option value="' + dt + '">' + dt + '</option>');
                    day.addDays(1);
                }
                $sel.val(dft||v);
            }
            function timeInt(v) {
                if (v.length > 5)
                    v = v.substr(v.length - 5);
                var tmp = v.split(":");
                if (tmp.length < 2) return 0;
                return parseInt(tmp[0], 10) * 100 + parseInt(tmp[1], 10);
            }
            if (!uni.isNoNull([panel, obj, obj.date])) {
                uni.msgBox("参数有误" + uni.hide("addDateTimePicker"));
                return;
            }
            panel = $(panel);
            var date = obj.date;
            var qz = $("#dlg_basic_dt_selecter");
            var para = obj;
            var horizon = type == "horizon" ? " .md_date" : "";
            var str = "";
            para.unit = "<%=GetConfig("resvTimeUnit")%>"||"10";
            if (!obj.isAdd)
                panel.html("");//清空
            var picker;
            if (para.fix) {//固定时段
                $(".tmp_fix" + horizon, qz).each(function () { str += $(this).html(); });
                if (type == "horizon") str = "<tr>" + str + "</tr>";
                picker = $(str);
                var sel = $(".mt_fix_time", picker);
                var now=new Date();
                var today=now.format("yyyy-MM-dd")==date;
                var e=now.getHours()*100+now.getMinutes();
                for (var i = 0; i < obj.ops.length; i++) {
                    var op=obj.ops[i];
                    var start=op.start;
                    var end = op.end;
                    var flg = false;
                    if(today){
                        if(timeInt(end)<=e){
                            continue;
                        }
                        if(timeInt(start)<=e){
                            flg = true;
                    }
                    }
                    sel.append("<option value='" +date+" " +start + "&" +date+" "+ end + "'>" + (flg ? "现在" : start) + " - " + end + "</option>");
                }
                    var mt_start = picker.find(".mt_start");
                    var mt_end = picker.find(".mt_end");
                    sel.change(function () {
                        var v = $(this).val();
                        var tm = v.split("&");
                        if (tm.length == 2) {
                            mt_start.val(tm[0]);
                            mt_end.val(tm[1]);
                        }
                    });
                    sel.change();
            }
            else if (obj.allowLong) {//长期
                if ("<%=GetConfig("resvAllDay")%>" == "1") {//整日
                    $(".tmp_date" + horizon, qz).each(function () { str += $(this).html(); });
                    if (type == "horizon") str = "<tr>" + str + "</tr>";
                    picker = $(str);
                    $(".mt_start_date", picker).val(obj.startDate || date);
                    dateSel($(".mt_end_date", picker), obj.startDate || date, obj.endDate);
                    $(".open_start", picker).val(obj.openStart);
                    $(".open_end", picker).val(obj.openEnd);
                    //multiTime();
                }
                else {//跨天
                    $(".tmp_datetime" + horizon, qz).each(function () { str += $(this).html(); });
                    if (type == "horizon") str = "<tr>" + str + "</tr>";
                    picker = $(str);
                    var mt_end = $(".mt_end_date", picker);
                    dateSel(mt_end, obj.startDate || date, obj.endDate);
                    $(".mt_start_time", picker).resvTimeClick($(".mt_end_time", picker), para, mt_end);
                    //multiTime();
                }
            }
            else if (obj.cycleDate) {//周期
                $(".tmp_cycledate" + horizon, qz).each(function () { str += $(this).html(); });
                if (type == "horizon") str = "<tr>" + str + "</tr>";
                picker = $(str);
                $(".set_cycle_date", picker).click(function () {
                    var pl = $("<div><table class='cycle_date_tbl'></table></div>");
                    var tp = {
                        stepMinute: obj.unit,
                        startDate: obj.startDate || date,
                        endDate: obj.endDate || date,
                        startTime: obj.start,
                        endTime: obj.end
                    };
                    pro.d.basic.cycleDateTimePicker($("table", pl), tp);
                    uni.dlg(pl, "周期时间选择", 680, 200, function (dlg) {
                        var rlt = pro.d.basic.analysisDateTime(dlg);
                        if (rlt) {
                            picker.find(".cycle_desc").html(rlt.desc);
                            picker.find(".cycle_start").val(rlt.start);
                            picker.find(".cycle_end").val(rlt.end);
                            picker.find(".calc_detail_date").removeAttr("disabled").click(function () {
                                var dts = rlt.date;
                                var str = "<strong>" + rlt.desc + "</strong><br/><br/>";
                                for (var i = 0; i < dts.length; i++)
                                    if (dts[i]) {
                                        var dt = uni.parseDate(dts[i]);
                                        str += dt.format("yyyy-MM-dd，星期E") + "<br/>";
                                    }
                                uni.msgBox(str, "详细日期");
                            });
                            $(dlg).dialog("close");
                        }
                    });
                });
            }
            else {//当日
                $(".tmp_time" + horizon, qz).each(function () { str += $(this).html(); });
                if (type == "horizon") str = "<tr>" + str + "</tr>";
                picker = $(str);
                $(".mt_start_time", picker).resvTimeClick($(".mt_end_time", picker), para);
                //multiTime();
            }
            $(".mt_date", picker).html(date);
            panel.append(picker);
            return picker;
            //function multiTime() {//多时段选择 必须水平样式
            //    if ("=GetConfig("allowMultiTime")" == "1" && type == "horizon") {
            //        if (obj.isAdd) {
            //            $(".add_picker", picker).remove();
            //        }
            //        else {
            //            $(".sub_picker", picker).remove();
            //            $(".add_picker", picker).click(function () {
            //                obj.isAdd = true;
            //                pro.d.basic.addDateTimePicker(panel, obj, type);
            //                obj.isAdd = false;
            //            });
            //        }
            //    }
            //    else {
            //        $(".sub_picker", picker).remove();
            //        $(".add_picker", picker).remove();
            //    }
            //}
        },
        //添加/维护成员
        mGroupMembers: function (panel, opt) {
            var qz = $("#dlg_basic_mb_add");
            if (opt && opt.md == "complex") {
                $(panel).html($(".tmp_complex", qz).html()).find(".set_group_mb").click(setStudents);
            }
            else {
                var gm = $(panel).html($(".tmp_simple", qz).html());
                $(".min_user", gm).val(opt.min);
                $(".max_user", gm).val(opt.max);
                var p = $(".dialog", gm);
                var ul = $(".ul_items", p);
                var mbList = $(".mb_list", gm);
                //var mbs = uni.getHash();//获取哈希表
                var pop = uni.pop(gm, {
                    con: p, orien: "right", colseBtn: false,except:[pro.acc.id], delItemFun: function (rlt) {
                        mbList.val(rlt.keys().join());
                    }
                });
                $(".mb_name_ipt", gm).procomplete(function (event, ui) {
                    debugger;
                    if (ui.item) {
                        if (ui.item.id && ui.item.id != "") {
                            //if (ui.item.szLogonName == pro.acc.id) { uni.msgBox("无需添加本人"); return; }
                            if (pop.items.size() == 0) pop.addItem(pro.acc.id,pro.acc.name);//默认加入本人
                            if (pop.items.size() < parseInt(opt.max))
                                mbList.val((pop.addItem(ui.item.szLogonName, ui.item.name)).keys().join());
                            else
                                uni.msgBox("组成员已满");
                        }
                    }
                });
            }
            //设置组
            function setStudents() {
                var pg_group = $(".group_id", panel);
                var para = uni.getObj(opt) || {};
                para.mb_accno = pro.acc.accno;
                if (pg_group.val()) {
                    para.group = pg_group.val();
                }
                parent.pro.d.group.manage('维护组成员', para, function (d) {
                    if (d.group_id) {//后台优先组号
                        pg_group.val(d.group_id);
                        $(".group_name", panel).html(d.group_name + "(<span class='red'>" + d.group_num + "</span>人)");
                    }
                    else if (d.mb_acc_list) {
                        pg_group.val('');
                        $(".mb_list", panel).val(d.mb_acc_list);
                        $(".group_name", panel).html(d.group_name + "(<span class='red'>" + d.group_num + "</span>人)");
                    }
                })
            }
        },
        //添加周期时间选择器
        cycleDateTimePicker: function (panel, para) {
            if (!para) para = {};
            var qz = $("#dlg_basic_cycle_dt");
            var tmp = $(panel).html($(".tmp_cycle", qz).html());
            //时间周期
            var cyc = $(".cycle_type", tmp);
            cyc.val(para.type || "d");//默认日
            cyc.change(function () {
                var pthis = $(this);
                var w = pthis.parent().find(".view_week").hide();
                var m = pthis.parent().find(".view_month").hide();
                if (pthis.val() == "w")
                    w.show();
                else if (pthis.val() == "m")
                    m.show();
            });
            cyc.trigger("change");
            //模式转换
            tmp.find(".cvt_single_day").click(function () {
                tmp.find(".single_hide").hide();
                tmp.find(".cycle_hide").show();
            });
            if (para.singleText)
                tmp.find(".cvt_single_day").html(para.singleText);
            tmp.find(".cvt_cycle_day").click(function () {
                tmp.find(".cycle_hide").hide();
                tmp.find(".single_hide").show();
            });
            if (para.cycleText)
                tmp.find(".cvt_cycle_day").html(para.cycleText);
            //选时控件
            var selDate = $(".sel_date", tmp);
            selDate.datepicker({
                minDate: 0
            });
            var selTime = $(".sel_time", tmp);
            if (selTime.timepicker) {
                selTime.timepicker({
                    controlType: 'select',
                    timeFormat: "HH:mm",
                    stepHour: para.stepHour || 1,
                    stepMinute: para.stepMinute || parseInt("<%=GetConfig("resvTimeUnit")%>"||0),
                    hourMin: para.hourMin || 6,
                    hourMax: para.hourMax || 23
                });
            }
            //时间联动
            var tm_start = $(".time_start", tmp);
            var tm_end = $(".time_end", tmp);
            tm_start.change(function () {
                var sta=tm_start.val();
                var en = tm_end.val();
                if (sta) {
                    if (en) {
                        var i_sta = parseInt(sta.replace(":", ""));
                        var i_en = parseInt(en.replace(":", ""));
                        if (i_en < i_sta) tm_end.val(sta);
                    }
                    else
                        tm_end.val(sta);
                }
            });
            //初始化时间
            $(".date_start", tmp).val(para.startDate || "");
            $(".date_end", tmp).val(para.endDate || "");
            $(".time_start", tmp).val(para.startTime || "");
            $(".time_end", tmp).val(para.endTime || "");
            if (para.weeks && cyc.val() == "w") {//周
                $(".cycle_week", tmp).each(function () {
                    var pthis = $(this);
                    if (uni.isInArray(pthis.val(), para.weeks))
                        pthis.attr("checked", true);
                });
            }
            if (para.day && cyc.val() == "m") {//月
                $(".cycle_day", tmp).val(para.day);
            }
            //频率
            $(".cycle_freq", tmp).val(para.freq || "1");//默认1
            //计算详细日期
            $(".cmp_detail", tmp).click(function () {
                var rlt = pro.d.basic.analysisDateTime(tmp);
                if (rlt) {
                    var dts = rlt.date;
                    var str = "<strong>" + rlt.desc + "</strong><br/><br/>------------------------<br/>";
                    for (var i = 0; i < dts.length; i++) {
                        if (dts[i]) {
                            var dt = uni.parseDate(dts[i]);
                            str += dt.format("yyyy-MM-dd，星期E") + "<br/>";
                        }
                    }
                    uni.msgBox(str, "详细日期");
                }
            });
            //默认模式
            if (para.dftM == "single")
                tmp.find(".cvt_single_day").trigger("click");
            else
                tmp.find(".cvt_cycle_day").trigger("click");
        },
        //周期时间选择器 计算周期时间
        analysisDateTime: function (info) {
            info = $(info);
            if (!$(info).mustItem()) return false;
            //取值
            var d_start = $(".date_start", info).val();
            var d_end = $(".date_end", info).val();
            var t_start = $(".time_start", info).val();
            var t_end = $(".time_end", info).val();
            var type = $(".cycle_type", info).val();
            var freq = parseInt($(".cycle_freq", info).val());
            var ws = $(".cycle_week:checked", info);
            var d = parseInt($(".cycle_day", info).val() || 0);
            if (uni.compareDate(uni.parseDate(d_start + " " + t_start), new Date(), "m") <= 0) { uni.msgBox("所选时间不能早于当前时间"); return false; }
            //单日模式
            if (info.find(".cycle_hide").is(":visible")) {
                return { start: [d_start + " " + t_start], end: [d_start + " " + t_end], desc: d_start + " " + t_start+"-"+t_end, date: [d_start], startTime: t_start, endTime: t_end, startDate: d_start, endDate: d_start, type: "d", freq: 1 };
            }
            //检查周次
            if (type == "w" && ws.length == 0) { uni.msgBox("至少勾选一个周次"); return false; }
            //初始值
            var rlt = { start: [], end: [], desc: "", date: [], startTime: t_start, endTime: t_end, startDate: d_start, endDate: d_end, weeks: [], day: d, type: type, freq: freq };
            //
            rlt.desc = d_start + "至" + d_end + "," + t_start + "-" + t_end;
            var dstart = uni.parseDate(d_start);
            var dend = uni.parseDate(d_end);
            var dt = uni.parseDate(d_start);
            var today = new Date();
            if (uni.compareDate(dt, today) < 0) dt = today;
            var len = uni.compareDate(dend, dt);
            if (len > 366) len = 366;//防止恶意
            if (type == "d") {
                rlt.desc += "(每" + freq + "天)";
                for (var i = 0; i <= len; i += freq) {
                    var fmt = dt.format("yyyy-MM-dd");
                    rlt.date.push(fmt);
                    rlt.start.push(fmt + " " + t_start);
                    rlt.end.push(fmt + " " + t_end);
                    dt.addDays(freq);
                }
            }
            else if (type == "w") {
                var chi = ["日", "一", "二", "三", "四", "五", "六"];
                rlt.desc += "(每" + freq + "周,星期";
                for (var k = 0; k < ws.length; k++) {
                    var w = parseInt(ws[k].value);
                    rlt.weeks.push(w);
                    rlt.desc += (k == 0 ? "" : "/") + chi[w];
                }
                for (var i = 0; i <= len; i++) {
                    if (uni.isInArray(dt.getDay(), rlt.weeks)) {
                        fmt = dt.format("yyyy-MM-dd");
                        rlt.date.push(fmt);
                        rlt.start.push(fmt + " " + t_start);
                        rlt.end.push(fmt + " " + t_end);
                        if (dt.getDay() == rlt.weeks[rlt.weeks.length - 1]) {
                            dt.addDays(7 * (freq - 1));
                            i += 7 * (freq - 1);
                        }
                    }
                    dt.addDays(1);
                }

                rlt.desc += ")";
            }
            else if (type == "m") {
                rlt.desc += "(每" + freq + "月," + d + "日)";
                dt.setDate(d);
                if (uni.compareDate(dt, dstart) < 0) dt.addMonths(1);
                while (uni.compareDate(dt, dend) <= 0) {
                    fmt = dt.format("yyyy-MM-dd");
                    rlt.date.push(fmt);
                    rlt.start.push(fmt + " " + t_start);
                    rlt.end.push(fmt + " " + t_end);
                    dt.addMonths(freq);
                }
            }
            if (rlt.date.length > 0)
                return rlt;
            else {
                uni.msgBox("未取到任何有效的日期");
                return false;
            }
        }
    }
</script>
<style>
    .dialog .ul_items li { min-width: 200px; line-height: 20px; height: 22px; font-size: 12px; background: #BFE5F0; border: 1px solid #eee; margin: 1px; padding: 1px 2px; position: relative; color: #666; overflow: hidden; }
    .dialog .ul_items li .del { font-size: 16px; font-weight: bold; position: absolute; top: 1px; right: 2px; color: #000; opacity: .2; background: #BFE5F0; }
    .dialog .cycle_date_tbl { width: 100%; }
    .dialog .cycle_date_tbl tr td { vertical-align: middle; height: 46px; }
    .dialog .cycle_date_tbl tr td:first-child { width: 87px; }
    .dialog .cycle_date_tbl .sel_time { width: 60px; }
    .dialog .cycle_date_tbl select, .dialog .cycle_date_tbl input[type=text] { height: 28px; }
</style>
