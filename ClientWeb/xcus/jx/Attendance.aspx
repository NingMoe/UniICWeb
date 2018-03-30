<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="net/Master.master" CodeFile="Attendance.aspx.cs" Inherits="ClientWeb_xcus_jx_Attendace" %>

<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
            <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/linkage/linkage.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/linkage/linkage.css" rel='stylesheet' />
    <style>
        .ui-dialog-title { font-family:'Microsoft YaHei';}
        .store-selector .content { min-width:590px;}
    </style>
    <script>
        function correct(sid,accno, score, eval) {
            var sc = $("#correct_score");
            var com = $("#comment");
            sc.val(score);
            com.val(eval);
            uni.dlg($("#dlg_correct"), "实验报告评分", 400, 320, function () {
                if(!isNaN(sc.val()))
                pro.j.test.correctReport(sid,accno, sc.val(), com.val(), function () {
                    uni.msgBoxR("评分成功");
                });
            });
        }
        $(function () {
            //选择项目
            $(".store-selector").linkage({
                curSelect:[<%=curLink%>,0],
                oneLevelData: <%=planLinkList%>,
                twoLevelData:<%=testLinkList%>,
                getThreeLevelData:function(id,callback){
                    pro.j.rsv.getTestResv(id,function (rlt) {
                        callback(rlt.data);
                    });
                },
                complete:function (isload,para) {
                    if(para.currentLv){
                        if(!isload){
                            var p=uni.url2Obj(location.href);
                            p.plan_id=para.oneLvId;
                            p.test_id=para.twoLvId;
                            p.resv_id=para.threeLvId;
                            location.href=location.pathname+"?"+uni.obj2Url(p);
                        }
                        else
                            $("#cur_resv_name").html(para.threeLvName);
                    }
                }
            });
        })
    </script>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
            <div class="panel panel-default" style="min-height:420px;">             
                                <div class="panel-heading text-info">
                                    <span><span class="glyphicon glyphicon-pushpin"></span> <span id="cur_resv_name"></span></span>
                </div>
                <div class="panel-body" id="filter_panel">
                    <h5 class="pull-left">选择节次:&nbsp;</h5>
          <div class="store-selector" >                 
			</div>
                    </div>
                                    <!-- Table -->
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th colspan="2"><span class="text-primary"><span class="glyphicon glyphicon-list"></span> 学生考勤列表</span></th>
                            <th>登录名</th>
                            <th>登录机器</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <%=attend_list %>
                    </tbody>
                </table>
            </div>
</asp:Content>
