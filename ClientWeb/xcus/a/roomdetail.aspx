<%@ Page Language="C#" AutoEventWireup="true" CodeFile="roomdetail.aspx.cs" Inherits="ClientWeb_xcus_all_roomdetail" %>
<%@ Register TagPrefix="Uni" TagName="calendar" Src="~/ClientWeb/pro/net/calendar.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <script>
        var uni_calendar_dft_opt = {
            byType: ("<%=GetConfig("resvSchedule")%>" == "1" && (parseInt(pro.acc.ident) & 512) > 0) ? "volume" : "devcls"};
    </script>
    <form runat="server">
        <input runat="server" type="hidden" class="room_id" name="room_id" id="roomId" />
        <input runat="server" type="hidden" class="room_name" name="room_name" id="roomName" />
    </form>
    <div>
        <div>
            <h1 class="h_title"><%=infoTitle %></h1>
            <div style="margin:10px;margin-bottom:20px;overflow:hidden;">
                <%=infoIntro %>
            </div>
        </div>
        <div class="info_unitab">
            <ul class="tab_head">
                <li style="display:<%=noResv%>">
                    <div class="title"><%=Translate("预约状态") %></div>
                    <div class="caret"></div>
                </li>
                <li style="display:none">
                    <div class="title"><%=Translate("预约须知") %></div>
                    <div class="caret"></div>
                </li>
              
                <li>
                    <div class="title"><%=Translate("相册展示") %></div>
                    <div class="caret"></div>
                </li>
                  <li>
                    <div class="title"><%=Translate("硬件配置") %></div>
                    <div class="caret"></div>
                </li>
                <%if (1 == 1) {%>
                   <li>
                    <div class="title"><%=Translate("座位查询") %></div>
                    <div class="caret"></div>
                </li>
                   <% } %>
            </ul>
            <div class="tab_con">
                <div class="cld-obj-detail">
                        <Uni:calendar runat="server" ID="Cld"/>
                </div>
                                <div>
                    <%=infoRule %>
                </div>
             
            <div class="rm_album">
                <div class="img_large">
                    <%=szPicZoom %>
                </div>
                <div class="img_thumb">
                    <ul class="clear">
                        <%=szPicPath %>
                    </ul>
                </div>
            </div>
                   <div>
                    <%=infoHard %>
                </div>
                   <div class="content">
            <div style="width:99%;margin-top:15px">
                <div>
                   <table id="tbSearch" style="margin:0 auto;border:0px solid #d1c1c1;width:90%"> 
                       <tbody style="padding:0px;">
                       <tr>
                        <td style="width:50px">座位名</td>
                    <td>
                             <input type="text" placeholder="请输入座位名" id="DevName"/>
                        </td>
                           <td>
                            <input type="button" value="查询" runat="server" id="Selectbtn"/>
                           </td>
                           </tr>
                           </tbody>        </table>
                </div>
            <div style="margin-top:10px;text-align:center; MARGIN-RIGHT: auto;height:350px; overflow-y:hidden;" id="divResvStatue">
                  <iframe id="iframe" style="width:100%;border:0px;height:99%">
                </iframe>
                </div>
                </div>
        </div>
                <script>
                    $(".rm_album").album();
                </script>
        </div>
        <script>
                $(".info_unitab").unitab();
        </script>
        <div id="dlg_group">
            <div id="form_default" class="resv_panel dialog">
                <form onsubmit="return false;" class="tbl">
                    <div class="tag hidden">
                        <div id="oth_info"></div></div>
                                        <div class="div_remark remark" id="div_remark">
                        本场馆当日开放时间：<span class="red open_t_start"></span> 到 <span class="red open_t_end"></span>；申请预约最长可提前：<span class="red rule_t_earliest"></span>
                        至少提前：<span class="red rule_t_latest"></span>；预约时间最长：<span class="red rule_t_max"></span> 最短：<span class="red rule_t_min"></span>。
                    </div>
                    <div>
                        <table style="width: 100%;">
                            <tbody>
                                <tr>
                                    <td colspan="4" style="text-align: left; padding: 5px;">
                                        <input type="hidden" name="dev_id" />
                                        <input type="hidden" name="type" value="dev" />
                                        <input type="hidden" value="10" name="level" />
                                        <input type="hidden" name="aty_id" class="aty_id" />
                                        <input type="hidden" name="ck_kind" class="ck_kind" />
                                        预约场馆：<span class="rsv_dev_name"></span> | 申请人：<span class="apply_people"></span></td>
                                </tr>
                                <tr>
                                    <td class="th">
                                        <div><span style="color: red;">*</span>活动名称</div>
                                    </td>
                                    <td style="width: 289px;">
                                        <input name="resv_name" type="text" class="ipt yard_name" /></td>
                                    <td class="th">
                                        <div>
                                            活动类型
                                        </div>
                                    </td>
                                    <td style="width: 260px;">
                                        <input type="text" disabled="disabled" class="aty_name" />
                                        <input type="hidden" name="aty_name" class="aty_name" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th">
                                        <div>活动团体</div>
                                    </td>
                                    <td>
                                        <input type="text" name="org" />
                                    </td>
                                    <td class="th">
                                        <div>
                                            负责人
                                        </div>
                                    </td>
                                    <td>
                                        <input type="text" name="orger" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th"><span style="color: red;">*</span>开始时间</td>
                                    <td>
                                        <input type="text" name="start" class="Wdate mt_begin_date" readonly="readonly" />
                                    </td>
                                    <td class="th">
                                        <div><span style="color: red;">*</span>结束时间</div>
                                    </td>
                                    <td>
                                        <input type="text" name="end" class="Wdate mt_end_date" readonly="readonly" /></td>
                                </tr>
