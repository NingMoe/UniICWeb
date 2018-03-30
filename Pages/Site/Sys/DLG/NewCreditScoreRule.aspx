<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewCreditScoreRule.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwLabID" name="dwLabID" type="hidden" />
            <table>
                <tr>
                    <th>������������</th>
                    <td colspan="3"> 
                        <select name="dwUseNum" id="dwUseNum">
                        <option value="1">һ��</option>
                        <option value="2">����</option>
                        <option value="3">����</option>
                        <option value="4">�Ķ�</option>
                             </select></td>
                </tr>
               <tr>
                    <th>��һ</th>
                    <td colspan="3">
                        <input type="text" id="dwMinValue1" name="dwMinValue1" style="width:50px" />���ӵ�
                        <input type="text" id="dwMaxValue1" name="dwMaxValue1" style="width:50px" />���ӣ�<%=szop %>
                        <input type="text" id="dwCreditScore1" name="dwCreditScore1" style="width:50px" />��
                    </td>
                </tr>
                 <tr>
                    <th>�ζ�</th>
                    <td colspan="3">
                        <input type="text" id="dwMinValue2" name="dwMinValue2" style="width:50px" />���ӵ�
                        <input type="text" id="dwMaxValue2" name="dwMaxValue2" style="width:50px" />���ӣ�<%=szop %>
                        <input type="text" id="dwCreditScore2" name="dwCreditScore2" style="width:50px" />��
                    </td>
                </tr>
                 <tr>
                    <th>����</th>
                    <td colspan="3">
                        <input type="text" id="dwMinValue3" name="dwMinValue3" style="width:50px" />���ӵ�
                        <input type="text" id="dwMaxValue3" name="dwMaxValue3" style="width:50px" />���ӣ�<%=szop %>
                        <input type="text" id="dwCreditScore3" name="dwCreditScore3" style="width:50px" />��
                    </td>
                </tr>
                 <tr>
                    <th>����</th>
                    <td colspan="3">
                        <input type="text" id="dwMinValue4" name="dwMinValue4" style="width:50px" />���ӵ�
                        <input type="text" id="dwMaxValue4" name="dwMaxValue4" style="width:50px" />���ӣ�<%=szop %>
                        <input type="text" id="dwCreditScore4" name="dwCreditScore4" style="width:50px" />��
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
            $("#OK,#Cancel").button();
            $("#Cancel").click(Dlg_Cancel);
    });
    </script>
</asp:Content>
