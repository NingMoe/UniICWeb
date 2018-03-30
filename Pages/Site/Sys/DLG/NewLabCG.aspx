<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewLabCG.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwLabID" name="dwLabID" type="hidden" />
            <input id="dwDeptID" name="dwDeptID" type="hidden" />
            <input id="dwClass" name="dwClass" type="hidden" />
            <table>
                <tr>
                    <th>��ţ�</th>
                    <td>
                        <input id="szLabSN" name="szLabSN" class="validate[required]" /></td>
                  
                    <th>����<%=ConfigConst.GCDeptName %>��</th>
                    <td>
                        <input type="text" id="szDeptName" name="szDeptName" /></td>
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
