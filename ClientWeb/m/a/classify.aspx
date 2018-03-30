<%@ Page Language="C#" AutoEventWireup="true" CodeFile="classify.aspx.cs" Inherits="ClientWeb_m_all_classify" %>

<html>
<body>
    <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding"><%=title %></div>
        </div>
    </div>
    <div class="page" data-page="p-classify">
        <div class="page-content">
            <div class="banner" style="height:200px;">
                <div class="banner_con">
                <div class="font-huge" style="margin-top:50px;text-align:center;"><%=title %></div>
                </div>
            </div>
            <div class="list-block touch_top">
                <ul>
                    <%=itemList %>
                </ul>
            </div>
        </div>
    </div>
</body>
</html>
