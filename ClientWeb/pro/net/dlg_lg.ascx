<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dlg_lg.ascx.cs" Inherits="ClientWeb_pro_net_dlg_lg" %>
<div style="display: none;">
    <!--注册账户-->
    <div id="dlg_regist_acc" class="dialog">
        <form class="dlg_lg_validate" onsubmit="return false;">
            <p class="intro tag"></p>
            <div>
                <table style="margin: 20px 5px 0 5px;">
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
    <!--激活账户-->
    <div id="dlg_act_acc" class="dialog" style="overflow: visible;">
        <form class="dlg_lg_validate" onsubmit="return false;">
            <p class="intro tag"></p>
            <div class="list">
                <table>
                    <tbody>
                        <tr>
                            <td>帐号 </td>
                            <td>
                                <input type="text" maxlength="16" class=" validate[required,maxSize[16]],ajax[ajaxIdIsRegisterOK]]" name="id" /></td>
                            <td><span style="color: red;">*</span><%=GetConfig("idIntro")%></td>
                        </tr>
                        <tr>
                            <td>密码 </td>
                            <td>
                                <input type="password" name="pwd" /></td>
                            <td><span style="color: red;">*</span><%=GetConfig("pwdIntro")%></td>
                        </tr>
                        <tr>
                            <td>手机 </td>
                            <td>
                                <input type="text" class=" validate[<%=msgMustAct %>custom[phone]]" name="phone" /></td>
                            <td>
                                <%if (msgMustAct != "")
                                    {%>
                                <span style="color: <%=msgFontColor%>;">*</span>
                                <%} %>
                                &nbsp<%=GetConfig("msgIntro")%></td>
                        </tr>
                        <tr>
                            <td>邮箱 </td>
                            <td>
                                <input type="text" class=" validate[<%=emailMustAct %>custom[email]]" name="email" /></td>
                            <td>
                                 <%if (emailMustAct != "")
                                    {%>
                                <span style="color: <%=emailFontColor%>;">*</span>
                                    <%} %>
                                &nbsp邮件通知需要</td>
                        </tr>
                        <%if (GetConfig("show2code") == "1")
                            {%>
                         <tr>
                           
                            <td colspan="3" style="text-align:center">
                                <div style="margin:auto 10px">
                                  <img style="width:100px" src="~/ClientWeb/pro/dft/2code.jpg" alt="公众号图片" runat="server" />
                                    <div>
                                        绑定微信号，接收微信通知
                                    </div>
                                    </div>
                               </td>
                        </tr>
                        <%} %>
                    </tbody>
                </table>
            </div>
            <script type="text/javascript">
            </script>
        </form>
    </div>
    <div id="dlg_act_acc_simple" class="dialog" style="overflow: visible;">
        <form class="dlg_lg_validate" onsubmit="return false;">
            <div class="list">
                <table>
                    <tbody>
                        <tr>
                            <td>手机 </td>
                            <td>
                                <input type="text" class="phone validate[<%=msgMustAct %>custom[phone]]" name="phone" /></td>
                            <td>
                                 <%if (msgMustAct != "")
                                    {%>
                                <span style="color: <%=msgFontColor%>;">*</span>
                                <%} %>
                                &nbsp<%=GetConfig("msgIntro")%></td>
                        </tr>
                        <tr>
                            <td>邮箱 </td>
                            <td>
                                <input type="text" class="email validate[required,custom[email]]" name="email" /></td>
                            <td><span style="color: red;">*</span>&nbsp邮件通知需要</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </form>
    </div>
    <!--账户登录-->
    <div id="dlg_login" class="dialog">
        <form class="dlg_lg_validate" onsubmit="return false;">
            <div style="min-width: 300px;" class="lg_form">
                <div class="intro tag"></div>
                <div class="list">
                    <table>
                        <tbody>
                            <tr>
                                <td><span class="uni_trans">帐号</span> </td>
                                <td>
                                    <input type="text" maxlength="18" name="id" /></td>
                                <td><span class="id_intro hint_intro"><%=GetConfig("idIntro")%></span></td>
                            </tr>
                            <tr>
                                <td><span class="uni_trans">密码</span> </td>
                                <td>
                                    <input type="password" name="pwd" /></td>
                                <td><span class="pwd_intro hint_intro"><%=GetConfig("pwdIntro")%></span></td>
                            </tr>
                            <%if (GetConfig("mustVerif") == "1")
                              { %>
                            <tr>
                                <td>验证码</td>
                                <td>
                                    <input type="text" name="number" class="verif_number" style="width: 60px;" maxlength="4" />
                                    <img class="verif_img" src="<%=this.ResolveClientUrl("~/ClientWeb/") %>pro/page/image.aspx" style="vertical-align: middle" alt="看不清，点击更换" onclick="this.src=this.src+'?'" />
                                </td>
                            </tr>
                            <%} %>
                            <%if (GetConfig("savePWD") == "1")
                              { %>
                            <tr>
                                <td></td>
                                <td>
                                    <label style="cursor: pointer;">
                                        <input type="checkbox" class="save_pwd_ck" />
                                        <%=Translate("记住密码")%></label>
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>
                </div>
                <div class="operate">
                    <input type="button" class="button default btn btn-info" value="<%=Translate("登录") %>" priority="9" onclick="$(this).parents('form:first').submit();" />
                    <input type="button" class="user_active button btn btn-warning" style="display: <%=mustAct?"":"none"%>" value="<%=Translate("新用户激活") %>" />
                </div>
            </div>

            <div class="intro_detail" style="float: left; width: 300px; border-left: 1px dotted #ddd; padding-left: 10px; margin-top: 5px;">
                <%--↓↓↓↓↓↓↓↓↓↓这个是登录详细补充信息--%>
                <%--                <strong>What is username and password?</strong><br>
<span style="color:grey;">(e.g. san.zhang11@xjtlu.edu.cn)<br>
Username is email prefix, Password is email password.<br>
e.g. username:san.zhang11</span>
                <br>
<br>
                                <strong>你的用户名和密码是什么？</strong><br>
<span style="color:grey;">例如：san.zhang11@xjtlu.edu.cn<br>
登录名是邮箱前缀san.zhang11, 密码是你的邮箱密码.</span>--%>
                <%--↑↑↑↑↑↑↑↑↑--%>
            </div>

        </form>
    </div>
</div>
<style>
.ui-dialog .ui-dialog-buttonpane .ui-dialog-buttonset {
    float:none;
	text-align: center;
}
</style>
<script type="text/javascript">

    //注册账户
    pro.d.lg.registAcc = function (suc, fail, intro) {
        var dlg = $("#dlg_regist_acc");
        var str = "▪ " + (intro || "请根据提示信息完成注册。");
        $(".intro", dlg).html(str);
        
        $("form:first", dlg).validationEngine({
            onValidationComplete: function (f, ret) {
                if (ret) {
                    pro.j.lg.fLogin("regist_acc", f, suc, fail);
                }
            }
        });
       
        uni.dlg(dlg, "用户注册", 460, 420, function (d, f) {
            $(f).submit();
        });
    }
    //用户激活
    pro.d.lg.actAcc = function (suc, fail, intro) {
        var dlg = $("#dlg_act_acc");
        
        var str = intro || "预约过程中，系统将通过您提供的联系方式发送反馈信息。";
        $(".intro", dlg).html(str);
        
        $("form:first", dlg).validationEngine({
            onValidationComplete: function (f, ret) {
                if (ret) {
                    pro.j.lg.fLogin("act", f, suc || function () { uni.msgBox("激活成功"); location.reload(); }, fail);//全页面刷新
                }
            }
        });
       
       
        uni.dlg(dlg, "用户激活", 460, 400, function (d, f) {
            $(f).submit();
        });
    }
    //用户登录
    pro.d.lg.login = function (suc, fail, intro) {
        var dlg = $("#dlg_login");
        //侧边提示
        var width = 420;
        var detail = $(".intro_detail");
        if ($.trim(detail.html()) == "") { detail.hide(); $(".hint_intro", dlg).show(); }
        else { detail.show(); width += 240; $(".lg_form", dlg).css("float", "left"); $(".hint_intro", dlg).hide(); }
        //头部提示
        var str = intro || "<%=mustAct?"新用户登录请先激活。":""%>";
            $(".intro", dlg).html(str);
            //保存密码
            var isSV = "<%=GetConfig("savePWD")%>" == "1";
            var svk = $(".save_pwd_ck", dlg);
            if ($.cookie("is_save_pwd") == "true") {
                svk.attr("checked", "checked");
            }
            var ipt_id = $("input[name=id]", dlg);
            var ipt_pwd = $("input[name=pwd]", dlg);
            if (isSV) {
                if (svk.is(':checked')) {
                    ipt_id.val($.cookie("pc_user") || "");
                    ipt_pwd.val($.cookie("pc_pwd") || "");
                }
                else {
                    $.cookie("pc_user", null);
                    $.cookie("pc_pwd", null);
                }
            }
            //事件注册
            if (!dlg.hasClass("dlg_inited")) {
                debugger;
                dlg.addClass("dlg_inited");
                if (isSV) {
                    svk.change(function () {
                        if (svk.is(':checked')) {
                            $.cookie("is_save_pwd", "true", { expires: 30 });
                        }
                        else {
                            $.cookie("is_save_pwd", null);
                        }
                    });
                }
                $("form:first", dlg).validationEngine({
                    onValidationComplete: function (f, ret) {
                        if (ret) {
                            var verif = $(".verif_number", f)
                            pro.j.lg.fLogin(verif.length > 0 ? "dlogin" : "login", f, function (rlt) {
                                if (rlt.ret == 2)
                                    uni.msgBox(rlt.msg, "", function () { location.reload(); })//$(".user_active", dlg).trigger("click"); 
                                else if (rlt.ret == 3) {
                                    uni.msgBox("微信未绑定", "", function () { location.reload(); });//微信绑定扫描二维码的特殊性，只能重登录检查
                                }
                                else {
                                    if (isSV && svk.is(':checked')) {
                                        $.cookie("pc_user", ipt_id.val(), { expires: 30 });
                                        $.cookie("pc_pwd", ipt_pwd.val(), { expires: 365 });
                                    }
                                    if (typeof (suc) == "function") {
                                        suc(rlt, dlg);
                                    }
                                    else
                                        location.reload();
                                }
                            }, function (rlt) {
                                if (verif.length > 0) {
                                    var img = $(".verif_img", f)[0];
                                    img.src = img.src + "?";
                                    verif.val("");
                                }
                                if (fail) fail(rlt);
                                else uni.msgBox(rlt.msg);
                            });
                        }
                    }
                });
            }
            if ($(".verif_img", dlg).length > 0) {//刷新验证码
                var img = $(".verif_img", dlg)[0];
                img.src = img.src + "?";
            }
            uni.dlg(dlg, "用户登录", width, 200);
        }
        $(function () {
            $("#dlg_login .user_active").click(function () {
                $("#dlg_act_acc input[name=id]").val($("#dlg_login input[name=id]").val());
                $("#dlg_act_acc input[name=pwd]").val($("#dlg_login input[name=pwd]").val());
            });
            //按钮事件
            $("a.login,span.login").click(function () {
                pro.d.lg.login();
            });
            $("a.user_active,span.user_active,input.user_active").click(function () {
                debugger;
                pro.d.lg.actAcc();
            });
            //检查微信绑定
            if ("<%=GetConfig("bindWechat")%>" == "1" && pro.isLogin() && !pro.acc.msn) {
            var qr = '<%=GetConfig("wechatQrCode")%>';
            var img = "<span>请使用微信扫二维码，绑定微信</span><div class='dft_qr_code'><img alt='' style='width:200px;height:200px;' src='" + qr + (qr.indexOf('?') < 0 ? "?" : "&") + "ID=" + pro.acc.id + "&session=<%=m_Request.m_UniDCom.SessionID%>'/></div>";
            uni.msgBox(img, "微信绑定", function () { pro.j.lg.initAcc(function () { if (!pro.acc.msn) { location.reload(); } else { uni.msgBox("绑定微信成功"); } }); });
        }
            //检查联系方式信息
            var vmsgInfo = false;
            var vMsgReq="<%=msgFontColor%>";
            if (vMsgReq == "red" && !pro.acc.email)
            {
                vmsgInfo = true;
            }

            if ("<%=mustAct%>".toLowerCase() == "true" && pro.isLogin() && (vmsgInfo)) {
            var dlg = $("#dlg_act_acc_simple");
            var phone = dlg.find(".phone");
            var email = dlg.find(".email");
            phone.val(pro.acc.phone || "");
            email.val(pro.acc.email || "");
            $("form:first", dlg).validationEngine({
                onValidationComplete: function (f, ret) {
                    if (ret) {
                        pro.j.acc.upContact(phone.val(), email.val(), function () { uni.msgBox("激活成功", "", function () { location.reload(); }); });
                    }
                }
            });
            uni.dlg(dlg, "用户激活", 420, 200, function (d, f) {
                $(f).submit();
            }, null, function () { pro.j.lg.logout(); });
        }
    });
</script>
