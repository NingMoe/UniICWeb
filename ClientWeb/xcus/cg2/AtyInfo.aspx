<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="../a/net/MasterPage.master" CodeFile="AtyInfo.aspx.cs" Inherits="ClientWeb_xcus_cg2_AtyInfo" %>

<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <link href="<%=ResolveClientUrl("~/ClientWeb/")  %>md/Timepickeraddon/jquery-ui-timepicker-addon.css" rel="stylesheet" />
    <script src="<%=ResolveClientUrl("~/ClientWeb/")  %>md/Timepickeraddon/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <script src="<%=ResolveClientUrl("~/ClientWeb/")  %>md/Timepickeraddon/jquery-ui-timepicker-zh-CN.js" type="text/javascript"></script>
    <link rel="stylesheet" href="theme/cus.css" />
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <style>
        .info_unitab .tab_head { padding-left: 10px; }
        .info_unitab .tab_head li { margin-right: 5px; }
        .info_unitab .tab_head li .title { line-height: 16px; padding: 5px 23px; }
        .info_unitab .tab_head li .title div.week { line-height: 14px; font-size: 12px; }
        .info_unitab .tab_head li .title.special { height: 40px; }
        .info_unitab .detail { font-family: '宋体'; font-size: 12px; }
        .info_unitab .aty { font-size: 16px; }
        .aty_item { border: 1px solid #eee; background: #FFFBEA; padding: 2px 6px; margin-bottom: 1px; }
        .aty_item li h3 { margin-top: 5px; margin-bottom: 3px; color: #555; }
        .aty_item .yard { margin-bottom: 5px; }
        .aty_item .aty { font-weight: bold; }
        #jumbotron {display:none;}
        .navbar-right { display:none;}
        .bs-docs-footer { display:none;}
    </style>
    <script>
        var getFilter;
        $(function () {
            //初始化筛选
            getFilter = $("#filter").filterItem(RetSelecteds);
            //初始化日期
            $("#filter .sel_date").datepicker();
            RetSelecteds(getFilter()); //首次返回结果
            //活动场景选定
            $("#aty_type a").click(function (e) {
                if ($(this).hasClass("f_sel")) {
                    var id = $(this).attr("value");
                    uni.hr.loadHtml(null, { "aty_id": id });
                }
                else {
                    uni.hr.loadHtml(null, {});
                }
                e.preventDefault();
            });
        });
        function RetSelecteds(fl) {
            if (!fl) fl = {};
            fl.start = $("#filter .date_start").val();
            fl.end = $("#filter .date_end").val();
            pro.j.rsv.getYardRsv(fl, function (rlt) {
                var list = rlt.data;
                var today = new Date();
                var td = today.format("yyyy-MM-dd");
                var its = $(".tab_con div.item").html("");
                var panel = $(".info_list").html("");
                for (var i = 0; i < list.length; i++) {
                    var rsv = list[i];
                    var it = getItem(rsv);
                    var dt = rsv.start.substr(0, 10);
                    var date = dt.replace(/-/g, '');
                    if (panel.find(".dt_" + date).length == 0) {
                        panel.append("<h1 class='h_title'>" + rsv.start.substr(5, 5) + (td == dt ? "  <span class='red'> 今日</span>" : "") + "</h1><div class='line'></div><div class='dt_" + date + "'></div>");
                    }
                    panel.find(".dt_" + date).append(it);
                }
                if (panel.html() == '') { panel.html("<h3 class='text-center'>没有活动</h3>")}
            });
        }
        function getItem(rsv) {
            var date = rsv.start.substr(5) + "-" + rsv.end.substr(11);
            var it = '<ul class="aty_item"><li class="title"><h3>' + (rsv.start).substr(11) + '</h3><span class="aty">' + rsv.name + "&nbsp;&nbsp;&nbsp;" + rsv.atyName + '</span></li>' +
            '<li class="yard"><span class="text-primary">' + rsv.devName + '&nbsp;' + rsv.devDept + '</span></li>' +
            '<li class="detail grey"><span><span class="glyphicon glyphicon-home"></span>&nbsp;位置：</span>' + rsv.campus + '&nbsp;' + rsv.labName + '&nbsp;' + rsv.roomName +
            '&nbsp;&nbsp;<span><span class="glyphicon glyphicon-user"></span>&nbsp;组织方：</span>' + rsv.org + '&nbsp;' + rsv.orger + '<span class="grey pull-right">' + date + '</span></li></ul>';
            return it;
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
        <div id="filter" class="panel panel-default">
        <input type="hidden" key="state" value="2" />
        <div class="panel-heading"><span class="glyphicon glyphicon-list"></span>&nbsp;过滤条件</div>
        <table class="table table-condensed">
            <tr class="its" key="campus" affect="building_id">
                <td class="text-right">校区：</td>
                <td class="text-left" colspan="3">
                    <%=CampusList %>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 15%;">位置：</td>
                <td class="text-left">
                    <select key="building_id" style="width: 200px;" class="form-control auto">
                        <option value="0">全部</option>
                        <%=BuildingList %>
                    </select>
                </td>
                <td class="text-right" style="width: 15%;">活动名称：</td>
                <td class="text-left">
                    <input class="form-control" type="text" key="resv_name" />
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 15%;">日期：</td>
                <td class="text-left" colspan="3">
                    <input type="text" class="date_start sel_date" readonly="readonly" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>">
                    -
                    <input type="text" class="date_end sel_date" readonly="readonly" value="<%=DateTime.Now.AddDays(6).ToString("yyyy-MM-dd") %>">
                    &nbsp;
                    <button type="button" class="btn btn-info sub_filter">&nbsp;查询&nbsp;</button>&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div  class="panel panel-default">
        <div class="panel-heading"><span class="glyphicon glyphicon-list"></span>&nbsp;日期列表</div>
<div class="info_list"></div>
    </div>
    
</asp:Content>
