<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" MasterPageFile="../a/net/MasterPage.master" Inherits="ClientWeb_m_xcus_ic2_Default" %>

<%@ Register TagPrefix="uni" TagName="timeslector" Src="~/ClientWeb/m/a/net/md_timeselector.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <%--<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=OiF1XvObLYktppV5F2PliclW"></script>--%>
    <link rel="stylesheet" type="text/css" href="../../fm/framework7/framework7.min.css" />
    <link rel="stylesheet" type="text/css" href="../a/theme/m.css" />
   <script type="text/javascript"> 

</script>
    <style>
        .box_square { border-radius: 0; height: auto !important; }
        .box_text { padding-top: 35%; padding-bottom: 35%; font-size: 40px; }

        .touch_top { margin-top: 0; }
        .touch_top ul:before { background: none; }
        .touch_down { margin-bottom: 0; }
        .narrow_top { margin-top: 15px; }
        i.icon.sign { line-height: 1em; color: #ccc; }

        .idt_box { display: inline-block; width: 12px; height: 12px; border: solid 1px #EFEFF4; box-shadow: 0 0 1px; }
        .sel_theme { display: inline-block; width: 12px; height: 12px; border: solid 1px #EFEFF4; margin: 4px 5px; }
        .sel_theme.active { box-shadow: 0 0 3px; }
        .item_media_title { font-size: 32px; }
        .target_title { color: #fff; width: 100px; text-align: center; padding: 5px; margin-top: 20px; margin-left: 0; margin-bottom: -10px; border-top-right-radius: 20px; }
        /*图片排列*/
        .swiper_queue { height: 240px; margin: 0 0 15px 0; }
        .swiper_queue .swiper-slide { background: #fff; box-sizing: border-box; border: 1px solid #ddd; }
        .swiper_queue img { width: 100%; height: 100%; }

        #ic_rsc_detail .content-block { margin-top: 10px; margin-bottom: 10px; }
        #ic_rsc_detail h3 { font-size: 1.25em; margin-bottom: 10px; font-weight: 500; color: #333; }

        /*预约列表*/
        #my_resv_remind .icon-remind { transition: transform 300ms; }
        .resv_card .card-header { display: block; padding: 5px 10px; }
        .resv_card .card-header a { display: block; position: relative; }
        .resv_card .card-header a.active-state { opacity: .3; -webkit-transition-duration: 0ms; transition-duration: 0ms; }
        .resv_card i.icon-pullright { position: absolute; top: 0; right: -5px; opacity: .7; font-size: 30px; }
        .resv_card .card-content-inner { padding: 10px; }
        .resv_card .obj_name { font-size: 14px; font-weight: bold; color: #333; }
        .resv_card .obj_position { font-size: 13px; color: #666; }

        /*预约提交*/
        #resv_sub_picker { margin-bottom: 10px; }
        #resvsub_form .list-block .item-title.label:not(.arrow) { width: 26%; }
        #resvsub_form .list-block .item-title.label.arrow { width: 20%; }
        #resvsub_form .list-block .list-group-title { background: #fcf8e3; font-size: 12px; }
        .card .resv_info_content { padding: 10px; height: 86px; text-shadow: 0 0 2px #999; }
        .card .obj_resv_stat { padding: 10px; height: 26px; }
        /*信息栏*/
        .info_column { margin-left: 12px; margin-top: -20px; font-size: 12px; color: #777; }
        /*banner*/
        .banner { position: relative; overflow: hidden; background-color: #333; }
        .banner_svg { width: 100%; height: 100%; }
        .banner_con { position: absolute; top: 0; left: 0; width: 100%; height: 100%; text-shadow: 0 0 2px #666; background: transparent; color: white; }

        /*个人中心*/
        .banner .list-block ul:after { background: transparent; }
        /*预约动态*/
        #valid_resv_list li.valid_selected .item-title { font-weight: bold; }
        #valid_resv_list li .item-after { display: none; }
        #valid_resv_list li.valid_selected .item-after { display: initial; }
        .cur_resv_act { height: 52px; background: #fff; -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; display: -webkit-box; display: -ms-flexbox; display: -webkit-flex; display: flex; -webkit-box-pack: justify; -ms-flex-pack: justify; -webkit-justify-content: center; justify-content: center; -webkit-box-align: center; -ms-flex-align: center; -webkit-align-items: center; align-items: center; }
        .cur_resv_act a { text-align: center; padding: 2px 8px; display: inline-block; }
        .cur_resv_act a.active-state { opacity: .6; }
        .cur_resv_act a .name { font-size: 12px; color: #999; }
        .item-delete.active-state { opacity: .6; }

        /*平面图*/
        #ic_resv_stat .floor_fixed_sub { position: fixed; width: 80%; padding: 10px 5%; 
                                         margin: 0 5%; left: 0; bottom: -160px; background: white;
                                         transition:bottom 500ms;-webkit-transition:bottom 500ms;-moz-transition:bottom 500ms;
                                         border: 1px solid #ddd;box-shadow: 0 0 5px 1px #ccc;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <uni:timeslector runat="server" />
    <div class="hidden" id="uni_config">
        <input type="hidden" id="re_login" value="<%=reLogin?"true":"false" %>" />
        <input type="hidden" class="is_allday" value="<%=GetConfig("resvAllDay")%>" />
        <input type="hidden" class="is_show_close" value="<%=GetConfig("showClose")%>" />
        <input type="hidden" class="floor_plan_kind" value="<%=GetConfig("floorPlanClsKind")%>" />
        <input type="hidden" id="is_third_login" value="<%=GetConfig("loginAll")=="1"?"true":"false" %>" />
        <input type="hidden" id="cfg_resv_theme_kind" value="<%=GetConfig("resvThemeClsKind")%>" />
        <input type="hidden" id="cfg_resv_interval" value="<%=GetConfig("resvInterval") %>" />
        <input type="hidden" id="cfg_resv_range" value="<%=GetConfig("resvRange") %>" />
        <input type="hidden" id="cfg_all_day_state" value="<%=GetConfig("allDayState") %>" />
        <input type="hidden" id="cfg_mobile_prop" value="<%=GetConfig("availMobile") %>" />
        <input type="hidden" id="cfg_coords" value="<%=GetConfig("Coords") %>" />
        <input type="hidden" id="cfg_seat_resource_mode" value="<%=GetConfig("subSeatResourceMode") %>" />
        <input type="hidden" id="cfg_open_aty" value="<%=GetConfig("openActivity") %>" />
        <input type="hidden" id="cfg_show_close" value="<%=GetConfig("showClose") %>" />
        <input type="hidden" id="cfg_is_multilanguage" value="<%=GetConfig("supMultilanguage") %>" />
        <input type="hidden" id="cfg_resv_unit" value="<%=GetConfig("resvTimeUnit") %>" />
         
    </div>
    <div class="panel-overlay"></div>
    <div class="panel panel-right panel-reveal">
        <div class="content-block">
            <a href="#" class="link close-panel uni_trans"><%=Translate("关闭侧栏") %></a>
        </div>
    </div>
    <div class="panel panel-right panel-cover">
    </div>
    <div class="views">
        <%--预约状态view--%>
        <div class="view view-main">
            <div class="navbar">
                <div class="navbar-inner" data-page="p-index">
                    <div id="current_tab_title" class="center sliding uni_trans"><%=Translate("资源分类") %></div>
                    <div class="right sliding">
                        <a href="../a/article.aspx?id=m_help&type=other" class="link icon-only">
                            <i class="icon icon-question"></i>
                        </a>
                    </div>
                </div>
                <div class="navbar-inner cached" data-page="cc-contact">
                    <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span><%=Translate("返回") %></span></a></div>
                    <div class="center sliding"><%=Translate("联系方式") %></div>
                </div>
                <div class="navbar-inner cached" data-page="cc-pwd">
                    <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span><%=Translate("返回") %></span></a></div>
                    <div class="center sliding"><%=Translate("修改密码") %></div>
                </div>
                <div class="navbar-inner cached" data-page="cc-dialog" id="dft_dialog_nav">
                    <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span><%=Translate("确认") %></span></a></div>
                    <div class="center sliding"></div>
                    <div class="right"></div>
                </div>
            </div>
            <div class="pages navbar-through">
                <div class="page toolbar-fixed" data-page="p-index">
                    <div class="page-content  pull-to-refresh-content">
                        <div class="pull-to-refresh-layer">
                            <div class="preloader"></div>
                            <div class="pull-to-refresh-arrow"></div>
                        </div>
                        <div class="tabs">
                            <%--当前预约--%>
                            <div id="tab_state" class="tab">
                                <%--<div class="content-block-title">当前预约</div>--%>
                                <div class="banner" style="height: 150px;">
                                    <div class="banner_con">
                                        <div id="my_resv_remind" class="list-block media-list touch_top">
                                            <ul style="background: transparent;">
                                                <li class="item-content">
                                                    <div class="item-media">
                                                        <i class="icon icon-remind" style="font-size: 60px; color: white;"></i>
                                                    </div>
                                                    <div class="item-inner">
                                                        <div class="item-title-row" style="margin-top: 10px;">
                                                            <div class="item-title font-huge" style="color: white;"><%=Translate("提醒") %></div>
                                                            <div class="item-after" style="color: white; text-shadow: 0 0 6px #333;"></div>
                                                        </div>
                                                        <div class="item-subtitle" style="border-bottom: 1px solid #ccc; font-size: 18px;"></div>
                                                        <div class="item-text font-normal" style="color: white;"><%=Translate("没有已生效或即将生效的预约") %></div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="cur_resv_act">
                                    <a href="#" class="act_item act_leave" style="display: none;">
                                        <i class="icon icon-logout"></i>
                                        <div class="name uni_trans"><%=Translate("暂时离开") %></div>
                                    </a>
                                    <a href="#" class="act_item act_checkin" style="display: none;">
                                        <i class="icon icon-location"></i>
                                        <div class="name uni_trans"><%=Translate("签到使用") %></div>
                                    </a>
                                  <%--  <a href="#" class="act_item act_back" style="display: none;">
                                        <i class="icon icon-repeal"></i>
                                        <div class="name uni_trans"><%=Translate("返回使用") %></div>
                                    </a>--%>
                                    <%--                                    <a href="#" class="act_item act_extend" style="display: none;">
                                        <i class="icon icon-countdown"></i>
                                        <div class="name uni_trans">延长预约</div>
                                    </a>--%>
                                    <%--                                    <a href="#" class="act_item act_stop" style="display: none;">
                                        <i class="icon icon-countdown"></i>
                                        <div class="name uni_trans"><%=Translate("提前结束")%></div>
                                    </a>--%>
                                    <a href="#" class="act_refresh">
                                        <i class="icon icon-refresh"></i>
                                        <div class="name uni_trans"><%=Translate("刷新状态") %></div>
                                    </a>
                                    <a href="#" class="act_item act_more" style="display: none;">
                                        <i class="icon icon-more"></i>
                                        <div class="name uni_trans"><%=Translate("更多操作") %></div>
                                    </a>
                                </div>
                                <div class="list-block media-list touch_top">
                                    <ul id="valid_resv_list" style="margin-top: 1px;"></ul>
                                </div>
                                <div class="my_resv_list_panel" style="display: none;">
                                    <div class="content-block-title" style="margin: 12px 5px -8px 5px;"><i class="icon icon-sort sign" style="font-size: 24px;"></i><span class="grey open_time_info"><%=Translate("等待列表") %></span></div>
                                    <div class="list-block virtual-list media-list touch_top">
                                        <ul id="my_resv_list"></ul>
                                    </div>
                                </div>
                                <div id="no_resv_emotion" class="text-center">
                                    <i class="icon icon-emoji" style="color: #ccc; font-size: 120px;"></i>
                                    <div class="grey uni_trans"><%=Translate("没有预约，个人中心可以查看历史记录") %></div>
                                </div>
                            </div>
                            <%--资源列表--%>
                            <div id="tab_reserve" class="tab active">
                                <div class="banner" style="height: 160px;">
                                    <div class="banner_con">
                                        <div class="text-center">
                                            <div class="font-big" style="margin-top: 38px; margin-bottom: 5px;"><%=Translate("欢迎使用") %></div>
                                            <div class="font-normal"><%=GetConfig("SysName")%></div>
                                            <div class="text-center">
                                                <span class="sel_theme bg-blue active" data-theme="blue"></span>
                                                <span class="sel_theme bg-pink" data-theme="pink"></span>
                                                <span class="sel_theme bg-orange" data-theme="orange"></span>
                                                <span class="sel_theme bg-green" data-theme="green"></span>
                                                <span class="sel_theme bg-red" data-theme="red"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%if ((clsKind & 1) > 0)
                                  { %>
                                <div class="content-block-title target_title theme-bg"><%=Translate(rmName) %></div>
                                <div class="list-block">
                                    <ul>
                                        <li><a class="item-content item-link" href="../a/classify.aspx?classkind=1&mode=<%=GetConfig("subRmResourceMode") %>&title=<%=Server.HtmlEncode(rmName+"介绍") %>">
                                            <div class="item-inner">
                                                <div class="item-title"><%=Translate(rmName+"介绍")%></div>
                                                <div class="item-after"></div>
                                            </div>
                                        </a></li>
                                        <li><a class="item-content item-link" href="../a/search.aspx?classkind=1&prop=<%=(ToUInt(GetConfig("resvForKind"))&1)>0?"8":"" %>&title=<%=Server.HtmlEncode("预约"+rmName) %>">
                                            <div class="item-inner">
                                                <div class="item-title"><%=Translate("预约"+rmName) %></div>
                                                <div class="item-after"></div>
                                            </div>
                                        </a></li>
                                        <!--
                                        <%if (ToUInt(GetConfig("openActivity")) > 0)
                                          {%>
                                        <li><a class="item-content item-link" data-ignore-cache="true" onclick="loginedLoad('../a/openaty.aspx?_t='+(new Date()).getTime())">
                                            <div class="item-inner">
                                                <div class="item-title"><%=Translate("活动中心") %></div>
                                                <div class="item-after"></div>
                                            </div>
                                        </a></li>
                                        -->
                                        <%} %>
                                    </ul>
                                </div>
                                <%} %>

                                 <%if ((clsKind & 16) > 0)
                                  { %>
                                <div class="content-block-title target_title theme-bg"><%=Translate("活动中心") %></div>
                                <div class="list-block">
                                      <ul>
                                            <li><a class="item-content item-link" data-ignore-cache="true" onclick="loginedLoad('../a/openaty.aspx?_t='+(new Date()).getTime())">
                                            <div class="item-inner">
                                                <div class="item-title"><%=Translate("活动中心") %></div>
                                                <div class="item-after"></div>
                                            </div>
                                        </a></li>
                                            </ul>
                                </div>
                                 <%} %>

                                <%if ((clsKind & 8) > 0)
                                  { %>
                                <div class="content-block-title target_title theme-bg"><%=Translate(seatName) %></div>
                                <div class="list-block">
                                    <ul>
                                        <li style="display: <%=(quickResv&8)>0?"":"none"%>"><a class="item-content item-link" href="../a/quick.aspx?classkind=8&title=<%=Translate(Server.HtmlEncode("快速抢座"))%>">
                                            <div class="item-inner">
                                                <div class="item-title"><%=Translate("快速抢座") %></div>
                                                <div class="item-after"></div>
                                            </div>
                                        </a></li>
                                        <%if ((ToUInt(GetConfig("subSeatResourceMode")) & 4) > 0)
                                          { %>
                                        <li><a class="item-content item-link" href="../a/roomstat.aspx?classkind=8">
                                            <div class="item-inner">
                                                <div class="item-title"><%=Translate("查找"+seatName)%></div>
                                                <div class="item-after"></div>
                                            </div>
                                        </a></li>
                                        <%}
                                          else
                                          { %>
                                        <li><a class="item-content item-link" href="../a/search.aspx?classkind=8&prop=<%=(ToUInt(GetConfig("resvForKind"))&8)>0?"8":"" %>&title=<%=Server.HtmlEncode("预约"+seatName) %>">
                                            <div class="item-inner">
                                                <div class="item-title"><%=Translate("预约"+seatName) %></div>
                                                <div class="item-after"></div>
                                            </div>
                                        </a></li>
                                        <%} %>
                                    </ul>
                                </div>
                                <%} %>
                                <%if ((clsKind & 2) > 0)
                                  { %>
                                <div class="content-block-title target_title theme-bg"><%=Translate(cptName) %></div>
                                <div class="list-block">
                                    <ul>
                                        <li style="display: <%=(quickResv&2)>0?"":"none"%>"><a class="item-content item-link" href="../a/quick.aspx?classkind=2&title=<%=Server.HtmlEncode("快速抢位")%>">
                                            <div class="item-inner">
                                                <div class="item-title"><%=Translate("快速抢位") %></div>
                                                <div class="item-after"></div>
                                            </div>
                                        </a></li>
                                        <li><a class="item-content item-link" href="../a/search.aspx?classkind=2&prop=<%=(ToUInt(GetConfig("resvForKind"))&2)>0?"8":"" %>&title=<%=Server.HtmlEncode("预约电脑") %>">
                                            <div class="item-inner">
                                                <div class="item-title"><%=Translate("预约电脑") %></div>
                                                <div class="item-after"></div>
                                            </div>
                                        </a></li>
                                    </ul>
                                </div>
                                <%} %>
                                <%if ((clsKind & 4) > 0)
                                  { %>
                                <div class="content-block-title target_title theme-bg"><%=Translate(loanName) %></div>
                                <div class="list-block">
                                    <ul>
                                        <li><a class="item-content item-link" href="../a/classify.aspx?classkind=4&mode=<%=GetConfig("subLoanResourceMode") %>&title=<%=Server.HtmlEncode("设备介绍") %>">
                                            <div class="item-inner">
                                                <div class="item-title"><%=Translate("设备介绍") %></div>
                                                <div class="item-after"></div>
                                            </div>
                                        </a></li>
                                        <li><a class="item-content item-link" href="../a/search.aspx?prop=8&classkind=4&title=<%=Server.HtmlEncode("预约设备") %>">
                                            <div class="item-inner">
                                                <div class="item-title"><%=Translate("预约设备") %></div>
                                                <div class="item-after"></div>
                                            </div>
                                        </a></li>
                                    </ul>
                                </div>
                                <%} %>
                            </div>
                            <%--个人中心--%>
                            <div id="tab_center" class="tab">
                                <div class="banner" style="height: 140px;">
                                    <div class="banner_con">
                                        <div class="list-block media-list touch_top">
                                            <ul style="background: transparent;">
                                                <li class="item-content">
                                                    <div class="item-media">
                                                        <i class="icon icon-my" style="font-size: 80px; color: #eee;"></i>
                                                    </div>
                                                    <div class="item-inner">
                                                        <div class="item-title-row" style="border-bottom: 1px solid #ccc; margin-top: 20px;">
                                                            <div class="item-title user_name font-bigger" style="color: white;"></div>
                                                        </div>
                                                        <div class="item-text user_dept font-normal" style="color: white;"></div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="list-block touch_top">
                                    <ul>
                                        <li>
                                            <a href="#cc-contact" class="item-content item-link">
                                                <div class="item-media">
                                                    <i class="icon icon-phone"></i>
                                                </div>
                                                <div class="item-inner">
                                                    <div class="item-title">
                                                        <%=Translate("联系方式") %>
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a class="item-content item-link" data-ignore-cache="true" onclick="loginedLoad('../a/resvlist.aspx?_t='+(new Date()).getTime())">
                                                <div class="item-media">
                                                    <i class="icon icon-sort"></i>
                                                </div>
                                                <div class="item-inner">
                                                    <div class="item-title">
                                                        <%=Translate("预约记录") %>
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <%if (GetConfig("userCredit") == "1")
                                          { %>
                                        <li>
                                            <a class="item-content item-link" data-ignore-cache="true" onclick="loginedLoad('../a/credit.aspx?_t='+(new Date()).getTime())">
                                                <div class="item-media">
                                                    <i class="icon icon-profile"></i>
                                                </div>
                                                <div class="item-inner">
                                                    <div class="item-title">
                                                        <%=Translate("个人信用") %>
                                                    </div>
                                                    <div class="item-after credit_score"></div>
                                                </div>
                                            </a>
                                        </li>
                                        <%} %>
                                        <%if (GetConfig("needChangePsw") == "1")
                                          {//修改密码%>
                                        <li>
                                            <a href="#cc-pwd" class="item-content item-link">
                                                <div class="item-media">
                                                    <i class="icon icon-more"></i>
                                                </div>
                                                <div class="item-inner">
                                                    <div class="item-title">
                                                        <%=Translate("修改密码") %>
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <%} %>
                                    </ul>
                                </div>
                                <div class="list-block">
                                    <ul>
                                        <li>
                                            <a href="../a/article.aspx?id=m_help&type=other" class="item-content item-link">
                                                <div class="item-media">
                                                    <i class="icon icon-question"></i>
                                                </div>
                                                <div class="item-inner">
                                                    <div class="item-title"><%=Translate("使用帮助") %></div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#" class="item-content item-link" onclick="location.reload()">
                                                <div class="item-media">
                                                    <i class="icon icon-refresh"></i>
                                                </div>
                                                <div class="item-inner">
                                                    <div class="item-title">
                                                        <%=Translate("重新加载") %>
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <%if (isMark)
                                          { %>
                                        <li>
                                            <a href="#" class="item-content item-link mark_coords">
                                                <div class="item-media">
                                                    <i class="icon icon-location"></i>
                                                </div>
                                                <div class="item-inner">
                                                    <div class="item-title">
                                                        <%=Translate("标记坐标") %>
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <%} %>
                                        <li>
                                            <a href="#" class="item-content item-link click_logout">
                                                <div class="item-media">
                                                    <i class="icon icon-logout"></i>
                                                </div>
                                                <div class="item-inner">
                                                    <div class="item-title">
                                                        <%=Translate("退出") %>
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="toolbar tabbar tabbar-labels">
                        <div class="toolbar-inner">
                            <a href="#tab_state" class="tab-link">
                                <i class="icon icon-remind">
                                    <span id="cur_resv_num" class="badge bg-green">0</span>
                                </i>
                                <span class="tabbar-label"><%=Translate("预约提醒") %></span>
                            </a>
                            <a href="#tab_reserve" class="tab-link active">
                                <i class="icon icon-form"></i>
                                <span class="tabbar-label"><%=Translate("资源预约") %></span>
                            </a>
                            <a href="#tab_center" class="tab-link">
                                <i class="icon icon-my"></i>
                                <span class="tabbar-label"><%=Translate("个人中心") %></span>
                            </a>
                        </div>
                    </div>
                </div>
                <!--个人中心 内联页面-->
                <div id="page_contact" class="page cached" data-page="cc-contact">
                    <div class="page-content">
                        <div class="list-block">
                            <ul>
                                <li class="item-content">
                                    <div class="item-inner">
                                        <div class="item-title label">
                                            <%=Translate("电话") %>
                                        </div>
                                        <div class="item-input">
                                            <input type="text" class="user_phone" placeholder="..." />
                                        </div>
                                    </div>
                                </li>
                                <li class="item-content">
                                    <div class="item-inner">
                                        <div class="item-title label">
                                           <%=Translate("邮箱") %> 
                                        </div>
                                        <div class="item-input">
                                            <input type="email" class="user_email" />
                                        </div>
                                    </div>
                                </li>
                                <%if (GetConfig("optNoteAlert") == "1")
                                  {//配置接收提醒%>
                                <li class="item-content">
                                    <div class="item-inner">
                                        <div class="item-title label">
                                           <%=Translate("接收短信") %> 
                                        </div>
                                        <div class="item-input">
                                            <label class="label-switch">
                                                <input type="checkbox" class="user_receive" />
                                                <div class="checkbox"></div>
                                            </label>
                                        </div>
                                    </div>
                                </li>
                                <%} %>
                            </ul>
                        </div>
                        <div class="content-block">
                            <p><a href="#" class="button button-big click_update_contact button-fill"><%=Translate("保存修改") %></a></p>
                        </div>
                    </div>
                </div>
                <div id="page_pwd" class="page cached" data-page="cc-pwd">
                    <div class="page-content">
                        <div class="list-block">
                            <ul>
                                <li class="item-content">
                                    <div class="item-inner">
                                        <div class="item-title label">
                                            <%=Translate("原密码") %>
                                        </div>
                                        <div class="item-input">
                                            <input type="password" class="old_pwd" placeholder="..." />
                                        </div>
                                    </div>
                                </li>
                                <li class="item-content">
                                    <div class="item-inner">
                                        <div class="item-title label">
                                            <%=Translate("新密码") %>
                                        </div>
                                        <div class="item-input">
                                            <input type="password" class="new_pwd1" />
                                        </div>
                                    </div>
                                </li>
                                <li class="item-content">
                                    <div class="item-inner">
                                        <div class="item-title label">
                                            <%=Translate("重复密码") %>
                                        </div>
                                        <div class="item-input">
                                            <input type="password" class="new_pwd2" />
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div class="content-block">
                            <p><a href="#" class="button button-big click_change_pwd button-fill"><%=Translate("保存修改") %></a></p>
                        </div>
                    </div>
                </div>
                <!--dialog 内联页面 -->
                <div class="page cached" data-page="cc-dialog">
                    <div id="dft_dialog_content" class="page-content">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--模块组件-->
    <div class="modals">
        <!-- 登录模块-->
        <div class="login-screen">
            <div class="view">
                <div class="page" data-page="login-screen">
                    <div class="page-content login-screen-content">
                         
           
                        <form runat="server">
                             <input type="hidden" value="<%=aluserid %>" name="userid" id="aluserid">
                            <input type="hidden" value="<%=wxuserid %>" name="userid" id="wxuserid">
                             <input type="hidden" value="<%=schoolCode %>" name="schoolCode" id="schoolCode">
                            <input type="hidden" id="currentLan" runat="server" class="lanClass" />
                                                         <div class="login-screen-title">
                                                             <%   if (GetConfig("supMultilanguage") == "1")  {%>
                                                             <asp:Button ID="imgcn" runat="server" Text="中文" OnClick="imgcn_Click" style="background-color: transparent; border: 0;" />
                                                             <asp:Button ID="imgus" runat="server" Text="English"   OnClick="imgus_Click" style="background-color: transparent; border: 0;" />
                                                       
                     <%} %>
                          </div>
                        <div class="login-screen-title uni_trans"><%=Translate("用户登录") %></div>
                            <div class="list-block">
                                <ul>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title label"><%=Translate("用户名") %></div>
                                            <div class="item-input">
                                                <input type="text" name="username" id="username" placeholder="<%=Translate(GetConfig("idIntro")) %>">
                                            </div>
                                        </div>
                                    </li>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title label"><%=Translate("密码") %></div>
                                            <div class="item-input">
                                                <input type="password" name="password" id="password" placeholder="<%=Translate(GetConfig("pwdIntro")) %>">
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="content-block">
                                <p><a href="#" class="button button-big click_login button-fill"><%=Translate("登录") %></a></p>
                                <p class="tag text-center"><%=GetConfig("SysName") %></p>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
