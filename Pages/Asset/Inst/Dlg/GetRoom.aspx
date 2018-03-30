<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetRoom.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <table>
                <tr>
                    <td>编号：</td>
                    <td>
                       <div id="szRoomNo"></div></td>
                </tr>
                <tr>
                    <td>名称：</td>
                    <td>
                         <div id="szRoomName"></div>
                       </td>
                </tr>
                <tr>
                    <td>所在楼层：</td>
                    <td>
                         <div id="dwFloor"></div>
                       </td>
                </tr>
                <tr>
                    <td>大楼编号：</td>
                    <td>
                       <div id="szBuildingNo"></div></td>
                </tr>
                <tr>
                    <td>大楼名称：</td>
                    <td>
                     <div id="szBuildingName"></div></td>
                </tr>
                <tr>
                    <td>所属<%=ConfigConst.GCLabName %>：</td>
                    <td>
                        <div id="szLabName"></div>
                </tr>
                <tr>
                    <td>控制方式：</td>
                    <td>
                        <%=m_ManMode %>
                       </td>
                </tr>
                <tr>
                    <td>开放规则：</td>
                    <td>
                        <div id="szOpenRuleName"></div>
                       </td>
                </tr>
                <tr>
                    <td>备注：</td>
                    <td>
                         <div id="szMemo"></div></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                      
                        <button type="button" id="Cancel">关闭</button></td>
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
