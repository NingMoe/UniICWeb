<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewCreditKind.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_szTitle %></div>
        <div class="formtable">
            <input id="dwLabID" name="dwLabID" type="hidden" />
            <table>
                <tr>
                    <th>��ţ�</th>
                    <td>
                        <input type="text" id="dwCTSN" name="dwCTSN" /></td>
                    <th>���ƣ�</th>
                    <td>
                      <input type="text" id="szCTName" name="szCTName" /></td>
                </tr>
                  <tr>
                    <th>�������ͣ�</th>
                    <td colspan="3">
                        <select id="Select1" name="dwForClsKind">
                            <%=m_szForClsKind %>
                        </select></td>
                </tr>
                <tr>
                    <th>ԤԼ��;��</th>
                    <td><%=m_szResvPurpose %></td>
                    <th>�����������֣�</th>
                    <td> <input type="text" name="dwMaxScore" id="dwMaxScore" /></td>
                </tr>
               <tr>
                    <th>���ڷ�ʽ��</th>
                    <td><select name="dwScoreCycle" id="dwScoreCycle">
                        <option value="1">ÿ��</option>
                        <option value="2">ÿѧ��</option>
                        </select></td>
                    <th>�ͷ�������</th>
                    <td> <input type="text" name="dwForbidUseTime" id="dwForbidUseTime" /></td>
                </tr>
                <!--
                <tr>
                    <td colspan="4">
                        <div>
                        <table border="1">
                           <tr>
                               <th>����һ</th>
                               <th>�����</th>
                               <th>������</th>
                               <th>������</th>
                           </tr>
                            <tr>
                                <td>��ǰɾ��<input type="text" id="dwMinValue1" name="dwMinValue1" style="width:20px" />���ӵ�<input type="text" id="dwMaxValue1" name="dwMaxValue1" style="width:20px" />���ӣ��۳�<input type="text" id="dwCreditScore1" name="dwCreditScore1" style="width:20px" />��</td>
                                   <td>��ǰɾ��<input type="text" id="Text1" name="dwMinValue1" style="width:20px" />���ӵ�</<input type="text" id="Text2" name="dwMaxValue1" style="width:20px" />���ӣ��۳�<input type="text" id="Text3" name="dwCreditScore1" style="width:20px" />��</td>

                                   <td>��ǰɾ��<input type="text" id="Text4" name="dwMinValue1" style="width:20px" />���ӵ�<input type="text" id="Text5" name="dwMaxValue1" style="width:20px" />���ӣ��۳�<input type="text" id="Text6" name="dwCreditScore1" style="width:20px" />��</td>

                                   <td>��ǰɾ��<input type="text" id="Text7" name="dwMinValue1" style="width:20px" />���ӵ�<input type="text" id="Text8" name="dwMaxValue1" style="width:20px" />���ӣ��۳�<input type="text" id="Text9" name="dwCreditScore1" style="width:20px" />��</td>

                            </tr>
                        </table>
                            </div>
                    </td>
                </tr>
                -->
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
