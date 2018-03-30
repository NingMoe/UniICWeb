<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="ClientWeb_xcus_bsd_xl_User" %>

<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <style>
        table td.f-tl { padding-left: 1px; }
    </style>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="position:relative">
        <div style="position:absolute;top:5px;right:20px;">
            <form runat="server">
            <asp:DropDownList runat="server" ID="SelTime" AutoPostBack="true">
                <asp:ListItem Value="1">最近一个月</asp:ListItem>
                <asp:ListItem Value="3">最近三个月</asp:ListItem>
            </asp:DropDownList>
            </form>
        </div>
            <div class="tabs">
        <ul class="tab_head">
            <li><a>科研实验</a></li>
            <li><a>研讨室预约</a></li>
            <%--<li><a>教学预约</a></li>--%>
            <li><a>用户信息</a></li>
        </ul>
        <div class="tab_con">
            <div id="rt_rsv_info" class="item box_tbl">
                <table>
                    <thead>
                        <tr class="title tbl_head">
                            <th>实验项目</th>
                            <th width="50">预约人</th>
                            <th width="120">预约实验室</th>
                            <th>实验名称</th>
                            <th width="80">提交时间</th>
                            <th width="80">开始时间</th>
                            <th width="80">结束时间</th>
                            <th width="60">状态</th>
                            <th width="60">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%=rtRsvList %>
                    </tbody>
                </table>
            </div>
            <div id="resv_info" class="item box_tbl">
                                <table>
                    <thead>
                        <tr class="title tbl_head">
                            <th>会议名称</th>
                            <th>预约研讨室</th>
                            <th width="160">成员</th>
                            <th width="80">提交时间</th>
                            <th width="80">开始时间</th>
                            <th width="80">结束时间</th>
                            <th width="80">状态</th>
                            <th width="60">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%=resvList %>
                    </tbody>
                </table>
            </div>
