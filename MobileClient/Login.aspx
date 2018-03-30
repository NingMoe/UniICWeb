<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Default" %>
<html>
<head>
    <meta http-equiv=Content-Type content="text/html;charset=utf-8">
    <title></title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<link rel="stylesheet" type="text/css" media="screen" href="css.aspx" />
    <script type="text/javascript" src="zepto.min.js"></script>
    <script type="text/javascript" src="js.aspx"></script>
</head>
<body>
<form action="#" method="post" enctype="application/x-www-form-urlencoded">
<input name="FID" type="hidden" value="<%=szFormID %>" />
<div id="headDiv">
    <div class="THead">
        <span>预约平台</span>
    </div>
</div>

<div class="Div" id="regDiv">
    <div class="Head">
        <span>登录</span>
    </div>
    <div class="Content">
        <table class="tblLoginContent">
            <tr><td class="label">登录名：</td><td><input name="szLogonName" value=""/></td></tr>
            <tr><td class="label">密码：</td><td><input type="password" name="szPassword" value=""/></td></tr>
            <tr><td colspan="2" style="text-align:center;padding:6px;"><button type="submit">登录</button></td></tr>
        </table>
    </div>
</div>

<div class="msg">
<%=szMsg %>
</div>

</form>

</body>
</html>
