<%@ Page Language="C#" AutoEventWireup="true" CodeFile="roomstat.aspx.cs" Inherits="ClientWeb_m_all_roomstat" %>

<html>
<body>
        <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding">区域列表</div>
        </div>
    </div>
    <div class="page" data-page="p-roomstat">
        <div class="page-content">
                                <div class="list-block touch_top">
                        <ul class="uni-date-picker full theme-bg">
                            <li class="item-content"  id="room_stat_picker">
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
                                        <input type="text" class="stat_time uni_ph_trans" placeholder="点击选择时间" style="height:0;"/>
                                        <input type="hidden" class="stat_time_start" />
                                        <input type="hidden" class="stat_time_end" />
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
            <div class="content-block-title" style="margin-top:-19px;margin-bottom:5px;"><i class="icon icon-sort sign" style="font-size: 24px;"></i> <span class="grey open_time_info"></span></div>
            <div id="room_stat_list">
            </div>
        </div>
    </div>
</body>
</html>
