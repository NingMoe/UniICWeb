<%@ Page Language="C#" AutoEventWireup="true" CodeFile="groupmember.aspx.cs" Inherits="_Default" %>
<html>
<head>
 <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="viewport" content="height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>table</title>
    <link rel="stylesheet" href="appui/seatStyle.css">
    <script src="appui/Adaptive.js"></script>
</head>
<body>
    <div class="wrap">
    <div class="login-header">
        <img src="appui/images/login-header.png">
        <div class="ic-title">成员列表</div>
    </div>
    <div class="table-content">
        <table cellspacing="0" cellpadding="0">
            <tr>
                <th>姓名</th>
                <th>学号</th>
            </tr>
        <%=szTable %>
        </table>
    </div>
</div>
</body>
</html>