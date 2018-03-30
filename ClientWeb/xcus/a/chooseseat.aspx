<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chooseseat.aspx.cs" Inherits="ClientWeb_xcus_all_chooseseat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
        <div class="click btn_back" onclick="uni.hr.back(function(){uni.reload()});"><span class="glyphicon glyphicon-chevron-left"></span>&nbsp;<%=Translate("返回")%></div>
    <div class="line"></div>
    <p class="cs_seat_msg grey">选座请点击空闲的位置</p>
    <div id="cs_seat_floorplan" name="" onselectstart="return false"></div>
    <div></div>
        <script>
            var panel = $("#cs_seat_floorplan");
          
            var calendar = panel.uniFloorPlan({
                width: 760,
                mode:"status",
                img: "<%=rmImg%>",
            isEdit: (pro.isLogin() && (parseInt(pro.acc.ident) & 268435456) > 0),
            evInit: function (callback) {
                pro.j.dev.getDevCoord({room_id:"<%=Request["roomId"]%>",class_kind:8}, function (rlt) {
                    callback(rlt);
                });
            },
                evUpStatus: function (callback) {
                    pro.j.rsv.getAtySeats("<%=Request["atyId"]%>", function (rlt) {
                        var list = rlt.data;
                        callback(list, "position", { showClose: ("<%=GetConfig("showClose")%>" == "1" ? true : false) });
                        for (var i = 0; i < list.length; i++) {
                            if (parseInt(pro.acc.accno) === list[i].accno) {
                                panel.mine = list[i];
                                $("p.cs_seat_msg").html("我的座位：" + list[i].devName);
                                break;
                            }
                        }
                    });
                },
                evSelDot: function (para) {
                    var obj = para.obj;
                    if (panel.mine) {
                        uni.msgBox("你已选择："+panel.mine.devName);
                    }
                    else {
                        uni.confirm("确定选择：" + obj.devName + "?", function () {
                            uni.msgBox("选座成功", "", function () {
                                pro.j.rsv.enrollAty({ aty_id: "<%=Request["atyId"]%>", dev_id: obj.devId, dev_sn: obj.devSN }, function () {
                                    panel.mine = obj;
                                    panel.find(".fp-user-search").trigger("click");
                                    $("p.cs_seat_msg").html("我的座位：" + obj.devName);
                                });
                            });
                        });
                    }
            },
            evSaveCoorb: function (para, boxs, callback) {
                var str = para.width + "&" + para.height + "&" + para.istitle;
                var list = boxs.values();
                $.each(list, function () {
                    str += "&" + this.key + "," + this.css("top") + "," + this.css("left") + "," + this.sz;
                });
                pro.j.dev.setDevCoord({ room_id: "<%=Request["roomId"]%>" }, str, callback);
            }
        });
        </script>

</body>
</html>
