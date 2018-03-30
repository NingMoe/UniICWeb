<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dlg_resv.ascx.cs" Inherits="ClientWeb_pro_net_dlg_resv" %>
<div id="dlg_resv_opts">
    <!-- 修改预约时间 -->
    <div id="dlg_resv_alter" class="dialog">
        <div class="div_remark remark">
        </div>
        <div>
            <div class="rsv_state_slider" style="margin:0 5px;"></div>
        </div>
        <form onsubmit="return false;" class="list">

            <div>
                <input type="hidden" name="resv_id" class="resv_id" />
                <table>
                    <tbody>
                        <tr>
                            <td><span class="uni_trans">预约对象</span></td>
                            <td><span class="rsv_obj_name"></span></td>
                        </tr>
                    </tbody>
                    <tbody class="dlg_dt_panel">
                    </tbody>
                </table>
            </div>
            <div class="submitarea">
                <input type="button" class="btn btn-info mt_sub_resv" value="<%=Translate("提交") %>"/>
                <input type="button" class="dlg_close btn btn-default" value="<%=Translate("返回") %>"/>
            </div>
        </form>
    </div>
</div>
<script>
    pro.d.resv.alterTime = function (devId, devKind, resvId, start, end, suc) {
        var dlg = $("#dlg_resv_alter");
        $(".resv_id", dlg).val(resvId);
        var start = uni.parseDate(start),
        date = start.format("yyyy-MM-dd"),
        end = uni.parseDate(end);
        if (devId&&devId!="0")
            pro.j.dev.getDevRsvSta(devId, date, function (rlt) {
                OpenDlg(rlt.data);
            });
        else if (devKind && devKind != "0") {
            pro.j.dev.getDevKindRsvSta(devKind, date, function (rlt) {
                OpenDlg(rlt.data);
            });
        }
        else
            uni.msgBox("参数有误");
            
        function OpenDlg(obj) {
            obj.alter = true;
            //时间
            obj.startDate = obj.date = start.format("yyyy-MM-dd");
            obj.start = start.format("HH:mm");
            obj.endDate = end.format("yyyy-MM-dd");
            obj.end = end.format("HH:mm");
            var open = obj.open;
            if (open && open.length > 1) {
                obj.openStart = open[0];
                obj.openEnd = open[1];
            }
            else {
                uni.msgBox("获取不到开放时间");
                return;
            }
            pro.d.basic.addDateTimePicker($(".dlg_dt_panel", dlg), obj);
            //规则详细
            $(".div_remark", dlg).html(pro.htm.getResvRule(obj));
            //参数
            $(".rsv_obj_name", dlg).html(uni.backText(obj.title));
            //注册提交
            $(".mt_sub_resv", dlg).removeAttr("disabled").click(function () {
                btn = $(this);
                btn.attr({ "disabled": "disabled" });
                pro.j.rsv.fRsv("set_resv", $("form:first", dlg), function () {
                    uni.msgBox("预约修改成功", "", suc || function () { uni.reload();});
                    btn.removeAttr("disabled");
                    dlg.dialog("close");
                }, function (rlt) {
                    uni.msgBox(rlt.msg);
                    btn.removeAttr("disabled");
                }, function () {
                    uni.msgBox("异步连接出现异常！");
                    btn.removeAttr("disabled");
                });
            });
            //打开窗口
            uni.dlg(dlg, "预约修改", 430, 200);
            //初始化状态条
            if(obj.allowLong==false)
            $(".rsv_state_slider", dlg).stateSlider(obj, { start: $(".md_date .mt_start_time", dlg), end: $(".md_date .mt_end_time", dlg),width:400 });
        }
    }
</script>
