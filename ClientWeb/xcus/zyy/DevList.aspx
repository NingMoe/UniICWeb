<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="DevList.aspx.cs" Inherits="_Default" %>

<%@ Register TagPrefix="Uni" TagName="leftMenu" Src="net/LeftMenu.ascx" %>
<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        //条件对象
        var result = {};
        $(function () {
            //初始化分页控件
            $("#pCtrl").pageCtrl(function (pageCtrlID, startLine, needLine) {
                SubmitRet(RetSelecteds('pctrl'));
            }, 15);
            //获取搜索关键字
            var Request = uni.getReq();
            var key = Request["key"];
            if (key != undefined && key != '') {
                $("#keyword").val(decodeURI(key));
            }
            var cps = Request["cps"];
            if (cps != undefined && cps != '') {
                $("#filter_campus").val(cps);
            }
            var cls = Request["cls"];
            if (!uni.isNull(cls)) {
                $("#filter_cls").val(cls);
            }
            var prc = Request["prc"];
            if (!uni.isNull(prc)) {
                result.prc = prc;
            }
            //状态日期
            var today = uni.formatDate("yyyy-MM-dd");
            $("#dev_sta_date").val(today).datetimepicker({
                timeFormat: "",
                dateFormat: "yy-mm-dd",
                onClose: function () {
                    SubmitRet(RetSelecteds());
                }
            });
            //校区联动
            function camDep() {
                var cam = parseInt($("#filter_campus").val());
                if (cam == 0) {
                    $("#filter_lab option").each(function () {
                        $(this).removeClass("hidden");
                    });
                }
                else {
                    $("#filter_lab").val(0);
                    $("#filter_lab option").each(function () {
                        var d = parseInt($(this).attr("dep"));
                        if (isNaN(d) || d == cam) {
                            $(this).removeClass("hidden");
                        }
                        else {
                            $(this).addClass("hidden");
                        }
                    });
                }
            };
            //$("#filter_campus").change(camDep);
            //camDep();
            SubmitRet(RetSelecteds()); //首次返回结果
        });

        function RetSelecteds(act) {
            //仪器名关键字 
            result.name = $("#keyword").val();
            //管理员关键字
            result.manager = $("#man_key").val();
            //校区
            result.campus = $("#filter_campus").val();
            //学院
            result.dept = $("#filter_college").val();
            //实验室
            result.lab = $("#filter_lab").val();
            //类别
            result.devcls = $("#filter_cls").val();
            //状态日期
            result.statdate = $("#dev_sta_date").val();
            //分页
            result.pctrlId = "pCtrl";
            if (act == 'pctrl') {
                result.pctrlStar = $("input[name='pCtrl_start']").attr('value');
            }
            else {
                result.pctrlStar = "1";
            }
            result.pctrlNeed = $("input[name='pCtrl_need']").attr('value');
            //显示模式
            $("#show_mode input").each(function () {
                if ($(this).attr("checked")) {
                    result.showMode = $(this).val();
                }
            });
            return result;
        }
        function SubmitRet(condition) {
            pro.j.dev.devFilter(condition, function (con) {
                var content = '';
                rlt = con.data;
                var devs = rlt.devs;
                //显示模式
                var showMode = 0;
                var req = uni.getReq();
                if (uni.isNull(req['prc'])) {
                    showMode = 1;
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
                        content += "<div class='m-box'><div class='box_h'><a href=\"DevDetail.aspx?dev=" + id + "\" title=\"管理员:" + manager + " 联系方式:" + phone + "\"><img alt='" + name + "' src='" + url + "' /></a></div><div class='box_c'>" +
                        "<ul><li class='name'>" + uni.cutStrT(name, 14) + "</li><li>校区：" + uni.cutStrT(campus, 10) + "</li><li>实验室：" + uni.cutStrT(lab, 10) + "</li><li>设备单价：<span>" + price + "元</span></li><li style='text-align:right;'>" +
                        "<a href=\"DevDetail.aspx?dev=" + id + "\" class=\"\">详细</a>|<a href=\"DevDetail.aspx?tab=1&dev=" + id + "\" onclick=\"return pro.isloginRT()\"> 预约>> </a>" + "</li></ul>"
                    + "</div></div>";
                    }
                    else if (showMode == 1) {
                        content += "<tr><td>" + (rlt.startLine + i) + "</td><td style='text-align:left;'>" + uni.cutStrT(name, 12) + "</td><td>" + uni.cutStrT(col, 10) + "</td><td>" + uni.cutStrT(campus + "|" + lab, 16) + "</td>" +
                            "<td>" + manager + "</td><td>" + devstat + "</td><td>" + "<a href=\"DevDetail.aspx?tab=1&dev=" + id + "\" class=\"\" onclick=\"return pro.isloginRT()\">预约</a>" +
        "|<a href=\"DevDetail.aspx?dev=" + id + "\" class=\"\">详细</a></td></tr>";
                    }
                });
                if (showMode == 0) {
                    $("#boxes").html(content);
                    $("#devbox").show();
                    $("#devtbl").hide();
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
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <Uni:leftMenu ID="leftMenu" runat="server" />
        <div id="content" class="float_r panel_devlist">
            <div id="position">当前位置：<a href="Default.aspx">首页</a> > 仪器设备</div>
            <div id="filter" class="grey_box">
                <div class="h_grey">设备搜索</div>
                <div class="c_grey">
                    <table>
                        <tbody>
                            <tr>
                                <td class="title">校<span style="width: 2em; display: inline-block;"></span>区</td>
                                <td>
                                    <select id="filter_campus">
                                        <option value="0">全部</option>
                                        <%=CampusList %>
                                    </select>
                                </td>
                                <td class="title">部<span style="width: 2em; display: inline-block;"></span>门</td>
                                <td>
                                    <select id="filter_college">
                                        <option value="0">全部</option>
                                        <%=ColList %>
                                    </select>
                                </td>
                                <td class="title">实<span style="width: 0.5em; display: inline-block;"></span>验<span style="width: 0.5em; display: inline-block;"></span>室</td>
                                <td>
                                    <select id="filter_lab">
                                        <option value="0">全部</option>
                                        <%=LabList %>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td class="title">仪器类型</td>
                                <td>
                                    <select id="filter_cls">
                                        <option value="0">全部</option>
                                        <%=ClsList %>
                                    </select>
                                </td>
                                <td class="title">管<span style="width: 0.5em; display: inline-block;"></span>理<span style="width: 0.5em; display: inline-block;"></span>员</td>
                                <td>
                                    <input id="man_key" type="text" />
                                </td>
                                <td class="title">设备名称</td>
                                <td>
                                    <input id="keyword" type="text" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="text-align: center;">
                                    <input type="button" class="button" value="搜索" style="height: 30px; width: 80px; cursor: pointer;" onclick="SubmitRet(RetSelecteds())" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="rlt_con">
                <div id="show_mode" class="hidden"><span>显示方式：</span><span>列表</span><input type="radio" id="showtbl" name="check_mode" value="1" onclick="SubmitRet(RetSelecteds());" /><span>框</span><input type="radio" id="showbox" name="check_mode" checked="checked" value="0" onclick="    SubmitRet(RetSelecteds());" /></div>
                <div class='m-boxes' id="devbox" style="display: none">
                    <div class="devtbl_head"><span style="color: #333;">搜索结果： 总  <span id="totol" style="color: #007dc1"></span>条记录</span></div>
                    <div id='boxes'>
                    </div>
                </div>
                <div id="devtbl" style="display: none">
                    <div class="devtbl_head">搜索结果 <span class="f-fr" style="margin-top:-2px;margin-right:2px;color:black;">状态日期:<input id="dev_sta_date" type="text" class="Wdate" readonly="readonly"/></span></div>
                    <div class="zebra">
                        <table>
                            <thead class="">
                                <tr class="title">
                                    <th>序号</th>
                                    <th>仪器名称</th>
                                    <th>部门</th>
                                    <th>放置位置</th>
                                    <th>管理员</th>
                                    <th style="background-color:#006699;color:#fff;">预约状态</th>
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
        <div class="cleaner"></div>
    </div>
    <!-- END of templatemo_main -->

</asp:Content>


