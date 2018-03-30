<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetRoom.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <table class="ListTbl2">
                <tr>
                    <th>��ţ�</th>
                    <td>
                       <div id="szRoomNo"></div></td>
                    <th>���ƣ�</th>
                    <td>
                         <div id="szRoomName"></div>
                       </td>
                </tr>
                <tr>
                    <th>����¥�㣺</th>
                    <td>
                         <div id="dwFloor"></div>
                       </td>
               
                    <th>��¥��ţ�</th>
                    <td>
                       <div id="szBuildingNo"></div></td>
                </tr>
                <tr>
                    <th>��¥���ƣ�</th>
                    <td>
                     <div id="szBuildingName"></div></td>
              
                    <th>����<%=ConfigConst.GCLabName %>��</th>
                    <td>
                        <div id="szLabName">
                        </div>
                          </td>
                </tr>
                <tr>
                    <th>���Ʒ�ʽ��</th>
                    <td>
                        <%=m_ManMode %>
                       </td>
             
                    <th>���Ź���</th>
                    <td>
                        <div id="szOpenRuleName"></div>
                       </td>
                </tr>
                <tr>
                    <th>��ע��</th>
                    <td colspan="3">
                         <div id="szMemo"></div></td>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="button" id="Cancel">�ر�</button></td>
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
        $("#Cancel").button().click(Dlg_Cancel);      
        setTimeout(function () { }, 1);
    });
    </script>
</asp:Content>
