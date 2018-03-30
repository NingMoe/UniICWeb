<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WxOpenDoorMsg.aspx.cs" Inherits="_Default" %>
<html>
<head>
    <meta http-equiv=Content-Type content="text/html;charset=utf-8">
    <title></title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0 , user-scalable=no"/>
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<script language="javascript" type="text/javascript">
	javascript:window.history.forward(1);
	document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
		WeixinJSBridge.call('hideOptionMenu');
		WeixinJSBridge.call('hideToolbar');
		});
	</script>
</head>
<body>
<table width="100%" style="border: 1px solid #aaa;padding: 3px; border-top: 3px solid #6da;">
<tr><td style="padding: 3px;border-bottom: 1px solid #ddd; text-align:center; font-weight: bold; font-size: 26px; background:<%=m_szColorBg%>; color:<%=m_szColor%>;"><%=m_szTitle %></td></tr>
<tr><td style="padding: 10px; padding-top: 20px; padding-bottom: 20px;font-size: 18px;"><%=m_szMsg %><p style="font-size: 16px; color: #999;"><%=m_szMsg2 %></p></td></tr>
<%if(m_nType == 2){%>
<tr><td style="padding: 6px; font-size: 18px;">
<form action='WxOpenDoor.aspx' method="post">
<input type="hidden" name="DoLogon" value="true"/>
<p><span>账号：</span><input style="font-size:18px;" name="szLogonName"/></p>
<p><span>密码：</span><input style="font-size:18px;" type="password" name="szPassword"/></p>
<p><label for="dwBind">绑定账号：</label><input type="checkbox" style="font-size: 20px;" id="dwBind" name="dwBind" value="1"/></p>
<p style="text-align:center;"><button type="submit" style="font-size: 24px;">确定</button></p>
</form>
</td></tr>
<%}%>
</table>
</body>
</html>
