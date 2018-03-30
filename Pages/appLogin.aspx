<%@ Page Language="C#" AutoEventWireup="true" CodeFile="appLogin.aspx.cs" Inherits="_Default" %>
<html>
<head>
 <meta charset="UTF-8">
  <title>登录</title>
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <meta name="format-detection" content="telephone=no">
  <meta name="renderer" content="webkit">
  <meta http-equiv="Cache-Control" content="no-siteapp" />
 
    <link rel="stylesheet" href="appui/seatStyle.css">
    <script src="appui/Adaptive.js"></script>
</head>
<body>
      <form method="post">
        <input type="hidden" id="op" name="op" />
    <div class="wrap">
    <div class="login-header">
        <img src="appui/images/login-header.png">
        <div class="ic-title">IC信息共享空间</div>
    </div>
    <div class="login-content">
        <div class="account">
            <i class="icon account-icon"></i>
            <input type="text" name="logonname">
        </div>
        <div class="password">
            <i class="icon password-icon"></i>
            <input type="password" name="password">
        </div>
        <div class="btn-wrap">
            <input class="btn" type="submit" value="登录"/>
        </div>
    </div>
    <div class="background back-login"></div>
</div>
          </form>
</body>
</html>