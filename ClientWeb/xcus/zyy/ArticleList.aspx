<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="ArticleList.aspx.cs" Inherits="ArticleList" %>

<%@ Register TagPrefix="Uni" TagName="leftMenu" Src="net/AtcMenu.ascx" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function getArt(id,gr) {
            location.href = location.pathname + "?gr="+gr+"&art=" + id;
        }
        function getArtList() {
            $("#div_list").show();
            $("#div_article").hide();
            var req = uni.getReq();
            var cl = req["cl"];
            var pc = getPageCtrl();
            pro.j.art.getArtListByCls(cl, pc.start, pc.need, function (rlt) {
                var list = rlt.data;
                var str = "";
                $(list).each(function () {
                    str += "<li><span>" + this.date + "</span><a title=" + this.title + " onclick='getArt(\"" + this.id + "\",\"" + this.gr + "\")'>" + this.title + "</a></li>";
                });
                $("#artList").html(str);
                var pc = rlt.ext;
                updatePageCtrl(pc.total, pc.start, pc.need);
                $("[title]").tooltip();
            });
        }
        $(function () {
            $("#p_ctrl").pageCtrl(function () {
                getArtList();
            }, 15);
            if ("<%=showMode%>" == "list") {
                getArtList();
            }
            else {
                $("#div_list").hide();
                $("#div_article").show();
            }
        });
    </script>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
    <div style="min-height:430px;">
        <Uni:leftMenu ID="leftMenu" runat="server" />
        <div id="content" class=" float_r" style="width: 730px;">
            <div id="position" class="div_position">当前位置：<a href="Default.aspx">首页</a><%=pagePosition %></div>
            <div id="div_list" style="display: none">
                <div class="div_list">
                    <ul id="artList">
                    </ul>
                </div>
                <div id="p_ctrl">
                </div>
            </div>
            <div id="div_article" style="display: none;">
                <div class="div_title">
                    <asp:Label runat="server" ID="lbArticleTitle"></asp:Label>
                </div>
                <div class="div_attribute">
                    <ul>
                        <li><span class="t">来源：</span><asp:Label runat="server" ID="lbAuthor" Text="本站"></asp:Label></li>
                        <li><span class="t">点击：</span><asp:Label runat="server" ID="lbHit" Text="8"></asp:Label></li>
                        <li><span class="t">时间：</span><asp:Label runat="server" ID="lbTime"></asp:Label></li>
                    </ul>
                </div>
                <iframe name="myContent" id="myContent" style="padding: 5px;" width="720" scrolling="no" frameborder="0" src="<%=iframeUrl %>"></iframe>
                <div class="div_footer"></div>
            </div>
        </div>
    </div>
    <div class="cleaner"></div>
</asp:Content>
