<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resvlist.aspx.cs" Inherits="ClientWeb_m_all_view" %>

<html>
<body>
        <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding">预约记录</div>
        </div>
    </div>
    <div class="page" data-page="p-resvlist">
        <div class="page-content">
                        <div class="list-block media-list touch_top touch_down">
                <ul class="theme-bg">
                    <li class="item-content">
                        <div class="item-media">
                            <i class="icon icon-form" style="font-size: 36px;color:#fff;"></i>
                        </div>
                        <div class="item-inner">
                            <div  style="height: auto;color:#fff;">
                                <table style="width:100%">
                                    <thead>
                                        <tr>
                                            <th class="text-center uni_trans">三月内</th>
                                            <th class="text-center uni_trans">半年内</th>
                                            <th class="text-center uni_trans">一年内</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr><%=detail %></tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <%--<div class="content-block-title"><i class="icon icon-sort" style="color:#ccc;"></i> <span>预约记录</span></div>--%>
            <div class="list-block media-list touch_top">
                <div class="list-group">
                <ul>
                    <li class="list-group-title uni_trans">近3月内</li>
                    <%=resvlist3 %>
                </ul>
                </div>
                                <div class="list-group">
                <ul>
                    <li class="list-group-title uni_trans">近3月 - 半年</li>
                    <%=resvlist6 %>
                </ul>
                </div>
                                <div class="list-group">
                <ul>
                    <li class="list-group-title uni_trans">半年 - 1年</li>
                    <%=resvlist12 %>
                </ul>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
