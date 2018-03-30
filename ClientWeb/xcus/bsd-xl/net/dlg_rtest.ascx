<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dlg_rtest.ascx.cs" Inherits="ClientWeb_pro_net_dlg_rtest" %>
<!-- 新建实验项目 -->
<div id="dlg_rt_addrtdialog" title="新建科研项目" class="dialog">
    <form name='addrt' id="addrtForm" onsubmit="return false;" class="dlg">
        <table style="vertical-align: middle;">
            <tr>
                <td>项目来源：</td>
                <td>
                    <select class="rt_level input_txt" name="rt_level">
                        <option value="21">其它科研项目</option>
                        <option value="5">国家社科基金单列学科项目</option>
                        <option value="6">国家社科基金项目</option>
                        <option value="7">教育部人文社科研究项目</option>
                        <option value="8">全国教育科学规划（教育部）项目</option>
                        <option value="9">国家自然科学基金项目</option>
                        <option value="10">中央其他部门社科专门项目</option>
                        <option value="11">高校古籍整理研究项目</option>
                        <option value="12">省、市、自治区社科基金项目</option>
                        <option value="13">省教育厅社科项目</option>
                        <option value="14">地、市、厅、局等政府部门项目</option>
                        <option value="15">国际合作研究项目</option>
                        <option value="16">与港、澳、台合作研究项目</option>
                        <option value="17">企事业单位委托项目</option>
                        <option value="18">外资项目</option>
                        <option value="19">学校社科项目</option>
                        <option value="20">学生毕业论文</option>
                    </select></td>
            </tr>
            <tr>
                <td>项目号：</td>
                <td>
                    <input name="rt_sn" class="rt_sn input_txt" type="text" /><span class="red">*</span>
                </td>
            </tr>
            <tr>
                <td>项目名称：</td>
                <td>
                    <input type="hidden" name="holder_name" />
                    <input type="hidden" name="holder_id" />
                    <input id="new_rtname" name="rt_name" type="text" class="input_txt" /><span class="red">*</span></td>
            </tr>
            <tr>
                <td>项目主持人：</td>
                <td>
                    <input class="rt_tutor input_txt" type="text" act="truename" url="searchAccount.aspx" para="ident=16777728" value="姓名关键字查找" onclick="if (this.value == '姓名关键字查找') { this.value = '' }" onblur="if(this.value=='') {this.value='姓名关键字查找';}" />
                    <span class="red">*</span>
                </td>
            </tr>
            <tr>
                <td>项目经费：</td>
                <td>
                    <input name="rt_fee" class="rt_fee input_txt" type="text" />(选填/万元)
                </td>
            </tr>
            <tr>
                <td>添加成员：</td>
                <td>
                    <input type="hidden" name="mb_list" />
                    <input class="mb_name_ipt input_txt" type="text" act="truename" url="searchAccount.aspx" value="姓名关键字查找" onclick="if (this.value == '姓名关键字查找') { this.value = '' }" onblur="if(this.value=='') {this.value='姓名关键字查找';}" />
            </tr>
            <tr>
                <td colspan="2">
                    <ul id="dlg_rt_memList" class="ul_member memList">
                    </ul>
                </td>
            </tr>
        </table>
        <div class="submitarea clear" style="text-align: center;">
            <a id="dlg_rt_addrt" class="detail button">提交</a>
        </div>
        <div class="grey" style="text-align: center; padding: 3px;">
            可暂不添加成员
        </div>
    </form>
