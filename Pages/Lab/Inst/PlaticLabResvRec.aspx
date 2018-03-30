<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="PlaticLabResvRec.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="dwAccNo" name="dwAccNo" /> 
        <input type="hidden" id="dwCourseID" name="dwCourseID" /> 
          <h2 style="margin-top:10px;font-weight:bold">实验室到课率统计</h2>
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
        <th>实验名称</th>
        <th>教师</th>
        <th>房间</th>
        <th>班级</th>
     
        <th>到课人数</th>
        <th>上课时间</th>
      
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
            AutoCourseName($("#courseName"), 1, $("#dwCourseID"), 1, false);
            AutoUserByName($("#szPid"), 1, $("#dwAccNo"), null, null, null);
            $(function () {
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
                $(".UniTab").UniTab();
                $("#btnOK").button();
                $(".attendUser").click(function () {
                    var dwCourseID = $(this).parents("tr").children().first().attr("dwCourseID");
                    var dwTestPlanID = $(this).parents("tr").children().first().attr("dwTestPlanID");
                    var dwTeacherID = $(this).parents("tr").children().first().attr("dwTeacherID");
                    var time = $(this).parents("tr").children().first().attr("time");
                    var dwResvID = $(this).parents("tr").children().first().attr("dwResvID");
                    $.lhdialog({
                        title: '到课情况',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/attendStudentRec.aspx?op=set&time=' + time + '&dwResvID=' + dwResvID + '&dwTeacherID=' + dwTeacherID + '&dwTestPlanID=' + dwTestPlanID + '&dwCourseID=' + dwCourseID
                    });
                });
                
            });
        </script>
         <style>
              .ui-datepicker select.ui-datepicker-year { width: 43%;}
              .tb_infoInLine td input {
            width:120px;
            }
             .attendUser {
             text-decoration:underline;
             }
           
        </style>
    </form>
</asp:Content>
