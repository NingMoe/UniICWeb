<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../a/net/MasterPage.master" CodeFile="Default.aspx.cs" Inherits="ClientWeb_xcus_all_Info" %>

<%@ Register TagPrefix="Uni" TagName="quick" Src="../../pro/net/quick.ascx" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unifloorplan/unifloorplan.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unifloorplan/unifloorplan.css" rel='stylesheet' />
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
        .drop-select { min-width: 120px; }
        /*二级菜单*/
        #info_tree .cls_sec { overflow: hidden; height: 26px; margin-left: -12px; }
        #info_tree .cls_sec a.nav_cls_name { color: #666; padding-left: 6px; }
        #info_tree .cls_sec .glyphicon { color: #ccc; }
    </style>
    <script>
        //公共对象
        var content;//内容区域
        $(function () {
            //判断登录 异步登录
            cus.showLogin = function (parameter) {
                pro.d.lg.login(function (rlt, dlg) {
                    if (typeof (rlt.data) == "object" && rlt.data.id) {
                        pro.acc = rlt.data;
                        $("body").removeClass("login_state_out").addClass("login_state_in");
                        $(".acc_info .acc_info_name").html(pro.acc.name);
                        $(".acc_info .acc_info_id").html(pro.acc.id);
                        $(".acc_info .acc_info_dept").html(pro.acc.dept);
                        if (typeof (parameter) == "function") parameter();
                        else if (typeof (parameter) == "object" && typeof (parameter.callback) == "function") parameter.callback(parameter);
                        else { uni.msgBox("登录成功") }
                    }
                    dlg.dialog("close");
                }, null, "");
            }
            // 初始化公共对象
            content = $("#detail_con");
            //载入主页
            $(".click_load").clickLoad();
            $("#home").trigger("click");
            //$(".cls_list").change(function () {
            //    treeFiler();
            //    reloadInfo();
            //});
            //添加选中标志
            $(".it_list li,.click_load").click(function () {
                $(".it_list li,.click_load").removeClass("activity");
                $(this).addClass("activity");
            });
            $(".it_list li").click(function () {
                reloadInfo();
            });
        })
        //unicalendar配置
        var uni_calendar_dft_opt = {
            cusPrepare: function (data, callback) {
                var obj = data.obj;
                if (obj.type != "kind" && obj.prop && (parseInt(obj.prop) & 65536) > 0) {//是否支持开放活动 prop=65536这个属性是临时使用 所以暂定义在外层
                    var ov = "<%=openAty%>";
                    var url = "../a/openaty.aspx?dev=" + obj.devId + "&devkind=" + obj.kindId + "&back=true&date=" + data.dt.replace(/-/g, "") + "&time=" + data.start;
                    if (ov == "1") {//可选
                        uni.confirm("本设备支持预约开放活动", function () {
                            uni.hr.loadHtml(url, null, null, data);
                        }, function () {
                            callback(data);
                        }, "", {okText:"开放活动",backText:"普通预约"});
                    }
                    else if (ov == "2") {//不可选
                        uni.hr.loadHtml(url, null, null, data);
                    }
                    else {
                        callback(data);
                    }
                }
                else {
                    callback(data);
                }
            },
            dev_order: "<%=GetConfig("devOrder")%>",
            kind_order: "<%=GetConfig("kindOrder")%>"
        }
        //条件过滤
        function treeFiler() {
            var ul = $(".it_cls_list");
            var list = $(".nav_cls_li", ul);
            var ih = $("li:first", list).height();
            var sec = $(".cls_sec", ul).height(ih + "px");
            //list.click(function () {
            //    $(".cls_sec.activity", ul).animate({ height: ih + "px" }, "fast");
            //    //var pthis = $(this);
            //    //if (!pthis.hasClass("activity")) {
            //    //    if (pthis.hasClass("cls_sec")) {
            //    //        var height = ih * (pthis.find(".it").length + 1);
            //    //        pthis.animate({ height: height + "px" }, "fast");
            //    //    }
            //    //}
            //});
            list.click(function () {
                var pthis = $(this);
                var self = (pthis.hasClass("activity") && pthis.hasClass("cls_sec"));
                sec.each(function () {
                    var sthis = $(this);
                    if (sthis.find(".activity").length == 0) {
                        sthis.removeClass("activity");
                        sthis.animate({ height: ih + "px" }, "fast", function () {
                            sthis.find(".glyphicon").removeClass("glyphicon-circle-arrow-up").addClass("glyphicon-circle-arrow-down");
                        });
                    }
                });
                if (self) return;
                if (pthis.hasClass("cls_sec")) {
                    if (!pthis.hasClass("activity")) {
                        pthis.addClass("activity");
                        var height = ih * (pthis.find(".it").length + 1);
                        pthis.animate({ height: height + "px" }, "fast", function () {
                            pthis.find(".glyphicon").removeClass("glyphicon-circle-arrow-down").addClass("glyphicon-circle-arrow-up");
                        });
                    }
                    else {
                        pthis.removeClass("activity");
                    }
                }
            });
            //ul.mouseleave(function () {
            //    sec.each(function () {
            //        var pthis = $(this);
            //    if (pthis.find(".activity").length==0) {
            //        pthis.animate({ height: ih + "px" }, "fast", function () {
            //            pthis.find(".glyphicon").removeClass("glyphicon-circle-arrow-up").addClass("glyphicon-circle-arrow-down");
            //        });
            //    }
            //    });

            //});
        }
        //主动载入信息方法
        function reloadInfo() {
            var url = $(".it_list li.activity").attr("url");
            if (url) {
                uni.backTop();
                uni.hr.loadHtml(url, {}, content);
            }
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
<%--    <%if (ToUInt(GetConfig("quickResv")) > 0)
      {%>
    <Uni:quick runat="server" />
    <%}%>--%>
    <input type="hidden" value="<%=closeDevCls %>" class="dft_close_devcls" />
    <div class="row con_top">
        <img style="height: 140px; width: 100%;" src="theme/images/index.jpg" class="<%=GetConfig("mHeadScroll")=="1"?"":"hidden" %>" />
        <table class="struct_tbl">
            <tbody>
                <tr>
                    <td class="left_panel">
                        <div class="col">
                            <div id="info_tree">
                                <div style="width: 263px;">
                                    <div url="index.aspx" con="#detail_con" class="click_load click">
                                        <img style="height: 100px; width: 100%; display: <%=GetConfig("mLogo")=="1"?"":"none" %>" src="theme/images/logo.jpg" />
                                        <h3>
                                            <a id="home" class="home"><span class="glyphicon glyphicon-home"></span>&nbsp;<%=Translate("主页") %>&nbsp;</a>
                                        </h3>
                                    </div>
                                    <div class="line"></div>
                                    <ul class="nav oth_list">
                                        <li><a url="../a/article.aspx?id=<%=helpUrl %>&type=other" con="#detail_con" class="click_load"><span><span class="glyphicon glyphicon-question-sign"></span>&nbsp;<%=Translate("使用帮助")%></span></a></li>
                                        <li class="login_show" style=""><a id="user_center" url="../a/center.aspx" con="#detail_con" class="click_load"><span><span class="glyphicon glyphicon-user"></span>&nbsp;<%=Translate("个人中心")%></span></a></li>
                                        <li style="display: <%=((openAty=="1"||openAty=="2"))?"":"none" %>"><a url="../a/openatycenter.aspx?type=pre" con="#detail_con" class="click_load"><span><span class="glyphicon glyphicon-bookmark"></span>&nbsp;<%=Translate("活动预告")%></span></a></li>
                                        <li style="display: <%=((openAty=="1"||openAty=="2"))?"":"none" %>"><a url="../a/openatycenterOld.aspx?type=old" con="#detail_con" class="click_load"><span><span class="glyphicon glyphicon-bookmark"></span>&nbsp;<%=Translate("活动回顾")%></span></a></li>
                                        <li class="login_show" style="display:<%=GetConfig("CastVote")=="1"?"":"none"%>">
                                            <a url="../a/castvote.aspx" con="#detail_con" class="click_load"><span><span class="glyphicon glyphicon-signal"></span>&nbsp;<%=Translate("网上投票")%></span></a></li>
                                    <!--    <li style=""><a href="http://www.lclibrary.shufe.edu.cn"><span class="glyphicon glyphicon-bookmark"></span>&nbsp;返回图书馆主页</a></li>-->
                                        
                                    </ul>
                                    <div class="line"></div>
                                    <div id="item_list">
                                        <h4><span class="glyphicon glyphicon-list" style="width: 18px;"></span>&nbsp;<%=Translate(GetConfig("mResourceList"))%></h4>
                                        <ul class="it_cls_list nav">
                                            <%=itemList %>
                                        </ul>
                                        <script type="text/javascript">
                                            treeFiler();
                                        </script>
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
