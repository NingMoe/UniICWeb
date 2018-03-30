<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="ClientWeb_xcus_all_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <style>
        /*#carousel_slider {width:500px;height:300px;}
        #carousel_slider img {width:500px;height:300px;}*/
        .intro_box { text-align: center; }
        .intro_box .box { background: #eee; }
        .intro_box .box-gray { background: #f8f8f8; padding: 20px 20px 30px; height: 190px; }
        .intro_box .box-gray h4 { font-weight: 800; }
        .intro_box .box-gray .icon { font-size: 30px; }
        .intro_box .box-bottom { padding: 10px 0; }
        .intro_box .box-bottom a { color: #fff; font-weight: 700; }
        /*预约列表*/
        ul.dyn_resv { margin: 10px; }
        ul.dyn_resv li .prop { border-bottom: 1px dotted #ddd; color: #aaa; }
        /*信息框*/
        .msg_box { background: #fefefe; padding: 5px 10px; border: 1px solid #eee; border-radius: 5px; }
        .msg_img { width: 60px; height: 60px; vertical-align: middle; text-align: center; color: #fff; font-size: 40px; float: left; background: #BFE5F0; margin: 10px; }
        .msg_img span { line-height: 60px; }

        .carousel-inner { height: 280px; }
        #carousel_slider img { height: 280px; width: 100%; }
    </style>
    <h1>WELCOME
    </h1>
    <div class="row">
        <div class="col col_7">
            <div id="carousel_slider" class="carousel slide" data-ride="carousel" data-interval="3000">
                <!-- Indicators -->
                <ol class="carousel-indicators">
                    <li data-target="#carousel_slider" data-slide-to="0" class="active"></li>
                    <li data-target="#carousel_slider" data-slide-to="1"></li>
                    <li data-target="#carousel_slider" data-slide-to="2"></li>
                </ol>
                <!-- Wrapper for slides -->
                <div class="carousel-inner" role="listbox">
                    <div class="item active">
                        <img src="theme/images/main_bg0.jpg" alt="...">
                        <div class="carousel-caption">
                        </div>
                    </div>
                    <div class="item">
                        <img src="theme/images/main_bg1.jpg" alt="...">
                        <div class="carousel-caption">
                        </div>
                    </div>
                    <div class="item">
                        <img src="theme/images/main_bg2.jpg" alt="...">
                        <div class="carousel-caption">
                        </div>
                    </div>
                </div>
                <!-- Controls -->
                <a class="left carousel-control" href="#carousel_slider" role="button" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="right carousel-control" href="#carousel_slider" role="button" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right"></span>
                    <span class="sr-only">Next</span>
                </a>
            </div>
            <div class="text-center">
                <h3>WHAT WE DO</h3>
                <div class="line"></div>
                <p class="grey">
                    实现简单方便的场地无人化管理。场地向学生全天后开放，提供对场地方便快捷的自助式预约服务，网上预约，网上审核，实现对场地的智能化控制。打造网上管理平台实现学生网上预约，网上确认，管理员网上审核的电子化流程管理。
                </p>
            </div>
        </div>
        <div class="col col_5">
            <h3 style="margin-top: 0;">最新通知</h3>
                            <div class="box_notice">
                    <%=infoContent %>
                </div>
            <div class="dyn_info">
                <h3>今日动态</h3>
                <div class="msg_box" style="min-height: 160px;">
                    <ul class="dyn_resv">
                        <%=dynamicInfo %>
                    </ul>
                </div>
            </div>
            <script>
                $(".box_notice .detail").clickLoad();
                pro.autoScroll.regist(".dyn_resv", 6);
            </script>
        </div>
    </div>
    <div style="margin-top: 10px;">
        <div class="col col_4 intro_box">
            <div class="box">
                <div class="box-gray">
                    <h4>资源</h4>
                    <div class="icon grey animated fadeInDown">
                        <span class="glyphicon glyphicon-leaf"></span>
                    </div>
                    <p class="grey">
                        这里有大量设施完善管理有序的资源提供读者使用，配置的计算机、网络、有线电视、投影仪等设备，可满足读者学习交流的需求。
                    </p>
                </div>
                <div class="box-bottom bg_color1">
                    <a>资源</a>
                </div>
            </div>
        </div>
        <div class="col col_4 intro_box">
            <div class="box">
                <div class="box-gray">
                    <h4>共享</h4>
                    <div class="icon grey">
                        <span class="glyphicon glyphicon-cloud"></span>
                    </div>
                    <p class="grey">
                        通过信息化的共享管理方式，所有资源通过网络向全校读者开放，所有人都轻松获取资源与服务，共建一个大家学习交流的圣地。
                    </p>
                </div>
                <div class="box-bottom bg_color1">
                    <a>共享</a>
                </div>
            </div>
        </div>
        <div class="col col_4 intro_box">
            <div class="box">
                <div class="box-gray">
                    <h4>服务</h4>
                    <div class="icon grey">
                        <span class="glyphicon glyphicon-heart"></span>
                    </div>
                    <p class="grey">
                        丰富的资源，便捷的共享方式，力求为广大读者提供更多完善的服务，给读者的学习交流带来方便，是我们最大的目标。
                    </p>
                </div>
                <div class="box-bottom bg_color1">
                    <a>服务</a>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
