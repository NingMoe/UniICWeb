<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewCode.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwKindID" name="dwKindID" type="hidden" />
            <table>

                <tr>
                    <th>��ţ�</th>
                    <td>
                        <input id="szCodeSN" name="szCodeSN" class="validate[required]" /></td>
                    <th>���ƣ�</th>
                    <td>
                        <input id="szCodeName" name="szCodeName"  class="validate[required]"/></td>
                </tr>   
                 <tr>
                    <th>��ע��</th>
                    <td colspan="3">
                        <input id="szMemo" name="szMemo" /></td>
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

    });
    </script>
</asp:Content>
