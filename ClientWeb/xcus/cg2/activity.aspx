<%@ Page Language="C#" AutoEventWireup="true" CodeFile="activity.aspx.cs" Inherits="ClientWeb_xcus_cg2_activity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <style>
        .info_unitab .tab_head { padding-left: 10px; }
        .info_unitab .tab_head li { margin-right: 5px; }
        .info_unitab .tab_head li .title { line-height: 16px; padding: 5px 23px; }
        .info_unitab .tab_head li .title div.week{ line-height: 14px; font-size:12px;}
        .info_unitab .tab_head li .title.special {height:40px;}
        .info_unitab .detail { font-family: '宋体'; font-size: 12px; }
        .info_unitab .aty { font-size:16px; }
        .aty_item { border-bottom: 1px solid #ddd;background: #FFFBEA;
padding: 2px 4px; }
        .aty_item li h2 { margin-top:5px;}
        .aty_item .yard { margin-bottom: 5px; }
    </style>
    <script>
        var getFilter;
        $(function () {
            //初始化筛选
            getFilter = $("#filter").filterItem(RetSelecteds);
            //初始化日期
            var dt = new Date();
            //dt.addDays(3);
            var head = $(".info_unitab .tab_head");
            for (var i = 0; i < 7; i++) {
                var str;
                if (i == 0) str = "今日";
                else if (i == 1) str = "明日"
                else if (i == 2) str = "后天";
                else str = dt.format("MM月dd日");
                $("li:eq(" + i + ") div.title", head).html(str+"<div class='week'>"+dt.format("星期E")+"</div>");
                dt.addDays(1);
            }
            RetSelecteds(getFilter()); //首次返回结果
            //初始化日期
            $("#datepicker").datepicker();
            $("#datepicker").change(function () { RetSelecteds(getFilter()); });
            //活动场景选定
            $("#aty_type a").click(function () {
                if ($(this).hasClass("f_sel")) {
                    var id = $(this).attr("value");
                    uni.hr.loadHtml(null, { "aty_id": id });
                }
                else {
                    uni.hr.loadHtml(null, {});
                }
            });
        });
        function RetSelecteds(fl) {
            if (!fl) fl = {};
            var today = new Date();
            fl.start = today.format("yyyyMMdd");
            today.addDays(6);
            fl.end = today.format("yyyyMMdd");
            //fl.multi = "true";
            pro.j.rsv.getYardRsv(fl, function (rlt) {
                var list = rlt.data;
                var today = new Date();
                var its=$(".tab_con div.item").html("");
                for (var i = 0; i < list.length; i++) {
                    var rsv = list[i];
                    var it = getItem(rsv);
                    var date = uni.parseDate(rsv.start)
                    var index = parseInt(uni.compareDate(date, today))
                    $(".tab_con div.item:eq(" + index + ")").append(it);
                }
                its.each(function (i) {
                    if (i < 7) {
                        var pthis = $(this);
                        if (pthis.html() == "") pthis.html("<p>无记录</p>");
                    }
                });
            });
            var date = $("#datepicker").val();
            if (date) {
                date = date.replace(/-/g, '');
                fl.start = date;
                fl.end = date;
                pro.j.rsv.getYardRsv(fl, function (rlt) {
                    var list = rlt.data;
                    var div = $(".tab_con div.sel_date").html("");
                    for (var i = 0; i < list.length; i++) {
                        div.append(getItem(list[i]));
                    }
                    if (div.html() == "") div.html("<p>无记录</p>");
                });
            }
        }
        function getItem(rsv) {
            var starts = rsv.starts ? rsv.starts.split(',') : new Array(rsv.start);
            var ends = rsv.ends ? rsv.ends.split(',') :new Array(rsv.end);
            if (starts == null || ends == null) return;
            var date = [];
            for (var i = 0; i < starts.length; i++) {
                date.push( starts[i].substring(5) + "/" + ends[i].substring(5) );
            }
            date = date.join(",");
            var it = '<ul class="aty_item"><li class="title"><h2>' + (rsv.start).substr(11) + '</h2><span class="aty">' + rsv.name + "&nbsp;&nbsp;&nbsp;" + rsv.atyName + '</span></li>' +
            '<li class="yard"><span class="text-primary">' + rsv.devName + '&nbsp;' + rsv.devDept + '</span></li>' +
            '<li class="detail grey"><span><span class="glyphicon glyphicon-home"></span>&nbsp;位置：</span>' + rsv.campus + '&nbsp;' + rsv.labName + '&nbsp;' + rsv.roomName +
            '&nbsp;&nbsp;<span><span class="glyphicon glyphicon-user"></span>&nbsp;组织方：</span>' + rsv.org + '&nbsp;' + rsv.orger + '<span class="grey pull-right">' + date + '</span></li></ul>';
            return it;
        }
    </script>
    <div id="filter" class="panel panel-default">
        <input type="hidden" key="only_open" value="true" />
        <input type="hidden" key="aty_type" value="<%=atyId %>" />
        <div class="panel-heading"><span class="glyphicon glyphicon-list"></span>&nbsp;过滤条件</div>
        <table class="table table-condensed">
            <tr class="its" id="aty_type">
                <td class="text-right">活动场景：</td>
                <td class="text-left">
                    <%=AtyTypeList %>
                </td>
            </tr>
            <tr class="its" key="campus" affect="building_id">
                <td class="text-right">校区：</td>
                <td class="text-left">
                    <%=CampusList %>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 15%;">位置：</td>
                <td class="text-left">
                    <select key="building_id" style="width:200px;" class="form-control auto">
                        <option value="0">全部</option>
                    <%=BuildingList %>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <div class="info_unitab">
        <ul class="tab_head">
            <li>
                <div class="title">
                    今日
                    <div class="week"></div>
                </div>
                <div class="caret"></div>
            </li>
            <li>
                <div class="title">
                    明日
                    <div class="week"></div>
                </div>
                <div class="caret"></div>
            </li>
            <li>
                <div class="title">
                    后天
                    <div class="week"></div>
                </div>
                <div class="caret"></div>
            </li>
            <li>
                <div class="title">
                    00月00日
                    <div class="week"></div>
                </div>
                <div class="caret"></div>
            </li>
            <li>
                <div class="title">
                    00月00日
                    <div class="week"></div>
                </div>
                <div class="caret"></div>
            </li>
            <li>
                <div class="title">
                    00月00日
                    <div class="week"></div>
                </div>
                <div class="caret"></div>
            </li>
            <li>
                <div class="title">
                    00月00日
                    <div class="week"></div>
                </div>
                <div class="caret"></div>
            </li>
            <li>
                <div class="title special">
                    <div>
                        <input type="text" id="datepicker" readonly="true" placeholder="指定日期" style="width: 100px;"></div>
                </div>
                <div class="caret"></div>
            </li>
        </ul>
        <div class="tab_con">
            <div>
            </div>
            <div>
            </div>
            <div>
            </div>
            <div></div>
            <div></div>
            <div></div>
            <div></div>
            <div class="sel_date"></div>
        </div>
    </div>
    <script>
        $(".info_unitab").unitab();
    </script>
</body>
</html>
