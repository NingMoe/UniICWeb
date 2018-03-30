<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SysKindRoomCam.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindRoom%>组合管理</h2>
        <input type="hidden" id="AllOp" name="AllOp" />

        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="syskindRoom.aspx" id="syskindRoom"><%=ConfigConst.GCSysKindRoom%>管理</a>
                     <a href="SysKindRoomCam.aspx" id="A1"><%=ConfigConst.GCSysKindRoom%>组合管理</a>
                    <a href="syskindRoomLab.aspx" id="syskindRoomLab"><%=ConfigConst.GCSysKindRoom%>区域</a>
                    <a href="DevKindRoom.aspx" id="DevKindRoom"><%=ConfigConst.GCSysKindRoom%><%=ConfigConst.GCKindName %></a>
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="DevClassRoom.aspx" id="DevClassRoom"><%=ConfigConst.GCSysKindRoom%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                     <a href="resvruleroom.aspx?kind=1024" id="resvrule">预约规则</a>
                </div>
            </div>

            <div class="FixBtn">
                <a id="btnNewRoom">新建<%=ConfigConst.GCSysKindRoom%></a>
                <select class="opt" id="lab" name="lab">
                    <%=m_szLab %>
                </select>

            </div>

        </div>

        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szRoomNo">编号</th>
                        <th name="szRoomName"><%=ConfigConst.GCSysKindRoom%>名称</th>
                        <th name="dwMinUsers">最少使用人数</th>
                        <th name="dwMaxUsers">最多使用人数</th>
                        <th><%=ConfigConst.GCSysKindRoom+ConfigConst.GCKindName %></th>
                        <th name="szLabName">所属<%=ConfigConst.GCLabName %></th>
                        <th>开放规则</th>
                        <th name="dwManMode">控制方式</th>
                        <th width="25px"></th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">

            $(function () {
                var tabl = $(".UniTab").UniTab();
                $(".opt").css({ width: "120px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                $(".class2").html('<div class="OPTDBtn">\
                            <a  class="setRoomBtn" href="#" title="修改"><img style="aimgsize" src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="delRoomBtn" title="删除"><img style="aimgsize" src="../../../themes/iconpage/del.png"/></a>\
                            <a href="#"  class="manGroupList" title="管理员名单"><img src="../../../themes/iconpage/mangerlist.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "defaultbg.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: 10, minWidth: "80", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNewRoom").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '720px',
                        height: '360px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDeviceAndRoom.aspx?op=new'
                    });
                });
                $(".InfoRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().children().first().val();
                    $.lhdialog({
                        title: '介绍',
                        width: '720px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwRoomID + "&type=RoomInfo"
                    });
                });
                $(".setRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().next().attr("data-id");
                    $.lhdialog({
                        title: '修改',
                        width: '720px',
                        height: '350px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDeviceAndRoom.aspx?op=set&id=' + dwRoomID
                    });
                });
                $(".manGroupList").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().next().attr("ManGroupID");
                    $.lhdialog({
                        title: '管理员名单',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ManGroupList.aspx?op=set&dwID=' + dwRoomID
                    });
                });
                $(".delRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().next().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwRoomID);
                    }, '提示', 1, function () { });
                });
                $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: true });
            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
