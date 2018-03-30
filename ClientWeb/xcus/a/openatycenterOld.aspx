<%@ Page Language="C#" AutoEventWireup="true" CodeFile="openatycenterOld.aspx.cs" Inherits="ClientWeb_xcus_all_openaty_center" %>

<html>
<body>
    <style>
        .openaty_pre .accordion { font-family: 'Microsoft YaHei'; }
        .openaty_pre .accordion .aty_date { color: grey; border-top: 1px dashed #ddd; padding: 2px; }
        .openaty_pre .accordion tr.aty_img td { padding-bottom: 10px; }
        .openaty_pre .accordion tr td { padding: 8px; vertical-align: top; }
        .openaty_pre .accordion tbody.aty_detail_list tr td:first-child { min-width: 120px; font-weight: bold; border-right: 1px solid #ddd; }
        .openaty_pre .accordion tbody.aty_act td { padding-top: 20px; }
        .openaty_pre .openaty_pre_title { height: 30px; background: #4193D7; color: #fff; line-height: 30px; padding: 0 25px; margin-bottom: 8px; }

        .accordion .ui-accordion-header.ui-state-active { background: #FFEE9A; }
        .accordion .ui-accordion-header.ui-state-hover { background: #FFEE9A; }
        .accordion .aty_detail { background: #FFFCE9; }
    </style>
    <link href="<%=url %>fm/pages/pagination.css" rel="stylesheet" />
    <script>
        $(function () {
            $(".M-box2 .pageset").click(function () {
                var pthis = $(this);
                var pageNum = pthis.attr("data-page");
                $.post("../a/pagesnationold.aspx?activityPage=" + pageNum, function (data) {
                    uni.reload();
                });

                
            });
            $(".accordion").accordion({ collapsible: true, active: false, heightStyle: "content" });
            $(".accordion .btn_mb").click(function () {
                var pthis = $(this);
                if (pro.isloginL(function () { uni.reload();})) {
                    var atyid = pthis.attr("atyid");
                    var purpose = pthis.attr("purpose");
                    if (atyid) {
                        if (purpose == "in") {
                            pro.j.rsv.enrollAty({ aty_id: atyid }, function () {
                                uni.msgBoxR("加入成功");
                            });
                        }
                        else if (purpose == "out") {
                            pro.j.rsv.quitAty(atyid, function () {
                                uni.msgBoxR("退出成功");
                            });
                        }
                        else if (purpose == "seat") {
                            //pro.j.group.addMem("100475941", pro.acc.id, function () { uni.reload() });
                            uni.hr.loadCache("../a/chooseseat.aspx", { atyId: atyid,roomId:pthis.attr("roomid") }, $("#cache_con"));
                        }
                    }
                }
            });
        });
    </script>
    <div class="openaty_pre">
        <h1 class="h_title"><%=Request["type"]=="pre"?"活动预告":"活动回顾" %></h1>
        <div class="line"></div>
        <div class="openaty_pre_title"><span>活动标题</span><span class="pull-right">活动日期</span></div>
        <div class="accordion">
            <%=preAty%>
        </div>
        <div class="M-box2" style="margin-top:10px">
            <%=szPageNation %>
    </div>
</body>
</html>

