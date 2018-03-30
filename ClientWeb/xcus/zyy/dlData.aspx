<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dlData.aspx.cs" Inherits="DevWeb_dlData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;">
<%
	Response.Write(m_szErrMsg);
%>
<button onclick="window.close();">返回</button>
</div>
    </form>
</body>
</html>
