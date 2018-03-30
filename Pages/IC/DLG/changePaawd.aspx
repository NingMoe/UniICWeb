<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="changePaawd.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle"><%=m_Title %></div>
  <input type="hidden" id="dwAccNo" name="dwAccNo" />
   <div class="formtable">
        <table>
            <tr><th>工号：</th><td><div id="szLogonName"></div></td></tr>
            <tr><th>　　姓名：</th><td><div id="szTrueName"></div></td></tr>
            <tr><th>　密码：</th><td><input type="text" id="passwd" name="passwd" /></td></tr>
            <tr><th>　　重复密码：</th><td><input type="text" id="confirmpasswd" name="confirmpasswd" /></td></tr>
            <tr><td></td><td><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
        </table>
    </div>      
         
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">       
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
    });
</script>
</asp:Content>
