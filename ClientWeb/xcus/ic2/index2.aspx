<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="ClientWeb_xcus_all_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <style>
        .intro_box { text-align: center;padding: 0 10px; }
        .intro_box .box { background: #eee; }
        .intro_box .box-gray { background: #f8f8f8; padding: 10px 20px 30px; height: 130px; }
        .intro_box .box-gray h4 { font-weight: 800; }
        .intro_box .box-gray .icon { font-size: 30px; }
        .intro_box .box-bottom { padding: 10px 0; }
        .intro_box .box-bottom span { color: #fff; font-weight: 700; }
        /*预约列表*/
        ul.dyn_resv { margin: 10px; }
        ul.dyn_resv li .prop { border-bottom: 1px dotted #ddd; color: #aaa; }

        .main_boxes .m-box { margin:2px;width:180px;height:120px;position:relative;float:left;overflow:hidden;}
        .main_boxes .m-box:first-child { height: 240px;width:360px;margin:2px 4px;}
        .main_boxes .m-box:first-child img {height:100%;width:100%;}
        .main_boxes .m-box img {width:180px;height:120px;}
        .main_boxes .m-box ul {position:absolute;bottom:0px;left:0;margin:0 2px;width:100%;margin:0;padding:0;min-height:20px;background:rgba(0,0,0,0.4);}
        .main_boxes .m-box ul li {line-height:16px;color:#fff;font-size:12px;padding:2px;}

        #carousel_slider { margin:0 auto; width: 480px;}
        .carousel-inner { height: 270px; }
        #carousel_slider img { height: 270px; width: 480px; }
    </style>
            <script>
                $(function () {
                    RetSelecteds();
                });
                function RetSelecteds() {
                    var result = {};
                    result.clskind = "<%=GetConfig("openClsKind")%>";
                    result.sortHot = "true";
                //分页
                result.pctrlId = "pCtrl";
                result.pctrlStar = "1";//筛选需重置分页
                result.pctrlNeed = "<%=GetConfig("mIndexMode")=="2"?"5":"21"%>";
                //显示模式
                SubmitRet(result);
            }
            //提交搜索
            function SubmitRet(condition) {
                pro.j.dev.devFilter(condition, function (con) {
                    var mode = <%=GetConfig("mIndexMode")%>;
                    var content = '';
                    rlt = con.data;
                    var devs = rlt.devs;
                    $(devs).each(function (i) {
                        var pthis = $(this);
                        var id = pthis.attr('id');
                        var name = pthis.attr('name');
                        var url = pthis.attr('url');
                        var campus = pthis.attr('campus');
                        var cls = pthis.attr('devcls');
                        var col = pthis.attr('dept');
                        var lab = pthis.attr('lab');
                        var price = pthis.attr("price");
                        var manager = pthis.attr("manager");
                        var phone = pthis.attr("phone") || '';
                        var intro = pthis.attr('intro');
                        var devstat = pthis.attr('devstat');
                        var runstat = pthis.attr('runstat');
                        //if(uni.isInArray(cls,$(".dft_close_devcls").val().split(',')))return true;
                        if(mode==2){
                            //content+="<div class='item "+(i==0?'active':'')+"'><a  url=\"../a/devdetail.aspx?back=true\" data='{dev:" + id + "}' cache='#cache_con' con='#detail_con' class=\"click_load\" title=\"<%=Translate("点击查看详情：")%>" + name + "\"><img alt='" + name + "' src='" + url + "' /></a><div class='carousel-caption'>"+uni.cutStrT(name, 20)+"</div></div>";
                        }
                        else {
                            content += "<div class='m-box'><div class='box_h'><a  url=\"../a/devdetail.aspx?back=true\" data='{dev:" + id + "}' cache='#cache_con' con='#detail_con' class=\"click_load\" title=\"<%=Translate("点击查看详情：")%>" + name + "\"><img alt='" + name + "' src='" + url + "' /></a>" +
                            "<ul><li class='name'>" + uni.cutStrT(name, 20) + "</li></ul></div></div>";
                        }
                    });
                    //if(mode==2)
                    //    $(".main_slide").html(content);
                    //else 
                        $(".main_boxes").html(content);
                    $(".devs_queue .click_load").clickLoad(function (p) {
                        uni.backTop();
                    });
                });
            }
        </script>
    <h1 class="h_title">Book a Room</h1>
    <div class="line"></div>
    <div class="row">
        <div class="col col_8 devs_queue">
            <div class="<%=GetConfig("mIndexMode")=="2"?"":"hidden" %>">
                        <div id="carousel_slider" class="carousel slide" data-ride="carousel" data-interval="3000">
                <!-- Wrapper for slides -->
                <div class="carousel-inner main_slide" role="listbox">
                    <%=clsSlide %>
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
                    这是一种新型的空间，一种新型的学习模式，一种共享的交流社区。配置计算机、网络、有线电视、投影仪等设施及常用的专业软件，打通了通信载体、内容分类、物理空间
                    的界限，提供资源的一站式服务，读者在享受最新的阅读体验的同时可以对空间进行多信息的交互和共享。
                </p>
            </div>
                <div style="margin-top: 30px;">
        <div class="col col_4 intro_box">
            <div class="box">
                <div class="box-gray">
                    <h4>资源</h4>
                    <div class="icon grey animated fadeInDown">
                        <span class="glyphicon glyphicon-leaf"></span>
                    </div>
                    <p class="grey">
                        大量设施完善管理有序的资源
                    </p>
                </div>
                <div class="box-bottom bg_color1">
                    <span>资源</span>
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
                        信息化的共享管理方式
                    </p>
                </div>
                <div class="box-bottom bg_color1">
                    <span>共享</span>
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
                        为广大读者提供更多优质的服务
                    </p>
                </div>
                <div class="box-bottom bg_color1">
                    <span>服务</span>
                </div>
            </div>
        </div>
    </div>
            </div>
            <div class="main_boxes"></div>
        </div>
                <div class="col col_4 pull-right" style="border-left:1px solid #ddd;min-height:500px;">
                    <div class="<%=GetConfig("mNotice")=="1"?"":"hidden" %>">
                    <h3 class="h_title" style="margin-top: 0;"><%=Translate("最新通知")%></h3>
                                    <div class="box_notice" style="min-height:100px;margin-bottom:10px;">
                    <%=infoContent %>
                </div>
                    </div>
                    <div class="<%=GetConfig("mResvDynamic")=="1"?"":"hidden" %>">
            <h3 class="h_title" style="margin-top: 0;"><%=Translate("今日预约")%></h3>
            <div class="dyn_info">
                <div  style="border-top:#ddd dotted 1px;">
                    <ul class="dyn_resv">
                        <%=dynamicInfo %>
                    </ul>
                </div>
            </div>
                    </div>
            <script>
                $(".box_notice .detail").clickLoad();
                pro.autoScroll.regist(".dyn_resv", 12);
            </script>
        </div>
    </div>
</body>
</html>
