<%@ Page Language="C#" AutoEventWireup="true" CodeFile="center.aspx.cs" Inherits="ClientWeb_xcus_cg2_Default" %>

<%@ Register TagPrefix="Uni" TagName="acc" Src="~/ClientWeb/pro/net/acc.ascx" %>
<%@ Register TagPrefix="Uni" TagName="info" Src="~/ClientWeb/pro/net/userinfo.ascx" %>
<html>
<body>
    <div>
        <Uni:acc runat="server" />
    </div>
    <style>
        .resv_list_tbl { width: 99%; margin-top: 20px; }
        .resv_list_tbl .popover { max-width: 600px; }
        .resv_list_tbl thead th { text-align: center; line-height: 40px; border-width: 0; border-bottom: 2px #31b0d5 solid; border-top: 1px #ddd solid; background-color: #f1f1f1; }
        .resv_list_tbl td { border: 1px solid #ddd; color: #333; }
        .resv_list_tbl .head td { background-color: #fafafa; color: #777; vertical-align: bottom; padding: 5px 5px 2px 5px; }
        .resv_list_tbl .head td span { padding: 0 5px; display: inline-block; }
        .resv_list_tbl .head td h3 { padding: 0 5px; display: inline; }
        .resv_list_tbl .content td { vertical-align: top; padding: 4px 3px; }
        .resv_list_tbl .content td .part { color: #999; }
        .resv_list_tbl .content td .primary { color: #31708f; }
        .resv_list_tbl .content .popover .popover-content { min-height: 160px; }
        .resv_list_tbl .box { min-height: 60px; }

        .resv_list_tbl .detail_info td { line-height: 24px; padding: 0 3px; background: none; border-width: 0; }
        .resv_list_tbl .detail_info td:first-child { color: #428bca; text-align: right; padding-right: 5px; }

        .resv_list_tbl .state_info td { line-height: 24px; padding: 0 3px; background: none; font-size: 12px; text-align: center; }
        .resv_list_tbl .state_info { width: 300px; }
        .resv_list_tbl .state_info th { border-bottom: 2px #31b0d5 solid; text-align: center; line-height: 20px; }
        .resv_list_tbl .state_info td:first-child { color: #428bca; }
        .resv_list_tbl .time_span { border-bottom: 1px dashed #ccc; }
        .resv_list_tbl .state_info tr:nth-child(2n) { background-color: #f7f7f7; }

        .del_resv_tbl tr:nth-child(2n) { background-color: #f7f7f7; }



        #updateacc { width: 520px; margin: 20px auto; padding: 5px; border: 1px solid #ccc; text-align: center; background-color: #fcfcfc; border-radius: 4px; }
        #updateacc table { width: 100%; background-color: #fff; margin-bottom: 10px; }
        #updateacc table td { border: solid 1px #ddd; padding-left: 5px; text-align: left; height: 36px; line-height: 36px; }
        #updateacc table td:first-child { padding-right: 5px; text-align: right; }
        #updateacc table td.act_td { vertical-align: middle; text-align: center; background: #fafafa; padding: 5px; }
        /*#feedback_list .detail span{display:inline-block;margin-right:5px;}*/
    </style>
    <script>
        var acc = pro.acc;
        if (acc.credit) {
            var rec = "<ul>";
            for (var i = 0; i < acc.credit.length; i++) {
                var it = acc.credit[i];
                var forbid = it[3] ? (" <span class='red'>禁用:" + it[3] + "</span>") : "";
                rec += "<li>" + it[0] + "：剩余" + it[1] + "分/总" + it[2] + "分  " + forbid + "</li>"
            }
            rec += "</ul>";
            $("#credit_score").html(rec);
        }

        var svc_arr = [<%=svcArray%>];
        function delGResv(id, name, rsv) {//删申请
            uni.confirm("确定要撤销活动【" + name + "】？", function () {
                pro.j.rsv.delResv(id, function () {
                    uni.msgBoxR("撤销成功");
                }, { resv_type: "group" });
            });
        }
        function feedback(resvId, devId, title) {
            var $dlg = $("#dlg_rsv_feedback");
            $(".title", $dlg).html("活动：" + title + " 场馆使用评价");
            uni.dlgInit($dlg);
            uni.dlg($dlg, "使用评价", 560, 380, function (dlg) {
                var kind = 1;
                var score = $(".score", dlg).val();
                var con = $(".con", dlg).val();
                pro.j.rsv.setFeedback(resvId, devId, con, kind, score, function () {
                    uni.msgBoxR("评价成功");
                    $dlg.dialog("close");
                });
            });
        }
        //function clock() {
        //    var dt = new Date();
        //    $(".cur_time").html(dt.format("HH:mm"));
        //    $(".cur_date").html(dt.format("yyyy年MM月dd日 星期E"));
        //}
        $(function () {
            $(".unitab").unitab();
            //clock();
            //setInterval("clock()", 60000);
            //审核状态
            $(".check_state").popover({
                html: true,
                placement: "left",
                title: "审核状态",
                content: function () {
                    var panel = $("#state_panel");
                    var resvId = $(this).attr("resv_id");
                    pro.j.rsv.getYardRsvCheck(resvId, function (rlt) {
                        var list = rlt.data;
                        var str = "";
                        for (var i = 0; i < list.length; i++) {
                            str += "<tr><td>" + list[i].title;
                            str += "</td><td>" +(uni.isInArray(list[i].kind,svc_arr)?"":list[i].state);
                            str += "</td><td>" + (list[i].date || "");
                            str += "</td><td>" + list[i].memo;
                            str += "</td></tr>";
                        }
                        $(".state_info_panel:visible tbody").html(str);
                    });
                    return panel.html();
                },
                trigger: "hover"
            });
            //时间明细
            $(".time_detail").popover({
                html: true,
                placement: "left",
                title: "时间明细",
                content: function () {
                    var panel = $("<div>" + $("#timedetail_" + $(this).attr("resv_id")).html() + "</div>");
                    var pthis = $(this);
                    if (pthis.attr("no_check").toLowerCase() != "true") panel.find(".resv_act").html("");

                    //关闭
                    var close = $("<span class='close' style='margin-top: -40px;'>x</span>").click(function () { pthis.popover("hide"); pthis.next(".popover").remove(); });
                    panel.prepend(close);
                    //排序
                    panel.tblsort();
                    return panel;
                },
                trigger: "click"
            });
            //申请详情
            $(".activity_detail").popover({
                html: true,
                placement: "left",
                content: function () {
                    var panel = $("#detail_" + $(this).attr("resv_id"));
                    return panel.html();
                },
                trigger: "hover"
            });
            //过滤预约
            filterResv("true");
            $("#sel_date").change(function () {
                var v = parseInt($(this).val());
                filterResv(v == 0 ? "true" : "false");
            });
            //删除明细分页排序
            var act = $(".del_resv_tbl").pctrl($(".del_resv_pctrl"), 20);
            $(".del_resv_tbl").tblsort(function () {
                act.reset();
            });


            //第二课堂
            var act = $(".second_list_tbl").pctrl($(".second_pctrl"), 20);
            //预约操作
            $(".second_act").click(function () {
                var third_id = $(this).attr("third_id");
                var resv_id = $(this).attr("resv_id");
                if (resv_id) {
                    uni.confirm("删除旧预约并重新预约场地？", function () {
                        pro.j.rsv.delResv(resv_id, function () { goResv(); }, null, function (rlt) {
                            if (rlt.msg.indexOf("不存在") > 0) {
                                goResv();
                            }
                            else {
                                uni.msgBox(rlt.msg);
                            }
                        });
                    });
                }
                else {
                    goResv();
                }
                function goResv() {
                    var dlg = $("#dlg_rsv_third");
                    var idlg = $(dlg.html());
                    uni.dlg(idlg, "选择活动场景", 400, 230);
                    $(".btn_next", idlg).click(function () {
                        idlg.dialog("close");
                        var id = $(".sel_aty", idlg).val();
                        uni.hr.loadCache("apply.aspx?back=true&thirdId=" + third_id + "&activityId=" + id, null, $("#cache_con"));
                        uni.backTop();
                    });
                }
            });
        })
        function filterResv(show) {
            var list = $(".yard_resv_tbl tbody");
            var num = 0;
            list.each(function () {
                var pthis = $(this);
                var over = pthis.attr("over");
                if (over == show) pthis.hide();
                else { pthis.show(); num++; }
            });
            if (num == 0) uni.msgBox("没有数据");
        }
        //function loadApplyDetail(id) {
        //    uni.hr.loadCache("applydetail.aspx?back=true&resv_id=" + id, null, $("#cache_con"));
        //}
        function copyApply(id, aty, url) {
            uni.hr.loadCache(url + "?back=true&activityId=" + aty + "&resv_id=" + id, null, $("#cache_con"));
        }
        //预约删除
        function delRsv(rsv) {
            var id = $(rsv).attr("rsvId");
            uni.confirm("确定要删除吗？", function () {
                pro.j.rsv.delResv(id, function () {
                    uni.msgBoxR("删除成功");
                    //$(rsv).parents("tr:first").remove();
                    //$("#timedetail_panel .tdetail_tr_" + id).remove();
                });
            });
        }
        //预约修改
        function alterRsv(rsv) {
            var rsv = $(rsv);
            var kind = rsv.attr("devKind");
            var devId = rsv.attr("devId");
            var rsvId = rsv.attr("rsvId");
            var start = rsv.attr("start");
            var end = rsv.attr("end");
            pro.d.resv.alterTime(devId, kind, rsvId, start, end);
        }
    </script>
    <div>
        <div class="dialog" id="dlg_rsv_feedback">
            <div class="title"></div>
            <div class="list">
                <table>
                    <tr>
                        <td>使用评分：</td>
                        <td>
                            <select class="score form-control" name="score">
                                <option value="5">★★★★★</option>
                                <option value="4">★★★★</option>
                                <option value="3">★★★</option>
                                <option value="2">★★</option>
                                <option value="1">★</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>评价内容：</td>
                        <td>
                            <textarea rows="6" cols="38" name="con" style="width: 420px;" class="con form-control"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div>
        <h1>个人中心&nbsp;&nbsp;<span class="grey">PERSONAL HOMEPAGE</span></h1>
        <div class="" style="min-height: 400px;">

            <div class="info_unitab">
                <ul class="tab_head">
                    <li>
                        <div class="title">申请列表</div>
                        <div class="caret"></div>
                    </li>
                    <li>
                        <div class="title">第二课堂</div>
                        <div class="caret"></div>
                    </li>
                    <li>
                        <div class="title del_detail_bt">撤销明细</div>
                        <div class="caret"></div>
                    </li>
                    <li>
                        <div class="title">活动评价</div>
                        <div class="caret"></div>
                    </li>
                    <li>
                        <div class="title">个人信息</div>
                        <div class="caret"></div>
                    </li>
                </ul>
                <div class="tab_con" style="min-height: 300px;">
                    <div class="item">
                        <table class="yard_resv_tbl resv_list_tbl">
                            <thead>
                                <tr>
                                    <th style="width: 18%;">场地</th>
                                    <th style="width: 15%;">归口部门</th>
                                    <th style="width: 15%;">物管部门</th>
                                    <th style="width: 20%;">
                                        <select id="sel_date" class="form-control">
                                            <option value="0">新申请记录</option>
                                            <option value="3">三月内结束记录</option>
                                        </select>
                                    </th>
                                    <th style="width: 20%;"></th>
                                    <th style="width: 12%;"></th>
                                </tr>
                            </thead>
                            <%=resvList %>
                        </table>
                    </div>
                    <div class="item">
                            <div id="dlg_rsv_third"> 
           <div class="dialog">
        <div class="title"></div>
        <div class="list">
            <table>
                <tr>
                    <td>
                        <select class="form-control sel_aty">
                            <%=atyList %>
                        </select>
                    </td>
                    <td>
                        <button type="button" class="btn btn-info btn_next">下一步</button></td>
                </tr>
            </table>
        </div>
    </div>
</div>
    <div>
        <div>
            <table class="second_list_tbl resv_list_tbl">
                <thead>
                    <tr>
                        <th>活动编号</th>
                        <th>活动名称</th>
                        <th>组织方</th>
                        <th>组织人</th>
                        <th>活动时间</th>
                        <th>状态</th>
                        <th class="no_sort">预约场地</th>
                    </tr>
                </thead>
                <tbody>
                    <%=secondList %>
                </tbody>
            </table>
            <div class="second_pctrl"></div>
        </div>

    </div>
                    </div>
                    <div class="item">
                        <table class="resv_list_tbl del_resv_tbl">
                            <thead>
                                <tr>
                                    <th>所属单号</th>
                                    <th>活动名称</th>
                                    <th>场地</th>
                                    <th>申请时间</th>
                                    <th>预约时间</th>
                                    <th class="no_sort">撤销原因</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=delResvList %>
                            </tbody>
                        </table>
                        <div class="del_resv_pctrl"></div>
                    </div>
                    <%--                    <div class="item">
                        <div class="text-right"><a onclick="uni.reload(function(){$('.del_detail_bt').trigger('click')});">刷新</a></div>
                        <table class="yard_resv_tbl del_resv_tbl">
                            <thead>
                                <tr> 
                                    <td style="width: 12%;">预约号</td>
                                    <td style="width: 18%;">活动名</td>
                                    <td style="width: 15%;">场地</td>
                                    <td style="width: 15%;">物管部门</td>
                                    <td style="width: 22%;">预约时间(一年内)</td>
                                    <td style="width: 18%;">申请时间</td>
                                </tr>
                            </thead>
                            <%=delList %>
                        </table>
                        <div class="del_resv_pctrl"></div>
                    </div>--%>
                    <div class="item">
                        <div class="tab_pctrl">
                            <div style="margin-left: 20px;">总：<span class="pc_total red"></span>&nbsp;条记录，分&nbsp;<span class="pc_ptotal red"></span>&nbsp;页，当前第&nbsp;<span class="pc_here red"></span>&nbsp;页</div>
                            <div id="feedback_list" class="feedback_list">
                                <table class="tab_con" style="width: 100%;">
                                    <tbody>
                                        <%=feedback %>
                                    </tbody>
                                </table>
                            </div>
                            <ul class="tab_head"></ul>
                        </div>
                        <script>
                            $(".tab_pctrl").unitab(null, {
                                pctrl: 10, pctrlFun: function (index, need, total, obj) {
                                    $(".pc_total", obj).html(total);
                                    $(".pc_here", obj).html(index + 1);
                                    $(".pc_ptotal", obj).html(obj.ptotal);
                                    uni.backTop();
                                },
                                custom: true
                            });
                        </script>
                    </div>
                    <div class="item">
                        <%--                        <Uni:info runat="server" />
                        <script>
                            $(".tr_credit").hide();
                        </script>--%>
                        <div id="updateacc">
                            <div style="font-size: 18px; font-weight: 700; margin: 5px; text-align: left; color: #ddd;">User Profile</div>
                            <form onsubmit="return false;">
                                <table class="acc_tbl">
                                    <tbody>
                                        <tr>
                                            <td style="width: 140px;"><span style="font-weight: 700;"><span class="uni_trans">姓名</span>：</span></td>
                                            <td><span id="accName"><%=acc.szTrueName %></span></td>
                                        </tr>
                                        <tr>
                                            <td><span style="font-weight: 700"><span class="uni_trans">帐号</span>：</span></td>
                                            <td><span id="accLgName"><%=acc.szLogonName %></span></td>
                                        </tr>
                                        <tr>
                                            <td><span style="font-weight: 700"><span class="uni_trans">部门</span>：</span></td>
                                            <td><span id="accColl"><%=acc.szDeptName %></span></td>
                                        </tr>
                                        <%if (GetConfig("userCredit")=="1"){ %>
                            <tr class="tr_credit">
                                <td><span style="font-weight: 700"><span class="uni_trans">信用积分</span>：</span></td>
                                <td>
                                    <span id="credit_score"></span>
                                </td>
                            </tr>
                                <%} %>
                                        <tr>
                                            <td><span style="font-weight: 700"><span class="uni_trans">手机</span>：</span></td>
                                            <td>
                                                <span id="accPhone"><%=acc.szHandPhone %></span></td>
                                        </tr>
                                        <tr>
                                            <td><span style="font-weight: 700"><span class="uni_trans">邮箱</span>：</span></td>
                                            <td>
                                                <span id="accEmail"><%=acc.szEmail %></span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </form>
                        </div>
                    </div>
                </div>

            </div>
            <script>
                $(".info_unitab").unitab();
            </script>
        </div>
    </div>
    <div id="detail_list" class="hidden">
        <%=detailList %>
    </div>
    <div id="state_panel" class="hidden">
        <table class="state_info state_info_panel">
            <thead>
                <tr>
                    <th>审核项目</th>
                    <th>审核状态</th>
                    <th>审核时间</th>
                    <th>说明</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td colspan="4">请稍等...</td>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="timedetail_panel" class="hidden">
        <%=timeDetail %>
    </div>
</body>
</html>
