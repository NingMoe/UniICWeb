<%@ Page Language="C#" AutoEventWireup="true" CodeFile="credit.aspx.cs" Inherits="ClientWeb_m_all_view" %>

<html>
<body>
    <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding">个人信用</div>
        </div>
    </div>
    <div class="page" data-page="p-credit">
        <div class="page-content">
            <div class="list-block media-list touch_top touch_down">
                <ul class="theme-bg">
                    <li class="item-content">
                        <div class="item-media">
                            <i class="icon icon-profile" style="font-size: 36px;color:#fff;"></i>
                        </div>
                        <div class="item-inner">
                            <div class="item-text" style="height: auto;color:#fff;">
                                <table style="width:100%">
                                    <thead>
                                        <tr>
                                            <th class="uni_trans">系统</th>
                                            <th class="text-center uni_trans">剩余</th>
                                            <th class="text-center uni_trans">总积分</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%=detail %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <%--<div class="content-block-title">信用记录</div>--%>
            <div id="my_resv_list" class="list-block media-list touch_top">
                <ul>
                    <%=creditrec %>
                </ul>
            </div>
        </div>
    </div>
</body>
</html>
