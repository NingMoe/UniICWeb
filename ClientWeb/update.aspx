<%@ Page Language="C#" AutoEventWireup="true" CodeFile="update.aspx.cs" Inherits="ClientWeb_option" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>应用程序配置升级</title>
    <script type="text/javascript" src="fm/jquery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="fm/jquery-ui/jquery-ui-1.10.3.custom.min.js"></script>
    <script type="text/javascript" src="fm/jquery-ui/bootstrap/js/bootstrap.js"></script>
    <link rel="stylesheet" href="fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="fm/jquery-ui/bootstrap/css/bootstrap.css" />

    <script type="text/javascript" src="fm/uni.lib.js"></script>
    <link rel="stylesheet" type="text/css" href="fm/uni.css" />
    <script type="text/javascript" src="pro/pro.lib.js"></script>
    <style>
        a { cursor: pointer; }
        label { cursor: pointer; margin-right: 5px; }
        .intro { color: #31708f; font-size: 14px; font-weight: bold; border-left: 3px solid #31b0d5; padding: 2px 5px; margin-top: 4px; margin-bottom: 1px; }
        .footer { text-align: center; }
        h3 { color: #999; border-bottom: 1px dashed #ccc; margin-left: 10px; padding-bottom: 3px; }
        .dashed { margin: 10px; border-bottom: 1px dashed #ccc; }
    </style>
</head>
<body style="padding-left: 15px; padding-bottom: 100px;">
    <h2>前端系统配置</h2>
    <div id="panel_login" style="display: <%=islogin?"none":"" %>">
        <h3>用户登录，启用系统请先完成基础配置，仅支持超级管理员登录。</h3>
        <script>
            var kvp = {};
            $(function () {
                if (!$("#panel_login").is(":visible"))
                init();
            });
            function login() {
                var id = $.trim($("#username").val());
                var pwd = $("#password").val();
                if (id.length == 0) {
                    uni.msgBox("请输入帐号和密码！");
                    return;
                }
                pro.j.lg.login(id, pwd, function (rlt) {
                    if (rlt.ret == 2) {
                        uni.msgBox(rlt.msg);
                    }
                    else
                        location.reload();
                });
            }
            //保存配置
            function save() {
                var opts = $("#options input");
                var ret = "";
                opts.each(function () {
                    var input = $(this);
                    var key = input.attr("name");
                    var value = input.val();
                    ret += key + '&' + value + '$';
                });
                if (ret.length > 0) ret = ret.substr(0, ret.length - 1);
                pro.j.objPostS(pro.j.util.p, { act: "save_app_settings", data: ret }, function () {
                    uni.msgBox("配置文件已升级完成");
                });
            }
            //初始化
            function init() {
                pro.j.objPostS(pro.j.util.p, { act: "get_app_settings", path: "Web.Config" }, function (rlt) {
                    var data = rlt.data;
                    if (data) {
                        var set = data.split('$');
                        for (var i = 0; i < set.length; i++) {
                            var arr = set[i].split('&');
                            if (arr.length == 2) {
                                kvp[arr[0]] = arr[1];
                            }
                        }
                        write();
                        $(".act_save").show();
                    }
                    else {
                        uni.msgBox("web.config不存在或没数据");
                    }
                });
            }
            //升级配置
            function update() {
                pro.j.objPostS(pro.j.util.p, { act: "get_app_settings", path: "options.xml" }, function (rlt) {
                    var tmp = rlt.data;
                    if (tmp) {
                        var items = tmp.split('$');
                        for (var i = 0; i < items.length; i++) {
                            var arr = items[i].split('&');
                            if (arr.length == 2 && kvp[arr[0]] == undefined) {
                                kvp[arr[0]] = '';
                            }
                        }
                        write();
                        save();
                    }
                    else {
                        uni.msgBox("web.xml不存在或没数据");
                    }
                });
            }
            //显示配置
            function write() {
                var opts = $("#options");
                opts.html("");
                for (var key in kvp) {
                    opts.append('<span class="bold">'+key + '</span>：<span class="red">'+kvp[key]+'</span><input type="hidden" name="' + key + '" value="' + kvp[key] + '"/><br/>')
                }
            }
        </script>
        <div id="login_info" style="width: 260px;">
            <p class="input-group">
                <span class="input-group-addon"><span>帐号</span></span>
                <input type="text" class="form-control" name="id" id="username" placeholder="<%=GetConfig("idIntro") %>" />
            </p>
            <p class="input-group">
                <span class="input-group-addon"><span>密码</span></span>
                <input type="password" class="form-control" name="pwd" id="password" placeholder="<%=GetConfig("pwdIntro") %>" />
            </p>
            <p class="text-center">
                <button type="button" class="btn btn-success default" onclick="login()">登录</button>
            </p>
        </div>
    </div>
    <%if (islogin)
      {%>
    <div>
        <h2>说明</h2>
        <p>此升级只为升级配置文件的版本，仅对旧配置文件补充未赋值的新配置项，所以旧配置不会改变且新配置也不会生效。</p>
        <div class="line"></div>
        <h2>配置明细</h2>
        <div id="options"></div>
        <div class="line"></div>
        <div class="footer">
            <div class="btn-group act_save" style="display:none;">
                <button type="button" class="btn btn-info" onclick="submit();">升级配置文件</button>
            </div>
            <script>
                function submit() {
                    uni.confirm("是否升级配置文件？", function () {
                        update();
                    });
                }
            </script>
        </div>
    </div>
    <%} %>
</body>
</html>
