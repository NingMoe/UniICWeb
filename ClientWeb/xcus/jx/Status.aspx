<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="net/Master.master" CodeFile="Status.aspx.cs" Inherits="ClientWeb_xcus_jx_Status" %>

<%@ MasterType VirtualPath="net/Master.master" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.sch.css" rel='stylesheet' />
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <style>
    </style>
    <script>
        $(function () {
            var dt = new Date();
            $(".cur_time").html(dt.format("HH:mm"));
            $(".cur_date").html(dt.format("yyyy年MM月dd日"));

            var term = pro.term;
            var cld = $("#calendar").uniCalendar({
                mode: "m",
                modes: "m",
                style: "cld",
                width: 1170,
                cellHeight: 65,
                borderWidth: 1,
                relative: true,
                schedule: true,
                secnum: 8,
                dateStart: term.start,
                dateEnd: term.end,
                evFinishDraw: function (dt, idate, opt, iqz) {
                    var wwd = pro.dt.date2wwd(dt);
                    $("#info_panel .week_num").html(parseInt(wwd / 10));
                    $("[data-content]", iqz).popover({
                        html: true,
                        placement: 'auto',
                        trigger: 'hover'
                    });
                }
            });
            //初始化数据
            var acc = pro.acc;
            if ((parseInt(acc.ident) & 536870912) > 0) {
                pro.j.rsv.getTestPlanResv(null, term.year, function (rlt) {
                    callback(rlt.data,cld);
                });
            }
            else {
                pro.j.rsv.getTestResvInfo(term.year,function (rlt) {
                    callback(rlt.data,cld);
                });
            }
        });
        function callback(list, cld) {
            var list = $(list);
            list.each(function () {
                var ltch = this.ltch;
                var start = parseInt(ltch / 100),
                end = parseInt(start / 100) * 100 + (ltch % 100);
                this.start = start;
                this.end = end;
            });
            cld.uploadCld(null, {
                plans: list
            });
        }
    </script>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
    <div class="btn-group" style="margin-left:10px;">
        <button type="button" class="btn btn_info cur_term_name"><%=curTerm %></button>
        <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown">
            <span class="caret"></span><span></span>
        </button>
        
        <ul class="dropdown-menu" role="menu" style="position:absolute;">
            <%=termList %>
        </ul>
    </div>
    <div id="calendar"></div>
</asp:Content>
