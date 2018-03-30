<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WxSeatSignMsg.aspx.cs" Inherits="_Default" %>
<html>
<head>
    <meta http-equiv=Content-Type content="text/html;charset=utf-8">
    <title></title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0 , user-scalable=no"/>
	<meta name="apple-mobile-web-app-capable" content="yes" />
    <link rel="stylesheet" href="seat/seatStyle.css">
    <script src="seat/Adaptive.js"></script>
	<script language="javascript" type="text/javascript">
	javascript:window.history.forward(1);
	document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
		WeixinJSBridge.call('hideOptionMenu');
		WeixinJSBridge.call('hideToolbar');
		});
	</script>
</head>
<body>
<%if(m_nType == 2){%>
<div class="wrap">
    <div class="login-header">
        <img src="seat/images/login-header.png">
    </div>
    <form action='WxSeatSign.aspx' method="post">
        <div class="login-content">
            <input type="hidden" name="DoLogon" value="true"/>
            <input type="hidden" name="sysidform" value="<%=sysid %>"/>
            <input type="hidden" name="aluseridform" value="<%=aluserid %>" />
            <input type="hidden" name="wxuseridform" value="<%=wxuserid %>" />
            <div class="account">
                <i class="icon account-icon"></i>
                <input type="text" name="szLogonName">
            </div>
            <div class="password">
                <i class="icon password-icon"></i>
                <input type="password" name="szPassword">
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
<%}else if(m_nType == 3){%>
<div class="wrap">
    <div class="header">
        <p>【 操作成功 】</p>
        <img src="seat/images/choose-time.png">
    </div>
    <form action='WxSeatSign.aspx' method="post">
        <div class="text">
            <p><%=m_szMsg %></p>
            <input type="hidden" name="DoUserIn" value="true"/>
            <p>
                <label>预计使用时长：</label>
                <select name="dwUseMin"><%=m_szTimes %></select>
            </p>
        </div>
        <div class="btn-group">
            <button type="submit">确定</button>
        </div>
    </form>
    <div class="background back-result"></div>
</div>
<%}else if(m_nType == 4 || m_nType == 8){%>
<div class="wrap ">
    <div class="header">
        <p>【 操作成功 】</p>
        <img src="seat/images/left-time.png">
    </div>
    <div class="info">
        <p><%=m_szMsg %></p>
    </div>
    <div class="btn-group" style="margin-bottom: 0;">
        <button onclick="location.href='WxSeatSign.aspx?DoUserOut=1';">暂时离开</button>
    </div>
<%if(m_nType == 8){ %>
<table width="100%" style="border: 1px solid #aaa;padding: 3px; border-top: 3px solid #6da;">
<tr><td style="padding: 3px;border-bottom: 1px solid #ddd; text-align:center; font-weight: bold; font-size: 26px; background:<%=m_szColorBg%>; color:<%=m_szColor%>;"><%=m_szTitle %>7</td></tr>
<tr><td style="padding: 10px; padding-top: 20px; padding-bottom: 20px;font-size: 18px;"><%=m_szMsg %>9<p style="font-size: 16px; color: #999;"><%=m_szMsg2 %>8</p></td></tr>
<p style="text-align:center;"><button style="font-size: 24px;" onclick="location.href='WxSeatSignMsg.aspx?type=16&title=<%=Request["title"] %>&msg=<%=Request["msg"] %>&msg2=<%=Request["msg2"] %>&dwMinUseMin=<%=Request["dwMinUseMin"] %>&dwMaxUseMin=<%=Request["dwMaxUseMin"] %>&szTrueName=<%=Request["szTrueName"] %>&szDevName=<%=Request["szDevName"] %>';">续约</button></p>
<%} %>
<div class="btn-group" style="margin-bottom: 0;">
        <button onclick="location.href='WxSeatSign.aspx?DoUserOut=2';">结束使用</button>
</div>
<div class="background back-result"></div>
</div>
<%}else if(m_nType == 16){%>
<table width="100%" style="border: 1px solid #aaa;padding: 3px; border-top: 3px solid #6da;">
<tr><td style="padding: 3px;border-bottom: 1px solid #ddd; text-align:center; font-weight: bold; font-size: 26px; background:<%=m_szColorBg%>; color:<%=m_szColor%>;"><%=m_szTitle %>7</td></tr>
<tr><td style="padding: 10px; padding-top: 20px; padding-bottom: 20px;font-size: 18px;"><%=m_szMsg %>9<p style="font-size: 16px; color: #999;"><%=m_szMsg2 %>8</p></td></tr>
<tr><td style="padding: 6px; font-size: 18px;">
<form action='WxSeatSign.aspx' method="post">
<input type="hidden" name="DoUserDelay" value="true"/>
<p><span>续约时长：</span><select style="font-size:18px;width: 120px; height: 60px;" name="dwDelayMin"><%=m_szTimes %></select></p>
<p style="text-align:center;"><button type="submit" style="font-size: 24px;">确定续约</button></p>
</form>
</td></tr>
</table>
<%} %>
<%else if(m_nType == 32){%>
<div class="wrap">
    <div class="header">
        <p>【 操作成功 】</p>
        <img src="seat/images/choose-time.png">
    </div>
    <form action='WxSeatSign.aspx' method="post">
        <div class="text">
            <p><%=m_szMsg %></p>
        </div>
    </form>
    <div class="background back-result"></div>
</div>
<%}else {%>
<div class="wrap">
    <div class="header">
        <p><%=m_szTitle %></p>
        <img src="seat/images/operate.png">
    </div>
    <div class="info">
        <p><%=m_szMsg %></p>
    </div>
    <%if (status==1){ %>
           <div class="btn-group">
            <button onclick="location.href='WxSeatSign.aspx?Advance=true'">预约提前</button>
           </div>
     <%} %>
    
    <div class="background back-result"></div>
</div>
<%} %>
</body>
</html>
