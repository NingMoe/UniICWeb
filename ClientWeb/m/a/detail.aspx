<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="ClientWeb_m_all_detail" %>

<html>
<body>
    <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding"><%=Request["name"] %></div>
            <%if(Request["right"]=="resv"){ %>
            <div class="right"><a href="../a/resvstat.aspx<%=Request.Url.Query %>" class="link icon-only"><span>预约</span><i class="icon icon-forward"></i></a></div>
        <%} %>
        </div>
    </div>
    <div class="page" data-page="p-detail">
        <div class="page-content" id="ic_rsc_detail">
                    <div runat="server" id="infoAlbum">
                            <div class="swiper-container swiper_queue">
                                <div class="swiper-pagination swiper_pagination_resvstat"></div>
                                <div class="swiper-wrapper">
                                    <%=images %>
                                </div>
                            </div>
                        <div class="line"></div>
                    </div>
                    <div runat="server" id="infoIntro">
                        <div class="content-block">
                            <h3>介绍</h3>
                            <%=introContent %>
                        </div>
                    </div>
        </div>
    </div>
</body>
</html>
