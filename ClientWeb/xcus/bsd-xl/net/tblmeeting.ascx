<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tblmeeting.ascx.cs" Inherits="ClientWeb_xcus_bsd_xl_net_tblmeeting" %>
<div>

    <div class="calendar"></div>
    <div id="meeting_apply" title="研讨室申请" class="resv_panel dialog" style="display: none;">
        <form name="resvForm" onsubmit="return false;">
            <div style="position: relative; height: 40px;" class="hidden">
                <div class="ui-tooltip ui-widget ui-widget-content ui-corner-all" style="top: 2px; left: 20px; max-width: 700px; font-size: 12px;" id="oth_info"></div>
            </div>
            <div class="resv_tbl">
                <table border="1" style="width: 760px;">
                    <tbody>
                        <tr>
                            <td colspan="4" style="text-align: left;" class="colored resv_tbl_h">
                                <input type="hidden" name="dev_id" />
                                <input type="hidden" name="type" value="dev" />
                                预约研讨室：<span id="rsv_dev_name"></span> | 申请人：<span id="apply_people"></span></td>
                        </tr>
                        <tr>
                            <td class="colored th">
                                <div style="width: 76px;"><span style="color: red;">*</span>会议名称</div>
                            </td>
                            <td class="con" style="width: 289px;">
                                <input name="test_name" id="meeting_name" type="text" class="ipt" /></td>
                            <td class="colored th">
                                <div style="width: 66px;">
                                    添加成员
                                </div>
                            </td>
                            <td class="con" style="width: 260px;">
                                <div class="tag">责任成员：最少<span class="min_user red"></span>人；最多<span class="max_user red"></span>人</div>
                    <input type="hidden" name="mb_list" />
                    <input class="mb_name_ipt input_txt" type="text" act="truename" url="searchAccount.aspx" value="姓名关键字查找" onclick="this.value = ''" onblur="if(this.value=='') {this.value='姓名关键字查找';}" />
                                                    <ul id="meeting_memList" class="ul_member memList ul_items">
                    </ul>
                            </td>
                        </tr>
                        <tr>
                            <td class="colored th"><span style="color: red;">*</span>开始日期</td>
                            <td class="con">
                                <input type="text" id="mt_begin_date" name="start" class="Wdate" readonly="readonly" /></td>

                            <td class="colored th">
                                <div style="width: 66px;"><span style="color: red;">*</span>结束日期</div>
                            </td>
                            <td class="con">
                                <input type="text" id="mt_end_date" name="end" class="Wdate" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td class="colored th"><span style="color: red;">*</span>申请说明</td>
                            <td colspan="2">
                                <textarea rows="4" cols="30" name="memo" style="width: 370px;" class="ipt memo" maxlength="60"></textarea></td>
                            <td style="font-size:12px;">
                                                        <div style="text-decoration: underline; margin-bottom: 3px;"><a href="心理学院研讨室使用申请书.docx">下载研讨室申请报告模版</a><span class="red">*</span></div>
                        <div>
                            <input type="file" name="meeting_file_name" id="meeting_file_name" />
                        </div>
                        <div style="height: 24px; line-height: 24px;">
                            <input type="button" style="cursor: pointer;" class="upload_file" file="meeting_file_name" value="上传" /><span class="cur_file_name color1"></span>
                        </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div_remark" id="div_remark">
                本研讨室当日开放时间：<span class="red open_t_start"></span> 到 <span class="red open_t_end"></span>；申请预约最长可提前：<span class="red rule_t_earliest"></span>
                至少提前：<span class="red rule_t_latest"></span>；预约时间最长：<span class="red rule_t_max"></span> 最短：<span class="red rule_t_min"></span>。
            </div>
            <div class="div_submit" style="text-align: center;">
                <input type="button" class="sub_form bt_sub_resv" onclick="subDevResv(); return false;" value="提交" />
                <input type="button" class="sub_form" onclick="$('#meeting_apply').dialog('close'); uni.reload(); return false;" value="返回" />
            </div>
        </form>
    </div>
    <style>
        .calendar { width: 700px; }
        #div_remark { margin: 3px 10px; padding: 5px; border: 1px dashed #ccc; background-color: #fbfbf3; color: #666; line-height: 16px; }
    </style>
    <script type="text/javascript">
        function subDevResv() {
            var cld = $("#meeting_apply");
            var mt = $("#meeting_name", cld);
            var name = $.trim(mt.val());
            var memo = $.trim($(".memo",cld).val());
            mt.val(name);
            if (name == "") {
                uni.msgBox("会议名称不能为空！");
                return false;
            }
            if (memo == "") {
                uni.msgBox("申请说明不能为空！");
                return false;
            }
            var file_name = $("#meeting_apply  .upload_file").attr("save_name");
            if (uni.isNull(file_name)) {
                uni.msgBox("请上传申请报告");
                return;
            }
            var mbs = "";
            $("#meeting_memList input[name=memid]").each(function () {
                mbs += $(this).val() + ",";
            });
            mbs += pro.acc.id;
            $("input[name=mb_list]", cld).val(mbs);
            $("#mt_sub_resv", cld).attr({ "disabled": "disabled" });
            pro.j.rsv.fRsv("set_resv", $("form", cld), function () {
                uni.confirm("预约提交成功，是否跳转到预约管理页面？", function () {
                    location.href = 'User.aspx?tab=1';
                }, function () {
                    uni.reload();
                });
                $("#mt_sub_resv").removeAttr("disabled");
            }, function (rlt) {
                uni.msgBox(rlt.msg);
                $("#mt_sub_resv").removeAttr("disabled");
            }, function () {
                uni.msgBox("异步连接出现异常！");
                $("#mt_sub_resv").removeAttr("disabled");
            });
        }
        function loadCld() {
            var req = uni.getReq();
            var purl = {};
            purl.classkind = req["classKind"] || "<%=ClassKind%>";
            purl.islong = req["isLongResv"] || "<%=IsLong%>";
            purl.classid = req["classId"] || "<%=DevClassId%>";
            purl.devid = req["dev"] || "<%=DevId%>";
            var selH = req["click"] ? undefined : selHours;
            var width = parseInt(req["w"] || "700");
            $(".calendar").uniCalendar({
                modes: "<%=Mode%>",
                width: width,
                objTitleMinWidth: 80,
                evSelTime: selH,
                evUpTime: function (date, callback) {
                    var pra = {};
                    pra.dev_id = purl.devid;
                    pra.class_id = purl.classid;
                    pra.date = date.format("yyyyMMdd");
                    pra.islong = purl.islong;
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
                        pro.j.rsv.getResvList(devId, start, end, function (rlt) {
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
            var md = data.md;
            var obj = data.obj;
            if (obj.type == "kind")
                return;
            var devid = obj.devId;
            var devname = obj.name;
            var date;
            if (md == "d")
                date = data.dt;
            else if (md == "m")
                date = data.dt;
            //成员限制
            $("#meeting_apply .min_user").html(obj.minUser);
            $("#meeting_apply .max_user").html(obj.maxUser);
            //上传文件
            var upFile = $("#meeting_apply .upload_file").uploadFile();
            //初始化时间
            $("#mt_begin_date").val(date + " 08:00");
            $("#mt_end_date").val(date + " 20:00");
            var start = obj.openStart.split(":");
            var end = obj.openEnd.split(":");
            var h_start = parseInt(start[0]);
            var h_end = parseInt(end[0]);
            var m_start = parseInt(start[1]);
            var m_end = parseInt(end[1]);
            var hour;
            if (md == "d")
                hour = parseInt(data.h);
            else if (md == "m")
                hour = h_start;
            var beginH = uni.num2Str(hour, 2) + ":00";
            var beginT = uni.num2Str(h_start, 2) + ":" + uni.num2Str(m_start, 2);
            var endH;
            var endT = uni.num2Str(h_end, 2) + ":" + uni.num2Str(m_end, 2);
            if (hour == h_end)
                endH = endT;
            else
                endH = uni.num2Str(hour + 1, 2) + ":00";
            $("#mt_begin_date").val(date + " " + beginH);
            $("#mt_end_date").val(date + " " + endH);
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
            $("#meeting_apply").dialog("open");
            $("#mt_sub_resv").removeAttr("disabled");
            $("input[name=dev_id]").val(devid);
            $('#rsv_dev_name').html(devname);
        }
        $("#meeting_apply").dialog({ width: 860, autoOpen: false, modal: true, minHeight: 402, bgiframe: true });
        loadCld();
        $(function () {
            var acc = pro.acc;
            $("#apply_people").html(acc.name);
            //添加成员
            $("#meeting_apply .mb_name_ipt").procomplete(function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        var list = $("#meeting_memList").html();
                        list += "<li><input type='hidden' name='memid' value='" + ui.item.szLogonName + "' /><span>" + ui.item.label + "</span><a class='click del' onclick='$(this).parent().remove();return false;'> ×</a></li>";
                        $("#meeting_memList").html(list);
                    }
                }
            });
        });
    </script>
</div>
