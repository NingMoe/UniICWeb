<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ResvForm2.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Modules/ResvTable.ascx" TagPrefix="uc1" TagName="ResvTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">

    <form id="Form1" runat="server">
       <input type="hidden" id="sec" name="sec" />
         <input type="hidden" id="vWeek" name="vWeek" />
        
		  <input type="hidden" id="dwTestPlanIDTemp" name="dwTestPlanIDTemp" />
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <table>
                <tr>
                    
                    <th>房间：</th>
                     <td colspan="3">
                         <%=szRoomList %>
                     </td>    
                  
                </tr>
                <tr>
                    
                    <th>学期：</th>
                     <td ><select id="dwYearTerm" name="dwYearTerm">
                       <%=m_TermList %>
                       </select></td>    
                  <th>上课教师：</th>
                     <td><select id="dwTeacherID" name="dwTeacherID"></select></td>   
                </tr>
                <tr>
                    
                </tr>
                <tr>
                     <th>实验计划：</th>
                    <td>
                        <select id="dwTestPlanID" name="dwTestPlanID"></select>
                        </td>
                      <th>实验项目</th>

                    <td>
                        
                      <select id="dwTestItemID" name="dwTestItemID"></select>
                       </td>
                </tr>
                 <tr>
                    <th>开始周次：</th>
                    <td>
                        <select id="dwBeginWeeksSec" name="dwBeginWeeksSec"></select>
                       </td>
           
                    <th>结束周次：</th>
                    <td>
                        <select id="dwEndWeeksSec" name="dwEndWeeksSec"></select></td>
                </tr>
                <tr>
                    <th>星期：</th>
                    <td colspan="3"><%=szWeek %></td>
                </tr>
                <tr>
                    <th>上课时间:</th>
                    <td colspan="3"><select id="dwResvTime" name="dwResvTime"></select></td>
                    <!--
                     <th>开始节次：</th>
                    <td>
                        <select id="dwBeginSec" name="dwBeginSec"><%=szSec %></select>
                       </td>
           
                    <th>结束节次：</th>
                    <td>
                        <select id="dwEndSec" name="dwEndSec"><%=szSec %></select></td>
                    -->
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
        var vFirst=1;
        $(function () {
            // AutoUserByIdentTeacher($("#szTeacherName"), 2, $("#dwTeacherID"), null, null, null);
            setTimeout(function(){
                GetUniTeache();
            },100);
            setTimeout(function(){
                if($("#vWeek").val()!=null)
                {
                    $("#dwBeginWeeksSec").val($("#vWeek").val());
                    $("#dwEndWeeksSec").val($("#vWeek").val());
                }
                if($("#sec").val()!=null)
                {
                    var vSeond=parseInt($("#sec").val());
                    vSeond=vSeond+1;
                  
                    if((vSeond%2)==0)
                    {
                        var vSevonValue=(vSeond-1)*100+(vSeond);
                    }
                    else
                    {
                        var vSevonValue=(vSeond)*100+(vSeond+1);
                    }
                    $("#dwResvTime").val(vSevonValue);
                }
                vFirst=0;
            },1700);
          
            function GetWeekTime()
            {
                setTimeout(function(){
                    debugger;
                    var dwTestPlanID = $("#dwTestPlanID").val();
                    $.get(
                        "../../data/getopenweek.aspx",
                        { dwTestPlanID: dwTestPlanID },
                        function (data) {
                            var vData = eval(data);
                            $("#dwBeginWeeksSec").empty();
                            $("#dwEndWeeksSec").empty();
                            for (var i = 0; i < vData.length; i++) {
                                var vOption = $("<option value='" + vData[i].id + "'>" + vData[i].label + "</option>");
                                var vOption2 = $("<option value='" + vData[i].id + "'>" + vData[i].label + "</option>");
                                $("#dwBeginWeeksSec").append(vOption);
                                $("#dwEndWeeksSec").append(vOption2);
                                
                            }
                        });
                },400);
            }
            function GetSecTime()
            {
                setTimeout(function(){
                    
                    var dwTestPlanID = $("#dwTestPlanID").val();
                    $.get(
                        "../../data/getopenSec.aspx",
                        { dwTestPlanID: dwTestPlanID },
                        function (data) {
                            var vData = eval(data);
                            
                            $("#dwResvTime").empty();
                            for (var i = 0; i < vData.length; i++) {
                                var vOption = $("<option value='" + vData[i].id + "'>" + vData[i].label + "</option>");
                                $("#dwResvTime").append(vOption);
                                
                            }
                        });
                },400);
            }
            var teacherIDTemp = "";
            function GetUniTeache()//获取教师
            {
                debugger;
                var YearTerm = $("#dwYearTerm").val();
                
                    $.get(
                        "../../data/searchUniTeacher.aspx",
                        { YearTerm: YearTerm },
                        function (data) {
                            var vData = eval(data);
                            $("#dwTeacherID").empty();
                            for (var i = 0; i < vData.length; i++) {
                                var vOption = $("<option value='" + vData[i].id + "'>" + vData[i].label + "</option>");
                                $("#dwTeacherID").append(vOption);
                                teacherIDTemp="";
                            }
                        });
            }
            $("#dwBeginWeeksSec").change(function(){
                var vThisValue=$(this).val();
                $("#dwEndWeeksSec").val(vThisValue);
            });            
            setTimeout(function(){
			var vdwTestPlanID=$("#dwTestPlanIDTemp").val();
			if(vdwTestPlanID!="")
			{
			 $.get(
                    "../../data/searchtestplan.aspx",
                    { "TestPlanID": vdwTestPlanID},
                    function (data) {
					  var testplan = $("#dwTestPlanID");
                        var testitem = $("#dwTestItemID");
						var testplanid=0;
                        var vArr = eval(data);
						if(vArr.length>0)
						{
						testplan.empty();
						testplanid=vArr[0].id;
						 $("#dwTeacherID").val(vArr[0].dwTeacherID);
						 testplan.append("<option value='" + vArr[0].id + "'>" + vArr[0].label+ "</option>");
						$.get(
                        "../../data/searchtestitem.aspx",
                        { testPlanID: testplanid },
                        function (data) {
                            var vArrTestItem = eval(data);
                            $("#dwTestItemID").empty();
                            for (var i = 0; i < vArrTestItem.length; i++) {
                                $("#dwTestItemID").append("<option value='" + vArrTestItem[0].id + "'>" + vArrTestItem[0].label+ "</option>");
                                //$("#dwTestItemID").val( vArrTestItem[i].id);
                            }
                           
                        }
                      );
						}
                        
                    }
                  );
			}
			GetWeekTime();//根据项目来获取开放周次
			GetSecTime();
			},300);
            $("#dwBeginSec").change(function(){
                var vThisValue=$(this).val();
                $("#dwEndSec").val(vThisValue);
            });
            setInterval(function () {   
                var teacherID = $("#dwTeacherID").val();
                var uTerm=$("#dwYearTerm").val();
                if (teacherIDTemp != teacherID&&teacherID!=null)
                {
                    teacherIDTemp = teacherID;
                    var TeacherID=teacherID;
                    $.get(
                    "../../data/searchtestplan.aspx",
                    { TeacherID: TeacherID,uniterm:uTerm},
                    function (data) {
                        var vArr = eval(data);
                        var testplan = $("#dwTestPlanID");
                        var testitem = $("#dwTestItemID");
                        testplan.empty();
                        testitem.empty();
                        var testplanid = 0;
                        for (var i = 0; i < vArr.length; i++)
                        {
                            testplanid = vArr[0].id;
                            testplan.append("<option value='" + vArr[i].id + "'>" + vArr[i].label+ "</option>");
                        }
                        GetWeekTime();
                        GetSecTime();
                        $.get(
                        "../../data/searchtestitem.aspx",
                        { testPlanID: testplanid },
                        function (data) {
                            var vArrTestItem = eval(data);
                            testitem.empty();
                            for (var i = 0; i < vArrTestItem.length; i++) {
                                 testitem.append("<option value='" + vArrTestItem[i].id + "'>" + vArrTestItem[i].label + "</option>");

                               // $("#dwTestItemID").val( vArrTestItem[i].id);
                            }
                           
                        }
                      );
                    }
                  );
                }
            }, 100);
          
            $("#dwTestItemID").change(function () {
                /*
                testplanid = $(this).val();
                var testitem = $("#dwTestPlanID");
                $.get(
                       "../../data/searchtestitem.aspx",
                       { testPlanID: testplanid },
                       function (data) {
                           var vArrTestItem = eval(data);
                           testitem.empty();
                           for (var i = 0; i < vArrTestItem.length; i++) {
                               testitem.append("<option value='" + vArrTestItem[i].id + "'>" + vArrTestItem[i].label + "</option>");
                           }

                       }
                     );
                     */
            });
            $("#btnOK").button().click(function(){
                var dwTeacherID=$("#dwTeacherID").val();
                var dwTestPlanID=$("#dwTestPlanID").val();
                var dwTestItemID=$("#dwTestItemID").val();
                if(dwTeacherID==null||dwTeacherID==""||dwTestPlanID==null||dwTestPlanID==""||dwTestItemID==null||dwTestItemID=="")
                {
                    MessageBox("请设置好实验项目", "提示", 2, 0);
                    return false;
                }
                var chk_value =[];//定义一个数组    
                $('input[name="szWeek"]:checked').each(function(){//遍历每一个名字为interest的复选框，其中选中的执行函数    
                    chk_value.push($(this).val());//将选中的值添加到数组chk_value中    
                });
                if(chk_value.length==0)
                {
                    MessageBox("请设置上课星期", "提示", 2, 0);
                    return false;
                }
            });
            $("#dwTestPlanID").change(function(){
                GetWeekTime();
                GetSecTime();
                var testplanid=$("#dwTestPlanID").val();
                var testitem=$("#dwTestItemID");
                $.get(
                       "../../data/searchtestitem.aspx",
                       { testPlanID: testplanid },
                       function (data) {
                           var vArrTestItem = eval(data);
                           testitem.empty();
                           for (var i = 0; i < vArrTestItem.length; i++) {
                               testitem.append("<option value='" + vArrTestItem[i].id + "'>" + vArrTestItem[i].label + "</option>");

                           }
                           
                       }
                     );
            });
            $("#btnCancel").button().click(Dlg_Cancel);
            setTimeout(function () {
      
                var vStartWeek=<%=uWeeStart%>;//开始周次
                var vweek=<%=uWeek%>;//星期
                var vstartSec=<%=szResvSec%>;//开始节次
                var vSelectWeek="<%=szResvWeeks%>";//提交的时候查看选中的星期，回传写入
                if(vFirst==0)
                {
                    $("#dwBeginWeeksSec").val(vStartWeek);
                    $("#dwEndWeeksSec").val(vStartWeek);
                   
                    $("#dwBeginSec").val(vstartSec);
                    $("#dwEndSec").val(vstartSec);

                }
                $("#szWeek").val(vStartWeek);
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
