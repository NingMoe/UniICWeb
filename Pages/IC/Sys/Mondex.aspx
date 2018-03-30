<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Mondex.aspx.cs" Inherits="SupSys_DCS" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>���������</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnNewDCS">�½�</a></div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>����</th>
                        <th>IP</th>
                        <th>�˿�</th>
                        <th>����</th>
                        <th>��ע</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="setResvRuleBtn2" title="��λ��������"><img src="../../../themes/iconpage/attachment.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".delResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '��ʾ', 1, function () { });
                 });
                $(".setResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸ļ����',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevMon.aspx?op=set&dwSN=' + dwID
                    });
                });
                $(".setResvRuleBtn2").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '��λ��������',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setmondev.aspx?op=set&dwMonitorID=' + dwID
                    });
                });
                $("#btnNewDCS").click(function () {
                    $.lhdialog({
                        title: '�½������',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevMon.aspx?op=new'
                    });
                });
            });
        </script>
    </form>
</asp:Content>
