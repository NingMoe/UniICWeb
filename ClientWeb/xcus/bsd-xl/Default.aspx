<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ClientWeb_Default" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="HeadContent">
    <link href="theme/TabStyle.css" rel='stylesheet' />
    <%--    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/highcharts/highcharts.js" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/highcharts/modules/exporting.js" type="text/javascript"></script>--%>
    <link href='<%=ResolveClientUrl("~/ClientWeb/") %>md/slide/ResponsiveSlide.css' rel='stylesheet' />
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/slide/ResponsiveSlide.min.js" type="text/javascript"></script>
    <style type="text/css">
        /*首页*/
        #headImg { height: 230px; }
        #headImg li img { width: 600px; height: 230px; }
        #keyword { height: 26px; width: 180px; }
        #sel_campus li { color: #999; }
        #sel_campus li a:hover { cursor: pointer; color: #065FA0; }
        #new_dev_list li a { display: block; }
        #new_dev_list li a:hover { font-weight: 600 !important; }
        .img_list img { width: 360px; height: 200px; margin-top: 5px; margin-left: 10px; }
        #slide li img { width: 320px; height: 200px; }
        .rslides_tabs { display: block; position: absolute; z-index: 2; font-size: 12px; text-shadow: none; color: #fff; background: #666; opacity: 0.8; left: 0; right: 0; bottom: 0; margin: 4px 5px; padding: 3px 5px; max-width: none; text-align: right; }
        .rslides_tabs li { color: #fff; background-color: #fff; margin: 0 2px; display: inline; }
        .rslides_tabs li a { display: inline-block; line-height: 20px; text-align: center; width: 22px; color: #000; }
        .rslides_tabs li:hover a { background-color: #065FA0; color: #fff; }
        .rslides_tabs .rslides_here { background-color: #065FA0; }
        .rslides_tabs .rslides_here a { color: #fff; }
        /*tooltip*/
        .tip { text-align: center; line-height: 26px; }
        #ul_notice_list li:hover { color: #8Bba10; }
                #news .d_list a { color: #333; }
        #news .d_list a:hover { color:#8Bba10 ; }
    </style>
</asp:Content>
<asp:Content runat="server" ID="content" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(function () {
            uni.hr.reload();
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
            //浏览器检测
            var ie = parseInt(uni.getIEVer());
            if (ie > 0 && ie < 9) {
                uni.msgBox("你的浏览器版本过低，为获得更好的浏览效果，建议升级浏览器。");
            }
            //初始化统计图
            //    $('#container').highcharts({
            //        chart: {
            //            type: 'column'
            //        },
            //        title: {
            //            text: ''
            //        },
            //        colors: ['#058DC7', '#8bbc21'],
            //        xAxis: {
            //            categories: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
            //        },
            //        yAxis: {
            //            allowDecimals: false,
            //            min: 0,
            //            title: {
            //                text: ''
            //            }
            //        },
            //        tooltip: {
            //            headerFormat: '<span>{point.key}</span>',
            //            pointFormat: '<div><span style="color:{series.color};padding:0;">{series.name}: </span>' +
            //                '<span style="padding:0"><b>{point.y:.1f} 时</b></span></div>',
            //            shared: true,
            //            useHTML: true
            //        },
            //        plotOptions: {
            //            column: {
            //                stacking: 'normal'
            //            }
            //        },
            //        credits: {
            //            enabled: false
            //        },
            //        series: [{
            //            name: '工作日',
            //            data: [=wUseTimes%>],
            //            stack: 'male'
            //        }, {
            //            name: '非工作日',
            //            data: [=rUseTimes%>],
            //            stack: 'female'
            //        }]
            //    });
            //    //判断是否管理员
            //    if (uni.isNoNull(pro.acc.accno)) {
            //        if (!(parseInt(pro.acc.ident) & 268435456))
            //            $("#manager_login").hide();
            //    }
        });
        //用户注册
        function regist() {
            pro.d.lg.registAcc(function (rlt) {
                uni.msgBoxR("注册成功！")
            })
        }
        //用户激活
        function innerReg() {
            $("#dlg_act_acc input[name=id]").val($("#d_id").val());
            $("#dlg_act_acc .pwd").val($("#d_pwd").val());
            pro.d.lg.actAcc();
        }
        ////
        function onDSubmit() {
            if ($("#d_id").val() == "") {
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
    </script>
    <div>
        <div class="model">
            <div class="grey_box ui-tabs ui-widget ui-corner-all" style="height: 240px;">
                <div id="headImg" class="f-fl">
                    <ul class="banner_slide">
<%--                        <li>
                            <img alt="" src="Theme/images/main_img/slide1.jpg" />
                        </li>--%>
                        <li>
                            <img alt="" src="Theme/images/main_img/slide2.jpg" />
                        </li>
                        <li>
                            <img alt="" src="Theme/images/main_img/slide3.jpg" />
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
                <div id="panel_login" class="short_box f-fr" style="border: none;">
                    <%--                <ul class="h_box ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">  ui-tabs ui-widget ui-corner-all
                    <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a class="ui-tabs-anchor">用户登录 LOGIN</a></li>
                </ul>--%>
                    <div class="c_box">
                        <!-- 登陆表单 begin -->
                        <a onclick="this.href='<%=this.ResolveClientUrl("~/Pages/LoginForm.aspx") %>'" class="click f-fr" id="manager_login">【管理员入口】</a>
                        <div id="d_login" class="zq_login">
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
                                <div class="f-tc" style="margin: 5px 0;">
                                    <input type="button" id="d_submit" class="button default" style="height: 30px; width: 50px; margin-right: 10px;" value="登录" onclick="onDSubmit()" />
                                </div>
                                <div class="f-tc">
                                    <a class="click" onclick="innerReg()">首次登录请先激活</a>
                                </div>
                            </form>
                        </div>
                        <!-- 登陆表单 end-->
                        <div id="d_user_stu" class="hidden panel_user zq_login">
                            <p>您好，<span class="name"></span></p>
                            <p>欢迎登录北师大心理学院实验室共享管理平台</p>
                            <p><%=tooltip %></p>
                            <div style="text-align: center;">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td><span class="button" onclick="location.href='User.aspx'"><a>我的预约</a></span></td>
                                            <td><span class="button" onclick="location.href='User.aspx?tab=2'"><a>个人信息</a></span></td>
                                        </tr>
                                        <tr>
                                            <td><span class="button" onclick="location.href='Research.aspx'"><a>科研实验</a></span></td>
                                            <td><span class="button"><a onclick="pro.j.lg.logout();">退出登录</a></span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div style="margin: 3px 0; text-align: center;">
                            <a href="ArticleList.aspx?id=resv&type=other&title=<%=Server.UrlEncode("预约规则")%>">预约规则</a> | 
                        <a href="ArticleList.aspx?id=rule&type=other&title=<%=Server.UrlEncode("文明使用守则")%>">文明使用守则</a> | 
                        <a href="ArticleList.aspx?id=help&type=other&title=<%=Server.UrlEncode("使用帮助")%>">使用帮助</a>
                        </div>
                    </div>
                    <script type="text/javascript">
                        if (pro.isLogin()) {
                            $("#d_login").addClass("hidden");
                            $("#d_user_stu .name").html(pro.acc.name);
                            $("#d_user_stu").removeClass("hidden");
                        }
                    </script>
                </div>
            </div>
        </div>
        <div class="model">
            <div id="news" style="position: relative;margin-left:10px;">
                <h3 class="color3" style="margin: 10px 0 10px 10px">通知公告
                </h3>
                <div class="c_box" id="div_notice_list">
                    <%--                    <div style="width: 295px; height: 210px; float: left; position: relative; padding: 4px 5px; background: #eee;">
                        <div style="line-height: 24px; height: 24px; padding: 3px; border-bottom: 1px solid #ddd;">
                            <span id="notice_title" class="color3"></span>
                        </div>
                        <p id="notice_content" style="text-indent:2em;"></p>
                    </div>--%>
                    <div class="f-fl d_list" style="width: 680px; font-family: 微软雅黑;">
                        <ul id="ul_notice_list">
                            <%=noticeList %>
                        </ul>
                    </div>
<%--                    <script type="text/javascript">
                        $("#ul_notice_list li").mouseover(function () {
                            var pthis = $(this);
                            var con = pthis.attr("content");
                            var date = pthis.attr("date");
                            if (date && date.length > 13) {
                                var y = date.substring(0, 4);
                                var M = date.substring(4, 6);
                                var d = date.substring(6, 8);
                                var H = date.substring(8, 10);
                                var m = date.substring(10, 12);
                                var s = date.substring(12, 14);
                            }
                            $("#notice_title").html(y + "年" + M + "月" + d + "日 " + H + "时" + m + "分");
                            $("#notice_content").html(con);
                        });
                        $("#ul_notice_list li").eq(0).trigger("mouseover");
                    </script>--%>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(".unitabs").unitab();
    </script>
</asp:Content>
