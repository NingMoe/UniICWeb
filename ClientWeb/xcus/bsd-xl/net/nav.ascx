<%@ Control Language="C#" AutoEventWireup="true" CodeFile="nav.ascx.cs" Inherits="WebUserControl" %>
		<div class="lg_nav">
            <script type="text/javascript">
                $(function () {
                    var str=location.pathname;
                    str=str.toLowerCase();
                    if (str.indexOf("default.aspx") < 0) {
                        $("#nav").show();
                    }
                });
            </script>
			<ul class="logined h_nav_lg ui-tooltip ui-widget ui-widget-content ui-corner-all" style="z-index:1;display:<%=bDisplay1%>;">
                <li class="welcome"><b>您好，<span><%=szTrueName%></span></b></li>
                <li class="logout "><a class="click">退出登录</a></li>
            </ul>
            <ul class="logined h_nav_lg ui-tooltip ui-widget ui-widget-content ui-corner-all" style="z-index:1;display:<%=bDisplay3%>;">
                <li class="welcome"><b>负责人，<span><%=szTrueName%></span></b></li>
                <li class="logout"><a class="click">退出登录</a></li>
            </ul>
        </div>