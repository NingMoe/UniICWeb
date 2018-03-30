<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="ArtSearch.aspx.cs" Inherits="ArtSearch" %>

<%@ Register TagPrefix="Uni" TagName="leftMenu" Src="net/AtcMenu.ascx" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function getArt(id, gr) {
            location.href = "ArticleList.aspx?gr=" + gr + "&art=" + id;
        }
        function getArtList() {
            var req = uni.getReq();
            var key = req["key"];
            var pc = getPageCtrl();
            debugger;
            pro.j.art.getArtListByKey(key, pc.start, pc.need, function (rlt) {
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
            getArtList();
        });
    </script>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
    <div style="min-height: 430px;">
        <Uni:leftMenu ID="leftMenu" runat="server" />
        <div id="content" class=" float_r" style="width: 730px;">
            <div id="position" class="div_position">当前位置：<a href="Default.aspx">首页</a>>>文章搜索</div>
            <div id="div_list">
                <div class="div_list">
                    <ul id="artList">
                    </ul>
                </div>
                <div id="p_ctrl">
                </div>
            </div>
        </div>
    </div>
    <div class="cleaner"></div>
</asp:Content>
