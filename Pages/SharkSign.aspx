<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SharkSign.aspx.cs" Inherits="Pages_SharkSign" %>

<html>
<head runat="server">
    <meta http-equiv=Content-Type content="text/html;charset=utf-8">
    <title></title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0 , user-scalable=no"/>
	<meta name="apple-mobile-web-app-capable" content="yes" />
    <link rel="stylesheet" href="seat/seatStyle.css">
    <script src="seat/Adaptive.js"></script>
</head>
<body>
   <%if(m_nType == 2){%>
<div class="wrap">
    <div class="login-header">
        <img src="seat/images/login-header.png">
    </div>
    <form action='SharkSign.aspx' method="post">
        <div class="login-content">
            <input type="hidden" name="DoLogon" value="true"/>
            <div class="account">
                <i class="icon account-icon"></i>
                <input type="text" name="szLogonName">
            </div>
            <div class="password">
                <i class="icon password-icon"></i>
                <input type="password" name="szPassword">
            </div>
             <div class="text">
            <p><%=m_szMsg %></p>
        </div>
            <div class="bind">
                <input type="checkbox" id="dwBind" name="dwBind" value="1">
                <label for="dwBind">绑定此账号</label>
            </div>
            <div class="btn-wrap">
                <button class="btn" type="submit">确定</button>
            </div>
        </div>
    </form>
    <div class="background back-login"></div>
</div>
<%}else if(m_nType == 4){%>
<div class="wrap">
    <div class="header">
        <p>【 操作成功 】</p>
        <img src="seat/images/left-time.png">
    </div>
    <form action='SharkSign.aspx' method="post">
        <div class="text">
            <p><%=m_szMsg %></p>
            <input type="hidden" name="DoUserIn" value="true"/>
             <input type="hidden" name="DoUserIn" value="true"/>
           <%-- <p>
                <label>预计使用时长：</label>
                <select name="dwUseMin"><%=m_szTimes %></select>
            </p>--%>
        </div>
        <div class="btn-group">
          <%--  <button type="submit">确定</button>--%>
        </div>
    </form>
     <div class="btn-group" style="margin-bottom: 0;">
        <button onclick="location.href='SharkSign.aspx?DoUserOut=1';">暂时离开</button>
        <button onclick="location.href='SharkSign.aspx?DoUserOut=2';">结束使用</button>
    </div>
</div>
   <%} else if(m_nType == 8||m_nType == 16){%>
<div class="wrap">
    <div class="header">
        <p><%=m_szTitle %></p>
        <img src="seat/images/operate.png">
    </div>
    <div class="info">
        <p><%=m_szMsg %></p>
    </div>
           <div class="btn-group">
            <button onclick='location.href="SharkSign.aspx?status=true"' runat="server" id="checkin">点击签到</button>
            <button  onclick="location.href='SharkSign.aspx?status=false'" runat="server" id="comein">点击入馆</button>
           </div>
    <div class="background back-result"></div>
</div>
<%}else {%>
<div class="wrap">
    <div class="header">
       <%-- <img src="seat/images/operate.png">--%>
    </div>
    <div class="info">
        <p><%=m_szMsg %></p>
    </div>
    <div class="background back-result"></div>
   
</div>
<%} %>
</body>
</html>
