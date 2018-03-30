<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ReserveRoomList.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindRoom %>预约状况</h2>
        <div class="toolbar">
            <input type="hidden" id="szGetKey" name="szGetKey" />
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <%if ((ConfigConst.GCSysKind & 1) > 0)
                      {%>
                    <a href="ReserveRoomList.aspx"><%=ConfigConst.GCSysKindRoom %>预约状况</a>
                    <%} %>
                    <%if ((ConfigConst.GCSysKind & 2) > 0)
                      {%>
                    <a href="ReservePCList.aspx"><%=ConfigConst.GCSysKindPC %>预约状况</a>
                    <%} %>
                    <%if ((ConfigConst.GCSysKind & 4) > 0)
                      {%>
                    <a href="ReserveLendList.aspx"><%=ConfigConst.GCSysKindLend %>预约状况</a>
                    <%} %>
                    <%if ((ConfigConst.GCSysKind & 8) > 0)
                      {%>
                    <a href="ReserveSeatList.aspx"><%=ConfigConst.GCSysKindSeat %>预约状况</a>
                    <%} %>
                </div>

            </div>

        </div>
         <div style="margin:10px;width:99%;">  
             <%if (ConfigConst.GCICTypeMode==1) {%>
             <a id="newAllResv" class="newResv">全体预约</a> 
           <a id="newTeachAllResvImport" class="resvImport">导入全体预约</a>   
             <%} else { %>
           <a id="newAllResvImport" class="resvImport">导入预约</a>   
             <%} %>
        </div>
        <div>
            <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
                <table style="margin: 5px; width: 70%">
                    <tr>
                        <th>开始日期:</th>
                        <td>
                            <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                        <th>结束日期:</th>
                        <td>
                            <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>学工号:</th>
                        <td>
                            <input type="text" name="dwPID" id="dwPID" /></td>
                        <th><%=ConfigConst.GCSysKindRoom %>名称:</th>
                        <td>
                            <input type="text" name="devName" id="devName" /></td>

                    </tr>
                    <tr>
                        <th>状态</th>
                        <td colspan="3">
                            <label>
                                <input class="enum" value="0" type="radio" name="dwCheckStat" checked="checked">全部预约</label>
                            <label>
                                <input class="enum" value="1" type="radio" name="dwCheckStat">待管理员审核</label>
                            <label>
                                <input class="enum" value="2" type="radio" name="dwCheckStat">审核通过</label>
                            <label>
                                <input class="enum" value="4" type="radio" name="dwCheckStat">审核不通过</label>
                            <label>
                                <input class="enum" value="512" type="radio" name="dwCheckStat">生效中</label>
                            <label>
                                <input class="enum" value="262144" type="radio" name="dwCheckStat">违约</label>
                            <label>
                                <input class="enum" value="1073741824" type="radio" name="dwCheckStat">已结束</label>
                        </td>
                    </tr>
                       <tr>
                        <th style="width:100px;"><%=ConfigConst.GCKindName %>名称</th>
                        <td colspan="3">
                          <%=szKindStr %>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4">
                            <input type="submit" id="btnOK" value="查询" style="height: 25px" />
                            <input type="button" id="export" value="导出" style="height: 25px" />
                        </th>
                    </tr>
                </table>

            </div>
        </div>
        <div class="content" style="margin-top: 10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>预约号</th>
                        <th>申请人</th>
                        <th><%=ConfigConst.GCSysKindRoom %>名称</th>
                        <th name="szLabName">所属<%=ConfigConst.GCLabName %></th>
                        <th>状态</th>
                        <th name="dwOccurTime">提交时间</th>
                        <th name="dwBeginTime">申请时间</th>
                        <th>预约人员</th>
                        <th>使用人数</th>
                        <th>主题</th>  
                        <th>申请说明</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">

            $(function () {
                $("#btnOK").button();
                $("#newAllResv").button().click(function () {
                    $.lhdialog({
                        title: '全体预约',
                        width: '750px',
                        height: '420px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/allresv.aspx?op=set'
                    });
                });
                $("#newAllResvImport").button().click(function () {
                    $.lhdialog({
                        title: '导入预约',
                        width: '750px',
                        height: '420px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newAllResvImport.aspx?op=set'
                    });
                });
                $("#newTeachAllResvImport").button().click(function () {
                    $.lhdialog({
                        title: '导入全体预约',
                        width: '750px',
                        height: '420px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newTeachAllResvImport.aspx?op=set'
                    });
                });
                $("#export").button().click(function () {
                    var dwCheckStat = $("input[name='dwCheckStat']:checked").val();
                    var szGetKey = $("#szGetKey").val();
                    var dwPID = $("#dwPID").val();
                    var dwDevKind = $("#dwDevKind").val();
                    var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                    var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                    $.lhdialog({
                        title: '导出记录',
                        width: '200px',
                        height: '90px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/RoomResvExport.aspx?op=set&dwCheckStat=' + dwCheckStat + '&szGetKey=' + szGetKey + '&dwPID=' + dwPID + '&dwDevKind=' + dwDevKind + '&dwStartDate=' + dwStartDate + '&dwEndDate=' + dwEndDate
                    });
                });

                
                
                var tabl = $(".UniTab").UniTab();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                AutoDevice($("#devName"), 1, $("#szGetKey"), 1, null, null, null);
                $(".OPTDGet").html('<div class="OPTDBtn">\
                <a class="GetResv" title="查看"><img src="../../../themes/icon_s/17.png"/></a></a>\
                <a class="setResv" title="修改"><img src="../../../themes/icon_s/18.png"/></a>\
                <a class="beforeDone" title="提前结束"><img src="../../../themes/icon_s/18.png"/></a>\</div>');

                $(".OPTDCheck").html('<div class="OPTDBtn">\
                       <a class="OPTDCheck" title="审核"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="GetResv" title="查看"><img src="../../../themes/icon_s/17.png"/></a>\
                        <a class="setResv" title="修改"><img src="../../../themes/icon_s/18.png"/></a>\
                       <a class="beforeDone" title="提前结束"><img src="../../../themes/icon_s/18.png"/></a></div>');

                $(".OPTDCheckDel").html('<div class="OPTDBtn">\
                        <a class="OPTDCheck" title="审核"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a class="GetResv" title="查看"><img src="../../../themes/icon_s/17.png"/></a>\
                        <a class="delBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                        <a class="setResv" title="修改"><img src="../../../themes/icon_s/18.png"/></a>\
                        <a class="beforeDone" title="提前结束"><img src="../../../themes/icon_s/18.png"/></a></div>');

                $(".OPTDDel").html('<div class="OPTDBtn">\
                       <a class="GetResv" title="查看"><img src="../../../themes/icon_s/17.png"/></a>\
                       <a class="OPTDCheck" title="审核"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                       <a class="beforeDone" title="提前结束"><img src="../../../themes/icon_s/18.png"/></a>\
                       <a class="setResv" title="修改"><img src="../../../themes/icon_s/18.png"/></a>\
                       <a class="adminCredit" title="人工违约"><img src="../../../themes/icon_s/19.png"/></a></div>');
                $(".OPTDCheckok").html('<div class="OPTDBtn">\
                       <a class="GetResv" title="查看"><img src="../../../themes/icon_s/17.png"/></a>\
                       <a class="delBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                       <a class="beforeDone" title="提前结束"><img src="../../../themes/icon_s/18.png"/></a>\
                       <a class="setResv" title="修改"><img src="../../../themes/icon_s/18.png"/></a>\
                       <a class="adminCredit" title="人工违约"><img src="../../../themes/icon_s/19.png"/></a></div>');

                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDCheck").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '审核',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/CheckResv.aspx?op=set&id=' + dwResvID
                    });
                });
                $(".GetResv").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '查看',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ResvGet.aspx?op=set&id=' + dwResvID
                    });
                });
                $(".delBtn").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwResvID);
                    }, '提示', 1, function () { });
                });
                $(".adminCredit").click(function () {
                    var delBtn =$(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '人工违约',
                        width: '700px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/resvOut.aspx?id=' + delBtn
                    });
                });
                $(".beforeDone").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '提前结束',
                        width: '350px',
                        height: '200px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ResvBeforeDone.aspx?op=beforeDone&delID=' + dwResvID
                    });
                });
                $(".resvRdit").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定提前结束?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=resvRdit&delID=" + dwResvID);
                    }, '提示', 1, function () { });
                   });
                
                $("input[name='dwCheckStat']").click(function () {
                    // pForm.data("UniTab_tab1", "reserve.aspx?dwCheckStat=" + $("input[name='dwCheckStat']").val());
                   // TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });
                
                $("#btnNewLab").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewLab.aspx?op=new'
                    });
                });
                $(".setResv").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetICResv.aspx?op=set&resvid=' + dwResvID
                    });
                });
                $(".ListTbl").UniTable();

            });
        </script>
        <style>
            #tbSearch {
                border-width: 1px;
                border-color: #d1c1c1;
                cursor: hand;
            }

                #tbSearch td {
                    font-family: "Trebuchet MS",Monospace,Serif;
                    font-size: 12px;
                    padding-top: 2px;
                    padding-bottom: 2px;
                    padding-left: 15px;
                    padding-right: 15px;
                    border-style: solid;
                    border-width: 1px;
                }

            td input {
                margin-left: 20px;
            }
        </style>
    </form>
</asp:Content>
