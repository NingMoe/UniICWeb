<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Holiday.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>������Ա</h2>
    <div class="toolbar">
        <div class="tb_info">
              <div class="UniTab" id="tabl">
                   <a href="OpenRule.aspx">���Ź���</a>
                <a href="OpenGroup.aspx">���Ŷ���</a>
                <a href="holiday.aspx">�ڼ���</a>
            </div>
        </div>
        <div class="FixBtn"><a id="btnResvRule">�½��ڼ���</a></div>
        <div class="tb_btn">
          
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th>�ڼ�������</th><th>��ʼ����</th><th>��������</th><th width="25px">����</th></tr>
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
                        <a href="#" class="setResvRuleBtn" title="�޸Ľڼ���"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btnResvRule").click(function () {
                $.lhdialog({
                    title: '�½��ڼ���',
                    width: '620px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewHoliday.aspx?op=new'
                });
            });
            $(".setResvRuleBtn").click(function () {
                
                var dwGroup = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '�޸Ľڼ���',
                    width: '620px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewHoliday.aspx?op=set&dwID=' + dwGroup
                });
            });
            $(".delResvRuleBtn").click(function () {                
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("ȷ��ɾ��?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delName=" + dwLabID);
                }, '��ʾ', 1, function () { });
              });
        });
    </script>
</form>
</asp:Content>