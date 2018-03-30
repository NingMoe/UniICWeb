<%@ Control Language="C#" AutoEventWireup="true" CodeFile="userinfo.ascx.cs" Inherits="ClientWeb_pro_net_userinfo" %>
<script>
    $(function () {
        //初始化用户信息
        var acc = pro.acc;
        var info = $("#updateacc");
        $("#accName").html(acc.name);
        $("#accLgName").html(acc.id);
        $("#accColl").html(acc.dept);
        $("#accPhone").val(acc.phone);
        $("#accEmail").val(acc.email);
        if (acc.credit) {
            var rec = "<ul>";
            for (var i = 0; i < acc.credit.length; i++) {
                var it = acc.credit[i];
                var forbid = it[3] ? (" <span class='red'>"+uni.translate("禁用") + it[3]+"</span>") : "";
                rec+="<li>"+uni.translate(it[0])+"："+uni.translate("剩余")+it[1]+uni.translate("分/总")+it[2]+uni.translate("积分")+forbid+"</li>"
            }
            rec += "</ul>";
            $("#credit_score").html(rec);
        }
        //微信解除绑定
        if ("<%=GetConfig("bindWechat")%>" == "2") {
            var del = $(".act_del_wechat", info);
            var bound = $(".wechat_bound", info);
            del.click(function () {
                pro.j.acc.deleteWeChat(function () {
                    uni.msgBoxR("解除绑定成功");
                })
            });
            if ("<%=curAcc.szMSN%>" != "") {
                del.show();
                bound.hide();
            }
            else {
                del.hide();
                bound.show();
            }
        }
    });
    //更新联系方式
    function subUserInfoUpdate() {
        var para = {};
        if ($("#updateacc .note_alert:visible").length > 0)
            para.note_alert = $("#updateacc .note_alert").is(":checked");
        var vmsgreq="<%=msgMustAct%>";
        var phone = $("#accPhone").val();
        if (!uni.ckMobile(phone)&&vmsgreq=="required") { uni.msgBox("手机填写有误！"); return false; }
        var email = $("#accEmail").val();
        if (!uni.ckEmail(email)) { uni.msgBox("邮箱填写有误！"); return false; }
        pro.j.acc.upContact(phone, email, function () {
            uni.msgBoxR("保存成功");
        }, para);
    };
    //修改密码
    function subChangePwd() {
        var dlg = $("#updateacc");
        var old = $(".old_pwd", dlg).val();
        var pwd1 = $(".pwd1", dlg).val();
        var pwd2 = $(".pwd2", dlg).val();
        pro.j.acc.changePwd(old, pwd1, pwd2, function () {
            uni.msgBoxR("修改密码成功");
        });
    }
    //显示微信二维码
    function showWechatQrCode() {
        var qr = '<%=GetConfig("wechatQrCode")%>';
        var img = "<span>请使用微信扫二维码，绑定微信</span><div class='dft_qr_code'><img alt='' style='width:200px;height:200px;' src='"+qr+(qr.indexOf('?')<0?"?":"&")+"ID=" + pro.acc.id + "&session=<%=m_Request.m_UniDCom.SessionID%>'/></div>";
        uni.msgBox(img, "微信绑定", function () { uni.reload();});
    }

</script>
<style>
    #updateacc {width: 520px; margin: 20px auto; padding:5px;border: 1px solid #ccc; text-align: center; background-color: #fcfcfc;border-radius:4px;}
    #updateacc table {width:100%;background-color:#fff;margin-bottom:10px;}
    #updateacc table td{border:solid 1px #ddd;padding-left:5px;text-align:left;height:36px;line-height:36px;}
    #updateacc table td:first-child{padding-right:5px;text-align:right;}
    #updateacc table td.act_td{vertical-align:middle;text-align:center;background:#fafafa;padding:5px;}

    #credit_score ul {margin:2px;}
    #credit_score ul li {line-height:22px;font-size: 12px;
  border-bottom: 1px solid #ccc;padding-left:2px;}
