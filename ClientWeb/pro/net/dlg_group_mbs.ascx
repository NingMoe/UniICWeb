<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dlg_group_mbs.ascx.cs" Inherits="ClientWeb_pro_net_dlg_group_mbs" %>
<!--成员管理-->
<div id="dlg_group_mbs" title="成员管理" class="dialog">
    <form name='dlg_group_mbs_f' id="dlg_group_mbs_f" onsubmit="return false;">
        <table>
            <tr>
                <td>组名称：</td>
                <td>
                    <div class="group_name"></div>
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
<script type="text/javascript">
    var dlg_group = {};//命名空间
    //删除成员
    dlg_group.delMb=function delMb(obj) {
        var group_id = $(obj).attr("groupid");
        var id = $(obj).attr("id");
        pro.j.rtest.delMem(group_id, id, function () {
            $(obj).parents("tr:first").hide();
        });
    }
    //组成员管理
    pro.d.group.MbM = function (group_id, name) {
        var dlgmb = $("#dlg_group_mbs");
        $(".group_name", dlgmb).html(name);
        $(".group_id", dlgmb).val(group_id);
        pro.j.group.getMbs(group_id, function (rlt) {
            var mbs = $(rlt.data);
            var list = "";
            var acc = pro.acc;
            mbs.each(function () {
                var mb = this;
                var del = mb.dwAccNo != acc.accno ? "<a class='click' groupid='" + mb.dwGroupID + "' id='" + mb.szPID + "'  onclick='dlg_group.delMb(this)'>删除</a>" : "";
                list += "<tr><td>" + mb.szPID + "</td><td>" + mb.szTrueName + "</td><td>" + mb.szDeptName + "</td><td>" +
                     del + "</td></tr>";
            });
            $(".group_id", dlgmb).val(group_id);
            $(".mb_list", dlgmb).html(list);
            uni.dlg(dlgmb, "组维护", 560, 200);
            $(".mb_name_ipt", dlgmb).procomplete(function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $(".add_mb_ipt", dlgmb).val(ui.item.szLogonName);
                    }
                }
            });
        });
    }
    $(function () {
        //成员管理
        var dlgmb = $("#dlg_group_mbs");
        $(".add_mb_bt", dlgmb).click(function () {
            var id = $(".add_mb_ipt", dlgmb).val();
            var group_id = $(".group_id", dlgmb).val();
            if (uni.isNull(id)) {
                uni.msgBox("请输入要添加成员的姓名！");
                return false;
            }
            pro.j.rtest.addMem(group_id, id, function (rlt) {
                var acc = rlt.data;
                var list = $(".mb_list", dlgmb);
                list.append("<tr><td>" + acc.id + "</td><td>" + acc.name + "</td><td>" + acc.dept + "</td>" +
                    "<td><a class='click' groupid='" + group_id + "' id='" + acc.id + "' onclick='dlg_group.delMb(this)'>" + "删除" + "</a></td></tr>");
            });
        });
    });
</script>