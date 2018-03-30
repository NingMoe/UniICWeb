<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dialog.ascx.cs" Inherits="WebUserControl" %>
<div id="zd_nxy_dialog">
<div id="alertL" title="����" class="dialog">
</div>
<div id="confirmL" title="��Ϣ" class="dialog">
</div>
<!-- ��½�������ȫվģ�� -->
<div id="logindialog" title="��¼" class="dialog">
    <form name='login' onsubmit="return false;">
        <!-- ��½�� begin -->
        <table>
            <tr>
                <th>ѧ��</th>
                <td>
                    <input name="id" id="id" type="text" class="input_txt" value="ѧ�Ż�̹���" onclick="if (this.value == 'ѧ�Ż�̹���') { this.value = '' }" onblur="if(this.value=='') {this.value='ѧ�Ż�̹���';}" />
                    <span class="error reg_number_msg" style="display: none;">ѧ�Ŵ���</span></td>
            </tr>
            <tr>
                <th>����</th>
                <td>
                    <input name="pwd" id="pwd" type="password" onblur="blurPassword(this)" onfocus="focusPassword()" class="input_txt" style="display: none;">
                    <input name="pwd_text" id="pwd_text" type="text" value="ͬһ��ͨ����" onfocus="focusPasswordText()" class="input_txt" /></td>
            </tr>

        </table>
        <div class="submitarea clear">
            <input type="submit" class="button" value="��¼" /><a class="reg button">���û�����</a>
        </div>
        <!-- ��½�� end-->
        <div class="fail">
            <p id="login_msg">
                <!--��½ʧ����ʾ-->
            </p>
        </div>
    </form>
</div>
<div id="regdialog" title="����" class="dialog">
    <form name='act' onsubmit="return false;">
        <!-- ����� begin -->
        <table>
            <tr>
                <th>ѧ��</th>
                <td>
                    <input name="id" id="rid" type="text" class="input_txt" />
                    <span class="error reg_number_msg" style="display: none;">ѧ�Ŵ���</span></td>
            </tr>
            <tr>
                <th>����</th>
                <td>
                    <input name="pwd" id="rpwd" type="password" class="input_txt" />
                    ͬһ��ͨ����              
                </td>
            </tr>
            <tr>
                <th>�ֻ�</th>
                <td>
                    <input name="phone" type="text" class="input_txt" /><span class="red">*</span><span class="error" id="reg_mobile_msg" style="display: none;">�ֻ���������</span></td>
            </tr>
            <tr>
                <th>����</th>
                <td>
                    <input name="mail" type="text" class="input_txt" /><span class="red">*</span><span class="error" id="reg_mail_msg" style="display: none;">������������</span></td>
            </tr>
        </table>
        <div class="submitarea clear">
            <input type="submit" class="input_submit button" value="����" />
        </div>
        <div class="fail">
            <p id="reg_msg">
                <!--�˿����Ѵ��ڼ���״̬�������ظ�����-->
            </p>
        </div>
    </form>
</div>
<div id="alterdialog" title="�޸���ϵ��ʽ" class="dialog">
    <form name='alt' onsubmit="return false;">
        <!-- ����� begin -->
        <table style="vertical-align: middle;">
            <tr>
                <th></th>
                <td>
                    <h3>�޸���ϵ��ʽ</h3>
                </td>
            </tr>
            <tr>
                <th>�ֻ���</th>
                <td>
                    <input name="altphone" type="text" class="input_txt" />���ֻ���<span class="error" id="alt_mobile" style="display: none;">�ֻ���������</span></td>
            </tr>
            <tr>
                <th>���䣺</th>
                <td>
                    <input name="altmail" type="text" class="input_txt" />�������ַ<span class="error" id="alt_mail" style="display: none;">������������</span></td>
            </tr>
        </table>
        <div class="submitarea clear">
            <input type="submit" class="input_submit button" value="�޸�" />
        </div>
        <div class="fail">
            <p>
                <!--������ʾ-->
            </p>
        </div>
    </form>
</div>
<div id="addrtdialog" title="�½�ʵ�����" class="dialog">
    <form name='addrt' id="addrtForm" onsubmit="return false;">
        <!-- ����� begin -->
        <table style="vertical-align: middle; margin: 0 20px;">
            <tr>
                <th>�������ƣ�</th>
                <td>
                    <input id="new_rtname" name="rtname" type="text" class="input_txt" /></td>
            </tr>
            <tr>
                <th>��ӳ�Ա��</th>
                <td>
                    <input id="rtmember" name="rtmember" type="text" class="input_txt rtmember" value="ѧ�Ż�̹���" onclick="if (this.value == 'ѧ�Ż�̹���') { this.value = '' }" onblur="if(this.value=='') {this.value='ѧ�Ż�̹���';}" />
                    |<a id="addmember" class="addmember click">���</a></td>
            </tr>
            <tr>
                <th>��Ա�б�</th>
                <td>
                    <ul id="memList" class="ul_member memList">
                    </ul>
                </td>
            </tr>
        </table>
        <div class="submitarea clear" style="text-align: right; margin-right: 120px;">
            <a id="addrt" class="detail button">�ύ</a>
        </div>
        <div class="grey" style="text-align: center; padding: 3px;">
            ���ݲ���ӳ�Ա
        </div>
    </form>
