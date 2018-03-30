<%@ Page Language="C#" AutoEventWireup="true" CodeFile="accnoTip.aspx.cs" Inherits="searchAccount" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
</head>
<body>
    <form id="form1" runat="server">
   <table style="border-collapse:separate; border-spacing:6px;">
       <tr><td>学号:</td><td><label id="lblLogonName" runat="server"></label></td></tr>
       <tr><td>姓名:</td><td><label id="lblTrueName" runat="server"></label></td></tr>
       <tr><td><%=ConfigConst.GCDeptName %>:</td><td><label id="lblDeptName" runat="server"></label></td></tr>
       <tr><td>手机:</td><td><label id="lblHandPhone" runat="server"></label></td></tr>
       <tr><td>电话:</td><td><label id="lblTel" runat="server"></label></td></tr>
       <tr><td>邮箱:</td><td><label id="lblMail" runat="server"></label></td></tr>
   </table>
    </form>
</body>
</html>
