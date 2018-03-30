<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="AttendRule.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>考勤规则</h2>
       <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="newRule">新建考勤规则</a></div>
             <div class="tb_info">
             <div class="UniTab" id="tabl">
               <a href="attendrule.aspx" id="attendrule">考勤规则</a>                
                <a href="attendTotal.aspx" id="attendTotal">考勤统计</a>
                 <a href="attendRec.aspx" id="attendRec">考勤明细</a>
                </div> 
    </div>
        </div>
          <div style="margin-top:15px;width:99%;">
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">                          
               <tr>
                   <th>名称：</th>
                   <td colspan="3"><input type="text" name="szAttendName" id="szAttendName" /></td>
                  
               </tr>
                <tr>
                    <td colspan="4" style="text-align:center"><input type="submit" id="sub" value="查询"></td>
                </tr>
            </table>
                </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>名称</th>
                        <th>开始日期</th>
                         <th>结束日期</th>
                        <th>进入时间</th>
                        <th>退出时间</th>
                        <th>最少停留时间（分钟）</th>
                        <th>考勤房间</th>
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
                $("#sub").button();
                $(".OPTD").html('<div class="OPTDBtn">\
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
                $(".getAttendRuleDetail").click(function () {
                   // debugger;
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    var dwStartDate = $(this).parents("tr").children().first().attr("data-startdate");
                    var dwenddate = $(this).parents("tr").children().first().attr("data-enddate");
                    fdata = "attendid=" + dwDevID + '&dwStartDate=' + dwStartDate + "&dwEndDate=" + dwenddate;
                    TabInJumpReload("l", fdata);
                });
                $(".setRuleGroup").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '设置考勤人员名单',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setusegroup.aspx?op=set&id=' + dwDevID
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
