<%@ Page Language="C#" AutoEventWireup="true" CodeFile="article.aspx.cs" Inherits="ClientWeb_m_all_article" %>
<html>
    <body>
        <div class="navbar">
            <div class="navbar-inner">
                <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
                <div class="center sliding"><%=title %></div>
            </div>
        </div>
        <div class="page no-toolbar" data-page="article">
                    <style>
            #divContent .info_date {text-align:right;font-size:12px;height:14px;line-height:14px;color:#999;}
        </style>
            <div class="page-content">
                <div class="content-block">
                    <div runat="server" id="divContent" class="article_content"></div>
                </div>
            </div>
        </div>
    </body>
</html>
