<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="net/Master.master" CodeFile="LabDetail.aspx.cs" Inherits="ClientWeb_xcus_bsd_xl_LabDetail" %>

<%@ Register TagPrefix="Uni" TagName="curdev" Src="net/curdev.ascx" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        $(document).ready(function () {
            //实验室基本信息
            var dev = pro.dev;
            $(".dev_title").html( dev.lab+" | "+dev.name);
            //初始化选项卡
            var req = uni.getReq();
            //预约
            if (!pro.isLogin()) {
                $("#content .login").show();
            }
            else {
                $("#content .rtest").show();
            }
        });
    </script>
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <Uni:curdev runat="server" />
    </div>
    <div>
        <div id="content">
            <h5 class="dev_name">实验室详情： <a href="LabList.aspx?clsKind=4096">返回搜索</a>>><span class="dev_title"></span>
                <span class="login hidden">>><a class="click">预约请登录</a></span><span class="rtest hidden">>>点击[<a href="Research.aspx">科研实验</a>]预约实验</span></h5>
            <div class="tabs">
                <ul class="tab_head">
                    <li><a>详细介绍</a></li>
                    <li><a>使用帮助</a></li>
                </ul>
                <div class="tab_con">
                    <div id="detail" class="item">
                        <%=ovDetail %>
                    </div>
                    <div id="lab_use_help" class="item">
                        <%=ovHelp %>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                $(".tabs").unitab();
            </script>
        </div>
        <div class="cleaner"></div>
    </div>
    <!-- END of templatemo_main -->
</asp:Content>
