<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewStocking.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
      <input type="hidden" id="dwKindID" name="dwKindID" />
        <input type="hidden" id="dwRoomID" name="dwRoomID" />
        <input type="hidden" id="dwLeaderID" name="dwLeaderID" />
        <div class="formtable">
            <table>
                  <tr>
                    <th style="width:120px">�̵�ƻ�������</th>
                    <td><input id="szMemo" name="szMemo" /></td>
                     <th>�̵����Ա��</th>
                    <td><input id="szLeaderName" name="szLeaderName" /></td>
                </tr>

                <tr>
                    <th>�̵㷿�䣺</th>
                    <td>
                        <input id="szRoomName" name="szRoomName" /></td>
                    <th>�̵����ͣ�</th>
                    <td>
                        <input id="szKindName" name="szKindName"/></td>
                </tr>
                 <tr>
                    <th>�̵���͵��ۣ�</th>
                    <td>
                        <input id="dwMinUnitPrice" name="dwMinUnitPrice" /></td>
                    <th>�̵���ߵ��ۣ�</th>
                    <td>
                        <input id="dwMaxUnitPrice" name="dwMaxUnitPrice" /></td>
                </tr>
                <tr>
                   
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">�ƶ��̵�ƻ�</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
                </tr>
            </table>
        </div>

    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .formtable table th {
        width:80px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            AutoUserByName($("#szLeaderName"), 2, $("#dwLeaderID"), null, null, null);
            AutoDevKind($("#szKindName"), 2, $("dwKindID"), null, false);
            AutoRoom($("#szRoomName"),2,$("#dwRoomID"),null,null);
            $("#dwPurchaseDate").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</asp:Content>
