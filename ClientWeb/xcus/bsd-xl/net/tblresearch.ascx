<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tblresearch.ascx.cs" Inherits="ClientWeb_xcus_bsd_xl_net_tblresearch" %>
<div>
    <div class="uni_cld"></div>
    <div id="rt_rsv_apply" title="预约申请" class="resv_panel dialog" style="display: none;">
        <form name="resvForm" id="resvForm" onsubmit="return false;">
            <div style="position: relative; height: 40px;">
                <div class="ui-tooltip ui-widget ui-widget-content ui-corner-all" style="top: 2px; left: 20px; max-width: 700px; font-size: 12px;" id="oth_info"></div>
            </div>
            <div class="resv_tbl">
                <table border="1" style="width: 760px;">
                    <tbody>
                        <tr>
                            <td colspan="4" style="text-align: left;" class="colored resv_tbl_h">
                                <input type="hidden" name="dev_id" />
                                预约实验室：<span id="rsv_dev_name"></span> | 申请人：<span class="my_name"></span> | 使用人：项目组</td>
                        </tr>
                        <tr>
                            <td class="colored th">
                                <div style="width: 76px;"><span style="color: red;">*</span>实验名称</div>
                            </td>
                            <td class="con" style="width: 289px;">
                                <input name="test_name" type="text" class="ipt test_name" /></td>
                            <td class="colored th">
                                <div style="width: 66px;">
                                    <span style="color: red;">*</span>
                                    实验项目
                                </div>
                            </td>
                            <td class="con" style="width: 260px;">
                                <input type="hidden" name="rt_id" />
                                <input type="hidden" name="rt_name" />
                                <span class="rt_name"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="colored th"><span style="color: red;">*</span>开始日期</td>
                            <td class="con">
                                <input type="text" id="rt_begin_date" name="start" class="Wdate" readonly="readonly" /></td>

                            <td class="colored th">
                                <div style="width: 66px;"><span style="color: red;">*</span>结束日期</div>
                            </td>
                            <td class="con">
                                <input type="text" id="rt_end_date" name="end" class="Wdate" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td class="colored th"><span style="color: red;">*</span>申请说明</td>
                            <td colspan="3">
                                <textarea rows="4" cols="30" name="memo" style="width: 594px;" class="ipt memo" maxlength="60"></textarea></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div_remark" id="div_remark">
                本实验室当日开放时间：<span class="red open_t_start"></span> 到 <span class="red open_t_end"></span>；申请预约最长可提前：<span class="red rule_t_earliest"></span>
                至少提前：<span class="red rule_t_latest"></span>；预约时间最长：<span class="red rule_t_max"></span> 最短：<span class="red rule_t_min"></span>。
            </div>
            <div class="div_submit" style="text-align: center;">
                <input type="button" class="sub_form" id="rt_sub_resv" onclick="subRTResv(this); return false;" value="提交" />
                <input type="button" class="sub_form" onclick="$('#rt_rsv_apply').dialog('close'); return false;" value="返回" />
            </div>
        </form>
    </div>
    <style type="text/css">
        .uni_cld { width: 700px; }
        #div_remark { margin: 3px 10px; padding: 5px; border: 1px dashed #ccc; background-color: #fbfbf3; color: #666; line-height: 16px; }
    </style>
    <script type="text/javascript">
        var unicld;
        var valid_time;
        function rtSelCheck() {
            var rtsel = $("#rtSel");
            $("#rtId").val(rtsel.val());
            if (!uni.isNull(rtsel.val()) && rtsel.val() != "0") {
            }
            else {
            }
        }
        function loadUniCalendar() {
            var req = uni.getReq();
            var purl = {};
            var rtid = req["rtId"] || "<%=RtId%>";
            if (uni.isNull(rtid)) return;
            purl.classkind = req["classKind"] || "<%=ClassKind%>";
            purl.islong = req["isLongResv"] || "<%=IsLong%>";
            purl.classid = req["classId"] || "<%=DevClassId%>";
            purl.devid = req["dev"] || "<%=DevId%>";
            purl.labid = req["labId"] || "<%=LabId%>";
            valid_time = "<%=ValidTime%>";
            $("#oth_info").html("剩余可预约时长：<span class='red'>" + pro.dt.m2ms(valid_time) + "</span>");
            var selH = req["click"] ? undefined : selHours;
            var width = parseInt(req["w"] || "700");
            unicld = $(".uni_cld").uniCalendar({
                modes: "<%=Mode%>",
                width: width,
                evSelTime: selH,
                evUpTime: function (date, callback) {
                    var pra = {};
                    pra.dev_id = purl.devid;
                    pra.lab_id = purl.labid;
                    pra.class_id = purl.classid;
                    pra.purpose = 8;//科研预约
                    pra.date = date.format("yyyyMMdd");
                    pra.classkind = purl.classkind;
                    pro.j.dev.getRsvSta(pra, function (rlt) {
                        callback(rlt.data, "unilab3");
                    });
                },
                evUpPlans: function (obj, start, end, callback, opt) {
                    var md = opt.mode;
                    start = start.format("yyyyMMdd");
                    end = end.format("yyyyMMdd");
                    if (md == "m" && uni.isNoNull(obj)) {
                        var devId = obj.id.split('&')[0];
                        pro.j.rsv.getRTRsvList(devId, start, end, function (rlt) {
                            var list = rlt.data;
                            if (list)
                                $(list).attr("title", "已预约");
                            callback(list);
                            $("[title]").tooltip({
                                content: function () {
                                    var e = $(this);
                                    if (e.hasClass("cld-m-cell")) {
                                        var con = e.attr("title").replace(/\|/g, "<br/>");
                                        return con;
                                    }
                                    else {
                                        return e.attr("title");
                                    }
                                }
                            });
                        });
                    }
                    else {
                        callback();
                    }
                }
            });
        }
        function selHours(data) {
            //实验名
            $("#rt_rsv_apply .test_name").val($("#curTestName").val());
            var obj = data.obj;
            if (obj.type == "kind")
                return;
            var req = uni.getReq();
            var rtid = req["rtId"] || "<%=RtId%>";
            pro.j.rtest.getRTest(rtid, function (rlt) {
                //项目信息
                var rt = rlt.data;
                var dlg = $("#rt_rsv_apply");
                $("input[name=rt_id]", dlg).val(rt.rt_id);
                $("input[name=rt_name]", dlg).val(rt.rt_name);
                $(".rt_name", dlg).html(rt.rt_name);
                //设备信息
                var devid = obj.devId;
                var devname = obj.name;
                var date = data.dt;
                //初始化
                $("#rt_begin_date").val(date + " 08:00");
                $("#rt_end_date").val(date + " 20:00");
                var start = obj.openStart.split(":");
                var end = obj.openEnd.split(":");
                var h_start = parseInt(start[0]);
                var h_end = parseInt(end[0]);
                var m_start = parseInt(start[1]);
                var m_end = parseInt(end[1]);
                var hour = parseInt(data.h || h_start);
                var beginH = uni.num2Str(hour, 2) + ":00";
                var beginT = uni.num2Str(h_start, 2) + ":" + uni.num2Str(m_start, 2);
                var endH;
                var endT = uni.num2Str(h_end, 2) + ":" + uni.num2Str(m_end, 2);
                if (hour == h_end)
                    endH = endT;
                else
                    endH = uni.num2Str(hour + 1, 2) + ":00";
                $("#rt_begin_date").val(date + " " + beginH);
                $("#rt_end_date").val(date + " " + endH);
                //日期控件
                $(".Wdate").datetimepicker({
                    controlType: 'select',
                    timeFormat: "HH:mm",
                    dateFormat: "yy-mm-dd",
                    stepHour: 1,
                    stepMinute: 1,
                    hourMin: h_start,
                    hourMax: h_end
                });
                var remark = $("#div_remark");
                $(".open_t_start", remark).html(beginT);
                $(".open_t_end", remark).html(endT);
                var earliest = obj.earliest;
                var latest = obj.latest;
                var max = obj.max;
                var min = obj.min;
                if (earliest == undefined || earliest == 0) earliest = "不限制";
                else earliest = pro.dt.m2dms(earliest);
                $(".rule_t_earliest", remark).html(earliest);
                if (latest == undefined || latest == 0) latest = "不限制";
                else latest = pro.dt.m2dms(latest);
                $(".rule_t_latest", remark).html(latest);
                if (max == undefined || max == 0) max = "不限制";
                else max = pro.dt.m2dms(max);
                $(".rule_t_max", remark).html(max);
                if (min == undefined || min == 0) min = "不限制";
                else min = pro.dt.m2dms(min);
                $(".rule_t_min", remark).html(min);
                $("#rt_rsv_apply").dialog("open");
                $("#rt_sub_resv").removeAttr("disabled");
                $("input[name=dev_id]").val(devid);
                $('#rsv_dev_name').html(devname);
                $('.my_name').html(pro.acc.name);
            });
        }
        function subRTResv(bt) {
            var cld = $("#rt_rsv_apply");
            var test = $(".test_name", cld);
            var name = $.trim(test.val());
            var memo = $.trim($(".memo", cld).val());
            var start = new Date($("#rt_begin_date").val());
            var end = new Date($("#rt_end_date").val());
            var tm = (end.getTime() - start.getTime()) / (1000 * 60);
            if (tm > parseInt(valid_time)) {
                uni.msgBox("可用预约时长不足！(可用："+pro.dt.m2ms(valid_time)+")；<br/>若需延长可用预约时长，请申请延时。");
                return false;
            }
            test.val(name);
            if (name == "") {
                uni.msgBox("实验名称不能为空！");
                return false;
            }
            if (memo == "") {
                uni.msgBox("申请说明不能为空！");
                return false;
            }
            $("#rt_sub_resv", cld).attr({ "disabled": "disabled" });
            pro.j.rsv.fRsv("set_rtrsv", $("form", cld), function () {
                $("#rt_rsv_apply").dialog("close");
                uni.confirm("预约提交成功，是否跳转到预约管理页面？", function () {
                    location.href = 'User.aspx';
                }, function () {
                    unicld.uploadCld();
                });
                $("#rt_sub_resv").removeAttr("disabled");
            }, function (rlt) {
                uni.msgBox(rlt.msg);
                $("#rt_sub_resv").removeAttr("disabled");
            }, function () {
                uni.msgBox("异步连接出现异常！");
                $("#rt_sub_resv").removeAttr("disabled");
            });
        }
        $("#rt_rsv_apply").dialog({ width: 810, autoOpen: false, modal: true, minHeight: 402, bgiframe: true });
        $(function () {
            loadUniCalendar();
        });
    </script>
</div>
