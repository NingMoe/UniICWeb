<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SupSys_DoorCtrl.aspx.cs" Inherits="SupSys_DoorCtrl"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>�Ž�����</h2>
    <div class="toolbar">
        <div class="tb_info">������5�������ߣ�5��</div>
        <div class="FixBtn"><a id="btnNewDoorCtrl">�½�</a></div>
        <div class="tb_btn">
            <div class="AdvOpts"><div class="AdvLab">�߼�ѡ��</div>
                <fieldset><legend>���</legend>
                    <label><input name="kind" value="1" type="checkbox" />���1</label>  <label><input name="kind" value="2" type="checkbox" />���2</label>
                </fieldset>
                <fieldset><legend>״̬</legend>
                    <label><input name="kind" value="1" type="checkbox" />������</label>  <label><input name="kind" value="2" type="checkbox" />δ����</label>
                </fieldset>
            </div>
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="65px">���������</th><th>�Ž����</th><th>����������</th><th>�����</th><th>վ��</th><th>��ע</th><th width="25px">����</th></tr>
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
                        <a class="setDoorCtrlBtn" title="�޸�"><img src="../themes/iconpage/edit.png"/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setDoorCtrlBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().text();
                $.lhdialog({
                    title: '�޸ļ�����',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_SetDoorCtrl.aspx?op=set&dwSN=' + dwSN
                });
            });
            $("#btnNewDoorCtrl").click(function () {
                $.lhdialog({
                    title: '�½�������',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_SetDoorCtrl.aspx?op=new'
                });
            });
        });
    </script>
</form>
</asp:Content>