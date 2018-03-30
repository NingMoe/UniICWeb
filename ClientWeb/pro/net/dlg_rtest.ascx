<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dlg_rtest.ascx.cs" Inherits="ClientWeb_pro_net_dlg_rtest" %>
<!-- 项目信息 -->
<div id="dlg_rt_manage" title="项目信息" class="dialog" style="width: 400px;">
    <form name='dlg_rt_manage_f' id="dlg_rt_manage_f" onsubmit="return false;" style="width: 400px;">
        <table style="vertical-align: middle; width: 400px;">
            <tr>
                <th>项目名称：</th>
                <td>
                    <input class="input_txt rt_name" name="rt_name" type="text" />
                    <input class="rt_id" name="rt_id" type="hidden" />
                    <input class="group_id" name="group_id" type="hidden" />
                </td>
            </tr>
            <tr>
                <th>项目级别：</th>
                <td>
                    <select class="rt_level input_txt" name="rt_level">
                        <option value="4096">其它</option>
                        <option value="4">校级</option>
                        <option value="3">厅局级</option>
                        <option value="2">省部级</option>
                        <option value="1">国家级</option>
                    </select></td>
            </tr>
            <tr>
                <th>委托授权：</th>
                <td><span class="leader_name"></span>
                    <input type="hidden" class="leader_accno" name="leader_accno" />
                    <input type="hidden" class="leader_name" name="leader_name" />
                    <input type="hidden" class="leader_id" name="leader_id" />
                </td>
            </tr>
            <tr>
                <th>指定委托授权：</th>
                <td>
                    <input class="leader_name_ipt input_txt" type="text" act="truename" url="searchAccount.aspx" value="姓名关键字查找" onclick="if (this.value == '姓名关键字查找') { this.value = '' }" onblur="if(this.value=='') {this.value='姓名关键字查找';}" />
                    <input type="hidden" class="leader_id_ipt"/>
                    <a class="replace_bt button">指定</a></td>
            </tr>
        </table>
        <div class="submitarea clear" style="text-align: center; margin: 2px auto;">
            <a class="button sub_bt">保存</a><a class="button dlg_close">取消</a>
        </div>
        <div class="fail">
            <p class="err_msg">
                <!--错误提示-->
            </p>
        </div>
    </form>
</div>
<!--成员管理-->
<div id="dlg_mb_manage" title="成员管理" class="dialog">
    <form name='dlg_mb_manage_f' id="dlg_mb_manage_f" onsubmit="return false;">
        <table>
            <tr>
                <th>项目名称：</th>
                <td>
                    <div class="rt_name"></div>
                    <input class="rt_id" name="rt_id" type="hidden" />
                    <input type="hidden" name="group_id" class="group_id" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 520px;">
                        <thead>
                            <tr>
                                <th>帐号</th>
                                <th>姓名</th>
                                <th>部门</th>
                                <th>状态</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody class="mb_list">
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <th>添加成员：</th>
                <td>
                    <input class="mb_name_ipt input_txt" type="text" act="truename" url="searchAccount.aspx" value="姓名关键字查找" onclick="if (this.value == '姓名关键字查找') { this.value = '' }" onblur="if(this.value=='') {this.value='姓名关键字查找';}" />
                    <input name="add_mb_ipt" class="add_mb_ipt" type="hidden" />
                    | <a class="add_mb_bt button">添加</a>
                </td>
            </tr>
        </table>
        <div class="submitarea clear" style="text-align: center;">
            <a class="button dlg_close_r">关闭</a>
        </div>
        <div class="fail">
            <p class="err_msg">
                <!--错误提示-->
            </p>
        </div>
    </form>
</div>
<!--搜索项目-->
<div id="dlg_rt_search" class="dialog">
    <div style="min-height: 320px;overflow:hidden;">
    <table>
        <tr>
            <th style="text-align:right;">项目名称：</th>
            <td>
                <input class="rt_name" name="rt_name" type="text"  style="width:200px;"/>
            </td>
        </tr>
        <tr>
            <th style="text-align:right;">项目负责人姓名：</th>
            <td>
                <input class="sel_tutor" type="text" act="truename" url="searchAccount.aspx" para="tutor=true" style="width:200px;"/>
                <input class="tutor_id" name="tutor_id" type="hidden" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; border-top: 1px solid #666;">
                <span class="button" onclick="dlg_rtest.srchTR ()">搜索</span>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 470px;">
                    <thead>
                        <tr>
                            <th>项目名称</th>
                            <th>负责人</th>
                            <th>授权委托</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody class="rt_list">
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
        </div>
    <div class="submitarea clear" style="text-align: center;">
        <a class="button dlg_close_r">关闭</a>
    </div>
