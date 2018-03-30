<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="UserGroup.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>特殊人员</h2>
    <div class="toolbar">
        <div class="tb_info">
              <div class="UniTab" id="tabl">
                  <a href="resvrule.aspx">预约规则</a><a href="UserGroup.aspx">特殊人员</a>
            </div>
        </div>
        <div class="FixBtn"><a id="btnResvRule">新建特殊人员</a></div>
      
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th>规则名称</th><th>成员</th><th width="25px"></th></tr>
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
                        <a href="#" class="setResvRuleBtn" title="修改/添加成员"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btnResvRule").click(function () {
                $.lhdialog({
                    title: '新建特殊人员',
                    width: '620px',
                    height: '200px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/newopenGroup.aspx?op=new&dwKind=2048'
                });
            });
            $(".setResvRuleBtn").click(function () {
                var dwGroup = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '修改特殊人员',
                    width: '620px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/setuseGroup.aspx?op=set&dwID=' + dwGroup
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