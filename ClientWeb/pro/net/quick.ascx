<%@ Control Language="C#" AutoEventWireup="true" CodeFile="quick.ascx.cs" Inherits="ClientWeb_xcus_ic2_client_jlcj_quick" %>
<style>
    .resv_interval_panel { padding: 3px; border: 1px solid #ddd; border-radius: 3px; width: 260px; }
    .resv_interval_panel label {line-height: 20px;margin: 0 6px;cursor:pointer; }
</style>
<script>
    $(function () {
        var btn = $(".quick_resv_btn");
        var dlg = $(".quick_resv_dialog");
        //提交
        btn.click(function () {
            var pthis=$(this);            
            var p={subsys:pthis.attr("subsys"),title:pthis.attr("title")};
            if(!pro.isloginL({callback:quickResv,para:p}))return;
            quickResv({para:p});
        });
        function quickResv(e){
            //参数
            var para=e.para||{};
            var sys=parseInt(para.subsys||0);
            var allow=parseInt("<%=ToUInt(GetConfig("resvThemeClsKind"))%>");
            if(allow&&(sys&allow)==0){//不显示主题
                $(".tr_theme",dlg).hide();
            }
            $(".sel_room option",dlg).each(function(){
                var pthis=$(this);
                if(pthis.attr("clskind")==sys)pthis.show();
                else pthis.hide();
            });
            uni.dlg(dlg, para.title||"快速预约", 400, 300, function () {
                if(dlg.mustItem({clsMsg:true})){
                    var f=dlg.find("form").getFormJson();
                    pro.j.dev.getRsvSta(f,function(rlt){
                        var obj;
                        for (var i = 0; i < rlt.data.length; i++) {
                            var dev=rlt.data[i];
                            if(dev.freeSta==0&&!dev.state){
                                obj=dev;
                                break;
                            }
                        }
                        if(obj){
                            f.act="set_resv";
                            f.dev_id=obj.devId;
                            f.start=f.date+" "+f.fr_start;
                            f.end=f.date+" "+f.fr_end;
                            pro.j.rsv.rsvAct(f,function(){
                                var msg="预约对象：<strong style='font-size:14px;'>"+obj.labName+" - "+obj.roomName+" - "+obj.devName+"</strong>；申请已提交，是否跳转查看预约信息？";
                                if(parseInt(obj.minUser)>1){
                                    msg+="<br/><br/><div style='font-weight:bold;'>"+uni.translate("注意！生效后需至少")+"<span style='color:red;font-size:bold;'>"+obj.minUser+"</span>"+uni.translate("人刷卡，否则将记为违约！")+"</div>";
                                }
                                uni.confirm(msg, function () {
                                    $("#user_center").trigger("click");
                                });
                                dlg.dialog("close");
                                //清理
                                dlg.find("input[type=text],textarea,select").val('');
                                dlg.find(".sel_lab").trigger('change');

                            });
                        }
                        else{
                            uni.msgBox("所选位置没有符合当前时间条件的空闲设备");
                        }
                    });
                }
            });
        }
        //实验室房间过滤
        $(".sel_lab", dlg).change(function () {
            var id = $(this).val();
            $(".sel_room").val('');
            $(".sel_room .it").hide();
            if (id) {
                $(".sel_room .it.lab_" + id).show();
            }
        });
        //时间控件
        $(".input_date", dlg).datepicker({ minDate: 0 });
        $(".input_time", dlg).timepicker({
            controlType: 'select',
            timeFormat: "HH:mm",
            stepHour: 1,
            stepMinute: parseInt("<%=GetConfig("resvTimeUnit")%>" || 10),
            hourMin: 6,
            hourMax: 23
        });
    });
</script>
<div class="quick_resv_dialog dialog">
    <div class="tag">* 房间非必选项，设备将自动分配。</div>
    <form onsubmit="return false;" class="list">
        <table>
            <tbody>
                <tr class="tr_theme <%=ToUInt(GetConfig("resvTheme"))>0?"":"hidden" %>">
                    <td><span class="name_theme uni_trans">主题</span></td>
                    <td>
                        <%if (GetConfig("fixTheme") == "1")
                          { %>
                        <select name="test_name" class="form-control con_theme <%=ToUInt(GetConfig("resvTheme"))==2?"must":"" %>">
                            <option value="">未选择</option>
                            <%=options %>
                        </select>
                        <%}
                          else
                          {%>
                        <input type="text" name="test_name" class="con_theme <%=ToUInt(GetConfig("resvTheme"))==2?"must":"" %>" data-msg="<%=Translate("必填内容不允许为空") %>" style="width: 233px;" maxlength="32" />
                        <%} %>
                    </td>
                </tr>
                <tr>
                    <td>预约日期</td>
                    <td>
                        <input type="text" class="input_date resv_date must" name="date" readonly />
                    </td>
                </tr>
                <tr>
                    <td>预约时长</td>
                    <td>
                        <div class="resv_interval_panel">
                            <label>
                                <input type="radio" name="resv_interval" class="resv_interval" value="30" />半小时</label>
                            <label>
                                <input type="radio" name="resv_interval" class="resv_interval" value="60" />1小时</label>
                            <label>
                                <input type="radio" name="resv_interval" class="resv_interval" value="120" />2小时</label>
                                                        <label>
                                <input type="radio" name="resv_interval" class="resv_interval" value="30" />半小时</label>
                            <label>
                                <input type="radio" name="resv_interval" class="resv_interval" value="60" />1小时</label>
                            <label>
                                <input type="radio" name="resv_interval" class="resv_interval" value="120" />2小时</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>时间区间</td>
                    <td>
                        <input type="text" class="input_time tm_start must" name="fr_start" readonly style="width: 60px;" />
                        -
                        <input type="text" class="input_time tm_end must" name="fr_end" readonly style="width: 60px;" />
                    </td>
                </tr>
                <tr>
                    <td>位置</td>
                    <td>
                        <select class="sel_room form-control" name="room_id">
                            <%=selRoom %>
                        </select>
                    </td>
                </tr>
                <tr class="date_flag <%=ToUInt(GetConfig("resvMemo"))>0?"":"hidden" %>">
                    <td><span class="uni_trans">申请说明</span></td>
                    <td>
                        <div class="uni_trans" style="line-height: 18px;"><%=GetConfig("memoTip") %>(45)</div>
                        <textarea rows="4" name="memo" style="width: 260px; line-height: 20px;" class="memo  <%=ToUInt(GetConfig("resvMemo"))==2?"must":"" %>" data-msg="<%=Translate("申请说明必须填写") %>" maxlength="45"></textarea></td>
                </tr>
            </tbody>
        </table>
    </form>
</div>