</div>
<script type="text/javascript">
    var dlg_rtest = {};//命名空间
    //项目搜索
    dlg_rtest.srchTR = function () {
        var dlgsrch = $("#dlg_rt_search");
        var id = "";
        var name = $(".rt_name", dlgsrch).val();
        var tutor_id = $(".tutor_id", dlgsrch).val();
        var leader_id = "";
        if (uni.isNull(tutor_id) && uni.isNull(name)) {
            uni.msgBox("请输入搜索条件！");
            return;
        }
        pro.j.rtest.srchRTest("", name, tutor_id, leader_id, function (rlt) {
            var list = rlt.data;
            var str = "";
            var id = pro.acc.id;
            $(list).each(function () {
                var test = this;
                str += "<tr><td>" +uni.cutStrT(test.rt_name,20) + "</td><td>" + test.rt_tutor +
                    "</td><td>" + test.leader_name +
                    "</td><td><a rtid='" + test.rt_id + "' groupid='" +
                    test.group_id + "' id='" + id + "' class='click' onclick='dlg_rtest.addMb(this,\"apply\")'>申请加入</a></td></tr>"
            });
            $(".rt_list", dlgsrch).html(str);
        });
    }
    //成员操作
    function delMb(obj) {
        var rt_id = $(obj).attr("rtid");
        var group_id = $(obj).attr("groupid");
        var id = $(obj).attr("id");
        pro.j.rtest.delRTMem(rt_id, group_id, id, function () {
            $(obj).parents("tr:first").hide();
        });
    }
    dlg_rtest.addMb = function (obj, type) {
        var mb = $(obj)
        var rt_id = mb.attr("rtid");
        var group_id = mb.attr("groupid");
        var id = mb.attr("id");
        pro.j.rtest.addRTMem(rt_id, group_id, id, function () {
            if (type == "add") {
                mb.parent().hide();
                var stat = "<span style='color:green'>已加入</span>";
                mb.parents("tr:first").find("td.stat").html(stat);
            }
            else if (type == "apply") {
                uni.msgBox("已申请加入，请等待批准。");
                mb.html("已申请");
            }
        });
    }
    pro.d.rtest.rtInfoM = function (id) {
        pro.j.rtest.getRTest(id, function (rlt) {
            var dlgrt = $("#dlg_rt_manage");
            var test = rlt.data;
            $(".rt_id", dlgrt).val(test.rt_id);
            $(".rt_name", dlgrt).val(test.rt_name);
            $(".rt_level", dlgrt).val(test.rt_level);
            $(".group_id", dlgrt).val(test.group_id);
            $(".leader_accno", dlgrt).val(test.leader_accno);
            $("span.leader_name", dlgrt).html(test.leader_name);
            $("input.leader_name", dlgrt).val(test.leader_name);
            dlgrt.dialog({ minWidth: 415, autoOpen: false, modal: true, minHeight: 190, bgiframe: true });
            dlgrt.dialog("open");
            $(".leader_name_ipt", dlgrt).procomplete(function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $(".leader_id_ipt", dlgrt).val(ui.item.szLogonName);
                    }
                }
            });
        });
    }
    pro.d.rtest.rtMbM = function (rt_id, rt_name) {
        var dlgmb = $("#dlg_mb_manage");
        $(".rt_name", dlgmb).html(rt_name);
        $(".rt_id", dlgmb).val(rt_id);
        pro.j.rtest.getRTMem(rt_id, function (rlt) {
            var mbs = $(rlt.data);
            var list = "";
            mbs.each(function () {
                var mb = this;
                var ck = mb.rtstatus == "no" ? "<span><a class='click' rtid='" + mb.rtid + "' id='" + mb.id + "'  onclick='dlg_rtest.addMb(this,\"add\")'>" + "批准" + "</a>|</span>" : "";
                var sta = mb.rtstatus == "yes" ? "<span style='color:green'>已加入<span>" : "<span style='color:red'>未加入</span>";
                var del = mb.ident != "special" ? "<a class='click' rtid='" + mb.rtid + "' groupid='" + mb.groupid + "' id='" + mb.id + "'  onclick='delMb(this)'>删除</a>" : "";
                var out = mb.ident == "out" ? "<span style='color:green'>[注册]</span>" : "";
                list += "<tr><td>" + mb.id + "</td><td>" + out + mb.name + "</td><td>" + mb.dept + "</td><td class='stat'>" + sta + "</td><td>" +
                    ck + del + "</td></tr>";
            });
            $(".group_id", dlgmb).val(rlt.ext);
            $(".mb_list", dlgmb).html(list);
            dlgmb.dialog({ minWidth: 540, autoOpen: false, modal: true, minHeight: 400, bgiframe: true });
            dlgmb.dialog("open");
            $(".mb_name_ipt", dlgmb).procomplete(function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $(".add_mb_ipt", dlgmb).val(ui.item.szLogonName);
                    }
                }
            });
        });
    }
    //搜索项目
    pro.d.rtest.srchRTest = function () {
        var dlgsrch = $("#dlg_rt_search");
        //$(".srchTutor", dlgsrch).click(function () {
        //    pro.d.acc.srchTutor(function (accno, name, d) {
        //        $(".tutor_id", dlgsrch).val(accno);
        //        $(".tutor_name", dlgsrch).html(name);
        //        $(d).dialog("close");
        //    });
        //});
        uni.dlgR(dlgsrch, "搜索项目", 500, 400);
        $(".sel_tutor", dlgsrch).procomplete(function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    $(".tutor_id", dlgsrch).val(ui.item.id);
                    dlg_rtest.srchTR();
                }
            }
        });
    }
    $(function () {
        //关闭窗口
        $(".dlg_close_r").click(function () {
            $(this).parents(".dialog:first").dialog("close");
            uni.tab.reload();
        });
        $(".dlg_close").click(function () {
            $(this).parents(".dialog:first").dialog("close");
        });
        //项目信息
        var dlgrt = $("#dlg_rt_manage");
        $(".replace_bt", dlgrt).click(function () {
            var id = $(".leader_id_ipt", dlgrt).val();
            if (uni.isNull(id)) {
                uni.msgBox("请输入要指定的人的姓名！");
                return false;
            }
            pro.j.acc.getAccById(id, function (rlt) {
                var acc = (rlt.data)[0];
                $(".leader_accno", dlgrt).val(acc.accno);
                $("span.leader_name", dlgrt).html(acc.name);
                $("input.leader_name", dlgrt).val(acc.name);
                $(".leader_id", dlgrt).val(acc.id);
            });
        });
        $(".sub_bt", dlgrt).click(function () {
            pro.j.rtest.fRTest("up_rt_info", $("#dlg_rt_manage_f"), function () {
                uni.msgBoxRT("保存成功！");
            });
        });
        //成员管理
        var dlgmb = $("#dlg_mb_manage");
        $(".add_mb_bt", dlgmb).click(function () {
            var id = $(".add_mb_ipt", dlgmb).val();
            var rt_id = $(".rt_id", dlgmb).val();
            var group_id = $(".group_id", dlgmb).val();
            if (uni.isNull(id)) {
                uni.msgBox("请输入要添加成员的姓名！");
                return false;
            }
            pro.j.rtest.addRTMem(rt_id, group_id, id, function (rlt) {
                var acc = rlt.data;
                var list = $(".mb_list", dlgmb).html();
                list += "<tr><td>" + acc.id + "</td><td>" + acc.name + "</td><td>" + acc.dept + "</td><td><span style='color:green'>已加入</span></td>" +
                    "<td><a class='click' rtid='" + rt_id + "' groupid='" + group_id + "' id='" + acc.id + "' onclick='delMb(this)'>" + "删除" + "</a></td></tr>";
                $(".mb_list", dlgmb).html(list);
            });
        });
    });
</script>
