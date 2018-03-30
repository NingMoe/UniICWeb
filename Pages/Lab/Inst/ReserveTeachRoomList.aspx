<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ReserveTeachRoomList.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>教学预约状况</h2>
        <div class="toolbar">
            <input type="hidden" id="szGetKey" name="szGetKey" />
            <input type="hidden" id="szRoomNo" name="szRoomNo" />
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="ReserveTeachRoomList.aspx">教学预约状况</a>
                    <a href="ReservePersonRoomList.aspx">个人预约状况</a>
                </div>

            </div>

        </div>
        <div style="margin-top:8px;width:99%;">  
             <a class="button" id="outplan">导出</a>
                <a class="button" id="outWeekResv">导出周日历</a>
        </div>
        <div>
            <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
                <table style="margin: 5px; width: 95%">
                    <tr>
                        <th>开始日期:</th>
                        <td>
                            <input type="text" name="dwBeginDate" id="dwBeginDate" runat="server" /></td>
                        <th>结束日期:</th>
                        <td>
                            <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>学工号:</th>
                        <td>
                            <input type="text" name="dwPID" id="dwPID" /></td>
                        <th><%=ConfigConst.GCRoomName %>名称:</th>
                        <td>
                            <input type="text" name="szRoomName" id="szRoomName" /></td>

                    </tr>
                    <tr>
                        <th>状态</th>
                        <td colspan="3">
                            <label><input class="enum" value="0" type="radio" name="dwCheckStat" checked="checked">全部</label>
                            <label><input class="enum" value="512" type="radio" name="dwCheckStat">生效中</label>
                            <label><input class="enum" value="1073741824" type="radio" name="dwCheckStat">已结束</label>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4">
                            <input type="submit" id="btnOK" value="查询" style="height: 25px" />
                        <!--     <input type="button" id="btnCheckCLSName" value="校正上课班级名称" style="height: 25px" />-->
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
                        <th>项目名称</th>
                        <th>上课教师</th>
                        <th><%=ConfigConst.GCRoomName %>名称</th>
                        <th>班级</th>
                        <th>状态</th>
                        <th name="dwBeginTime">上课时间</th>
                         <th name="dwBeginTime">绝对时间</th>
                        <th width="25px">操作</th>
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
                $("#btnOK,#outplan,#btnCheckCLSName").button();
                var tabl = $(".UniTab").UniTab();
                $("#<%=dwBeginDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
              //  AutoDevice($("#devName"), 1, $("#szGetKey"), 1, null, null, null);
                $("#outWeekResv").button().click(function () {
                    $.lhdialog({
                        title: '导出周日历',
                        width: '450px',
                        height: '220px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/weekCaloutport.aspx'
                    });
                    
                });
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="set" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a></div>');
                $(".OPTDDel").html('<div class="OPTDBtn">\
                       <a class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                AutoRoom($("#szRoomName"), 1, $("#szRoomNo"), null, null);
                $("#szRoomName").on("autocompleteselect", function (event, ui) {
                    setTimeout(function () {
                        $("#szRoomNo").val(ui.item.szRoomNo);
                    }, 200);
                });
                $(".set").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    var dwResvDate = $(this).parents("tr").children().first().attr("data-resvDate");
                    $.lhdialog({
                        title: '修改预约',
                        width: '700px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ResvFormSet.aspx?op=set&id=' + dwResvID + '&date=' + dwResvDate
                    });
                });
                $("#btnCheckCLSName").click(function () {
                    var vResvID = "";
                    $("input[name^='tblSelect']").each(function () {
                        if ($(this).prop("checked") == true) {
                            vResvID = vResvID + $(this).parent().data("id")+',';
                        }
                    });
                    if (vResvID == "") {
                        return;
                    }

                    $.lhdialog({
                        title: '校正上课班级名称',
                        width: '450px',
                        height: '220px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/CheckClsName.aspx?op=set&id=' + vResvID
                    });
                });

                $(".classgroup").click(function () {
                    var dwID = $(this).data("groupid");
                    $.lhdialog({
                        title: '查看上课名单',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetUseGroup.aspx?op=set&id=' + dwID
                    });
                });
                $("#outplan").click(function () {
                    var dwStartDate = $("#<%=dwBeginDate.ClientID%>").val();
                    var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                    var szRoomNo = $("#szRoomNo").val();
                    var dwCheckStat = $("#dwCheckStat").val();
                    var szGetKey = $("#szGetKey").val();
                    var szPid = $("#szPid").val();
                    $.lhdialog({
                        title: '导出',
                        width: '200px',
                        height: '90px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/resvTeachExport.aspx?op=set&dwStartDate=' + dwStartDate + "&dwEndDate=" + dwEndDate + "&szRoomNo=" + szRoomNo + "&szPid=" + szPid
                    });

                });
                $(".delResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                    }, '提示', 1, function () { });
                });
                $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: true });

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
            .classgroup {
            text-decoration:underline;
            }
        </style>
    </form>
</asp:Content>
