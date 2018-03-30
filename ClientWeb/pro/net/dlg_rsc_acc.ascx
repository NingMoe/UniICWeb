<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dlg_rsc_acc.ascx.cs" Inherits="ClientWeb_pro_net_dlg_rsc_acc" %>
<!-- 搜索账户-->
<div id="dlg_acc_get" title="搜索账户" class="dialog">
    <form id="dlg_acc_get_f" name="dlg_acc_get_f" onsubmit="return false;">
        <div class="list">
            <table>
                <tr>
                    <td>姓名：</td>
                    <td>
                        <input type="hidden" class="acc_accno" />
                        <input type="hidden" class="acc_id" />
                        <input type="hidden" class="acc_name" />
                        <input name="acc_rsc_name" type="text" class="input_txt acc_rsc_name" act="truename" url="searchAccount.aspx" value="姓名关键字" onclick="if (this.value == '姓名关键字') { this.value = '' }" onblur="if(this.value=='') {this.value='姓名关键字';}" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</div>
<script type="text/javascript">
    pro.d.acc.srchAccByName = function (okFun, ident) {
        var dlgacc = $("#dlg_acc_get");
        var acc_ipt = $(".acc_rsc_name", dlgacc);
        var id = $(".acc_id", dlgacc);
        var accno = $(".acc_accno", dlgacc);
        var name = $(".acc_name", dlgacc);
        if (ident)
            acc_ipt.attr("para", "ident=" + ident);
        acc_ipt.procomplete(function (event, ui) {
            if (ui.item && !uni.isNull(ui.item.id)) {
                id.val(ui.item.szLogonName);
                accno.val(ui.item.id);
                name.val(ui.item.name);
            }
            else {
                id.val("");
            }
        });
        uni.dlg(dlgacc, "查找账户", 380, 200, function () {
            if (uni.isNoNull(id.val())) {
                if (okFun) {
                    var acc = {};
                    acc.id = id.val();
                    acc.accno = accno.val();
                    acc.name = name.val();
                    okFun(acc,dlgacc);
                }
            }
            else {
                uni.msgBox("未找到");
            }
        });
    }
</script>
