<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewCheckType.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwDeptID" name="dwDeptID" type="hidden" />
              <input id="dwCheckKind" name="dwCheckKind" type="hidden" />
            <table>
                <tr>
                    <th>名称</th>
                    <td colspan="3"><input type="text" id="szCheckName" name="szCheckName" /></td>
                </tr>
                <tr>
                    <th>审核类型：</th>
                    <td>
                        <select id="dwMainKind" name="dwMainKind"><%=m_szCheckTypeMainKind %></select></td>
                    <th>审核<%=ConfigConst.GCDeptName %>：</th>
                    <td>
                        <input type="text" id="szDeptName" name="szDeptName" disabled="disabled" /></td>
                </tr>
                         
                <tr>
                    <td class="btnRow" colspan="4">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
        <%if (bSet)
          {%>
       
            <%}%>
            AutoDept($("#szDeptName"),2,$("#dwDeptID"),false);
            $("#OK").button();
            $("#dwMainKind").change(function () {
                var value = $(this).val();
                if (value == "256") {
                    $("#dwDeptID").val("0");
                    $("#szDeptName").val("");
                    $("#szDeptName").attr("disabled", "disabled");
                }
                else {
                    $("#szDeptName").removeAttr("disabled");
                    if (value == "2048") {
                        $("#szDeptName").val("不设置默认为归属部门");
                    }
                    else {
                        $("#szDeptName").val("");
                    }
                }
                
            });
            setTimeout(function () {
                var level = $("#dwMainKind").val();
                if (level == "256") {
                    $("#dwDeptID").val("0");
                    $("#szDeptName").val("");
                    $("#szDeptName").attr("disabled", "disabled");
                }
                else {
                    $("#szDeptName").removeAttr("disabled");
                    if (value == "2048") {
                        $("#szDeptName").val("不设置默认为归属部门");
                    }
                    else {
                        $("#szDeptName").val("");
                    }
                }
            }, 100);
        $("#Cancel").button().click(Dlg_Cancel);
        
    });
    </script>
</asp:Content>
