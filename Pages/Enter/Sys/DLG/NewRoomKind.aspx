<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewRoomKind.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwKindID" name="dwKindID" type="hidden" />
            <input id="dwClassKind" name="dwClassKind" type="hidden" value="1" /><!--2:����-->
            <input type="hidden" id="dwUsableNum" name="dwUsableNum" value="0" />
            <table>
                <tr>
                    <th>���ƣ�</th>
                    <td colspan="3"><input id="szKindName" name="szKindName" class="validate[required]" /></td>
                   
                </tr>
                <tr>
                    <th>����ʹ��������</th>
                    <td><input type="text" id="dwMinUsers" name="dwMinUsers" /></td>
                    <th>���ʹ��������</th>
                    <td><input type="text" id="dwMaxUsers" name="dwMaxUsers" /></td>
                </tr>
                <tr>                  
                    <th>���ԣ�</th>
                    <td colspan="1"><%=m_KindProperty %></td>
                     <th></th>
                    <td>   <LABEL><INPUT class="enum" value="1" type="checkbox" name="isOpen" /> �����⿪��</LABEL></td>
               
                </tr>
                <tr>
                    <%if(ConfigConst.GCKindAndClass==0) {%>
                     <th>����<%=ConfigConst.GCClassName %>��</th>
                    <td colspan="3"><select id="dwClassID" name="dwClassID" ><%=m_dwDevClass %></select></td>
                  <%} %>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
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
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);

            setTimeout(function () {
            }, 1);
        });
    </script>
</asp:Content>
