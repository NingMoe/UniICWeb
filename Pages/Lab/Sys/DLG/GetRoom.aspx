<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetRoom.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <table class="ListTbl2">
                <tr>
                    <th>编号：</th>
                    <td>
                       <div id="szRoomNo"></div></td>
                    <th>名称：</th>
                    <td>
                         <div id="szRoomName"></div>
                       </td>
                </tr>
                <tr>
                    <th>所在楼层：</th>
                    <td>
                         <div id="dwFloor"></div>
                       </td>
               
                    <th>大楼编号：</th>
                    <td>
                       <div id="szBuildingNo"></div></td>
                </tr>
                <tr>
                    <th>大楼名称：</th>
                    <td>
                     <div id="szBuildingName"></div></td>
              
                    <th>所属<%=ConfigConst.GCLabName %>：</th>
                    <td>
                        <div id="szLabName">
                        </div>
                          </td>
                </tr>
                <tr>
                    <th>控制方式：</th>
                    <td>
                        <%=m_ManMode %>
                       </td>
             
                    <th>开放规则：</th>
                    <td>
                        <div id="szOpenRuleName"></div>
                       </td>
                </tr>
                <tr>
                    <th>备注：</th>
                    <td colspan="3">
                         <div id="szMemo"></div></td>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="button" id="Cancel">关闭</button></td>
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
