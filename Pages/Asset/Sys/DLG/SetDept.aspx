<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetDept.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwID" name="dwID" type="hidden" />
          
            <table>
                <tr>
                     <th>���ƣ�</th>
                    <td>
                        <input id="szName" name="szName" class="validate[required]" /></td>
                  <th>��д��</th>
                    <td>
                        <input id="szMemo" name="szMemo" /></td>
                   
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
   
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        AutoDept($("#szDeptName"), 2, $("#dwDeptID"), false);
     

        setTimeout(function () {
            
        }, 1);
    });
    </script>
</asp:Content>
