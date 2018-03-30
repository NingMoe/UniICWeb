<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="apply.aspx.cs" Inherits="ClientWeb_xcus_cg2_apply" %>

<html>
<body>
    <style>
        .apply_table { width: 100%; background-color: #fff; }
        .apply_table .title { background-color: #fafafa; }
        .apply_table td { border: 1px solid #ddd; padding: 3px 4px; }
        .apply_table select { height: 28px; max-width: 200px; }
        .apply_table .sel_time { width: 60px; }
        .apply_table .p_dev_tmp .p_dev_info .close { display: none; }
        .apply_table .p_dev_info { margin: 5px; border: 1px solid #ccc; width: 99%; }
        .apply_table .head { background: #31b0d5; color: #fff; }
        .apply_table .cycle_tm_panel .sel_time_panel { min-height: 30px; }
        .apply_table .cycle_tm_panel tr td:first-child { width: 84px; }
        .p_rule { padding: 6px; line-height: 1.5em; text-align: left; color: #8a6d3b; background-color: #fcf8e3; border: 1px dotted #faebcc; }
        .resv_dev_detail { color: #aaa; }
        .p_resv_ret_msg .msg_wait { margin-bottom: 3px; padding-bottom: 3px; border-bottom: 1px dotted #ccc; }
        .resv_dev_name { font-weight: bold; }
    </style>
    <script>
        $(function () {
            var p = $("#apply_panel");
            //初始化
            var initTime = {};//初始时间
            var data = uni.hr.getPara();
            if (data) {
                var obj = data.obj;
                $(".p_dev_tmp .resv_dev_id", p).val(obj.devId);
                $(".p_dev_tmp .resv_dev_name", p).html(obj.devName);
                var detail = "位置:" + obj.campus + "," + obj.buildingName;
                initTime.startDate = initTime.endDate = data.dt;
                initTime.startTime = data.start;
                initTime.endTime = data.end;
                //室外场地
                if ((parseInt(obj.prop) & 2) > 0) {
                    $(".mb_min_num", p).val(obj.minUser);
                    $(".mb_max_num", p).val(obj.minUser);
                    $(".attend_info", p).attr("rowspan", "1");
                    $(".p_dev_tmp .resv_dev_detail", p).html(detail);
                    $(".mb_num", p).hide();
                }
                else {
                    $(".p_dev_tmp .resv_dev_detail", p).html("容量:" + obj.maxUser + "人" + "；" + detail);
                }
                var dlg = $(".p_rsch_devs_dlg");
                //隐藏空白类型 custom
                $(".filter .sel_devcls option", dlg).each(function () {
                    var pthis = $(this);
                    if (pthis.html() == "空白") {
                        pthis.hide();
                        return false;
                    }
                });
                //隐藏无效的校区
                var builds = $(".filter .sel_building option", dlg);
                $(".filter .sel_campus option", dlg).each(function () {
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
            }
            //第二课堂
            if ("<%=third.dwThirdResvID%>") {
                $(".p_add_dev_info", p).hide();
                initTime.startDate = initTime.endDate = "<%=thirdDate%>";
                initTime.startTime = "<%=thirdStart%>";
                initTime.endTime = "<%=thirdEnd%>";
            }

            if (pro.acc.phone && !$(".p_phone", p).val()) $(".p_phone", p).val(pro.acc.phone);
            if (pro.acc.email && !$(".p_email", p).val()) $(".p_email", p).val(pro.acc.email);
            $(".p_user", p).html(pro.acc.name);
            $(".p_apply_time", p).html((new Date()).format("yyyy-MM-dd HH:mm"));
            //初始化增加场地
            var sour = $(".p_dev_tmp", p);
            initDateTime(sour, initTime, uni.getVessel($(".spare_dev_name", sour), null, $(".spare_dev_id", sour)));
            $(".p_add_dev_info", p).click(function () {
                var rlt = pro.d.basic.analysisDateTime(sour);
                if (rlt) {
                    var tmp = $(sour.html());
                    $(".spare_dev_name,.resv_dev_name,.resv_dev_detail", tmp).html("");
                    $(".spare_dev_id,.resv_dev_id", tmp).val("");
                    $(".p_devs_list", p).append(tmp);
                    initDateTime(tmp, rlt, uni.getVessel($(".spare_dev_name", tmp), null, $(".spare_dev_id", tmp)));
                }
            });
            $(".reset_tbl", p).click(function () {
                uni.hr.reload(function () {
                    uni.backTop();
                });
            });
            //第二课堂 去掉周期时间
            if ("<%=third.dwThirdResvID%>") {
                $(".cvt_cycle_day", p).hide();
            }
            //教室零星借用 却掉多场地和周期
            if (("<%=isTemporary%>").toLowerCase() == "true") {
                $(".p_add_dev_info", p).hide();
                $(".cvt_cycle_day", p).hide();
            }
            //教室批量
            if ("<%=notCourse%>".toLowerCase() == "false") {
                $(".cvt_single_day",p).hide();
            }

            //提交预约
            var msgdlg = $(".p_resv_ret_msg", p);
            $(".sub_yard_resv", p).click(function () {
                //活动类型必选
                var typeList = $(".aty_type_list input:visible", p);
                if (typeList.length>0&&!typeList.is(":checked")) {
                    uni.msgBox("活动类型必选");
                    return;
                }
                //必填检查
                if (!p.mustItem()) return;
                //赋值最少人数
                //$(".mb_min_num", p).val($(".mb_max_num", p).val());
                //if ($(".signature",p).val() != pro.acc.name) {
                //    uni.msgBox("签名未检测通过");
                //    return;
                //}
                //计算审核类型值
                var ck = 0;
                ck += parseInt(($(".devman_v", p).val()) || 0);
                $(".service_v:checked", p).each(function () {
                    var v = parseInt($(this).val());
                    if (!isNaN(v))
                        ck += v;
                });
                //循环提交时间
                var ckFlag = true;
                var array = [];//预约的场地对象数组
                $(".p_dev_info", p).each(function () {
                    var para = {};
                    var info = $(this);
                    var rlt = pro.d.basic.analysisDateTime(info);
                    if (!rlt) { ckFlag = false; return false; }
                    var id = $(".resv_dev_id", info).val();
                    if (!id) {
                        uni.msgBox("请选择预约的场地");
                        ckFlag = false;
                        return false;
                    }
                    para.info = info;
                    para.rlt = rlt;
                    para.id = id;
                    para.name = $(".resv_dev_name", info).html();
                    para.ck = ck + parseInt(($(".director_v", info).val()) || 0);
                    para.dir = $(".director_v option:selected", info).html();
                    para.spare = $(".spare_dev_id", info).val();
                    array.push(para);
                });
                if (ckFlag) {
                    var msg = "<strong style='font-size:14px;'>确认提交申请？</strong><br/>------------------------------------------<br/>";
                    $.each(array, function () {
                        msg += "<strong class='text-primary'>" + this.name + "：</strong>" + this.rlt.desc + " / " + this.dir + "<br/>";
                    });
                    uni.confirm(msg, function () {
                        //状态窗口
                        msgdlg.html("<div class='msg_wait'>正在提交预约请稍等...</div>");
                        uni.dlg(msgdlg, "提交状态", 320, 240, null, null, function () {
                            var failalter = $(".resv_fail_list");
                            if (fail.length > 0) {
                                failalter.html("<strong style='font-size:16px;'>错误信息 >> </strong>" + fail).show();
                                uni.backTop();
                            }
                            //else
                            //    failalter.hide();
                        });
                        //状态信息
                        var fail = "";
                        var finish = $("<div class='finish'><strong class='red'>提交失败，有错误!</strong></div>");
                        //finish.find(".back").click(function () {&nbsp;&nbsp;<a class='back'>关闭窗口</a>
                        //    msgdlg.dialog("close");
                        //    //uni.hr.reload(function () {
                        //        uni.backTop();
                        //        var failalter = $(".resv_fail_list");
                        //        if (fail.length > 0)
                        //            failalter.html("<strong style='font-size:16px;'>错误信息：</strong>" + fail).show();
                        //        else
                        //            failalter.hide();
                        //    //});
                        //});
                        var sucList = $(".resv_suc_list");
                        function revoke() {//撤销成功的预约
                            var id = sucList.val();
                            if (id) {
                                id = id.substr(0, id.length - 1);
                                pro.j.rsv.delResvGroup(id, function () { });
                            }
                        };
                        sucList.val('');
                        //循环提交
                        $.each(array, function (index) {
                            var info = this.info;
                            var rlt = this.rlt;
                            var name = this.name;
                            $(".dev_id", p).val(this.id);
                            $(".spare_devs", p).val(this.spare);
                            $(".ck_kind", p).val(this.ck);
                            $(".desc", p).val(rlt.date.length == 1 ? "" : rlt.desc);
                            $(".start", p).val(rlt.start);
                            $(".end", p).val(rlt.end);
                            pro.j.rsv.fRsv("set_yard_rsv", $("form", p), function (rlt) {
                                if (index >= $(".p_dev_info", p).length - 1) {
                                    if (fail) {//有失败
                                        $(".msg_wait", msgdlg).html(finish);
                                        revoke();
                                    }
                                    else {
                                        msgdlg.dialog("close");
                                        uni.msgBox("申请提交成功，将跳转到个人中心！", null, function () { $("#user_center").trigger("click"); });
                                        //uni.confirm("申请提交成功，是否跳转到个人中心？", function () { $("#user_center").trigger("click"); });
                                    }
                                }
                                //msgdlg.append("<div class='success'><strong>" + name + "</strong>：<span class='green'>预约成功</span></div>");
                                sucList.val(sucList.val() + rlt.data.dwResvID + ',');
                            }, function (rlt) {
                                if (index >= $(".p_dev_info", p).length - 1) {
                                    $(".msg_wait", msgdlg).html(finish);
                                    revoke();
                                }
                                var m = "<span class='fail'><strong>" + name + "</strong> <span class='red'>预约出错</span>：" + rlt.msg + "</span>";
                                msgdlg.append(m + "<br/>");
                                fail += m + "；";
                            });
                        });
                    }, null, '', { width: 460 });
                }
                //$(".p_dev_info", p).each(function (index) {
                //    var info = $(this);
                //    var rlt = pro.d.basic.analysisDateTime(info);
                //    if (!rlt) return false;
                //    var name = $(".resv_dev_name", info).html();
                //    if (!name) return true;//未设置预约设备
                //    $(".dev_id", p).val($(".resv_dev_id", info).val());
                //    $(".spare_devs", p).val($(".spare_dev_id", info).val());
                //    var dir_v = parseInt(($(".director_v", info).val()) || 0);
                //    $(".ck_kind", p).val(ck + dir_v);
                //    $(".desc", p).val(rlt.desc);
                //    $(".start", p).val(rlt.start);
                //    $(".end", p).val(rlt.end);
                //    pro.j.rsv.fRsv("set_yard_rsv", $("form", p), function () {
                //        if (index >= $(".p_dev_info", p).length-1) {
                //            $(".msg_wait", msgdlg).html(finish);
                //        }
                //        msgdlg.append("<div class='success'><strong>" + name + "</strong>：<span class='green'>预约成功</span></div>");
                //    }, function (rlt) {
                //        if (index >= $(".p_dev_info", p).length - 1) {
                //            $(".msg_wait", msgdlg).html(finish);
                //        }
                //        var m = "<span class='fail'><strong>" + name + "</strong>：<span class='red'>预约失败</span>(" + rlt.msg + ")</span>";
                //        msgdlg.append(m + "<br/>");
                //        fail += m + "；";
                //    });
                //});
            });
            //初始化时间
            function initDateTime(info, rlt, vessel) {
                rlt.dftM = ("<%=notCourse%>").toLowerCase() == "true" ? "single" : "multi";//默认单日模式
                rlt.cycleText = "按周期借用";
                rlt.singleText = "单次借用";
                pro.d.basic.cycleDateTimePicker($(".cycle_tm_panel", info), rlt);
                //初始化添加设备
                $(".p_add_dev", info).click(function () {
                    var info = $(this).parents(".p_dev_info");
                    var flag = false;
                    $("input:visible", info).each(function () {
                        if ($(this).val() == "") flag = true;
                    });
                    if (flag) {
                        uni.msgBox("请先填入预约时间");
                        return;
                    }
                    var dlg = $(".p_rsch_devs_dlg");
                    dlg.src = this;
                    dlg.vessel = vessel;
                    dlg.rlt = pro.d.basic.analysisDateTime(info);
                    if (!dlg.rlt) return;
                    $(".resv_dt_desc", dlg).html(dlg.rlt.desc);
                    $(".dev_rsch_result", dlg).html("");
                    $("#pCtrl").html("");
                    uni.dlg(dlg, "选择场地", 890, 620);
                });
                //初始化检查空闲
                $(".p_ck_free", info).click(function () {
                    var id = $(".resv_dev_id", info).val();
                    if (id) {
                        var ret = pro.d.basic.analysisDateTime(info);
                        if (ret) {
                            var para = {};
                            para.dev_id = id;
                            para.date = ret.date.join();
                            para.fr_start = ret.startTime;
                            para.fr_end = ret.endTime;
                            pro.j.dev.getRsvSta(para, function (rlt) {
                                var data = rlt.data;
                                if (data.length > 0) {
                                    var sta = data[0].freeSta;
                                    //if (sta > 0) {
                                    //    uni.msg.warning("有预约，但可以继续预约。");已被占用
                                    //}
                                    //else
                                        if (sta == 0) {
                                        uni.msgBox("场地空闲");
                                    }
                                        else if (sta == -1 || sta > 0) {
                                        uni.msg.error("场地不空闲，不可预约。");
                                    }
                                    else {
                                        var open = data[0].open;
                                        uni.msg.error("不在开放时间(" + open[0] + "-" + open[1] + ")");
                                    }
                                }
                            });
                        }
                    }
                    else {
                        uni.msgBox("未选择场地");
                    }
                });
            }
        });
    </script>
    <div class="click btn_back" style="display: <%=Request["back"] == "true"?"":"none"%>" onclick="uni.hr.back();"><span class="glyphicon glyphicon-chevron-left"></span>&nbsp;返回</div>
    <div class="alert alert-warning resv_fail_list" style="display: none;" role="alert"></div>
    <div>
        <input type="hidden" class="resv_suc_list" />
    </div>
    <div id="apply_panel">
        <h1 class="h_title">场地申请</h1>
        <div class="line"></div>
        <form onsubmit="return false;">
            <input type="hidden" class="dev_id" name="dev_id" />
            <input type="hidden" class="spare_devs" name="spare_devs" />
            <input type="hidden" class="desc" name="cyc_desc" />
            <input type="hidden" class="start" name="start" />
            <input type="hidden" class="end" name="end" />
            <input type="hidden" name="type" value="dev" />
            <input type="hidden" value="10" name="level" />
            <input type="hidden" value="<%=activity.dwSecurityLevel %>" name="security" />
            <input type="hidden" name="aty_id" class="aty_id" value="<%=atyId %>" />
            <input type="hidden" name="aty_name" class="aty_name" value="<%=atyName %>" />
            <input type="hidden" name="ck_kind" class="ck_kind" />
            <input type="hidden" class="devman_v" runat="server" id="devman" />
            <input type="hidden" name="third_id" value="<%=third.dwThirdResvID%>" />
            <table class="apply_table">
                <tbody>
                    <tr class="thead">
                        <td colspan="5" class="text-center head"><strong class="apply_aty"><%=atyName %>申请表</strong></td>
                    </tr>
                    <tr>
                        <td class="title text-center" rowspan="2" style="width: 86px;">申请人信息</td>
                        <td class="title text-right" style="width: 18%;">姓名：</td>
                        <td colspan="3">
                            <span class="p_user"><%=resv.szApplicantName %></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="title text-right"><span class="red">*</span>&nbsp;手机：</td>
                        <td>
                            <input type="text" class="p_phone must" data-msg="请填入手机号" name="phone" size="15" maxlength="20" value="<%=third.dwThirdResvID!=null?third.szHandPhone:resv.szHandPhone %>"></td>
                        <td class="title text-right"><span class="red">*</span>&nbsp;E-mail：</td>
                        <td>
                            <input type="text" class="p_email must" data-msg="请填入邮箱地址" name="email" size="15" maxlength="32" value="<%=third.dwThirdResvID!=null?third.szEmail:resv.szEmail %>"></td>
                    </tr>
                    <tr>
                        <td class="title text-center" rowspan="<%=isSports?"5":"6" %>" style="text-align: center">用途/性质</td>
                        <td class="title text-right"><span class="red">*</span>&nbsp;活动名称：</td>
                        <td colspan="3">
                            <input type="text" name="resv_name" class="must" size="50" maxlength="30" value="<%=third.dwThirdResvID!=null?third.szResvTitle:resv.szResvName %>"></td>
                    </tr>
                    <tr>
                        <td class="text-right title"><span class="red">*</span>&nbsp;组织方：</td>
                        <td colspan="3">
                            <input type="text" name="org" size="30" maxlength="30" value="<%=third.dwThirdResvID!=null?third.szOrganization:resv.szOrganization %>" class="must" data-msg="组织方必须填写" /></td>
                    </tr>
                    <tr class="<%=isSports?"hidden":""%>">
                        <td class="title text-right"><span class="red">*</span>&nbsp;活动类型：</td>
                        <td colspan="3" class="aty_type_list">
                            <%=typeList %>
                        </td>
                    </tr>
                    <tr>
                        <td class="title text-right"><span class="red">*</span>&nbsp;是否有偿使用：</td>
                        <td colspan="3">
                            <label>
                                <input type="radio" name="prop_profit" class="prop_profit" value="true" <%=(profit=="true"?"checked":"") %>>&nbsp;有偿&nbsp;</label>
                            <label>
                                <input type="radio" name="prop_profit" <%=(profit=="true"?"":"checked") %>>&nbsp;无偿&nbsp;</label>
                        </td>
                    </tr>
                                        <tr>
                        <td class="title text-right"><span class="red">*</span>&nbsp;是否使用多媒体：</td>
                        <td colspan="3">
                            <label>
                                <input type="radio" name="prop_media" class="prop_media" value="true" <%=(media=="true"?"checked":"") %>>&nbsp;使用多媒体&nbsp;</label>
                            <label>
                                <input type="radio" name="prop_media" <%=(media=="true"?"":"checked") %>>&nbsp;不使用多媒体&nbsp;</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title text-right"><span class="red">*</span>&nbsp;是否公开活动：</td>
                        <td colspan="3">
                            <label>
                                <input type="radio" class="prop_open" name="prop_open" value="true" <%=(open=="true"?"checked":"") %>>&nbsp;公开活动&nbsp;</label>
                            <label>
                                <input type="radio" name="prop_open" <%=(open=="true"?"":"checked") %>>&nbsp;不公开活动&nbsp;</label>
                        </td>
                    </tr>
                    <%if(!isSports){ %>
                    <tr>
                        <td class="title text-center" rowspan="2" style="text-align: center">主讲人情况</td>
                        <td class="text-right title">主讲人姓名：</td>
                        <td colspan="3">
                            <input type="text" maxlength="30" name="presenter" value="<%=third.dwThirdResvID!=null?third.szPresenter:resv.szPresenter %>" /></td>
                    </tr>
                    <tr>
                        <td class="text-right title">背景介绍：</td>
                        <td colspan="3">
                            <textarea name="intro" rows="3" cols="50" maxlength="30"><%=third.dwThirdResvID!=null?third.szIntroInfo:resv.szIntroInfo %></textarea></td>
                    </tr>
                    <%} %>
                    <tr>
                        <td class="title text-center attend_info" rowspan="2">出席者情况</td>
                        <td class="title text-right">参与条件：</td>
                        <td colspan="3">
                            <div>
                                <%=condition %>
                            </div>
                            <textarea name="require" rows="3" cols="50" maxlength="30"><%=require %></textarea></td>
                    </tr>
                    <tr class="mb_num">
                        <td class="title text-right"><span class="red">*</span>&nbsp;参与人数：</td>
                        <td colspan="3">
                            <%if(isSports){ %>
                            <input type="hidden" class="mb_max_num" /><!--避免向mb_max_num赋值出错-->
                            <input type="text"  class="must" data-msg="请填入参与人数" data-reg="number" data-ckmsg="只能填入数字" maxlength="5" name="presenter" style="width: 80px;" value="<%=third.dwThirdResvID!=null?third.szPresenter:resv.szPresenter %>" />
                            <%}else{ %>
                            <input type="text" name="mb_min_num" class="mb_min_num must hint hidden" placeholder="人数下限" data-msg="请填入参与人数下限" data-reg="number" data-ckmsg="只能填入数字" maxlength="5" style="width: 80px;" value="<%=third.dwThirdResvID!=null?third.dwMinAttendance:resv.dwMinAttendance %>"><!--&nbsp;到&nbsp;-->
                            <input type="text" name="mb_max_num" class="mb_max_num must hint" data-msg="请填入参与人数" data-reg="number" data-ckmsg="只能填入数字" maxlength="5" style="width: 80px;" value="<%=third.dwThirdResvID!=null?third.dwMaxAttendance:resv.dwMaxAttendance %>">
                            <%} %>
                        </td>
                    </tr>
                    <tr style="display: <%=resv.dwResvID==null?"none":""%>">
                        <td colspan="5" style="height: 2px; padding: 0; background-color: red;"></td>
                    </tr>
                    <tr class="<%=isSports?"hidden":""%>">
                        <td class="title text-center">申请服务</td>
                        <td colspan="4">
                            <div class='checkbox'>
                                <%=checkService %>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="title text-center">&nbsp;场地信息</td>
                        <td class="p_devs_panel" colspan="4">
                            <div class="p_devs_act text-right"><a class="p_add_dev_info"><span class="glyphicon glyphicon-plus-sign"></span>&nbsp;增加场地申请表&nbsp;</a></div>
                            <div class="p_dev_tmp">
                                <table class="p_dev_info">
                                    <tbody>
                                        <tr>
                                            <td colspan="4" class="text-center head"><strong>场地申请表</strong><span class="pull-right click close" onclick="$(this).parents('.p_dev_info').remove();">x&nbsp;</span></td>
                                        </tr>
                                        <tr>
                                            <td class="title text-center" style="width: 90px;"><span class="red">*</span>&nbsp;预约时间：</td>
                                            <td colspan="3">
                                                <table style="width: 100%; height: 100%;">
                                                    <tbody class="cycle_tm_panel"></tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title text-center">预约场地：</td>
                                            <td colspan="3">
                                                <a class="pull-right p_add_dev p_change_dev" data-type="resv">选择/更换场地&nbsp;</a>
                                                <a class="pull-right p_ck_free">&nbsp;&nbsp;检查空闲&nbsp;&nbsp;</a>
                                                <div style="padding-right: 190px;">
                                                    <span class="resv_dev_name"></span>&nbsp;&nbsp;
                                                <span class="resv_dev_detail"></span>
                                                    <input type="hidden" class="resv_dev_id" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="hidden">
                                            <td class="title text-center">备选场地：</td>
                                            <td colspan="3">
                                                <a class="pull-right p_add_dev" data-type="spare">添加场地&nbsp;</a>
                                                <ul class="spare_dev_name ul_vessel" style="margin: 0 80px 0 0;"></ul>
                                                <input type="hidden" class="spare_dev_id" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="title text-center">归口部门：</td>
                                            <td colspan="3">
                                                <select class="director_v must" data-msg="未选择归口部门"><%=checkDirector %></select></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="p_devs_list">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="title text-center">备注信息</td>
                        <td colspan="4">
                            <textarea rows="3" class="memo" name="memo" maxlength="40" style="width: 100%;"><%=third.dwThirdResvID!=null?third.szMemo:resv.szMemo %></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="title text-center">审核说明</td>
                        <td colspan="4">
                            <%--                            1.各院系学术讲座、办班等院长或系主任负责审批；<br />
                            2.各院系学生活动由各院系总支副书记负责审批(学生社团活动除外)；<br />
                            3.校团委、校学生会、社团联合会以及所有学生社团活动归口团委审批。<br />
                            4.后勤管理处直接审批(适应于“就业指导中心”等特殊部门)。--%>
                            <div>
                                <%=ckIntro %>
                            </div>
                        </td>
                    </tr>
<%--                    <tr>
                        <td class="title text-center">申请人承诺</td>
                        <td colspan="4">(1)遵守学校教室场所使用管理要求，保持环境整洁，不吸烟、不乱抛口香糖等杂物。
                            <br />
                            (2)遵守学校治安管理规定，确保安全使用。若因借用人管理和使用不当造成安全事故，借用人自行承担责任。<br />
                            (3)遵守学校财产物资规定，损坏设备设施按原值赔偿。
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="title text-center">填表时间</td>
                        <td colspan="4">
                            <%--<input type="text" class="must signature" data-msg="请在签名框输入本人姓名" maxlength="20">(请在框内输入本人姓名)--%>
                            <span class="p_apply_time"></span></td>
                    </tr>
                </tbody>
            </table>
        </form>
        <div class="text-center" style="margin-top: 10px;">
            <button type="button" class="btn btn-info sub_yard_resv">提交</button>
            <button type="button" class="btn btn-default reset_tbl">重置</button>
        </div>
        <%--结果窗口--%>
        <div class="dialog p_resv_ret_msg">
        </div>
    </div>
    <%--选择设备窗口--%>
    <style>
        .p_rsch_devs_dlg .filter select { width: 160px; margin: auto; }
        .p_rsch_devs_dlg .filter input { width: 160px; }
        .p_rsch_devs_dlg .sel_dev_tbl { width: 100%; }
        .p_rsch_devs_dlg .sel_dev_tbl td { border: 1px solid #ddd; padding: 2px 1px; }
        .p_rsch_devs_dlg .sel_dev_tbl th { border: 1px solid #ddd; text-align: center; background: #f9f9f9; }
        .p_rsch_devs_dlg .sel_dev_tbl .filter td { text-align: center; background: #f9f9f9; }
        .p_rsch_devs_dlg .ck_free_panel label { margin-bottom: 0; padding-left: 3px; }
    </style>
    <div class="dialog p_rsch_devs_dlg">
        <div style="min-height: 500px;">
            <div>日期：<span class="resv_dt_desc"></span></div>
            <table class="sel_dev_tbl">
                <thead>
                    <tr>
                        <th>名称</th>
                        <th>校区</th>
                        <th>位置</th>
                        <th style="display: <%=notCourse?"none":""%>">类型</th>
                        <th>人数</th>
                        <th>状态</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="filter">
                        <td>
                            <input id="keyword" class="form-control must_sel" type="text" key="dev_name"><input type="hidden" key="ext_id" value="<%=atyId %>" /></td>
                        <td>
                            <select class="form-control must_sel sel_campus" key="campus" affect="building_id">
                                <option value="0">未选择</option>
                                <%=CampusList %>
                            </select></td>
                        <td>
                            <select class="form-control must_sel sel_building" key="building_id">
                                <option value="0">未选择</option>
                                <%=BuildingList %>
                            </select></td>
                        <td style="display: <%=notCourse?"none":""%>">
                            <select class="form-control sel_devcls" key="class_id">
                                <option value="0">全部</option>
                                <%=ClassList %>
                            </select></td>
                        <td>
                            <input class="form-control must allow_null" data-reg="number" data-ckmsg="只能填入数字" type="text" key="user_num" style="width: 40px;" /></td>
                        <td></td>
                        <td>
                            <button type="button" class="btn btn-info sub_filter">搜索</button></td>
                    </tr>
                    <tr class="ck_free_panel">
                        <td colspan="7">
                            <div class="text-left">
<%--                                <label>
                                    <input type="checkbox" name="free_sta" class="ck_freesta_resv" value="1" />包含有预约&nbsp;</label>--%>
                                <label>
                                    <input type="checkbox" name="free_sta" class="ck_freesta_all" value="-1" />显示全部&nbsp;</label>
                                <%--<span class="pull-right" style="line-height: 20px;"><code>状态解释:</code>&nbsp;<span class="green">空闲</span>:无人预约；<span class="orange">有预约</span>:有预约但还未获批准；<span class="red">已占用</span>:有预约并已获批准；<span class="red">不开放</span>:不在开放时间内。</span>--%>
                            </div>
                        </td>
                    </tr>
                </tbody>
                <tbody>
                    <tr>
                        <td colspan="7">
                            <table class="dev_rsch_result table-striped" style="width: 100%;"></table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="pCtrl"></div>
    </div>
    <script>
        (function () {
            $(function () {
                //初始化筛选
                $(".p_rsch_devs_dlg .filter").filterItem(pRetSelecteds);
            });
            function pRetSelecteds(filter, type) {
                if (!$(".p_rsch_devs_dlg .filter").mustItem()) return;
                var flg = false;
                $(".p_rsch_devs_dlg .must_sel").each(function () {
                    if ($(this).val() != "0" && $(this).val() != "") flg = true;
                });
                if (!flg) {
                    uni.msgBox("校区/位置至少选一个");
                    return;
                }
                var result = filter || {};
                pSubmitRet(result);
            }
            //提交搜索
            function pSubmitRet(condition) {
                var dlg = uni.dlgInst.dlg;
                if (!dlg) return;
                var src = $(dlg.src);
                var vessel = dlg.vessel;
                var type = src.data("type");
                var info = src.parents(".p_dev_info");
                var rlt = dlg.rlt;
                condition.date = rlt.date.join(',');
                condition.fr_start = rlt.startTime;
                condition.fr_end = rlt.endTime;
                pro.j.dev.getRsvSta(condition, function (con) {
                    var content = $("<tbody></tbody>");
                    var devs = con.data;
                    $(devs).each(function (i) {
                        if (this.state == "close" || this.state == "forbid") return true;//不开放或禁用
                        var state = "free";
                        if (parseInt(this.freeSta) > 0)
                            state = "resv";
                        if (parseInt(this.freeSta) < 0)
                            state = "occu";
                        var tr = $("<tr state='" + state + "' data-sn='" + this.freeSta + "'><td class='sort sort_name'>" + this.devName + "</td><td>" + this.campus + "</td><td>" + this.buildingName + "</td><td style='display:<%=notCourse?"none":""%>'>" + this.className + "</td><td>" + this.maxUser + "人</td><td class='sort sort_free'>" + convertState(this) + "</td><td style='width:40px;'><a class='sel_dev'>选择</a></td></tr>");
                        tr.find("a.sel_dev").click(this, function (e) {
                            var dev = e.data;
                            if (type == "resv") {
                                $(".resv_dev_name", info).html(dev.devName);
                                $(".resv_dev_id", info).val(dev.devId);
                                var fm = $("#apply_panel");
                                var str = "位置:" + dev.campus + "," + dev.buildingName;
                                if ((parseInt(dev.prop) & 2) > 0) {//户外场地
                                    $(".resv_dev_detail", info).html(str);
                                    $(".mb_min_num", fm).val(dev.minUser);
                                    $(".mb_max_num", fm).val(dev.minUser);
                                    $(".attend_info", fm).attr("rowspan", "1");
                                    $(".mb_num", fm).hide();
                                }
                                else {//非户外场地
                                    $(".resv_dev_detail", info).html("容量:" + dev.maxUser + "人；" + str);
                                    $(".mb_min_num", fm).val("<%=third.dwThirdResvID!=null?third.dwMinAttendance:resv.dwMinAttendance %>");
                                    $(".mb_max_num", fm).val("<%=third.dwThirdResvID!=null?third.dwMaxAttendance:resv.dwMaxAttendance %>");
                                    $(".attend_info", fm).attr("rowspan", "2");
                                    $(".mb_num", fm).show();
                                }
                            }
                            else if (type == "spare") {
                                //var na = $(".spare_dev_name", info).html();
                                //var id = $(".spare_dev_id", info).val();
                                //na += (na ? "," : "") + dev.devName;
                                //id += (id ? "," : "") + dev.devId;
                                //var na = $(".spare_dev_name", info).html(na);
                                //var id = $(".spare_dev_id", info).val(id);
                                if ($(".resv_dev_id").val() == dev.devId) {
                                    uni.msgBox("已被选作主场地");
                                    return;
                                }
                                else if (vessel.mbs.get(dev.devId)) {
                                    uni.msgBox("已在备用场地中");
                                    return;
                                }
                                else vessel.addItem(dev.devId, dev.devName);
                            }
                            $(dlg).dialog("close");
                        });
                        content.append(tr);
                    });
                    //var list = content.find("tr");
                    //uni.sort(list);
                    //content.html(list);
                    staFilter();
                    //content.pctrl($("#pCtrl"), 20);
                    var rsch_rlt = $(".p_rsch_devs_dlg .dev_rsch_result").html(content);
                    $("[name=free_sta]", dlg).change(function () {
                        staFilter();
                    });
                    function staFilter() {
                        var list = content.find("tr");
                        //var resv = $(".ck_freesta_resv", dlg).is(":checked");
                        var all = $(".ck_freesta_all", dlg).is(":checked");
                        list.each(function () {
                            var pthis = $(this);
                            var state = pthis.attr("state");
                            pthis.addClass("it");
                            pthis.hide();
                            if (!all) {
                                if (state == "occu" || state == "resv")
                                    pthis.removeClass("it");
                                //if (!resv) {
                                //    if (state == "resv")
                                //        pthis.removeClass("it");
                                //}
                            }
                        });
                        content.pctrl($("#pCtrl"), 20);
                    }
                    function convertState(dev) {
                        var sta = parseInt(dev.freeSta);
                        if (sta == 0)
                            return "<span class='green'>空闲</span>";
                        //else if (sta > 0)
                        //    return "<span class='orange'>有预约</span>(" + sta + ")";已占用
                        else if (sta == -1 || sta == -2 || sta > 0)
                            return "<span class='red'>有预约</span>";
                        else if (sta == -3)
                            return "<span class='red'>不开放</span>(" + dev.open[0] + "-" + dev.open[1] + ")";
                    }
                });
            }
        })();
    </script>
</body>
</html>
