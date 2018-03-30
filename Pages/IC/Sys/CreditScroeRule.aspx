<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="CreditScroeRule.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>信用制度</h2>
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
                        <th name="dwFeeSN">名称</th>
                        <th name="szFeeName">类别</th>
                        <th>用途</th>
                        <th name="dwPriority">周期</th>
                        <th name="dwPriority">周期内最大分值</th>
                        <th>惩罚天数</th>
                        <th>说明</th>
                        <th width="25px">操作</th>
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
                       <a class="set" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
               
                $(".set").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    var creditsn = $(this).parents("tr").children().first().attr("data-creditsn");
                    var ctsn = $(this).parents("tr").children().first().attr("data-ctsn");
                    $.lhdialog({
                        title: '修改',
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
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '提示', 1, function () { });
            });
                $("#btnNew").click(function () {
                $.lhdialog({
                    title: '新建',
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
