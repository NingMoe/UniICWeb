<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RTest.aspx.cs" Inherits="ClientWeb_xcus_bsd_xl_RTest" %>

<%@ Register TagPrefix="Uni" TagName="tblresearch" Src="net/tblresearch.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <script type="text/javascript">
        //申请实验室
        function applyLab(roleid, rtid, labid, applyid) {
            $("#rt_apply_lab .upload_file").uploadFile();//上传文件
            $("#rt_apply_lab .rt_name").html($("#m_right_title").text());
            uni.dlgR($("#rt_apply_lab"), "项目实验申请", 420, 500, function (dlg) {
                var test_name = $(".test_name", dlg).val();
                var user_num = $(".user_num", dlg).val();
                if (!uni.isNoNull([user_num, test_name])) {
                    uni.msgBox("带“*”项为必填");
                    return;
                }
                var lab_usetime = $(".lab_usetime", dlg).val();
                lab_usetime = $.trim(lab_usetime);
                lab_usetime = parseFloat(lab_usetime);
                if (uni.isNull(lab_usetime) || isNaN(lab_usetime)) {
                    uni.msgBox("请正确填写实验总时长！");
                    return;
                }
                var once_time = $(".once_time", dlg).val();
                once_time = $.trim(once_time);
                once_time = parseFloat(once_time);
                if (uni.isNull(once_time) || isNaN(once_time)) {
                    uni.msgBox("请正确填写单次实验时间！");
                    return;
                }
                var file_name = $(".upload_file", dlg).attr("save_name");
                if (uni.isNull(file_name)) {
                    uni.msgBox("请上传申请报告");
                    return;
                }
                var para = {};
                para.act = "apply_rt_lab_userole";
                para.lab_id = labid;
                para.rt_id = rtid;
                para.role_id = roleid;
                para.apply_id = applyid;
                para.usetime = Math.round(lab_usetime * 60);
                para.test_name = test_name;
                para.once_time = Math.round(once_time * 60);
                para.user_num = user_num;
                para.save_file_name = file_name;
                pro.j.rtest.rtestAct(para, function () {
                    uni.msgBoxR("提交申请成功，等待管理员审核！");
                    $(dlg).dialog("close");
                });
            });
        }
        function reApplyLab(roleid, rtid, labid, applyid, test) {
            $("#re_rt_apply .upload_file").uploadFile();//上传文件
            $("#re_rt_apply .rt_name").html($("#m_right_title").text());
            $("#re_rt_apply .test_name").html(test);
            uni.dlgR($("#re_rt_apply"), "实验延时申请", 420, 260, function (dlg) {
                var lab_usetime = $(".lab_usetime", dlg).val();
                lab_usetime = $.trim(lab_usetime);
                lab_usetime = parseFloat(lab_usetime);
                if (uni.isNull(lab_usetime) || isNaN(lab_usetime)) {
                    uni.msgBox("请正确填写延长时间！");
                    return;
                }
                var file_name = $(".upload_file", dlg).attr("save_name");
                if (uni.isNull(file_name)) {
                    uni.msgBox("请上传申请报告");
                    return;
                }
                var para = {};
                para.act = "apply_rt_lab_userole";
                para.lab_id = labid;
                para.rt_id = rtid;
                para.role_id = roleid;
                para.apply_id = applyid;
                para.usetime = Math.round(lab_usetime * 60);
                para.re_apply = "true";
                para.save_file_name = file_name;
                pro.j.rtest.rtestAct(para, function () {
                    uni.msgBoxR("提交申请成功，等待管理员审核！");
                    $(dlg).dialog("close");
                });
            });
        }
        $(function () {
            //初始化
            $(".zebra").zebra();
            $("[title]").tooltip();
            $(".act_get").clickLoad();
            $(".button").button();
            //判断负责人
            var acc = pro.acc;
            if ((acc.ident & 16777216) > 0) {
                $(".is_leader").show();
            }
            else {
                $(".is_leader").hide();
            }
            //项目组操作
            $(".operate .op").click(function () {
                var rtid = $(this).parent().attr("rtid");
                var groupid = $(this).parent().attr("groupid");
                var id = pro.acc.id;
                if ($(this).hasClass("quit")) {
                    uni.confirm("确定退出项目？", function () {
                        pro.j.rtest.delRTMem(rtid, groupid, id, function () {
                            uni.msgBoxR("操作成功");
                        });
                    });
                }
                else if ($(this).hasClass("join")) {
                    uni.confirm("确定加入项目？", function () {
                        pro.j.rtest.addRTMem(rtid, groupid, id, function () {
                            uni.msgBoxR("操作成功");
                        });
                    });
                }
                else {
                    return;
                }
            });
        });
    </script>
    <div>
        <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a class="ui-tabs-anchor" id="m_right_title"><%=title %></a></li>
        </ul>
        <div class="dlg">
            <div id="rt_apply_lab" class="dialog">
                <form onsubmit="return false" enctype="multipart/form-data">
                    <div class="list">
                        <table>
                            <tr>
                                <td>项目名称：</td>
                                <td>
                                    <span class="rt_name"></span></td>
                            </tr>
                            <tr>
                                <td>实验名称：</td>
                                <td>
                                    <input type="text" class="test_name" name="test_name" /><span class="red">*</span></td>
                            </tr>
                            <tr>
                                <td>实验总时长：</td>
                                <td>
                                    <input type="text" class="lab_usetime" name="lab_usetime" /><span class="red">*</span>(小时)</td>
                            </tr>
                            <tr>
                                <td>单次实验时间：</td>
                                <td>
                                    <input type="text" class="once_time" name="once_time" /><span class="red">*</span>(小时)</td>
                            </tr>
                            <tr>
                                <td>被试人数：</td>
                                <td>
                                    <input type="text" class="user_num" name="user_num" /><span class="red">*</span></td>
                            </tr>
                            <tr>
                                <td>申请报告：</td>
                                <td>
                                    <div style="text-decoration: underline; margin-bottom: 3px;"><a href="实验室使用申请书.rtf">下载申请报告模版</a></div>
                                    <div>
                                        <input type="file" name="up_file_name" id="up_file_name" /><span class="red">*</span></div>
                                    <div style="height: 24px; line-height: 24px;">
                                        <input type="button" style="cursor: pointer;" class="upload_file" file="up_file_name" value="上传" /><span class="cur_file_name color1"></span></div>
                                </td>
                            </tr>
                        </table>
                    </div>

                </form>
            </div>
            <div id="re_rt_apply" class="dialog">
                <form onsubmit="return false" enctype="multipart/form-data">
                    <div class="list">
                        <table>
                            <tr>
                                <td>项目名称：</td>
                                <td>
                                    <span class="rt_name"></span></td>
                            </tr>
                            <tr>
                                <td>实验名称：</td>
                                <td>
                                    <span class="test_name"></span></td>
                            </tr>
                            <tr>
                                <td>延长时间：</td>
                                <td>
                                    <input type="text" class="lab_usetime" name="lab_usetime" /><span class="red">*</span>(小时)</td>
                            </tr>
                            <tr>
                                <td>申请报告：</td>
                                <td>
                                    <div style="text-decoration: underline; margin-bottom: 3px;"><a href="实验室延时使用申请书.rtf">下载延时申请报告模版</a></div>
                                    <div>
                                        <input type="file" name="re_file_name" id="re_file_name" /><span class="red">*</span></div>
                                    <div style="height: 24px; line-height: 24px;">
                                        <input type="button" style="cursor: pointer;" class="upload_file" file="re_file_name" value="上传" /><span class="cur_file_name color1"></span></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </form>
            </div>
        </div>
        <div class="box_tbl" style="position: relative; <%=isList%>">
            <div style="position: absolute; top: 5px; right: 5px;">
                <span onclick="pro.d.rtest.create()" class="button" style="display:<%=isLeader %>">创建项目</span>
                <span class="button" onclick="pro.d.rtest.srchRTest();">申请加入项目</span>
            </div>
            <div class="tabs">
                <ul class="tab_head">
                    <li style="display:<%=isLeader %>"><a>我管理的项目</a></li>
                    <li><a>我加入的项目</a></li>
                </ul>
                <div class="tab_con">
                    <div class="item">
                        <div class="box_tbl zebra">
                            <table>
                                <thead>
                                    <tr class="title tbl_head">
                                        <th style="width: 80px;">项目号</th>
                                        <th>项目名称</th>
                                        <th style="width: 80px;">项目主持人</th>
                                        <th style="width: 80px;">项目经费</th>
                                        <th style="width: 80px;">成员数量</th>
                                        <th style="width: 140px;">操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%=myRtList %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="item">
                        <div class="box_tbl zebra">
                            <table>
                                <thead>
                                    <tr class="title tbl_head">
                                        <th style="width: 80px;">项目号</th>
                                        <th>项目名称</th>
                                        <th style="width: 80px;">主持人</th>
                                        <th style="width: 80px;">负责人</th>
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
        </div>
        <div class="box_tbl" style="<%=isDetail%>">
            <table class="tblResvs zebra">
                <thead>
                    <tr>
                        <th>实验室类型</th>
                        <th>使用资格</th>
                        <th>项目实验</th>
                        <th>实验名称</th>
                        <th>总时长</th>
                        <th>单次时长</th>
                        <th>剩余时长</th>
                        <th>被试人数</th>
                        <th>预约</th>
                    </tr>
                </thead>
                <tbody>
                    <%=rtRoleList %>
                </tbody>
            </table>
        </div>
        <div id="qz_resv">
            <input type="hidden" id="curTestName" runat="server" />
            <Uni:tblresearch ID="MyRTCld" runat="server" />
        </div>
        <script type="text/javascript">
            uni.hr.loadSuccess(function () {
                $(".tabs").unitab();
            });
        </script>
    </div>

</body>
</html>
