<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="Teach.aspx.cs" Inherits="ClientWeb_xcus_bsd_xl_Teach" %>
<%@ Register TagPrefix="Uni" TagName="basic" Src="~/ClientWeb/pro/net/dlg_basic.ascx" %>
<%@ Register TagPrefix="Uni" TagName="curdev" Src="net/curdev.ascx" %>
<%@ Register TagPrefix="Uni" TagName="tblresearch" Src="net/tblresearch.ascx" %>
<%@ Register TagPrefix="Uni" TagName="dlgrtest" Src="net/dlg_rtest.ascx" %>
<%@ Register TagPrefix="Uni" TagName="calendar" Src="~/ClientWeb/pro/net/calendar.ascx" %>
<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.green.css" rel='stylesheet' />
    <style>
        .tbl_list table td {height:18px;line-height:18px;padding:0 2px;border:1px solid #ccc;text-align:left;}
        .tbl_list table th {height:18px;line-height:18px;padding:0;border:1px solid #ccc;}
    </style>
    <script type="text/javascript">
        $(function () {
            var acd = $(".accordion").accordion({ heightStyle: "content" });
            var menu = $(".menu").menu();
            var req = uni.getReq();
            var testId = req["testId"];
            if (testId) {
                $("li a", menu).each(function () {
                    var pthis = $(this);
                    var id = pthis.attr("test");
                    if (testId && testId == id) {
                        pthis.addClass("ui-state-default");
                        return;
                    }
                });
            }
            else
                $("#first_item").addClass("ui-state-default");

            //加载状态表
            $("#lab_filter").change(function () {
                var v = $(this).val();
                if (v && v != "0") {
                    var cld = pro.calendar.instants["resv_cld"];
                    if (cld) {
                        cld.reload({lab_id:v});
                    }
                }
            });
        });
        function openGroup(id) {
            pro.j.group.getMbs(id, function (rlt) {
                var list = rlt.data;
                var dlg = $("<div class='dialog tbl_list'></div>");
                var tbl = "<table style='width:100%;'><thead><tr><th>姓名</th><th>学号</th><th>部门</th></tr></thead><tbody>";
                for (var i = 0; i < list.length; i++) {
                    var mb=list[i];
                    var tr = "<tr><td>" + mb.szTrueName + "</td><td>" + mb.szPID + "</td><td>" + mb.szDeptName + "</td></tr>";
                    tbl += tr;
                }
                tbl += "</tbody></table>";
                dlg.append(tbl);
                uni.dlgM(dlg, "班级成员",230,400);
            });
        }
        //预约删除
        function delResv(id) {
            uni.confirm("确定要删除吗？", function () {
                pro.j.rsv.delResv(id, function () {
                    uni.msgBoxR("删除成功！");
                });
            });
        }
    </script>
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <Uni:basic runat="server" />
    </div>
    <div>
        <div class="f-fl qzone ui-tabs ui-widget ui-corner-all" id="act_list">
            <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
                <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a class="ui-tabs-anchor">实验计划列表</a></li>
            </ul>
            <ul class="menu">
                <li><a href="?" id="first_item"><span class='ui-icon ui-icon-calculator'></span>管理实验计划</a></li>
            </ul>
            <div>
                <%=testList %>
            </div>
            <script type="text/javascript">

            </script>
        </div>
        <div class="f-fr qzone ui-tabs ui-widget ui-corner-all" id="act_qzone">
            <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
                <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a class="ui-tabs-anchor">实验信息</a></li>
            </ul>
            <div style="display:<%=string.IsNullOrEmpty(testId)?"none":""%>">
                <div class="tabs ">
                            <ul class="tab_head">
            <li class="resv_detail"><a>预约信息</a></li>
            <li><a class="resv_stat">预约实验室</a></li>
        </ul>
                    <div class="tab_con">
                <div class="box_tbl zebra">
                    <table>
                        <thead>
                            <tr class="title tbl_head">
                                <th style="">主题</th>
                                <th style="">预约时间</th>
                                <th>地点</th>
                                <th style="">成员</th>
                                <th style="">学时</th>
                                <th style="">预约状态</th>
                                <th style="">操作</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%=testDetail %>
                        </tbody>
                    </table>
                </div>
                        <div>
                            <%if(!string.IsNullOrEmpty(testId)){ %>
                            <script>
                                var uni_calendar_dft_opt = {
                                    test_id: "<%=testId%>",
                                    auto_load:"false",
                                    operate: "click",
                                    triggerHeight:60,
                                    term: "<%=curTerm.dwYearTerm%>",
                                    group_id: "<%=curTest.dwGroupID%>",
                                    submitSuc: function (dlg) {
                                        uni.msgBox("申请提交成功", "", function () {
                                            $(".unitab .resv_detail").trigger("click");
                                            uni.reload();
                                        });
                                    }
                                }
                                //处理
                            </script>
                            <div>
                                <strong>请选择实验室类型：</strong>
                                    <select id="lab_filter" style="height:30px;line-height:30px;width:200px;">
                                        <option value="0">未选择</option>
                                                            <%=LabList %>
                                    </select>
                            </div>
                            <Uni:calendar runat="server" ID="cld" Width="680" Name="resv_cld"/>
                            <%} %>
                        </div>
                    </div>
                </div>
                <script>
                    $(".tabs ").unitab();
                </script>
            </div>
            <!--实验计划  -->
                        <div style="display:<%=string.IsNullOrEmpty(testId)?"":"none"%>">
                <div class="box_tbl zebra">
                    <table>
                        <thead>
                            <tr class="title tbl_head">
                                <th style="">课程编号</th>
                                <th>实验计划名</th>
                                <th style="">计划学时</th>
                                <th style="">使用学时</th>
                                <th style="">班级</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%=planDetail %>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
