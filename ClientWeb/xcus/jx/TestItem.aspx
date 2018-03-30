<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestItem.aspx.cs" MasterPageFile="net/Master.master" Inherits="ClientWeb_xcus_jx_TestItem" %>
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
                    var panel = $("<div>" + $("#panel_resv_" + $(this).attr("test_id")).html() + "</div>");
                    //关闭
                    var pthis = $(this);
                    var close = $("<span class='close' style='margin-top: -40px;'>x</span>").click(function () { pthis.popover("hide"); pthis.next(".popover").remove(); });
                    panel.prepend(close);
                    if ($("table tr", panel).length == 0) {
                        $("table tbody", panel).html("<tr><td>无预约</td></tr>");
                    }
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
        })
        function upload(sid, testId, testName) {
            $(".sid").val(sid);
            $(".test_name").html(testName);
            $("#btn_upload").attr("dir", testId + "/");
            uni.dlg($("#dlg_upload"), "上传实验报告", 400, 300);
        }
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
                    <button type="button" class="btn btn-info pull-right" style="display:<%=(ToUInt(GetConfig("testPlanKind"))&2)>0?"":"none"%>;margin-top:50px;" onclick="location.href='SelTest.aspx'">教学开放实验选课 >></button>
                    <h1>本学期实验课程</h1>
                    <h5 class="text-info"><span class="glyphicon glyphicon-time" style="font-size:16px;"></span> <span class="h_today"></span> 第 <code class="h_weeks"></code> 周 星期 <code class="h_week"></code></h5>
                    <div class="line"></div>
                    </div>
                        <%=resultList %>
            </div>
                <div class="panel_resv_list hidden">
                <%=resvPanelList %>
            </div>
</asp:Content>
