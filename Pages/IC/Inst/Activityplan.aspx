<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Activityplan.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>活动管理</h2>
        <div class="toolbar">
                      <div class="FixBtn"><a>新建活动</a></div>
         
        </div>
         <div>
            <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
                <table style="margin: 5px; width: 70%">
                  <tr>
                        <th>活动日期:</th>
                        <td colspan="3">
                            <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>审核状态:</th>
                        <td>
                             <label><input name="dwStatue1" class="enum" type="radio" checked="checked" value="0"> 全部</label>
                            <label><input name="dwStatue1" class="enum" type="radio" value="2"> 审核通过</label>
                            <label><input name="dwStatue1" class="enum" type="radio"value="4"> 不通过</label>
                           </td>
                          <th>开放状态:</th>
                        <td>
                            <label><input name="dwStatue2" class="enum" type="radio" checked="checked" value="0"> 全部</label>
                              <label><input name="dwStatue2" class="enum" type="radio" value="256"> 不开放</label>
                              <label><input name="dwStatue2" class="enum" type="radio" value="512"> 开放</label>
                           </td>
                        <tr>
                        <td colspan="4" style="text-align:center">
                             <input type="submit" id="btnOK" value="查询" style="height: 25px" />
                             <input type="button" id="btnExport" value="导出" style="height: 25px" />
                            </td>
                    </tr>
                    </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szActivityPlanName">活动主题</th>
                        <th>空间名称</th>
                        <th>主讲人</th>
                        <th>联系人</th>
                        <th>电话</th>
                        <th>手机</th>
                        <th>活动人数</th>
                         <th>使用人数</th>
                        <th name="dwBeginTime">活动时间</th>
                        <th>状态</th>
                        <th width="25px"></th>
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
                $("#btnOK,#btnExport,#btnofflinecheck").button();
                
                $("#btnExport").click(function () {
                    var vDate = $("#<%=dwStartDate.ClientID%>").val();
                    var dwStatue1 = $("input[name='dwStatue1']:checked").val();
                    var dwStatue2 = $("input[name='dwStatue2']:checked").val();
                    $.lhdialog({
                        title: '导出',
                        width: '200px',
                        height: '50px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/outPortActitityPlan.aspx?op=set&vDate=' + vDate + '&dwStatue1=' + dwStatue1 + '&dwStatue2=' + dwStatue2
                    });
                });
                $("#<%=dwStartDate.ClientID%>").datepicker();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="checkActivityPlan" href="#" title="审核"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="setDev" href="#" title="修改"><img src="../../../themes/icon_s/15.png"/></a>\
<a class="getSeatMember" href="#" title="查看座位分配情况"><img src="../../../themes/iconpage/mangerlist.png"/></a>\
 <a class="getGroupMember" href="#" title="查看参与人员"><img src="../../../themes/icon_s/16.png"/></a>\
<a class="offlinecheck" href="#" title="离线签到"><img src="../../../themes/icon_s/16.png"/></a>\
                    <a class="delDev" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTD1").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
<a class="getSeatMember" href="#" title="查看座位分配情况"><img src="../../../themes/iconpage/mangerlist.png"/></a>\
 <a class="getGroupMember" href="#" title="查看参与人员"><img src="../../../themes/icon_s/16.png"/></a>\
<a class="offlinecheck" href="#" title="离线签到"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: "5", minWidth: "175", maxWidth: "175", minHeight: "25", maxHeight: "95", speed: 50
                });
                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewActitityPlan.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".checkActivityPlan").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '审核',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/checkActivityPlan.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".getSeatMember").click(function () {
                    var dwid = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '查看座位分配情况',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/getSeatMember.aspx?op=set&id=' + dwid
                    });
                });
                $(".offlinecheck").click(function () {
                    var dwid = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '离线签到',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/offlinecheck.aspx?op=set&planid='+dwid
                    });
                });
                
                $(".getGroupMember").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-groupid");
                    var dwid = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '查看参与人员',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/getactivitymember.aspx?op=set&id=' + dwDevID + '&dwID=' + dwDevID+"&activityid="+dwid
                    });
                });
                $(".delDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID);
                }, '提示', 1, function () { });
            });
                $(".FixBtn").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '660px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewActitityPlan.aspx?op=new'
                    });
                });
            });
            $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: true });
            
        </script>
    </form>
</asp:Content>
