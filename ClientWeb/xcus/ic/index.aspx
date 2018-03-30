<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Page_" %>

<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx" %>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx" %>
<%@ Register TagPrefix="Uni" TagName="include" Src="modules/include.ascx" %>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <title><%=GetConfig("SysAutoSchoolName") %></title>
    <meta content="" name="keywords" />
    <meta content="" name="description" />
    <link rel="stylesheet" href="style/css/main.css">
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.20.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.carousel.min.js"></script>
    <script type="text/javascript" src="js/jquery.easing.js"></script>
    <script type="text/javascript" src="js/site.js"></script>
    <script type="text/javascript" src="js/divCycle.js"></script>
    <script type="text/javascript" language="javascript" src="js/datepicker/WdatePicker.js"></script>
    <Uni:include ID="Include1" runat="server" />
    <script type="text/javascript">
        //
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
            //清华
            var req = new request();
            var list = $("#space li");
            list.each(function () {
                var id = $(this).attr("class_id");
                if (req["site"] == "yxxj" && id == "10333")//逸夫馆
                    $("span", this).trigger("click");
                else if (req["site"] == "cab" && id == "10309")
                    $("span", this).trigger("click");
            });
            ///      
        });
        $(function () {
            var top = $("#top");
            var bottom = $("#bottom");
            var obj = $(".carousel-wrap ul");
            var w = obj.find("li").innerWidth();

            top.click(function () {
                obj.find("li:last").prependTo(obj);
                obj.css("margin-top", -w);
                obj.animate({ "margin-top": 0 });
            });

            bottom.click(function () {
                obj.animate({ "margin-top": -w }, function () {
                    obj.find("li:first").appendTo(obj);
                    obj.css("margin-top", "0");
                });
            });

        });
        $(function () {
            var img = $("#gallerymid");
            var obj = $("#gallerymid ul");
            var w = obj.find("li").innerWidth();
            img.click(function () {
                obj.find("li:last").prependTo(obj);
                obj.css("margin-left", -w);
                obj.animate({ "margin-left": 0 });
            });
            var moving = setInterval(function () { img.click() }, 5000);

            obj.hover(function () {
                clearInterval(moving);
            }, function () {
                moving = setInterval(function () { img.click() }, 5000);
            })
        });
        $(function () {
            var speed = 1000;
            var i = 1;
            var line = 5;
            var idValue = "#saloninfoprodiv";
            var _this = $(idValue).eq(0).find("ul:first");
            var lineH = $(idValue).find("li:first").height(); //获取行高     
            line = line ? parseInt(line, 10) : parseInt($(idValue).height() / lineH, 10); //每次滚动的行数，默认为一屏，即父容器高度
            speed = speed ? parseInt(speed, 10) : 600; //卷动速度，数值越大，速度越慢（毫秒） 
            var m = line;  //用于计算的变量
            var count = $(idValue).find("li").length; //总共的<li>元素的个数
            var upHeight = line * lineH;
            function newclick() {
                if (i <= (count / line)) {
                    _this.animate({ marginTop: "-=" + upHeight + "px" }, speed);
                    i = i + 1;
                }
                else {
                    _this.animate({ marginTop: "+=" + (count - count % line) * lineH + "px" }, speed);
                    i = 1;
                }

            }
            var moving = setInterval(newclick, 10000);
        });
        $(function () {
            var speed = 1000;
            var i = 1;
            var line = 5;
            var idValue = "#newsdiv";
            var _this = $(idValue).eq(0).find("ul:first");
            var lineH = $(idValue).find("li:first").height(); //获取行高     
            line = line ? parseInt(line, 10) : parseInt($(idValue).height() / lineH, 10); //每次滚动的行数，默认为一屏，即父容器高度
            speed = speed ? parseInt(speed, 10) : 600; //卷动速度，数值越大，速度越慢（毫秒） 
            var m = line;  //用于计算的变量
            var count = $(idValue).find("li").length; //总共的<li>元素的个数
            var upHeight = line * lineH;
            function newclick() {
                if (i <= (count / line)) {
                    _this.animate({ marginTop: "-=" + upHeight + "px" }, speed);
                    i = i + 1;
                }
                else {
                    _this.animate({ marginTop: "+=" + (count - count % line) * lineH + "px" }, speed);
                    i = 1;
                }

            }
            var moving = setInterval(newclick, 8000);
        });

    </script>

    <style type="text/css">
        ul, li { list-style-type: none; margin: 0; padding: 0; }
        #saloninfoprodiv { width: 300px; height: 125px; min-height: 25px; line-height: 25px; overflow: hidden; }
        #saloninfoprodiv li { height: 25px; padding-left: 10px; }
        #newsdiv { width: 300px; height: 125px; min-height: 25px; line-height: 25px; overflow: hidden; }
        #newsdiv li { height: 25px; padding-left: 10px; }

        * { padding: 0; margin: 0; }
        #newsReacherdiv { width: 400px; height: 400px; margin: 10px auto; position: relative; overflow: hidden; }
        #newsReacherdiv ul { position: absolute; top: 0; }
        #newsReacherdiv li { height: 22px; line-height: 22px; }
        #newsReacherdiv li a { color: #333; text-decoration: none; font-size: 14px; }
        #newsReacherdiv a:hover { text-decoration: underline; color: #000; }

        .dyn_info ul { padding-left: 10px; }
        #divNotice h3 { font-weight: 700; font-size: 18px; font-family: 'Microsoft YaHei'; }
        #divNotice li { padding-left: 0; }
        .dyn_info h5 { font-weight: 700; }
        .dyn_info .tab_head .active .title { color: #428bca; font-weight: bold; }
        .dyn_info .tab_con .item { min-height: 200px; padding: 5px; border: dotted 1px#ccc; background: #f3f3f3; }
        .dyn_info .tab_con .item .con { text-indent: 2em; }
    </style>
</head>

<body id="index">
    <div class="body">
        <Uni:sidebar ID="Sidebar1" runat="server" />
        <div class="content">
            <Uni:nav ID="Nav1" runat="server" />
            <div class="index_col_mid">
                <div id="intro">
                    <img src="style/img/bg_intro.jpg" width="390" height="126" /></div>
                <div id="gallerymid" style="overflow: hidden; width: 390px; height: 270px;">
                    <ul>
                        <li>
                            <img src="style/img/gallerymid1.jpg" width="390" height="270" alt="" />
                        </li>
                        <li>
                            <img src="style/img/gallerymid2.jpg" width="390" height="270" alt="" />
                        </li>
                    </ul>
                </div>
                <div id="intro_mid">
                    <%--                <a href="space_info.aspx#overview_tabs_1" class="intro_mid_1">了解更多</a>
                <a href="space_info.aspx#overview_tabs_2"  class="intro_mid_2">服务内容</a>
                <a href="space_info.aspx#overview_tabs_3" class="intro_mid_3">空间须知</a>
                <a href="space_Resv.aspx" class="intro_mid_4"></a>--%>
                </div>
            </div>
            <div class="index_col_right">
                <div style="text-align: center;">
                    <!--<span style="width:50px;position:absolute;"><a href="#" class="btn_order">预约空间</a></span>-->
                    <a href="space_Resv.aspx" style="float: right; margin-right: 12px; width: 100px; height: 29px; text-indent: -1000px; overflow: hidden; background: url(style/img/btn_order.jpg);">空间预约</a>
                </div>
                <br />

                <div id="spacegallery">
                    <h2>空间分区</h2>
                    <div class="gallery" tabindex="0">
                        <input type="button" value="Previous" class="carousel-control previous carousel-previous hover disabled" id="top">
                        <div class="carousel-wrap" style="overflow-x: hidden; overflow-y: hidden; position: relative; height: 172px;">
                            <ul style="position: absolute; width: 270px; height: 1376px; top: 0px;">
                                <li style="height: 172px;">
                                    <a class="img">
                                        <img src="style/img/spacegallery1.jpg" width="270" height="130" alt=""></a>
                                    <a class="title">
                                        <img src="style/img/spacegallerytitle1.jpg" width="240" height="22"></a>
                                </li>
                                <li style="height: 172px;">
                                    <a class="img">
                                        <img src="style/img/spacegallery2.jpg" width="270" height="130" alt=""></a>
                                    <a class="title">
                                        <img src="style/img/spacegallerytitle2.jpg" width="240" height="22"></a>
                                </li>
                            </ul>
                        </div>
                        <input type="button" value="Next" class="carousel-control next carousel-next" id="bottom">
                    </div>
                </div>

                <div id="divNotice">
                    <h3 style="margin-top: 10px; margin-bottom: 10px;">最新通知</h3>
                    <div class="unitab dyn_info">
                        <ul>
                            <%=dynamicInfo %>
                        </ul>
                        <div style="margin-top: 20px;">
                            <%=infoContent %>
                        </div>
                    </div>
                    <script>
                        $(".unitab").unitab();
                        $(".unitab .item").each(function () {
                            var p = $(".con", this);
                            p.html(p.text());
                        });
                    </script>
                </div>
                <div class="copyright">版权说明</div>
            </div>
        </div>
    </div>
    <Uni:dialog ID="Dialog1" runat="server" />
    <!-- 登陆激活弹窗，全站模块 -->

</body>
</html>

