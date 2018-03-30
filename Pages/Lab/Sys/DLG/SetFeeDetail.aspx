<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetFeeDetail.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
    <table>            
            <tr><th>收费类别：</th><td colspan="3"><div id="feeType" runat="server"></div></td></tr>
            <tr><th>每小时/费率(分,0.01元)：</th><td><input id="dwUnitFee" name="dwUnitFee" class="validate[required,validate[custom[onlyNumber]]"" title="100表示1元"   /></td><th>计费时间(分钟)：</th><td><input id="dwUnitTime" name="dwUnitTime" class="validate[required,validate[custom[onlyNumber]]" title="多少时间收一次费"  /></td></tr>
            <tr><th>收费方式：</th><td><%=szUsablePayKind %></td><th>账单是否审核:</th><td><select id="dwDefaultCheckStat" name="dwDefaultCheckStat"><%=m_checkInfo %></select></td></tr>
            
            <tr><td colspan="4" class="btnRow"><button type="submit" id="OK">修改</button><button type="button" id="Cancel">关闭</button></td></tr>
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
