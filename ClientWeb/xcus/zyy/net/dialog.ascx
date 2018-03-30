<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dialog.ascx.cs" Inherits="WebUserControl" %>

<!-- ȫվ����ģ�� -->
<!-- ��¼�� -->
<div id="dlg_login" title="��¼" class="dialog">
    <form name='login' onsubmit="return false;" class="dlg">
        <!-- ��¼�� begin -->
        <table>
            <tr>
                <th>�ʺ�</th>
                <td>
                    <input name="id" id="id" type="text" class="input_txt id" /></td><td style="width:40px;">
                    <span class="error reg_number_msg" style="display: none;">������</span></td>
            </tr>
            <tr>
                <th>����</th>
                <td>
                    <input name="pwd" id="pwd" type="password" onblur="blurPassword(this)" onfocus="focusPassword()" class="input_txt" style="display: none;">
                    <input name="pwd_text" id="pwd_text" type="text" onfocus="focusPasswordText()" class="input_txt" /></td><td></td>
            </tr>

        </table>
        <div class="submitarea clear">
            <input type="submit" class="button" value="��¼" /><a class="reg button">���û�����</a>
        </div>
        <!-- ��¼�� end-->
        <div class="fail">
            <p id="login_msg" style="color: red;">
                <!--��¼ʧ����ʾ-->
            </p>
        </div>
    </form>
</div>
<!-- ���� -->
<div id="regdialog" title="����" class="dialog">
    <form name='act' onsubmit="return false;" class="dlg">
        <!-- ����� begin -->
        <table>
            <tr>
                <th>���ֻ�У԰ƽ̨�ʺ�</th>
                <td>
                    <input name="id" id="rid" type="text" class="input_txt id" /></td>
                <td class="msg"><span class="red">*</span><span class="error reg_number_msg" style="display: none;">������</span></td>
            </tr>
            <tr>
                <th>���ֻ�У԰ƽ̨����</th>
                <td>
                    <input name="pwd" id="rpwd" type="password" class="input_txt" /></td>
                <td class="msg"><span class="red">*</span>
                </td>
            </tr>
            <tr>
                <th>�ֻ�(����)</th>
                <td>
                    <input name="phone" type="text" class="input_txt" /></td>
                <td class="msg"><span class="red">*</span><span class="error" id="reg_mobile_msg" style="display: none;">��ʽ����</span></td>
            </tr>
            <tr>
                <th>����(����)</th>
                <td>
                    <input name="email" type="text" class="input_txt" /></td>
                <td class="msg"><span class="red">*</span><span class="error" id="reg_mail_msg" style="display: none;">��ʽ����</span></td>
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
<!-- �½�ʵ����Ŀ -->
<div id="addrtdialog" title="�½�ʵ����Ŀ" class="dialog">
    <form name='addrt' id="addrtForm" onsubmit="return false;" class="dlg">
        <!-- ����� begin -->
        <table style="vertical-align: middle; margin: 0 20px;">
            <tr>
                <th>��Ŀ���ƣ�</th>
                <td>
                    <input id="new_rtname" name="rtname" type="text" class="input_txt" /></td>
            </tr>
            <tr>
                <th>��Ŀ����</th>
                <td>
                    <select class="rt_level input_txt" name="rt_level">
                        <option value="4096">����</option>
                        <option value="4">У��</option>
                        <option value="3">���ּ�</option>
                        <option value="2">ʡ����</option>
                        <option value="1">���Ҽ�</option>
                    </select></td>
            </tr>
            <tr>
                <th>��ӳ�Ա��</th>
                <td>
                    <input id="rtmember" name="rtmember" type="text" class="input_txt rtmember" value="�������ʺ�" onclick="if (this.value == '�������ʺ�') { this.value = '' }" onblur="if(this.value=='') {this.value='�������ʺ�';}" />
                    <a id="addmember" class="addmember button">���</a></td>
            </tr>
            <tr>
                <th>��Ա�б�</th>
                <td>
                    <ul id="memList" class="ul_member memList">
                    </ul>
                </td>
            </tr>
        </table>
        <div class="submitarea clear" style="text-align: center;">
            <a id="addrt" class="detail button">�ύ</a>
        </div>
        <div class="grey" style="text-align: center; padding: 3px;">
            ���ݲ���ӳ�Ա
        </div>
    </form>
