<%@ Page Language="C#" AutoEventWireup="true" CodeFile="clsdetail.aspx.cs" Inherits="ClientWeb_xcus_all_detail" %>

<%@ Register TagPrefix="Uni" TagName="calendar" Src="~/ClientWeb/pro/net/calendar.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <form runat="server">
        <input runat="server" type="hidden" class="class_id" name="class_id" id="classId" />
        <input runat="server" type="hidden" class="islong" name="islong" id="isLong" value="false" />
        <input runat="server" type="hidden" class="isKind" name="iskind" id="isKind" value="false" />
        <input runat="server" type="hidden" class="class_kind" name="class_kind" id="classKind" />
        <input runat="server" type="hidden" class="class_name" name="class_name" id="className" />
    </form>
    <div>
        <div class="click btn_back" onclick="uni.hr.back();" style="display: <%=Request["back"]=="true"?"":"none"%>"><span class="glyphicon glyphicon-chevron-left"></span>&nbsp;<span class="uni_trans">返回</span></div>
        <div>
            <h1 class="h_title"><%=infoTitle %></h1>
            <div style="margin: 10px; margin-bottom: 20px; overflow: hidden;">
                <%=infoIntro %>
            </div>
        </div>
        <div class="info_unitab">
            <ul class="tab_head">
                <li>
                    <div class="title"><%=Translate("预约状态") %></div>
                    <div class="caret"></div>
                </li>
                <li class="hidden">
                    <div class="title"><%=Translate("预约须知") %></div>
                    <div class="caret"></div>
                </li>
                <li>
                    <div class="title"><%=Translate("硬件配置") %></div>
                    <div class="caret"></div>
                </li>
                <li>
                    <div class="title"><%=Translate("相册展示") %></div>
                    <div class="caret"></div>
                </li>
            </ul>
            <div class="tab_con">
                <div class="cld-obj-detail" onselectstart="return false">
                    <code><%=Translate("拖拽选择时间") %></code>
                    <Uni:calendar runat="server" ID="Cld" />
                    <div class="calendar_pctrl"></div>
                </div>
                <div>
                    <%=infoRule %>
                </div>
                <div>
                    <%=infoHard %>
                </div>
                <div>
                    <div class="img_large">
                        <%=szPicZoom %>
                    </div>
                    <div class="img_thumb">
                        <ul class="clear">
                            <%=szPicPath %>
                        </ul>
                    </div>
                    <script>
                    $(".img_thumb a").click(function () {
                        var thumbimgurl = $(this).children().attr('src');
                        var largeimagenurl = thumbimgurl.replace("", "");
                        $(".img_large img").attr('src', largeimagenurl);
                        $(".img_thumb a").each(function () {
                            if ($(this).hasClass('cur')) {
                                $(this).removeClass('cur');
                            };
                        })
                        $(this).addClass('cur');
                        return false;
                    })
                    </script>
                </div>
            </div>
        </div>
        <script>
            $(".info_unitab").unitab();
        </script>
        <script>
            function subDevResv() {
                $(".mt_sub_resv", dlg).attr({ "disabled": "disabled" });
                pro.j.rsv.fRsv("set_yard_rsv", $("form", dlg), function () {
                    uni.confirm("申请提交成功，是否跳转到个人中心？", function () {
                        $("#user_center").trigger("click");
                    }, function () {
                        uni.reload();
                    });
                    $(".mt_sub_resv", dlg).removeAttr("disabled");
                    dlg.dialog("close");
                }, function (rlt) {
                    uni.msgBox(rlt.msg);
                    $(".mt_sub_resv", dlg).removeAttr("disabled");
                }, function () {
                    uni.msgBox("异步连接出现异常！");
                    $(".mt_sub_resv", dlg).removeAttr("disabled");
                });
            }
        </script>
    </div>
</body>
</html>
