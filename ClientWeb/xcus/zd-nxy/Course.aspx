<%@ Page Language="C#" MasterPageFile="Modules/Master.master" AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="DevWeb_Course" %>

<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <link href='Scripts/UniTag/TagStyleH.css' rel='stylesheet' />
    <style type="text/css">
        #alterUser { font-size: 14px; }
    </style>
    <script type="text/javascript">
        $(function () {
            //判断动作
            var req = new QueryString();
            var str = req["act"];
            if (str == "rt") {
                $("#tag_rt").trigger("click");
            }
            else if (str == "check") {
                $("#memApply").trigger("click");
            }

            $("#alterrtdialog").dialog({ width: 435, autoOpen: false, modal: true, minHeight: 402, bgiframe: true });
            $("#alterrtdialog a.close").click(function () {
                $("#alterrtdialog").dialog('close');
                return false;
            });
            $("a.alterCourse").click(function () {
                var id = $(this).parent().parent().find("input.courseId").val();
                if (id != "" && id != undefined) {
                    ShowWait();
                    $("#set_rt_id").val(id);
                    submitProc("get", id);
                }
                return false;
            });
            $("a.delCourse").click(function () {
                var id = $(this).parent().parent().find("input.courseId").val();
                ConfirmBox("确定要删除课题？", function () {
                    if (id != "" && id != undefined) {
                        submitProc("del", id);
                    }
                });
                return false;
            });
            $("#set_alterrt_bt").click(function () {
                var id = $("#set_rt_id").val();
                if (id != "" && id != undefined) {
                    submitProc("set", id);
                }
            });
            $("#set_addmember").click(function () {
                var id = $(this).parent().find("input.rtmember").val();
                var groupId = $("#set_group_id").val();
                if (id == "") {
                    MessageBox("请输入成员学工号！");
                    return false;
                }
                $.ajax({
                    type: "GET",
                    url: "Ajax_Code/account.aspx?act=addmember&id=" + id + "&groupId=" + groupId,
                    dataType: "json",
                    success: function (rlt) {
                        if (rlt.ret == 0) {
                            MessageBox(rlt.msg);
                        }
                        else if (rlt.ret == 1) {
                            var list = $("#set_mem_list").html();
                            list += "<li><span name='memid' class='add'>" + id + "</span>|<span>" + rlt.name + "</span>|<a href='#' onclick='$(this).parent().hide();return false;'>删除</a></li>";
                            $("#set_mem_list").html(list);
                        }
                    }
                });
            });
            $("#set_replace").click(function () {
                var id = $("#leader_logonname").val();
                if (id == "") {
                    MessageBox("请输入要改换的负责人学工号！");
                    return false;
                }
                $.ajax({
                    type: "GET",
                    url: "Ajax_Code/account.aspx?act=getleader&id=" + id,
                    dataType: "json",
                    success: function (rlt) {
                        if (rlt.ret == 0) {
                            MessageBox(rlt.msg);
                        }
                        else if (rlt.ret == 1) {
                            $("#curLeader").html(rlt.get_leader);
                            $("#set_leader").val(rlt.get_leader);
                            $("#set_leader_acc").val(rlt.get_leader_acc);
                            $("#set_leader_lgname").val(rlt.get_leader_lgname);
                        }
                    }
                });
            });

            $(".tutor_check").click(function () {
                var obj = $(this);
                if ($(this).hasClass("ok")) {
                    ConfirmBox("是否批准此学生课题实验资格？", function () {
                        actCheck("ok", obj);
                    });
                }
                else if ($(this).hasClass("del")) {
                    ConfirmBox("确定删除此学生？", function () {
                        actCheck("del", obj);
                    });
                }
                else if ($(this).hasClass("fail")) {
                    ConfirmBox("是否撤销此学生课题实验资格？", function () {
                        actCheck("fail", obj);
                    });
                }
            });
        });
        function actCheck(order, obj) {
            if (order == undefined || order == "") {
                return;
            }
            var str = "&order=" + order + "&stu_name=" + obj.parents("tr").find(".stu_name").html() + "&stu_accno=" + obj.parents("tr").find(".stu_accno").val();
            $.ajax({
                type: "GET",
                url: "Ajax_Code/account.aspx?act=tutorcheck" + encodeURI(str),
                dataType: "json",
                success: function (rlt) {
                    if (rlt.ret == 0) {
                        MessageBox(rlt.msg);
                    }
                    else if (rlt.ret == 1) {
                        MsgBoxR("操作成功,将重新加载页面！");
                    }
                }
            });
        }
        function submitProc(act, id) {
            var data = "";
            if (act == "set") {
                var set_addmem_list = "set_addmem_list=";
                var set_delmem_list = "&set_delmem_list=";
                $("#set_mem_list span[name=memid]").each(function () {
                    if ($(this).is(':hidden')) {
                        if (!$(this).hasClass('add')) {
                            set_delmem_list += $(this).html() + ',';
                        }
                    }
                    else if ($(this).hasClass('add')) {
                        set_addmem_list += $(this).html() + ',';
                    }
                });

                data = set_addmem_list + set_delmem_list + "&" + $("#alterrtForm").serialize();
            }
            $.ajax({
                type: "GET",
                url: "Ajax_Code/rTestes.aspx?act=" + act + "&id=" + id + "&" + data,
                dataType: "json",
                success: function (rlt) {
                    if (rlt.ret == 0) {
                        MessageBox(rlt.msg);
                    }
                    else if (rlt.ret == 1) {
                        if (rlt.act == "get") {
                            $("#set_rt_name").val(rlt.get_rtname);
                            $("#curLeader").html(rlt.get_leader);
                            $("#set_leader").val(rlt.get_leader);
                            $("#set_leader_acc").val(rlt.get_leader_acc);
                            $("#set_group_id").val(rlt.get_group_id);
                            $("#set_mem_list").html(rlt.get_member);
                            $("#alterrtdialog").dialog('open');
                        }
                        else if (rlt.act == "del") {
                            $("#" + rlt.id).slideUp();
                        }
                        else if (rlt.act == "set") {
                            MsgBoxR("保存成功，将重新载入页面");
                        }
                    }
                    HideWait();
                },
                error: function (err) {
                    MessageBox("异步操作返回异常！");
                    HideWait();
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="alterrtdialog" title="管理课题" class="dialog">
        <form name='alterrtForm' id="alterrtForm" onsubmit="return false;">
            <table style="vertical-align: middle; margin: 0 20px;">
                <tr>
                    <th>课题名称：</th>
                    <td>
                        <input id="set_rt_name" name="set_rt_name" type="text" class="input_txt" />
                        <input id="set_rt_id" name="set_rt_id" type="hidden" />
                    </td>
                </tr>
                <tr>
                    <th>负责人：</th>
                    <td><span id="curLeader"></span>
                        <input type="hidden" id="set_leader_acc" name="set_leader_acc" />
                        <input type="hidden" id="set_leader" name="set_leader" />
                        <input type="hidden" id="set_leader_lgname" name="set_leader_lgname" />
                    </td>
                </tr>
                <tr>
                    <th>改换负责人：</th>
                    <td>
                        <input id="leader_logonname" name="leader_logonname" type="text" class="input_txt" value="学号或教工号" onclick="if (this.value == '学号或教工号') { this.value = '' }" onblur="if(this.value=='') {this.value='学号或教工号';}" />
                        |<a id="set_replace" class="click">替换</a></td>
                </tr>
                <tr>
                    <th>成员列表：</th>
                    <td>
                        <input type="hidden" id="set_group_id" name="set_group_id" />
                        <ul id="set_mem_list" class="ul_member"></ul>
                    </td>
                </tr>
                <tr>
                    <th>添加成员：</th>
                    <td>
                        <input id="set_rt_member" name="set_rt_member" type="text" class="input_txt rtmember" value="学号或教工号" onclick="if (this.value == '学号或教工号') { this.value = '' }" onblur="if(this.value=='') {this.value='学号或教工号';}" />
                        |<a id="set_addmember" class="addmember click" style="cursor: pointer;">添加</a></td>
                </tr>
            </table>
            <div class="submitarea clear" style="text-align: right; margin-right: 120px;">
                <a id="set_alterrt_bt" class="button">保存</a>
            </div>
            <div class="fail">
                <p id="alterrtErr">
                    <!--错误提示-->
                </p>
            </div>
        </form>
    </div>
    <div class="g-b-m">
        <div id="con" class="tags" style="margin-top: 20px;">
            <ul id="tags" class="tag_head">
                <li id="memApply"><a>管理学生</a></li>
                <li id="tag_rt" class="hidden"><a>我的课题</a></li>
            </ul>
            <div id="tagContent" class="float_all tag_con">
                <div class="item">
                    <div style="text-align: left;">此处可审核学生是否有权作为您的学生到实验室完成实验。</div>
                    <div class="tbl_list">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 120px;">学生</th>
                                    <th style="">学号</th>
                                    <th style="">学院</th>
                                    <th style="">联系方式</th>
                                    <th style="width: 60px;">状态</th>
                                    <th style="width: 120px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=stuList %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="item">
                    <div style="text-align: right;"><a id="addRTest" class="button" onclick="$('#addrtdialog').dialog('open');return false;">新建课题</a></div>
                    <div class="resv_list tbl_list">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 240px;">课题名称</th>
                                    <th style="width: 120px;">负责人</th>
                                    <th style="">实验次数</th>
                                    <th style="">实验时间</th>
                                    <th style="width: 80px;">成员数量</th>
                                    <th style="width: 160px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=rtList %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="cleaner"></div>
    </div>
</asp:Content>
