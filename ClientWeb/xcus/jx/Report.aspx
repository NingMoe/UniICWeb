<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="net/Master.master" CodeFile="Report.aspx.cs" Inherits="ClientWeb_xcus_jx_Report" %>

<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
            <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/linkage/linkage.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/linkage/linkage.css" rel='stylesheet' />
    <style>
        #filter_panel select {width:200px; }
        #filter_panel input {width:200px; }
        .ui-dialog-title { font-family:'Microsoft YaHei';}
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
                level: <%=GetConfig("scheduleMode")=="2"?1:2%>,
                curSelect:[<%=curLink%>,0,0],
                oneLevelData: <%=planLinkList%>,
                getTwoLevelData:function(id,callback){
                    pro.j.test.getTestitemList(id,"",function(rlt){
                        callback(rlt.data);
                    });
                },
                clickOneLevel:function(id){
                    if("<%=GetConfig("scheduleMode")%>"=="2"){
                        pro.j.test.getTestitemList(id,"",function(rlt){
                            var item=rlt.data[0];
                            var para = uni.url2Obj(location.href);
                            para.test_id = item.id;
                            location.href = location.pathname + "?" + uni.obj2Url(para);
                        });
                    }
                },
                clickTwoLevel: function (id, name) {
                    var para = uni.url2Obj(location.href);
                    para.test_id = id;
                    location.href = location.pathname + "?" + uni.obj2Url(para);
                }
            });
        })
    </script>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
    <div class="dialog" id="dlg_correct">
        <form role="form">
  <div class="form-group">
    <label for="correct_score">评分</label>
    <input type="text" class="form-control" id="correct_score" style="width:120px;">
  </div>
  <div class="form-group">
    <label for="comment">评语</label>
    <textarea class="form-control" rows="4" id="comment" placeholder="最多120字"></textarea>
  </div>
</form>
    </div>
            <div class="panel panel-default" style="min-height:420px;">             
                                <div class="panel-heading" id="">
                </div>
                <div class="panel-body" id="filter_panel">
                    <h2><%=curTest %> 实验报告</h2>
                    <div style="padding-top:5px;margin-top:5px;border-top:dashed 1px #ccc;">
                                <h5 class="pull-left">选择实验:&nbsp;</h5>
          <div class="store-selector" >                 
			</div>
                    </div>
                    </div>
                                    <!-- Table -->
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th colspan="2"><span class="text-primary"><span class="glyphicon glyphicon-list"></span> 实验报告列表</span></th>
                            <th></th>
                            <th></th>
                            <th class="text-center text-info" style="width:180px;">考勤</th>
                            <th style="width:70px;"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <%=report_list %>
                    </tbody>
                </table>
            </div>
</asp:Content>
