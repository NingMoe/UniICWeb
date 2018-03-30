<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DoorCamera.aspx.cs" Inherits="SupSys_DoorCtrl"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>����ͷ����</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn"><a id="btnNewDoorCtrl">�½�</a></div>       
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="60px">����ͷ���</th><th>����������</th><th>���������</th><th>�����</th><th>վ��</th><th>��ע</th><th width="25px">����</th></tr>
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
                        <a class="setDoorCtrlBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
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
                    content: 'url:dlg/SetDoorCam.aspx?op=set&dwSN=' + dwSN
                });
            });
            $("#btnNewDoorCtrl").click(function () {
                $.lhdialog({
                    title: '�½�������',
                    width: '750px',
                    height: '300px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:dlg/SetDoorCam.aspx?op=new'
                });
            });
        });
    </script>
</form>
</asp:Content>