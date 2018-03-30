<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true" CodeFile="manage.aspx.cs" MasterPageFile="../pageMaster.master" Inherits="ClientWeb_pro_page_group_manage" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../../fm/jquery-ui/bootstrap/js/bootstrap.js"></script>
    <link rel="stylesheet" href="../../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <style media="print">
        #group_detail_panel .no_print { display: none; }
        #group_detail_panel .print_content { display: block; margin-top: 16px; }
        #group_detail_panel .print_content:after { content: ""; }
    </style>
    <script>
        function delSubmit() {
            var user_list = $("#del_user_list");
            var cls_list = $("#del_cls_list");
            var min = parseInt($(".min_user").val());
            if ($(".unitab tr.sel.my").length > 0) {//检查本人
                uni.msgBox("不能删除本人");
                return;
            }
            if ((parseInt($(".mb_num").val()) - $(".unitab tr.sel[kind=2]").length) < min) {//只检查了个人
                uni.msgBox("删除失败，需剩余至少" + min + "名成员");
                return;
            }
            $(".unitab tr.sel").each(function () {
                var tr = $(this);
                var id = tr.attr("key");
                var kind = tr.attr("kind");
                if (kind == "2")
                    user_list.val(user_list.val() + "," + id);
                else if (kind == "1")
                    cls_list.val(cls_list.val() + "," + id);
            });
            PostBack();
        }
        function submit() {
            var req = uni.getReq();
            if (!req["test_id"]) return;//非教学组管理 不处理分组
            //处理分组
            var key = req["dlg_key"];
            parent.uni.dlgInst[key].group_id = "";
            parent.uni.dlgInst[key].group_name = "临时分组";
            //成员列表
            var list = $(".unitab tbody tr.it.sel");
            var arr = [];
            list.each(function () {
                arr.push($(this).attr("key"));
            });
            parent.uni.dlgInst[key].group_num = list.length;
            parent.uni.dlgInst[key].mb_acc_list = "&" + arr.join();
        }
        //function delMb(mb) {
        //    var bt=$(mb);
        //    bt.hide();
        //    bt.siblings().show();
        //    bt.parents('tr:first').addClass("del");
        //}
        //function revoke(mb) {
        //    var bt=$(mb);
        //    bt.hide();
        //    bt.siblings().show();
        //    bt.parents('tr:first').removeClass("del");
        //}
        function setGroupName(id) {
            var gn = $(".ipt_group_name");
            gn.val($(".group_name").val());
            var d = $("#dlg_group_name");
            uni.dlg(d, "修改名称", 360, 200, function () {
                pro.j.group.setGroupName($(".group_id").val(), gn.val(), function () {
                    PostBack();
                });
            });
        }
        $(function () {
            //if (parent.pro.term.end) {
            //    $("#pg_class").attr("para","line:"+parent.pro.term.end);
            //}
            $("#pg_class").procomplete(function (event, ui) {
                var it = ui.item;
                if (it && it.id) {
                    $("#pg_class_id").val(it.id);
                    $("#pg_class").val(it.name);
                    $(".add_class").trigger("click");
                }
            });
            $("#pg_user").procomplete(function (event, ui) {
                var it = ui.item;
                if (it && it.id) {
                    $("#pg_user_accno").val(it.id);
                    $(".add_user").trigger("click");
                }
            });
            if ($(".group_id").val()) {
                var req = uni.getReq();
                var key = req["dlg_key"];
                parent.uni.dlgInst[key].group_id = $(".group_id").val();
                parent.uni.dlgInst[key].group_name = $(".group_name").val();
                parent.uni.dlgInst[key].group_num = $(".mb_num").val();
                $(".sp_mb_num").html($(".mb_num").val());
            }
            //注册toggle
            $(".unitab .item_toggle").click(function () {
                var tr = $(this).parents("tr:first");
                if (tr.hasClass("sel"))
                    tr.removeClass("sel");
                else
                    tr.addClass("sel");
                selNum();
            });
            //详细组信息
            $(".show_detail").click(function () {
                $("#main_panel").hide();
                $("#group_detail_panel").show();
            });
            $(".hide_detail").click(function () {
                $("#main_panel").show();
                $("#group_detail_panel").hide();
            });
            //导入学生 上传控件
            $("#import_mbs_panel .import_file").uploadFile({}, function (rlt) {
                if (rlt.data) {
                    var arr = rlt.data.array;
                    var str = "";
                    var mbs = "";
                    for (var i = 0; i < arr.length; i++) {
                        str += "<tr><td>" + i + "</td>";
                        for (var j = 0; j < arr[i].length; j++) {
                            str += "<td>" + arr[i][j] + "</td>";
                            if (i > 0 && j == 0) mbs += arr[i][j] + ",";
                        }
                        str += "</tr>";
                    }
                    if (mbs.length > 0) mbs = mbs.substr(0, mbs.length - 1);
                    $("#import_mbs_panel .import_content").html(str);
                    //操作
                    var sub = $("<button class='btn btn-info'>确认导入</button>");
                    sub.click(function () {
                        pro.j.group.addMbs($(".group_id").val(), mbs, function (rlt) {
                            uni.msgBox("已成功导入：" + rlt.data + "位成员", "", function () {
                                PostBack();
                            });
                        });
                    });
                    $("#import_mbs_panel .import_submit").html(sub);
                }
            });
            $(".show_import").click(function () {
                $("#main_panel").hide();
                $("#import_mbs_panel").show();
            });
            $(".hide_import").click(function () {
                $("#import_mbs_panel").hide();
                $("#main_panel").show();
            });
        });
        function showAll(i) {
            var list = $(".unitab tbody tr.done");
            if ($(i).find("input").is(":checked")) {
                list.addClass("it");
            }
            else {
                list.hide();
                list.removeClass("it");
            }
            $(".tab_con").pctrl($(".tab_head"), 10);
            selNum();
        }
        function selAll(i) {
            var list = $(".unitab tbody tr.it");
            if ($(i).find("input").is(":checked")) {
                list.addClass("sel");
            }
            else {
                list.removeClass("sel");
            }
            selNum();
        }
        function selItem(n) {
            var list = $(".unitab tbody tr.it");
            var v = $(n).val();
            if (isNaN(v)) return;
            list.each(function (i) {
                if (i < v)
                    $(this).addClass("sel");
                else
                    $(this).removeClass("sel");
            });
            selNum();
        }
        function selNum() {
            var list = $(".unitab tbody tr.it.sel");
            $(".sel_mb_num").html(list.length);
        }
    </script>
    <style>
        .unitab .pagination { margin: 2px 4px; }
        .unitab .sel td:first-child { background-color: #D9EDF7; }
        .unitab .sel .glyphicon { color: yellowgreen; }
        #pg_group_name { border-bottom: 1px dotted #ddd; margin-bottom: 5px; }
        .input_group .input-group { }
        .tab_con { margin-bottom: 5px; }
        .tab_con.table-condensed tr td { padding: 3px 5px; }
        .unitab tr.done { display: none; }
        #import_mbs_panel table { width: 100%; }
        #import_mbs_panel tr td:first-child { width: 20px; }
        #import_mbs_panel td { border: 1px solid #ddd; }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
            <form class="form" role="form" runat="server">
    <div id="main_panel" style="display:<%=Request["readonly"]=="true"?"none":""%>">
        <div class="dialog" id="dlg_group_name">
            <div class="list">
                <table>
                    <tbody>
                        <tr>
                            <td class="uni_trans">名称</td>
                            <td>
                                <input type="text" class="ipt_group_name" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

            <input type="hidden" runat="server" id="group_id" name="group_id" class="group_id" />
            <input type="hidden" runat="server" id="group_name" name="group_name" class="group_name" />
            <input type="hidden" runat="server" id="mb_num" name="mb_num" class="mb_num" />
            <input type="hidden" runat="server" id="min_user" name="min_user" class="min_user" />
            <input type="hidden" id="del_user_list" name="del_user_list" class="del_user_list" />
            <input type="hidden" id="del_cls_list" name="del_cls_list" class="del_cls_list" />
            <input type="hidden" id="pg_class_id" name="class_id" class="class_id" />
            <input type="hidden" id="pg_user_accno" name="user_accno" class="user_accno" />
            <div class="panel panel-default" style="min-height: 400px; display: block;">
                <div class="panel-body" style="padding: 3px 15px; min-height: 100px;">
                    <div class="<%=testId==null?"":"hidden" %>">
                        <div id="pg_group_name"><span style="font-weight: bold;"><span class="uni_trans">名称</span>：</span><%=groupName %>&nbsp;<span class="text-primary click uni_trans" onclick="setGroupName()">修改</span></div>
                        <div style="min-height: 50px;" class="input_group">
                            <div class="input-group" style="margin-bottom: 4px; display: <%=hideCls%>">
                                <span class="input-group-addon uni_trans">添加班级</span>
                                <input type="text" class="form-control hint" id="pg_class" name="class_name" placeholder="<%=Translate("班级名称搜索")%>" url="searchCls.aspx">
                            </div>
                            <button type="button" class="btn btn-info add_class hidden" id="add_class" runat="server" onserverclick="add_class_ServerClick"><%=Translate("添加班级") %></button>
                            <div class="input-group">
                                <span class="input-group-addon uni_trans">添加个人</span>
                                <input type="text" class="form-control hint" id="pg_user" name="user_name" placeholder="<%=Translate(szSearchKey) %>" url="searchAccount.aspx">
                            </div>
                            <button type="button" class="btn btn-info add_user hidden" id="add_user" runat="server" onserverclick="add_user_ServerClick"><%=Translate("添加个人") %></button>
                        </div>
                    </div>
                    <div class="<%=testId==null?"hidden":"" %>">
                        <strong class="text-primary">请选择参与本次预约的成员：</strong>
                        <div class="grey">默认显示符合条件的成员 &nbsp;&nbsp;<label onclick="showAll(this);" class="show_all"><input type="checkbox" />显示全部成员</label></div>
                        <div class="text-right" style="margin-top: 15px;">
                            <label onclick="selAll(this);" class="sel_all">
                                <input type="checkbox" />全选</label>
                            | 选中前
                            <input type="text" style="width: 40px;" onchange="selItem(this);" />
                            人
                        </div>
                    </div>
                </div>
                <div class="unitab">
                    <div style="min-height: 310px;">
                        <table class="table table-striped table-bordered table-condensed tab_con">
                            <thead>
                                <tr>
                                    <th><span class="text-primary"><span class="uni_trans">成员列表</span>（<span class="uni_trans">人数</span>:<span class="sp_mb_num red"></span>&nbsp;<span class="uni_trans">已选</span>:<span class="sel_mb_num red"></span>&nbsp;<%=groupVol %>）</span></th>
                                    <th class="text-center" style="width: 18%;"><span class="uni_trans">选择</span></th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=mbList %>
                            </tbody>
                        </table>
                    </div>
                    <ul class="pagination pagination-sm tab_head">
                    </ul>
                </div>
                <script>
                    showAll($(".show_all"));
                </script>
            </div>

        <div class="text-center">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 40%;">
                        <div class="btn-group  pull-left"><%if (testId == null)
                                                            { %>
                            <button type="button" class="show_import btn btn-default"><%=Translate("导入成员") %></button><%}
                                                            else
                                                            {%>
                            <button type="button" class="btn btn-default  dlg_page_close"><%=Translate("全班上课") %></button><%} %></div>
                    </td>
                    <td>
                        <button type="button" class="btn btn-info  dlg_page_close" onclick="submit();"><%=Translate("确定") %></button></td>
                    <td style="width: 40%;">
                        <div class="btn-group  pull-right">
                            <%=mbDetail!=""?"<button type='button' class='btn btn-default show_detail uni_trans'>成员详情</button>":"" %>
                            <button type="button" class="btn btn-default  <%=testId==null?"":"hidden" %>" onclick="delSubmit();"><%=Translate("删除已选") %></button>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="group_detail_panel"  style="display:<%=Request["readonly"]=="true"?"":"none"%>">
        <div class="no_print" style="display:<%=Request["readonly"]=="true"?"none":""%>"><a class="hide_detail" style="margin: 10px; line-height: 30px; font-size: 16px; cursor: pointer; text-decoration: none;"><span class="glyphicon glyphicon-chevron-left"></span>返回</a></div>
        <div class="print_content">
            <table class="table table-striped table-bordered table-condensed detail_list">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>学号</th>
                        <th>班级</th>
                        <th>学院</th>
                    </tr>
                </thead>
                <tbody>
                    <%=mbDetail %>
                </tbody>
            </table>
        </div>
        <div class="text-center no_print">
            <div class="btn-group">
                <button type="button" class="btn btn-info" onclick="window.print();">打印</button>
                <button type="button" class="btn btn-info" runat="server" id="exportFile" onserverclick="exportFile_ServerClick">导出</button>
            </div>
        </div>
<%--        <ul class="pagination pagination-sm detail_ctrl">
        </ul>--%>
    </div>
        </form>
    <div id="import_mbs_panel" style="display: none; margin: 2px 5px;">
        <div><a class="hide_import" style="margin: 10px; line-height: 30px; font-size: 16px; cursor: pointer; text-decoration: none;"><span class="glyphicon glyphicon-chevron-left"></span><%=Translate("返回") %></a></div>
        <h3>1 <small class="uni_trans">下载导入文件模版</small></h3>
        <div><a href="import_template.csv" class="uni_trans">点击下载导入文件模版</a> <%=Translate("文件为csv格式 ，建议使用office打开")%></div>
        <div class="line"></div>
        <h3>2 <small class="uni_trans">上传导入文件</small></h3>
        <div style="margin: 10px;">
            <input type="button" class="import_file" value="<%=Translate("上传") %>" limit="csv" act="import_acc" />
        </div>
        <div class="line"></div>
        <h3>3 <small class="uni_trans">解析导入文件结果</small></h3>
        <div class="red text-center uni_trans">请检查无误，再确认导入</div>
        <div>
            <table>
                <tbody class="import_content"></tbody>
            </table>
        </div>
        <div class="text-center import_submit" style="margin-top: 10px;">
        </div>
    </div>
</asp:Content>


