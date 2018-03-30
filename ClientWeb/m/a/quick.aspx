<%@ Page Language="C#" AutoEventWireup="true" CodeFile="quick.aspx.cs" Inherits="ClientWeb_m_all_view" %>

<html>
<body>
    <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding"><%=title %></div>
        </div>
    </div>
    <div class="page" data-page="p-quick">
        <div class="page-content">
            <form class="quick_form block-list">
                <input type="hidden" name="classkind" value="<%=Request["classkind"] %>" />
                <div class="list-block touch_top">
                        <ul class="uni-date-picker full theme-bg">
                            <li class="item-content"  id="quick_date_picker">
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
                                <input type="text" name="date" class="stat_date" />
                            </div>
                        </div>
                            </li>
                        </ul>
                    </div>
                    <div class="content-block-title"><span style="font-size:20px;color:red">提醒：</span> <span class="uni_trans">记得选择预约时间、预约时长、预约类型</span></div>
                        <div class="content-block-title"><span style="font-size:36px;">1</span> <span class="uni_trans">时间范围</span></div>
            <div class="list-block">
                <ul class="range_list">
                    <%=qkRange %>
                    <li>
                        <label class="label-radio item-content">
                        <input type="radio" class="range_time" name="range">
                        <div class="item-inner"><div class="item-title">
                            <input type="text" class="stat_time" style="height:0;"/>
                            <span class="uni_trans">其它</span><span class="stat_time_text grey">(<span class="uni_trans">自定义时段</span>)</span> <a href="#" class="sel_cur_range disabled uni_trans">选时</a>
                            </div></div>
                        </label>
                    </li>
                </ul>
            </div>
                                    <div class="content-block-title"><span style="font-size:36px;">2</span> <span class="uni_trans">使用时长</span></div>
            <div class="list-block">
                <ul>
                    <%=qkInterval %>
                </ul>
            </div>
                        <div class="content-block-title <%=hideKind %>"><span style="font-size:36px;">3</span> <span class="uni_trans">类型</span></div>
            <div class="list-block <%=hideKind %>">
                <ul>
                    <%=qkKind %>
                </ul>
            </div>
<%--            <div class="content-block-title">限制条件<span class="grey">（可选）</span></div>
            <div class="list-block">
                <ul>
                    <%=qkFilter %>
                </ul>
            </div>--%>
            </form>
                                    <div class="content-block">
                            <p><a href="#" class="button button-big button-fill quick_submit">提交</a></p>
                        </div>
        </div>
    </div>
</body>
</html>
