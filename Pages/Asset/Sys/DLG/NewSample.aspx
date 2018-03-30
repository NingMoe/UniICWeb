<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewSample.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <table class="ListTbl">
                <tr>
                    <th>��Ʒ���ƣ�</th>
                    <th>�Ʒѵ�λ��</th>
                    <th>У��(0.01)Ԫ��</th>
                    <th>У��(0.01)Ԫ��</th>
                    </tr>
                <tr>
                    <td>
                        <input type="text" id="szSampleName" name="szSampleName" /></td>
                    <td>
                        <select id="szUnitName" name="szUnitName">
                            <option value="ÿ��">ÿ��</option>
                        </select></td>
                    <td>
                        <input type="text" id="dwUnitFee2" name="dwUnitFee2" value="0" class="validate[required,validate[custom[onlyNumber]]" /></td>
                    
                    <td>
                        <input type="text" id="dwUnitFee3" name="dwUnitFee3" value="0" class="validate[required,validate[custom[onlyNumber]]" /></td>

                </tr>
                <tr>
                    <td colspan="5" class="btnRow">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
                </tr>
            </table>
        </div>

    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
       .formtable input, select, .input {
width: 80px;
}
        .formtable table th {
        text-align:center;
        }
        
        .formtable table td {
        text-align:center;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
        });
    </script>
</asp:Content>