<%--            <div class="item box_tbl">
                                <table>
                    <thead>
                        <tr class="title tbl_head">
                            <th>教师</th>
                                <th style="">主题</th>
                                <th style="">预约时间</th>
                                <th>地点</th>
                                <th style="">成员</th>
                                <th style="">学时</th>
                                <th style="width:80px;">预约状态</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%=teachResv %>
                    </tbody>
                </table>
            </div>--%>
            <div id="user_info" class="item">
                <div class="dialog" id="dlg_rsc_tutor">
                    <form onsubmit="return false">
                        <table>
                            <tr>
                                <td>导师姓名：</td>
                                <td>
                                    <input class="sel_tutor" type="text" act="truename" url="searchAccount.aspx" para="tutor=true" style="width: 200px;" />
                                    <input class="tutor_id" name="tutor_id" type="hidden" />
                                    <input class="tutor_name" name="tutor_name" type="hidden" />
                                </td>
                            </tr>
                        </table>
                    </form>
                </div>
                <div style="width: 500px; margin: 20px auto; border: 1px solid #ccc; text-align: center; background-color: #fcfcfc;">
                    <form id="updateacc" onsubmit="return false;">
                        <table class="acc_tbl">
                            <tr>
                                <td style="width: 80px;"><span style="font-weight: 700;">姓名：</span></td>
                                <td><span id="accName"></span></td>
                            </tr>
                            <tr>
                                <td><span style="font-weight: 700">帐号：</span></td>
                                <td><span id="accLgName"></span></td>
                            </tr>
                            <tr>
                                <td><span style="font-weight: 700">部门：</span></td>
                                <td><span id="accColl"></span></td>
                            </tr>
                            <tr>
                                <td><span style="font-weight: 700">(必填)手机：</span></td>
                                <td>
                                    <input type="text" id="accPhone" name="accPhone" /></td>
                            </tr>
                            <tr>
                                <td><span style="font-weight: 700">(必填)邮箱：</span></td>
                                <td>
                                    <input type="text" id="accEmail" name="accEmail" /></td>
                            </tr>
                            <tr>
                                <td><span style="font-weight: 700">信用积分：</span></td>
                                <td>
                                    <span id="credit_score"></span>
                                    | <a href="ArticleList.aspx?id=credit&type=other&title=<%=Server.UrlEncode("信用积分规则")%>"">信用积分规则</a>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: center;">
                            <span id="submit" class="button" onclick="subUpdate();">保存</span>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div>
        <script type="text/javascript">
            $(".tabs").unitab();
            $(function () {
                //初始化用户信息
                var acc = pro.acc;
                $("#accName").html(acc.name);
                $("#accLgName").html(acc.id);
                $("#accColl").html(acc.dept);
                $("#accPhone").val(acc.phone);
                $("#accEmail").val(acc.email);
                var score = parseInt(acc.score);
                if (!isNaN(score)) {
                    if (score >= 0)
                        $("#credit_score").html(score + "分");
                    else
                        $("#credit_score").html("0分 (禁用:<span style='color:red;'>" + -score + "</span>天)");
                }
                //if (!pro.isTutor(acc.ident)) {
                //    $("#is_tutor_hidden").show();
                //    $("#sp_tutor_name").html(acc.tutor);
                //    $("#tutor_name").val(acc.tutor);
                //    $("#tutor_acc").val(acc.tutorid);
                //}
                //日期控件
                $(".Wdate").timepicker({
                    controlType: 'select',
                    timeFormat: "HH:mm",
                    stepHour: 1,
                    stepMinute: 1,
                });
                $(".Wdate").attr("readonly", "readonly");
            });
            //更新联系方式
            function subUpdate() {
                var phone = $("#accPhone").val();
                if (!uni.ckMobile(phone)) { uni.msgBox("手机填写有误！"); return false; }
                var email = $("#accEmail").val();
                if (!uni.ckEmail(email)) { uni.msgBox("邮箱填写有误！"); return false; }
                pro.j.acc.upContact(phone, email, function () {
                    uni.msgBoxR("保存成功");
                });
            };
            //查找导师
            function findTutor() {
                uni.dlg($("#dlg_rsc_tutor"), "指定导师", 320, 160, function () {
                    if (uni.isNull($("#dlg_rsc_tutor .tutor_id").val())) {
                        uni.msgBox("查询失败");
                        return;
                    }
                    pro.j.acc.assignTuCked($("#dlg_rsc_tutor .tutor_id").val(), $("#dlg_rsc_tutor .tutor_name").val(), function () {
                        uni.msgBoxR("指定导师成功！");
                    });
                });
                $("#dlg_rsc_tutor .sel_tutor").procomplete(function (event, ui) {
                    if (ui.item) {
                        if (ui.item.id && ui.item.id != "") {
                            $("#dlg_rsc_tutor .tutor_id").val(ui.item.id);
                            $("#dlg_rsc_tutor .tutor_name").val(ui.item.name);
                        }
                    }
                });
            }
            //删除预约
            function delRsv(rsv) {
                var id = $(rsv).attr("rsvId");
                uni.confirm("确定要删除吗？", function () {
                    pro.j.rsv.delResv(id, function () {
                        uni.msgBox("删除成功！");
                        $(rsv).parents("tr:first").remove();
                    });
                });
            }
            //修改预约
            function alterRsv(bt) {
                var td = $(bt).parents("td:first");
                var tm = td.find(".time");
                var alt = td.find(".alter");
                tm.hide();
                alt.show();
            }
            function back(bt) {
                var td = $(bt).parents("td:first");
                var tm = td.find(".time");
                var alt = td.find(".alter");
                tm.show();
                alt.hide();
            }
            function subAlter(bt) {
                var td = $(bt).parents("td:first");
                var id = td.attr("rsvId");
                var start = new Date(td.attr("start"));
                var now = new Date();
                var end = new Date(td.attr("end"));
                var old_m = end.getTime() / (1000 * 60);
                var time = td.find(".alter input").val();
                var tm = time.split(':');
                var h = parseInt(tm[0]);
                var m = parseInt(tm[1]);
                end.setHours(h);
                end.setMinutes(m);
                var diff = end.getTime() / (1000 * 60) - old_m;
                var valid = parseInt(td.attr("valid"));
                if (!isNaN(valid)) {
                    if (diff > valid) {
                        uni.msgBox("延时超出了可用时长");
                        return;
                    }
                }
                if (end <= now) {
                    uni.msgBox("结束时间不能早于当前时间");
                    return;
                }
                if (end <= start) {
                    uni.msgBox("结束时间不能早于开始时间");
                    return;
                }
                var para = {};
                para.act = "set_resv";
                para.resv_id = id;
                para.end = end.format("yyyy-MM-dd HH:mm:ss");
                pro.j.rsv.rsvAct(para, function () {
                    uni.msgBoxR("操作成功");
                });
            }
        </script>
    </div>
</asp:Content>
