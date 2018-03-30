<%@ Page Language="C#" AutoEventWireup="true" CodeFile="labdetail.aspx.cs" Inherits="ClientWeb_xcus_all_labdetail" %>
<%@ Register TagPrefix="Uni" TagName="calendar" Src="~/ClientWeb/pro/net/calendar.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <script>
        var uni_calendar_dft_opt = {allDay:true, byType: ("<%=GetConfig("resvSchedule")%>" == "1" && (parseInt(pro.acc.ident) & 512) > 0) ? "volume" : "devcls" };
        function loanResv(id) {
            uni.hr.loadCache("../a/kinddetail.aspx?back=true&kind=" + id, null, $("#cache_con"));//
            uni.backTop();
        }
        //外借类型设备
        $(".click_detail").popover({
            html: true,
            placement: "right",
            title: "设备信息",
            content: function () {
                var panel = $("#detail_" + $(this).attr("kind"));
                return panel.html();
            },
            trigger: "hover"
        });
    </script>
    <style>
        .detail_tbl {min-width:200px;}
        .detail_tbl th{text-align:center; }
        .detail_tbl td{text-align:center; }
        .detail_tbl th:first-child{text-align:left; }
        .detail_tbl td:first-child{text-align:left; }
        .loan_tbl .popover {max-width:400px; }
    </style>
    <form runat="server">
        <input runat="server" type="hidden" class="lab_id" name="lab_id" id="labId" />
        <input runat="server" type="hidden" class="lab_name" name="lab_name" id="labName" />
    </form>
    <div>
        <div>
            <h1 class="h_title"><%=infoTitle %></h1>
            <div style="margin:10px;margin-bottom:20px;overflow:hidden;">
                <%=infoIntro %>
            </div>
        </div>
        <div class="info_unitab">
            <ul class="tab_head">
                <li style="display:<%=noResv%>">
                    <div class="title"<%=Translate("预约状态") %>></div>
                    <div class="caret"></div>
                </li>
                <li style="display:<%=GetConfig("resvLoanDetail")=="1"?"":"none"%>">
                    <div class="title"><%=Translate("外借设备") %></div>
                    <div class="caret"></div>
                </li>
                <li style="display:none">
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
                <div class="cld-obj-detail">
                        <Uni:calendar runat="server" ID="Cld"/>
                </div>
                <div class="loan_tbl">
                    <table class="table table-striped table-condensed">
                        <thead>
                            <tr>
                                <th>设备名称</th>
                                <th class="text-center">品牌型号</th>
                                <th class="text-center">设备总数</th>
                                <th class="text-center">剩余数量</th>
                                <th class="text-center">租借</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%=loanList %>
                        </tbody>
                    </table>
                </div>
                <div>
                    <%=infoRule %>
                </div>
                <div>
                    <%=infoHard %>
                </div>
            <div class="rm_album hidden">
                <div class="img_large">
                    <%=szPicZoom %>
                </div>
                <div class="img_thumb">
                    <ul class="clear">
                        <%=szPicPath %>
                    </ul>
                </div>
            </div>
                <script>
                    $(".rm_album").album();
                </script>
            </div>
        </div>
        <script>
            $("body").one("uni_hr_load_success", function () {
                $(".info_unitab").unitab();//为什么要load后
            });
        </script>
    </div>
    <div class="hidden">
        <%=devDetail %>
    </div>
</body>
</html>
