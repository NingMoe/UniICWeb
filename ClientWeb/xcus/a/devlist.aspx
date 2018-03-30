<%@ Page Language="C#" AutoEventWireup="true" CodeFile="devlist.aspx.cs" Inherits="ClientWeb_xcus_all_devlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <script>
        //条件对象
        var pc;
        var getFilter;
        $(function () {
            //初始化分页控件
            pc = $("#pCtrl").pageCtrl(function (pageCtrlID, startLine, needLine) {
                RetSelecteds(getFilter(), "pctrl");
            }, 16);
            //初始化筛选
            getFilter = $("#filter").filterItem(RetSelecteds, {multi:true});
            //获取搜索关键字
            var Request = uni.getReq();
            var key = Request["key"];
            if (key != undefined && key != '') {
                $("#keyword").val(decodeURI(key));
            }
            RetSelecteds(getFilter()); //首次返回结果
        });

        function RetSelecteds(filter, type) {
            var result = filter || {};
            //分页
            result.pctrlId = "pCtrl";
            if (type == "pctrl")
                result.pctrlStar = pc.getStart();
            else
                result.pctrlStar = "1";//筛选需重置分页
            result.pctrlNeed = pc.getNeed();
            //显示模式
            result.showMode = $("#show_mode input:checked").val();
            SubmitRet(result);
        }
        //提交搜索
        function SubmitRet(condition) {
            pro.j.dev.devFilter(condition, function (con) {
                var content = '';
                rlt = con.data;
                var devs = rlt.devs;
                //显示模式
                var showMode = 0;
                var req = uni.getReq();
                if (uni.isNull(req['prc'])) {
                    showMode = 0;
                }
                $(devs).each(function (i) {
                    var pthis = $(this);
                    var id = pthis.attr('id');
                    var name = pthis.attr('name');
                    var url = pthis.attr('url');
                    var campus = pthis.attr('campus');
                    var cls = pthis.attr('devcls');
                    var col = pthis.attr('dept');
                    var lab = pthis.attr('lab');
                    var price = pthis.attr("price");
                    var manager = pthis.attr("manager");
                    var phone = pthis.attr("phone") || '';
                    var intro = pthis.attr('intro');
                    var devstat = pthis.attr('devstat');
                    var runstat = pthis.attr('runstat');
                    if (showMode == 0) {
                        content += "<div class='m-box'><div class='box_h'><a  url=\"../a/devdetail.aspx?dev=" + id + "\" cache='#cache_con' con='#detail_con' class=\"click_load\" title=\"点击查看详情：" + name + "\"><img alt='" + name + "' src='" + url + "' /></a></div><div class='box_c'>" +
                        "<ul><li class='name'>" + uni.cutStrT(name, 14) + "</li><li>" + uni.cutStrT(campus + "," + lab + "," + cls, 30) + "</li></ul>" +
                        "</div></div>";
                    }
                    else if (showMode == 1) {
                        content += "<tr><td>" + (rlt.startLine + i) + "</td><td style='text-align:left;'>" + uni.cutStrT(name, 12) + "</td><td>" + uni.cutStrT(col, 10) + "</td><td>" + uni.cutStrT(lab, 16) + "</td>" +
                            "<td>" + manager + "</td><td>" + devstat + "</td><td>" + "<a href=\"LabDetail.aspx?dev=" + id + "\" class=\"click\">详情</a></td></tr>";
                    }
                });
                if (showMode == 0) {
                    $("#boxes").html(content);
                    $("#devbox").show();
                    $("#devtbl").hide();
                    $("#boxes .click_load").clickLoad(function () {
                        uni.backTop();
                    });
                }
                else if (showMode == 1) {
                    $("#devlist").html(content)
                    $("#devtbl").show();
                    $("#devbox").hide();
                    $(".zebra").zebra();
                }
                $("#totol").html(rlt.totalLines);
                updatePageCtrl(rlt.totalLines, rlt.startLine, rlt.needLines);
            });
        }
    </script>
    <div id="content" class="float_r panel_devlist">
        <div id="filter" class="panel panel-default">
            <div class="panel-heading"><span class="glyphicon glyphicon-list"></span>&nbsp;过滤条件</div>
            <table class="table table-condensed">
                <tr class="its" key="campus">
                    <td class="text-right">校区：</td>
                    <td class="text-left">
                        <%=CampusList %>
                    </td>
                </tr>
                <tr class="its" key="lab">
                    <td class="text-right" style="width: 15%;">位置：</td>
                    <td class="text-left">
                        <%=LabList %>
                    </td>
                </tr>
            </table>
            <div class="line" style="margin-top:0px;"></div>
            <div class="keys" style="margin: 10px auto;">
                <div class="input-group" style="width: 40%; margin-left: 30%;">
                    <span class="input-group-addon">名称</span>
                    <input id="keyword" class="form-control" type="text" key="name" />
                    <span class="input-group-btn">
                        <button type="button" class="btn btn-info sub_filter">&nbsp;<span class="glyphicon glyphicon-search"></span>&nbsp;</button></span>
                </div>
            </div>

        </div>
        <div id="rlt_con" style="margin-top: 10px;">
            <div id="show_mode" class="hidden"><span>显示方式：</span><span>列表</span><input type="radio" id="showtbl" name="check_mode" value="1" onclick="SubmitRet(RetSelecteds());" /><span>框</span><input type="radio" id="showbox" name="check_mode" checked="checked" value="0" onclick="    SubmitRet(RetSelecteds());" /></div>
            <div class='m-boxes' id="devbox">
                <div class="alter alert-info"><span>&nbsp;查询结果： 总  <span id="totol" style="color: orange"></span>条记录</span></div>
                <div id='boxes' style="min-height: 300px;">
                </div>
            </div>
            <div id="devtbl" style="display: none">
                <div class="devtbl_head">搜索结果 <span class="f-fr" style="margin-top: -2px; margin-right: 2px; color: black;">状态日期:<input id="dev_sta_date" type="text" class="Wdate" readonly="readonly" /></span></div>
                <div class="zebra">
                    <table>
                        <thead class="">
                            <tr class="title">
                                <th>序号</th>
                                <th>实验室名称</th>
                                <th>部门</th>
                                <th>实验室类型</th>
                                <th>管理员</th>
                                <th style="background-color: #67b021; color: #fff;">预约状态</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody id="devlist" class="list-c">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div id="pCtrl"></div>
    </div>
</body>
</html>
