<%@ Page Language="C#" MasterPageFile="Modules/Master.master" AutoEventWireup="true" CodeFile="UserCenter.aspx.cs" Inherits="UserCenter" %>

<%@ Register TagPrefix="uni" TagName="rscAcc" Src="~/ClientWeb/pro/net/dlg_rsc_acc.ascx" %>
<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <link href='Scripts/UniTag/TagStyleH.css' rel='stylesheet' />
    <link rel="stylesheet" type="text/css" href="<%=this.ResolveClientUrl("~/ClientWeb/") %>fm/uni.css" />
    <link rel="stylesheet" type="text/css" href="<%=this.ResolveClientUrl("~/ClientWeb/") %>pro/pro.css" />
    <style type="text/css">
        #alterUser { font-size: 14px; }
        .tbl_list th { cursor: pointer; }
        .achi_con li { display: inline; }
        .achi_con li label { cursor: pointer; }
        .achi_con table { border-collapse: collapse; }
        .achi_con th, .achi_con td { border-width: 1px; border-style: solid; }
        .date_input { height: 20px; line-height: 20px; width: 160px; }
    </style>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <uni:rscAcc runat="server" />
    <script type="text/javascript">
        function delAct(act, id) {
            ConfirmBox("确定删除吗？",
                function () {
                    $.ajax({
                        type: "GET",
                        url: "Ajax_Code/utilFun.aspx?act=" + act + "&id=" + id,
                        dataType: "json",
                        success: function (rlt) {
                            if (rlt.ret == "0") {
                                MessageBox(rlt.msg);
                            }
                            else if (rlt.ret == "1") {
                                MsgBoxR(rlt.msg);
                            }
                        },
                        error: function (err) {
                            MessageBox("异步连接返回异常！");
                        }
                    });
                });
        }
        function delAchi(id) {
            uni.confirm("确定要删除成果吗？", function () {
                pro.j.achi.delAchi(id, function () {
                    uni.msgBoxR("删除成功");
                });
            });
        }
        function staFilter(sta) {
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
        function selAcc(acc, dlg) {
            pro.j.acc.assignTuCked(acc.accno, acc.name, function (rlt) {
                uni.msgBoxR("指定成功");
            }, { rtest: "auto" });
            $(dlg).dialog("close");
        }
        $(function () {
            //已有导师 不可修改
            var tt = $("#sp_tutor_name").html();
            //if (tt != "未指定")
            //    $("#alter_tutor").hide();
            //功能切换
            var req = new QueryString();
            var str = req["act"];
            if (str == "info") {
                $("#my_info").trigger("click");
            }
            else if (str == "resv") {
                $("#my_resv").trigger("click");
            }
            else if (str == "data") {
                $("#my_data").trigger("click");
            }
            else if (str == "course") {
                $("#my_course").trigger("click");
            }
            else if (str == "record") {
                $("#my_record").trigger("click");
            }
            else if (str == "achi") {
                $("#my_achi").trigger("click");
            }

            $("#alterUser").click(function () {
                $('#alterdialog').dialog('open');
                return false;
            });
            $("#gettutordialog").dialog({ minWidth: 300, autoOpen: false, modal: true, minHeight: 200, bgiframe: true });
            $("#gettutordialog .close").click(function () {
                $("#gettutordialog").dialog("close");
                return false;
            });

            $("#rt_list .operate .op").click(function () {
                var id = $(this).parent().attr("groupid");
                var lg = $("#cur_logonname").val();
                var act;
                if ($(this).hasClass("quit")) {
                    act = "delm";
                }
                else if ($(this).hasClass("join")) {
                    act = "addm";
                }
                else {
                    return;
                }
                $.ajax({
                    type: "GET",
                    url: "Ajax_Code/rTestes.aspx?act=" + act + "&id=" + id + "&lg=" + lg,
                    dataType: "json",
                    success: function (rlt) {
                        if (rlt.ret == 0) {
                            MessageBox("操作失败！");
                        }
                        else if (rlt.ret == 1) {
                            MsgBoxR("操作成功，讲重新加载页面！");
                        }
                    }
                });
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
            //获取导师
            $("#gettutor").submit(function () {
                var data = $(this).serialize();
                MemberAjax({
                    Prm: "act=gettutor&" + data,
                    Function: function (rlt) {
                        if (rlt.ret == 0) {
                            $("#tutor_msg").html(rlt.msg);
                        }
                        else if (rlt.ret == 1) {
                            var list = rlt.list;
                            var list_str = "";
                            $(list).each(function () {
                                list_str += "<tr><td>" + this.szLogonName + "</td><td class='tru_name'>" + this.szTrueName + "</td><td>(" + $(this).attr("szDeptName") +
                                    ")</td><td class='click' onclick=\"selTutor('" + this.szTrueName + "','" + this.dwAccNo + "');\">选择</td></tr>";
                            });
                            $("#tutor_list").html(list_str);
                        }
                    }
                });
            });
            //排序
            $(".tbl_list").tblsort();
            //日期控件
            $(".date_input").datepicker();
        });
        function subUpdate() {
            var data = $("#updateacc").serialize();
            MemberAjax({
                Prm: "act=updatetutor&" + data,
                Function: function (rlt) {
                    if (rlt.ret == 0) {
                        MessageBox(rlt.msg);
                    }
                    else if (rlt.ret == 1) {
                        MsgBoxR("保存成功");
                    }
                }
            });
        };
        function selTutor(name, acc) {
            $("#tutor_name").val(name);
            $("#sp_tutor_name").html(name);
            $("#tutor_acc").val(acc);
            $("#gettutordialog").dialog("close");
        };
    </script>
    <div class="g-b-m">
        <div id="con" class="tags" style="margin-top: 20px;">
            <ul id="tags" class="tag_head">
                <li id="my_resv"><a>我的预约</a></li>
                <li id="my_record"><a>实验记录</a></li>
                <li id="my_data"><a>实验数据</a></li>
                <li id="my_achi"><a>成果管理</a></li>
                <li id="my_course" style="display: none;"><a>我的课题</a></li>
                <li id="my_info"><a>个人信息</a></li>
            </ul>
            <div class="tag_con" id="tagContent">
                <div class="item">
                    <div>
                        <label>
                            <input type="radio" name="rev_sta" value="全部" checked="checked" onclick="staFilter(-1)" />全部</label>
                        <span style="display: inline-block; width: 30px;"></span>
                        <label>
                            <input type="radio" name="rev_sta" value="预约未生效" onclick="staFilter(256)" />预约未生效</label>
                        <span style="display: inline-block; width: 30px;"></span>
                        <label>
                            <input type="radio" name="rev_sta" value="预约已生效" onclick="staFilter(512)" />预约已生效</label>
                        <span style="float:right;color:orange;">点击表头可排序</span>
                    </div>
                    <div class="resv_list tbl_list">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 120px;">仪器</th>
                                    <th style="width: 80px;">预约人</th>
                                    <th style="">实验信息</th>
                                    <th style="">导师</th>
                                    <th style="width: 200px;">预约时间</th>
                                    <th style="width: 60px;">状态</th>
                                    <th style="width: 80px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=resvList %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="item">
                    <div>
                        开始日期：<input type="text" class="resv_rec_start date_input" readonly="readonly" />
                        &nbsp;&nbsp;&nbsp; 结束日期：<input type="text" class="resv_rec_end date_input" readonly="readonly" />
                        <input type="button" class="resv_rec_submit button" value="查询" />
                        <label style="float: right; cursor: pointer;">
                            <input type="checkbox" class="resv_stat" />显示统计</label>
                        <script>
                            $(".resv_stat").click(function () {
                                if ($(this).is(":checked")) {
                                    $(".resv_rec_list").hide();
                                    $(".resv_stat_list").show();
                                }
                                else {
                                    $(".resv_rec_list").show();
                                    $(".resv_stat_list").hide();
                                }
                            });
                            $(".resv_rec_submit").click(function () {
                                uni.showWait();
                                $("#resv_rec_panel").load("resvRec.aspx", { start: $(".resv_rec_start").val(), end: $(".resv_rec_end").val() }, function () {
                                    uni.hideWait();
                                    $("#resv_rec_panel").find(".resv_rec_list").tblsort();
                                });
                            });
                        </script>
                    </div>
                    <div style="text-align:right;color:orange;"><span>明细表点击表头可排序</span></div>
                    <div id="resv_rec_panel">
                        <%--                    <div class="resv_stat_list tbl_list" style="display:none;">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th>预约人</th>
                                    <th>实验次数</th>
                                    <th tp="strong_number">使用总时长</th>
                                    <th tp="strong_number">总应收费</th>
                                    <th tp="strong_number">总实收费</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=statList %>
                            </tbody>
                        </table>
                    </div>
                    <div class="resv_list tbl_list resv_rec_list">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 120px;">仪器</th>
                                    <th style="width: 80px;">预约人</th>
                                    <th style="">实验信息</th>
                                    <th style="">导师</th>
                                    <th style="width: 180px;" class="sort_desc">预约时间</th>
                                    <th style="width:60px;" tp="strong_number">实际使用</th>
                                    <th style="width: 60px;" tp="strong_number">应收费</th>
                                    <th style="width: 60px;" tp="strong_number">实收费</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=recordList %>
                            </tbody>
                        </table>
                    </div>--%>
                    </div>
                </div>
                <div class="item">
                                        <div>
                        开始日期：<input type="text" class="data_rec_start date_input" readonly="readonly" />
                        &nbsp;&nbsp;&nbsp; 结束日期：<input type="text" class="data_rec_end date_input" readonly="readonly" />
                        <input type="button" class="data_rec_submit button" value="查询" />
                        <script>
                            $(".date_input").datepicker();
                            $(".data_rec_submit").click(function () {
                                uni.showWait();
                                $("#test_data_panel").load("testDataRec.aspx", { start: $(".data_rec_start").val(), end: $(".data_rec_end").val() }, function (rlt) {
                                    uni.hideWait()
                                    $("#test_data_panel").tblsort();
                                });
                            });
                            $(function () {
                                var now=new Date();
                                $(".data_rec_end").val(now.format("yyyy-MM-dd"));
                                now.addDays(-30);
                                $(".data_rec_start").val(now.format("yyyy-MM-dd"));
                                $(".data_rec_submit").trigger("click");
                            });
                        </script>
                    </div>
                    <div>
                        <label onclick="$('td.status').each(function () { $(this).parent().removeClass('hidden') });">
                            <input type="radio" name="sta" value="全部" checked="checked" />
                            全部
                        </label>
                        <span style="display: inline-block; width: 30px;"></span>
                        <label onclick="$('td.status').each(function () { if ((parseInt($(this).attr('stat')) & 4) > 0) $(this).parent().removeClass('hidden'); else $(this).parent().addClass('hidden'); });">
                            <input type="radio" name="sta" value="已下载" />
                            已下载</label><span style="display: inline-block; width: 30px;"></span>
                        <label onclick="$('td.status').each(function () { if ((parseInt($(this).attr('stat')) & 4) == 0) $(this).parent().removeClass('hidden'); else $(this).parent().addClass('hidden'); });">
                            <input type="radio" name="sta" value="未下载" />
                            未下载</label>
                        <span style="float:right;color:orange;">点击表头可排序</span>
                    </div>
                    <div class="tbl_list" id="test_data_panel">
                    </div>
                </div>
                <div class="item">
                    <div>
                        <div class="achi_con">
                            <ul class="tab_head">
                                <li>
                                    <label>
                                        <input type="radio" name="radio_group_kind" class="radio_kind" value="1" checked />论文发表</label></li>
                                <li>
                                    <label>
                                        <input type="radio" name="radio_group_kind" class="radio_kind" value="2" />获奖</label></li>
                            </ul>
                            <div class="tab_con">
                                <div class="tbl_list" kind="1">
                                    <table style="width: 100%; margin-top: 0;" id="devtbl">
                                        <thead>
                                            <tr class="tbl_head">
                                                <th>论文名称</th>
                                                <th>论文作者</th>
                                                <th>发表刊物</th>
                                                <th>刊物等级</th>
                                                <th>影响因子</th>
                                                <th>录入员</th>
                                                <th style="width:60px;">操作</th>
                                            </tr>
                                        </thead>
                                        <tbody runat="server" id="thesis">
                                        </tbody>
                                    </table>
                                </div>
                                <div class="tbl_list hidden" kind="2">
                                    <table style="width: 100%; margin-top: 0;">
                                        <thead>
                                            <tr class="tbl_head">
                                                <th>获奖名称</th>
                                                <th>获奖人员</th>
                                                <th>颁奖部门</th>
                                                <th>获奖等级</th>
                                                <th>录入员</th>
                                                <th style="width:60px;">操作</th>
                                            </tr>
                                        </thead>
                                        <tbody runat="server" id="prize">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <script>
                            $(".achi_con .radio_kind").click(function () {
                                var pthis = $(this);
                                if (pthis.is(":checked")) {
                                    $(".achi_con .tbl_list").each(function () {
                                        var p = $(this);
                                        if (p.attr("kind") == pthis.val())
                                            p.show();
                                        else
                                            p.hide();
                                    });
                                }
                            });
                        </script>
                    </div>
                </div>
                <div id="rt_list item">
                    <div id="group_box"></div>
                    <div class="tbl_list">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 240px;">课题名称</th>
                                    <th style="width: 120px;">负责人</th>
                                    <th style="">实验次数</th>
                                    <th style="">实验时间</th>
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
                    <%--                    <div style="text-align: left; color:red; display: <%=isTutor%>">注意：若查找不到自己的导师，可能是因为导师的身份还未被管理员确认，请联系实验室管理员：</div>--%>
                    <div style="width: 500px; margin: 20px auto; border: 1px solid #ccc; text-align: center; background-color: #fcfcfc;">
                        <form id="updateacc" onsubmit="return false;">
                            <table class="acc_tbl">
                                <tr>
                                    <td style="width: 80px;"><span style="font-weight: 700;">姓名：</span></td>
                                    <td><span runat="server" id="accName"></span></td>
                                </tr>
                                <tr>
                                    <td><span style="font-weight: 700">学工号：</span></td>
                                    <td><span runat="server" id="accLgName"></span></td>
                                </tr>
                                <tr>
                                    <td><span style="font-weight: 700">学院：</span></td>
                                    <td><span runat="server" id="accColl"></span></td>
                                </tr>
                                <tr>
                                    <td><span style="font-weight: 700">电话：</span></td>
                                    <td>
                                        <input type="text" id="accPhone" name="accPhone" value="<%=accPhone %>" /></td>
                                </tr>
                                <tr>
                                    <td><span style="font-weight: 700">邮箱：</span></td>
                                    <td>
                                        <input type="text" id="accEmail" name="accEmail" value="<%=accEmail %>" /></td>
                                </tr>
                                <tr style="display: <%=isTutor%>">
                                    <td><span style="font-weight: 700">导师：</span></td>
                                    <td>
                                        <span id="sp_tutor_name"><%=(tutorName+tutorckSta) %></span><input type="hidden" id="tutor_name" name="tutor_name" value="<%=tutorName %>" /><input type="hidden" id="tutor_acc" name="tutor_acc" value="<%=tutorAcc %>" />
                                        | <a class="click" id="alter_tutor" onclick="pro.d.acc.srchAccByName(selAcc,512)">查找导师</a>
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
