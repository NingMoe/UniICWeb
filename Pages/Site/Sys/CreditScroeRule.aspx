<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="CreditScroeRule.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�����ƶ�</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"></div>
            <div class="tb_btn">
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="dwFeeSN">����</th>
                        <th name="szFeeName">���</th>
                        <th>��;</th>
                        <th name="dwPriority">����</th>
                        <th name="dwPriority">����������ֵ</th>
                        <th>�ͷ�����</th>
                        <th>˵��</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="set" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
               
                $(".set").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    var creditsn = $(this).parents("tr").children().first().attr("data-creditsn");
                    var ctsn = $(this).parents("tr").children().first().attr("data-ctsn");
                    $.lhdialog({
                        title: '�޸�',
                        width: '750px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewCreditScoreRule.aspx?op=set&dwID=' + dwLabID + '&dwCTSN=' + ctsn + '&dwCreditSN=' + creditsn
                    });
                });
                $(".delLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '��ʾ', 1, function () { });
            });
                $("#btnNew").click(function () {
                $.lhdialog({
                    title: '�½�',
                    width: '750px',
                    height: '300px',
                    lock: true,
                    content: 'url:Dlg/NewCreditScoreRule.aspx?op=new'
                });
            });
            //$(".ListTbl").UniTable();

        });
        </script>
    </form>
</asp:Content>
