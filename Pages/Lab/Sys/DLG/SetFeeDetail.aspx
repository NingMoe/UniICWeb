<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetFeeDetail.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
    <table>            
            <tr><th>�շ����</th><td colspan="3"><div id="feeType" runat="server"></div></td></tr>
            <tr><th>ÿСʱ/����(��,0.01Ԫ)��</th><td><input id="dwUnitFee" name="dwUnitFee" class="validate[required,validate[custom[onlyNumber]]"" title="100��ʾ1Ԫ"   /></td><th>�Ʒ�ʱ��(����)��</th><td><input id="dwUnitTime" name="dwUnitTime" class="validate[required,validate[custom[onlyNumber]]" title="����ʱ����һ�η�"  /></td></tr>
            <tr><th>�շѷ�ʽ��</th><td><%=szUsablePayKind %></td><th>�˵��Ƿ����:</th><td><select id="dwDefaultCheckStat" name="dwDefaultCheckStat"><%=m_checkInfo %></select></td></tr>
            
            <tr><td colspan="4" class="btnRow"><button type="submit" id="OK">�޸�</button><button type="button" id="Cancel">�ر�</button></td></tr>
        </table>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
       input{
            width:40px;
        }
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {      
        <%if(nIsAdminSup==1){ %>
        <%}%>
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        
        setTimeout(function () {}, 1);
    });
</script>
</asp:Content>
