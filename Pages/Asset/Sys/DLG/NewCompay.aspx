<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewCompay.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwComID" name="dwComID" type="hidden" />
            <table>
                <tr>
                    <th>���ƣ�</th>
                    <td>
                        <input id="szComName" name="szComName" class="validate[required]" /></td>
                    <th>����</th>
                    <td>
                        <select id="dwComKind" name="dwComKind"><%=m_sKind %></select>
                       </td>
                </tr>
              <tr>
                    <th>��ϵ������:</th>
                  <td><input type="text" id="szTrueName" name="szTrueName" /></td>
                  <th>ְ��:</th>
                  <td><input type="text" id="szJobTitle" name="szJobTitle" /></td>
                  
              </tr>
                <tr>
                    <th>�ֻ�:</th>
                  <td><input type="text" id="szHandPhone" name="szHandPhone" /></td>
                  <th>�绰:</th>
                  <td><input type="text" id="szTel" name="szTel" /></td>
                  
              </tr>    
                 <tr>
                    <th>����:</th>
                  <td colspan="3"><input type="text" id="szEmail" name="szEmail" /></td>
                  
              </tr>                
                <tr>
                    <td class="btnRow" colspan="4">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
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
