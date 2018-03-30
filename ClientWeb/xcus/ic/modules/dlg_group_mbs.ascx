<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dlg_group_mbs.ascx.cs" Inherits="ClientWeb_pro_net_dlg_group_mbs" %>
<!--成员管理-->
<div id="dlg_group_mbs" title="成员管理" class="dialog">
    <form name='dlg_group_mbs_f' id="dlg_group_mbs_f" onsubmit="return false;">
        <table style="margin:15px 10px;width:95%;">
            <tr>
                <td></td>
                <td style="padding:10px 0 20px 0;">
                    <div class="group_name"></div>
                    <input type="hidden" name="group_id" class="group_id" />
                    <input type="hidden" class="resv_sta" />
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width:100%;">
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
        </table>
                    <div style="margin:10px 5px;">
                <span>添加成员：</span>
                <span>
                    <input class="mb_name_ipt input_txt" type="text" act="truename" url="searchAccount.aspx" value="学工号查找" onclick="if (this.value == '学工号查找') { this.value = '' }" onblur="if(this.value=='') {this.value='学工号查找';}" />
                    <input name="add_mb_ipt" class="add_mb_ipt" type="hidden" />
                    | <a class="add_mb_bt click2">添加</a>
                </span>
            </div>
        <div style="text-align: center;">
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
    //删除成员
    var dlg_group = {};//命名空间
    dlg_group.delMb = function delMb(obj) {
        if (dlg_group.flag == "no") {
            alert("不可删除");
            return;
        }
        var group_id = $(obj).attr("groupid");
        var id = $(obj).attr("id");
        //不检查最少人数
        pro.j.rtest.delMem(group_id, id, function () {
            $(obj).parents("tr:first").hide();
        });
        //检查最少人数
        //var len = $(obj).parents("table:first").find("tr").length-1;
        //pro.j.group.getGroupById(group_id, function (rlt) {
        //    var min = rlt.data.dwMinUsers;
        //    if (len > min) {
        //        pro.j.rtest.delMem(group_id, id, function () {
        //            $(obj).parents("tr:first").hide();
        //        });
        //    }
        //    else
        //        uni.msgBox("不能删除，至少需要"+min+"名成员");
        //});
    }
    //组成员管理
    pro.d.group.MbM = function (group_id, name,status) {
        var dlgmb = $("#dlg_group_mbs");
        $(".group_name", dlgmb).html(name);
        $(".group_id", dlgmb).val(group_id);
        $(".resv_sta", dlgmb).val(status);
        pro.j.group.getMbs(group_id, function (rlt) {
            var mbs = $(rlt.data);
            var list = "";
            var acc = pro.acc;
            mbs.each(function () {
                var mb = this;
                var del = ((mb.dwAccNo != acc.accno)&&(parseInt(status)&256)>0)? "<a class='click' groupid='" + mb.dwGroupID + "' id='" + mb.szPID + "'  onclick='dlg_group.delMb(this)'>删除</a>" : "";
                list += "<tr><td>" + mb.szPID + "</td><td>" + mb.szTrueName + "</td><td>" + mb.szDeptName + "</td><td>"+del+"</td></tr>";
            });
            $(".group_id", dlgmb).val(group_id);
            $(".mb_list", dlgmb).html(list);
            uni.dlg(dlgmb, "组维护", 560, 200);
        });
    }
    $(function () {
        //成员管理
        var dlgmb = $("#dlg_group_mbs");
        $(".add_mb_bt", dlgmb).click(function () {
            var id = $(".mb_name_ipt", dlgmb).val();
            var group_id = $(".group_id", dlgmb).val();
            var resv_sta = $(".resv_sta", dlgmb).val();
            if (uni.isNull(id)||id=="学工号查找") {
                uni.msgBox("请输入要添加成员的学工号！");
                return false;
            }
            pro.j.rtest.addMem(group_id, id, function (rlt) {
                var acc = rlt.data;
                var list = $(".mb_list", dlgmb);
                var del = (parseInt(resv_sta) & 256) > 0 ? "<a class='click' groupid='" + group_id + "' id='" + acc.id + "' onclick='dlg_group.delMb(this)'>" + "删除" + "</a>" : "";
                list.append("<tr><td>" + acc.id + "</td><td>" + acc.name + "</td><td>" + acc.dept + "</td>" +
                    "<td>"+del+"</td></tr>");
            });
        });
    });
</script>
<style type="text/css">
        #dlg_group_mbs table table {text-align:center;}
                #dlg_group_mbs table table th{text-align:center;}
    #dlg_group_mbs table table td {border:1px solid #aaa;}
    #dlg_group_mbs a {cursor:pointer;}
</style>