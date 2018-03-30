<%@ Control Language="C#" AutoEventWireup="true" CodeFile="nav.ascx.cs" Inherits="WebUserControl" %>
		<div id="nav" class="clear">
			<ul class="logined clear" style="width:301px;display:<%=bDisplay1%>;">
                <li class="welcome"><a href="my.aspx#space_tab_3" style="text-indent:1px;font-family:黑体; font-weight:bolder; text-decoration:underline">您好：<%=szTrueName%><%=szRes %></a></li>
                <li class="index"><a href="index.aspx">首页</a></li>
                <li class="my"><a href="my.aspx">我的空间</a></li>           
                <li class="logout"><a href="#">退出</a></li>
            </ul>
            <ul class="unlogin clear" style="display:<%=bDisplay2%>;">
                <li class="index"><a href="index.aspx">首页</a></li>
                <li class="login"><a href="#">登录</a></li>
                <li class="active"><a href="#" style="display:<%=GetConfig("mustAct")=="1"?"":"none"%>">激活</a></li>
            </ul>
        </div>
        <script type="text/javascript">
            var obj = document.getElementById("lblCancelResv");
            $(function () {
                var tbl = $("table.list_tbl");
                tbl.each(function () {
                    var con = $(this).find("tbody").html();
                    if ($.trim(con) == "")
                        $(this).hide();
                    else
                        $(this).show();
                });
                    $(".goRes").click(function () {
                        $('#tab_3 a').trigger('click');
                    }).css("cursor", "pointer");
            });
        </script>