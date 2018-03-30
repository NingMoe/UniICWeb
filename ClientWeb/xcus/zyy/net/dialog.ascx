<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dialog.ascx.cs" Inherits="WebUserControl" %>

<!-- 全站窗口模块 -->
<!-- 登录表单 -->
<div id="dlg_login" title="登录" class="dialog">
    <form name='login' onsubmit="return false;" class="dlg">
        <!-- 登录表单 begin -->
        <table>
            <tr>
                <th>帐号</th>
                <td>
                    <input name="id" id="id" type="text" class="input_txt id" /></td><td style="width:40px;">
                    <span class="error reg_number_msg" style="display: none;">不存在</span></td>
            </tr>
            <tr>
                <th>密码</th>
                <td>
                    <input name="pwd" id="pwd" type="password" onblur="blurPassword(this)" onfocus="focusPassword()" class="input_txt" style="display: none;">
                    <input name="pwd_text" id="pwd_text" type="text" onfocus="focusPasswordText()" class="input_txt" /></td><td></td>
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
<!-- 激活 -->
<div id="regdialog" title="激活" class="dialog">
    <form name='act' onsubmit="return false;" class="dlg">
        <!-- 激活表单 begin -->
        <table>
            <tr>
                <th>数字化校园平台帐号</th>
                <td>
                    <input name="id" id="rid" type="text" class="input_txt id" /></td>
                <td class="msg"><span class="red">*</span><span class="error reg_number_msg" style="display: none;">不存在</span></td>
            </tr>
            <tr>
                <th>数字化校园平台密码</th>
                <td>
                    <input name="pwd" id="rpwd" type="password" class="input_txt" /></td>
                <td class="msg"><span class="red">*</span>
                </td>
            </tr>
            <tr>
                <th>手机(必填)</th>
                <td>
                    <input name="phone" type="text" class="input_txt" /></td>
                <td class="msg"><span class="red">*</span><span class="error" id="reg_mobile_msg" style="display: none;">格式有误</span></td>
            </tr>
            <tr>
                <th>邮箱(必填)</th>
                <td>
                    <input name="email" type="text" class="input_txt" /></td>
                <td class="msg"><span class="red">*</span><span class="error" id="reg_mail_msg" style="display: none;">格式有误</span></td>
            </tr>
        </table>
        <div class="submitarea clear">
            <input type="submit" class="input_submit button" value="激活" />
        </div>
        <div class="fail">
            <p id="reg_msg">
                <!--此卡号已处于激活状态，请勿重复操作-->
            </p>
        </div>
    </form>
</div>
<!-- 新建实验项目 -->
<div id="addrtdialog" title="新建实验项目" class="dialog">
    <form name='addrt' id="addrtForm" onsubmit="return false;" class="dlg">
        <!-- 激活表单 begin -->
        <table style="vertical-align: middle; margin: 0 20px;">
            <tr>
                <th>项目名称：</th>
                <td>
                    <input id="new_rtname" name="rtname" type="text" class="input_txt" /></td>
            </tr>
            <tr>
                <th>项目级别：</th>
                <td>
                    <select class="rt_level input_txt" name="rt_level">
                        <option value="4096">其它</option>
                        <option value="4">校级</option>
                        <option value="3">厅局级</option>
                        <option value="2">省部级</option>
                        <option value="1">国家级</option>
                    </select></td>
            </tr>
            <tr>
                <th>添加成员：</th>
                <td>
                    <input id="rtmember" name="rtmember" type="text" class="input_txt rtmember" value="请输入帐号" onclick="if (this.value == '请输入帐号') { this.value = '' }" onblur="if(this.value=='') {this.value='请输入帐号';}" />
                    <a id="addmember" class="addmember button">添加</a></td>
            </tr>
            <tr>
                <th>成员列表：</th>
                <td>
                    <ul id="memList" class="ul_member memList">
                    </ul>
                </td>
            </tr>
        </table>
        <div class="submitarea clear" style="text-align: center;">
            <a id="addrt" class="detail button">提交</a>
        </div>
        <div class="grey" style="text-align: center; padding: 3px;">
            可暂不添加成员
        </div>
    </form>
