<%@ Page Language="C#" AutoEventWireup="true" CodeFile="center.aspx.cs" Inherits="ClientWeb_xcus_cg2_Default" %>
<%@ Register TagPrefix="Uni" TagName="info" Src="~/ClientWeb/pro/net/userinfo.ascx" %>
<%@ Register TagPrefix="Uni" TagName="basic" Src="~/ClientWeb/pro/net/dlg_basic.ascx" %>
<%@ Register TagPrefix="Uni" TagName="resv" Src="~/ClientWeb/pro/net/dlg_resv.ascx" %>
<%@ Register TagPrefix="Uni" TagName="acc" Src="~/ClientWeb/pro/net/acc.ascx" %>
<html>
<body>
    <style>
        .list_style_tbl { width: 99%; margin-top: 20px; }
        .list_style_tbl .popover { max-width: 600px; }
        .list_style_tbl thead td { text-align: center;padding:5px 0; border-width: 0; border-bottom: 2px #31b0d5 solid; border-top: 1px #ddd solid; background-color: #f1f1f1;line-height:16px; }
        .list_style_tbl td { border: 1px solid #ddd; color: #333;padding:2px; }
        .list_style_tbl .head td { background-color: #fafafa; color: #777; vertical-align: bottom; padding: 5px 5px 2px 5px; }
        .list_style_tbl .head td span { padding: 0 5px; display: inline-block; }
        .list_style_tbl .head td h3 { padding: 0 5px; display: inline; }
        .list_style_tbl .content td { vertical-align: top; padding: 4px 3px; }
        .list_style_tbl .content td .part { color: #999; }
        .list_style_tbl .content td .primary { color: #31708f; }
        .list_style_tbl .content .popover .popover-content { min-height: 160px; }
        .list_style_tbl .box { min-height: 60px; }

        .list_style_tbl .detail_info td { line-height: 24px; padding: 0 3px; background: none; border-width: 0; }
        .list_style_tbl .detail_info td:first-child { color: #428bca; text-align: right; padding-right: 5px; }

        .list_style_tbl .state_info td { line-height: 24px; padding: 0 3px; background: none; font-size: 12px; }
        .list_style_tbl.uni_list tbody tr:nth-child(2n) td { background:#edf2f5}
        .list_style_tbl .state_info { width: 500px; }
        .list_style_tbl .state_info th { border-bottom: 2px #31b0d5 solid; text-align: center; }
        .list_style_tbl .state_info td:first-child { color: #428bca; }
        .list_style_tbl .time_span { border-bottom: 1px dashed #ccc; }

        /*#creditrec_list .detail span{display:inline-block;margin-right:5px;}*/
    </style>
    <script>
        $(function () {
            //标签初始化
            $(".unitab").unitab();
            //过滤预约
            filterResv("true");
            $("#sel_date").change(function () {
                var v = parseInt($(this).val());
                filterResv(v > 0 ? "false" : "true",v);
            }).bsDropdown({width:140});
        })
        //预约过滤 hide true则隐藏过期
        function filterResv(hide, state) {
            
            var list = $("#my_resv_tbl tbody");
            state = parseInt(state);
            list.each(function () {
                var pthis = $(this);
                
                var over = pthis.attr("over");
              
                var vDate = pthis.attr("date");
                if (over == null || vDate == null)
                {
                    return true;
                }
                vDate = vDate.substring(0, 10);
                vDate = vDate.replace(new RegExp('-', 'gm'), '');
                
                vDate = parseInt(vDate);
                var DateNow = parseInt(GetDateNow().replace(new RegExp('-', 'gm'), ''));
                var sta = parseInt(pthis.attr("state"));
                if (over == "true")
                {
                    sta = 1073741824;
                }
                if (over == hide) {
                    pthis.hide();
                }
                else if (!state || (sta & state) > 0) {
                      pthis.show();
                }
                else {
                    pthis.hide();
                }
                
                if ((!state|| state == 0) && vDate > DateNow)
                {
                    pthis.hide();
                }
                 
            });
        }
        function GetDateNow()
        {
            var date = new Date();
            var seperator1 = "-";
            var seperator2 = ":";
            var month = date.getMonth() + 1;
            var strDate = date.getDate();
            if (month >= 1 && month <= 9) {
                month = "0" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = "0" + strDate;
            }
            var currentdate = date.getFullYear() + "-" + month + "-" + (strDate);
            return currentdate;

        }
        //预约删除
        function delRsv(rsv) {
            var id = $(rsv).attr("rsvId");
            uni.confirm('<%=Translate("确定要删除吗？")%>', function () {
                pro.j.rsv.delResv(id, function () {
                    uni.msgBox('<%=Translate("删除成功！")%>');
                    $(rsv).parents("tbody:first").remove();
                });
            });
        }
        //删除活动
        function delOpenAty(id, resv) {
            uni.confirm("确定要删除活动吗？", function () {
                pro.j.rsv.delOpenAty(id, resv, function () {
                    uni.msgBoxR("删除成功！");
                });
            });
        }
        //预约修改
        function alterRsv(rsv) {
            var rsv = $(rsv);
            var kind = rsv.attr("devKind");
            var devId = rsv.attr("devId");
            var rsvId = rsv.attr("rsvId");
            var start = rsv.attr("start");
            var end = rsv.attr("end");
            pro.d.resv.alterTime(devId, kind, rsvId, start, end);
        }
        //$.fn.datepicker = function () {
        //    $(this).click(function () {
        //        WdatePicker();
        //    });
        //    return $(this);
        //}
    </script>
    <div class="user_center_panel" style="position:relative;">
        <Uni:acc runat="server"/>
        <Uni:basic runat="server"/>
        <Uni:resv runat="server" />
        <h1 class="h_title"><%=Translate("个人中心")%>&nbsp;&nbsp;<span class="grey hidden">PERSONAL HOMEPAGE</span></h1>
        <span class="pull-right blod" style="color:#666;position:absolute;top:26px;right:0;"><%=useInfo %></span>
        <div class="line"></div>
        <div class="" style="min-height: 400px;">

            <div class="info_unitab">
                <ul class="tab_head">
                    <li>
                        <div class="title"><%=Translate("个人预约")%></div>
                        <div class="caret"></div>
                    </li>
                    <li class="<%=(centerActivity!=false)?"":"hidden" %>">
                        <div class="title"><%=Translate("活动维护")%></div>
                        <div class="caret"></div>
                    </li>
                    <li class="<%=(GetConfig("userCredit")=="1")?"":"hidden" %>">
                        <div class="title"><%=Translate("信用明细")%></div>
                        <div class="caret"></div>
                    </li>
                    <li>
                        <div class="title"><%=Translate("用户信息")%></div>
                        <div class="caret"></div>
                    </li>
                </ul>
                <div class="tab_con" style="min-height:300px;">
                    <div class="item">
                        <table id="my_resv_tbl" class="list_style_tbl">
                            <thead>
                                <tr>
                                    <td style="width: 14%;"><%=Translate("预约对象")%></td>
                                    <td style="width: 14%;"><%=Translate("预约人")%></td>
                                    <td style="width: 16%;"><%=Translate("组成员")%></td>
                                    <td style="width: 26%;">
                                        <select id="sel_date">
                                            <option value="0"><%=Translate("新预约记录")%></option>
                                            <option value="1073741824"><%=Translate("近三个月历史记录")%></option>
                                            <option value="262144"><%=Translate("近三个月违约记录")%></option>
                                        </select>
                                    </td>
                                    <td style="width: 18%;"></td>
                                    <td style="width: 12%;"></td>
                                </tr>
                            </thead>
                            <%=resvList %>
                        </table>
                    </div>
                    <div class="item">
                        <div class="atylist_con">
                            <div  id="openaty_list" class="list_style_tbl" style="margin-top:0;">
                                <div class="cur_status text-right" style="padding:2px;"></div>
                            <table class="tab_con" style="width:100%;">
                                <thead>
                                    <tr>
                                        <td><%=Translate("活动名称")%></td>
                                        <td><%=Translate("主办单位")%></td>
                                        <td><%=Translate("联系人")%></td>
                                        <td><%=Translate("活动地点")%></td>
                                        <td><%=Translate("活动时间")%></td>
                                        <td><%=Translate("活动状态")%></td>
                                        <td><%=Translate("操作")%></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%=atyList %>
                                </tbody>
                            </table>
                            </div>
                            <div class="atylist_pctrl"></div>
                        </div>
                        <script>
                            $("#openaty_list").pctrl($(".atylist_pctrl"), 10);
                        </script>
                    </div>
                    <div class="item">
                        <div class="creditrec_con">
                            <div  id="creditrec_list" class="list_style_tbl" style="margin-top:0;">
                                <div class="cur_status text-right" style="padding:2px;"></div>
                            <table class="tab_con" style="width:100%;">
                                <thead>
                                    <tr>
                                        <td><%=Translate("发生日期")%></td>
                                        <td><%=Translate("地点")%></td>
                                        <td><%=Translate("类型")%></td>
                                        <td><%=Translate("状态")%></td>
                                        <td><%=Translate("扣分")%></td>
                                        <td><%=Translate("处罚")%></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%=creditrec %>
                                </tbody>
                            </table>
                            </div>
                            <div class="creditrec_pctrl"></div>
                        </div>
                        <script>
                            $("#creditrec_list").pctrl($(".creditrec_pctrl"), 10);
                        </script>
                    </div>
                    <div class="item">
                        <Uni:info runat="server" />
                    </div>
                </div>

            </div>
            <script>
                    $(".info_unitab").unitab(null, {hide:"<%=hideTab%>"});
            </script>
        </div>
    </div>
</body>
</html>
