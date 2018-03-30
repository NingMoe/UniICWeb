<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="OpenRule.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>开放规则管理</h2>
    <div class="toolbar">
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="OpenRule.aspx">开放规则</a>
                <a href="OpenGroup.aspx">开放对象</a>
                <a href="holiday.aspx">节假日</a>
            </div>
        </div>
        <div class="FixBtn"><a id="btnResvRule">新建开放规则</a></div>
        <div class="tb_btn">
          
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th>名称</th><th>备注</th><th width="25px"></th></tr>
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
                        <a href="#" class="setResvRuleBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="setSpecialDate" title="设置特殊日期"><img src="../../../themes/iconpage/calendar.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btnResvRule").click(function () {
                $.lhdialog({
                    title: '新建开放规则',
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
                    title: '设置特殊日期',
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
                    title: '修改开放规则',
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
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '提示', 1, function () { });
              });
        });
    </script>
</form>
</asp:Content>