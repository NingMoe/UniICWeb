<%@ Page Language="C#" AutoEventWireup="true" CodeFile="../a/devdetail.aspx.cs" Inherits="ClientWeb_xcus_all_devdetail" %>
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
    <div id="devtail_resv_panel" style="display:<%=noResv%>;">
        <div style="min-height:180px;">
    <Uni:calendar runat="server" ID="Cld" Alone="true"/></div>
    <code>拖拽选择时间，进行预约</code>
    </div>
    <div class="line"></div>
    <div class="info_unitab">
        <ul class="tab_head">
            <li>
                <div class="title"><span class="uni_trans">详细属性</span></div>
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
                            <td colspan="2" rowspan="6" style="text-align: center;width:45%;"><a href="" class="detail_img">
                                <img alt="<%=CurDevName %>" src="<%=imgUrl %>" border="0" style="width: 320px; height: 200px;" /></a></td>
                            <td class="dt" style="width:180px;"><span class="uni_trans">名称</span></td>
                            <td class="dd"><span runat="server" id="devName"></span></td>
                        </tr>
                        <tr>
                            <td class="dt"><span class="uni_trans">所在位置</span></td>
                            <td class="dd"><span runat="server" id="devLoc"></span></td>
                        </tr>
                        <tr>
                            <td class="dt"><span class="uni_trans">支持人数</span></td>
                            <td class="dd"><%=devUsers %></td>
                        </tr>
                        <tr>
                            <td class="dt"><span class="uni_trans">硬件配置</span></td>
                            <td class="dd"><%=deploy %></td>
                        </tr>
                        <tr>
                            <td class="dt"><span class="uni_trans">管理员</span></td>
                            <td class="dd"><span runat="server" id="devMan"></span></td>
                        </tr>
                        <tr>
                            <td class="dt"><span class="uni_trans">联系方式</span></td>
                            <td class="dd"><span runat="server" id="devCon"></span></td>
                        </tr>
                    </tbody>
                </table>
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