</div>
<!-- ������ʦ -->
<div id="gettutordialog" title="������ʦ" class="dialog">
    <form id='gettutor' onsubmit="return false;" class="dlg">
        <table>
            <tr>
                <th>����</th>
                <td>
                    <input name="tutor" id="dlg_tutor_name" type="text" class="input_txt" value="�����ؼ���" onclick="if (this.value == '�����ؼ���') { this.value = '' }" onblur="if(this.value=='') {this.value='�����ؼ���';}" />
                </td>
            </tr>
        </table>
        <table>
            <tbody id="dlg_tutor_list">
            </tbody>
        </table>
        <div style="margin-top: 30px; text-align: center;">
            <input type="submit" class="button" id="get_tutor" value="����" />
        </div>
        <div style="text-align: center; color: red;">
            <p id="dlg_tutor_msg">
                <!--ʧ����ʾ-->
            </p>
        </div>
    </form>
</div>
<script>
    //��¼��ָ������ҳ��
    var HrefPage = "";
    function dlgReload() {
        if (HrefPage == "") {
            location.reload();
        }
        else {
            location = HrefPage;
        }
    }
    $(function () {
        //��¼
        $('form[name=login]').submit(function () {
            pro.j.lg.fLogin("login", $(this), function (rlt) {
                if (rlt.ret == 1) {
                    dlgReload();
                }
                else if (rlt.ret == 2) {
                    uni.msgBox(rlt.msg, "", function () {
                        $("#regdialog .id").val($("#dlg_login .id").val());
                        $("#regdialog").dialog('open');
                    });
                    $("#dlg_login").dialog('close');
                }
            },
            function (rlt) {
                document.getElementById("login_msg").innerText = rlt.msg;
            });
        });

        $("#dlg_login").dialog({
            width: 383, autoOpen: false, modal: true, minHeight: 270, bgiframe: true, open: function () {
                var dig = document.getElementById('commanResvStat'); if (dig != null) dig.style.display = 'none';
            }, beforeclose: function () {
                HrefPage = "";
                var dig = document.getElementById('commanResvStat'); if (dig != null) dig.style.display = 'block';
            }
        });
        $("#dlg_login .close").click(function () {
            $("#dlg_login").dialog('close');
            return false;
        });

        $("#dlg_login .reg").click(function () {
            $("#dlg_login").dialog('close');
            $("#regdialog").dialog('open');
            return false;
        });
        //����
        $('form[name=act]').submit(function () {
            var value = $('input[name=email]', this).val();
            if (!CheckEmailBox(value, $(this)))
                return false;
            value = $('input[name=phone]', this).val();
            if (!CheckPhoneBox(value, $(this)))
                return false;
            pro.j.lg.fLogin("act", $(this), function (rlt) {
                uni.msgBoxR("����ɹ���");
            },
            function (rlt) {
                document.getElementById("reg_msg").innerText = rlt.msg;
            });
        });

        $("#regdialog").dialog({ width: 480, autoOpen: false, modal: true, minHeight: 302, bgiframe: true });
        $("#regdialog .close").click(function () {
            $("#regdialog").dialog('close');
            return false;
        });

        $("form[name=act] input[name=phone]").change(function () {
            CheckPhoneBox(this.value, $("form[name=act]"));
        });

        $("form[name=act] input[name=email]").change(function () {
            CheckEmailBox(this.value, $("form[name=act]"));
        });

        $("form.dlg input[name=id]").change(function () {
            var msg = $(this).parent().parent().find('.reg_number_msg')[0];
            pro.j.lg.isExist(this.value, function (rlt) {
                msg.style.display = 'none';
            }, function () {
                msg.style.display = '';
            });
        });

        $(".login a").click(function () {
            $("#dlg_login").dialog('open');
            return false;
        });
        $(".active a").click(function () {
            $("#regdialog").dialog('open');

            return false;
        });

        //��ȡ��ʦ
        pro.d.acc.srchTutor = function (sel) {
            var dlgsrchT = $("#gettutordialog");
            $("#gettutordialog").dialog({ minWidth: 300, autoOpen: false, modal: true, minHeight: 200, bgiframe: true });
            $("#gettutordialog .close").click(function () {
                $("#gettutordialog").dialog("close");
            });
            $("#gettutordialog form:first").submit(function () {
                var name = $("#dlg_tutor_name").val();
                pro.j.acc.getTutorByName(name, function (rlt) {
                    var list = rlt.data;
                    var list_str = "";
                    $(list).each(function () {
                        list_str += "<tr><td>" + this.id + "</td><td class='tru_name'>" + this.name + "</td><td>(" + this.dept +
                            ")</td><td><a class='sel click' accno='" + this.accno + "' name='" + this.name + "'>ѡ��</a></td></tr>";
                    });
                    $("#dlg_tutor_list").html(list_str);
                    $("#dlg_tutor_list a.sel").click(function () {
                        pthis = $(this);
                        sel(pthis.attr("accno"), pthis.attr("name"), dlgsrchT);
                    });
                },
                function (rlt) {
                    $("#dlg_tutor_msg").html(rlt.msg);
                });
            });
            $("#gettutordialog").dialog("open");
        }


        //��Ŀ���
        $("#addrtdialog").dialog({ width: 435, autoOpen: false, modal: true, minHeight: 302, bgiframe: true });
        $("#addrt").click(function () {
            var rtname = $("#new_rtname").val();
            if (rtname == "") {
                uni.msgBox("��������Ŀ���ƣ�");
                return false;
            }
            var level = $("#addrtdialog select.rt_level").val();
            var list = "";
            $("#memList span[name=memid]").each(function () {
                if (!$(this).is(':hidden')) {
                    list += $(this).html() + ',';
                }
            });
            pro.j.rtest.creRTest(rtname, level, "0", "", list, function () {//δ��������
                uni.msgBoxR("������Ŀ�ɹ���");
            });
        });
        $("#addmember").click(function () {
            var id = $(this).parent().find("input.rtmember").val();
            if (id == "") {
                uni.msgBox("�������Ա�ʺţ�");
                return false;
            }
            pro.j.acc.getAccById(id, function (rlt) {
                var list = $("#memList").html();
                var acc = (rlt.data)[0];
                list += "<li><span name='memid'>" + id + "</span>|<span>" + acc.name + "</span>|<a href='#' onclick='$(this).parent().hide();return false;'>ɾ��</a></li>";
                $("#memList").html(list);
            });
        });
        //��ť�¼�
        $("a.btn_activate").click(function () {
            $("#regdialog").dialog('open');
            return false;
        });
        $("a.reg").click(function () {
            $("#rid")[0].value = $("#id")[0].value;
            $("#rpwd")[0].value = $("#pwd")[0].value;
            $("#regdialog").dialog('open');
            return false;
        });
        $("a.btn_login").click(function () {
            $("#dlg_login").dialog('open');
            return false;
        });
    });

    function CheckEmailBox(taget, table) {
        var IsReady = uni.ckEmail(taget);

        if (IsReady)
            table.find('#reg_mail_msg').hide();
        else
            table.find('#reg_mail_msg').show();

        return IsReady;

    }
    function CheckPhoneBox(taget, table) {
        var IsReady = (taget.length==11);//uni.ckMobile(taget);

        if (IsReady)
            table.find('#reg_mobile_msg').hide();
        else
            table.find('#reg_mobile_msg').show();

        return IsReady;
    }
    function blurPassword(temp) {
        //�Ƿ�����������
        if (uni.isNull(temp.value)) {
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

</script>
