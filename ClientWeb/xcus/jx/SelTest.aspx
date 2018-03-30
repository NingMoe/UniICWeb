<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelTest.aspx.cs" MasterPageFile="net/Master.master" Inherits="ClientWeb_xcus_jx_SelTest" %>
<%@ MasterType VirtualPath="net/Master.master" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
            <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/linkage/linkage.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/linkage/linkage.css" rel='stylesheet' />
    <style>
        ul, li { list-style-type: none; padding-left: 0; }
        #course_panel .popover { min-width: 700px; }
        #course_panel .popover-content { padding: 5px 6px; }
        .panel_test_name { line-height: 20px; font-size: 16px; font-family: 'Microsoft YaHei'; }
        #course_panel table { border-bottom:1px solid #ddd;}
        .plan_kind_2 .h_title {border-color:orange;}
    </style>
    <script>
        $(function () {
            $(".btn_test_resv").popover({
                html: true,
                placement: "bottom",
                title: "实验预约",
                content: function () {
                    var panel = $("<div><span class='close' style='margin-top: -40px;'>x</span><div class='rlist'>正在加载数据...</div></div>");
                    pro.j.objGetS("net/ajax/items.aspx", {act:"get", plan_id:$(this).attr("plan_id"), plan_name: $(this).attr("plan_name") }, function (rlt) {
                        panel.find(".rlist").html(rlt.data);
                    });
                    //关闭
                    var pthis = $(this);
                    $(".close", panel).click(function () { pthis.popover("hide"); pthis.next(".popover").remove();});
                    return panel;
                },
                trigger: "click"
            });
            //日期
            var today = new Date();
            var wwd = pro.dt.date2wwd(today);
            var chi = ['一', '二', '三', '四', '五', '六', '七'];
            $(".h_today").html(uni.formatDate("yyyy年MM月dd日", today));
            $(".h_weeks").html(parseInt(wwd / 10) + "");
            $(".h_week").html(chi[wwd % 10]);
            //上传
            $(".upload_file").uploadFile({}, function (rlt) {
                var sid=$(".sid").val();
                pro.j.test.uploadReport(sid, rlt.data.save, function () {
                    uni.msgBoxR("上传成功，将刷新页面");
                });
            });
            //选课加组
            $(".group_act").click(function () {
                var pthis = $(this);
                var title = pthis.attr("plan_name");
                var gId = pthis.attr("group_id");
                if (pthis.hasClass('join')) {
                    uni.confirm("是否确定加入<span class='red bold'>" + title + "</span>", function () {
                        pro.j.group.addMem(gId, pro.acc.id, function () {
                            uni.msgBoxR("加入成功");
                        });
                    });
                }
                else {
                    uni.confirm("是否确定退出<span class='red bold'>" + title + "</span>", function () {
                        pro.j.group.delMem(gId, pro.acc.id, function () {
                            uni.msgBoxR("退出成功");
                        });
                    });
                }
            });
            //教师过滤
            $(".teacher_filter").procomplete(function (item) {
                var id = item.id;
                $(".tr_tch").hide();
                $(".tch_" + id).show();
            });
        })

    </script>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
    <div class="dialog" id="dlg_upload">
        <form class="form" role="form">
            <input type="hidden" class="sid" name="sid" />
    <div class="panel panel-default">
            <div class="panel-body list">
                <h3 class="test_name"></h3>
                                <div class="input-group" style="width:100%;margin-top:20px;">
                    
                                                            <div style="margin-bottom:5px;">
                            <input type="file" name="report_file_name" id="report_file_name" class="click"/>
                        </div>
                                    <div>
                            <input type="button" id="btn_upload" class="upload_file btn btn-info btn-sm" file="report_file_name" value="上传"/><span class="cur_file_name text-primary" style="padding-left:5px;"></span>
                </div>
                                </div>

            </div>
        </div> 
                        <div class="text-center">
            <button type="button" class="btn btn-default dlg_close">返回</button>
        </div>
        </form>
    </div>
            <div class="panel panel-default" style="min-height:420px;" id="course_panel">             
                                <div class="panel-heading" id="">
                </div>
                <div class="panel-body" id="filter_panel">
    <div class="input-group" style="width:280px;float:right;margin-top:56px;">
        <span class="input-group-addon">教师过滤</span>
      <input type="text" class="form-control teacher_filter" placeholder="教师姓名/工号"  act="truename" url="searchAccount.aspx"  onclick="this.value=''" para="ident=536870912">
    </div>
                    <h1>本学期教学开放实验</h1>
                    <h5 class="text-info"><span class="glyphicon glyphicon-time" style="font-size:16px;"></span> <span class="h_today"></span> 第 <code class="h_weeks"></code> 周 星期 <code class="h_week"></code></h5>
                    <div class="line"></div>
                    </div>
                        <%=testPlanList %>
            </div>
                <div class="panel_resv_list hidden">
                <%=resultList %>
            </div>
</asp:Content>
