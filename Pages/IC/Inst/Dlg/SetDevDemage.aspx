<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetDevDemage.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <input type="hidden" id="dwSID" name="dwSID" value="1" />
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <table>
                <tr>
                    <th><%=ConfigConst.GCDevName %>��ţ�</th>
                    <td>
                        <div id="dwDevSN" name="dwDevSN" />
                    </td>
                      <th><%=ConfigConst.GCDevName %>���ƣ�</th>
                    <td>
                        <div id="szDevName" name="szDevName" />
                    </td>
                </tr>
                <tr>
                    <th>�޸�״̬��</th>
                    <td>
                        <select id="dwStatus" name="dwStatus"><%=m_szSta %></select></td>
                    <th>�޸�˵����Ϣ��</th>
                    <td>
                        <input id="szRepareInfo" name="szRepareInfo" /></td>
                </tr>
                <tr>
                    <th>���Ż��𿨺ţ�</th>
                    <td>
                        <input id="szFundsNo1" name="szFundsNo1" /></td>
                    <th>���Ż������(Ԫ)��</th>
                    <td>
                        <input id="dwPay1" name="dwPay1" class="validate[custom[onlyFee]]" /></td>
                </tr>
                <tr>
                    <th>�Գ���𿨺ţ�</th>
                    <td>
                        <input id="szFundsNo2" name="szFundsNo2" /></td>
                    <th>�Գ�������(Ԫ)��</th>
                    <td>
                        <input id="dwPay2" name="dwPay2" class="validate[custom[onlyFee]]" /></td>
                </tr>
                <tr>
                    <th>��ά�޽��(Ԫ)��</th>
                    <td>
                        <input id="dwRepareCost" name="dwRepareCost" class="validate[custom[onlyFee]]" /></td>
                </tr>
               
                <tr>
                    <th>ά�޵�λ��</th>
                    <td>
                        <input id="szRepareCom" name="szRepareCom" /></td>
               
                    <th>ά�޵�λ�绰��</th>
                    <td>
                        <input id="szRepareComTel" name="szRepareComTel" /></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:center">
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
