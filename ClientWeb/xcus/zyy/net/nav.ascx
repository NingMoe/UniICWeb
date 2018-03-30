<%@ Control Language="C#" AutoEventWireup="true" CodeFile="nav.ascx.cs" Inherits="WebUserControl" %>
		<div id="nav" class="clear hidden">
            <script type="text/javascript">
                $(function () {
                    var str=location.pathname;
                    str=str.toLowerCase();
                    if (str.indexOf("default.aspx") < 0) {
                        $("#nav").show();
                    }
                });
            </script>
			<ul class="logined h_nav_lg ui-tooltip ui-widget ui-widget-content ui-corner-all" style="display:<%=bDisplay1%>;">
                <li class="welcome"><b>你好，<span><%=szTrueName%></span></b></li>
                <li class="my"><a href="UserCenter.aspx">个人中心</a></li>
                <li class="logout "><a class="click">退出登录</a></li>
            </ul>
            <ul class="unlogin h_nav_lg" id="islogin" style="display:<%=bDisplay2%>;top:84px;">
                <li><a class="button" onclick="pro.isloginL();return false;">用户登录</a></li>
            </ul>
            <ul class="logined h_nav_lg ui-tooltip ui-widget ui-widget-content ui-corner-all" style="display:<%=bDisplay3%>;">
                <li class="welcome"><b>负责人，<span><%=szTrueName%></span></b></li>
                <li class="my"><a href="UserCenter.aspx">个人中心</a></li>
                <li class="my"><a href="Course.aspx">项目中心</a></li>
                <li class="logout"><a class="click">退出登录</a></li>
            </ul>
        </div>