<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ResvForm3.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Modules/ResvTable.ascx" TagPrefix="uc1" TagName="ResvTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">

    <form id="Form1" runat="server">
 
		  <input type="hidden" id="dwTestPlanIDTemp" name="dwTestPlanIDTemp" />
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <table>
                <tr>
                    
                    <th>ѧ�ڣ�</th>
                     <td ><select id="dwYearTerm" name="dwYearTerm">
                       <%=m_TermList %>
                       </select></td>    
                   <th><%=ConfigConst.GCRoomName %>��</th>
                    <td><%=szRoomInfo %></td>
                </tr>
                <tr>
                     <th>�Ͽν�ʦ��</th>
                     <td ><select id="dwTeacherID" name="dwTeacherID"></select></td>    
                     <th>ʵ��ƻ���</th>
                    <td>
                        <select id="dwTestPlanID" name="dwTestPlanID"></select>
                        </td>
                  </tr>
                <tr>
                      <th>ʵ����Ŀ��</th>
                    <td colspan="3">
                        
                       <select id="dwTestItemID" name="dwTestItemID"></select>
                       </td>
                </tr>
                 <tr>
                    <th>��ʼ�ܴΣ�</th>
                    <td>
                        <select id="dwBeginWeeksSec" name="dwBeginWeeksSec"><%=szWeeks %></select>
                       </td>
           
                    <th>�����ܴΣ�</th>
                    <td>
                        <select id="dwEndWeeksSec" name="dwEndWeeksSec"><%=szWeeks %></select></td>
                </tr>
                <tr>
                    <th>���ڣ�</th>
                    <td colspan="3"><%=szWeek %></td>
                </tr>
                <tr>
                     <th>��ʼ�ڴΣ�</th>
                    <td>
                        <select id="dwBeginSec" name="dwBeginSec"><%=szSec %></select>
                       </td>
           
                    <th>�����ڴΣ�</th>
                    <td>
                        <select id="dwEndSec" name="dwEndSec"><%=szSec %></select></td>
                </tr>
                <tr>
                    <td class="tblBtn" colspan="4">
                        <input type="submit" value="ȷ��" id="btnOK" />
                        <input type="button" value="ȡ��" id="btnCancel" /></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <script language="javascript" type="text/javascript">
        $(function () {
            // AutoUserByIdentTeacher($("#szTeacherName"), 2, $("#dwTeacherID"), null, null, null);
            setTimeout(function(){GetUniTeache();},100);
            $("#dwYearTerm").change(function(){
                GetUniTeache();
            });
            var teacherIDTemp = "";
            function GetUniTeache()
            {
                var vYearTerm = $("#dwYearTerm").val();
                    $.get(
                        "../../data/searchUniTeacher.aspx",
                        { YearTerm: vYearTerm },
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
            $("#dwTestPlanID").change(function(){
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
                            for (var i = 0; i < vArrTestItem.length; i++) {
                                $("#dwTestItemID").val( vArrTestItem[i].id);
                            }
                           
                        }
                      );
						}
                        
                    }
                  );
			}
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
                        $.get(
                        "../../data/searchtestitem.aspx",
                        { testPlanID: testplanid },
                        function (data) {
                            var vArrTestItem = eval(data);
                            for (var i = 0; i < vArrTestItem.length; i++) {
                                  testitem.append("<option value='" + vArrTestItem[i].id + "'>" + vArrTestItem[i].label + "</option>");
                                //$("#dwTestItemID").val( vArrTestItem[i].id);
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
                var testitem = $("#dwTestItemID");
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
                     */
            });
            $("#btnOK").button().click(function(){
                var dwTeacherID=$("#dwTeacherID").val();
                var dwTestPlanID=$("#dwTestPlanID").val();
                var dwTestItemID=$("#dwTestItemID").val();
                if(dwTeacherID==null||dwTeacherID==""||dwTestPlanID==null||dwTestPlanID==""||dwTestItemID==null||dwTestItemID=="")
                {
                    MessageBox("�����ú�ʵ����Ŀ", "��ʾ", 2, 0);
                    return false;
                }
                var chk_value =[];//����һ������    
                $('input[name="szWeek"]:checked').each(function(){//����ÿһ������Ϊinterest�ĸ�ѡ������ѡ�е�ִ�к���    
                    chk_value.push($(this).val());//��ѡ�е�ֵ��ӵ�����chk_value��    
                });
                if(chk_value.length==0)
                {
                    MessageBox("�������Ͽ�����", "��ʾ", 2, 0);
                    return false;
                }
            });

            $("#btnCancel").button().click(Dlg_Cancel);
            setTimeout(function () {
                var vStartWeek=<%=uWeeStart%>;//��ʼ�ܴ�
                var vweek=<%=uWeek%>;//����
                var vstartSec=<%=szResvSec%>;//��ʼ�ڴ�
                var vSelectWeek="<%=szResvWeeks%>";//�ύ��ʱ��鿴ѡ�е����ڣ��ش�д��
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
