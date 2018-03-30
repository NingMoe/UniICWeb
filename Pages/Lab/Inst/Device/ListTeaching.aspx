<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ListTeaching.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>教学所需设备</h2>
        <div class="toolbar">
            <div class="tb_info">
                <!--总数：5条，班级：5个，房间数：5个 ----->
                <%=m_szOpts %>
            </div>
            <div>
                <table border="1">
                    <tr>
                        <td>
                            <button type="button" id="Back">返回</button></td>
                        <td>已排课房间:<%=m_szRoom %>|</td>
                        <td>设备状态:<label><input class="enum" type="radio" name="devRunState" value="1">开机</label><input class="enum" type="radio" name="devRunState" value="4">
                            被预约<label><input class="enum" type="radio" name="devRunState" value="2">
                                使用中
                            </label>
                        </td>
                        <td>上课中的课程:<%=m_szResvTeaching %></td>
                          <td><button type="button" id="btnSerach">查询</button></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <tdead>
                    <tr>
                        <th name="dwDevSN">机器编号</th>
                        <th name="szRoomName">房间</th>
                        <th name="dwRunStat">状态</th>
                        <th name="szCurTrueName">使用者</th>
                        <th name="szGroupName">上课班级</th>
                        <th name="szCourseName">课程</th>                       
                        <th name="szTeacherName">上课教师</th>
                        <th name="dwTeachingTime">上课时间</th>
                        <th>登陆时间!</th>
                        <th width="25px">操作</th>
                    </tr>
                </tdead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
            <fieldset style="width: 99%">
                <legend align="center">教学设备使用情况</legend>
                <div class="ColumnStat tblBottomStat" data-color="1">
                    <h1><span></span><strong>所需设备</strong><strong>使用设备</strong><strong>上课人数</strong><strong>实到人数</strong></h1>
                    <%=m_szPie %>
                </div>
            </fieldset>
        </div>
        <script type="text/javascript">
            $(function () {

                $("#Back").button().click(function () {
                    TabJump("Device/Stat.aspx");
                });
                $("#btnSerach").button;
                $(".OPTDUN").html('<div class="OPTDBtn">\
                    <a href="#" class="NOLOGIN" title="免登陆"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="NEEDLOGIN" title="需要登陆"><img src="../../../themes/icon_s/11.png"/></a>\
                    <a href="#" class="WAKEUP" title="远程开机"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" class="SHUTDOWN"  title="远程关机"><img src="../../../themes/icon_s/13.png"/></a>\
                    <a href="#" class="RESTART" title="远程重启"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" class="ADMINMESSAGE" title="发送消息"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" class="SCREEN" title="查看屏幕"><img src="../../../themes/iconpage/edit.png"/></a></div>');
                $(".OPTDUSING").html('<div class="OPTDBtn">\
                    <a href="#" class="ADMINMESSAGE" title="发送消息"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" class="SHUTDOWN" title="关机"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="RESTART"  title="重启"><img src="../../../themes/icon_s/11.png"/></a>\
                    <a href="#" class="SCREEN" title="查看屏幕"><img src="../../../themes/iconpage/edit.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                var OldTabReload = null;

                if (OldTabReload == null) {
                    OldTabReload = TabReload;
                }
                $("[name='room']").click(function (event) {
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });
                $("[name='devRunState']").click(function (event) {
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                    });
                    $("[name='resvid']").click(function (event) {
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                    });
                    var pForm = $("form");
                    TabReload = function (fdata) {
                        var url = "device/ListTeaching.aspx";
                        if (url != null && url + "" != "undefined") {
                            $.ajax({
                                url: url,
                                data: fdata,
                                type: "POST",
                                timeout: 600000,
                                async: true,
                                dataType: "html",
                                success: function (data, status) {
                                    pForm.empty()
                                    var pData = $("<div>" + data + "</div>").appendTo(pForm);
                                    if (OnTabLoad) {
                                        OnTabLoad(null, { panel: pData });
                                    }
                                },
                                error: function (data, status, error) {
                                    MessageBox(status, "", 2);
                                }
                            });
                        } else {
                            if (OldTabReload != null) {
                                OldTabReload(fdata);
                            }
                        }
                    }

            });
                    setTimeout(function () {
                        $(".WAKEUP").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            $.lhdialog({
                                title: '远程开机',
                                width: '250px',
                                height: '160px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendCtrlDev.aspx?type=11&id=' + dwID
                            });
                        });
                        $(".SHUTDOWN").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: '远程关机',
                                width: '250px',
                                height: '160px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendCtrlDev.aspx?type=12&id=' + dwID + '&labid=' + labid
                            });
                        });
                        $(".RESTART").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: '远程重启',
                                width: '250px',
                                height: '160px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendCtrlDev.aspx?type=13&id=' + dwID + '&labid=' + labid
                            });
                        });
                        $(".NOLOGIN").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: '免登陆',
                                width: '250px',
                                height: '160px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendCtrlDev.aspx?type=52&id=' + dwID + '&labid=' + labid
                            });
                        });
                        $(".NEEDLOGIN").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: '需要登陆',
                                width: '250px',
                                height: '160px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendCtrlDev.aspx?type=51&id=' + dwID + '&labid=' + labid
                            });
                        });
                        $(".SCREEN").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: '查看屏幕',
                                width: '800px',
                                height: '600px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/RomteScreen.aspx?id=' + dwID + '&labid=' + labid
                        });
                        });
                        $(".ADMINMESSAGE").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: '发送消息',
                                width: '600px',
                                height: '200px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendMessageCtrlDev.aspx?type=81&id=' + dwID + '&labid=' + labid
                        });
                        });


                    }, 1);

        </script>
    </form>
</asp:Content>
