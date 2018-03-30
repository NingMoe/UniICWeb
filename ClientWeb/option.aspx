<%@ Page Language="C#" AutoEventWireup="true" CodeFile="option.aspx.cs" Inherits="ClientWeb_option" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>应用程序配置</title>
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
        input.long { width: 320px; }
        h3 { color: #999; border-bottom: 1px dashed #ccc; margin-left: 10px; padding-bottom: 3px; }
        .dashed { margin: 10px; border-bottom: 1px dashed #ccc; }
    </style>
</head>
<body style="padding-left: 15px; padding-bottom: 100px;">
    <h2>前端系统配置</h2>
    <div id="panel_login" style="display: <%=islogin?"none":"" %>">
        <h3>用户登录，启用系统请先完成基础配置，仅支持超级管理员登录。</h3>
        <script>
            $(function () {
                //初始化
                var req = uni.getReq();
                var noread = $("#no_need_read").click(function () {
                    var p = $(this);
                    if (p.is(":checked"))
                        location.href = location.href.split('?')[0] + "?use_dft=true";
                    else
                        location.href = location.href.split('?')[0];
                });
                if (req["use_dft"] != "true") { init(); noread.removeAttr("checked"); }
                else noread.attr("checked", "checked");
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
            function uploadFile(para, title, suc) {
                if (!para) para = {};
                var dlg = $($("#upload_dlg").html());
                var btn = dlg.find(".btn_upload");
                btn.attr("ren", para.ren || "");
                btn.attr("limit", para.limit || "");
                btn.attr("dir", para.dir || "");
                btn.uploadFile({}, suc);
                uni.dlg(dlg, title || "上传文件");
            }
            //分析配置内容
            function analysis() {
                var opts = $("#options .item");
                var ret = "";
                opts.each(function () {
                    var opt = $(this);
                    var key = opt.attr("opt");
                    var value = null;
                    var input = opt.find("input");
                    if (input.is("[type=text]")) {
                        value = input.val();
                    }
                    else if (input.is("[type=radio]")) {
                        var ck = input.filter(":checked");
                        if (ck.length == 1)
                            value = ck.val();
                    }
                    else if (input.is("[type=checkbox]")) {
                        var val;
                        input.filter(":checked").each(function (i) {
                            var pthis = $(this);
                            if (pthis.hasClass("key")) {//带key则只认此项的值
                                val = pthis.val();
                                return false;
                            }
                            if (opt.hasClass("string")) {//字符串拼接
                                if (i == 0)
                                    val = pthis.val();
                                else
                                    val += ',' + pthis.val();
                            }
                            else {//数字加法
                                if (i == 0)
                                    val = parseInt(pthis.val());
                                else
                                    val += parseInt(pthis.val());
                            }
                        });
                        value = val;
                    }
                    if (value != null && value != undefined)
                        ret += key + '&' + value + '$';
                });
                if (ret.length > 0) ret = ret.substr(0, ret.length - 1);
                return ret;
            }
            //保存配置
            function save(data) {
                pro.j.objPostS(pro.j.util.p, { act: "save_app_settings", data: data }, function () {
                    uni.msgBoxRT("保存成功，将跳转到主页。", "提示", "Default.aspx");
                });
            }
            //初始化配置
            function init() {
                pro.j.objPostS(pro.j.util.p, { act: "get_app_settings"}, function (rlt) {
                    var data = rlt.data;
                    var kvp = {};
                    var set = data.split('$');
                    for (var i = 0; i < set.length; i++) {
                        var arr = set[i].split('&');
                        if (arr.length == 2) {
                            kvp[arr[0]] = arr[1];
                        }
                    }
                    var opts = $("#options .item");
                    opts.each(function () {
                        var opt = $(this);
                        var key = opt.attr("opt");
                        if (kvp[key] == undefined) return true;//无定义
                        var value = kvp[key];
                        var input = opt.find("input");
                        if (input.is("[type=text]")) {
                            input.val(value);
                        }
                        else if (input.is("[type=radio]")) {
                            input.each(function () {
                                var p = $(this);
                                if (p.val() == value)
                                    p.attr("checked", "checked");
                                else
                                    p.removeAttr("checked");
                            });
                        }
                        else if (input.is("[type=checkbox]")) {
                            input.each(function (i) {
                                var pthis = $(this);
                                if (pthis.hasClass("key")) {//带key
                                    if (pthis.val() == value) {
                                        pthis.attr("checked", "checked");
                                        return false;
                                    }
                                }
                                if (opt.hasClass("string")) {//字符串
                                    if (value.indexOf(pthis.val()) < 0)
                                        pthis.removeAttr("checked");
                                    else
                                        pthis.attr("checked", "checked");
                                }
                                else {//数字
                                    var v = parseInt(pthis.val());
                                    if ((parseInt(value) & v) > 0)
                                        pthis.attr("checked", "checked");
                                    else
                                        pthis.removeAttr("checked");
                                }
                            });
                        }
                    });
                });
            }
        </script>
        <div id="upload_dlg">
            <div style="display: none;">
                <form onsubmit="return false;">
                    <input type="button" class="btn_upload" value="上传" />
                </form>
            </div>
        </div>
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
    <div id="options">
        <h2>操作必读</h2>
        <div>1、本页面配置将保存到ClientWeb/Web.config文件，保存后系统以Web.config文件配置为准，并请确保拥有ClientWeb/Web.config的写入权限。</div>
        <div>
            2、登录本页面后将自动读取Web.config文件配置，手动读取请点击：<a onclick="init()">读取配置</a>；不需要自动读取请勾选：<label><input type="checkbox" id="no_need_read" />不需自动读取</label>
            即恢复到默认选项。
        </div>
        <div>
            升级的一些提示：<br />
            保留旧配置，只需要把旧配置文件Web.config拷贝到ClientWeb下并读取配置后保存即可。(不仅保留了旧配置，新增加的配置项也得到补充)<br />
            旧IC系统曾经把部分配置放入ClientWeb/xcus/ic/Web.config(优先级更高)，现在升级若想保留旧配置，可先读取ClientWeb/Web.config后，再把ic/Web.config拷贝过来读取，保存即可。现统一只用一个配置文件，请把ic/Web.config删除。
        </div>
        <div class="red">
            注意：<br />
            a、本页面供实施的同事内部使用，切勿告知客户。b、本配置页内容主要针对新版IC系统及其共用模块，其它系统部分生效。c、本页面并不一定包含所有配置项，对于非常个性化的配置，请直接到Web.config页内操作。
            <%--            <br />
            相关概念
            普通预约：主要指IC/研修间的预约，即最基础的预约，没有场馆、教学、活动等特殊需求的拓展；
            教学预约：主要指在普通预约基础上拓展了实验计划、实验项目等信息的拓展；--%>
        </div>
        <div class="line"></div>
        <div style="font-weight: bold;"><a onclick="init()">读取配置</a></div>
        <h2>项目选择</h2>
        <!--pro 项目标志 旧IC/研修间=ic 新IC/研修间=ic2 实验教学=jx 北师大心理=bsd-xl 浙大农学院=zd-nxy 场馆=cg2 中医药=zyy-->
        <div class="item" opt="proTab">
            <div class="intro">当前项目</div>
            <label>
                <input type="radio" name="proTab" value="" checked="checked" />未设置 
            </label>
            <label>
                <input type="radio" name="proTab" value="ic" />旧IC/研修间(不建议使用) 
            </label>
            <label>
                <input type="radio" name="proTab" value="ic2" />新IC/研修间 
            </label>
            <label>
                <input type="radio" name="proTab" value="jx" />教学实验室 
            </label>
            <label>
                <input type="radio" name="proTab" value="bsd-xl" />北师大心理 
            </label>
            <label>
                <input type="radio" name="proTab" value="zd-nxy" />浙大农学院 
            </label>
            <label>
                <input type="radio" name="proTab" value="cg2" />场馆 
            </label>
            <label>
                <input type="radio" name="proTab" value="zyy" />浙江中医药 
            </label>
        </div>
        <!--pro 项目涉及的使用目的(累加) 全部=0 教学=1 个人=2 开放活动=4 科研=8 场馆=16  使用座位=0x40  使用电脑=0x80 外借=0x100 使用研修间=0x400 -->
        <div class="item" opt="proTarget">
            <div class="intro">项目涉及的使用目的</div>
            <label class="red">
                <input type="checkbox" name="proTarget" class="key" value="0" checked="checked" />许可证所授权的全部(优先生效)
            </label>
            <label>
                <input type="checkbox" name="proTarget" value="1" />教学
            </label>
            <label>
                <input type="checkbox" name="proTarget" value="2" />个人
            </label>
            <label>
                <input type="checkbox" name="proTarget" value="4" />(个人子目的)开放活动
            </label>
            <label>
                <input type="checkbox" name="proTarget" value="8" />科研
            </label>
            <label>
                <input type="checkbox" name="proTarget" value="16" />场管
            </label>
            <label>
                <input type="checkbox" name="proTarget" value="64" />(个人子目的)使用座位
            </label>
            <label>
                <input type="checkbox" name="proTarget" value="128" />(个人子目的)使用电脑
            </label>
            <label>
                <input type="checkbox" name="proTarget" value="256" />(个人子目的)外借
            </label>
            <label>
                <input type="checkbox" name="proTarget" value="1024" />(个人子目的)使用研修间
            </label>
        </div>
        <div class="line"></div>
        <h2>基本设置</h2>
        <h3>杂项</h3>
        <!--自动跳转手机端=1-->
        <div class="item" opt="availMobile">
            <div class="intro">自动跳转手机端</div>
            <label>
                <input type="radio" name="availMobile" value="1" />是 
            </label>
            <label>
                <input type="radio" name="availMobile" value="0" checked="checked" />否 
            </label>
        </div>
        <!--支持提前结束预约=1-->
        <div class="item" opt="showResvFinish">
            <div class="intro">支持提前结束预约</div>
            <label>
                <input type="radio" name="showResvFinish" value="1" />是 
            </label>
            <label>
                <input type="radio" name="showResvFinish" value="0" checked="checked" />否 
            </label>
        </div>
        <!--启用开放活动功能 可选普通预约=1 只选申请活动=2-->
        <div class="item" opt="openActivity">
            <div class="intro">启用IC空间系统开放活动功能</div>
            <label>
                <input type="radio" name="openActivity" value="0" checked="checked" />不启用 
            </label>
            <label>
                <input type="radio" name="openActivity" value="1" />启用(普通预约与开放活动可选) 
            </label>
            <label>
                <input type="radio" name="openActivity" value="2" />启用(不可选普通预约) 
            </label>
        </div>
        <!--pro 记录文件上传日志 =1-->
        <div class="item" opt="upfileLog">
            <div class="intro">记录文件上传日志</div>
            <label>
                <input type="radio" name="upfileLog" value="1" />是 
            </label>
            <label>
                <input type="radio" name="upfileLog" value="0" checked="checked" />否 
            </label>
        </div>
        <h3>登录配置</h3>
        <!--v登录前需激活=1-->
        <div class="item" opt="mustAct">
            <div class="intro">登录前需激活</div>
            <label>
                <input type="radio" name="mustAct" value="1" checked="checked" />是 
            </label>
            <label>
                <input type="radio" name="mustAct" value="0" />否 
            </label>
        </div>
                <!--登录必须验证码=1-->
        <div class="item" opt="mustVerif">
            <div class="intro">登录必须验证码</div>
            <label>
                <input type="radio" name="mustVerif" value="1"/>是 
            </label>
            <label>
                <input type="radio" name="mustVerif" value="0"  checked="checked" />否 
            </label>
        </div>
        <!--前端必须登录=1-->
        <div class="item" opt="mustLogin">
            <div class="intro">前端必须登录</div>
            <label>
                <input type="radio" name="mustLogin" value="1" />是 
            </label>
            <label>
                <input type="radio" name="mustLogin" value="0" checked="checked" />否 
            </label>
        </div>
        <!--pro 只允许登录的身份(多个可累加) 全部=0 教师=512 学生=256-->
        <div class="item" opt="allowIdent">
            <div class="intro">只允许登录的身份</div>
            <label>
                <input type="radio" name="allowIdent" value="0" checked="checked" />全部 
            </label>
            <label>
                <input type="radio" name="allowIdent" value="512" />教师 
            </label>
            <label>
                <input type="radio" name="allowIdent" value="256" />学生 
            </label>
        </div>
        <!--pro 从第三方地址登录-->
        <div class="item" opt="thirdLogin">
            <div class="intro">重定向登录 完整地址(包含http://) 需设置前端必须登录</div>
            <input type="text" name="thirdLogin" class="long" />
        </div>
        <h3>IC系统资源结构</h3>
        <!--(旧ic系统)ic空间需开放的类型(多个用逗号隔开)  座位=seat 空间=common 外借=loan 电子阅览室=computer-->
        <div class="item string" opt="icClsKind">
            <div class="intro">(旧ic/研修间系统)开放的大类型</div>
            <label>
                <input type="checkbox" name="icClsKind" value="seat" />座位 
            </label>
            <label>
                <input type="checkbox" name="icClsKind" value="common" checked="checked" />空间 
            </label>
            <label>
                <input type="checkbox" name="icClsKind" value="computer" />电子阅览室 
            </label>
        </div>
        <!--(新)开放的设备大类别(多个累加)不过滤=0 空间=1 电子阅览室/电脑=2 外借=4 座位=8-->
        <div class="item" opt="openClsKind">
            <div class="intro">(新ic/研修间系统)开放的大类型</div>
            <label class="red">
                <input type="checkbox" name="openClsKind" class="key" value="0" />全开放(优先生效)
            </label>
            <label>
                <input type="checkbox" name="openClsKind" value="1" checked="checked" />空间
            </label>
            <label>
                <input type="checkbox" name="openClsKind" value="2" />电子阅览室/电脑
            </label>
            <label>
                <input type="checkbox" name="openClsKind" value="4" />外借
            </label>
            <label>
                <input type="checkbox" name="openClsKind" value="8" />座位
            </label>
        </div>
        <!--普通预约的资源分类方式 类别=1 类型=2(未用) 实验室+房间=4 学院+部门=8(用于部门开课介绍) 设备=16 实验室=32 多选则累加-->
        <div class="item" opt="resourceMode">
            <div class="intro">(新ic/研修间系统)资源分类方式 即页面左边资源列表的内容</div>
            <label>
                <input type="checkbox" name="resourceMode" value="1" checked="checked" />类别(=空间类型) 
            </label>
            <label>
                <input type="checkbox" name="resourceMode" value="2" />类型 (未用)
            </label>
            <label>
                <input type="checkbox" name="resourceMode" value="4" />实验室+房间二级模式 
            </label>
            <label>
                <input type="checkbox" name="resourceMode" value="8" />学院+部门二级模式(用于部门开课介绍)
            </label>
            <label>
                <input type="checkbox" name="resourceMode" value="16" />设备
            </label>
            <label>
                <input type="checkbox" name="resourceMode" value="32" />实验室
            </label>
        </div>
        <h3>科研预约配置</h3>
        <!--实验数据保存路径-->
        <div class="item" opt="dataFilePath">
            <div class="intro">实验数据保存路径</div>
            <input type="text" name="dataFilePath" value="" class="long" />
        </div>
        <!--pro 实验申请报告叠加保存 =1-->
        <div class="item" opt="multiApplyFile">
            <div class="intro">实验申请报告叠加保存(保留历史申请文件)</div>
            <label>
                <input type="radio" name="multiApplyFile" value="1" />是 
            </label>
            <label>
                <input type="radio" name="multiApplyFile" value="0" checked="checked" />否 
            </label>
        </div>
        <!--pro 学生指定导师 自动转导师身份 =1-->
        <div class="item" opt="autoToTutor">
            <div class="intro">学生指定导师 非导师自动转导师身份</div>
            <label>
                <input type="radio" name="autoToTutor" value="1" checked="checked" />是 
            </label>
            <label>
                <input type="radio" name="autoToTutor" value="0" />否 
            </label>
        </div>
        <h3>教学预约设置(教学实验系统)</h3>
        <!--访问模式 login=登录页 home=门户页 default=主页-->
        <div class="item" opt="visitMode">
            <div class="intro">教学预约访问模式</div>
            <label>
                <input type="radio" name="visitMode" value="login" />登录转教学实验
            </label>
            <label>
                <input type="radio" name="visitMode" value="default" checked="checked" />新IC系统转教学实验
            </label>
            <label>
                <input type="radio" name="visitMode" value="home" />独立门户转教学实验(未使用) 
            </label>
        </div>
        <!-- 教学支持实验计划类型 默认/1=教学统一安排 2=开放实验-->
        <div class="item" opt="testPlanKind">
            <div class="intro">教学支持实验计划类型</div>
            <label>
                <input type="checkbox" name="testPlanKind" value="1" checked="checked" />教学统一安排
            </label>
            <label>
                <input type="checkbox" name="testPlanKind" value="2" />教学开放实验
            </label>
        </div>
        <!--检查教室容量=1-->
        <div class="item" opt="ckClsRoomCapacity">
            <div class="intro">教学预约检查教室容量</div>
            <label>
                <input type="radio" name="ckClsRoomCapacity" value="1" />是 
            </label>
            <label>
                <input type="radio" name="ckClsRoomCapacity" value="0" checked="checked" />否 
            </label>
        </div>
        <!--自动识别身份登录=1-->
        <div class="item" opt="autoJudgeRole">
            <div class="intro">教学端自动识别身份登录</div>
            <label>
                <input type="radio" name="autoJudgeRole" value="1" checked="checked" />是 
            </label>
            <label>
                <input type="radio" name="autoJudgeRole" value="0" />否 
            </label>
        </div>
        <!--教学预约流程 自动创建项目=1 隐藏项目概念=2 个人预约=4 默认标准(严格实验计划、实验项目模式) -->
        <div class="item" opt="scheduleMode">
            <div class="intro">教学预约流程</div>
            <label>
                <input type="radio" name="scheduleMode" value="0" />传统(计划+项目模式)
            </label>
            <label>
                <input type="radio" name="scheduleMode" value="1" />传统+自动创建项目 
            </label>
            <label>
                <input type="radio" name="scheduleMode" value="2" />隐藏项目概念 
            </label>
            <label>
                <input type="radio" name="scheduleMode" value="4" />个人预约拓展
            </label>
        </div>
        <h3>普通预约设置</h3>
        <!--预约时间粒度 (分钟)不允许配置大于60-->
        <div class="item" opt="resvTimeUnit">
            <div class="intro">预约时间粒度 (分钟)不允许配置大于60 如:时间粒度为10，则可预约时间将被限制为10分钟的倍数</div>
            <input type="text" name="resvTimeUnit" value="10" />
        </div>
        <!--长期预约为预约全天(只约日期)=1-->
        <div class="item" opt="resvAllDay">
            <div class="intro">长期预约为预约全天(只约日期)</div>
            <label>
                <input type="radio" name="resvAllDay" value="1"/>是 
            </label>
            <label>
                <input type="radio" name="resvAllDay" value="0" checked="checked"/>否 
            </label>
        </div>
        <!--预约固定时段 格式：时段用英文逗号【,】隔开,开始结束时间用英文冒号【:】隔开，小时分钟不隔开 如 830: 1200, 1400: 1700 -->
        <div class="item" opt="fixTimeSpan">
            <div class="intro">预约固定时段 格式：时段用英文逗号【,】隔开,开始结束时间用英文冒号【:】隔开，小时分钟不隔开。如 830: 1200, 1400: 1700  PS:确保时间符合开放与预约规则并满足预约时间粒度</div>
            <input type="text" name="fixTimeSpan" value="" class="long" />
        </div>
        <!--预约状态表显示不开放的对象-->
        <div class="item" opt="showClose">
            <div class="intro">预约状态表显示不开放的对象</div>
            <label>
                <input type="radio" name="showClose" value="1" />是
            </label>
            <label>
                <input type="radio" name="showClose" value="0" checked="checked" />否 
            </label>
        </div>
        <!--pro 预约前需阅读预约须知 通用=1 类别=2 类型=3 设备=4 房间=5 实验室=6-->
        <div class="item" opt="needToKnow">
            <div class="intro">预约前需阅读预约须知(须知内容按所选级别区分。如：选类型， 则不同类型须知内容不同)</div>
            <label>
                <input type="radio" name="needToKnow" value="0" checked="checked" />不启用 
            </label>
            <label>
                <input type="radio" name="needToKnow" value="1" />通用 
            </label>
            <label>
                <input type="radio" name="needToKnow" value="2" />类别(未用) 
            </label>
            <label>
                <input type="radio" name="needToKnow" value="3" />类型 
            </label>
            <label>
                <input type="radio" name="needToKnow" value="4" />设备 
            </label>
            <label>
                <input type="radio" name="needToKnow" value="5" />房间 
            </label>
            <label>
                <input type="radio" name="needToKnow" value="6" />实验室 
            </label>
        </div>
        <h3>多语言(新IC)</h3>
        <!--支持多语言-->
        <div class="item" opt="supMultilanguage">
            <div class="intro">支持多语言</div>
            <label>
                <input type="radio" name="supMultilanguage" value="1" />是 
            </label>
            <label>
                <input type="radio" name="supMultilanguage" value="0" checked="checked" />否 
            </label>
        </div>
        <!--多语言环境下 系统默认语言(不设置则默认中文) -->
        <div class="item" opt="dftLanguage">
            <div class="intro">多语言环境下 系统默认语言</div>
            <label>
                <input type="radio" name="dftLanguage" value="" checked="checked" />中文 
            </label>
            <label>
                <input type="radio" name="dftLanguage" value="en-gb" />英语 
            </label>
        </div>
        <h3>用户信息配置(新IC)</h3>
        <!--pro 支持修改密码=1-->
        <div class="item" opt="needChangePsw">
            <div class="intro">支持修改密码</div>
            <label>
                <input type="radio" name="needChangePsw" value="1" />是 
            </label>
            <label>
                <input type="radio" name="needChangePsw" value="0" checked="checked" />否 
            </label>
        </div>
        <!--启用用户信用功能=1-->
        <div class="item" opt="userCredit">
            <div class="intro">启用用户信用功能</div>
            <label>
                <input type="radio" name="userCredit" value="1" checked="checked" />是 
            </label>
            <label>
                <input type="radio" name="userCredit" value="0" />否 
            </label>
        </div>
        <!--pro 允许用户配置短信提醒=1-->
        <div class="item" opt="optNoteAlert">
            <div class="intro">允许用户配置短信提醒</div>
            <label>
                <input type="radio" name="optNoteAlert" value="1" />是 
            </label>
            <label>
                <input type="radio" name="optNoteAlert" value="0" checked="checked" />否 
            </label>
        </div>
        <h3>平面图/外借拓展(暂时仅支持实验室分类方式下的新IC系统)</h3>
        <!--pro 预约状态呈现模式 列表=默认 平面图=1 平面图在resourceMode=32上生效-->
        <div class="item" opt="resvStateMode">
            <div class="intro">预约状态呈现模式(平面图需资源分类方式为实验室)</div>
            <label>
                <input type="radio" name="resvStateMode" value="0" checked="checked" />列表 
            </label>
            <label>
                <input type="radio" name="resvStateMode" value="1" />平面图 
            </label>
        </div>
        <!--pro 支持平面图呈现模式的大类 不限制=0 空间=1 电子阅览室/电脑=2 外借=4 座位=8 -->
        <div class="item" opt="floorPlanClsKind">
            <div class="intro">支持平面图呈现模式的大类</div>
            <label class="red">
                <input type="checkbox" name="floorPlanClsKind" class="key" value="0" checked="checked" />全部(优先生效)
            </label>
            <label>
                <input type="checkbox" name="floorPlanClsKind" value="1" />空间
            </label>
            <label>
                <input type="checkbox" name="floorPlanClsKind" value="2" />电子阅览室/电脑
            </label>
            <label>
                <input type="checkbox" name="floorPlanClsKind" value="4" />外借
            </label>
            <label>
                <input type="checkbox" name="floorPlanClsKind" value="8" />座位
            </label>
        </div>
        <!--pro 外借设备预约状态呈现为详细列表=1 暂时在resourceMode=32上生效-->
        <div class="item" opt="resvLoanDetail">
            <div class="intro">外借设备预约状态呈现为详细列表(暂时需资源分类方式为实验室)</div>
            <label>
                <input type="radio" name="resvLoanDetail" value="1" />是 
            </label>
            <label>
                <input type="radio" name="resvLoanDetail" value="0" checked="checked" />否 
            </label>
        </div>
        <h3>微信绑定</h3>
        <!--pro 需绑定微信=1-->
        <div class="item" opt="bindWechat">
            <div class="intro">需绑定微信</div>
            <label>
                <input type="radio" name="bindWechat" value="1" />是 
            </label>
            <label>
                <input type="radio" name="bindWechat" value="0" checked="checked" />否 
            </label>
        </div>
        <!--pro 绑定微信二维码完整地址(包含http://)-->
        <div class="item" opt="wechatQrCode">
            <div class="intro">绑定微信二维码完整地址(如http://【地址】?sysid=【客户号】)</div>
            <input type="text" class="long" name="wechatQrCode" value="http://update.unifound.net/uniwx/qrcode.aspx?sysid=" />
        </div>
        <div class="line"></div>
        <h2>页面显示</h2>
        <h3>杂项</h3>
        <!--教学预约上课类型 1=实验 2=授课-->
        <div class="item" opt="courseKind">
            <div class="intro">教学预约上课类型(影响文字显示)</div>
            <label>
                <input type="radio" name="courseKind" value="1" checked="checked" />实验 
            </label>
            <label>
                <input type="radio" name="courseKind" value="2" />授课 
            </label>
        </div>
        <!--pro 前端显示演示内容=1-->
        <div class="item" opt="isDemo">
            <div class="intro">前端显示演示内容</div>
            <label>
                <input type="radio" name="isDemo" value="1" checked="checked" />是 
            </label>
            <label>
                <input type="radio" name="isDemo" value="0" />否 
            </label>
        </div>
        <!--显示跳转到管理端-->
        <div class="item" opt="mShowJumpManage">
            <div class="intro">显示跳转到管理端</div>
            <label>
                <input type="radio" name="mShowJumpManage" value="1" />是 
            </label>
            <label>
                <input type="radio" name="mShowJumpManage" value="0" checked="checked" />否 
            </label>
        </div>
        <!--pro 登录时帐号描述-->
        <div class="item" opt="idIntro">
            <div class="intro">登录时帐号描述</div>
            <input type="text" class="long" name="idIntro" value="学号/工号" />
        </div>
        <!--pro 登录时密码描述-->
        <div class="item" opt="pwdIntro">
            <div class="intro">登录时密码描述</div>
            <input type="text" class="long" name="pwdIntro" value="同一卡通密码" />
        </div>
        <h3>提交预约窗口呈现内容/功能(主要新IC系统使用)</h3>
        <!--预约时总是(无论是否必须)显示上传附件=1-->
        <div class="item" opt="showResvAttach">
            <div class="intro">预约时空间总是(无论是否必须)显示上传附件(新旧IC通用)</div>
            <label>
                <input type="radio" name="showResvAttach" value="1" />是 
            </label>
            <label>
                <input type="radio" name="showResvAttach" value="0" checked="checked" />否 
            </label>
        </div>
        <!--pro普通预约 支持教师排课=1-->
        <div class="item" opt="resvSchedule">
            <div class="intro">显示教师排课</div>
            <label>
                <input type="radio" name="resvSchedule" value="1" />是 
            </label>
            <label>
                <input type="radio" name="resvSchedule" value="0" checked="checked" />否 
            </label>
        </div>
        <!--pro普通预约 收费，计算费用=1-->
        <div class="item" opt="resvBilling">
            <div class="intro">显示费用 未启用</div>
            <label>
                <input type="radio" name="resvBilling" value="1" />是 
            </label>
            <label>
                <input type="radio" name="resvBilling" value="0" checked="checked" />否 
            </label>
        </div>
        <!--pro普通预约 显示主题=1 必须=2-->
        <div class="item" opt="resvTheme">
            <div class="intro">显示主题</div>
            <label>
                <input type="radio" name="resvTheme" value="0" checked="checked" />不显示 
            </label>
            <label>
                <input type="radio" name="resvTheme" value="1" />显示不必填
            </label>
            <label>
                <input type="radio" name="resvTheme" value="2" />显示并必填 
            </label>
        </div>
        <!--pro普通预约 显示备注=1 必须=2-->
        <div class="item" opt="resvMemo">
            <div class="intro">显示备注</div>
            <label>
                <input type="radio" name="resvMemo" value="0" checked="checked" />不显示 
            </label>
            <label>
                <input type="radio" name="resvMemo" value="1" />显示不必填
            </label>
            <label>
                <input type="radio" name="resvMemo" value="2" />显示并必填 
            </label>
        </div>
                <!--pro 普通预约 预约主题只能选择固定项=1-->
        <div class="item" opt="fixTheme">
            <div class="intro">预约主题只能选择固定选项</div>
            <label>
                <input type="radio" name="fixTheme" value="1" />是 
            </label>
            <label>
                <input type="radio" name="fixTheme" value="0"  checked="checked"/>否 
            </label>
        </div>
        <!--pro 显示预约状态条=1-->
        <div class="item" opt="supStateSlider">
            <div class="intro">显示预约状态条</div>
            <label>
                <input type="radio" name="supStateSlider" value="1" checked="checked" />是 
            </label>
            <label>
                <input type="radio" name="supStateSlider" value="0" />否 
            </label>
        </div>
        <!--pro普通预约 备注提示内容-->
        <div class="item" opt="memoTip">
            <div class="intro">备注提示内容</div>
            <input type="text" class="long" name="memoTip" value="其它备注信息，请填到下边输入框中" />
        </div>
        <h3>新IC系统首页</h3>
        <!--显示文字 资源列表-->
        <div class="item" opt="mResourceList">
            <div class="intro">资源列表标题文字</div>
            <input type="text" name="mResourceList" value="资源列表" />
        </div>
        <!--最新通知=1-->
        <div class="item" opt="mNotice">
            <div class="intro">显示最新通知</div>
            <label>
                <input type="radio" name="mNotice" value="1" checked="checked" />是 
            </label>
            <label>
                <input type="radio" name="mNotice" value="0" />否 
            </label>
        </div>
        <!--最新预约动态=1-->
        <div class="item" opt="mResvDynamic">
            <div class="intro">显示最新预约动态</div>
            <label>
                <input type="radio" name="mResvDynamic" value="1" checked="checked" />是 
            </label>
            <label>
                <input type="radio" name="mResvDynamic" value="0" />否 
            </label>
        </div>
        <!--头部背景=1-->
        <div class="item" opt="mHeadBg">
            <div class="intro">显示头部背景</div>
            <label>
                <input type="radio" name="mHeadBg" value="1" checked="checked" />是 
            </label>
            <label>
                <input type="radio" name="mHeadBg" value="0" />否 
            </label>
        </div>
        <!--logo=1-->
        <div class="item" opt="mLogo">
            <div class="intro">显示logo</div>
            <label>
                <input type="radio" name="mLogo" value="1" />是 
            </label>
            <label>
                <input type="radio" name="mLogo" value="0" checked="checked" />否 
            </label>
        </div>
        <!--带logo 头部条幅=1-->
        <div class="item" opt="mHeadScroll">
            <div class="intro">显示头部条幅</div>
            <label>
                <input type="radio" name="mHeadScroll" value="1" />是 
            </label>
            <label>
                <input type="radio" name="mHeadScroll" value="0" checked="checked" />否 
            </label>
        </div>
        <!--普通预约index页显示模式 排列=1 幻灯片=2-->
        <div class="item" opt="mIndexMode">
            <div class="intro">图片显示模式</div>
            <label>
                <input type="radio" name="mIndexMode" value="1" />排列 
            </label>
            <label>
                <input type="radio" name="mIndexMode" value="2" checked="checked" />幻灯片 
            </label>
        </div>
        <div class="line"></div>
        <h2>页面主题</h2>
        <!--页面主题-->
        <div class="item" opt="sysTheme">
            <div class="intro">页面主题</div>
            <label>
                <input type="radio" name="sysTheme" value="dft" checked="checked" />默认主题 
            </label>
        </div>
        <div class="line"></div>
        <h2>未分类</h2>
        <div class="line"></div>
        <div class="footer">
            <p class="red">保存后旧配置将丢失</p>
            <%--<form runat="server">  <button type="button" class="btn btn-default" runat="server" onserverclick="Read_ServerClick">读取默认</button>              </form>--%>
            <div class="btn-group">
                <button type="button" class="btn btn-info" onclick="submit();">保存到文件</button>
            </div>
            <script>
                function submit() {
                    uni.confirm("保存后旧配置将丢失，是否继续？", function () {
                        save(analysis());
                    });
                }
            </script>
        </div>
    </div>
    <%} %>
</body>
</html>
