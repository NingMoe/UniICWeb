<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="PlaticLabResvRec.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="dwAccNo" name="dwAccNo" /> 
        <input type="hidden" id="dwCourseID" name="dwCourseID" /> 
          <h2 style="margin-top:10px;font-weight:bold">ʵ���ҵ�����ͳ��</h2>
            <div class="toolbar">
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="PlaticTeachResv.aspx" id="PlaticTeachResv">��ǰ�������</a>
                <a href="PlaticTeachResvRec.aspx" id="PlaticTeachResvRec">���ڼ�¼</a>
                <a href="PlaticCourseResvRec.aspx" id="PlaticCourseResvRec">�γ̿���ͳ�Ʊ�</a>
                <a href="PlaticLabResvRec.aspx" id="PlaticLabResvRec">ʵ���ҵ�����ͳ��</a>
            </div>
    </div>
                </div>
         <div>
              <div  class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">
                         <tr>
                            <th>ѧ��:</th>
                            <td colspan="3">
                            <select id="dwYearTermSelect" name="dwYearTermSelect">
                                <%=szTerm %>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th>��ʼ����:</th>
                            <td>
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th>��������:</th>
                            <td>
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                        </tr>
                        <tr>
                            <th class="thHead">�γ�:</th>
                            <td class="tdHead">
                                <input type="text" name="courseName" id="courseName" />

                            </td>
                            <th>��ʦ����:</th>
                               <td><input type="text" name="szPid" id="szPid" /></td>
                        </tr>
                        <tr>
                            <th colspan="4">
                                <input type="submit" id="btnOK" value="��ѯ" /></th>
                        </tr>
                    </table>
                </div>
           <div class="content">
            <table class="ListTbl">
                <thead>
                     <tr>
                        <th>�γ�����</th>
        <th>ʵ������</th>
        <th>��ʦ</th>
        <th>����</th>
        <th>�༶</th>
     
        <th>��������</th>
        <th>�Ͽ�ʱ��</th>
      
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
                        title: '�������',
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
