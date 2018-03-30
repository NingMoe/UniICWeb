<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetDevDemage.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <input type="hidden" id="dwSID" name="dwSID" value="1" />
        <div class="formtitle"><%=m_Title %>1243</div>
        <div class="formtable">
            <table>
                <tr>
                    <th><%=ConfigConst.GCDevName %>编号：</th>
                    <td>
                        <div id="dwDevSN" name="dwDevSN" />
                    </td>
                      <th><%=ConfigConst.GCDevName %>名称：</th>
                    <td>
                        <div id="szDevName" name="szDevName" />
                    </td>
                </tr>
                <tr>
                    <th>修复状态：</th>
                    <td>
                        <select id="dwStatus" name="dwStatus"><%=m_szSta %></select></td>
                    <th>修复说明信息：</th>
                    <td>
                        <input id="szRepareInfo" name="szRepareInfo" /></td>
                </tr>
                <!--
                <tr>
                    <th>开放基金卡号：</th>
                    <td>
                        <input id="szFundsNo1" name="szFundsNo1" /></td>
                    <th>开放基金费用(元)：</th>
                    <td>
                        <input id="dwPay1" name="dwPay1" class="validate[custom[onlyFee]]" /></td>
                </tr>
                <tr>
                    <th>自筹基金卡号：</th>
                    <td>
                        <input id="szFundsNo2" name="szFundsNo2" /></td>
                    <th>自筹基金费用(元)：</th>
                    <td>
                        <input id="dwPay2" name="dwPay2" class="validate[custom[onlyFee]]" /></td>
                </tr>
                -->
                <tr>
                    <th>总维修金额(元)：</th>
                    <td>
                        <input id="dwRepareCost" name="dwRepareCost" class="validate[custom[onlyFee]]" /></td>
                </tr>
               
                <tr>
                    <th>维修单位：</th>
                    <td>
                        <input id="szRepareCom" name="szRepareCom" /></td>
               
                    <th>维修单位电话：</th>
                    <td>
                        <input id="szRepareComTel" name="szRepareComTel" /></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:center">
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
    <script language="javascript" type="text/javascript" >
        $(function () {
            
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            $("#dwPay1,#dwPay2").change(function () {
                var dwPay1 = $("#dwPay1").val();
                var dwPay2 = $("#dwPay2").val();
                $("#dwRepareCost").val(toDecimal((parseFloat(dwPay1) + parseFloat(dwPay2)).toString()));
            });
            function toDecimal(x) {
                var f = parseFloat(x);
                if (isNaN(f)) {
                    return;
                }
                f = Math.round(x * 100) / 100;
                return f.toFixed(2);
            }

        });
</script>
</asp:Content>
