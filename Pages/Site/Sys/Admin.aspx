<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>管理员</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnResvRule">新建管理员</a></div>
            <div class="tb_btn">
            </div>
        </div>
         <div  class="tb_infoInLine"  style="margin:5px 0px">
                 <input type="hidden" id="dwAccno" name="dwAccno" />
            <table style="width:99%;margin-top:10px">
               <tr>
                   <th>管理员学号/工号:</th>
                   <td>
                       <input type="text" name="szLogonName" id="szLogonName" />
                        </td>
              <th>姓名:</th>
                   <td>
                       <input type="text" name="szTrueName" id="szTrueName" />
                        </td>
                     <th>部门名称:</th>
                   <td>
                       <input type="text" name="szDeptName" id="szDeptName" />
                        </td>

                  <th><input type="submit" id="btn" value="查询" /></th>
               </tr>
           </table>
               </div>

        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szLogonName">学工号</th>
                        <th name="szTrueName">姓名</th>
                        <th>管理员类型</th>
                        <th>管理员级别</th>
                        <th>所在部门</th>
                        <th>联系方式</th>
                        <th>备注</th>
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
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                        <a href="#" class="adminCopy" title="管理员复制"><img src="../../../themes/icon_s/13.png"/></a>\
                        <a href="#" class="admingetroom" title="查看管理房间"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    $.lhdialog({
                        title: '新建管理员',
                        width: '750px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewAdmin.aspx?op=new'
                    });
                });
                
            $(".adminCopy").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '复制管理员',
                        width: '790px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/admincopy.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".setResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '修改管理员',
                        width: '790px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetAdmin.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".admingetroom").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '查看管理房间',
                        width: '790px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/getadminroom.aspx?op=set&dwID=' + dwID
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
