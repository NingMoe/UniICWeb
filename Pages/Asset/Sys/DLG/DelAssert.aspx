<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="DelAssert.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
         <div class="formtitle"><%=m_Title %></div>
      <input type="hidden" id="dwKindID" name="dwKindID" />
        <input type="hidden" id="dwDevID" name="dwDevID" />
        <input type="hidden" id="dwRoomID" name="dwRoomID" />
        <input type="hidden" id="dwLabID" name="dwLabID" />
        <div class="formtable">
            <table>
                <tr>
                    <th>资产号：</th>
                    <td>
                        <div id="szAssertSN" /></td>
                    <th>资产名称：</th>
                    <td>
                        <div id="szDevName" /></td>
                </tr>
              <tr>
                    <th>单价(元)：</th>
                    <td>
                        <div id="dwUnitPrice"  /></td>
                    <th>购置日期：</th>
                    <td>
                        <div id="dwPurchaseDate"  /></td>
                </tr>
                <tr>
                    <th>资产类型：</th>
                    <td>
                        <div id="szKindName"  /></td>
                     <th>属性：</th>
                         <td><%=m_szPorperty%></td>
                </tr>
                <tr>
                    <th>型号：</th>
                    <td>
                        <div id="szModel"  /></td>
                  <th>所在房间：</th>
                     <td colspan="3">
                        <div id="szRoomName" /></td>
                </tr>
                <tr>
                    
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">删除</button>
                        <button type="button" id="Cancel">取消</button></td>
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
    });
    </script>
</asp:Content>
