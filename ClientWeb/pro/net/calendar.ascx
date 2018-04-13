<%@ Control Language="C#" AutoEventWireup="true" CodeFile="calendar.ascx.cs" Inherits="ClientWeb_pro_net_calendar" %>
<div class="dlg_resv_panel_group" onselect="return false;">
    <div class="resv_panel dialog" id="dlg_resv_panel_default_<%=id %>">
        <div class="div_remark remark">
        </div>
        <div class="rsv_state_slider" style="margin: 0 5px;"></div>
        <form onsubmit="return false;" class="list">
            <div>
                <input type="hidden" class="dev_id" name="dev_id" />
                <input type="hidden" class="lab_id" name="lab_id" />
                <input type="hidden" class="kind_id" name="kind_id" />
                <input type="hidden" class="room_id" name="room_id" />
                <input type="hidden" class="type" name="type" value="dev" />
                <input type="hidden" class="prop" name="prop" />
                <input type="hidden" class="test_id" name="test_id" />
                <input type="hidden" class="term" name="term" />
                <table>
                    <tbody>
                        <tr>
                            <td><span class="uni_trans">申请信息</span></td>
                            <td>
                                <span class="rsv_obj_name"></span>，<span class="uni_trans">申请人</span>：<span class="apply_people"></span>
                            </td>
                        </tr>
                        <%if((ToUInt(GetConfig("resvKind"))&ToUInt(ClassKind))>0){ %>
                        <tr class="tr_theme">
                            <td><span class="resv_kind uni_trans"><%=Translate("主题类型") %></span></td>
                            <td>
                                <select name="resv_kind" class="form-control con_theme must" style="width:233px;">
                                    <%=resvKinds %>
                                </select>
                            </td>
                        </tr>
                        <%} %>
                        <tr class="tr_theme <%=ToUInt(GetConfig("resvTheme"))>0?"":"hidden" %>">
                            <td><span class="name_theme uni_trans"><%=Translate("主题")%></span></td>
                            <td>
                                <%if (GetConfig("fixTheme") == "1")
                                  { %>
                                <select name="test_name" class="form-control con_theme <%=ToUInt(GetConfig("resvTheme"))==2?"must":"" %>">
                                    <%=themeOptions %>
                                </select>
                                <%}
                                  else
                                  {%>
                                <input type="text" name="test_name" class="con_theme <%=ToUInt(GetConfig("resvTheme"))==2?"must":"" %>" data-msg="<%=Translate("必填内容不允许为空") %>" style="width: 233px;" maxlength="32" />
                                <%} %>
                            </td>
                        </tr>
                        <tr class="md_group">
                            <td><span class="uni_trans"><%=Translate("成员")%></span></td>
                            <td class="dlg_mb_panel"></td>
                        </tr>
                    </tbody>
                    <tbody class="dlg_dt_panel">
                    </tbody>
                    <tbody>
                        <tr class="resv_fee_panel <%=GetConfig("resvBilling")!="1"?"hidden":"" %>">
                            <td><span class="uni_trans">费用</span></td>
                            <td><span class="uni_trans">单价：</span><span class="unit_price"></span>；
                                <span class="uni_trans">总计：</span><span class="total_price"></span></td>
                        </tr>
                        <tr class="file_up_panel" style="font-size: 12px;">
                            <td><span class="uni_trans">申请报告</span></td>
                            <td>
                                <div style="text-decoration: underline; line-height: 20px; display: <%=dload==""?"none":""%>"><a href="<%=dload %>"><span class="uni_trans">下载申请报告模版</span></a><span class="red">*</span></div>
                                <div style="overflow: hidden;" class="up_file_panel">
                                    <div class="choice_file_panel"></div>
                                    <div class="btn-group">
                                        <button type="button" class="upload_file btn btn-info"><span class="uni_trans"><%=Translate("上传")%></span></button><input type='hidden' name='up_file' />
                                        <button type="button" class="cur_file_name btn btn-default" style="max-width: 230px;" disabled></button>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr class="date_flag <%=(ToUInt(GetConfig("resvMemo"))&3)>0?"":"hidden" %>">
                            <td><span class="uni_trans"><%=Translate("申请说明")%></span></td>
                            <td>
                                <div class="uni_trans" style="line-height: 18px;"><%=Translate(GetConfig("memoTip")) %>(45)</div>
                                <textarea rows="4" name="memo" style="width: 260px; line-height: 20px;" placeholder="<%=Translate(GetConfig("placeholder"))%>" class="memo  <%=(ToUInt(GetConfig("resvMemo"))&2)>0?"must":"" %>" data-msg="<%=Translate("申请说明必须填写") %>" maxlength="45"></textarea>
                            </td>
                        </tr>
                        <%if ((ToUInt(GetConfig("resvMemo")) & 4) > 0)
                          {%>
                        <tr>
                            <td><span class="uni_trans"><%=Translate("附加条目")%></span></td>
                            <td>
                                <div class="spare_items">
                                </div>
                                <input type="hidden" class="spare_con" name="memo" />
                            </td>
                        </tr>
                        <%} %>
                    </tbody>
                </table>
            </div>
            <div class="submitarea">
                <input type="button" class="btn btn-info mt_sub_resv" value="<%=Translate("提交") %>" />
                <input type="button" class="dlg_close btn btn-default" value="<%=Translate("返回") %>" />
            </div>
        </form>
    </div>
