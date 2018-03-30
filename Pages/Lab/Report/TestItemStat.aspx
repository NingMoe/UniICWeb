<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="TestItemStat.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold">实验室课程项目统计</h2>
         <div style="margin-top:10px;width:99%;">
             <div style="margin-top:5px;width:99%;">  
          
        </div>
            </div>
        <input type="hidden" value="2" name="dwPurpose" />
              <input type="hidden" id="dwTeacherID" name="dwTeacherID" /> 
        <input type="hidden" id="dwCourseID" name="dwCourseID" /> 
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info" style="width:99%">
                <div>
                    <table style="width: 99%">
                         <tr>
                            <th>学期:</th>
                            <td colspan="3">
                            <select id="dwYearTermSelect" name="dwYearTermSelect">
                                <%=szTerm %>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th class="thHead">开始日期:</th>
                            <td class="tdHead">
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <td class="thHead">结束日期:</td>
                            <td class="tdHead">
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
                              <td class="tdHead" colspan="4" style="text-align:center">
                                <input type="submit" id="btnOK" value="查询" />
                                     <a class="button" id="outplan">导出</a>
                              </td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>
        <div style="margin:10px;">
            <table class="ListTbl">
                <thead>
                    <tr>
                     
                          <th>课程编号</th>
						<th  name="szCourseName">课程名称</th>   
                        <th>课程属性</th> 
                           <th  name="szTestName">项目名称</th>   
                        <th>每组人数</th>       
                        <th>学时</th>               
                        <th  name="szTeacherName">教师</th>
                      
                         <th  name="szGroupName">班级</th>
                         <th>班级人数</th>
                        <th>实验室</th>
						<th>设备数</th>
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

                $("#outplan").button();
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
                        content: 'url:Dlg/TestItemOutExport.aspx?op=set&dwStartDate=' + dwStartDate + "&dwEndDate=" + dwEndDate + "&dwTeacherID=" + dwTeacherID + "&dwCourseID=" + dwCourseID
                    });

                });
                AutoCourseName($("#courseName"), 1, $("#dwCourseID"), 1, false);
                AutoUserByName($("#szPid"), 1, $("#dwTeacherID"), null, null, null);
                $(".ListTbl").UniTable();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
            });
        });
        $("#btnOK").button();

        </script>
        <style>
            .thHead
            {
                background: #e5f1f4;
                text-align: right;
            }

            .tdHead
            {
                text-align: left;
            }

            td input
            {
                margin-left: 10px;
            }
        </style>
    </form>
</asp:Content>
