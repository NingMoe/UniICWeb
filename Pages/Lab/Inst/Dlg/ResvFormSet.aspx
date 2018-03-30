<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ResvFormSet.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Modules/ResvTable.ascx" TagPrefix="uc1" TagName="ResvTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">

    <form id="Form1" runat="server">
        <input type="hidden" id="dwResvID" name="dwResvID" value="0" />
        <input type="hidden" id="dwTeacherID" name="dwTeacherID" />
        <input type="hidden" id="dwTestPlanID" name="dwTestPlanID" />
        <input type="hidden" id="dwTestItemID" name="dwTestItemID" />
        <input type="hidden" id="dwBeginSecHiden" name="dwBeginSecHiden" />
        <input type="hidden" id="dwEndSecHiden" name="dwEndSecHiden" />
        <input type="hidden" id="szWeekHiden" name="szWeekHiden" />
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <table>
                <tr>
                    <th>上课教师：</th>
                    <td><div id="szTeacherName" ></div></td>    
                    <th>实验计划</th>
                    <td><div id="szTestPlanName" ></div></td>    
                </tr>
                 <tr>
                       <th>上课房间：</th>
                    <td>
                        <select id="dwRoom" name="dwRoom"><%=szRoomList %>
                            </select>
                            </td>
                    <th>周次：</th>
                    <td>
                        <select id="dwWeeks" name="dwWeeks"><%=szWeeks %></select>
                       </td>
                </tr>
                <tr>
                    <th>星期：</th>
                    <td colspan="3"><select id="szWeek" name="szWeek"><%=szWeek %></select>  </td>
                </tr>
                <tr>
                     <th>开始节次：</th>
                    <td>
                        <select id="dwBeginSec" name="dwBeginSec"><%=szSec %></select>
                       </td>
           
                    <th>结束节次：</th>
                    <td>
                        <select id="dwEndSec" name="dwEndSec"><%=szSec %></select></td>
                </tr>
                <tr>
                    <td class="tblBtn" colspan="4">
                        <input type="submit" value="确定" id="btnOK" />
                        <input type="button" value="取消" id="btnCancel" /></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <script language="javascript" type="text/javascript">

        $(function () {
            setTimeout(function(){
                $("#szWeek").val($("#szWeekHiden").val());
                $("#dwEndSec").val($("#dwEndSecHiden").val());
                $("#dwBeginSec").val($("#dwBeginSecHiden").val());
            },200);
            AutoUserByIdentTeacher($("#szTeacherName"), 2, $("#dwTeacherID"), null, null, null);
            var teacherIDTemp = "";
            setInterval(function () {         
                var teacherID = $("#dwTeacherID").val();
                if (teacherIDTemp != teacherID)
                {
                    teacherIDTemp = teacherID;
                    var TeacherID=teacherID;
                    $.get(
                    "../../data/searchtestplan.aspx",
                    { TeacherID: TeacherID},
                    function (data) {
                        var vArr = eval(data);
                        var testplan = $("#szTestPlanName");
                        var testitem = $("#szTestItemName");
                        testplan.empty();
                        testitem.empty();
                        var testplanid = 0;
                        for (var i = 0; i < vArr.length; i++)
                        {
                            testplanid = vArr[0].id;
                            testplan.append("<option value='" + vArr[i].id + "'>" + vArr[i].label+ "</option>");
                        }
                        $.get(
                        "../../data/searchtestitem.aspx",
                        { testPlanID: testplanid },
                        function (data) {
                            var vArrTestItem = eval(data);
                            for (var i = 0; i < vArrTestItem.length; i++) {
                                testitem.append("<option value='" + vArrTestItem[i].id + "'>" + vArrTestItem[i].label + "</option>");
                            }
                           
                        }
                      );
                    }
                  );
                }
            }, 5);
            $("#szTestPlanName").change(function () {
                testplanid = $(this).val();
                var testitem = $("#szTestItemName");
                testitem.empty();
                $.get(
                       "../../data/searchtestitem.aspx",
                       { testPlanID: testplanid },
                       function (data) {
                           var vArrTestItem = eval(data);
                           for (var i = 0; i < vArrTestItem.length; i++) {
                               testitem.append("<option value='" + vArrTestItem[i].id + "'>" + vArrTestItem[i].label + "</option>");
                           }

                       }
                     );
            });
            $("#btnOK").button().click(function(){
        
            });

            $("#btnCancel").button().click(Dlg_Cancel);
            setTimeout(function () {
      
                var vStartWeek=<%=uWeeStart%>;//开始周次
                var vweek=<%=uWeek%>;//星期
                var vstartSec=<%=szResvSec%>;//开始节次
                var vSelectWeek="<%=szResvWeeks%>";//提交的时候查看选中的星期，回传写入
                $("#dwBeginWeeksSec").val(vStartWeek);
                $("#dwEndWeeksSec").val(vStartWeek);
                $("#szWeek").val(vStartWeek);
                $("#dwBeginSec").val(vstartSec);
                $("#dwEndSec").val(vstartSec);

                $("[name = szWeek]:checkbox").each(function () {
                    var vVaule= $(this).val();
                    if(vVaule==vweek)
                    {
                        $(this).attr("checked", !$(this).attr("checked"));
                    }
                });
                if(vSelectWeek!="-1")
                { 
                    $("[name ='szWeek']:checkbox").each(function () {
                        var vVaule= $(this).val();
                        if(vSelectWeek.indexOf(vVaule)>-1)
                        {
                            $(this).prop("checked", true);
                        }
                        else
                        {
                            $(this).removeAttr("checked");
                        }
                    });
                }
                
            }, 100);
        });
    </script>
</asp:Content>
