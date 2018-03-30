<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sidebar.ascx.cs" Inherits="WebUserControl" %>
<!--清华-->
<style>
    .othe_line { border-top: 1px solid #776054; margin-top: 10px; padding-top: 10px; margin-bottom: 10px; }
    #space a {white-space:nowrap;}
</style>
<!--//-->
<div class="aside">
    <a href="index.aspx" id="logo">浙江大学信息共享空间</a>
    <div id="twitter">
        <a class="tweet_sina" href="">转发到新浪微博</a>
        <a class="tweet_qq" href="">转发到腾讯微博</a>
        <a class="tweet_renren" href="">转发到人人网</a>
    </div>
    <div id="aboutus">
        <h2>关于我们</h2>
        <ul>
            <li>
                <a href="space_info.aspx">空间概览</a>
            </li>

        </ul>
    </div>
    <div id="space" style="overflow:visible">
        <h2>分区入口</h2>
        <ul>
            <%=szDevCLS %>
        </ul>
    </div>
    <!--清华-->
<script>
    var list = $("#space li");
    list.each(function () {
        var id = $(this).attr("class_id");
        if (id == "10333")//逸夫馆
        {
            $(this).addClass("othe_line");
            return false;
        }
    });
</script>
<!--//-->
</div>
