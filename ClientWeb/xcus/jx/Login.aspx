<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="ClientWeb_xcus_jx_Login" %>

<%@ Register TagPrefix="Uni" TagName="login" Src="~/ClientWeb/pro/net/dlg_lg.ascx" %>
<%@ Register TagPrefix="Uni" TagName="include" Src="net/include.ascx" %>
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
        <style>
        .panel { width:657px;margin-left:345px;}
        </style>
    <![endif]-->
    <style>
        html { background: url(theme/images/main_bg.jpg) no-repeat center center fixed; -webkit-background-size: cover; -moz-background-size: cover; -o-background-size: cover; background-size: cover; }
        body { padding-top: 20px; font-size: 16px; background: transparent; }
        .panel { background-color: rgba(255, 255, 255, 0.9); }
        .margin-base-vertical { margin: 40px 0; }
        #login_info input[type=text], #login_info input[type=password] { font-size: 16px; line-height: 35px; }
    </style>
    <script>
        function login() {
            var id = $.trim($("#username").val());
            var pwd = $("#password").val();
            if (id.length == 0) {
                uni.msgBox("请输入帐号和密码！");
                return;
            }
            var req = uni.getReq();
            var role = req["role"];
            if (role) {
                if (role == "teach") role = "teacher";
                else if (role == "stu") role = "student";
                else role = "auto";
            }
            else {
                role = $(".login_role:checked").val() || "auto";
            }
            pro.j.lg.login(id, pwd, function (rlt) {
                if (rlt.ret == 2) {
                    uni.msgBox(rlt.msg, "", function () {
                        actAcc();
                    })
                }
                else {
                    var acc = rlt.data;
                    if ((parseInt(acc.role) & 4) > 0)
                        location.href = "TestPlan.aspx";
                    else
                        location.href = "TestItem.aspx";
                }
            }, role);
        }
        function actAcc() {
            var dlg = $("#dlg_act_acc");
            var id = $.trim($("#username").val());
            var pwd = $("#password").val();
            $("input[name=id]", dlg).val(id);
            $("input[name=pwd]", dlg).val(pwd);
            pro.d.lg.actAcc(function () {
                uni.msgBox("激活成功！", "", function () {
                    location.href = "TestPlan.aspx";
                });
            });
        }
    </script>
</head>
<body>
    <Uni:login runat="server" />
    <div class="container">
        <div class="row">
            <div class="col col_6 panel panel-default" style="margin-left: 25%;">
                <h1 class="margin-base-vertical">教学预约<%=string.IsNullOrEmpty(role)?"":(role=="teacher"?"教师":"学生") %>登录</h1>
                <div class="margin-base-vertical" id="login_info">
                    <p class="input-group">
                        <span class="input-group-addon"><span>帐号</span></span>
                        <input type="text" class="form-control input-lg" name="id" id="username" placeholder="<%=GetConfig("idIntro") %>" />
                    </p>
                    <p class="input-group">
                        <span class="input-group-addon"><span>密码</span></span>
                        <input type="password" class="form-control input-lg" name="pwd" id="password" placeholder="<%=GetConfig("pwdIntro") %>" />
                    </p>
                    <%if (GetConfig("autoJudgeRole") != "1" && string.IsNullOrEmpty(role))
                      { %>
                    <div class="text-right" style="font-size: 14px; padding-right: 10px;">
                        <label class="click">
                            <input type="radio" name="role" class="login_role" value="student" />学生登录</label>
                        &nbsp;&nbsp;
                    <label class="click">
                        <input type="radio" name="role" class="login_role" value="teacher" checked="checked" />教师登录</label>
                    </div>
                    <%} %>
                    <p class="help-block text-center"><small>为能即时联系用户，联系信息不完整请点击激活，以补充联系信息。</small></p>
                    <p class="text-center">
                        <button type="button" class="btn btn-success btn-lg default" onclick="login()">登录</button>
                        <button type="button" class="btn btn-default btn-lg" onclick="actAcc()">激活</button>
                    </p>
                    <p class="text-center" style="font-size: 14px;">
                        <%if (GetConfig("visitMode") != "login"){ %>[ <a href="default.aspx">返回首页</a> ] <%} %>[ <a href="Art.aspx?type=other&id=help">使用帮助</a> ]<%if (GetConfig("mShowJumpManage")=="1"){ %> [ <a href="../../../pages/default.aspx">转到管理端</a> ]<%} %>
                    </p>
                </div>
                <div class="margin-base-vertical text-center">
                    <small class="text-muted">版权所有：</small><small class="text-primary"><%=GetConfig("SysAutoSchoolName") %></small>
                </div>

            </div>
            <!-- //main content -->
        </div>
    </div>

    <!-- //row -->
</body>
</html>
