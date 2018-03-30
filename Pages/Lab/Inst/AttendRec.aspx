<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="AttendRec.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>考勤明细</h2>
       <div class="toolbar">
            <div class="tb_info"></div>
            <!--<div class="FixBtn"><a id="newRule">新建考勤规则</a></div>-->
             <div class="tb_info">
             <div class="UniTab" id="tabl">
                 <a href="attendrule.aspx" id="attendrule">考勤规则</a>                
                <a href="attendTotal.aspx" id="attendTotal">考勤统计</a>
                 <a href="attendRec.aspx" id="attendRec">考勤明细</a>  
                </div> 
    </div>
        </div>
           <input type="hidden" name="dwAccNo" id="dwAccNo" />
         <div style="margin-top:15px;width:99%;">
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">                          
               <tr>
                   <th>考勤规则：</th>
                   <td><select id="attendid" name="attendid"><%=szAttendRule %></select></td>
                  <th>学工号：</th>
                   <td><input  type="text" name="truename" id="truename" /></td>
               </tr>
                        <tr>
                             <th>开始日期</th>
                   <td>
                    <input type="text" name="dwStartDate" id="dwStartDate" runat="server" />   
                   </td>
                   <th>结束日期</th>
                   <td><input type="text" name="dwEndDate" id="dwEndDate" runat="server"  /></td>
                        </tr>
                <tr>
                    <td colspan="4" style="text-align:center"><input type="submit" id="sub" value="查询">
                           <input type="submit" id="import" value="导出">
                    </td>
                </tr>
            </table>
                </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                  <tr>
                        <th>学工号</th>
                        <th>姓名</th>
                         <th>考勤规则</th>
                        <th>考勤日期</th>
                        <th>考勤房间</th>
                        <th>进入时间</th>
                        <th>退出时间</th>
                        <th>最近一次进入时间</th>
                        <th>停留时间(分钟)</th>
                        <th>刷卡次数</th>
                        <th>状态</th>
                      
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
                $("#sub").button();
                $("#import").button().click(function () {
                    var vFrom = $("#formAdvOpts").serialize();
                    $.lhdialog({
                        title: '导出',
                        width: '300px',
                        height: '100px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/AttendRecOutExport.aspx?op=set&attendid=' + $("#attendid").val() + "&dwAccNo=" + $("#dwAccNo").val() + "&dwStartDate=" + $("#<%=dwStartDate.ClientID%>").val() + "&dwEndDate=" + $("#<%=dwEndDate.ClientID%>").val()
                    });
                });
                AutoUserByLogonname($("#truename"), 1, $("#dwAccNo"), null, null, null);
                $("input,select").css("width", "150");
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="set" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="del" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                    <a class="setRuleGroup" title="设置考勤人员名单"><img src="../../../themes/icon_s/11.png"/></a>\
                    </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
              
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: "5", minWidth: "175", maxWidth: "175", minHeight: "25", maxHeight: "95", speed: 50
                });
                $(".UniTab").UniTab();
                $(".set").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newAttendRule.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".setRuleGroup").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '设置考勤人员名单',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setgroupmember.aspx?op=set&dwID=' + dwDevID
                    });
                });
               
                $(".del").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID);
                }, '提示', 1, function () { });
            });
                $("#newRule").click(function () {
                    $.lhdialog({
                        title: '新建考勤规则',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newAttendRule.aspx?op=new'
                    });
                });
            });
            $(".ListTbl").UniTable({ HeaderIndex: false });
        </script>
    </form>
</asp:Content>
