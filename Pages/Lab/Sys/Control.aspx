<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Control.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>控制台管理</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn"><a id="btnResvRule">新建控制台</a></div>
        <div class="tb_btn">
          
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th name="dwConsoleSN">控制台编号</th><th name="szConsoleName">名称</th><th name="szIP">IP地址</th><th name="dwOpenTime">开放时间</th><th>类型</th><th>对象</th><th  name="dwStatus">状态</th><th width="25px">操作</th></tr>
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
                        <a href="#" class="setResvRuleBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btnResvRule").click(function () {
                $.lhdialog({
                    title: '新建控制台',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewControl.aspx?op=new'
                });
            });
            $(".setResvRuleBtn").click(function () {             
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '修改控制台',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetControl.aspx?op=set&dwID=' + dwID
                });
            });
            $(".delResvRuleBtn").click(function () {                
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '提示', 1, function () { });
              });
            //$(".ListTbl").UniTable();

        });
    </script>
</form>
</asp:Content>