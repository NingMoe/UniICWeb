<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="UserCenter.aspx.cs" Inherits="UserCenter" %>

<%@ Register TagPrefix="Uni" TagName="dlg_rtest" Src="~/ClientWeb/pro/net/dlg_rtest.ascx" %>
<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="theme/TabStyleH.css" rel='stylesheet' />
    <style type="text/css">
        #alterUser { font-size: 14px; }
        .tablesorter { text-align:center;}
        .tablesorter .tl { text-align:left;}
    </style>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        function delAct(act, id) {
            uni.confirm("确定删除吗？",
                function () {
                    if (act == "del_data") {
                        pro.j.data.delTestData(id, function (rlt) {
                            uni.msgBoxRT(rlt.msg);
                        });
                    }
                    else if (act == "del_rt_resv") {
                        pro.j.rsv.delRTRsv(id, function (rlt) {
                            uni.msgBoxRT(rlt.msg);
                        });
                    }
                });
        }
        function ckResv(cmd, id) {
            uni.confirm(cmd == "ok" ? "确定审核通过？" : "确定拒绝通过审核？",
    function () {
        pro.j.rsv.ckRTRsv(id, cmd, function (rlt) {
            uni.msgBoxRT(rlt.msg);
        });
    });
        }
        function staFilter(sta) {
            //判断导师
            var ist = 0;
            if (pro.isTutor(pro.acc.ident)) {
                ist = 1;
            }
            var td = $('td.rsv_stat');
            $('td.rsv_stat').each(function () {
                var k = parseInt($(this).attr('myself')) | ist;
                if (sta == -1) {
                    if (k > 0) {
                        $(this).parent().removeClass('hidden');
                    }
                    else {
                        $(this).parent().addClass('hidden');
                    }
                }
                else {
                    if ((parseInt($(this).attr('stat')) & sta) > 0 && k > 0)
                        $(this).parent().removeClass('hidden');
                    else
                        $(this).parent().addClass('hidden');
                }
            });
        }
        function findTutor() {
            pro.d.acc.srchTutor(function (accno, name) {
                pro.j.acc.assignTutor(accno, name, function () { uni.msgBoxRT('指定导师成功！'); });
            });
        }
        //function selTutor(name, acc) {
        //    $("#tutor_name").val(name);
        //    $("#sp_tutor_name").html(name);
        //    $("#tutor_acc").val(acc);
        //    $("#gettutordialog").dialog("close");
        //};
        $(function () {
            var pagerOptions = {
                container: $(".pager"),
                output: '第 {startRow} 条 - 第 {endRow} 条 | 总 : {filteredRows} 条',
                fixedHeight: true,
                removeRows: false,
                cssGoto: '.gotoPage'
            };

            // Initialize tablesorter
            $(".grid")
                .tablesorter({
                    theme: 'blue',
                    headerTemplate: '{content} {icon}', 
                    widthFixed: true,
                    widgets: ['zebra', 'filter'],
                    widgetOptions: {
                        filter_hideFilters: false,
                        filter_functions: {
                            7: function (e, n, f, i, $r) {
                                return e === f;
                            },
                            8: function (e, n, f, i, $r) {
                                return e === f;
                            }
                        }
                    }
                })
                .tablesorterPager(pagerOptions);


            $("#alterUser").click(function () {
                $('#alterdialog').dialog('open');
                return false;
            });
            $("#rt_list .operate .op").click(function () {
                var rtid = $(this).parent().attr("rtid");
                var groupid = $(this).parent().attr("groupid");
                var id = $("#cur_logonname").val();
                if ($(this).hasClass("quit")) {
                    pro.j.rtest.delRTMem(rtid, groupid, id, function () {
                        uni.msgBoxRT("操作成功，将重新加载页面！");
                    });
                }
                else if ($(this).hasClass("join")) {
                    pro.j.rtest.addRTMem(rtid, groupid, id, function () {
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
            //日期控件
            $(".Wdate").datetimepicker({
                timeFormat: "",
                dateFormat: "yy-mm-dd"
            });
            //加载完成即筛选预约
            //staFilter(-1);
        });
        function subUpdate() {
            var phone = $("#accPhone").val();
            if (!uni.ckMobile(phone)) { uni.msgBox("手机填写有误！"); return false; }
            var email = $("#accEmail").val();
            if (!uni.ckEmail(email)) { uni.msgBox("邮箱填写有误！"); return false; }
            pro.j.acc.upContact(phone, email, function () {
                uni.msgBox("保存成功");
            });
        };
    </script>
    <div id="templatemo_main">
        <Uni:dlg_rtest ID="Dlg_rtest1" runat="server" />
        <div id="con" class="tabs">
            <ul id="tabs" class="tab_head">
                <li id="my_resv"><a><%=(TutorHide=="none"?"审核预约":"我的预约") %></a></li>
                <li id="my_data"><a>实验数据</a></li>
                <li id="my_course" style="display: <%=TutorHide%>"><a>我的项目</a></li>
                <li id="my_fee" style="display: <%=TutorShow%>"><a>费用结算</a></li>
                <li id="my_info"><a>个人信息</a></li>
            </ul>
            <div class="tab_con" id="tabContent">
                <form id="Form1" runat="server">
                    <div style="padding: 4px; margin: 3px; border-bottom: 1px solid #999;">
                        开始日期：<input runat="server" id="iptDateStart" class="Wdate" type="text" readonly="readonly" />
                        结束日期：<input runat="server" id="iptDateEnd" class="Wdate" type="text" readonly="readonly" />
                        <asp:Button runat="server" ID="DateSubmit" Text="查询" OnClick="DateSubmit_Click" CssClass="button" />
                    </div>
                </form>
                <div class="item">
                    <div class="hidden">
                        <label for="radio_rev_all">全部</label>
                        <input type="radio" id="radio_rev_all" name="rev_sta" value="全部" onclick="staFilter(-1)" />
                        <span style="display: inline-block; width: 30px;"></span>
                        <label for="radio_rev_proun">未审核</label>
                        <input type="radio" id="radio_rev_proun" name="rev_sta" value="未审核" onclick="staFilter(1)" />
                        <span style="display: inline-block; width: 30px;"></span>
                        <label for="radio_rev_proyes">项目审核通过</label>
                        <input type="radio" id="radio_rev_proyes" name="rev_sta" value="项目审核通过" onclick="staFilter(8)" />
                        <span style="display: inline-block; width: 30px;"></span>
                        <label for="radio_rev_prono">项目审核未通过</label>
                        <input type="radio" id="radio_rev_prono" name="rev_sta" value="项目审核未通过" onclick="staFilter(16)" />
                        <span style="display: inline-block; width: 30px;"></span>
                        <label for="radio_rev_myes">管理员审核通过</label>
                        <input type="radio" id="radio_rev_myes" name="rev_sta" value="管理员审核通过" onclick="staFilter(2)" />
                        <span style="display: inline-block; width: 30px;"></span>
                        <label for="radio_rev_mno">管理员审核未通过</label>
                        <input type="radio" id="radio_rev_mno" name="rev_sta" value="管理员审核未通过" onclick="staFilter(4)" />
                        <span style="display: inline-block; width: 30px;"></span>
                        <label for="radio_rev_doing">预约已生效</label>
                        <input type="radio" id="radio_rev_doing" name="rev_sta" value="预约已生效" onclick="staFilter(512)" />
                    </div>
                    <div class="">
                        <%--tblResvsresv_listtbl_list<%=TutorShow%>--%>
                        <table class="grid tablesorter" width="960">
                            <thead>
                                <tr class="title tbl_head">
                                    <th width="50">预约人</th>
                                    <th width="120">预约仪器</th>
                                    <th width="100">实验名称</th>
                                    <th width="110">实验项目</th>
                                    <th width="60">提交时间</th>
                                    <th width="100">预约时间</th>
                                    <th width="60" class="filter-select" data-placeholder="全部">项目审核</th>
                                    <th width="60" class="filter-select" data-placeholder="全部">管理员审核</th>
                                    <th width="60" style="display: <%=TutorShow%>;">参考费用</th>
                                    <th width="60">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=resvList %>
                            </tbody>
                        </table>
                        <div class="pager">
                            <form>
                                <input type="button" class="bt first" value="首页"/>
                                <input type="button" class="bt prev" value="上一页"/>
                                <span class="pagedisplay"></span>
                                <input type="button" class="bt next" value="下一页"/>
                                <input type="button" class="bt last" value="尾页"/>
                                <span>  每页显示：</span>
                                <select class="pagesize">
                                    <option selected="selected" value="5">5</option>
                                    <option value="20">20</option>
                                    <option value="30">30</option>
                                    <option value="40">40</option>
                                </select>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="item">
                    <div>
                        <label for="radio_all">全部</label>
                        <input type="radio" id="radio_all" name="sta" value="0" checked="checked" onclick="$('td.status').each(function () { $(this).parent().removeClass('hidden') });" />
                        <span style="display: inline-block; width: 30px;"></span>
                        <label for="radio_un">未下载</label>
                        <input type="radio" id="radio_un" name="sta" value="2" onclick="$('td.status').each(function () { if ((parseInt($(this).attr('stat')) & 2) > 0) $(this).parent().removeClass('hidden'); else $(this).parent().addClass('hidden'); });" />
                        <span style="display: inline-block; width: 30px;"></span>
                        <label for="radio_do">已下载</label>
                        <input type="radio" id="radio_do" name="sta" value="4" onclick="$('td.status').each(function () { if ((parseInt($(this).attr('stat')) & 4) > 0) $(this).parent().removeClass('hidden'); else $(this).parent().addClass('hidden'); });" />
                    </div>
                    <div class="tbl_list">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class='title tbl_head'>
                                    <th>名称</th>
                                    <th>日期</th>
                                    <th>仪器</th>
                                    <th>文件大小</th>
                                    <th>下载状态</th>
                                    <th>下载</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=dataList %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="rt_list" class="item">
                    <div><span class="button" onclick="pro.d.rtest.srchRTest();">申请加入项目</span></div>
                    <div class="tbl_list zebra">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 200px;">项目名称</th>
                                    <th>负责人</th>
                                    <th style="width: 80px;">授权委托</th>
                                    <th style="">下发单位</th>
                                    <th style="">所属部门</th>
                                    <th style="width: 80px;">成员数量</th>
                                    <th style="width: 160px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=rtList %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="item">
                    <div class="resv_list tbl_list zebra">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 120px;">仪器</th>
                                    <th style="width: 60px;">预约人</th>
                                    <th style="">实验名称</th>
                                    <th style="">项目名称</th>
                                    <th style="width: 160px;">预约时间</th>
                                    <th style="width: 60px">实际用时</th>
                                    <th style="width: 60px;">状态</th>
                                    <th style="width: 80px;">参考费用</th>
                                    <th style="width: 80px;">实际费用</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=feeList %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="item">
                    <div style="text-align: left;">为保证您能收到系统的预约提醒，请确保联系方式真实有效。</div>
                    <div style="width: 500px; margin: 20px auto; border: 1px solid #ccc; text-align: center; background-color: #fcfcfc;">
                        <form id="updateacc" onsubmit="return false;">
                            <table class="acc_tbl">
                                <tr>
                                    <td style="width: 80px;"><span style="font-weight: 700;">姓名：</span></td>
                                    <td><span runat="server" id="accName"></span></td>
                                </tr>
                                <tr>
                                    <td><span style="font-weight: 700">帐号：</span></td>
                                    <td><span runat="server" id="accLgName"></span></td>
                                </tr>
                                <tr>
                                    <td><span style="font-weight: 700">部门：</span></td>
                                    <td><span runat="server" id="accColl"></span></td>
                                </tr>
                                <tr>
                                    <td><span style="font-weight: 700">(必填)手机：</span></td>
                                    <td>
                                        <input type="text" id="accPhone" name="accPhone" value="<%=accPhone %>" /></td>
                                </tr>
                                <tr>
                                    <td><span style="font-weight: 700">(必填)邮箱：</span></td>
                                    <td>
                                        <input type="text" id="accEmail" name="accEmail" value="<%=accEmail %>" /></td>
                                </tr>
                                <tr style="display: none">
                                    <td><span style="font-weight: 700">(可选)导师：</span></td>
                                    <td>
                                        <span id="sp_tutor_name"><%=(tutorName+tutorckSta) %></span><input type="hidden" id="tutor_name" name="tutor_name" value="<%=tutorName %>" /><input type="hidden" id="tutor_acc" name="tutor_acc" value="<%=tutorAcc %>" />
                                        | <a class="click" id="alter_tutor" onclick="findTutor()">查找导师</a>
                                    </td>
                                </tr>
                            </table>
                            <div style="text-align: center;">
                                <span id="submit" class="button" onclick="subUpdate();">保存</span>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
