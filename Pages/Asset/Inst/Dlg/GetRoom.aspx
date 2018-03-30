<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetRoom.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <table>
                <tr>
                    <td>��ţ�</td>
                    <td>
                       <div id="szRoomNo"></div></td>
                </tr>
                <tr>
                    <td>���ƣ�</td>
                    <td>
                         <div id="szRoomName"></div>
                       </td>
                </tr>
                <tr>
                    <td>����¥�㣺</td>
                    <td>
                         <div id="dwFloor"></div>
                       </td>
                </tr>
                <tr>
                    <td>��¥��ţ�</td>
                    <td>
                       <div id="szBuildingNo"></div></td>
                </tr>
                <tr>
                    <td>��¥���ƣ�</td>
                    <td>
                     <div id="szBuildingName"></div></td>
                </tr>
                <tr>
                    <td>����<%=ConfigConst.GCLabName %>��</td>
                    <td>
                        <div id="szLabName"></div>
                </tr>
                <tr>
                    <td>���Ʒ�ʽ��</td>
                    <td>
                        <%=m_ManMode %>
                       </td>
                </tr>
                <tr>
                    <td>���Ź���</td>
                    <td>
                        <div id="szOpenRuleName"></div>
                       </td>
                </tr>
                <tr>
                    <td>��ע��</td>
                    <td>
                         <div id="szMemo"></div></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                      
                        <button type="button" id="Cancel">�ر�</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .formtitle {
            padding: 6px;
            background: #d0d0d0;
            height: 30px;
            color: #fff;
            font-size: 20px;
        }

        .formtable table {
            text-align: center;
            margin: auto;
        }

        td {
            padding: 6px;
        }

        input, select {
            width: 200px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {       
        $("#Cancel").button().click(Dlg_Cancel);      
        setTimeout(function () { }, 1);
    });
    </script>
</asp:Content>
