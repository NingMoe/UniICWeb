<%@ Page Language="C#" AutoEventWireup="true" CodeFile="atydetail.aspx.cs" Inherits="ClientWeb_m_all_openaty" %>

<html>
<body>
    <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding">活动详情</div>
        </div>
    </div>
    <div class="page" data-page="p-atydetail">
        <div class="page-content" style="background:#fff;">
            <div class="aty_detail_img">
                <%=image %>
            </div>
            <div class="aty_detail_panel">
                <%=atyDetails %>
            </div>
        </div>
    </div>
</body>
</html>
