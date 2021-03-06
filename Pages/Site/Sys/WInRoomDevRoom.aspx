<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="WInRoomDevRoom.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCKindName %></h2>
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="WInRoomDev.aspx" id="WDevTeachingRoom">室内资源管理</a>
                    <a href="WInRoomDevKind.aspx" id="WInRoomDevKind">室内资源类型管理</a>
                    <a href="WInRoomDevLab.aspx" id="WInRoomDevLab">室内资源楼宇管理</a>
                    <a href="WInRoomDevRoom.aspx" id="WInRoomDevRoom">室内资源楼层管理</a>
                </div>
            </div>
            <div class="FixBtn">
                <a id="btnNew" class="btnClss">新建楼层</a>
            </div>
        </div>
        <div>
            <div class="tb_infoInLine" style="margin: 5px 0px">                
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szRoomNo">编号</th>
                        <th name="szRoomName">楼层名称</th>
                        <th name="szLabName">所属楼宇</th>
                        <th>房间数</th>                    
                        <th name="szOpenRuleName">开放规则</th>
                        <th width="25px">操作</th>
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
                $(".opt").css({ width: "120px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                var tabl = $(".UniTab").UniTab();
               

                $(".class2").html('<div class="OPTDBtn">\
                            <a  class="setRoomBtn" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="delRoomBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                            <a href="#"  class="manGroupList" title="管理员名单"><img src="../../../themes/icon_s/5.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "defaultbg.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#OPTDCTRL").html('<div class="OPTDBtnSet">\
                            <a href="javascript:fAllOp("open")" title="远程开门"><img src="../../../themes/icon_s/6.png"/></a>\
                            <a href="javascript:fAllOp("shutdown")" title="设备关机"><img src="../../../themes/icon_s/15.png"/></a>\
                            <a href="javascript:fAllOp("restart")" title="设备重启"><img src="../../../themes/icon_s/13.png"/></a>\
                            <a href="javascript:fAllOp("nologin")" title="设备免登陆"><img src="../../../themes/icon_s/21.png"/></a>\
                            <a href="javascript:fAllOp("neddlogin")" title="设备需要登陆"><img src="../../../themes/icon_s/3.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="设备卸载客户端"><img src="../../../themes/icon_s/5.png"/></a>\</div>');
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: 10, minWidth: "80", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNew").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/WNewRoomInRoom.aspx?op=new'
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
                    var dwRoomID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '修改',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/WSetRoomInRoom.aspx?op=set&roomid=' + dwRoomID
                    });
                });
                $(".manGroupList").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().data("mangroupid");
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
                    var dwRoomID = $(this).parents("tr").children().first().data("id");
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
