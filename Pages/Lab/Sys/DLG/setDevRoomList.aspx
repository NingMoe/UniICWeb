<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="setDevRoomList.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle">�����޸�<%=ConfigConst.GCRoomName %></div>
        <div class="formtable">
              <input type="hidden" name="devRoomID" id="devRoomID" />
            <table>
                <tr>
                    <th><%=ConfigConst.GCRoomName %>���ƣ�</th>
                    <td>
                        <input type="text" name="devRoomName" id="devRoomName" />
                    </td>
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
            AutoRoom($("#devRoomName"), 2, $("#devRoomID"), null, null);
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        AutoDept($("#szDeptName"), 2, $("#dwDeptID"), false);
     

        setTimeout(function () {
            
        }, 1);
    });
    </script>
</asp:Content>
