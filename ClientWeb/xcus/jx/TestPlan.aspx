<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="net/Master.master" CodeFile="TestPlan.aspx.cs" Inherits="ClientWeb_xcus_jx_TestPlan" %>

<%@ MasterType VirtualPath="net/Master.master" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.sch.css" rel='stylesheet' />
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <style>
        ul, li { list-style-type: none; padding-left: 0; }
        #course_panel .popover { min-width: 700px; }
        #course_panel .popover-content { padding: 5px 6px; }
        .affix { position: fixed; top: 60px; overflow: visible; white-space: nowrap; }
        .affix-bottom { position: absolute; }
        .affix-top { overflow: visible; white-space: nowrap; }
        .col-sm-1 { padding: 0; }
        .nav-pills > li > a { padding: 5px 10px; }
        .scroll_navbar li a { color: #999; font-size: 12px; padding-left: 2px; padding-right: 0; }
        .scroll_navbar li.active a { color: #3276b1; font-weight: bold; }
        #course_panel h2 { border-left: 3px solid #31b0d5; }
        #course_panel .plan_h { padding-top: 52px; }
        .info { color: #777; }
        .course_title { font-weight: bold; font-family: 'Microsoft YaHei'; line-height: 30px; padding-right: 20px; }
        div.panel_resv_list td { background: #fdfdf8; }
        .panel-body.plan_md { padding-bottom: 3px; }
        .panel_test_name { line-height: 24px; font-size: 18px; font-family: 'Microsoft YaHei'; }
        .panel > .panel-body + .table { border-bottom: 1px solid #ddd; }
        .table-striped > tbody > tr:nth-child(odd) > td { background-color: #f0f0f0; }
        #top_nav .navbar-inverse .navbar-nav > li > a { color: #fff; }
        .tr_test_it .text-info a { margin-right:30px;}
.panel_resv_list {
    padding: 2px 5px 8px 5px;
}
    </style>
    <script>
        function delPlan(id) {
            uni.confirm("确定 <span class='red'>删除</span> 此<%=courseKind%>计划？删除后将不可恢复！", function () {
                pro.j.test.delTestPlan(id, function () {
                    uni.msgBox("删除成功", "", function () {
                        if ($(".plan_md").length == 1) location.reload();
                        else
                            $(".plan_md_" + id).remove();
                    });
                });
            });
        }
        function delTestitem(id,card_id) {
            uni.confirm("确定要 <span class='red'>删除</span> 此<%=courseKind%>？删除后将不可恢复！", function () {
                pro.j.test.delTestitem(id,card_id, function () {
                    uni.msgBox("删除成功", "", function () {
                        var test = $("#test_it_" + id);
                        if ($(".tr_test_it", test.parent()).length == 1) location.reload();
                        test.remove();
                    });
                });
            });
        }
        function delResv(rsv) {
            var id = $(rsv).attr("rsv_id");
            if (!id) {
                uni.msgBox("参数有误");
                return;
            }
            uni.confirm("确定要删除此预约？", function () {
                pro.j.rsv.delResv(id, function () {
                    $(rsv).parents("tr:first").remove();
                    uni.msgBoxR("删除成功");
                });
            });
        }
        function setTestitem(test) {
            var id = $(test).attr("test_id");
            if (!id) {
                uni.msgBox("参数有误");
                return;
            }
            pro.d.test.setTest("更改设置", { test_id: id});
        }
        function uploadFile(test) {
            var id = $(test).attr("test_id");
            if (!id) {
                uni.msgBox("参数有误");
                return;
            }
            pro.d.test.uploadFile("上传实验报告模版", { test_id: id });
        }
        function analysisUrl() {
            var req = uni.getReq();
            var type = req["type"];
            if (!type) type = "unite";
            $(".btn_" + type).removeClass("btn-default")
            .addClass("btn-primary");
        }
        function openCourse(type) {
            pro.d.test.crePlan("新建<%=courseKind%>计划", { type: type, term: pro.term.year, sel_group: "<%=GetConfig("clientTab")=="jl"?"true":"false"%>" }, function (dlg) {
                pro.j.group.delGroup(dlg.group_id, function () { });
            });
        }
        function openCreTest(para) {
            pro.d.test.creTest("新建实验项目", para);
        }
        function openResv(id) {

        }
        function openGroup(group) {
            if (group)
                pro.d.group.manage("设置上课班级", { width: 520, group: group, need_cls: "true",readonly:"<%=GetConfig("clientTab")=="jl"?"true":"false"%>" }, function (dlg) {
                    if (dlg.group_id) {
                        var p = $("#btn_g_" + dlg.group_id);
                        p.children(".group_name").html(dlg.group_name);
                        p.children(".group_num").html(dlg.group_num);
                    }
                });
        }
        function clock() {
            var dt = new Date();
            $(".cur_time").html(dt.format("HH:mm"));
            $(".cur_date").html(dt.format("yyyy年MM月dd日 星期E"));
        }
        function selPlanType(type) {
            var para = uni.url2Obj(location.href);
            para.type = type;
            location.href = location.pathname + "?" + uni.obj2Url(para);
        }
        $(function () {
            analysisUrl();
            clock();
            setInterval("clock()", 60000);
            var term = pro.term;
            var acc = pro.acc;
            $(".cur_week").html(parseInt(pro.dt.date2wwd(new Date()) / 10));
            var cld = $("#calendar").uniCalendar({
                mode: "m",
                modes: "m",
                style: "mini",
                width: 340,
                cellHeight: 5,
                borderWidth: 1,
                relative: true,
                schedule: true,
                secnum: 8,
                dateStart: term.start,
                dateEnd: term.end,
                evFinishDraw: function (dt, idate, opt, iqz) {
                    $("[data-content]", iqz).popover({
                        html: true,
                        placement: 'auto',
                        trigger: 'hover'
                    });
                },
                evUpPlans: function (obj, start, end, callback, opt) {
                    start = start.format("yyyyMMdd");
                    end = end.format("yyyyMMdd");
                    pro.j.rsv.getTchResv(start, end, function (rlt) {
                        var list = $(rlt.data);
                        list.each(function () {
                            var ltch = this.ltch;
                            var start = parseInt(ltch / 100),
                            end = parseInt(start / 100) * 100 + (ltch % 100);
                            this.start = start;
                            this.end = end;
                        });
                        callback(list);
                    }, { teacher_accno: acc.accno });
                }
            });
        })
        //创建实验计划成功
        function crePlanSuc(type) {
            var href = location.href.split('?')[0];
            location.href = href + "?type=" + type;
        }
        //改变试验计划状态
        function changePlanStatus(id, max_user, deadline, v) {
            var dlg = $($("#change_open_status").html());
            dlg.find(".open_status[value=" + v + "]").attr("checked", "checked");
            dlg.find(".max").val(max_user);
            dlg.find(".deadline").val(deadline).datepicker({ minDate: 0 });
            dlg.find(".sel_close").click(function () {
                dlg.dialog("close");
            });
            dlg.find(".sel_ok").click(function () {
                var m = $(".max", dlg).val();
                if (!parseInt(m)) {
                    uni.msgBox("请填入数字");
                    return;
                }
                pro.j.test.setPlanStatus(id, $(".open_status:checked").val(), function () {
                    uni.msgBoxR("修改选课设置成功");
                }, { deadline: $(".deadline", dlg).val(), max_user: m });
            });
            uni.dlg(dlg, "修改选课设置", 320);
        }
        //链接到预约页面
        function linkResv(url,btn) {
            if ("<%=GetConfig("clientTab")%>" == "gd") {
                var planId = $(btn).attr("planid");
                var plan = $("#plan_" + planId);
                if (plan.attr("testhour") && plan.attr("testhour") == plan.attr("donehour")) {
                    location.href = url;
                }
                else {
                    uni.msgBox("排课前请安排完所有实验计划学时");
                }
            }
            else {
                location.href = url;
            }
        }
    </script>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
    <div id="change_open_status">
        <div class="dialog">
            <table class="list">
                <tbody>
                    <tr>
                        <td>开放状态</td>
                        <td>
                            <label class="click">
                                <input type="radio" class="open_status" name="open_status" value="512" />开放中</label>&nbsp;&nbsp;
            <label class="click">
                <input type="radio" class="open_status" name="open_status" value="256" />未开放</label></td>
                    </tr>
                    <tr>
                        <td>截止日期</td>
                        <td>
                            <input type='text' class='deadline' readonly='readonly' /></td>
                    </tr>
                    <tr>
                        <td>班级人数</td>
                        <td>
                            <input type='text' class='max' /></td>
                    </tr>
                </tbody>
            </table>
            <div class="line"></div>
            <div class="text-center">
                <button type="button" class="btn btn-info sel_ok">确定</button>
                <button type="button" class="btn btn-default sel_close">返回</button>
            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-body">
            <div class="row">
                <div class="col col_3">
                    <h2 style="margin-top: 0px; border-bottom: dashed 1px #ddd; padding-bottom: 3px;"><span class="glyphicon glyphicon-time"></span><span class="cur_time"></span>
                        <br />
                        <small class="cur_date"></small></h2>
                    <h3 style="color: #777;">第 <code class="cur_week"></code>周</h3>
                    <div class="btn-group">
                        <button disabled="disabled" type="button" class="btn btn_info"><%=curTerm %></button>
                        <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown">
                            <span class="caret"></span><span></span>
                        </button>
                        <ul class="dropdown-menu" role="menu">
                            <%=termList %>
                        </ul>
                    </div>
                </div>
                <div class="col col_5" style="border-left: 1px solid #ccc; border-right: 1px solid #ccc; height: 170px;">
                    <ul>
                        <li style="border-bottom: 1px solid #ccc;">
                            <h4 style="margin-bottom: 30px;"><%=courseKind%>计划</h4>
                            <span class="grey">本学期<%=courseKind%>计划数：<span class="red"><%=testPlanTotal %></span>&nbsp;&nbsp;&nbsp; 实验项目数：<span class="red"><%=testTotal %></span> &nbsp;&nbsp;&nbsp; 总<%=courseKind%>学时：<span class="red"><%=period %></span> 小时</span>
                        </li>
                        <li style="margin-top: 20px;" class="text-center">
                            <div class="btn-group">
                                <button type="button" class="btn btn-primary btn-lg dropdown-toggle" data-toggle="dropdown">
                                    新建<%=courseKind%>计划 <span class="caret"></span>
                                </button>
                                <ul class="dropdown-menu" role="menu">
                                    <%if (testPlanKind == 0 || (testPlanKind & 1) > 0)
                                      { %>
                                    <li><a onclick="openCourse('unite');">教学统一安排</a></li>
                                    <%} %>
                                    <%if ((testPlanKind & 2) > 0)
                                      { %>
                                    <li><a onclick="openCourse('open');">教学开放<%=courseKind%></a></li>
                                    <%} %>
                                    <li class="divider"></li>
                                    <li><a href="Art.aspx?type=other&id=help">查看操作说明</a></li>
                                </ul>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="col col_4">
                    <div id="calendar"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-11">
            <div class="panel panel-default" id="course_panel">
                <div class="panel-heading">
                    <div class="btn-group btn-sm">
                        <%if (testPlanKind == 0 || (testPlanKind & 1) > 0)
                          { %>
                        <button type="button" class="btn btn-default btn_unite" onclick="selPlanType('unite')">教学统一安排</button>
                        <%} %>
                        <%if ((testPlanKind & 2) > 0)
                          { %>
                        <button type="button" class="btn btn-default btn_open" onclick="selPlanType('open')">教学开放<%=courseKind%></button>
                        <%} %>
                    </div>
                </div>
                <h1 style="margin-bottom: -10px; padding-left: 5px;" id="my_course">&nbsp;<span class="glyphicon glyphicon-calendar" style="color: #ccc"></span>&nbsp;我的<%=(Request["type"]=="open"?"教学开放":"教学统一安排")+courseKind%></h1>
                <%=testPlanList %>
            </div>
            <div class="panel_resv_list">
                <%=resvPanelList %>
            </div>
        </div>
        <div class="col-sm-1">
            <div data-spy="affix" data-offset-top="360" data-offset-bottom="0" class="scroll_navbar">
                <ul class="nav">
                    <li>
                        <span class="glyphicon glyphicon-th-list"></span>课程列表<a href="#my_course" class="hidden"></a>
                    </li>
                    <%=sideList %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
