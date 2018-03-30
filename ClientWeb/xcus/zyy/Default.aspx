<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ClientWeb_Default" %>

<%@ Register TagPrefix="Uni" TagName="dlg_lg" Src="~/ClientWeb/pro/net/dlg_lg.ascx" %>
<asp:Content runat="server" ID="head" ContentPlaceHolderID="HeadContent">
    <link href="theme/TabStyle.css" rel='stylesheet' />
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/highcharts/highcharts.js" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/highcharts/modules/exporting.js" type="text/javascript"></script>
    <link href='<%=ResolveClientUrl("~/ClientWeb/") %>md/slide/ResponsiveSlide.css' rel='stylesheet' />
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/slide/ResponsiveSlide.min.js" type="text/javascript"></script>
    <style type="text/css">
        #headImg { height: 230px;width: 960px; }
        #headImg li img { width: 960px; height: 230px; }
        #keyword { height: 26px; width: 180px; }
        #sel_campus li { color: #999; }

        #sel_campus li a:hover { cursor: pointer; color: #065FA0; }

        #new_dev_list li a { display: block; }

        #new_dev_list li a:hover { font-weight: 600 !important; }

        .img_list img { width: 360px; height: 200px; margin-top: 5px; margin-left: 10px; }

        #news .d_list a { color: #333; }

        #news .d_list a:hover { color: #0171bb; }
        #slide li img { width: 320px; height: 200px; }
        .rslides_tabs { display: block; position: absolute; z-index: 2; font-size: 12px; text-shadow: none; color: #fff; background: #666; opacity: 0.8; left: 0; right: 0; bottom: 0; margin: 4px 5px; padding: 3px 5px; max-width: none; text-align: right; }
        .rslides_tabs li { color: #fff; background-color: #fff; margin: 0 2px; display: inline; }
        .rslides_tabs li a { display: inline-block; line-height: 20px; text-align: center; width: 22px; color: #000; }
        .rslides_tabs li:hover a { background-color: #065FA0; color: #fff; }
        .rslides_tabs .rslides_here { background-color: #065FA0; }
        .rslides_tabs .rslides_here a { color: #fff; }
    </style>
</asp:Content>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(function () {
            //注册点击校区
            $("#sel_campus").find("li").click(function () {
                search($(this).val());
            });
            //初始化最新仪器
            $("#new_dev_list").find(".tab_con li").mouseover(function () {
                var id = $(this).val();
                $("#new_dev_img").find("img").each(function () {
                    var val = $(this).attr("id");
                    if (val == id) {
                        $(this).removeClass("hidden");
                    }
                    else {
                        $(this).addClass("hidden");
                    }
                });
            });
            $("#new_dev_list").find(".tab_con li:first").trigger("mouseover");
            //初始化统计图
            $('#container').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                colors: ['#058DC7', '#8bbc21'],
                xAxis: {
                    categories: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                },
                yAxis: {
                    allowDecimals: false,
                    min: 0,
                    title: {
                        text: ''
                    }
                },
                tooltip: {
                    headerFormat: '<span>{point.key}</span>',
                    pointFormat: '<div><span style="color:{series.color};padding:0;">{series.name}: </span>' +
                        '<span style="padding:0"><b>{point.y:.1f} 时</b></span></div>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        stacking: 'normal'
                    }
                },
                credits: {
                    enabled: false
                },
                series: [{
                    name: '工作日',
                    data: [<%=wUseTimes%>],
                    stack: 'male'
                }, {
                    name: '非工作日',
                    data: [<%=rUseTimes%>],
                    stack: 'female'
                }]
            });
            //判断是否管理员
            if (uni.isNoNull( pro.acc.accno)) {
                if(!(parseInt(pro.acc.ident)&268435456))
                    $("#manager_login").hide();
            }
        });
        //用户注册
        function regist() {
            pro.d.lg.registAcc(function (rlt) {
                uni.msgBoxR("注册成功！")
            },undefined," 本校用户使用数字化校园平台帐号登录，无需注册。")
        }
        //用户激活
        function innerReg() {
            $("#rid")[0].value = $("#d_id").val();
            $("#rpwd")[0].value = $("#d_pwd").val();
            $('#regdialog').dialog('open');
        }
        ////
        function onDSubmit() {
            if ($("#d_id").val() == "") {
                uni.msgBox("帐号不能为空!");
                return;
            }
            pro.j.lg.fLogin("dlogin", $("#d_login_dialog"), function (rlt) {
                if (rlt.ret == 1) {
                    location.reload();
                }
                if (rlt.ret == 2) {
                    uni.msgBox(rlt.msg, "提醒", function () {
                        innerReg();
                    });
                }
            },
            function (rlt) {
                var verf = document.getElementById("verf_img");
                verf.src = verf.src + "?";
                $("#number").val("");
                uni.msgBox(rlt.msg);
                return false;
            });
        }
        function search(c) {
            var rsch = $("input[name=search_type]:checked").val();
            var key = $("#keyword").val();
            var cps = '';
            if (c != undefined && c != '') {
                cps = c;
                rsch = "dev";
            }
            if (rsch == "dev") {
                window.location.href = "DevList.aspx?key=" + encodeURI(key) + "&cps=" + cps;
            }
            else if (rsch == "art") {
                window.location.href = "ArtSearch.aspx?cl=rsch&key=" + encodeURI(key);
            }
        }
    </script>
    <div id="headImg">
        <ul class="banner_slide">
            <li>
                <img alt="" src="Theme/images/main_img/main_img.jpg" />
            </li>
        </ul>
        <script type="text/javascript">
            //banner
            $(".banner_slide").responsiveSlides({
                fade: 1000,
                speed: 3000
            });
        </script>
    </div>
    <div>
        <Uni:dlg_lg runat="server" />
        <div class="model">
            <div id="news" class="tabs dft box float_l" style="width: 650px; position: relative;">
                <a onclick="this.href='ArticleList.aspx?gr=2&cl=10003'" class="u-more" style="top: 5px; right: 10px;">+More</a>
                <ul class="tab_head h_box">
                    <li><a>新闻动态</a></li>
                    <li><a>通知公告</a></li>
                </ul>
                <div class="tab_con c_box">
                    <div class="item">
                        <div style="overflow: hidden; width: 320px; float: left; position: relative; padding: 4px 5px;">
                            <ul id="slide" class="float_l news_slide">
                                <%=slideImgs %>
                            </ul>
                            <script type="text/javascript">
                                //图片新闻
                                $(".news_slide").responsiveSlides({
                                    fade: 500,
                                    speed: 2000,
                                    maxwidth: 320,
                                    pager: true
                                });
                                //初始化新闻
                                pro.j.art.getArtListByCls(10003, 1, 8, function (rlt) {
                                    var list = rlt.data;
                                    var str = "";
                                    $(list).each(function () {
                                        str += "<li><a  class='float_l' href='ArticleList.aspx?gr=" + this.gr + "&art=" + this.id + "'>▪ " + uni.cutStrT(this.title, 19) + "</a><span class='float_r'>" + this.date + "</span></li>";
                                    });
                                    $("#ul_news_list").html(str);
                                });
                                pro.j.art.getArtListByCls(10004, 1, 16, function (rlt) {
                                    var list = rlt.data;
                                    var str1 = "";
                                    var str2 = "";
                                    $(list).each(function (i) {
                                        var str = "<li><a  class='float_l'  href='ArticleList.aspx?gr=" + this.gr + "&art=" + this.id + "'>▪ " + uni.cutStrT(this.title, 16) + "</a><span class='float_r'>" + this.date + "</span></li>";
                                        if (i < 8) {
                                            str1 += str;
                                        }
                                        else {
                                            str2 += str;
                                        }
                                    });
                                    $("#ul_notice1").html(str1);
                                    $("#ul_notice2").html(str2);
                                });
                            </script>
                        </div>
                        <div class="float_l d_list" style="width: 310px; font-family: 微软雅黑;">
                            <ul id="ul_news_list">
                            </ul>
                        </div>
                    </div>
                    <div class="item d_list">
                        <div class="float_l" style="width: 310px; font-family: 微软雅黑;">
                            <ul id="ul_notice1">
                                <li><span class="float_l">▪ 设备处开展原子吸收分光光度计...</span><span class="float_r " style="float: right">2013-09-17</span></li>
                                <li>▪ 设备处与保卫处联合开展新学期...<span class="float_r">2013-09-09</span></li>
                                <li>▪ 设备处召开新学期部门工作会议<span class="float_r">2013-06-21</span></li>
                                <li>▪ 学校开展气相色谱类大型仪器设...<span class="float_r">2013-06-08</span></li>
                                <li>▪ 学校首次举办大仪操作使用培训<span class="float_r">2013-05-09</span></li>
                                <li>▪ 学校首次举办大仪操作使用培训<span class="float_r">2013-05-09</span></li>
                                <li>▪ 设备处开展原子吸收分光光度计...<span class="float_r">2013-05-09</span></li>
                            </ul>
                        </div>
                        <div class="float_r" style="width: 310px; font-family: 微软雅黑;">
                            <ul id="ul_notice2">
                                <li>▪ 设备处开展原子吸收分光光度计...<span class="float_r">2013-09-17</span></li>
                                <li>▪ 设备处与保卫处联合开展新学期...<span class="float_r">2013-09-09</span></li>
                                <li>▪ 设备处召开新学期部门工作会议<span class="float_r">2013-06-21</span></li>
                                <li>▪ 学校开展气相色谱类大型仪器设...<span class="float_r">2013-06-08</span></li>
                                <li>▪ 学校首次举办大仪操作使用培训<span class="float_r">2013-05-09</span></li>
                                <li>▪ 学校首次举办大仪操作使用培训<span class="float_r">2013-05-09</span></li>
                                <li>▪ 设备处开展原子吸收分光光度计...<span class="float_r">2013-05-09</span></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div id="panel_login" class="short_box float_r" style="width: 300px;">
                <div class="h_box">
                    用户登录 LOGIN
                    <a onclick="this.href='<%=this.ResolveClientUrl("~/Pages/LoginForm.aspx") %>'" style="color:#fff;margin-left:70px;" id="manager_login">【管理员入口】</a>
                </div>
                <div class="c_box">
                    <!-- 登陆表单 begin -->
                    <div id="d_login">
                        <form id="d_login_dialog" onsubmit="return false;">
                            <div>
                                <p><span style="color: #666; font-weight: 600; padding-left: 10px;">▪ 预约请登录</span><span style="color: #888; text-align: left; margin: 2px;"></span></p>
                            </div>
                            <table style="width: 210px;">
                                <tbody>
                                    <tr>
                                        <td>帐<span style="width: 1em; display: inline-block;"></span>号</td>
                                        <td>
                                            <input name="id" id="d_id" maxlength="20" type="text" class="input_txt" style="width: 149px;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>密<span style="width: 1em; display: inline-block;"></span>码</td>
                                        <td>
                                            <input name="pwd" id="d_pwd" type="password" maxlength="14" value="" class="input_txt" style="width: 149px;" /></td>
                                    </tr>
                                    <tr>
                                        <td>验证码</td>
                                        <td>
                                            <input id="number" name="number" style="font-size: 14px; vertical-align: middle"
                                                type="text" class="input" value="" size="6" maxlength="4" />
                                            <img id="verf_img" src="image.aspx" style="vertical-align: middle" alt="看不清，点击更换" onclick="this.src=this.src+'?'" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="f-tc" style="margin: 2px 0;">
                                <input type="button" id="d_submit" class="button" style="height: 30px; width: 50px; margin-right: 10px;" value="登录" onclick="onDSubmit()" />
                            </div>
                            <div class="f-tc">
                                <a class="click" onclick="regist()">校外用户注册</a> | 
                                <a class="click" onclick="innerReg()">校内用户激活</a>
                            </div>
                        </form>
                    </div>
                    <!-- 登陆表单 end-->
                    <div id="d_user_stu" class="hidden panel_user">
                        <p>您好，<span class="name"></span></p>
                        <p><%=tooltip %></p>
                        <div style="text-align: center;">
                            <table>
                                <tbody>
                                    <tr>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?tab=0'"><a>我的预约</a></span></td>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?tab=4'"><a>个人信息</a></span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?tab=1'"><a>实验数据</a></span></td>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?tab=2'"><a>我的项目</a></span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="button proxy" onclick="location.href='Proxy.aspx?tab=0'"><a>代审预约</a></span></td>
                                        <td><span class="button proxy" onclick="location.href='Proxy.aspx?tab=1'"><a>代管项目</a></span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="button" onclick="location.href='DevList.aspx'"><a>预约设备</a></span></td>
                                        <td><span class="button"><a onclick="pro.j.lg.logout();">退出登录</a></span></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div id="d_user_tutor" class="hidden panel_user">
                        <p>您好，<span class="name"></span></p>
                        <p>欢迎使用浙江中医药大学大型仪器开放共享管理平台</p>
                        <div style="text-align: center;">
                            <table>
                                <tbody>
                                    <tr>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?tab=0'"><a>审核预约</a></span></td>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?tab=4'"><a>个人信息</a></span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?tab=1'"><a>实验数据</a></span></td>
                                        <td><span class="button" onclick="location.href='Course.aspx?tab=0'"><a>项目管理</a></span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="button" onclick="location.href='DevList.aspx'"><a>预约设备</a></span></td>
                                        <td><span class="button"><a onclick="pro.j.lg.logout();">退出登录</a></span></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    if ($("#cur_name").val() != "") {
                        $("#d_login").addClass("hidden");
                        if (pro.acc.pro == "1") {
                            $(".proxy").show();
                        }
                        else {
                            $(".proxy").hide();
                        }
                        if ((parseInt($("#cur_ident").val()) & 1048576) > 0) {
                            $("#d_user_tutor .name").html($("#cur_name").val() + "，负责人");
                            $("#d_user_tutor").removeClass("hidden");
                        }
                        else {
                            $("#d_user_stu .name").html($("#cur_name").val());
                            $("#d_user_stu").removeClass("hidden");
                        }
                    }
                    //登录框提示
                    if (pro.isLogin()) {
                        var s = $("#cur_tsta").val();
                        var t = $("#t_sta_tip");
                        if (s == "1") {
                            t.html("预约项目实验，请先进入[<a href='UserCenter.aspx?tab=4'>个人信息</a>]指定负责人。");
                        }
                        else if (s == "2") {
                            t.html("预约项目实验，需等待负责人审核通过。");
                        }
                        else if (s == "3") {
                            t.html("您未获得预约项目实验的许可。");
                        }
                        else if (s == "4") {
                            t.html("您已获得负责人许可，请点击[<a href='DevList.aspx'>预约设备</a>]预约项目实验。");
                        }
                    }
                </script>
            </div>
        </div>
        <div class="model">
            <div class="short_box float_l" style="margin-bottom: 4px;">
                <div class="h_box">查询</div>
                <div id="search" class="c_box" style="height: 134px;">
                    <div style="text-align: left; margin-left: 20px; margin-top: 10px;">仪器<input type="radio" name="search_type" value="dev" checked="checked" style="margin-right: 20px;" />文章<input type="radio" name="search_type" value="art" style="margin-right: 20px;" /></div>
                    <div style="margin-top: 15px;">
                        <input id="keyword" type="text" style="padding: 0px; line-height: 26px;" /><input type="button" id="search_bt" value="搜索" onclick="search()" />
                    </div>
                    <div id="sel_campus">
                        <ul runat="server" id="campusList">
                        </ul>
                    </div>
                </div>
            </div>
            <div class="tabs dft box float_r" style="margin-bottom: 4px;">
                <ul class="tab_head h_box">
                    <li class=""><a>最新预约</a></li>
                    <li><a>当前未审核</a></li>
                    <li><a>违规记录</a></li>
                </ul>
                <div class="tab_con c_box">
                    <div class="box_tbl zebra">
                        <table>
                            <thead>
                                <tr>
                                    <th>预约人</th>
                                    <th>仪器名称</th>
                                    <th>预约时段</th>
                                    <th>提交时间</th>
                                    <th>审核状态</th>
                                    <th>管理员</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="newResv">
                            </tbody>
                        </table>
                    </div>
                    <div class="box_tbl zebra">
                        <table>
                            <thead>
                                <tr>
                                    <th>预约人</th>
                                    <th>仪器名称</th>
                                    <th>所在校区</th>
                                    <th>预约时段</th>
                                    <th>提交时间</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="unCheckResv">
                            </tbody>
                        </table>
                    </div>
                    <div class="box_tbl zebra">
                        <table>
                            <thead>
                                <tr>
                                    <th>姓名</th>
                                    <th>使用仪器</th>
                                    <th>违规说明</th>
                                    <th>时间</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="Breach">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="tabs dft short_box float_l">
                <ul class="tab_head h_box">
                    <li><a>仪器使用月排行</a></li>
                    <li><a>仪器使用年排行</a></li>
                </ul>
                <div class="tab_con c_box" style="height: 282px;">
                    <div class="box_tbl zebra">
                        <table>
                            <thead>
                                <tr>
                                    <th>仪器名称</th>
                                    <th>所属部门</th>
                                    <th>使用次数</th>
                                    <th>使用时长</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="moonRank">
                            </tbody>
                        </table>
                    </div>
                    <div class="box_tbl zebra">
                        <table>
                            <thead>
                                <tr>
                                    <th>仪器名称</th>
                                    <th>所属部门</th>
                                    <th>使用次数</th>
                                    <th>使用时长</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="yearRank">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box  float_r">
                <div class="h_box">仪器实时使用情况</div>
                <div class="c_box">
                    <div class="box_tbl zebra">
                        <table>
                            <thead>
                                <tr>
                                    <th>仪器名称</th>
                                    <th>所属部门</th>
                                    <th>实验室</th>
                                    <th>使用人</th>
                                    <th>开始时间</th>
                                    <th>使用状态</th>
                                    <th>管理员</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="curTest">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="model">
            <div class="box float_l">
                <div class="h_box">最新设备展示</div>
                <div class="c_box d_list">
                    <div id="new_dev_list" class="float_l" style="width: 180px; margin: 5px 20px 0 10px;">
                        <div runat="server" id="newDevs">
                        </div>
                    </div>
                    <div id="new_dev_img" class="float_l img_list" style="width: 360px;">
                        <div runat="server" id="devImg">
                        </div>
                    </div>
                </div>
            </div>
            <div class="short_box float_r">
                <div class="h_box">设备使用走势图</div>
                <div class="c_box">
                    <div id="container" style="width: 350px; height: 208px; margin-left: 2px;"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
