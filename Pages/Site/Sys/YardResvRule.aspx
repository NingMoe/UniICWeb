<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="YardResvRule.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>规则配置</h2>
        <div class="toolbar">
           <div class="tb_info">
              <div class="UniTab" id="tabl">
                    <a id="YardActivity" href="YardActivity.aspx">活动类型管理</a><a id="YardResvRule"  href="YardResvRule.aspx">规则配置</a>
            </div>
        </div>
            <div>
                <input type="hidden" id="extValue" name="extValue" />
            </div>
        <div class="FixBtn"><a id="btnResvRule">新建规则配置</a></div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szRuleName">规则名称</th>
                        <th name="dwIdent">身份</th>
                        <th><%=ConfigConst.GCKindName %></th>
                        <th>预约时长</th>
                        <th>可预约时间范围</th>
                        <th width="25px">操作</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">
            $(function () {
                $(".ListTbl").UniTable();
                $(".UniTab").UniTab();
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    var extValue = $("#extValue").val();
                    if (extValue == null || extValue == "") {
                        $.lhdialog({
                            title: '新建',
                            width: '920px',
                            height: '700px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:Dlg/NewYardResvRule.aspx?op=new'
                        });
                    }
                    else {
                        $.lhdialog({
                            title: '新建',
                            width: '920px',
                            height: '700px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:Dlg/NewYardResvRule.aspx?op=new&extValue=' + extValue
                        });
                    }
                  
                });
                $(".CopyResvRule").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '复制规则给其他设备',
                        width: '920px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/CopyResvRule.aspx?op=set&dwID=' + dwLabID
                    });
                });
                
                $(".setResvRuleBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '修改',
                        width: '920px',
                        height: '700px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewYardResvRule.aspx?op=set&dwID=' + dwLabID
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
            //$(".ListTbl").UniTable();
        });
        </script>
    </form>
</asp:Content>
