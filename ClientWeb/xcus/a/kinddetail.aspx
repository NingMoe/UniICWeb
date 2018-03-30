<%@ Page Language="C#" AutoEventWireup="true" CodeFile="../a/kinddetail.aspx.cs" Inherits="ClientWeb_xcus_all_kinddetail" %>
<%@ Register TagPrefix="Uni" TagName="calendar" Src="~/ClientWeb/pro/net/calendar.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <style>
        .detail_list { width: 90%; }
        .detail_list td { line-height: 28px; text-indent: 0; }
        .detail_list td.dt { border-right: 1px solid #aaa; color: #666; font-weight: 600; padding-right: 10px;text-align:right; }
        .detail_list td.dd { color: #999; padding-left: 30px;line-height: 16px; }
    </style>
    <script>
        var uni_calendar_dft_opt = {
            finishDraw: function (date, opt, ipz, more) {
                var obj = more.iobj;
                if (!obj||!obj.iskind||!obj.islong) return;//必须长期类型预约
                var tbl = obj.freeTbl;
                if (tbl && tbl.length > 0) {
                    var begine = uni.parseDate(pro.dt.num2date(obj.freeTime));
                    var start = uni.parseDate($(".cld-m-cell:first", ipz).attr("date"));
                    var diff = uni.compareDate(start, begine);
                    var list = $(".cld-ttd .cld-m-cell", ipz);
                    list.each(function (i) {
                        var cell = $(this);
                        var info = cell.find(".cld-m-cell-info");
                        cell.find(".cld-other-info").remove();
                        if (cell.hasClass("cld-cell-nofree")) return true;
                        var v=tbl[diff+i];
                        if (v && v != "0") {
                            if (v == "A") v = "剩余:9+";
                            else if (v == "U") v = "";
                            else v = "剩余:"+v;
                            info.append("<div calss='cld-other-info' style='clear: both;font-size:14px;font-family:\"Microsoft YaHei\";padding-left:2px;color:green;'>" + v + "</div>");
                    }
                });
                }
            }
        };
    </script>
    <div class="click btn_back" onclick="uni.hr.back();" style="display:<%=isBack%>"><span class="glyphicon glyphicon-chevron-left"></span>&nbsp;<span class="uni_trans">返回</span></div>
    <h2 class="h_title"><%=CurKindName %></h2>
    <div style="display:<%=noResv%>;">
        <div style="min-height:180px;">
    <Uni:calendar runat="server" ID="Cld" Alone="true"/></div>
    </div>
    <div class="line hidden"></div>
    <div class="kind_info_unitab" style="display:none;">
        <ul class="tab_head">
            <li>
                <div class="title"><span class="uni_trans">详细属性</span></div>
                <div class="caret"></div>
            </li>
        </ul>
        <div class="tab_con tab-content">
            <div style="border:1px solid #eee;padding:12px 2px;">
                <table class="detail_list">
                    <tbody>
                        <tr>
                            <td colspan="2" rowspan="6" style="text-align: center;width:45%;"><a href="" class="detail_img">
                                <img alt="<%=CurKindName %>" src="<%=imgUrl %>" border="0" style="width: 320px; height: 200px;" /></a></td>
                            <td class="dt" style="width:180px;"><span class="uni_trans">名称</span></td>
                            <td class="dd"><%=CurKindName %></td>
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
        </div>
    </div>
    <script>
        $(".kind_info_unitab").unitab();
    </script>
</body>
</html>
