<%@ Page Language="C#" AutoEventWireup="true" CodeFile="devdetail.aspx.cs" Inherits="ClientWeb_xcus_cg2_devdetail" %>
<%@ Register TagPrefix="Uni" TagName="calendar" Src="~/ClientWeb/pro/net/calendar.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <style>
        .detail_list { width: 90%; }
        .detail_list td { line-height: 28px; text-indent: 0; }
        .detail_list td.dt { border-right: 1px solid #aaa; color: #666; font-weight: 600; padding-right: 10px;text-align:right; }
        .detail_list td.dd { color: #999; padding-left: 30px;line-height: 16px; }
        #devtail_resv_panel .cld-pctrl {display:none; }
    </style>
    <div class="click btn_back" onclick="uni.hr.back();" style="display:<%=isBack%>"><span class="glyphicon glyphicon-chevron-left"></span>&nbsp;<span class="uni_trans">返回</span></div>
    <h2 class="h_title"><%=CurDevName %></h2>
    <div class="line"></div>
        <%=devIntro %>
    <div id="devtail_resv_panel" style="display:<%=noResv%>;">
        <script>
            var uni_calendar_dft_opt = {
                cusPrepare: function (data) {
                    SelDateTime(data);
                }
            }
        </script>
        <div style="min-height:180px;">
    <Uni:calendar runat="server" ID="Cld" Alone="true"/></div>
    </div>
    <div class="info_unitab">
        <ul class="tab_head">
            <li>
                <div class="title"><span class="uni_trans">基本属性</span></div>
                <div class="caret"></div>
            </li>
                        <li>
                <div class="title"><span class="uni_trans">使用评价</span></div>
                <div class="caret"></div>
            </li>
            <li>
                <div class="title"><span class="uni_trans">相册展示</span></div>
                <div class="caret"></div>
            </li>
        </ul>
        <div class="tab_con tab-content">
            <div style="border:1px solid #eee;padding:12px 2px;">
                <table class="detail_list">
                    <tbody>
                        <tr>
<%--                            <td colspan="2" rowspan="5" style="text-align: center;width:45%;"><a href="" class="detail_img">
                                <img alt="<%=CurDevName %>" src="<%=imgUrl %>" border="0" style="width: 320px; height: 200px;" /></a></td>--%>
                            <td class="dt" style="width:120px;"><span class="uni_trans">名称</span></td>
                            <td class="dd"><span runat="server" id="devName"></span></td>
                        </tr>
                        <tr>
                            <td class="dt"><span class="uni_trans">所在校区</span></td>
                            <td class="dd"><%=devCam %></td>
                        </tr>
                        <tr>
                            <td class="dt"><span class="uni_trans">所在位置</span></td>
                            <td class="dd"><span runat="server" id="devLoc"></span></td>
                        </tr>
                        <tr>
                            <td class="dt"><span class="uni_trans">场地容量</span></td>
                            <td class="dd"><%=devUsers %></td>
                        </tr>
                        <tr>
                            <td class="dt"><span class="uni_trans">场地配置</span></td>
                            <td class="dd"><%=deploy %></td>
                        </tr>

                    </tbody>
                </table>

            </div>
            <div>
                        <div class="tab_pctrl">
                            <div style="margin-left:20px;">总：<span class="pc_total red"></span>&nbsp;条记录，分&nbsp;<span class="pc_ptotal red"></span>&nbsp;页，当前第&nbsp;<span class="pc_here red"></span>&nbsp;页</div>
                            <div  id="feedback_list" class="feedback_list">
                            <table class="tab_con" style="width:100%;">
                                <tbody>
                                    <%=feedback %>
                                </tbody>
                            </table>
                            </div>
                            <ul class="tab_head"></ul>
                        </div>
                        <script>
                            $(".tab_pctrl").unitab(null, {
                                pctrl: 10, pctrlFun: function (index, need, total, obj) {
                                    $(".pc_total", obj).html(total);
                                    $(".pc_here", obj).html(index + 1);
                                    $(".pc_ptotal", obj).html(obj.ptotal);
                                },
                                custom: true
                            });
                        </script>
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
        $(".img_thumb a").click(function(){
        var thumbimgurl = $(this).children().attr('src');
        var largeimagenurl = thumbimgurl.replace("","");
        $(".img_large img").attr('src',largeimagenurl);
        $(".img_thumb a").each(function(){
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
</body>
</html>
