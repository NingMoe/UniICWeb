<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../a/net/MasterPage.master" CodeFile="Default.aspx.cs" Inherits="ClientWeb_xcus_all_Info" %>
<%@ Register TagPrefix="Uni" TagName="resv" Src="~/ClientWeb/pro/net/dlg_resv.ascx" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/printArea/printArea.js" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/printArea/printArea.js" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.sch.css" rel='stylesheet' />
    <link href="<%=ResolveClientUrl("~/ClientWeb/")  %>md/Timepickeraddon/jquery-ui-timepicker-addon.css" rel="stylesheet" />
    <script src="<%=ResolveClientUrl("~/ClientWeb/")  %>md/Timepickeraddon/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/ClientWeb/")  %>md/Timepickeraddon/jquery-ui-timepicker-zh-CN.js" type="text/javascript"></script>
    <link rel="stylesheet" href="theme/cus.css" />
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <style>
        /*.affix { top: 20px; overflow: visible; white-space: nowrap; }
        .affix-bottom { position: absolute; }
        .affix-top { overflow: visible; white-space: nowrap; }*/
        .drop-select { min-width:120px;}
        .checkbox label input[type=checkbox] {margin-left:-14px;}
        header.navbar-inverse .navbar-nav>li>a { padding: 15px;color:#ddd; }
        .struct_tbl > tbody > tr > td.left_panel { width: 16%;}
        .apply_table p { margin:0;}
    </style>
    <script>
        //公共对象
        var content;//内容区域
        $(function () {
            // 初始化公共对象
            content = $("#detail_con");
            //载入主页
            $(".click_load").clickLoad();
            var req = uni.getReq();
            if (req['page'] == 'center' && $("#user_center").is(":visible")) {
                $("#user_center").trigger("click");
            }
            else {
                $("#home").trigger("click");
            }
            $(".cls_list").change(function () {
                treeFiler();
                reloadInfo();
            });
            $(".it_list li,.click_load").click(function () {
                $(".it_list li,.click_load").removeClass("activity");
                $(this).addClass("activity");
            });
            $(".it_list li").click(function () {
                reloadInfo();
            });
            //注册点击预约表对象事件
            pro.calendar.selObjFun = function (obj) {
                if (obj.type == "kind") return;//按类型预约
                var objid = obj.id;
                if (uni.isNoNull(objid)) {
                    objid = objid.devId;
                    uni.hr.loadHtml("../a/devdetail.aspx?dev=" + objid, null, null, null, null, $("#cache_con"));
                }
            }
        })
        function treeFiler() {
            var its = $(".it_list li");
            var include = $(".cls_list option:selected").attr("include");
            if (include && include != "0") {
                $(".it_list").hide();
                var list = include.split(',');
                var kill = false;
                its.each(function (i) {
                    var it = $(this);
                    if (uni.isInArray(it.attr("it"), list)) it.show();
                    else {
                        if (it.hasClass("activity")) { kill = true; it.removeClass("activity") }
                        it.hide();
                    }
                });
                $(".it_list").fadeIn();
                if (kill) $(".it_list li:visible:first").addClass("activity");
            }
            else
                its.show();
        }
        function reloadInfo() {
            var aty = $(".cls_list").val();
            var url = $(".it_list li.activity").attr("url");
            if (aty) {
                uni.backTop();
                uni.hr.loadHtml(url, { "activityId": aty }, content);
            }
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <Uni:resv runat="server" />
    <div class="row con_top">
        <table class="struct_tbl">
            <tbody>
            <tr>
                <td class="left_panel">
        <div class="col">
            <div id="info_tree">
                <div style="width: 160px;">
                    <h3>
                        <%--<a id="home" url="index.aspx" con="#detail_con" class="click_load"><span class="glyphicon glyphicon-home" style="font-size: 30px;"></span>&nbsp;主页&nbsp;<span class="grey">HOME</span></a>--%>
                    <a id="home" url="../a/article.aspx?id=help&type=other" con="#detail_con" class="click_load"><span><span class="glyphicon glyphicon-question-sign"></span>&nbsp;<%=Translate("使用说明")%></span></a>
                    </h3>
                    <div class="line"></div>
                    <ul class="nav oth_list">
                        <li  style="display:<%=islogin?"":"none" %>"><a id="user_center" url="center.aspx" con="#detail_con" class="click_load"><span class="glyphicon glyphicon-user"></span>&nbsp;个人中心</a></li>
                        <%--<li  style="display:<%=islogin?"":"none" %>"><a url="second.aspx" con="#detail_con" class="click_load"><span class="glyphicon glyphicon-bookmark"></span>&nbsp;第二课堂活动</a></li>--%>
                        <li><a url="atylist.aspx" con="#detail_con" class="click_load"><span class="glyphicon glyphicon-bookmark"></span>&nbsp;活动中心</a></li>

                        <%--<li><a url="../a/devlist.aspx" con="#detail_con" class="click_load"><span class="glyphicon glyphicon-bookmark"></span>&nbsp;场馆资源</a></li>--%>
                    </ul>
                    <div class="line"></div>
                    <div id="item_list" style="display:<%=islogin?"":"none" %>">
<%--                        <div class="one_hidden">
                    <h4><span class="glyphicon glyphicon-list" style="width: 18px;"></span>&nbsp;活动场景</h4>
                    <select class="cls_list">
                        <%=itemClsList %>
                    </select>
                        <script>
                            $(".cls_list").bsDropdown({ style: "btns" });
                        </script>
                        </div>--%>
                    <h4><span class="glyphicon glyphicon-list" style="width: 18px;"></span>&nbsp;场馆资源</h4>
                    <ul class="it_list nav">
                        <%=itemList %>
                    </ul>
                    <script type="text/javascript">
                        treeFiler();
                    </script>
                    </div>
                    <div id="panel_login" style="margin:30px -14px 20px -11px;display:<%=islogin?"none":"" %>">
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
                                    else
                                        location.reload();
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
    </script>
            <h4>登录&nbsp;<span class="glyphicon glyphicon-log-in"></span></h4>
            <div id="login_info">
                <p class="input-group">
                    <span class="input-group-addon"><span>帐号</span></span>
                    <input type="text" class="form-control" name="id" id="username" placeholder="<%=GetConfig("idIntro") %>" />
                </p>
                <p class="input-group">
                    <span class="input-group-addon"><span>密码</span></span>
                    <input type="password" class="form-control" name="pwd" id="password" placeholder="<%=GetConfig("pwdIntro") %>" />
                </p>
                <p class="help-block text-center <%=GetConfig("mustAct")!="1"?"hidden":"" %>""><small>为能即时联系用户，联系信息不完整请点击激活，以补充联系信息。</small></p>
                <p class="text-center">
                    <button type="button" class="btn btn-success default" onclick="login()">登录</button>
                    <button type="button" class="btn btn-default" onclick="actAcc()">激活</button>
                </p>
            </div>
                    </div>
                </div>
            </div>
        </div>
                </td>
                    <td>
                        <div id="panel_right" class="col">
                            <div style="background: left no-repeat url(theme/images/index_01.jpg); height: 80px; width: 100%; border-radius: 6px;" class="hidden"></div>
                            <div id="detail_con" style="display: none;"></div>
                            <div id="cache_con" style="display: none;"></div>
                        </div>
                    </td>
            </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
