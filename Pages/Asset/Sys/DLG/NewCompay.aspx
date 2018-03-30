<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewCompay.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwComID" name="dwComID" type="hidden" />
            <table>
                <tr>
                    <th>名称：</th>
                    <td>
                        <input id="szComName" name="szComName" class="validate[required]" /></td>
                    <th>类型</th>
                    <td>
                        <select id="dwComKind" name="dwComKind"><%=m_sKind %></select>
                       </td>
                </tr>
              <tr>
                    <th>联系人姓名:</th>
                  <td><input type="text" id="szTrueName" name="szTrueName" /></td>
                  <th>职务:</th>
                  <td><input type="text" id="szJobTitle" name="szJobTitle" /></td>
                  
              </tr>
                <tr>
                    <th>手机:</th>
                  <td><input type="text" id="szHandPhone" name="szHandPhone" /></td>
                  <th>电话:</th>
                  <td><input type="text" id="szTel" name="szTel" /></td>
                  
              </tr>    
                 <tr>
                    <th>邮箱:</th>
                  <td colspan="3"><input type="text" id="szEmail" name="szEmail" /></td>
                  
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
        $("#Cancel").button().click(Dlg_Cancel);
          setTimeout(function () {
           
        }, 1);
    });
    </script>
</asp:Content>
