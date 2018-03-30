<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="DevDetail.aspx.cs" Inherits="DevDetail" %>

<%@ Register TagPrefix="Uni" TagName="leftMenu" Src="net/LeftMenu.ascx" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <link href="theme/TabStyleH.css" rel='stylesheet' />
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/media/jquery.media.js" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/fullcalendar/fullcalendar.week.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/fullcalendar/fullcalendar.week.css" rel='stylesheet' />
    <script type="text/javascript">
        //当前设备
        var curDev = {};
        function rtSelCheck() {
            var rtsel = $("#rtSel");
            $("#rtId").val(rtsel.val());
            if (!uni.isNull(rtsel.val()) && rtsel.val() != "0") {
                pro.j.rsv.getRTRsvFee(curDev.id, rtsel.val(), function (fee) {
                    rlt = fee.data;
                    $("#resv_fee_name").html(uni.cutStrT(rlt.feeName, 15))
                    //样品
                    if (uni.isNoNull(rlt.splOpt)) {
                        var splcon = "<option value='0'>未选择</option>" + rlt.splOpt;
                        $("#spl_clone .splSel").html(splcon);
                        var item = $("#spl_clone").find(".spl_item");
                        $("#spl_list").html(item.clone());
                        $("#rt_rsv_add_spl").show();
                    }
                    else {
                        $("#spl_list").html("本仪器无样品费用。");
                    }
                    //单价
                    use_fee = (rlt.useFee / 100.00);
                    use_fee_u = rlt.useFeeUnit;
                    sub_fee = (rlt.subFee / 100.00);
                    sub_fee_u = rlt.subFeeUnit;
                    var usub = pro.getFee2(sub_fee, sub_fee_u, fee_k);
                    if (usub + "" == "NaN") usub = 0;
                    $("#unit_sub ").html(usub + "元/时");
                    var uuse = pro.getFee2(use_fee, use_fee_u, fee_k);
                    if (uuse + "" == "NaN") uuse = 0;
                    $("#unit_use").html(uuse + "元/时");
                    //if (use_fee > 0) {
                    //    $("#sampleFee").parents("tr:first").hide();
                    //}
                    //else {
                    //    $("#unit_use").parents("tr:first").hide();
                    //}
                    $("#rt_rsv_fee_tbl").show();
                    calcSample();
                });
            }
            else {
                $("#rt_rsv_fee_tbl").hide();
                $("#rt_rsv_add_spl").hide();
                $("#spl_list").html("请选择实验项目！");
            }
        }
        //选择样品
        function calcSample(sel) {
            var list = $("#spl_list .spl_item");
            var total = 0;
            list.each(function () {
                var u = $(".splSel", this).find("option:selected");
                var n = parseInt($(".splNum", this).val());
                if (sel) {
                    var it = $(sel).parent();
                    if (sel.val() != "0" && u.val() == sel.val() && this != it[0]) {
                        uni.msgBox("所选样品已选过！");
                        sel.val("0");
                        return;
                    }
                }
                if (u.val() != "0") {
                    var fee = u.attr("fee");
                    total += (parseInt(fee) * n) / 100;
                }
            });
            sample_fee = total;
            calcFee();
        }
        //添加样品
        function addSpl() {
            var items = $("#spl_list").find(".spl_item");
            var sel = $("select", items.last());

            if (items.length < ($("option", sel).length - 1)) {
                $("#spl_list").append($("#spl_clone").html());
            }
            else {
                uni.msgBox("已达到最大样品种类！");
            }
        }
        //选管理员
        function manSelCheck() {
            var mansel = $("#manSel");
            $("#labManId").val(mansel.val());
            var op = $("#manSel option:selected");
            $("#labMan").val(op.html());
        }
        function m2dms(m) {
            var t = parseInt(m);
            var resv_t = parseInt(t / 1440) + "天";
            resv_t += parseInt((t % 1440) / 60) + "时";
            resv_t += parseInt((t % 1440) % 60) + "分";
            return resv_t;
        }
        var use_fee = 0;
        var use_fee_u = 0;
        var sample_fee = 0;
        var sample_fee_u = 0;
        var sub_fee = 0;
        var sub_fee_u = 0;
        var fee_k = 60;//单价常量
        function calcFee() {
            var begin = $("#beginDate").val();
            var beginTime = NewDate(begin).getTime();
            var end = $("#endDate").val();
            var endTime = NewDate(end).getTime();
            var t = Math.ceil((endTime - beginTime) / (1000 * 60));//时间 分
            var resv_t = m2dms(t);
            $("#total_resv_time").html(resv_t);
            var use = pro.getFee2(use_fee, use_fee_u, t, fee_k);
            if (use + "" == "NaN") {
                use = 0;
            }
            var sample = sample_fee;
            if (sample + "" == "NaN") {
                sample = 0;
            }
            var sub = parseInt($("#check").val()) * pro.getFee2(sub_fee, sub_fee_u, t, fee_k);
            if (sub + "" == "NaN") {
                sub = 0;
            }
            var total = (use + sample + sub).toFixed(2);
            $("#useFee").html(use + "元");
            $("#sampleFee").html(sample.toFixed(2) + "元");
            $("#subFee").html(sub + "元");
            $("#totol").html(total + "元");
        }
        function submitForm() {
            if ($("#rtSel").val() == "0") {
                uni.msgBox("请选择实验项目！");
                return false;
            }
            var str = $.trim($("#labName").val());
            $("#labName").val(str);
            if (str == "") {
                uni.msgBox("实验名称不能为空！");
                return false;
            }
            var total = $("#totol").html();
            if (total == "0.00元") {
                uni.msgBox("请选择检测内容！");
                return false;
            }
            //获取样品
            var s = $("#spl_list").find(".splNum");
            var list = $("#spl_list").find(".spl_item");
            var spls = "";
            var ckN = false;
            list.each(function () {
                var pthis = $(this);
                var sel = pthis.children(".splSel");
                var num = parseInt(pthis.children(".splNum").val());
                if (isNaN(num)) {
                    ckN = true;
                    return false;
                }
                if (sel.val() != "0" && num != 0) {
                    var num = parseInt(pthis.children(".splNum").val());
                    var fee = sel.find("option:selected").attr("fee");
                    spls += sel.val() + "," + fee + "," + num + ",";
                }
            });
            if (ckN) {
                uni.msgBox("样品数量填写有误，请核对！");
                return false;
            }
            if (spls.length > 0) spls = spls.substring(0, spls.length - 1);

            $("#spls").val(spls);
            //
            $("#bt_sub_resv").attr({ "disabled": "disabled" });
            pro.j.rsv.fRsv("sub_dev_rtrsv_fm", $("#resvForm"), function () {
                uni.confirm("预约提交成功，是否跳转到预约管理页面？", function () {
                    location.href = 'UserCenter.aspx?tab=0';
                }, function () {
                    uni.tab.reload();
                });
                $("#bt_sub_resv").removeAttr("disabled");
            }, function (rlt) {
                uni.msgBox(rlt.msg);
                $("#bt_sub_resv").removeAttr("disabled");
            }, function () {
                uni.msgBox("异步连接出现异常！");
                $("#bt_sub_resv").removeAttr("disabled");
            });
        }
        //预约表
        function loadCalendar() {
            var dev = curDev;
            var early = "未设置";
            var last = "未设置";
            var max = "未设置";
            var min = "未设置";
            if (uni.isNoNull(dev.early)) dev.early == "0" ? early = "不限制" : early = m2dms(dev.early);
            if (uni.isNoNull(dev.last)) dev.last == "0" ? last = "不限制" : last = m2dms(dev.last);
            if (uni.isNoNull(dev.max)) dev.max == "0" ? max = "不限制" : max = m2dms(dev.max);
            if (uni.isNoNull(dev.min)) dev.min == "0" ? min = "不限制" : min = m2dms(dev.min);
            var str = "<div id='fm_append'><ul class='fm-append ui-tooltip ui-widget ui-widget-content ui-corner-all'>" +
                "<li><img alt='123' src='" + dev.url + "'/></li>" +
                "<li class='dev_title'>" + uni.cutStrT(dev.name, 12) + "</li><li><span class='h'>部门：</span>" + dev.dept + "</li>" +
                "<li><span class='h'>位置：</span>" + dev.cps + "|" + dev.lab + "</li><li><span class='h'>实时状态：</span>" + dev.sta + "</li><li><span class='h'>属性：</span>" +
                (dev.pro == "l" ? "<span class='green'>支持跨天预约</span>" : "<span class='red'>不支持跨天预约</span>") +
                "</li><li><span class='h' style='color:orange;'>预约规则</span></li><li><span class='h'>最早提前：</span><span class='t'>" + early + "</span></li><li><span class='h'>至少提前：</span><span class='t'>" + last +
                "</span></li><li><span class='h'>最长预约：</span><span class='t'>" + max + "</span></li><li><span class='h'>最短预约：</span><span class='t'>" + min +
                "</span></li></ul></div>";
            $("#left_img").html(str)
            $('#calendar').fullCalendar({
                height: 480,
                evClickDay: function (fmt, title) {
                    if (pro.isloginRT()) {
                        ShowApply(fmt, title);
                    }
                },
                events: function (start, end, callback) {
                    start =start.format("yyyyMMdd");
                    end = end.format("yyyyMMdd");
                    pro.j.rsv.getRTRsvList(curDev.id, start, end, function (rlt) {
                        var resvs = rlt.data;
                        var events = [];
                        $(resvs).each(function () {
                            var pthis = $(this);
                            events.push({
                                id: pthis.attr('id'),
                                title: pthis.attr('title'),
                                start: pthis.attr('start'),
                                end: pthis.attr('end'),
                                color: pthis.attr('state'),
                                pro: pthis.attr('prop'),
                                allDay: pthis.attr('allDay')
                            });
                        });
                        callback(events);
                        $("[title]").tooltip({
                            content: function () {
                                var e = $(this);
                                if (e.hasClass("fc-widget-content")) {
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
            });
        }
        function ShowApply(date, info) {
            //初始化
            $("#beginDate").val(date + " 08:00");
            $("#endDate").val(date + " 19:00");
            if (info && info.length > 10) {
                info = "<span style='color:orange;font-weight:bold;'>预约情况：</span>" + info;
            }
            else {
                info = "<span style='color:orange;font-weight:bold;'>当日无预约</span> " + info;
            }
            $('#oth_info').html(info);
            pro.j.rsv.getRTRsvFm(curDev.id, date, function (con) {
                var rlt = con.data;
                if (!rlt.devStatus) { uni.msgBox("本台仪器缺少预约规则，不能预约！"); return false; }
                var open = rlt.devStatus.open;
                var rule = rlt.devStatus.rule;
                var start = parseInt(open.start);
                var end = parseInt(open.end);
                var h_start = parseInt(start / 100);
                var h_end = parseInt(end / 100);
                var m_start = start % 100;
                var m_end = end % 100;
                var beginT = uni.num2Str(h_start, 2) + ":" + uni.num2Str(m_start, 2);
                var endT = uni.num2Str(h_end, 2) + ":" + uni.num2Str(m_end, 2);
                $("#beginDate").val(date + " " + beginT);
                $("#endDate").val(date + " " + endT);
                //日期控件
                $(".Wdate").datetimepicker({
                    timeFormat: "HH:mm",
                    dateFormat: "yy-mm-dd",
                    stepHour: 1,
                    stepMinute: 10,
                    hourMin: h_start,
                    hourMax: h_end
                });
                var remark = $("#div_remark");
                $(".open_t_start", remark).html(beginT);
                $(".open_t_end", remark).html(endT);
                if (rule.earliest == undefined || rule.earliest == 0) rule.earliest = "不限制";
                else rule.earliest = m2dms(rule.earliest);
                $(".rule_t_earliest", remark).html(rule.earliest);
                if (rule.latest == undefined || rule.latest == 0) rule.latest = "不限制";
                else rule.latest = m2dms(rule.latest);
                $(".rule_t_latest", remark).html(rule.latest);
                if (rule.max == undefined || rule.max == 0) rule.max = "不限制";
                else rule.max = m2dms(rule.max);
                $(".rule_t_max", remark).html(rule.max);
                if (rule.min == undefined || rule.min == 0) rule.min = "不限制";
                else rule.min = m2dms(rule.min);
                $(".rule_t_min", remark).html(rule.min);
                $("#rtSel ").html(rlt.rtOpt);
                $("#manSel").html(rlt.manOpt);
                $("#myTutor").html(rlt.myTutor);
                $("#resvApply").dialog("open");
                $("#bt_sub_resv").removeAttr("disabled");
                rtSelCheck();
                manSelCheck();
                $("input[name=dev_id]").val(curDev.id);
                $('#rsv_dev_name').html(curDev.name);
            });
        }
        ////
        $(document).ready(function () {
            //初始化当前设备
            curDev.id = $("#cur_dev_id").val();
            curDev.name = $("#cur_dev_name").val();;
            curDev.cps = $("#cur_dev_cps").val();
            curDev.dept = $("#cur_dev_dept").val();
            curDev.lab = $("#cur_dev_lab").val();
            curDev.url = $("#cur_dev_url").val();
            curDev.pro = $("#cur_dev_pro").val();
            curDev.sta = $("#detail .dev_status").eq(0).html();
            curDev.early = $("#cur_dev_early").val();
            curDev.last = $("#cur_dev_last").val();
            curDev.max = $("#cur_dev_max").val();
            curDev.min = $("#cur_dev_min").val();

            $("#resvApply").dialog({ width: 810, autoOpen: false, modal: true, minHeight: 602, bgiframe: true });
            var req = uni.getReq();
            var str = req["tab"];
            if (str == "1") {
                $("#tabResv").parent().trigger("click");
                loadCalendar();
            }
            else {
                $("#tabResv").one("click", function () {
                    $("#tabResv").parent().trigger("click");
                    loadCalendar();
                });
            }
            $("#applyName").html($("#cur_name").val());

            //视频
            $('.media').media({
                autoplay: false,
                width: 480,
                height: 360
            });
        });
    </script>
    <style type="text/css">
        #calendar { width: 720px; text-align: center; font-size: 14px; color: #666; font-family: Arial,Helvetica,Verdana,sans-serif; }
        #calendar .fc-content { text-align: left; margin-bottom: 20px; }
        .fm-append { overflow: hidden; width: 160px; position: absolute; font-family: 微软雅黑; top: 0; left: 10px; z-index: 99 !important; }
        .fm-append li { line-height: 22px; height: auto; border-bottom: 1px dashed #999; color: #274A5C; }
        .fm-append li.dev_title { font-weight: bold; color: #333; text-align: center; }
        .fm-append li span.h { font-weight: bold; }
        .fm-append img { height: 90px; width: 140px; margin: 10px; }

        #fee_standard td { text-align: center; font-family: 微软雅黑; padding: 0 12px; }
        #fee_standard td.h { border-right: 1px solid #aaa; color: #274A5C; font-weight: bold; padding: 2px 0; }
        #fee_standard tr.bt td { border-bottom: 1px solid #aaa; }
        #fee_standard td.t { color: #274A5C; font-weight: bold; }
        #fee_standard .s-f { color: #777; }
        #fee_standard .s-f span { color: #000; }

        #div_remark { margin: 3px 10px; padding: 5px; border: 1px dashed #ccc; background-color: #fbfbf3; color: #666; line-height: 16px; }
    </style>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <div class="hidden">
            <input type='hidden' id='cur_dev_id' value='<%=CurDevId %>' />
            <input type='hidden' id='cur_dev_name' value='<%=CurDevName %>' />
            <input type='hidden' id='cur_dev_cps' value='<%=CurDevCps %>' />
            <input type='hidden' id='cur_dev_dept' value='<%=CurDevDept %>' />
            <input type='hidden' id='cur_dev_lab' value='<%=CurDevLab %>' />
            <input type='hidden' id='cur_dev_url' value='<%=imgUrl %>' />
            <input type='hidden' id='cur_dev_pro' value='<%=CurDevPro %>' />
            <input type='hidden' id='cur_dev_status' value='<%=CurDevSta %>' />
            <input type='hidden' id='cur_dev_early' value='<%=CurDevEarly %>' />
            <input type='hidden' id='cur_dev_last' value='<%=CurDevLast %>' />
            <input type='hidden' id='cur_dev_max' value='<%=CurDevMax %>' />
            <input type='hidden' id='cur_dev_min' value='<%=CurDevMin %>' />
        </div>
        <Uni:leftMenu ID="leftMenu" runat="server" />
        <div id="content" class=" float_r" style="width: 720px;">
            <div id="position">当前位置：<a href="Default.aspx">首页</a> > <a href="DevList.aspx">仪器设备</a> > <%=pagePosition %></div>
            <div class="tabs">
                <ul class="tab_head">
                    <li><a>仪器详情</a></li>
                    <li style="margin: 0 2px;"><a id="tabResv">仪器预约</a></li>
                    <li><a id="tabMedia">视频展示</a></li>
                </ul>
                <div class="tab_con">
                    <div id="detail" class="item">
                        <div class="detail_up grey_box">
                            <div class="h_grey">仪器属性</div>
                            <div class="detail_con c_grey">
                                <table class="detail_list">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" rowspan="8"><a href="" class="detail_img">
                                                <img alt="<%=CurDevName %>" src="<%=imgUrl %>" border="0" style="width: 320px; height: 200px;" /></a></td>
                                            <td class="dt">仪器名称</td>
                                            <td class="dd"><span runat="server" id="devName"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">型号规格</td>
                                            <td class="dd"><span runat="server" id="devModel"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">生产厂商</td>
                                            <td class="dd"><span runat="server" id="devProFactory"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">产地</td>
                                            <td class="dd"><span runat="server" id="devProPlace"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">所在校区</td>
                                            <td class="dd"><span runat="server" id="devCam"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">所属部门</td>
                                            <td class="dd"><span runat="server" id="devCol"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">放置位置</td>
                                            <td class="dd"><span runat="server" id="devLoc"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">管理员</td>
                                            <td class="dd"><span runat="server" id="devMan"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">仪器编号</td>
                                            <td class="dd"><span runat="server" id="devNum"></span></td>
                                            <td class="dt">联系方式</td>
                                            <td class="dd"><span runat="server" id="devCon"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">购置日期</td>
                                            <td class="dd"><span runat="server" id="devDate">2004年09月21日</span></td>
                                            <td class="dt">仪器状态</td>
                                            <td class="dd"><span runat="server" id="devSta" class="dev_status"></span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="grey_box" style="border-bottom: none;">
                            <div class="h_grey">主要应用</div>
                            <div class="c_grey">
                                <p runat="server" id="devFun"></p>
                            </div>
                        </div>
                        <div class="detail_para grey_box">
                            <div class="h_grey">收费标准</div>
                            <div class="c_grey">
                                <table id="fee_standard">
                                    <tbody>
                                        <tr>
                                            <td></td>
                                            <td class="t">一类费率</td>
                                            <td class="t">二类费率</td>
                                        </tr>
                                        <tr>
                                            <td class="h">使用费：</td>
                                            <%=useFee %>
                                        </tr>
                                        <tr class="bt">
                                            <td class="h">代检费：</td>
                                            <%=subFee %>
                                        </tr>
                                        <tr>
                                            <td class="h">样品费：</td>
                                            <td colspan="3">
                                                <table>
                                                    <thead>
                                                        <tr>
                                                            <td class="t">样品</td>
                                                            <td class="t">一类费率</td>
                                                            <td class="t">二类费率</td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <%=sampleFee %>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="detail_para grey_box">
                            <div class="h_grey">性能指标</div>
                            <div class="c_grey">
                                <p runat="server" id="devPara"></p>
                            </div>
                        </div>
                        <div class="grey_box" style="border-top: none;">
                            <div class="h_grey">样品要求</div>
                            <div class="c_grey">
                                <p runat="server" id="devSpecimen"></p>
                            </div>
                        </div>
                    </div>
                    <div id="resv" class="item">
                        <div id='calendar'></div>
                        <div id="resvApply" title="预约申请" class="resv_panel dialog" style="display: none;">
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
                                                    预约仪器：<span id="rsv_dev_name"></span> | 申请人：<span id="applyName"></span><span id="myTutor"></span></td>
                                            </tr>
                                            <tr>
                                                <td class="colored th">
                                                    <div style="width: 76px;"><span style="color: red;">*</span>实验名称</div>
                                                </td>
                                                <td class="con" style="width: 289px;">
                                                    <input name="labName" id="labName" type="text" class="ipt" /></td>
                                                <td class="colored th">
                                                    <div style="width: 66px;">
                                                        <span style="color: red;">*</span>
                                                        实验项目
                                                    </div>
                                                </td>
                                                <td class="con" style="width: 260px;">
                                                    <input type="hidden" id="rtId" name="rtId" />
                                                    <select name="rtSel" id="rtSel" onchange="rtSelCheck()">
                                                    </select></td>
                                            </tr>
                                            <tr>
                                                <td class="colored th"><span style="color: red;">*</span>开始日期</td>
                                                <td class="con">
                                                    <input type="text" id="beginDate" name="beginDate" class="Wdate" readonly="readonly" onchange="calcFee();" /></td>

                                                <td class="colored th">
                                                    <div style="width: 66px;"><span style="color: red;">*</span>结束日期</div>
                                                </td>
                                                <td class="con">
                                                    <input type="text" id="endDate" name="endDate" class="Wdate" readonly="readonly" onchange="calcFee();" /></td>
                                            </tr>
                                            <tr>
                                                <td class="colored th">委托检测</td>
                                                <td class="con">
                                                    <select name="check" id="check" onchange="calcFee();">
                                                        <option value="0">否</option>
                                                        <option value="1">是</option>
                                                    </select></td>
                                                <td class="colored th">
                                                    <div style="width: 66px;">自带耗材</div>
                                                </td>
                                                <td class="con">
                                                    <select name="selMat" id="selMat">
                                                        <option value="0">否</option>
                                                        <option value="1">是</option>
                                                    </select>
                                                    <input type="hidden" id="labManId" name="labManId" />
                                                    <input type="hidden" id="labMan" name="labMan" />
                                                    <select name="manSel" id="manSel" class="hidden" onchange="manSelCheck()">
                                                    </select></td>
                                            </tr>
                                            <tr>
                                                <td class="colored th">检测内容</td>
                                                <td style="text-align: left;" class="con">
                                                    <input name="spls" id="spls" type="hidden" />
                                                    <div class="hidden" id="spl_clone">
                                                        <div class="spl_item">
                                                            <select class="splSel" onchange="calcSample($(this))">
                                                            </select>
                                                            <span class="f-fl">X&nbsp;</span>
                                                            <input class="splNum" type="text" onchange="calcSample()" value="1" />
                                                        </div>
                                                    </div>
                                                    <div class="addSpl hidden" id="rt_rsv_add_spl"><a class="click" onclick="addSpl()">[+增加]</a></div>
                                                    <div id="spl_list">
                                                        请选择实验项目！
                                                    </div>
                                                </td>
                                                <td class="colored th" style="width: 66px;">
                                                    <div>预约信息</div>
                                                </td>
                                                <td class="con">
                                                    <table id="rt_rsv_fee_tbl" class="hidden">
                                                        <tbody>
                                                            <tr>
                                                                <td><span class="h">时长：</span></td>
                                                                <td><span id="total_resv_time"></span></td>
                                                            </tr>
                                                            <tr>
                                                                <td><span class="h">费用类别：</span></td>
                                                                <td><span id="resv_fee_name"></span></td>
                                                            </tr>
                                                            <tr>
                                                                <td><span class="h">使用费：</span></td>
                                                                <td><span id="useFee">0</span> (<span id="unit_use" style="color: #777"></span>)</td>
                                                            </tr>
                                                            <tr>
                                                                <td><span class="h">代检费：</span></td>
                                                                <td><span id="subFee">0</span> (<span id="unit_sub" style="color: #777"></span>)</td>
                                                            </tr>
                                                            <tr>
                                                                <td><span class="h">样品费：</span></td>
                                                                <td><span id="sampleFee">0</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td><span class="h" style="font-weight: bold;">总计：</span></td>
                                                                <td><span id="totol">0</span></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="colored th">申请说明</td>
                                                <td colspan="3">
                                                    <textarea rows="4" cols="30" id="memo" name="memo" style="width: 594px;" class="ipt"></textarea></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="div_remark" id="div_remark">
                                    本仪器当日开放时间：<span class="red open_t_start"></span> 到 <span class="red open_t_end"></span>；申请预约最长可提前：<span class="red rule_t_earliest"></span>
                                    至少提前：<span class="red rule_t_latest"></span>；预约时间最长：<span class="red rule_t_max"></span> 最短：<span class="red rule_t_min"></span>。
                                </div>
                                <div class="div_submit" style="text-align: center;">
                                    <input type="button" class="sub_form" id="bt_sub_resv" onclick="submitForm('form'); return false;" value="提交" />
                                    <input type="button" class="sub_form" onclick="$('#resvApply').dialog('close'); uni.tab.reload(); return false;" value="返回" />
                                </div>
                            </form>
                        </div>
                        <div style="text-align: center; color: #666; font-size: 12px;">
                            <p>注意：预约仪器需要管理员审核通过方可生效。预约时间可能会被管理员调整，实际预约时间需审核时与管理员协商。</p>
                        </div>
                    </div>
                    <div id="video" class="item">
                        <div style="padding-left: 120px;">
                            <a class="media" href="<%=vadioFile%>"></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="cleaner"></div>
    </div>
    <!-- END of templatemo_main -->
</asp:Content>
