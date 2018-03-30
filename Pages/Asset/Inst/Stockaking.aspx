<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Stockaking.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>盘点计划</h2>
          <div class="tb_info" style="margin-bottom:15px">
            <div class="UniTab" id="tabl">
                <a href="stockaking.aspx" id="stockaking">盘点计划</a>
                <a href="stockakingDetail.aspx" id="stockakingDetail">盘点明细</a>
            </div>
              </div>
        <div>
            <div class="FixBtn"><a id="btnStockings">新建盘点计划</a></div>
         
        </div>
         <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                   <th>盘点开始日期:</th>
                   <td><input type="text" id="dwStartDate" runat="server" name="dwStartDate" /></td>
                   <th>盘点结束日期:</th>
                   <td><input type="text" id="dwEndDate" runat="server" name="dwEndDate" /></td>
                    <th>盘点状态:</th>
                    <td><select id="dwSTStat" name="dwSTStat"><%=sz_Staues %></select></td>
                    <td><input type="submit" id="btn" value="查询" style="width:80px" /></td>
                </tr>
           </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>计划描述内容</th>
                        <th name="dwSTDate">盘点日期</th>
                        <th name="szKindName">盘点类型</th>
                        <th name="szRoomName">盘点房间</th>
                        <th name="dwMinUnitPrice">单价范围(元)</th>
                        <th name="dwSTStat">状态</th>
                        <th name="dwSTEndDate">盘点结束日期</th>
                        <th>操作</th>
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
                var tabl = $(".UniTab").UniTab();
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="getStockDetail" title="查看盘点明细"><img src="../../../themes/icon_s/17.png"/></a>\
                       <a class="setLabBtn" title="设置盘点结束"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btn").button();
            $("#<%=dwEndDate.ClientID%>,#<%=dwStartDate.ClientID%>").datepicker({
            });
            $(".setLabBtn").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("确定盘点已经结束?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=over&delID=" + dwID);
                }, '提示', 1, function () { });
            });
                $(".getStockDetail").click(function () {
                    var id =$(this).parents("tr").children().first().attr("data-id");
                    fdata = "dwSTID=" + id;
                    TabInJumpReload("stockakingDetail", fdata);
                });
            $(".delLabBtn").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=del&delID=" + dwID);
                }, '提示', 1, function () { });
            });
            $("#btnStockings").click(function () {
                $.lhdialog({
                    title: '新建盘点计划',
                    width: '720px',
                    height: '500px',
                    lock: true,
                    content: 'url:Dlg/NewStocking.aspx?op=new'
                });
            });
            $(".ListTbl").UniTable();

        });
        </script>
    </form>
</asp:Content>
