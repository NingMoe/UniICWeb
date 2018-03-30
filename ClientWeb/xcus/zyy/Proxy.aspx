<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="Proxy.aspx.cs" Inherits="ClientWeb_xcus_zyy_Proxy" %>

<%@ Register TagPrefix="Uni" TagName="dlg_rtest" Src="~/ClientWeb/pro/net/dlg_rtest.ascx" %>
<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="theme/TabStyleH.css" rel='stylesheet' />
    <style type="text/css">
        #alterUser { font-size: 14px; }
    </style>
    <script type="text/javascript">
        function ckResv(cmd, id) {
            uni.confirm(cmd == "ok" ? "确定审核通过？" : "确定拒绝通过审核？",
    function () {
        pro.j.rsv.ckRTRsv(id, cmd, function (rlt) {
            uni.msgBoxRT(rlt.msg);
        });
    });
        }
        function staFilter(sta) {
            var td = $('td.rsv_stat');
            $('td.rsv_stat').each(function () {
                if (sta == -1) {
                        $(this).parent().removeClass('hidden');
                }
                else {
                    if ((parseInt($(this).attr('stat')) & sta) > 0)
                        $(this).parent().removeClass('hidden');
                    else
                        $(this).parent().addClass('hidden');
                }
            });
        }
        $(function () {
            $("a.alterCourse").click(function () {
                var id = $(this).parent().parent().find("input.courseId").val();
                if (id != "" && id != undefined) {
                    pro.d.rtest.rtInfoM(id);
                }
            });
            //组操作
            $("#rt_list .operate .op").click(function () {
                var groupid = $(this).parent().attr("groupid");
                var id = $("#cur_logonname").val();
                if ($(this).hasClass("quit")) {
                    pro.j.rtest.delMem(groupid, id, function () {
                        uni.msgBoxRT("操作成功，将重新加载页面！");
                    });
                }
                else if ($(this).hasClass("join")) {
                    pro.j.rtest.addMem(groupid, id, function () {
                        uni.msgBoxRT("操作成功，将重新加载页面！");
                    });
                }
                else {
                    return;
                }
            });
            //组成员
            $("#rt_list .operate .group").tooltip({
                show: null,
                position: {
                    my: "left top",
                    at: "left bottom"
                },
                open: function (event, ui) {
                    ui.tooltip.animate({ top: ui.tooltip.position().top + 10 }, "fast");
                }
            });
            //加载完成即筛选预约
            staFilter(-1);
        });
        //成员管理
        function mbManage(rt_id, rt_name) {
            pro.d.rtest.rtMbM(rt_id, rt_name);
        }
        function actCheck(order, obj) {
            if (order == undefined || order == "") {
                return;
            }
            var stu_name = obj.parents("tr").find(".stu_name").html();
            var stu_accno = obj.parents("tr").find(".stu_accno").val();
            pro.j.acc.tutorRel(order, stu_accno, stu_name, function () {
                MsgBoxRT("操作成功,将重新加载页面！");
            });
        }
    </script>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <Uni:dlg_rtest ID="Dlg_rtest1" runat="server" />
    <div id="templatemo_main">
        <div id="con" class="tabs">
            <ul id="tabs" class="tab_head">
                <li id="tab_rsv"><a>代审预约</a></li>
                <li id="tab_rt"><a>代管项目</a></li>
            </ul>
            <div id="tabContent" class="float_all tab_con">
                <div class="item">
                    <div>
                        <input type="radio" id="radio_all" name="rev_sta" value="全部" checked="checked" onclick="staFilter(-1)" />
                        <label for="radio_all">全部</label>
                        <span style="display: inline-block; width: 30px;"></span>
                        <input type="radio" id="radio_nock" name="rev_sta" value="未审核" onclick="staFilter(1)" />
                        <label for="radio_nock">未审核</label>
                    </div>
                    <div class="resv_list tbl_list zebra">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 120px;">仪器</th>
                                    <th style="width: 60px;">预约人</th>
                                    <th style="">实验名称</th>
                                    <th style="">项目名称</th>
                                    <th style="width: 70px;">项目负责人</th>
                                    <th style="width: 140px;">预约时间</th>
                                    <th style="width: 60px;">项目审核</th>
                                    <th style="width: 70px;">管理员审核</th>
                                    <th>参考费用</th>
                                    <th style="width: 60px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=resvList %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="item">
                    <div class="resv_list tbl_list zebra">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 200px;">项目名称</th>
                                    <th>项目负责人</th>
                                    <th style="">项目级别</th>
                                    <th style="">下发单位</th>
                                    <th style="">所属部门</th>
                                    <th style="width: 80px;">成员数量</th>
                                    <th style="width: 80px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=rtList %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="cleaner"></div>
    </div>
</asp:Content>
