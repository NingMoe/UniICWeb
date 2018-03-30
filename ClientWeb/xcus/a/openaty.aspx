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
                //��ʼ��
                var para = data.obj;
                pro.j.dev.getDevRsvSta(para.devId, data.dt, function (rlt) {
                    $(".apply_rule").html(pro.htm.getResvRule(rlt.data) + ("<br/>����λ�ã�<span class='red'>" + para.labName + "," + para.roomName + "</span>��"));
                });
                //$(".apply_rule").html(pro.htm.getResvRule(para) + ("<br/>����λ�ã�<span class='red'>" + para.labName + "," + para.roomName + "</span>��"));
                //ʱ��
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
                //��ϵ��Ϣ
                $(".user", panel).val(pro.acc.name);
                $(".phone", panel).val(pro.acc.phone || "");
                $(".email", panel).val(pro.acc.email || "");
                //��������
                $(".min", panel).val(para.minUser);
                $(".max", panel).val(para.maxUser);
                $(".s_min", panel).html(para.minUser);
                $(".s_max", panel).html(para.maxUser);
                //��
                if (para.maxUser>1) {
                    var mb_panel = $(".dlg_mb_panel", panel);
                    var p = {};
                    p.md = "complex";
                    p.min = para.minUser;
                    p.max = para.maxUser;
                    pro.d.basic.mGroupMembers(mb_panel, p);
                }
                //�豸��Ϣ
                $(".dev_id", panel).val(para.devId);
                $(".dev_name", panel).html(para.devName);
            }
            //�ϴ�
            $(".upload_file", panel).uploadFile();
            //�ύ�¼�
            $(".submit_openaty", panel).click(function () {
                var form = $("form:first", panel);
                if (panel.mustItem()) {
                    if ($(".datum_up_file", panel).val() == "") {
                        uni.msgBox("���ϴ��������");
                        return;
                    }
                    if (uni.compareDate(new Date(), $(".deadline").val()) >= 0) {
                        uni.msgBox("��ֹ����Ӧ�����ڽ���");
                        return;
                    }
                    pro.j.rsv.fRsv("set_open_aty", form, function () {
                        uni.msgBox("�ѳɹ��ύ���룬��ȴ���ˡ�", "", function () {
                            $("#user_center").trigger("click");
                        });
                    });
                }
            });
            uni.backTop();
        });
    </script>
    <div class="click btn_back" onclick="uni.hr.back();" style="display: <%=isBack%>"><span class="glyphicon glyphicon-chevron-left"></span>&nbsp;<span class="uni_trans">����</span></div>
    <div class="application">
        <h1 class="h_title">�����&nbsp;&nbsp;&nbsp;<span class="dev_name"></span></h1>
        <div class="line"></div>
        <form onsubmit="return false;">
            <div>
                <input type="hidden" class="dev_id" name="dev_id" />
            </div>
            <div>
                <table>
                    <tr>
                        <th>������֪
                        </th>
                        <td>
                            <div class="apply_rule"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <h4 class="h_title ">������Ϣ</h4>
            <div class="f1 fieldset">
                <table>
                    <%if((ToUInt(GetConfig("resvKind"))&1024)>0){ %>
                        <tr class="tr_theme">
                        <th>�����<span class="red"> *</span>
                        </th>
                            <td>
                                <select name="dwKind" class="form-control must" style="width:233px;">
                                    <option value="">δѡ��</option>
                                    <%=atyKinds %>
                                </select>
                            </td>
                        </tr>
                        <%} %>
                    <tr>
                        <th>�����<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text must" data-msg="����д�����" name="szActivityPlanName" />
                        </td>
                    </tr>
                    <tr>
                        <th>���쵥λ<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text must" data-msg="����д���쵥λ" name="szHostUnit" />
                        </td>
                    </tr>
                    <tr>
                        <th>�а쵥λ
                        </th>
                        <td>
                            <input type="text" class="input_text" name="szOrganizer" />
                        </td>
                    </tr>
                    <tr>
                        <th>������
                        </th>
                        <td>
                            <input type="text" class="input_text" name="szPresenter" />
                        </td>
                    </tr>
                    <tr>
                        <th>������Ҫ��
                        </th>
                        <td>
                            <textarea name="szDesiredUser"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
            <h4 class="h_title ">��ϵ��Ϣ</h4>
            <div class="f2 fieldset">
                <table>
                    <tr>
                        <th>��ϵ��<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text user must" data-msg="����д��ϵ��" name="szContact" />
                        </td>
                    </tr>
                    <tr>
                        <th>��ϵ�绰<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text phone must" data-msg="����д��ϵ�绰" name="szHandPhone" />
                        </td>
                    </tr>
                    <tr>
                        <th>��������<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text email must" data-msg="����д��������" name="szEmail" />
                        </td>
                    </tr>
                </table>
            </div>
            <h4 class="h_title ">��������� <small>�����Ӧ�����ڳ���������Χ�ڣ�<span class="s_min red"></span>-<span class="s_max red"></span></small></h4>
            <div class="f3 fieldset">
                <table>
                    <tr>
                        <th>��С��������<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text min must" data-msg="��������С��������" data-reg="number" name="dwMinUsers" />
                        </td>
                    </tr>
                    <tr>
                        <th>�����������<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text max must" data-msg="�����������������" data-reg="number" name="dwMaxUsers" />
                        </td>
                    </tr>
                    <tr>
                        <th><span class="uni_trans">��ʼ��Ա</span></th>
                        <td class="dlg_mb_panel"></td>
                    </tr>
                </table>
            </div>
            <h4 class="h_title ">�ʱ�䰲��</h4>
            <div class="f4 fieldset">
                <table>
                    <tr>
                        <th>������������<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text aty_date deadline" name="dwEnrollDeadline" readonly="true" />
                            <span class="grey">(��������)</span>
                        </td>
                    </tr>
                    <tr>
                        <th>��������<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text aty_date publish" name="dwPublishDate" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <th>�����<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text aty_date begin_date" name="dwActivityDate" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <th>��ʼʱ��<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text aty_time start" name="dwBeginTime" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <th>����ʱ��<span class="red"> *</span>
                        </th>
                        <td>
                            <input type="text" class="input_text aty_time end" name="dwEndTime" readonly="true" />
                        </td>
                    </tr>
                </table>
            </div>
                        <h4 class="h_title ">�����</h4>
            <div class="f5 fieldset">
                  <table>
                    <tr>
                        <td>
                            <label class="click requirment"><input type="checkbox" name="require_seat" class="require_seat" value="true"/> ֧��ѡ��</label>
                            <label class="click requirment"><input type="checkbox" name="require_check_in" class="require_check_in" value="true"/> �Գ�ϯ��Ա����</label>
                        </td>
                    </tr>
                </table>
            </div>
            <h4 class="h_title ">������Ϣ</h4>
            <div class="f6 fieldset">
                <table>
                    <tr>
                        <th>����
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
                        <th>�����
                        </th>
                        <td>
                            <span class="text-primary">����ͼƬ��ʽjpg,gif,png,bmp</span>
                            <div class="input-group" style="width: 100%;">
                                <div>
                                    <input type="hidden" class="up_file placard_up_file" name="szActivityPlanURL" />
                                    <input type="button" class="placard_file upload_file" value="�ϴ�" limit="jpg,gif,png,bmp" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="fieldset">
                <table>
                    <tr>
                        <th>�������<span class="red"> *</span>
                        </th>
                        <td>
                            <div>
                            <%if (kindurl != "")
                                {%>
                            <a target="_blank" href="../../../<%=kindurl %>"">�����������ģ��</a>
                            <%} %>
                                </div>
                            <span class="text-primary">�������Ҫ���ύ�������</span>
                            <div class="input-group" style="width: 100%;">
                                <div>
                                    <input type="hidden" class="up_file datum_up_file" name="szApplicationURL" />
                                    <input type="button" class="datum_file upload_file" value="�ϴ�" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="submitarea text-center">
                <input type="button" class="submit_openaty btn btn-info" value="�ύ" />
                <input type="button" class="btn btn-default" value="����" onclick="uni.reload();" />
            </div>
        </form>
    </div>
</body>
</html>

