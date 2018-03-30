<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetRoomKind.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwKindID" name="dwKindID" type="hidden" />     
            <input type="hidden" value="0" id="dwUsableNum" name="dwUsableNum" />       
            <input id="dwClassKind" name="dwClassKind" type="hidden" value="1" /><!--2:����-->
               <%if(ConfigConst.GCKindAndClass==1) {%>                    
            <%} %>
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
                    <%if(ConfigConst.GCKindAndClass==0) {%>
                     <th>����<%=ConfigConst.GCClassName %>��</th>
                    <td colspan="3"><select id="dwClassID" name="dwClassID" ><%=m_dwDevClass %></select></td>
                  <%} else{%>
                    <input type="hidden" id="Hidden1" name="dwClassID" />
                    <%}  %>
                </tr>
                <tr>
                    
                    <th >���ԣ�</th>
                    <td colspan="3"><%=m_KindProperty %></td>
                </tr>
             
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button>                        
                    </td>
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
