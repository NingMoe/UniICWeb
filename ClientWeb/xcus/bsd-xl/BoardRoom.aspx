<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="BoardRoom.aspx.cs" Inherits="ClientWeb_xcus_bsd_xl_BoardRoom" %>
<%@ Register TagPrefix="Uni" TagName="tblmeeting" Src="net/tblmeeting.ascx" %>
<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
        <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.green.css" rel='stylesheet' />
    <script type="text/javascript">
        function selDev(dev) {
            dev=$(dev);
            var id = dev.attr("devId");
        }
    </script>
    <style type="text/css">

    </style>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <div class="f-fl qzone ui-tabs ui-widget ui-corner-all" id="act_list">
                        <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
                <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a class="ui-tabs-anchor">研讨室列表</a></li>
            </ul>
                <div>
                    <ul class="menu">
                       <li><a href='?classId=0' classId="0" onclick='selBoardRoom(this)'><span class='ui-icon ui-icon-calculator'></span>全部研讨室预约状态</a></li>
                    </ul>
                    <ul class="menu">
                        <%=clsList %>
                    </ul>
                </div>
            <script type="text/javascript">
                var menu =$(".menu").menu();
                var req = uni.getReq();
                var clsId = req["classId"];
                $("li a", menu).each(function () {
                    var pthis = $(this);
                    var id = pthis.attr("classId");
                    if (clsId) {
                        if (id == clsId) {
                            pthis.addClass("ui-state-default");
                            return;
                        }
                    }
                    else {
                        if (id == "0") {
                            pthis.addClass("ui-state-default");
                        }
                    }
                });
            </script>
        </div>
        <div class="f-fr qzone ui-tabs ui-widget ui-corner-all" id="act_qzone">
                                    <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
                <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a class="ui-tabs-anchor">预约状态表</a></li>
            </ul>
            <Uni:tblmeeting runat="server" ID="MyCld"/>
        </div>
    </div>
</asp:Content>
