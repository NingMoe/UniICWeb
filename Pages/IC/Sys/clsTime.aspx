<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="clsTime.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>��Ϣʱ���</h2>
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="term.aspx" id="term">ѧ�ڹ���</a>
                    <a href="clsTime.aspx" id="clsTime">��Ϣʱ���</a>
                </div>
            </div>
            <div class="FixBtn"><a id="btnResvRule">�½���Ϣʱ���</a></div>
            <div class="tb_btn">
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>���</th>
                        <th>����</th>
                        <th>��ʼʱ��</th>
                        <th>����ʱ��</th>
                        <th width="25px">����</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>          
        </div>
        <script type="text/javascript">
            $(function () {
                $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: false });
                var tabl = $(".UniTab").UniTab();
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    $.lhdialog({
                        title: '�½���Ϣʱ��',
                        width: '750px',
                        height: '380px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewClsTable.aspx?op=new'
                    });
                });
                $(".setResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸���Ϣʱ��',
                        width: '750px',
                        height: '380px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewClsTable.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".delResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '��ʾ', 1, function () { });
            });
        });
        </script>
    </form>
</asp:Content>
