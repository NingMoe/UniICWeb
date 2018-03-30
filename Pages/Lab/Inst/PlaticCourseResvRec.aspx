<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="PlaticCourseResvRec.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="dwTeacherID" name="dwTeacherID" /> 
        <input type="hidden" id="dwCourseID" name="dwCourseID" /> 
          <h2 style="margin-top:10px;font-weight:bold">课程考情统计表</h2>
            <div class="toolbar">
        <div class="tb_info">
            <div class="UniTab" id="tabl">
               <a href="PlaticTeachResv.aspx" id="PlaticTeachResv">当前考勤情况</a>
                <a href="PlaticTeachResvRec.aspx" id="PlaticTeachResvRec">考勤记录</a>
                <a href="PlaticCourseResvRec.aspx" id="PlaticCourseResvRec">课程考情统计表</a>
                <a href="PlaticLabResvRec.aspx" id="PlaticLabResvRec">实验室到课率统计</a>
            </div>
    </div>
                </div>
        <div style="margin-top:10px;width:99%;">
             <div style="margin-top:5px;width:99%;">  
             <a class="button" id="outplan">导出</a>
        </div>
            </div>
         <div>
              <div  class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">
                        <tr>
                            <th>学期:</th>
                            <td colspan="3">
                            <select id="dwYearTermSelect" name="dwYearTermSelect">
                                <%=szTerm %>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th>开始日期:</th>
                            <td>
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th>结束日期:</th>
                            <td>
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                        </tr>
                        <tr>
                            <th class="thHead">课程:</th>
                            <td class="tdHead">
                                <input type="text" name="courseName" id="courseName" />

                            </td>
                            <th>教师名称:</th>
                               <td><input type="text" name="szPid" id="szPid" /></td>
                        </tr>
                        <tr>
                            <th colspan="4">
                                <input type="submit" id="btnOK" value="查询" /></th>
                        </tr>
                    </table>
                </div>
           <div class="content">
            <table class="ListTbl">
                <thead>
                     <tr>
                       
        <th>课程名称</th>
        <!--<th>实验名称</th>-->
        <th>教师</th>
        <th>房间</th>
        <th>班级</th>
        <th>上课人数</th>
        <th title="离下课时间（到课率低于50%）">低于50%</th>
        <th>上课时间</th>
        <th>到课率</th>
        <!--<th>早退情况</th>-->
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
               </div>
        <script type="text/javascript">
            $("#outplan").button();
            $("#dwYearTermSelect").change(function () {
                var vVal = $("#dwYearTermSelect").val();
                if (vVal != 0) {
                    var vStart = vVal.substring(0, 8);
                    var stratDate = vStart.substring(0, 4) + '-' + vStart.substring(4, 6) + '-' + vStart.substring(6, 8);
                    $("#<%=dwStartDate.ClientID%>").val(stratDate);
                    var vEnd = vVal.substring(8, 16);
                    var endDate = vEnd.substring(0, 4) + '-' + vEnd.substring(4, 6) + '-' + vEnd.substring(6, 8);
                    $("#<%=dwEndDate.ClientID%>").val(endDate);
                }
             });
            AutoCourseName($("#courseName"), 1, $("#dwCourseID"), 1, false);
            AutoUserByName($("#szPid"), 1, $("#dwTeacherID"), null, null, null);
           $("#outplan").click(function () {
                    var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                     var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                     var dwTeacherID=$("#dwTeacherID").val();
                     var dwCourseID = $("#dwCourseID").val();
                    $.lhdialog({
                        title: '导出',
                        width: '200px',
                        height: '90px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/platicCourseResvOutExport.aspx?op=set&dwStartDate=' + dwStartDate + "&dwEndDate=" + dwEndDate + "&dwTeacherID=" + dwTeacherID + "&dwCourseID=" + dwCourseID
                    });

                });
            $(function () {
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="playVideo" href="#" title="播放监控"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".UniTab").UniTab();
                $("#btnOK").button();
                $(".useRate").click(function () {
                    var dwCourseID = $(this).parents("tr").children().first().attr("dwCourseID");
                    var dwTestPlanID = $(this).parents("tr").children().first().attr("dwTestPlanID");
                    var dwTeacherID = $(this).parents("tr").children().first().attr("dwTeacherID");
                    var time = $(this).parents("tr").children().first().attr("time");
                    var dwResvID = $(this).parents("tr").children().first().attr("dwResvID");
                    $.lhdialog({
                        title: '查看图表',
                        width: '780px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/attendstudentchar.aspx?op=set&time=' + time + '&dwResvID=' + dwResvID + '&dwTeacherID=' + dwTeacherID + '&dwTestPlanID=' + dwTestPlanID + '&dwCourseID=' + dwCourseID
                    });
                });
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
                $(".playVideo").click(function () {
                    var roomno = $(this).parents("tr").children().first().attr("data-roomno");
                    var time = $(this).parents("tr").children().first().attr("data-time");
                    $.lhdialog({
                        title: '播放监控',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/playVideo.aspx?op=set&szRoomNO=' + roomno+'&time='+time
                    });
                });
                $("#roomName").autocomplete({
                    source: "../data/GetRoomList.aspx?ctrlType=1",
                    minLength: 0,
                    select: function (event, ui) {
                        if (ui.item) {
                            if (ui.item.id && ui.item.id != "") {
                                $("#roomName").val(ui.item.label);
                                $("#szGetKey").val(ui.item.id);
                            }
                        }
                        return false;
                    },
                    response: function (event, ui) {
                        if (ui.content.length == 0) {
                            ui.content.push({ label: " 未找到配置项 " });
                        }
                    }
                }).blur(function () {
                    if ($(this).val() == "") {
                        $("#szGetKey").val("");
                    } else {

                    }
                }).click(function () {
                    $("#roomName").autocomplete("search", "");
                });
            });
        </script>
         <style>
              .ui-datepicker select.ui-datepicker-year { width: 43%;}
              .tb_infoInLine td input {
            width:120px;
            }
             .useRate {
             text-decoration:underline;
             }
        </style>
    </form>
</asp:Content>
