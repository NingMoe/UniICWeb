<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dlg_acc.ascx.cs" Inherits="ClientWeb_pro_net_dlg_acc" %>
<div style="display: none;">
    <!-- 搜索账户-->
    <div id="dlg_acc_get" title="搜索账户" class="dialog">
        <form id="dlg_acc_get_f" name="dlg_acc_get_f" onsubmit="return false;">
            <table>
                <tr>
                    <th>姓名</th>
                    <td>
                        <input name="acc_name" type="text" class="input_txt acc_name" act="truename" url="searchAccount.aspx" value="姓名关键字" onclick="if (this.value == '姓名关键字') { this.value = '' }" onblur="if(this.value=='') {this.value='姓名关键字';}" />
                    </td>
                </tr>
            </table>
            <table>
                <tbody class="acc_list">
                </tbody>
            </table>
            <div style="margin-top: 30px; text-align: center;">
                <input type="submit" class="button" value="搜索" />
            </div>
            <div style="text-align: center; color: red;">
                <p class="err_msg">
                    <!--失败提示-->
                </p>
            </div>
        </form>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        //搜索账户
        $("#dlg_acc_get_f").submit(function () {
            var dlg = $(this);
            var name = $(".acc_name", dlg).val();
            pro.j.acc.getAccByName(name, function (rlt) {
                var list = rlt.data;
                var list_str = "";
                $(list).each(function () {
                    list_str += "<tr><td>" + this.id + "</td><td class='tru_name'>" + this.name + "</td><td>(" + this.dept +
                        ")</td><td class='click' onclick=\"pro.j.acc.assignTutor('" + this.accno + "','" + this.name + "',function(){uni.msgBoxR('指定导师成功！');});\">选择</td></tr>";
                });
                $(".acc_list", dlg).html(list_str);
            },
            function (rlt) {
                $(".err_msg", dlg).html(rlt.msg);
            });
        });
    });
</script>