</div>
<script>
    (function () {
        var calendar_para = {};//calendar私有公共参数
        if (typeof (uni_calendar_dft_opt) != "object") uni_calendar_dft_opt = {};//外部配置
        var idlg = $("#dlg_resv_panel_default_<%=id%>");//公共dlg对象
        $(function () {
            var disable = "<%=Disable%>";
            if (disable == "true") return;//停止初始化
            //解析参数
            var req = uni.getReq();
            var purl = uni.getObj(uni_calendar_dft_opt);
            //过滤掉函数
            for (var key in purl) {
                if (typeof (purl[key]) == "function") purl[key] = null;
            }
            //
            calendar_para.src_type = "<%=SrcType%>";
            calendar_para.classkind = purl.classkind = req["classKind"] || "<%=ClassKind%>" || purl.classkind;
            calendar_para.test_id = purl.test_id = req["testId"] || purl.test_id;//教学实验项目号
            calendar_para.term = purl.term = req["term"] || purl.term;//教学学期
            calendar_para.display = purl.display = req["display"] || purl.display || "<%=DisplayMode%>";
            purl.islong = req["isLong"] || "<%=IsLong%>" || purl.islong;
            var alone = req["alone"] || "<%=Alone%>";
            if (alone == "true") purl.alone = true;
            purl.iskind = req["isKind"] || "<%=IsKind%>" || purl.iskind;
            purl.md = purl.md || ((purl.islong || "").toLowerCase() == "true" ? "m" : "<%=Mode%>");
            purl.class_id = req["classId"] || "<%=DevClassId%>" || purl.class_id;
            purl.lab_id = req["labId"] || "<%=LabId%>" || purl.lab_id;
            purl.kind_id = req["kindId"] || "<%=KindId%>" || purl.kind_id;
            purl.room_id = req["roomId"] || "<%=RoomId%>" || purl.room_id;
            purl.dev_id = req["dev"] || "<%=Dev%>" || purl.dev_id;
            purl.operate = req["operate"] || purl.operate;
            purl.purpose = purl.purpose || "<%=Purpose%>";
            var selDate = req["click"] == "false" ? undefined : SelDateTime;
            var width = parseInt(req["w"] || "<%=Width%>" || "840");
            purl.img = req["img"] || "<%=Img%>" || purl.img;
            purl.cld_name = purl.cld_name || "<%=Name%>";//状态控件名
            //获取实例
            var panel = $("#calendar_<%=id %>");
            panel.attr("name", purl.cld_name);
            var calendar;
            //载入状态控件
            function loadCld(filter) {
                debugger
                var kvp = uni.getObj(purl);
                if (filter && typeof (filter) == "object") $.extend(true, kvp, filter);
                //不需要传的值
                delete kvp.img;
                //
                var vconfigInterBase= "<%=GetConfig("resvInterval")%>";
                var vconfigRes=parseInt("<%=GetConfig("resvInterval")%>" || 10)
                var selectTimeRes=false;
                if(vconfigInterBase==0)
                {
                    var vEndTime=20*60;
                    var dateNow= new Date();
                    dateNow=timeStep(dateNow);
                    debugger;
                    vconfigInterBase=vEndTime-dateNow.getHours()*60-dateNow.getMinutes();
                    selectTimeRes=true;
                }
                
                if (kvp.display == "fp") {//平面图
                   
                    calendar = panel.uniFloorPlan({
                        width: width,
                        img: purl.img,
                        allDay: (parseInt("<%=GetConfig("allDayState")%>") & parseInt(calendar_para.classkind))>0,
                        step: parseInt("<%=GetConfig("resvTimeUnit")%>" || 10),
                        interval:vconfigInterBase,
                        isEdit: (pro.isLogin() && (parseInt(pro.acc.ident) & 268435456) > 0),
                        selectTime:selectTimeRes,
                        evInit: function (callback) {
                            pro.j.dev.getDevCoord(kvp, function (rlt) {
                                callback(rlt);
                            });
                        },
                        evSelDot: selDate,
                        evUpTime: function (date, start, end, callback) {
                            var pra = kvp || {};
                            pra.date = date;
                            if (start && end) {
                                pra.fr_start = start;
                                pra.fr_end = end;
                            }
                            else {
                                pra.fr_all_day="true";
                            }
                            pro.j.dev.getRsvSta(pra, function (rlt) {
                                var list = rlt.data;
                                callback(list, "unilab3", { showClose: ("<%=GetConfig("showClose")%>" == "1" ? true : false) });
                            });
                        },
                        evSaveCoorb: function (para, boxs, callback) {
                            var str = para.width + "&" + para.height + "&" + para.istitle;
                            var list = boxs.values();
                            $.each(list, function () {
                                str += "&" + this.key + "," + this.css("top") + "," + this.css("left") + "," + this.sz;
                            });
                            pro.j.dev.setDevCoord(kvp, str, callback);
                        },
                        evFinish: function (fp) {
                            var search = $(fp).find(".fp-user-search");
                            $(fp).find(".fp-date").attr("readonly", "true").datepicker({ minDate: 0 });
                                $(fp).find(".fp-time-start,.fp-time-end").attr("readonly", "true").timepicker({
                                    controlType: 'select',
                                    timeFormat: "HH:mm",
                                    onClose: function () { search.trigger("click"); },
                                    stepHour: 1,
                                    stepMinute: 10,
                                    hourMin: 6,
                                    hourMax: 23
                                });
                            }
                    });
                }
                else {//状态列表
                    calendar = panel.uniCalendar({               
                        mode: kvp.md,
                        modes: kvp.md,
                        style: kvp.style || "dft",
                        alone: kvp.alone || false,
                        operate: kvp.operate || 'drag',
                        width: width,
                        objTitleMinWidth: 100,
                        pctrl: { num: 20, triggerHeight: kvp.triggerHeight || 120 },//定义即启用
                        evSelTime: selDate,
                        dayOpt: {
                            unit: parseInt("<%=GetConfig("resvTimeUnit")%>" || 10),
                            evSelObj: function (data) {
                                if (pro.calendar && pro.calendar.selObjFun) {//不能定义在hrload页面内
                                    pro.calendar.selObjFun(data.obj);
                                }
                            }
                        },
                        evUpTime: function (date, callback,options) {
                            var pra = kvp || {};
                            pra.date = date.format("yyyyMMdd");
                            //if (kvp.islong == "true")
                            //pra.ck_close = "true";//检查开放区间不开放日期 耗资源
                            pro.j.dev.getRsvSta(pra, function (rlt) {
                                var list = rlt.data;
                                if (list.length > 0) {
                                    if ((list[0].limit & 1024) > 0) {//预约不检测冲突
                                        options.dayOpt.occupy = false;//时间条不覆盖全部
                                    }
                                }
                                //if(list.length==0) uni.msgBox("没有获取到数据");
                                callback(list, "unilab3", { byType: kvp.byType || "lab", showClose: ("<%=GetConfig("showClose")%>" == "1" ? true : false) });
                            });
                        },
                        evFinishDraw: function (dt, date, opt, iqz, more) {
                            if (uni_calendar_dft_opt.finishDraw) uni_calendar_dft_opt.finishDraw(date, opt, iqz, more);
                        }
                    });
                }
            }
            //默认自动加载
            if (purl.auto_load != "false") loadCld();
            //记录对象
            if (pro.calendar) {
                if (!pro.calendar.instants) pro.calendar.instants = {};
                var cld = {};
                cld.type = purl.display;
                cld.instant = calendar;
                cld.reload = loadCld;
                pro.calendar.instants[purl.cld_name] = cld;
            }
            //点击刷新
            if (purl.display == "cld") {
                $(".resv_stat").click(function () {
                    if (calendar)
                        calendar.uploadCld();
                });
            }
        });
        function timeStep(dt) {
            var t = dt.getMinutes();
            var diff=t % 10;
            if (diff > 0) {
                dt.addMinutes(10-diff);
            }
            return dt;
        }
        function SelDateTime(data) {
            if (!pro.isloginL(function () { SelDateTime(data) })) return;
            var k = "<%=GetConfig("needToKnow")%>";//预约须知
            if (k == "" || k == "0")
                Show();
            else {
                var para = {};//通用
                var obj = data.obj;
                if (k == "2") {
                    var prefix = "";
                    var forSub = parseInt("<%=GetConfig("editForSubsys")%>");
                    if (obj.clskind=="2" && (forSub & 2) > 0) prefix = "cpt";
                    else if (obj.clskind =="8" && (forSub & 8) > 0) prefix = "seat";
                    if (calendar_para.src_type == "rm") {
                        para.type = "rm_"+prefix+"rule";//房间
                        para.id = obj.roomId;
                    }
                    else if (calendar_para.src_type == "kind") {
                        para.type = "kind_" + prefix + "rule";//类型
                        para.id = obj.kindId;
                    }
                    else if (calendar_para.src_type == "dev") {
                        para.type = "dev_" + prefix + "rule";//设备
                        para.id = obj.devId;
                    }
                    else if (obj.classId) {
                        para.type = prefix+"rule";//类别
                        para.id = obj.classId;
                    }
                }
                pro.d.other.resvNotice(null, para, function (dlg) {
                    if (dlg.agree) {
                        Show();
                    }
                });
            }
            function Show() {//外层定义检查回调函数
                if (uni_calendar_dft_opt.cusPrepare) {
                    uni_calendar_dft_opt.cusPrepare(data, ShowForm);
                }
                else {
                    ShowForm(data);
                }
            }
        }
        function ShowForm(data) {
            var md = data.md;
            var obj = data.obj;
            var dlg = idlg;//$(idlg.html());
            //初始化备选项
            if ((parseInt("<%=GetConfig("resvMemo")%>" || 0) & 4) > 0) {
                pro.j.util.getCodeTbl(9, '', function (rlt) {//服务=9
                    var arr = rlt.data;
                    var str = "";
                    for (var i = 0; i < arr.length; i++) {
                        str += "<span class='spare_item'>" + arr[i].szCodeName + "</span>";
                    }
                    $(".spare_items", dlg).html(str);
                    var spare = $(".spare_item", dlg);
                    var memo = $(".spare_con", dlg);
                    spare.click(function () {
                        var pthis = $(this);
                        var v = pthis.html() + ',';
                        if (pthis.hasClass("selected")) {
                            pthis.removeClass("selected");
                            var ht = memo.val();
                            memo.val(ht.replace(v, ''));
                        }
                        else {
                            pthis.addClass("selected");
                            memo.val(v + memo.val());
                        }
                    })
                });
            }
            //初始化时间  
            obj.date = data.dt;
            if (data.startT) {
                obj.startDate = data.startD;
                obj.endDate = data.endD;
                obj.start = data.startT;
                obj.end = data.endT;
            }
            else {
                obj.start = data.start;
                obj.end = data.end;
            }
            //教师排课
            if ("<%=GetConfig("resvSchedule")%>" == "1" && (parseInt(pro.acc.ident) & 512) > 0) {
                obj.cycleDate = true;
                $(".prop", dlg).val(8);//lock_room属性=8 custom
                //$(".tr_theme", dlg).removeClass("hidden");//是否需要/必须填写课题 交由resvTheme配置项确定
                //$(".con_theme", dlg).addClass("must");
                //$(".name_theme", dlg).html("<span class='red'>*</span><%=Translate("课程")%>");
            }
            else
                $(".prop", dlg).val("");
            //主题显示检测
            var clsk = parseInt("<%=GetConfig("resvThemeClsKind")%>");
            if (clsk) {
                if ((clsk & (calendar_para.classkind || 0)) == 0) {
                    $(".tr_theme", dlg).addClass("hidden");
                }
            }
            //教学实验项目
            if (calendar_para.test_id && calendar_para.term) {
                $(".test_id", dlg).val(calendar_para.test_id);
                $(".term", dlg).val(calendar_para.term);
            }
            //按类型与否赋值
            var tmp = obj.id.split("_");//20150610前为&
            var id = tmp[0];
            $(".lab_id", dlg).val(tmp[1]);
            $(".type", dlg).val(obj.type || "dev");
            debugger;
            if (obj.type == "kind") {
                $(".kind_id", dlg).val(id);
                $(".room_id", dlg).val(tmp[2]||"");
            }
            else {
                $(".kind_id", dlg).val(obj.typeId);
                $(".dev_id", dlg).val(id);
            }
            //按长期与否分别初始化
            if (obj.islong) {
                //异步获取开放时间
                if (obj.type == "kind")
                    pro.j.dev.getDevKindRsvSta(id, obj.date, function (rlt) {
                        obj.open = rlt.data.open;
                        openResvDlg(dlg, obj);
                    });
                else
                    pro.j.dev.getDevRsvSta(id, obj.date, function (rlt) {
                        obj.open = rlt.data.open;
                        openResvDlg(dlg, obj);
                    });
            }
            else {
                openResvDlg(dlg, obj);
            }
            //注册提交事件
            var sub_btn = $(".mt_sub_resv", dlg);
            if (!sub_btn.hasClass("inited")) {
                sub_btn.addClass("inited");
                sub_btn.click(function () {
                    debugger
                    //如果显示主题
                    if (clsk) {
                        //如果可输入主题
                        if (!parseInt("<%=GetConfig("fixTheme")%>")) {
                            //如果主题为空
                            var value = $(".con_theme").val(); // 获取值
                            value = $.trim(value); // 用jQuery的trim方法删除前后空格
                            if (value == ''||value==null) {// 判断是否是空字符串，而不是null
                                uni.msgBox("输入主题不能为空或空格!");
                                return false;
                            }
                        }
                    }
                    if (dlg.mustItem())
                        subUserResv(this, obj);
                });
            }
        }
        function openResvDlg(dlg, obj) {
            if (obj.islong) {
                var open = obj.open;
                if (open && open.length > 1) {
                    obj.openStart = open[0];
                    obj.openEnd = open[1];
                }
                else {
                    uni.msgBox("所选日期不开放");
                    return;
                }
            }
            //固定时间段
            if(obj.ops&&obj.ops.length>0&&(obj.ops[0].limit&2)>0){
                obj.fix = true;
                //{<%=GetConfig("fixTimeSpan")%> };
            }
            pro.d.basic.addDateTimePicker($(".dlg_dt_panel", dlg), obj);
            //初始化状态条
            if ("<%=GetConfig("supStateSlider")%>" == "1" && obj.allowLong == false) {
                $(".rsv_state_slider", dlg).stateSlider(obj, { start: $(".md_date .mt_start_time", dlg), end: $(".md_date .mt_end_time", dlg), width: 410 });
            }
            //成员添加
            if (parseInt(obj.maxUser) < 2) $(".md_group", dlg).hide();
            else {
                $(".md_group", dlg).show();

                var mb_panel = $(".dlg_mb_panel", dlg);
                if ($.trim(mb_panel.html()) == "") {
                    var para = { md: "simple" };
                    if (calendar_para.test_id) {//教学组
                        para.md = "complex";
                        para.test_id = calendar_para.test_id;
                    }
                    else {//普通组
                        if (obj.maxUser > 30) para.md = "complex";
                        para.min = obj.minUser;
                        para.max = obj.maxUser;
                    }
                    pro.d.basic.mGroupMembers(mb_panel, para);
                }
            }
            //上传文件
            if ((parseInt(obj.limit) & 8) > 0 || ("<%=GetConfig("showResvAttach")%>" == "1" && (parseInt(calendar_para.classkind || 0) & 1) > 0)) //limit=8必须上传附件 研修间类型可上传
                var upFile = $(".upload_file", dlg).uploadFile();
            else
                $(".file_up_panel", dlg).hide();
            //规则详细
            $(".div_remark", dlg).html(pro.htm.getResvRule(obj));
            //参数
            $(".rsv_obj_name", dlg).html(uni.backText(obj.title));
            $(".apply_people", dlg).html(pro.acc.name);
            //打开窗口
            //$(".mt_sub_resv", dlg).removeAttr("disabled");
            uni.dlg(dlg, "<%=Translate("预约申请") %>", 460, 200);
        }
        //提交预约
        function subUserResv(btn, obj) {

            btn = $(btn);
            debugger;
            uni.translate("2017-08-23最少需3人同时使用");
            uni.translate("2017-08-23最少需3人同时使用abc");
            
            var dlg = btn.parents(".dialog:first");
            //btn.attr({ "disabled": "disabled" });
            pro.j.rsv.fRsv("set_resv", $("form:first", dlg), function () {
                if (uni_calendar_dft_opt.submitSuc) {
                    uni_calendar_dft_opt.submitSuc(dlg, obj);
                }
                else {
                    var msg = '<%=Translate("申请提交成功，是否跳转查看预约信息?")%>';
                    if (parseInt(obj.minUser) > 1) {
                        msg += "<br/><br/><div style='font-weight:bold;'>" + uni.translate("注意！生效后需至少") + "<span style='color:red;font-size:bold;'>" + obj.minUser + "</span>" + uni.translate("人刷卡，否则将记为违约！") + "</div>";
                    }
                    uni.confirm(msg, function () {
                        $("#user_center").trigger("click");
                    }, function () {
                        uni.reload();
                    });
                    $(".group_id", dlg).val("");
                    $(".group_name", dlg).html("<%=Translate("小组未创建")%>");
                    dlg.dialog("close");
                }
                //btn.removeAttr("disabled");
            }, function (rlt) {
                
                uni.msgBox(uni.translate(rlt.msg));
                //btn.removeAttr("disabled");
            }, function () {
                uni.msgBox(uni.translate("异步连接出现异常！"));
                //btn.removeAttr("disabled");
            });
        }
    })()
</script>
<div id="calendar_<%=id %>" name="" onselectstart="return false"></div>
