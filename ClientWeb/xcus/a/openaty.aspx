<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="openaty.aspx.cs" Inherits="ClientWeb_xcus_all_openaty" %>

<html>
<body>
    <style>
        .application th { text-align: left; width: 120px; }
        .application td { padding: 5px 0; }
        .application h4 { color: #2a6496; }
        .application .fieldset { margin-bottom: 10px; border: 1px dotted #ddd; padding: 10px; background-color: #f9f9f9; }
        .application .fieldset.f1 input { width: 400px; }
        .application .fieldset textarea { width: 600px; height: 100px; }
        .apply_rule { padding: 6px; line-height: 1.5em; text-align: left; color: #8a6d3b; background-color: #fcf8e3; border: 1px dotted #faebcc; }
        label.requirment { margin-left:15px;}
    </style>
    <script>
        $(function () {
            var panel = $(".application");
            var data = uni.hr.getPara();
            if (data) {
                //初始化
                var para = data.obj;
                pro.j.dev.getDevRsvSta(para.devId, data.dt, function (rlt) {
                    $(".apply_rule").html(pro.htm.getResvRule(rlt.data) + ("<br/>所在位置：<span class='red'>" + para.labName + "," + para.roomName + "</span>。"));
                });
                //$(".apply_rule").html(pro.htm.getResvRule(para) + ("<br/>所在位置：<span class='red'>" + para.labName + "," + para.roomName + "</span>。"));
                //时间
                $(".aty_date", panel).datepicker({
                    minDate: 0
                });
                $(".aty_time", panel).timepicker({
                    controlType: 'select',
                    timeFormat: "HH:mm",
                    stepHour: 1,
                    stepMinute: 5,
                    hourMin: 6,
                    hourMax: 23
                });
                $(".deadline,.publish,.begin_date", panel).val(data.dt);
                $(".start", panel).val(data.start);
                $(".end", panel).val(data.end);
                //联系信息
                $(".user", panel).val(pro.acc.name);
                $(".phone", panel).val(pro.acc.phone || "");
                $(".email", panel).val(pro.acc.email || "");
                //人数限制
                $(".min", panel).val(para.minUser);
                $(".max", panel).val(para.maxUser);
                $(".s_min", panel).html(para.minUser);
                $(".s_max", panel).html(para.maxUser);
                //组
                if (para.maxUser>1) {
                    var mb_panel = $(".dlg_mb_panel", panel);
                    var p = {};
                    p.md = "complex";
                    p.min = para.minUser;
                    p.max = para.maxUser;
                    pro.d.basic.mGroupMembers(mb_panel, p);
                }
                //设备信息
                $(".dev_id", panel).val(para.devId);
                $(".dev_name", panel).html(para.devName);
            }
            //上传
            $(".upload_file", panel).uploadFile();
            //提交事件
            $(".submit_openaty", panel).click(function () {
                var form = $("form:first", panel);
                if (panel.mustItem()) {
                    if ($(".datum_up_file", panel).val() == "") {
                        uni.msgBox("请上传申请材料");
                        return;
                    }
                    if (uni.compareDate(new Date(), $(".deadline").val()) >= 0) {
                        uni.msgBox("截止日期应该晚于今日");
                        return;
                    }
                    pro.j.rsv.fRsv("set_open_aty", form, function () {
                        uni.msgBox("已成功提交申请，请等待审核。", "", function () {
                            $("#user_center").trigger("click");
                        });
                    });
                }
            });
            uni.backTop();
        });
    </script>
    <div class="click btn_back" onclick="uni.hr.back();" style="display: <%=isBack%>"><span class="glyphicon glyphicon-chevron-left"></span>&nbsp;<span class="uni_trans">返回</span></div>
    <div class="application">
        <h1 class="h_title">活动申请&nbsp;&nbsp;&nbsp;<span class="dev_name"></span></h1>
        <div class="line"></div>
        <form onsubmit="return false;">
            <div>
                <input type="hidden" class="dev_id" name="dev_id" />
            </div>
            <div>
                <table>
                    <tr>
                        <th>申请须知
                        </th>
                        <td>
                            <div class="apply_rule"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <h4 class="h_title ">基本信息</h4>
            <div class="f1 fieldset">
                <table>
                    <%if((ToUInt(GetConfig("resvKind"))&1024)>0){ %>
                        <tr class="tr_theme">
                        <th>活动类型<span class="red"> *</span>
                        </th>
                            <td>
                                <select name="dwKind" class="form-control must" style="width:233px;">
                                    <option value="">未选择</option>
                                    <%=atyKinds %>
                                </select>
                            </td>
                        </tr>
                        <%} %>
                    <tr>
                        <th>活动名称<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text must" data-msg="请填写活动名称" name="szActivityPlanName" />
                        </td>
                    </tr>
                    <tr>
                        <th>主办单位<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text must" data-msg="请填写主办单位" name="szHostUnit" />
                        </td>
                    </tr>
                    <tr>
                        <th>承办单位
                        </th>
                        <td>
                            <input type="text" class="input_text" name="szOrganizer" />
                        </td>
                    </tr>
                    <tr>
                        <th>主持人
                        </th>
                        <td>
                            <input type="text" class="input_text" name="szPresenter" />
                        </td>
                    </tr>
                    <tr>
                        <th>参与者要求
                        </th>
                        <td>
                            <textarea name="szDesiredUser"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
            <h4 class="h_title ">联系信息</h4>
            <div class="f2 fieldset">
                <table>
                    <tr>
                        <th>联系人<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text user must" data-msg="请填写联系人" name="szContact" />
                        </td>
                    </tr>
                    <tr>
                        <th>联系电话<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text phone must" data-msg="请填写联系电话" name="szHandPhone" />
                        </td>
                    </tr>
                    <tr>
                        <th>电子邮箱<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text email must" data-msg="请填写电子邮箱" name="szEmail" />
                        </td>
                    </tr>
                </table>
            </div>
            <h4 class="h_title ">活动人数限制 <small>活动人数应限制在场地容量范围内：<span class="s_min red"></span>-<span class="s_max red"></span></small></h4>
            <div class="f3 fieldset">
                <table>
                    <tr>
                        <th>最小限制人数<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text min must" data-msg="请填入最小限制人数" data-reg="number" name="dwMinUsers" />
                        </td>
                    </tr>
                    <tr>
                        <th>最大限制人数<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text max must" data-msg="请填入最大限制人数" data-reg="number" name="dwMaxUsers" />
                        </td>
                    </tr>
                    <tr>
                        <th><span class="uni_trans">初始成员</span></th>
                        <td class="dlg_mb_panel"></td>
                    </tr>
                </table>
            </div>
            <h4 class="h_title ">活动时间安排</h4>
            <div class="f4 fieldset">
                <table>
                    <tr>
                        <th>申请加入截至日<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text aty_date deadline" name="dwEnrollDeadline" readonly="true" />
                            <span class="grey">(不含当日)</span>
                        </td>
                    </tr>
                    <tr>
                        <th>发布日期<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text aty_date publish" name="dwPublishDate" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <th>活动日期<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text aty_date begin_date" name="dwActivityDate" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <th>开始时间<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text aty_time start" name="dwBeginTime" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <th>结束时间<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text aty_time end" name="dwEndTime" readonly="true" />
                        </td>
                    </tr>
                </table>
            </div>
                        <h4 class="h_title ">活动配置</h4>
            <div class="f5 fieldset">
                  <table>
                    <tr>
                        <td>
                            <label class="click requirment"><input type="checkbox" name="require_seat" class="require_seat" value="true"/> 支持选座</label>
                            <label class="click requirment"><input type="checkbox" name="require_check_in" class="require_check_in" value="true"/> 对出席人员考勤</label>
                        </td>
                    </tr>
                </table>
            </div>
            <h4 class="h_title ">其它信息</h4>
            <div class="f6 fieldset">
                <table>
                    <tr>
                        <th>活动简介
                        </th>
                        <td>
                            <textarea name="szIntroInfo"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="fieldset">
                <table>
                    <tr>
                        <th>活动海报
                        </th>
                        <td>
                            <span class="text-primary">接受图片格式jpg,gif,png,bmp</span>
                            <div class="input-group" style="width: 100%;">
                                <div>
                                    <input type="hidden" class="up_file placard_up_file" name="szActivityPlanURL" />
                                    <input type="button" class="placard_file upload_file" value="上传" limit="jpg,gif,png,bmp" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="fieldset">
                <table>
                    <tr>
                        <th>申请材料<span class="red"> *</span>
                        </th>
                        <td>
                            <div>
                            <%if (kindurl != "")
                                {%>
                            <a target="_blank" href="../../../<%=kindurl %>"">点击下载申请模板</a>
                            <%} %>
                                </div>
                            <span class="text-primary">根据相关要求提交申请材料</span>
                            <div class="input-group" style="width: 100%;">
                                <div>
                                    <input type="hidden" class="up_file datum_up_file" name="szApplicationURL" />
                                    <input type="button" class="datum_file upload_file" value="上传" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="submitarea text-center">
                <input type="button" class="submit_openaty btn btn-info" value="提交" />
                <input type="button" class="btn btn-default" value="重置" onclick="uni.reload();" />
            </div>
        </form>
    </div>
</body>
</html>

