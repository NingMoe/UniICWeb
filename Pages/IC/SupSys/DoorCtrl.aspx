<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DoorCtrl.aspx.cs" Inherits="SupSys_DoorCtrl"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>�Ž�����</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn">
              <a id="btndoorctrl">����������Pad</a>
              <a id="export">��������ά�룩</a>
            <a id="btnNewDoorCtrl">�½�</a>

        </div>       
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="60px">�Ž����</th><th>���������</th><th>����������</th><th>�����</th><th>վ��</th><th>��ע</th><th width="25px">����</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
    </div>
    <script type="text/javascript">
        $(function () {
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setDoorCtrlBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delDoorCtrlBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                        <a href="#" class="print" title="print"><img src="../../../themes/icon_s/15.png"/></a>\</div>');

            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
            });
            
            $(".setDoorCtrlBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().text();
                $.lhdialog({
                    title: '�޸Ŀ�����',
                    width: '750px',
                    height: '300px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:dlg/SetDoorCtrl.aspx?op=set&dwSN=' + dwSN
                });
            });
            $("#btndoorctrl").button().click(function () {
                $.lhdialog({
                    title: '����������Pad',
                    width: '250px',
                    height: '120px',
                    lock: true,
                    content: 'url:Dlg/DoorCtrlExportPad.aspx?op=new'
                });
            });
            $("#export").button().click(function () {
                $.lhdialog({
                    title: '����',
                    width: '250px',
                    height: '120px',
                    lock: true,
                    content: 'url:Dlg/doorctrlexport.aspx?op=new'
                });
            });
            $(".print").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                var dwsn = $(this).parents("tr").children().first().attr("data-sn");
                var roomno = $(this).parents("tr").children().first().attr("data-roomno");
                window.open("http://update.unifound.net/wxnotice/qrcode.aspx?pcid=" + dwsn + "&id=" + dwID + "&session=InDoor&msg=" + roomno)

            });
            
            $(".delDoorCtrlBtn").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                var dwsn = $(this).parents("tr").children().first().attr("data-sn");
                ConfirmBox("ȷ��ɾ��?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID + '&delsn=' + dwsn);
                }, '��ʾ', 1, function () { });
            });
            $("#btnNewDoorCtrl").click(function () {
                $.lhdialog({
                    title: '�½�������',
                    width: '750px',
                    height: '300px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:dlg/SetDoorCtrl.aspx?op=new'
                });
            });
        });
    </script>
</form>
</asp:Content>