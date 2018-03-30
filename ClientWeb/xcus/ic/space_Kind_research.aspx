<%@ Page Language="C#" AutoEventWireup="true" CodeFile="space_Kind.aspx.cs" Inherits="Page_" %>

<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx" %>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx" %>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx" %>
<%@ Register TagPrefix="Uni" TagName="calendar" Src="modules/calendar.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE" />
    <meta name="renderer" content="webkit">
    <link rel="stylesheet" href="style/css/main.css">

    <script type="text/javascript" src="js/jquery.min.js"></script>

    <script type="text/javascript" src="js/jquery-ui-1.8.20.custom.min.js"></script>


    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>fm/uni.lib.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>fm/uni.css" rel='stylesheet' />
<%--        <script src="fm/jquery-ui/sunny/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <link href="fm/jquery-ui/sunny/jquery-ui-1.10.4.custom.min.css" rel='stylesheet' />--%>

        <script src="<%=ResolveClientUrl("~/ClientWeb/") %>pro/pro.lib.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>pro/pro.css" rel='stylesheet' />
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.yellow.css" rel='stylesheet' />

    <script type="text/javascript" src="js/site.js"></script>
</head>
<body>
    <div class="body">
        <Uni:sidebar runat="server" />
        <div class="content">
            <Uni:nav runat="server" />
            <div class="research">

                <%=szKindUrl %>
                <div style="display:<%=cmpHide%>">
                <div class="opt">
                    <a class="btn_order goRes resv_stat" style="display: none">预约空间</a>
                    <a href="#" class="btn_activate reg" style="display: <%=szActDisplay%>;">新用户请先激活</a>
                </div>
                <div id="space_tabs" class="space_tabs tabs3">
                    <ul>
                        <li id="tab_3"><a href="#space_tab_1" class='resv_stat'>空闲状况</a></li>
                        <li id="tab_1"><a href="#space_tab_2">空间相册</a></li>
                        <li id="tab_2"><a href="#space_tab_3">硬件配置</a></li>
                    </ul>
                                        <div id="space_tab_1">
                        <div style="padding-left: 5px;">
                            <table style="width:360px;">
                                <tr>
<td>不在开放时间:</td>
<td style="background-color: #ccc; width: 60px; height: 15px; text-align: center;color:#fff;"></td>

<td>繁忙时段:</td>
                                    <td style="background-color: rgb(13, 139, 213); width: 40px; height: 15px; text-align: center;color:#fff;"></td>
                                    <td><span style="margin-left:20px;">请点击空闲时段预约:</span></td>
                                </tr>
                            </table>
                        </div>
                        <div class="cld">
                            <Uni:calendar runat="server" ID="Cld"/>
                        </div>
                    </div>
                    <div id="space_tab_2">
                        <div class="img_large">
                            <%=szPicZoom %>
                        </div>
                        <div class="img_thumb">
                            <ul class="clear">
                                <%=szPicPath %>
                            </ul>
                        </div>
                    </div>
                    <div id="space_tab_3">
                        <div class="hardware">
                            <ul class="hardware clear">
                                <%=szPicHard %>
                            </ul>
                        </div>
                    </div>
                </div>
                </div>
            </div>
            <div class="copyright">
                版权说明
            </div>
        </div>
    </div>
    <Uni:dialog runat="server" />
</body>
</html>
