<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewCreditScoreRule.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwLabID" name="dwLabID" type="hidden" />
            <table>
                <tr>
                    <th>启动区段数：</th>
                    <td colspan="3"> 
                        <select name="dwUseNum" id="dwUseNum">
                        <option value="1">一段</option>
                        <option value="2">二段</option>
                        <option value="3">三段</option>
                        <option value="4">四段</option>
                             </select></td>
                </tr>
               <tr>
                    <th>段一</th>
                    <td colspan="3">
                        <input type="text" id="dwMinValue1" name="dwMinValue1" style="width:50px" />分钟到
                        <input type="text" id="dwMaxValue1" name="dwMaxValue1" style="width:50px" />分钟，<%=szop %>
                        <input type="text" id="dwCreditScore1" name="dwCreditScore1" style="width:50px" />分
                    </td>
                </tr>
                 <tr>
                    <th>段二</th>
                    <td colspan="3">
                        <input type="text" id="dwMinValue2" name="dwMinValue2" style="width:50px" />分钟到
                        <input type="text" id="dwMaxValue2" name="dwMaxValue2" style="width:50px" />分钟，<%=szop %>
                        <input type="text" id="dwCreditScore2" name="dwCreditScore2" style="width:50px" />分
                    </td>
                </tr>
                 <tr>
                    <th>段三</th>
                    <td colspan="3">
                        <input type="text" id="dwMinValue3" name="dwMinValue3" style="width:50px" />分钟到
                        <input type="text" id="dwMaxValue3" name="dwMaxValue3" style="width:50px" />分钟，<%=szop %>
                        <input type="text" id="dwCreditScore3" name="dwCreditScore3" style="width:50px" />分
                    </td>
                </tr>
                 <tr>
                    <th>段四</th>
                    <td colspan="3">
                        <input type="text" id="dwMinValue4" name="dwMinValue4" style="width:50px" />分钟到
                        <input type="text" id="dwMaxValue4" name="dwMaxValue4" style="width:50px" />分钟，<%=szop %>
                        <input type="text" id="dwCreditScore4" name="dwCreditScore4" style="width:50px" />分
                    </td>
                </tr>
             
                <tr>
                    <td class="btnRow" colspan="4">
                        <button type="submit" id="OK">确定</button>
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
            $("#OK,#Cancel").button();
            $("#Cancel").click(Dlg_Cancel);
    });
    </script>
</asp:Content>