</style>
                <div id="updateacc">
                    <div style="font-size:18px;font-weight:700;margin:5px;text-align:left;color:#ddd;">Contact Info</div>
                    <form onsubmit="return false;">
                        <table class="acc_tbl">
                            <tbody>
                            <tr>
                                <td style="width: 140px;"><span style="font-weight: 700;"><span class="uni_trans">姓名</span>：</span></td>
                                <td><span id="accName"></span></td>
                            </tr>
                            <tr>
                                <td><span style="font-weight: 700"><span class="uni_trans">帐号</span>：</span></td>
                                <td><span id="accLgName"></span></td>
                            </tr>
                            <tr>
                                <td><span style="font-weight: 700"><span class="uni_trans">部门</span>：</span></td>
                                <td><span id="accColl"></span></td>
                            </tr>
                                <%if (GetConfig("userCredit")=="1"){ %>
                            <tr class="tr_credit">
                                <td><span style="font-weight: 700"><span class="uni_trans">信用积分</span>：</span></td>
                                <td>
                                    <span id="credit_score"></span>
                                </td>
                            </tr>
                                <%} %>
                            <tr>
                                <td><span style="font-weight: 700"><span class="red"><%if (msgMustAct != "")
                                                                                         {%>*<%} %></span><span class="uni_trans">手机</span>：</span>
                                    
                                </td>
                                <td>
                                    <input type="text" id="accPhone" name="accPhone" />   <span style="font-size:12px">&nbsp<%=GetConfig("msgIntro")%></span> </td>
                            </tr>
                            <tr>
                                <td><span style="font-weight: 700"><span class="red">*</span><span class="uni_trans">邮箱</span>：</span></td>
                                <td>
                                    <input type="text" id="accEmail" name="accEmail" /></td>
                            </tr>
                <%if (GetConfig("bindWechat") == "1" || GetConfig("bindWechat") == "2"){%>
                <tr>
                    <td><span style="font-weight: 700"><span class="uni_trans">微信绑定</span>：</span></td>
                    <td>
                        <a onclick="showWechatQrCode()">点击显示二维码</a>
                        <span class="act_del_wechat" style="display: none;">| <span class="grey uni_trans">已绑定</span>(<a class="click">解除绑定</a>)</span>
                        <span class="wechat_bound" style="display: none;">| <span class="grey uni_trans">未绑定</span></span>
                    </td>
                </tr>
                <%} %>
                            <%if(GetConfig("optNoteAlert")=="1"){%> 
                            <tr>
                                <td><span style="font-weight: 700"><span class="uni_trans"><%=Translate("允许通知") %></span>：</span></td>
                                <td>
                                    <input type="checkbox" class="note_alert" name="note_alert" <%=checkAlert%>/></td>
                            </tr>
                            <%} %>
                                <%if (GetConfig("show2code") == "1") { %>
                            <tr>
                                
                                <td colspan="2" style="text-align:center">
                                  <div style="margin:auto 10px">
                                  <img style="width:100px" src="~/ClientWeb/pro/dft/2code.jpg" alt="公众号图片" runat="server" />
                                    <div>
                                        绑定微信号，接收微信通知
                                    </div>
                                    </div>
                                </td>
                            </tr>
                            <%} %>
                            <tr>
                                <td colspan="2" class="act_td"><span class="button btn btn-info uni_trans" onclick="subUserInfoUpdate();">保存</span></td>
                            </tr>
                            </tbody>
                            <tbody style="display:<%=changePsw%>">
                            <tr>
                                <td><span style="font-weight: 700"><span class="uni_trans">原密码</span>：</span></td>
                                <td>
                                    <input type="password" class="old_pwd"/></td>
                            </tr>
                            <tr>
                                <td><span style="font-weight: 700"><span class="uni_trans">新密码</span>：</span></td>
                                <td>
                                    <input type="password" class="pwd1" /></td>
                            </tr>
                            <tr>
                                <td><span style="font-weight: 700"><span class="uni_trans">重复密码</span>：</span></td>
                                <td><input type="password" class="pwd2" /></td>
                            </tr>
                            <tr>
                                <td colspan="2" class="act_td"><span class="button btn btn-info uni_trans" onclick="subChangePwd();">修改密码</span></td>
                            </tr>
                            </tbody>
                        </table>
                    </form>
                </div>