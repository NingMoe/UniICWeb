<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetUrl.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">  
        <input id="dwID" name="dwID" type="hidden" runat="server" />
        <input id="IsNewCtl" name="bIsNew" type="hidden" runat="server" />
        <table>
            <tr class="trInput">
                <td>��ַ:</td><td><input id="szURL" name="szURL" class="validate[required]" /></td>
            </tr>
             <tr class="trInput">
                <td>��������:</td><td><select id="dwClassSN" name="dwClassSN"><%=m_szClassP %></select></td>
            </tr>
           
             <tr><td>��ע:</td><td><input id="szMemo" name="szMemo" /></td></tr>  
            
        <tr><td></td><td><button type="submit" id="OK">ȷ��</button><button type="button" id="Cancel">ȡ��</button></td></tr>
        </table>
       
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
       
    </style>
    <script type="text/javascript" language="javascript">
        $(function () {
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
        });
        
     
    </script>
</asp:Content>