<%--                                                               <tr>
                                    <td class="th"><span style="color: red;">*</span>开始时间</td>
                                    <td>
                                        <input type="text" name="start" class="Wdate mt_begin_date" readonly="readonly" />
                                    </td>
                                    <td class="th">
                                        <div><span style="color: red;">*</span>结束时间</div>
                                    </td>
                                    <td>
                                        <input type="text" name="end" class="Wdate mt_end_date" readonly="readonly" /></td>
                                </tr>--%>
                                <tr>
                                    <td class="th">申请说明</td>
                                    <td colspan="3">
                                        <textarea rows="6" cols="30" name="memo" style="width: 560px;" class="ipt memo"></textarea></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="submitarea">
                        <input type="button" class="btn btn-info" onclick="subDevResv(this)" value="提交" />
                        <input type="button" class="dlg_close btn btn-default" value="返回" />
                    </div>
                </form>
            </div>
        </div>
    <script>
        function subDevResv() {
            var cld = $("#form_default");
            //名称
            var mt = $(".yard_name", cld);
            var name = $.trim(mt.val());
            mt.val(name);
            if (!name) {
                uni.msgBox("活动名称不能为空！");
                return false;
            }
            //审核类型
            var ck = 0;
            ck += parseInt(($(".director_v", cld).val()) || 0);
            ck += parseInt(($(".devman_v", cld).val()) || 0);
            $(".service_v:checked", cld).each(function () {
                var v = parseInt($(this).val());
                if (!isNaN(v))
                    ck += v;
            });
            $(".ck_kind", cld).val(ck);
            var dts = $(".director:checked", cld);
            var value = 0;
            dts.each(function () {
                var v = parseInt($(this).val());
                if (!isNaN(v))
                    value += v;
            });
            if (value > 0) {
                $("input[name=director]", cld).val(value);
            }
            //组成员
            var mbs = "";
            $(".yard_memList input[name=memid]", cld).each(function () {
                mbs += $(this).val() + ",";
            });
            $("input[name=mb_list]", cld).val(mbs);
            $(".mt_sub_resv", cld).attr({ "disabled": "disabled" });
            pro.j.rsv.fRsv("set_yard_rsv", $("form", cld), function () {
                uni.confirm("申请提交成功，是否跳转到个人中心？", function () {
                    $("#user_center").trigger("click");
                }, function () {
                    uni.reload();
                });
                $(".mt_sub_resv", cld).removeAttr("disabled");
                cld.dialog("close");
            }, function (rlt) {
                uni.msgBox(rlt.msg);
                $(".mt_sub_resv", cld).removeAttr("disabled");
            }, function () {
                uni.msgBox("异步连接出现异常！");
                $(".mt_sub_resv", cld).removeAttr("disabled");
            });
        }
        function SelDateTime(data) {
            var dlg = $("#form_default");
            var md = data.md;
            var obj = data.obj;
            var devid = obj.devId;
            var devname = obj.name;
            var date = data.dt;
            //成员限制
            $(".min_user", dlg).html(obj.minUser);
            $(".max_user", dlg).html(obj.maxUser);
            //上传文件
            var upFile = $(".upload_file", dlg).uploadFile();
            //初始化时间
            $(".mt_begin_date", dlg).val(date + " 08:00");
            $(".mt_end_date", dlg).val(date + " 20:00");
            var start = obj.openStart.split(":");
            var end = obj.openEnd.split(":");
            var h_start = parseInt(start[0]);
            var h_end = parseInt(end[0]);
            var m_start = parseInt(start[1]);
            var m_end = parseInt(end[1]);
            var hour;
            if (md == "d")
                hour = parseInt(data.h);
            else if (md == "m")
                hour = h_start;
            var beginH = uni.num2Str(hour, 2) + ":00";
            var beginT = uni.num2Str(h_start, 2) + ":" + uni.num2Str(m_start, 2);
            var endH;
            var endT = uni.num2Str(h_end, 2) + ":" + uni.num2Str(m_end, 2);
            if (hour == h_end)
                endH = endT;
            else
                endH = uni.num2Str(hour + 1, 2) + ":00";
            $(".mt_begin_date", dlg).val(date + " " + beginH);
            $(".mt_end_date", dlg).val(date + " " + endH);
            //日期控件
            $(".Wdate").datetimepicker({
                controlType: 'select',
                timeFormat: "HH:mm",
                dateFormat: "yy-mm-dd",
                stepHour: 1,
                stepMinute: 1,
                hourMin: h_start,
                hourMax: h_end
            });

            var remark = $(".div_remark", dlg);
            $(".open_t_start", remark).html(beginT);
            $(".open_t_end", remark).html(endT);
            var earliest = obj.earliest ? pro.dt.m2dms(obj.earliest) : "不限制";
            var latest = obj.latest ? pro.dt.m2dms(obj.latest) : "不限制";
            var max = obj.max ? pro.dt.m2dms(obj.max) : "不限制";
            var min = obj.min ? pro.dt.m2dms(obj.min) : "不限制";
            $(".rule_t_earliest", remark).html(earliest);
            $(".rule_t_latest", remark).html(latest);
            $(".rule_t_max", remark).html(max);
            $(".rule_t_min", remark).html(min);
            $(".mt_sub_resv", dlg).removeAttr("disabled");
            $("input[name=dev_id]", dlg).val(devid);
            $('.rsv_dev_name', dlg).html(devname);
            //打开窗口
            uni.dlg(dlg, "设备预约", 760, 402);
            //dlg.modal({show:true});
        }

        $("#Selectbtn").click(function () {
            if ($("#DevName").val().trim() != "") {
                var path;
                var i = location.href.toLowerCase().indexOf("/clientweb/");
                if (i < 0) path = location.origin;
                else path = location.href.substring(0, i);
                var Devname = $("#DevName").val();
                var url = path + "/ClientWeb/xcus/a/selectdev.aspx?Devname=" + Devname + "&classKind=8&kindId=" + "";
                $("#iframe").attr("src",url);
            }
        })
    </script>
    </div>
</body>
</html>
