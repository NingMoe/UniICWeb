<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Term.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>学期管理</h2>
    <div class="toolbar">
       <div class="tb_info">
            <div class="UniTab" id="tabl">
                    <a href="term.aspx" id="term">学期管理</a>
                    <a href="clsTime.aspx" id="clsTime">作息时间表</a>                                      
                </div>
        </div>
        <div class="FixBtn"><a id="btnResvRule">新建学期</a></div>
        <div class="tb_btn">
          
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th name="szMemo">学期</th><th name="dwBeginDate">学期时间</th><th>第一周天数</th><th name="dwTotalWeeks">周数</th><th name="dwStatus">状态</th><th width="25px">操作</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table> 
         <uc1:PageCtrl runat="server" ID="PageCtrl"/>      
    </div>
    <script type="text/javascript">
        $(function () {
            $(".ListTbl").UniTable();
            var tabl = $(".UniTab").UniTab();
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btnResvRule").click(function () {
                $.lhdialog({
                    title: '新建学期',
                    width: '750px',
                    height: '380px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewTerm.aspx?op=new'
                });
            });
            $(".setResvRuleBtn").click(function () {             
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '修改学期',
                    width: '750px',
                    height: '380px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetTerm.aspx?op=set&dwID=' + dwID
                });
            });
            $(".delResvRuleBtn").click(function () {                
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