<%@ Control Language="C#" AutoEventWireup="true" CodeFile="calendar.ascx.cs" Inherits="ClientWeb_pro_net_calendar" %>
<script>
    $(function () {
        var req = uni.getReq();
        var purl = {};
        purl.classkind = req["classKind"] || "<%=ClassKind%>";
        purl.islong = req["isLongResv"] || "<%=IsLong.ToString().ToLower()%>";
        purl.md=purl.islong.toLowerCase() == "true" ? "m" : "<%=Mode%>";
        purl.classid = req["classId"] || "<%=DevClassId%>";
        purl.iskind = req["isKind"];
        var selH = req["click"] ? undefined : selHours;
        var width = parseInt(req["w"] || "700");
        var cld = $(".calendar").uniCalendar({
            mode: purl.md,
            modes: purl.md,
            width: width,
            objTitleMinWidth: 90,
            evSelTime: selH,
            evUpTime: function (date, callback) {
                var pra = {};
                pra.class_id = purl.classid;
                pra.date = date.format("yyyyMMdd");
                pra.islong = purl.islong;
                pra.classkind = purl.classkind;
                pra.iskind = purl.iskind;
                //if (purl.islong == "true")//非常耗资源
                //    pra.ck_close = "true";
                pro.j.dev.getRsvSta(pra, function (rlt) {
                    var list = rlt.data;
                    callback(list, "unilab3", {showClose: ("<%=GetConfig("showClose")%>" == "1" ? true : false),byLab:true});
                });
            }
        });
$(".resv_stat").click(function () {
            cld.uploadCld();
        });
    });
    function selHours(data) {
        if (!IsLogin()) {
            $("#logindialog").dialog('open');
            return;
        }
        var k = "<%=GetConfig("needToKnow")%>";
        if (k == "" || k == "0")
            ToResvForm(data);
        else {
            var para = {};//通用
            pro.d.other.resvNotice(null, para, function (dlg) {
                if (dlg.agree) {
                    ToResvForm(data);
                }
            });
        }
    }
    function ToResvForm(data) {
        var mode = data.md;
        var obj = data.obj;
        var tmp = obj.id.split("_");//20150610前为&
        var id = "devkind=";
        if (obj.type == "kind")
            id += tmp[0];
        else {
            id += obj.typeId;
            id += "&dev=" + tmp[0];
        }
        var date = data.dt.replace(/-/g, "");
        var url;
        var path;
        var i = location.href.toLowerCase().indexOf("/clientweb/");
        if (i < 0) path = location.origin;
        else path = location.href.substring(0, i);
        var time = mode == "d" ? "&time=" + data.h + ":00" : "";
        if (obj.type != "kind" && obj.prop && ((parseInt(obj.prop) & 65536) > 0))
            url = path + "/clientweb/xcus/<%=GetConfig("proTab")%>/activerec_step.aspx?" + id + "&date=" + date + "&time=" + data.h + ":00";
        else
            url = path + "/clientweb/xcus/<%=GetConfig("proTab")%>/space_Resvset.aspx?" + id + "&date=" + date + time + "&labid=" + tmp[1];
        location.href = url;
    }
</script>
<div class="calendar"></div>
