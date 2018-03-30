<%@ Page Language="C#" AutoEventWireup="true" CodeFile="atydetail.aspx.cs" Inherits="ClientWeb_xcus_all_atydetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <style>
        #filter td { vertical-align: middle; }
        #filter select { height: 32px; width: 160px; }
        #filter input { height: 32px; width: 160px; }
        .cld-top-c { display:none;}
    </style>
    <script>
        var getFilter;
        $(function () {
            //unitab
            $(".info_unitab").unitab();
            //初始化筛选
            getFilter = $("#filter").filterItem(RetSelecteds);
            $("#filter .date_sel").datepicker({
                minDate: 0
            });
            $("#filter .time_sel").timepicker({
                controlType: 'select',
                timeFormat: "HH:mm",
                stepHour: 1,
                stepMinute: 5,
                hourMin: 6,
                hourMax: 23
            });
            //时间联动
            var tm_start = $("#filter .time_start");
            var tm_end = $("#filter .time_end");
            tm_start.change(function () {
                var sta = tm_start.val();
                var en = tm_end.val();
                if (sta) {
                    if (en) {
                        var i_sta = parseInt(sta.replace(":", ""));
                        var i_en = parseInt(en.replace(":", ""));
                        if (i_en < i_sta) tm_end.val(sta);
                    }
                    else
                        tm_end.val(sta);
                }
            });
            //隐藏空白类型 custom
            $("#filter .sel_devcls option").each(function () {
                var pthis = $(this);
                if (pthis.html() == "空白") {
                    pthis.hide();
                    return false;
                }
            });
            //隐藏无效的校区
            var builds = $("#filter .sel_building option");
            $("#filter .sel_campus option").each(function () {
                var v = this.value;
                if (v == "0") return true;
                var flag = true;
                builds.each(function () {
                    if ($(this).attr("depend") == v) {
                        flag = false;
                        return false;
                    }
                });
                if (flag) $(this).hide();
            });
            //假如位置只有一个，默认选中
            if (builds.length == 2) {
                builds[1].selected = true;
            }
            //批量预约
            $(".info_unitab .resv_multi").click(function () {
                SelDateTime(null);
            });
            //重置
            $(".clear_filter").click(function () {
                uni.hr.para.$cache = null;
                uni.hr.reload();
            });
            //自动查询
            if ("<%=Request["rsch"]%>" == "auto") {
                $("#filter .sub_filter").trigger("click");
            }
        });
        function RetSelecteds(fl) {
            var flg = false;
            if ("<%=Request["rsch"]%>" == "auto") flg = true;
            $("#filter .must_sel").each(function () {
                if ($(this).val() != "0" && $(this).val() != "") flg = true;
            });
            if (!flg) {
                uni.msgBox("校区/位置至少选一个");
                return;
            }
            if (!$("#filter").mustItem()) return;
            if (!fl) fl = {};
            updateFilter(fl);
            fl.req_prop = 4;//获取第三方共享设备空闲状态
            loadCld(fl);
        }
        function updateFilter(filter) {
            filter.kind_id = $(".kind_id").val();
            filter.islong = $(".islong").val();
            filter.activity_id = $(".activity_id").val();
            filter.activity_name = $(".activity_name").val();
        }
        function SelDateTime(data) {
            uni.hr.loadCache("<%=applyTbl%>.aspx?back=true&kinds=<%=kinds%>&activityId=<%=activityId.Value%>", null, $("#cache_con"), data, function () {
                $(".info_unitab .resv_dft").trigger("click");
            });
            uni.backTop();
            //showApplyTbl(data);
        }
        function loadCld(para) {
            if (!para) return;
            $("#aty_detail").uniCalendar({
                modes: "d",
                width: 900,
                operate: "drag",
                beginDay: para.beginDay,
                objTitleMinWidth: 160,
                evSelTime: SelDateTime,
                pctrl: { num: 20 },
                dayOpt: {
                    //occupy: false,
                    evSelObj: function (data) {
                        var devId = data.obj.devId;
                        if (uni.isNoNull(devId)) {
                            uni.hr.loadCache("devdetail.aspx?back=true&dev=" + devId, null, $("#cache_con"));
                            uni.backTop();
                        }
                    }
                },
                evUpTime: function (date, callback) {
                    para.date = date.format("yyyyMMdd");
                    //para.need_dept = "false";//获取设备所在部门 耗资源
                    pro.j.dev.getRsvSta(para, function (rlt) {
                        $(".resv_state_info").show();
                        var list = rlt.data;
                        var arr = [];
                        //过滤
                        for (var i = 0; i < list.length; i++) {
                            if (list[i].freeSta && list[i].freeSta != 0)
                                continue;
                            if (list[i].ts) {
                                var ts = list[i].ts;
                                for (var j = 0; j < ts.length; j++) {
                                    ts[j].title && (ts[j].title = ts[j].title.replace(/(@#)/g, ","));
                                }
                            }
                            arr.push(list[i]);
                        }
                        callback(arr, "unilab3", {
                            showClose: ("<%=GetConfig("showClose")%>" == "1" ? true : false), detailFun: function (obj) {
                                var str = "容量:" + obj.maxUser + "人" + "；位置:" + obj.campus + "," + obj.buildingName;
                                return str;
                            }
                        });
                    });
                },
                evUpPlans: function (obj, start, end, callback, opt) {
                    var md = opt.mode;
                    start = start.format("yyyyMMdd");
                    end = end.format("yyyyMMdd");
                    if (md == "m" && uni.isNoNull(obj)) {
                        var devId = obj.id.split('&')[0];
                        pro.j.rsv.getYardRsvList(devId, start, end, function (rlt) {
                            var list = rlt.data;
                            if (list)
                                $(list).attr("title", "已预约");
                            callback(list);
                            $("[title]").tooltip({
                                content: function () {
                                    var e = $(this);
                                    if (e.hasClass("cld-m-cell")) {
                                        var con = e.attr("title").replace(/\|/g, "<br/>");
                                        return con;
                                    }
                                    else {
                                        return e.attr("title");
                                    }
                                }
                            });
                        });
                    }
                    else {
                        callback();
                    }
                }
            });
            }
    </script>
    <form id="Form1" runat="server">
        <input runat="server" type="hidden" class="kind_id" name="kind_id" id="kindId" />
        <input runat="server" type="hidden" class="islong" name="islong" id="isLong" value="false" />
        <input runat="server" type="hidden" class="activity_id" name="activity_id" id="activityId" />
        <input runat="server" type="hidden" class="activity_name" name="activity_name" id="activityName" />
    </form>
    <div>
        <style>
            .cld_style_detail .cld-obj-title { cursor: pointer; }
            .dialog .drop-select { display: inline-block; min-width: 150px; }
            .dlg_dt_panel tr td:nth-child(2) { width: 260px; background: #FDF9E7; vertical-align: top; }
            .color_span { height: 20px; width: 50px; display: inline-block; opacity: .6; }
            /*.cld-plan-bar.cld-occupy { background-color: #42B32C; border: 1px solid #129C12; }*/
        </style>
        <div>
            <h1 class="h_title"><%=infoTitle %></h1>
            <div class="line"></div>
            <div style="margin: 10px; margin-bottom: 20px; overflow: hidden;">
                <%=infoIntro %>
            </div>
        </div>
<%--        <code>单日预约请先查询场地，多日或多场所预约请直接点击【批量预约】标签！</code>--%>
        <div class="info_unitab">
            <ul class="tab_head">
                <li class="resv_dft">
                    <div class="title">场所预约</div>
                    <div class="caret"></div>
                </li>
                <li class="resv_multi" style="display: <%=multiResv%>">
                    <div class="title">批量预约</div>
                    <div class="caret"></div>
                </li>
                <li>
                    <div class="title">借用流程</div>
                    <div class="caret"></div>
                </li>
                <li class="hidden">
                    <div class="title">相册展示</div>
                    <div class="caret"></div>
                </li>
            </ul>
            <div class="tab_con">
                <div>
                    <div id="filter" class="panel panel-default">
                        <div class="panel-heading"><span class="glyphicon glyphicon-list"></span>&nbsp;场所预约状态查询</div>
                        <table class="table table-condensed">
                            <tr class="its">
                                <td class="text-right" style="width: 6%;">校区：</td>
                                <td class="text-left">
                                    <select class="form-control must_sel sel_campus" key="campus" affect="building_id">
                                        <option value="0">未选择</option>
                                        <%=CampusList %>
                                    </select>
                                </td>
                                <td class="text-right" style="width: 6%;">位置：</td>
                                <td class="text-left">
                                    <select class="form-control must_sel sel_building" key="building_id">
                                        <option value="0">未选择</option>
                                        <%=BuildingList %>
                                    </select>
                                </td>
                                <td class="text-right" style="width: 6%; display: <%=hideDevCls?"none":""%>">类型：</td>
                                <td class="text-left" style="display: <%=hideDevCls?"none":""%>">
                                    <select class="form-control sel_devcls" key="class_id">
                                        <option value="0">全部</option>
                                        <%=DevClsList %>
                                    </select>
                                </td>
                                <td class="text-right" style="width: 6%;">名称：</td>
                                <td class="text-left">
                                    <input id="keyword" class="form-control must_sel" type="text" key="dev_name" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">场地容量：<input class="form-control must allow_null" data-reg="number" data-ckmsg="只能填入数字" type="text" key="user_num" style="width: 40px;" />
                                </td>
                                <td colspan="4">空闲时间：<input type="text" class="date_sel" style="width: 120px;" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" key="beginDay" readonly="readonly" />
                                    &nbsp;<input type="text" class="time_sel time_start" style="width: 60px;" key="fr_start" readonly="readonly" />
                                    -
                                    <input type="text" class="time_sel time_end" style="width: 60px;" key="fr_end" readonly="readonly" />
                                </td>
                                <td colspan="2" class="text-right">
                                    <input type="hidden" value="<%=activityId.Value%>" key="ext_id" />
                                    &nbsp;
                                    <button type="button" class="btn btn-info sub_filter">&nbsp;查询&nbsp;</button>&nbsp;
                &nbsp;
                                    <button type="button" class="btn btn-default clear_filter">&nbsp;重置&nbsp;</button>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div onselectstart="return false;">
                        <div class="resv_state_info" style="display: none;">
                            预约请点击或拖拽时间区域！&nbsp;&nbsp;&nbsp;
                            <%--<span class="color_span cld-plan-bar"></span>有预约(他人可预约)&nbsp;&nbsp;&nbsp;--%>
                            <span class="color_span cld-plan-bar cld-occupy"></span> 已占用&nbsp;&nbsp;&nbsp;
                            <span class="color_span" style="background-color: #ddd;"></span> 已过期/不开放
                        </div>
                        <div id="aty_detail" class="calendar cld_style_detail"></div>
                    </div>
                </div>
                <div></div>
                <div>
                    <%=infoRule %>
                </div>
                <div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
