<%@ Page Language="C#" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="ClientWeb_m_all_search" %>

<html>
<body>
    <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding"><%=title %></div>
            <div class="right"><a href="#cc-dialog" class="link icon-only"><span>筛选</span><i class="icon icon-forward"></i></a></div>
        </div>
    </div>
    <div class="page" data-page="p-search">
        <div class="page-content">
            <div class="list-block touch_top">
                <ul class="uni-date-picker full theme-bg">
                    <li class="item-content" id="search_stat_picker">
                        <div>
                            <div class="act_panel">
                                <div class="item-media"><i class="icon icon-calendar"></i><span class="flag_today">今日</span></div>
                                <div class="item-title label arrow">
                                    <a href="#" class="link icon-only add"><i class="icon icon-right"></i></a>
                                </div>
                                <div class="item-title label arrow">
                                    <a href="#" class="link icon-only minus"><i class="icon icon-left"></i></a>
                                </div>
                            </div>
                            <div class="item-input">
                                <input type="text" class="stat_date" />
                            </div>
                        </div>
                    </li>
                    <li class="item-content stat_time_li">
                        <div class="item-media"><i class="icon icon-time am_rotate"></i></div>
                        <div class="item-inner">
                            <div class="item-input">
                                <div class="visual_div click_sel_time"><span class="grey uni_trans">点击选择时间</span></div>
                                <%--<input type="text" class="stat_time" placeholder="点击选择时间"/>--%>
                                <input type="text" class="stat_time"  style="height:0;"/>
                                <input type="hidden" class="stat_time_start" />
                                <input type="hidden" class="stat_time_end" />
                            </div>
                        </div>
                    </li>
                </ul>
            </div>

            <div style="margin-left: 12px; margin-top: -20px; font-size: 12px; color: #777;">
                <span class="idt_box" style="background: #ccc;"></span><span class="uni_trans">无效</span>&nbsp;&nbsp;<span class="idt_box theme-bg"></span> <span class="uni_trans">忙碌</span>
            </div>
            <div class="search_list_panel"></div>
        </div>
        <div class="hidden" id="search_filter_content">
            <div class="hidden">
                <input type="hidden" name="prop" class="filter_prop" value="<%=prop %>" />
                <input type="hidden" name="purpose" class="filter_purpose" value="<%=purpose %>" />
                <input type="hidden" name="kinds" class="filter_kinds" value="<%=availableKinds %>" />
                <input type="hidden" class="filter_cls_rooms" value="<%=closeRoom %>" />
            </div>
            <form>
                <%=itemList %>
            </form>
        </div>
    </div>
</body>
</html>
