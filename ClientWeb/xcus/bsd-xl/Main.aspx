<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="ClientWeb_xcus_bsd_xl_Main" %>

<%@ Register TagPrefix="Uni" TagName="acc" Src="~/ClientWeb/pro/net/acc.ascx" %>
<%@ Register TagPrefix="Uni" TagName="nav" Src="net/nav.ascx" %>
<%@ Register TagPrefix="Uni" TagName="include" Src="net/include.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>北师大心理学院实验室共享管理平台</title>
    <Uni:include runat="server" />
    <script type="text/javascript">
        function tabRef(index) {
            $("#m_tab").tabs("load", index);
        }
        $(function () {
            $("#m_tab").tabs({
                heightStyle: "fill",
                beforeLoad: function (event, ui) {
                    var t = ui.tab;

                }
            });
        })
    </script>
    <style type="text/css">
    </style>
</head>
<body>
    <div id="pub_resource">
        <div id="acc_info">
            <Uni:acc runat="server" ID="MyAcc" />
        </div>
    </div>
    <div id="tp_body">
        <div id="tp_header">
            <img alt="" src="theme/images/logo.jpg" id="tp_logo"/>
            <Uni:nav runat="server" ID="MyNav" />
            <div id="tp_title">实验室共享管理平台</div>
        </div>
        <!-- END of templatemo_header -->
        <div id="tp_main">
            <div id="m_tab" style="min-height: 600px;">
                <ul id="act_tabs">
                    <li><a id="tab_action" href="Research.aspx">科研实验</a></li>
                    <li><a id="" href="Action.aspx">会议室预约</a></li>
                    <li><a id="" href="User.aspx">教学预约</a></li>
                    <li><a href="User.aspx">用户中心</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div id="tp_footer">
        <p>
            版权所有：  北京师范大学心理学院
        </p>
    </div>
    <!-- END of templatemo_footer -->
</body>
</html>
