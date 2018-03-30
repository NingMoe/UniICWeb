<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shareredirt.aspx.cs" Inherits="_Default" %>
<html>
<head>
    <meta http-equiv=Content-Type content="text/html;charset=utf-8">
    <title></title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0 , user-scalable=no"/>
	<meta name="apple-mobile-web-app-capable" content="yes" />
   
</head>
<body>
      <form method="post" runat="server">
        <input type="hidden" id="op" name="op" />
    <div class="wrap">
    <div class="login-header">
        <img src="appui/images/login-header.png">
        <div class="ic-title">IC信息共享空间</div>
    </div>
    <div class="login-content">
        <div class="account">
            <i class="icon account-icon"></i>
            <input type="text" name="logonname" id="logonname">
        </div>
        <div class="password">
            <i class="icon password-icon"></i>
            <input type="password" name="password" id="password">
        </div>
        <div class="btn-wrap">
            <input class="btn" type="submit" id="btnLogin" value="登录"/>
        </div>
    </div>
    <div class="background back-login"></div>
</div>
          </form>
</body>
</html>