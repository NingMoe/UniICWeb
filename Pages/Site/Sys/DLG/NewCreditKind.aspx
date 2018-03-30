<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewCreditKind.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_szTitle %></div>
        <div class="formtable">
            <input id="dwLabID" name="dwLabID" type="hidden" />
            <table>
                <tr>
                    <th>编号：</th>
                    <td>
                        <input type="text" id="dwCTSN" name="dwCTSN" /></td>
                    <th>名称：</th>
                    <td>
                      <input type="text" id="szCTName" name="szCTName" /></td>
                </tr>
                  <tr>
                    <th>适用类型：</th>
                    <td colspan="3">
                        <select id="Select1" name="dwForClsKind">
                            <%=m_szForClsKind %>
                        </select></td>
                </tr>
                <tr>
                    <th>预约用途：</th>
                    <td><%=m_szResvPurpose %></td>
                    <th>周期内最大积分：</th>
                    <td> <input type="text" name="dwMaxScore" id="dwMaxScore" /></td>
                </tr>
               <tr>
                    <th>周期方式：</th>
                    <td><select name="dwScoreCycle" id="dwScoreCycle">
                        <option value="1">每年</option>
                        <option value="2">每学期</option>
                        </select></td>
                    <th>惩罚天数：</th>
                    <td> <input type="text" name="dwForbidUseTime" id="dwForbidUseTime" /></td>
                </tr>
                <!--
                <tr>
                    <td colspan="4">
                        <div>
                        <table border="1">
                           <tr>
                               <th>区间一</th>
                               <th>区间二</th>
                               <th>区间三</th>
                               <th>区间四</th>
                           </tr>
                            <tr>
                                <td>提前删除<input type="text" id="dwMinValue1" name="dwMinValue1" style="width:20px" />分钟到<input type="text" id="dwMaxValue1" name="dwMaxValue1" style="width:20px" />分钟，扣除<input type="text" id="dwCreditScore1" name="dwCreditScore1" style="width:20px" />分</td>
                                   <td>提前删除<input type="text" id="Text1" name="dwMinValue1" style="width:20px" />分钟到</<input type="text" id="Text2" name="dwMaxValue1" style="width:20px" />分钟，扣除<input type="text" id="Text3" name="dwCreditScore1" style="width:20px" />分</td>

                                   <td>提前删除<input type="text" id="Text4" name="dwMinValue1" style="width:20px" />分钟到<input type="text" id="Text5" name="dwMaxValue1" style="width:20px" />分钟，扣除<input type="text" id="Text6" name="dwCreditScore1" style="width:20px" />分</td>

                                   <td>提前删除<input type="text" id="Text7" name="dwMinValue1" style="width:20px" />分钟到<input type="text" id="Text8" name="dwMaxValue1" style="width:20px" />分钟，扣除<input type="text" id="Text9" name="dwCreditScore1" style="width:20px" />分</td>

                            </tr>
                        </table>
                            </div>
                    </td>
                </tr>
                -->
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
