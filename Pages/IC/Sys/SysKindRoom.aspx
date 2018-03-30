<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SysKindRoom.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindRoom%>����</h2>
        <input type="hidden" id="AllOp" name="AllOp" />

        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="syskindRoom.aspx" id="syskindRoom"><%=ConfigConst.GCSysKindRoom%>����</a>
                    <a href="syskindRoomLab.aspx" id="syskindRoomLab"><%=ConfigConst.GCSysKindRoom%><%=ConfigConst.GCLabName%></a>
                    <a href="DevKindRoom.aspx" id="DevKindRoom"><%=ConfigConst.GCSysKindRoom%><%=ConfigConst.GCKindName %></a>
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="DevClassRoom.aspx" id="DevClassRoom"><%=ConfigConst.GCSysKindRoom%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                     <a href="resvruleroom.aspx?kind=1024" id="resvrule">ԤԼ����</a>
                     <a href="SysKindRoomSeat.aspx" id="SysKindRoomSeat">���λ����</a>
                </div>
            </div>

            <div class="FixBtn">
                <a id="btnNewRoom">�½�<%=ConfigConst.GCSysKindRoom%></a>
                <select class="opt" id="lab" name="lab">
                    <%=m_szLab %>
                </select>

            </div>

        </div>

        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szRoomNo">���</th>
                        <th name="szRoomName"><%=ConfigConst.GCSysKindRoom%>����</th>
                        <th name="dwMinUsers">����ʹ������</th>
                        <th name="dwMaxUsers">���ʹ������</th>
                        <th><%=ConfigConst.GCSysKindRoom+ConfigConst.GCKindName %></th>
                        <th name="szLabName">����<%=ConfigConst.GCLabName %></th>
                        <th>���Ź���</th>
                        <th name="dwManMode">���Ʒ�ʽ</th>
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
                            <a  class="setRoomBtn" href="#" title="�޸�"><img style="aimgsize" src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="delRoomBtn" title="ɾ��"><img style="aimgsize" src="../../../themes/iconpage/del.png"/></a>\
                            <a href="#" class="padimg" title="padչʾ���"><img style="aimgsize" src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#"  class="manGroupList" title="����Ա����"><img src="../../../themes/iconpage/mangerlist.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "defaultbg.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: 10, minWidth: "80", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNewRoom").click(function () {
                    $.lhdialog({
                        title: '�½�',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDeviceAndRoom.aspx?op=new'
                    });
                });
                $(".padimg").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().next().attr("data-roomno");
                    var szRoomName = $(this).parents("tr").children().first().next().attr("data-roomName");
                        window.open("../../../ClientWeb/pro/page/editContent.aspx?name=" + szRoomName + "&id=" + dwRoomID + "&type=RoomPadImg");
                });
                $(".padimg02Bak").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().children().first().val();
                    $.lhdialog({
                        title: '����',
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
                        title: '�޸�',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDeviceAndRoom.aspx?op=set&id=' + dwRoomID
                    });
                });
                $(".manGroupList").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().next().attr("ManGroupID");
                    $.lhdialog({
                        title: '����Ա����',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ManGroupList.aspx?op=set&dwID=' + dwRoomID
                    });
                });
                $(".delRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().next().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwRoomID);
                    }, '��ʾ', 1, function () { });
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
