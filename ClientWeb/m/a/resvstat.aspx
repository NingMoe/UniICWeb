<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resvstat.aspx.cs" Inherits="ClientWeb_m_all_resvstat" %>

<html>
<body>
    <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding"><%=Request["name"] %></div>
            <%if (Request["right"] == "detail")
              { %>
            <div class="right"><a href="../a/detail.aspx<%=Request.Url.Query %>" class="link icon-only"><span>介绍</span><i class="icon icon-forward"></i></a></div>
            <%} %>
        </div>
    </div>
    <div class="page" data-page="p-resvstat">
        <div class="page-content" id="ic_resv_stat">
            <div class="list-block touch_top">
                <ul class="uni-date-picker <%=isFloorPlan?"":"full theme-bg" %>">
                    <li class="item-content" id="resv_stat_picker">
                        <%if (!isFloorPlan)//完整版
                          { %>
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
                        <%}
                          else//简化版
                          { %>
                        <div class="item-media"><i class="icon icon-calendar"></i></div>
                        <div class="item-inner">
                            <div class="item-title label arrow">
                                <a href="#" class="link icon-only minus"><i class="icon icon-back"></i></a>
                            </div>
                            <div class="item-input">
                                <input type="text" class="stat_date" />
                            </div>
                            <div class="item-title label arrow">
                                <a href="#" class="link icon-only add"><i class="icon icon-forward"></i></a>
                            </div>
                        </div>
                        <%} %>
                    </li>
                    <li class="item-content stat_time_li">
                        <div class="item-media"><i class="icon icon-time <%=isFloorPlan?"":"am_rotate" %>"></i></div>
                        <div class="item-inner">
                            <div class="item-input">
                                <div class="visual_div click_sel_time"><span class="grey uni_trans">点击选择时间</span></div>
                                <input type="text" class="stat_time uni_ph_trans" placeholder="点击选择时间" style="height: 0;" />
                                <input type="hidden" class="stat_time_start" />
                                <input type="hidden" class="stat_time_end" />
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="resv_list_con" style="display: none;">
                <div class="info_column">
                    <span class="idt_box" style="background: #ccc;"></span><span class="uni_trans">无效</span>&nbsp;&nbsp;<span class="idt_box theme-bg"></span> <span class="uni_trans">忙碌</span>
                        <span style="float: right; margin-right: 12px"><%=unit %></span>
                </div>
                <div class="resv_list_panel">loading...</div>
            </div>
            <div class="resv_floor_con" style="display: none;">
                <div class="info_column" style="margin-bottom: 5px;">
                    <span><span class="fp-dot fp-dot-ok"></span><span class="uni_trans">空闲</span>&nbsp;<span class="fp-dot fp-dot-busy" style="position: relative;">
                        <div class="fp-dot-layer fp-layer-0"></div>
                        <div class="fp-dot-layer fp-layer-1"></div>
                    </span><span class="uni_trans">半空闲</span>&nbsp;<span class="fp-dot fp-dot-busy"></span><span class="uni_trans">忙碌</span>&nbsp;<span class="fp-dot fp-dot-close"></span><span class="uni_trans">不开放</span>&nbsp;</span>
                    <span class="am_shake" style="float: right; margin-left: 8px; font-weight: bold;">&nbsp<span class="uni_trans">双击缩放</span>&nbsp</span>
                </div>
                <div class="resv_floor_panel" style="border-top: solid 1px #bbb; border-bottom: solid 1px #bbb;"></div>
                <p class="text-center grey uni_trans" style="font-size:12px;">提示：点击状态点预约，双击平面图可缩放</p>
                <div class="content-block floor_fixed_sub">
                    <p class="text-center grey sel_intro text-ellipsis uni_trans" style="margin: 10px; font-size: 16px; overflow: hidden;">未选择</p>
                    <div class="button-row">
                    <a href="#" class="button click_sel_apply button-fill">选定</a>
                    <a href="#" class="button click_cancel" style="margin-top:10px;">取消</a>
                </div></div>
            </div>
                                <%--svg 滤镜--%>
        <svg height="0" xmlns="http://www.w3.org/2000/svg">
            <filter id="fp-drop-shadow">
                <feGaussianBlur in="SourceAlpha" stdDeviation="2.2" />
                <feOffset dx="0" dy="0" result="offsetblur" />
                <feFlood flood-color="rgba(0,0,0,0.8)" />
                <feComposite in2="offsetblur" operator="in" />
                <feMerge>
                    <feMergeNode />
                    <feMergeNode in="SourceGraphic" />
                </feMerge>
            </filter>
        </svg>
        </div>
    </div>
</body>
</html>
