<%@ Page Language="C#" MasterPageFile="Modules/Master.master" AutoEventWireup="true" CodeFile="DevDetail.aspx.cs" Inherits="DevDetail" %>

<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <link href='Scripts/UniTag/TagStyleH.css' rel='stylesheet' />
    <script src='<%=ResolveClientUrl("~/ClientWeb/fm/") %>uni.lib.js' type="text/javascript"></script>
    <link href='<%=ResolveClientUrl("~/ClientWeb/fm/") %>uni.css' rel='stylesheet' />
    <script src='<%=ResolveClientUrl("~/ClientWeb/pro/") %>pro.lib.js' type="text/javascript"></script>
    <link href='<%=ResolveClientUrl("~/ClientWeb/md/") %>fullcalendar/fullcalendar.css' rel='stylesheet' />
    <script src='<%=ResolveClientUrl("~/ClientWeb/md/") %>fullcalendar/fullcalendar.js' type="text/javascript"></script>

    <script src="Scripts/media/jquery.media.js" type="text/javascript"></script>
    <script type="text/javascript">
        function applyDevUseRole(rule, apply) {
            pro.j.acc.applyUseRole(rule, apply, null, function (rlt) {
                uni.msgBoxR("已提交申请，请等待审核");
            });
        }
        function rtSelCheck() {
            var rtsel = $("#rtSel");
            $("#rtId").val(rtsel.val());
        }
        function manSelCheck() {
            var mansel = $("#manSel");
            $("#labManId").val(mansel.val());
            var op = $("#manSel option:selected");
            $("#labMan").val(op.html());
        }

        var use_fee = 0;
        var use_fee_u = 0;
        var sample_fee = 0;
        var sample_fee_u = 0;
        var sub_fee = 0;
        var sub_fee_u = 0;
        var fee_k = 60;//单价常量
        function calcFee() {
            var begin = $("#beginDate").val();
            var beginTime = NewDate(begin).getTime();
            var end = $("#endDate").val();
            var endTime = NewDate(end).getTime();
            var t = Math.ceil((endTime - beginTime) / (1000 * 60));//时间 分

            var resv_t = parseInt(t / 1440) + "天";
            resv_t += parseInt((t % 1440) / 60) + "时";
            resv_t += parseInt((t % 1440) % 60) + "分";
            $("#total_resv_time").html(resv_t);

            var use = GetFee2(use_fee, use_fee_u, t, fee_k);
            var sample = parseInt($("#sample").val()) * sample_fee;
            if (isNaN(sample)) {
                sample = 0;
            }
            var sub = parseInt($("#check").val()) * GetFee2(sub_fee, sub_fee_u, t, fee_k);
            $("#useFee").html(use);
            $("#sampleFee").html(sample);
            $("#subFee").html(sub);
            $("#totol").html((use + sample + sub).toFixed(2) + "元");
        }
        function submitForm(act, date) {
            var data = "";
            if (act == "form") {
                if ($("#labName").val() == "") {
                    MessageBox("实验内容不能为空！");
                    return false;
                }
                $("#bt_sub_resv").attr({ "disabled": "disabled" });
                data = $("#resvForm").serialize();
            }
            else if (act == "get") {
                data = "date=" + date;
            }
            else {
                return;
            }
            ShowWait();
            $.ajax({
                type: "GET",
                cache: false,
                url: "Ajax_Code/resvForm.aspx?act=" + act + "&" + data,
                dataType: "json",
                success: function (rlt) {
                    if (rlt.ret == 1) {
                        MsgBoxR("预约提交成功，可到[<a href='UserCenter.aspx?act=resv'>我的预约</a>]页面查看预约状态！");
                    }
                    else if (rlt.ret == 2) {
                        $("#rtSel ").html(rlt.rtOpt);
                        $("#manSel").html(rlt.manOpt);
                        $("#myTutor").html(rlt.myTutor);
                        $('#oth_info').html(rlt.resvStat);
                        $("#resvApply").dialog("open");
                        //单价
                        use_fee = (rlt.useFee / 100.00);
                        use_fee_u = rlt.useFeeUnit;
                        sample_fee = (rlt.sampleFee / 100.00);
                        sample_fee_u = rlt.sampleFeeUnit;
                        sub_fee = (rlt.subFee / 100.00);
                        sub_fee_u = rlt.subFeeUnit;
                        $("#unit_use").html(GetFee2(use_fee, use_fee_u, fee_k) + "元/时");
                        $("#unit_sample ").html(sample_fee + "元/份");
                        $("#unit_sub ").html(GetFee2(sub_fee, sub_fee_u, fee_k) + "元/时");
                        calcFee();
                        rtSelCheck();
                        manSelCheck();
                        $('#rsv_dev_name').html($('#cur_dev_name').val());
                    }
                    else {
                        MessageBox(rlt.msg);
                    }
                    $("#bt_sub_resv").removeAttr("disabled");
                    HideWait();
                },
                error: function (err) {
                    MessageBox("异步连接返回错误！");
                    $("#bt_sub_resv").removeAttr("disabled");
                    HideWait();
                }
            });
        }
        //预约表
        function loadCalendar() {
            $('#calendar').fullCalendar({
                height: 680,
                allDaySlot: false,
                axisFormat: 'HH:mm',
                timeFormat: 'HH:mm',
                minTime: 8,
                maxTime: 24,
                <%if (enable == "true")
                  {%>
                selectable: true,
                selectHelper: true,
                select: function (start, end, allDay) {
                    if (isloginTu()) {
                        if ("<%=useRole%>" != "y") {
                            uni.msgBox("没有使用资格，请查看日程表上方的资格状态。");
                        }
                        else {
                            var date = start;
                            $("#beginDate").val(start.format("yyyy-MM-dd HH:mm"));
                            $("#endDate").val(end.format("yyyy-MM-dd HH:mm"));
                            //$('#oth_info').html(info || "");
                            submitForm("get", start.format("yyyy-MM-dd"));
                        }
                    }
                    $('#calendar').fullCalendar('unselect');
                },
                <%}%>
                //editable: true,
                events: function (start, end, callback) {
                    var events = [];
                    //获取过时预约
                    var rsv_end=end;
                    //if (uni.compareDate(rsv_end, new Date) > 0) rsv_end = new Date();//最迟今天结束
                    $.ajax({
                        url: "Ajax_Code/reserve.aspx",
                        dataType: 'json',
                        cache: false,
                        data: {
                            start: Math.round(start.getTime() / 1000),
                            end: Math.round(rsv_end.getTime() / 1000)
                        },
                        success: function (resvs) {
                            if (resvs.ret != undefined || resvs.ret == 0) {
                                MsgBoxR("获取失败，刷新页面！");
                            }
                            else {
                                $(resvs).each(function () {
                                    var pthis = $(this);
                                    events.push({
                                        id: pthis.attr('id'),
                                        title: pthis.attr('title'),
                                        detail:this,
                                        start: pthis.attr('start'),
                                        end: pthis.attr('end'),
                                        color: pthis.attr('color'),
                                        allDay: pthis.attr('allDay')
                                    });
                                });
                                //预约状态
                    //            var startDate = new Date();//今天开始
                    //            var endDate = new Date(end);
                    //            var dts = [];
                    //            while (uni.compareDate(startDate, endDate) < 0) {
                    //                dts.push(startDate.format("yyyy-MM-dd"));
                    //                startDate.addDays(1);
                    //            }
                    //            if (dts.length > 0) {
                                //pro.j.dev.getDevRsvSta(devID, dts.join(), function (rlt) {
                    //                    var resvs = rlt.data.ts;
                    //        $(resvs).each(function (i) {
                    //            var pthis = $(this);
                    //            var sta = this.state;
                    //            var color = "#ccc";
                    //            if (sta == "doing") color = "#77A500";
                    //            if (sta == "undo") color = "#006DA3";
                    //            if (sta == "done") return true;
                    //            var t_start = pthis.attr('start').substr(11);
                    //            var t_end = pthis.attr('end').substr(11);
                    //            events.push({
                    //                id: i,
                    //                title: ">> " + t_end + "（" + this.owner + "，实验:" + this.title+"）",
                    //                detail:this,
                    //                start: pthis.attr('start'),
                    //                end: pthis.attr('end'),
                    //                color: color,
                    //                allDay: false
                    //            });
                    //        });
                    //        callback(events);
                    //    }, { all_open: "true", fail: function () { } });
                    //}
                    //else
                        callback(events);
                }
                        }
                    });

                    //$.ajax({
                    //    url: "Ajax_Code/reserve.aspx",
                    //    dataType: 'json',
                    //    cache: false,
                    //    data: {
                    //        start: Math.round(start.getTime() / 1000),
                    //        end: Math.round(end.getTime() / 1000)
                    //    },
                    //    success: function (resvs) {
                    //        if (resvs.ret != undefined || resvs.ret == 0) {
                    //            MsgBoxR("获取失败，刷新页面！");
                    //        }
                    //        else {
                    //            var events = [];
                    //            $(resvs).each(function () {
                    //                var pthis = $(this);
                    //                events.push({
                    //                    id: pthis.attr('id'),
                    //                    title: pthis.attr('title'),
                    //                    start: pthis.attr('start'),
                    //                    end: pthis.attr('end'),
                    //                    color: pthis.attr('color'),
                    //                    allDay: pthis.attr('allDay')
                    //                });
                    //            });
                    //            callback(events);
                    //            $("[title]").tooltip({
                    //                content: function () {
                    //                    var e = $(this);
                    //                    if (e.hasClass("fc-widget-content")) {
                    //                        var con = e.attr("title").replace(/\|/g, "<br/>");
                    //                        return con;
                    //                    }
                    //                    else {
                    //                        return e.attr("title");
                    //                    }
                    //                }
                    //            });
                    //        }
                    //    }
                    //});
                },
                eventClick: function (a) {
                    if ((parseInt($("#cur_ident").val()) & 268435456) > 0) {
                        var accno = a.detail.accno;
                        if (accno) {
                            pro.j.acc.getAccByAccNo(accno, function (rlt) {
                                var acc = rlt.data;
                                var str = "账户信息<br/>姓名:" + acc.name + "<br/>学工号:" + acc.id + "<br/>部门:" + acc.dept + "<br/>导师:" + (acc.tutor||'') + "<br/>电话:" + acc.phone + "<br/>Email:" + acc.email;
                                uni.msgBox(str, "实验者详情", function () { });
                            });
                        }
                    }
                }
            });
}
//function ShowApply(date, info) {
//    $("#beginDate").val(date + " 08:00");
//    $("#endDate").val(date + " 22:00");
//    $('#oth_info').html(info || "");
//    submitForm("get", date);
//}
//function ApplyResv() {
//    if (isloginTu()) {
//        var date = $("#calendar").fullCalendar("getDate");
//        ShowApply(date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate());
//    }
//}
////
$(document).ready(function () {
    //$(".button").button();
    $("#resvApply").dialog({ width: 780, autoOpen: false, modal: true, bgiframe: true });
    var req = uni.getReq();
    var str = req["act"];
    if (str == "resv") {
        $("#tagResv").parent().trigger("click");
        loadCalendar();
    }
    else {
        $("#tagResv").one("click", function () {
            $("#tagResv").parent().trigger("click");
            loadCalendar();
        });
        if (str == "achi") {
            $("#tagAchieve").parent().trigger("click");
        }
    }
    $("#applyName").html($("#cur_name").val());

    //视频
    $("#tagMedia").one(function () {
        $('.media').media();
    });

    //日期控件
    $(".Wdate").datetimepicker({
        timeFormat: "HH:mm",
        dateFormat: "yy-mm-dd",
        stepHour: 1,
        stepMinute: 10,
        hourMin: 8,
        hourMax: 24
    });
    //上传附件
    $(".upload_file").uploadFile();
    //上传奖状
    var rewardV = uni.getVessel($(".reward_rlt"), null, $(".reward_up_file"));
    $(".reward_file").uploadFile({}, function (rlt) {
        var f = rlt.data;
        rewardV.addItem(f.save, f.name);
    });
});
//成果上传
function UploadAchv() {
    if (isloginL()) {
        uni.dlg($("#dlg_upload"), "研究成果追踪表", 800, 200);
        $(".achi_kind:checked").trigger("click");
    }
}
$(function () {
    //成果
    $(".achi_kind").click(function () {
        if ($(this).is(":checked")) {
            var kind = $(this).val();
            $(".achi_tbl").each(function () {
                var p = $(this);
                if (p.attr("kind") == kind)
                    $(".achi_kind_panel").append(p);//p.show();
                else
                    $(".achi_tmp_panel").append(p);//p.hide();
            });
        }
    });
    var vessel = uni.getVessel($(".other_devs_rlt"), function (mbs) {
        $(".other_devs").val(mbs.keys());
    });
    $(".srch_devs").procomplete(function (it) {
        vessel.addItem(it.id, it.name, function (mbs) {
            $(".other_devs").val(mbs.keys());
        });
    });
    var upForm = $("#dlg_upload");
    $(".submit_achi", upForm).click(function () {
        if (upForm.mustItem()) {
            if ($("input[name=memo]", upForm).val() == "") {
                uni.msg.warning("还有文件未上传！");
                return;
            }
            pro.j.achi.setAchi($("form", upForm).getFormJson(), function (rlt) {
                uni.msgBoxR("提交成功");
            });
        }
    });
});
    </script>
    <style type="text/css">
        #calendar { text-align: center; font-size: 14px; color: #666; font-family: Arial,Helvetica,Verdana,sans-serif; }
        .must_it { border-color: red !important; }
        #calendar .fc-content { text-align: left; margin-bottom: 20px; }
        .achi_con li { display: inline; }
        .achi_con li label { cursor: pointer; }
        .achi_con table { border-collapse: collapse; }
        .achi_con th, .achi_con td { border-width: 1px; border-style: solid; }
        .sfrole_panel { margin-bottom: 5px; padding: 5px; background: #FFE6BA; border: 1px dotted #ddd; font-weight: bold; color: #5E3400; font-family: 'Microsoft YaHei'; }
        .achi_apply_tbl { width: 100%; }
        .achi_apply_tbl td { border: #ddd solid 1px; padding: 2px; }
        .achi_apply_tbl td input[type=text] { border: #ddd solid 1px; }
        .achi_apply_tbl td.title { background: #f9f9f9; width: 90px; vertical-align: middle; font-size: 12px; }
        .achi_tbl { width: 100%; }
        .kind_panel label { cursor: pointer; }
        .vessul_items li { display: inline; line-height: 20px; height: 22px; font-size: 12px; background: #BFE5F0; border: 1px solid #eee; margin: 1px; padding: 1px 2px; position: relative; color: #666; }
        .detail_intro { padding: 5px 3px; border: 1px dotted #ddd; margin-bottom: 5px; }

        .file_name_input { margin:5px 0;}
    </style>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="achi_tmp_panel hidden">
        <table class="thesis achi_tbl" kind="8">
            <tr>
                <td rowspan="5" class="title">发表论文</td>
                <td class="title">论文名称</td>
                <td colspan="3">
                    <input type="text" style="width: 460px;" name="achi_name" class="must" /></td>
            </tr>
            <tr>
                <td class="title">期刊名称</td>
                <td colspan="3">
                    <input type="text" style="width: 460px;" name="org" class="must" /></td>
            </tr>
            <tr>
                <td class="title">论文作者(前三)</td>
                <td colspan="3">
                    <input type="text" style="width: 460px;" name="member" class="must" /></td>
            </tr>
            <tr>
                <td class="title">期数、页码</td>
                <td colspan="3">
                    <input type="text" style="width: 460px;" name="cert_id" class="must" /></td>
            </tr>
            <tr>
                <td class="title">期刊等级</td>
                <td>
                    <select name="level" class="must">
                        <option value="">未选择</option>
                        <option value="1">SCI</option>
                        <option value="2">国家级</option>
                        <option value="3">省部级</option>
                    </select></td>
                <td class="title">影响因子</td>
                <td>
                    <input type="text" name="ext" class="must" /></td>
            </tr>
            <tr>
                <td class="title">论文附件</td>
                <td colspan="4">
                    <span>只接受PDF格式</span>
                    <div class="input-group" style="width: 100%;">
                        <div style="margin-bottom: 5px;">
                            <input type="file" name="report_file_name" id="report_file_name" class="click" />
                        </div>
                        <div>
                            <input type="hidden" class="up_file" name="memo" />
                            <input type="button" id="btn_upload" class="upload_file" file="report_file_name" value="上传" limit="pdf" /><span class="cur_file_name text-primary" style="padding-left: 5px;"></span>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <table class="prize achi_tbl" kind="1">
            <tr>
                <td rowspan="4" class="title">获奖情况</td>
                <td class="title">获奖名称</td>
                <td colspan="3">
                    <input type="text" style="width: 460px;" name="achi_name" class="must" /></td>
            </tr>
            <tr>
                <td class="title">获奖人员</td>
                <td colspan="3">
                    <input type="text" style="width: 460px;" name="member" class="must" /></td>
            </tr>
            <tr>
                <td class="title">获奖等级</td>
                <td>
                    <select name="level" class="must">
                        <option value="">未选择</option>
                        <option value="101">国家级</option>
                        <option value="102">省部级</option>
                        <option value="103">院校级</option>
                        <option value="0">其它</option>
                    </select></td>
                <td class="title">颁奖部门</td>
                <td>
                    <input type="text" name="org" class="must" /></td>
            </tr>
            <tr>
                <td class="title">获奖截图</td>
                <td colspan="3">
                    <span>奖状、证书截图，接受图片格式jpg,gif,png,bmp</span>
                    <div class="input-group" style="width: 100%;">
                        <div style="margin-bottom: 5px;">
                            <input type="file" name="img_file_name" id="img_file_name" class="click" />
                        </div>
                        <div>
                            <input type="hidden" class="reward_up_file" name="memo" />
                            <input type="button" id="btn_upload_img" class="reward_file" file="img_file_name" value="上传" limit="jpg,gif,png,bmp" /><span class="cur_file_name text-primary" style="padding-left: 5px;"></span>
                        </div>
                        <ul class="reward_rlt vessul_items"></ul>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="title">主要涉及论文</td>
                <td colspan="4">
                    <textarea name="ext" rows="5" style="width: 100%; padding: 3px;" class="must"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div class="dialog" id="dlg_upload">
        <form class="form" role="form" onsubmit="return false;">
            <input type="hidden" name="dev_id" value="<%=Request["dev"] %>" />
            <div style="margin: 20px 5px;" class="tbl">
                <table class="achi_apply_tbl detail">
                    <tbody>
                        <tr>
                            <td rowspan="3" class="title">仪器概况</td>
                            <td class="title">仪器名称</td>
                            <td colspan="3"><%=CurDevName %></td>
                        </tr>
                        <tr>
                            <td class="title">研究方向</td>
                            <td style="width: 232px;"><%=devLab %></td>
                            <td class="title">管理人</td>
                            <td><%=devMan %></td>
                        </tr>
                        <tr>
                            <td class="title">其它相关仪器</td>
                            <td colspan="3">
                                <input type="hidden" class="other_devs" name="achi_devs" />
                                <ul class="other_devs_rlt vessul_items"></ul>
                                <div>
                                    仪器名称搜索：<input type="text" class="srch_devs" url="searchDevice.aspx" onclick="this.value = ''" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="3" class="title">使用者概况</td>
                            <td class="title">姓  名</td>
                            <td><%=CurUserName %></td>
                            <td class="title">身 份</td>
                            <td><%=curAcc.dwIdent %></td>
                        </tr>
                        <tr>
                            <td class="title">所属院系</td>
                            <td><%=curAcc.szDeptName %></td>
                            <td class="title">导师姓名</td>
                            <td><%=curAcc.szTutorName %></td>
                        </tr>
                        <tr>
                            <td class="title">联系电话</td>
                            <td><%=curAcc.szHandPhone %></td>
                            <td class="title">邮件地址</td>
                            <td><%=curAcc.szEmail %></td>
                        </tr>
                    </tbody>
                    <tbody>
                        <tr>
                            <td class="title">成果类型</td>
                            <td colspan="4" class="title kind_panel" style="text-align: left;">&nbsp;<label><input type="radio" name="achi_kind" class="achi_kind" value="8" checked />论文发表</label>&nbsp;&nbsp;
                                &nbsp;<label><input type="radio" name="achi_kind" class="achi_kind" value="1" />获奖</label>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" class="achi_kind_panel"></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div style="text-align: center;">
                <button type="button" class="button submit_achi">提交</button>
                <button type="button" class="button dlg_close">返回</button>
            </div>
        </form>
    </div>
    <div class="g-b-m">
        <div class="hidden">
            <input type='hidden' id='cur_dev_name' value='<%=CurDevName %>' />
            <input type='hidden' id='cur_user_name' value='<%=CurUserName %>' />
            <input type='hidden' id='cur_user_id' value='<%=CurUserID %>' />
        </div>
        <div id="content">
            <div id="position">当前位置：<a href="Default.aspx">首页</a> > <a href="DevList.aspx">仪器共享</a> > <span class="u_pri_f"><%=pagePosition %></span></div>
            <div class="tags">
                <ul class="tag_head">
                    <li><a>仪器详情</a></li>
                    <li style="margin: 0 2px;"><a id="tagResv">仪器预约</a></li>
                    <li style="margin-right: 2px;"><a id="tagAchieve">成果展示</a></li>
                    <li><a id="tagMedia">规章制度</a></li>
                </ul>
                <div class="tag_con">
                    <div id="detail" class="item">
                        <div class="detail_up grey_box">
                            <div class="h_grey">仪器属性</div>
                            <div class="detail_con c_grey">
                                <table class="detail_list">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" rowspan="8"><a href="" class="detail_img">
                                                <img alt="" src="<%=imgUrl %>" border="0" style="width: 320px; height: 200px;" /></a></td>
                                            <td class="dt">仪器名称</td>
                                            <td class="dd"><span runat="server" id="devName"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">型号规格</td>
                                            <td class="dd"><span runat="server" id="devModel"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">英文名称</td>
                                            <td class="dd"><span runat="server" id="devEgName"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">资产编号</td>
                                            <td class="dd"><span runat="server" id="devAssertSN"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">仪器类型</td>
                                            <td class="dd"><span runat="server" id="devKind"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">研究方向</td>
                                            <td class="dd"><%=devLab %></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">放置位置</td>
                                            <td class="dd"><span runat="server" id="devLoc"></span></td>
                                        </tr>
                                        <tr>
                                            <td class="dt" rowspan="2">管理员</td>
                                            <td class="dd" rowspan="2"><%=devMan %></td>
                                        </tr>
                                        <tr>
                                            <td class="dt">仪器编号</td>
                                            <td class="dd"><span runat="server" id="devNum"></span></td>
                                            <%--<td class="dt">联系方式</td>--%>
<%--                                            <td class="dd"><span runat="server" id="devCon"></span></td>--%>
                                        </tr>
                                        <tr>
                                            <td class="dt">启用日期</td>
                                            <td class="dd"><span runat="server" id="devDate">2004年09月21日</span></td>
                                            <td class="dt">仪器状态</td>
                                            <td class="dd"><span runat="server" id="devSta"></span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="grey_box" style="border-bottom: none;">
                            <div class="h_grey">功能介绍</div>
                            <div class="c_grey">
                                <p runat="server" id="devFun"></p>
                            </div>
                        </div>
                        <div class="detail_para grey_box">
                            <div class="h_grey">使用说明</div>
                            <div class="c_grey">
                                <p runat="server" id="devPara"></p>
                            </div>
                        </div>
                        <div class="grey_box" style="border-top: none;">
                            <div class="h_grey">其它仪器资料</div>
                            <div class="c_grey">
                                <p runat="server" id="devSpecimen"></p>
                            </div>
                        </div>
                    </div>
                    <div id="resv" class="item" style="min-height: 500px;">
                        <div class="detail_intro">
                            <%=detailIntro %>
                        </div>
                        <div class="sfrole_panel">
                            <%=sfroleInfo %>
                        </div>
                        <div id='calendar'></div>
                        <div id="resvApply" title="预约申请" class="resv_panel dialog" style="display: none;">
                            <form name="resvForm" id="resvForm" onsubmit="return false;">
                                <div style="position: relative; height: 40px;">
                                    <div class="ui-tooltip ui-widget ui-widget-content ui-corner-all" style="top: 2px; left: 20px; max-width: 670px; font-size: 12px;" id="oth_info"></div>
                                </div>
                                <div class="resv_tbl">
                                    <table border="1">
                                        <tbody>
                                            <tr>
                                                <td colspan="4" style="text-align: left;" class="colored resv_tbl_h">预约仪器：<span id="rsv_dev_name"></span> | 申请人：<span id="applyName"></span><span id="myTutor"></span></td>
                                            </tr>
                                            <tr>
                                                <td class="colored th">
                                                    <div style="width: 76px;"><span style="color: red;">*</span>实验内容</div>
                                                </td>
                                                <td colspan="3" class="con">
                                                    <input name="labName" id="labName" type="text" class="ipt" /></td>
                                                <td class="colored th hidden">
                                                    <div style="width: 66px;">
                                                        实验课题
                                                    </div>
                                                </td>
                                                <td class="con hidden">
                                                    <input type="hidden" id="rtId" name="rtId" />
                                                    <select name="rtSel" id="rtSel" onchange="rtSelCheck()">
                                                    </select></td>
                                            </tr>
                                            <tr>
                                                <td class="colored th"><span style="color: red;">*</span>开始日期</td>
                                                <td class="con">
                                                    <input type="text" id="beginDate" name="beginDate" class="Wdate" readonly="readonly" onchange="calcFee();" /></td>
                                                <td class="colored th">
                                                    <div style="width: 66px;"><span style="color: red;">*</span>结束日期</div>
                                                </td>
                                                <td class="con">
                                                    <input type="text" id="endDate" name="endDate" class="Wdate" readonly="readonly" onchange="calcFee();" /></td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="colored th">委托检测</td>
                                                <td class="con">
                                                    <select name="check" id="check" onchange="calcFee();">
                                                        <option value="0">否</option>
                                                        <option value="1">是</option>
                                                    </select></td>
                                                <td class="colored th">
                                                    <div style="width: 66px;">自带耗材</div>
                                                </td>
                                                <td class="con">
                                                    <select name="selMat" id="selMat">
                                                        <option value="0">否</option>
                                                        <option value="1">是</option>
                                                    </select>
                                                    <input type="hidden" id="labManId" name="labManId" />
                                                    <input type="hidden" id="labMan" name="labMan" />
                                                    <select name="manSel" id="manSel" class="hidden" onchange="manSelCheck()">
                                                    </select></td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="colored th">样本数</td>
                                                <td style="text-align: left;" class="con">
                                                    <input name="sample" id="sample" type="text" class="ipt" onchange="calcFee()" value="0" /></td>
                                                <td class="colored th" style="width: 66px;">
                                                    <div>预约时长</div>
                                                </td>
                                                <td class="con">
                                                    <span id="total_resv_time"></span></td>
                                            </tr>
                                            <tr class="hidden">
                                                <td class="colored th">费用预估<br />
                                                    (仅参考)</td>
                                                <td colspan="3" class="fee_tbl">
                                                    <table style="width: 600px; text-align: center; border: 1px solid #ccc;">
                                                        <thead>
                                                            <tr style="background-color: #fafafa;">
                                                                <th>使用费(<span id="unit_use"></span>)</th>
                                                                <th>代检费(<span id="unit_sub"></span>)</th>
                                                                <th>样本费(<span id="unit_sample"></span>)</th>
                                                                <th>总计</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td><span id="useFee">0</span></td>
                                                                <td><span id="subFee">0</span></td>
                                                                <td><span id="sampleFee">0</span></td>
                                                                <td><span id="totol">0</span></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="colored th">申请说明</td>
                                                <td colspan="3">
                                                    <textarea rows="8" cols="10" id="memo" name="memo" style="width: 580px;" class="ipt"></textarea></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="div_submit" style="text-align: center;">
                                    <input type="button" class="sub_form" id="bt_sub_resv" onclick="submitForm('form'); return false;" value="提交" />
                                    <input type="button" class="sub_form" onclick="$('#resvApply').dialog('close'); return false;" value="返回" />
                                </div>
                            </form>
                        </div>
                        <div style="text-align: center; color: #666; font-size: 12px;">
                            <p>注意：学生预约仪器需要指定导师。预约使用仪器请遵守实验室的相关规章制度。</p>
                        </div>
                    </div>
                    <div id="achieve" class="item">
                        <div style="height: 40px; line-height: 40px; overflow: hidden; text-align: right;">
                            <button class="button" type="button" onclick="UploadAchv()">成果提交</button>
                        </div>
                        <div class="achi_con">
                            <ul class="tab_head">
                                <li>
                                    <label>
                                        <input type="radio" name="radio_group_kind" class="radio_kind" value="1" checked />论文发表</label></li>
                                <li>
                                    <label>
                                        <input type="radio" name="radio_group_kind" class="radio_kind" value="2" />获奖</label></li>
                            </ul>
                            <div class="tab_con">
                                <div class="tbl_list" kind="1">
                                    <table style="width: 100%; margin-top: 0;" id="devtbl">
                                        <thead>
                                            <tr class="tbl_head">
                                                <th>论文名称</th>
                                                <th>论文作者</th>
                                                <th>发表刊物</th>
                                                <th>刊物等级</th>
                                                <th>影响因子</th>
                                                <th>详细信息</th>
                                            </tr>
                                        </thead>
                                        <tbody runat="server" id="thesis">
                                        </tbody>
                                    </table>
                                </div>
                                <div class="tbl_list hidden" kind="2">
                                    <table style="width: 100%; margin-top: 0;">
                                        <thead>
                                            <tr class="tbl_head">
                                                <th>获奖名称</th>
                                                <th>获奖人员</th>
                                                <th>颁奖部门</th>
                                                <th>获奖等级</th>
                                                <th>详细信息</th>
                                            </tr>
                                        </thead>
                                        <tbody runat="server" id="prize">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <script>
                            $(".achi_con .radio_kind").click(function () {
                                var pthis = $(this);
                                if (pthis.is(":checked")) {
                                    $(".achi_con .tbl_list").each(function () {
                                        var p = $(this);
                                        if (p.attr("kind") == pthis.val())
                                            p.show();
                                        else
                                            p.hide();
                                    });
                                }
                            });
                        </script>
                    </div>
                    <div id="video" class="item">
                        <div runat="server" id="tagCon2">
                            <%--                            <div class="media" style="width: 720px; margin: 10px; text-align: center;">
                                <object codebase="http://www.apple.com/qtactivex/qtplugin.cab"
                                    classid="clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"
                                    type="application/x-oleobject" style="width: 500px; height: 360px;">
                                    <param name="url" value="Upload/DevImg/ski.wmv">
                                    <embed src="Upload/DevImg/ski.wmv"
                                        type="application/x-mplayer2"
                                        pluginspage="http://www.microsoft.com/Windows/MediaPlayer/"></embed>
                                </object>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="cleaner"></div>
    </div>
    <!-- END of templatemo_main -->
</asp:Content>