</div>
<!--成员管理-->
<div id="dlg_mb_manage" title="成员管理" class="dialog">
    <form name='dlg_mb_manage_f' id="dlg_mb_manage_f" onsubmit="return false;">
        <div style="font-size:14px;font-weight:bold;font-family:'Microsoft YaHei';margin-bottom:10px;">
                <span>项目名称：</span>
                <span>
                    <span class="rt_name"></span>
                    <input class="rt_id" name="rt_id" type="hidden" />
                    <input type="hidden" name="group_id" class="group_id" />
                </span>
        </div>
        <table>
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
                <td>添加成员：</td>
                <td>
                    <input class="mb_name_ipt input_txt" type="text" act="truename" url="searchAccount.aspx" value="姓名关键字查找" onclick="if (this.value == '姓名关键字查找') { this.value = '' }" onblur="if(this.value=='') {this.value='姓名关键字查找';}" />
                    <input name="add_mb_ipt" class="add_mb_ipt" type="hidden" />
                    | <a class="add_mb_bt click2">添加</a>
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
    <div style="min-height: 320px; overflow: hidden;">
        <table>
            <tr>
                <td style="text-align: right;">项目名称：</td>
                <td>
                    <input class="rt_name" name="rt_name" type="text" style="width: 200px;" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">项目主持人姓名：</td>
                <td>
                    <input class="sel_tutor" type="text" act="truename" url="searchAccount.aspx" para="ident=16777728" style="width: 200px;" onclick="this.value=''"/>
                    <input class="tutor_id" name="tutor_id" type="hidden" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; border-top: 1px solid #ccc;">
                    <span class="button" onclick="dlg_rtest.srchTR ()">搜索</span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 470px;">
                        <thead>
                            <tr>
                                <th>项目名称</th>
                                <th>主持人</th>
                                <th>负责人</th>
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
<%--    <div class="submitarea clear" style="text-align: center;">
        <a class="button dlg_close_r">关闭</a>
    </div>--%>
</div>
<script type="text/javascript">
    var dlg_rtest = {};//命名空间
    //项目搜索
    dlg_rtest.srchTR = function () {
        var dlgsrch = $("#dlg_rt_search");
        var id = "";
        var name = $(".rt_name", dlgsrch).val();
        var tutor_id =$(".sel_tutor",dlgsrch).val()?$(".tutor_id", dlgsrch).val():"";
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
                str += "<tr><td>" + uni.cutStrT(test.rt_name, 20) + "</td><td>" + test.rt_tutor +
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
    //新建项目
    pro.d.rtest.create = function () {
        if (!pro.isloginL())
            return;
        var dlgnew = $("#dlg_rt_addrtdialog");

        dlgnew.dialog({ width: 460, autoOpen: true, modal: true, minHeight: 302, bgiframe: true });
        $(".mb_name_ipt", dlgnew).procomplete(function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    var list = $("#dlg_rt_memList").html();
                    list += "<li><input type='hidden' name='memid' value='" + ui.item.szLogonName + "' /><span>" + ui.item.label + "</span>|<a class='click' onclick='$(this).parent().hide();return false;'>删除</a></li>";
                    $("#dlg_rt_memList").html(list);
                }
            }
        });
        $(".rt_tutor", dlgnew).procomplete(function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    $("input[name=holder_name]", dlgnew).val(ui.item.name);
                    $("input[name=holder_id]", dlgnew).val(ui.item.id);
                }
            }
        });
        $("#dlg_rt_addrt").click(function () {
            var rtname = $.trim($("#new_rtname").val());
            var sn = $.trim($(".rt_sn", dlgnew).val());
            if (sn == "") {
                uni.msgBox("请输入项目号！");
                return false;
            }
            if (rtname == "") {
                uni.msgBox("请输入项目名称！");
                return false;
            }
            if ($("input[name=holder_id]", dlgnew).val() == "") {
                uni.msgBox("项目主持人无效！");
                return false;
            }
            var list = "";
            $("#dlg_rt_memList input[name=memid]").each(function () {
                if (!$(this).parent().is(':hidden')) {
                    list += $(this).val() + ',';
                }
            });
            $("input[name=mb_list]", dlgnew).val(list);
            pro.j.rtest.fRTest("new", $("form", dlgnew), function () {
                uni.msgBoxR("创建项目成功！");
                dlgnew.dialog("close");
            });
        });
    }
    //项目成员管理
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
        uni.dlgR(dlgsrch, "搜索项目", 500, 400);
        $(".sel_tutor", dlgsrch).procomplete(function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    $(".tutor_id", dlgsrch).val(ui.item.id);
                    //dlg_rtest.srchTR();
                }
            }
        });
    }
    $(function () {
        //关闭窗口
        $(".dlg_close_r").click(function () {
            $(this).parents(".dialog:first").dialog("close");
            uni.reload();
        });
        $(".dlg_close").click(function () {
            $(this).parents(".dialog:first").dialog("close");
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
