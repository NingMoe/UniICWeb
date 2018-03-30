<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewSerCheckType.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwDeptID" name="dwDeptID" type="hidden" />
              <input id="dwCheckKind" name="dwCheckKind" type="hidden" />
            <input id="dwMainKind" name="dwMainKind" type="hidden" value="4096" />
            <input id="dwCheckLevel" name="dwCheckLevel" type="hidden" value="10" />
            <table>
                <tr>
                    <th>名称：</th>
                    <td>
                        <input id="szCheckName" name="szCheckName" class="validate[required]" /></td>
                      <th>服务部门：</th>
                    <td>
                      <input type="text" id="szDeptName" name="szDeptName" /></td>
                </tr>
                <tr>
                    <th>备注</th>
                    <td><select id="szMemo" name="szMemo"><%=m_szCheckYardTypeKind %></select></td>
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
                if (value != "4096") {
                    $("#dwDeptID").val("0");
                    $("#szDeptName").attr("disabled", "disabled");
                }
                else {
                    $("#szDeptName").removeAttr("disabled");
                }
                
            });
            setTimeout(function () {
                var level = $("#dwMainKind").val();
                if (level != "4096") {
                    $("#dwDeptID").val("0");
                    $("#szDeptName").attr("disabled", "disabled");
                }
                else {
                    $("#szDeptName").removeAttr("disabled");
                }
            }, 100);
        $("#Cancel").button().click(Dlg_Cancel);
        
    });
    </script>
</asp:Content>
