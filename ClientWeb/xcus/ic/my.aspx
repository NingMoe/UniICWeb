<%@ Page Language="C#" AutoEventWireup="true" CodeFile="my.aspx.cs" Inherits="Page_" %>

<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx" %>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx" %>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx" %>
<%@ Register TagPrefix="Uni" TagName="include" Src="modules/include.ascx" %>
<%@ Register TagPrefix="Uni" TagName="group" Src="modules/dlg_group_mbs.ascx" %>
<%@ Register TagPrefix="Uni" TagName="acc" Src="~/ClientWeb/pro/net/acc.ascx" %>
<%@ Register TagPrefix="Uni" TagName="basic" Src="~/ClientWeb/pro/net/dlg_basic.ascx" %>
<%@ Register TagPrefix="Uni" TagName="resv" Src="~/ClientWeb/pro/net/dlg_resv.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE" />
    <meta name="renderer" content="webkit">
    <title>我的空间</title>
    <link rel="stylesheet" href="style/css/main.css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.20.custom.min.js"></script>
        <script type="text/javascript"  src="js/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" src="js/site.js"></script>
    <Uni:include runat="server" />
    <style>
        .seat { display: none; }
        .hidden { display: none; }
        .HeaderStyle { text-align: center; }
        .list_tbl { width: 100%; }
        .unitab ul { margin-top: 10px; border-bottom: 1px solid #ddd; }
        .unitab ul li { padding: 5px; color: #999; font-size: 14px; }
        .unitab ul li.h_sel { color: #FFBA00; }
    </style>
    <script type="text/javascript">
        function delRsv(rsv) {
            var id = $(rsv).attr("rsvId");
            uni.confirm("确定要删除吗？", function () {
                pro.j.rsv.delResv(id, function () {
                    uni.msgBox("删除成功！");
                    $(rsv).parents("tr:first").remove();
                });
            });
        }
        function alterRsv(rsv) {
            var rsv=$(rsv);
            var kind = rsv.attr("devKind");
            var devId = rsv.attr("devId");
            var rsvId=rsv.attr("rsvId");
            var start=rsv.attr("start");
            var end=rsv.attr("end");
            pro.d.resv.alterTime(devId,kind,rsvId,start,end);
        }
    </script>
</head>
<body>
        <Uni:basic ID="Resv1" runat="server" />
    <Uni:resv runat="server" />
    <div class="body">
        <Uni:sidebar runat="server" />
        <div class="content">
            <Uni:acc runat="server"/>
            <Uni:nav runat="server" />
            <Uni:group runat="server" />
            <form id="Form1" runat="server">
                <div class="my">
                    <h1>我的空间</h1>
                    <div id="space_tabs" class="space_tabs tabs3">
                        <ul>
                            <li id="tab_1"><a href="#space_tab_1">预约记录</a></li>
                            <li id="tab_2"><a href="#space_tab_2">修改资料</a></li>
                            <li id="tab_3"><a href="#space_tab_3">违约记录</a></li>
                        </ul>


                        <div id="space_tab_1">
                            <div style="font-size: 13px; margin-bottom: 5px;">
                                <asp:RadioButtonList ID="radState" Width="200" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="radState_SelectedIndexChanged" TextAlign="Left">
                                    <asp:ListItem Selected="True" Value="768">未执行预约</asp:ListItem>
                                    <asp:ListItem Value="1073741824">一年内历史记录</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="unitab">
                                <div>总：<span class="pc_total red"></span>&nbsp;条记录，分&nbsp;<span class="pc_ptotal red"></span>&nbsp;页，当前第&nbsp;<span class="pc_here red"></span>&nbsp;页</div>
                                <table class="list_tbl tab_con">
                                    <thead>
                                        <tr>
                                            <th>预约号</th>
                                            <th>预约人</th>
                                            <th style="max-width: 180px;">预约组成员</th>
                                            <th>预约对象</th>
                                            <th>预约状态</th>
                                            <th>开始时间</th>
                                            <th>结束时间</th>
                                            <th style="width: 80px;">操作</th>
                                        </tr>
                                    </thead>
                                    <tbody><%=rsvList %></tbody>

                                </table>
                                <ul class="tab_head"></ul>
                            </div>
                            <table class="orderlog device">
                            </table>
                        </div>

                        <div id="space_tab_2">
                            <table class="changeinfo">
                                <tr>
                                    <th>
                                        <p>学号:</p>
                                    </th>
                                    <td><%=vrAccInfo.szLogonName%></td>
                                </tr>
                                <tr>
                                    <th>
                                        <p>手机:</p>
                                    </th>
                                    <td>
                                        <input name="phone" type="text" class="input_txt" value="<%=vrAccInfo.szHandPhone%>" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        <p>邮箱:</p>
                                    </th>
                                    <td>
                                        <input name="mail" type="text" class="input_txt" value="<%=vrAccInfo.szEmail%>" /></td>
                                </tr>
                                <tr style="display: <%=changePsw%>">
                                    <th>
                                        <p>新密码:</p>
                                    </th>
                                    <td>
                                        <input name="szPasswd1" type="password" class="input_txt" value="" /></td>
                                </tr>
                                <tr style="display: <%=changePsw%>">
                                    <th>
                                        <p>重复新密码:</p>
                                    </th>
                                    <td>
                                        <input name="szPasswd2" type="password" class="input_txt" value="" /></td>
                                </tr>
                            </table>
                            <div class="submitarea">
                                <asp:Button class="input_submit" value="保存" runat="server" ID="updateAccount" OnClick="updateAccount_Click" />
                            </div>

                        </div>
                        <div id="space_tab_3">
                            <div style="font-size: 13px;">
                                <div class="unitab">
                                    <div>总：<span class="pc_total red"></span>&nbsp;条记录，分&nbsp;<span class="pc_ptotal red"></span>&nbsp;页，当前第&nbsp;<span class="pc_here red"></span>&nbsp;页</div>
                                    <table class="list_tbl tab_con">
                                        <thead>
                                            <tr>
                                                <th>预约号</th>
                                                <th>预约人</th>
                                                <th>预约对象</th>
                                                <th>预约状态</th>
                                                <th>开始时间</th>
                                                <th>结束时间</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <%=rsvList2 %>
                                        </tbody>
                                    </table>
                                    <ul class="tab_head"></ul>
                                </div>
                                <script>
                                    $(".unitab").unitab(null, {
                                        pctrl: 20, pctrlFun: function (index, need, total, obj) {
                                            $(".pc_total", obj).html(total);
                                            $(".pc_here", obj).html(index + 1);
                                            $(".pc_ptotal", obj).html(obj.ptotal);
                                        },
                                        custom: true
                                    });
                                </script>
                                <table class="orderlog device">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
            <div class="copyright">版权说明</div>
        </div>
    </div>
    <Uni:dialog runat="server" />
    <script>
        $("form").submit(function () {
            var data = $(this).serialize();
            $.ajax({
                type: "GET",
                url: "Ajax_Code/account.aspx?act=update&" + data,
                dataType: "json",
                success: function (object) {
                    if (object.MsgId > 0)
                        alert(object.Message);
                    else {
                        alert("更新成功");
                        location.reload();
                    }
                }
            });
        });
    </script>
</body>
</html>

