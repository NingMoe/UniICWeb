<%@ Page Language="C#" MasterPageFile="Modules/Master.master" AutoEventWireup="true" CodeFile="DevList.aspx.cs" Inherits="_Default" %>

<%@ Register TagPrefix="Uni" TagName="leftMenu" Src="Modules/LeftMenu.ascx" %>
<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        $(function () {
            //初始化分页控件
            $("#pCtrl").jsonPageCtrl(function (pageCtrlID, startLine, needLine) {
                SubmitRet(RetSelecteds('pctrl'));
            }, 16);

            //获取搜索关键字
            var Request = new QueryString();
            var key = Request["key"];
            if (key != undefined && key != '') {
                $("#keyword").val(decodeURI(key));
            }
            var cls = Request["cls"];
            if (cls != undefined && cls != '') {
                $("#cls a").each(function () {
                    if ($(this).attr('name') == cls) {
                        $(this).addClass("seled");
                        $("input", this).attr('checked', true);
                    }
                });
            }
            var lab = Request['lab'];
            if (lab != undefined && lab != '') {
                $("#lab a").each(function () {
                    if ($(this).attr('name') == lab) {
                        $(this).addClass("seled");
                        $("input", this).attr('checked', true);
                    }
                });
            }
            //为filter下的所有a标签添加单击事件
            $("#filter a").click(function () {
                if ($(this).hasClass("seled")) {
                    $(this).removeClass("seled");
                    $("input", this).attr('checked', false);
                }
                else {
                    $(this).addClass("seled");
                    $("input", this).attr('checked', true);
                }
                SubmitRet(RetSelecteds());
            });
            SubmitRet(RetSelecteds()); //首次返回结果
        });

        function RetSelecteds(act) {
            var result = "";
            //仪器关键字
            result += $("#keyword").val();
            result += '&';
            //管理员关键字
            result += $("#man_key").val();
            result += '&';
            //条件
            $("#filter .it").each(function () {
                var str = '';
                $("a.seled", this).each(function () {
                    str += $(this).attr('name') + ",";
                });
                result += (str == '' ? '' : str.substring(0, str.length - 1)) + '&';
            });
            //分页
            result += "pCtrl&";
            if (act == 'pctrl') {
                result += $("input[name='pCtrl_dwStartLine']").attr('value') + "&";
            }
            else {
                result += "0&";
            }
            result += $("input[name='pCtrl_dwNeedLines']").attr('value');
            //显示模式
            $("#show_mode input").each(function () {
                if ($(this).attr("checked")) {
                    result += "&" + $(this).val();
                }
            });

            return result;
        }
        function SubmitRet(condition) {
            ShowWait();
            $.ajax({
                url: 'Ajax_Code/devFilter.aspx',
                dataType: 'json',
                data: { con: condition },
                success: function (rlt) {
                    var content = '';
                    var devs = rlt.devs;
                    $(devs).each(function (i) {
                        var pthis = $(this);
                        var id = pthis.attr('id');
                        var name = pthis.attr('name');
                        var model = pthis.attr('model');
                        var url = pthis.attr('url');
                        url = url.substring(3);
                        var campus = pthis.attr('campus');
                        var col = pthis.attr('col');
                        var cls = pthis.attr('cls');
                        var lab = pthis.attr('lab');
                        var manager = pthis.attr("manager");
                        var phone = pthis.attr("phone");
                        var intro = pthis.attr('intro');
                        var devstat = pthis.attr('devstat');
                        var runstat = pthis.attr('runstat');
                        if (rlt.showMode == 0) {
                            content += "<div class='m-box'><div class='box_h'><a href=\"DevDetail.aspx?dev=" + id + "\" title=\"点击查看详细\"><img alt='" + name + "' src='" + url + "' onerror='Upload/DevImg/dft.jpg'/></a></div><div class='box_c'>" +
                            "<ul><li class='name'>" + cutStrT(name, 14) + "</li><li>类型：" + cutStrT(cls,14) + "</li><li>方向：" + cutStrT(lab,14) + "</li><li class='f-tar'>" +
                            "<a href='DevDetail.aspx?act=achi&dev=" + id + "'>成果</a> | <a href=\"DevDetail.aspx?dev=" + id + "\" class=\"\">详细</a> | <a href=\"DevDetail.aspx?act=resv&dev=" + id + "\" onclick=\"return isloginTu()\"> 预约>> </a>" + "</li></ul>"
                        + "</div></div>";
                        }
                        else if (rlt.showMode == 1) {
                            content += "<tr class='" + (i % 2 == 0 ? 'odd' : '') + "'><td>" + (rlt.startLine + i) + "</td><td style='text-align:left;'>" + name + "</td><td style='text-align:left;'>" + model + "</td><td>" + kind + "</td><td>" + lab + "</td>" +
                                "<td><span title='联系方式：" + phone + "'>" + manager + "</span></td><td>" + devstat + "</td><td><a href='DevDetail.aspx?act=achi&dev=" + id + "'>成果</a> | <a href=\"DevDetail.aspx?act=resv&dev=" + id + "\" class=\"\" onclick=\"return isloginTu()\">预约</a>" +
            "|<a href=\"DevDetail.aspx?dev=" + id + "\" class=\"\">详细</a></td></tr>";
                        }
                    });
                    if (rlt.showMode == 0) {
                        $("#boxes").html(content).show();
                        $("#devlist").parents("table").hide();
                    }
                    else if (rlt.showMode == 1) {
                        $("#devlist").html(content);
                        $("#devlist").parents("table").show();
                        $("#boxes").hide();
                    }
                    updatePageCtrl(rlt.pageCtrlID, rlt.totolLines, rlt.startLine, rlt.needLines);
                    $('#totol').html(rlt.totolLines);
                    HideWait();
                },
                error: function (err) {
                    MessageBox("异步连接返回错误！");
                    HideWait();
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="g-b-m">
        <%--        <Uni:leftMenu ID="leftMenu" runat="server" />--%>
        <div id="content" class="panel_devlist">
            <div id="position">当前位置：<a href="Default.aspx">首页</a> > 仪器共享</div>
            <div id="filter" class="grey_box">
                <div class="h_grey">仪器搜索</div>
                <div class="c_grey">
                    <table class="its">
                        <tr class="it" id="cls">
                            <td class="th">仪器类型：</td>
                            <td class="list">
                                <%=ClsList %>
                            </td>
                        </tr>
                        <tr class="it" id="lab">
                            <td class="th">研究方向：</td>
                            <td class="list">
                                <%=LabList %>
                            </td>
                        </tr>
                    </table>
                    <div class="keys">
                        <div class="f-tal">
                            <span class="title">仪器名称</span>
                            <span>
                                <input id="keyword" type="text" />
                            </span>
                            <span class="title">管<span style="width: 0.5em; display: inline-block;"></span>理<span style="width: 0.5em; display: inline-block;"></span>员</span>
                            <span>
                                <input id="man_key" type="text" />
                            </span>
                        </div>
                    </div>
                    <div class="f-tac">
                        <input type="button" class="button" value="搜索" style="height: 30px; width: 80px; cursor: pointer;" onclick="SubmitRet(RetSelecteds())" />
                    </div>
                </div>
            </div>
            <div id="rlt_con">
                <div id="show_mode" class="hidden"><span>显示方式：</span><span>列表</span><input type="radio" id="showtbl" name="check_mode" value="1" onclick="SubmitRet(RetSelecteds());" /><span>框</span><input type="radio" id="showbox" name="check_mode" checked="checked" value="0" onclick="    SubmitRet(RetSelecteds());" /></div>
                <div class='m-boxes'>
                    <div class="devtbl_head"><span style="color: #333;">搜索结果： 总  <span id="totol" style="color: #007dc1"></span>条记录</span></div>
                    <div id='boxes'>
                    </div>
                </div>
                <div id="devtbl" style="display: none">
                    <div class="devtbl_head">搜索结果</div>
                    <div class="m-list1">
                        <table>
                            <thead class="">
                                <tr class="title">
                                    <th>序号</th>
                                    <th>仪器名称</th>
                                    <th>仪器型号</th>
                                    <th>仪器类型</th>
                                    <th>研究方向</th>
                                    <th>管理员</th>
                                    <th>仪器状态</th>
                                    <th>操作</th>
                                </tr>
                            </thead>
                            <tbody id="devlist" class="list-c">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div id="pCtrl"></div>
        </div>
        <div class="cleaner"></div>
    </div>
    <!-- END of templatemo_main -->

</asp:Content>


