<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="OpenRule.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>���Ź������</h2>
    <div class="toolbar">
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="OpenRule.aspx">���Ź���</a>
                <a href="OpenGroup.aspx">���Ŷ���</a>
                <a href="holiday.aspx">�ڼ���</a>
            </div>
        </div>
        <div class="FixBtn"><a id="btnResvRule">�½����Ź���</a></div>
        <div class="tb_btn">
          
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th>����</th><th>��ע</th><th width="25px"></th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>       
    </div>
    <script type="text/javascript">
        $(function () {
            $(".UniTab").UniTab();
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="setSpecialDate" title="������������"><img src="../../../themes/iconpage/calendar.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btnResvRule").click(function () {
                $.lhdialog({
                    title: '�½����Ź���',
                    width: '920px',
                    height: '550px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewOpenRule.aspx?op=new'
                });
            });

            
            $(".setSpecialDate").click(function () {
                var openruleSN = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '������������',
                    width: '1000px',
                    height: '450px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/setSpecaliDate.aspx?op=set&dwID=' + openruleSN
                });
            });
            $(".setResvRuleBtn").click(function () {                
                var openruleSN = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '�޸Ŀ��Ź���',
                    width: '920px',
                    height: '550px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewOpenRule.aspx?op=set&dwID=' + openruleSN
                });
            });
            $(".delResvRuleBtn").click(function () {
                var szLabSN = $(this).parents("tr").children().first().text();
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("ȷ��ɾ��?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '��ʾ', 1, function () { });
              });
        });
    </script>
</form>
</asp:Content>