<%@ Page Language="C#" AutoEventWireup="true" CodeFile="space_info.aspx.cs" Inherits="Page_" %>

<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx" %>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx" %>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <title>空间概览</title>
    <meta content="" name="keywords" />
    <meta content="" name="description" />
    <link rel="stylesheet" href="style/css/main.css">
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.20.custom.min.js"></script>
    <script type="text/javascript" src="js/site.js"></script>
</head>
<body>
    <script>
        function request() {
            var name, value, i;
            var str = location.href;
            var num = str.indexOf("?")
            str = str.substr(num + 1);
            str = str.split("#")[0];
            var arrtmp = str.split("&");
            for (i = 0; i < arrtmp.length; i++) {
                num = arrtmp[i].indexOf("=");
                if (num > 0) {
                    name = arrtmp[i].substring(0, num);
                    value = arrtmp[i].substr(num + 1);
                    this[name] = value;
                }
            }
        }
        $(function () {
            var req = new request();
            var tab = req["tab"];
            $("#" + tab + " span").trigger("click");
        });
    </script>
    <div class="body">
        <Uni:sidebar runat="server" />
        <div class="content clear">
            <Uni:nav runat="server" />
            <div id="overview" class="overview">
                <h1>空间概览</h1>
                <div>
                    <%=ov_intro %>
                </div>
                <div class="opt">
                    <!--<a href="reservation_step.php?id=0" class="btn_order">预约空间</a><a href="" class="btn_activate">新用户请先激活</a>-->
                </div>
                <div id="space_tabs2" class="space_tabs">
                    <ul>

                        <li id="tab_1"><a href="#overview_tabs_1">空间详情</a></li>
                        <li id="tab_2"><a href="#overview_tabs_2">服务内容</a></li>
                        <li id="tab_3"><a href="#overview_tabs_3"><span>使用帮助</span></a></li>
                        <li id="tab_4"><a href="#overview_tabs_4"><span>联系我们</span></a></li>

                    </ul>
                    <div id="overview_tabs_1">
                        <%=ov_detail %>
                    </div>
                    <div id="overview_tabs_2">
                        <%=ov_help %>
                    </div>
                    <div id="overview_tabs_3">
                        <%=ov_rule %>
                    </div>
                    <div id="overview_tabs_4">
                        <%=ov_contact %>
                    </div>
                </div>
            </div>
            <div class="copyright">版权说明</div>
        </div>
    </div>
    <Uni:dialog runat="server" />
</body>
</html>

