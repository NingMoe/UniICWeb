<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewSTDetail.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
      <input type="hidden" id="dwKindID" name="dwKindID" />
        <input type="hidden" id="dwRoomID" name="dwRoomID" />
        <div class="formtable">
            <table>
                <tr>
                    <th>�ʲ��ţ�</th>
                    <td>
                        <input id="szAssertSN" name="szAssertSN" class="validate[required]" /></td>
                    <th>�ʲ����ƣ�</th>
                    <td>
                        <input id="szDevName" name="szDevName" class="validate[required]" /></td>
                </tr>
              <tr>
                    <th>����(Ԫ)��</th>
                    <td>
                        <input id="dwUnitPrice" name="dwUnitPrice"  /></td>
                    <th>�������ڣ�</th>
                    <td>
                        <input id="dwPurchaseDate" name="dwPurchaseDate" /></td>
                </tr>
                <tr>
                    <th>�ʲ����ͣ�</th>
                    <td>
                        <input id="szKindName" name="szKindName" /></td>
                     <th>���ԣ�</th>
                    <td>
                        <input id="Text1" name="szKindName" /></td>
                </tr>
                <tr>
                    <th>�ͺţ�</th>
                    <td>
                        <input id="szModel" name="szModel" /></td>
                    <th>���</th>
                    <td>
                        <input id="szSpecification" name="szSpecification"  /></td>
                </tr>
                <tr>
                    <th>���ڷ���</th>
                     <td colspan="3">
                        <input id="szRoomName" name="szRoomName" /></td>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">���</button>
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
            AutoDevKind($("#szKindName"), 2, $("dwKindID"), null, false);
            AutoRoom($("#szRoomName"),2,$("#dwRoomID"),null,null);
            $("#dwPurchaseDate").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</asp:Content>
