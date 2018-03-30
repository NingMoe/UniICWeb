<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../a/net/MasterPage.master" CodeFile="Default.aspx.cs" Inherits="ClientWeb_xcus_all_Info" %>

<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.sch.css" rel='stylesheet' />
        <link href="<%=ResolveClientUrl("~/ClientWeb/")  %>md/Timepickeraddon/jquery-ui-timepicker-addon.css" rel="stylesheet" />
    <script src="<%=ResolveClientUrl("~/ClientWeb/")  %>md/Timepickeraddon/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/ClientWeb/")  %>md/Timepickeraddon/jquery-ui-timepicker-zh-CN.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <style>
        /*.affix { top: 20px; overflow: visible; white-space: nowrap; }
        .affix-bottom { position: absolute; }
        .affix-top { overflow: visible; white-space: nowrap; }*/
        .drop-select { min-width:120px;}
    </style>
    <script>
        //公共对象
        var content;//内容区域
        $(function () {
            // 初始化公共对象
            content = $("#detail_con");
            //载入主页
            $(".click_load").clickLoad();
            $("#home").trigger("click");
            $(".cls_list").change(function () {
                treeFiler();
                reloadInfo();
            });
            //添加选中标志
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
        //条件过滤
        function treeFiler() {
            var cls = $(".cls_list").val();
            var its = $(".it_list li");
            if (cls && cls != "0") {
                $(".it_list").hide();
                var kill = false;
                its.each(function (i) {
                    var it = $(this);
                    if (cls==it.attr("it")) it.show();
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
    <div class="row">
                <table class="struct_tbl">
            <tbody>
                <tr>
                    <td class="left_panel">
                        <div class="col">
                            <div id="info_tree">
                                <div style="width: 263px;">
                    <h3>
                        <span class="glyphicon glyphicon-home"></span>&nbsp;<a id="home" url="index.aspx" con="#detail_con" class="click_load" style="color: #666;">主页&nbsp;<span class="grey">HOME</span></a>
                    </h3>
                    <div class="line"></div>
                    <ul class="nav oth_list">
                        <li  style="display:<%=islogin?"":"none" %>"><a id="user_center" url="../a/center.aspx" con="#detail_con" class="click_load"><span id="user_center"><span class="glyphicon glyphicon-user"></span>&nbsp;个人中心</span></a></li>
                        <li><a url="../a/devlist.aspx" con="#detail_con" class="click_load"><span class="glyphicon glyphicon-bookmark"></span>&nbsp;设备资源</a></li>
                        <li><a url="../a/courselist.aspx" con="#detail_con" class="click_load"><span><span class="glyphicon glyphicon-bookmark"></span>&nbsp;<%=Translate("课程介绍")%></span></a></li>
                        <li><a href="Login.aspx?sys=teach" class="text-primary"><span class="glyphicon glyphicon-bookmark"></span>&nbsp;进入教学预约系统</a></li>
                    </ul>
                    <div class="line"></div>
                    <div id="item_list">
                        <div class="one_hidden">
                    <h4><span class="glyphicon glyphicon-list" style="width: 18px;"></span>&nbsp;资源类型</h4>
                    <select class="cls_list hidden" style="width:95%">
                        <%=itemClsList %>
                    </select>
                            <script>
                                if ($(".cls_list option").length < 2) $(".one_hidden").hide();
                                else $(".cls_list").bsDropdown({ style: "btns" });
                            </script>
                        </div>
                    <h4><span class="glyphicon glyphicon-list" style="width: 18px;"></span>&nbsp;资源列表</h4>
                    <ul class="it_list nav">
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
            <div id="detail_con" style="display: none;"></div>
            <div id="cache_con" style="display: none;"></div>
        </div></td>
                    </tr>
                </tbody>
                    </table>
    </div>
</asp:Content>
