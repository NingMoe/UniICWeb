<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="ClientWeb_xcus_bsd_xl_Login" %>

<%@ Register TagPrefix="Uni" TagName="login" Src="~/ClientWeb/pro/net/dlg_lg.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>IC空间管理系统</title>
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>fm/jquery/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>fm/jquery-ui/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>fm/jquery-ui/sunny/jquery-ui-1.10.4.custom.min.css" rel='stylesheet' />

    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>fm/uni.lib.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>fm/uni.css" rel='stylesheet' />
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>pro/pro.lib.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>pro/pro.css" rel='stylesheet' />

        <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/validationEngine/validationEngine.jquery.css" rel="stylesheet" />
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/validationEngine/validationEngine.js" type="text/javascript"></script>

    <script type="text/javascript" src="theme/cus.js"></script>
    <link rel="stylesheet" type="text/css" href="theme/cus.css" />
    <style>
        /* ------- Remove Chrome's border around active fields ------- */

        #container *:focus { outline: none; }

        /* ------- Disable background and border for input fields ------- */

        #container input { background: transparent; border: 0; }

        /* --------------------------------------------------------------- */
        /* ------- Container ------- */

        #container { margin: 30px auto; height: 262px; width: 524px; text-align: center; font-weight: normal; font-size: 12px; color: #333; }

        /* ------- Login Form ------- */

        .login-bg { background: url(theme/images/form-bg.png) top left no-repeat; width: 524px; height: 262px; text-align: left; padding-top: 1px; }

        .login { width: 360px; float: left; font-family: "微软雅黑"; margin-top: 30px; margin-left: 20px; font-size: 20px; line-height: 24px; border-bottom: 1px dashed #ccc; }
        .tag-info { float: left; margin-top: 26px; margin-left: 80px; color: #666; line-height: 24px; }
        .username-text { width: 190px; float: left; margin-top: 50px; margin-left: 40px; }

        .password-text { width: 190px; float: left; margin-top: 50px; margin-left: 0px; }

        /*.welcome-text { width: 360px; float: left; margin-top: 50px; margin-left: 40px; line-height: 16px; }*/

        .username-field { width: 168px; height: 38px; float: left; margin-top: 10px; margin-left: 35px; background: url(theme/images/username-field.png) center left no-repeat; }

        .username-field:hover { background: url(theme/images/username-field-hover.png) center left no-repeat; }

        .password-field { width: 180px; height: 38px; float: left; margin-top: 10px; margin-left: 22px; background: url(theme/images/password-field.png) center left no-repeat; }

        .password-field:hover { background: url(theme/images/password-field-hover.png) center left no-repeat; }

        input[type="text"], input[type="password"] { width: 120px; height: 16px; margin-top: 10px; margin-left: 10px; font-family: Verdana, Arial; font-size: 16px; color: #2d2d2d; }

        /* -------------------------------------------------------------------------- */
        #container input[type="submit"] { width: 90px; height: 73px; margin-top: 160px; font-family: "微软雅黑"; font-size: 26px; cursor: pointer; color: #fff; }

        #container input[type="submit"]:hover, input[type="submit"]:focus { background: url(theme/images/go-hover.png) top left no-repeat; }
    </style>
    <script type="text/javascript">
        var para="";
        $(function () {
            var req = uni.getReq();
            if (req["site"])
                para = "?site=" + req["site"];
        })
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
                    location.href = "index.aspx"+para;
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
                    location.href = "index.aspx"+para;
                });
            });
        }
    </script>
</head>
<body style="background: #eee; ">
    <div id="pub_resource">
        <Uni:login runat="server" />
    </div>
    <div style="background: #fff; ">
    <div id="tp_body">
        <div id="tp_header">
            <img alt="" src="theme/images/logo.jpg" id="tp_logo" style="height: 75px; width: 406px;" />
        </div>
    </div>
    </div>
    <div style="background: #B28E5E; height: 80px; text-align: center;">
        <div style="font-family: 微软雅黑; font-size: 36px; font-weight: 500; line-height: 46px; color: #fff; word-spacing: 4px; letter-spacing: 4px; padding-top: 10px;">
            登录IC空间管理系统</div>
    </div>
    <div id="container">
        <div class="login-bg">
            <form onsubmit="return false;" autocomplete="on">
            <div class="f-fl f-ofh" style="width: 420px;">
                <div class="login">用户登录
                   <span style="font-size:14px;float:right;"><a onclick="this.href='<%=this.ResolveClientUrl("~/Pages/LoginForm.aspx") %>'">【管理员入口】</a></span>
                </div>
                <div class="username-text">帐号:</div>
                <div class="password-text">密码:</div>
                <div class="username-field">
                    <input type="text" name="username" id="username" />
                </div>
                <div class="password-field">
                    <input type="password" name="password" id="password" />
                </div>
                <div class="tag-info">使用一卡通帐号与密码登录， 若第一次使用，请先 <a class="click" onclick="actAcc();" style="font-size: 14px; font-family: '微软雅黑'">激活</a>
                </div>
            </div>
            <div class="f-fr default" style="width: 100px; cursor: pointer;" onclick="login()">
                <input type="submit" name="submit" value="登录" />
            </div>
            </form>
        </div>
    </div>
</body>
</html>
