<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="net/Master.master" CodeFile="ArticleList.aspx.cs" Inherits="ClientWeb_xcus_bsd_xl_ArticleList" %>
<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
        <script type="text/javascript">
            $(function () {
                function setiFrameWidth(r) {
                    if ($.browser.msie && ($.browser.version == "6.0") && !$.support.style) {
                        setTimeout(function () {
                            return iFrameWidth(r);
                        }
                            , 100);
                    }
                }
                function iFrameWidth(f) {
                    var ifm = document.getElementById(f.id);
                    var subWeb = document.frames ? document.frames[f.name].document : ifm.contentDocument;
                    //alert(subWeb.body.scrollWidth); 
                    if (ifm != null && subWeb != null) {
                        ifm.width = subWeb.body.scrollWidth;
                        return false;
                        //ie6下iframewidth100% 有margin的bug 
                    }
                }
            });
    </script>
    </asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <div id="content" class="float_r panel_devlist">
            <div id="rlt_con" style="margin-top:10px;">
                                <div class='m-boxes ui-tabs ui-widget ui-corner-all' id="devbox" style="min-height:480px;">
                                    <ul class="h_grey ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
                    <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a class="ui-tabs-anchor"><span><%=artTitle %></span></a></li>
                </ul>
                    <div>
                        <iframe name="myContent" id="myContent" width="965" scrolling="no" frameborder="0" src="<%=iframeUrl %>"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- END of templatemo_main -->

</asp:Content>
