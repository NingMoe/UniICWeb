<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="MemberInGroup.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold">查询人所在组列表</h2>
        <input type="hidden" id="szGetKey" name="szGetKey" />  
       <input type="hidden" id="dwAccNo" name="dwAccNo" /> 
        <div class="toolbar">
           
        </div>
       <div>
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                 <input type="hidden" id="dwDeptID" name="dwDeptID" />  
        <input type="hidden" id="dwRoomID" name="dwRoomID" />  
                    <table style="margin:5px;width:70%">
                        <tr>
                            <th>学号:</th>
                            <td><input type="text" name="szLogonName" id="szLogonName" /></td>
                            <th>结束日期:</th>
                            <td> <select id="dwKind" name="dwkind">
                                <option value="256">预约组</option>
                                <option value="512">使用组</option>
                                <option value="1024">管理员组</option>
                                 </select></td>
                        </tr>
                        <tr>
                            <th colspan="4"><input type="submit" id="btnOK" value="查询" style="height:25px" /></th>
                        </tr>
                    </table>
             
            </div>
       </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                         <th>ID号</th>
                        <th>名称</th>
                         <th>截止日期</th>
                        <th>备足</th>
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
                $(".UniTab").UniTab();
                //$(".ListTbl").UniTable();
                AutoDept($("#szDeptName"), 1, $("#dwDeptID"), false);
                AutoRoom($("#szRoomName"), 1, $("#dwRoomID"), null, null);
                AutoUserByName($("#szName"), 1, $("#dwAccNo"), null, null, null);
                $(".UISelect").UISelect();           
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
              
            });
            $("#devName").autocomplete({
                source: "../data/searchdevice.aspx",
                minLength:0,
                select: function (event, ui) {
                    if (ui.item) {
                        if (ui.item.id && ui.item.id != "") {
                            $("#devName").val(ui.item.label);
                            $("#szGetKey").val(ui.item.id);
                        }
                    }
                    return false;
                },
                response: function (event, ui) {
                    if (ui.content.length == 0) {

                        ui.content.push({ label: " 未找到配置项 " });
                    }
                }
            }).blur(function () {
                if ($(this).val() == "") {
                    $("#szGetKey").val("");
                } else {

                }
            }).click(function () {
                $("#devName").autocomplete("search", "");
            });
            $("#btnOK").button();

        </script>
        <style>
          
            .ui-datepicker select.ui-datepicker-year { width: 43%;}
            .tb_infoInLine td input {
            width:120px;
            }
           
        </style>
    </form>
</asp:Content>
