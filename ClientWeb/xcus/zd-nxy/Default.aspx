<%@ Page Language="C#" MasterPageFile="Modules/Master.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ClientWeb_Default" %>

<%@ Register TagPrefix="Uni" TagName="dlg_lg" Src="~/ClientWeb/pro/net/dlg_lg.ascx" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <%--<link href="Styles/Style.css" rel='stylesheet' />--%>
    <link href='Scripts/UniTag/TagStyle.css' rel='stylesheet' />
    <script src="Scripts/highcharts/highcharts.js" type="text/javascript"></script>
    <script src="Scripts/highcharts/modules/exporting.js" type="text/javascript"></script>

    <link href='Scripts/slide/ResponsiveSlide.css' rel='stylesheet' />
    <script src="Scripts/slide/ResponsiveSlide.min.js" type="text/javascript"></script>
    <style type="text/css">
        .rank_date {text-align:right; }
    </style>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        //Date对象拓展
        Date.prototype.Format = function (fmt) {
            //author: meizz 
            var o =
            {
                "M+": this.getMonth() + 1, //月份 
                "d+": this.getDate(), //日 
                "h+": this.getHours(), //小时 
                "m+": this.getMinutes(), //分 
                "s+": this.getSeconds(), //秒 
                "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
                "S": this.getMilliseconds() //毫秒 
            };
            if (/(y+)/.test(fmt))
                fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o)
                if (new RegExp("(" + k + ")").test(fmt))
                    fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            return fmt;
        }


        Date.prototype.addDays = function (d) {
            this.setDate(this.getDate() + d);
        };


        Date.prototype.addWeeks = function (w) {
            this.addDays(w * 7);
        };


        Date.prototype.addMonths = function (m) {
            var d = this.getDate();
            this.setMonth(this.getMonth() + m);

            if (this.getDate() < d)
                this.setDate(0);
        };


        Date.prototype.addYears = function (y) {
            var m = this.getMonth();
            this.setFullYear(this.getFullYear() + y);

            if (m < this.getMonth()) {
                this.setDate(0);
            }
        };
        $(function () {
            if ($("#cur_name").val() != "") {
                $("#d_login").addClass("hidden");
                if (cus.isTutor($("#cur_ident").val())) {
                    $("#d_user_tutor .name").html($("#cur_name").val() + "，导师");
                    $("#d_user_tutor").removeClass("hidden");
                }
                else {
                    $("#d_user_stu .name").html($("#cur_name").val());
                    $("#d_user_stu").removeClass("hidden");
                }
            }
            //登录框提示
            if (IsLogin()) {
                var s = $("#cur_tsta").val();
                var t = $("#t_sta_tip");
                if (s == "1") {
                    t.html("预约实验，请先进入[<a href='UserCenter.aspx?act=info'>个人信息</a>]指定导师。");
                }
                else if (s == "2") {
                    t.html("预约实验，需等待导师审核通过。");
                }
                else if (s == "3") {
                    t.html("您未获得导师许可预约实验。");
                }
                else if (s == "4") {
                    t.html("您已指定导师，请点击[<a href='DevList.aspx'>预约仪器</a>]进行预约。");
                }
            }
            //注册点击类别
            $("#sel_cls").find("li").click(function () {
                search($(this).val());
            });
            //注册点击实验室
            $("#sel_lab").find("li").click(function () {
                search('', $(this).val());
            });
            //初始化最新仪器
            $("#new_dev_list").find("li").mouseover(function () {
                var id = $(this).val();
                $("#new_dev_img").find("img").each(function () {
                    var val = $(this).attr("id");
                    if (val == id) {
                        $(this).removeClass("hidden");
                    }
                    else {
                        $(this).addClass("hidden");
                    }
                });
            });
            $("#new_dev_list").find("li:first").trigger("mouseover");
            //初始化统计图
            $('#container').highcharts({

                chart: {
                    type: 'column'
                },

                title: {
                    text: ''
                },
                colors: ['#058DC7', '#8bbc21'],
                xAxis: {
                    categories: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                },

                yAxis: {
                    allowDecimals: false,
                    min: 0,
                    title: {
                        text: ''
                    }
                },
                tooltip: {
                    headerFormat: '<span>{point.key}</span>',
                    pointFormat: '<div><span style="color:{series.color};padding:0;">{series.name}: </span>' +
                        '<span style="padding:0"><b>{point.y:.1f} 时</b></span></div>',
                    shared: true,
                    useHTML: true
                },

                plotOptions: {
                    column: {
                        stacking: 'normal'
                    }
                },
                credits: {
                    enabled: false
                },
                series: [{
                    name: '工作日',
                    data: [<%=wUseTimes%>],
                    stack: 'male'
                }, {
                    name: '非工作日',
                    data: [<%=rUseTimes%>],
                    stack: 'female'
                }]
            });
            //日期初始化
            var today = new Date();
            var y = today.getFullYear();
            var m = today.getMonth();
            var ys = "";
            var ms = "";
            for (var i = 0; i < 5; i++) {
                var k = y - i;
                ys += "<option value='" + k + "'>" + k + "年</option>";
            }
            for (var j = 0; j < 12; j++) {
                ms += "<option value='" + j + "'>" + (j+1) + "月</option>";
            }
            $(".rank_y_sel").html(ys);
            $(".rank_m_sel").html(ms);
            $(".rank_m_sel").val(m);
            $(".trigger_y_rank").trigger("change");
            $(".trigger_m_rank").trigger("change");
        });
        function devMRank(sel) {
            var y = $(sel).parent().find(".rank_y_sel").val();
            var m = parseInt($(sel).val());
            var date = new Date(y, m, 1);
            var start = date.Format("yyyyMMdd");
            date.addMonths(1);
            var end = date.Format("yyyyMMdd");
            devRank(start, end, 10, "get_dev_m_rank");
        }
        function devYRank(sel) {
            var y = parseInt($(sel).val());
            var date = new Date(y, 0, 1);
            var start = date.Format("yyyyMMdd");
            date.addYears(1);
            var end = date.Format("yyyyMMdd");
            devRank(start, end, 10, "get_dev_y_rank");
        }
        function devRank(start,end,need,act) {
            $.ajax({
                type: "GET",
                url: "Ajax_Code/utilFun.aspx?act="+act+"&start=" + start+"&end="+end+"&need="+need,
                dataType: "json",
                success: function (rlt) {
                    if (rlt.ret == "0") {
                        MessageBox(rlt.msg);
                    }
                    else if (rlt.ret == "1") {
                        var devs = $(rlt.data);
                        var list = "";
                        devs.each(function (i) {
                        list += i % 2 == 0 ? "<tr class='odd'>" : "<tr>";
                        list += "<td>" + cus.cutStrT(this.name, 10) + "</td><td>" + this.count + "</td><td>" + this.time + " 分钟</td></tr>";
                        });
                        if (rlt.act == "get_dev_y_rank") {
                            $(".rank_y_tbl").html(list);
                        }
                        else if (rlt.act == "get_dev_m_rank") {
                            $(".rank_m_tbl").html(list);
                        }
                    }
                },
                error: function (err) {
                    MessageBox("异步连接返回异常！");
                }
            });
        }
        function onDSubmit() {
            if ($("#d_id").val() == "") {
                MessageBox("帐号不能为空!");
                return;
            }
            MemberAjax({
                Prm: 'act=dlogin&' + $("#d_login_dialog").serialize(),
                Function: function (rlt) {
                    if (rlt.MsgId == 0) {
                        location.reload();
                    }
                    else {
                        var verf = document.getElementById("verf_img");
                        verf.src = verf.src + '?';
                        $("#number").val("");
                        if (rlt.MsgId == 2) {

                            $("#rid").val($("#d_id").val());
                            $("#regdialog").dialog('open');
                        }
                        MessageBox(rlt.Message);
                    }
                }
            });
            return false;
        }
        function search(a, b) {
            var cls = '';//类别
            if (a != undefined && a != '') {
                cls = a;
            }
            var lab = '';//实验室
            if (b != undefined && b != '') {
                lab = b;
            }
            var key = $("#keyword").val();
            window.location.href = "DevList.aspx" + "?key=" + encodeURI(key) + "&cls=" + cls + "&lab=" + lab;
        }
        //滚动
        function AutoScroll(obj) {
            $(obj).animate({
                marginTop: "-31px"
            }, 500, function () {
                $(this).css({ marginTop: "0px" }).find("tbody tr:first").appendTo(this);
            });
        }
        $(document).ready(function () {
            setInterval('$(".scroll_tbl").each(function () {if ($("tr", this).length > 7) AutoScroll(this);})', 2000)
        });
        //用户注册
        function regist() {
            pro.d.lg.registAcc(function (rlt) {
                uni.msgBoxR("注册成功！")
            }, undefined, " 本校用户使用一卡通账户与密码登录，无需注册。")
        }
    </script>
    <div class="g-b-m">
        <Uni:dlg_lg ID="Dlg_lg1" runat="server" />
        <div class="g-bm-l" runat="server" id="devByCls" style="border-bottom: none;">
        </div>
        <div class="g-bm-r">
            <div id="panel_login" class="m-box2">
                <div class="box-h">用户登录 LOGIN<span id="" style="float: right; padding-right: 10px; font-size: 14px;"><a href="<%=Page.ResolveClientUrl("~/Pages/LoginForm.aspx")%>">登录管理端</a></span></div>
                <div class="box-c">
                    <!-- 登陆表单 begin -->
                    <div id="d_login" class="lg_panel">
                        <form id="d_login_dialog" onsubmit="return false;">
                            <div>
                                <p><span style="color: #666; font-weight: 600; padding-left: 10px;">▪ 预约请登录</span><span style="color: #888; text-align: left; margin: 2px;">(使用一卡通账户与密码登录)</span></p>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>帐<span style="width: 1em; display: inline-block;"></span>号</td>
                                        <td>
                                            <input name="d_id" id="d_id" maxlength="20" type="text" class="input_txt" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>密<span style="width: 1em; display: inline-block;"></span>码</td>
                                        <td>
                                            <input name="d_pwd" id="d_pwd" type="password" maxlength="14" value="" class="input_txt" /></td>
                                    </tr>
                                    <tr>
                                        <td>验证码</td>
                                        <td>
                                            <input id="number" name="number" style="font-size: 14px; vertical-align: middle"
                                                type="text" class="verf_txt" value="" size="6" maxlength="4" />
                                            <img id="verf_img" src="image.aspx" style="vertical-align: middle" alt="看不清，点击更换" onclick="this.src=this.src+'?'" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div style="text-align: center; margin: 8px 0;">
                                <input type="button" id="d_submit" class="lg_bt button" value="登录" onclick="onDSubmit()" />
                                <div style="line-height: 40px; font-size: 14px;">
                                <a class="click" onclick="regist()">校外用户注册</a> | 
                                <a class="reg click" onclick="$('#regdialog').dialog('open')">新用户激活</a>
                                </div>
                            </div>
                        </form>
                    </div>
                    <!-- 登陆表单 end-->
                    <div id="d_user_stu" class="hidden panel_user lg_panel">
                        <p>您好，<span class="name"></span></p>
                        <p><%=tooltip %></p>
                        <div style="text-align: center;">
                            <table>
                                <tbody>
                                    <tr>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?act=resv'"><a>我的预约</a></span></td>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?act=info'"><a>个人信息</a></span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?act=data'"><a>实验数据</a></span></td>
                                        <td><span class="button" onclick="location.href='DevList.aspx'"><a>预约仪器</a></span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?act=achi'"><a>成果管理</a></span></td>
                                        <td class="logout"><span class="button"><a>退出登录</a></span></td>
                                    </tr>
                                </tbody>
                            </table>
                            <p>点击以上功能按钮进行操作。</p>
                        </div>
                    </div>
                    <div id="d_user_tutor" class="hidden panel_user lg_panel">
                        <p>您好，<span class="name"></span></p>
                        <p>欢迎使用浙江大学985IAES仪器共享使用平台</p>
                        <div style="text-align: center;">
                            <table>
                                <tbody>
                                    <tr>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?act=resv'"><a>我的预约</a></span></td>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?act=info'"><a>个人信息</a></span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="button" onclick="location.href='Course.aspx?act=check'"><a>管理学生</a></span></td>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?act=data'"><a>实验数据</a></span></td>
                                    </tr>
                                    <tr>
                                        <td><span class="button" onclick="location.href='UserCenter.aspx?act=achi'"><a>成果管理</a></span></td>
                                        <td class="logout"><span class="button"><a>退出登录</a></span></td>
                                    </tr>
                                </tbody>
                            </table>
                            <p>点击以上功能按钮进行相关操作。</p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="m-box2">
                <div class="box-h">仪器查询</div>
                <div id="search" class="box-c">
                    <div style="margin-top: 5px;">
                        <input id="keyword" type="text" style="padding: 0px; line-height: 26px; height: 26px; width: 200px;" /><input type="button" id="search_bt" value="搜索" onclick="search()" />
                    </div>
                </div>
            </div>
            <div class="m-box2">
                <div class="box-h">研究方向</div>
                <div id="sel_lab" class="box-c">
                    <ul runat="server" id="labList" class="m_list2">
                    </ul>
                </div>
            </div>
            <div class="tags m-box2">
                <ul class="tag_head box-h">
                    <li class="f"><a>仪器月排行</a></li>
                    <li class="l"><a>仪器年排行</a></li>
                </ul>
                <div class="tag_con box-c" style="min-height: 260px;">
                    <div class="m-list1">
                        <div class="rank_date"><select class="rank_y_sel"></select><select class="rank_m_sel trigger_m_rank" onchange="devMRank(this)"></select></div>
                        <table style="height:350px;">
                            <thead class="list-h">
                                <tr>
                                    <th style="width: 130px;">仪器名称</th>
                                    <th>使用次数</th>
                                    <th style="width: 90px;">使用时长</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="moonRank" class="list-c rank_m_tbl">
                                <tr><td colspan="20">加载中...</td></tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="m-list1">
                        <div class="rank_date"><select class="rank_y_sel trigger_y_rank" onchange="devYRank(this)"></select></div>
                        <table style="height:350px;">
                            <thead class="list-h">
                                <tr>
                                    <th style="width: 130px;">仪器名称</th>
                                    <th>使用次数</th>
                                    <th style="width: 90px;">使用时长</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="yearRank" class="list-c rank_y_tbl">
                                <tr><td colspan="20">加载中...</td></tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="g-bm-all">
            <div class="m-box1">
                <div class="box-h">
                    最新预约
                </div>
                <div class="box-c">
                    <div class="m-list1">
                        <table>
                            <thead class="list-h">
                                <tr>
                                    <th style="width:60px;">预约人</th>
                                    <th style="width:142px;">部门</th>
                                    <th style="width:60px;">导师</th>
                                    <th style="width:142px;">实验内容</th>
                                    <th style="width:160px;">使用仪器名称</th>
                                    <th style="width:186px;">预约时段</th>
                                    <th style="width:99px;">提交时间</th>
                                    <th style="width:74px;">预约状态</th>
                                    <th style="width:58px;">管理员</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td colspan="9" style="overflow:hidden;">
                                        <div style="overflow: hidden;height: 217px;">
                                                                                    <table  class="scroll_tbl">
                            <tbody runat="server" id="newResv" class="list-c">
                                <tr>
                                    <td>暂无数据</td>
                                </tr>
                            </tbody>
                                        </table>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="m-box1">
                <div class="box-h">实时实验</div>
                <div class="box-c">
                    <div class="m-list1">
                        <table>
                            <thead class="list-h">
                                <tr>
                                    <th style="width:200px;">仪器名称</th>
                                    <th style="width:264px;">房间</th>
                                    <th style="width:69px;">使用人</th>
                                    <th style="width:140px;">部门</th>
                                    <th style="width:50px;">导师</th>
                                    <th style="width:108px;">开机时间</th>
                                    <th style="width:60px;">使用状态</th>
                                    <th style="width:50px;">管理员</th>
                                </tr>
                            </thead>
                            <tbody class="list-c">
                                <tr>
                                    <td colspan="8" style="overflow:hidden;">
                                                                               <div style="overflow: hidden;height: 217px;">
                                                                                    <table  class="scroll_tbl">
                            <tbody runat="server" id="curTest" class="list-c">
                                <tr>
                                    <td>暂无数据</td>
                                </tr>
                            </tbody>
                                        </table>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="g-bm-all" style="border-bottom: none;">
            <div class="m-box1 f-fl" style="width: 600px;">
                <div class="box-h">最新仪器展示</div>
                <div class="box-c d_list">
                    <div id="new_dev_list" class="float_l" style="width: 180px; margin: 5px 20px 0 10px;">
                        <ul runat="server" id="newDevs">
                        </ul>
                    </div>
                    <div id="new_dev_img" class="float_l img_list" style="width: 360px;">
                        <div runat="server" id="devImg">
                            <img alt="" src="Theme/images/temp02.jpg" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="m-box2 f-fr" style="width: 370px;">
                <div class="box-h">仪器使用走势图</div>
                <div class="box-c">
                    <div id="container" style="width: 370px; height: 228px;"></div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
