<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SWCtrlP.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>软件监控管理</h2>
    <div class="toolbar">
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="SWCtrl.aspx">黑名单</a><a href="SWCtrlP.aspx">白名单</a>
            </div>
        </div>
        <div class="FixBtn"><a id="newBtn">新建白名单</a></div>
        <div class="tb_btn">
          
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th>名称</th><th>适用年龄</th><th>备注</th><th width="25px">操作</th></tr>
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
                        <a href="#" class="setBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#newBtn").click(function () {
                $.lhdialog({
                    title: '新建白名单',
                    width: '600px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetClassP.aspx?op=new&type=sw'
                });
            });
            $(".setBtn").click(function () {                
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '修改黑名单',
                    width: '600px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetClassF.aspx?op=set&id=' + dwID
                });
            });
            $(".delBtn").click(function () {
             
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '提示', 1, function () { });
              });
        });
    </script>
</form>
</asp:Content>