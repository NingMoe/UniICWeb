<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetTerm.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle"><%=m_Title %></div>
  
    <div class="formtable">
        <br />
        <br />
        <table>
            <input type="hidden" id="dwYearTerm" name="dwYearTerm"/>
            <tr><th style="vertical-align:top;">ѧ�ڱ�ţ�</th><td style="text-align:left;"><div id="dwYearTermCode"></div></td><th>���������ƣ�</th><td><input id="szMemo" name="szMemo" class="validate[required]" /></td></tr>        
           <tr><th>��ʼ���ڣ�</th><td><input type="text" id="dwBeginDate" name="dwBeginDate" class="validate[required]" readonly="readonly"  /></td><th>�������ڣ�</th><td><input type="text" id="dwEndDate" name="dwEndDate" class="validate[required]" readonly="readonly"  /></td></tr>
            <tr><td colspan="4" class="tblBtn"><button type="submit" id="OK">ȷ��</button><button type="button" id="Cancel">ȡ��</button></td></tr>
        </table>
    </div>    
         
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .formtable table{
        }
    </style>
<script language="javascript" type="text/javascript" >
   $(function () {                  
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        setTimeout(function () {
         
        }, 1);     
        $("#dwBeginDate").datepicker();
        $("#dwEndDate").datepicker();
     
    });
</script>
</asp:Content>
