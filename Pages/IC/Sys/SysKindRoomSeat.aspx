<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SysKindRoomSeat.aspx.cs" Inherits="Sub_Room" %>

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
                    <a id="btnNew">�½�<%=ConfigConst.GCSysKindSeat %></a>
                <a id="newList">�����½�</a>
            </div>

        </div>

        <div class="content">
               <table class="ListTbl">
                <thead>
                    <tr>         
                        <th name="dwDevSN">��λ���</th>                       
                        <th name="szDevName">��λ����</th> 
                        <th name="dwDevStat">״̬</th>                       
                        <th name="szKindName">����<%=ConfigConst.GCKindName %></th>                                              
                        <th name="szRoomName">����<%=ConfigConst.GCRoomName %></th>            
                       <!-- <th name="szLabName">����<%=ConfigConst.GCLabName %></th>-->
                        <th name="szAssertSN">�ʲ���</th>      
                        <th>���ܿ���</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\
                            <a  class="set" href="#" title="�޸�"><img style="aimgsize" src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="del" title="ɾ��"><img style="aimgsize" src="../../../themes/iconpage/del.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "defaultbg.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: 10, minWidth: "80", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNew").click(function () {
                    $.lhdialog({
                        title: '�½�',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewRoomSeatDev.aspx?op=new'
                    });
                });
                $("#newList").click(function () {
                    $.lhdialog({
                        title: '�����½�',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewRoomDeviceSeatList.aspx?op=new'
                    });
                });

                $(".InfoRoomBtn").click(function () {
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
                $(".set").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().next().attr("data-id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '720px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setRoomSeatDev.aspx?op=set&id=' + dwRoomID
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
                $(".del").click(function () {
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
