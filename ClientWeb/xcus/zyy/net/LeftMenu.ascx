<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftMenu.ascx.cs" Inherits="DevWeb_Modules_LeftMenu" %>

<div id="sidebar" class="float_l" style="min-height:630px;">
            <div class="sidebar_box">
                <div class="s_h_box">仪器共享</div>
                <div class="s_c_box">
                    <ul class="sidebar_list">
                        <li class="first"><a class="click" href="DevList.aspx">仪器查找</a></li>
                        <li><a class="click"  href="DevList.aspx?prc=100">百万仪器</a></li>
                        <li class="last"><a class="click"  href="ArticleList.aspx?gr=3&cl=10007">使用说明</a></li>
                    </ul>
                </div>
            </div>
            <div id="left_img" style="position:relative;margin-top:40px;">
                        <img alt="" src="Theme/images/side_img/side.jpg" />
                    </div>
        </div>