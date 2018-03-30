<%@ Control Language="C#" AutoEventWireup="true" CodeFile="nav.ascx.cs" Inherits="WebUserControl" %>
<div id="nav" class="clear">
    <input id="cur_name" type="hidden" value="<%=szTrueName %>" />
    <input id="cur_logonname" type="hidden" value="<%=logonName %>" />
    <input id="cur_accno" type="hidden" value="<%=accNo %>" />
    <input id="cur_phone" type="hidden" value="<%=phone %>" />
    <input id="cur_email" type="hidden" value="<%=email %>" />
    <input id="cur_ident" type="hidden" value="<%=ident %>" />
    <input id="cur_tsta" type="hidden" value="<%=tutorSta %>" />
    <ul class="logined h_nav_lg ui-tooltip ui-widget ui-widget-content ui-corner-all" style="display: <%=bDisplay1%>;">
        <li class="welcome"><b>你好，<span><%=szTrueName%></span></b></li>
        <li class="my">|<a href="UserCenter.aspx">用户中心</a>|</li>
        <li class="logout "><a class="click">退出登录</a></li>
    </ul>
    <ul class="unlogin h_nav_lg" id="islogin" style="display: <%=bDisplay2%>;top: 60px;right:5px;">
        <li><a onclick="isloginL();return false;">用户登录</a></li>
    </ul>
    <ul class="logined h_nav_lg ui-tooltip ui-widget ui-widget-content ui-corner-all" style="display: <%=bDisplay3%>;">
        <li class="welcome"><b>导师，<span><%=szTrueName%></span></b></li>
        <li class="my">|<a href="UserCenter.aspx">用户中心</a>|</li>
        <li class="logout"><a class="click">退出登录</a></li>
    </ul>
</div>
