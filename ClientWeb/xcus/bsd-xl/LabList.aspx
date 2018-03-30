<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="net/Master.master" CodeFile="LabList.aspx.cs" Inherits="ClientWeb_xcus_bsd_xl_LabList" %>

<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        //条件对象
        var result = {};
        $(function () {
            //初始化分页控件
            $("#pCtrl").pageCtrl(function (pageCtrlID, startLine, needLine) {
                SubmitRet(RetSelecteds('pctrl'));
            }, 16);
            //获取搜索关键字
            var Request = uni.getReq();
            var key = Request["key"];
            if (key != undefined && key != '') {
                $("#keyword").val(decodeURI(key));
            }
            var cls = Request["cls"];
            if (!uni.isNull(cls)) {
                $("#filter_cls").val(cls);
            }
            var clskind = Request["clsKind"];
            if (!uni.isNull(clskind)) {
                $("#filter_cls_kind").val(clskind);
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
            SubmitRet(RetSelecteds()); //首次返回结果
        });

        function RetSelecteds(act) {
            //实验室关键字 
            result.name = $("#keyword").val();
            //类别类型
            result.clskind = $("#filter_cls_kind").val();
            //类别
            result.devcls = $("#filter_cls").val();
            //管理员关键字
            //result.manager = $("#man_key").val();
            //实验室类别
            result.lab = $("#filter_lab").val();
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
                        content += "<div class='m-box'><div class='box_h'><a href=\"LabDetail.aspx?dev=" + id + "\" title=\"点击查看详情：" + name + "\"><img alt='" + name + "' src='" + url + "' /></a></div><div class='box_c'>" +
                        "<ul><li class='name'>" + uni.cutStrT(name, 14) + "</li><li>类<span class='em'/>型：" + uni.cutStrT(lab, 10) + "</li><li style='text-align:right;'>" +
                        "<a href=\"LabDetail.aspx?dev=" + id + "\" class=\"\">点击详情</a>" + "</li></ul>"
                    + "</div></div>";
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
        <div id="content" class="float_r panel_devlist">
            <div id="filter" class="grey_box ui-tabs ui-widget ui-corner-all">
                <ul class="h_grey ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
                    <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a class="ui-tabs-anchor">查询条件</a></li>
                </ul>
                <div class="c_grey">
                    <input type="hidden" id="filter_cls_kind" name="clskind" />
                    <table>
                        <tbody>
                            <tr>
                                <td class="title"><%=ClsTitle %>类型</td>
                                <td>
                                    <select style="<%=HideLab%>" id="filter_lab" onchange="SubmitRet(RetSelecteds())">
                                        <option value="0">全部</option>
                                        <%=LabList %>
                                    </select>
                                      <select style="<%=HideCls%>" id="filter_cls" onchange="SubmitRet(RetSelecteds())">
                                        <option value="0">全部</option>
                                        <%=ClsList %>
                                    </select>
                                </td>
                                <td class="title"><%=ClsTitle %>名称</td>
                                <td><input id="keyword" type="text" /></td>
                                                                <td colspan="6" style="text-align: center;">
                                    <input type="button" class="button" value="搜索" style="height: 30px; width: 80px; cursor: pointer;" onclick="SubmitRet(RetSelecteds())" />
                                </td>
                            </tr>
<%--                            <tr>
                                <td class="title"></td>
                                <td>
                                    
                                </td>
                                <td class="title">管<span style="width: 0.5em; display: inline-block;"></span>理<span style="width: 0.5em; display: inline-block;"></span>员</td>
                                <td>
                                    <input id="man_key" type="text" />
                                </td>
                            </tr>--%>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="rlt_con" style="margin-top:10px;">
                <div id="show_mode" class="hidden"><span>显示方式：</span><span>列表</span><input type="radio" id="showtbl" name="check_mode" value="1" onclick="SubmitRet(RetSelecteds());" /><span>框</span><input type="radio" id="showbox" name="check_mode" checked="checked" value="0" onclick="    SubmitRet(RetSelecteds());" /></div>
                <div class='m-boxes ui-tabs ui-widget ui-corner-all' id="devbox">
                                    <ul class="h_grey ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
                    <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a class="ui-tabs-anchor"><span>查询结果： 总  <span id="totol" style="color:orange"></span> 条记录</span></a></li>
                </ul>
                    <div id='boxes'>
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
        <div class="cleaner"></div>
    </div>
    <!-- END of templatemo_main -->

</asp:Content>



