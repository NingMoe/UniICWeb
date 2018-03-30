<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dlg_lg.ascx.cs" Inherits="ClientWeb_xcus_bsd_xl_net_dlg_lg" %>
<div style="display: none;">
    <style type="text/css">
        .dlg_lg_validate { }
        .dlg_lg_validate .intro { color: #666; font-family: 微软雅黑; font-weight: bold; }
    </style>
    <!--注册账户-->
    <div id="dlg_regist_acc">
        <form class="dlg_lg_validate" onsubmit="return false;">
            <p class="intro"></p>
            <div>
                <table style="margin: 15px 5px 0 5px;">
                    <tbody>
                        <tr>
                            <td>帐号 </td>
                            <td>
                                <input type="text" id="regist_acc_id" maxlength="16" class=" validate[required,maxSize[16]],custom[onlyLetterNumber],ajax[ajaxIdIsExistFail]]" name="id" /></td>
                            <td><span style="color: red;">*</span>&nbsp数字与字母，最多16位</td>
                        </tr>
                        <tr>
                            <td>密码 </td>
                            <td>
                                <input type="password" id="regist_acc_pwd" maxlength="14" class=" validate[required,maxSize[14]]" name="pwd" /></td>
                            <td><span style="color: red;">*</span>&nbsp最多14位</td>
                        </tr>
                        <tr>
                            <td>密码确认 &nbsp</td>
                            <td>
                                <input type="password" id="regist_acc_pwd_r" class=" validate[required,equals[regist_acc_pwd]]" /></td>
                            <td><span style="color: red;">*</span></td>
                        </tr>
                        <tr>
                            <td>身份证号 &nbsp</td>
                            <td>
                                <input type="text" id="regist_acc_id_card" class=" validate[required,funcCall[chkIDCard]]" name="id_card" /></td>
                            <td><span style="color: red;">*</span>&nbsp必须真实，字母大写</td>
                        </tr>
                        <tr>
                            <td>真实姓名 &nbsp</td>
                            <td>
                                <input type="text" id="regist_acc_name" maxlength="14" class=" validate[required,maxSize[14]]" name="name" /></td>
                            <td><span style="color: red;">*</span></td>
                        </tr>
                        <tr>
                            <td>单位 </td>
                            <td>
                                <input type="text" id="regist_acc_dept" maxlength="18" class=" validate[required,maxSize[18]]" name="dept" /></td>
                            <td><span style="color: red;">*</span></td>
                        </tr>
                        <tr>
                            <td>部门 </td>
                            <td>
                                <input type="text" id="regist_acc_cls" maxlength="18" class=" validate[required,maxSize[18]]" name="cls" /></td>
                            <td><span style="color: red;">*</span></td>
                        </tr>
                        <tr>
                            <td>手机 </td>
                            <td>
                                <input type="text" id="regist_acc_phone" class=" validate[required,custom[phone]]" name="phone" /></td>
                            <td><span style="color: red;">*</span>&nbsp短信通知需要</td>
                        </tr>
                        <tr>
                            <td>邮箱 </td>
                            <td>
                                <input type="text" id="regist_acc_email" class=" validate[required,custom[email]]" name="email" /></td>
                            <td><span style="color: red;">*</span>&nbsp邮件通知需要</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <script type="text/javascript">
            </script>
        </form>
    </div>
    <!-- 登录表单 -->
    <div id="dlg_login" title="登录" class="dialog">
        <form name='login' onsubmit="return false;" class="dlg">
            <table>
                <tr>
                    <td>帐号 </td>
                    <td>
                        <input name="id" id="lg_id" type="text" class="input_txt id" /></td>
                    <td style="width: 40px;"></td>
                </tr>
                <tr>
                    <td>密码 </td>
                    <td>
                        <input name="pwd" id="lg_pwd" type="password" class="input_txt pwd">
                    <td></td>
                </tr>
            </table>
            <div class="submitarea clear">
                <input type="submit" class="button" value="登录" /><a class="reg button">新用户激活</a>
            </div>
            <!-- 登录表单 end-->
            <div class="fail">
                <p id="login_msg" style="color: red;">
                    <!--登录失败提示-->
                </p>
            </div>
        </form>
    </div>
    <!--激活账户-->
    <div id="dlg_act_acc">
        <form class="dlg_lg_validate" onsubmit="return false;">
            <p class="intro"></p>
            <div>
                <table style="margin: 15px 5px 0 15px;">
                    <tbody>
                        <tr>
                            <td>帐号 </td>
                            <td>
                                <input type="text" maxlength="16" class=" validate[required,maxSize[16]],custom[onlyLetterNumber],ajax[ajaxIdIsExistOk]]" name="id" /></td>
                            <td><span style="color: red;">*</span></td>
                        </tr>
                        <tr>
                            <td>密码 </td>
                            <td>
                                <input type="password" name="pwd" class="pwd" /></td>
                            <td><span style="color: red;">*</span></td>
                        </tr>
                        <tr>
                            <td>手机 </td>
                            <td>
                                <input type="text" class=" validate[required,custom[phone]]" name="phone" /></td>
                            <td><span style="color: red;">*</span>&nbsp短信通知需要</td>
                        </tr>
                        <tr>
                            <td>邮箱 </td>
                            <td>
                                <input type="text" class=" validate[required,custom[email]]" name="email" /></td>
                            <td><span style="color: red;">*</span>&nbsp邮件通知需要</td>
                        </tr>
                        
                    </tbody>
                </table>
            </div>
            <script type="text/javascript">
            </script>
        </form>
    </div>
</div>
<script type="text/javascript">
    //注册账户
    pro.d.lg.registAcc = function (suc, fail, intro) {
        var dft = function () {
            uni.msgBoxR("注册成功！");
        }
        var dlg = $("#dlg_regist_acc");
        var str = "▪ " + (intro || "请根据提示信息完成注册。");
        $(".intro", dlg).html(str);
        $("form:first", dlg).validationEngine({
            onValidationComplete: function (f, ret) {
                if (ret) {
                    pro.j.lg.fLogin("regist_acc", f, suc || dft, fail);
                }
            }
        });
        uni.dlg(dlg, "用户注册", 420, 400, function (d, f) {
            $(f).submit();
        });
    }
    //用户激活
    pro.d.lg.actAcc = function (suc, fail, intro) {
        var dft = function () {
            uni.msgBoxR("激活成功！");
        }
        var dlg = $("#dlg_act_acc");
        var str = "▪ " + (intro || "预约过程中，系统将通过您提供的联系方式发送反馈信息。");
        $(".intro", dlg).html(str);
        $("form:first", dlg).validationEngine({
            onValidationComplete: function (f, ret) {
                if (ret) {
                    pro.j.lg.fLogin("act", f, suc || dft, fail);
                }
            }
        });
        uni.dlg(dlg, "用户激活", 420, 400, function (d, f) {
            $(f).submit();
        });
    }
    //登录
    $('form[name=login]').submit(function () {
        pro.j.lg.fLogin("login", $(this), function (rlt) {
            if (rlt.ret == 1) {
                location.reload();
            }
            else if (rlt.ret == 2) {
                uni.msgBox(rlt.msg, "", function () {
                    $("#dlg_act_acc input[name=id]").val($("#dlg_login .id").val());
                    pro.d.lg.actAcc();
                });
                $("#dlg_login").dialog('close');
            }
        });
    });
    $(function () {
        $("#dlg_login").dialog({ width: 360, autoOpen: false, modal: true, minHeight: 240, bgiframe: true });
        $("#dlg_login .close").click(function () {
            $("#dlg_login").dialog('close');
            return false;
        });
        $("#dlg_login .reg").click(function () {
            $("#dlg_login").dialog('close');
            pro.d.lg.actAcc();
            return false;
        });
        //类命令
        $(".login").click(function () {
            $("#dlg_login").dialog('open');
            return false;
        });
        $("a.active").click(function () {
            pro.d.lg.actAcc();
        });
    });
</script>
