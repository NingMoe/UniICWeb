<%@ Page Language="C#" MasterPageFile="Modules/Master.master" AutoEventWireup="true" CodeFile="ArticleList.aspx.cs" Inherits="ArticleList" %>

<%@ Register TagPrefix="Uni" TagName="leftMenu" Src="Modules/AtcMenu.ascx" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
    <div class="g-b-m">
        <Uni:leftMenu ID="leftMenu" runat="server" />
        <div id="content" class=" float_r" style="width: 720px;">
            <div id="position">当前位置：<a href="Default.aspx">首页</a><%=pagePosition %></div>
                                    <div id="div_article">
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
                            <iframe name="myContent" id="myContent" style="padding: 5px; margin: 0 5px;" width="710" scrolling="no" frameborder="0" src="<%=iframeUrl %>"></iframe>
                            <div runat="server" id="aff"></div>
                            <div runat="server" class="div_enclosure hidden" id="divEnclosure">
                            </div>
                            <div class="div_footer"></div>
                        </div>
            </div>
    </div>
    <div class="cleaner"></div>
</asp:Content>
