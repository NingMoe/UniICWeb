<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Station.aspx.cs" Inherits="Sub_Station"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>站点管理</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn"><a id="btnNewStation">新建站点</a></div>
       
    </div>
    <div class="content">
        <table class="ListTbl" ShowCheck="true">
            <thead>
                <tr><th width="60px">编号</th><th>名称</th><th>系统</th><th><%=ConfigConst.GCDeptName %></th><th>管理员</th><th>值班员</th><th>状态</th><th>备注</th><th width="25px">操作</th></tr>
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
                        <a class="setStationBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setStationBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '修改站点',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetStation.aspx?op=set&dwSN=' + dwSN
                });
            });
            $(".delBtn").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id")
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&ID=" + dwID);
                }, '提示', 1, function () { });
            });
            
            $("#btnNewStation").click(function () {
                $.lhdialog({
                    title: '新建站点',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetStation.aspx?op=new'
                });
            });
        });
    </script>
</form>
</asp:Content>