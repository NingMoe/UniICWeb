<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="ClientWeb_xcus_cg_Default" %>

<%@ Register TagPrefix="Uni" TagName="login" Src="~/ClientWeb/pro/net/dlg_lg.ascx" %>
<%@ Register TagPrefix="Uni" TagName="include" Src="../a/net/include.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title><%=GetConfig("SysName") %></title>
    <Uni:include runat="server" />
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <!--[if lt IE 9]>
      <script src="../../fm/add/html5shiv.js"></script>
      <script src="../../fm/add/respond.js"></script>
    <![endif]-->
    <style>
        .jumbotron { background: url(theme/images/main_bg.jpg) no-repeat bottom center;-webkit-background-size: cover; -moz-background-size: cover; -o-background-size: cover; background-size: cover;padding-top:10px;margin-bottom:10px;}
        .navbar{margin-bottom:0;}
        body {font-size: 16px; background: transparent;position:relative; }
        small { font-size:12px;}
        .margin-base-vertical { margin: 40px 0;}
        .nav_content .img-circle{height:100px;width:100px;border:3px solid white;float:left;}
        .nav_content li {overflow:hidden;}
        .nav_list {overflow:hidden;}
        .nav_list li {float:left;margin:2px;padding: 2px 16px;background-color:rgba(255, 255, 255, 0.6);}
        .nav_list a {color:#A3740F;font-size:16px;}
        .nav_list li:hover { }
        .nav_list li:hover a { }

        #login_info input[type=text], #login_info input[type=password] { font-size: 16px; line-height: 35px; }
        .panel_list {height:100px;overflow:hidden;}
        #notice h1 {color:#444;}
        #notice .panel_list li{padding-left:5px;border-bottom:#ddd dashed 1px;margin:0 3px;color:#666;line-height:23px;font-size:14px;}
        .container {width: 1170px; max-width: 1170px; min-width: 1170px;}
    </style>
    <script>
        function login() {
            var id = $.trim($("#username").val());
            var pwd = $("#password").val();
            if (id.length == 0) {
                uni.msgBox("请输入帐号和密码！");
                return;
            }
            pro.j.lg.login(id, pwd, function (rlt) {
                if (rlt.ret == 2) {
                    uni.msgBox(rlt.msg, "", function () {
                        actAcc();
                    })
                }
                else {
                        location.href = "Default.aspx";
                }
            });
        }
        function actAcc() {
            var dlg = $("#dlg_act_acc");
            var id = $.trim($("#username").val());
            var pwd = $("#password").val();
            $("input[name=id]", dlg).val(id);
            $("input[name=pwd]", dlg).val(pwd);
            pro.d.lg.actAcc(function () {
                uni.msgBox("激活成功！", "", function () {
                    location.href = "Default.aspx";
                });
            });
        }
        $(function () {
            //浏览器检测
            var ie = parseInt(uni.getIEVer());
            if (ie > 0 && ie < 9) {
                uni.msgBox("你的浏览器版本过低，为获得更好的浏览效果，建议升级浏览器。");
            }
        });
    </script>
</head>
<body style="background: #fbfbfb;">
    <Uni:login runat="server" />
    <div class="jumbotron">
    <div class="container">
<div class="row">
            <div class="col col_7">
                <h2><%=GetConfig("SysName")%></h2>
        <ul class="nav_content" >
            <li style="border-bottom:solid 1px #fff;padding-bottom:5px;margin-bottom:5px;">
                <ul class="nav_list"><li style="text-align:right"><a href="YardList.aspx">场馆展示</a></li><li><a href="Activity.aspx">活动动态</a></li></ul>
            </li>
            <li>
                <img src="../../upload/DevImg/1397621640.jpg" alt="" class="img-circle">
                <img src="../../upload/DevImg/1397621706.jpg" alt="" class="img-circle">
                <div class="img-circle"></div>
                <img src="../../upload/DevImg/1397620220.jpg" alt="" class="img-circle">
                <div class="img-circle"></div>
                <img src="../../upload/DevImg/1397626290.jpg" alt="" class="img-circle">
<div class="img-circle"></div>
                <img src="../../upload/DevImg/1397624330.jpg" alt="" class="img-circle">
                <img src="../../upload/DevImg/1397626990.jpg" alt="" class="img-circle">
            </li>
            <li>
                <img src="../../upload/DevImg/1397627492.jpg" alt="" class="img-circle">
            </li>
        </ul>
    </div>
        <div class="col col_5">
            <h1 class="margin-base-vertical" style="color:white;margin-top:20px;">用户登录</h1>
            <div class="margin-base-vertical" id="login_info">
                <p class="input-group">
                    <span class="input-group-addon"><span>帐号</span></span>
                    <input type="text" class="form-control input-lg" name="id" id="username" placeholder="<%=GetConfig("idIntro") %>" />
                </p>
                <p class="input-group">
                    <span class="input-group-addon"><span>密码</span></span>
                    <input type="password" class="form-control input-lg" name="pwd" id="password" placeholder="<%=GetConfig("pwdIntro") %>" />
                </p>
                <p class="text-center" style="color:white;"><small>为能即时联系用户，联系信息不完整的用户需要激活，以补充联系信息。</small></p>
                <p class="text-center">
                    <button type="button" class="btn btn-success btn-lg default" onclick="login()">登录</button>
                    <button type="button" class="btn btn-default btn-lg" onclick="actAcc()">激活</button>
            </div>
            <div class="text-center white">
                <p style="font-size:14px;">预约规则 | 文明使用守则 | 使用帮助</p>
            </div>

        </div>
        <!-- //main content -->
    </div>
    </div>
    </div>
    <div class="container hidden" id="notice">
<%--        <div class="row">
            <ul class="nav_list"><li style="text-align:right"><a href="">场馆展示</a></li><li><a href="">活动展示</a></li></ul>
        </div>--%>
        <div class="row">
        <div class="col col_6">
            <h1><span class="glyphicon glyphicon-volume-up"></span> <small>系统公告</small></h1>
            <div class="panel_list">
                <ul>
                    <li>系统维护公告</li>
                    <li>系统维护公告</li>
                    <li>系统维护公告</li>
                    <li>4</li>
                </ul>
            </div>
        </div>
        <div class="col col_6">
            <h1><span class="glyphicon glyphicon-list-alt"></span> <small>最新活动</small></h1>
            <div class="panel_list">
                                <ul>
                    <li>1</li>
                    <li>2</li>
                    <li>3</li>
                    <li>4</li>
                </ul>
            </div>
        </div>
        </div>
    </div>
        <footer class="bs-docs-footer" role="contentinfo" style="margin-top:100px;">
        <div class="container text-center">
            <p>版权所有：<%=GetConfig("SysAutoSchoolName")%></p>
            <p>全国热线：4008581115 | E-mail：hkp@unifound.net | 网站：http://www.unifound.net</p>
        </div>
    </footer>
    <!-- //row -->
</body>
</html>