</div>
<!-- 搜索导师 -->
<div id="gettutordialog" title="搜索导师" class="dialog">
    <form id='gettutor' onsubmit="return false;" class="dlg">
        <table>
            <tr>
                <th>姓名</th>
                <td>
                    <input name="tutor" id="dlg_tutor_name" type="text" class="input_txt" value="姓名关键字" onclick="if (this.value == '姓名关键字') { this.value = '' }" onblur="if(this.value=='') {this.value='姓名关键字';}" />
                </td>
            </tr>
        </table>
        <table>
            <tbody id="dlg_tutor_list">
            </tbody>
        </table>
        <div style="margin-top: 30px; text-align: center;">
            <input type="submit" class="button" id="get_tutor" value="搜索" />
        </div>
        <div style="text-align: center; color: red;">
            <p id="dlg_tutor_msg">
                <!--失败提示-->
            </p>
        </div>
    </form>
</div>
<script>
    //登录后指定导航页面
    var HrefPage = "";
    function dlgReload() {
        if (HrefPage == "") {
            location.reload();
        }
        else {
            location = HrefPage;
        }
    }
    $(function () {
        //登录
        $('form[name=login]').submit(function () {
            pro.j.lg.fLogin("login", $(this), function (rlt) {
                if (rlt.ret == 1) {
                    dlgReload();
                }
                else if (rlt.ret == 2) {
                    uni.msgBox(rlt.msg, "", function () {
                        $("#regdialog .id").val($("#dlg_login .id").val());
                        $("#regdialog").dialog('open');
                    });
                    $("#dlg_login").dialog('close');
                }
            },
            function (rlt) {
                document.getElementById("login_msg").innerText = rlt.msg;
            });
        });

        $("#dlg_login").dialog({
            width: 383, autoOpen: false, modal: true, minHeight: 270, bgiframe: true, open: function () {
                var dig = document.getElementById('commanResvStat'); if (dig != null) dig.style.display = 'none';
            }, beforeclose: function () {
                HrefPage = "";
                var dig = document.getElementById('commanResvStat'); if (dig != null) dig.style.display = 'block';
            }
        });
        $("#dlg_login .close").click(function () {
            $("#dlg_login").dialog('close');
            return false;
        });

        $("#dlg_login .reg").click(function () {
            $("#dlg_login").dialog('close');
            $("#regdialog").dialog('open');
            return false;
        });
        //激活
        $('form[name=act]').submit(function () {
            var value = $('input[name=email]', this).val();
            if (!CheckEmailBox(value, $(this)))
                return false;
            value = $('input[name=phone]', this).val();
            if (!CheckPhoneBox(value, $(this)))
                return false;
            pro.j.lg.fLogin("act", $(this), function (rlt) {
                uni.msgBoxR("激活成功！");
            },
            function (rlt) {
                document.getElementById("reg_msg").innerText = rlt.msg;
            });
        });

        $("#regdialog").dialog({ width: 480, autoOpen: false, modal: true, minHeight: 302, bgiframe: true });
        $("#regdialog .close").click(function () {
            $("#regdialog").dialog('close');
            return false;
        });

        $("form[name=act] input[name=phone]").change(function () {
            CheckPhoneBox(this.value, $("form[name=act]"));
        });

        $("form[name=act] input[name=email]").change(function () {
            CheckEmailBox(this.value, $("form[name=act]"));
        });

        $("form.dlg input[name=id]").change(function () {
            var msg = $(this).parent().parent().find('.reg_number_msg')[0];
            pro.j.lg.isExist(this.value, function (rlt) {
                msg.style.display = 'none';
            }, function () {
                msg.style.display = '';
            });
        });

        $(".login a").click(function () {
            $("#dlg_login").dialog('open');
            return false;
        });
        $(".active a").click(function () {
            $("#regdialog").dialog('open');

            return false;
        });

        //获取导师
        pro.d.acc.srchTutor = function (sel) {
            var dlgsrchT = $("#gettutordialog");
            $("#gettutordialog").dialog({ minWidth: 300, autoOpen: false, modal: true, minHeight: 200, bgiframe: true });
            $("#gettutordialog .close").click(function () {
                $("#gettutordialog").dialog("close");
            });
            $("#gettutordialog form:first").submit(function () {
                var name = $("#dlg_tutor_name").val();
                pro.j.acc.getTutorByName(name, function (rlt) {
                    var list = rlt.data;
                    var list_str = "";
                    $(list).each(function () {
                        list_str += "<tr><td>" + this.id + "</td><td class='tru_name'>" + this.name + "</td><td>(" + this.dept +
                            ")</td><td><a class='sel click' accno='" + this.accno + "' name='" + this.name + "'>选择</a></td></tr>";
                    });
                    $("#dlg_tutor_list").html(list_str);
                    $("#dlg_tutor_list a.sel").click(function () {
                        pthis = $(this);
                        sel(pthis.attr("accno"), pthis.attr("name"), dlgsrchT);
                    });
                },
                function (rlt) {
                    $("#dlg_tutor_msg").html(rlt.msg);
                });
            });
            $("#gettutordialog").dialog("open");
        }


        //项目相关
        $("#addrtdialog").dialog({ width: 435, autoOpen: false, modal: true, minHeight: 302, bgiframe: true });
        $("#addrt").click(function () {
            var rtname = $("#new_rtname").val();
            if (rtname == "") {
                uni.msgBox("请输入项目名称！");
                return false;
            }
            var level = $("#addrtdialog select.rt_level").val();
            var list = "";
            $("#memList span[name=memid]").each(function () {
                if (!$(this).is(':hidden')) {
                    list += $(this).html() + ',';
                }
            });
            pro.j.rtest.creRTest(rtname, level, "0", "", list, function () {//未处理问题
                uni.msgBoxR("创建项目成功！");
            });
        });
        $("#addmember").click(function () {
            var id = $(this).parent().find("input.rtmember").val();
            if (id == "") {
                uni.msgBox("请输入成员帐号！");
                return false;
            }
            pro.j.acc.getAccById(id, function (rlt) {
                var list = $("#memList").html();
                var acc = (rlt.data)[0];
                list += "<li><span name='memid'>" + id + "</span>|<span>" + acc.name + "</span>|<a href='#' onclick='$(this).parent().hide();return false;'>删除</a></li>";
                $("#memList").html(list);
            });
        });
        //按钮事件
        $("a.btn_activate").click(function () {
            $("#regdialog").dialog('open');
            return false;
        });
        $("a.reg").click(function () {
            $("#rid")[0].value = $("#id")[0].value;
            $("#rpwd")[0].value = $("#pwd")[0].value;
            $("#regdialog").dialog('open');
            return false;
        });
        $("a.btn_login").click(function () {
            $("#dlg_login").dialog('open');
            return false;
        });
    });

    function CheckEmailBox(taget, table) {
        var IsReady = uni.ckEmail(taget);

        if (IsReady)
            table.find('#reg_mail_msg').hide();
        else
            table.find('#reg_mail_msg').show();

        return IsReady;

    }
    function CheckPhoneBox(taget, table) {
        var IsReady = (taget.length==11);//uni.ckMobile(taget);

        if (IsReady)
            table.find('#reg_mobile_msg').hide();
        else
            table.find('#reg_mobile_msg').show();

        return IsReady;
    }
    function blurPassword(temp) {
        //是否有输入密码
        if (uni.isNull(temp.value)) {
            document.getElementById('pwd').style.display = "none";
            document.getElementById('pwd_text').style.display = "inline-block";
        } else {
            document.getElementById('pwd').style.color = "#666";
            document.getElementById('pwd').style.opacity = "0.75";//不透明度
        }
    }
    function focusPassword() {
        document.getElementById('pwd').style.color = "#000";
        document.getElementById('pwd').style.opacity = "1";
    }

    function focusPasswordText() {
        document.getElementById('pwd_text').style.display = "none";
        document.getElementById('pwd').style.display = "inline-block";
        document.getElementById('pwd').focus();
    }

</script>
