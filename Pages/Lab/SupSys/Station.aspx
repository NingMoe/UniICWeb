<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Station.aspx.cs" Inherits="Sub_Station"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>վ�����</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn"><a id="btnNewStation">�½�վ��</a></div>
       
    </div>
    <div class="content">
        <table class="ListTbl" ShowCheck="true">
            <thead>
                <tr><th width="60px">���</th><th>����</th><th>ϵͳ</th><th><%=ConfigConst.GCDeptName %></th><th>����Ա</th><th>ֵ��Ա</th><th>״̬</th><th>��ע</th><th width="25px">����</th></tr>
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
                        <a class="setStationBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setStationBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '�޸�վ��',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetStation.aspx?op=set&dwSN=' + dwSN
                });
            });
            $(".delBtn").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id")
                ConfirmBox("ȷ��ɾ��?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&ID=" + dwID);
                }, '��ʾ', 1, function () { });
            });
            
            $("#btnNewStation").click(function () {
                $.lhdialog({
                    title: '�½�վ��',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetStation.aspx?op=new'
                });
            });
        });
    </script>
</form>
</asp:Content>