</div>
<div id="gettutordialog" title="������ʦ" class="dialog">
    <form id='gettutor' onsubmit="return false;">
        <!-- �޸ĵ�ʦ begin -->
        <table>
            <tr>
                <th>����</th>
                <td>
                    <input name="tutor" id="tutor" type="text" class="input_txt" value="��ʦ�����ؼ���" onclick="if (this.value == '��ʦ�����ؼ���') { this.value = '' }" onblur="if(this.value=='') {this.value='��ʦ�����ؼ���';}" />
                </td>
            </tr>
        </table>
        <table>
            <tbody id="tutor_list">
            </tbody>
        </table>
        <div style="margin-top: 30px; text-align: center;">
            <input type="submit" class="button" id="get_tutor" value="����" />
        </div>
        <!-- ��½�� end-->
        <div style="text-align: center; color: red;">
            <p id="tutor_msg">
                <!--��½ʧ����ʾ-->
            </p>
        </div>
    </form>
</div>
    </div>
<script>
    var zd_nxy_dlg = $("#zd_nxy_dialog");
    var HrefPage = "";
    $(function () {
        //��ʼ����ť
        $('form[name=login]', zd_nxy_dlg).submit(function () {
            var data = $(this).serialize();
            $.ajax({
                type: "GET",
                url: "Ajax_Code/account.aspx?act=login&" + data,
                dataType: "json",
                success: function (object) {
                    if (Number(object.MsgId) > 0)
                        document.getElementById("login_msg").innerText = object.Message;
                    else if (Number(object.MsgId) < 0) {
                        MessageBox(object.Message);
                        $("#logindialog").dialog('close');
                        $("#regdialog").dialog('open');
                        if (object.Message.length > 0)
                            document.getElementById("login_msg").innerText = object.Message;
                    } else {
                        if (HrefPage == "") {
                            location.reload();
                        }
                        else {
                            location = HrefPage;
                        }
                    }
                }
            });
        });



        $('form[name=act]', zd_nxy_dlg).submit(function () {
            var value = $('input[name=mail]',this).val();
            if (!CheckEmailBox(value, $(this)))
                return false;

            value = $('input[name=phone]', this).val();
            if (!CheckPhoneBox(value, $(this)))
                return false;

            var data = $(this).serialize();
            MemberAjax({
                Prm: "act=act&" + data,
                Function: function (object) {
                    if (Number(object.MsgId) != 0) {
                        //MessageBox("��������˺Ż����벻��ȷ�������ԡ�");
                        //}
                        //else if(Number(object.MsgId)<0){
                        if ($("#reg_msg").length > 0) {
                            document.getElementById("reg_msg").innerText = object.Message;
                        }
                    } else {
                        MsgBoxR("����ɹ�������������ҳ��");
                        //$('#nav ul').toggle();
                        //$('#nav ul li[class=welcome] span').html(object.Name);
                        //document.getElementById("reg_msg").innerText="����ɹ����밴���Ͻǹرհ�ť";
                    }
                }
            });
        });


        $("form[name=act] input[name=phone]", zd_nxy_dlg).change(function () {
            CheckPhoneBox(this.value, $("form[name=act]", zd_nxy_dlg));
        });

        $("form[name=act] input[name=mail]", zd_nxy_dlg).change(function () {
            CheckEmailBox(this.value, $("form[name=act]", zd_nxy_dlg));
        });
        //custom
        $("form[name=alt] input[name=alt_phone]", zd_nxy_dlg).change(function () {
            CheckPhoneBox(this.value, $("form[name=alt]", zd_nxy_dlg));
        });
        $("form[name=alt] input[name=alt_mail]", zd_nxy_dlg).change(function () {
            CheckEmailBox(this.value, $("form[name=alt]", zd_nxy_dlg));
        });


        $("form input[name=id]", zd_nxy_dlg).change(function () {
            var buf = $(this);
            MemberAjax({
                Prm: "act=login&id=" + this.value + "&pwd=",
                Buffer: buf.parent().find('.reg_number_msg')[0],
                Function: function (object) {
                    if (object.Message == 'δ��Ȩ�û�') this.buf.style.display = '';
                    else this.buf.style.display = 'none';
                }
            });
        });

        $('.logout a').click(function () {
            if (typeof(pro)!="undefined") return;
            MemberAjax({
                Prm: 'act=logout',
                Function: function (i) {
                    location.reload();
                }
            });
        });
        $("#addmember").click(function () {
            var id = $(this).parent().find("input.rtmember").val();
            if (id == "") {
                MessageBox("�������Աѧ���ţ�");
                return false;
            }
            $.ajax({
                type: "GET",
                url: "Ajax_Code/account.aspx?act=addmember&id=" + id,
                dataType: "json",
                success: function (rlt) {
                    if (rlt.ret == 0) {
                        MessageBox(rlt.msg);
                    }
                    else if (rlt.ret == 1) {
                        var list = $("#memList").html();
                        list += "<li><span name='memid'>" + id + "</span>|<span>" + rlt.name + "</span>|<a href='#' onclick='$(this).parent().hide();return false;'>ɾ��</a></li>";
                        $("#memList").html(list);
                    }
                }
            });
        });
    });

    function MemberAjax(Action) {
        var url = "Ajax_Code/account.aspx?" + Action.Prm;
        $.ajax({
            type: "GET",
            buf: Action.Buffer,
            url: url,
            dataType: "json",
            cache:false,
            success: Action.Function,
            error: function (error) {
                MessageBox("�첽���������쳣��");
            }
        });
    }


    function CheckEmailBox(taget, table) {
        var IsReady = check_email(taget);

        if (IsReady)
            table.find('#reg_mail_msg').hide();
        else
            table.find('#reg_mail_msg').show();

        return IsReady;

    }
    function CheckPhoneBox(taget, table) {
        var IsReady = CheckMobile(taget);

        if (IsReady)
            table.find('#reg_mobile_msg').hide();
        else
            table.find('#reg_mobile_msg').show();

        return IsReady;
    }
    function blurPassword(temp) {
        //�Ƿ�����������
        if (isNull(temp.value)) {
            document.getElementById('pwd').style.display = "none";
            document.getElementById('pwd_text').style.display = "inline-block";
        } else {
            document.getElementById('pwd').style.color = "#666";
            document.getElementById('pwd').style.opacity = "0.75";//��͸����
        }
    }
    function focusPassword() {
        document.getElementById('pwd').style.color = "#000";
        document.getElementById('pwd').style.opacity = "1";
    }

    function focusPasswordText() {
        document.getElementById('pwd_text').style.display = "none";
        document.getElementById('pwd').style.display = "inline-block";
        document.getElementById('pwd').focus();
    }

    //�ж��ַ����Ƿ�Ϊ��
    function isNull(obj) {
        if (obj == null || obj.length == 0)
            return true;
        else
            return false;
    }

    $(function () {
        $("#logindialog").dialog({
            width: 383, autoOpen: false, modal: true, minHeight: 248, bgiframe: true, open: function () {
                var dig = document.getElementById('commanResvStat'); if (dig != null) dig.style.display = 'none';
            }, beforeclose: function () {
                HrefPage = "";
                var dig = document.getElementById('commanResvStat'); if (dig != null) dig.style.display = 'block';
            }
        });
        $("#logindialog .close").click(function () {
            $("#logindialog").dialog('close');
            return false;
        });

        $("#logindialog .reg").click(function () {
            $("#logindialog").dialog('close');
            $("#regdialog").dialog('open');
            return false;
        });

        $("#regdialog").dialog({ width: 435, autoOpen: false, modal: true, minHeight: 302, bgiframe: true });
        $("#regdialog .close").click(function () {
            $("#regdialog").dialog('close');
            return false;
        });

        $("#nav .login a").click(function () {
            $("#logindialog").dialog('open');
            return false;
        });

        $("#nav .active a").click(function () {
            $("#regdialog").dialog('open');

            return false;
        });

        $("#alterdialog").dialog({ width: 435, autoOpen: false, modal: true, minHeight: 202, bgiframe: true });
        $("#alterdialog a.close").click(function () {
            $("#alterdialog").dialog('close');
            return false;
        });

        $("#addrtdialog").dialog({ width: 435, autoOpen: false, modal: true, minHeight: 202, bgiframe: true });

        $("#addrt").click(function () {
            var rtname = $("#new_rtname").val();
            if (rtname == "") {
                MessageBox("������������ƣ�");
                return false;
            }
            var list = "";
            $("#memList span[name=memid]").each(function () {
                if (!$(this).is(':hidden')) {
                    list += $(this).html() + ',';
                }
            });
            var url = "Ajax_Code/rTestes.aspx?act=new&rtname=" + rtname + "&memlist=" + list;
            $.ajax({
                type: "GET",
                url: encodeURI(url),
                dataType: "json",
                success: function (rlt) {
                    if (rlt.ret == 1) {
                        MsgBoxR("��������ɹ���");
                    }
                    else {
                        MessageBox(rlt.msg);
                    }
                },
                error: function (err) {
                    debugger;
                    MessageBox("�޷����ӷ���");
                    HideWait();
                }
            });

        });



        $("a.btn_activate").click(function () {
            $("#regdialog").dialog('open');
            return false;
        });

        //$("a.reg").click(function () {
        //    $("#rid")[0].value = $("#id")[0].value;
        //    $("#rpwd")[0].value = $("#pwd")[0].value;

        //    $("#regdialog").dialog('open');
        //    return false;
        //});

        $("a.btn_login").click(function () {
            $("#logindialog").dialog('open');
            return false;
        });
    });

</script>
